/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 当前版本: V3.0
 * 创建日期: 2021-09-05
 *
 * 2021-09-05: V3.0.6 增加文件说明
******************************************************************************/

using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace Sunny.UI
{
    public sealed class MMFile : BackgroundWorkerEx, IDisposable
    {
        public string MapName { get; }
        public int Capacity { get; }

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

        public event MMFileEventHandler OnMessage;

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
                accessor.Write(0, false);
                var len = accessor.ReadInt32(128);
                var data = new byte[len];
                accessor.ReadArray(128 + 4, data, 0, len);
                message.Source = Encoding.Unicode.GetString(data);

                len = accessor.ReadInt32(1024);
                data = new byte[len];
                accessor.ReadArray(1024 + 4, data, 0, len);
                message.Value = Encoding.Unicode.GetString(data);
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

        ~MMFile()
        {
            Dispose();
        }
    }

    public delegate void MMFileEventHandler(object sender, MMFileEventArgs e);
    public class MMFileEventArgs : EventArgs
    {
        public string Source { get; set; }
        public string Value { get; set; }

        public MMFileEventArgs(string source, string value)
        {
            Source = source;
            Value = value;
        }
    }
}