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
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Sunny.UI
{
    public class IniFileEx
    {
        private readonly Dictionary<string, NameValueCollection> data = new Dictionary<string, NameValueCollection>();

        private static readonly Regex regRemoveEmptyLines =
            new Regex
            (
                @"(\s*;[\d\D]*?\r?\n)+|\r?\n(\s*\r?\n)*",
                RegexOptions.Multiline | RegexOptions.Compiled
            );

        private static readonly Regex regParseIniData =
            new Regex
            (
                @"
                (?<IsSection>
                    ^\s*\[(?<SectionName>[^\]]+)?\]\s*$
                )
                |
                (?<IsKeyValue>
                    ^\s*(?<Key>[^(\s*\=\s*)]+)?\s*\=\s*(?<Value>[\d\D]*)$
                )",
                RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace
            );

        public IniFileEx(string fileName) : this(fileName, Encoding.Default) { }

        public IniFileEx(string fileName, Encoding encoding)
        {
            FileName = fileName;
            Encoding = encoding;
            using FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            ReadIniData(fs, encoding);
        }

        /// <summary>
        /// 文件名
        /// </summary>
        [Description("文件名")]
        public string FileName { get; set; } //INI文件名

        public Encoding Encoding { get; set; }

        private void ReadIniData(Stream stream, Encoding encoding)
        {
            string lastSection = string.Empty;
            data.Add(lastSection, new NameValueCollection());
            if (stream != null && encoding != null)
            {
                using StreamReader reader = new StreamReader(stream, encoding);
                string iniData = reader.ReadToEnd();

                iniData = regRemoveEmptyLines.Replace(iniData, "\n");
                string[] lines = iniData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in lines)
                {
                    Match m = regParseIniData.Match(s);
                    if (m.Success)
                    {
                        if (m.Groups["IsSection"].Length > 0)
                        {
                            string sName = m.Groups["SectionName"].Value.ToLowerInvariant();
                            if (lastSection != sName)
                            {
                                lastSection = sName;
                                if (!data.ContainsKey(sName))
                                {
                                    data.Add(sName, new NameValueCollection());
                                }
                            }
                        }
                        else if (m.Groups["IsKeyValue"].Length > 0)
                        {
                            data[lastSection].Add(m.Groups["Key"].Value, m.Groups["Value"].Value);
                        }
                    }
                }
            }
        }

        public NameValueCollection this[string section]
        {
            get
            {
                section = section.ToLowerInvariant();
                if (!data.ContainsKey(section))
                    data.Add(section, new NameValueCollection());
                return data[section];
            }
        }

        public string this[string section, string key]
        {
            get => this[section][key];
            set => this[section][key] = value;
        }

        public object this[string section, string key, Type t]
        {
            get
            {
                if (t == null || t == (Type)Type.Missing)
                    return this[section][key];
                return GetValue(section, key, null, t);
            }
            set
            {
                if (t == null || t == (Type)Type.Missing)
                    this[section][key] = string.Empty;
                else
                    SetValue(section, key, value);
            }
        }

        public string[] KeyNames(string section)
        {
            return this[section].AllKeys;
        }

        public string[] SectionValues(string section)
        {
            return this[section].GetValues(0);
        }

        private object GetValue(string section, string key, object defaultValue, Type t)
        {
            section = section.ToLowerInvariant();
            key = key.ToLowerInvariant();
            if (!data.ContainsKey(section)) return defaultValue;
            string v = data[section][key];
            if (string.IsNullOrEmpty(v)) return defaultValue;
            TypeConverter conv = TypeDescriptor.GetConverter(t);
            if (!conv.CanConvertFrom(typeof(string))) return defaultValue;

            try
            {
                return conv.ConvertFrom(v);
            }
            catch
            {
                return defaultValue;
            }
        }

        private T GetValue<T>(string section, string key, T defaultValue)
        {
            return (T)GetValue(section, key, defaultValue, typeof(T));
        }

        private void SetValue(string section, string key, object value)
        {
            if (value == null)
            {
                this[section][key] = string.Empty;
            }
            else
            {
                TypeConverter conv = TypeDescriptor.GetConverter(value);
                if (!conv.CanConvertTo(typeof(string)))
                {
                    this[section][key] = value.ToString();
                }
                else
                {
                    this[section][key] = (string)conv.ConvertTo(value, typeof(string));
                }
            }

            UpdateFile();
        }

        public void Write(string section, string key, string value)
        {
            SetValue(section, key, value);
        }

        public string Read(string section, string key, string Default)
        {
            return GetValue(section, key, Default);
        }

        /// <summary>
        /// 读取指定的Section的所有Value到列表中
        /// </summary>
        /// <param name="section">section</param>
        public NameValueCollection GetSectionValues(string section)
        {
            return this[section];
        }

        public void UpdateFile()
        {
            Save();
        }

        public void Save()
        {
            Save(FileName, Encoding);
        }

        public void Save(string fileName, Encoding encoding)
        {
            using FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            Save(fs, encoding);
        }

        private void Save(Stream stream, Encoding encoding)
        {
            using StreamWriter sw = new StreamWriter(stream, encoding);
            foreach (var cur in data)
            {
                if (!string.IsNullOrEmpty(cur.Key))
                {
                    sw.WriteLine("[{0}]", cur.Key);
                }

                NameValueCollection col = cur.Value;
                foreach (string key in col.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        string value = col[key];
                        if (!string.IsNullOrEmpty(value))
                            sw.WriteLine("{0}={1}", key, value);
                    }
                }
            }

            sw.Flush();
        }

        public bool HasSection(string section)
        {
            return data.ContainsKey(section.ToLowerInvariant());
        }

        public bool HasKey(string section, string key)
        {
            return
                data.ContainsKey(section) &&
                !string.IsNullOrEmpty(data[section][key]);
        }

        /// <summary>
        /// 写结构
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <typeparam name="T">T</typeparam>
        public void WriteStruct<T>(string section, string key, T value) where T : struct
        {
            Write(section, key, value.ToBytes());
        }

        /// <summary>
        /// 读结构
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public T ReadStruct<T>(string section, string key, T Default) where T : struct
        {
            //得到结构体的大小
            int size = StructEx.Size(Default);
            byte[] bytes = Read(section, key, "").ToHexBytes();
            return size > bytes.Length ? Default : bytes.ToStruct<T>();
        }

        /// <summary>
        /// 写Byte数组
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, byte[] value)
        {
            Write(section, key, value.ToHexString());
        }

        /// <summary>
        /// 读Byte数组
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public byte[] ReadBytes(string section, string key, byte[] Default)
        {
            return Read(section, key, Default.ToHexString()).ToHexBytes();
        }

        /// <summary>
        /// 写Char
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, char value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读Char
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public char ReadChar(string section, string key, char Default = ' ')
        {
            return Read(section, key, Default.ToString()).ToChar(Default);
        }

        /// <summary>
        /// 写Decimal
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, decimal value)
        {
            Write(section, key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 读Decimal
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public decimal ReadDecimal(string section, string key, decimal Default = 0)
        {
            return Read(section, key, Default.ToString(CultureInfo.InvariantCulture)).ToDecimal(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, short value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public short ReadShort(string section, string key, short Default = 0)
        {
            return Read(section, key, Default.ToString()).ToShort(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, ushort value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public ushort ReadUShort(string section, string key, ushort Default = 0)
        {
            return Read(section, key, Default.ToString()).ToUShort(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, int value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public int ReadInt(string section, string key, int Default = 0)
        {
            return Read(section, key, Default.ToString()).ToInt(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, uint value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public uint ReadUInt(string section, string key, uint Default = 0)
        {
            return Read(section, key, Default.ToString()).ToUInt(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, ulong value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public ulong ReadULong(string section, string key, ulong Default = 0)
        {
            return Read(section, key, Default.ToString()).ToULong(Default);
        }

        /// <summary>
        /// 写整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, long value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读整数
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public long ReadLong(string section, string key, long Default = 0)
        {
            return Read(section, key, Default.ToString()).ToLong(Default);
        }

        /// <summary>
        /// 写布尔
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, bool value)
        {
            Write(section, key, value ? bool.TrueString : bool.FalseString);
        }

        /// <summary>
        /// 读布尔
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public bool ReadBool(string section, string key, bool Default = false)
        {
            string str = Read(section, key, Default.ToString());
            if (string.Equals(str, bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            if (string.Equals(str, bool.FalseString, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return Default;
        }

        /// <summary>
        /// 写Double
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, double value)
        {
            Write(section, key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 读Double
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public double ReadDouble(string section, string key, double Default = 0)
        {
            return Read(section, key, Default.ToString(CultureInfo.InvariantCulture)).ToDouble(Default);
        }

        /// <summary>
        /// 写Float
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, float value)
        {
            Write(section, key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 读Float
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public float ReadFloat(string section, string key, float Default = 0)
        {
            return Read(section, key, Default.ToString(CultureInfo.InvariantCulture)).ToFloat(Default);
        }

        /// <summary>
        /// 写Byte
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, byte value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读Byte
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public byte ReadByte(string section, string key, byte Default = 0)
        {
            return Read(section, key, Default.ToString()).ToByte(Default);
        }

        /// <summary>
        /// 写SByte
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, sbyte value)
        {
            Write(section, key, value.ToString());
        }

        /// <summary>
        /// 读Byte
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public sbyte ReadSByte(string section, string key, sbyte Default = 0)
        {
            return Read(section, key, Default.ToString()).ToSByte(Default);
        }

        /// <summary>
        /// 写DateTime
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, DateTime value)
        {
            Write(section, key, value.ToString(DateTimeEx.DateTimeFormat));
        }

        /// <summary>
        /// 读DateTime
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public DateTime ReadDateTime(string section, string key, DateTime Default)
        {
            string str = Read(section, key, Default.ToString(CultureInfo.InvariantCulture));
            try
            {
                return str.ToDateTime(DateTimeEx.DateTimeFormat);
            }
            catch (Exception)
            {
                return Default;
            }
        }

        /// <summary>
        /// 写Point
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, Point value)
        {
            Write(section, key, ConvertEx.ObjectToString(value, typeof(Point)));
        }

        /// <summary>
        /// 读Point
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public Point ReadPoint(string section, string key, Point Default)
        {
            string str = Read(section, key, "");
            return (Point)ConvertEx.StringToObject(str, typeof(Point), Default);
        }

        /// <summary>
        /// 写PointF
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, PointF value)
        {
            Write(section, key, ConvertEx.ObjectToString(value, typeof(PointF)));
        }

        /// <summary>
        /// 读PointF
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public PointF ReadPointF(string section, string key, PointF Default)
        {
            string str = Read(section, key, "");
            return (PointF)ConvertEx.StringToObject(str, typeof(PointF), Default);
        }

        /// <summary>
        /// 写Size
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, Size value)
        {
            Write(section, key, ConvertEx.ObjectToString(value, typeof(Size)));
        }

        /// <summary>
        /// 读Size
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public Size ReadSize(string section, string key, Size Default)
        {
            string str = Read(section, key, "");
            return (Size)ConvertEx.StringToObject(str, typeof(Size), Default);
        }

        /// <summary>
        /// 写SizeF
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, SizeF value)
        {
            Write(section, key, ConvertEx.ObjectToString(value, typeof(SizeF)));
        }

        /// <summary>
        /// 读SizeF
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public SizeF ReadSizeF(string section, string key, SizeF Default)
        {
            string str = Read(section, key, "");
            return (SizeF)ConvertEx.StringToObject(str, typeof(SizeF), Default);
        }

        /// <summary>
        /// 写Color
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, Color value)
        {
            Write(section, key, ConvertEx.ObjectToString(value, typeof(Color)));
        }

        /// <summary>
        /// 读Color
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public Color ReadColor(string section, string key, Color Default)
        {
            string str = Read(section, key, "");
            return (Color)ConvertEx.StringToObject(str, typeof(Color), Default);
        }
    }
}
