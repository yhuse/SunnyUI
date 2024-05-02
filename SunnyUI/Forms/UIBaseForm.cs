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
 * 文件名称: UIBaseForm.cs
 * 文件说明: 窗体基类
 * 当前版本: V3.6
 * 创建日期: 2024-04-29
 *
 * 2024-04-29: V3.6.5 增加文件说明
 * 2024-04-29: V3.6.5 删除ShowTitleIcon，默认使用ShowIcon
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIBaseForm : Form, IFrame, IStyleInterface, ITranslate
    {
        public UIBaseForm()
        {
            InitializeComponent();

            this.Register();
            Version = UIGlobal.Version;

            controlBoxForeColor = UIStyles.Blue.FormControlBoxForeColor;
            controlBoxFillHoverColor = UIStyles.Blue.FormControlBoxFillHoverColor;
            controlBoxCloseFillHoverColor = UIStyles.Blue.FormControlBoxCloseFillHoverColor;
            rectColor = UIStyles.Blue.FormRectColor;
            ForeColor = UIStyles.Blue.FormForeColor;
            titleColor = UIStyles.Blue.FormTitleColor;
            titleForeColor = UIStyles.Blue.FormTitleForeColor;
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

        private System.Windows.Forms.Timer AfterShownTimer;
        public event EventHandler AfterShown;

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

        public event EventHandler UIStyleChanged;

        public void Render()
        {
            if (!DesignMode && UIStyles.Style.IsValid())
            {
                SetInheritedStyle(UIStyles.Style);
            }
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

        protected bool showDragStretch;

        protected void SetPadding()
        {
            if (showDragStretch)
            {
                Padding = new Padding(Math.Max(Padding.Left, 2), showTitle ? TitleHeight + 1 : 2, Math.Max(Padding.Right, 2), Math.Max(Padding.Bottom, 2));
            }
            else
            {
                Padding = new Padding(0, showTitle ? TitleHeight : 0, 0, 0);
            }
        }

        [Browsable(false)]
        [Description("显示标题栏图标"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowTitleIcon { get; set; }

        protected Image IconToImage(Icon icon)
        {
            MemoryStream mStream = new MemoryStream();
            icon.Save(mStream);
            Image image = Image.FromStream(mStream);
            return image;
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

        protected bool InControlBox, InMaxBox, InMinBox, InExtendBox;

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

        protected ConcurrentDictionary<int, HotKey> hotKeys;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString
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

        [Description("窗体关闭时提示文字，为空则不提示"), Category("SunnyUI"), DefaultValue(null)]
        public string CloseAskString
        {
            get; set;
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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.HideComboDropDown();
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


        [DefaultValue(true)]
        [Description("是否点击标题栏可以移动窗体"), Category("SunnyUI")]
        public bool Movable { get; set; } = true;

        [DefaultValue(false)]
        [Description("允许在标题栏放置控件"), Category("SunnyUI")]
        public bool AllowAddControlOnTitle
        {
            get; set;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (ShowTitle && !AllowAddControlOnTitle && e.Control.Top < TitleHeight)
            {
                e.Control.Top = Padding.Top;
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

        internal Rectangle ControlBoxRect;

        internal Rectangle MaximizeBoxRect;

        internal Rectangle MinimizeBoxRect;

        internal Rectangle ExtendBoxRect;

        internal int ControlBoxLeft;

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

        /// <summary>
        /// 标题字体
        /// </summary>
        protected Font titleFont = UIStyles.Font();

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
        /// 标题栏高度
        /// </summary>
        protected int titleHeight = 35;

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

        protected virtual void CalcSystemBoxPos() { }

        /// <summary>
        /// 是否显示窗体的标题栏
        /// </summary>
        protected bool showTitle = true;

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

        public readonly Guid Guid = Guid.NewGuid();

        protected FormWindowState lastWindowState = FormWindowState.Normal;
        public event OnWindowStateChanged WindowStateChanged;

        protected void DoWindowStateChanged(FormWindowState thisState)
        {
            lastWindowState = thisState;
            DoWindowStateChanged(thisState, WindowState);
        }

        protected void DoWindowStateChanged(FormWindowState thisState, FormWindowState lastState)
        {
            WindowStateChanged?.Invoke(this, thisState, lastState);

            foreach (var page in UIStyles.Pages.Values)
            {
                page.DoWindowStateChanged(thisState, lastState);
            }
        }

        protected virtual void AfterSetRectColor(Color color) { }

        public event EventHandler RectColorChanged;

        /// <summary>
        /// 标题颜色
        /// </summary>
        protected Color titleForeColor;

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

        protected Color titleColor;

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

        protected Color controlBoxCloseFillHoverColor;

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

        protected Color controlBoxForeColor = Color.White;

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

        protected Color controlBoxFillHoverColor;

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

        /// <summary>
        /// 当前控件的版本
        /// </summary>
        [Description("控件版本"), Category("SunnyUI")]
        public string Version
        {
            get;
        }

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

        private UIBaseForm SetDefaultTabControl()
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
    }
}
