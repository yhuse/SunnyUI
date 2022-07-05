using System.Runtime.InteropServices;
using HANDLE = System.IntPtr;
using HWND = System.IntPtr;

namespace Sunny.UI.Win32
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public struct DRAGINFO
    {
        public int uSize;
        public POINT pt;
        public int fNC;
        public string lpFileList;
        public int grfKeyState;
    }
    public struct APPBARDATA
    {
        public int cbSize;
        public HWND hwnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public int lParam;
    }
    public struct SHFILEOPSTRUCT
    {
        public HWND hwnd;
        public int wFunc;
        public string pFrom;
        public string pTo;
        public short fFlags;
        public int fAnyOperationsAborted;
        public HANDLE hNameMappings;
        public string lpszProgressTitle;
    }
    public struct SHNAMEMAPPING
    {
        public string pszOldPath;
        public string pszNewPath;
        public int cchOldPath;
        public int cchNewPath;
    }
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public int fMask;
        public HWND hwnd;
        public string lpVerb;
        public string lpFile;
        public string lpParameters;
        public string lpDirectory;
        public int nShow;
        public HANDLE hInstApp;
        public int lpIDList;
        public string lpClass;
        public HANDLE hkeyClass;
        public int dwHotKey;
        public HANDLE hIcon;
        public HANDLE hProcess;
    }
    public struct NOTIFYICONDATA
    {
        public int cbSize;
        public HWND hwnd;
        public int uID;
        public int uFlags;
        public int uCallbackMessage;
        public HANDLE hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] public string szTip;
    }
    public struct SHFILEINFO
    {
        public HANDLE hIcon;
        public int iIcon;
        public int dwAttributes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Kernel.MAX_PATH)] public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
    }

    public abstract class Shell
    {
        [DllImport("shell32")] public static extern int CommandLineToArgv(string lpCmdLine, short pNumArgs);
        [DllImport("shell32")] public static extern int DoEnvironmentSubst(string szString, int cbString);
        [DllImport("shell32")] public static extern int DragQueryFile(HANDLE hDROP, int UINT, string lpStr, int ch);
        [DllImport("shell32")] public static extern int DragQueryPoint(HANDLE hDROP, ref POINT lpPoint);
        [DllImport("shell32")] public static extern int DuplicateIcon(HANDLE hInst, HANDLE hIcon);
        [DllImport("shell32")] public static extern int ExtractAssociatedIcon(HANDLE hInst, string lpIconPath, ref int lpiIcon);
        [DllImport("shell32")] public static extern int ExtractIcon(HANDLE hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("shell32")] public static extern int ExtractIconEx(string lpszFile, int nIconIndex, ref int phiconLarge, ref int phiconSmall, int nIcons);
        [DllImport("shell32")] public static extern int FindExecutable(string lpFile, string lpDirectory, string lpResult);
        [DllImport("shell32")] public static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);
        [DllImport("shell32")] public static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);
        [DllImport("shell32")] public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, int uFlags);
        [DllImport("shell32")] public static extern int SHGetNewLinkInfo(string pszLinkto, string pszDir, string pszName, ref int pfMustCopy, int uFlags);
        [DllImport("shell32")] public static extern int ShellAbout(HWND hwnd, string szApp, string szOtherStuff, HANDLE hIcon);
        [DllImport("shell32")] public static extern int ShellExecute(HWND hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
        [DllImport("shell32")] public static extern int Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);
        [DllImport("shell32")] public static extern void DragAcceptFiles(HWND hwnd, int fAccept);
        [DllImport("shell32")] public static extern void DragFinish(HANDLE hDrop);
        [DllImport("shell32")] public static extern void SHFreeNameMappings(HANDLE hNameMappings);
        [DllImport("shell32")] public static extern void WinExecError(HWND hwnd, int error, string lpstrFileName, string lpstrTitle);
        [DllImport("shell32")] public static extern int SHBrowseForFolder(BROWSEINFO lpbi);
        [DllImport("shell32")] public static extern int SHGetPathFromIDList(int pidList, string lpBuffer);

        public const int ABE_BOTTOM = 3;
        public const int ABE_LEFT = 0;
        public const int ABE_RIGHT = 2;
        public const int ABE_TOP = 1;
        public const int ABM_ACTIVATE = 0x6;
        public const int ABM_GETAUTOHIDEBAR = 0x7;
        public const int ABM_GETSTATE = 0x4;
        public const int ABM_GETTASKBARPOS = 0x5;
        public const int ABM_NEW = 0x0;
        public const int ABM_QUERYPOS = 0x2;
        public const int ABM_REMOVE = 0x1;
        public const int ABM_SETAUTOHIDEBAR = 0x8;
        public const int ABM_SETPOS = 0x3;
        public const int ABM_WINDOWPOSCHANGED = 0x9;
        public const int ABN_FULLSCREENAPP = 0x2;
        public const int ABN_POSCHANGED = 0x1;
        public const int ABN_STATECHANGE = 0x0;
        public const int ABN_WINDOWARRANGE = 0x3;
        public const int ABS_ALWAYSONTOP = 0x2;
        public const int ABS_AUTOHIDE = 0x1;
        public const int EIRESID = -1;
        public const int FOF_ALLOWUNDO = 0x40;
        public const int FOF_CONFIRMMOUSE = 0x2;
        public const int FOF_FILESONLY = 0x80;
        public const int FOF_MULTIDESTFILES = 0x1;
        public const int FOF_NOCONFIRMATION = 0x10;
        public const int FOF_NOCONFIRMMKDIR = 0x200;
        public const int FOF_RENAMEONCOLLISION = 0x8;
        public const int FOF_SILENT = 0x4;
        public const int FOF_SIMPLEPROGRESS = 0x100;
        public const int FOF_WANTMAPPINGHANDLE = 0x20;
        public const int FO_COPY = 0x2;
        public const int FO_DELETE = 0x3;
        public const int FO_MOVE = 0x1;
        public const int FO_RENAME = 0x4;
        public const int NIF_ICON = 0x2;
        public const int NIF_MESSAGE = 0x1;
        public const int NIF_TIP = 0x4;
        public const int NIM_ADD = 0x0;
        public const int NIM_DELETE = 0x2;
        public const int NIM_MODIFY = 0x1;
        public const int PO_DELETE = 0x13;
        public const int PO_PORTCHANGE = 0x20;
        public const int PO_RENAME = 0x14;
        public const int PO_REN_PORT = 0x34;
        public const int SEE_MASK_CLASSKEY = 0x3;
        public const int SEE_MASK_CLASSNAME = 0x1;
        public const int SEE_MASK_CONNECTNETDRV = 0x80;
        public const int SEE_MASK_DOENVSUBST = 0x200;
        public const int SEE_MASK_FLAG_DDEWAIT = 0x100;
        public const int SEE_MASK_FLAG_NO_UI = 0x400;
        public const int SEE_MASK_HOTKEY = 0x20;
        public const int SEE_MASK_ICON = 0x10;
        public const int SEE_MASK_IDLIST = 0x4;
        public const int SEE_MASK_INVOKEIDLIST = 0xC;
        public const int SEE_MASK_NOCLOSEPROCESS = 0x40;
        public const int SE_ERR_ACCESSDENIED = 5;
        public const int SE_ERR_ASSOCINCOMPLETE = 27;
        public const int SE_ERR_DDEBUSY = 30;
        public const int SE_ERR_DDEFAIL = 29;
        public const int SE_ERR_DDETIMEOUT = 28;
        public const int SE_ERR_DLLNOTFOUND = 32;
        public const int SE_ERR_FNF = 2;
        public const int SE_ERR_NOASSOC = 31;
        public const int SE_ERR_OOM = 8;
        public const int SE_ERR_PNF = 3;
        public const int SE_ERR_SHARE = 26;
        public const int SHGFI_ATTRIBUTES = 0x800;
        public const int SHGFI_DISPLAYNAME = 0x200;
        public const int SHGFI_EXETYPE = 0x2000;
        public const int SHGFI_ICON = 0x100;
        public const int SHGFI_ICONLOCATION = 0x1000;
        public const int SHGFI_LARGEICON = 0x0;
        public const int SHGFI_LINKOVERLAY = 0x8000;
        public const int SHGFI_OPENICON = 0x2;
        public const int SHGFI_PIDL = 0x8;
        public const int SHGFI_SELECTED = 0x10000;
        public const int SHGFI_SHELLICONSIZE = 0x4;
        public const int SHGFI_SMALLICON = 0x1;
        public const int SHGFI_SYSICONINDEX = 0x4000;
        public const int SHGFI_TYPENAME = 0x400;
        public const int SHGFI_USEFILEATTRIBUTES = 0x10;
        public const int SHGNLI_PIDL = 0x1;
        public const int SHGNLI_PREFIXNAME = 0x2;
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}