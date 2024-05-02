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
 * 文件名称: UIControl.cs
 * 文件说明: 控件基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2021-12-13: V3.0.9 边框线宽可设置1或者2
 * 2022-01-10: V3.1.0 调整边框和圆角的绘制
 * 2022-02-16: V3.1.1 基类增加只读颜色设置
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-02-03: V3.3.1 增加WIN10系统响应触摸屏的按下和弹起事件
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-11-05: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 控件基类
    /// </summary>
    [ToolboxItem(false)]
    public class UIControl : Control, IStyleInterface, IZoomScale
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIControl()
        {
            Version = UIGlobal.Version;
            base.Font = UIStyles.Font();
            Size = new Size(100, 35);
            base.MinimumSize = new Size(1, 1);
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
            radius = UIZoomScale.Calc(baseRadius, scale);
        }

        protected bool selected;

        private float DefaultFontSize = -1;

        public virtual void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
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

        /// <summary>
        /// 是否在设计期
        /// </summary>
        protected bool IsDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    return true;
                }

                if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                {
                    return true;
                }

                return false;
            }
        }

        private ToolStripStatusLabelBorderSides _rectSides = ToolStripStatusLabelBorderSides.All;

        /// <summary>
        /// 边框显示位置
        /// </summary>
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

        /// <summary>
        /// 圆角显示位置
        /// </summary>
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

        private int radius = 5;
        private int baseRadius = 5;

        /// <summary>
        /// 圆角角度
        /// </summary>
        [Description("圆角角度"), Category("SunnyUI")]
        [DefaultValue(5)]
        public int Radius
        {
            get => radius;
            set
            {
                if (radius != value)
                {
                    baseRadius = radius = Math.Max(0, value);
                    Invalidate();
                }
            }
        }

        private bool showText = true;

        /// <summary>
        /// 是否显示文字
        /// </summary>
        [Description("是否显示文字"), Category("SunnyUI")]
        [DefaultValue(true)]
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

        private bool showRect = true;

        /// <summary>
        /// 是否显示边框
        /// </summary>
        protected bool ShowRect
        {
            get => showRect;
            set
            {
                if (showRect != value)
                {
                    showRect = value;
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

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get;
        }

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

        public void SetInheritedStyle(UIStyle style)
        {
            SetStyle(style);
            _style = UIStyle.Inherited;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        /// <summary>
        /// 设置主题样式颜色
        /// </summary>
        /// <param name="uiColor"></param>
        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor = uiColor.ButtonFillColor;
            fillColor2 = uiColor.ButtonFillColor2;
            foreColor = uiColor.ButtonForeColor;
            rectColor = uiColor.ButtonRectColor;

            fillDisableColor = uiColor.FillDisableColor;
            foreDisableColor = uiColor.ForeDisableColor;
            rectDisableColor = uiColor.RectDisableColor;

            fillReadOnlyColor = uiColor.FillDisableColor;
            rectReadOnlyColor = uiColor.RectDisableColor;
            foreReadOnlyColor = uiColor.ForeDisableColor;

            fillHoverColor = fillColor;
            foreHoverColor = foreColor;
            rectHoverColor = rectColor;

            fillPressColor = fillColor;
            forePressColor = foreColor;
            rectPressColor = rectColor;

            fillSelectedColor = fillColor;
            foreSelectedColor = foreColor;
            rectSelectedColor = rectColor;
        }

        /// <summary>
        /// 是否鼠标移上
        /// </summary>
        [Browsable(false)]
        public bool IsHover;

        /// <summary>
        /// 是否鼠标按下
        /// </summary>
        [Browsable(false)]
        public bool IsPress;

        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        /// <summary>
        /// 文字对齐方向
        /// </summary>
        [Description("文字对齐方向"), Category("SunnyUI")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlign
        {
            get => textAlign;
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    Invalidate();
                }
            }
        }

        private bool useDoubleClick;

        [Description("是否启用双击事件"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool UseDoubleClick
        {
            get
            {
                return useDoubleClick;
            }
            set
            {
                if (useDoubleClick != value)
                {
                    useDoubleClick = value;
                    SetStyle(ControlStyles.StandardDoubleClick, useDoubleClick);
                    //Invalidate();
                }
            }
        }

        protected bool lightStyle;

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
            if (ShowFill && fillColor.IsValid())
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
                OnPaintFore(e.Graphics, path);
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// 获取边框颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetRectColor()
        {
            //边框
            Color color = rectColor;
            if (IsHover)
                color = rectHoverColor;
            if (IsPress)
                color = rectPressColor;
            if (selected)
                color = rectSelectedColor;
            if (ShowFocusColor && Focused)
                color = rectHoverColor;
            if (isReadOnly)
                color = rectReadOnlyColor;
            return Enabled ? color : rectDisableColor;
        }

        [Description("是否显示激活状态颜色"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowFocusColor
        {
            get;
            set;
        }

        protected bool isReadOnly;

        /// <summary>
        /// 获取字体颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetForeColor()
        {
            //文字
            Color color = lightStyle ? rectColor : foreColor;
            if (IsHover)
                color = foreHoverColor;
            if (IsPress)
                color = forePressColor;
            if (selected)
                color = foreSelectedColor;
            if (ShowFocusColor && Focused)
                color = foreHoverColor;
            if (isReadOnly)
                color = foreReadOnlyColor;
            return Enabled ? color : foreDisableColor;
        }

        /// <summary>
        /// 获取填充颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetFillColor()
        {
            //填充
            Color color = lightStyle ? plainColor : fillColor;
            if (IsHover)
                color = fillHoverColor;
            if (IsPress)
                color = fillPressColor;
            if (selected)
                color = fillSelectedColor;
            if (ShowFocusColor && Focused)
                color = fillHoverColor;
            if (isReadOnly)
                color = fillReadOnlyColor;
            return Enabled ? color : fillDisableColor;
        }

        /// <summary>
        /// 绘制填充
        /// </summary>
        /// <param name="g">GDI绘图图面</param>
        /// <param name="path">路径</param>
        protected virtual void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = GetFillColor();
            g.FillPath(color, path);
        }

        private int rectSize = 1;

        /// <summary>
        /// 边框宽度
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

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected virtual void OnPaintRect(Graphics g, GraphicsPath path)
        {
            radius = Math.Min(radius, Math.Min(Width, Height));
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

        /// <summary>
        /// 绘制字体
        /// </summary>
        /// <param name="g">GDI绘图图面</param>
        /// <param name="path">路径</param>
        protected virtual void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Rectangle rect = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom);
            g.DrawString(Text, Font, GetForeColor(), rect, TextAlign);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 填充浅色
        /// </summary>
        protected Color plainColor = UIStyles.Blue.PlainColor;

        /// <summary>
        /// 填充颜色
        /// </summary>
        protected Color fillColor = UIStyles.Blue.ButtonFillColor;

        /// <summary>
        /// 填充鼠标移上颜色
        /// </summary>
        protected Color fillHoverColor = UIStyles.Blue.ButtonFillColor;

        /// <summary>
        /// 填充鼠标按下颜色
        /// </summary>
        protected Color fillPressColor = UIStyles.Blue.ButtonFillColor;

        /// <summary>
        /// 选中颜色
        /// </summary>
        protected Color fillSelectedColor = UIStyles.Blue.ButtonFillColor;

        /// <summary>
        /// 填充不可用颜色
        /// </summary>
        protected Color fillDisableColor = UIStyles.Blue.FillDisableColor;

        /// <summary>
        /// 填充只读颜色
        /// </summary>
        protected Color fillReadOnlyColor = UIStyles.Blue.FillDisableColor;

        /// <summary>
        /// 填充颜色
        /// </summary>
        protected Color fillColor2 = UIStyles.Blue.ButtonFillColor2;

        protected bool fillColorGradient = false;

        /// <summary>
        /// 边框颜色
        /// </summary>
        protected Color rectColor = UIStyles.Blue.ButtonRectColor;

        /// <summary>
        /// 边框鼠标移上颜色
        /// </summary>
        protected Color rectHoverColor = UIStyles.Blue.ButtonRectColor;

        /// <summary>
        /// 边框鼠标按下颜色
        /// </summary>
        protected Color rectPressColor = UIStyles.Blue.ButtonRectColor;

        /// <summary>
        /// 边框选中颜色
        /// </summary>
        protected Color rectSelectedColor = UIStyles.Blue.ButtonRectColor;

        /// <summary>
        /// 边框不可用颜色
        /// </summary>
        protected Color rectDisableColor = UIStyles.Blue.RectDisableColor;

        /// <summary>
        /// 边框只读颜色
        /// </summary>
        protected Color rectReadOnlyColor = UIStyles.Blue.RectDisableColor;

        /// <summary>
        /// 字体颜色
        /// </summary>
        protected Color foreColor = UIStyles.Blue.ButtonForeColor;

        /// <summary>
        /// 字体鼠标移上颜色
        /// </summary>
        protected Color foreHoverColor = UIStyles.Blue.ButtonForeColor;

        /// <summary>
        /// 字体鼠标按下颜色
        /// </summary>
        protected Color forePressColor = UIStyles.Blue.ButtonForeColor;

        /// <summary>
        /// 字体选中颜色
        /// </summary>
        protected Color foreSelectedColor = UIStyles.Blue.ButtonForeColor;

        /// <summary>
        /// 字体不可用颜色
        /// </summary>
        protected Color foreDisableColor = UIStyles.Blue.ForeDisableColor;

        /// <summary>
        /// 字体只读颜色
        /// </summary>
        protected Color foreReadOnlyColor = UIStyles.Blue.ForeDisableColor;

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetPlainColor(Color color)
        {
            if (plainColor != color)
            {
                plainColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillSelectedColor(Color color)
        {
            if (fillSelectedColor != color)
            {
                fillSelectedColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeSelectedColor(Color color)
        {
            if (foreSelectedColor != color)
            {
                foreSelectedColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectSelectedColor(Color color)
        {
            if (rectSelectedColor != color)
            {
                rectSelectedColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillHoverColor(Color color)
        {
            if (fillHoverColor != color)
            {
                fillHoverColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillPressColor(Color color)
        {
            if (fillPressColor != color)
            {
                fillPressColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillDisableColor(Color color)
        {
            if (fillDisableColor != color)
            {
                fillDisableColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充只读颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillReadOnlyColor(Color color)
        {
            if (fillReadOnlyColor != color)
            {
                fillReadOnlyColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设备边框鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectHoverColor(Color color)
        {
            if (rectHoverColor != color)
            {
                rectHoverColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置边框鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectPressColor(Color color)
        {
            if (rectPressColor != color)
            {
                rectPressColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置边框不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectDisableColor(Color color)
        {
            if (rectDisableColor != color)
            {
                rectDisableColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置边框只读颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectReadOnlyColor(Color color)
        {
            if (rectReadOnlyColor != color)
            {
                rectReadOnlyColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置字体鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeHoverColor(Color color)
        {
            if (foreHoverColor != color)
            {
                foreHoverColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置字体鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForePressColor(Color color)
        {
            if (forePressColor != color)
            {
                forePressColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置字体不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeDisableColor(Color color)
        {
            if (foreDisableColor != color)
            {
                foreDisableColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置字体只读颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeReadonlyColor(Color color)
        {
            if (foreReadOnlyColor != color)
            {
                foreReadOnlyColor = color;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetRectColor(Color value)
        {
            if (rectColor != value)
            {
                rectColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetFillColor(Color value)
        {
            if (fillColor != value)
            {
                fillColor = value;
                Invalidate();
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
                Invalidate();
            }
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetForeColor(Color value)
        {
            if (foreColor != value)
            {
                foreColor = value;
                Invalidate();
            }
        }

        /// <summary>引发 <see cref="E:System.Windows.Forms.Control.GotFocus" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        /// <summary>引发 <see cref="M:System.Windows.Forms.ButtonBase.OnLostFocus(System.EventArgs)" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        [Description("开启后可响应某些触屏的点击事件"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool TouchPressClick { get; set; } = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////  WndProc窗口程序：
        //////  当按压屏幕时，产生一个WM_POINTERDOWN消息时，我们通过API函数 PostMessage 投送出一个WM_LBUTTONDOWN消息
        //////  WM_LBUTTONDOWN消息会产生一个相对应的鼠标按下左键的事件，于是我们只要在mouse_down事件里写下处理过程即可
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region WndProc 窗口程序

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        const int WM_POINTERDOWN = 0x0246;
        const int WM_POINTERUP = 0x0247;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;

        protected override void WndProc(ref Message m)
        {
            if (TouchPressClick)
            {
                switch (m.Msg)
                {
                    case WM_POINTERDOWN:
                        break;
                    case WM_POINTERUP:
                        break;
                    default:
                        base.WndProc(ref m);
                        return;
                }

                switch (m.Msg)
                {
                    case WM_POINTERDOWN:
                        PostMessage(m.HWnd, WM_LBUTTONDOWN, (int)m.WParam, (int)m.LParam);
                        break;
                    case WM_POINTERUP:
                        PostMessage(m.HWnd, WM_LBUTTONUP, (int)m.WParam, (int)m.LParam);
                        break;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        #endregion

    }
}