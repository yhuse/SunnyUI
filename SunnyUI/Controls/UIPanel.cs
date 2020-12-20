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
 * 文件名称: UIPanel.cs
 * 文件说明: 面板
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.Collections.Generic;
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
        protected Color rectColor = UIStyles.Blue.RectColor;
        protected Color fillColor = UIStyles.Blue.PlainColor;
        protected Color foreColor = UIStyles.Blue.PanelForeColor;

        public UIPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.DoubleBuffered = true;
            UpdateStyles();

            Version = UIGlobal.Version;
            AutoScaleMode = AutoScaleMode.None;
            base.Font = UIFontColor.Font;
            base.MinimumSize = new System.Drawing.Size(1, 1);
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
        public string TagString { get; set; }

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
                if (!ctrl.StyleCustomMode)
                {
                    ctrl.Style = Style;
                }
            }

            if (e.Control is Panel)
            {
                List<Control> controls = e.Control.GetUIStyleControls("IStyleInterface");
                foreach (var control in controls)
                {
                    if (control is IStyleInterface item)
                    {
                        if (!item.StyleCustomMode)
                        {
                            item.Style = Style;
                        }
                    }
                }
            }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!Visible || Width <= 0 || Height <= 0) return;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = GDIEx.CreateRoundedRectanglePath(rect, radius, RadiusSides);

            //填充背景色
            if (fillColor.IsValid())
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

            PaintOther?.Invoke(this, e);

            path.Dispose();
        }

        public event PaintEventHandler PaintOther;

        protected virtual void OnPaintFore(Graphics g, GraphicsPath path)
        {
            g.DrawString(Text, Font, Enabled ? foreColor : foreDisableColor, Size, Padding, TextAlignment);
        }

        protected virtual void OnPaintRect(Graphics g, GraphicsPath path)
        {
            Color color = GetRectColor();

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

            if (RadiusSides == UICornerRadiusSides.None)
                g.DrawRectangle(new Pen(color), 0, 0, Width - 1, Height - 1);
            else
                g.DrawPath(color, path);

            using (Pen pen = new Pen(fillColor))
            using (Pen penR = new Pen(rectColor))
            {
                if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                {
                    g.DrawLine(penR, 0, 0, 0, Height - 1);
                }

                if (!ShowRadius || (ShowRadius && !RadiusRightTop && !RadiusLeftTop))
                {
                    g.DrawLine(penR, 0, 0, Width - 1, 0);
                }

                if (!ShowRadius || (ShowRadius && !RadiusRightTop && !RadiusRightBottom))
                {
                    g.DrawLine(penR, Width - 1, 0, Width - 1, Height - 1);
                }

                if (!ShowRectLeft)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                    {
                        g.DrawLine(pen, 0, 1, 0, Height - 2);
                    }
                }

                if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusRightBottom))
                {
                    g.DrawLine(penR, 0, Height - 1, Width - 1, Height - 1);
                }

                if (!ShowRectTop)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusRightTop && !RadiusLeftTop))
                    {
                        g.DrawLine(pen, 1, 0, Width - 2, 0);
                    }
                }

                if (!ShowRectRight)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusRightTop && !RadiusRightBottom))
                    {
                        g.DrawLine(pen, Width - 1, 1, Width - 1, Height - 2);
                    }
                }

                if (!ShowRectBottom)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusRightBottom))
                    {
                        g.DrawLine(pen, 1, Height - 1, Width - 2, Height - 1);
                    }
                }

                if (!ShowRectLeft && !ShowRectTop)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                        g.DrawLine(pen, 0, 0, 0, 1);
                }

                if (!ShowRectRight && !ShowRectTop)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                        g.DrawLine(pen, Width - 1, 0, Width - 1, 1);
                }

                if (!ShowRectLeft && !ShowRectBottom)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                        g.DrawLine(pen, 0, Height - 1, 0, Height - 2);
                }

                if (!ShowRectRight && !ShowRectBottom)
                {
                    if (!ShowRadius || (ShowRadius && !RadiusLeftBottom && !RadiusLeftTop))
                        g.DrawLine(pen, Width - 1, Height - 1, Width - 1, Height - 2);
                }
            }
        }

        protected virtual void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = GetFillColor();

            if (RadiusSides == UICornerRadiusSides.None)
                g.Clear(color);
            else
                g.FillPath(color, path);
        }

        // protected override void WndProc(ref Message m)
        // {
        //     if (m.Msg != 20)
        //     {
        //         base.WndProc(ref m);
        //     }
        // }

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
        public bool StyleCustomMode { get; set; }

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
            this.SetChildUIStyle(style);

            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.PlainColor;
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

        public string Version { get; }

        private ContentAlignment _textAlignment = ContentAlignment.MiddleCenter;

        /// <summary>
        /// 文字对齐方向
        /// </summary>
        [Description("文字对齐方向"), Category("SunnyUI")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlignment
        {
            get => _textAlignment;
            set
            {
                if (_textAlignment != value)
                {
                    _textAlignment = value;
                    TextAlignmentChange?.Invoke(this, value);
                    Invalidate();
                }
            }
        }

        public delegate void OnTextAlignmentChange(object sender, ContentAlignment alignment);

        public event OnTextAlignmentChange TextAlignmentChange;
    }
}