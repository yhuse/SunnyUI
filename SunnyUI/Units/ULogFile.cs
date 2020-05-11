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
 * 文件名称: ULogFile.cs
 * 文件说明: 日志文件记录类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace Sunny.UI
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 所有
        /// </summary>
        [DisplayText("ALL")]
        ALL = -2147483648,

        /// <summary>
        /// 调试
        /// </summary>
        [DisplayText("DEBUG")]
        DEBUG = 30000,

        /// <summary>
        /// 信息
        /// </summary>
        [DisplayText("INFO")]
        INFO = 40000,

        /// <summary>
        /// 警告
        /// </summary>
        [DisplayText("WARN")]
        WARN = 60000,

        /// <summary>
        /// 错误
        /// </summary>
        [DisplayText("ERROR")]
        ERROR = 70000,

        /// <summary>
        /// 致命错误
        /// </summary>
        [DisplayText("FATAL")]
        FATAL = 110000,

        /// <summary>
        /// 关闭
        /// </summary>
        [DisplayText("OFF")]
        OFF = 2147483647
    }

    /// <summary>
    /// 日志文件记录类
    /// </summary>
    public class LogFile : IDisposable
    {
        private readonly StringBuilder sb = new StringBuilder();

        private BackgroundWorker saver;

        private Timer timer;

        private readonly ConcurrentQueue<LogInfo> Queue = new ConcurrentQueue<LogInfo>();

        private LogInfo NoWriteLog;

        private bool needLog = true;

        /// <summary>
        /// 日子级别
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        public bool LogDebugInfo { get; set; }

        /// <summary>
        /// 使用多线程保存
        /// </summary>
        public bool UseThread { get; set; }

        /// <summary>
        /// 使用定时器保存
        /// </summary>
        public bool UseTimer { get; set; }

        /// <summary>
        /// 日志时间格式
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// 获取或设置日志文件的名称
        /// </summary>
        public string FileNamePrefix { get; set; }

        /// <summary>
        /// 获取或设置日志文件的路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 获取或设置定义日志文件大小
        /// </summary>
        public int MaxFileSize { get; set; }

        /// <summary>
        /// 默认构造函数,初始化日志文件大小[2M]
        /// </summary>
        public LogFile()
            : this("Log", string.Empty)
        {
        }

        /// <summary>
        /// 一个参数构造方法。参数用以初始化日志文件（部分）名称
        /// </summary>
        /// <param name="name">日志文件（部分）名称</param>
        public LogFile(string name)
            : this(name, string.Empty)
        {
        }

        /// <summary>
        /// 两个参数构造方法。分别初始化日志文件（部分）名称和日志路径
        /// </summary>
        /// <param name="name">日志文件（部分）名称</param>
        /// <param name="direct">日志文件相对路径</param>
        public LogFile(string name, string direct)
        {
            Level = LogLevel.ALL;
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

            MaxFileSize = 1048576;
            FileNamePrefix = name;
            FilePath = string.Concat(CurrentDir(), "Logs\\");
            if (!direct.IsNullOrEmpty())
            {
                FilePath = string.Concat(FilePath, direct, "\\");
            }
        }

        /// <summary>
        /// 启动定时器
        /// </summary>
        /// <param name="interval"></param>
        public void StartTimer(int interval = 200)
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Tick += timer_Tick;
            }

            timer.Interval = 200;
            UseThread = false;
            UseTimer = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            WriteLog();
            timer.Start();
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void StopTimer()
        {
            UseTimer = false;
            timer?.Stop();
        }

        /// <summary>
        /// 开启线程
        /// </summary>
        public void StartThread()
        {
            if (saver == null)
            {
                saver = new BackgroundWorker();
                saver.DoWork += Saver_DoWork;
            }

            saver.WorkerSupportsCancellation = true;
            UseThread = true;
            UseTimer = false;

            if (!saver.IsBusy)
            {
                saver.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void StopThread()
        {
            needLog = false;
        }

        private static string CurrentDir()
        {
            return DealPath(AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// 处理文件夹名称末尾加反斜杠\
        /// </summary>
        /// <param name="path">文件夹名称</param>
        /// <returns>结果</returns>
        private static string DealPath(string path)
        {
            return path.Right(1) == "\\" ? path : path + "\\";
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                StopTimer();
                StopThread();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static String StackFrameLocationInfo()
        {
            StackTrace st = new StackTrace(1, true);
            //GetFrame(2), 参数：0 当前行数；1 上级函数；2：上上级函数，依次类推
            StackFrame sf = st.GetFrame(2);
            if (sf == null)
            {
                return "UnknownClass";
            }

            string className = Path.GetFileNameWithoutExtension(sf.GetFileName());
            return $@"{className},{sf.GetMethod().Name},L{sf.GetFileLineNumber()}";
        }

        private static string YearMonthFolder(DateTime dt, string path, bool createIfNotExist = false)
        {
            if (path.IsNullOrEmpty())
            {
                return path;
            }

            string result = DealPath(path) + dt.Year.ToString("D4") + dt.Month.ToString("D2") + "\\";
            if (createIfNotExist)
            {
                CreateDir(result);
            }

            return result;
        }

        private static void CreateDir(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private string GetLogFileName(LogInfo log)
        {
            string filepath = YearMonthFolder(log.Time, FilePath, true);
            int i = 1;
            string fileName = string.Concat(filepath, log.Time.ToString("yyyyMMdd"), ".", i, ".", FileNamePrefix, ".log");
            FileInfo infile = new FileInfo(fileName);
            while (true)
            {
                if (!infile.Exists || infile.Length < MaxFileSize)
                {
                    break;
                }

                i++;
                fileName = string.Concat(filepath, log.Time.ToString("yyyyMMdd"), ".", i, ".", FileNamePrefix, ".log");
                infile = new FileInfo(fileName);
            }

            return fileName;
        }

        private void ReleaseUnmanagedResources()
        {
            saver.CancelAsync();
        }

        private void Saver_DoWork(object sender, DoWorkEventArgs e)
        {
            while (needLog)
            {
                if (saver.CancellationPending)
                {
                    break;
                }

                WriteLog();
                Thread.Sleep(100);
            }

            e.Cancel = true;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">信息</param>
        public void Debug(params string[] message)
        {
            AddLog(LogLevel.DEBUG, message);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">信息</param>
        public void Info(params string[] message)
        {
            AddLog(LogLevel.INFO, message);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">信息</param>
        public void Warn(params string[] message)
        {
            AddLog(LogLevel.WARN, message);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">信息</param>
        public void Error(params string[] message)
        {
            AddLog(LogLevel.ERROR, message);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">信息</param>
        public void Fatal(params string[] message)
        {
            AddLog(LogLevel.FATAL, message);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="e">错误信息</param>
        public void WarnException(Exception e)
        {
            AddLog(LogLevel.WARN, e.Message.Trim(), e.StackTrace.Trim());
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="e">错误信息</param>
        public void ErrorException(Exception e)
        {
            AddLog(LogLevel.ERROR, e.Message.Trim(), e.StackTrace.Trim());
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="e">错误信息</param>
        public void FatalException(Exception e)
        {
            AddLog(LogLevel.FATAL, e.Message.Trim(), e.StackTrace.Trim());
        }

        private void AddLog(LogLevel level, params string[] message)
        {
            if (level < Level)
            {
                return;
            }

            LogInfo info = new LogInfo();
            if (LogDebugInfo || level >= LogLevel.WARN)
            {
                info.DebugInfo = StackFrameLocationInfo();
            }

            info.Level = level;
            info.Time = DateTime.Now;
            info.Message = string.Join(",", message);

            if (UseThread)
            {
                Queue.Enqueue(info);
            }
            else
            {
                WriteAtOnce(info);
            }
        }

        private void WriteAtOnce(LogInfo log)
        {
            sb.Clear();
            AddLog(log);

            bool isAdd = false;
            while (!isAdd)
            {
                try
                {
                    string filename = GetLogFileName(log);
                    StreamWriter sw = File.AppendText(filename);
                    sw.WriteLine(sb.ToString());
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    isAdd = true;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void AddLog(LogInfo log)
        {
            sb.Append(log.Time.ToString(DateTimeFormat));
            sb.Append(",");

            sb.Append(log.Level.DisplayText());
            sb.Append(",");

            sb.Append(log.DebugInfo);
            sb.Append(",");

            sb.Append(log.Message);
        }

        private void WriteLog()
        {
            if (NoWriteLog != null || Queue.Count != 0)
            {
                sb.Clear();

                LogInfo log = null;
                if (NoWriteLog == null)
                {
                    while (log == null)
                    {
                        Queue.TryDequeue(out log);
                    }
                }
                else
                {
                    log = NoWriteLog;
                    NoWriteLog = null;
                }

                try
                {
                    DateTime dt = log.Time;
                    string filename = GetLogFileName(log);
                    AddLog(log);

                    while (Queue.Count > 0)
                    {
                        if (Queue.TryDequeue(out log))
                        {
                            if (log.Time.Date == dt.Date)
                            {
                                sb.Append("\r\n");
                                AddLog(log);
                            }
                            else
                            {
                                NoWriteLog = log;
                                break;
                            }
                        }
                    }

                    //                    using (StreamWriter sw = File.AppendText(filename))
                    //                    {
                    //                        sw.WriteLine(sb.ToString());
                    //                        sw.Flush();
                    //                    }

                    StreamWriter sw = File.AppendText(filename);
                    sw.WriteLine(sb.ToString());
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private class LogInfo
        {
            public LogLevel Level { get; set; }
            public DateTime Time { get; set; }
            public string Message { get; set; }
            public string DebugInfo { get; set; }
        }
    }
}