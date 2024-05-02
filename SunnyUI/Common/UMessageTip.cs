/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UMessageTip.cs
 * 文件说明: 轻快型消息提示窗
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2023-03-28: V3.3.4 解决了Release模式下GDI位图未释放的Bug
******************************************************************************
 * 文件名称: MessageTip.cs
 * 文件说明: 轻快型消息提示窗
 * 文件作者: AhDung
 * 引用地址: https://www.cnblogs.com/ahdung/p/UIMessageTip.html
 * 当前版本: V2.0.0.2
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    /// <summary>
    /// 轻便消息窗
    /// </summary>
    public static class UIMessageTip
    {
        //默认字体。当样式中的Font==null时用该字体替换
        static readonly Font DefaultFont = UIStyles.Font();

        //文本格式。用于测量和绘制
        static readonly StringFormat DefStringFormat = StringFormat.GenericTypographic;

        /// <summary>
        /// 获取或设置默认消息样式
        /// </summary>
        public static TipStyle DefaultStyle { get; set; }

        /// <summary>
        /// 获取或设置良好消息样式
        /// </summary>
        public static TipStyle OkStyle { get; set; }

        /// <summary>
        /// 获取或设置警告消息样式
        /// </summary>
        public static TipStyle WarningStyle { get; set; }

        /// <summary>
        /// 获取或设置出错消息样式
        /// </summary>
        public static TipStyle ErrorStyle { get; set; }

        /// <summary>
        /// 获取或设置全局淡入淡出时长（毫秒）。默认100
        /// </summary>
        public static int Fade { get; set; }

        /// <summary>
        /// 是否使用漂浮动画。默认true
        /// </summary>
        public static bool Floating { get; set; }

        /// <summary>
        /// 全局消息停留时长（毫秒）。默认600
        /// </summary>
        public static int Delay { get; set; }

        static UIMessageTip()
        {
            Fade = 100;
            Floating = true;
            Delay = 600;
            DefaultStyle = TipStyle.Gray;
            OkStyle = TipStyle.Green;
            WarningStyle = TipStyle.Orange;
            ErrorStyle = TipStyle.Red;
        }

        /// <summary>
        /// 在指定控件附近显示良好消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。为负时使用全局时长</param>
        /// <param name="floating">是否漂浮，不指定则使用全局设置</param>
        /// <param name="centerInControl">是否在控件中央显示，不指定则自动判断</param>
        public static void ShowOk(Component controlOrItem, string text = null, int delay = -1, bool? floating = null, bool? centerInControl = null) =>
            Show(controlOrItem, text, OkStyle ?? TipStyle.Green, delay, floating, centerInControl);

        /// <summary>
        /// 显示良好消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。为负时使用全局时长</param>
        /// <param name="floating">是否漂浮，不指定则使用全局设置</param>
        /// <param name="point">消息窗显示位置。不指定则智能判定，当由工具栏项(ToolStripItem)弹出时，请指定该参数或使用接收控件的重载</param>
        /// <param name="centerByPoint">是否以point参数为中心进行呈现。为false则是在其附近呈现</param>
        public static void ShowOk(string text = null, int delay = -1, bool? floating = null, Point? point = null, bool centerByPoint = false) =>
            Show(text, OkStyle ?? TipStyle.Green, delay, floating, point, centerByPoint);

        /// <summary>
        /// 在指定控件附近显示警告消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒，若要使用全局时长请设为-1</param>
        /// <param name="floating">是否漂浮。默认不漂浮。若要使用全局设置请设为null</param>
        /// <param name="centerInControl">是否在控件中央显示，不指定则自动判断</param>
        public static void ShowWarning(Component controlOrItem, string text = null, int delay = 1000, bool? floating = false, bool? centerInControl = null) =>
            Show(controlOrItem, text, WarningStyle ?? TipStyle.Orange, delay, floating, centerInControl);

        /// <summary>
        /// 显示警告消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒，若要使用全局时长请设为-1</param>
        /// <param name="floating">是否漂浮。默认不漂浮。若要使用全局设置请设为null</param>
        /// <param name="point">消息窗显示位置。不指定则智能判定，当由工具栏项(ToolStripItem)弹出时，请指定该参数或使用接收控件的重载</param>
        /// <param name="centerByPoint">是否以point参数为中心进行呈现。为false则是在其附近呈现</param>
        public static void ShowWarning(string text = null, int delay = 1000, bool? floating = false, Point? point = null, bool centerByPoint = false) =>
            Show(text, WarningStyle ?? TipStyle.Orange, delay, floating, point, centerByPoint);

        /// <summary>
        /// 在指定控件附近显示出错消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒，若要使用全局时长请设为-1</param>
        /// <param name="floating">是否漂浮。默认不漂浮。若要使用全局设置请设为null</param>
        /// <param name="centerInControl">是否在控件中央显示，不指定则自动判断</param>
        public static void ShowError(Component controlOrItem, string text = null, int delay = 1000, bool? floating = false, bool? centerInControl = null) =>
            Show(controlOrItem, text, ErrorStyle ?? TipStyle.Red, delay, floating, centerInControl);

        /// <summary>
        /// 显示出错消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="delay">消息停留时长(ms)。默认1秒，若要使用全局时长请设为-1</param>
        /// <param name="floating">是否漂浮。默认不漂浮。若要使用全局设置请设为null</param>
        /// <param name="point">消息窗显示位置。不指定则智能判定，当由工具栏项(ToolStripItem)弹出时，请指定该参数或使用接收控件的重载</param>
        /// <param name="centerByPoint">是否以point参数为中心进行呈现。为false则是在其附近呈现</param>
        public static void ShowError(string text = null, int delay = 1000, bool? floating = false, Point? point = null, bool centerByPoint = false) =>
            Show(text, ErrorStyle ?? TipStyle.Red, delay, floating, point, centerByPoint);

        /// <summary>
        /// 在指定控件附近显示消息
        /// </summary>
        /// <param name="controlOrItem">控件或工具栏项</param>
        /// <param name="text">消息文本</param>
        /// <param name="style">消息样式。不指定则使用默认样式</param>
        /// <param name="delay">消息停留时长(ms)。为负时使用全局时长</param>
        /// <param name="floating">是否漂浮，不指定则使用全局设置</param>
        /// <param name="centerInControl">是否在控件中央显示，不指定则自动判断</param>
        public static void Show(Component controlOrItem, string text, TipStyle style = null, int delay = -1, bool? floating = null, bool? centerInControl = null)
        {
            if (controlOrItem == null)
            {
                throw new ArgumentNullException(nameof(controlOrItem));
            }

            Show(text, style, delay, floating, GetCenterPosition(controlOrItem), centerInControl ?? IsContainerLike(controlOrItem));
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="text">消息文本</param>
        /// <param name="style">消息样式。不指定则使用默认样式</param>
        /// <param name="delay">消息停留时长(ms)。为负时使用全局时长</param>
        /// <param name="floating">是否漂浮，不指定则使用全局设置</param>
        /// <param name="point">消息窗显示位置。不指定则智能判定，当由工具栏项(ToolStripItem)弹出时，请指定该参数或使用接收控件的重载</param>
        /// <param name="centerByPoint">是否以point参数为中心进行呈现。为false则是在其附近呈现</param>
        public static void Show(string text, TipStyle style = null, int delay = -1, bool? floating = null, Point? point = null, bool centerByPoint = false)
        {
            var basePoint = point ?? DetemineActive();

            new Thread(arg =>
            {
                LayeredWindow layer = null;
                try
                {
                    layer = new LayeredWindow(CreateTipImage(text ?? string.Empty, style ?? DefaultStyle ?? TipStyle.Gray, out Rectangle contentBounds))
                    {
                        Alpha = 0,
                        Location = GetLocation(contentBounds, basePoint, centerByPoint, out var floatDown),
                        MouseThrough = true,
                        TopMost = true,
                        Tag = new object[] { delay < 0 ? Delay : delay, floating ?? Floating, floatDown }
                    };
                    layer.Showing += layer_Showing;
                    layer.Closing += layer_Closing;
                    layer.Show();
                }
                finally
                {
                    if (layer != null)
                    {
                        layer.Showing -= layer_Showing;
                        layer.Closing -= layer_Closing;
                        layer.Dispose();
                    }
                }
            })
            {
                IsBackground = true,
                Name = "T_Showing"
            }.Start();
        }

        static void layer_Showing(object sender, EventArgs e)
        {
            var layer = (LayeredWindow)sender;
            var args = layer.Tag as object[];
            if (args == null || args.Length <= 2) return;
            var delay = (int)args[0];
            var floating = (bool)args[1];
            var floatDown = (bool)args[2];

            if (floating)
            {
                //另起线程浮动窗体
                new Thread(arg =>
                {
                    int adj = floatDown ? 1 : -1;

                    while (layer.Visible) //layer.IsDisposed有lock，不适合此循环
                    {
                        layer.Top += adj;
                        Thread.Sleep(30);
                    }
                })
                { IsBackground = true, Name = "T_Floating" }.Start();
            }

            FadeEffect(layer, true);
            Thread.Sleep(delay < 0 ? 0 : delay);
            layer.Close();
        }

        static void layer_Closing(object sender, CancelEventArgs e) =>
            FadeEffect((LayeredWindow)sender, false);

        /// <summary>
        /// 淡入淡出处理
        /// </summary>
        private static void FadeEffect(LayeredWindow window, bool fadeIn)
        {
            byte target = fadeIn ? byte.MaxValue : byte.MinValue;
            const int Updateinterval = 10;//动画更新间隔（毫秒）
            int step = Fade < Updateinterval ? 0 : (Fade / Updateinterval);

            for (int i = 1; i <= step; i++)
            {
                Thread.Sleep(Updateinterval);

                if (i == step) { break; }

                var tmp = (double)(fadeIn ? i : (step - i));
                window.Alpha = (byte)(tmp / step * 255);
            }

            window.Alpha = target;
        }

        /// <summary>
        /// 判定活动点
        /// </summary>
        private static Point DetemineActive()
        {
            var point = Control.MousePosition;

            var focusControl = Control.FromHandle(GetFocus());
            if (focusControl is TextBoxBase)//若焦点是文本框，取光标位置
            {
                var pt = GetCaretPosition();
                pt.Y += focusControl.Font.Height / 2;
                point = focusControl.PointToScreen(pt);
            }
            else if (focusControl is ButtonBase)//若焦点是按钮，取按钮中心点
            {
                point = GetCenterPosition(focusControl);
            }
            return point;
        }

        /// <summary>
        /// 创建消息窗图像，同时输出内容区，用于外部定位
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)] //避免争用style中的图标画笔等资源，否则在同时调用时容易引发资源占用异常
        private static Bitmap CreateTipImage(string text, TipStyle style, out Rectangle contentBounds)
        {
            var size = Size.Empty;
            var iconBounds = Rectangle.Empty;
            var textBounds = Rectangle.Empty;
            Font font = style.TextFont ?? DefaultFont;
            using Font tmpFont = font.DPIScaleFont(font.Size);

            if (style.Icon != null)
            {
                size = style.Icon.Size;
                iconBounds.Size = size;
                textBounds.X = size.Width;
            }

            if (text.Length != 0)
            {
                if (style.Icon != null)
                {
                    size.Width += style.IconSpacing;
                    textBounds.X += style.IconSpacing;
                }

                DefStringFormat.Alignment = StringAlignment.Near;
                DefStringFormat.LineAlignment = StringAlignment.Near;
                textBounds.Size = Size.Truncate(GraphicsUtils.MeasureString(text, tmpFont, 0, DefStringFormat));
                size.Width += textBounds.Width;

                if (size.Height < textBounds.Height)
                {
                    size.Height = textBounds.Height;
                }
                else if (size.Height > textBounds.Height)//若文字没有图标高，令文字与图标垂直居中，否则与图标平齐
                {
                    textBounds.Y += (size.Height - textBounds.Height) / 2;
                }
                textBounds.Offset(style.TextOffset);
            }

            size += style.Padding.Size;
            iconBounds.Offset(style.Padding.Left, style.Padding.Top);
            textBounds.Offset(style.Padding.Left, style.Padding.Top);

            contentBounds = new Rectangle(Point.Empty, size);
            var fullBounds = GraphicsUtils.GetBounds(contentBounds, style.Border, style.ShadowRadius, style.ShadowOffset.X, style.ShadowOffset.Y);
            contentBounds.Offset(-fullBounds.X, -fullBounds.Y);
            iconBounds.Offset(-fullBounds.X, -fullBounds.Y);
            textBounds.Offset(-fullBounds.X, -fullBounds.Y);

            var bmp = new Bitmap(fullBounds.Width, fullBounds.Height);

            Graphics g = null;
            Brush backBrush = null;
            Brush textBrush = null;
            try
            {
                g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                backBrush = (style.BackBrush ?? (r => new SolidBrush(style.BackColor)))(contentBounds);
                GraphicsUtils.DrawRectangle(g, contentBounds,
                    backBrush,
                    style.Border,
                    style.CornerRadius,
                    style.ShadowColor,
                    style.ShadowRadius,
                    style.ShadowOffset.X,
                    style.ShadowOffset.Y);

                if (style.Icon != null)
                {
                    //DEBUG: g.DrawRectangle(new Border(Color.Red) { Width = 1, Direction = Direction.Inner }.Pen, iconBounds);
                    g.DrawImageUnscaled(style.Icon, iconBounds.Location);
                }
                if (text.Length != 0)
                {
                    textBrush = new SolidBrush(style.TextColor);
                    //DEBUG: g.DrawRectangle(new Border(Color.Red){ Width=1, Direction= Direction.Inner}.Pen, textBounds);
                    DefStringFormat.Alignment = StringAlignment.Near;
                    DefStringFormat.LineAlignment = StringAlignment.Near;
                    g.DrawString(text, tmpFont, textBrush, textBounds.Location, DefStringFormat);
                }

                g.Flush(FlushIntention.Sync);
                return bmp;
            }
            finally
            {
                g?.Dispose();
                backBrush?.Dispose();
                textBrush?.Dispose();
            }
        }

        /// <summary>
        /// 根据基准点处理窗体显示位置
        /// </summary>
        /// <param name="contentBounds">内容区。依据该区域处理定位，而不是根据整个消息窗图像，因为阴影也许偏移很大</param>
        /// <param name="basePoint">定位参考点</param>
        /// <param name="centerByBasePoint">是否以参考点为中心呈现。false则是在参考点附近呈现</param>
        /// <param name="floatDown">指示是否应当向下浮动（当太靠近屏幕顶部时）。默认是向上</param>
        private static Point GetLocation(Rectangle contentBounds, Point basePoint, bool centerByBasePoint, out bool floatDown)
        {
            //以基准点所在屏为界
            var screen = Screen.FromPoint(basePoint).Bounds;

            var p = basePoint;
            p.X -= contentBounds.Width / 2;

            //横向处理。距离屏幕左右两边太近时的处理
            //多屏下left可能为负，所以right = width - (-left) = width + left
            int spacing = 10; //至少距离边缘多少像素
            int left, right;
            if (p.X < (left = screen.Left + spacing))
            {
                p.X = left;
            }
            else if (p.X > (right = screen.Width + screen.Left - spacing - contentBounds.Width))
            {
                p.X = right;
            }

            //纵向处理
            if (centerByBasePoint)
            {
                p.Y -= contentBounds.Height / 2;
            }
            else
            {
                spacing = 20;//错开基准点上下20像素
                p.Y -= contentBounds.Height + spacing;
            }

            floatDown = false;
            if (p.Y < screen.Top + 50)//若太靠屏幕顶部
            {
                if (!centerByBasePoint)
                {
                    p.Y += contentBounds.Height + 2 * spacing;//在下方错开
                }

                floatDown = true;//动画改为下降
            }

            p.Offset(-contentBounds.X, -contentBounds.Y);
            return p;
        }

        /// <summary>
        /// 获取控件中心点的屏幕坐标
        /// </summary>
        private static Point GetCenterPosition(Component controlOrItem)
        {
            if (controlOrItem is Control c)
            {
                var size = c.ClientSize;
                return c.PointToScreen(new Point(size.Width / 2, size.Height / 2));
            }

            if (controlOrItem is ToolStripItem item)
            {
                var pos = item.Bounds.Location;
                pos.X += item.Width / 2;
                pos.Y += item.Height / 2;
                return item.Owner.PointToScreen(pos);
            }

            throw new ArgumentException("参数只能是Control或ToolStripItem！");
        }

        /// <summary>
        /// 判断控件看起来是否像容器（占一定面积那种）
        /// </summary>
        private static bool IsContainerLike(Component controlOrItem)
        {
            if (controlOrItem is ContainerControl
                || controlOrItem is GroupBox
                || controlOrItem is Panel
                || controlOrItem is TabControl
                || controlOrItem is DataGridView
                || controlOrItem is ListBox
                || controlOrItem is ListView)
            {
                return true;
            }

            if (controlOrItem is TextBox txb && txb.Multiline)
            {
                return true;
            }

            if (controlOrItem is RichTextBox rtb && rtb.Multiline)
            {
                return true;
            }

            return false;
        }

        #region Win32 API

        /// <summary>
        /// 获取输入光标位置，文本框内坐标
        /// </summary>
        private static Point GetCaretPosition()
        {
            GetCaretPos(out Point pt);
            return pt;
        }

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool GetCaretPos(out Point pt);

        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();

        #endregion
    }

    /// <summary>
    /// 消息窗样式
    /// </summary>
    public sealed class TipStyle : IDisposable
    {
        bool _isPresets;
        bool _keepFont;
        bool _keepIcon;

        readonly Border _border;
        /// <summary>
        /// 获取边框信息。内部用
        /// </summary>
        internal Border Border => _border;

        /// <summary>
        /// 获取或设置图标。默认null
        /// </summary>
        public Bitmap Icon { get; set; }

        /// <summary>
        /// 获取或设置图标与文本的间距。默认为3像素
        /// </summary>
        public int IconSpacing { get; set; }

        /// <summary>
        /// 获取或设置文本字体。默认12号的消息框文本
        /// </summary>
        public Font TextFont { get; set; }

        /// <summary>
        /// 获取或设置文本偏移，用于微调
        /// </summary>
        public Point TextOffset { get; set; }

        /// <summary>
        /// 获取或设置文本颜色（默认黑色）
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// 获取或设置背景颜色（默认浅白）
        /// <para>- 若想呈现多色及复杂背景，请使用BackBrush属性，当后者不为null时，本属性被忽略</para>
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 获取或设置背景画刷生成方法
        /// <para>- 是个委托，入参矩形由绘制函数传入，表示内容区，便于构造画刷</para>
        /// <para>- 默认null，为null时使用BackColor绘制单色背景</para>
        /// <para>- 方法返回的画刷需释放</para>
        /// </summary>
        public BrushSelector<Rectangle> BackBrush { get; set; }

        /// <summary>
        /// 获取或设置边框颜色（默认深灰）
        /// </summary>
        public Color BorderColor
        {
            get => _border.Color;
            set => _border.Color = value;
        }

        /// <summary>
        /// 获取或设置边框粗细（默认1）
        /// </summary>
        public int BorderWidth
        {
            get => _border.Width / 2;
            set => _border.Width = value * 2;
        }

        /// <summary>
        /// 获取或设置圆角半径（默认3）
        /// </summary>
        public int CornerRadius { get; set; }

        /// <summary>
        /// 获取或设置阴影颜色（默认深灰）
        /// </summary>
        public Color ShadowColor { get; set; }

        /// <summary>
        /// 获取或设置阴影羽化半径（默认4）
        /// </summary>
        public int ShadowRadius { get; set; }

        /// <summary>
        /// 获取或设置阴影偏移（默认x=0,y=3）
        /// </summary>
        public Point ShadowOffset { get; set; }

        /// <summary>
        /// 获取或设置四周空白（默认left,right=10; top,bottom=5）
        /// </summary>
        public Padding Padding { get; set; }

        /// <summary>
        /// 初始化样式
        /// </summary>
        public TipStyle()
        {
            _border = new Border(PresetsResources.Colors[0, 0])
            {
                Behind = true,
                Width = 2
            };
            IconSpacing = 5;
            TextFont = UIStyles.Font();
            var fontName = TextFont.Name;
            if (fontName == "宋体") { TextOffset = new Point(1, 1); }
            TextColor = Color.Black;
            BackColor = Color.FromArgb(252, 252, 252);
            CornerRadius = 3;
            ShadowColor = PresetsResources.Colors[0, 2];
            ShadowRadius = 4;
            ShadowOffset = new Point(0, 3);
            Padding = new Padding(10, 5, 10, 5);
        }

        /// <summary>
        /// 清理本类使用的资源
        /// </summary>
        /// <param name="keepFont">是否保留字体</param>
        /// <param name="keepIcon">是否保留图标</param>
        public void Clear(bool keepFont = false, bool keepIcon = false)
        {
            _keepFont = keepFont;
            _keepIcon = keepIcon;
            ((IDisposable)this).Dispose();
        }

        static TipStyle _gray;
        /// <summary>
        /// 预置的灰色样式
        /// </summary>
        public static TipStyle Gray => _gray ?? (_gray = CreatePresetsStyle(0));

        static TipStyle _green;
        /// <summary>
        /// 预置的绿色样式
        /// </summary>
        public static TipStyle Green => _green ?? (_green = CreatePresetsStyle(1));

        static TipStyle _orange;
        /// <summary>
        /// 预置的橙色样式
        /// </summary>
        public static TipStyle Orange => _orange ?? (_orange = CreatePresetsStyle(2));

        static TipStyle _red;
        /// <summary>
        /// 预置的红色样式
        /// </summary>
        public static TipStyle Red => _red ?? (_red = CreatePresetsStyle(3));

        private static TipStyle CreatePresetsStyle(int index)
        {
            var style = new TipStyle
            {
                Icon = PresetsResources.Icons[index],
                BorderColor = PresetsResources.Colors[index, 0],
                ShadowColor = PresetsResources.Colors[index, 2],
                _isPresets = true
            };
            style.BackBrush = r =>
            {
                var brush = new LinearGradientBrush(r,
                    PresetsResources.Colors[index, 1],
                    Color.White,
                    LinearGradientMode.Horizontal);
                brush.SetBlendTriangularShape(0.5f);
                return brush;
            };
            return style;
        }

        bool _disposed;
        [Obsolete("请改用Clear指定是否清理字体和图标")]
        [MethodImpl(MethodImplOptions.Synchronized)]
        void IDisposable.Dispose()
        {
            if (_disposed || _isPresets)//不销毁预置对象
            {
                return;
            }

            _border.Dispose();
            BackBrush = null;
            if (!_keepFont && TextFont != null && !TextFont.IsSystemFont)
            {
                TextFont.Dispose();
                TextFont = null;
            }
            if (!_keepIcon && Icon != null)
            {
                Icon.Dispose();
                Icon = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// 预置资源
        /// </summary>
        private static class PresetsResources
        {
            public static readonly Color[,] Colors =
            {
                    //边框色、背景色、阴影色
             /*灰*/ {UIColor.Gray, Color.FromArgb(245, 245,245), Color.FromArgb(110,  0,  0,  0)},
             /*绿*/ {UIColor.Green, UIColor.LightGreen, Color.FromArgb(150,  0,150,  0)},
             /*橙*/ {UIColor.Orange, UIColor.LightOrange, Color.FromArgb(150,250,100,  0)},
             /*红*/ {UIColor.Red, UIColor.LightRed, Color.FromArgb(140,255, 30, 30)}
            };

            //CreateIcon依赖Colors，所以需在Colors后初始化
            public static readonly Bitmap[] Icons =
            {
                CreateIcon(0),
                CreateIcon(1),
                CreateIcon(2),
                CreateIcon(3)
            };

            /// <summary>
            /// 创建图标
            /// </summary>
            /// <param name="index">0=i；1=√；2=！；3=×；其余=null</param>
            private static Bitmap CreateIcon(int index)
            {
                if (index < 0 || index > 3)
                {
                    return null;
                }
                Graphics g = null;
                Pen pen = null;
                Brush brush = null;
                Bitmap bmp = null;
                try
                {
                    bmp = new Bitmap(24, 24);
                    g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    var color = Colors[index, 0];
                    if (index == 0) //i
                    {
                        brush = new SolidBrush(Color.FromArgb(103, 148, 186));
                        g.FillEllipse(brush, 3, 3, 18, 18);

                        pen = new Pen(Colors[index, 1], 2);
                        g.DrawLine(pen, new Point(12, 6), new Point(12, 8));
                        g.DrawLine(pen, new Point(12, 10), new Point(12, 18));
                    }
                    else if (index == 1) //√
                    {
                        pen = new Pen(color, 4);
                        g.DrawLines(pen, new[] { new Point(3, 11), new Point(10, 18), new Point(20, 5) });
                    }
                    else if (index == 2) //！
                    {
                        var points = new[] { new Point(12, 3), new Point(3, 20), new Point(21, 20) };
                        pen = new Pen(color, 2) { LineJoin = LineJoin.Bevel };
                        g.DrawPolygon(pen, points);

                        brush = new SolidBrush(color);
                        g.FillPolygon(brush, points);

                        pen.Color = Colors[index, 1];
                        g.DrawLine(pen, new Point(12, 8), new Point(12, 15));
                        g.DrawLine(pen, new Point(12, 17), new Point(12, 19));
                    }
                    else //×
                    {
                        pen = new Pen(color, 4);
                        g.DrawLine(pen, 5, 5, 19, 19);
                        g.DrawLine(pen, 5, 19, 19, 5);
                    }
                    return bmp;
                }
                catch
                {
                    bmp?.Dispose();
                    throw;
                }
                finally
                {
                    pen?.Dispose();
                    brush?.Dispose();
                    g?.Dispose();
                }
            }
        }
    }

    //干脆自建一个委托，不依赖Func了
    /// <summary>
    /// 画刷选择器委托
    /// </summary>
    public delegate Brush BrushSelector<in T>(T arg);

    /// <summary>
    /// 描边位置
    /// </summary>
    internal enum Direction
    {
        /// <summary>
        /// 居中
        /// </summary>
        Middle,
        /// <summary>
        /// 内部
        /// </summary>
        Inner,
        /// <summary>
        /// 外部
        /// </summary>
        Outer
    }

    /// <summary>
    /// 存储一系列边框要素并产生合适的画笔
    /// <para>- 边框居中+奇数粗度时是介于两像素之间画，所以粗细在视觉上不精确，建议错开任一条件</para>
    /// </summary>
    internal class Border : IDisposable
    {
        //编写本类除了整合边框信息外，更重要的原因是如果不对画笔做额外处理，
        //Draw出来的边框是不理想的。本类的原理是：
        // - 偶数边框（这是得到理想效果的前提）
        // - 再利用画笔的CompoundArray属性将边框裁切掉一半，
        //   同时根据不同参数偏移描边位置，达到可内可外可居中的效果

        float[] _compoundArray;
        /// <summary>
        /// 根据_direction处理线段
        /// </summary>
        float[] CompoundArray
        {
            get
            {
                if (_compoundArray == null)
                {
                    _compoundArray = new float[2];
                }
                switch (_direction)
                {
                    case Direction.Middle: goto default;
                    case Direction.Inner: _compoundArray[0] = 0.5f; _compoundArray[1] = 1f; break;
                    case Direction.Outer: _compoundArray[0] = 0f; _compoundArray[1] = 0.5f; break;
                    default: _compoundArray[0] = 0.25f; _compoundArray[1] = 0.75f; break;
                }
                return _compoundArray;
            }
        }

        readonly Pen _pen;

        /// <summary>
        /// 获取用于画本边框的画笔。建议销毁本类而不是该画笔
        /// </summary>
        public Pen Pen => _pen;

        /// <summary>
        /// 边框宽度。默认1
        /// </summary>
        public int Width
        {
            get => (int)Pen.Width / 2;
            set => Pen.Width = value * 2;
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color Color
        {
            get => Pen.Color;
            set => Pen.Color = value;
        }

        Direction _direction;
        /// <summary>
        /// 边框位置。默认居中
        /// </summary>
        public Direction Direction
        {
            get => _direction;
            set
            {
                if (_direction == value) { return; }
                _direction = value;
                Pen.CompoundArray = CompoundArray;
            }
        }

        /// <summary>
        /// 描边是否躲在填充之后。默认false
        /// <para>- 如果躲，则处于内部的部分会被填充遮挡，反之则是填充被这部分边框遮挡</para>
        /// <para>- 此属性仅供外部在绘制时确定描边和填充的次序</para>
        /// </summary>
        public bool Behind { get; set; }

        /// <summary>
        /// 获取指定矩形加上本边框后的边界
        /// </summary>
        public Rectangle GetBounds(Rectangle rectangle)
        {
            if (!IsValid() || _direction == Direction.Inner)
            {
                return rectangle;
            }
            var inflate = _direction == Direction.Middle
                ? (int)Math.Ceiling(Width / 2d)
                : Width;
            rectangle.Inflate(inflate, inflate);
            return rectangle;
        }

        public Border(Color color) : this(new Pen(color), false) { }

        /// <summary>
        /// 基于现有画笔的副本构造
        /// </summary>
        public Border(Pen pen) : this(pen, true) { }

        protected Border(Pen pen, bool useCopy)
        {
            _pen = useCopy ? (Pen)pen.Clone() : pen;
            _pen.Alignment = PenAlignment.Center;
            _pen.Width = Convert.ToInt32(_pen.Width * 2);
            _pen.CompoundArray = CompoundArray;
        }

        /// <summary>
        /// 是否有效边框。无宽度或完全透明视为无效
        /// </summary>
        public bool IsValid() => Width > 0 && (Pen.PenType != PenType.SolidColor || Color.A > 0);

        /// <summary>
        /// 是否有效边框。无宽度或完全透明视为无效
        /// </summary>
        public static bool IsValid(Border border) => border != null && border.IsValid();

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose() => Pen?.Dispose();
    }

    /// <summary>
    /// 简易层窗体
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public class LayeredWindow : IDisposable
    {
        const string ClassName = "AhDungLayeredWindow";

        static WndProcDelegate _wndProc;

        readonly object syncObj = new object();
        readonly PointOrSize _size;
        IntPtr _dcMemory;
        IntPtr _hBmp;
        IntPtr _oldObj;

        IntPtr _activeWindow; //用于模式显示时，记录并disable原窗体，然后在本类关闭后enable它
        bool _continueLoop = true;
        short _wndClass;
        IntPtr _hWnd;
        BLENDFUNCTION _blend = new BLENDFUNCTION
        {
            BlendOp = 0,               //AC_SRC_OVER
            BlendFlags = 0,
            SourceConstantAlpha = 255, //透明度
            AlphaFormat = 1            //AC_SRC_ALPHA
        };

        /// <summary>
        /// 窗体显示时
        /// </summary>
        public event EventHandler Showing;

        /// <summary>
        /// 窗体关闭时
        /// </summary>
        public event CancelEventHandler Closing;

        static IntPtr _hInstance;
        static IntPtr HInstance
        {
            get
            {
                if (_hInstance == IntPtr.Zero)
                {
                    Assembly assembly = Assembly.GetEntryAssembly();
                    if (assembly != null)
                    {
                        _hInstance = Marshal.GetHINSTANCE(assembly.ManifestModule);
                    }
                }

                return _hInstance;
            }
        }

        /// <summary>
        /// 获取窗体位置。内部用
        /// </summary>
        PointOrSize LocationInternal => new PointOrSize(_left, _top);

        /// <summary>
        /// 窗体句柄
        /// </summary>
        public IntPtr Handle => _hWnd;

        /// <summary>
        /// 指示窗体是否已显示
        /// </summary>
        public bool Visible { get; private set; }

        int _left;
        /// <summary>
        /// 获取或设置左边缘坐标
        /// </summary>
        public int Left
        {
            get => _left;
            set
            {
                if (_left == value) { return; }
                _left = value;
                Update();
            }
        }

        int _top;
        /// <summary>
        /// 获取或设置上边缘坐标
        /// </summary>
        public int Top
        {
            get => _top;
            set
            {
                if (_top == value) { return; }
                _top = value;
                Update();
            }
        }

        /// <summary>
        /// 获取或设置定位
        /// </summary>
        public Point Location
        {
            get => new Point(_left, _top);
            set
            {
                if (_left == value.X && _top == value.Y) { return; }
                _left = value.X;
                _top = value.Y;
                Update();
            }
        }

        /// <summary>
        /// 获取窗体宽度
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 获取窗体高度
        /// </summary>
        public int Height { get; private set; }

        float _opacity;
        /// <summary>
        /// 获取或设置窗体透明度（建议优先用Alpha）。0=完全透明；1=不透明
        /// </summary>
        public float Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity.Equals(value)) { return; }
                if (value < 0) { _opacity = 0; }
                else if (value > 1) { _opacity = 1; }
                else { _opacity = value; }
                _blend.SourceConstantAlpha = (byte)(_opacity * 255);
                Update();
            }
        }

        /// <summary>
        /// 获取或设置窗体透明度。0=完全透明；255=不透明
        /// </summary>
        public byte Alpha
        {
            get => _blend.SourceConstantAlpha;
            set
            {
                if (_blend.SourceConstantAlpha == value) { return; }
                _blend.SourceConstantAlpha = value;
                Update();
            }
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指示窗体是否以模式状态打开
        /// </summary>
        public bool IsModal { get; private set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool TopMost { get; set; }

        /// <summary>
        /// 是否在显示后激活本窗体。模式显示时强制为true
        /// </summary>
        public bool Activation { get; set; }

        /// <summary>
        /// 是否在任务栏显示
        /// </summary>
        public bool ShowInTaskbar { get; set; }

        /// <summary>
        /// 是否让鼠标事件穿透本窗体
        /// </summary>
        public bool MouseThrough { get; set; }

        /// <summary>
        /// 指示窗体是否已释放
        /// </summary>
        public bool IsDisposed
        {
            get { lock (syncObj) { return _disposed; } }
        }

        /// <summary>
        /// 获取或设置自定对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 创建层窗体
        /// </summary>
        /// <param name="bmp">窗体图像</param>
        /// <param name="keepBmp">是否保留图像，为false会销毁图像</param>
        public LayeredWindow(Bitmap bmp, bool keepBmp = false)
        {
            try
            {
                RegisterWindowClass();

                //初始化绘制相关
                _dcMemory = CreateCompatibleDC(IntPtr.Zero);
                _hBmp = bmp.GetHbitmap(Color.Empty);
                _oldObj = SelectObject(_dcMemory, _hBmp);

                Width = bmp.Width;
                Height = bmp.Height;
                _size = new PointOrSize(Width, Height);
            }
            finally
            {
                if (!keepBmp)
                {
                    bmp.Dispose();
                }
            }
        }

        /// <summary>
        /// 注册私有窗口类
        /// </summary>
        private void RegisterWindowClass()
        {
            if (_wndProc == null)
            {
                _wndProc = WndProc;
            }

            var wc = new WNDCLASS
            {
                hInstance = HInstance,
                lpszClassName = ClassName,
                lpfnWndProc = _wndProc,
                hCursor = LoadCursor(IntPtr.Zero, 32512/*IDC_ARROW*/),
            };

            _wndClass = RegisterClass(wc);

            if (_wndClass == 0)
            {
                //ERROR_CLASS_ALREADY_EXISTS
                if (Marshal.GetLastWin32Error() != 0x582)
                {
                    throw new Win32Exception();
                }
            }
        }

        /// <summary>
        /// 创建窗口
        /// </summary>
        private void CreateWindow()
        {
            int exStyle = 0x80000;                      //WS_EX_LAYERED
            if (TopMost) { exStyle |= 0x8; }            //WS_EX_TOPMOST
            if (!Activation) { exStyle |= 0x08000000; } //WS_EX_NOACTIVATE
            if (MouseThrough) { exStyle |= 0x20; }      //WS_EX_TRANSPARENT
            if (ShowInTaskbar) { exStyle |= 0x40000; }  //WS_EX_APPWINDOW

            int style = unchecked((int)0x80000000)      //WS_POPUP。不能加WS_VISIBLE，会抢焦点，改用ShowWindow显示
                | 0x80000;                              //WS_SYSMENU

            _hWnd = CreateWindowEx(exStyle, ClassName, null, style,
                0, 0, 0, 0, //坐标尺寸全由UpdateLayeredWindow接管，这里无所谓
                IntPtr.Zero, IntPtr.Zero, HInstance, IntPtr.Zero);

            if (_hWnd == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }

        private int DoMessageLoop()
        {
            var m = new MSG();
            int result;

            while (_continueLoop && (result = GetMessage(ref m, IntPtr.Zero, 0, 0)) != 0)
            {
                if (result == -1)
                {
                    return Marshal.GetLastWin32Error();
                }
                TranslateMessage(ref m);
                DispatchMessage(ref m);
            }
            return 0;
        }

        protected virtual IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            //Debug.WriteLine((hWnd == _hWnd) + "：0x" + Convert.ToString(msg, 16), "WndProc");

            switch (msg)
            {
                case 0x10://WM_CLOSE
                    Close();
                    break;
                case 0x2://WM_DESTROY
                    EnableWindow(_activeWindow, true);
                    _continueLoop = false;
                    break;
            }
            return DefWndProc(hWnd, msg, wParam, lParam);
        }

        protected virtual IntPtr DefWndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam) =>
            DefWindowProc(hWnd, msg, wParam, lParam);

        private void ShowCore(bool modal)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(Name ?? string.Empty);
            }
            lock (syncObj)
            {
                if (Visible) { return; }
                if (modal)
                {
                    IsModal = true;
                    Activation = true;
                    _activeWindow = GetActiveWindow();
                    EnableWindow(_activeWindow, false);
                }
                CreateWindow();
                ShowWindow(_hWnd, Activation ? 5/*SW_SHOW*/: 8/*SW_SHOWNA*/);
                Visible = true;
            }
            OnShowing(EventArgs.Empty);
            if (IsDisposed) { return; }//Showing事件中也许会关闭窗体
            Update();
            if (modal)
            {
                var result = DoMessageLoop();
                SetActiveWindow(_activeWindow);
                if (result != 0)
                {
                    throw new Win32Exception(result);
                }
            }
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        public void Show() => ShowCore(false);

        /// <summary>
        /// 显示模式窗体
        /// </summary>
        public void ShowDialog() => ShowCore(true);

        protected virtual void OnShowing(EventArgs e)
        {
            var handle = Showing;
            handle?.Invoke(this, e);
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            var handle = Closing;
            handle?.Invoke(this, e);
        }

        /// <summary>
        /// 更新窗体
        /// </summary>
        protected virtual void Update()
        {
            if (!Visible) { return; }

            //后续更新其实在nt6开启桌面主题的情况下，一干参数可以为null，
            //但是为了兼容其他情况，还是都指定
            if (!UpdateLayeredWindow(_hWnd,
                IntPtr.Zero, LocationInternal, _size,
                _dcMemory, PointOrSize.Empty,
                0, ref _blend, 2/*ULW_ALPHA*/))
            {
                //忽略窗体句柄无效ERROR_INVALID_WINDOW_HANDLE
                if (Marshal.GetLastWin32Error() == 0x578) { return; }

                throw new Win32Exception();
            }
        }

        /// <summary>
        /// 关闭并销毁窗体
        /// </summary>
        public void Close()
        {
            var e = new CancelEventArgs();
            OnClosing(e);
            if (!e.Cancel)
            {
                Visible = false;
                Dispose();
            }
        }

        bool _disposed;

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (syncObj)
            {
                if (_disposed) { return; }

                if (disposing)
                {
                    Tag = null;
                }

                //销毁窗体
                DestroyWindow(_hWnd);
                _hWnd = IntPtr.Zero;

                //注销窗口类
                //窗口类是共用的，每个实例都尝试注册和注销
                //实际效果就是先开的注册，后关的注销
                if (_wndClass != 0)
                {
                    if (UnregisterClass(ClassName, HInstance))
                    {
                        _wndProc = null;//只有注销成功时才解绑窗口过程
                    }
                    _wndClass = 0;
                }

                SelectObject(_dcMemory, _oldObj);
                DeleteDC(_dcMemory);
                DeleteObject(_hBmp);
                _oldObj = IntPtr.Zero;
                _dcMemory = IntPtr.Zero;
                _hBmp = IntPtr.Zero;
                _disposed = true;
            }
        }

        ~LayeredWindow()
        {
            Dispose(false);
        }

        //窗口过程委托
        private delegate IntPtr WndProcDelegate(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #region Win32 API

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int iconId);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern short RegisterClass(WNDCLASS wc);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetMessage(ref MSG msg, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG msg);

        [DllImport("user32.dll")]
        private static extern bool TranslateMessage(ref MSG msg);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UpdateLayeredWindow(IntPtr hWnd, IntPtr hdcDst, PointOrSize pptDst, PointOrSize pSizeDst, IntPtr hdcSrc, PointOrSize pptSrc, int crKey, ref BLENDFUNCTION pBlend, int dwFlags);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyWindow(IntPtr hWnd);

