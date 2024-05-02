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
 * 文件名称: UPrivilegeAPI.cs
 * 文件说明: 系统权限管理类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    internal class PrivilegeAPI
    {
        /// <summary>
        /// 授予权限
        /// </summary>
        /// <param name="privilegeName"></param>
        /// <returns></returns>
        public static bool GrantPrivilege(string privilegeName)
        {
            try
            {
                LUID locallyUniqueIdentifier = new LUID();
                if (LookupPrivilegeValue(null, privilegeName, ref locallyUniqueIdentifier))
                {
                    TOKEN_PRIVILEGES tokenPrivileges = new TOKEN_PRIVILEGES();
                    tokenPrivileges.PrivilegeCount = 1;

                    LUID_AND_ATTRIBUTES luidAndAtt = new LUID_AND_ATTRIBUTES();
                    // luidAndAtt.Attributes should be SE_PRIVILEGE_ENABLED to enable privilege
                    luidAndAtt.Attributes = PrivilegeAttributes.SE_PRIVILEGE_ENABLED;
                    luidAndAtt.Luid = locallyUniqueIdentifier;
                    tokenPrivileges.Privilege = luidAndAtt;

                    IntPtr tokenHandle = IntPtr.Zero;
                    try
                    {
                        if (OpenProcessToken(GetCurrentProcess(), TokenAccess.TOKEN_ADJUST_PRIVILEGES | TokenAccess.TOKEN_QUERY, out tokenHandle))
                        {
                            if (AdjustTokenPrivileges(tokenHandle, false, ref tokenPrivileges, 1024, IntPtr.Zero, 0))
                            {
                                // 当前用户没有关联该权限
                                // 需要在windows系统（本地安全策略——本地策略——用户权限分配）中设置为该权限添加当前用户
                                if (Marshal.GetLastWin32Error() != ERROR_NOT_ALL_ASSIGNED)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                        {
                            CloseHandle(tokenHandle);
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("授权异常: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 收回权限
        /// </summary>
        /// <param name="privilegeName"></param>
        /// <returns></returns>
        public static bool RevokePrivilege(string privilegeName)
        {
            try
            {
                LUID locallyUniqueIdentifier = new LUID();

                if (LookupPrivilegeValue(null, privilegeName, ref locallyUniqueIdentifier))
                {
                    TOKEN_PRIVILEGES tokenPrivileges = new TOKEN_PRIVILEGES();
                    tokenPrivileges.PrivilegeCount = 1;

                    LUID_AND_ATTRIBUTES luidAndAtt = new LUID_AND_ATTRIBUTES();
                    // luidAndAtt.Attributes should be none (not set) to disable privilege
                    luidAndAtt.Luid = locallyUniqueIdentifier;
                    tokenPrivileges.Privilege = luidAndAtt;

                    IntPtr tokenHandle = IntPtr.Zero;
                    try
                    {
                        if (OpenProcessToken(GetCurrentProcess(), TokenAccess.TOKEN_ADJUST_PRIVILEGES | TokenAccess.TOKEN_QUERY, out tokenHandle))
                        {
                            if (AdjustTokenPrivileges(tokenHandle, false, ref tokenPrivileges, 1024, IntPtr.Zero, 0))
                            {
                                // 当前用户没有关联该权限
                                // 需要在windows系统（本地安全策略——本地策略——用户权限分配）中设置为该权限添加当前用户
                                if (Marshal.GetLastWin32Error() != ERROR_NOT_ALL_ASSIGNED)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                        {
                            CloseHandle(tokenHandle);
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("撤权异常: " + ex.Message);
                return false;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccesss, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

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

        // 如果进程的访问令牌中没有关联某权限，则AdjustTokenPrivileges函数调用将会返回错误码ERROR_NOT_ALL_ASSIGNED（值为1300）
        public const int ERROR_NOT_ALL_ASSIGNED = 1300;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct LUID
    {
        internal int LowPart;
        internal uint HighPart;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct LUID_AND_ATTRIBUTES
    {
        internal LUID Luid;
        internal uint Attributes;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct TOKEN_PRIVILEGES
    {
        internal int PrivilegeCount;
        internal LUID_AND_ATTRIBUTES Privilege;
    }

    internal class TokenAccess
    {
        internal const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        internal const uint STANDARD_RIGHTS_READ = 0x00020000;
        internal const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        internal const uint TOKEN_DUPLICATE = 0x0002;
        internal const uint TOKEN_IMPERSONATE = 0x0004;
        internal const uint TOKEN_QUERY = 0x0008;
        internal const uint TOKEN_QUERY_SOURCE = 0x0010;
        internal const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        internal const uint TOKEN_ADJUST_GROUPS = 0x0040;
        internal const uint TOKEN_ADJUST_DEFAULT = 0x0080;
        internal const uint TOKEN_ADJUST_SESSIONID = 0x0100;
        internal const uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        internal const uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);
    }

    internal class PrivilegeAttributes
    {
        internal const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        internal const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const uint SE_PRIVILEGE_REMOVED = 0x00000004;
        internal const uint SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;
    }

    internal class PrivilegeConstants
    {
        internal const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
        internal const string SE_AUDIT_NAME = "SeAuditPrivilege";
        internal const string SE_BACKUP_NAME = "SeBackupPrivilege";
        internal const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
        internal const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
        internal const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
        internal const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
        internal const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";
        internal const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
        internal const string SE_DEBUG_NAME = "SeDebugPrivilege";
        internal const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
        internal const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
        internal const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
        internal const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        internal const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
        internal const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
        internal const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
        internal const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
        internal const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
        internal const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        internal const string SE_RELABEL_NAME = "SeRelabelPrivilege";
        internal const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
        internal const string SE_RESTORE_NAME = "SeRestorePrivilege";
        internal const string SE_SECURITY_NAME = "SeSecurityPrivilege";
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
        internal const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
        internal const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
        internal const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
        internal const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
        internal const string SE_TCB_NAME = "SeTcbPrivilege";
        internal const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
        internal const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
        internal const string SE_UNDOCK_NAME = "SeUndockPrivilege";
        internal const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
    }
}