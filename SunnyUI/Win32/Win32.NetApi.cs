using System;
using System.Runtime.InteropServices;
using System.Text;

using HANDLE = System.IntPtr;
using HWND = System.IntPtr;

namespace Sunny.UI.Win32
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public struct NETRESOURCE
    {
        public int dwScope;
        public int dwType;
        public int dwDisplayType;
        public int dwUsage;
        public string lpLocalName;
        public string lpRemoteName;
        public string lpComment;
        public string lpProvider;
    }
    public struct USER_INFO_3
    {
        public int Name;
        public int Password;
        public int PasswordAge;
        public int Privilege;
        public int HomeDir;
        public int Comment;
        public int Flags;
        public int ScriptPath;
        public int AuthFlags;
        public int FullName;
        public int UserComment;
        public int Parms;
        public int Workstations;
        public int LastLogon;
        public int LastLogoff;
        public int AcctExpires;
        public int MaxStorage;
        public int UnitsPerWeek;
        public int LogonHours;
        public int BadPwCount;
        public int NumLogons;
        public int LogonServer;
        public int CountryCode;
        public int CodePage;
        public int UserID;
        public int PrimaryGroupID;
        public int Profile;
        public int HomeDirDrive;
        public int PasswordExpired;
    }
    public struct GROUP_INFO_2
    {
        public int Name;
        public int Comment;
        public int GroupID;
        public int Attributes;
    }
    public struct LOCALGROUP_MEMBERS_INFO_0
    {
        public int pSID;
    }
    public struct LOCALGROUP_MEMBERS_INFO_1
    {
        public int pSID;
        public g_netSID_NAME_USE eUsage;
        public int psName;
    }
    public struct WKSTA_INFO_102
    {
        public int wki102_platform_id;
        public int wki102_computername;
        public int wki102_langroup;
        public int wki102_ver_major;
        public int wki102_ver_minor;
        public int wki102_lanroot;
        public int wki102_logged_on_users;
    }
    public struct WKSTA_USER_INFO_1
    {
        public int wkui1_username;
        public int wkui1_logon_domain;
        public int wkui1_oth_domains;
        public int wkui1_logon_server;
    }
    public enum g_netSID_NAME_USE
    {
        SidTypeUser = 1,
        SidTypeGroup = 2,
        SidTypeDomain = 3,
        SidTypeAlias = 4,
        SidTypeWellKnownGroup = 5,
        SidTypeDeletedAccount = 6,
        SidTypeInvalid = 7,
        SidTypeUnknown = 8,
    }


    public abstract class Mpr
    {
        [DllImport("mpr")] public static extern int WNetAddConnection(string lpszNetPath, string lpszPassword, string lpszLocalName);
        [DllImport("mpr")] public static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUserName, int dwFlags);
        [DllImport("mpr")] public static extern int WNetCancelConnection(string lpszName, int bForce);
        [DllImport("mpr")] public static extern int WNetCancelConnection2(string lpName, int dwFlags, int fForce);
        [DllImport("mpr")] public static extern int WNetCloseEnum(HANDLE hEnum);
        [DllImport("mpr")] public static extern int WNetConnectionDialog(HWND hwnd, int dwType);
        [DllImport("mpr")] public static extern int WNetDisconnectDialog(HWND hwnd, int dwType);
        [DllImport("mpr")] public static extern int WNetEnumResource(HANDLE hEnum, ref int lpcCount, ref NETRESOURCE lpBuffer, ref int lpBufferSize);
        [DllImport("mpr")] public static extern int WNetGetConnection(string lpszLocalName, string lpszRemoteName, int cbRemoteName);
        [DllImport("mpr")] public static extern int WNetGetLastError(int lpError, StringBuilder lpErrorBuf, int nErrorBufSize, string lpNameBuf, int nNameBufSize);
        [DllImport("mpr")] public static extern int WNetGetUser(string lpName, StringBuilder lpUserName, ref int lpnLength);
        [DllImport("mpr")] public static extern int WNetOpenEnum(int dwScope, int dwType, int dwUsage, ref NETRESOURCE lpNetResource, ref int lphEnum);
    }

    public abstract class NetApi
    {
        [DllImport("Netapi32")] public static extern int NetApiBufferFree(int lpBuffer);
        [DllImport("Netapi32")] public static extern int NetRemoteTOD(IntPtr yServer, int pBuffer);
        [DllImport("Netapi32")] public static extern int NetUserChangePassword(IntPtr Domain, IntPtr User, Byte OldPass, Byte NewPass);
        [DllImport("Netapi32")] public static extern int NetUserGetGroups(IntPtr lpServer, Byte UserName, int Level, ref int lpBuffer, int PrefMaxLen, ref int lpEntriesRead, ref int lpTotalEntries);
        [DllImport("Netapi32")] public static extern int NetUserGetInfo(IntPtr lpServer, Byte UserName, int Level, ref int lpBuffer);
        [DllImport("Netapi32")] public static extern int NetUserGetLocalGroups(IntPtr lpServer, Byte UserName, int Level, int Flags, ref int lpBuffer, int MaxLen, ref int lpEntriesRead, ref int lpTotalEntries);
        [DllImport("Netapi32")] public static extern int NetWkstaGetInfo(IntPtr lpServer, int Level, IntPtr lpBuffer);
        [DllImport("Netapi32")] public static extern int NetWkstaUserGetInfo(IntPtr reserved, int Level, IntPtr lpBuffer);
        [DllImport("netapi32")] public static extern int NetUserAdd(IntPtr lpServer, int Level, ref USER_INFO_3 lpUser, ref int lpError);
        [DllImport("netapi32")] public static extern int NetLocalGroupDelMembers(int psServer, int psLocalGroup, int lLevel, ref LOCALGROUP_MEMBERS_INFO_0 uMember, int lMemberCount);
        [DllImport("netapi32")] public static extern int NetLocalGroupGetMembers(int psServer, int psLocalGroup, int lLevel, int pBuffer, int lMaxLength, int plEntriesRead, int plTotalEntries, int phResume);

        public const int CNLEN = 15;
        public const int CONNECT_UPDATE_PROFILE = 0x1;
        public const int FILTER_INTERDOMAIN_TRUST_ACCOUNT = 0x8;
        public const int FILTER_NORMAL_ACCOUNT = 0x2;
        public const int FILTER_PROXY_ACCOUNT = 0x4;
        public const int FILTER_SERVER_TRUST_ACCOUNT = 0x20;
        public const int FILTER_TEMP_DUPLICATE_ACCOUNT = 0x1;
        public const int FILTER_WORKSTATION_TRUST_ACCOUNT = 0x10;
        public const int GNLEN = UNLEN;
        public const int LG_INCLUDE_INDIRECT = 0x1;
        public const int LM20_PWLEN = 14;
        public const int MAXCOMMENTSZ = 256;
        public const int NERR_BASE = 2100;
        public const int NERR_GroupExists = (NERR_BASE + 123);
        public const int NERR_InvalidComputer = (NERR_BASE + 251);
        public const int NERR_NotPrimary = (NERR_BASE + 126);
        public const int NERR_PasswordTooShort = (NERR_BASE + 145);
        public const int NERR_Success = 0;
        public const int NERR_UserExists = (NERR_BASE + 124);
        public const int PWLEN = 256;
        public const int RESOURCEDISPLAYTYPE_DOMAIN = 0x1;
        public const int RESOURCEDISPLAYTYPE_FILE = 0x4;
        public const int RESOURCEDISPLAYTYPE_GENERIC = 0x0;
        public const int RESOURCEDISPLAYTYPE_GROUP = 0x5;
        public const int RESOURCEDISPLAYTYPE_SERVER = 0x2;
        public const int RESOURCEDISPLAYTYPE_SHARE = 0x3;
        public const int RESOURCETYPE_ANY = 0x0;
        public const int RESOURCETYPE_DISK = 0x1;
        public const int RESOURCETYPE_PRINT = 0x2;
        public const int RESOURCETYPE_UNKNOWN = 0xFFFF;
        public const int RESOURCEUSAGE_ALL = 0x0;
        public const int RESOURCEUSAGE_CONNECTABLE = 0x1;
        public const int RESOURCEUSAGE_CONTAINER = 0x2;
        public const int RESOURCEUSAGE_RESERVED = unchecked((int)0x80000000);
        public const int RESOURCE_CONNECTED = 0x1;
        public const int RESOURCE_ENUM_ALL = 0xFFFF;
        public const int RESOURCE_GLOBALNET = 0x2;
        public const int RESOURCE_PUBLICNET = 0x2;
        public const int RESOURCE_REMEMBERED = 0x3;
        public const int TIMEQ_FOREVER = -1;
        public const int UF_ACCOUNTDISABLE = 0x2;
        public const int UF_HOMEDIR_REQUIRED = 0x8;
        public const int UF_LOCKOUT = 0x10;
        public const int UF_PASSWD_CANT_CHANGE = 0x40;
        public const int UF_PASSWD_NOTREQD = 0x20;
        public const int UF_SCRIPT = 0x1;
        public const int UNITS_PER_DAY = 24;
        public const int UNITS_PER_WEEK = UNITS_PER_DAY * 7;
        public const int UNLEN = 256;
        public const int USER_MAXSTORAGE_UNLIMITED = -1;
        public const int USER_NO_LOGOFF = -1;
        public const int USER_PRIV_ADMIN = 2;
        public const int USER_PRIV_GUEST = 0;
        public const int USER_PRIV_MASK = 3;
        public const int USER_PRIV_USER = 1;
        public const int WN_ACCESS_DENIED = ERROR.ERROR_ACCESS_DENIED;
        public const int WN_ALREADY_CONNECTED = ERROR.ERROR_ALREADY_ASSIGNED;
        public const int WN_BAD_LOCALNAME = ERROR.ERROR_BAD_DEVICE;
        public const int WN_BAD_NETNAME = ERROR.ERROR_BAD_NET_NAME;
        public const int WN_BAD_PASSWORD = ERROR.ERROR_INVALID_PASSWORD;
        public const int WN_BAD_POINTER = ERROR.ERROR_INVALID_ADDRESS;
        public const int WN_BAD_PROFILE = ERROR.ERROR_BAD_PROFILE;
        public const int WN_BAD_PROVIDER = ERROR.ERROR_BAD_PROVIDER;
        public const int WN_BAD_USER = ERROR.ERROR_BAD_USERNAME;
        public const int WN_BAD_VALUE = ERROR.ERROR_INVALID_PARAMETER;
        public const int WN_CANNOT_OPEN_PROFILE = ERROR.ERROR_CANNOT_OPEN_PROFILE;
        public const int WN_CONNECTION_CLOSED = ERROR.ERROR_CONNECTION_UNAVAIL;
        public const int WN_DEVICE_ERROR = ERROR.ERROR_GEN_FAILURE;
        public const int WN_DEVICE_IN_USE = ERROR.ERROR_DEVICE_IN_USE;
        public const int WN_EXTENDED_ERROR = ERROR.ERROR_EXTENDED_ERROR;
        public const int WN_FUNCTION_BUSY = ERROR.ERROR_BUSY;
        public const int WN_MORE_DATA = ERROR.ERROR_MORE_DATA;
        public const int WN_NET_ERROR = ERROR.ERROR_UNEXP_NET_ERR;
        public const int WN_NOT_CONNECTED = ERROR.ERROR_NOT_CONNECTED;
        public const int WN_NOT_SUPPORTED = ERROR.ERROR_NOT_SUPPORTED;
        public const int WN_NO_NETWORK = ERROR.ERROR_NO_NETWORK;
        public const int WN_NO_NET_OR_BAD_PATH = ERROR.ERROR_NO_NET_OR_BAD_PATH;
        public const int WN_OPEN_FILES = ERROR.ERROR_OPEN_FILES;
        public const int WN_OUT_OF_MEMORY = ERROR.ERROR_NOT_ENOUGH_MEMORY;
        public const int WN_SUCCESS = ERROR.NO_ERROR;
        public const int WN_WINDOWS_ERROR = ERROR.ERROR_UNEXP_NET_ERR;
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}