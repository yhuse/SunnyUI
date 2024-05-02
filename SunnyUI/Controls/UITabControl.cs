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
 * 文件名称: UITabControl.cs
 * 文件说明: 标签控件
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-06-27: V2.2.5 重绘左右选择按钮
 * 2020-08-12: V2.2.7 标题垂直居中
 * 2021-04-01: V3.0.2 修改第一个TabPage关不掉的Bug
 * 2021-06-08: V3.0.4 Tab页标题选中高亮颜色增加可调整高度
 * 2021-07-14: V3.0.5 支持Tab在下方显示
 * 2021-08-14: V3.0.6 增加DisposeTabPageAfterRemove标志，移除TabPage后，是否自动销毁TabPage
 * 2022-01-02: V3.0.9 增加角标
 * 2022-01-13: V3.1.0 修改删除页面时的页面跳转
 * 2022-04-18: V3.1.5 关闭按钮增加鼠标移入的效果
 * 2022-04-20: V3.1.5 不显示标签页时屏蔽左右键
 * 2022-05-11: V3.1.8 修复屏蔽左右键后其他控件无法使用左右键的问题
 * 2022-05-17: V3.1.9 修复了一个首页无法关闭的问题
 * 2022-06-19: V3.2.0 多页面框架关闭页面时执行UIPage的FormClosed事件
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-11-06: V3.5.2 重构主题
 * 2023-12-13: V3.6.2 优化UIPage的Init和Final加载逻辑
******************************************************************************/

