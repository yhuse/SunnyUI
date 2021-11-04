using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Sunny.UI.Win32
{
    public static class Win32Helper
    {
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
                IntPtr hProcess =Kernel.OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
                //将制定内存中的值读入缓冲区
                Kernel.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, buffer.Length,System.IntPtr.Zero);
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

    public partial class Kernel
    {
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
    }

    public partial class WinMM
    {
        [DllImport("winmm.dll")]
        public static extern int timeSetEvent(int uDelay, int uResolution, TimerSetEventCallback fptc, int dwUser, int uFlags);

        public delegate void TimerSetEventCallback(int uTimerID, uint uMsg, uint dwUser, UIntPtr dw1, UIntPtr dw2);
    }
}
