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
 * 文件名称: UUdp.cs
 * 文件说明: UDP 通讯类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Sunny.UI
{
    /// <summary>
    /// UDP 数据接收类
    /// </summary>
    public class UdpReceiver : BackgroundWorkerEx
    {
        private UdpClient _socket;
        private IPEndPoint remoteHost;

        /// <summary>
        /// Udp端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 最后一次接收到的数据
        /// </summary>
        public byte[] LastBytes;

        /// <summary>
        /// 数据长度，为0时不判断长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        /// 接收的数据队列
        /// </summary>
        public ConcurrentQueue<byte[]> Datas = new ConcurrentQueue<byte[]>();

        /// <summary>
        /// UDP是否连接成功
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// 开始前调用
        /// </summary>
        protected override void InitStart()
        {
            try
            {
                _socket = new UdpClient(Port) { Client = { ReceiveBufferSize = 1024 * 1024 } };
                remoteHost = new IPEndPoint(IPAddress.Any, 0);
                Connected = true;
            }
            catch
            {
                _socket = null;
                Connected = false;
            }

            WorkerDelay = 0;
        }

        /// <summary>
        /// 停止前调用
        /// </summary>
        protected override void BeforeStop()
        {
            if (Connected)
            {
                Connected = false;
                Thread.Sleep(100);
                _socket.Close();
                _socket = null;
            }

            Clear();
        }

        /// <summary>
        /// 停止后调用
        /// </summary>
        protected override void FinalStop()
        {
        }

        /// <summary>
        /// 完成后调用
        /// </summary>
        protected override void DoCompleted()
        {
        }

        /// <summary>
        /// 线程执行代码
        /// </summary>
        protected override void DoWorker()
        {
            try
            {
                if (!Connected || _socket == null)
                {
                    return;
                }

                if (_socket.Available <= 0)
                {
                    return;
                }

                if (_socket.Client == null)
                {
                    return;
                }

                byte[] receiveBytes = _socket.Receive(ref remoteHost);

                if (DataLength == 0 || receiveBytes.Length == DataLength)
                {
                    Datas.Enqueue(receiveBytes);
                }

                LastBytes = (byte[])receiveBytes.Clone();
            }
            catch (Exception y)
            {
                Console.WriteLine(y.Message);
            }
        }

        /// <summary>
        /// 接收记录条数
        /// </summary>
        public int Count => Datas.Count;

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            while (Datas.Count > 0)
            {
                Datas.TryDequeue(out _);
            }
        }
    }

    /// <summary>
    /// UDP 数据发送类
    /// </summary>
    public class UdpSender : BackgroundWorkerEx
    {
        private readonly ConcurrentQueue<byte[]> _queue = new ConcurrentQueue<byte[]>();

        /// <summary>
        /// 发送至此IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 发送至此端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 开始前调用
        /// </summary>
        protected override void InitStart()
        {
            WorkerDelay = 10;
            Clear();
        }

        /// <summary>
        /// 停止后调用
        /// </summary>
        protected override void FinalStop()
        {
            Clear();
        }

        /// <summary>
        /// 停止前调用
        /// </summary>
        protected override void BeforeStop()
        {
        }

        /// <summary>
        /// 待发数据条数
        /// </summary>
        public int Count => _queue.Count;

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            while (_queue.Count > 0)
            {
                _queue.TryDequeue(out _);
            }
        }

        /// <summary>
        /// 线程执行代码
        /// </summary>
        protected override void DoWorker()
        {
            while (_queue.Count > 0)
            {
                if (!_queue.TryDequeue(out var data))
                {
                    continue;
                }

                SendData(data);
            }
        }

        /// <summary>
        /// 结束后调用
        /// </summary>
        protected override void DoCompleted()
        {
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="Data">数据</param>
        public void SendData(byte[] Data)
        {
            try
            {
                UdpClient uc = new UdpClient(IpAddress, Port);
                uc.Send(Data, Data.Length);
                uc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="datas">数据</param>
        public void AddData(byte[] datas)
        {
            if (!IsStart)
            {
                return;
            }

            _queue.Enqueue(datas);
        }
    }
}