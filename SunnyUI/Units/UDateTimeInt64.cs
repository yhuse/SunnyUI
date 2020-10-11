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
 * 文件名称: UDateTimeInt64.cs
 * 文件说明: 日期长整型
 * 当前版本: V2.2
 * 创建日期: 2020-09-23
 *
 * 2020-09-23: V2.2.8 增加文件说明
 * 2020-10-10: V2.2.8 增加与Double互转
******************************************************************************/

using System;

namespace Sunny.UI
{
    /// <summary>
    /// 日期与长整形和浮点型互转
    /// Value：长整形，为1970年1月1日0时起的毫秒数
    /// DoubleValue：双精度浮点数，1970年1月1日0时起的天数
    /// </summary>
    public struct DateTimeInt64 : IComparable, IEquatable<DateTimeInt64>, IEquatable<long>, IEquatable<DateTime>
    {
        public long Value { get; set; }

        public const long Jan1st1970Ticks = 621355968000000000; //Jan1st1970.Ticks;
        public const long Dec31th9999Ticks = 3155378975999990000; //DateTime.MaxValue.Ticks;
        public const string DefaultFormatString = DateTimeEx.DateTimeFormatEx;
        public const double MillisecondsPerDay = 86400000.0;

        /// <summary>
        /// 返回当前时间的毫秒数, 这个毫秒其实就是自1970年1月1日0时起的毫秒数
        /// </summary>
        public static long CurrentDateTimeToTicks()
        {
            return (DateTime.UtcNow.Ticks - Jan1st1970Ticks) / 10000;
        }

        public double DoubleValue
        {
            get { return Value * 1.0 / MillisecondsPerDay; }
            set { Value = (long)(value * MillisecondsPerDay); }
        }

        /// <summary>
        /// 返回指定时间的毫秒数, 这个毫秒其实就是自1970年1月1日0时起的毫秒数
        /// </summary>
        public static long DateTimeToTicks(DateTime datetime)
        {
            return (datetime.ToUniversalTime().Ticks - Jan1st1970Ticks) / 10000;
        }

        /// <summary>
        /// 从一个代表自1970年1月1日0时起的毫秒数，转换为DateTime (北京时间)
        /// </summary>
        public static DateTime TicksToDateTime(long ticks)
        {
            return TimeZone.CurrentTimeZone.ToLocalTime(TicksToUTCDateTime(ticks));
        }

        /// <summary>
        /// 从一个代表自1970年1月1日0时起的毫秒数，转换为DateTime (UTC时间)
        /// </summary>
        public static DateTime TicksToUTCDateTime(long ticks)
        {
            return new DateTime(ticks * 10000 + Jan1st1970Ticks);
        }

        public DateTimeInt64(long ticks)
        {
            ticks = MakeValidDate(ticks);
            Value = ticks;
        }

        public DateTimeInt64(double doubleTicks)
        {
            doubleTicks = MakeValidDate((long)(doubleTicks * MillisecondsPerDay));
            Value = (long)doubleTicks;
        }

        public DateTimeInt64(DateTime dateTime)
        {
            Value = DateTimeToTicks(dateTime);
        }

        public DateTimeInt64(int year, int month, int day)
        {
            Value = DateTimeToTicks(new DateTime(year, month, day));
        }

        public DateTimeInt64(int year, int month, int day, int hour, int minute, int second)
        {
            Value = DateTimeToTicks(new DateTime(year, month, day, hour, minute, second));
        }

        public DateTimeInt64(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            Value = DateTimeToTicks(new DateTime(year, month, day, hour, minute, second, millisecond));
        }

        public DateTimeInt64(DateTimeInt64 dtValue)
        {
            Value = dtValue.Value;
        }

        public bool IsValidDate
        {
            get { return Value >= 0 && Value <= Dec31th9999Ticks - Jan1st1970Ticks; }
        }

        public DateTime DateTime
        {
            get { return TicksToDateTime(Value); }
            set { Value = DateTimeToTicks(value); }
        }

        public static bool CheckValidDate(long ticks)
        {
            return ticks >= 0 && ticks <= Dec31th9999Ticks - Jan1st1970Ticks;
        }

        public static long MakeValidDate(long ticks)
        {
            if (ticks < 0)
                ticks = 0;
            if (ticks > Dec31th9999Ticks - Jan1st1970Ticks)
                ticks = Dec31th9999Ticks - Jan1st1970Ticks;

            return ticks;
        }

        public void AddMilliseconds(double milliseconds)
        {
            Value = DateTimeToTicks(DateTime.AddMilliseconds(milliseconds));
        }

