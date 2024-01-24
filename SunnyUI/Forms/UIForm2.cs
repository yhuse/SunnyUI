using Sunny.UI.Win32;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIForm2 : Form
    {
        public UIForm2()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            Version = UIGlobal.Version;
            fieldW = typeof(Control).GetField("_clientWidth", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Control).GetField("clientWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldH = typeof(Control).GetField("_clientHeight", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Control).GetField("clientHeight", BindingFlags.NonPublic | BindingFlags.Instance);

            controlBoxForeColor = UIStyles.Blue.FormControlBoxForeColor;
            controlBoxFillHoverColor = UIStyles.Blue.FormControlBoxFillHoverColor;
            ControlBoxCloseFillHoverColor = UIStyles.Blue.FormControlBoxCloseFillHoverColor;
            rectColor = UIStyles.Blue.FormRectColor;
            ForeColor = UIStyles.Blue.FormForeColor;
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

        [DefaultValue(true)]
        [Description("是否点击标题栏可以移动窗体"), Category("SunnyUI")]
        public bool Movable { get; set; } = true;


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

        private int extendSymbol = 0;

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

        private bool showFullScreen = false;

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
                    Invalidate();
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
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 标题字体
        /// </summary>
        private Font titleFont = UIStyles.Font();

        /// <summary>
        /// 标题字体
        /// </summary>
        [Description("标题字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "宋体, 12pt")]
        public Font TitleFont
        {
            get => titleFont;
            set
            {
                titleFont = value;
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
                if (WindowState == FormWindowState.Maximized)
                {
                    ControlBoxRect = new Rectangle(Width - 6 - 28 - 16, titleHeight / 2 - 14, 28, 28);
                }
                else
                {
                    ControlBoxRect = new Rectangle(Width - 6 - 28, titleHeight / 2 - 14, 28, 28);
                }

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

                    ControlBoxLeft = ExtendBoxRect.Left - 2;
                }
            }
            else
            {
                ExtendBoxRect = MaximizeBoxRect = MinimizeBoxRect = ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcSystemBoxPos();
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InExtendBox = InControlBox = InMaxBox = InMinBox = false;
            Invalidate();
        }

        private bool InControlBox, InMaxBox, InMinBox, InExtendBox;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (ShowTitle)
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
                    if (!showFullScreen)
                    {
                        if (WindowState == FormWindowState.Maximized)
                        {
                            WindowState = FormWindowState.Normal;
                            if (Location.Y < 0) Location = new Point(Location.X, 0);
                        }
                        else
                        {
                            WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        if (WindowState == FormWindowState.Maximized)
                        {
                            FormBorderStyle = FormBorderStyle.Sizable;
                            WindowState = FormWindowState.Normal;
                            if (Location.Y < 0) Location = new Point(Location.X, 0);
                        }
                        else
                        {
                            FormBorderStyle = FormBorderStyle.None;
                            WindowState = FormWindowState.Maximized;
                        }
                    }
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

        /// <summary>
        /// 重载鼠标按下事件f
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (InControlBox || InMaxBox || InMinBox || InExtendBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;
            if (e.X > ControlBoxLeft) return;
            if (!Movable) return;

            Win32.User.ReleaseCapture();
            Win32.User.SendMessage(this.Handle, Win32.User.WM_SYSCOMMAND, Win32.User.SC_MOVE + Win32.User.HTCAPTION, 0);
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool inControlBox = e.Location.InRect(ControlBoxRect);
            if (WindowState == FormWindowState.Maximized && ControlBox)
            {
                if (e.Location.X > ControlBoxRect.Left && e.Location.Y < TitleHeight)
                    inControlBox = true;
            }

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

            base.OnMouseMove(e);
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
                    Invalidate();
                }
            }
        }

        protected Color rectColor;

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
                Invalidate();
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
                    Invalidate();
                }
            }
        }

        protected virtual void AfterSetRectColor(Color color) { }

        public event EventHandler RectColorChanged;

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

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0)
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
                Point[] points = new[]
                    {
                        new Point(0, 0),
                        new Point(Width - 1, 0),
                        new Point(Width - 1, Height - 1),
                        new Point(0, Height - 1),
                        new Point(0, 0)
                    };

                e.Graphics.DrawLines(rectColor, points);
                e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(2, 1), new Point(1, 2));
                e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(2, Height - 1 - 1), new Point(1, Height - 1 - 2));
                e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(Width - 1 - 2, 1), new Point(Width - 1 - 1, 2));
                e.Graphics.DrawLine(Color.FromArgb(120, rectColor), new Point(Width - 1 - 2, Height - 1 - 1), new Point(Width - 1 - 1, Height - 1 - 2));
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
                    if (WindowState == FormWindowState.Maximized)
                    {
                        e.Graphics.FillRectangle(ControlBoxCloseFillHoverColor, new Rectangle(ControlBoxRect.Left, 0, Width - ControlBoxRect.Left, TitleHeight));
                    }
                    else
                    {
                        e.Graphics.FillRectangle(ControlBoxCloseFillHoverColor, ControlBoxRect);
                    }
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

            if (TextAlignment == StringAlignment.Center)
            {
                e.Graphics.DrawString(Text, TitleFont, titleForeColor, new Rectangle(0, 0, Width, TitleHeight), ContentAlignment.MiddleCenter);
            }
            else
            {
                e.Graphics.DrawString(Text, TitleFont, titleForeColor, new Rectangle(6 + (ShowTitleIcon && Icon != null ? 26 : 0), 0, Width, TitleHeight), ContentAlignment.MiddleLeft);
            }
        }

        private StringAlignment textAlignment = StringAlignment.Near;

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

        public event EventHandler ExtendBoxClick;

        private FieldInfo fieldW;
        private FieldInfo fieldH;
        private bool boundsCoreState = false;

        protected override void SetClientSizeCore(int x, int y)
        {
            if (DesignMode && fieldW != null && fieldH != null)
            {
                fieldW.SetValue(this, x);
                fieldH.SetValue(this, y);
                OnClientSizeChanged(EventArgs.Empty);
                Size = SizeFromClientSize(new Size(x, y));
            }
            else
            {
                base.SetClientSizeCore(x, y);
            }
        }

        protected Padding GetNonClientMetrics()
        {
            var screenRect = ClientRectangle;
            screenRect.Offset(-Bounds.Left, -Bounds.Top);
            var rect = new RECT(screenRect.Left, screenRect.Top, screenRect.Right, screenRect.Bottom);
            Win32.User.AdjustWindowRectEx(ref rect, (int)CreateParams.Style, false, (int)CreateParams.ExStyle);
            return new Padding
            {
                Top = screenRect.Top - rect.Top,
                Left = screenRect.Left - rect.Left,
                Bottom = rect.Bottom - screenRect.Bottom,
                Right = rect.Right - screenRect.Right
            };
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (boundsCoreState && base.WindowState != FormWindowState.Minimized)
            {
                if (y != Top) y = Top;
                if (x != Left) x = Left;
                boundsCoreState = false;
            }

            var size = SetBoundsCore(width, height);
            base.SetBoundsCore(x, y, size.Width, size.Height, specified);
        }

        private Size SetBoundsCore(int width, int height)
        {
            if (base.WindowState == FormWindowState.Normal)
            {
                var boundsSpecified = typeof(Form).GetField("restoredWindowBoundsSpecified", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Form).GetField("_restoredWindowBoundsSpecified", BindingFlags.NonPublic | BindingFlags.Instance);
                var restoredSpecified = (BoundsSpecified)boundsSpecified!.GetValue(this)!;

                if ((restoredSpecified & BoundsSpecified.Size) != BoundsSpecified.None)
                {
                    var boundsField = typeof(Form).GetField("FormStateExWindowBoundsWidthIsClientSize", BindingFlags.NonPublic | BindingFlags.Static);
                    var stateField = typeof(Form).GetField("formStateEx", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Form).GetField("_formStateEx", BindingFlags.NonPublic | BindingFlags.Instance);
                    var restoredField = typeof(Form).GetField("restoredWindowBounds", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Form).GetField("_restoredWindowBounds", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (boundsField != null && stateField != null && restoredField != null)
                    {
                        var restoredBounds = (Rectangle)restoredField.GetValue(this)!;
                        var section = (BitVector32.Section)boundsField.GetValue(this)!;
                        var vector = (BitVector32)stateField.GetValue(this)!;
                        if (vector[section] == 1)
                        {
                            width = restoredBounds.Width;
                            height = restoredBounds.Height;
                        }
                    }
                }
            }

            return new Size(width, height);
        }

        protected override void WndProc(ref Message m)
        {
            var msg = (int)m.Msg;
            switch (msg)
            {
                case Win32.User.WM_ACTIVATE:
                    var margins = new Win32.Dwm.MARGINS(0, 0, 1, 0);
                    Win32.Dwm.DwmExtendFrameIntoClientArea(Handle, ref margins);
                    break;
                case Win32.User.WM_NCCALCSIZE when m.WParam != IntPtr.Zero:
                    if (CalcSize(ref m)) return;
                    break;
            }

            base.WndProc(ref m);
        }

        private bool CalcSize(ref Message m)
        {
            if (FormBorderStyle == FormBorderStyle.None) return false;
#if NET40
            var sizeParams = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
#else
            var sizeParams = Marshal.PtrToStructure<NCCALCSIZE_PARAMS>(m.LParam);
#endif
            var borders = GetNonClientMetrics();

            if (Win32.User.IsZoomed(Handle) == 1)
            {
                sizeParams.rgrc0.Top -= borders.Top;
                sizeParams.rgrc0.Top += borders.Bottom;
                Marshal.StructureToPtr(sizeParams, m.LParam, false);
            }
            else
            {
                m.Result = new IntPtr(1);
                return true;
            }

            m.Result = new IntPtr(0x0400);
            return false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //if (AutoScaleMode == AutoScaleMode.Font) AutoScaleMode = AutoScaleMode.None;
            //if (base.BackColor == SystemColors.Control) base.BackColor = UIStyles.Blue.PageBackColor;

            //Render();
            CalcSystemBoxPos();
            //IsShown = true;
            //SetDPIScale();
            //SetZoomScaleRect();
            //
            //if (AfterShown != null)
            //{
            //    AfterShownTimer = new System.Windows.Forms.Timer();
            //    AfterShownTimer.Tick += AfterShownTimer_Tick;
            //    AfterShownTimer.Start();
            //}
        }
    }
}
