/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIButton.cs
 * 文件说明: 按钮
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-07-26: V2.2.6 增加Selected及选中颜色配置
 * 2020-08-22: V2.2.7 空格键按下press背景效果，添加双击事件，解决因快速点击导致过慢问题
 * 2020-09-14: V2.2.7 Tips颜色可设置
 * 2021-07-18: V3.0.5 增加ShowFocusColor，用来显示Focus状态
 * 2021-12-11: V3.0.9 增加了渐变色
 * 2022-02-26: V3.1.1 增加了AutoSize属性
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-03-31: V3.1.2 是否显示浅色背景
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// ReSharper disable All

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public class UIButton : UIControl, IButtonControl
    {
        public UIButton()
        {
            SetStyleFlags();
            TabStop = true;
            Width = 100;
            Height = 35;
            Cursor = Cursors.Hand;

            foreHoverColor = UIStyles.Blue.ButtonForeHoverColor;
            forePressColor = UIStyles.Blue.ButtonForePressColor;
            foreSelectedColor = UIStyles.Blue.ButtonForeSelectedColor;

            rectHoverColor = UIStyles.Blue.ButtonRectHoverColor;
            rectPressColor = UIStyles.Blue.ButtonRectPressColor;
            rectSelectedColor = UIStyles.Blue.ButtonRectSelectedColor;

            fillHoverColor = UIStyles.Blue.ButtonFillHoverColor;
            fillPressColor = UIStyles.Blue.ButtonFillPressColor;
            fillSelectedColor = UIStyles.Blue.ButtonFillSelectedColor;
            SetStyle(ControlStyles.StandardDoubleClick, UseDoubleClick);
        }

        [DefaultValue(false)]
        [Description("是否显示浅色背景"), Category("SunnyUI")]
        public bool LightStyle
        {
            get => lightStyle;
            set
            {
                if (lightStyle != value)
                {
                    lightStyle = value;
                    Invalidate();
                }
            }
        }

        private bool autoSize;

        [Browsable(true), DefaultValue(false)]
        [Description("自动大小"), Category("SunnyUI")]
        public override bool AutoSize
        {
            get => autoSize;
            set
            {
                autoSize = value;
                Invalidate();
            }
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
            Form form = FindFormInternal();
            if (form != null) form.DialogResult = DialogResult;

            Focus();
            base.OnClick(e);
        }

        internal Form FindFormInternal()
        {
            Control cur = this;
            while (cur != null && !(cur is Form))
            {
                cur = cur.Parent;
            }

            return (Form)cur;
        }

        private bool showTips = false;

        [Description("是否显示角标"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowTips
        {
            get
            {
                return showTips;
            }
            set
            {
                if (showTips != value)
                {
                    showTips = value;
                    Invalidate();
                }
            }
        }

        private string tipsText = "";

        [Description("角标文字"), Category("SunnyUI")]
        [DefaultValue("")]
        public string TipsText
        {
            get { return tipsText; }
            set
            {
                if (tipsText != value)
                {
                    tipsText = value;
                    if (ShowTips)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Font tipsFont = UIFontColor.SubFont();

        [Description("角标文字字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "微软雅黑, 9pt")]
        public Font TipsFont
        {
            get { return tipsFont; }
            set
            {
                if (!tipsFont.Equals(value))
                {
                    tipsFont = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (FillColorGradient)
            {
                if (IsHover || IsPress || Selected)
                {
                    base.OnPaintFill(g, path);
                }
                else
                {
                    LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                    br.GammaCorrection = true;
                    g.FillPath(br, path);
                    br.Dispose();
                }
            }
            else
            {
                base.OnPaintFill(g, path);
            }
        }

        private Color tipsColor = Color.Red;

        [Description("角标背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Red")]
        public Color TipsColor
        {
            get => tipsColor;
            set
            {
                tipsColor = value;
                Invalidate();
            }
        }

        private Color tipsForeColor = Color.White;

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI"), Description("角标文字颜色")]
        public Color TipsForeColor
        {
            get => tipsForeColor;
            set
            {
                tipsForeColor = value;
                Invalidate();
            }
        }

        Font tmpFont;

        private Font TempFont
        {
            get
            {
                if (tmpFont == null || !tmpFont.Size.EqualsFloat(TipsFont.DPIScaleFontSize()))
                {
                    tmpFont?.Dispose();
                    tmpFont = TipsFont.DPIScaleFont();
                }

                return tmpFont;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            tmpFont?.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (autoSize && Dock == DockStyle.None)
            {
                SizeF sf = e.Graphics.MeasureString(Text, Font);
                if (Width != (int)(sf.Width) + 6) Width = (int)(sf.Width) + 6;
                if (Height != (int)(sf.Height) + 6) Height = (int)(sf.Height) + 6;
            }

            if (Enabled && ShowTips && !string.IsNullOrEmpty(TipsText))
            {
                e.Graphics.SetHighQuality();
                SizeF sf = e.Graphics.MeasureString(TipsText, TempFont);
                float sfMax = Math.Max(sf.Width, sf.Height);
                float x = Width - 1 - 2 - sfMax;
                float y = 1 + 1;
                e.Graphics.FillEllipse(TipsColor, x, y, sfMax, sfMax);
                e.Graphics.DrawString(TipsText, TempFont, TipsForeColor, x + sfMax / 2.0f - sf.Width / 2.0f, y + sfMax / 2.0f - sf.Height / 2.0f);
            }

            if (Focused && ShowFocusLine)
            {
                Rectangle rect = new Rectangle(2, 2, Width - 5, Height - 5);
                var path = rect.CreateRoundedRectanglePath(Radius);
                using (Pen pn = new Pen(ForeColor))
                {
                    pn.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawPath(pn, path);
                }

                path.Dispose();
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

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillHoverColor = uiColor.ButtonFillHoverColor;
            rectHoverColor = uiColor.ButtonRectHoverColor;
            foreHoverColor = uiColor.ButtonForeHoverColor;

            fillPressColor = uiColor.ButtonFillPressColor;
            rectPressColor = uiColor.ButtonRectPressColor;
            forePressColor = uiColor.ButtonForePressColor;

            fillSelectedColor = uiColor.ButtonFillSelectedColor;
            foreSelectedColor = uiColor.ButtonForeSelectedColor;
            rectSelectedColor = uiColor.ButtonRectSelectedColor;
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
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor2
        {
            get => fillColor2;
            set => SetFillColor2(value);
        }

        [Description("填充颜色渐变"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool FillColorGradient
        {
            get => fillColorGradient;
            set
            {
                if (fillColorGradient != value)
                {
                    fillColorGradient = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
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

        [DefaultValue(typeof(Color), "173, 178, 181"), Category("SunnyUI")]
        [Description("不可用时边框颜色")]
        public Color RectDisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        [DefaultValue(typeof(Color), "109, 109, 103"), Category("SunnyUI")]
        [Description("不可用时字体颜色")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时填充颜色")]
        public Color FillHoverColor
        {
            get => fillHoverColor;
            set => SetFillHoverColor(value);
        }

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
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
            set => SetForeHoverColor(value);
        }

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI")]
        [Description("鼠标按下时字体颜色")]
        public Color ForePressColor
        {
            get => forePressColor;
            set => SetForePressColor(value);
        }

        [DefaultValue(typeof(Color), "115, 179, 255"), Category("SunnyUI")]
        [Description("鼠标移上时边框颜色")]
        public Color RectHoverColor
        {
            get => rectHoverColor;
            set => SetRectHoverColor(value);
        }

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("鼠标按下时边框颜色")]
        public Color RectPressColor
        {
            get => rectPressColor;
            set => SetRectPressColor(value);
        }

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
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

        [DefaultValue(typeof(Color), "64, 128, 204"), Category("SunnyUI")]
        [Description("选中时边框颜色")]
        public Color RectSelectedColor
        {
            get => rectSelectedColor;
            set => SetRectSelectedColor(value);
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

        [DefaultValue(false)]
        [Description("显示激活时边框线"), Category("SunnyUI")]
        public bool ShowFocusLine { get; set; }
    }
}