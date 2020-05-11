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
 * 文件名称: UWMI.cs
 * 文件说明: 系统信息获取类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Management;
using System.Text;

namespace Sunny.UI
{
    #region WMIPath

    /// <summary>
    /// WMI 路径
    /// </summary>
    public enum WMIPath
    {
        #region 硬件

        /// <summary>
        /// CPU 处理器
        /// </summary>
        Win32_Processor,

        /// <summary>
        /// 物理内存条
        /// </summary>
        Win32_PhysicalMemory,

        /// <summary>
        /// 键盘
        /// </summary>
        Win32_Keyboard,

        /// <summary>
        /// 点输入设备，包括鼠标
        /// </summary>
        Win32_PointingDevice,

        /// <summary>
        /// 软盘驱动器
        /// </summary>
        Win32_FloppyDrive,

        /// <summary>
        /// 硬盘驱动器
        /// </summary>
        Win32_DiskDrive,

        /// <summary>
        /// 光盘驱动器
        /// </summary>
        Win32_CDROMDrive,

        /// <summary>
        /// 主板
        /// </summary>
        Win32_BaseBoard,

        /// <summary>
        /// BIOS 芯片
        /// </summary>
        Win32_BIOS,

        /// <summary>
        /// 并口
        /// </summary>
        Win32_ParallelPort,

        /// <summary>
        /// 串口
        /// </summary>
        Win32_SerialPort,

        /// <summary>
        /// 串口配置
        /// </summary>
        Win32_SerialPortConfiguration,

        /// <summary>
        /// 多媒体设置，一般指声卡
        /// </summary>
        Win32_SoundDevice,

        /// <summary>
        ///  主板插槽 (ISA、PCI、AGP)
        /// </summary>
        Win32_SystemSlot,

        /// <summary>
        /// USB 控制器
        /// </summary>
        Win32_USBController,

        /// <summary>
        /// 网络适配器
        /// </summary>
        Win32_NetworkAdapter,

        /// <summary>
        /// 网络适配器设置
        /// </summary>
        Win32_NetworkAdapterConfiguration,

        /// <summary>
        /// 打印机
        /// </summary>
        Win32_Printer,

        /// <summary>
        /// 打印机设置
        /// </summary>
        Win32_PrinterConfiguration,

        /// <summary>
        /// 打印机任务
        /// </summary>
        Win32_PrintJob,

        /// <summary>
        /// 打印机端口
        /// </summary>
        Win32_TCPIPPrinterPort,

        /// <summary>
        /// MODEM
        /// </summary>
        Win32_POTSModem,

        /// <summary>
        /// MODEM 端口
        /// </summary>
        Win32_POTSModemToSerialPort,

        /// <summary>
        /// 显示器
        /// </summary>
        Win32_DesktopMonitor,

        /// <summary>
        /// 显卡
        /// </summary>
        Win32_DisplayConfiguration,

        /// <summary>
        /// 显卡设置
        /// </summary>
        Win32_DisplayControllerConfiguration,

        /// <summary>
        /// 显卡细节
        /// </summary>
        Win32_VideoController,

        /// <summary>
        /// 显卡支持的显示模式
        /// </summary>
        Win32_VideoSettings,

        #endregion 硬件

        #region 软件

        /// <summary>
        /// 时区
        /// </summary>
        Win32_TimeZone,

        /// <summary>
        /// 驱动程序
        /// </summary>
        Win32_SystemDriver,

        /// <summary>
        /// 磁盘分区
        /// </summary>
        Win32_DiskPartition,

        /// <summary>
        /// 逻辑磁盘
        /// </summary>
        Win32_LogicalDisk,

        /// <summary>
        /// 逻辑磁盘所在分区及始末位置
        /// </summary>
        Win32_LogicalDiskToPartition,

        /// <summary>
        /// 逻辑内存配置
        /// </summary>
        Win32_LogicalMemoryConfiguration,

        /// <summary>
        /// 系统页文件信息
        /// </summary>
        Win32_PageFile,

        /// <summary>
        /// 页文件设置
        /// </summary>
        Win32_PageFileSetting,

        /// <summary>
        /// 系统启动配置
        /// </summary>
        Win32_BootConfiguration,

        /// <summary>
        /// 计算机信息简要
        /// </summary>
        Win32_ComputerSystem,

        /// <summary>
        /// 操作系统信息
        /// </summary>
        Win32_OperatingSystem,

        /// <summary>
        /// 系统自动启动程序
        /// </summary>
        Win32_StartupCommand,

        /// <summary>
        /// 系统安装的服务
        /// </summary>
        Win32_Service,

        /// <summary>
        /// 系统管理组
        /// </summary>
        Win32_Group,

        /// <summary>
        /// 系统组帐号
        /// </summary>
        Win32_GroupUser,

        /// <summary>
        /// 用户帐号
        /// </summary>
        Win32_UserAccount,

        /// <summary>
        /// 系统进程
        /// </summary>
        Win32_Process,

        /// <summary>
        /// 系统线程
        /// </summary>
        Win32_Thread,

        /// <summary>
        /// 共享
        /// </summary>
        Win32_Share,

        /// <summary>
        /// 已安装的网络客户端
        /// </summary>
        Win32_NetworkClient,

        /// <summary>
        /// 已安装的网络协议
        /// </summary>
        Win32_NetworkProtocol,

        #endregion 软件
    }

