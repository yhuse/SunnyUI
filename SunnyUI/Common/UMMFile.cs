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
 * 文件名称: UMMFile.cs
 * 文件说明: 多进程通信类
 * 当前版本: V3.1
 * 创建日期: 2021-09-05
 *
 * 2021-09-05: V3.0.6 增加文件说明
 ******************************************************************************
 * 用法：
 * 1、分别在不同的进程Demo1、Demo2中创建通信类
 * 在Demo1里创建通信类mmfile1：
   var mmfile1 = new MMFile("Demo1");
   mmfile1.Start();
   mmfile1.OnMessage += Mmfile1_OnMessage;
   
 * 在Demo2里创建通信类mmfile2：
   var mmfile2 = new MMFile("Demo2");
   mmfile2.Start();
   mmfile2.OnMessage += Mmfile2_OnMessage;
   
 * 2、发送消息
 * Demo1发送一条消息给Demo2：
   mmfile1.Write("Demo2", "Hello world.");
 
 * 3、接收消息
 * Demo2的接收消息事件里处理消息，注意，该消息与界面交互需用Invoke
   private void Mmfile2_OnMessage(object sender, MMFileEventArgs e)
   {
       AddMessage(e);
   }

   private void AddMessage(MMFileEventArgs e)
   {
       if (listBox1.InvokeRequired)
       {
           listBox1.Invoke(new Action<MMFileEventArgs>(AddMessage), e);
       }
       else
       {
           listBox1.Items.Add(e.Source + "," + e.Value);
       }
   }

 * 4、关闭及销毁通信类
 * mmfile1.Stop();
   mmfile1.Dispose();
 * mmfile2.Stop();
   mmfile2.Dispose();
******************************************************************************/

using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace Sunny.UI
{
    /// <summary>
    /// 多进程通信类
    /// </summary>
    public sealed class MMFile : BackgroundWorkerEx, IDisposable
    {
        /// <summary>
        /// 内存文件名
        /// </summary>
        public string MapName { get; }

        /// <summary>
        /// 大小
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapName">内存文件名</param>
        /// <param name="capacity">大小</param>
        /// <exception cref="Exception">报错消息</exception>
        public MMFile(string mapName, int capacity = 4096)
        {
            if (!FileEx.IsValidFileName(mapName))
            {
                throw new Exception("MapName is not valid.");
            }

            MapName = mapName;
            WorkerDelay = 10;
            Capacity = Math.Max(4096, capacity);

            var mmf = MemoryMappedFile.CreateOrOpen(MapName, Capacity, MemoryMappedFileAccess.ReadWrite);
            using (var accessor = mmf.CreateViewAccessor(0, Capacity))
            {
                var value = accessor.ReadBoolean(0);
                if (!value) accessor.Write(0, false);
            }
        }

        /// <summary>
        /// 消息回调
        /// </summary>
        public event MMFileEventHandler OnMessage;

        /// <summary>
        /// 线程运行
        /// </summary>
        protected override void DoWorker()
        {
            try
            {
                if (ExistsValue())
                {
                    var message = Read();
                    OnMessage?.Invoke(this, new MMFileEventArgs(message.Source, message.Value));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 给目标内存文件写字符串
        /// </summary>
        /// <param name="dest">目标内存文件名</param>
        /// <param name="message">消息字符串</param>
        public void Write(string dest, string message)
        {
            var mmf = MemoryMappedFile.CreateOrOpen(dest, Capacity, MemoryMappedFileAccess.ReadWrite);
            using (var accessor = mmf.CreateViewAccessor(0, Capacity))
            {
                if (accessor.ReadBoolean(0))
                {
                    Thread.Sleep(10);
                }

                var data = Encoding.Unicode.GetBytes(MapName);
                accessor.Write(128, data.Length);
                accessor.WriteArray(128 + 4, data, 0, data.Length);

                data = Encoding.Unicode.GetBytes(message);
                accessor.Write(1024, data.Length);
                accessor.WriteArray(1024 + 4, data, 0, data.Length);

                accessor.Write(0, true);
            }
        }

        private Message Read()
        {
            Message message = new Message();
            var mmf = MemoryMappedFile.CreateOrOpen(MapName, Capacity, MemoryMappedFileAccess.ReadWrite);
            using (var accessor = mmf.CreateViewAccessor(0, Capacity))
            {
                var len = accessor.ReadInt32(128);
                var data = new byte[len];
                accessor.ReadArray(128 + 4, data, 0, len);
                message.Source = Encoding.Unicode.GetString(data);

                len = accessor.ReadInt32(1024);
                data = new byte[len];
                accessor.ReadArray(1024 + 4, data, 0, len);
                message.Value = Encoding.Unicode.GetString(data);

                accessor.Write(0, false);
                return message;
            }
        }

        private bool ExistsValue()
        {
            var mmf = MemoryMappedFile.CreateOrOpen(MapName, Capacity, MemoryMappedFileAccess.ReadWrite);
            using (var accessor = mmf.CreateViewAccessor(0, Capacity))
            {
                return accessor.ReadBoolean(0);
            }
        }

        internal struct Message
        {
            public string Source { get; set; }
            public string Value { get; set; }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            Stop();
            try
            {
                var mmf = MemoryMappedFile.OpenExisting(MapName);
                mmf.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~MMFile()
        {
            Dispose();
        }
    }

    /// <summary>
    /// 回调事件定义
    /// </summary>
    /// <param name="sender">对象</param>
    /// <param name="e">事件</param>
    public delegate void MMFileEventHandler(object sender, MMFileEventArgs e);

    /// <summary>
    /// 内存文件事件
    /// </summary>
    public class MMFileEventArgs : EventArgs
    {
        /// <summary>
        /// 消息来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 消息字符串
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="source">消息来源</param>
        /// <param name="value">消息字符串</param>
        public MMFileEventArgs(string source, string value)
        {
            Source = source;
            Value = value;
        }
    }
}