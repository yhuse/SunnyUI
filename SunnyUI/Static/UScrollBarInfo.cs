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
 * 文件名称: UScrollBarInfo.cs
 * 文件说明: 滚动条信息获取类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public static class ScrollBarInfo
    {
        public struct ScrollInfo
        {
            public int cbSize;    //ScrollInfo结构体本身的字节大小
            public int fMask;     //fMask表示设置或获取哪些数据，如：SIF_ALL所有数据成员都有效、SIF_PAGE（nPage有效）、SIF_POS（nPos有效）、SIF_RANGE（nMin和nMax有效）、SIF_TRACKPOS（nTrackPos有效）。
            public int nMin;      //最小滚动位置
            public int nMax;      //最大滚动位置
            public int nPage;     //页面尺寸
            public int nPos;      //滚动块的位置
            public int nTrackPos; //滚动块当前被拖动的位置，不能在SetScrollInfo中指定

            public int ScrollMax => (nMax + 1 - nPage);
        }

        //API声明
        [DllImport("user32.dll")]//[return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr handle, int fnBar, ref ScrollInfo si);

        [DllImport("user32.dll")]//[return: MarshalAs(UnmanagedType.Bool)]
        private static extern int SetScrollInfo(IntPtr handle, int fnBar, ref ScrollInfo si, bool fRedraw);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        private static extern bool PostMessage(IntPtr handle, int msg, uint wParam, uint lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// ShowScrollBar
        /// </summary>
        /// <param name="hWnd">hWnd</param>
        /// <param name="wBar">0:horizontal,1:vertical,3:both</param>
        /// <param name="bShow">bShow</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        //常量声明
        public const int SB_HORZ = 0;//找回所指定窗体的标准水平滚动条参数

        public const int SB_VERT = 1;//找回所指定窗体的标准垂直滚动条参数
        public const int SB_CTL = 2;//找回滚动条控制参数
        public const int SB_THUMBTRACK = 5;
        public const int SIF_RANGE = 1;
        public const int SIF_PAGE = 2;
        public const int SIF_POS = 4;
        public const int SIF_TRACKPOS = 16;
        public const int SIF_DISABLENOSCROLL = 8;
        public const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;
        public const int EM_GETLINECOUNT = 0x00BA;
        public const int WM_VSCROLL = 0x0115;
        public const int SB_LINEUP = 0;
        public const int SB_LINEDOWN = 1;

        //私有方法
        private static uint MakeLong(short lowPart, short highPart)
        {
            return (ushort)lowPart | (uint)(highPart << 16);
        }

        public static int VerticalScrollBarWidth()
        {
            return SystemInformation.VerticalScrollBarWidth;
        }

        public static int HorizontalScrollBarHeight()
        {
            return SystemInformation.HorizontalScrollBarHeight;
        }

        //共有方法
        /// <summary>
        /// 获取TextBox行数
        /// </summary>
        /// <param name="handle">TextBox句柄</param>
        /// <returns>行数</returns>
        public static int GetTextBoxLineLength(IntPtr handle)
        {
            int cont = SendMessage(handle, EM_GETLINECOUNT, 0, 0);
            return cont;
        }

        /// <summary>
        /// 获取控件滚动条信息
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <returns>信息结构</returns>
        public static ScrollInfo GetInfo(IntPtr handle)
        {
            ScrollInfo si = new ScrollInfo();
            si.cbSize = Marshal.SizeOf(si);
            si.fMask = SIF_DISABLENOSCROLL | SIF_ALL;
            GetScrollInfo(handle, SB_VERT, ref si);
            return si;
        }

        /// <summary>
        /// 设置控件滚动条滚动值
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <param name="value">滚动值</param>
        public static void SetScrollValue(IntPtr handle, int value)
        {
            ScrollInfo info = GetInfo(handle);
            info.nPos = value;
            SetScrollInfo(handle, SB_VERT, ref info, true);
            PostMessage(handle, WM_VSCROLL, MakeLong(SB_THUMBTRACK, highPart: (short)info.nPos), 0);
        }

        /// <summary>
        /// 控件向上滚动一个单位
        /// </summary>
        /// <param name="handle">控件句柄</param>
        public static void ScrollUp(IntPtr handle)
        {
            SendMessage(handle, WM_VSCROLL, SB_LINEUP, 0);
        }

        /// <summary>
        /// 控件向下滚动一个单位
        /// </summary>
        /// <param name="handle">控件句柄</param>
        public static void ScrollDown(IntPtr handle)
        {
            SendMessage(handle, WM_VSCROLL, SB_LINEDOWN, 0);
        }
    }
}