    #endregion WMIPath

    /*
    <summary>
    获取系统信息
    </summary>
    <example>
    <code>
    WMI w = new WMI(WMIPath.Win32_NetworkAdapterConfiguration);
    for (int i = 0; i < w.Count; i ++)
    {
    if ((bool)w[i, "IPEnabled"])
    {
    Console.WriteLine("Caption:{0}", w[i, "Caption"]);
    Console.WriteLine("MAC Address:{0}", w[i, "MACAddress"]);
    }
    }
    </code>
    </example>
    */

    /// <summary>
    /// WMI
    /// </summary>
    public sealed class WMI
    {
        private readonly ArrayList mocs;
        private readonly StringDictionary names; // 用来存储属性名，便于忽略大小写查询正确名称。

        /// <summary>
        /// 信息集合数量
        /// </summary>
        public int Count => mocs.Count;

        /// <summary>
        /// 获取指定属性值，注意某些结果可能是数组。
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="propertyName">propertyName</param>
        /// <returns>object</returns>
        public object this[int index, string propertyName]
        {
            get
            {
                try
                {
                    string trueName = names[propertyName.Trim()]; // 以此可不区分大小写获得正确的属性名称。
                    Hashtable h = (Hashtable)mocs[index];
                    return h[trueName];
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 返回所有属性名称。
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>string[]</returns>
        public string[] PropertyNames(int index)
        {
            try
            {
                Hashtable h = (Hashtable)mocs[index];
                string[] result = new string[h.Keys.Count];
                h.Keys.CopyTo(result, 0);
                Array.Sort(result);
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 返回测试信息。
        /// </summary>
        /// <returns>string</returns>
        public string Test()
        {
            try
            {
                StringBuilder result = new StringBuilder(1000);

                for (int i = 0; i < Count; i++)
                {
                    int j = 0;
                    foreach (string s in PropertyNames(i))
                    {
                        result.Append(string.Format("{0}:{1}={2}\n", ++j, s, this[i, s]));

                        if (this[i, s] is Array)
                        {
                            Array v1 = this[i, s] as Array;
                            for (int x = 0; x < v1.Length; x++)
                            {
                                result.Append("\t" + v1.GetValue(x) + "\n");
                            }
                        }
                    }

                    result.Append("======WMI=======\n");
                }

                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">path</param>
        public WMI(string path)
        {
            names = new StringDictionary();
            mocs = new ArrayList();

            try
            {
                ManagementClass cimobject = new ManagementClass(path);
                ManagementObjectCollection moc = cimobject.GetInstances();
                bool ok = false;
                foreach (var mo in moc)
                {
                    Hashtable o = new Hashtable();
                    mocs.Add(o);

                    foreach (PropertyData p in mo.Properties)
                    {
                        o.Add(p.Name, p.Value);
                        if (!ok)
                        {
                            names.Add(p.Name, p.Name);
                        }
                    }

                    ok = true;
                    mo.Dispose();
                }

                moc.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">path</param>
        public WMI(WMIPath path) : this(path.ToString())
        {
        }
    }

    /// <summary>
    /// WMI 扩展类
    /// </summary>
    public static class WMIEx
    {
        private static string GetString(WMIPath path, string name, int index = 0)
        {
            try
            {
                WMI wmi = new WMI(path);
                if (wmi.Count > index)
                {
                    var obj = wmi[index, name];
                    if (obj != null)
                    {
                        return obj.ToString().Trim();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return path.ToString();
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <returns>字符串</returns>
        public static string BoardID()
        {
            return GetString(WMIPath.Win32_BaseBoard, "SerialNumber");
        }

        /// <summary>
        /// 主板厂家
        /// </summary>
        /// <returns>字符串</returns>
        public static string BoardManufacturer()
        {
            return GetString(WMIPath.Win32_BaseBoard, "Manufacturer");
        }

        /// <summary>
        /// 主板名称
        /// </summary>
        /// <returns>字符串</returns>
        public static string BoardName()
        {
            return GetString(WMIPath.Win32_BaseBoard, "Name");
        }

        /// <summary>
        /// 主板型号
        /// </summary>
        /// <returns>字符串</returns>
        public static string BoardProduct()
        {
            return GetString(WMIPath.Win32_BaseBoard, "Product");
        }

        /// <summary>
        /// 硬盘编号
        /// </summary>
        /// <returns>字符串</returns>
        public static string DiskID()
        {
            return GetString(WMIPath.Win32_DiskDrive, "SerialNumber");
        }

        /// <summary>
        /// 硬盘名称
        /// </summary>
        /// <returns>字符串</returns>
        public static string DiskCaption()
        {
            return GetString(WMIPath.Win32_DiskDrive, "Caption");
        }

        /// <summary>
        /// CPU 编号
        /// </summary>
        /// <returns>字符串</returns>
        public static string CPUID()
        {
            return GetString(WMIPath.Win32_Processor, "ProcessorId");
        }

        /// <summary>
        /// CPU 名称
        /// </summary>
        /// <returns>字符串</returns>
        public static string CPUName()
        {
            return GetString(WMIPath.Win32_Processor, "Name");
        }

        /// <summary>
        /// 当前系统唯一识别号
        /// </summary>
        /// <returns>字符串</returns>
        public static string ThisOSMD5()
        {
            return (BoardID() + CPUID() + DiskID()).Md5String();
        }
    }
}