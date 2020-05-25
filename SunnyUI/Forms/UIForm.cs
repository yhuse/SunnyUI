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
        private readonly UIButton btn = new UIButton();

        public readonly Guid Guid = Guid.NewGuid();

        public UIStatusForm StatusForm;

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
            AddMousePressMove(this);
        }

        public void ShowStatus(string title, string desc, int max = 100, int value = 0)
        {
            if (StatusForm == null)
            {
                StatusForm = new UIStatusForm();
            }

            StatusForm.Style = Style;
            StatusForm.Show(title, desc, max, value);
        }

        public void HideStatus()
        {
            StatusForm.Hide();
        }

        public void StatusStepIt()
        {
            StatusForm.StepIt();
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

            if (ShowTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = TitleHeight;
            }
        }

        [DefaultValue(null)]
        public string TagString { get; set; }

        private bool showTitle = true;

        [DefaultValue(true)]
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

        private bool controlBox = true;

        [DefaultValue(true)]
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

        private bool maximizeBox = true;

        [DefaultValue(true)]
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

        private bool minimizeBox = true;

        [DefaultValue(true)]
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

        public string Version { get; }

        public virtual void Init()
        {
        }

        public virtual void Final()
        {
        }

        /// <summary>
        /// 最大化时全屏
        /// </summary>
        [DefaultValue(false)]
        public bool ShowFullScreen { get; set; }

        private int titleHeight = 35;

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

        private Color titleColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
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

        private Color titleForeColor = Color.White;

        [DefaultValue(typeof(Color), "White")]
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
            if (form.FormBorderStyle == FormBorderStyle.None)
            {
                int region = CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                SetWindowRgn(form.Handle, region, true);
                DeleteObject(region);
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
        [Description("背景颜色"), Category("自定义")]
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
        [Description("边框颜色")]
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

        }
        public bool FormSizeable = true;
        private Size memorizedSize;
        private Point memorizedLocation;
        private void ShowMaximize(bool MoveAtNormalize = true)
        {
            if (FormSizeable)
            {
                if (windowState == FormWindowState.Normal)
                {
                    memorizedSize = Size;
                    memorizedLocation = Location;
                    var formWorkingArea = Screen.GetWorkingArea(this);
                    var formBounds = Screen.GetBounds(formWorkingArea.Location);
                    Width = ShowFullScreen ? formBounds.Width : formWorkingArea.Width;
                    Height = ShowFullScreen ? formBounds.Height : formWorkingArea.Height;
                    Left = ShowFullScreen ? formBounds.Left : formWorkingArea.Left;
                    Top = ShowFullScreen ? formBounds.Top : formWorkingArea.Top;
                    StartPosition = FormStartPosition.Manual;
                    SetFormRoundRectRegion(this, 0);

                    windowState = FormWindowState.Maximized;
                }
                else if (windowState == FormWindowState.Maximized)
                {
                    if (memorizedSize.Width == 0 || memorizedSize.Height == 0)
                    {
                        memorizedSize = new Size(800, 600);
                    }

                    Size = memorizedSize;
                    if (MoveAtNormalize)
                    {
                        Location = memorizedLocation;
                    }
                    StartPosition = FormStartPosition.CenterScreen;
                    SetFormRoundRectRegion(this, ShowRadius ? 5 : 0);
                    windowState = FormWindowState.Normal;
                }

                Invalidate();
            }
        }

        private bool isMouseMoved = false;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                if (isMouseDown) // 若鼠标按下时触发事件
                {
                    if (isMoveAtMouseDown) // 若鼠标按下时触发移动
                    {
                        int MvX = MousePosition.X - Mx;
                        int MvY = MousePosition.Y - My;
                        if (MvX != 0 || MvY != 0)
                        {
                            if (windowState == FormWindowState.Maximized)
                            {
                                // 记录最大化时窗体宽度
                                int pWidth = Width;
                                // 计算鼠标X与当前最大化左侧的位置差
                                int dMx = Mx - Left;
                                // 还原窗体
                                ShowMaximize(false);
                                // 计算缩放后，窗体宽度等比变化的情况下，窗体的实际左边距
                                Fx = Mx - Width * dMx / pWidth;
                            }
                            isMouseMoved = true;
                            Left = Fx + MvX;
                            Top = Fy + MvY;
                        }
                    }
                }
                else // 若鼠标没有按下
                {
                    // 如果鼠标经过控制按钮且焦点改变，刷新窗体
                    bool tmpStatus = e.Location.InRect(ControlBoxRect);
                    if ((tmpStatus = e.Location.InRect(ControlBoxRect)) != InControlBox)
                    {
                        InControlBox = tmpStatus;
                        Invalidate();
                    }
                    else if ((tmpStatus = e.Location.InRect(MaximizeBoxRect)) != InMaxBox)
                    {
                        InMaxBox = tmpStatus;
                        Invalidate();
                    }
                    else if ((tmpStatus = e.Location.InRect(MinimizeBoxRect)) != InMinBox)
                    {
                        InMinBox = tmpStatus;
                        Invalidate();
                    }
                }
            }
            else
            {
                InControlBox = InMaxBox = InMinBox = false;
            }
        }
        private int Mx;
        private int My;
        private int Fx;
        private int Fy;
        private bool MouseDownAtClose = false;
        private bool MouseDownAtMaximum = false;
        private bool MouseDownAtMinimum = false;
        private bool isMouseDown = false;
        private bool isMoveAtMouseDown = false;
        /// <summary>
        /// Handles the MouseDown event of the c control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void ctrlMouseDown(object sender, MouseEventArgs e)
        {
            Mx = MousePosition.X;
            My = MousePosition.Y;
            Fx = Left;
            Fy = Top;
            // 按下鼠标时针对窗体控制按钮检测并重绘
            if (MouseDownAtClose = e.Location.InRect(ControlBoxRect))
            {
                InControlBox = true;
                Invalidate();
            }
            else if (MouseDownAtMaximum = e.Location.InRect(MaximizeBoxRect))
            {
                InMaxBox = true;
                Invalidate();
            }
            else if (MouseDownAtMinimum = e.Location.InRect(MinimizeBoxRect))
            {
                InMinBox = true;
                Invalidate();
            }
            // 若控制键被按下，则不允许响应移动
            isMoveAtMouseDown = !(MouseDownAtClose || MouseDownAtMinimum || MouseDownAtMaximum);
            isMouseDown = true;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!IsDisposed)
            {
                isMouseDown = false;
                isMoveAtMouseDown = false;
                if (isMouseMoved && MousePosition.Y == 0)
                {
                    ShowMaximize();
                    isMouseMoved = false;
                }

                var formWorkingArea = Screen.GetWorkingArea(this);
                if (Top < 0)
                {
                    Top = 0;
                }
                else if (Top > formWorkingArea.Bottom)
                {
                    Top = formWorkingArea.Bottom - 20;
                }

                if (FormBorderStyle == FormBorderStyle.None)
                {
                    // 鼠标键抬起时，再次检测鼠标是否活动在控制按钮区域，若按下与抬起均相同，则响应事件
                    if (MouseDownAtClose && e.Location.InRect(ControlBoxRect))
                    {
                        Close();
                    }

                    if (MouseDownAtMinimum && e.Location.InRect(MinimizeBoxRect))
                    {
                        base.WindowState = FormWindowState.Minimized;
                    }

                    if (MouseDownAtMaximum && e.Location.InRect(MaximizeBoxRect))
                    {
                        ShowMaximize();
                    }

                    InControlBox = InMinBox = InMaxBox = false;
                    MouseDownAtClose = MouseDownAtMinimum = MouseDownAtMaximum = false;
                    Invalidate();
                }
            }
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            ShowMaximize();
        }
        private bool InControlBox, InMaxBox, InMinBox;

        /// <summary>
        /// 是否屏蔽Alt+F4
        /// </summary>
        [Description("是否屏蔽Alt+F4")]
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

                e.Graphics.DrawFontImage(61453, 24, Color.White, ControlBoxRect, 2);
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
                        : FontAwesomeIcons.fa_window_maximize, 24, Color.White, MaximizeBoxRect, 2);
            }

            if (MinimizeBox)
            {
                if (InMinBox)
                {
                    e.Graphics.FillRoundRectangle(new SolidBrush(btn.FillHoverColor), MinimizeBoxRect, 5);
                }

                e.Graphics.DrawFontImage(62161, 24, Color.White, MinimizeBoxRect, 2);
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

        protected UIStyle _style = UIStyle.Blue;

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

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

        private bool _showRadius = true;

        /// <summary>
        /// 是否重绘边框样式
        /// </summary>
        private bool _showRect = true;

        /// <summary>
        /// 是否显示圆角
        /// </summary>
        [Description("是否显示圆角")]
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
        [Description("是否显示边框")]
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

        [DefaultValue(true)]
        [Description("Esc键关闭窗口")]
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

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr handle, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 通过Windows的API控制窗体的拖动
        /// </summary>
        public static void MousePressMove(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, 0x0112, 0xF010 + 0x0002, 0);
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
                    ctrl.MouseDown += ctrlMouseDown;
                }
            }
        }

        //private void ctrlMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        Left = Fx + (MousePosition.X - Mx);
        //        Top = Fy + (MousePosition.Y - My);
        //    }
        //}
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

        private FormWindowState windowState = FormWindowState.Normal;

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
    }
}