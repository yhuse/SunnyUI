/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 文件名称: UIUserControl.cs
 * 文件说明: 用户控件基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2022-04-02: V3.1.1 增加用户控件基类
 * 2022-04-02: V3.1.2 默认设置AutoScaleMode为None
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-07-02: V3.3.9 渐变色增加方向选择
 * 2023-11-05: V3.5.2 重构主题
 * 2023-11-28: V3.6.0 修复Panel内控件颜色设置问题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#pragma warning disable 1591
namespace Sunny.UI
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    [DefaultEvent("Click"), DefaultProperty("Text")]
    public partial class UIUserControl : UserControl, IStyleInterface, IZoomScale
    {
        private int radius = 5;
        protected Color rectColor = UIStyles.Blue.PanelRectColor;
        protected Color fillColor = UIStyles.Blue.PanelFillColor;
        protected Color foreColor = UIStyles.Blue.PanelForeColor;
        protected Color fillColor2 = UIStyles.Blue.PanelFillColor2;
        protected bool InitializeComponentEnd;

        public UIUserControl()
        {
            InitializeComponent();
            Version = UIGlobal.Version;
            AutoScaleMode = AutoScaleMode.None;
            base.Font = UIStyles.Font();
            base.MinimumSize = new System.Drawing.Size(1, 1);
            SetStyleFlags(true, false);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.HideComboDropDown();
        }

        [Browsable(false)]
        public bool Disabled => !Enabled;

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        [Browsable(false), DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public virtual void SetZoomScale(float scale)
        {

        }

        protected float DefaultFontSize = -1;

        public virtual void SetDPIScale()
        {
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        protected bool isReadOnly;

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

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
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
        [Description("显示文字")]
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

        //protected bool IsDesignMode
        //{
        //    get
        //    {
        //        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        //        {
        //            return true;
        //        }
        //        else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
        //        {
        //            return true;
        //        }
        //
        //        return false;
        //    }
        //}

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
                    Invalidate();
                }

                AfterSetRectColor(value);
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
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
        protected virtual void SetFillColor2(Color value)
        {
            if (fillColor2 != value)
            {
                fillColor2 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor2
        {
            get => fillColor2;
            set => SetFillColor2(value);
        }

        protected virtual void SetFillDisableColor(Color color)
        {
            fillDisableColor = color;
            Invalidate();
        }

        protected virtual void SetRectDisableColor(Color color)
        {
            rectDisableColor = color;
            Invalidate();
        }

        protected virtual void SetForeDisableColor(Color color)
        {
            foreDisableColor = color;
            Invalidate();
        }

        protected bool showText = false;

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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (AutoScaleMode == AutoScaleMode.Font)
            {
                AutoScaleMode = AutoScaleMode.None;
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Visible || Width <= 0 || Height <= 0) return;
            if (IsDisposed) return;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = rect.CreateRoundedRectanglePath(radius, RadiusSides, RectSize);

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
            rect = new Rectangle(1, 1, Width - 3, Height - 3);
            using var path1 = rect.GraphicsPath();
            OnPaintFore(e.Graphics, path1);
            base.OnPaint(e);
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected virtual void OnPaintFore(Graphics g, GraphicsPath path)
        {
            string text = Text;
            if (!showText && Text.IsValid()) text = "";
            Rectangle rect = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom);
            g.DrawString(text, Font, GetForeColor(), rect, TextAlignment);
        }

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected virtual void OnPaintRect(Graphics g, GraphicsPath path)
        {
            radius = Math.Min(radius, Math.Min(Width, Height));
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
                    g.DrawLine(GetRectColor(), RectSize - 1, 0, RectSize - 1, Height, false, RectSize);
                if (ShowRectTop)
                    g.DrawLine(GetRectColor(), 0, RectSize - 1, Width, RectSize - 1, false, RectSize);
                if (ShowRectRight)
                    g.DrawLine(GetRectColor(), Width - 1, 0, Width - 1, Height, false, RectSize);
                if (ShowRectBottom)
                    g.DrawLine(GetRectColor(), 0, Height - 1, Width, Height - 1, false, RectSize);
            }
            else
            {
                g.DrawPath(GetRectColor(), path, true, RectSize);
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
                g.DrawLine(GetFillColor(), RectSize - 1, 0, RectSize - 1, Height, false, RectSize);
            }

            if (!ShowRectTop && !RadiusRightTop && !RadiusLeftTop)
            {
                g.DrawLine(GetFillColor(), 0, RectSize - 1, Width, RectSize - 1, false, RectSize);
            }

            if (!ShowRectRight && !RadiusRightTop && !RadiusRightBottom)
            {
                g.DrawLine(GetFillColor(), Width - 1, 0, Width - 1, Height, false, RectSize);
            }

            if (!ShowRectBottom && !RadiusLeftBottom && !RadiusRightBottom)
            {
                g.DrawLine(GetFillColor(), 0, Height - 1, Width, Height - 1, false, RectSize);
            }
        }

        private FlowDirection fillColorGradientDirection = FlowDirection.TopDown;

        [Description("填充颜色渐变方向"), Category("SunnyUI")]
        [DefaultValue(FlowDirection.TopDown)]
        public FlowDirection FillColorGradientDirection
        {
            get => fillColorGradientDirection;
            set
            {
                if (fillColorGradientDirection != value)
                {
                    fillColorGradientDirection = value;
                    Invalidate();
                }
            }
        }

        protected virtual void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = GetFillColor();

            if (fillColorGradient)
            {
                LinearGradientBrush br;
                switch (fillColorGradientDirection)
                {
                    case FlowDirection.LeftToRight:
                        br = new LinearGradientBrush(new Point(0, 0), new Point(Width, y: 0), FillColor, FillColor2);
                        break;
                    case FlowDirection.TopDown:
                        br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                        break;
                    case FlowDirection.RightToLeft:
                        br = new LinearGradientBrush(new Point(Width, 0), new Point(0, y: 0), FillColor, FillColor2);
                        break;
                    case FlowDirection.BottomUp:
                        br = new LinearGradientBrush(new Point(0, Height), new Point(0, 0), FillColor, FillColor2);
                        break;
                    default:
                        br = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), FillColor, FillColor2);
                        break;
                }

                br.GammaCorrection = true;

                if (RadiusSides == UICornerRadiusSides.None)
                    g.FillRectangle(br, ClientRectangle);
                else
                    g.FillPath(br, path);

                br.Dispose();
            }
            else
            {
                if (RadiusSides == UICornerRadiusSides.None || Radius == 0)
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

        protected virtual void AfterSetFillReadOnlyColor(Color color)
        {
        }

        protected virtual void AfterSetRectReadOnlyColor(Color color)
        {
        }

        protected virtual void AfterSetForeReadOnlyColor(Color color)
        {
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }


        protected UIStyle _style = UIStyle.Inherited;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Inherited), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        private void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
        }

        public virtual void SetInheritedStyle(UIStyle style)
        {
            SetStyle(style);
            _style = UIStyle.Inherited;
        }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor2 = uiColor.PanelFillColor2;
            fillColor = uiColor.PanelFillColor;
            rectColor = uiColor.PanelRectColor;
            foreColor = uiColor.PanelForeColor;

            fillDisableColor = uiColor.FillDisableColor;
            rectDisableColor = uiColor.RectDisableColor;
            foreDisableColor = uiColor.ForeDisableColor;

            fillReadOnlyColor = uiColor.FillDisableColor;
            rectReadOnlyColor = uiColor.RectDisableColor;
            foreReadOnlyColor = uiColor.ForeDisableColor;
        }

        /// <summary>
        /// 设置填充只读颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillReadOnlyColor(Color color)
        {
            fillReadOnlyColor = color;
            AfterSetFillReadOnlyColor(color);
            Invalidate();
        }

        /// <summary>
        /// 设置边框只读颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected virtual void SetRectReadOnlyColor(Color color)
        {
            rectReadOnlyColor = color;
            AfterSetRectReadOnlyColor(color);
            Invalidate();
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

        protected virtual void OnRadiusChanged(int value)
        {
        }

        protected Color foreDisableColor = UIStyles.Blue.ForeDisableColor;
        protected Color rectDisableColor = UIStyles.Blue.RectDisableColor;
        protected Color fillDisableColor = UIStyles.Blue.FillDisableColor;
        /// <summary>
        /// 字体只读颜色
        /// </summary>
        protected Color foreReadOnlyColor = UIStyles.Blue.ForeDisableColor;

        /// <summary>
        /// 边框只读颜色
        /// </summary>
        protected Color rectReadOnlyColor = UIStyles.Blue.RectDisableColor;


        /// <summary>
        /// 填充只读颜色
        /// </summary>
        protected Color fillReadOnlyColor = UIStyles.Blue.FillDisableColor;

        protected Color GetRectColor()
        {
            return Enabled ? (isReadOnly ? rectReadOnlyColor : rectColor) : rectDisableColor;
        }

        protected Color GetForeColor()
        {
            return Enabled ? (isReadOnly ? foreReadOnlyColor : foreColor) : foreDisableColor;
        }

        protected Color GetFillColor()
        {
            return Enabled ? (isReadOnly ? fillReadOnlyColor : fillColor) : fillDisableColor;
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

        [Browsable(false)]
        public new bool AutoScroll { get; set; } = false;
    }
}
