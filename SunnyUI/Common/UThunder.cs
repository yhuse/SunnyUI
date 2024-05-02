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
 * 文件名称: UThunder.cs
 * 文件说明: 迅雷下载帮助类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable 1591

namespace Sunny.UI
{
    public class Thunder : IDisposable
    {
        private readonly Timer timer;
        private bool Exit;

        public delegate void OnTaskInfoChange(object sender, ThunderTask task);

        public readonly ConcurrentDictionary<string, ThunderTask> Tasks = new ConcurrentDictionary<string, ThunderTask>();

        public event OnTaskInfoChange TaskInfoChange;

        public event OnTaskInfoChange TaskComplete;

        public bool InitSuccess { get; }

        public bool AutoManage { get; set; }

        public int MaxTaskCount { get; set; } = 10;

        public Thunder()
        {
            InitSuccess = ThunderDll.Init();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        ~Thunder()
        {
            timer?.Stop();
            timer?.Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            foreach (var task in Tasks.Values)
            {
                if (!task.IsStart || task.IsStop || task.IsComplete)
                {
                    continue;
                }

                ThunderDll.QueryTaskInfo(task.ID, task.Info);

                if (task.Info.Status == ThunderDll.DOWN_TASK_STATUS.TSC_STOPENDING)
                {
                    task.IsStop = true;
                }

                if (task.Info.Status == ThunderDll.DOWN_TASK_STATUS.TSC_COMPLETE)
                {
                    TaskComplete?.Invoke(this, task);
                    task.IsComplete = true;
                }

                TaskInfoChange?.Invoke(this, task);
            }

            if (AutoManage)
            {
                int cnt = RunTaskCount();
                if (cnt < MaxTaskCount)
                {
                    foreach (var task in Tasks.Values)
                    {
                        if (!task.IsStart)
                        {
                            task.StartTask();
                            cnt++;
                        }

                        if (cnt >= MaxTaskCount)
                        {
                            break;
                        }
                    }
                }

                DeleteCompleted();
            }

            if (!Exit)
            {
                timer.Start();
            }
        }

        public int RunTaskCount()
        {
            int cnt = 0;
            foreach (var task in Tasks.Values)
            {
                if (task.IsStart && !task.IsComplete)
                {
                    cnt++;
                }
            }

            return cnt;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            timer.Stop();
            Exit = true;
            ThunderDll.Final();
        }

        public ThunderTask CreateTask(string url, string savefile)
        {
            if (Tasks.ContainsKey(url))
            {
                return Tasks[url];
            }

            ThunderTask task = new ThunderTask(url, savefile);
            task.ID = ThunderDll.CreateTask(task.Param);
            Tasks.TryAdd(url, task);
            return task;
        }

        public bool DeleteTask(ThunderTask task, bool deletefile = false)
        {
            bool result = ThunderDll.DeleteTask(task.ID);
            if (deletefile)
            {
                ThunderDll.DelTempFile(task.Param);
            }

            Tasks.TryRemove(task.URL, out _);
            return result;
        }

        public void DeleteCompleted()
        {
            List<string> urls = new List<string>();
            foreach (var task in Tasks.Values)
            {
                if (task.IsComplete)
                {
                    urls.Add(task.URL);
                }
            }

            foreach (var url in urls)
            {
                Tasks.TryRemove(url, out _);
            }
        }
    }

    public class ThunderTask
    {
        public IntPtr ID { get; set; }

        public ThunderDll.DownTaskInfo Info { get; set; }

        public ThunderDll.DownTaskParam Param { get; set; }

        public bool IsStart { get; set; }

        public bool IsStop { get; set; }

        public bool IsComplete { get; set; }

        public string URL { get; private set; }

        public string SaveFile { get; private set; }

        public ThunderTask(string url, string savefile)
        {
            URL = url;
            SaveFile = savefile;
            Param = ThunderDll.CreateDownTaskParam(url, savefile);
            Info = new ThunderDll.DownTaskInfo();
        }
    }

    public static class ThunderDll
    {
        public static bool StartTask(this ThunderTask task)
        {
            bool result = StartTask(task.ID);
            task.IsStart = result;
            task.IsStop = false;
            return result;
        }

        public static bool StopTask(this ThunderTask task)
        {
            return StopTask(task.ID);
        }

        public static bool Init()
        {
            return XL_Init();
        }

        public static bool Final()
        {
            return XL_UnInit();
        }

        public static IntPtr CreateTask(string url, string savefile)
        {
            return XL_CreateTask(CreateDownTaskParam(url, savefile));
        }

        public static IntPtr CreateTask(DownTaskParam param)
        {
            return XL_CreateTask(param);
        }

        public static DownTaskParam CreateDownTaskParam(string url, string savefile)
        {
            return new DownTaskParam
            {
                szTaskUrl = url,
                szFilename = Path.GetFileName(savefile),
                szSavePath = Path.GetDirectoryName(savefile)
            };
        }

        public static bool StartTask(IntPtr task)
        {
            return XL_StartTask(task);
        }