        public void AddSeconds(double seconds)
        {
            Value = DateTimeToTicks(DateTime.AddSeconds(seconds));
        }

        public void AddMinutes(double minutes)
        {
            Value = DateTimeToTicks(DateTime.AddMinutes(minutes));
        }

        public void AddHours(double hours)
        {
            Value = DateTimeToTicks(DateTime.AddHours(hours));
        }

        public void AddDays(double days)
        {
            Value = DateTimeToTicks(DateTime.AddDays(days));
        }

        public void AddMonths(int months)
        {
            Value = DateTimeToTicks(DateTime.AddMonths(months));
        }

        public void AddYears(int years)
        {
            Value = DateTimeToTicks(DateTime.AddYears(years));
        }

        public static long operator -(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value - dtValue2.Value;
        }

        public static long operator +(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value + dtValue2.Value;
        }

        public static DateTimeInt64 operator -(DateTimeInt64 dtValue1, long dtValue2)
        {
            dtValue1.Value -= dtValue2;
            return dtValue1;
        }

        public static DateTimeInt64 operator +(DateTimeInt64 dtValue1, long dtValue2)
        {
            dtValue1.Value += dtValue2;
            return dtValue1;
        }

        public static DateTimeInt64 operator ++(DateTimeInt64 dtValue)
        {
            dtValue.AddDays(1);
            return dtValue;
        }

        public static DateTimeInt64 operator --(DateTimeInt64 dtValue)
        {
            dtValue.AddDays(-1);
            return dtValue;
        }

        public static implicit operator long(DateTimeInt64 dtValue)
        {
            return dtValue.Value;
        }

        public static implicit operator DateTimeInt64(long ticks)
        {
            return new DateTimeInt64(ticks);
        }

        public static implicit operator DateTimeInt64(DateTime dt)
        {
            return new DateTimeInt64(dt);
        }

        public static implicit operator DateTime(DateTimeInt64 dt)
        {
            return dt.DateTime;
        }

        public static implicit operator double(DateTimeInt64 dtValue)
        {
            return dtValue.DoubleValue;
        }

        public static implicit operator DateTimeInt64(double ticks)
        {
            return new DateTimeInt64(ticks);
        }

        public static bool operator ==(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value == dtValue2.Value;
        }

        public static bool operator !=(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value != dtValue2.Value;
        }

        public static bool operator ==(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value == ticks;
        }

        public static bool operator !=(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value != ticks;
        }

        public static bool operator <(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value < dtValue2.Value;
        }

        public static bool operator >(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value > dtValue2.Value;
        }

        public static bool operator <=(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value <= dtValue2.Value;
        }

        public static bool operator >=(DateTimeInt64 dtValue1, DateTimeInt64 dtValue2)
        {
            return dtValue1.Value >= dtValue2.Value;
        }

        public static bool operator <(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value < ticks;
        }

        public static bool operator >(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value > ticks;
        }

        public static bool operator <=(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value <= ticks;
        }

        public static bool operator >=(DateTimeInt64 dtValue, long ticks)
        {
            return dtValue.Value >= ticks;
        }

        public override bool Equals(object obj)
        {
            if (obj is DateTimeInt64 dtValue)
            {
                return dtValue.Value == Value;
            }
            else if (obj is long longValue)
            {
                return longValue == Value;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is DateTimeInt64)) throw new ArgumentException();
            return (Value).CompareTo(((DateTimeInt64)obj).Value);
        }

        public string ToString(long ticks)
        {
            return ToString(ticks, DefaultFormatString);
        }

        public override string ToString()
        {
            return ToString(Value, DefaultFormatString);
        }

        public string ToString(string fmtStr)
        {
            return ToString(Value, fmtStr);
        }

        public static string ToString(long dtValue, string fmtStr)
        {
            DateTime dt = TicksToDateTime(dtValue);
            return dt.ToString(fmtStr);
        }

        public bool Equals(DateTimeInt64 dtValue)
        {
            return Value == dtValue.Value;
        }

        public bool Equals(long ticks)
        {
            return Value == ticks;
        }

        public bool Equals(DateTime datetime)
        {
            return Value == (new DateTimeInt64(datetime)).Value;
        }

        public string DateTimeString => DateTime.DateTimeString();

        public string DateString => DateTime.DateString();

        public string TimeString => DateTime.TimeString();

        public string HourMinuteString => DateTime.ToString("HH:mm");

        public int Year => DateTime.Year;
        public int Month => DateTime.Month;
        public int Day => DateTime.Day;
        public int Hour => DateTime.Hour;
        public int Minute => DateTime.Minute;
        public int Second => DateTime.Second;
        public int Millisecond => DateTime.Millisecond;
    }
}