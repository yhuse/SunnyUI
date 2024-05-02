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
 * 文件名称: UIForm2.cs
 * 文件说明: 窗体基类
 * 当前版本: V3.6
 * 创建日期: 2024-01-20
 *
 * 2024-01-20: V3.6.3 增加文件说明
 * 2024-01-25: V3.6.3 增加主题等
 * 2024-04-16: V3.6.5 设置默认Padding.Top为TitleHeight
 * 2024-04-28: V3.6.5 增加WindowStateChanged事件
******************************************************************************/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIForm2 : UIBaseForm
    {
        public UIForm2()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            fieldW = typeof(Control).GetField("_clientWidth", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Control).GetField("clientWidth", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldH = typeof(Control).GetField("_clientHeight", BindingFlags.NonPublic | BindingFlags.Instance) ?? typeof(Control).GetField("clientHeight", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [Description("显示边框可拖拽调整窗体大小"), Category("SunnyUI"), DefaultValue(false)]
        public bool ShowDragStretch
        {
            get => showDragStretch;
            set
            {
                showDragStretch = value;
                SetPadding();
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

            if (!ShowTitle)
            {
                return;
            }

            if (ShowIcon && Icon != null)
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
                e.Graphics.DrawString(Text, TitleFont, titleForeColor, new Rectangle(6 + (ShowIcon && Icon != null ? 26 : 0), 0, Width, TitleHeight), ContentAlignment.MiddleLeft);
            }

            e.Graphics.SetHighQuality();

            if (ControlBoxLeft != Width)
            {
                e.Graphics.FillRectangle(TitleColor, new Rectangle(ControlBoxLeft, 1, Width, TitleHeight - 2));
            }

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
                    e.Graphics.DrawFontImage(ExtendSymbol, ExtendSymbolSize, controlBoxForeColor, ExtendBoxRect, ExtendSymbolOffset.X, ExtendSymbolOffset.Y);
                }
            }

            e.Graphics.SetDefaultQuality();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (InControlBox || InMaxBox || InMinBox || InExtendBox) return;
            if (!ShowTitle || e.Y > Padding.Top)
                base.OnMouseDoubleClick(e);
        }

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
                    DoWindowStateChanged(FormWindowState.Minimized);
                    WindowState = FormWindowState.Minimized;
                }

                if (InMaxBox)
                {
                    InMaxBox = false;
                    ShowMaxOrNormal();
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

        public event EventHandler ExtendBoxClick;

        private void ShowMaxOrNormal()
        {
            if (!ShowFullScreen)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    DoWindowStateChanged(FormWindowState.Normal);
                    WindowState = FormWindowState.Normal;
                    if (Location.Y < 0) Location = new Point(Location.X, 0);
                }
                else
                {
                    DoWindowStateChanged(FormWindowState.Maximized);
                    WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                    DoWindowStateChanged(FormWindowState.Normal);
                    WindowState = FormWindowState.Normal;
                    if (Location.Y < 0) Location = new Point(Location.X, 0);
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.None;
                    DoWindowStateChanged(FormWindowState.Maximized);
                    WindowState = FormWindowState.Maximized;
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

            if (e.Clicks == 1)
            {
                Win32.User.ReleaseCapture();
                Win32.User.SendMessage(this.Handle, Win32.User.WM_SYSCOMMAND, Win32.User.SC_MOVE + Win32.User.HTCAPTION, 0);
            }
            else
            {
                ShowMaxOrNormal();
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

        protected override void CalcSystemBoxPos()
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

                if (ControlBoxLeft != Width) ControlBoxLeft -= 6;
            }
            else
            {
                ExtendBoxRect = MaximizeBoxRect = MinimizeBoxRect = ControlBoxRect = new Rectangle(Width + 1, Height + 1, 1, 1);
            }
        }

        private FormWindowState winState = FormWindowState.Normal;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            var msg = (int)m.Msg;
            switch (msg)
            {
                case Win32.User.WM_ACTIVATE:
                    var margins = new Win32.Dwm.MARGINS(0, 0, 1, 0);
                    Win32.Dwm.DwmExtendFrameIntoClientArea(Handle, ref margins);
                    if (WindowState != FormWindowState.Minimized && lastWindowState == FormWindowState.Minimized)
                    {
                        DoWindowStateChanged(WindowState, lastWindowState);
                        lastWindowState = WindowState;
                    }
                    break;
                case Win32.User.WM_NCCALCSIZE when m.WParam != IntPtr.Zero:
                    if (WM_NCCALCSIZE(ref m)) return;
                    break;
                case Win32.User.WM_NCACTIVATE:
                    if (WM_NCACTIVATE(ref m)) return;
                    break;
                case Win32.User.WM_SIZE:
                    WM_SIZE(ref m);
                    break;
                case Win32.User.WM_HOTKEY:
                    int hotKeyId = (int)(m.WParam);
                    if (hotKeys != null && hotKeys.ContainsKey(hotKeyId))
                    {
                        HotKeyEventHandler?.Invoke(this, new HotKeyEventArgs(hotKeys[hotKeyId], DateTime.Now));
                    }
                    break;
                case Win32.User.WM_ACTIVATEAPP:
                    if (WindowState == FormWindowState.Minimized && lastWindowState != FormWindowState.Minimized)
                    {
                        DoWindowStateChanged(WindowState, lastWindowState);
                        lastWindowState = FormWindowState.Minimized;
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

        public event HotKeyEventHandler HotKeyEventHandler;

        public new Point Location
        {
            get => winState == FormWindowState.Normal ? base.Location : FormRect.Location;
            set => base.Location = value;
        }

        public new int Top
        {
            get => Location.Y;
            set => base.Top = value;
        }

        public new int Left
        {
            get => Location.X;
            set => base.Left = value;
        }

        public new int Right => FormRect.Right;

        public new int Bottom => FormRect.Bottom;

        public new Size Size
        {
            get => winState == FormWindowState.Normal ? base.Size : FormRect.Size;
            set => base.Size = value;
        }

        public new int Width
        {
            get => Size.Width;
            set => base.Width = value;
        }

        public new int Height
        {
            get => Size.Height;
            set => base.Height = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private Rectangle FormRect
        {
            get
            {
                if (winState == FormWindowState.Normal) return new Rectangle(base.Location, base.Size);
                var rect = ClientRectangle;
                var point = RectangleToScreen(Rectangle.Empty);
                return new Rectangle(point.Location, rect.Size);
            }
            set
            {
                base.Location = value.Location;
                base.Size = value.Size;
            }
        }

        private void WM_SIZE(ref System.Windows.Forms.Message m)
        {
            if ((int)m.WParam == Win32.User.SIZE_MINIMIZED)
            {
                winState = FormWindowState.Minimized;
            }
            else if ((int)m.WParam == Win32.User.SIZE_MAXIMIZED)
            {
                winState = FormWindowState.Maximized;
                boundsCoreState = true;
            }
            else if ((int)m.WParam == Win32.User.SIZE_RESTORED)
            {
                winState = FormWindowState.Normal;
            }
        }

        private bool WM_NCCALCSIZE(ref System.Windows.Forms.Message m)
        {
            if (FormBorderStyle == FormBorderStyle.None) return false;
#if NET40
            var sizeParams = (Win32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(Win32.NCCALCSIZE_PARAMS));
#else
            var sizeParams = Marshal.PtrToStructure<Win32.NCCALCSIZE_PARAMS>(m.LParam);
#endif
            var borders = GetBorders();

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

        private bool WM_NCACTIVATE(ref System.Windows.Forms.Message m)
        {
            if (m.HWnd == IntPtr.Zero) return false;
            if (IsIconic(m.HWnd)) return false;
            m.Result = DefWindowProc(m.HWnd, (uint)m.Msg, m.WParam, new IntPtr(-1));
            return true;
        }

        [DllImport("user32.dll", SetLastError = false, CharSet = CharSet.Auto)]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = false, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsIconic(IntPtr hWnd);

        private FieldInfo fieldW;
        private FieldInfo fieldH;
        protected override void SetClientSizeCore(int x, int y)
        {
            if (DesignMode && fieldW != null && fieldH != null)
            {
                fieldW.SetValue(this, x);
                fieldH.SetValue(this, y);
                OnClientSizeChanged(EventArgs.Empty);
                Size = SizeFromClientSize(new Size(x, y));
            }
            else base.SetClientSizeCore(x, y);
        }

        private Padding GetBorders()
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

        protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
        {
            var rect = base.GetScaledBounds(bounds, factor, specified);
            if (!GetStyle(ControlStyles.FixedWidth) && (specified & BoundsSpecified.Width) != BoundsSpecified.None)
            {
                var clientWidth = bounds.Width;// - sz.Width;
                rect.Width = (int)Math.Round((double)(clientWidth * factor.Width));// + sz.Width;
            }
            if (!GetStyle(ControlStyles.FixedHeight) && (specified & BoundsSpecified.Height) != BoundsSpecified.None)
            {
                var clientHeight = bounds.Height;// - sz.Height;
                rect.Height = (int)Math.Round((double)(clientHeight * factor.Height));// + sz.Height;
            }
            return rect;
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            return clientSize;
        }

        private bool boundsCoreState = false;

        private Size SetBoundsCore(int width, int height)
        {
            if (WindowState == FormWindowState.Normal)
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
    }
}