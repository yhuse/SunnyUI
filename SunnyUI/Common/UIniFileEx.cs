/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UIniFileEx.cs
 * 文件说明: INI 文件读取类（不用WinAPI）
 * 当前版本: V3.1
 * 创建日期: 2021-10-27
 *
 * 2021-10-27: V2.2.0 增加文件说明
 * 2025-07-06: V3.8.6 增加对 .NetFramework 的支持
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sunny.UI;

/// <summary>
/// INI文件读写类
/// </summary>
public sealed class IniFileEx : IDisposable
{
    /// <summary>
    /// INI文件版本
    /// </summary>
    public const string Version = "NETool IniFile V1.0";

    /// <summary>
    /// 键分隔符
    /// </summary>
    public static readonly string KeyDelimiter = ":";

    private ConcurrentDictionary<string, string> _values;

    public void Dispose()
    {
        UpdateFile();
    }

    /// <summary>
    /// 更新INI文件
    /// </summary>
    public void UpdateFile()
    {
        Save();
    }

    /// <summary>
    /// 获取配置文件名
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// 获取配置文件编码
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// 创建一个新的INI文件读写类
    /// </summary>
    /// <param name="fileName">文件名</param>
    public IniFileEx(string fileName) : this(fileName, Encoding.UTF8)
    {
    }

    /// <summary>
    /// 创建一个新的INI文件读写类
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="encoding">文件编码</param>
    public IniFileEx(string fileName, Encoding encoding)
    {
        FileName = fileName;
        Encoding = encoding;
        Load();
    }

    /// <summary>
    /// 加载INI文件内容到字典中
    /// </summary>
    public void Load()
    {
        var lines = File.Exists(FileName) ? File.ReadAllLines(FileName, Encoding) : [];
        _values = ReadString(lines);
    }

    /// <summary>
    /// 保存当前内容到文件
    /// </summary>
    public void Save() => SaveAs(FileName);

    /// <summary>
    /// 将当前内容保存到指定文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    public bool SaveAs(string fileName) => SaveToFile(IniString(), fileName, Encoding);

    /// <summary>
    /// 保存字符串到文件
    /// </summary>
    /// <param name="this">字符串</param>
    /// <param name="fileName">文件名</param>
    /// <param name="encoding">文件编码，最好用Encoding.UTF8</param>
    /// <returns>是否保存成功</returns>
    private bool SaveToFile(string @this, string fileName, Encoding encoding)
    {
        if (fileName.IsNullOrEmpty()) return false;
        using var sw = new StreamWriter(fileName, false, encoding);
        sw.WriteLine(@this);
        sw.Flush();
        sw.Close();
        return File.Exists(fileName);
    }

