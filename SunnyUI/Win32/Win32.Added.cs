using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Sunny.UI.Win32
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public static class Win32Helper
    {
        /// <summary>
        /// 自然排序
        /// </summary>
        /// <param name="strs">字符串列表</param>
        /// <returns>自然排序结果</returns>
        /// var names = new [] { "2.log", "10.log", "1.log" };
        /// 排序结果：
        /// 1.log
        /// 2.log
        /// 10.log
        public static IOrderedEnumerable<string> NatualOrdering(this IEnumerable<string> strs)
        {
            if (strs == null) return null;
            return strs.OrderBy(s => s, new NatualOrderingComparer());
        }

        public static readonly IntPtr TRUE = new IntPtr(1);

        public static IntPtr IntPtr(this int value)
        {
            return new IntPtr(value);
        }

        //根据进程名获取PID
        public static int GetPidByProcessName(string processName)
        {
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            foreach (Process p in arrayProcess)
            {
                return p.Id;
            }
            return 0;
        }

        /// <summary>
        /// 读取指定进程内存中的值
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="baseAddress"></param>
        /// <param name="buffer"></param>
        public static void ReadMemoryValue(string processName, int baseAddress, ref byte[] buffer)
        {
            try
            {
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = Kernel.OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                //将制定内存中的值读入缓冲区
                Kernel.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, buffer.Length, System.IntPtr.Zero);
                //关闭操作
                Kernel.CloseHandle(hProcess);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 将值写入指定进程内存地址中
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="baseAddress"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static int WriteMemoryValue(string processName, int baseAddress, byte[] buffer)
        {
            try
            {
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = Kernel.OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                int count = 0;
                //从指定内存中写入字节集数据
                Kernel.WriteProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, buffer.Length, ref count);
                //关闭操作
                Kernel.CloseHandle(hProcess);
                return count;
            }
            catch
            {
                return 0;
            }
        }
    }

    public partial class GDI
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr window, IntPtr hdcBlt, uint nFlags);
    }

    public partial class User
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr wnd, int hRgn, bool bRedraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint = true);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PtInRect([In] ref RECT lprc, Point pt);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr handle, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, int dwExStyle);

    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NCCALCSIZE_PARAMS
    {
        public RECT rgrc0, rgrc1, rgrc2;
        public WINDOWPOS lppos;
    }

    public class Dwm
    {
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;

            public MARGINS(int left, int right, int top, int bottom)
            {
                leftWidth = left;
                rightWidth = right;
                topHeight = top;
                bottomHeight = bottom;
            }
        }
    }

    public partial class AdvApi
    {
        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccesss, out IntPtr TokenHandle);


        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            [MarshalAs(UnmanagedType.Struct)] ref LUID lpLuid);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
            [MarshalAs(UnmanagedType.Struct)] ref TOKEN_PRIVILEGES NewState,
            uint BufferLength, IntPtr PreviousState, uint ReturnLength);
    }

    [Flags]
    public enum ExecutionState : uint
    {
        /// <summary>
        /// Forces the system to be in the working state by resetting the system idle timer.
        /// </summary>
        SystemRequired = 0x01,

        /// <summary>
        /// Forces the display to be on by resetting the display idle timer.
        /// </summary>
        DisplayRequired = 0x02,

        /// <summary>
        /// This value is not supported. If <see cref="UserPresent"/> is combined with other esFlags values, the call will fail and none of the specified states will be set.
        /// </summary>
        [Obsolete("This value is not supported.")]
        UserPresent = 0x04,

        /// <summary>
        /// Enables away mode. This value must be specified with <see cref="Continuous"/>.
        /// <para />
        /// Away mode should be used only by media-recording and media-distribution applications that must perform critical background processing on desktop computers while the computer appears to be sleeping.
        /// </summary>
        AwaymodeRequired = 0x40,

        /// <summary>
        /// Informs the system that the state being set should remain in effect until the next call that uses <see cref="Continuous"/> and one of the other state flags is cleared.
        /// </summary>
        Continuous = 0x80000000,
    }

    public partial class Kernel
    {
        [DllImport("kernel32")]
        public static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CompareStringEx(string localeName, int flags, string str1, int count1, string str2,
                int count2, IntPtr versionInformation, IntPtr reserved, int param);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, IntPtr dwThreadId);

        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //从指定内存中读取字节集数据
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);
    }

    internal class NatualOrderingComparer : IComparer<string>
    {
        //static readonly Int32 NORM_IGNORECASE = 0x00000001;
        //static readonly Int32 NORM_IGNORENONSPACE = 0x00000002;
        //static readonly Int32 NORM_IGNORESYMBOLS = 0x00000004;
        //static readonly Int32 LINGUISTIC_IGNORECASE = 0x00000010;
        //static readonly Int32 LINGUISTIC_IGNOREDIACRITIC = 0x00000020;
        //static readonly Int32 NORM_IGNOREKANATYPE = 0x00010000;
        //static readonly Int32 NORM_IGNOREWIDTH = 0x00020000;
        //static readonly Int32 NORM_LINGUISTIC_CASING = 0x08000000;
        //static readonly Int32 SORT_STRINGSORT = 0x00001000;
        static readonly Int32 SORT_DIGITSASNUMBERS = 0x00000008;

        //static readonly String LOCALE_NAME_USER_DEFAULT = null;
        static readonly String LOCALE_NAME_INVARIANT = String.Empty;
        //static readonly String LOCALE_NAME_SYSTEM_DEFAULT = "!sys-default-locale";

        readonly String locale;

        public NatualOrderingComparer() : this(CultureInfo.CurrentCulture)
        {
        }

        public NatualOrderingComparer(CultureInfo cultureInfo)
        {
            if (cultureInfo.IsNeutralCulture)
                this.locale = LOCALE_NAME_INVARIANT;
            else
                this.locale = cultureInfo.Name;
        }

        public Int32 Compare(String x, String y)
        {
            // CompareStringEx return 1, 2, or 3. Subtract 2 to get the return value.
            return Kernel.CompareStringEx(this.locale, SORT_DIGITSASNUMBERS, // Add other flags if required.
              x, x.Length, y, y.Length, IntPtr.Zero, IntPtr.Zero, 0) - 2;
        }
    }

    public partial class WinMM
    {
        [DllImport("winmm.dll")]
        public static extern int timeSetEvent(int uDelay, int uResolution, TimerSetEventCallback fptc, int dwUser, int uFlags);

        public delegate void TimerSetEventCallback(int uTimerID, uint uMsg, uint dwUser, UIntPtr dw1, UIntPtr dw2);
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}