using Sunny.UI.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITabControl : TabControl, IStyleInterface, IZoomScale
    {
        private readonly UITabControlHelper Helper;
        private int DrawedIndex = -1;
        private readonly Timer timer;

        public UITabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            UpdateStyles();

            ItemSize = new Size(150, 40);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            base.Font = UIStyles.Font();
            AfterSetFillColor(FillColor);
            Version = UIGlobal.Version;

            Helper = new UITabControlHelper(this);
            Helper.TabPageAndUIPageChanged += Helper_TabPageAndUIPageChanged;
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;

            DisposeTabPageAfterRemove = true;
            AutoClosePage = true;

            tabSelectedForeColor = UIStyles.Blue.TabControlTabSelectedColor;
            tabSelectedHighColor = UIStyles.Blue.TabControlTabSelectedColor;
            _fillColor = UIStyles.Blue.TabControlBackColor;
        }

        private void Helper_TabPageAndUIPageChanged(object sender, TabPageAndUIPageArgs e)
        {
            TabPageAndUIPageChanged?.Invoke(this, e);
        }

        public event TabPageAndUIPageEventHandler TabPageAndUIPageChanged;

        [Browsable(false), DefaultValue(null)]
        public IFrame Frame
        {
            get; set;
        }

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
        public void SetZoomScale(float scale)
        {

        }

        private ConcurrentDictionary<TabPage, string> TipsTexts = new ConcurrentDictionary<TabPage, string>();

        public void SetTipsText(TabPage tabPage, string tipsText)
        {
            if (TipsTexts.ContainsKey(tabPage))
                TipsTexts[tabPage] = tipsText;
            else
                TipsTexts.TryAdd(tabPage, tipsText);

            Invalidate();
        }

        private string GetTipsText(TabPage tabPage)
        {
            return TipsTexts.ContainsKey(tabPage) ? TipsTexts[tabPage] : string.Empty;
        }

        private Color tipsColor = Color.Red;

        [Description("角标背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Red")]
        public Color TipsColor
        {
            get => tipsColor;
            set
            {
                tipsColor = value;
                Invalidate();
            }
        }

        private Color tipsForeColor = Color.White;

        [DefaultValue(typeof(Color), "White"), Category("SunnyUI"), Description("角标文字颜色")]
        public Color TipsForeColor
        {
            get => tipsForeColor;
            set
            {
                tipsForeColor = value;
                Invalidate();
            }
        }

        private Font tipsFont = UIStyles.SubFont();

        [Description("角标文字字体"), Category("SunnyUI")]
        [DefaultValue(typeof(Font), "宋体, 9pt")]
        public Font TipsFont
        {
            get { return tipsFont; }
            set
            {
                if (!tipsFont.Equals(value))
                {
                    tipsFont = value;
                    Invalidate();
                }
            }
        }

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();
        }

        private string mainPage = "";

        [DefaultValue(true)]
        [Description("主页名称，此页面不显示关闭按钮"), Category("SunnyUI")]
        public string MainPage
        {
            get => mainPage;
            set
            {
                mainPage = value;
                Invalidate();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            DrawedIndex = SelectedIndex;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ForbidCtrlTab)
            {
                switch (keyData)
                {
                    case (Keys.Tab | Keys.Control):
                        //组合键在调试时，不容易捕获；可以先按住Ctrl键（此时在断点处已经捕获），然后按下Tab键，都释放后，再进入断点处单步向下执行；
                        //此时会分两次进入断点，第一次（好像）是处理Ctrl键，第二次处理组合键
                        return true;
                }
            }

            if (Focused && !TabVisible)
            {
                switch (keyData)
                {
                    case Keys.Left:
                        //if (TabVisible)
                        return true;
                    case Keys.Right:
                        //if (TabVisible)
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        [DefaultValue(true)]
        [Description("是否禁用Ctrl+Tab"), Category("SunnyUI")]
        public bool ForbidCtrlTab { get; set; } = true;

        public bool SelectPage(int pageIndex) => Helper.SelectPage(pageIndex);

        public bool SelectPage(Guid pageGuid) => Helper.SelectPage(pageGuid);

        public bool RemovePage(int pageIndex) => Helper.RemovePage(pageIndex);

        public bool RemovePage(Guid guid) => Helper.RemovePage(guid);

        public void RemoveAllPages(bool keepMainPage = true) => Helper.RemoveAllPages(keepMainPage);

        public UIPage GetPage(int pageIndex) => Helper.GetPage(pageIndex);

        public UIPage GetPage(Guid guid) => Helper.GetPage(guid);

        public void SetTipsText(int pageIndex, string tipsText) => Helper.SetTipsText(pageIndex, tipsText);

        public void SetTipsText(Guid guid, string tipsText) => Helper.SetTipsText(guid, tipsText);

        public void AddPages(params UIPage[] pages)
        {
            foreach (var page in pages) AddPage(page);
        }

        public void AddPage(UIPage page)
        {
            Helper.AddPage(page);
            PageAdded?.Invoke(this, new UIPageEventArgs(page));
        }

        internal event OnUIPageChanged PageAdded;
        internal event OnUIPageChanged PageRemoved;

        public void AddPage(int pageIndex, UITabControl page) => Helper.AddPage(pageIndex, page);

        public void AddPage(int pageIndex, UITabControlMenu page) => Helper.AddPage(pageIndex, page);

        public void AddPage(Guid guid, UITabControl page) => Helper.AddPage(guid, page);

        public void AddPage(Guid guid, UITabControlMenu page) => Helper.AddPage(guid, page);

        public T GetPage<T>() where T : UIPage => Helper.GetPage<T>();

        public List<T> GetPages<T>() where T : UIPage => Helper.GetPages<T>();

        public string Version
        {
            get;
        }

        private Color _fillColor = UIColor.LightBlue;
        private Color tabBackColor = Color.FromArgb(56, 56, 56);

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
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        private HorizontalAlignment textAlignment = HorizontalAlignment.Center;

        [DefaultValue(HorizontalAlignment.Center)]
        [Description("文字显示方向"), Category("SunnyUI")]
        public HorizontalAlignment TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        private bool tabVisible = true;

        [DefaultValue(true)]
        [Description("标签页是否显示"), Category("SunnyUI")]
        public bool TabVisible
        {
            get => tabVisible;
            set
            {
                tabVisible = value;
                if (!tabVisible)
                {
                    ItemSize = new Size(0, 1);
                }
                else
                {
                    if (ItemSize == new Size(0, 1))
                    {
                        ItemSize = new Size(150, 40);
                    }
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                AfterSetFillColor(value);
                Invalidate();
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "56, 56, 56")]
        public Color TabBackColor
        {
            get => tabBackColor;
            set
            {
                if (tabBackColor != value)
                {
                    tabBackColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedColor = Color.FromArgb(36, 36, 36);

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页背景色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "36, 36, 36")]
        public Color TabSelectedColor
        {
            get => tabSelectedColor;
            set
            {
                if (tabSelectedColor != value)
                {
                    tabSelectedColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedForeColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页字体色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedForeColor
        {
            get => tabSelectedForeColor;
            set
            {
                if (tabSelectedForeColor != value)
                {
                    tabSelectedForeColor = value;
                    Invalidate();
                }
            }
        }

        private Color tabUnSelectedForeColor = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("未选中Tab页字体色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "240, 240, 240")]
        public Color TabUnSelectedForeColor
        {
            get => tabUnSelectedForeColor;
            set
            {
                if (tabUnSelectedForeColor != value)
                {
                    tabUnSelectedForeColor = value;
                    _menuStyle = UIMenuStyle.Custom;
                    Invalidate();
                }
            }
        }

        private Color tabSelectedHighColor = UIColor.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("选中Tab页高亮"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TabSelectedHighColor

        {
            get => tabSelectedHighColor;
            set
            {
                if (tabSelectedHighColor != value)
                {
                    tabSelectedHighColor = value;
                    Invalidate();
                }
            }
        }

        private int tabSelectedHighColorSize = 4;

        /// <summary>
        /// 选中Tab页高亮高度
        /// </summary>
        [Description("选中Tab页高亮高度"), Category("SunnyUI")]
        [DefaultValue(4)]
        public int TabSelectedHighColorSize

        {
            get => tabSelectedHighColorSize;
            set
            {
                value = Math.Max(value, 0);
                value = Math.Min(value, 8);
                tabSelectedHighColorSize = value;
                Invalidate();
            }
        }

        private UIStyle _style = UIStyle.Inherited;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Inherited), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        [Browsable(false)]
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                if (tabVisible)
                {
                    return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
                }
                else
                {
                    return new Rectangle(rect.Left - 4, rect.Top - 5, rect.Width + 8, rect.Height + 9);
                }
            }
        }

        private void AfterSetFillColor(Color color)
        {
            foreach (TabPage page in TabPages)
            {
                page.BackColor = color;
            }
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

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            tabSelectedForeColor = uiColor.TabControlTabSelectedColor;
            tabSelectedHighColor = uiColor.TabControlTabSelectedColor;
            _fillColor = uiColor.TabControlBackColor;
        }

        private UIMenuStyle _menuStyle = UIMenuStyle.Black;

        [DefaultValue(UIMenuStyle.Black)]
        [Description("主题风格"), Category("SunnyUI")]
        public UIMenuStyle MenuStyle
        {
            get => _menuStyle;
            set
            {
                if (value != UIMenuStyle.Custom)
                {
                    SetMenuStyle(UIStyles.MenuColors[value]);
                }

                _menuStyle = value;
            }
        }

        private void SetMenuStyle(UIMenuColor uiColor)
        {
            tabBackColor = uiColor.BackColor;
            tabSelectedColor = uiColor.SelectedColor;
            tabUnSelectedForeColor = uiColor.UnSelectedForeColor;
            Invalidate();
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            Appearance = TabAppearance.Normal;
            //Alignment = TabAlignment.Top;
        }

        private bool showCloseButton;

        [DefaultValue(false), Description("所有Tab页面标题显示关闭按钮"), Category("SunnyUI")]
        public bool ShowCloseButton
        {
            get => showCloseButton;
            set
            {
                if (showCloseButton != value)
                {
                    showCloseButton = value;
                    if (showActiveCloseButton) showActiveCloseButton = false;
                    Invalidate();
                }
            }
        }

        private bool showActiveCloseButton;

        [DefaultValue(false), Description("当前激活的Tab页面标题显示关闭按钮"), Category("SunnyUI")]
        public bool ShowActiveCloseButton
        {
            get => showActiveCloseButton;
            set
            {
                if (showActiveCloseButton != value)
                {
                    showActiveCloseButton = value;
                    if (showCloseButton) showCloseButton = false;
                    Invalidate();
                }
            }
        }

        private ConcurrentDictionary<int, bool> CloseRects = new ConcurrentDictionary<int, bool>();

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 绘制背景色
            e.Graphics.Clear(TabBackColor);

            if (!TabVisible)
            {
                return;
            }

            for (int index = 0; index <= TabCount - 1; index++)
            {
                Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, ItemSize.Width, ItemSize.Height);
                if (Alignment == TabAlignment.Bottom)
                {
                    TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y + 2, ItemSize.Width, ItemSize.Height);
                }

                Size sf = TextRenderer.MeasureText(TabPages[index].Text, Font);
                int textLeft = ImageList?.ImageSize.Width ?? 0;
                if (ImageList != null) textLeft += 4 + 4 + 6;
                if (TextAlignment == HorizontalAlignment.Right)
                    textLeft = (int)(TabRect.Width - 4 - sf.Width);
                if (TextAlignment == HorizontalAlignment.Center)
                    textLeft = textLeft + (int)((TabRect.Width - textLeft - sf.Width) / 2.0f);

                // 绘制标题
                e.Graphics.FillRectangle(tabBackColor, TabRect);
                if (index == SelectedIndex)
                {
                    e.Graphics.FillRectangle(TabSelectedColor, TabRect);
                    if (TabSelectedHighColorSize > 0)
                        e.Graphics.FillRectangle(TabSelectedHighColor, TabRect.Left, TabRect.Height - TabSelectedHighColorSize, TabRect.Width, TabSelectedHighColorSize);
                }

                e.Graphics.DrawString(TabPages[index].Text, Font, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor,
                    new Rectangle(TabRect.Left + textLeft, TabRect.Top, TabRect.Width, TabRect.Height), ContentAlignment.MiddleLeft);

                var menuItem = Helper[index];
                bool show1 = TabPages[index].Text != MainPage;
                bool show2 = menuItem == null || !menuItem.AlwaysOpen;
                bool showButton = show1 && show2;

                if (showButton)
                {
                    if (ShowCloseButton || (ShowActiveCloseButton && index == SelectedIndex))
                    {
                        Color color = TabUnSelectedForeColor;
                        if (CloseRects.ContainsKey(index) && CloseRects[index])
                        {
                            color = tabSelectedForeColor;
                        }

                        e.Graphics.DrawFontImage(77, 28, color, new Rectangle(TabRect.Left + TabRect.Width - 28, 0, 24, TabRect.Height));
                    }
                }

                // 绘制图标
                if (ImageList != null)
                {
                    int imageIndex = TabPages[index].ImageIndex;
                    if (imageIndex >= 0 && imageIndex < ImageList.Images.Count)
                    {
                        e.Graphics.DrawImage(ImageList.Images[imageIndex], TabRect.Left + 4 + 6, TabRect.Y + (TabRect.Height - ImageList.ImageSize.Height) / 2.0f, ImageList.ImageSize.Width, ImageList.ImageSize.Height);
                    }
                }

                string TipsText = GetTipsText(TabPages[index]);
                if (Enabled && TipsText.IsValid())
                {
                    using var TempFont = TipsFont.DPIScaleFont(TipsFont.Size);
                    sf = TextRenderer.MeasureText(TipsText, TempFont);
                    int sfMax = Math.Max(sf.Width, sf.Height);
                    int x = TabRect.Width - 1 - 2 - sfMax;
                    if (showActiveCloseButton || ShowCloseButton) x -= 24;
                    int y = 1 + 1;
                    e.Graphics.FillEllipse(TipsColor, TabRect.Left + x - 1, y, sfMax, sfMax);
                    e.Graphics.DrawString(TipsText, TempFont, TipsForeColor, new Rectangle(TabRect.Left + x, y, sfMax, sfMax), ContentAlignment.MiddleCenter);
                }
            }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ShowActiveCloseButton || ShowCloseButton)
            {
                for (int index = 0; index <= TabCount - 1; index++)
                {
                    Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, ItemSize.Width, ItemSize.Height);
                    Rectangle closeRect = new Rectangle(TabRect.Right - 28, 0, 28, TabRect.Height);
                    bool inrect = e.Location.InRect(closeRect);
                    if (!CloseRects.ContainsKey(index))
                        CloseRects.TryAdd(index, false);

                    if (inrect != CloseRects[index])
                    {
                        CloseRects[index] = inrect;
                        Invalidate();
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

            int removeIndex = -1;
            for (int index = 0; index <= TabCount - 1; index++)
            {
                Rectangle TabRect = new Rectangle(GetTabRect(index).Location.X - 2, GetTabRect(index).Location.Y - 2, ItemSize.Width, ItemSize.Height);
                Rectangle rect = new Rectangle(TabRect.Right - 28, TabRect.Top, 24, TabRect.Height);
                if (e.Location.InRect(rect))
                {
                    removeIndex = index;
                    break;
                }
            }

            if (removeIndex < 0 || removeIndex >= TabCount)
            {
                return;
            }

            var menuItem = Helper[removeIndex];
            bool show1 = TabPages[removeIndex].Text != MainPage;
            bool show2 = menuItem == null || !menuItem.AlwaysOpen;
            bool showButton = show1 && show2;
            if (showButton)
            {
                if (ShowCloseButton)
                {
                    if (BeforeRemoveTabPage == null || BeforeRemoveTabPage.Invoke(this, removeIndex))
                    {
                        RemoveTabPage(removeIndex);
                    }
                }
                else if (ShowActiveCloseButton && removeIndex == SelectedIndex)
                {
                    if (DrawedIndex == removeIndex)
                    {
                        if (BeforeRemoveTabPage == null || BeforeRemoveTabPage.Invoke(this, removeIndex))
                        {
                            RemoveTabPage(removeIndex);
                        }
                    }
                }
            }
        }

        public delegate bool OnBeforeRemoveTabPage(object sender, int index);

        public delegate void OnAfterRemoveTabPage(object sender, int index);

        public event OnBeforeRemoveTabPage BeforeRemoveTabPage;

        public event OnAfterRemoveTabPage AfterRemoveTabPage;

        [DefaultValue(true)]
        [Description("多页面框架时，包含UIPage，在点击Tab页关闭时关闭UIPage"), Category("SunnyUI")]
        public bool AutoClosePage
        {
            get; set;
        }

        [DefaultValue(true)]
        [Description("移除TabPage后，是否自动销毁TabPage"), Category("SunnyUI")]
        public bool DisposeTabPageAfterRemove
        {
            get; set;
        }

        internal void RemoveTabPage(int index)
        {
            if (index < 0 || index >= TabCount)
            {
                return;
            }

            TabPage tabPage = TabPages[index];
            var pages = tabPage.GetControls<UIPage>();
            for (int i = 0; i < pages.Count; i++)
            {
                if (AutoClosePage)
                {
                    PageRemoved?.Invoke(this, new UIPageEventArgs(pages[i]));

                    try
                    {
                        pages[i].Final();
                        pages[i].Close();
                        pages[i].Dispose();
                        pages[i] = null;
                    }
                    catch
                    { }
                }
                else
                {
                    pages[i].Parent = null;
                }
            }

            if (TabCount > 1 && index > 0)
            {
                SelectedTab = TabPages[index - 1];
            }

            TabPages.Remove(tabPage);
            AfterRemoveTabPage?.Invoke(this, index);

            //if (TabCount > 0)
            //{
            //    if (index == 0) SelectedIndex = 0;
            //    if (index > 0) SelectedIndex = index - 1;
            //}

            if (DisposeTabPageAfterRemove) tabPage.Dispose();
        }

        public enum UITabPosition
        {
            Left,
            Right
        }

        [DefaultValue(UITabPosition.Left)]
        [Description("标签页显示位置"), Category("SunnyUI")]
        public UITabPosition TabPosition
        {
            get => (RightToLeftLayout && RightToLeft == RightToLeft.Yes)
                ? UITabPosition.Right
                : UITabPosition.Left;
            set
            {
                RightToLeftLayout = value == UITabPosition.Right;
                RightToLeft = (value == UITabPosition.Right) ? RightToLeft.Yes : RightToLeft.No;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            CloseRects.Clear();
            Init();
            if (ShowActiveCloseButton && !ShowCloseButton)
            {
                timer.Start();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            timer?.Start();
        }

        //private int LastIndex;

        public void Init()
        {
            if (SelectedIndex < 0 || SelectedIndex >= TabPages.Count)
            {
                return;
            }

            if (SelectedIndex >= 0)
            {
                List<UIPage> pages = TabPages[SelectedIndex].GetControls<UIPage>();
                foreach (var page in pages)
                {
                    page.ReLoad();
                }
            }

            List<UITabControlMenu> leftTabControls = TabPages[SelectedIndex].GetControls<UITabControlMenu>();
            foreach (var tabControl in leftTabControls)
            {
                tabControl.Init();
            }

            List<UITabControl> topTabControls = TabPages[SelectedIndex].GetControls<UITabControl>();
            foreach (var tabControl in topTabControls)
            {
                tabControl.Init();
            }
        }

        internal IntPtr UpDownButtonHandle => FindUpDownButton();

        private IntPtr FindUpDownButton()
        {
            return User.FindWindowEx(Handle, IntPtr.Zero, UpDownButtonClassName, null).IntPtr();
        }

        public void OnPaintUpDownButton(UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            Color upButtonArrowColor = tabUnSelectedForeColor;
            Color downButtonArrowColor = tabUnSelectedForeColor;

            Rectangle upButtonRect = rect;
            upButtonRect.X = 0;
            upButtonRect.Y = 0;
            upButtonRect.Width = rect.Width / 2 - 1;
            upButtonRect.Height -= 1;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right + 1;
            downButtonRect.Y = 0;
            downButtonRect.Width = rect.Width / 2 - 1;
            downButtonRect.Height -= 1;
            g.Clear(tabBackColor);

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        //鼠标按下
                        if (e.MouseInUpButton)
                            upButtonArrowColor = Color.FromArgb(200, TabSelectedHighColor);
                        else
                            downButtonArrowColor = Color.FromArgb(200, TabSelectedHighColor);
                    }
                    else
                    {
                        //鼠标移动
                        if (e.MouseInUpButton)
                            upButtonArrowColor = TabSelectedHighColor;
                        else
                            downButtonArrowColor = TabSelectedHighColor;
                    }
                }
            }
            else
            {
                upButtonArrowColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            RenderButton(g, upButtonRect, upButtonArrowColor, ArrowDirection.Left);
            RenderButton(g, downButtonRect, downButtonArrowColor, ArrowDirection.Right);
            UpDownButtonPaintEventHandler handler = Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            handler?.Invoke(this, e);
        }

        private static void RenderButton(Graphics g, Rectangle rect, Color arrowColor, ArrowDirection direction)
        {
            switch (direction)
            {
                case ArrowDirection.Left:
                    g.DrawFontImage(61700, 24, arrowColor, rect);
                    break;

                case ArrowDirection.Right:
                    g.DrawFontImage(61701, 24, arrowColor, rect, 1);
                    break;
            }
        }

        private static readonly object EventPaintUpDownButton = new object();
        private const string UpDownButtonClassName = "msctls_updown32";
        private UpDownButtonNativeWindow _upDownButtonNativeWindow;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_upDownButtonNativeWindow != null)
            {
                _upDownButtonNativeWindow.Dispose();
                _upDownButtonNativeWindow = null;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }

            if (e.Control is TabPage)
            {
                e.Control.Padding = new Padding(0);
                if (ShowActiveCloseButton && !ShowCloseButton)
                {
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private UITabControl _owner;
            private bool _bPainting;
            private Rectangle clipRect;

            public UpDownButtonNativeWindow(UITabControl owner)
            {
                _owner = owner;
                AssignHandle(owner.UpDownButtonHandle);
            }

            private static bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (User.GetKeyState(User.VK_RBUTTON) < 0);
                }

                return (User.GetKeyState(User.VK_LBUTTON) < 0);
            }

            private void DrawUpDownButton()
            {
                RECT rect = new RECT();
                bool mousePress = LeftKeyPressed();
                Point cursorPoint = SystemEx.GetCursorPos();
                User.GetWindowRect(Handle, ref rect);
                var mouseOver = User.PtInRect(ref rect, cursorPoint);
                cursorPoint.X -= rect.Left;
                cursorPoint.Y -= rect.Top;
                var mouseInUpButton = cursorPoint.X < clipRect.Width / 2;
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    UpDownButtonPaintEventArgs e = new UpDownButtonPaintEventArgs(g, clipRect, mouseOver, mousePress, mouseInUpButton);
                    _owner.OnPaintUpDownButton(e);
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case User.WM_PAINT:
                        if (!_bPainting)
                        {
                            int itemTop = 0;
                            if (_owner.Alignment == TabAlignment.Top)
                            {
                                itemTop = 0;
                            }
                            else if (_owner.Alignment == TabAlignment.Bottom)
                            {
                                itemTop = _owner.Size.Height - _owner.ItemSize.Height;
                            }
                            Point UpDownButtonLocation = new Point(_owner.Size.Width - 52, itemTop);
                            Size UpDownButtonSize = new Size(52, _owner.ItemSize.Height);
                            clipRect = new Rectangle(UpDownButtonLocation, UpDownButtonSize);
                            User.MoveWindow(Handle, UpDownButtonLocation.X, UpDownButtonLocation.Y, clipRect.Width, clipRect.Height);

                            PAINTSTRUCT ps = new PAINTSTRUCT();
                            _bPainting = true;
                            User.BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            User.EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = Win32Helper.TRUE;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            #region IDisposable 成员

            /// <summary>
            /// 析构函数
            /// </summary>
            public void Dispose()
            {
                _owner = null;
                base.ReleaseHandle();
            }

            #endregion IDisposable 成员
        }

        public delegate void UpDownButtonPaintEventHandler(object sender, UpDownButtonPaintEventArgs e);

        public class UpDownButtonPaintEventArgs : PaintEventArgs
        {
            public UpDownButtonPaintEventArgs(
                Graphics graphics,
                Rectangle clipRect,
                bool mouseOver,
                bool mousePress,
                bool mouseInUpButton)
                : base(graphics, clipRect)
            {
                MouseOver = mouseOver;
                MousePress = mousePress;
                MouseInUpButton = mouseInUpButton;
            }

            public bool MouseOver
            {
                get;
            }

            public bool MousePress
            {
                get;
            }

            public bool MouseInUpButton
            {
                get;
            }
        }
    }
}