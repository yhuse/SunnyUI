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
 * 文件名称: UIPanel.cs
 * 文件说明: 面板
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2021-05-09: V3.0.3 增加双缓冲，减少闪烁
 * 2021-09-03: V3.0.6 支持背景图片显示
 * 2021-12-11: V3.0.9 增加了渐变色
 * 2021-12-13: V3.0.9 边框线宽可设置1或者2
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// ReSharper disable All
#pragma warning disable 1591

namespace Sunny.UI
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    [DefaultEvent("Click"), DefaultProperty("Text")]
    public partial class UIPanel : UserControl, IStyleInterface
    {
        private int radius = 5;
        protected Color rectColor = UIStyles.GetStyleColor(UIStyle.Blue).RectColor;
        protected Color fillColor = UIStyles.GetStyleColor(UIStyle.Blue).PlainColor;
        protected Color foreColor = UIStyles.GetStyleColor(UIStyle.Blue).PanelForeColor;

        public UIPanel()
        {
            InitializeComponent();
            Version = UIGlobal.Version;
            AutoScaleMode = AutoScaleMode.None;
            base.Font = UIFontColor.Font;
            base.MinimumSize = new System.Drawing.Size(1, 1);
            SetStyleFlags(true, false);
        }

        [Browsable(false)]
        public bool IsScaled { get; set; }

        public virtual void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        protected void SetStyleFlags(bool supportTransparent = true, bool selectable = true, bool resizeRedraw = false)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            if (supportTransparent) SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (selectable) SetStyle(ControlStyles.Selectable, true);
            if (resizeRedraw) SetStyle(ControlStyles.ResizeRedraw, true);
            base.DoubleBuffered = true;
            UpdateStyles();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString
        {
            get; set;
        }

        private string text;

        [Category("SunnyUI")]
        [Description("按钮文字")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    Invalidate();
                }
            }
        }

        protected bool IsDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    return true;
                }
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                {
                    return true;
                }

                return false;
            }
        }

        private ToolStripStatusLabelBorderSides _rectSides = ToolStripStatusLabelBorderSides.All;

        [DefaultValue(ToolStripStatusLabelBorderSides.All), Description("边框显示位置"), Category("SunnyUI")]
        public ToolStripStatusLabelBorderSides RectSides
        {
            get => _rectSides;
            set
            {
                _rectSides = value;
                OnRectSidesChange();
                Invalidate();
            }
        }

        protected virtual void OnRadiusSidesChange()
        {
        }

        protected virtual void OnRectSidesChange()
        {
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is IStyleInterface ctrl)
            {
                if (!ctrl.StyleCustomMode) ctrl.Style = Style;
            }

            UIStyleHelper.SetRawControlStyle(e, Style);
        }

        private UICornerRadiusSides _radiusSides = UICornerRadiusSides.All;

        [DefaultValue(UICornerRadiusSides.All), Description("圆角显示位置"), Category("SunnyUI")]
        public UICornerRadiusSides RadiusSides
        {
            get => _radiusSides;
            set
            {
                _radiusSides = value;
                OnRadiusSidesChange();
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        [Description("是否显示圆角"), Category("SunnyUI")]
        protected bool ShowRadius => (int)RadiusSides > 0;

        //圆角角度
        [Description("圆角角度"), Category("SunnyUI")]
        [DefaultValue(5)]
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (radius != value)
                {
                    radius = Math.Max(0, value);
                    OnRadiusChanged(radius);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("SunnyUI")]
        [DefaultValue(true)]
        protected bool ShowRect => (int)RectSides > 0;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get
            {
                return rectColor;
            }
            set
            {
                if (rectColor != value)
                {
                    rectColor = value;
                    RectColorChanged?.Invoke(this, null);
                    _style = UIStyle.Custom;
                    Invalidate();
                }

                AfterSetRectColor(value);
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
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
                    FillColorChanged?.Invoke(this, null);
                    _style = UIStyle.Custom;
                    Invalidate();
                }

                AfterSetFillColor(value);
            }
        }

        private bool fillColorGradient;

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
        /// 设置填充颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetFillColor2(Color value)
        {
            if (fillColor2 != value)
            {
                fillColor2 = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色
        /// </summary>
        protected Color fillColor2 = UIStyles.GetStyleColor(UIStyle.Blue).ButtonFillColor;

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

        protected void SetFillDisableColor(Color color)
        {
            fillDisableColor = color;
            _style = UIStyle.Custom;
        }

        protected void SetRectDisableColor(Color color)
        {
            rectDisableColor = color;
            _style = UIStyle.Custom;
        }

        protected void SetForeDisableColor(Color color)
        {
            foreDisableColor = color;
            _style = UIStyle.Custom;
        }

        private bool showText = true;

        [Description("是否显示文字"), Category("SunnyUI")]
        [Browsable(false)]
        protected bool ShowText
        {
            get => showText;
            set
            {
                if (showText != value)
                {
                    showText = value;
                    Invalidate();
                }
            }
        }

        private bool showFill = true;

        /// <summary>
        /// 是否显示填充
        /// </summary>
        protected bool ShowFill
        {
            get => showFill;
            set
            {
                if (showFill != value)
                {
                    showFill = value;
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Visible || Width <= 0 || Height <= 0) return;
            if (IsDisposed) return;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = rect.CreateRoundedRectanglePath(radius, RadiusSides, RectSize);

            //填充背景色
            if (BackgroundImage == null && ShowFill && fillColor.IsValid())
            {
                OnPaintFill(e.Graphics, path);
            }

            //填充边框色
            if (ShowRect)
            {
                OnPaintRect(e.Graphics, path);
            }

            //填充文字
            if (ShowText)
            {
                rect = new Rectangle(1, 1, Width - 3, Height - 3);
                using (var path1 = rect.GraphicsPath())
                {
                    OnPaintFore(e.Graphics, path1);
                }
            }

            path.Dispose();
            base.OnPaint(e);
        }

        protected virtual void OnPaintFore(Graphics g, GraphicsPath path)
        {
            g.DrawString(Text, Font, Enabled ? foreColor : foreDisableColor, Size, Padding, TextAlignment);
        }

        protected virtual void OnPaintRect(Graphics g, GraphicsPath path)
        {
            g.DrawPath(GetRectColor(), path, true, RectSize);
            PaintRectDisableSides(g);
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

            var ShowRadius = RadiusSides > 0;//肯定少有一个角显示圆角

            if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
            {
                g.DrawLine(GetRectColor(), 0, 0, 0, Height - 1);
            }

            if (!ShowRadius || (!RadiusRightTop && !RadiusLeftTop))
            {
                g.DrawLine(GetRectColor(), 0, 0, Width - 1, 0);
            }

            if (!ShowRadius || (!RadiusRightTop && !RadiusRightBottom))
            {
                g.DrawLine(GetRectColor(), Width - 1, 0, Width - 1, Height - 1);
            }

            if (!ShowRadius || (!RadiusLeftBottom && !RadiusRightBottom))
            {
                g.DrawLine(GetRectColor(), 0, Height - 1, Width - 1, Height - 1);
            }

            if (!ShowRectLeft)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
                {
                    g.DrawLine(GetFillColor(), 0, RectSize, 0, Height - 1 - RectSize);
                    if (RectSize == 2) g.DrawLine(GetFillColor(), 1, RectSize, 1, Height - 1 - RectSize);
                }
            }

            if (!ShowRectTop)
            {
                if (!ShowRadius || (!RadiusRightTop && !RadiusLeftTop))
                {
                    g.DrawLine(GetFillColor(), RectSize, 0, Width - 1 - RectSize, 0);
                    if (RectSize == 2) g.DrawLine(GetFillColor(), RectSize, 1, Width - 1 - RectSize, 1);
                }
            }

            if (!ShowRectRight)
            {
                if (!ShowRadius || (!RadiusRightTop && !RadiusRightBottom))
                {
                    g.DrawLine(GetFillColor(), Width - 1, RectSize, Width - 1, Height - 1 - RectSize);
                    if (RectSize == 2) g.DrawLine(GetFillColor(), Width - 2, RectSize, Width - 2, Height - 1 - RectSize);
                }
            }

            if (!ShowRectBottom)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusRightBottom))
                {
                    g.DrawLine(GetFillColor(), RectSize, Height - 1, Width - 1 - RectSize, Height - 1);
                    if (RectSize == 2) g.DrawLine(GetFillColor(), RectSize, Height - 2, Width - 1 - RectSize, Height - 2);
                }
            }

            if (!ShowRectLeft && !ShowRectTop)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
                    g.FillRectangle(GetFillColor(), 0, 0, RectSize, RectSize);
            }

            if (!ShowRectRight && !ShowRectTop)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
                    g.FillRectangle(GetFillColor(), Width - 1 - RectSize, 0, RectSize, RectSize);
            }

            if (!ShowRectLeft && !ShowRectBottom)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
                    g.FillRectangle(GetFillColor(), 0, Height - 1 - RectSize, RectSize, RectSize);
            }

            if (!ShowRectRight && !ShowRectBottom)
            {
                if (!ShowRadius || (!RadiusLeftBottom && !RadiusLeftTop))
                    g.FillRectangle(GetFillColor(), Width - 1 - RectSize, Height - 1 - RectSize, RectSize, RectSize);
            }
        }

        protected virtual void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = GetFillColor();

            if (fillColorGradient)
            {
                LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                br.GammaCorrection = true;

                if (RadiusSides == UICornerRadiusSides.None)
                    g.FillRectangle(br, ClientRectangle);
                else
                    g.FillPath(br, path);

                br.Dispose();
            }
            else
            {
                if (RadiusSides == UICornerRadiusSides.None)
                    g.Clear(color);
                else
                    g.FillPath(color, path);
            }
        }

        protected virtual void AfterSetFillColor(Color color)
        {
        }

        protected virtual void AfterSetRectColor(Color color)
        {
        }

        protected virtual void AfterSetForeColor(Color color)
        {
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode
        {
            get; set;
        }

        protected UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        public void SetStyle(UIStyle style)
        {
            this.SuspendLayout();
            UIStyleHelper.SetChildUIStyle(this, style);

            UIBaseStyle uiColor = UIStyles.GetStyleColor(style);
            if (!uiColor.IsCustom()) SetStyleColor(uiColor);
            _style = style;
            this.ResumeLayout();
        }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor2 = fillColor = uiColor.PlainColor;
            rectColor = uiColor.RectColor;
            foreColor = uiColor.PanelForeColor;

            fillDisableColor = uiColor.FillDisableColor;
            rectDisableColor = uiColor.RectDisableColor;
            foreDisableColor = uiColor.ForeDisableColor;
            Invalidate();
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                foreColor = value;
                AfterSetForeColor(value);
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "244, 244, 244")]
        [Description("不可用时填充颜色"), Category("SunnyUI")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
        }

        [DefaultValue(typeof(Color), "173, 178, 181")]
        [Description("不可用时边框颜色"), Category("SunnyUI")]
        public Color RectDisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        [DefaultValue(typeof(Color), "109, 109, 103")]
        [Description("不可用时字体颜色"), Category("SunnyUI")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        protected virtual void OnRadiusChanged(int value)
        {
        }

        protected Color foreDisableColor = Color.FromArgb(109, 109, 103);
        protected Color rectDisableColor = Color.FromArgb(173, 178, 181);
        protected Color fillDisableColor = Color.FromArgb(244, 244, 244);

        protected Color GetRectColor()
        {
            return Enabled ? rectColor : rectDisableColor;
        }

        protected Color GetForeColor()
        {
            return Enabled ? foreColor : foreDisableColor;
        }

        protected Color GetFillColor()
        {
            return Enabled ? fillColor : fillDisableColor;
        }

        /// <summary>
        /// 屏蔽原属性，获取或设置一个值，该值指示是否在 Windows 任务栏中显示窗体。
        /// </summary>
        /// <value><c>true</c> if [show in taskbar]; otherwise, <c>false</c>.</value>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("禁用该属性！", true)]
        public new BorderStyle BorderStyle => BorderStyle.None;

        public event EventHandler FillColorChanged;

        public event EventHandler RectColorChanged;

        public string Version
        {
            get;
        }

        private ContentAlignment _textAlignment = ContentAlignment.MiddleCenter;

        /// <summary>
        /// 文字对齐方向
        /// </summary>
        [Description("文字对齐方向"), Category("SunnyUI")]
        public ContentAlignment TextAlignment
        {
            get => _textAlignment;
            set
            {
                _textAlignment = value;
                TextAlignmentChange?.Invoke(this, value);
                Invalidate();
            }
        }

        public delegate void OnTextAlignmentChange(object sender, ContentAlignment alignment);

        public event OnTextAlignmentChange TextAlignmentChange;

        private int rectSize = 1;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框宽度"), Category("SunnyUI")]
        [DefaultValue(1)]
        public int RectSize
        {
            get => rectSize;
            set
            {
                int v = value;
                if (v > 2) v = 2;
                if (v < 1) v = 1;
                if (rectSize != v)
                {
                    rectSize = v;
                    Invalidate();
                }
            }
        }
    }
}