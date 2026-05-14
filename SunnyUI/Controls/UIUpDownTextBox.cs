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
 * 文件名称: UIUpDownTextBox.cs
 * 文件说明: 含有上下按钮的输入框
 * 当前版本: V3.9
 * 创建日期: 2026-03-31
 *
 * 2026-03-31: V3.9.3 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("输入框控件")]
    public partial class UIUpDownTextBox : UIPanel, IToolTip
    {
        private readonly UIEdit _edit = new UIEdit();
        private readonly Timer _timer = new Timer();
        private readonly Timer _valueTimer = new Timer();

        public UIUpDownTextBox()
        {
            InitializeComponent();
            InitializeComponentEnd = true;
            SetStyleFlags(true, true, true);

            ShowText = false;
            MinimumSize = new Size(1, 16);

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
            _edit.Enter += Edit_Enter;
            _edit.Type = UITextBox.UIEditType.Integer;

            _edit.Invalidate();
            Controls.Add(_edit);
            fillColor = Color.White;

            TextAlignment = ContentAlignment.MiddleRight;
            _edit.TextAlign = HorizontalAlignment.Right;

            _lastEditHeight = _edit.Height;
            Width = 150;
            Height = 29;

            AlwaysShow = true;

            _editCursor = Cursor;
            TextAlignmentChange += UITextBox_TextAlignmentChange;
            _timer.Interval = 500; // 设置定时器间隔
            _timer.Tick += (s, e) =>
            {
                _timer.Stop();
                if (_edit.Focused)
                {

                    return; // 如果编辑框仍然有焦点，则不执行后续操作
                }
                SizeChange();
                _inUp = _inDown = false;
                Invalidate();
            };

            _valueTimer.Interval = 1000; // 设置数值调整的持续时间间隔
            _valueTimer.Tick += (s, e) =>
            {
                _valueTimer.Interval = 200;
                ButtonMouseClick();
            };
        }

        private void Edit_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoEnter?.Invoke(this, e);
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Add)
            {
                if (Type == UITextBox.UIEditType.Integer)
                {
                    _edit.IntValue += IntStep;
                }

                if (Type == UITextBox.UIEditType.Double)
                {
                    _edit.DoubleValue += DoubleStep;
                }
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Subtract)
            {
                if (Type == UITextBox.UIEditType.Integer)
                {
                    _edit.IntValue -= IntStep;
                }
                if (Type == UITextBox.UIEditType.Double)
                {
                    _edit.DoubleValue -= DoubleStep;
                }
            }

            //_edit.SelectionStart = _edit.Text.Length;
            //_edit.SelectionLength = 0; // 清除任何文本选中状态
            KeyDown?.Invoke(this, e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _inUp = _inDown = false;

            _valueTimer.Stop();
            _valueTimer.Interval = 1000;
            Invalidate();
            base.OnMouseLeave(e);
        }

        public event EventHandler DoEnter;

        private void Edit_OnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }

        private int _intStep = 1;

        [DefaultValue(1)]
        [Description("整数步进值"), Category("SunnyUI")]
        public int IntStep
        {
            get => _intStep;
            set => _intStep = Math.Max(1, value);
        }

        [DefaultValue(1)]
        [Description("浮点数步进值"), Category("SunnyUI")]
        public double DoubleStep { get; set; } = 1;

        private void Edit_Enter(object sender, EventArgs e)
        {
            // 将光标定位到文本末尾
            if (Control.MouseButtons == MouseButtons.None)
            {
                _edit.SelectionStart = _edit.Text.Length;
                _edit.SelectionLength = 0; // 清除任何文本选中状态
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            ButtonMouseClick();
            base.OnMouseClick(e);
        }

        private void ButtonMouseClick()
        {
            if (_inUp)
            {
                if (Type == UITextBox.UIEditType.Integer)
                {
                    _edit.IntValue += IntStep;
                }

                if (Type == UITextBox.UIEditType.Double)
                {
                    _edit.DoubleValue += DoubleStep;
                }
            }

            if (_inDown)
            {
                if (Type == UITextBox.UIEditType.Integer)
                {
                    _edit.IntValue -= IntStep;
                }

                if (Type == UITextBox.UIEditType.Double)
                {
                    _edit.DoubleValue -= DoubleStep;
                }
            }

            _edit.SelectionStart = _edit.Text.Length;
            _edit.Focus();
        }

        private void Edit_LostFocus(object sender, EventArgs e)
        {
            LostFocus?.Invoke(this, e);
            if (!AlwaysShow) _timer.Start();
        }

        private void Edit_GotFocus(object sender, EventArgs e)
        {
            GotFocus?.Invoke(this, e);
            SizeChange();
            Invalidate();
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_edit.Type == UITextBox.UIEditType.Double)
            {
                if (!Text.IsDouble()) Text = DoubleValue.ToString("F" + DecimalPlaces);
            }

            if (_edit.Type == UITextBox.UIEditType.Integer)
            {
                if (!Text.IsInt()) Text = IntValue.ToString();
            }

            if (AlwaysShow || (!DesignMode && _edit.Focused))
            {
                e.Graphics.DrawFontImage(66, 24, _inUp ? RectColor : RectDisableColor, new RectangleF(Width - 24, 0, 24, Height / 2.0f), 0, Height / 8);
                e.Graphics.DrawFontImage(67, 24, _inDown ? RectColor : RectDisableColor, new RectangleF(Width - 24, Height / 2.0f, 24, Height / 2.0f), 0, -Height / 8 + 1);
            }

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

        [Description("是否始终显示上下按钮"), DefaultValue(true)]
        public bool AlwaysShow
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                SizeChange();
                Invalidate();
            }
        }

        //protected new Color GetRectColor()
        //{
        //    return Enabled ? (isReadOnly ? rectReadOnlyColor : (_edit.Focused ? rectColor : RectDisableColor)) : rectDisableColor;
        //}

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            if (RectSides == ToolStripStatusLabelBorderSides.None)
            {
                return;
            }

            if (RadiusSides == UICornerRadiusSides.None || Radius == 0)
            {
                //IsRadius为False时，显示左侧边线
                bool ShowRectLeft = RectSides.GetValue(ToolStripStatusLabelBorderSides.Left);
                //IsRadius为False时，显示上侧边线
                bool ShowRectTop = RectSides.GetValue(ToolStripStatusLabelBorderSides.Top);
                //IsRadius为False时，显示右侧边线
                bool ShowRectRight = RectSides.GetValue(ToolStripStatusLabelBorderSides.Right);
                //IsRadius为False时，显示下侧边线
                bool ShowRectBottom = RectSides.GetValue(ToolStripStatusLabelBorderSides.Bottom);

                if (ShowRectLeft)
                    g.DrawLine(GetRectColor(), RectSize - 1, 0, RectSize - 1, Height, false);
                if (ShowRectTop)
                    g.DrawLine(GetRectColor(), 0, RectSize - 1, Width, RectSize - 1, false);
                if (ShowRectRight)
                    g.DrawLine(GetRectColor(), Width - 1, 0, Width - 1, Height, false);
                if (ShowRectBottom)
                    g.DrawLine(GetRectColor(), 0, Height - 1, Width, Height - 1, false);
            }
            else
            {
                g.DrawPath(GetRectColor(), path, true);
                PaintRectDisableSides(g);
            }
        }

        private void PaintRectDisableSides(Graphics g)
        {
            //IsRadius为False时，显示左侧边线
            bool ShowRectLeft = RectSides.GetValue(ToolStripStatusLabelBorderSides.Left);
            //IsRadius为False时，显示上侧边线
            bool ShowRectTop = RectSides.GetValue(ToolStripStatusLabelBorderSides.Top);
            //IsRadius为False时，显示右侧边线
            bool ShowRectRight = RectSides.GetValue(ToolStripStatusLabelBorderSides.Right);
            //IsRadius为False时，显示下侧边线
            bool ShowRectBottom = RectSides.GetValue(ToolStripStatusLabelBorderSides.Bottom);

            //IsRadius为True时，显示左上圆角
            bool RadiusLeftTop = RadiusSides.GetValue(UICornerRadiusSides.LeftTop);
            //IsRadius为True时，显示左下圆角
            bool RadiusLeftBottom = RadiusSides.GetValue(UICornerRadiusSides.LeftBottom);
            //IsRadius为True时，显示右上圆角
            bool RadiusRightTop = RadiusSides.GetValue(UICornerRadiusSides.RightTop);
            //IsRadius为True时，显示右下圆角
            bool RadiusRightBottom = RadiusSides.GetValue(UICornerRadiusSides.RightBottom);

            var ShowRadius = RadiusSides > 0 && Radius > 0;//肯定少有一个角显示圆角
            if (!ShowRadius) return;

            if (!ShowRectLeft && !RadiusLeftBottom && !RadiusLeftTop)
            {
                g.DrawLine(GetFillColor(), RectSize - 1, 0, RectSize - 1, Height, false);
            }

            if (!ShowRectTop && !RadiusRightTop && !RadiusLeftTop)
            {
                g.DrawLine(GetFillColor(), 0, RectSize - 1, Width, RectSize - 1, false);
            }

            if (!ShowRectRight && !RadiusRightTop && !RadiusRightBottom)
            {
                g.DrawLine(GetFillColor(), Width - 1, 0, Width - 1, Height, false);
            }

            if (!ShowRectBottom && !RadiusLeftBottom && !RadiusRightBottom)
            {
                g.DrawLine(GetFillColor(), 0, Height - 1, Width, Height - 1, false);
            }
        }

        private bool _inUp;
        private bool _inDown;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool inUp = e.Location.InRect(new RectangleF(Width - 22, 0, 22, Height / 2.0f));
            bool inDown = e.Location.InRect(new RectangleF(Width - 22, Height / 2.0f, 22, Height / 2.0f));
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

            if (!(_inUp || _inDown))
            {
                _valueTimer.Stop();
                _valueTimer.Interval = 1000;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _valueTimer.Stop();
            _valueTimer.Interval = 1000;
            base.OnMouseUp(e);
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

        private int _lastEditHeight;
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
        }

        private void Edit_MouseEnter(object sender, EventArgs e)
        {
            Cursor = _editCursor;
            if (FocusedSelectAll)
            {
                SelectAll();
            }
        }

        [DefaultValue(true)]
        public bool WordWarp
        {
            get => _edit.WordWrap;
            set => _edit.WordWrap = value;
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

        [DefaultValue(null)]
        [Description("水印文字"), Category("SunnyUI")]
        public string Watermark
        {
            get => _edit.Watermark;
            set => _edit.Watermark = value;
        }

        [DefaultValue(typeof(Color), "Gray")]
        [Description("水印文字颜色"), Category("SunnyUI")]
        public Color WatermarkColor
        {
            get => _edit.WaterMarkColor;
            set => _edit.WaterMarkColor = value;
        }

        [DefaultValue(typeof(Color), "Gray")]
        [Description("水印文字激活颜色"), Category("SunnyUI")]
        public Color WatermarkActiveColor
        {
            get => _edit.WaterMarkActiveForeColor;
            set => _edit.WaterMarkActiveForeColor = value;
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

            int added = Radius <= 5 ? 0 : (Radius - 5) / 2;

            _edit.Left = 4;
            _edit.Width = Width - 10;
            _edit.Left = _edit.Left + added;
            _edit.Width = _edit.Width - added * 2;

            if (_edit.Focused || AlwaysShow)
            {
                _edit.Width = _edit.Width - _buttonWidth - 4;
            }
        }

        private readonly int _buttonWidth = 24;

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

        [Description("输入类型"), Category("SunnyUI")]
        [DefaultValue(UITextBox.UIEditType.Double)]
        public UITextBox.UIEditType Type
        {
            get => _edit.Type;
            set => _edit.Type = value;
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

        [DefaultValue(0.00)]
        [Description("浮点返回值"), Category("SunnyUI")]
        public double DoubleValue
        {
            get => _edit.DoubleValue;
            set => _edit.DoubleValue = value;
        }

        [DefaultValue(0)]
        [Description("整型返回值"), Category("SunnyUI")]
        public int IntValue
        {
            get => _edit.IntValue;
            set => _edit.IntValue = value;
        }

        [Description("文本返回值"), Category("SunnyUI")]
        [Browsable(true)]
        [DefaultValue("")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public override string Text
        {
            get => _edit.Text;
            set => _edit.Text = value;
        }

        [Description("浮点数，显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => _edit.DecLength;
            set => _edit.DecLength = Math.Max(value, 0);
        }

        [DefaultValue(false)]
        [Description("整型或浮点输入时，是否可空显示"), Category("SunnyUI")]
        public bool CanEmpty
        {
            get => _edit.CanEmpty;
            set => _edit.CanEmpty = value;
        }

        public void Empty()
        {
            if (_edit.CanEmpty)
                _edit.Text = "";
        }

        public bool IsEmpty => _edit.Text == "";

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ActiveControl = _edit;

            if (_inUp || _inDown)
            {
                _valueTimer.Start();
            }
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
            foreColor = uiColor.EditorForeColor;
            _edit.BackColor = GetFillColor();
            _edit.ForeColor = GetForeColor();
            _edit.ForeDisableColor = uiColor.ForeDisableColor;
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

        [DefaultValue(AutoCompleteMode.None), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => _edit.AutoCompleteMode;
            set => _edit.AutoCompleteMode = value;
        }

        [
            DefaultValue(AutoCompleteSource.None),
            TypeConverter(typeof(TextBoxAutoCompleteSourceConverter)),
            Browsable(true),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public AutoCompleteSource AutoCompleteSource
        {
            get => _edit.AutoCompleteSource;
            set => _edit.AutoCompleteSource = value;
        }

        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Localizable(true),
        Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)),
            Browsable(true),
            EditorBrowsable(EditorBrowsableState.Always)
        ]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => _edit.AutoCompleteCustomSource;
            set => _edit.AutoCompleteCustomSource = value;
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

        internal class TextBoxAutoCompleteSourceConverter : EnumConverter
        {
            public TextBoxAutoCompleteSourceConverter(Type type) : base(type)
            {
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                StandardValuesCollection values = base.GetStandardValues(context);
                ArrayList list = new ArrayList();
                int count = values.Count;
                for (int i = 0; i < count; i++)
                {
                    string currentItemText = values[i].ToString();
                    if (!currentItemText.Equals("ListItems"))
                    {
                        list.Add(values[i]);
                    }
                }

                return new StandardValuesCollection(list);
            }
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
            Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))
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
    }
}