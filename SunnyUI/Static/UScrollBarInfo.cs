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

using Sunny.UI.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public static class ScrollBarInfo
    {
        public const int SIF_RANGE = 1;
        public const int SIF_PAGE = 2;
        public const int SIF_POS = 4;
        public const int SIF_TRACKPOS = 16;
        public const int SIF_DISABLENOSCROLL = 8;
        public const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;

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

        /// <summary>
        /// 获取控件滚动条信息
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <returns>信息结构</returns>
        public static SCROLLINFO GetInfo(IntPtr handle)
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = Marshal.SizeOf(si);
            si.fMask = SIF_DISABLENOSCROLL | SIF_ALL;
            User.GetScrollInfo(handle, User.SB_VERT, ref si);
            return si;
        }

        /// <summary>
        /// 设置控件滚动条滚动值
        /// </summary>
        /// <param name="handle">控件句柄</param>
        /// <param name="value">滚动值</param>
        public static void SetScrollValue(IntPtr handle, int value)
        {
            SCROLLINFO info = GetInfo(handle);
            info.nPos = value;
            User.SetScrollInfo(handle, User.SB_VERT, ref info, true);
            User.PostMessage(handle, User.WM_VSCROLL, MakeLong(User.SB_THUMBTRACK, highPart: (short)info.nPos), 0);
        }

        /// <summary>
        /// 控件向上滚动一个单位
        /// </summary>
        /// <param name="handle">控件句柄</param>
        public static void ScrollUp(IntPtr handle)
        {
            User.SendMessage(handle, User.WM_VSCROLL, User.SB_LINEUP, 0);
        }

        /// <summary>
        /// 控件向下滚动一个单位
        /// </summary>
        /// <param name="handle">控件句柄</param>
        public static void ScrollDown(IntPtr handle)
        {
            User.SendMessage(handle, User.WM_VSCROLL, User.SB_LINEDOWN, 0);
        }
    }
}