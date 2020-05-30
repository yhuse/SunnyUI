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
 * 文件名称: UIForm.cs
 * 文件说明: 窗体基类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIForm : Form, IStyleInterface
    {

        public UIForm()
        {
            InitializeComponent();

            base.BackColor = UIColor.LightBlue;

            if (this.Register())
            {
                SetStyle(UIStyles.Style);
            }

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            base.DoubleBuffered = true;
            Padding = new Padding(0, titleHeight, 0, 0);
            UpdateStyles();

            base.Font = UIFontColor.Font;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Version = UIGlobal.Version;
        }


        #region Public Attributes
        [DefaultValue(null)]
        public string TagString { get; set; }

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
                if (value)
                    minimizeBox = true;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

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
                if (!value)
                    maximizeBox = false;
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        /// <summary>
        /// 当前控件的版本
        /// </summary>
        [Description("控件版本"), Category("SunnyUI"), DefaultValue(true)]
        public string Version { get; }


        /// <summary>
        /// 是否以全屏模式进入最大化
        /// </summary>
        [Description("是否以全屏模式进入最大化"), Category("WindowStyle"), DefaultValue(false)]
        public bool ShowFullScreen { get; set; }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        [Description("标题栏高度"), Category("Appearance"), DefaultValue(35)]
        public int TitleHeight
        {
            get => titleHeight;
            set
            {
                titleHeight = Math.Max(value, 0);
                CalcSystemBoxPos();
                Invalidate();
            }
        }

        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏颜色"), Category("Appearance"), DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TitleColor
        {
            get => titleColor;
            set
            {
                titleColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 标题颜色
        /// </summary>
        [Description("标题前景色（标题颜色）"), Category("Appearance"), DefaultValue(typeof(Color), "White")]
        public Color TitleForeColor
        {
            get => titleForeColor;
            set
            {
                titleForeColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("背景颜色"), Category("Appearance")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    _style = UIStyle.Custom;
                    AfterSetForeColor(ForeColor);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        /// <value>The color of the border style.</value>
        [Description("边框颜色"), Category("Appearance")]
        public Color RectColor
        {
            get => rectColor;
            set
            {
                rectColor = value;
                AfterSetRectColor(value);
                RectColorChanged?.Invoke(this, null);
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置或获取显示器边缘停留的最大时间(ms)，默认500ms
        /// </summary>
        [Description("设置或获取在显示器边缘停留的最大时间(ms)"), Category("SunnyUI")]
        [DefaultValue(500)]
        public long StickyBorderTime
        {
            get { return _StickyBorderTime / 10000; }
            set { _StickyBorderTime = value / 10000; }
        }

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        [Description("是否显示圆角"), Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowRadius
        {
            get => _showRadius;
            set
            {
                _showRadius = value;
                SetRadius();
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("Appearance")]
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

        
        [Description("使用Esc键关闭窗口"), Category("Key")]
        [DefaultValue(true)]
        public bool EscClose { get; set; } = true;

        /// <summary>
        /// 是否屏蔽Alt+F4
        /// </summary>
        [Description("是否屏蔽Alt+F4"), Category("Key")]
        [DefaultValue(false)]
        public bool IsForbidAltF4 { get; set; }

        /// <summary>
        /// 配色主题
        /// </summary>
        [Description("配色主题"), Category("Appearance")]
        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        [Description("自定义主题模式（开启后全局主题更改将对当前窗体无效）"), Category("Appearance")]
        [DefaultValue(false)]
        public bool StyleCustomMode { get; set; }

        [Description("文字对齐方式"), Category("Appearance")]
        public StringAlignment TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        public string CloseAskString { get; set; }

        #endregion


        #region Private Attributes

        /// <summary>
        /// 是否显示窗体的标题栏
        /// </summary>
        private bool showTitle = true;

        /// <summary>
        /// 是否显示窗体的控制按钮
        /// </summary>
        private bool controlBox = true;

        /// <summary>
        /// 是否显示窗体的最大化按钮
        /// </summary>
        private bool maximizeBox = true;

        /// <summary>
        /// 是否显示窗体的最小化按钮
        /// </summary>
        private bool minimizeBox = true;

        /// <summary>
        /// 标题栏高度
        /// </summary>
        private int titleHeight = 35;

        /// <summary>
        /// 标题栏颜色
        /// </summary>
        private Color titleColor = UIColor.Blue;

        /// <summary>
        /// 标题颜色
        /// </summary>
        private Color titleForeColor = Color.White;

        protected Color foreColor = UIFontColor.Primary;

        protected Color rectColor = UIColor.Blue;

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        private bool _showRadius = true;

        /// <summary>
        /// 是否重绘边框样式
        /// </summary>
        private bool _showRect = true;

        private Rectangle ControlBoxRect;

        private Rectangle MaximizeBoxRect;

        private Rectangle MinimizeBoxRect;

        private int ControlBoxLeft;

        private StringAlignment textAlignment = StringAlignment.Near;

        private long _StickyBorderTime = 5000000;

        #endregion


        #region Public Variables

        public event EventHandler RectColorChanged;

        public new FormWindowState WindowState
        {
            get => windowState;
            set
            {
                if (value == FormWindowState.Minimized)
                {
                    base.WindowState = FormWindowState.Minimized;
                    return;
                }

                ShowMaximize();
                windowState = value;
            }
        }

        public readonly Guid Guid = Guid.NewGuid();

        public UIStatusForm StatusForm;

        private FormWindowState windowState = FormWindowState.Normal;

        #endregion


        #region Private Variables

        /// <summary>
        /// 窗体最大化前的大小
        /// </summary>
        private Size size;

        /// <summary>
        /// 窗体最大化前所处的位置
        /// </summary>
        private Point mLocation;


        /// <summary>
        /// 是否触发在显示器边缘停留事件
        /// </summary>
        private bool IsStayAtTopBorder = false;

        /// <summary>
        /// 显示器边缘停留事件被触发的时间
        /// </summary>
        private long TopBorderStayTicks;

        private bool InControlBox, InMaxBox, InMinBox;

        private bool isShow;

        /// <summary>
        /// 鼠标在窗体上移动时，鼠标的位置
        /// </summary>
        private Point MousePos;

        /// <summary>
        /// 鼠标左键按下时，窗体的位置
        /// </summary>
        private Point FormLocation;

        /// <summary>
        /// 鼠标左键按下时，鼠标的位置
        /// </summary>
        private Point mouseOffset;

        private readonly UIButton btn = new UIButton();

        private bool TitleMouseDown = false;

        #endregion


        #region Public Methods

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

        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr wnd, int hRgn, Boolean bRedraw);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRegion(Form form, int rgnRadius)
        {
            if (form != null && form.FormBorderStyle == FormBorderStyle.None)
            {
                int region = CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                SetWindowRgn(form.Handle, region, true);
                DeleteObject(region);
            }
        }

        public void SetStyle(UIStyle style)
        {
            this.SetChildUIStyle(style);
            btn.SetStyle(style);

            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            rectColor = uiColor.RectColor;
            foreColor = UIFontColor.Primary;
            BackColor = uiColor.PlainColor;
            titleColor = uiColor.TitleColor;
            titleForeColor = uiColor.TitleForeColor;
            Invalidate();
        }

        /// <summary>
        /// 显示进度窗口
        /// </summary>
        /// <param name="title">进度窗口的标题</param>
        /// <param name="desc">进度窗口的描述</param>
        /// <param name="max">最大进度值</param>
        /// <param name="value">当前进度值</param>
        public void ShowStatus(string title, string desc, int max = 100, int value = 0)
        {
            if (StatusForm == null)
            {
                StatusForm = new UIStatusForm();
            }

            StatusForm.Style = Style;
            StatusForm.Show(title, desc, max, value);
        }

        /// <summary>
        /// 隐藏进度窗口
        /// </summary>
        public void HideStatus()
        {
            StatusForm.Hide();
        }

        /// <summary>
        /// 使进度条按步长自增一次
        /// </summary>
        public void StatusStepIt()
        {
            StatusForm.StepIt();
        }

        #endregion


        #region Private Methods

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
                    MinimizeBoxRect =
                        new Rectangle(MaximizeBox ? MaximizeBoxRect.Left - 28 - 2 : ControlBoxRect.Left - 28 - 2,
                            ControlBoxRect.Top, 28, 28);
                    ControlBoxLeft = MinimizeBoxRect.Left - 2;
                }
                else
                {
                    MinimizeBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
                }
            }
            else
            {
                MaximizeBoxRect = MinimizeBoxRect = ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }
        }

        /// <summary>
        /// 获取当前鼠标活动区域所属的监视器
        /// </summary>
        /// <returns>Screen</returns>
        private Screen GetMouseActiveScreen(Point mPnt)
        {
            int screenIndex = 0;
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (mPnt.InRect(Screen.AllScreens[i].Bounds))
                {
                    screenIndex = i;
                    break;
                }
            }
            return Screen.AllScreens[screenIndex];
        }

        private void ShowMaximize(bool IsOnMoving = false)
        {
            Screen screen = GetMouseActiveScreen(MousePos);

            if (windowState == FormWindowState.Normal)
            {
                size = Size;
                if (IsOnMoving)
                {
                    mLocation = FormLocation;
                }
                else
                {
                    mLocation = Location;
                }

                Width = ShowFullScreen ? screen.Bounds.Width : screen.WorkingArea.Width;
                Height = ShowFullScreen ? screen.Bounds.Height : screen.WorkingArea.Height;
                Left = screen.Bounds.Left;
                Top = screen.Bounds.Top;
                StartPosition = FormStartPosition.Manual;
                SetFormRoundRectRegion(this, 0);

                windowState = FormWindowState.Maximized;
            }
            else if (windowState == FormWindowState.Maximized)
            {
                if (size.Width == 0 || size.Height == 0)
                {
                    size = new Size(800, 600);
                }
                if (mLocation.IsEmpty)
                {
                    mLocation = screen.WorkingArea.Location;
                }
                Size = size;
                Location = mLocation;
                StartPosition = FormStartPosition.CenterScreen;
                SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
                windowState = FormWindowState.Normal;
            }

            Invalidate();
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr handle, int wMsg, int wParam, int lParam);

        private void SetRadius()
        {
            if (DesignMode)
            {
                return;
            }

            if (windowState == FormWindowState.Maximized)
            {
                SetFormRoundRectRegion(this, 0);
            }
            else
            {
                SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
            }

            Invalidate();
        }

        private void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseAskString.IsValid())
            {
                if (!this.ShowAskDialog(CloseAskString))
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion


        #region Protected Methods

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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                if (InControlBox)
                {
                    Close();
                    InControlBox = false;
                }

                if (InMinBox)
                {
                    base.WindowState = FormWindowState.Minimized;
                    InMinBox = false;
                }

                if (InMaxBox)
                {
                    ShowMaximize();
                    InMaxBox = false;
                }
            }
        }

        protected UIStyle _style = UIStyle.Blue;

        protected virtual void AfterSetBackColor(Color color)
        {
        }

        protected virtual void AfterSetRectColor(Color color)
        {
        }

        protected virtual void AfterSetForeColor(Color color)
        {

        }

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

            if (ShowTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = TitleHeight;
            }
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
        #endregion


        #region override functions

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (InControlBox || InMaxBox || InMinBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;

            if (e.Button == MouseButtons.Left)
            {
                TitleMouseDown = true;
                FormLocation = Location;
                mouseOffset = MousePosition;
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (!MaximizeBox) return;
            if (InControlBox || InMaxBox || InMinBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;

            ShowMaximize();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            
            if (!IsDisposed && TitleMouseDown)
            {
                Screen screen = GetMouseActiveScreen(PointToScreen(e.Location));
                if (MousePos.Y == screen.WorkingArea.Top && MaximizeBox)
                {
                    ShowMaximize(true);
                }
                if(Top < screen.WorkingArea.Top) // 防止窗体上移时标题栏超出容器，导致后续无法移动
                {
                    Top = screen.WorkingArea.Top;
                }
                else if(screen.WorkingArea.Bottom - Top < 10) // 防止窗体下移时标题栏超出容器，导致后续无法移动
                {
                    Top = screen.WorkingArea.Bottom - 10;
                }
            }
            // 鼠标抬起后强行关闭粘滞并恢复鼠标移动区域
            IsStayAtTopBorder = false;
            Cursor.Clip = new Rectangle();
            TitleMouseDown = false;
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            MousePos = PointToScreen(e.Location);

            Point pt = MousePosition;

            if (TitleMouseDown && !pt.Equals(mouseOffset))
            {
                if (this.windowState == FormWindowState.Maximized)
                {
                    int MaximizedWidth = Width;
                    int LocationX = Left;
                    ShowMaximize();
                    // 计算等比例缩放后，鼠标与原位置的相对位移
                    float offsetXRatio = 1 - (float)Width / MaximizedWidth;
                    mouseOffset.X -= (int)((mouseOffset.X - LocationX) * offsetXRatio);
                }
                int offsetX = mouseOffset.X - pt.X;
                int offsetY = mouseOffset.Y - pt.Y;
                Rectangle WorkingArea = Screen.GetWorkingArea(this);
                /// 若当前鼠标停留在容器上边缘，将会触发一个时间为MaximumBorderInterval(ms)的边缘等待，
                /// 若此时结束移动，窗口将自动最大化，该功能为上下排列的多监视器提供
                /// 此处判断设置为特定值的好处是，若快速移动窗体跨越监视器，很难触发停留事件
                if (MousePos.Y - WorkingArea.Top == 0)
                {
                    if (!IsStayAtTopBorder)
                    {
                        Cursor.Clip = WorkingArea;
                        TopBorderStayTicks = DateTime.Now.Ticks;
                        IsStayAtTopBorder = true;
                    }
                    else if (DateTime.Now.Ticks - TopBorderStayTicks > _StickyBorderTime)
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
                    if (inControlBox != InControlBox)
                    {
                        InControlBox = inControlBox;
                        Invalidate();
                    }

                    bool inMaxBox = e.Location.InRect(MaximizeBoxRect);
                    if (inMaxBox != InMaxBox)
                    {
                        InMaxBox = inMaxBox;
                        Invalidate();
                    }

                    bool inMinBox = e.Location.InRect(MinimizeBoxRect);
                    if (inMinBox != InMinBox)
                    {
                        InMinBox = inMinBox;
                        Invalidate();
                    }
                }
                else
                {
                    InControlBox = InMaxBox = InMinBox = false;
                }
            }
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

            //Color titleColor = rectColor;// IsDesignMode ? rectColor : IsActive ? rectColor : Color.FromArgb(173, 178, 181);

            if (ShowTitle)
            {
                e.Graphics.FillRectangle(new SolidBrush(titleColor), 0, 0, Width, TitleHeight);
            }

            if (ShowRect)
            {
                Point[] points;

                bool unShowRadius = !ShowRadius || windowState == FormWindowState.Maximized ||
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
                    e.Graphics.FillRoundRectangle(UIColor.Red, ControlBoxRect, 5);
                }

                e.Graphics.DrawFontImage(61453, 24, Color.White, ControlBoxRect, 1);
            }

            if (MaximizeBox)
            {
                if (InMaxBox)
                {
                    e.Graphics.FillRoundRectangle(new SolidBrush(btn.FillHoverColor), MaximizeBoxRect, 5);
                }

                e.Graphics.DrawFontImage(
                    windowState == FormWindowState.Maximized
                        ? FontAwesomeIcons.fa_window_restore
                        : FontAwesomeIcons.fa_window_maximize, 24, Color.White, MaximizeBoxRect, 1);
            }

            if (MinimizeBox)
            {
                if (InMinBox)
                {
                    e.Graphics.FillRoundRectangle(new SolidBrush(btn.FillHoverColor), MinimizeBoxRect, 5);
                }

                e.Graphics.DrawFontImage(62161, 24, Color.White, MinimizeBoxRect, 1);
            }

            e.Graphics.SetDefaultQuality();

            SizeF sf = e.Graphics.MeasureString(Text, Font);
            if (TextAlignment == StringAlignment.Center)
            {
                e.Graphics.DrawString(Text, Font, titleForeColor, (Width - sf.Width) / 2, (TitleHeight - sf.Height) / 2);
            }
            else
            {
                e.Graphics.DrawString(Text, Font, titleForeColor, 6, (TitleHeight - sf.Height) / 2);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
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
            CalcSystemBoxPos();

            if (isShow)
            {
                SetRadius();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            CalcSystemBoxPos();
            SetRadius();
            isShow = true;
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
        #endregion


    }
}