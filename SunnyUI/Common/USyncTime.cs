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
 * 文件名称: USyncTime.cs
 * 文件说明: 时间同步类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// 时间同步类
    /// </summary>
    public static class SyncTime
    {
        private const string chinaTimeZoneName_en = "China Standard Time";
        private const string chinaTimeZoneName_zh = "中国标准时间";

        /// <summary>
        /// 阿里云时间服务器
        /// </summary>
        public const string NTPServer_ALIYUN = "ntp1.aliyun.com";

        /// <summary>
        /// Linux时间服务器
        /// </summary>
        public const string NTPServer_API_BZ = "ntp.api.bz";

        [DllImport("Kernel32.dll")]
        private static extern void GetLocalTime(ref SystemTime lpSystemTime);

        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime lpSystemTime);

        [DllImport("Kernel32.dll")]
        private static extern void GetSystemTime(ref SystemTime lpSystemTime);

        /// <summary>
        ///
        /// </summary>
        /// <param name="lpTimeZoneInformation"></param>
        /// <returns>0: TIME_ZONE_ID_UNKNOWN, 1: TIME_ZONE_ID_STANDARD, 2: TIME_ZONE_ID_DAYLIGHT</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetTimeZoneInformation(ref TimeZoneInformation lpTimeZoneInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetTimeZoneInformation(ref TimeZoneInformation lpTimeZoneInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetDynamicTimeZoneInformation(ref DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetDynamicTimeZoneInformation(ref DynamicTimeZoneInformation lpDynamicTimeZoneInformation);

        [StructLayout(LayoutKind.Sequential)]
        private struct SystemTime
        {
            [MarshalAs(UnmanagedType.U2)]
            internal ushort year; // 年

            [MarshalAs(UnmanagedType.U2)]
            internal ushort month; // 月

            [MarshalAs(UnmanagedType.U2)]
            internal ushort dayOfWeek; // 星期

            [MarshalAs(UnmanagedType.U2)]
            internal ushort day; // 日

            [MarshalAs(UnmanagedType.U2)]
            internal ushort hour; // 时

            [MarshalAs(UnmanagedType.U2)]
            internal ushort minute; // 分

            [MarshalAs(UnmanagedType.U2)]
            internal ushort second; // 秒

            [MarshalAs(UnmanagedType.U2)]
            internal ushort milliseconds; // 毫秒
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct TimeZoneInformation
        {
            [MarshalAs(UnmanagedType.I4)]
            internal int bias; // 以分钟为单位

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            internal string standardName; // 标准时间的名称

            internal SystemTime standardDate;

            [MarshalAs(UnmanagedType.I4)]
            internal int standardBias; // 标准偏移

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            internal string daylightName; // 夏令时的名称

            internal SystemTime daylightDate;

            [MarshalAs(UnmanagedType.I4)]
            internal int daylightBias; // 夏令时偏移
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DynamicTimeZoneInformation
        {
            [MarshalAs(UnmanagedType.I4)]
            internal int bias; // 偏移，以分钟为单位

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            internal string standardName; // 标准时间的名称

            internal SystemTime standardDate;

            [MarshalAs(UnmanagedType.I4)]
            internal int standardBias; // 标准偏移

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            internal string daylightName; // 夏令时的名称

            internal SystemTime daylightDate;

            [MarshalAs(UnmanagedType.I4)]
            internal int daylightBias; // 夏令时偏移

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
            internal string timeZoneKeyName; // 时区名

            [MarshalAs(UnmanagedType.Bool)]
            internal bool dynamicDaylightTimeDisabled; // 是否自动调整时钟的夏令时
        }

        /// <summary>
        /// 判断当前时区是否为中国时区
        /// </summary>
        /// <returns></returns>
        public static bool IsCnZone()
        {
            string timeZoneName = GetLocalTimeZone();
            return timeZoneName.StartsWith(chinaTimeZoneName_zh);
        }

        /// <summary>
        /// 设置当前时区为中国时区
        /// </summary>
        /// <returns></returns>
        public static bool SetCnZone()
        {
            if (IsCnZone())
            {
                return true;
            }

            try
            {
                SetLocalTimeZone(chinaTimeZoneName_en);
                return IsCnZone();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 同步网络时间
        /// </summary>
        /// <returns></returns>
        public static bool SyncLocalTime(string NTPServer = NTPServer_API_BZ)
        {
            DateTime webTime = GetWebTime(NTPServer);
            return SetLocalTime(webTime);
        }

        /// <summary>
        /// 获取本地时区
        /// </summary>
        /// <returns></returns>
        public static string GetLocalTimeZone()
        {
            // 检测当前系统是否为旧系统
            if (IsOldOsVersion())
            {
                TimeZoneInformation tzi = new TimeZoneInformation();
                GetTimeZoneInformation(ref tzi);
                return TimeZoneInfo2CustomString(tzi);
            }

            DynamicTimeZoneInformation dtzi = new DynamicTimeZoneInformation();
            GetDynamicTimeZoneInformation(ref dtzi);
            return DynamicTimeZoneInfo2CustomString(dtzi);
        }

        /// <summary>
        /// 设置本地时区
        /// </summary>
        /// <param name="timeZoneName_en"></param>
        /// <returns></returns>
        public static bool SetLocalTimeZone(string timeZoneName_en)
        {
            if (PrivilegeAPI.GrantPrivilege(PrivilegeConstants.SE_TIME_ZONE_NAME))
            {
                DynamicTimeZoneInformation dtzi = timeZoneName2DynamicTimeZoneInformation(timeZoneName_en);
                bool success = false;

                // 检测当前系统是否为旧系统
                if (IsOldOsVersion())
                {
                    Console.WriteLine("检测当前系统为: Old OS Version");
                    TimeZoneInformation tzi = DynamicTimeZoneInformation2TimeZoneInformation(dtzi);
                    success = SetTimeZoneInformation(ref tzi);
                }
                else
                {
                    success = SetDynamicTimeZoneInformation(ref dtzi);
                }

                if (success)
                {
                    TimeZoneInfo.ClearCachedData();  // 清除缓存
                }

                if (!PrivilegeAPI.RevokePrivilege(PrivilegeConstants.SE_TIME_ZONE_NAME))
                {
                    Console.WriteLine("撤权失败: 更改时区");
                }

                return success;
            }

            Console.WriteLine("授权失败: 更改时区");
            return false;
        }

        // 将TimeZoneInformation转换为自定义string
        private static string TimeZoneInfo2CustomString(TimeZoneInformation tzi)
        {
            return tzi.standardName + "(" + tzi.bias + ")";
        }

        // 将DynamicTimeZoneInformation转换为自定义string
        private static string DynamicTimeZoneInfo2CustomString(DynamicTimeZoneInformation dtzi)
        {
            return dtzi.standardName + "(" + dtzi.bias + ")";
        }

        // 根据时区名获取对应的DynamicTimeZoneInformation
        private static DynamicTimeZoneInformation timeZoneName2DynamicTimeZoneInformation(string timeZoneName)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);

            DynamicTimeZoneInformation dtzi = new DynamicTimeZoneInformation();
            dtzi.standardName = timeZoneInfo.StandardName;
            dtzi.standardDate = new SystemTime();
            dtzi.daylightName = timeZoneInfo.DaylightName;
            dtzi.daylightDate = new SystemTime();
            dtzi.timeZoneKeyName = timeZoneInfo.Id;
            dtzi.dynamicDaylightTimeDisabled = false;
            dtzi.bias = -Convert.ToInt32(timeZoneInfo.BaseUtcOffset.TotalMinutes);
            return dtzi;
        }

        // 将DynamicTimeZoneInformation转换为TimeZoneInformation
        private static TimeZoneInformation DynamicTimeZoneInformation2TimeZoneInformation(DynamicTimeZoneInformation dtzi)
        {
            return new TimeZoneInformation
            {
                bias = dtzi.bias,
                standardName = dtzi.standardName,
                standardDate = dtzi.standardDate,
                standardBias = dtzi.standardBias,
                daylightName = dtzi.daylightName,
                daylightDate = dtzi.daylightDate,
                daylightBias = dtzi.daylightBias
            };
        }

        // 判断Windows系统是否为旧版本
        private static bool IsOldOsVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            return os.Platform != PlatformID.Win32NT || os.Version.Major < 6;
        }

        /// <summary>
        /// 获取本地时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLocalTime()
        {
            SystemTime sysTime = new SystemTime();
            GetLocalTime(ref sysTime);
            return SystemTime2DateTime(sysTime);
        }

        /// <summary>
        /// 设置本地时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool SetLocalTime(DateTime dateTime)
        {
            if (PrivilegeAPI.GrantPrivilege(PrivilegeConstants.SE_SYSTEMTIME_NAME))
            {
                SystemTime sysTime = DateTime2SystemTime(dateTime);
                bool success = SetLocalTime(ref sysTime);
                if (!PrivilegeAPI.RevokePrivilege(PrivilegeConstants.SE_SYSTEMTIME_NAME))
                {
                    Console.WriteLine("撤权失败: 更改系统时间");
                }

                return success;
            }

            Console.WriteLine("授权失败: 更改系统时间");
            return false;
        }

        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSystemTime()
        {
            SystemTime sysTime = new SystemTime();
            GetSystemTime(ref sysTime);
            return SystemTime2DateTime(sysTime);
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWebTime(string NTPServer = NTPServer_API_BZ)
        {
            // NTP message size - 16 bytes of the digest (RFC 2030)
            byte[] ntpData = new byte[48];
            // Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; // LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            IPAddress[] addresses = Dns.GetHostEntry(NTPServer).AddressList;
            // The UDP port number assigned to NTP is 123
            IPEndPoint ipEndPoint = new IPEndPoint(addresses[0], 123);

            // NTP uses UDP
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Connect(ipEndPoint);
            // Stops code hang if NTP is blocked
            socket.ReceiveTimeout = 3000;
            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            // Offset to get to the "Transmit Timestamp" field (time at which the reply
            // departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;
            // Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            // Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
            // Convert From big-endian to little-endian
            intPart = swapEndian(intPart);
            fractPart = swapEndian(fractPart);
            ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000UL);

            // UTC time
            DateTime webTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds);
            // Local time
            return webTime.ToLocalTime();
        }

        // 小端存储与大端存储的转换
        private static uint swapEndian(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
            ((x & 0x0000ff00) << 8) +
            ((x & 0x00ff0000) >> 8) +
            ((x & 0xff000000) >> 24));
        }

        // 将SystemTime转换为DateTime
        private static DateTime SystemTime2DateTime(SystemTime sysTime)
        {
            return new DateTime(sysTime.year, sysTime.month, sysTime.day, sysTime.hour, sysTime.minute, sysTime.second, sysTime.milliseconds);
        }

        // 将DateTime转换为SystemTime
        private static SystemTime DateTime2SystemTime(DateTime dateTime)
        {
            SystemTime sysTime = new SystemTime();
            sysTime.year = Convert.ToUInt16(dateTime.Year);
            sysTime.month = Convert.ToUInt16(dateTime.Month);
            sysTime.day = Convert.ToUInt16(dateTime.Day);
            sysTime.hour = Convert.ToUInt16(dateTime.Hour);
            sysTime.minute = Convert.ToUInt16(dateTime.Minute);
            sysTime.second = Convert.ToUInt16(dateTime.Second);
            sysTime.milliseconds = Convert.ToUInt16(dateTime.Millisecond);
            return sysTime;
        }
    }
}