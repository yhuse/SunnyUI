/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UIForm2.cs
 * 文件说明: 窗体基类
 * 当前版本: V3.6
 * 创建日期: 2024-01-20
 *
 * 2024-01-20: V3.6.3 增加文件说明
 * 2024-01-25: V3.6.3 增加主题等
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
    public partial class UIForm2 : Form, IStyleInterface, ITranslate, IFrame
    {
        public UIForm2()
        {
            InitializeComponent();

            this.Register();

            SetStyle(ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint, true);
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

        public readonly Guid Guid = Guid.NewGuid();

        public void Translate()
        {
            List<Control> controls = this.GetInterfaceControls("ITranslate");
            foreach (var control in controls)
            {
                if (control is ITranslate item)
                {
                    item.Translate();
                }
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (ShowTitle && !AllowAddControlOnTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = Padding.Top;
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

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (InControlBox || InMaxBox || InMinBox || InExtendBox) return;
            if (!ShowTitle || e.Y > Padding.Top)
                base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// 是否屏蔽Alt+F4
        /// </summary>
        [Description("是否屏蔽Alt+F4"), Category("Key")]
        [DefaultValue(false)]
        public bool IsForbidAltF4
        {
            get; set;
        }

        [Description("使用Esc键关闭窗口"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool EscClose { get; set; } = false;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.HideComboDropDown();
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
                if (keyData == (Keys.Alt | Keys.F4) && IsForbidAltF4)
                {
                    //屏蔽Alt+F4
                    return true;
                }

                if (keyData == Keys.Escape)
                {
                    DoEsc();
                }

                if (keyData == Keys.Enter)
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

        [Description("窗体关闭时提示文字，为空则不提示"), Category("SunnyUI"), DefaultValue(null)]
        public string CloseAskString
        {
            get; set;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (CloseAskString.IsValid())
            {
                if (!this.ShowAskDialog(CloseAskString, false))
                {
                    e.Cancel = true;
                }
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (MainTabControl != null)
            {
                foreach (var item in MainTabControl.GetControls<UIPage>(true))
                {
                    item.Final();
                    item.Close();
                    item.Dispose();
                }
            }
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
            Invalidate();
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
        /// 重载鼠标按下事件
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

        private bool showTitleIcon = false;

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
            var rect = new Win32.RECT(screenRect.Left, screenRect.Top, screenRect.Right, screenRect.Bottom);
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
                case Win32.User.WM_HOTKEY:
                    int hotKeyId = (int)(m.WParam);
                    if (hotKeys != null && hotKeys.ContainsKey(hotKeyId))
                    {
                        HotKeyEventHandler?.Invoke(this, new HotKeyEventArgs(hotKeys[hotKeyId], DateTime.Now));
                    }
                    break;
            }

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
                    else
                        m.Result = (IntPtr)Win32.User.HTLEFT;
                }
                else if (vPoint.X >= ClientSize.Width - dragSize)
                {
                    if (vPoint.Y <= dragSize)
                        m.Result = (IntPtr)Win32.User.HTTOPRIGHT;
                    else if (vPoint.Y >= ClientSize.Height - dragSize)
                        m.Result = (IntPtr)Win32.User.HTBOTTOMRIGHT;
                    else
                        m.Result = (IntPtr)Win32.User.HTRIGHT;
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
        }

        private bool CalcSize(ref Message m)
        {
            if (FormBorderStyle == FormBorderStyle.None) return false;
#if NET40
            var sizeParams = (Win32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(Win32.NCCALCSIZE_PARAMS));
#else
            var sizeParams = Marshal.PtrToStructure<Win32.NCCALCSIZE_PARAMS>(m.LParam);
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

            if (AutoScaleMode == AutoScaleMode.Font) AutoScaleMode = AutoScaleMode.None;
            if (base.BackColor == SystemColors.Control) base.BackColor = UIStyles.Blue.PageBackColor;

            Render();
            CalcSystemBoxPos();
            SetDPIScale();

            if (AfterShown != null)
            {
                AfterShownTimer = new System.Windows.Forms.Timer();
                AfterShownTimer.Tick += AfterShownTimer_Tick;
                AfterShownTimer.Start();
            }
        }

        private void AfterShownTimer_Tick(object sender, EventArgs e)
        {
            AfterShownTimer.Stop();
            AfterShownTimer.Tick -= AfterShownTimer_Tick;
            AfterShownTimer?.Dispose();
            AfterShownTimer = null;

            AfterShown?.Invoke(this, EventArgs.Empty);
            AfterShown = null;
        }

        public void Render()
        {
            if (!DesignMode && UIStyles.Style.IsValid())
            {
                SetInheritedStyle(UIStyles.Style);
            }
        }

        private System.Windows.Forms.Timer AfterShownTimer;
        public event EventHandler AfterShown;

        #region IFrame实现

        private UITabControl mainTabControl;

        [DefaultValue(null)]
        public UITabControl MainTabControl
        {
            get => mainTabControl;
            set
            {
                mainTabControl = value;
                mainTabControl.Frame = this;

                mainTabControl.PageAdded += DealPageAdded;
                mainTabControl.PageRemoved += DealPageRemoved;
                mainTabControl.Selected += MainTabControl_Selected;
                mainTabControl.Deselected += MainTabControl_Deselected;
                mainTabControl.TabPageAndUIPageChanged += MainTabControl_TabPageAndUIPageChanged;
            }
        }

        private void MainTabControl_TabPageAndUIPageChanged(object sender, TabPageAndUIPageArgs e)
        {
            List<UIPage> pages = e.TabPage.GetControls<UIPage>();
            SelectedPage = pages.Count == 1 ? pages[0] : null;
        }

        private void MainTabControl_Deselected(object sender, TabControlEventArgs e)
        {
            List<UIPage> pages = e.TabPage.GetControls<UIPage>();
            if (pages.Count == 1) pages[0].Final();
        }

        private void MainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            List<UIPage> pages = e.TabPage.GetControls<UIPage>();
            SelectedPage = pages.Count == 1 ? pages[0] : null;
        }

        private UIPage selectedPage = null;
        [Browsable(false)]
        public UIPage SelectedPage
        {
            get => selectedPage;
            private set
            {
                if (selectedPage != value)
                {
                    selectedPage = value;
                    PageSelected?.Invoke(this, new UIPageEventArgs(SelectedPage));
                }
            }
        }

        public event OnUIPageChanged PageSelected;

        public UIPage AddPage(UIPage page, int pageIndex)
        {
            page.PageIndex = pageIndex;
            return AddPage(page);
        }

        public UIPage AddPage(UIPage page, Guid pageGuid)
        {
            page.PageGuid = pageGuid;
            return AddPage(page);
        }

        public UIPage AddPage(UIPage page)
        {
            SetDefaultTabControl();

            if (MainTabControl == null)
            {
                throw (new ApplicationException("未指定MainTabControl，无法承载多页面。"));
            }

            page.Frame = this;
            page.OnFrameDealPageParams += Page_OnFrameDealPageParams;
            MainTabControl?.AddPage(page);
            return page;
        }

        private UIForm2 SetDefaultTabControl()
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

        public virtual bool SelectPage(Guid pageGuid)
        {
            SetDefaultTabControl();
            if (MainTabControl == null) return false;
            return MainTabControl.SelectPage(pageGuid);
        }

        public bool RemovePage(int pageIndex) => MainTabControl?.RemovePage(pageIndex) ?? false;

        public bool RemovePage(Guid pageGuid) => MainTabControl?.RemovePage(pageGuid) ?? false;

        public void RemoveAllPages(bool keepMainPage = true) => MainTabControl?.RemoveAllPages(keepMainPage);

        public UIPage GetPage(int pageIndex) => SetDefaultTabControl().MainTabControl?.GetPage(pageIndex);

        public UIPage GetPage(Guid pageGuid) => SetDefaultTabControl().MainTabControl?.GetPage(pageGuid);

        public bool ExistPage(int pageIndex) => GetPage(pageIndex) != null;

        public bool ExistPage(Guid pageGuid) => GetPage(pageGuid) != null;

        public bool SendParamToPage(int pageIndex, object value)
        {
            SetDefaultTabControl();
            UIPage page = GetPage(pageIndex);
            if (page == null)
            {
                throw new NullReferenceException("未能查找到页面的索引为: " + pageIndex);
            }

            var args = new UIPageParamsArgs(null, page, value);
            page?.DealReceiveParams(args);
            return args.Handled;
        }

        public bool SendParamToPage(Guid pageGuid, object value)
        {
            SetDefaultTabControl();
            UIPage page = GetPage(pageGuid);
            if (page == null)
            {
                throw new NullReferenceException("未能查找到页面的索引为: " + pageGuid);
            }

            var args = new UIPageParamsArgs(null, page, value);
            page?.DealReceiveParams(args);
            return args.Handled;
        }

        private void Page_OnFrameDealPageParams(object sender, UIPageParamsArgs e)
        {
            if (e == null) return;
            if (e.DestPage == null)
            {
                ReceiveParams?.Invoke(this, e);
            }
            else
            {
                e.DestPage?.DealReceiveParams(e);
            }
        }

        public event OnReceiveParams ReceiveParams;

        public T GetPage<T>() where T : UIPage => SetDefaultTabControl().MainTabControl?.GetPage<T>();

        public List<T> GetPages<T>() where T : UIPage => SetDefaultTabControl().MainTabControl?.GetPages<T>();

        public event OnUIPageChanged PageAdded;

        internal void DealPageAdded(object sender, UIPageEventArgs e)
        {
            PageAdded?.Invoke(this, e);
        }

        public event OnUIPageChanged PageRemoved;
        internal void DealPageRemoved(object sender, UIPageEventArgs e)
        {
            PageRemoved?.Invoke(this, e);
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

        #endregion IFrame实现

        #region IStyleInterface
        protected UIStyle _style = UIStyle.Inherited;

        /// <summary>
        /// 配色主题
        /// </summary>
        [Description("配色主题"), Category("SunnyUI")]
        [DefaultValue(UIStyle.Inherited)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        public virtual void SetInheritedStyle(UIStyle style)
        {
            if (!DesignMode)
            {
                this.SuspendLayout();
                UIStyleHelper.SetChildUIStyle(this, style);

                if (_style == UIStyle.Inherited && style.IsValid())
                {
                    SetStyleColor(style.Colors());
                    Invalidate();
                    _style = UIStyle.Inherited;
                }

                UIStyleChanged?.Invoke(this, new EventArgs());
                this.ResumeLayout();
            }
        }

        protected virtual void SetStyle(UIStyle style)
        {
            this.SuspendLayout();

            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
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
            ForeColor = uiColor.FormForeColor;
            BackColor = uiColor.FormBackColor;
            titleColor = uiColor.FormTitleColor;
            titleForeColor = uiColor.FormTitleForeColor;
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

        private float DefaultFontSize = -1;
        private float TitleFontSize = -1;

        public void SetDPIScale()
        {
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            if (TitleFontSize < 0) TitleFontSize = this.TitleFont.Size;

            this.SetDPIScaleFont(DefaultFontSize);
            TitleFont = TitleFont.DPIScaleFont(TitleFontSize);
            foreach (var control in this.GetAllDPIScaleControls())
            {
                control.SetDPIScale();
            }
        }
        #endregion

        #region 一些辅助窗口

        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowSuccessDialog(string msg, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.SuccessTitle, false, UIStyle.Green, showMask, true);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowInfoDialog(string msg, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.InfoTitle, false, UIStyle.Gray, showMask, true);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowWarningDialog(string msg, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.WarningTitle, false, UIStyle.Orange, showMask, true);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowErrorDialog(string msg, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, UILocalize.ErrorTitle, false, UIStyle.Red, showMask, true);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <returns>结果</returns>
        public bool ShowAskDialog(string msg, bool showMask = false, UIMessageDialogButtons defaultButton = UIMessageDialogButtons.Ok)
        {
            return UIMessageDialog.ShowMessageDialog(msg, UILocalize.AskTitle, true, UIStyle.Blue, showMask, true, defaultButton);
        }

        /// <summary>
        /// 正确信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowSuccessDialog(string title, string msg, UIStyle style = UIStyle.Green, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, true);
        }

        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowInfoDialog(string title, string msg, UIStyle style = UIStyle.Gray, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, true);
        }

        /// <summary>
        /// 警告信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowWarningDialog(string title, string msg, UIStyle style = UIStyle.Orange, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, true);
        }

        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        public void ShowErrorDialog(string title, string msg, UIStyle style = UIStyle.Red, bool showMask = false)
        {
            UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, true);
        }

        /// <summary>
        /// 确认信息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">信息</param>
        /// <param name="style">主题</param>
        /// <param name="showMask">显示遮罩层</param>
        /// <returns>结果</returns>
        public bool ShowAskDialog(string title, string msg, UIStyle style = UIStyle.Blue, bool showMask = false, UIMessageDialogButtons defaultButton = UIMessageDialogButtons.Ok)
        {
            return UIMessageDialog.ShowMessageDialog(msg, title, true, style, showMask, true, defaultButton);
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
                    Padding = new Padding(Math.Max(Padding.Left, 2), showTitle ? TitleHeight + 1 : 2, Math.Max(Padding.Right, 2), Math.Max(Padding.Bottom, 2));
                }
                else
                {
                    Padding = new Padding(0, showTitle ? TitleHeight : 0, 0, 0);
                }
            }
        }

        public void RegisterHotKey(Sunny.UI.ModifierKeys modifierKey, Keys key)
        {
            if (hotKeys == null) hotKeys = new ConcurrentDictionary<int, HotKey>();

            int id = HotKey.CalculateID(modifierKey, key);
            if (!hotKeys.ContainsKey(id))
            {
                HotKey newHotkey = new HotKey(modifierKey, key);
                hotKeys.TryAdd(id, newHotkey);
                Win32.User.RegisterHotKey(Handle, id, (int)newHotkey.ModifierKey, (int)newHotkey.Key);
            }
        }

        public void UnRegisterHotKey(Sunny.UI.ModifierKeys modifierKey, Keys key)
        {
            if (hotKeys == null) return;

            int id = HotKey.CalculateID(modifierKey, key);
            if (hotKeys.ContainsKey(id))
            {
                hotKeys.TryRemove(id, out _);
                Win32.User.UnregisterHotKey(Handle, id);
            }
        }

        public event HotKeyEventHandler HotKeyEventHandler;

        private ConcurrentDictionary<int, HotKey> hotKeys;
    }
}
