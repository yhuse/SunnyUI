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
 * 文件名称: UString.cs
 * 文件说明: 字符串扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Sunny.UI
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 处理文件夹名称末尾加文件夹分隔符(windows为\，linux为/)
        /// </summary>
        /// <param name="dir">文件夹名称</param>
        /// <returns>结果</returns>
        public static string DealPath(this string dir)
        {
            return dir.IsNullOrEmpty() ? dir : (dir[dir.Length - 1] == Path.DirectorySeparatorChar ? dir : dir + Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// 默认加密密码
        /// </summary>
        public const string DefaultCryptString = "!@#$%^&*";

        /// <summary>
        /// 浮点数的格式化字符串（例如：0.00）
        /// </summary>
        /// <param name="decimalcount">小数位数</param>
        /// <returns>字符串</returns>
        public static string FormatString(this int decimalcount)
        {
            if (decimalcount <= 0)
            {
                return "0";
            }

            return "0." + "0".Repet(decimalcount);
        }

        /// <summary>忽略大小写的字符串相等比较，判断是否以任意一个待比较字符串相等</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns>相等比较</returns>
        public static bool EqualsIgnoreCase(this string value, params string[] strs)
        {
            foreach (var item in strs)
            {
                if (string.Equals(value, item, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>忽略大小写的字符串相等比较，判断是否以任意一个待比较字符串相等</summary>
        /// <param name="value">字符串</param>
        /// <param name="comparestring">待比较字符串</param>
        /// <returns>相等比较</returns>
        public static bool EqualsIgnoreCase(this string value, string comparestring)
        {
            return string.Equals(value, comparestring, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static bool ToBoolean(this string s, bool def = default(bool))
        {
            return bool.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static char ToChar(this string s, char def = default(char))
        {
            return char.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static decimal ToDecimal(this string s, decimal def = default(decimal))
        {
            return decimal.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static double ToDouble(this string s, double def = default(double))
        {
            return double.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static float ToFloat(this string s, float def = default(float))
        {
            return float.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static byte ToByte(this string s, byte def = default(byte))
        {
            return byte.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static sbyte ToSByte(this string s, sbyte def = default(sbyte))
        {
            return sbyte.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static short ToShort(this string s, short def = default(short))
        {
            return short.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static ushort ToUShort(this string s, ushort def = default(ushort))
        {
            return ushort.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static int ToInt(this string s, int def = default(int))
        {
            return int.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static uint ToUInt(this string s, uint def = default(uint))
        {
            return uint.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static long ToLong(this string s, long def = default(long))
        {
            return long.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static ulong ToULong(this string s, ulong def = default(ulong))
        {
            return ulong.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static DateTime ToDateTime(this string s, DateTime def = default(DateTime))
        {
            return DateTime.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 字符串转换为日期
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="formatString">日期格式</param>
        /// <returns>结果</returns>
        public static DateTime ToDateTime(this string s, string formatString)
        {
            return DateTime.ParseExact(s, formatString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 字符串转换为日期
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="formatString">日期格式，可包含的格式化字符串为：yyyy、yy、MM、dd、HH、mm、ss、fff、：、-、空格</param>
        /// <param name="def">默认日期</param>
        /// <returns>结果</returns>
        public static DateTime ToDateTimeEx(this string s, string formatString, DateTime def = default(DateTime))
        {
            int year = def.Year;
            int month = def.Month;
            int day = def.Day;
            int hour = def.Hour;
            int minute = def.Minute;
            int second = def.Second;
            int millisecond = def.Millisecond;
            if (formatString.Contains("yyyy"))
                year = s.Substring(formatString.IndexOf("yyyy", StringComparison.CurrentCulture), 4).ToInt();
            else if (formatString.Contains("yy"))
                year = 2000 + s.Substring(formatString.IndexOf("yy", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("MM"))
                month = s.Substring(formatString.IndexOf("MM", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("dd"))
                day = s.Substring(formatString.IndexOf("dd", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("HH"))
                hour = s.Substring(formatString.IndexOf("HH", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("mm"))
                minute = s.Substring(formatString.IndexOf("mm", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("ss"))
                second = s.Substring(formatString.IndexOf("ss", StringComparison.CurrentCulture), 2).ToInt();

            if (formatString.Contains("fff"))
                millisecond = s.Substring(formatString.IndexOf("fff", StringComparison.CurrentCulture), 3).ToInt();

            try
            {
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="def">默认</param>
        /// <returns>结果</returns>
        public static Guid ToGuid(this string s, Guid def = default(Guid))
        {
            return Guid.TryParse(s, out var result) ? result : def;
        }

        /// <summary>
        /// 是否是布尔字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsBoolean(this string s)
        {
            return bool.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Char字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsChar(this string s)
        {
            return char.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Decima字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsDecimal(this string s)
        {
            return decimal.TryParse(s, out decimal _);
        }

        /// <summary>
        /// 是否是Double字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsDouble(this string s)
        {
            return double.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Float字符串转换
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsFloat(this string s)
        {
            return float.TryParse(s, out float _);
        }

        /// <summary>
        /// 是否是Byte字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsByte(this string s)
        {
            return byte.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是SByte字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsSByte(this string s)
        {
            return sbyte.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Short字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsShort(this string s)
        {
            return short.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是UShort字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsUShort(this string s)
        {
            return ushort.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Int字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsInt(this string s)
        {
            return int.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是UInt字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsUInt(this string s)
        {
            return uint.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Long字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsLong(this string s)
        {
            return long.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是ULong字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsULong(this string s)
        {
            return ulong.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是日期字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsDateTime(this string s)
        {
            return DateTime.TryParse(s, out _);
        }

        /// <summary>
        /// 是否是Guid字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsGuid(this string s)
        {
            return Guid.TryParse(s, out _);
        }

        /// <summary>
        /// 结果序列化字符串
        /// [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        /// </summary>
        public const string StructLayoutString = "[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]";

        /// <summary>
        /// 字符串重复显示
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Repet(this string str, int count)
        {
            if (count <= 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("");
            for (int i = 0; i < count; i++)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 字符串左侧按长度截取后的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>字符串</returns>
        public static string Left(this string s, int length)
        {
            return s.IsNullOrEmpty() ? s : (length <= s.Length ? s.Substring(0, length) : s);
        }

        /// <summary>
        /// 字符串中间按长度截取后的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="start">开始</param>
        /// <param name="length">长度</param>
        /// <returns>字符串</returns>
        public static string Middle(this string s, int start, int length)
        {
            return s.IsNullOrEmpty() ? s : (length <= s.Length - start ? s.Substring(start, length) : s.Substring(start, s.Length - start));
        }

        /// <summary>
        /// 指示指定的字符串是 null还是 Empty 字符串。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 指示指定的字符串是 null还是 Empty 字符串。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsValid(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="maxLength">长度</param>
        /// <returns>结果</returns>
        public static string TrimToMaxLength(this string value, int maxLength)
        {
            return (value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }

        /// <summary>
        /// 字符串为空时返回缺省值，不为空时直接返回原字符串
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="defaultValue">defaultValue</param>
        /// <returns>结果</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNullOrEmpty() ? defaultValue : value);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 字符串右侧按长度截取后的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>字符串</returns>
        public static string Right(this string s, int length)
        {
            if (s.IsNullOrEmpty())
            {
                return s;
            }

            return length >= s.Length ? s : s.Substring(s.Length - length, length);
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns>结果</returns>
        public static bool IsNumber(this string s)
        {
            Regex regNumber = new Regex("^[0-9]+$");
            return regNumber.Match(s).Success;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns>结果</returns>
        public static bool IsNumberWithSign(this string s)
        {
            Regex regNumberSign = new Regex("^[+-]?[0-9]+$");
            return regNumberSign.Match(s).Success;
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool HasCnChar(this string s)
        {
            Regex regChzn = new Regex("[\u4e00-\u9fa5]");
            return regChzn.Match(s).Success;
        }

        /// <summary>
        /// 使用utf8编码将字符串散列
        /// </summary>
        /// <param name="s">要散列的字符串</param>
        /// <returns>散列后的字符串</returns>
        public static string Md5String(this string s)
        {
            return s.Md5String(Encoding.UTF8).ToUpper();
        }

        /// <summary>
        /// 使用指定的编码将字符串散列
        /// </summary>
        /// <param name="s">要散列的字符串</param>
        /// <param name="encode">编码</param>
        /// <returns>散列后的字符串</returns>
        private static string Md5String(this string s, Encoding encode)
        {
            MD5 md5 = MD5.Create();
            try
            {
                byte[] source = md5.ComputeHash(encode.GetBytes(s));
                var sBuilder = new StringBuilder();
                foreach (byte str in source)
                {
                    sBuilder.Append(str.ToString("x2"));
                }

                return sBuilder.ToString();
            }
            finally
            {
                md5.Dispose();
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="s">待加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string DesEncrypt(this string s, string key = "")
        {
            try
            {
                string deskey = key + DefaultCryptString;
                byte[] keyBytes = Encoding.UTF8.GetBytes(deskey.Substring(0, 8));
                byte[] keyIv = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(s);
                var provider = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="s">待解密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(this string s, string key = "")
        {
            try
            {
                string deskey = key + DefaultCryptString;
                byte[] keyBytes = Encoding.UTF8.GetBytes(deskey.Substring(0, 8));
                byte[] keyIv = keyBytes;
                byte[] inputByteArray = Convert.FromBase64String(s);
                var provider = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="s">待加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string MyEncrypt(this string s, string key = "")
        {
            try
            {
                string deskey = key + DefaultCryptString;
                byte[] keyBytes = Encoding.UTF8.GetBytes(deskey.Substring(0, 8));
                byte[] keyIv = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(s);
                var provider = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                string str = Convert.ToBase64String(mStream.ToArray());
                int cnt = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == '=')
                    {
                        cnt++;
                    }
                    else
                    {
                        break;
                    }
                }

                str = str.Left(str.Length - cnt) + cnt;
                return str;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="s">待解密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string MyDecrypt(this string s, string key = "")
        {
            try
            {
                string deskey = key + DefaultCryptString;
                byte[] keyBytes = Encoding.UTF8.GetBytes(deskey.Substring(0, 8));
                byte[] keyIv = keyBytes;
                int cnt = s.Right(1).ToInt();
                s = s.Left(s.Length - 1);
                for (int i = 0; i < cnt; i++)
                {
                    s = s + "=";
                }

                byte[] inputByteArray = Convert.FromBase64String(s);
                var provider = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 字符串是否为IP地址
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static bool IsIP4(this string s)
        {
            string[] strs = s.Split(".");
            if (strs.Length != 4)
            {
                return false;
            }

            foreach (string str in strs)
            {
                if (!str.Trim().IsNumber())
                {
                    return false;
                }

                if (str.Trim().ToInt() < 0)
                {
                    return false;
                }

                if (str.Trim().ToInt() > 255)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="ignoreEmpty">忽略空字符串</param>
        /// <returns>结果</returns>
        public static string[] Split(this string s, string separator, bool ignoreEmpty = false)
        {
            string[] strs = s.Split(new[] { separator }, StringSplitOptions.None);
            if (!ignoreEmpty)
            {
                return strs;
            }

            List<string> lsts = new List<string>();
            foreach (var str in strs)
            {
                if (!str.IsNullOrEmpty())
                {
                    lsts.Add(str);
                }
            }

            return lsts.ToArray();
        }

        /// <summary>
        /// 字符串包含分隔符的个数
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>个数</returns>
        public static int SplitSeparatorCount(this string s, string separator)
        {
            if (!s.Contains(separator))
            {
                return 0;
            }

            string strReplaced = s.Replace(separator, "");
            return (s.Length - strReplaced.Length) / separator.Length;
        }

        /// <summary>
        /// 以分隔符分割后的最后一个字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="ignoreEmpty">忽略空字符串</param>
        /// <returns>结果</returns>
        public static string SplitLast(this string s, string separator, bool ignoreEmpty = false)
        {
            string[] strs = s.Split(separator, ignoreEmpty);
            return strs.Length == 0 ? string.Empty : strs[strs.Length - 1];
        }

        /// <summary>
        /// 以分隔符分割后的第一个字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="ignoreEmpty">忽略空字符串</param>
        /// <returns>结果</returns>
        public static string SplitFirst(this string s, string separator, bool ignoreEmpty = false)
        {
            string[] strs = s.Split(separator, ignoreEmpty);
            return strs.Length == 0 ? string.Empty : strs[0];
        }

        /// <summary>
        /// 返回字符串分割后指定索引的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="index">索引</param>
        /// <param name="ignoreEmpty">忽略空字符串</param>
        /// <returns>结果</returns>
        public static string SplitIndex(this string s, string separator, int index, bool ignoreEmpty = false)
        {
            string[] strs = s.Split(separator, ignoreEmpty);
            return strs.Length == 0 ? string.Empty : strs[index];
        }

        /// <summary>
        /// 以分隔符分割的最后一个字符串之前的字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>结果</returns>
        public static string SplitBeforeLast(this string s, string separator)
        {
            int iLast = s.LastIndexOf(separator, StringComparison.Ordinal);
            return iLast > 0 ? s.Substring(0, iLast) : s;
        }

        /// <summary>
        /// 检测输入的邮件地址是否合法，非法则返回true。
        /// </summary>
        /// <param name="s">邮件</param>
        /// <returns>结果</returns>
        public static bool IsMail(this string s)
        {
            const string StrWords = "abcdefghijklmnopqrstuvwxyz_-.0123456789"; //定义合法字符范围

            string strTmp = s.Trim();
            //检测输入字符串是否为空，不为空时才执行代码。
            if (strTmp == "" || strTmp.Length == 0)
            {
                return false;
            }

            //判断邮件地址中是否存在一个“@”号
            if (s.SplitSeparatorCount("@") != 1)
            {
                return false;
            }

            //以“@”号为分割符，把地址切分成两部分，分别进行验证。
            string[] strChars = strTmp.Split('@');
            if (strChars.Length != 2)
            {
                return false;
            }

            if (strChars[0] == "" || strChars[1] == "")
            {
                return false;
            }

            foreach (string strChar in strChars)
            {
                //逐个字进行验证，如果超出所定义的字符范围strWords，则表示地址非法。
                int i;
                for (i = 0; i < strChar.Length; i++)
                {
                    string strResult = strChar.Substring(i, 1).ToLower();
                    if (SplitSeparatorCount(StrWords, strResult) == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 英文字符串转换为ASCII编码的数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="len">长度</param>
        /// <returns>结果</returns>
        public static byte[] ToEnBytes(this string s, int len)
        {
            byte[] strs = ToEncodeBytes(s, Encoding.ASCII);
            byte[] result = new byte[len];
            Array.Copy(strs, 0, result, 0, Math.Min(strs.Length, len));
            return result;
        }

        /// <summary>
        /// ASCII编码的数组转换为英文字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>结果</returns>
        public static string ToEnString(this byte[] s)
        {
            return ToEncodeString(s, Encoding.ASCII).Trim('\0').Trim();
        }

        /// <summary>
        /// 字符串按指定编码转换为数组
        /// </summary>
        /// <param name="dealString">字符串</param>
        /// <param name="encode">编码</param>
        /// <returns>结果</returns>
        public static byte[] ToEncodeBytes(this string dealString, Encoding encode)
        {
            byte[] outs = encode.GetBytes(dealString);
            return outs;
        }

        /// <summary>
        /// 数组按指定编码转换为字符串
        /// </summary>
        /// <param name="dealBytes">数组</param>
        /// <param name="encode">编码</param>
        /// <returns>结果</returns>
        public static string ToEncodeString(this byte[] dealBytes, Encoding encode)
        {
            return encode.GetString(dealBytes);
        }

        /// <summary>
        /// 	Gets the string before the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns>结果</returns>
        /// <remarks>Unlike GetBetween and GetAfter, this does not Trim the result.</remarks>
        public static string Before(this string value, string x)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            return xPos == -1 ? string.Empty : value.Substring(0, xPos);
        }

        /// <summary>
        /// 	Gets the string between the given string parameters.
        /// </summary>
        /// <param name = "value">The source value.</param>
        /// <param name = "x">The left string sentinel.</param>
        /// <param name = "y">The right string sentinel</param>
        /// <returns>结果</returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string Between(this string value, string x, string y)
        {
            var xPos = value.IndexOf(x, StringComparison.Ordinal);
            var yPos = value.LastIndexOf(y, StringComparison.Ordinal);

            if (xPos == -1 || xPos == -1)
            {
                return string.Empty;
            }

            var startIndex = xPos + x.Length;
            return startIndex >= yPos ? string.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
        }

        /// <summary>
        /// 	Gets the string after the given string parameter.
        /// </summary>
        /// <param name = "value">The default value.</param>
        /// <param name = "x">The given string parameter.</param>
        /// <returns>结果</returns>
        /// <remarks>Unlike GetBefore, this method trims the result</remarks>
        public static string After(this string value, string x)
        {
            var xPos = value.LastIndexOf(x, StringComparison.Ordinal);

            if (xPos == -1)
            {
                return string.Empty;
            }

            var startIndex = xPos + x.Length;
            return startIndex >= value.Length ? string.Empty : value.Substring(startIndex).Trim();
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the default encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string Base64Encode(this string value)
        {
            return value.Base64Encode(null);
        }

        /// <summary>
        /// 	Encodes the input value to a Base64 string using the supplied encoding.
        /// </summary>
        /// <param name = "value">The input value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string Base64Encode(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the default encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <returns>The decoded string</returns>
        public static string Base64Decode(this string encodedValue)
        {
            return encodedValue.Base64Decode(null);
        }

        /// <summary>
        /// 	Decodes a Base 64 encoded value to a string using the supplied encoding.
        /// </summary>
        /// <param name = "encodedValue">The Base 64 encoded value.</param>
        /// <param name = "encoding">The encoding.</param>
        /// <returns>The decoded string</returns>
        public static string Base64Decode(this string encodedValue, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Calculates the SHA1 hash of the supplied string and returns a base 64 string.
        /// </summary>
        /// <param name="stringToHash">String that must be hashed.</param>
        /// <returns>The hashed string or null if hashing failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToHash or key is null or empty.</exception>
        public static string SHA1Hash(this string stringToHash)
        {
            if (stringToHash.IsNullOrEmpty())
            {
                return null;
            }

            byte[] data = Encoding.UTF8.GetBytes(stringToHash);
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Return the string with the leftmost number_of_characters characters removed.
        /// </summary>
        /// <param name="str">The string being extended</param>
        /// <param name="number_of_characters">The number of characters to remove.</param>
        /// <returns>结果</returns>
        public static string RemoveLeft(this string str, int number_of_characters)
        {
            if (str.Length <= number_of_characters)
            {
                return "";
            }

            return str.Substring(number_of_characters);
        }

        /// <summary>
        /// Return the string with the rightmost number_of_characters characters removed.
        /// </summary>
        /// <param name="str">The string being extended</param>
        /// <param name="number_of_characters">The number of characters to remove.</param>
        /// <returns>结果</returns>
        public static string RemoveRight(this string str, int number_of_characters)
        {
            if (str.Length <= number_of_characters)
            {
                return "";
            }

            return str.Substring(0, str.Length - number_of_characters);
        }

        /// <summary>
        /// Converts a regular string into SecureString
        /// </summary>
        /// <param name="u">String value.</param>
        /// <param name="makeReadOnly">Makes the text value of this secure string read-only.</param>
        /// <returns>Returns a SecureString containing the value of a transformed object. </returns>
        public static SecureString ToSecureString(this string u, bool makeReadOnly = true)
        {
            if (u.IsNullOrEmpty())
            {
                return null;
            }

            SecureString s = new SecureString();
            foreach (char c in u)
            {
                s.AppendChar(c);
            }

            if (makeReadOnly)
            {
                s.MakeReadOnly();
            }

            return s;
        }

        /// <summary>
        /// Coverts the SecureString to a regular string.
        /// </summary>
        /// <param name="s">Object value.</param>
        /// <returns>Item of secured string.</returns>
        public static string ToUnSecureString(this SecureString s)
        {
            if (s == null)
            {
                return null;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(s);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>结果</returns>
        public static string GZipCompress(this string input)
        {
            return input.IsNullOrEmpty() ? input : Convert.ToBase64String(Encoding.Default.GetBytes(input).GZipCompress());
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns>结果</returns>
        public static string GZipDecompress(this string input)
        {
            return input.IsNullOrEmpty() ? input : Encoding.Default.GetString(Convert.FromBase64String(input).GZipDecompress());
        }

        /// <summary>
        /// 是否是纯英文字母
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>结果</returns>
        public static bool IsPureEnglishChar(string str)
        {
            Regex regEnglish = new Regex(@"^[a-zA-Z]+$");
            return regEnglish.IsMatch(str);
        }

        /// <summary>
        /// 是否是命名空间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>结果</returns>
        public static bool IsNameSpace(string str)
        {
            Regex regEnglish = new Regex(@"^[a-zA-Z.]+$");
            return regEnglish.IsMatch(str) && !str.StartsWith(".") && !str.EndsWith(".") && str.SplitSeparatorCount(".") <= 3 && !str.Contains("..") && !str.Contains("...");
        }

        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public static string Reverse(this string text)
        {
            var charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}