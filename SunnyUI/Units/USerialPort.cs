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
 * 文件名称: USerialPort.cs
 * 文件说明: 串口扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.IO.Ports;
using System.Threading;

namespace Sunny.UI
{
    /// <summary>
    /// 串口数据类型
    /// </summary>
    public enum SerialPortDataType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String,

        /// <summary>
        /// 数组
        /// </summary>
        Bytes
    }

    /// <summary>
    /// 串口扩展类
    /// </summary>
    public abstract class SerialPortEx
    {
        private SerialPort _comm;

        /// <summary>
        /// 串口数据类型
        /// </summary>
        public SerialPortDataType DataType { get; protected set; }

        /// <summary>
        /// 最后需要处理的字符串
        /// </summary>
        public string LastString { get; protected set; }

        /// <summary>
        /// 串口
        /// </summary>
        public SerialPort Com => _comm;

        /// <summary>
        /// 最后需要处理的数组
        /// </summary>
        public byte[] LastBytes;

        /// <summary>
        /// 数组转换为字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public string ToHexString(byte[] value)
        {
            return value.ToHexString();
        }

        /// <summary>
        /// 字符串转换为数组
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public byte[] ToBytes(string value)
        {
            return value.ToHexBytes();
        }

        /// <summary>
        /// 串口是否打开
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void CloseCom()
        {
            if (!IsOpen)
            {
                return;
            }

            _comm.Close();
            _comm.Dispose();
            IsOpen = false;
        }

        /// <summary>
        /// 获取所有的串口名称
        /// </summary>
        public string[] AllPortNames
        {
            get { return SerialPort.GetPortNames(); }
        }

        /// <summary>
        /// 串口名称
        /// </summary>
        public string PortName => _comm == null ? "" : _comm.PortName;

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">奇偶校验</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        public void OpenCom(string portName, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            CloseCom();

            _comm = new SerialPort
            {
                PortName = portName,
                BaudRate = baudRate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = stopBits,
            };
            _comm.DataReceived += SerialPortDataReceived;

            try
            {
                _comm.Open();
                IsOpen = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (DataType == SerialPortDataType.String)
            {
                DealString(ReadCommString());
            }

            if (DataType == SerialPortDataType.Bytes)
            {
                DealBytes(ReadCommBytes());
            }
        }

        /// <summary>
        /// 处理字符串
        /// </summary>
        /// <param name="value">字符串</param>
        public abstract void DealString(string value);

        /// <summary>
        /// 处理数组
        /// </summary>
        /// <param name="value">数组</param>
        public abstract void DealBytes(byte[] value);

        private string ReadCommString()
        {
            LastString = _comm.ReadExisting();
            return LastString;
        }

        private byte[] ReadCommBytes()
        {
            Thread.Sleep(200);
            var bytesRead = _comm.BytesToRead;
            var dataBytes = new byte[bytesRead];
            //读取缓冲区数据
            _comm.Read(dataBytes, 0, bytesRead);
            LastBytes = (byte[])dataBytes.Clone();
            return dataBytes;
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="commandBytes">命令</param>
        public void SendCommand(byte[] commandBytes)
        {
            if (_comm.IsOpen)
            {
                _comm.Write(commandBytes, 0, commandBytes.Length);
            }
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmdString">字符串</param>
        /// <param name="addEnterNew">回车换行</param>
        public void SendCommand(string cmdString, bool addEnterNew = false)
        {
            if (!IsOpen)
            {
                return;
            }

            if (!addEnterNew)
            {
                _comm.WriteLine(cmdString);
            }
            else
            {
                _comm.WriteLine(cmdString + "\r\n");
            }
        }
    }
}