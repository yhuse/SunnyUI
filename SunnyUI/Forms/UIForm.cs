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
 * 文件名称: UIForm.cs
 * 文件说明: 窗体基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-30: V2.2.5 更新标题移动、双击最大化/正常、到顶最大化、最大化后拖拽正常
 * 2020-07-01: V2.2.6 仿照QQ，重绘标题栏按钮
 * 2020-07-05: V2.2.6 更新窗体控制按钮圆角和跟随窗体圆角变化。
 * 2020-09-17: V2.2.7 重写WindowState相关代码
 * 2020-09-17: V2.2.7 增加了窗体可拉拽调整大小ShowDragStretch属性
 * 2021-02-04: V3.0.1 标题栏增加扩展按钮
 * 2021-05-06: V3.0.3 增加属性，标题栏可放置控件
 * 2021-08-17: V3.0.6 增加TitleFont属性
 * 2021-08-17: V3.0.6 适应主屏幕任务栏在屏幕各个方向均可
 * 2021-08-17: V3.0.8 增加IFrame接口
 * 2022-01-03: V3.0.9 标题栏按钮可以设置颜色
 * 2022-02-09: V3.1.0 增加页面间传值方法SetParamToPage
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-03-28: V3.1.1 增加了查找页面的方法
 * 2022-04-02: V3.1.2 默认设置AutoScaleMode为None
 * 2022-04-26: V3.1.8 屏蔽一些属性
 * 2022-05-06: V3.1.8 可拖拽时Padding可以调整大小
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIForm : Form, IStyleInterface, ITranslate, IFrame
    {
        public readonly Guid Guid = Guid.NewGuid();

        public UIForm()
        {
            base.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;//设置最大化尺寸
            InitializeComponent();

            if (this.Register())
            {
                SetStyle(UIStyles.Style);
            }

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            Version = UIGlobal.Version;
            extendSymbol = 0;
            FormBorderStyle = FormBorderStyle.None;
            m_aeroEnabled = false;
            showTitleIcon = false;

            controlBoxForeColor = UIStyles.Blue.FormControlBoxForeColor;
            controlBoxFillHoverColor = UIStyles.Blue.FormControlBoxFillHoverColor;
            ControlBoxCloseFillHoverColor = UIStyles.Blue.FormControlBoxCloseFillHoverColor;
            rectColor = UIStyles.Blue.FormRectColor;
            foreColor = UIStyles.Blue.FormForeColor;
            BackColor = UIStyles.Blue.FormBackColor;
            titleColor = UIStyles.Blue.FormTitleColor;
            titleForeColor = UIStyles.Blue.FormTitleForeColor;
        }

        [Browsable(false)]
        public new bool IsMdiContainer
        {
            get => base.IsMdiContainer;
            set => base.IsMdiContainer = false;
        }

        [Browsable(false)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = false;
        }

        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        [Browsable(false)]
        public bool IsScaled { get; private set; }

        public void SetDPIScale()
        {
            if (DesignMode) return;
            if (!IsScaled && UIStyles.DPIScale)
            {
                this.SetDPIScaleFont();

                if (!UIDPIScale.DPIScaleIsOne())
                {
                    TitleFont = TitleFont.DPIScaleFont();
                }

                foreach (Control control in this.GetAllDPIScaleControls())
                {
                    if (control is UIDataGridView dgv)
                    {
                        dgv.SetDPIScale();
                    }
                    else
                    {
                        control.SetDPIScaleFont();
                    }
                }

                IsScaled = true;
            }
        }

        private void SetZoomScaleRect()
        {
            if (ZoomScaleRect.Width == 0 && ZoomScaleRect.Height == 0)
            {
                ZoomScaleRect = new Rectangle(ZoomScaleSize.Width, ZoomScaleSize.Height, 0, 0);
            }

            if (ZoomScaleRect.Width == 0 && ZoomScaleRect.Height == 0)
            {
                ZoomScaleRect = new Rectangle(Left, Top, Width, Height);
            }

            ZoomScaleRectChanged?.Invoke(this, ZoomScaleRect);
        }

        [DefaultValue(typeof(Size), "0, 0")]
        [Description("设计界面大小"), Category("SunnyUI")]
        public Size ZoomScaleSize
        {
            get;
            set;
        }

        [Browsable(false)]
        public Rectangle ZoomScaleRect { get; set; }

        private void SetZoomScale()
        {
            if (ZoomScaleDisabled) return;
            if (!UIStyles.DPIScale || !UIStyles.ZoomScale) return;
            if (ZoomScaleRect.Width == 0 || ZoomScaleRect.Height == 0) return;
            if (Width == 0 || Height == 0) return;
            float scale = Math.Min(Width * 1.0f / ZoomScaleRect.Width, Height * 1.0f / ZoomScaleRect.Height);
            if (scale.EqualsFloat(0)) return;
            foreach (Control control in this.GetAllZoomScaleControls())
            {
                if (control is IZoomScale ctrl)
                {
                    UIZoomScale.SetZoomScale(control, scale);
                }
            }

            ZoomScaleChanged?.Invoke(this, scale);
        }

        public event OnZoomScaleChanged ZoomScaleChanged;

        public void ResetDPIScale()
        {
            IsScaled = false;
            SetDPIScale();
        }

        [DefaultValue(true)]
        [Description("是否点击标题栏可以移动窗体"), Category("SunnyUI")]
        public bool Movable { get; set; } = true;

        public void Translate()
        {
            List<Control> controls = this.GetTranslateControls("ITranslate");
            foreach (var control in controls)
            {
                if (control is ITranslate item)
                {
                    item.Translate();
                }
            }
        }

        [DefaultValue(false)]
        [Description("允许在标题栏放置控件"), Category("SunnyUI")]
        public bool AllowAddControlOnTitle
        {
            get; set;
        }

        [DefaultValue(false)]
        [Description("允许显示标题栏"), Category("SunnyUI")]
        public bool AllowShowTitle
        {
            get => ShowTitle;
            set => ShowTitle = value;
        }

        private bool extendBox;

        [DefaultValue(false)]
        [Description("显示扩展按钮"), Category("SunnyUI")]
        public bool ExtendBox
        {
            get => extendBox;
            set
            {
                extendBox = value;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        private int extendSymbol;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("Sunny.UI.UIImagePropertyEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [DefaultValue(0)]
        [Description("扩展按钮字体图标"), Category("SunnyUI")]
        public int ExtendSymbol
        {
            get => extendSymbol;
            set
            {
                extendSymbol = value;
                Invalidate();
            }
        }

        private int _symbolSize = 24;

        [DefaultValue(24)]
        [Description("扩展按钮字体图标大小"), Category("SunnyUI")]
        public int ExtendSymbolSize
        {
            get => _symbolSize;
            set
            {
                _symbolSize = Math.Max(value, 16);
                _symbolSize = Math.Min(value, 128);
                Invalidate();
            }
        }

        private Point extendSymbolOffset = new Point(0, 0);

        [DefaultValue(typeof(Point), "0, 0")]
        [Description("扩展按钮字体图标偏移量"), Category("SunnyUI")]
        public Point ExtendSymbolOffset
        {
            get => extendSymbolOffset;
            set
            {
                extendSymbolOffset = value;
                Invalidate();
            }
        }

        [DefaultValue(null)]
        [Description("扩展按钮菜单"), Category("SunnyUI")]
        public UIContextMenuStrip ExtendMenu
        {
            get; set;
        }

        //不显示FormBorderStyle属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            set
            {
                if (!Enum.IsDefined(typeof(FormBorderStyle), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FormBorderStyle));
                base.FormBorderStyle = FormBorderStyle.None;
            }
        }

        public void Render()
        {
            SetStyle(UIStyles.Style);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            AfterSetFillColor(BackColor);
            _style = UIStyle.Custom;
        }

        protected virtual void AfterSetFillColor(Color color)
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

            if (ShowTitle && !AllowAddControlOnTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = Padding.Top;
            }
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
        /// 是否显示窗体的标题栏
        /// </summary>
        private bool showTitle = true;

        /// <summary>
        /// 是否显示窗体的标题栏
        /// </summary>
        [Description("是否显示窗体的标题栏"), Category("WindowStyle"), DefaultValue(true)]
        public bool ShowTitle
        {
            get => showTitle;
            set
            {
                showTitle = value;
                Padding = new Padding(Padding.Left, value ? titleHeight : 0, Padding.Right, Padding.Bottom);
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示窗体的控制按钮
        /// </summary>
        private bool controlBox = true;

        /// <summary>
        /// 是否显示窗体的控制按钮
        /// </summary>
        [Description("是否显示窗体的控制按钮"), Category("WindowStyle"), DefaultValue(true)]
        public new bool ControlBox
        {
            get => controlBox;
            set
            {
                controlBox = value;
                if (!controlBox)
                {
                    MinimizeBox = MaximizeBox = false;
                }

                CalcSystemBoxPos();
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示窗体的最大化按钮
        /// </summary>
        [Description("是否显示窗体的最大化按钮"), Category("WindowStyle"), DefaultValue(true)]
        public new bool MaximizeBox
        {
            get => maximizeBox;
            set
            {
                maximizeBox = value;
                if (value) minimizeBox = true;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示窗体的最大化按钮
        /// </summary>
        private bool maximizeBox = true;

        /// <summary>
        /// 是否显示窗体的最小化按钮
        /// </summary>
        private bool minimizeBox = true;

        /// <summary>
        /// 是否显示窗体的最小化按钮
        /// </summary>
        [Description("是否显示窗体的最小化按钮"), Category("WindowStyle"), DefaultValue(true)]
        public new bool MinimizeBox
        {
            get => minimizeBox;
            set
            {
                minimizeBox = value;
                if (!value) maximizeBox = false;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        /// <summary>
        /// 当前控件的版本
        /// </summary>
        [Description("控件版本"), Category("SunnyUI")]
        public string Version
        {
            get;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void Final()
        {
        }

        private bool showFullScreen;

        /// <summary>
        /// 是否以全屏模式进入最大化
        /// </summary>
        [Description("是否以全屏模式进入最大化"), Category("WindowStyle")]
        public bool ShowFullScreen
        {
            get => showFullScreen;
            set
            {
                showFullScreen = value;
                base.MaximumSize = ShowFullScreen ? Screen.PrimaryScreen.Bounds.Size : Screen.PrimaryScreen.WorkingArea.Size;
            }
        }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        private int titleHeight = 35;

        /// <summary>
        /// 标题栏高度
        /// </summary>
        [Description("标题栏高度"), Category("SunnyUI"), DefaultValue(35)]
        public int TitleHeight
        {
            get => titleHeight;
            set
            {
                titleHeight = Math.Max(value, 29);
                Padding = new Padding(Padding.Left, showTitle ? titleHeight : Padding.Top, Padding.Right, Padding.Bottom);
                Invalidate();
                CalcSystemBoxPos();
            }
        }

        private Color titleColor;

        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TitleColor
        {
            get => titleColor;
            set
            {
                if (titleColor != value)
                {
                    titleColor = value;
                    SetStyleCustom();
                }
            }
        }

        /// <summary>
        /// 标题颜色
        /// </summary>
        private Color titleForeColor;

        /// <summary>
        /// 标题颜色
        /// </summary>
        [Description("标题前景色（标题颜色）"), Category("SunnyUI"), DefaultValue(typeof(Color), "White")]
        public Color TitleForeColor
        {
            get => titleForeColor;
            set
            {
                if (titleForeColor != value)
                {
                    titleForeColor = value;
                    SetStyleCustom();
                }
            }
        }

        /// <summary>
        /// 标题字体
        /// </summary>
        private Font titleFont = UIFontColor.Font();

        /// <summary>
        /// 标题字体
        /// </summary>
        [Description("标题字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "微软雅黑, 12pt")]
        public Font TitleFont
        {
            get => titleFont;
            set
            {
                titleFont = value;
                Invalidate();
            }
        }

        private Rectangle ControlBoxRect;

        private Rectangle MaximizeBoxRect;

        private Rectangle MinimizeBoxRect;

        private Rectangle ExtendBoxRect;

        private int ControlBoxLeft;

        private void CalcSystemBoxPos()
        {
            ControlBoxLeft = Width;

            if (ControlBox)
            {
                ControlBoxRect = new Rectangle(Width - 6 - 28, titleHeight / 2 - 14, 28, 28);
                ControlBoxLeft = ControlBoxRect.Left - 2;

                if (MaximizeBox)
                {
                    MaximizeBoxRect = new Rectangle(ControlBoxRect.Left - 28 - 2, ControlBoxRect.Top, 28, 28);
                    ControlBoxLeft = MaximizeBoxRect.Left - 2;
                }
                else
                {
                    MaximizeBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
                }

                if (MinimizeBox)
                {
                    MinimizeBoxRect = new Rectangle(MaximizeBox ? MaximizeBoxRect.Left - 28 - 2 : ControlBoxRect.Left - 28 - 2, ControlBoxRect.Top, 28, 28);
                    ControlBoxLeft = MinimizeBoxRect.Left - 2;
                }
                else
                {
                    MinimizeBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
                }

                if (ExtendBox)
                {
                    if (MinimizeBox)
                    {
                        ExtendBoxRect = new Rectangle(MinimizeBoxRect.Left - 28 - 2, ControlBoxRect.Top, 28, 28);
                    }
                    else
                    {
                        ExtendBoxRect = new Rectangle(ControlBoxRect.Left - 28 - 2, ControlBoxRect.Top, 28, 28);
                    }
                }
            }
            else
            {
                ExtendBoxRect = MaximizeBoxRect = MinimizeBoxRect = ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }
        }

        protected bool IsDesignMode
        {
            get
            {
                bool ReturnFlag = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    ReturnFlag = true;
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                    ReturnFlag = true;

                return ReturnFlag;
            }
        }

        protected Color foreColor;

        protected Color rectColor;

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    AfterSetForeColor(ForeColor);
                    SetStyleCustom();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        /// <value>The color of the border style.</value>
        [Description("边框颜色"), Category("SunnyUI")]
        public Color RectColor
        {
            get => rectColor;
            set
            {
                rectColor = value;
                AfterSetRectColor(value);
                RectColorChanged?.Invoke(this, EventArgs.Empty);
                SetStyleCustom();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None && ShowTitle)
            {
                if (InControlBox)
                {
                    InControlBox = false;
                    Close();
                }

                if (InMinBox)
                {
                    InMinBox = false;
                    WindowState = FormWindowState.Minimized;
                }

                if (InMaxBox)
                {
                    InMaxBox = false;
                    ShowMaximize();
                }

                if (InExtendBox)
                {
                    InExtendBox = false;
                    if (ExtendMenu != null)
                    {
                        this.ShowContextMenuStrip(ExtendMenu, ExtendBoxRect.Left, TitleHeight - 1);
                    }
                    else
                    {
                        ExtendBoxClick?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler ExtendBoxClick;

        /// <summary>
        /// 窗体最大化前的大小
        /// </summary>
        private Size size;

        /// <summary>
        /// 窗体最大化前所处的位置
        /// </summary>
        private Point location;

        private void ShowMaximize(bool IsOnMoving = false)
        {
            Screen screen = Screen.FromPoint(MousePosition);
            base.MaximumSize = ShowFullScreen ? screen.Bounds.Size : screen.WorkingArea.Size;
            if (screen.Primary)
                MaximizedBounds = ShowFullScreen ? screen.Bounds : screen.WorkingArea;
            else
                MaximizedBounds = new Rectangle(0, 0, 0, 0);

            if (WindowState == FormWindowState.Normal)
            {
                size = Size;
                // 若窗体从正常模式->最大化模式，该操作是由移动窗体至顶部触发的，记录的是移动前的窗体位置
                location = IsOnMoving ? FormLocation : Location;
                FormEx.SetFormRoundRectRegion(this, 0);
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                if (size.Width == 0 || size.Height == 0)
                {
                    int w = 800;
                    if (MinimumSize.Width > 0) w = MinimumSize.Width;
                    int h = 600;
                    if (MinimumSize.Height > 0) h = MinimumSize.Height;
                    size = new Size(w, h);
                }

                Size = size;
                if (location.X == 0 && location.Y == 0)
                {
                    location = new Point(screen.Bounds.Left + screen.Bounds.Width / 2 - size.Width / 2,
                      screen.Bounds.Top + screen.Bounds.Height / 2 - size.Height / 2);
                }

                Location = location;
                FormEx.SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
                WindowState = FormWindowState.Normal;
            }

            Invalidate();
        }

        private bool FormMoveMouseDown;

        /// <summary>
        /// 鼠标左键按下时，窗体的位置
        /// </summary>
        private Point FormLocation;

        /// <summary>
        /// 鼠标左键按下时，鼠标的位置
        /// </summary>
        private Point mouseOffset;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (InControlBox || InMaxBox || InMinBox || InExtendBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;

            if (e.Button == MouseButtons.Left && Movable)
            {
                FormMoveMouseDown = true;
                FormLocation = Location;
                mouseOffset = MousePosition;
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (!MaximizeBox) return;
            if (InControlBox || InMaxBox || InMinBox || InExtendBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;

            ShowMaximize();
        }

        private long stickyBorderTime = 5000000;

        /// <summary>
        /// 设置或获取显示器边缘停留的最大时间(ms)，默认500ms
        /// </summary>
        [Description("设置或获取在显示器边缘停留的最大时间(ms)"), Category("SunnyUI")]
        [DefaultValue(500)]
        public long StickyBorderTime
        {
            get => stickyBorderTime / 10000;
            set => stickyBorderTime = value * 10000;
        }

        /// <summary>
        /// 是否触发在显示器边缘停留事件
        /// </summary>
        private bool IsStayAtTopBorder;

        /// <summary>
        /// 显示器边缘停留事件被触发的时间
        /// </summary>
        private long TopBorderStayTicks;

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!IsDisposed && FormMoveMouseDown)
            {
                //int screenIndex = GetMouseInScreen(PointToScreen(e.Location));
                Screen screen = Screen.FromPoint(MousePosition);
                if (MousePosition.Y == screen.WorkingArea.Top && MaximizeBox)
                {
                    ShowMaximize(true);
                }

                // 防止窗体上移时标题栏超出容器，导致后续无法移动
                if (Top < screen.WorkingArea.Top)
                {
                    Top = screen.WorkingArea.Top;
                }

                // 防止窗体下移时标题栏超出容器，导致后续无法移动
                if (Top > screen.WorkingArea.Bottom - TitleHeight)
                {
                    Top = screen.WorkingArea.Bottom - TitleHeight;
                }
            }

            // 鼠标抬起后强行关闭粘滞并恢复鼠标移动区域
            IsStayAtTopBorder = false;
            Cursor.Clip = new Rectangle();
            FormMoveMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (FormMoveMouseDown && !MousePosition.Equals(mouseOffset))
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    int MaximizedWidth = Width;
                    int LocationX = Left;
                    ShowMaximize();
                    // 计算等比例缩放后，鼠标与原位置的相对位移
                    float offsetXRatio = 1 - (float)Width / MaximizedWidth;
                    mouseOffset.X -= (int)((mouseOffset.X - LocationX) * offsetXRatio);
                }

                int offsetX = mouseOffset.X - MousePosition.X;
                int offsetY = mouseOffset.Y - MousePosition.Y;
                Rectangle WorkingArea = Screen.GetWorkingArea(this);

                // 若当前鼠标停留在容器上边缘，将会触发一个时间为MaximumBorderInterval(ms)的边缘等待，
                // 若此时结束移动，窗口将自动最大化，该功能为上下排列的多监视器提供
                // 此处判断设置为特定值的好处是，若快速移动窗体跨越监视器，很难触发停留事件
                if (MousePosition.Y - WorkingArea.Top == 0)
                {
                    if (!IsStayAtTopBorder)
                    {
                        Cursor.Clip = WorkingArea;
                        TopBorderStayTicks = DateTime.Now.Ticks;
                        IsStayAtTopBorder = true;
                    }
                    else if (DateTime.Now.Ticks - TopBorderStayTicks > stickyBorderTime)
                    {
                        Cursor.Clip = new Rectangle();
                    }
                }

                Location = new Point(FormLocation.X - offsetX, FormLocation.Y - offsetY);
            }
            else
            {
                if (FormBorderStyle == FormBorderStyle.None)
                {
                    bool inControlBox = e.Location.InRect(ControlBoxRect);
                    bool inMaxBox = e.Location.InRect(MaximizeBoxRect);
                    bool inMinBox = e.Location.InRect(MinimizeBoxRect);
                    bool inExtendBox = e.Location.InRect(ExtendBoxRect);
                    bool isChange = false;

                    if (inControlBox != InControlBox)
                    {
                        InControlBox = inControlBox;
                        isChange = true;
                    }

                    if (inMaxBox != InMaxBox)
                    {
                        InMaxBox = inMaxBox;
                        isChange = true;
                    }

                    if (inMinBox != InMinBox)
                    {
                        InMinBox = inMinBox;
                        isChange = true;
                    }

                    if (inExtendBox != InExtendBox)
                    {
                        InExtendBox = inExtendBox;
                        isChange = true;
                    }

                    if (isChange)
                    {
                        Invalidate();
                    }
                }
                else
                {
                    InExtendBox = InControlBox = InMaxBox = InMinBox = false;
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InExtendBox = InControlBox = InMaxBox = InMinBox = false;
            Invalidate();
        }

        private bool InControlBox, InMaxBox, InMinBox, InExtendBox;

        /// <summary>
        /// 是否屏蔽Alt+F4
        /// </summary>
        [Description("是否屏蔽Alt+F4"), Category("Key")]
        [DefaultValue(false)]
        public bool IsForbidAltF4
        {
            get; set;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            IsActive = true;
            Invalidate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            IsActive = false;
            Invalidate();
        }

        protected bool IsActive;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            if (FormBorderStyle != FormBorderStyle.None)
            {
                return;
            }

            if (ShowTitle)
            {
                e.Graphics.FillRectangle(titleColor, 0, 0, Width, TitleHeight);
                e.Graphics.DrawLine(RectColor, 0, titleHeight, Width, titleHeight);
            }

            if (ShowRect)
            {
                Point[] points;
                bool unShowRadius = !ShowRadius || WindowState == FormWindowState.Maximized ||
                                    (Width == Screen.PrimaryScreen.WorkingArea.Width &&
                                     Height == Screen.PrimaryScreen.WorkingArea.Height);
                if (unShowRadius)
                {
                    points = new[]
                    {
                        new Point(0, 0),
                        new Point(Width - 1, 0),
                        new Point(Width - 1, Height - 1),
                        new Point(0, Height - 1),
                        new Point(0, 0)
                    };
                }
                else
                {
                    points = new[]
                    {
                            new Point(0, 2),
                            new Point(2, 0),
                            new Point(Width - 1 - 2, 0),
                            new Point(Width - 1, 2),
                            new Point(Width - 1, Height - 1 - 2),
                            new Point(Width - 1 - 2, Height - 1),
                            new Point(2, Height - 1),
                            new Point(0, Height - 1 - 2),
                            new Point(0, 2)
                        };
                }

                e.Graphics.DrawLines(rectColor, points);

                if (!unShowRadius)
                {
                    e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(2, 1), new Point(1, 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(2, Height - 1 - 1), new Point(1, Height - 1 - 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(Width - 1 - 2, 1), new Point(Width - 1 - 1, 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(Width - 1 - 2, Height - 1 - 1), new Point(Width - 1 - 1, Height - 1 - 2));
                }
            }

            if (!ShowTitle)
            {
                return;
            }

            e.Graphics.SetHighQuality();
            if (ControlBox)
            {
                if (InControlBox)
                {
                    if (ShowRadius)
                        e.Graphics.FillRoundRectangle(ControlBoxCloseFillHoverColor, ControlBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(ControlBoxCloseFillHoverColor, ControlBoxRect);
                }

                e.Graphics.DrawLine(controlBoxForeColor,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 - 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 - 5,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 + 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 + 5);
                e.Graphics.DrawLine(controlBoxForeColor,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 - 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 + 5,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 + 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 - 5);
            }

            if (MaximizeBox)
            {
                if (InMaxBox)
                {
                    if (ShowRadius)
                        e.Graphics.FillRoundRectangle(ControlBoxFillHoverColor, MaximizeBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(ControlBoxFillHoverColor, MaximizeBoxRect);
                }

                if (WindowState == FormWindowState.Maximized)
                {
                    e.Graphics.DrawRectangle(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 1,
                        7, 7);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 1,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 3,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3);
                }

                if (WindowState == FormWindowState.Normal)
                {
                    e.Graphics.DrawRectangle(controlBoxForeColor,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4,
                        10, 9);
                }
            }

            if (MinimizeBox)
            {
                if (InMinBox)
                {
                    if (ShowRadius)
                        e.Graphics.FillRoundRectangle(ControlBoxFillHoverColor, MinimizeBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(ControlBoxFillHoverColor, MinimizeBoxRect);
                }

                e.Graphics.DrawLine(controlBoxForeColor,
                    MinimizeBoxRect.Left + MinimizeBoxRect.Width / 2 - 6,
                    MinimizeBoxRect.Top + MinimizeBoxRect.Height / 2,
                    MinimizeBoxRect.Left + MinimizeBoxRect.Width / 2 + 5,
                    MinimizeBoxRect.Top + MinimizeBoxRect.Height / 2);
            }

            if (ExtendBox)
            {
                if (InExtendBox)
                {
                    if (ShowRadius)
                        e.Graphics.FillRoundRectangle(ControlBoxFillHoverColor, ExtendBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(ControlBoxFillHoverColor, ExtendBoxRect);
                }

                if (ExtendSymbol == 0)
                {
                    e.Graphics.DrawLine(controlBoxForeColor,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 5 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 - 2,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 + 3);

                    e.Graphics.DrawLine(controlBoxForeColor,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 + 5 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 - 2,
                        ExtendBoxRect.Left + ExtendBoxRect.Width / 2 - 1,
                        ExtendBoxRect.Top + ExtendBoxRect.Height / 2 + 3);
                }
                else
                {
                    e.Graphics.DrawFontImage(extendSymbol, ExtendSymbolSize, controlBoxForeColor, ExtendBoxRect, ExtendSymbolOffset.X, ExtendSymbolOffset.Y);
                }
            }

            e.Graphics.SetDefaultQuality();

            if (ShowTitleIcon && Icon != null)
            {
                using (Image image = IconToImage(Icon))
                {
                    e.Graphics.DrawImage(image, 6, (TitleHeight - 24) / 2, 24, 24);
                }
            }

            SizeF sf = e.Graphics.MeasureString(Text, TitleFont);
            if (TextAlignment == StringAlignment.Center)
            {
                e.Graphics.DrawString(Text, TitleFont, titleForeColor, (Width - sf.Width) / 2, (TitleHeight - sf.Height) / 2);
            }
            else
            {
                e.Graphics.DrawString(Text, TitleFont, titleForeColor, 6 + (ShowTitleIcon && Icon != null ? 26 : 0), (TitleHeight - sf.Height) / 2);
            }
        }

        private Image IconToImage(Icon icon)
        {
            MemoryStream mStream = new MemoryStream();
            icon.Save(mStream);
            Image image = Image.FromStream(mStream);
            return image;
        }

        private bool showTitleIcon;

        [Description("显示标题栏图标"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowTitleIcon
        {
            get => showTitleIcon;
            set
            {
                showTitleIcon = value;
                Invalidate();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 配色主题
        /// </summary>
        [Description("配色主题"), Category("SunnyUI")]
        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        [Description("自定义主题模式（开启后全局主题更改将对当前窗体无效）"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool StyleCustomMode
        {
            get; set;
        }

        private Color controlBoxForeColor = Color.White;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏按钮颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "White")]
        public Color ControlBoxForeColor
        {
            get => controlBoxForeColor;
            set
            {
                if (controlBoxForeColor != value)
                {
                    controlBoxForeColor = value;
                    _style = UIStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color controlBoxFillHoverColor;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏按钮移上背景颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "115, 179, 255")]
        public Color ControlBoxFillHoverColor
        {
            get => controlBoxFillHoverColor;
            set
            {
                if (ControlBoxFillHoverColor != value)
                {
                    controlBoxFillHoverColor = value;
                    _style = UIStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color controlBoxCloseFillHoverColor;
        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏关闭按钮移上背景颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "Red")]
        public Color ControlBoxCloseFillHoverColor
        {
            get => controlBoxCloseFillHoverColor;
            set
            {
                if (controlBoxCloseFillHoverColor != value)
                {
                    controlBoxCloseFillHoverColor = value;
                    Invalidate();
                }
            }
        }

        public void SetStyle(UIStyle style)
        {
            this.SuspendLayout();
            UIStyleHelper.SetChildUIStyle(this, style);

            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style;
            UIStyleChanged?.Invoke(this, new EventArgs());
            this.ResumeLayout();
        }

        public event EventHandler UIStyleChanged;

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            controlBoxForeColor = uiColor.FormControlBoxForeColor;
            controlBoxFillHoverColor = uiColor.FormControlBoxFillHoverColor;
            ControlBoxCloseFillHoverColor = uiColor.FormControlBoxCloseFillHoverColor;
            rectColor = uiColor.FormRectColor;
            foreColor = uiColor.FormForeColor;
            BackColor = uiColor.FormBackColor;
            titleColor = uiColor.FormTitleColor;
            titleForeColor = uiColor.FormTitleForeColor;
        }

        protected void SetStyleCustom(bool needRefresh = true)
        {
            _style = UIStyle.Custom;
            if (needRefresh) Invalidate();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            List<UIPage> pages = this.GetControls<UIPage>(true);
            foreach (var page in pages)
            {
                page.ParentLocation = Location;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetZoomScale();
            CalcSystemBoxPos();

            if (isShow)
            {
                SetRadius();
            }
        }

        protected virtual void AfterSetBackColor(Color color)
        {
        }

        protected virtual void AfterSetRectColor(Color color)
        {
        }

        protected virtual void AfterSetForeColor(Color color)
        {
        }

        public event EventHandler RectColorChanged;

        private bool isShow;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (AutoScaleMode == AutoScaleMode.Font) AutoScaleMode = AutoScaleMode.None;

            CalcSystemBoxPos();
            SetRadius();
            isShow = true;
            SetDPIScale();
            SetZoomScaleRect();
        }

        public event OnZoomScaleRectChanged ZoomScaleRectChanged;

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        private bool _showRadius = true;

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        [Description("是否显示圆角"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ShowRadius
        {
            get
            {
                return (_showRadius && !_showShadow);
            }
            set
            {
                _showRadius = value;
                SetRadius();
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        private bool _showShadow;

        #region 边框阴影

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        [Description("是否显示阴影"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowShadow
        {
            get => _showShadow;
            set
            {
                _showShadow = value;
                Invalidate();
            }
        }

        private bool m_aeroEnabled;

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                Win32.Dwm.DwmIsCompositionEnabled(ref enabled);
                return enabled == 1;
            }

            return false;
        }

        #endregion 边框阴影

        /// <summary>
        /// 是否重绘边框样式
        /// </summary>
        private bool _showRect = true;

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ShowRect
        {
            get => _showRect;
            set
            {
                _showRect = value;
                Invalidate();
            }
        }

        [Description("使用Esc键关闭窗口"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool EscClose { get; set; } = false;

        /// <summary>
        /// Does the escape.
        /// </summary>
        protected virtual void DoEsc()
        {
            if (EscClose)
                Close();
        }

        protected virtual void DoEnter()
        {
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="msg">通过引用传递的 <see cref="T:System.Windows.Forms.Message" />，它表示要处理的 Win32 消息。</param>
        /// <param name="keyData"><see cref="T:System.Windows.Forms.Keys" /> 值之一，它表示要处理的键。</param>
        /// <returns>如果控件处理并使用击键，则为 true；否则为 false，以允许进一步处理。</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int num = 256;
            int num2 = 260;
            if (msg.Msg == num | msg.Msg == num2)
            {
                if (keyData == (Keys)262259 && IsForbidAltF4)
                {
                    //屏蔽Alt+F4
                    return true;
                }

                if (keyData != Keys.Enter)
                {
                    if (keyData == Keys.Escape)
                    {
                        DoEsc();
                    }
                }
                else
                {
                    DoEnter();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);   //其他键按默认处理
        }

        /// <summary>
        /// 通过Windows的API控制窗体的拖动
        /// </summary>
        public static void MousePressMove(IntPtr handle)
        {
            Win32.User.ReleaseCapture();
            Win32.User.SendMessage(handle, Win32.User.WM_SYSCOMMAND, Win32.User.SC_MOVE + Win32.User.HTCAPTION, 0);
        }

        /// <summary>
        /// 在构造函数中调用设置窗体移动
        /// </summary>
        /// <param name="cs">The cs.</param>
        protected void AddMousePressMove(params Control[] cs)
        {
            foreach (Control ctrl in cs)
            {
                if (ctrl != null && !ctrl.IsDisposed)
                {
                    ctrl.MouseDown += CtrlMouseDown;
                }
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the c control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void CtrlMouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                return;
            }

            if (sender == this)
            {
                if (FormBorderStyle == FormBorderStyle.None && e.Y <= titleHeight && e.X < ControlBoxLeft)
                {
                    MousePressMove(Handle);
                }
            }
            else
            {
                MousePressMove(Handle);
            }
        }

        private void SetRadius()
        {
            if (DesignMode)
            {
                return;
            }

            if (WindowState == FormWindowState.Maximized)
            {
                FormEx.SetFormRoundRectRegion(this, 0);
            }
            else
            {
                FormEx.SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
            }

            Invalidate();
        }

        private StringAlignment textAlignment = StringAlignment.Near;

        private void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseAskString.IsValid())
            {
                if (!this.ShowAskDialog(CloseAskString, false))
                {
                    e.Cancel = true;
                }
            }
        }

        [Description("文字对齐方式"), Category("SunnyUI")]
        public StringAlignment TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        [Description("窗体关闭时提示文字，为空则不提示"), Category("SunnyUI"), DefaultValue(null)]
        public string CloseAskString
        {
            get; set;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                {
                    cp.ClassStyle |= Win32.User.CS_DROPSHADOW;
                }

                if (FormBorderStyle == FormBorderStyle.None)
                {
                    // 当边框样式为FormBorderStyle.None时
                    // 点击窗体任务栏图标，可以进行最小化
                    cp.Style = cp.Style | Win32.User.WS_MINIMIZEBOX;
                    return cp;
                }

                cp.ExStyle |= 0x02000000;
                return base.CreateParams;
            }
        }

        private bool showDragStretch;

        [Description("显示边框可拖拽调整窗体大小"), Category("SunnyUI"), DefaultValue(false)]
        public bool ShowDragStretch
        {
            get => showDragStretch;
            set
            {
                showDragStretch = value;
                if (value)
                {
                    ShowRect = true;
                    ShowRadius = false;
                    Padding = new Padding(Math.Max(Padding.Left, 2), showTitle ? TitleHeight + 1 : 2, Math.Max(Padding.Right, 2), Math.Max(Padding.Bottom, 2));
                }
                else
                {
                    ShowRect = false;
                    Padding = new Padding(0, showTitle ? TitleHeight : 0, 0, 0);
                }
            }
        }

        public void RegisterHotKey(Sunny.UI.ModifierKeys modifierKey, Keys key)
        {
            if (hotKeys == null) hotKeys = new Dictionary<int, HotKey>();

            int id = HotKey.CalculateID(modifierKey, key);
            if (!hotKeys.ContainsKey(id))
            {
                HotKey newHotkey = new HotKey(modifierKey, key);
                this.hotKeys.Add(id, newHotkey);
                Win32.User.RegisterHotKey(Handle, id, (int)newHotkey.ModifierKey, (int)newHotkey.Key);
            }
        }

        public event HotKeyEventHandler HotKeyEventHandler;

        private Dictionary<int, HotKey> hotKeys;

        #region 拉拽调整窗体大小

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.User.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;

                case Win32.User.WM_HOTKEY:
                    int hotKeyId = (int)(m.WParam);
                    if (hotKeys.ContainsKey(hotKeyId))
                        HotKeyEventHandler?.Invoke(this, new HotKeyEventArgs(hotKeys[hotKeyId], DateTime.Now));

                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }

            if (m.Msg == Win32.User.WM_NCHITTEST && ShowDragStretch && WindowState == FormWindowState.Normal)
            {
                //Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                Point vPoint = new Point(MousePosition.X, MousePosition.Y);//修正有分屏后，调整窗体大小时鼠标显示左右箭头问题
                vPoint = PointToClient(vPoint);
                int dragSize = 5;
                if (vPoint.X <= dragSize)
                {
                    if (vPoint.Y <= dragSize)
                        m.Result = (IntPtr)Win32.User.HTTOPLEFT;
                    else if (vPoint.Y >= ClientSize.Height - dragSize)
                        m.Result = (IntPtr)Win32.User.HTBOTTOMLEFT;
                    else m.Result = (IntPtr)Win32.User.HTLEFT;
                }
                else if (vPoint.X >= ClientSize.Width - dragSize)
                {
                    if (vPoint.Y <= dragSize)
                        m.Result = (IntPtr)Win32.User.HTTOPRIGHT;
                    else if (vPoint.Y >= ClientSize.Height - dragSize)
                        m.Result = (IntPtr)Win32.User.HTBOTTOMRIGHT;
                    else m.Result = (IntPtr)Win32.User.HTRIGHT;
                }
                else if (vPoint.Y <= dragSize)
                {
                    m.Result = (IntPtr)Win32.User.HTTOP;
                }
                else if (vPoint.Y >= ClientSize.Height - dragSize)
                {
                    m.Result = (IntPtr)Win32.User.HTBOTTOM;
                }
            }

            if (m.Msg == Win32.User.WM_NCPAINT && ShowShadow && m_aeroEnabled)
            {
                var v = 2;
                Win32.Dwm.DwmSetWindowAttribute(Handle, 2, ref v, 4);
                Win32.Dwm.MARGINS margins = new Win32.Dwm.MARGINS()
                {
                    bottomHeight = 0,
                    leftWidth = 0,
                    rightWidth = 0,
                    topHeight = 1
                };

                Win32.Dwm.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        #endregion 拉拽调整窗体大小

        #region 一些辅助窗口

        /// <summary>
        /// 显示进度提示窗
        /// </summary>
        /// <param name="desc">描述文字</param>
        /// <param name="maximum">最大进度值</param>
        /// <param name="decimalCount">显示进度条小数个数</param>
        public void ShowStatusForm(int maximum = 100, string desc = "系统正在处理中，请稍候...", int decimalCount = 1)
        {
            UIStatusFormService.ShowStatusForm(maximum, desc, decimalCount);
        }

        /// <summary>
        /// 隐藏进度提示窗
        /// </summary>
        public void HideStatusForm()
        {
            UIStatusFormService.HideStatusForm();
        }

        /// <summary>
        /// 设置进度提示窗步进值加1
        /// </summary>
        public void StatusFormStepIt()
        {
            UIStatusFormService.StepIt();
        }

        /// <summary>
        /// 设置进度提示窗描述文字
        /// </summary>
        /// <param name="desc">描述文字</param>
        public void SetStatusFormDescription(string desc)
        {
            UIStatusFormService.SetDescription(desc);
        }

        /// <summary>
        /// 显示等待提示窗
        /// </summary>
        /// <param name="desc">描述文字</param>
        public void ShowWaitForm(string desc = "系统正在处理中，请稍候...")
        {
            UIWaitFormService.ShowWaitForm(desc);
        }

        /// <summary>
        /// 隐藏等待提示窗
        /// </summary>
        public void HideWaitForm()
        {
            UIWaitFormService.HideWaitForm();
        }

        /// <summary>
        /// 设置等待提示窗描述文字
        /// </summary>
        /// <param name="desc">描述文字</param>
        public void SetWaitFormDescription(string desc)
        {
            UIWaitFormService.SetDescription(desc);
        }

        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowSuccessDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.SuccessTitle, false, UIStyle.Green, showMask, TopMost);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowInfoDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.InfoTitle, false, UIStyle.Gray, showMask, TopMost);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowWarningDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.WarningTitle, false, UIStyle.Orange, showMask, TopMost);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowErrorDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.ErrorTitle, false, UIStyle.Red, showMask, TopMost);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <returns>结果</returns>
        public bool ShowAskDialog(string msg, bool showMask = true)
        {
            return UIMessageDialog.ShowMessageDialog(msg, UILocalize.AskTitle, true, UIStyle.Blue, showMask, TopMost);
        }

        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowSuccessDialog(string title, string msg, UIStyle style = UIStyle.Green, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, TopMost);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowInfoDialog(string title, string msg, UIStyle style = UIStyle.Gray, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, TopMost);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowWarningDialog(string title, string msg, UIStyle style = UIStyle.Orange, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, TopMost);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowErrorDialog(string title, string msg, UIStyle style = UIStyle.Red, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, TopMost);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <returns>结果</returns>
        public bool ShowAskDialog(string title, string msg, UIStyle style = UIStyle.Blue, bool showMask = true)
        {
            return UIMessageDialog.ShowMessageDialog(msg, title, true, style, showMask, TopMost);
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowInfoTip(string text, int delay = 1000, bool floating = true)
            => UIMessageTip.Show(text, null, delay, floating);

        /// <summary>
        /// 显示成功消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowSuccessTip(string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowOk(text, delay, floating);

        /// <summary>
        /// 显示警告消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowWarningTip(string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowWarning(text, delay, floating);

        /// <summary>
        /// 显示出错消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowErrorTip(string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowError(text, delay, floating);

        /// <summary>
        /// 在指定控件附近显示消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowInfoTip(Component controlOrItem, string text, int delay = 1000, bool floating = true)
            => UIMessageTip.Show(controlOrItem, text, null, delay, floating);

        /// <summary>
        /// 在指定控件附近显示良好消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowSuccessTip(Component controlOrItem, string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowOk(controlOrItem, text, delay, floating);

        /// <summary>
        /// 在指定控件附近显示出错消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowErrorTip(Component controlOrItem, string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowError(controlOrItem, text, delay, floating);

        /// <summary>
        /// 在指定控件附近显示警告消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒</param>
        /// <param name="floating">是否漂浮</param>
        public void ShowWarningTip(Component controlOrItem, string text, int delay = 1000, bool floating = true)
            => UIMessageTip.ShowWarning(controlOrItem, text, delay, floating, false);

        public void ShowInfoNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.INFO, UILocalize.InfoTitle, isDialog, timeout);
        }

        public void ShowSuccessNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.OK, UILocalize.SuccessTitle, isDialog, timeout);
        }

        public void ShowWarningNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.WARNING, UILocalize.WarningTitle, isDialog, timeout);
        }

        public void ShowErrorNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.ERROR, UILocalize.ErrorTitle, isDialog, timeout);
        }

        public void ShowInfoNotifier(string desc, EventHandler clickEvent, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.INFO, UILocalize.InfoTitle, timeout);
        }

        public void ShowSuccessNotifier(string desc, EventHandler clickEvent, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.OK, UILocalize.SuccessTitle, timeout);
        }

        public void ShowWarningNotifier(string desc, EventHandler clickEvent, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.WARNING, UILocalize.WarningTitle, timeout);
        }

        public void ShowErrorNotifier(string desc, EventHandler clickEvent, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.ERROR, UILocalize.ErrorTitle, timeout);
        }

        #endregion 一些辅助窗口

        #region IFrame实现

        public UITabControl MainTabControl { get; set; }

        public UIPage AddPage(UIPage page, int index)
        {
            page.PageIndex = index;
            return AddPage(page);
        }

        public UIPage AddPage(UIPage page, Guid guid)
        {
            page.PageGuid = guid;
            return AddPage(page);
        }

        public UIPage AddPage(UIPage page)
        {
            SetDefaultTabControl();
            page.Frame = this;
            MainTabControl?.AddPage(page);
            return page;
        }

        private UIForm SetDefaultTabControl()
        {
            List<UITabControl> ctrls = this.GetControls<UITabControl>();
            if (ctrls.Count == 1)
            {
                if (MainTabControl == null)
                {
                    MainTabControl = ctrls[0];
                }

                List<UINavMenu> Menus = this.GetControls<UINavMenu>();
                if (Menus.Count == 1 && Menus[0].TabControl == null)
                {
                    Menus[0].TabControl = ctrls[0];
                }

                List<UINavBar> Bars = this.GetControls<UINavBar>();
                if (Bars.Count == 1 && Bars[0].TabControl == null)
                {
                    Bars[0].TabControl = ctrls[0];
                }
            }

            return this;
        }

        public virtual bool SelectPage(int pageIndex)
        {
            SetDefaultTabControl();
            if (MainTabControl == null) return false;
            return MainTabControl.SelectPage(pageIndex);
        }

        public virtual bool SelectPage(Guid guid)
        {
            SetDefaultTabControl();
            if (MainTabControl == null) return false;
            return MainTabControl.SelectPage(guid);
        }

        public bool RemovePage(int pageIndex) => MainTabControl?.RemovePage(pageIndex) ?? false;

        public bool RemovePage(Guid guid) => MainTabControl?.RemovePage(guid) ?? false;

        public virtual void FeedbackFormPage(int fromPageIndex, params object[] objects) { }

        public UIPage GetPage(int pageIndex) => SetDefaultTabControl().MainTabControl?.GetPage(pageIndex);

        public UIPage GetPage(Guid guid) => SetDefaultTabControl().MainTabControl?.GetPage(guid);

        public bool ExistPage(int pageIndex) => GetPage(pageIndex) != null;

        public bool ExistPage(Guid guid) => GetPage(guid) != null;

        public bool SetParamToPage(int toPageIndex, int fromPageIndex, params object[] objects)
        {
            SetDefaultTabControl();
            UIPage page = GetPage(toPageIndex);
            if (page == null) return false;
            return page.SetParam(fromPageIndex, objects);
        }

        public bool SetParamToPage(Guid toPageGuid, Guid fromPageGuid, params object[] objects)
        {
            SetDefaultTabControl();
            UIPage page = GetPage(toPageGuid);
            if (page == null) return false;
            return page.SetParam(fromPageGuid, objects);
        }

        public T GetPage<T>() where T : UIPage => SetDefaultTabControl().MainTabControl?.GetPage<T>();

        public List<T> GetPages<T>() where T : UIPage => SetDefaultTabControl().MainTabControl?.GetPages<T>();

        #endregion IFrame实现
    }
}