        public static bool StopTask(IntPtr task)
        {
            return XL_StopTask(task);
        }

        public static object SetSpeedLimit(int KBps)
        {
            return XL_SetSpeedLimit(KBps);
        }

        public static object DelTempFile(DownTaskParam param)
        {
            return XL_DelTempFile(param);
        }

        public static bool DeleteTask(IntPtr task)
        {
            return XL_DeleteTask(task);
        }

        public static bool GetFileSize(string url, long filesize)
        {
            return XL_GetFileSizeWithUrl(url, filesize);
        }

        public static bool QueryTaskInfo(IntPtr task, DownTaskInfo info)
        {
            return XL_QueryTaskInfoEx(task, info);
        }

        public static DownTaskInfo QueryTaskInfo(IntPtr task)
        {
            DownTaskInfo info = new DownTaskInfo();
            return XL_QueryTaskInfoEx(task, info) ? info : null;
        }

        public static IntPtr CreateBTTask(DownBTTaskParam param)
        {
            return XL_CreateBTTask(param);
        }

        public static long QueryBTFileInfo(IntPtr task, UIntPtr fileindex, ulong filesize, ulong completesize, UIntPtr status)
        {
            return XL_QueryBTFileInfo(task, fileindex, filesize, completesize, status);
        }

        public static BTTaskInfo QueryBTFileInfo(IntPtr task)
        {
            BTTaskInfo info = new BTTaskInfo();
            XL_QueryBTFileInfo(task, info);
            return info;
        }

        public static long QueryBTFileInfo(IntPtr task, BTTaskInfo info)
        {
            return XL_QueryBTFileInfo(task, info);
        }

        [DllImport("xldl.dll", CharSet = CharSet.Unicode)]
        private static extern bool XL_Init();

        [DllImport("xldl.dll", CharSet = CharSet.Unicode)]
        private static extern bool XL_UnInit();

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr XL_CreateTask([In] DownTaskParam stParam);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_StartTask(IntPtr hTask);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_StopTask(IntPtr hTask);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern object XL_SetSpeedLimit(int nKBps);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int XL_CreateTaskByThunder(string pszUrl, string pszFileName, string pszReferUrl, string pszCharSet, string pszCookie);

        //LONG XL_CreateTaskByThunder(wchar_t *pszUrl, wchar_t *pszFileName, wchar_t *pszReferUrl, wchar_t *pszCharSet, wchar_t *pszCookie)
        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern object XL_SetUploadSpeedLimit(int nTcpKBps, int nOtherKBps);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern object XL_SetProxy(DOWN_PROXY_INFO stProxyInfo);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern object XL_DelTempFile(DownTaskParam stParam);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_DeleteTask(IntPtr hTask);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_GetBtDataFileList(string szFilePath, string szSeedFileFullPath);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_SetUserAgent(string pszUserAgent);

        [DllImport("xldl.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool XL_GetFileSizeWithUrl(string lpURL, long iFileSize);

        [DllImport("xldl.dll", EntryPoint = "XL_QueryTaskInfoEx", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool XL_QueryTaskInfoEx(IntPtr hTask, [Out] DownTaskInfo stTaskInfo);

        [DllImport("xldl.dll", EntryPoint = "XL_QueryTaskInfoEx", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr XL_CreateBTTask(DownBTTaskParam stParam);

        [DllImport("xldl.dll", EntryPoint = "XL_QueryTaskInfoEx", CallingConvention = CallingConvention.Cdecl)]
        private static extern long XL_QueryBTFileInfo(IntPtr hTask, UIntPtr dwFileIndex, ulong ullFileSize, ulong ullCompleteSize, UIntPtr dwStatus);

        [DllImport("xldl.dll", EntryPoint = "XL_QueryTaskInfoEx", CallingConvention = CallingConvention.Cdecl)]
        private static extern long XL_QueryBTFileInfo(IntPtr hTask, BTTaskInfo pTaskInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        public class DownTaskParam
        {
            public int nReserved;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2084)]
            public string szTaskUrl;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2084)]
            public string szRefUrl;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4096)]
            public string szCookies;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szFilename;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szReserved0;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szSavePath;

            public IntPtr hReserved;

            public int bReserved = 0;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szReserved2;

            public int IsOnlyOriginal = 0;

            public uint nReserved1 = 5;

            public int DisableAutoRename = 0;

            public int IsResume = 1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048, ArraySubType = UnmanagedType.U4)]
            public uint[] reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BTTaskInfo
        {
            public int lTaskStatus;

            public uint dwUsingResCount;

            public uint dwSumResCount;

            public ulong ullRecvBytes;

            public ulong ullSendBytes;

            [MarshalAs(UnmanagedType.Bool)]
            public bool bFileCreated;

            public uint dwSeedCount;

            public uint dwConnectedBTPeerCount;

            public uint dwAllBTPeerCount;

            public uint dwHealthyGrade;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DownTaskInfo
        {
            public DOWN_TASK_STATUS Status;

            public TASK_ERROR_TYPE FailCode;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string Filename;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string ReservedString;

            public long TotalSize;

            public long TotalDownload;

            public float Percent;

            public int Reserved0;

            public int SrcTotal;

            public int SrcUsing;

            public int Reserved1;

            public int Reserved2;

            public int Reserved3;

            public int Reserved4;

            public long Reserved5;

            public long DonationP2P;

            public long Reserved6;

            public long DonationOrgin;

            public long DonationP2S;

            public long Reserved7;

            public long Reserved8;

            public int Speed;

            public int SpeedP2S;

            public int SpeedP2P;

            public bool IsOriginUsable;

            public float HashPercent;

            public int IsCreatingFile;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.U4)]
            public uint[] Reserved;

