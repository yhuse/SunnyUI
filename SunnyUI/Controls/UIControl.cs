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
 * 文件名称: UIControl.cs
 * 文件说明: 控件基类
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2021-12-13: V3.0.9 边框线宽可设置1或者2
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 控件基类
    /// </summary>
    [ToolboxItem(false)]
    public class UIControl : Control, IStyleInterface
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIControl()
        {
            Version = UIGlobal.Version;
            base.Font = UIFontColor.Font;
            Size = new Size(100, 35);
            base.MinimumSize = new Size(1, 1);
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

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode
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
                    radius = Math.Max(0, value);
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        public void SetStyle(UIStyle style)
        {
            UIBaseStyle uiColor = UIStyles.GetStyleColor(style);
            if (!uiColor.IsCustom()) SetStyleColor(uiColor);
            _style = style;
        }

        /// <summary>
        /// 设置主题样式颜色
        /// </summary>
        /// <param name="uiColor"></param>
        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            fillColor = uiColor.ButtonFillColor;
            fillColor2 = uiColor.ButtonFillColor2;
            rectColor = uiColor.RectColor;
            foreColor = uiColor.ButtonForeColor;

            fillDisableColor = uiColor.FillDisableColor;
            rectDisableColor = uiColor.RectDisableColor;
            foreDisableColor = uiColor.ForeDisableColor;

            fillPressColor = fillHoverColor = fillColor;
            rectPressColor = rectHoverColor = rectColor;
            forePressColor = foreHoverColor = foreColor;

            fillSelectedColor = uiColor.ButtonFillSelectedColor;

            Invalidate();
        }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
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

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Visible || Width <= 0 || Height <= 0) return;
            if (IsDisposed) return;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = rect.CreateRoundedRectanglePath(radius, RadiusSides, RectSize);

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

            path.Dispose();
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
                color = rectPressColor;
            return Enabled ? color : rectDisableColor;
        }

        [Description("是否显示激活状态颜色"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowFocusColor
        {
            get;
            set;
        }

        /// <summary>
        /// 获取字体颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetForeColor()
        {
            //文字
            Color color = foreColor;
            if (IsHover)
                color = foreHoverColor;
            if (IsPress)
                color = forePressColor;
            if (selected)
                color = foreSelectedColor;
            if (ShowFocusColor && Focused)
                color = forePressColor;
            return Enabled ? color : foreDisableColor;
        }

        /// <summary>
        /// 获取填充颜色
        /// </summary>
        /// <returns>颜色</returns>
        protected Color GetFillColor()
        {
            //填充
            Color color = fillColor;
            if (IsHover)
                color = fillHoverColor;
            if (IsPress)
                color = fillPressColor;
            if (selected)
                color = fillSelectedColor;
            if (ShowFocusColor && Focused)
                color = fillPressColor;
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

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="g">GDI绘图图面</param>
        /// <param name="path">路径</param>
        protected virtual void OnPaintRect(Graphics g, GraphicsPath path)
        {
            g.DrawPath(GetRectColor(), path, true, RectSize);
            PaintRectDisableSides(g);
        }

        /// <summary>
        /// 绘制字体
        /// </summary>
        /// <param name="g">GDI绘图图面</param>
        /// <param name="path">路径</param>
        protected virtual void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Color color = GetForeColor();
            g.DrawString(Text, Font, color, Size, Padding, TextAlign);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 选中颜色
        /// </summary>
        protected Color fillSelectedColor = UIStyles.GetStyleColor(UIStyle.Blue).ButtonFillSelectedColor;

        /// <summary>
        /// 边框颜色
        /// </summary>
        protected Color rectColor = UIStyles.GetStyleColor(UIStyle.Blue).RectColor;

        protected bool fillColorGradient = false;

        /// <summary>
        /// 填充颜色
        /// </summary>
        protected Color fillColor = UIStyles.GetStyleColor(UIStyle.Blue).ButtonFillColor;

        /// <summary>
        /// 填充颜色
        /// </summary>
        protected Color fillColor2 = UIStyles.GetStyleColor(UIStyle.Blue).ButtonFillColor;

        /// <summary>
        /// 字体颜色
        /// </summary>
        protected Color foreColor = UIStyles.GetStyleColor(UIStyle.Blue).ButtonForeColor;

        /// <summary>
        /// 字体鼠标移上颜色
        /// </summary>
        protected Color foreHoverColor;

        /// <summary>
        /// 字体鼠标按下颜色
        /// </summary>
        protected Color forePressColor;

        /// <summary>
        /// 字体不可用颜色
        /// </summary>
        protected Color foreDisableColor = UIStyles.GetStyleColor(UIStyle.Blue).ForeDisableColor;

        /// <summary>
        /// 边框鼠标移上颜色
        /// </summary>
        protected Color rectHoverColor;

        /// <summary>
        /// 边框鼠标按下颜色
        /// </summary>
        protected Color rectPressColor;

        /// <summary>
        /// 边框不可用颜色
        /// </summary>
        protected Color rectDisableColor = UIStyles.GetStyleColor(UIStyle.Blue).RectDisableColor;

        /// <summary>
        /// 填充鼠标移上颜色
        /// </summary>
        protected Color fillHoverColor;

        /// <summary>
        /// 填充鼠标按下颜色
        /// </summary>
        protected Color fillPressColor;

        /// <summary>
        /// 填充不可用颜色
        /// </summary>
        protected Color fillDisableColor = UIStyles.GetStyleColor(UIStyle.Blue).FillDisableColor;

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetFillSelectedColor(Color value)
        {
            if (fillSelectedColor != value)
            {
                fillSelectedColor = value;
                Invalidate();
            }
        }

        protected bool selected;
        protected Color foreSelectedColor;

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetForeSelectedColor(Color value)
        {
            if (foreSelectedColor != value)
            {
                foreSelectedColor = value;
                Invalidate();
            }
        }

        protected Color rectSelectedColor;

        /// <summary>
        /// 设置选中颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetRectSelectedColor(Color value)
        {
            if (rectSelectedColor != value)
            {
                rectSelectedColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置填充鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillHoveColor(Color color)
        {
            fillHoverColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置填充鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillPressColor(Color color)
        {
            fillPressColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置填充不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetFillDisableColor(Color color)
        {
            fillDisableColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设备边框鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectHoveColor(Color color)
        {
            rectHoverColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置边框鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectPressColor(Color color)
        {
            rectPressColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置边框不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetRectDisableColor(Color color)
        {
            rectDisableColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置字体鼠标移上颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeHoveColor(Color color)
        {
            foreHoverColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置字体鼠标按下颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForePressColor(Color color)
        {
            forePressColor = color;
            _style = UIStyle.Custom;
        }

        /// <summary>
        /// 设置字体不可用颜色
        /// </summary>
        /// <param name="color">颜色</param>
        protected void SetForeDisableColor(Color color)
        {
            foreDisableColor = color;
            _style = UIStyle.Custom;
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
                _style = UIStyle.Custom;
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
                _style = UIStyle.Custom;
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
                _style = UIStyle.Custom;
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
                _style = UIStyle.Custom;
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
    }
}