/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIDropControl.cs
 * 文件说明: 下拉框基类
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-07-05: V2.2.6 更新KeyDown、KeyUp、KeyPress事件。
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public enum UIDropDownStyle
    {
        /// <summary>
        /// 通过单击下箭头指定显示列表，并指定文本部分可编辑。 这表示用户可以输入新的值，而不仅限于选择列表中现有的值。
        /// </summary>
        DropDown,
        /// <summary>
        /// 通过单击下箭头指定显示列表，并指定文本部分不可编辑。 这表示用户不能输入新的值。 只能选择列表中已有的值。
        /// </summary>
        DropDownList
    }

    [ToolboxItem(false)]
    public partial class UIDropControl : UIPanel
    {
        public UIDropControl()
        {
            InitializeComponent();
            SetStyleFlags();
            Padding = new Padding(0, 0, 30, 2);

            edit.Font = UIFontColor.Font;
            edit.Left = 3;
            edit.Top = 3;
            edit.Text = String.Empty;
            edit.ForeColor = UIFontColor.Primary;
            edit.BorderStyle = BorderStyle.None;
            edit.TextChanged += EditTextChanged;
            edit.KeyDown += EditOnKeyDown;
            edit.KeyUp += EditOnKeyUp;
            edit.KeyPress += EditOnKeyPress;
            edit.LostFocus += Edit_LostFocus;
            edit.Invalidate();
            Controls.Add(edit);

            TextAlignment = ContentAlignment.MiddleLeft;
            fillColor = Color.White;
            edit.BackColor = Color.White;
            MouseMove += UIDropControl_MouseMove;
        }

        [Browsable(false)]
        public TextBox TextBox => edit;

        protected Point MouseLocation;

        private void UIDropControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Enabled)
            {
                if (Radius == 0 || RadiusSides == UICornerRadiusSides.None)
                    e.Graphics.DrawRectangle(RectColor, 0, 0, Width - 1, Height - 1);
                else
                    e.Graphics.DrawRoundRectangle(RectColor, 0, 0, Width, Height, Radius);

                edit.BackColor = Color.White;
            }
            else
            {
                if (Radius == 0 || RadiusSides == UICornerRadiusSides.None)
                    e.Graphics.DrawRectangle(RectDisableColor, 0, 0, Width - 1, Height - 1);
                else
                    e.Graphics.DrawRoundRectangle(RectDisableColor, 0, 0, Width, Height, Radius);

                edit.BackColor = GetFillColor();
            }
        }

        private void Edit_LostFocus(object sender, EventArgs e)
        {
            EditorLostFocus?.Invoke(sender, e);
        }

        public event EventHandler EditorLostFocus;

        public new event KeyEventHandler KeyDown;

        public new event KeyEventHandler KeyUp;

        public new event KeyPressEventHandler KeyPress;

        [Browsable(true)]
        public new event EventHandler TextChanged;

        private void EditOnKeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress?.Invoke(sender, e);
        }

        public event EventHandler DoEnter;

        private void EditOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoEnter?.Invoke(sender, e);
            }

            KeyDown?.Invoke(sender, e);
        }

        private void EditOnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        [DefaultValue(null)]
        [Description("水印文字"), Category("SunnyUI")]
        public string Watermark
        {
            get => edit.Watermark;
            set => edit.Watermark = value;
        }

        private UIDropDown itemForm;

        protected UIDropDown ItemForm
        {
            get
            {
                if (itemForm == null)
                {
                    CreateInstance();

                    if (itemForm != null)
                    {
                        itemForm.ValueChanged += ItemForm_ValueChanged;
                        itemForm.VisibleChanged += ItemForm_VisibleChanged;
                        itemForm.Closed += ItemForm_Closed;
                    }
                }

                return itemForm;
            }
            set => itemForm = value;
        }

        private void ItemForm_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            DropDownClosed?.Invoke(this, null);
        }

        private void ItemForm_VisibleChanged(object sender, EventArgs e)
        {
            dropSymbol = SymbolNormal;

            if (DroppedDown)
            {
                dropSymbol = SymbolDropDown;
            }

            Invalidate();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown => itemForm != null && itemForm.Visible;

        private int symbolNormal = 61703;
        private int dropSymbol = 61703;

        [DefaultValue(61703)]
        [Description("正常显示时字体图标"), Category("SunnyUI")]
        public int SymbolNormal
        {
            get => symbolNormal;
            set
            {
                symbolNormal = value;
                dropSymbol = value;
            }
        }

        [DefaultValue(61702)]
        [Description("下拉框显示时字体图标"), Category("SunnyUI")]
        public int SymbolDropDown { get; set; } = 61702;

        protected virtual void CreateInstance()
        {
        }

        protected virtual void ItemForm_ValueChanged(object sender, object value)
        {
        }

        protected virtual int CalcItemFormHeight()
        {
            return 200;
        }

        private UIDropDownStyle _dropDownStyle = UIDropDownStyle.DropDown;

        [DefaultValue(UIDropDownStyle.DropDown)]
        [Description("下拉框显示样式"), Category("SunnyUI")]
        public UIDropDownStyle DropDownStyle
        {
            get => _dropDownStyle;
            set
            {
                if (_dropDownStyle != value)
                {
                    _dropDownStyle = value;
                    edit.Visible = value == UIDropDownStyle.DropDown;
                    Invalidate();
                }
            }
        }

        public event EventHandler ButtonClick;

        protected readonly TextBoxEx edit = new TextBoxEx();

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            edit.Text = Text;
            Invalidate();
        }

        private void EditTextChanged(object s, EventArgs e)
        {
            Text = edit.Text;
            TextChanged?.Invoke(s, e);
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            edit.Font = Font;
            Invalidate();
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            if (Padding.Right < 30 || Padding.Bottom < 2)
            {
                Padding = new Padding(Padding.Left, Padding.Top, Padding.Right < 30 ? 30 : Padding.Right, Padding.Bottom < 2 ? 2 : Padding.Bottom);
            }
            base.OnPaddingChanged(e);
            SizeChange();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SizeChange();
        }

        private void SizeChange()
        {
            edit.Top = (Height - edit.Height) / 2;
            edit.Left = 3 + Padding.Left;
            edit.Width = Width - Padding.Left - Padding.Right;
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            SizeChange();

            if (!edit.Visible)
            {
                base.OnPaintFore(g, path);
                g.FillRoundRectangle(GetFillColor(), new Rectangle(Width - 27, edit.Top, 26, edit.Height), Radius, false);
                g.DrawRoundRectangle(rectColor, new Rectangle(0, 0, Width, Height), Radius, true);
            }

            g.FillRoundRectangle(GetFillColor(), new Rectangle(Width - 27, edit.Top, 25, edit.Height), Radius);
            Color color = GetRectColor();
            SizeF sf = g.GetFontImageSize(dropSymbol, 24);
            g.DrawFontImage(dropSymbol, 24, color, Width - 28 + (12 - sf.Width / 2.0f), (Height - sf.Height) / 2.0f);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            edit.Focus();
        }

        public void Clear()
        {
            edit.Clear();
        }

        [DefaultValue('\0')]
        [Description("m"), Category("SunnyUI")]
        public char PasswordChar
        {
            get => edit.PasswordChar;
            set => edit.PasswordChar = value;
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly
        {
            get => edit.ReadOnly;
            set
            {
                edit.ReadOnly = value;
                edit.BackColor = Color.White;
            }
        }

        [CategoryAttribute("文字"), Browsable(true)]
        [DefaultValue("")]
        public override string Text
        {
            get => edit.Text;
            set => edit.Text = value;
        }

        [Browsable(false)]
        public bool IsEmpty => edit.Text == "";

        protected override void OnMouseDown(MouseEventArgs e)
        {
            ActiveControl = edit;
        }

        [DefaultValue(32767)]
        public int MaxLength
        {
            get => edit.MaxLength;
            set => edit.MaxLength = Math.Max(value, 1);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => edit.SelectionLength;
            set => edit.SelectionLength = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => edit.SelectionStart;
            set => edit.SelectionStart = value;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            foreColor = uiColor.DropDownControlColor;
            edit.BackColor = fillColor = Color.White;
            Invalidate();
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            edit.BackColor = fillColor;
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            edit.ForeColor = foreColor;
        }

        protected bool fullControlSelect;

        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                if (ItemForm != null)
                {
                    ItemForm.SetRectColor(rectColor);
                    ItemForm.SetFillColor(fillColor);
                    ItemForm.SetForeColor(foreColor);
                    ItemForm.SetStyle(UIStyles.ActiveStyleColor);
                }

                DropDown?.Invoke(this, e);


                if (fullControlSelect || MouseLocation.X > Width - 30)
                {
                    ButtonClick?.Invoke(this, e);
                }
                else
                {
                    base.OnClick(e);
                }
            }
        }

        public event EventHandler DropDown;

        public event EventHandler DropDownClosed;

        public void Select(int start, int length)
        {
            edit.Select(start, length);
        }

        public void SelectAll()
        {
            edit.SelectAll();
        }

        protected class TextBoxEx : TextBox
        {
            private string watermark;

            [DefaultValue(null)]
            public string Watermark
            {
                get => watermark;
                set
                {
                    watermark = value;
                    Win32.User.SendMessage(Handle, 0x1501, (int)IntPtr.Zero, value);
                }
            }
        }
    }
}