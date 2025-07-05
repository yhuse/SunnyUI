/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIIntegerUpDown.cs
 * 文件说明: 数字上下选择框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-08-14: V2.2.7 增加字体调整
 * 2020-12-10: V3.0.9 增加Readonly属性
 * 2022-02-07: V3.1.0 增加圆角控制
 * 2022-02-24: V3.1.1 可以设置按钮大小和颜色
 * 2022-05-05: V3.1.8 增加禁止输入属性
 * 2022-09-16: V3.2.4 增加是否可以双击输入属性
 * 2022-11-12: V3.2.8 修改整数离开判断为实时输入判断
 * 2022-11-12: V3.2.8 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性
 * 2023-01-28: V3.3.1 修改文本框数据输入数据变更事件为MouseLeave
 * 2023-03-24: V3.3.3 删除ForbidInput属性，使用Inputable属性
 * 2023-12-28: V3.6.2 修复设置Style时按钮颜色不一致
 * 2024-08-27: V3.6.9 修改编辑框字体与显示字体一致
 * 2024-08-27: V3.7.0 增加按钮字体图标的偏移位置
 * 2025-03-16: V3.8.2 增加按钮字体图标的大小属性
 * 2025-06-12: V3.8.4 重写控件
 * 2025-06-19: V3.8.5 增加两侧按钮长按快速增加或减少值
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Description("整数上下选择框")]
    public partial class UIIntegerUpDown : UIPanel, IToolTip
    {
        public delegate void OnValueChanged(object sender, int value);
        private readonly UIEdit _edit = new();
        private readonly Timer _timer = new();

        public UIIntegerUpDown()
        {
            InitializeComponent();
            InitializeComponentEnd = true;
            SetStyleFlags(true, true, true);

            ShowText = false;
            base.MinimumSize = new Size(1, 16);

            _edit.AutoSize = false;
            _edit.Top = (Height - _edit.Height) / 2;
            _edit.Left = 4;
            _edit.Width = Width - 8;
            _edit.Text = String.Empty;
            _edit.BorderStyle = BorderStyle.None;
            _edit.TextChanged += Edit_TextChanged;
            _edit.KeyDown += Edit_OnKeyDown;
            _edit.KeyUp += Edit_OnKeyUp;
            _edit.KeyPress += Edit_OnKeyPress;
            _edit.MouseEnter += Edit_MouseEnter;
            _edit.Click += Edit_Click;
            _edit.DoubleClick += Edit_DoubleClick;
            _edit.Leave += Edit_Leave;
            _edit.Validated += Edit_Validated;
            _edit.Validating += Edit_Validating;
            _edit.GotFocus += Edit_GotFocus;
            _edit.LostFocus += Edit_LostFocus;
            _edit.MouseLeave += Edit_MouseLeave;
            _edit.MouseDown += Edit_MouseDown;
            _edit.MouseUp += Edit_MouseUp;
            _edit.MouseMove += Edit_MouseMove;
            _edit.SelectionChanged += Edit_SelectionChanged;
            _edit.MouseClick += Edit_MouseClick;
            _edit.MouseDoubleClick += Edit_MouseDoubleClick;
            _edit.SizeChanged += Edit_SizeChanged;
            _edit.FontChanged += Edit_FontChanged;
            _edit.Type = UITextBox.UIEditType.Integer;
            _edit.Enter += Edit_Enter;

            _edit.Invalidate();
            Controls.Add(_edit);
            fillColor = Color.White;

            TextAlignment = ContentAlignment.MiddleCenter;
            _edit.TextAlign = HorizontalAlignment.Center;

            _lastEditHeight = _edit.Height;
            Width = 150;
            Height = 29;
            SizeChange();
            _editCursor = base.Cursor;
            TextAlignmentChange += UITextBox_TextAlignmentChange;

            _rectHoverColor = UIStyles.Blue.ButtonRectHoverColor;
            _rectPressColor = UIStyles.Blue.ButtonRectPressColor;

            _timer.Interval = 1500;
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!IsPress)
            {
                _timer.Stop();
                _timer.Interval = 1500;
                return;
            }

            _timer.Interval = 100;
            if (_inUp)
            {
                Value += Step;
            }

            if (_inDown)
            {
                Value -= Step;
            }
        }

        public event OnValueChanged ValueChanged;

        protected override void OnMouseLeave(EventArgs e)
        {
            _inUp = _inDown = false;
            IsPress = false;
            IsHover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (_inUp)
            {
                Value += Step;
            }

            if (_inDown)
            {
                Value -= Step;
            }

            _edit.SelectionStart = _edit.Text.Length;
            _edit.Focus();
            base.OnMouseClick(e);
        }

        private void Edit_LostFocus(object sender, EventArgs e)
        {
            LostFocus?.Invoke(this, e);
            Invalidate();
        }

        private void Edit_GotFocus(object sender, EventArgs e)
        {
            GotFocus?.Invoke(this, e);
            Invalidate();
        }

        private int _step = 1;

        [DefaultValue(1)]
        [Description("整数步进值"), Category("SunnyUI")]
        public int Step
        {
            get => _step;
            set => _step = Math.Max(1, value);
        }

        private void Edit_Enter(object sender, EventArgs e)
        {
            // 将光标定位到文本末尾
            if (Control.MouseButtons == MouseButtons.None)
            {
                _edit.SelectionStart = _edit.Text.Length;
                _edit.SelectionLength = 0; // 清除任何文本选中状态
            }
        }

        private void Edit_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoEnter?.Invoke(this, e);
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Add)
            {
                Value += Step;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Subtract)
            {
                Value -= Step;
            }

            KeyDown?.Invoke(this, e);
        }

        /// <summary>
        /// 获取字体颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetSymbolForeColor(bool inArea)
        {
            //文字
            Color color = _symbolColor;
            if (inArea)
            {
                if (IsHover)
                    color = _symbolHoverColor;
                if (IsPress)
                    color = _symbolPressColor;
            }

            return Enabled ? color : _symbolDisableColor;
        }

        /// <summary>
        /// 边框鼠标移上颜色
        /// </summary>
        private Color _rectHoverColor;

        /// <summary>
        /// 边框鼠标按下颜色
        /// </summary>
        private Color _rectPressColor;

        /// <summary>
        /// 鼠标移上时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时边框颜色")]
        public Color RectHoverColor
        {
            get => _rectHoverColor;
            set => SetRectHoverColor(value);
        }

        /// <summary>
        /// 设备边框鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectHoverColor(Color color)
        {
            if (_rectHoverColor != color)
            {
                _rectHoverColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标按下时边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("鼠标按下时边框颜色")]
        public Color RectPressColor
        {
            get => _rectPressColor;
            set => SetRectPressColor(value);
        }

        /// <summary>
        /// 设置边框鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectPressColor(Color color)
        {
            if (_rectPressColor != color)
            {
                _rectPressColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取字体颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetSymbolBackColor(bool inArea)
        {
            //填充
            Color color = rectColor;
            if (inArea)
            {
                if (IsHover)
                    color = _rectHoverColor;
                if (IsPress)
                    color = _rectPressColor;
            }

            return Enabled ? color : rectDisableColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Text.IsInt32()) Text = Value.ToString();

            Rectangle rect1 = new(0, 0, Height, Height - 1);
            using GraphicsPath path1 = rect1.CreateRoundedRectanglePath(Radius, UICornerRadiusSides.LeftTop | UICornerRadiusSides.LeftBottom, RectSize);
            e.Graphics.FillPath(GetSymbolBackColor(_inDown), path1);
            Rectangle rect2 = new(Width - 1 - Height, 0, Height, Height - 1);
            using GraphicsPath path2 = rect2.CreateRoundedRectanglePath(Radius, UICornerRadiusSides.RightBottom | UICornerRadiusSides.RightTop, RectSize);
            e.Graphics.FillPath(GetSymbolBackColor(_inUp), path2);

            e.Graphics.DrawFontImage(361544, SymbolSize, GetSymbolForeColor(_inDown), rect1, 0, 1);
            e.Graphics.DrawFontImage(361543, SymbolSize, GetSymbolForeColor(_inUp), rect2, 0, 1);

            if (Text.IsValid() && NeedDrawDisabledText)
            {
                string text = Text;
                if (PasswordChar > 0)
                {
                    text = PasswordChar.ToString().Repeat(text.Length);
                }

                ContentAlignment textAlign = ContentAlignment.MiddleLeft;
                if (TextAlignment == ContentAlignment.TopCenter || TextAlignment == ContentAlignment.MiddleCenter || TextAlignment == ContentAlignment.BottomCenter)
                    textAlign = ContentAlignment.MiddleCenter;

                if (TextAlignment == ContentAlignment.TopRight || TextAlignment == ContentAlignment.MiddleRight || TextAlignment == ContentAlignment.BottomRight)
                    textAlign = ContentAlignment.MiddleRight;

                e.Graphics.DrawString(text, _edit.Font, ForeDisableColor, _edit.Bounds, textAlign);
            }
        }

        private bool _inUp;
        private bool _inDown;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool inDown = e.Location.X < Height;
            bool inUp = e.Location.X > Width - Height;
            if (_inUp != inUp)
            {
                _inUp = inUp;
                Invalidate();
            }

            if (_inDown != inDown)
            {
                _inDown = inDown;
                Invalidate();
            }
        }

        [Browsable(false)]
        public override string[] FormTranslatorProperties => null;

        private void Edit_FontChanged(object sender, EventArgs e)
        {
            if (!_edit.Multiline)
            {
                int height = _edit.Font.Height;
                _edit.AutoSize = false;
                _edit.Height = height + 2;
                SizeChange();
            }
        }

        int _lastEditHeight;
        private void Edit_SizeChanged(object sender, EventArgs e)
        {
            if (_lastEditHeight != _edit.Height)
            {
                _lastEditHeight = _edit.Height;
                SizeChange();
            }
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            _edit.SetDPIScale();
        }

        [Description("开启后可响应某些触屏的点击事件"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool TouchPressClick
        {
            get => _edit.TouchPressClick;
            set => _edit.TouchPressClick = value;
        }

        private bool _autoSize;
        public new bool AutoSize
        {
            get => _autoSize;
            set
            {
                _autoSize = value;
                SizeChange();
            }
        }

        public new event EventHandler MouseDoubleClick;
        public new event EventHandler MouseClick;

        private void Edit_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDoubleClick?.Invoke(this, e);
        }

        private void Edit_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClick?.Invoke(this, e);
        }

        private void Edit_SelectionChanged(object sender, UITextBoxSelectionArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        public event OnSelectionChanged SelectionChanged;

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            if (_edit != null) _edit.ContextMenuStrip = ContextMenuStrip;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public new Color FillColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    Invalidate();
                }

                AfterSetFillColor(value);
            }
        }

        /// <summary>
        /// 字体只读颜色
        /// </summary>
        [DefaultValue(typeof(Color), "109, 109, 103")]
        public Color ForeReadOnlyColor
        {
            get => foreReadOnlyColor;
            set => SetForeReadOnlyColor(value);
        }

        /// <summary>
        /// 边框只读颜色
        /// </summary>
        [DefaultValue(typeof(Color), "173, 178, 181")]
        public Color RectReadOnlyColor
        {
            get => rectReadOnlyColor;
            set => SetRectReadOnlyColor(value);
        }

        /// <summary>
        /// 填充只读颜色
        /// </summary>
        [DefaultValue(typeof(Color), "244, 244, 244")]
        public Color FillReadOnlyColor
        {
            get => fillReadOnlyColor;
            set => SetFillReadOnlyColor(value);
        }

        private void Edit_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        private void Edit_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        private void Edit_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        private void Edit_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return _edit;
        }

        private void Edit_Validating(object sender, CancelEventArgs e)
        {
            Validating?.Invoke(this, e);
        }

        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event MouseEventHandler MouseMove;
        public new event EventHandler GotFocus;
        public new event EventHandler LostFocus;
        public new event CancelEventHandler Validating;
        public new event EventHandler Validated;
        public new event EventHandler MouseLeave;
        public new event EventHandler DoubleClick;
        public new event EventHandler Click;
        [Browsable(true)]
        public new event EventHandler TextChanged;
        public new event KeyEventHandler KeyDown;
        public new event KeyEventHandler KeyUp;
        public new event KeyPressEventHandler KeyPress;
        public new event EventHandler Leave;

        private void Edit_Validated(object sender, EventArgs e)
        {
            Validated?.Invoke(this, e);
        }

        public new void Focus()
        {
            base.Focus();
            _edit.Focus();
        }

        [Browsable(false)]
        public UIEdit TextBox => _edit;

        private void Edit_Leave(object sender, EventArgs e)
        {
            Leave?.Invoke(this, e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _edit.BackColor = GetFillColor();

            _edit.Visible = true;
            _edit.Enabled = Enabled;
            if (!Enabled)
            {
                if (NeedDrawDisabledText) _edit.Visible = false;
            }
        }

        private bool NeedDrawDisabledText => !Enabled && StyleCustomMode && (ForeDisableColor != Color.FromArgb(109, 109, 103) || FillDisableColor != Color.FromArgb(244, 244, 244));

        public override bool Focused => _edit.Focused;

        [DefaultValue(false)]
        [Description("激活时选中全部文字"), Category("SunnyUI")]
        public bool FocusedSelectAll
        {
            get => _edit.FocusedSelectAll;
            set => _edit.FocusedSelectAll = value;
        }

        private void UITextBox_TextAlignmentChange(object sender, ContentAlignment alignment)
        {
            if (_edit == null) return;
            if (alignment == ContentAlignment.TopLeft || alignment == ContentAlignment.MiddleLeft ||
                alignment == ContentAlignment.BottomLeft)
                _edit.TextAlign = HorizontalAlignment.Left;

            if (alignment == ContentAlignment.TopCenter || alignment == ContentAlignment.MiddleCenter ||
                alignment == ContentAlignment.BottomCenter)
                _edit.TextAlign = HorizontalAlignment.Center;

            if (alignment == ContentAlignment.TopRight || alignment == ContentAlignment.MiddleRight ||
                alignment == ContentAlignment.BottomRight)
                _edit.TextAlign = HorizontalAlignment.Right;
        }

        private void Edit_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick?.Invoke(this, e);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        protected override void OnCursorChanged(EventArgs e)
        {
            base.OnCursorChanged(e);
            _edit.Cursor = Cursor;
        }

        private Cursor _editCursor;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _editCursor = Cursor;
            Cursor = Cursors.Default;
            IsHover = true;
            Invalidate();
        }

        private void Edit_MouseEnter(object sender, EventArgs e)
        {
            Cursor = _editCursor;
            if (FocusedSelectAll)
            {
                SelectAll();
            }
        }

        public void Select(int start, int length)
        {
            _edit.Focus();
            _edit.Select(start, length);
        }

        public void ScrollToCaret()
        {
            _edit.ScrollToCaret();
        }

        private void Edit_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress?.Invoke(this, e);
        }

        public event EventHandler DoEnter;

        private void Edit_OnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }

        public void SelectAll()
        {
            _edit.Focus();
            _edit.SelectAll();
        }

        internal void CheckMaxMin()
        {
            _edit.CheckMaxMin();
        }

        private void Edit_TextChanged(object s, EventArgs e)
        {
            if (IsDisposed) return;
            TextChanged?.Invoke(this, e);
            if (int.TryParse(Text, out var value))
                ValueChanged?.Invoke(this, value);
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (DefaultFontSize < 0 && _edit != null)
            {
                _edit.Font = this.Font;
            }

            Invalidate();
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SizeChange();
        }

        protected override void OnRadiusChanged(int value)
        {
            base.OnRadiusChanged(value);
            SizeChange();
        }

        private void SizeChange()
        {
            if (!InitializeComponentEnd) return;
            if (_edit == null) return;

            //AutoSize自动设置高度
            if (Dock == DockStyle.None && AutoSize)
            {
                if (Height != _edit.Height + 5)
                    Height = _edit.Height + 5;
            }

            //根据字体大小编辑框垂直居中
            if (_edit.Top != (Height - _edit.Height) / 2 + 1)
            {
                _edit.Top = (Height - _edit.Height) / 2 + 1;
            }

            _edit.Left = 4 + Height;
            _edit.Width = Width - _edit.Left * 2;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _edit.Focus();
        }

        public void Clear()
        {
            _edit.Clear();
        }

        [DefaultValue('\0')]
        [Description("密码掩码"), Category("SunnyUI")]
        public char PasswordChar
        {
            get => _edit.PasswordChar;
            set => _edit.PasswordChar = value;
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                _edit.ReadOnly = value;
                _edit.BackColor = GetFillColor();
                Invalidate();
            }
        }

        /// <summary>
        /// 当InputType为数字类型时，能输入的最大值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最大值。"), Category("SunnyUI")]
        [DefaultValue(2147483647D)]
        public double Maximum
        {
            get => _edit.MaxValue;
            set => _edit.MaxValue = value;
        }

        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最小值。"), Category("SunnyUI")]
        [DefaultValue(-2147483648D)]
        public double Minimum
        {
            get => _edit.MinValue;
            set => _edit.MinValue = value;
        }

        [DefaultValue(0)]
        [Description("整型返回值"), Category("SunnyUI")]
        public int Value
        {
            get => _edit.IntValue;
            set
            {
                if (_edit.IntValue == value) return; // 避免重复赋值引起的事件触发

                _edit.IntValue = value;
            }
        }

        [Description("文本返回值"), Category("SunnyUI")]
        [Browsable(true)]
        [DefaultValue("")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get => _edit.Text;
            set => _edit.Text = value;
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ActiveControl = _edit;
            IsPress = true;
            if (_inUp || _inDown)
            {
                _timer.Start();
            }

            Invalidate();
            base.OnMouseDown(e);
        }

        [DefaultValue(32767)]
        public int MaxLength
        {
            get => _edit.MaxLength;
            set => _edit.MaxLength = Math.Max(value, 1);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillColor = uiColor.EditorBackColor;
            foreColor = UIFontColor.Primary;
            rectColor = uiColor.ButtonRectColor;

            _rectHoverColor = uiColor.ButtonRectHoverColor;
            _rectPressColor = uiColor.ButtonRectPressColor;

            _edit.BackColor = GetFillColor();
            _edit.ForeColor = GetForeColor();
            _edit.ForeDisableColor = uiColor.ForeDisableColor;

            _symbolColor = uiColor.ButtonForeColor;
            _symbolHoverColor = uiColor.ButtonForeHoverColor;
            _symbolPressColor = uiColor.ButtonForePressColor;
            _symbolDisableColor = uiColor.ForeDisableColor;
        }

        protected override void SetForeDisableColor(Color color)
        {
            base.SetForeDisableColor(color);
            _edit.ForeDisableColor = color;
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            _edit.ForeColor = GetForeColor();
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            _edit.BackColor = GetFillColor();
        }

        protected override void AfterSetFillReadOnlyColor(Color color)
        {
            base.AfterSetFillReadOnlyColor(color);
            _edit.BackColor = GetFillColor();
        }

        protected override void AfterSetForeReadOnlyColor(Color color)
        {
            base.AfterSetForeReadOnlyColor(color);
            _edit.ForeColor = GetForeColor();
        }

        [DefaultValue(false)]
        public bool AcceptsReturn
        {
            get => _edit.AcceptsReturn;
            set => _edit.AcceptsReturn = value;
        }

        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get => _edit.CharacterCasing;
            set => _edit.CharacterCasing = value;
        }

        public void Paste(string text)
        {
            _edit.Paste(text);
        }

        [DefaultValue(false)]
        public bool AcceptsTab
        {
            get => _edit.AcceptsTab;
            set => _edit.AcceptsTab = value;
        }

        [DefaultValue(false)]
        public bool EnterAsTab
        {
            get => _edit.EnterAsTab;
            set => _edit.EnterAsTab = value;
        }

        [DefaultValue(true)]
        public bool ShortcutsEnabled
        {
            get => _edit.ShortcutsEnabled;
            set => _edit.ShortcutsEnabled = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get => _edit.CanUndo;
        }

        [DefaultValue(true)]
        public bool HideSelection
        {
            get => _edit.HideSelection;
            set => _edit.HideSelection = value;
        }

        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            MergableProperty(false),
            Localizable(true),
            Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))
        ]
        public string[] Lines
        {
            get => _edit.Lines;
            set => _edit.Lines = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get => _edit.Modified;
            set => _edit.Modified = value;
        }

        [
            Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public int PreferredHeight
        {
            get => _edit.PreferredHeight;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get => _edit.SelectedText;
            set => _edit.SelectedText = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => _edit.SelectionLength;
            set => _edit.SelectionLength = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => _edit.SelectionStart;
            set => _edit.SelectionStart = value;
        }

        [Browsable(false)]
        public int TextLength
        {
            get => _edit.TextLength;
        }

        public void AppendText(string text)
        {
            _edit.AppendText(text);
        }

        public void ClearUndo()
        {
            _edit.ClearUndo();
        }

        public void Copy()
        {
            _edit.Copy();
        }

        public void Cut()
        {
            _edit.Cut();
        }

        public void Paste()
        {
            _edit.Paste();
        }

        public char GetCharFromPosition(Point pt)
        {
            return _edit.GetCharFromPosition(pt);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            return _edit.GetCharIndexFromPosition(pt);
        }

        public int GetLineFromCharIndex(int index)
        {
            return _edit.GetLineFromCharIndex(index);
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return _edit.GetPositionFromCharIndex(index);
        }

        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return _edit.GetFirstCharIndexFromLine(lineNumber);
        }

        public int GetFirstCharIndexOfCurrentLine()
        {
            return _edit.GetFirstCharIndexOfCurrentLine();
        }

        public void DeselectAll()
        {
            _edit.DeselectAll();
        }

        public void Undo()
        {
            _edit.Undo();
        }

        private int _symbolSize = 24;

        /// <summary>
        /// 字体图标大小
        /// </summary>
        [DefaultValue(24)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, UIGlobal.EditorMaxHeight);
                SizeChange();
                Invalidate();
            }
        }

        private Point _symbolOffset = new(0, 1);

        /// <summary>
        /// 字体图标的偏移位置
        /// </summary>
        [DefaultValue(typeof(Point), "0, 1")]
        [Description("字体图标的偏移位置"), Category("SunnyUI")]
        public Point SymbolOffset
        {
            get => _symbolOffset;
            set
            {
                _symbolOffset = value;
                Invalidate();
            }
        }

        private Color _symbolColor = Color.White;

        /// <summary>
        /// 字体图标颜色
        /// </summary>
        [Description("图标颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color SymbolColor
        {
            get => _symbolColor;
            set
            {
                if (_symbolColor != value)
                {
                    _symbolColor = value;
                    Invalidate();
                }
            }
        }

        private Color _symbolHoverColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("图标鼠标移上时字体颜色")]
        public Color SymbolHoverColor
        {
            get => _symbolHoverColor;
            set
            {
                if (_symbolHoverColor != value)
                {
                    _symbolHoverColor = value;
                    Invalidate();
                }
            }
        }

        private Color _symbolPressColor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("图标鼠标按下时字体颜色")]
        public Color SymbolPressColor
        {
            get => _symbolPressColor;
            set
            {
                if (_symbolPressColor != value)
                {
                    _symbolPressColor = value;
                    Invalidate();
                }
            }
        }

        private Color _symbolDisableColor = Color.FromArgb(109, 109, 103);
        [DefaultValue(typeof(Color), "109, 109, 103"), Category("SunnyUI")]
        [Description("图标不可用时字体颜色")]
        public Color SymbolDisableColor
        {
            get => _symbolDisableColor;
            set
            {
                if (_symbolDisableColor != value)
                {
                    _symbolDisableColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }
    }
}