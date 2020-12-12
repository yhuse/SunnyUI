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
 * 2020-05-30: V2.2.5 更新标题移动、双击最大化/正常、到顶最大化、最大化后拖拽正常
 * 2020-07-01: V2.2.6 仿照QQ，重绘标题栏按钮
 * 2020-07-05: V2.2.6 更新窗体控制按钮圆角和跟随窗体圆角变化。
 * 2020-09-17: V2.2.7 重写WindowState相关代码
 * 2020-09-17: V2.2.7 增加了窗体可拉拽调整大小ShowDragStretch属性
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIForm : Form, IStyleInterface
    {
        private readonly UIButton btn = new UIButton();

        public readonly Guid Guid = Guid.NewGuid();

        public UIForm()
        {
            base.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);//设置最大化尺寸
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
            FormBorderStyle = FormBorderStyle.None;
            m_aeroEnabled = false;
        }

        //不显示FormBorderStyle属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
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

            if (ShowTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = Padding.Top;
            }
        }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

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
        [Description("控件版本"), Category("SunnyUI"), DefaultValue(true)]
        public string Version { get; }

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
                titleHeight = Math.Max(value, 31);
                Padding = new Padding(0, showTitle ? titleHeight : 0, 0, 0);
                Invalidate();
                CalcSystemBoxPos();
            }
        }

        /// <summary>
        /// 标题栏颜色
        /// </summary>
        private Color titleColor = UIColor.Blue;

        /// <summary>
        /// 标题栏颜色
        /// </summary>
        [Description("标题栏颜色"), Category("SunnyUI"), DefaultValue(typeof(Color), "80, 160, 255")]
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
        private Color titleForeColor = Color.White;

        /// <summary>
        /// 标题颜色
        /// </summary>
        [Description("标题前景色（标题颜色）"), Category("SunnyUI"), DefaultValue(typeof(Color), "White")]
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

        private Rectangle ControlBoxRect;

        private Rectangle MaximizeBoxRect;

        private Rectangle MinimizeBoxRect;

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
            }
            else
            {
                MaximizeBoxRect = MinimizeBoxRect = ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
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

        protected Color foreColor = UIFontColor.Primary;

        protected Color rectColor = UIColor.Blue;

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
        [Description("边框颜色"), Category("SunnyUI")]
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
            }
        }

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
            if (WindowState == FormWindowState.Normal)
            {
                size = Size;
                // 若窗体从正常模式->最大化模式，该操作是由移动窗体至顶部触发的，记录的是移动前的窗体位置
                location = IsOnMoving ? FormLocation : Location;
                Width = ShowFullScreen ? screen.Bounds.Width : screen.WorkingArea.Width;
                Height = ShowFullScreen ? screen.Bounds.Height : screen.WorkingArea.Height;
                Left = screen.Bounds.Left;
                Top = screen.Bounds.Top;
                GDIEx.SetFormRoundRectRegion(this, 0);
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                if (size.Width == 0 || size.Height == 0)
                {
                    size = new Size(800, 600);
                }

                Size = size;
                Point center = new Point(screen.Bounds.Left + screen.WorkingArea.Width / 2 - Size.Width / 2,
                    screen.Bounds.Top + screen.WorkingArea.Height / 2 - Size.Height / 2);

                if (location.X == 0 && location.Y == 0) location = center;
                Location = location;
                GDIEx.SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
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

            if (InControlBox || InMaxBox || InMinBox) return;
            if (!ShowTitle) return;
            if (e.Y > Padding.Top) return;

            if (e.Button == MouseButtons.Left)
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
            if (InControlBox || InMaxBox || InMinBox) return;
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

                    if (isChange)
                    {
                        Invalidate();
                    }
                }
                else
                {
                    InControlBox = InMaxBox = InMinBox = false;
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InControlBox = InMaxBox = InMinBox = false;
            Invalidate();
        }

        private bool InControlBox, InMaxBox, InMinBox;

        /// <summary>
        /// 是否屏蔽Alt+F4
        /// </summary>
        [Description("是否屏蔽Alt+F4"), Category("Key")]
        [DefaultValue(false)]
        public bool IsForbidAltF4 { get; set; }

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

            //Color showTitleColor = IsDesignMode || IsActive ? rectColor : Color.FromArgb(173, 178, 181);
            Color showTitleColor = rectColor;

            if (ShowTitle)
            {
                e.Graphics.FillRectangle(showTitleColor, 0, 0, Width, TitleHeight);
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

                e.Graphics.DrawLines(showTitleColor, points);

                if (!unShowRadius)
                {
                    e.Graphics.DrawLine(Color.FromArgb(120, showTitleColor), new Point(2, 1), new Point(1, 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, showTitleColor), new Point(2, Height - 1 - 1), new Point(1, Height - 1 - 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, showTitleColor), new Point(Width - 1 - 2, 1), new Point(Width - 1 - 1, 2));
                    e.Graphics.DrawLine(Color.FromArgb(120, showTitleColor), new Point(Width - 1 - 2, Height - 1 - 1), new Point(Width - 1 - 1, Height - 1 - 2));
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
                        e.Graphics.FillRoundRectangle(UIColor.Red, ControlBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(UIColor.Red, ControlBoxRect);
                }

                //e.Graphics.DrawFontImage(61453, 24, Color.White, ControlBoxRect, 1);

                e.Graphics.DrawLine(Color.White,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 - 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 - 5,
                    ControlBoxRect.Left + ControlBoxRect.Width / 2 + 5,
                    ControlBoxRect.Top + ControlBoxRect.Height / 2 + 5);
                e.Graphics.DrawLine(Color.White,
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
                        e.Graphics.FillRoundRectangle(btn.FillHoverColor, MaximizeBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(btn.FillHoverColor, MaximizeBoxRect);
                }

                // e.Graphics.DrawFontImage(
                //     windowState == FormWindowState.Maximized
                //         ? FontAwesomeIcons.fa_window_restore
                //         : FontAwesomeIcons.fa_window_maximize, 24, Color.White, MaximizeBoxRect, 1);

                if (WindowState == FormWindowState.Maximized)
                {
                    e.Graphics.DrawRectangle(Color.White,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 1,
                        7, 7);

                    e.Graphics.DrawLine(Color.White,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 1,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4);

                    e.Graphics.DrawLine(Color.White,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 - 2,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4);

                    e.Graphics.DrawLine(Color.White,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 - 4,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3);

                    e.Graphics.DrawLine(Color.White,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 5,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3,
                        MaximizeBoxRect.Left + MaximizeBoxRect.Width / 2 + 3,
                        MaximizeBoxRect.Top + MaximizeBoxRect.Height / 2 + 3);
                }

                if (WindowState == FormWindowState.Normal)
                {
                    e.Graphics.DrawRectangle(Color.White,
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
                        e.Graphics.FillRoundRectangle(btn.FillHoverColor, MinimizeBoxRect, 5);
                    else
                        e.Graphics.FillRectangle(btn.FillHoverColor, MinimizeBoxRect);
                }

                e.Graphics.DrawLine(Color.White,
                    MinimizeBoxRect.Left + MinimizeBoxRect.Width / 2 - 6,
                    MinimizeBoxRect.Top + MinimizeBoxRect.Height / 2,
                    MinimizeBoxRect.Left + MinimizeBoxRect.Width / 2 + 6,
                    MinimizeBoxRect.Top + MinimizeBoxRect.Height / 2);
                //e.Graphics.DrawFontImage(62161, 24, Color.White, MinimizeBoxRect, 1);
            }

            e.Graphics.SetDefaultQuality();

            if (ShowIcon && Icon != null)
            {
                e.Graphics.DrawImage(Icon.ToBitmap(), 6, (TitleHeight - 24) / 2, 24, 24);
            }

            SizeF sf = e.Graphics.MeasureString(Text, Font);
            if (TextAlignment == StringAlignment.Center)
            {
                e.Graphics.DrawString(Text, Font, titleForeColor, (Width - sf.Width) / 2, (TitleHeight - sf.Height) / 2);
            }
            else
            {
                e.Graphics.DrawString(Text, Font, titleForeColor, 6 + (ShowIcon && Icon != null ? 26 : 0), (TitleHeight - sf.Height) / 2);
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
        public bool StyleCustomMode { get; set; }

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
            CalcSystemBoxPos();
            SetRadius();
            isShow = true;
        }

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

        [Description("使用Esc键关闭窗口"), Category("Key")]
        [DefaultValue(true)]
        public bool EscClose { get; set; } = true;

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
                GDIEx.SetFormRoundRectRegion(this, 0);
            }
            else
            {
                GDIEx.SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
            }

            Invalidate();
        }

        private StringAlignment textAlignment = StringAlignment.Near;

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

        public string CloseAskString { get; set; }

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
                    Padding = new Padding(2, showTitle ? TitleHeight : 2, 2, 2);
                }
                else
                {
                    ShowRect = false;
                    Padding = new Padding(0, showTitle ? TitleHeight : 0, 0, 0);
                }
            }
        }

        #region 拉拽调整窗体大小

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

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
        public void ShowStatusForm(int maximum = 100, string desc = "系统正在处理中，请稍候...")
        {
            UIStatusFormService.ShowStatusForm(maximum, desc);
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
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.SuccessTitle, false, UIStyle.Green, showMask);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowInfoDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.InfoTitle, false, UIStyle.Gray, showMask);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowWarningDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.WarningTitle, false, UIStyle.Orange, showMask);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowErrorDialog(string msg, bool showMask = true)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.ErrorTitle, false, UIStyle.Red, showMask);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <returns>结果</returns>
        public bool ShowAskDialog(string msg, bool showMask = true)
        {
            return UIMessageDialog.ShowMessageDialog(msg, UILocalize.AskTitle, true, UIStyle.Blue, showMask);
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
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask);
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
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask);
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
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask);
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
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask);
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
            return UIMessageDialog.ShowMessageDialog(msg, title, true, style, showMask);
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
            UINotifierHelper.ShowNotifier(desc, UINotifierType.INFO, UILocalize.InfoTitle, false, timeout);
        }

        public void ShowSuccessNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.OK, UILocalize.SuccessTitle, false, timeout);
        }

        public void ShowWarningNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.WARNING, UILocalize.WarningTitle, false, timeout);
        }

        public void ShowErrorNotifier(string desc, bool isDialog = false, int timeout = 2000)
        {
            UINotifierHelper.ShowNotifier(desc, UINotifierType.ERROR, UILocalize.ErrorTitle, false, timeout);
        }

        #endregion
    }
}