#pragma warning disable 414
#pragma warning disable 649
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class WNDCLASS
        {
            public int style;
            public WndProcDelegate lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr HWnd;
            public int Message;
            public IntPtr WParam;
            public IntPtr LParam;
            public int Time;
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class PointOrSize
        {
            public int XOrWidth, YOrHeight;

            public static readonly PointOrSize Empty = new PointOrSize();

            public PointOrSize() { XOrWidth = 0; YOrHeight = 0; }

            public PointOrSize(int xOrWidth, int yOrHeight)
            {
                XOrWidth = xOrWidth;
                YOrHeight = yOrHeight;
            }
        }

#pragma warning restore 649
#pragma warning restore 414
        #endregion
    }

    internal static class GraphicsUtils
    {
        /// <summary>
        /// 计算指定边界添加描边和阴影后的边界
        /// </summary>
        public static Rectangle GetBounds(Rectangle rectangle, Border border = null, int shadowRadius = 0, int offsetX = 0, int offsetY = 0)
        {
            if (border != null)
            {
                rectangle = border.GetBounds(rectangle);
            }
            var boundsShadow = DropShadow.GetBounds(rectangle, shadowRadius);
            boundsShadow.Offset(offsetX, offsetY);
            return Rectangle.Union(rectangle, boundsShadow);
        }

        /// <summary>
        /// 测量文本区尺寸
        /// </summary>
        public static SizeF MeasureString(string text, Font font, int width = 0, StringFormat stringFormat = null) =>
            MeasureString(text, font, new SizeF(width, width > 0 ? 999999 : 0), stringFormat);

        /// <summary>
        /// 测量文本区尺寸
        /// </summary>
        public static SizeF MeasureString(string text, Font font, SizeF area, StringFormat stringFormat = null)
        {
            IntPtr dcScreen = IntPtr.Zero;
            Graphics g = null;
            try
            {
                dcScreen = GetDC(IntPtr.Zero);
                g = Graphics.FromHdc(dcScreen);
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                return g.MeasureString(text, font, area, stringFormat);
            }
            finally
            {
                g?.Dispose();
                if (dcScreen != IntPtr.Zero)
                {
                    ReleaseDC(IntPtr.Zero, dcScreen);
                }
            }
        }

        /// <summary>
        /// 构造圆角矩形
        /// </summary>
        private static GraphicsPath GetRoundedRectangle(Rectangle rectangle, int radius)
        {
            //合理化圆角半径
            radius = Math.Min(radius, Math.Min(rectangle.Width, rectangle.Height) / 2);

            var path = new GraphicsPath();
            if (radius < 1)
            {
                path.AddRectangle(rectangle);
                return path;
            }

            int d = radius * 2;
            var arc = new Rectangle(rectangle.X, rectangle.Y, d, d);
            path.AddArc(arc, 180, 90);
            arc.X = rectangle.X + rectangle.Width - d;
            path.AddArc(arc, 270, 90);
            arc.Y = rectangle.Y + rectangle.Height - d;
            path.AddArc(arc, 0, 90);
            arc.X = rectangle.X;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 绘制矩形。可带圆角、阴影
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rectangle">矩形</param>
        /// <param name="brush">用于填充的画刷。为null则不填充</param>
        /// <param name="border">边框描述对象。对象无效则不描边</param>
        /// <param name="radius">圆角半径</param>
        /// <param name="shadowColor">阴影颜色</param>
        /// <param name="shadowRadius">阴影羽化半径</param>
        /// <param name="offsetX">阴影横向偏移</param>
        /// <param name="offsetY">阴影纵向偏移</param>
        public static void DrawRectangle(Graphics g, Rectangle rectangle, Brush brush, Border border, int radius, Color shadowColor, int shadowRadius = 0, int offsetX = 0, int offsetY = 0)
        {
            if (shadowColor.A == 0 || (shadowRadius == 0 && offsetX == 0 && offsetY == 0))
            {
                DrawRectangle(g, rectangle, brush, border, radius);
                return;
            }

            GraphicsPath path = null;
            Bitmap shadow = null;
            try
            {
                path = GetRoundedRectangle(rectangle, radius);
                shadow = DropShadow.Create(path, shadowColor, shadowRadius);

                var shadowBounds = DropShadow.GetBounds(rectangle, shadowRadius);
                shadowBounds.Offset(offsetX, offsetY);

                g.DrawImageUnscaled(shadow, shadowBounds.Location);
                DrawPath(g, path, brush, border);
            }
            finally
            {
                path?.Dispose();
                shadow?.Dispose();
            }
        }

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rectangle">矩形</param>
        /// <param name="brush">用于填充的画刷。为null则不填充</param>
        /// <param name="border">边框描述对象。对象无效则不描边</param>
        /// <param name="radius">圆角半径</param>
        public static void DrawRectangle(Graphics g, Rectangle rectangle, Brush brush = null, Border border = null, int radius = 0)
        {
            using var path = GetRoundedRectangle(rectangle, radius);
            DrawPath(g, path, brush, border);
        }

        /// <summary>
        /// 画路径
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path">路径</param>
        /// <param name="brush">用于填充的画刷。为null则不填充</param>
        /// <param name="border">边框描述对象。对象无效则不描边</param>
        public static void DrawPath(Graphics g, GraphicsPath path, Brush brush = null, Border border = null)
        {
            if (border == null) return;
            if (Border.IsValid(border) && border.Behind)
            {
                g.DrawPath(border.Pen, path);
            }
            if (brush != null)
            {
                g.FillPath(brush, path);
            }
            if (Border.IsValid(border) && !border.Behind)
            {
                g.DrawPath(border.Pen, path);
            }
        }


        /// <summary>
        /// 阴影生成类
        /// </summary>
        /* =================================================================================
         * 算法源于：http://blog.ivank.net/fastest-gaussian-blur.html
         * C#版实现取自：http://stackoverflow.com/questions/7364026/algorithm-for-fast-drop-shadow-in-gdi
         * 修改+优化：AhDung
         * - unsafe转safe
         * - RGB色值处理。解决边缘羽化区域黑化问题
         * - 减少运算量
         * =================================================================================
         */
        private static class DropShadow
        {
            const int CHANNELS = 4;
            const int InflateMultiple = 2;//单边外延radius的倍数

            /// <summary>
            /// 获取阴影边界。供外部定位阴影用
            /// </summary>
            /// <param name="path">形状</param>
            /// <param name="radius">模糊半径</param>
            /// <param name="pathBounds">形状边界</param>
            /// <param name="inflate">单边外延像素</param>
            public static Rectangle GetBounds(GraphicsPath path, int radius, out Rectangle pathBounds, out int inflate)
            {
                var bounds = pathBounds = Rectangle.Ceiling(path.GetBounds());
                inflate = radius * InflateMultiple;
                bounds.Inflate(inflate, inflate);
                return bounds;
            }

            /// <summary>
            /// 获取阴影边界
            /// </summary>
            /// <param name="source">原边界</param>
            /// <param name="radius">模糊半径</param>
            public static Rectangle GetBounds(Rectangle source, int radius)
            {
                var inflate = radius * InflateMultiple;
                source.Inflate(inflate, inflate);
                return source;
            }

            /// <summary>
            /// 创建阴影图片
            /// </summary>
            /// <param name="path">阴影形状</param>
            /// <param name="color">阴影颜色</param>
            /// <param name="radius">模糊半径</param>
            public static Bitmap Create(GraphicsPath path, Color color, int radius = 5)
            {
                var bounds = GetBounds(path, radius, out Rectangle pathBounds, out int inflate);
                var shadow = new Bitmap(bounds.Width, bounds.Height);

                if (color.A == 0)
                {
                    return shadow;
                }

                //将形状用color色画在阴影区中心
                Graphics g = null;
                GraphicsPath pathCopy = null;
                Matrix matrix = null;
                SolidBrush brush = null;
                try
                {
                    matrix = new Matrix();
                    matrix.Translate(-pathBounds.X + inflate, -pathBounds.Y + inflate);//先清除形状原有偏移再向中心偏移
                    pathCopy = (GraphicsPath)path.Clone();                             //基于形状副本操作
                    pathCopy.Transform(matrix);

                    brush = new SolidBrush(color);

                    g = Graphics.FromImage(shadow);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.FillPath(brush, pathCopy);
                }
                finally
                {
                    g?.Dispose();
                    brush?.Dispose();
                    pathCopy?.Dispose();
                    matrix?.Dispose();
                }

                if (radius <= 0)
                {
                    return shadow;
                }

                BitmapData data = null;
                try
                {
                    data = shadow.LockBits(new Rectangle(0, 0, shadow.Width, shadow.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                    //两次方框模糊就能达到不错的效果
                    //var boxes = DetermineBoxes(radius, 3);
                    BoxBlur(data, radius, color);
                    BoxBlur(data, radius, color);
                    //BoxBlur(shadowData, radius);

                    return shadow;
                }
                finally
                {
                    shadow.UnlockBits(data);
                }
            }

            /// <summary>
            /// 方框模糊
            /// </summary>
            /// <param name="data">图像内存数据</param>
            /// <param name="radius">模糊半径</param>
            /// <param name="color">透明色值</param>
#if UNSAFE
            private static unsafe void BoxBlur(BitmapData data, int radius, Color color)
#else
            private static void BoxBlur(BitmapData data, int radius, Color color)
#endif
            {
#if UNSAFE //unsafe项目下请定义编译条件：UNSAFE
                IntPtr p1 = data1.Scan0;
#else
                byte[] p1 = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, p1, 0, p1.Length);
#endif
                //色值处理
                //这步的意义在于让图片中的透明像素拥有color的色值（但仍然保持透明）
                //这样在混合时才能合出基于color的颜色（只是透明度不同），
                //否则它是与RGB(0,0,0)合，就会得到乌黑的渣特技
                byte R = color.R, G = color.G, B = color.B;
                for (int i = 3; i < p1.Length; i += 4)
                {
                    if (p1[i] == 0)
                    {
                        p1[i - 1] = R;
                        p1[i - 2] = G;
                        p1[i - 3] = B;
                    }
                }

                byte[] p2 = new byte[p1.Length];
                int radius2 = 2 * radius + 1;
                int First, Last, Sum;
                int stride = data.Stride,
                    width = data.Width,
                    height = data.Height;

                //只处理Alpha通道

                //横向
                for (int r = 0; r < height; r++)
                {
                    int start = r * stride;
                    int left = start;
                    int right = start + radius * CHANNELS;

                    First = p1[start + 3];
                    Last = p1[start + stride - 1];
                    Sum = (radius + 1) * First;

                    for (int column = 0; column < radius; column++)
                    {
                        Sum += p1[start + column * CHANNELS + 3];
                    }
                    for (var column = 0; column <= radius; column++, right += CHANNELS, start += CHANNELS)
                    {
                        Sum += p1[right + 3] - First;
                        p2[start + 3] = (byte)(Sum / radius2);
                    }
                    for (var column = radius + 1; column < width - radius; column++, left += CHANNELS, right += CHANNELS, start += CHANNELS)
                    {
                        Sum += p1[right + 3] - p1[left + 3];
                        p2[start + 3] = (byte)(Sum / radius2);
                    }
                    for (var column = width - radius; column < width; column++, left += CHANNELS, start += CHANNELS)
                    {
                        Sum += Last - p1[left + 3];
                        p2[start + 3] = (byte)(Sum / radius2);
                    }
                }

                //纵向
                for (int column = 0; column < width; column++)
                {
                    int start = column * CHANNELS;
                    int top = start;
                    int bottom = start + radius * stride;

                    First = p2[start + 3];
                    Last = p2[start + (height - 1) * stride + 3];
                    Sum = (radius + 1) * First;

                    for (int row = 0; row < radius; row++)
                    {
                        Sum += p2[start + row * stride + 3];
                    }
                    for (int row = 0; row <= radius; row++, bottom += stride, start += stride)
                    {
                        Sum += p2[bottom + 3] - First;
                        p1[start + 3] = (byte)(Sum / radius2);
                    }
                    for (int row = radius + 1; row < height - radius; row++, top += stride, bottom += stride, start += stride)
                    {
                        Sum += p2[bottom + 3] - p2[top + 3];
                        p1[start + 3] = (byte)(Sum / radius2);
                    }
                    for (int row = height - radius; row < height; row++, top += stride, start += stride)
                    {
                        Sum += Last - p2[top + 3];
                        p1[start + 3] = (byte)(Sum / radius2);
                    }
                }
#if !UNSAFE
                Marshal.Copy(p1, 0, data.Scan0, p1.Length);
#endif
            }

            // private static int[] DetermineBoxes(double Sigma, int BoxCount)
            // {
            //     double IdealWidth = Math.Sqrt((12 * Sigma * Sigma / BoxCount) + 1);
            //     int Lower = (int)Math.Floor(IdealWidth);
            //     if (Lower % 2 == 0)
            //         Lower--;
            //     int Upper = Lower + 2;
            //
            //     double MedianWidth = (12 * Sigma * Sigma - BoxCount * Lower * Lower - 4 * BoxCount * Lower - 3 * BoxCount) / (-4 * Lower - 4);
            //     int Median = (int)Math.Round(MedianWidth);
            //
            //     int[] BoxSizes = new int[BoxCount];
            //     for (int i = 0; i < BoxCount; i++)
            //         BoxSizes[i] = (i < Median) ? Lower : Upper;
            //     return BoxSizes;
            // }
        }

        #region Win32 API

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        #endregion
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}