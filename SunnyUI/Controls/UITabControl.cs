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
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITabControl : TabControl, IStyleInterface
    {
        private readonly UITabControlHelper Helper;

        public UITabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            ItemSize = new Size(150, 40);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Font = UIFontColor.Font;
            AfterSetFillColor(FillColor);
            Size = new Size(450, 270);
            Version = UIGlobal.Version;

            Helper = new UITabControlHelper(this);
        }

        public void SelectPage(int pageIndex)
        {
            Helper.SelectPage(pageIndex);
        }

        public void SelectPage(Guid pageGuid)
        {
            Helper.SelectPage(pageGuid);
        }

        public void AddPage(UIPage page)
        {
            Helper.AddPage(page);
        }

        public void AddPage(int pageIndex, UITabControl page)
        {
            Helper.AddPage(pageIndex, page);
        }

        public void AddPage(int pageIndex, UITabControlMenu page)
        {
            Helper.AddPage(pageIndex, page);
        }

        public void AddPage(Guid guid, UITabControl page)
        {
            Helper.AddPage(guid, page);
        }

        public void AddPage(Guid guid, UITabControlMenu page)
        {
            Helper.AddPage(guid, page);
        }


        public string Version { get; }

        private Color _fillColor = UIColor.LightBlue;
        private Color tabBackColor = Color.FromArgb(56, 56, 56);

        [DefaultValue(null)]
        public string TagString { get; set; }

        [DefaultValue(false)]
        public bool StyleCustomMode { get; set; }

        private HorizontalAlignment textAlignment = HorizontalAlignment.Center;

        [DefaultValue(HorizontalAlignment.Center)]
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
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
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
        [Description("边框颜色"), Category("自定义")]
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
        [Description("选中Tab页背景色"), Category("自定义")]
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
        [Description("选中Tab页字体色"), Category("自定义")]
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
        [Description("未选中Tab页字体色"), Category("自定义")]
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
        [Description("选中Tab页高亮"), Category("自定义")]
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

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

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

        [DefaultValue(false)]
        public bool ShowCloseButton
        {
            get => showCloseButton;
            set
            {
                showCloseButton = value;
                Invalidate();
            }
        }

        private bool showActiveCloseButton;

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowActiveCloseButton
        {
            get => showActiveCloseButton;
            set
            {
                showActiveCloseButton = value;
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

                g.DrawString(TabPages[index].Text, Font, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, textLeft, TabRect.Top + 2 + (TabRect.Height - sf.Height) / 2.0f);
                if (ShowCloseButton || (ShowActiveCloseButton && index == SelectedIndex))
                {
                    g.DrawFontImage(61453, 20, index == SelectedIndex ? tabSelectedForeColor : TabUnSelectedForeColor, new Rectangle(TabRect.Width - 28, TabRect.Top, 24, TabRect.Height));
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
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

            if (ShowCloseButton || (ShowActiveCloseButton && removeIndex == SelectedIndex))
            {
                if (BeforeRemoveTabPage == null || (BeforeRemoveTabPage != null && BeforeRemoveTabPage.Invoke(this, removeIndex)))
                {
                    RemoveTabPage(removeIndex);
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
            return NativeMethods.FindWindowEx(Handle, IntPtr.Zero, UpDownButtonClassName, null);
        }

        public void OnPaintUpDownButton(UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;

            Color upButtonBaseColor = tabBackColor;
            Color upButtonBorderColor = tabBackColor;
            Color upButtonArrowColor = tabUnSelectedForeColor;

            Color downButtonBaseColor = tabBackColor;
            Color downButtonBorderColor = tabBackColor;
            Color downButtonArrowColor = tabUnSelectedForeColor;

            Rectangle upButtonRect = rect;
            upButtonRect.X += 0;
            upButtonRect.Y += 0;
            upButtonRect.Width = rect.Width / 2 - 1;
            upButtonRect.Height -= 1;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right + 1;
            downButtonRect.Y += 0;
            downButtonRect.Width = rect.Width / 2 - 1;
            downButtonRect.Height -= 1;

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(tabBackColor, 0, -35, -24, -9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(tabBackColor, 0, -35, -24, -9);
                        }
                    }
                    else
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(tabBackColor, 0, 35, 24, 9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(tabBackColor, 0, 35, 24, 9);
                        }
                    }
                }
            }
            else
            {
                upButtonBaseColor = SystemColors.Control;
                upButtonBorderColor = SystemColors.ControlDark;
                upButtonArrowColor = SystemColors.ControlDark;

                downButtonBaseColor = SystemColors.Control;
                downButtonBorderColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color color = Enabled ? BackColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(color))
            {
                rect.Inflate(1, 1);
                g.FillRectangle(brush, rect);
            }

            RenderButton(g, upButtonRect, upButtonBaseColor, upButtonBorderColor, upButtonArrowColor, ArrowDirection.Left);
            RenderButton(g, downButtonRect, downButtonBaseColor, downButtonBorderColor, downButtonArrowColor, ArrowDirection.Right);
            UpDownButtonPaintEventHandler handler = Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            handler?.Invoke(this, e);
        }

        internal void RenderButton(Graphics g, Rectangle rect, Color baseColor, Color borderColor, Color arrowColor, ArrowDirection direction)
        {
            RenderBackgroundInternal(g, rect, baseColor, borderColor, 0.45f, true, LinearGradientMode.Vertical);

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

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }

        internal void RenderBackgroundInternal(Graphics g, Rectangle rect, Color baseColor, Color borderColor, float basePosition, bool drawBorder, LinearGradientMode mode)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectTop = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectTop.Height = (int)(rectTop.Height * basePosition);
                }
                else
                {
                    rectTop.Width = (int)(rect.Width * basePosition);
                }
                using (SolidBrush brushAlpha = new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillRectangle(brushAlpha, rectTop);
                }
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
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

            public UpDownButtonNativeWindow(UITabControl owner)
            {
                _owner = owner;
                AssignHandle(owner.UpDownButtonHandle);
            }

            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_RBUTTON) < 0);
                }
                else
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_LBUTTON) < 0);
                }
            }

            private void DrawUpDownButton()
            {
                bool mousePress = LeftKeyPressed();
                NativeMethods.RECT rect = new NativeMethods.RECT();
                NativeMethods.GetClientRect(Handle, ref rect);
                Rectangle clipRect = Rectangle.FromLTRB(rect.Top, rect.Left, rect.Right, rect.Bottom);
                Point cursorPoint = new Point();
                NativeMethods.GetCursorPos(ref cursorPoint);
                NativeMethods.GetWindowRect(Handle, ref rect);
                var mouseOver = NativeMethods.PtInRect(ref rect, cursorPoint);
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
                    case NativeMethods.WM_PAINT:
                        if (!_bPainting)
                        {
                            NativeMethods.PAINTSTRUCT ps = new NativeMethods.PAINTSTRUCT();
                            _bPainting = true;
                            NativeMethods.BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            NativeMethods.EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = NativeMethods.TRUE;
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

        internal class NativeMethods
        {
            public const int WM_PAINT = 0xF;

            public const int VK_LBUTTON = 0x1;
            public const int VK_RBUTTON = 0x2;

            private const int TCM_FIRST = 0x1300;
            public const int TCM_GETITEMRECT = (TCM_FIRST + 10);

            public static readonly IntPtr TRUE = new IntPtr(1);

            [StructLayout(LayoutKind.Sequential)]
            public struct PAINTSTRUCT
            {
                internal IntPtr hdc;
                internal int fErase;
                internal RECT rcPaint;
                internal int fRestore;
                internal int fIncUpdate;
                internal int Reserved1;
                internal int Reserved2;
                internal int Reserved3;
                internal int Reserved4;
                internal int Reserved5;
                internal int Reserved6;
                internal int Reserved7;
                internal int Reserved8;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                internal RECT(int X, int Y, int Width, int Height)
                {
                    this.Left = X;
                    this.Top = Y;
                    this.Right = Width;
                    this.Bottom = Height;
                }

                internal int Left;
                internal int Top;
                internal int Right;
                internal int Bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr FindWindowEx(
                IntPtr hwndParent,
                IntPtr hwndChildAfter,
                string lpszClass,
                string lpszWindow);

            [DllImport("user32.dll")]
            public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);

            [DllImport("user32.dll")]
            public static extern short GetKeyState(int nVirtKey);

            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref RECT lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetCursorPos(ref Point lpPoint);

            [DllImport("user32.dll")]
            public extern static int OffsetRect(ref RECT lpRect, int x, int y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PtInRect([In] ref RECT lprc, Point pt);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetClientRect(IntPtr hWnd, ref RECT r);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern bool IsWindowVisible(IntPtr hwnd);
        }

        public delegate void UpDownButtonPaintEventHandler(object sender, UpDownButtonPaintEventArgs e);

        public class UpDownButtonPaintEventArgs : PaintEventArgs
        {
            private bool _mouseOver;
            private bool _mousePress;
            private bool _mouseInUpButton;

            public UpDownButtonPaintEventArgs(
                Graphics graphics,
                Rectangle clipRect,
                bool mouseOver,
                bool mousePress,
                bool mouseInUpButton)
                : base(graphics, clipRect)
            {
                _mouseOver = mouseOver;
                _mousePress = mousePress;
                _mouseInUpButton = mouseInUpButton;
            }

            public bool MouseOver
            {
                get { return _mouseOver; }
            }

            public bool MousePress
            {
                get { return _mousePress; }
            }

            public bool MouseInUpButton
            {
                get { return _mouseInUpButton; }
            }
        }
    }
}