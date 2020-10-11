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
 * 文件名称: UMath.cs
 * 文件说明: 数学扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Sunny.UI
{
    /// <summary>
    /// 数学扩展类
    /// </summary>
    public static class MathEx
    {
        public static double CalcDistance(PointF pt1, PointF pt2)
        {
            float xx = pt1.X - pt2.X;
            float yy = pt1.Y - pt2.Y;
            return Math.Sqrt(xx * xx + yy * yy);
        }

        public static double CalcDistance(Point pt1, Point pt2)
        {
            int xx = pt1.X - pt2.X;
            int yy = pt1.Y - pt2.Y;
            return Math.Sqrt(xx * xx + yy * yy);
        }

        public static double CalcAngle(Point thisPoint, Point toPoint)
        {
            double az = Math.Atan2(thisPoint.Y - toPoint.Y, thisPoint.X - toPoint.X) * 180 / Math.PI;
            az = (az - 270 + 720) % 360;
            return az;
        }

        public static double CalcAngle(PointF thisPoint, PointF toPoint)
        {
            double az = Math.Atan2(thisPoint.Y - toPoint.Y, thisPoint.X - toPoint.X) * 180 / Math.PI;
            az = (az - 270 + 720) % 360;
            return az;
        }

        /// <summary>
        /// 二分查找与最近值序号
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="target">值</param>
        /// <returns>最近值序号</returns>
        public static int BinarySearchNearIndex<T>(this IList<T> list, T target) where T : IComparable
        {
            if (list.Count == 0) return -1;
            int i = 0, j = list.Count - 1;

            if (target.CompareTo(list[0]) == -1) return 0;
            if (target.CompareTo(list[j]) == 1) return list.Count - 1;
            while (i <= j)
            {
                var mid = (i + j) / 2;
                if (target.CompareTo(list[mid]) == 1) i = mid + 1;
                if (target.CompareTo(list[mid]) == -1) j = mid - 1;
                if (target.CompareTo(list[mid]) == 0) return mid;
            }

            if (i - 1 < 0) return i;

            TypeCode typeCode = Type.GetTypeCode(target.GetType());
            switch (typeCode)
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return target.ToString().ToLong() - list[i - 1].ToString().ToLong() >
                          list[i].ToString().ToLong() - target.ToString().ToLong() ? i : i - 1;

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return target.ToString().ToULong() - list[i - 1].ToString().ToULong() >
                           list[i].ToString().ToULong() - target.ToString().ToULong() ? i : i - 1;

                case TypeCode.Single:
                case TypeCode.Double:
                    return target.ToString().ToDouble() - list[i - 1].ToString().ToDouble() >
                           list[i].ToString().ToDouble() - target.ToString().ToDouble() ? i : i - 1;

                case TypeCode.Decimal:
                    return target.ToString().ToDecimal() - list[i - 1].ToString().ToDecimal() >
                           list[i].ToString().ToDecimal() - target.ToString().ToDecimal() ? i : i - 1;

                case TypeCode.DateTime:
                    return target.ToString().ToDateTime() - list[i - 1].ToString().ToDateTime() >
                           list[i].ToString().ToDateTime() - target.ToString().ToDateTime() ? i : i - 1;

                default: return i - 1;
            }
        }

        /// <summary>
        /// 二分查找与最近值序号
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="target">值</param>
        /// <returns>最近值序号</returns>
        public static int BinarySearchNearIndex<T>(this T[] list, T target) where T : IComparable
        {
            return BinarySearchNearIndex(list.ToList(), target);
        }

        /*
        /// <summary>
        /// 二分查找与最近值序号
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="target">值</param>
        /// <returns>最近值序号</returns>
        public static int BinarySearch(List<int> list, int target)
        {
            int i = 0, j = list.Count - 1;
            if (target < list[0]) return 0;
            if (target > list[j]) return list.Count - 1;
            while (i <= j)
            {
                var mid = (i + j) / 2;
                if (target > list[mid]) i = mid + 1;
                if (target < list[mid]) j = mid - 1;
                if (target == list[mid]) return mid;
            }

            if (i - 1 < 0) return i;
            if (Math.Abs(list[i - 1] - target) > Math.Abs(list[i] - target)) return i;
            else return i - 1;
        }

        /// <summary>
        /// 二分查找与最近值序号
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="target">值</param>
        /// <returns>最近值序号</returns>
        public static int BinarySearch(int[] list, int target)
        {
            return BinarySearch(list.ToList(), target);
        }
        */

        public static T CheckLowerLimit<T>(this T obj, T lowerLimit) where T : IComparable
        {
            return obj.CompareTo(lowerLimit) == -1 ? lowerLimit : obj;
        }

        public static T CheckUpperLimit<T>(this T obj, T upperLimit) where T : IComparable
        {
            return obj.CompareTo(upperLimit) == 1 ? upperLimit : obj;
        }

        public static T CheckRange<T>(this T obj, T lowerLimit, T upperLimit) where T : IComparable
        {
            if (lowerLimit.CompareTo(upperLimit) == -1)
                return obj.CheckLowerLimit(lowerLimit).CheckUpperLimit(upperLimit);
            else if (lowerLimit.CompareTo(upperLimit) == 1)
                return obj.CheckLowerLimit(upperLimit).CheckUpperLimit(lowerLimit);
            else
                return lowerLimit;
        }

        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="region">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this Point point, Region region)
        {
            return region.IsVisible(point);
        }

        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="points">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this Point point, Point[] points)
        {
            using (GraphicsPath path = points.Path())
            {
                using (Region region = path.Region())
                {
                    return region.IsVisible(point);
                }
            }
        }

        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="points">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this PointF point, PointF[] points)
        {
            using (GraphicsPath path = points.Path())
            {
                using (Region region = path.Region())
                {
                    return region.IsVisible(point);
                }
            }
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="points">点集合</param>
        /// <returns>路径</returns>
        public static GraphicsPath Path(this Point[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();
            path.AddPolygon(points);
            return path;
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="points">点集合</param>
        /// <returns>路径</returns>
        public static GraphicsPath Path(this PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();
            path.AddPolygon(points);
            return path;
        }

        /// <summary>
        /// 创建区域
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>区域</returns>
        public static Region Region(this GraphicsPath path)
        {
            Region region = new Region();
            region.MakeEmpty();
            region.Union(path);
            return region;
        }

        /// <summary>
        /// 十六进制转十进制
        /// </summary>
        /// <param name="str">str</param>
        /// <returns>int</returns>
        public static int HexToInt(this string str)
        {
            return Convert.ToInt32(str, 16);
        }

        /// <summary>
        /// 二进制转十进制
        /// </summary>
        /// <param name="str">str</param>
        /// <returns>int</returns>
        public static int BinToInt(this string str)
        {
            return Convert.ToInt32(str, 2);
        }

        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="length">length</param>
        /// <returns>string</returns>
        public static string ToHex(this int value, int length = 0)
        {
            string str = Convert.ToString(value, 16).ToUpper();
            str = str.Length.Mod(2) == 0 ? str : "0" + str;

            if (length == 0)
            {
                return str;
            }

            if (str.Length >= length)
            {
                return str;
            }

            return str.PadLeft(length, '0');
        }

        /// <summary>
        /// 十进制转二进制
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="length">length</param>
        /// <returns>string</returns>
        public static string ToBin(this int value, int length = 0)
        {
            string str = Convert.ToString(value, 2);
            if (length == 0)
            {
                return str;
            }

            if (str.Length >= length)
            {
                return str;
            }

            return str.PadLeft(length, '0');
        }

        /// <summary>
        /// 取整
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static int RoundEx(this double value)
        {
            return (int)(value + 0.5);
        }

        /// <summary>
        /// 取整
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static int RoundEx(this float value)
        {
            return (int)(value + 0.5f);
        }

        /// <summary>
        /// 判断浮点数相等
        /// </summary>
        /// <param name="valueA">A</param>
        /// <param name="valueB">B</param>
        /// <param name="eps">范围</param>
        /// <returns>结果</returns>
        public static bool EqualsFloat(this float valueA, float valueB, float eps = 0.00000001f)
        {
            return Math.Abs(valueA - valueB) < eps;
        }

        /// <summary>
        /// 判断双精度数是否相等
        /// </summary>
        /// <param name="valueA">A</param>
        /// <param name="valueB">B</param>
        /// <param name="eps">范围</param>
        /// <returns>结果</returns>
        public static bool EqualsDouble(this double valueA, double valueB, float eps = 0.00000001f)
        {
            return Math.Abs(valueA - valueB) < eps;
        }

        /// <summary>
        /// 除取整
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="divParam">除数</param>
        /// <returns>结果</returns>
        public static int Div(this int value, int divParam)
        {
            return value / divParam;
        }

        /// <summary>
        /// 除取余
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="divParam">除数</param>
        /// <returns>结果</returns>
        public static int Mod(this int value, int divParam)
        {
            return value % divParam;
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="d">角度</param>
        /// <returns>弧度</returns>
        public static double Rad(this double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        /// <param name="rad">弧度</param>
        /// <returns>角度</returns>
        public static double Deg(this double rad)
        {
            return rad * 180.0 / Math.PI;
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="d">角度</param>
        /// <returns>弧度</returns>
        public static double Rad(this float d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        /// <param name="rad">弧度</param>
        /// <returns>角度</returns>
        public static double Deg(this float rad)
        {
            return rad * 180.0 / Math.PI;
        }

        /// <summary>
        /// 是否为0
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static bool IsZero(this double value)
        {
            return (value >= -double.Epsilon && value <= double.Epsilon);
        }

        /// <summary>
        /// 是否为0
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static bool IsZero(this float value)
        {
            return (value >= -float.Epsilon && value <= float.Epsilon);
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>结果</returns>
        public static bool InRange(this float value, float minValue, float maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>结果</returns>
        public static float InRange(this float value, float minValue, float maxValue, float defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>结果</returns>
        public static bool InRange(this double value, double minValue, double maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>结果</returns>
        public static double InRange(this double value, double minValue, double maxValue, double defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }

        /// <summary>
        /// 是否是偶数
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// 是否是奇数
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>结果</returns>
        public static bool InRange(this int value, int minValue, int maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>结果</returns>
        public static int InRange(this int value, int minValue, int maxValue, int defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }

        /// <summary>
        /// 	Determines whether the value is even
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsEven(this long value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// 	Determines whether the value is odd
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsOdd(this long value)
        {
            return value % 2 != 0;
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>结果</returns>
        public static bool InRange(this long value, long minValue, long maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>结果</returns>
        public static long InRange(this long value, long minValue, long maxValue, long defaultValue)
        {
            return value.InRange(minValue, maxValue) ? value : defaultValue;
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation (either "Yes" or "No").
        /// </summary>
        /// <param name="boolean">是否</param>
        /// <returns>string</returns>
        public static string ToYesNoString(this bool boolean)
        {
            return boolean ? "Yes" : "No";
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation (either "True" or "False").
        /// </summary>
        /// <param name="boolean">是否</param>
        /// <returns>string</returns>
        public static string ToTrueFalseString(this bool boolean)
        {
            return boolean ? "True" : "False";
        }

        /// <summary>
        /// Converts the value in number format {1 , 0}.
        /// </summary>
        /// <param name="boolean">是否</param>
        /// <returns>int</returns>
        public static int ToInt(this bool boolean)
        {
            return boolean ? 1 : 0;
        }

        /// <summary>
        /// Converts the value in number format {1 , 0}.
        /// </summary>
        /// <param name="boolean">是否</param>
        /// <returns>byte</returns>
        public static byte ToByte(this bool boolean)
        {
            return (byte)ToInt(boolean);
        }

        /// <summary>
        /// Converts number format {1 , 0} to bool value.
        /// </summary>
        /// <param name="value">int</param>
        /// <param name="Default">是否</param>
        /// <returns>结果</returns>
        public static bool ToBool(this int value, bool Default = false)
        {
            switch (value)
            {
                case 1:
                    return true;

                case 0:
                    return false;
            }

            return Default;
        }

        /// <summary>
        /// Converts number format {1 , 0} to bool value.
        /// </summary>
        /// <param name="value">byte</param>
        /// <param name="Default">是否</param>
        /// <returns>结果</returns>
        public static bool ToBool(this byte value, bool Default = false)
        {
            switch (value)
            {
                case 1:
                    return true;

                case 0:
                    return false;
            }

            return Default;
        }

        /// <summary>
        /// 二进制
        /// </summary>
        public const string CHARS_BINARY = "01";

        /// <summary>
        /// 四进制
        /// </summary>
        public const string CHARS_QUATERNARY = "0123";

        /// <summary>
        /// 六进制
        /// </summary>
        public const string CHARS_SEPTENARY = "012345";

        /// <summary>
        /// 八进制
        /// </summary>
        public const string CHARS_OCTAL = "01234567";

        /// <summary>
        /// 十进制
        /// </summary>
        public const string CHARS_DECIMAL = "0123456789";

        /// <summary>
        /// 十二进制
        /// </summary>
        public const string CHARS_DUODECIMAL = "0123456789MN";

        /// <summary>
        /// 十六进制
        /// </summary>
        public const string CHARS_HEX = "0123456789ABCDEF";

        /// <summary>
        /// 纯大写字母
        /// </summary>
        public const string CHARS_PUREUPPERCHAR = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 纯字母（包含大小写）
        /// </summary>
        public const string CHARS_PURECHAR = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 大写字母和数字
        /// </summary>
        public const string CHARS_36 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// 字母和数字
        /// </summary>
        public const string CHARS_62 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 字符串转换进制
        /// </summary>
        public enum Characters
        {
            /// <summary>
            /// 二进制 "01"
            /// </summary>
            [DisplayText(CHARS_BINARY)]
            BINARY,

            /// <summary>
            /// 四进制 "0123"
            /// </summary>
            [DisplayText(CHARS_QUATERNARY)]
            QUATERNARY,

            /// <summary>
            /// 六进制 "012345"
            /// </summary>
            [DisplayText(CHARS_SEPTENARY)]
            SEPTENARY,

            /// <summary>
            /// 八进制 "01234567"
            /// </summary>
            [DisplayText(CHARS_OCTAL)]
            OCTAL,

            /// <summary>
            /// 十进制 "0123456789"
            /// </summary>
            [DisplayText(CHARS_DECIMAL)]
            DECIMAL,

            /// <summary>
            /// 十二进制 "0123456789MN"
            /// </summary>
            [DisplayText(CHARS_DUODECIMAL)]
            DUODECIMAL,

            /// <summary>
            /// 十六进制 "0123456789ABCDEF"
            /// </summary>
            [DisplayText(CHARS_HEX)]
            HEX,

            /// <summary>
            /// 自定义 默认 "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            /// </summary>
            [DisplayText(CHARS_PUREUPPERCHAR)]
            Custom
        }

        /// <summary>
        /// 数字转换为指定的进制形式字符串
        /// </summary>
        /// <param name="number">待转换数字</param>
        /// <param name="characters">进制格式</param>
        /// <param name="customCharacter">自定义进制字符串</param>
        /// <returns>结果</returns>
        public static string ToNumberString(this ulong number, Characters characters = Characters.DECIMAL, string customCharacter = CHARS_PUREUPPERCHAR)
        {
            string DisplayText = characters.DisplayText();
            if (characters == Characters.Custom && !customCharacter.IsNullOrEmpty())
            {
                DisplayText = customCharacter;
            }

            List<string> result = new List<string>();
            ulong t = number;
            if (t == 0)
            {
                return DisplayText[0].ToString();
            }

            while (t > 0)
            {
                ulong mod = t % ((ulong)DisplayText.Length);
                t /= ((ulong)DisplayText.Length);
                string character = DisplayText[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }

            return string.Join("", result.ToArray());
        }

        public static string ToNumberString(this uint number, Characters characters = Characters.DECIMAL,
            string customCharacter = CHARS_PUREUPPERCHAR)
        {
            return ((ulong)number).ToNumberString(characters, customCharacter);
        }

        public static string ToNumberString(this ushort number, Characters characters = Characters.DECIMAL,
            string customCharacter = CHARS_PUREUPPERCHAR)
        {
            return ((ulong)number).ToNumberString(characters, customCharacter);
        }

        public static string ToNumberString(this byte number, Characters characters = Characters.DECIMAL,
            string customCharacter = CHARS_PUREUPPERCHAR)
        {
            return ((ulong)number).ToNumberString(characters, customCharacter);
        }

        /// <summary>
        /// 指定字符串转换为指定进制的数字形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="characters">进制格式</param>
        /// <param name="customCharacter">自定义进制字符串</param>
        /// <returns>结果</returns>
        public static long ToNumberLong(this string str, Characters characters = Characters.DECIMAL, string customCharacter = CHARS_PUREUPPERCHAR)
        {
            string DisplayText = characters.DisplayText();
            if (characters == Characters.Custom && !customCharacter.IsNullOrEmpty())
            {
                DisplayText = customCharacter;
            }

            long result = 0;
            int j = 0;
            foreach (var ch in new string(str.ToCharArray().Reverse().ToArray()))
            {
                if (!DisplayText.Contains(ch))
                {
                    continue;
                }

                result += DisplayText.IndexOf(ch) * ((long)Math.Pow(DisplayText.Length, j));
                j++;
            }

            return result;
        }

        /// <summary>
        /// 转换人民币大小金额
        /// </summary>
        /// <param name="num">金额</param>
        /// <returns>返回大写形式</returns>
        public static string ToRMBString(this decimal num)
        {
            const string Str1 = "零壹贰叁肆伍陆柒捌玖"; //0-9所对应的汉字
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
            string str5 = ""; //人民币大写金额形式
            int i; //循环变量
            string ch2 = ""; //数字位的汉字读法
            int nzero = 0; //用来计算连续的零值是几个

            num = Math.Round(Math.Abs(num), 2); //将num取绝对值并四舍五入取2位小数
            string str4 = ((long)(num * 100)).ToString();
            int j = str4.Length;
            if (j > 15)
            {
                return "溢出";
            }

            str2 = str2.Substring(15 - j); //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

            //循环取出每一位需要转换的值
            for (i = 0; i < j; i++)
            {
                string str3 = str4.Substring(i, 1); //从原num值中取出的值
                int temp = Convert.ToInt32(str3); //从原num值中取出的值
                string ch1; //数字的汉语读法
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + Str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = Str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + Str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = Str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }

                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上
                    ch2 = str2.Substring(i, 1);
                }

                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”
                    str5 = str5 + '整';
                }
            }

            if (num == 0)
            {
                str5 = "零元整";
            }

            return str5;
        }
    }
}