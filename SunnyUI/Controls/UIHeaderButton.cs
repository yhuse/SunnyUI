/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIHeaderButton.cs
 * 文件说明: 顶部图标按钮
 * 当前版本: V3.0
 * 创建日期: 2021-02-10
 *
 * 2021-02-10: V3.0.1 增加文件说明
******************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI.Controls
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class UIHeaderButton : UIControl, IButtonControl
    {
        public UIHeaderButton()
        {
            Size = new Size(100, 88);
            ShowText = false;
            ShowRect = false;
            Radius = 0;
            RadiusSides = UICornerRadiusSides.None;
            RectSides = ToolStripStatusLabelBorderSides.None;
            Padding = new Padding(0, 8, 0, 3);

            foreHoverColor = UIStyles.Blue.ButtonForeHoverColor;
            forePressColor = UIStyles.Blue.ButtonForePressColor;
            foreSelectedColor = UIStyles.Blue.ButtonForeSelectedColor;
            fillHoverColor = UIStyles.Blue.ButtonFillHoverColor;
            fillPressColor = UIStyles.Blue.ButtonFillPressColor;
            fillSelectedColor = UIStyles.Blue.ButtonFillSelectedColor;
            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);
        }

        private bool isClick;

        public void PerformClick()
        {
            if (isClick) return;
            if (CanSelect && Enabled)
            {
                isClick = true;
                OnClick(EventArgs.Empty);
                isClick = false;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            Focus();

            List<UIHeaderButton> buttons = Parent.GetControls<UIHeaderButton>();
            foreach (var button in buttons)
            {
                button.Selected = false;
            }

            Selected = true;
            TabControl?.SelectPage(PageIndex);

            base.OnClick(e);
        }

        private int _symbolSize = 48;

        [DefaultValue(48)]
        [Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        [DefaultValue(0)]
        [Description("多页面框架的页面索引"), Category("SunnyUI")]
        public int PageIndex { get; set; }

        public Color symbolColor = Color.White;

        [DefaultValue(typeof(Color), "White")]
        [Description("字体图标颜色"), Category("SunnyUI")]
        public Color SymbolColor
        {
            get => symbolColor;
            set
            {
                symbolColor = value;
                Invalidate();
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillHoverColor = uiColor.ButtonFillHoverColor;
            foreHoverColor = uiColor.ButtonForeHoverColor;

            fillPressColor = uiColor.ButtonFillPressColor;
            forePressColor = uiColor.ButtonForePressColor;

            fillSelectedColor = uiColor.ButtonFillSelectedColor;
            foreSelectedColor = uiColor.ButtonForeSelectedColor;

            Invalidate();
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        [DefaultValue(typeof(Color), "244, 244, 244"), Category("SunnyUI")]
        [Description("不可用时填充颜色")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
        }

        [DefaultValue(typeof(Color), "109, 109, 103"), Category("SunnyUI")]
        [Description("不可用时字体颜色")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        [DefaultValue(typeof(Color), "111, 168, 255"), Category("SunnyUI")]
        [Description("鼠标移上时填充颜色")]
        public Color FillHoverColor
        {
            get => fillHoverColor;
            set => SetFillHoveColor(value);
        }

        [DefaultValue(typeof(Color), "74, 131, 229"), Category("SunnyUI")]
        [Description("鼠标按下时填充颜色")]
        public Color FillPressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标移上时字体颜色")]
        public Color ForeHoverColor
        {
            get => foreHoverColor;
            set => SetForeHoveColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标按下时字体颜色")]
        public Color ForePressColor
        {
            get => forePressColor;
            set => SetForePressColor(value);
        }

        [DefaultValue(typeof(Color), "74, 131, 229"), Category("SunnyUI")]
        [Description("选中时填充颜色")]
        public Color FillSelectedColor
        {
            get => fillSelectedColor;
            set => SetFillSelectedColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("选中时字体颜色")]
        public Color ForeSelectedColor
        {
            get => foreSelectedColor;
            set => SetForeSelectedColor(value);
        }

        [DefaultValue(null)]
        [Description("图片"), Category("SunnyUI")]
        public Image Image { get; set; }

        private int _symbol = FontAwesomeIcons.fa_check;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor(typeof(UIImagePropertyEditor), typeof(UITypeEditor))]
        [DefaultValue(61452)]
        [Description("字体图标"), Category("SunnyUI")]
        public int Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                Invalidate();
            }
        }

        private int imageTop;

        [DefaultValue(0)]
        [Description("图片距离顶部距离"), Category("SunnyUI")]
        public int ImageTop
        {
            get => imageTop;
            set
            {
                imageTop = value;
                Invalidate();
            }
        }

        private Point symbolOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("字体图标的偏移位置"), Category("SunnyUI")]
        public Point SymbolOffset
        {
            get => symbolOffset;
            set
            {
                symbolOffset = value;
                Invalidate();
            }
        }

        private Color circleColor = Color.Bisque;

        [DefaultValue(typeof(Color), "Bisque")]
        [Description("字体图标背景颜色"), Category("SunnyUI")]
        public Color CircleColor
        {
            get => circleColor;
            set
            {
                circleColor = value;
                Invalidate();
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            Invalidate();
        }

        private int circleSize = 50;

        [DefaultValue(50)]
        [Description("字体图标背景大小"), Category("SunnyUI")]
        public int CircleSize
        {
            get => circleSize;
            set
            {
                circleSize = value;
                Invalidate();
            }
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (!selected)
            {
                Color color = GetFillColor();
                g.Clear(color);
            }
            else
            {
                g.Clear(fillSelectedColor);
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        [DefaultValue(false)]
        [Description("是否选中"), Category("SunnyUI")]
        public bool Selected
        {
            get => selected;
            set
            {
                if (selected != value)
                {
                    selected = value;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsPress = false;
            IsHover = false;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsHover = true;
            Invalidate();
        }

        public void NotifyDefault(bool value)
        {
        }

        [DefaultValue(DialogResult.None)]
        [Description("指定标识符以指示对话框的返回值"), Category("SunnyUI")]
        public DialogResult DialogResult { get; set; } = DialogResult.None;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Focused && e.KeyCode == Keys.Space)
            {
                IsPress = true;
                Invalidate();
                PerformClick();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            IsPress = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //重绘父类
            base.OnPaint(e);


            SizeF ImageSize = new SizeF(0, 0);
            if (Symbol > 0)
                ImageSize = e.Graphics.GetFontImageSize(Symbol, SymbolSize);
            if (Image != null)
                ImageSize = Image.Size;

            //字体图标
            if (Symbol > 0 && Image == null)
            {
                e.Graphics.FillEllipse(CircleColor, (Width - CircleSize) / 2.0f, Padding.Top, CircleSize, CircleSize);
                e.Graphics.DrawFontImage(Symbol, SymbolSize, SymbolColor,
                    new RectangleF(
                        symbolOffset.X + (Width - CircleSize) / 2.0f,
                        symbolOffset.Y + Padding.Top,
                        CircleSize,
                        CircleSize));
            }
            else if (Image != null)
            {
                e.Graphics.DrawImage(Image, (Width - ImageSize.Width) / 2.0f, ImageTop, ImageSize.Width, ImageSize.Height);
            }

            Color color = GetForeColor();
            SizeF sf = e.Graphics.MeasureString(Text, Font);
            e.Graphics.DrawString(Text, Font, color, (Width - sf.Width) / 2, Height - Padding.Bottom - sf.Height);
        }

        [DefaultValue(null)]
        [Description("关联的TabControl"), Category("SunnyUI")]
        public UITabControl TabControl { get; set; }
    }
}