            public override string ToString()
            {
                return Status.DisplayText() + "，已完成：" + (Percent * 100.0f).ToString("F2") + "%，速度：" + Speed + "KB/s";
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public enum DOWN_TASK_STATUS
        {
            /// <summary>
            /// 无
            /// </summary>
            [DisplayText("无")]
            NOITEM = 0,

            /// <summary>
            /// 错误
            /// </summary>
            [DisplayText("错误")]
            TSC_ERROR,

            /// <summary>
            /// 暂停
            /// </summary>
            [DisplayText("暂停")]
            TSC_PAUSE,

            /// <summary>
            /// 下载
            /// </summary>
            [DisplayText("下载中")]
            TSC_DOWNLOAD,

            /// <summary>
            /// 完成
            /// </summary>
            [DisplayText("完成")]
            TSC_COMPLETE,

            /// <summary>
            /// 停止开始
            /// </summary>
            [DisplayText("已开始")]
            TSC_STARTENDING,

            /// <summary>
            /// 停止完成
            /// </summary>
            [DisplayText("已停止")]
            TSC_STOPENDING
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public enum TASK_ERROR_TYPE
        {
            TASK_ERROR_UNKNOWN = 0,

            TASK_ERROR_DISK_CREATE = 1,

            TASK_ERROR_DISK_WRITE = 2,

            TASK_ERROR_DISK_READ = 3,

            TASK_ERROR_DISK_RENAME = 4,

            TASK_ERROR_DISK_PIECEHASH = 5,

            TASK_ERROR_DISK_FILEHASH = 6,

            TASK_ERROR_DISK_DELETE = 7,

            TASK_ERROR_DOWN_INVALID = 16,

            TASK_ERROR_PROXY_AUTH_TYPE_UNKOWN = 32,

            TASK_ERROR_PROXY_AUTH_TYPE_FAILED = 33,

            TASK_ERROR_HTTPMGR_NOT_IP = 48,

            TASK_ERROR_TIMEOUT = 64,

            TASK_ERROR_CANCEL = 65,

            TASK_ERROR_TP_CRASHED = 66,

            TASK_ERROR_ID_INVALID = 67
        }

        /// <summary>
        /// 代理类型
        /// </summary>
        public enum DOWN_PROXY_TYPE
        {
            PROXY_TYPE_IE = 0,

            PROXY_TYPE_HTTP = 1,

            PROXY_TYPE_SOCK4 = 2,

            PROXY_TYPE_SOCK5 = 3,

            PROXY_TYPE_FTP = 4,

            PROXY_TYPE_UNKOWN = 255
        }

        /// <summary>
        /// 代理模式
        /// </summary>
        public enum DOWN_PROXY_AUTH_TYPE
        {
            PROXY_AUTH_NONE = 0,

            PROXY_AUTH_AUTO,

            PROXY_AUTH_BASE64,

            PROXY_AUTH_NTLM,

            PROXY_AUTH_DEGEST,

            PROXY_AUTH_UNKOWN
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class DOWN_PROXY_INFO
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool bIEProxy;

            [MarshalAs(UnmanagedType.Bool)]
            public bool bProxy;

            public DOWN_PROXY_TYPE stPType;

            public DOWN_PROXY_AUTH_TYPE stAType;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
            public string szHost;

            public int nPort;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string szUser;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string szPwd;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
            public string szDomain;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TrackerInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string szTrackerUrl;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class DownBTTaskParam
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szSeedFullPath;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szFilePath;

            public uint dwNeedDownloadFileCount;

            public IntPtr dwNeedDownloadFileIndexArray;

            public uint dwTrackerInfoCount;

            public IntPtr pTrackerInfoArray;

            public int IsResume;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class tracker_info
        {
            public uint tracker_url_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string tracker_url;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class bt_file_info
        {
            public ulong file_size;

            public uint path_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string file_path;

            public uint name_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string file_name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class bt_seed_file_info
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string info_id;

            public uint title_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string title;

            public uint file_info_count;

            public IntPtr file_info_array;

            public uint tracker_count;

            public IntPtr tracker_info_array;

            public uint publisher_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8192)]
            public string publisher;

            public uint publisher_url_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string publisher_url;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class bt_data_file_item
        {
            public uint path_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string file_path;

            public uint name_len;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string file_name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class bt_data_file_list
        {
            public uint item_count;

            public IntPtr item_array;
        }
    }
}