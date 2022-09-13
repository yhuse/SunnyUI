/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIniFile.cs
 * 文件说明: INI 文件读取类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-09-13: V3.2.4 修改IniFile，改WinApi读取为直接C#代码读取
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// INI 文件读取基类
    /// </summary>
    public abstract class IniBase : IDisposable
    {
        public IniBase(string fileName) : this(fileName, Encoding.Unicode) { }

        public IniBase(string fileName, Encoding encoding)
        {
            Load(fileName, encoding);
        }

        public void Dispose()
        {
            Save();
        }

        private IDictionary<string, IDictionary<string, object>> ini;

        public string FileName { get; private set; }

        private Encoding Encoding;

        public void Load(string fileName)
        {
            Load(fileName, Encoding.Unicode);
        }

        public void Load(string fileName, Encoding encoding)
        {
            FileName = fileName;
            Encoding = encoding;

            if (!File.Exists(fileName))
            {
                ini = CreateDefault();
                return;
            }

            using (var sr = new StreamReader(fileName, encoding))
            {
                ini = Read(sr);
            }
        }

        public void Save()
        {
            File.WriteAllText(FileName, ToString(), Encoding);
        }

        public void SaveAs(string fileName, Encoding encoding)
        {
            File.WriteAllText(fileName, ToString(), encoding);
        }

        public void Write(string section, string key, string value)
        {
            if (!ini.ContainsKey(section))
            {
                ini.Add(section, new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase));
            }

            if (ini[section].ContainsKey(key))
            {
                ini[section][key] = value;
            }
            else
            {
                ini[section].Add(key, value);
            }

            if (saveNow)
            {
                Save();
            }
        }

        private bool saveNow = true;
        public void BeginUpdate()
        {
            saveNow = false;
        }

        public void EndUpdate()
        {
            saveNow = true;
            Save();
        }

        public string ReadString(string section, string key, string Default = "")
        {
            return Read(section, key, Default);
        }

        protected string Read(string section, string key, string Default = "")
        {
            if (ini.ContainsKey(section) && ini[section].ContainsKey(key))
            {
                return ini[section][key].ToString();
            }

            return Default;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var sentry in ini)
            {
                sb.AppendLine("[" + sentry.Key + "]");
                var d = sentry.Value;
                foreach (var entry in d)
                {
                    if (entry.Value is IList<string>)
                    {
                        var l = ((IList<string>)entry.Value);
                        sb.AppendLine(string.Format("{0}={1}", entry.Key, l[0]));
                        for (var i = 1; i < l.Count; ++i)
                        {
                            sb.AppendLine("\t" + l[i]);
                        }

                        sb.AppendLine();
                    }
                    else
                    {
                        sb.AppendLine(string.Format("{0}={1}", entry.Key, entry.Value));
                    }
                }
            }

            return sb.ToString();
        }

        private IDictionary<string, IDictionary<string, object>> CreateDefault()
        {
            return new Dictionary<string, IDictionary<string, object>>(StringComparer.OrdinalIgnoreCase);
        }

        private IDictionary<string, IDictionary<string, object>> Read(TextReader reader)
        {
            var result = CreateDefault();

            int lc = 1;
            string section = "";
            string name = null;
            string line;
            while (null != (line = reader.ReadLine()))
            {
                var i = line.IndexOf(';');
                if (i > -1)
                {
                    line = line.Substring(0, i);
                }

                line = line.Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Length > 2 && line[0] == '[' && line[line.Length - 1] == ']')
                    {
                        section = line.Substring(1, line.Length - 2);
                        if (!result.ContainsKey(section))
                        {
                            result.Add(section, new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase));
                        }
                    }
                    else
                    {
                        var d = result[section];
                        i = line.IndexOf('=');
                        if (i > -1)
                        {
                            name = line.Substring(0, i).TrimEnd();
                            if (d.TryGetValue(name, out object o))
                            {
                                AddIniEntryValue(d, line, name, i, o);
                            }
                            else
                            {
                                d.Add(name, line.Substring(i + 1).TrimStart());
                            }
                        }
                        else if (null == name)
                        {
                            throw new IOException("Invalid INI file at line " + lc.ToString());
                        }
                        else
                        {
                            var o = d[name];
                            AddIniEntryValue(d, line, name, i, o);
                        }
                    }
                }

                ++lc;
            }

            return result;
        }

        private void AddIniEntryValue(IDictionary<string, object> d, string line, string name, int i, object o)
        {
            if (o is string)
            {
                var s = (string)o;
                d.Remove(name);
                var col = new List<string>();
                d.Add(name, col);
                col.Add(s);
                col.Add(line.Substring(i + 1).TrimStart());
            }
            else
            {
                ((List<string>)o).Add(line.Substring(i + 1).TrimStart());
            }
        }

        /// <summary>
        /// 获取指定的Section名称中的所有Key
        /// </summary>
        /// <param name="section">section</param>
        /// <returns>结果</returns>
        public string[] GetKeys(string section)
        {
            return ini.ContainsKey(section) && ini[section].Count > 0 ? (string[])ini[section].Keys : new string[0];
        }

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        /// <returns></returns>
        public string[] Sections()
        {
            return ini != null ? (string[])ini.Keys : new string[0];
        }

        /// <summary>
        /// 读取指定的Section的所有Value到列表中
        /// </summary>
        /// <param name="section">section</param>
        public NameValueCollection GetSectionValues(string section)
        {
            NameValueCollection values = new NameValueCollection();
            string[] keys = GetKeys(section);
            foreach (string key in keys)
            {
                values.Add(key, ini[section][key].ToString());
            }

            return values;
        }

        /// <summary>
        /// 清除某个Section
        /// </summary>
        /// <param name="section">section</param>
        public void EraseSection(string section)
        {
            if (ini.ContainsKey(section))
            {
                ini.Remove(section);
                Save();
            }
        }

        /// <summary>
        /// 删除某个Section下的键
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        public void DeleteKey(string section, string key)
        {
            if (KeyExists(section, key))
            {
                ini[section].Remove(key);
                if (ini[section].Count == 0)
                {
                    ini.Remove(section);
                }

                Save();
            }
        }

        /// <summary>
        /// 检查某个Section下的某个键值是否存在
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>结果</returns>
        public bool KeyExists(string section, string key)
        {
            return ini.ContainsKey(section) && ini[section].ContainsKey(key);
        }
    }

    /// <summary>
    /// IniFile的类
    /// </summary>
    public class IniFile : IniBase
    {
        public IniFile(string fileName) : base(fileName, Encoding.Unicode) { }

        public IniFile(string fileName, Encoding encoding) : base(fileName, encoding) { }

        /// <summary>
        /// 写结构
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <typeparam name="T">T</typeparam>
        public void WriteStruct<T>(string section, string key, T value) where T : struct
        {
            Write(section, key, value.StructToBytes());
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
            int size = Default.Size();
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