    private string IniString()
    {
        var sb = new StringBuilder(1024);
        sb.AppendLine($";<?Ini Version=\"{Version}\" Encoding=\"{Encoding.UTF8.BodyName}\" CreateTime=\"{DateTimeOffset.Now}\"?>");
        var sections = Sections();
        foreach (string section in sections)
        {
            sb.AppendLine("");
            sb.AppendLine($"[{section}]");
            var keys = GetKeys(section);
            foreach (string key in keys)
            {
                if (TryGetValue(section, key, out var value))
                {
                    sb.AppendLine($"{key}={value}");
                }
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// 读取INI文件内容到字典中
    /// </summary>
    /// <param name="lines">INI文件的行内容</param>
    /// <returns>包含键值对的字典</returns>
    private static ConcurrentDictionary<string, string> ReadString(string[] lines)
    {
        var data = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var sectionPrefix = string.Empty;

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();

            // 忽略空行
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // 忽略注释
            if (line[0] is ';' or '#' or '/')
            {
                continue;
            }

            // [Section:header]
            if (line[0] == '[' && line[line.Length - 1] == ']')
            {
                // 移除方括号
                sectionPrefix = string.Concat(line.TrimStart('[').TrimEnd(']'), KeyDelimiter);
                continue;
            }

            // key = value 或 "value"
            var separator = line.IndexOf('=');
            if (separator < 0)
            {
                throw new FormatException("Error_UnrecognizedLineFormat");
            }

            var key = sectionPrefix + line.Left(separator).Trim();
            var value = line.Middle(separator + 1, line.Length).Trim();

            // 移除引号
            if (value.Length > 1 && value[0] == '"' && value[line.Length - 1] == '"')
            {
                value = value.Substring(1, value.Length - 2);
            }

            if (!data.TryAdd(key, value))
            {
                throw new FormatException("Error_KeyIsDuplicated");
            }
        }

        return data;
    }

    /// <summary>
    /// 写入键值对到指定节
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="value">值</param>
    public void Write(string section, string key, string value)
    {
        if (section.IsNullOrEmpty()) throw new ArgumentNullException(nameof(section), @"Section cannot be null or empty.");
        if (key.IsNullOrEmpty()) throw new ArgumentNullException(nameof(key), @"Key cannot be null or empty.");
        _values[$"{section}:{key}"] = value;
    }

    /// <summary>
    /// 从指定节读取键值对
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="defaultString">默认值</param>
    /// <returns>读取到的值</returns>
    public string ReadString(string section, string key, string defaultString = "")
    {
        if (section.IsNullOrEmpty()) throw new ArgumentNullException(nameof(section), @"Section cannot be null or empty.");
        if (key.IsNullOrEmpty()) throw new ArgumentNullException(nameof(key), @"Key cannot be null or empty.");
        return _values.TryGetValue($"{section}:{key}", out var result) ? result : defaultString;
    }

    /// <summary>
    /// 获取指定节的所有键
    /// </summary>
    /// <param name="section">节名称</param>
    /// <returns>键集合</returns>
    public ICollection GetKeys(string section)
    {
        if (section.IsNullOrEmpty()) throw new ArgumentNullException(nameof(section), @"Section cannot be null or empty.");
        var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var sectionAndKey in _values.Keys)
        {
            var parts = sectionAndKey.Split(':');
            if (!parts[0].Equals(section, StringComparison.OrdinalIgnoreCase)) continue;
            dictionary[parts[1]] = parts[1];
        }

        return dictionary.Keys;
    }

    /// <summary>
    /// 获取指定节的所有键值对
    /// </summary>
    /// <param name="section">节</param>
    /// <returns>键值对</returns>
    public NameValueCollection GetSectionValues(string section)
    {
        var values = new NameValueCollection();
        var keys = GetKeys(section);
        foreach (string key in keys)
        {
            values.Add(key, Read(section, key, ""));
        }

        return values;
    }

    /// <summary>
    /// 获取所有节
    /// </summary>
    /// <returns>节集合</returns>
    public ICollection Sections()
    {
        var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var sectionAndKey in _values.Keys)
        {
            var section = sectionAndKey.Split(':')[0];
            dictionary[section] = section;
        }

        return dictionary.Keys;
    }

    /// <summary>
    /// 删除指定节
    /// </summary>
    /// <param name="section">节名称</param>
    public void EraseSection(string section)
    {
        if (section.IsNullOrEmpty()) return;
        foreach (var key in _values.Keys)
        {
            if (key.StartsWith(section, StringComparison.OrdinalIgnoreCase))
            {
                _values.TryRemove(key, out _);
            }
        }
    }

    /// <summary>
    /// 删除指定节中的键
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <returns>是否成功删除</returns>
    public bool DeleteKey(string section, string key)
    {
        if (section.IsNullOrEmpty()) return false;
        if (key.IsNullOrEmpty()) return false;
        return _values.TryRemove($"{section}:{key}", out _);
    }

    /// <summary>
    /// 检查指定节中是否包含键
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <returns>是否包含键</returns>
    public bool ContainsKey(string section, string key)
    {
        if (section.IsNullOrEmpty()) return false;
        if (key.IsNullOrEmpty()) return false;
        return _values.ContainsKey($"{section}:{key}");
    }

    /// <summary>
    /// 检查指定节中是否包含键
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <returns>是否包含键</returns>
    public bool KeyExists(string section, string key)
    {
        if (section.IsNullOrEmpty()) return false;
        if (key.IsNullOrEmpty()) return false;
        return _values.ContainsKey($"{section}:{key}");
    }

    /// <summary>
    /// 尝试获取指定节中的键值对
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="value">输出值</param>
    /// <returns>是否成功获取</returns>
    public bool TryGetValue(string section, string key, out string value)
    {
        value = null;
        if (section.IsNullOrEmpty()) return false;
        if (key.IsNullOrEmpty()) return false;
        return _values.TryGetValue($"{section}:{key}", out value);
    }

    /// <summary>
    /// 写入对象，所支持的类型见 <see cref="StrAndObjConverter"/>
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="value">值</param>
    /// <param name="format">格式化字符串</param>
    public void Write(string section, string key, object value, string format = "")
    {
        Write(section, key, StrAndObjConverter.ObjectToString(value, format));
    }

