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
 * 文件名称: UITabControl.cs
 * 文件说明: 标签控件
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-06-27: V2.2.5 重绘左右选择按钮
 * 2020-08-12: V2.2.7 标题垂直居中
******************************************************************************/

using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITabControl : TabControl, IStyleInterface
    {
        private readonly UITabControlHelper Helper;
        private int DrawedIndex = -1;
        private readonly Timer timer = new Timer();

        public UITabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            ItemSize = new Size(150, 40);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Font = UIFontColor.Font;
            AfterSetFillColor(FillColor);
            Size = new Size(450, 270);
            Version = UIGlobal.Version;

            Helper = new UITabControlHelper(this);
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
        }

        ~UITabControl()
        {
            timer.Stop();
            timer.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            DrawedIndex = SelectedIndex;
        }

        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);

            if (ShowActiveCloseButton && !ShowCloseButton)
            {
                timer.Start();
            }
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

            return base.ProcessCmdKey(ref msg, keyData);
        }

        [DefaultValue(true)]
        [Description("是否禁用Ctrl+Tab"), Category("SunnyUI")]
        public bool ForbidCtrlTab { get; set; } = true;

        public void SelectPage(int pageIndex) => Helper.SelectPage(pageIndex);

        public void SelectPage(Guid pageGuid) => Helper.SelectPage(pageGuid);

        public void AddPage(UIPage page) => Helper.AddPage(page);

        public void AddPages(params UIPage[] pages)
        {
            foreach (var page in pages) AddPage(page);
        }

        public void AddPage(int pageIndex, UITabControl page) => Helper.AddPage(pageIndex, page);

        public void AddPage(int pageIndex, UITabControlMenu page) => Helper.AddPage(pageIndex, page);

        public void AddPage(Guid guid, UITabControl page) => Helper.AddPage(guid, page);

        public void AddPage(Guid guid, UITabControlMenu page) => Helper.AddPage(guid, page);

        public string Version { get; }

        private Color _fillColor = UIColor.LightBlue;
        private Color tabBackColor = Color.FromArgb(56, 56, 56);

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
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
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                AfterSetFillColor(value);
                _style = UIStyle.Custom;
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
                tabBackColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
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
                tabSelectedColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
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
                tabSelectedForeColor = value;
                _style = UIStyle.Custom;
                Invalidate();
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
                tabUnSelectedForeColor = value;
                _menuStyle = UIMenuStyle.Custom;
                Invalidate();
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
                tabSelectedHighColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
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

        public void SetStyle(UIStyle style)
        {
            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            tabSelectedForeColor = tabSelectedHighColor = uiColor.MenuSelectedColor;
            _fillColor = uiColor.PlainColor;
            Invalidate();
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
            Alignment = TabAlignment.Top;
        }

        private bool showCloseButton;

        [DefaultValue(false), Description("所有Tab页面标题显示关闭按钮"), Category("SunnyUI")]
        public bool ShowCloseButton
        {
            get => showCloseButton;
            set
            {
                showCloseButton = value;
                if (showActiveCloseButton) showActiveCloseButton = false;
                Invalidate();
            }
        }

        private bool showActiveCloseButton;

        [DefaultValue(false), Description("当前激活的Tab页面标题显示关闭按钮"), Category("SunnyUI")]
        public bool ShowActiveCloseButton
        {
            get => showActiveCloseButton;
            set
            {
                showActiveCloseButton = value;
                if (showCloseButton) showCloseButton = false;
                Invalidate();
            }
        }

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

                Bitmap bmp = new Bitmap(TabRect.Width, TabRect.Height);
                Graphics g = Graphics.FromImage(bmp);

                SizeF sf = e.Graphics.MeasureString(TabPages[index].Text, Font);
                int textLeft = ImageList?.ImageSize.Width ?? 0;
                if (ImageList != null) textLeft += 4 + 4 + 6;
                if (TextAlignment == HorizontalAlignment.Right)
                    textLeft = (int)(TabRect.Width - 4 - sf.Width);
                if (TextAlignment == HorizontalAlignment.Center)
                    textLeft = textLeft + (int)((TabRect.Width - textLeft - sf.Width) / 2.0f);

                // 绘制标题
                g.Clear(TabBackColor);
                if (index == SelectedIndex)
                {
                    g.Clear(TabSelectedColor);
                    g.FillRectangle(TabSelectedHighColor, 0, bmp.Height - 4, bmp.Width, 4);
                }

                g.DrawString(TabPages[index].Text, Font, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, textLeft, TabRect.Top + 2 + (TabRect.Height - sf.Height - 4) / 2.0f);

                var menuItem = Helper[index];
                bool showButton = menuItem == null || !menuItem.AlwaysOpen;

                if (showButton)
                {
                    if (ShowCloseButton || (ShowActiveCloseButton && index == SelectedIndex))
                    {
                        g.DrawFontImage(77, 28, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, new Rectangle(TabRect.Width - 28, TabRect.Top, 24, TabRect.Height));
                    }
                }

                // 绘制图标
                if (ImageList != null)
                {
                    int imageIndex = TabPages[index].ImageIndex;
                    if (imageIndex >= 0 && imageIndex < ImageList.Images.Count)
                    {
                        g.DrawImage(ImageList.Images[imageIndex], 4 + 6, TabRect.Y + (TabRect.Height - ImageList.ImageSize.Height) / 2.0f, ImageList.ImageSize.Width, ImageList.ImageSize.Height);
                    }
                }

                if (RightToLeftLayout && RightToLeft == RightToLeft.Yes)
                {
                    bmp = bmp.HorizontalFlip();
                }

                e.Graphics.DrawImage(bmp, TabRect.Left, TabRect.Top);
                bmp.Dispose();
            }
        }

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
            bool showButton = menuItem == null || !menuItem.AlwaysOpen;
            if (showButton)
            {
                if (ShowCloseButton)
                {
                    if (BeforeRemoveTabPage == null || (BeforeRemoveTabPage != null && BeforeRemoveTabPage.Invoke(this, removeIndex)))
                    {
                        RemoveTabPage(removeIndex);
                    }
                }
                else if (ShowActiveCloseButton && removeIndex == SelectedIndex)
                {
                    if (DrawedIndex == removeIndex)
                    {
                        if (BeforeRemoveTabPage == null || (BeforeRemoveTabPage != null && BeforeRemoveTabPage.Invoke(this, removeIndex)))
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

        internal void RemoveTabPage(int index)
        {
            if (index < 0 || index >= TabCount)
            {
                return;
            }

            TabPages.Remove(TabPages[index]);
            AfterRemoveTabPage?.Invoke(this, index);

            if (TabCount == 0) return;
            if (index == 0) SelectedIndex = 0;
            if (index > 0) SelectedIndex = index - 1;
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
            Init(SelectedIndex);
        }

        public void Init(int index = 0)
        {
            if (index < 0 || index >= TabPages.Count)
            {
                return;
            }

            if (SelectedIndex != index)
                SelectedIndex = index;

            List<UIPage> pages = TabPages[SelectedIndex].GetControls<UIPage>();
            foreach (var page in pages)
            {
                page.Init();
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

        internal void RenderButton(Graphics g, Rectangle rect, Color arrowColor, ArrowDirection direction)
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
            }
        }

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

            private bool LeftKeyPressed()
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
                            Point UpDownButtonLocation = new Point(_owner.Size.Width - 52, 0);
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

            public bool MouseOver { get; }

            public bool MousePress { get; }

            public bool MouseInUpButton { get; }
        }
    }
}