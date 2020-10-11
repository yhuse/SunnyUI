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
 * 文件名称: UDateTime.cs
 * 文件说明: 日期扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sunny.UI
{
    /// <summary>
    /// 日期扩展类
    /// </summary>
    public static class DateTimeEx
    {
        /// <summary>
        /// 日期连接符
        /// </summary>
        public const string DateLink = "-";

        /// <summary>
        /// 日期连接符
        /// </summary>
        public const string DateLinkEx = "/";

        /// <summary>
        /// 时间连接符
        /// </summary>
        public const string TimeLink = ":";

        /// <summary>
        /// 日期(包含毫秒) - 格式化字符串 yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public const string DateTimeFormatEx = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>
        /// 日期 - 格式化字符串 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 年月日 - 格式化字符串 yyyy-MM-dd
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 时分秒 - 格式化字符串 HH:mm:ss
        /// </summary>
        public const string TimeFormat = "HH:mm:ss";

        /// <summary>
        /// 去除格式化字符串中的分隔符
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static string TrimLink(string format)
        {
            return format.Replace(DateLink, "").Replace(TimeLink, "").Replace(".", "").Replace(" ", "").Trim();
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss (2001-02-03 04:05:06)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>string</returns>
        public static string String(this DateTime dt)
        {
            return dt.ToString(DateTimeFormat);
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss (2001-02-03 04:05:06)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="withLink"></param>
        /// <returns>string</returns>
        public static string DateTimeString(this DateTime dt, bool withLink = true)
        {
            return dt.ToString(withLink ? DateTimeFormat : TrimLink(DateTimeFormat));
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.fff (2001-02-03 04:05:06.789)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="withLink"></param>
        /// <returns>string</returns>
        public static string DateTimeExString(this DateTime dt, bool withLink = true)
        {
            return dt.ToString(withLink ? DateTimeFormatEx : TrimLink(DateTimeFormatEx));
        }

        /// <summary>
        /// yyyy-MM-dd (2001-02-03)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="withLink"></param>
        /// <returns>string</returns>
        public static string DateString(this DateTime dt, bool withLink = true)
        {
            return dt.ToString(withLink ? DateFormat : TrimLink(DateFormat));
        }

        /// <summary>
        /// HH:mm:ss (04:05:06)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="withLink"></param>
        /// <returns>string</returns>
        public static string TimeString(this DateTime dt, bool withLink = true)
        {
            return dt.ToString(withLink ? TimeFormat : TrimLink(TimeFormat));
        }

        /// <summary>
        /// yyyy-MM (2001-02)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="withLink"></param>
        /// <returns>string</returns>
        public static string YearMonthString(this DateTime dt, bool withLink = true)
        {
            return dt.ToString(withLink ? "yyyy-MM" : "yyyyMM");
        }

        /// <summary>
        /// yyyy (2001)
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>string</returns>
        public static string YearString(this DateTime dt)
        {
            return dt.ToString("yyyy");
        }

        /// <summary>
        /// 十分钟一次的索引（0-143）
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>结果</returns>
        public static int TenMinuteIndex(this DateTime dt)
        {
            return dt.Hour * 6 + dt.Minute / 10;
        }

        /// <summary>
        /// 当月天数列表
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>List</returns>
        public static List<DateTime> MonthDays(this DateTime dt)
        {
            List<DateTime> days = new List<DateTime>();

            int cnt = dt.DaysInMonth();
            for (int i = 1; i <= cnt; i++)
            {
                days.Add(new DateTime(dt.Year, dt.Month, i));
            }

            return days;
        }

        /// <summary>
        /// 当月天数列表字符串
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>List</returns>
        public static List<string> MonthDayStrings(this DateTime dt)
        {
            List<DateTime> days = dt.MonthDays();
            List<string> list = new List<string>();

            foreach (DateTime day in days)
            {
                list.Add(day.ToString(DateFormat));
            }

            return list;
        }

        /// <summary>
        /// 在指定目录下创建以年月日分级的子目录，末尾包括\
        /// </summary>
        /// <param name="dt">日期</param>
        /// <param name="path">文件夹</param>
        /// <param name="createIfNotExist">不存在是否创建</param>
        /// <returns>文件夹名</returns>
        public static string YearMonthDayFolder(this DateTime dt, string path, bool createIfNotExist = false)
        {
            if (path.IsNullOrEmpty())
            {
                return path;
            }

            string result = path.DealPath() + dt.YearString() + "\\" + dt.YearMonthString() + "\\" + dt.DateString() + "\\";
            if (createIfNotExist)
            {
                DirEx.CreateDir(result);
            }

            return result;
        }

        /// <summary>
        /// 在指定目录下创建以年月分级的子目录，末尾包括\
        /// </summary>
        /// <param name="dt">日期</param>
        /// <param name="path">文件夹</param>
        /// <param name="createIfNotExist">不存在是否创建</param>
        /// <returns>文件夹名</returns>
        public static string YearMonthFolder(this DateTime dt, string path, bool createIfNotExist = false)
        {
            if (path.IsNullOrEmpty())
            {
                return path;
            }

            string result = path.DealPath() + dt.YearString() + "\\" + dt.YearMonthString() + "\\";
            if (createIfNotExist)
            {
                DirEx.CreateDir(result);
            }

            return result;
        }

        /// <summary>
        /// 时间日期转浮点，以2000-01-01 00：00：00起始
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns>浮点</returns>
        public static double ToDouble(this DateTime datetime)
        {
            return datetime.Subtract(Jan1st1970).TotalDays;
        }

        /// <summary>
        /// 起始日期，UTC时间，1970-01-01 00：00：00起始
        /// </summary>
        public static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 浮点转时间日期，以UTC时间，1970-01-01 00：00：00起始
        /// </summary>
        /// <param name="iDays">浮点</param>
        /// <returns>日期</returns>
        public static DateTime ToDateTime(this double iDays)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(Jan1st1970.AddDays(iDays));
        }

        /// <summary>
        /// 计算当前月份有多少天
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The number of days.</returns>
        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        /// <summary>
        /// 此月第一天
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 此月第一天
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dayOfWeek">The desired day of week.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime FirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.FirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(1);
            }

            return dt;
        }

        /// <summary>
        /// 此月最后一天
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <returns>The last day of the month.</returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DaysInMonth(date));
        }

        /// <summary>
        /// 此月最后一天
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dayOfWeek">The desired day of week.</param>
        /// <returns>The date time</returns>
        public static DateTime LastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.LastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(-1);
            }

            return dt;
        }

        /// <summary>
        /// 是否是当天
        /// </summary>
        /// <param name = "dt">The date.</param>
        /// <returns>
        /// 	<c>true</c> if the specified date is today; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsToday(this DateTime dt)
        {
            return (dt.Date == DateTime.Today.Date);
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "hours">The hours to be set.</param>
        /// <param name = "minutes">The minutes to be set.</param>
        /// <param name = "seconds">The seconds to be set.</param>
        /// <returns>The DateTime including the new time value</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hours, minutes, seconds));
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name="hours">The hour</param>
        /// <param name="minutes">The minute</param>
        /// <param name="seconds">The second</param>
        /// <param name="milliseconds">The millisecond</param>
        /// <returns>The DateTime including the new time value</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds, int milliseconds)
        {
            return date.SetTime(new TimeSpan(0, hours, minutes, seconds, milliseconds));
        }

        /// <summary>
        /// 设置秒，毫秒为0
        /// </summary>
        /// <param name="datetime">值</param>
        /// <returns>结果</returns>
        public static DateTime ZeroSecond(this DateTime datetime)
        {
            return datetime.SetTime(new TimeSpan(0, datetime.Hour, datetime.Minute, 0, 0));
        }

        /// <summary>
        /// 设置毫秒为0
        /// </summary>
        /// <param name="datetime">值</param>
        /// <returns>结果</returns>
        public static DateTime ZeroMillisecond(this DateTime datetime)
        {
            return datetime.SetTime(new TimeSpan(0, datetime.Hour, datetime.Minute, datetime.Second, 0));
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name = "date">The base date.</param>
        /// <param name = "time">The TimeSpan to be applied.</param>
        /// <returns>
        /// 	The DateTime including the new time value
        /// </returns>
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }

        /// <summary>
        /// 日期是否相同
        /// </summary>
        /// <param name = "date">The date.</param>
        /// <param name = "dateToCompare">The date to compare with.</param>
        /// <returns>
        /// 	<c>true</c> if both date values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
        {
            return (date.Date == dateToCompare.Date);
        }

        /// <summary>
        /// 时间是否相同
        /// </summary>
        /// <param name = "time">The time.</param>
        /// <param name = "timeToCompare">The time to compare.</param>
        /// <returns>
        /// 	<c>true</c> if both time values are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
        {
            return (time.TimeOfDay == timeToCompare.TimeOfDay);
        }

        /// <summary>
        /// Get seconds of UNIX area. This is the milliseconds since 1/1/1970
        /// </summary>
        /// <param name = "datetime">Up to which time.</param>
        /// <returns>number of milliseconds.</returns>
        /// <remarks>
        /// http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static long SecondsSince1970(this DateTime datetime)
        {
            TimeSpan ts = datetime.ToUniversalTime().Subtract(Jan1st1970);
            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// 在指定日期之前
        /// </summary>
        /// <param name="source">The source DateTime.</param>
        /// <param name="other">The compared DateTime.</param>
        /// <returns>True if the source is before the other DateTime, False otherwise</returns>
        public static bool IsBefore(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) < 0;
        }

        /// <summary>
        /// 在指定日期之后
        /// </summary>
        /// <param name="source">The source DateTime.</param>
        /// <param name="other">The compared DateTime.</param>
        /// <returns>True if the source is before the other DateTime, False otherwise</returns>
        public static bool IsAfter(this DateTime source, DateTime other)
        {
            return source.CompareTo(other) > 0;
        }

        /// <summary>
        /// 明天
        /// </summary>
        /// <param name="date">The current day</param>
        /// <returns>结果</returns>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }

        /// <summary>
        /// 昨天
        /// </summary>
        /// <param name="date">The current day</param>
        /// <returns>结果</returns>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }

        /// <summary>
        /// 当日结束
        /// </summary>
        /// <param name="date">The DateTime to be processed</param>
        /// <returns>The date at 23:50.59.999</returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.SetTime(23, 59, 59, 999);
        }

        /// <summary>
        /// 当月结束
        /// </summary>
        /// <param name="date">The DateTime to be processed</param>
        /// <returns>当月结束</returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// 当月结束
        /// </summary>
        /// <param name="date">The DateTime to be processed</param>
        /// <returns>当月结束</returns>
        public static DateTime BeginOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).Midnight();
        }

        /// <summary>
        /// 中午
        /// </summary>
        /// <param name="time">The current date</param>
        /// <returns>结果</returns>
        public static DateTime Noon(this DateTime time)
        {
            return time.SetTime(12, 0, 0);
        }

        /// <summary>
        /// 午夜
        /// </summary>
        /// <param name="time">The current date</param>
        /// <returns>结果</returns>
        public static DateTime Midnight(this DateTime time)
        {
            return time.SetTime(0, 0, 0, 0);
        }

        /// <summary>
        /// 一月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime January(this int year, int day)
        {
            return new DateTime(year, 1, day);
        }

        /// <summary>
        /// 二月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime February(this int year, int day)
        {
            return new DateTime(year, 2, day);
        }

        /// <summary>
        /// 三月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime March(this int year, int day)
        {
            return new DateTime(year, 3, day);
        }

        /// <summary>
        /// 四月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime April(this int year, int day)
        {
            return new DateTime(year, 4, day);
        }

        /// <summary>
        /// 五月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime May(this int year, int day)
        {
            return new DateTime(year, 5, day);
        }

        /// <summary>
        /// 六月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime June(this int year, int day)
        {
            return new DateTime(year, 6, day);
        }

        /// <summary>
        /// 七月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime July(this int year, int day)
        {
            return new DateTime(year, 7, day);
        }

        /// <summary>
        /// 八月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime August(this int year, int day)
        {
            return new DateTime(year, 8, day);
        }

        /// <summary>
        /// 九月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime September(this int year, int day)
        {
            return new DateTime(year, 9, day);
        }

        /// <summary>
        /// 十月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime October(this int year, int day)
        {
            return new DateTime(year, 10, day);
        }

        /// <summary>
        /// 十一月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime November(this int year, int day)
        {
            return new DateTime(year, 11, day);
        }

        /// <summary>
        /// 十二月
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="day">日</param>
        /// <returns>时间</returns>
        public static DateTime December(this int year, int day)
        {
            return new DateTime(year, 12, day);
        }

        /// <summary>
        /// 两个日期间所有日期列表
        /// </summary>
        /// <param name="thisDateTime">起始</param>
        /// <param name="toDateTime">结束</param>
        /// <returns>列表</returns>
        public static List<DateTime> AllDaysTo(this DateTime thisDateTime, DateTime toDateTime)
        {
            List<DateTime> all = new List<DateTime>();

            DateTime dtb = toDateTime.IsAfter(thisDateTime) ? thisDateTime : toDateTime;
            DateTime dte = toDateTime.IsAfter(thisDateTime) ? toDateTime : thisDateTime;

            dtb = dtb.Midnight();
            dte = dte.EndOfDay();

            while (dtb < dte)
            {
                all.Add(dtb);
                dtb = dtb.AddDays(1);
            }

            return all;
        }

        /// <summary>
        /// 获取当天的每隔十分钟的时间列表（共144场）
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        public static List<DateTime> AllTenMinutes(this DateTime date)
        {
            List<DateTime> all = new List<DateTime>();
            date = date.Midnight();

            for (int i = 0; i < 144; i++)
            {
                all.Add(date);
                date = date.AddMinutes(10);
            }

            return all;
        }

        /// <summary>
        /// 格式化时间设置秒、毫秒为0
        /// </summary>
        /// <param name="dateTime">值</param>
        /// <returns>结果</returns>
        public static DateTime TrimSeconds(this DateTime dateTime)
        {
            return dateTime.SetTime(dateTime.Hour, dateTime.Minute, 0, 0);
        }

        /// <summary>
        /// 格式化时间设置毫秒为0
        /// </summary>
        /// <param name="dateTime">值</param>
        /// <returns>结果</returns>
        public static DateTime TrimMilliseconds(this DateTime dateTime)
        {
            return dateTime.SetTime(dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
        }

        /// <summary>
        /// TimeTicks转时间
        /// </summary>
        /// <param name="timeTicks">值</param>
        /// <returns>结果</returns>
        public static DateTime TicksToDateTime(this long timeTicks)
        {
            return DateTime.MinValue.AddTicks(timeTicks);
        }

        /// <summary>
        /// 某月第一天第一个小时
        /// </summary>
        /// <param name="value">时间</param>
        /// <returns>结果</returns>
        public static bool IsMonthBeginOneHour(this DateTime value)
        {
            return value.Day == 1 && value.Hour == 0;
        }

        /// <summary>
        /// 是否是六分钟的整数倍
        /// </summary>
        /// <param name="value">时间</param>
        /// <returns>结果</returns>
        public static bool IsMultiSix(this DateTime value)
        {
            return value.Minute.Mod(6) == 0;
        }

        /// <summary>
        /// 设置本地电脑的年月日
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        public static void SetLocalDate(int year, int month, int day)
        {
            //实例一个Process类，启动一个独立进程
            Process p = new Process();
            //Process类有一个StartInfo属性
            //设定程序名
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = $"/c date {year}-{month}-{day}";
            //关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口
            p.StartInfo.CreateNoWindow = true;
            //启动
            p.Start();
            //从输出流取得命令执行结果
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的时分秒
        /// </summary>
        /// <param name="hour">时</param>
        /// <param name="min">分</param>
        /// <param name="sec">秒</param>
        public static void SetLocalTime(int hour, int min, int sec)
        {
            //实例一个Process类，启动一个独立进程
            Process p = new Process();
            //Process类有一个StartInfo属性
            //设定程序名
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = $"/c time {hour}:{min}:{sec}";
            //关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口
            p.StartInfo.CreateNoWindow = true;
            //启动
            p.Start();
            //从输出流取得命令执行结果
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的年月日和时分秒
        /// </summary>
        /// <param name="time">时间</param>
        public static void SetLocalDateTime(DateTime time)
        {
            SetLocalDate(time.Year, time.Month, time.Day);
            SetLocalTime(time.Hour, time.Minute, time.Second);
        }
    }
}