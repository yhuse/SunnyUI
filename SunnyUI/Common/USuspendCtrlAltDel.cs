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
 * 文件名称: USuspendCtrlAltDel.cs
 * 文件说明: 通过挂起winlogon线程来屏蔽Ctrl+Alt+Delete
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-09-17: V2.2.7 增加文件说明
******************************************************************************/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// 通过挂起winlogon.exe线程来屏蔽Ctrl+Alt+Delete
    /// 程序需要管理员权限
    /// 需要提升进程权限为SE_PRIVILEGE_ENABLED权限
    /// winlogon管理着开关机登录界面等功能
    /// 调用Suspend挂起winlogon.exe，用完后需要Resume，否则无法调用关机界面
    /// </summary>
    public static class SuspendCtrlAltDelete
    {
        /// <summary>
        /// 获取权限
        /// </summary>
        public static void GetSeDebugPrivilege()
        {
            IntPtr hToken;
            LUID luidSEDebugNameValue = new LUID();


            if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken))
            {
                Console.WriteLine("OpenProcessToken() failed, error = {0} . SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                return;
            }
            else
            {
                Console.WriteLine("OpenProcessToken() successfully");
            }

            if (!LookupPrivilegeValue(null, SE_DEBUG_NAME, ref luidSEDebugNameValue))
            {
                Console.WriteLine("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                CloseHandle(hToken);
                return;
            }
            else
            {
                Console.WriteLine("LookupPrivilegeValue() successfully");
            }

            TOKEN_PRIVILEGES tkpPrivileges = new TOKEN_PRIVILEGES();
            tkpPrivileges.PrivilegeCount = 1;
            tkpPrivileges.Privilege.Luid = luidSEDebugNameValue;
            tkpPrivileges.Privilege.Attributes = SE_PRIVILEGE_ENABLED;

            if (!AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, 0))
            {
                Console.WriteLine("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
            }
            else
            {
                Console.WriteLine("SeDebugPrivilege is now available");
            }

            CloseHandle(hToken);
        }

        /// <summary>
        /// 挂起
        /// </summary>
        public static void Suspend()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == "winlogon")
                {

                    IntPtr p = OpenThread(PROCESS_ALL_ACCESS, false, (IntPtr)process.Threads[0].Id);
                    SuspendThread(p);
                    CloseHandle(p);
                }
            }
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public static void Resume()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == "winlogon")
                {
                    IntPtr p = OpenThread(PROCESS_ALL_ACCESS, false, (IntPtr)process.Threads[0].Id);
                    ResumeThread(p);
                    CloseHandle(p);
                }
            }
        }

        const int PROCESS_ALL_ACCESS = 0x001F03FF;
        const string SE_DEBUG_NAME = "SeDebugPrivilege";
        const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        const uint STANDARD_RIGHTS_READ = 0x00020000;
        const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        const uint TOKEN_DUPLICATE = 0x0002;
        const uint TOKEN_IMPERSONATE = 0x0004;
        const uint TOKEN_QUERY = 0x0008;
        const uint TOKEN_QUERY_SOURCE = 0x0010;
        const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        const uint TOKEN_ADJUST_GROUPS = 0x0040;
        const uint TOKEN_ADJUST_DEFAULT = 0x0080;
        const uint TOKEN_ADJUST_SESSIONID = 0x0100;
        const uint TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY;
        const uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
                                                TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
                                                TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
                                                TOKEN_ADJUST_SESSIONID);


        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, IntPtr dwThreadId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32")]
        private static extern int SuspendThread(IntPtr hThread);

        [DllImport("kernel32")]
        private static extern int ResumeThread(IntPtr hThread);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            [MarshalAs(UnmanagedType.Struct)] ref LUID lpLuid);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
            [MarshalAs(UnmanagedType.Struct)] ref TOKEN_PRIVILEGES NewState,
            uint BufferLength, IntPtr PreviousState, uint ReturnLength);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct LUID
        {
            internal int LowPart;
            internal uint HighPart;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct TOKEN_PRIVILEGES
        {
            internal int PrivilegeCount;
            internal LUID_AND_ATTRIBUTES Privilege;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct LUID_AND_ATTRIBUTES
        {
            internal LUID Luid;
            internal uint Attributes;
        }
    }
}
