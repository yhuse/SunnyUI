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
 * 文件名称: UIniFile.cs
 * 文件说明: INI 文件读取类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-11-01: V3.2.6 增加读取字符串长度到4096，增加文件编码
 * 2023-07-07: V3.3.9 将文件版本和文件编码写入文件头部
 * 2023-08-11: v3.4.1 增加了文件绝对路径判断和文件夹是否存在判断
******************************************************************************/

using Sunny.UI.Win32;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// INI 文件读取基类
    /// </summary>
    public abstract class IniBase : IDisposable
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [Description("文件名")]
        public string FileName { get; set; } //INI文件名

        /// <summary>
        /// Ini文件编码格式
        /// </summary>
        public Encoding IniEncoding { get; set; } = Encoding.Default;

        /// <summary>
        /// 类的构造函数，文件名必须是完全路径，不能是相对路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        public IniBase(string fileName)
        {
            if (!fileName.Contains(":"))
            {
                throw new ArgumentException("The file name must be an absolute path.");
            }

            //必须是完全路径，不能是相对路径
            FileName = fileName;

            FileInfo fi = new FileInfo(FileName);
            DirEx.CreateDir(fi.DirectoryName);

            if (!Directory.Exists(fi.DirectoryName))
            {
                throw new ArgumentException("Folder does not exist: " + fi.DirectoryName);
            }

            // 判断文件是否存在
            if (!File.Exists(fileName))
            {
                //文件不存在，建立文件
                using (StreamWriter sw = new StreamWriter(fileName, false, IniEncoding))
                {
                    sw.WriteLine(";<?ini version=\"" + UIGlobal.Version + "\" encoding=\"" + IniEncoding.BodyName + "\"?>");
                    sw.WriteLine("");
                }
            }
        }

        public IniBase(string fileName, Encoding encoding) : this(fileName)
        {
            IniEncoding = encoding;
        }

        /// <summary>
        /// 确保资源的释放
        /// </summary>
        ~IniBase()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            UpdateFile();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>结果</returns>
        public bool Write(string section, string key, string value)
        {
            if (value == null)
            {
                value = "";
            }

            return Kernel.WritePrivateProfileString(IniEncoding.GetBytes(section), IniEncoding.GetBytes(key), IniEncoding.GetBytes(value), FileName);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public string ReadString(string section, string key, string Default)
        {
            return Read(section, key, Default);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="Default">Normal</param>
        /// <returns>结果</returns>
        public string Read(string section, string key, string Default)
        {
            byte[] buffer = new byte[4096];
            if (Default == null)
            {
                Default = "";
            }

            int bufLen = Kernel.GetPrivateProfileString(IniEncoding.GetBytes(section), IniEncoding.GetBytes(key), IniEncoding.GetBytes(Default), buffer, buffer.Length, FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            return IniEncoding.GetString(buffer, 0, bufLen).Trim();
        }

        /// <summary>
        /// 获取指定的Section名称中的所有Key
        /// </summary>
        /// <param name="section">section</param>
        /// <returns>结果</returns>
        public string[] GetKeys(string section)
        {
            StringCollection keyList = new StringCollection();
            GetKeys(section, keyList);
            return keyList.Cast<string>().ToArray();
        }

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        public string[] Sections
        {
            get
            {
                StringCollection keyList = new StringCollection();
                GetSections(keyList);
                return keyList.Cast<string>().ToArray();
            }
        }

        /// <summary>
        /// 从Ini文件中，将指定的Section名称中的所有Key添加到列表中
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="keys">keys</param>
        private void GetKeys(string section, StringCollection keys)
        {
            byte[] buffer = new byte[65535];
            int bufLen = Kernel.GetPrivateProfileString(IniEncoding.GetBytes(section), null, null, buffer, 65535, FileName);
            //对Section进行解析
            GetStringsFromBuffer(buffer, bufLen, keys);
        }

        private void GetStringsFromBuffer(byte[] buffer, int bufLen, StringCollection strings)
        {
            strings.Clear();
            if (bufLen == 0)
            {
                return;
            }

            int start = 0;
            for (int i = 0; i < bufLen; i++)
            {
                if ((buffer[i] == 0) && ((i - start) > 0))
                {
                    string s = IniEncoding.GetString(buffer, start, i - start);
                    strings.Add(s);
                    start = i + 1;
                }
            }
        }

        /// <summary>
        /// 从Ini文件中，读取所有的Sections的名称
        /// </summary>
        /// <param name="sectionList">sectionList</param>
        private void GetSections(StringCollection sectionList)
        {
            //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
            byte[] buffer = new byte[65535];
            int bufLen = Kernel.GetPrivateProfileString(null, null, null, buffer, buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(buffer, bufLen, sectionList);
        }

        /// <summary>
        /// 读取指定的Section的所有Value到列表中
        /// </summary>
        /// <param name="section">section</param>
        public NameValueCollection GetSectionValues(string section)
        {
            NameValueCollection values = new NameValueCollection();
            StringCollection keyList = new StringCollection();
            GetKeys(section, keyList);
            values.Clear();
            foreach (string key in keyList)
            {
                values.Add(key, Read(section, key, ""));
            }

            return values;
        }

        /// <summary>
        /// 清除某个Section
        /// </summary>
        /// <param name="section">section</param>
        public void EraseSection(string section)
        {
            if (!Kernel.WritePrivateProfileString(IniEncoding.GetBytes(section), null, null, FileName))
            {
                throw (new ApplicationException("无法清除Ini文件中的Section"));
            }
        }

        /// <summary>
        /// 删除某个Section下的键
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        public void DeleteKey(string section, string key)
        {
            Kernel.WritePrivateProfileString(IniEncoding.GetBytes(section), IniEncoding.GetBytes(key), null, FileName);
        }

        /// <summary>
        /// Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
        /// 在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
        /// 执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
        /// </summary>
        public void UpdateFile()
        {
            Kernel.WritePrivateProfileString(null, null, null, FileName);
        }

        /// <summary>
        /// 检查某个Section下的某个键值是否存在
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>结果</returns>
        public bool KeyExists(string section, string key)
        {
            StringCollection keys = new StringCollection();
            GetKeys(section, keys);
            return keys.IndexOf(key) > -1;
        }
    }

    /// <summary>
    /// IniFile的类
    /// </summary>
    public class IniFile : IniBase
    {
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

        /// <summary>
        /// 类的构造函数，文件名必须是完全路径，不能是相对路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        public IniFile(string fileName) : base(fileName)
        {
        }

        /// <summary>
        /// 类的构造函数，文件名必须是完全路径，不能是相对路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">文件编码</param>
        public IniFile(string fileName, Encoding encoding) : base(fileName, encoding)
        {

        }
    }
}