    /// <summary>
    /// 读取对象，所支持的类型见 <see cref="StrAndObjConverter"/>
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="format">格式化字符串</param>
    /// <returns>对象</returns>
    public T Read<T>(string section, string key, T defaultValue = default, string format = "")
    {
        return StrAndObjConverter.StringToObject<T>(ReadString(section, key), defaultValue, format);
    }

    /// <summary>
    /// 读取字符串
    /// </summary>
    /// <param name="section">节名称</param>
    /// <param name="key">键名称</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public string Read(string section, string key, string defaultValue)
    {
        return ReadString(section, key, defaultValue);
    }
}

/// <summary>
/// 对象与字符串转换器
/// </summary>
internal static class StrAndObjConverter
{
    /// <summary>
    /// 字符串转换器
    /// </summary>
    public static Dictionary<Type, Func<object, string, string>> StringConverters { get; }

    /// <summary>
    /// 对象转换器
    /// </summary>
    public static Dictionary<Type, Func<string, string, object, object>> ObjectConverters { get; }

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static StrAndObjConverter()
    {
        StringConverters = [];
        StringConverters[typeof(char)] = (value, _) => value.ToString();
        StringConverters[typeof(sbyte)] = (value, _) => value.ToString();
        StringConverters[typeof(byte)] = (value, _) => value.ToString();
        StringConverters[typeof(short)] = (value, _) => value.ToString();
        StringConverters[typeof(ushort)] = (value, _) => value.ToString();
        StringConverters[typeof(int)] = (value, _) => value.ToString();
        StringConverters[typeof(uint)] = (value, _) => value.ToString();
        StringConverters[typeof(long)] = (value, _) => value.ToString();
        StringConverters[typeof(ulong)] = (value, _) => value.ToString();
        StringConverters[typeof(float)] = (value, format) => ((float)value).ToString(format, CultureInfo.InvariantCulture);
        StringConverters[typeof(double)] = (value, format) => ((double)value).ToString(format, CultureInfo.InvariantCulture);
        StringConverters[typeof(decimal)] = (value, _) => value.ToString();
        StringConverters[typeof(bool)] = (value, _) => (bool)value ? bool.TrueString : bool.FalseString;
        StringConverters[typeof(DateTime)] = (value, format) => ((DateTime)value).ToString(format);
        StringConverters[typeof(Point)] = (value, _) => $"{((Point)value).X},{((Point)value).Y}";
        StringConverters[typeof(PointF)] = (value, format) => $"{((PointF)value).X.ToString(format, CultureInfo.InvariantCulture)},{((PointF)value).Y.ToString(format, CultureInfo.InvariantCulture)}";
        StringConverters[typeof(Size)] = (value, _) => $"{((Size)value).Width},{((Size)value).Height}";
        StringConverters[typeof(SizeF)] = (value, format) => $"{((SizeF)value).Width.ToString(format, CultureInfo.InvariantCulture)},{((SizeF)value).Height.ToString(format, CultureInfo.InvariantCulture)}";
        StringConverters[typeof(Color)] = (value, _) => $"{((Color)value).A},{((Color)value).R},{((Color)value).G},{((Color)value).B}";
        StringConverters[typeof(byte[])] = (value, _) => ((byte[])value).ToHexString();
        StringConverters[typeof(Rectangle)] = (value, _) => $"{((Rectangle)value).X},{((Rectangle)value).Y},{((Rectangle)value).Width},{((Rectangle)value).Height}";
        StringConverters[typeof(RectangleF)] = (value, format) => $"{((RectangleF)value).X.ToString(format, CultureInfo.InvariantCulture)},{((RectangleF)value).Y.ToString(format, CultureInfo.InvariantCulture)},{((RectangleF)value).Width.ToString(format, CultureInfo.InvariantCulture)},{((RectangleF)value).Height.ToString(format, CultureInfo.InvariantCulture)}";
        StringConverters[typeof(Guid)] = (value, _) => value.ToString();
        StringConverters[typeof(TimeSpan)] = (value, _) => ((TimeSpan)value).Ticks.ToString();

        ObjectConverters = [];
        ObjectConverters[typeof(char)] = (value, _, obj) => value.ToChar((char)obj);
        ObjectConverters[typeof(sbyte)] = (value, _, obj) => value.ToSByte((sbyte)obj);
        ObjectConverters[typeof(byte)] = (value, _, obj) => value.ToByte((byte)obj);
        ObjectConverters[typeof(short)] = (value, _, obj) => value.ToShort((short)obj);
        ObjectConverters[typeof(ushort)] = (value, _, obj) => value.ToUShort((ushort)obj);
        ObjectConverters[typeof(int)] = (value, _, obj) => value.ToInt((int)obj);
        ObjectConverters[typeof(uint)] = (value, _, obj) => value.ToUInt((uint)obj);
        ObjectConverters[typeof(long)] = (value, _, obj) => value.ToLong((long)obj);
        ObjectConverters[typeof(ulong)] = (value, _, obj) => value.ToULong((ulong)obj);
        ObjectConverters[typeof(float)] = (value, _, obj) => value.ToFloat((float)obj);
        ObjectConverters[typeof(double)] = (value, _, obj) => value.ToDouble((double)obj);
        ObjectConverters[typeof(decimal)] = (value, _, obj) => value.ToDecimal((decimal)obj);
        ObjectConverters[typeof(bool)] = (value, _, obj) =>
        {
            if (string.Equals(value, bool.TrueString, StringComparison.OrdinalIgnoreCase)) return true;
            if (string.Equals(value, bool.FalseString, StringComparison.OrdinalIgnoreCase)) return false;
            return (bool)obj;
        };
        ObjectConverters[typeof(DateTime)] = (value, format, obj) =>
        {
            try
            {
                return value.ToDateTime(format);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return (DateTime)obj;
            }
        };
        ObjectConverters[typeof(Point)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 2 && int.TryParse(parts[0].Trim(), out var x) && int.TryParse(parts[1].Trim(), out var y)
                ? new Point(x, y) : (Point)obj;
        };
        ObjectConverters[typeof(PointF)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 2 && float.TryParse(parts[0].Trim(), out var x) && float.TryParse(parts[1].Trim(), out var y)
                ? new PointF(x, y) : (PointF)obj;
        };
        ObjectConverters[typeof(Size)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 2 && int.TryParse(parts[0].Trim(), out var width) && int.TryParse(parts[1].Trim(), out var height)
                ? new Size(width, height) : (Size)obj;
        };
        ObjectConverters[typeof(SizeF)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 2 && float.TryParse(parts[0].Trim(), out var width) && float.TryParse(parts[1].Trim(), out var height)
                ? new SizeF(width, height) : (SizeF)obj;
        };
        ObjectConverters[typeof(Color)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 4 && byte.TryParse(parts[0].Trim(), out var a) && byte.TryParse(parts[1].Trim(), out var r) &&
                   byte.TryParse(parts[2].Trim(), out var g) && byte.TryParse(parts[3].Trim(), out var b)
                ? Color.FromArgb(a, r, g, b) : (Color)obj;
        };
        ObjectConverters[typeof(byte[])] = (value, _, obj) =>
        {
            try
            {
                return value.ToHexBytes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (byte[])obj;
            }
        };
        ObjectConverters[typeof(Rectangle)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 4 && int.TryParse(parts[0].Trim(), out var x) && int.TryParse(parts[1].Trim(), out var y) &&
                   int.TryParse(parts[2].Trim(), out var width) && int.TryParse(parts[3].Trim(), out var height)
                ? new Rectangle(x, y, width, height) : (Rectangle)obj;
        };
        ObjectConverters[typeof(RectangleF)] = (value, _, obj) =>
        {
            var parts = value.Split(',');
            return parts.Length == 4 && float.TryParse(parts[0].Trim(), out var x) && float.TryParse(parts[1].Trim(), out var y) &&
                   float.TryParse(parts[2].Trim(), out var width) && float.TryParse(parts[3].Trim(), out var height)
                ? new RectangleF(x, y, width, height) : (RectangleF)obj;
        };
        ObjectConverters[typeof(Guid)] = (value, _, obj) => Guid.TryParse(value, out var result) ? result : (Guid)obj;
        ObjectConverters[typeof(TimeSpan)] = (value, _, obj) => long.TryParse(value, out var ticks) ? TimeSpan.FromTicks(ticks) : (TimeSpan)obj;
    }

    /// <summary>
    /// 将对象转换为字符串
    /// </summary>
    /// <param name="value">对象</param>
    /// <param name="format">格式</param>
    /// <returns>字符串</returns>
    public static string ObjectToString(object value, string format = "")
    {
        if (StringConverters.TryGetValue(value.GetType(), out var func))
            return func(value, format);
        else
            throw new NotSupportedException(nameof(value));
    }

    /// <summary>
    /// 将字符串转换为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">字符串</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="format">格式</param>
    /// <returns>对象</returns>
    public static T StringToObject<T>(string value, object defaultValue, string format = "")
    {
        if (ObjectConverters.TryGetValue(typeof(T), out var func))
            return (T)func(value, format, defaultValue);
        else
            throw new NotSupportedException(nameof(value));
    }
}