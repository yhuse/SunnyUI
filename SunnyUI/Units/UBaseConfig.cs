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
 * 文件名称: UBaseConfig.cs
 * 文件说明: 配置文件基类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Sunny.UI
{
    /// <summary>
    /// 配置文件基类
    /// </summary>
    /// <typeparam name="TConfig">类型</typeparam>
    public abstract class BaseConfig<TConfig> where TConfig : BaseConfig<TConfig>, new()
    {
        /// <summary>
        /// 实体对象
        /// </summary>
        protected static TConfig current;

        /// <summary>
        /// 配置文件描述
        /// </summary>
        public static string Description
        {
            get
            {
                var fileanddesc = ConfigHelper.GetConfigFile<TConfig>();
                return fileanddesc.Item2;
            }
        }

        /// <summary>
        /// 配置文件名
        /// </summary>
        public static string ConfigFile
        {
            get
            {
                var fileanddesc = ConfigHelper.GetConfigFile<TConfig>();
                return fileanddesc.Item1;
            }
        }

        /// <summary>
        /// 当前实例。通过置空可以使其重新加载。
        /// </summary>
        public static TConfig Current
        {
            get
            {
                if (current != null)
                {
                    return current;
                }

                // 现在没有对象，尝试加载，若返回null则实例化一个新的
                current = CreateNew();
                if (!current.Load(DirEx.CurrentDir() + ConfigFile))
                {
                    try
                    {
                        // 根据配置，有可能不保存，直接返回默认
                        current.Save();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return current;
            }

            set => current = value;
        }

        private static TConfig CreateNew()
        {
            TConfig config = new TConfig();
            config.SetDefault();
            return config;
        }

        /// <summary>保存到配置文件中去</summary>
        public void Save()
        {
            Save(null);
        }

        /// <summary>
        /// 加载指定配置文件
        /// </summary>
        /// <returns>结果</returns>
        public bool Load()
        {
            return Load(null);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>加密后字符串</returns>
        public string Encrypt(string str)
        {
            string tmp = str + RandomEx.RandomNumber(6);
            return tmp.DesEncrypt();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解密后字符串</returns>
        public string Decrypt(string str)
        {
            string result = str.DesDecrypt();
            return result.Left(result.Length - 6);
        }

        /// <summary>加载指定配置文件</summary>
        /// <param name="filename">文件名</param>
        /// <returns>结果</returns>
        public abstract bool Load(string filename);

        /// <summary>保存到配置文件中去</summary>
        /// <param name="filename">文件名</param>
        public abstract void Save(string filename);

        /// <summary>
        /// 设置默认值
        /// </summary>
        public virtual void SetDefault()
        {
        }
    }

    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <typeparam name="TConfig">类型</typeparam>
        /// <returns>文件名</returns>
        public static Tuple<string, string> GetConfigFile<TConfig>()
        {
            string filename;
            string description = "配置文件";
            var att = typeof(TConfig).GetCustomAttribute<ConfigFileAttribute>();
            if (att == null || att.FileName.IsNullOrWhiteSpace())
            {
                filename = $"{typeof(TConfig).Name}.cfg";
            }
            else
            {
                filename = att.FileName;

                if (!att.Description.IsNullOrEmpty())
                {
                    description = att.Description;
                }
            }

            return new Tuple<string, string>(filename, description);
        }

        /// <summary>
        /// 初始化属性
        /// </summary>
        /// <param name="config">配置类</param>
        /// <typeparam name="TConfig">类型</typeparam>
        /// <returns>节点</returns>
        /// <exception cref="ApplicationException">ApplicationException</exception>
        public static ConcurrentDictionary<string, Ident> InitIdents<TConfig>(TConfig config)
        {
            ConcurrentDictionary<string, Ident> idents = new ConcurrentDictionary<string, Ident>();
            var list = config.GetType().GetNeedProperties();
            foreach (PropertyInfo info in list)
            {
                Ident ident = new Ident
                {
                    Key = info.Name,
                    Show = true,
                    Description = info.Description(),
                    IsList = info.PropertyType.IsList()
                };

                var section = info.GetCustomAttribute<ConfigSectionAttribute>();
                ident.Section = section != null ? section.Section : "";

                var propertyatt = info.GetCustomAttribute<ConfigPropertyAttribute>();
                ident.Caption = propertyatt != null ? propertyatt.Caption : "";
                ident.Unit = propertyatt != null ? propertyatt.Unit : "";
                ident.Description = propertyatt != null ? propertyatt.Description : "";

                var indexatt = info.GetCustomAttribute<ConfigIndexAttribute>();
                ident.Index = indexatt?.Index ?? short.MaxValue - idents.Count;
                ident.Show = indexatt == null || indexatt.Show;

                if (ident.Description.IsNullOrEmpty())
                {
                    ident.Description = info.DisplayName() ?? info.Description();
                }

                if (ident.Description.IsNullOrEmpty())
                {
                    ident.Description = "";
                }

                if (!idents.ContainsKey(ident.Key))
                {
                    idents.TryAdd(ident.Key, ident);
                }
            }

            return idents;
        }

        /// <summary>
        /// 类型默认值
        /// </summary>
        /// <param name="targetType">类型</param>
        /// <returns>默认值</returns>
        public static object DefaultValue(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        /// <summary>
        /// 读取对象数值
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <param name="idents">节点</param>
        /// <typeparam name="TConfig">类型</typeparam>
        /// <exception cref="ApplicationException">ApplicationException</exception>
        public static void LoadConfigValue<TConfig>(TConfig config, ConcurrentDictionary<string, Ident> idents)
        {
            var list = config.GetType().GetNeedProperties();
            foreach (PropertyInfo info in list)
            {
                object defaultobj = info.GetValue(config, null);
                Type type = info.PropertyType;

                if (type == typeof(string))
                {
                    object value = idents[info.Name].Value;
                    info.SetValue(config, Convert.ChangeType(value==null? defaultobj: value, type), null);
                    continue;
                }

                if (ConvertEx.CanConvent(type))
                {
                    object value = ConvertEx.StringToObject(idents[info.Name].Value, type, defaultobj);
                    info.SetValue(config, Convert.ChangeType(value, type), null);
                    continue;
                }

                if (type.IsList())
                {
                    Type[] genericTypes = type.GetGenericArguments();
                    if (genericTypes.Length != 1)
                    {
                        throw new ApplicationException("转换出错: " + type.FullName);
                    }

                    Type childtype = genericTypes[0];
                    Type typeDataList = typeof(List<>).MakeGenericType(childtype);
                    MethodInfo methodInfo = typeDataList.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                    if (methodInfo == null)
                    {
                        continue;
                    }

                    object listvalue = typeDataList.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, new object[] { });

                    foreach (string strvalue in idents[info.Name].Values)
                    {
                        if (childtype == typeof(string))
                        {
                            object value = strvalue;
                            methodInfo.Invoke(listvalue, new[] { value });
                        }
                        else if (ConvertEx.CanConvent(childtype))
                        {
                            object value = ConvertEx.StringToObject(strvalue, childtype, DefaultValue(childtype));
                            methodInfo.Invoke(listvalue, new[] { value });
                        }
                    }

                    info.SetValue(config, listvalue, null);
                    continue;
                }

                throw new ApplicationException("不支持此类型: " + type.FullName);
            }
        }

        /// <summary>
        /// 设置对象数值
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <param name="idents">节点</param>
        /// <typeparam name="TConfig">类型</typeparam>
        /// <exception cref="ApplicationException">ApplicationException</exception>
        public static void SaveConfigValue<TConfig>(TConfig config, ConcurrentDictionary<string, Ident> idents)
        {
            var list = config.GetType().GetNeedProperties();
            foreach (PropertyInfo info in list)
            {
                object obj = info.GetValue(config, null);
                Type type = info.PropertyType;

                if (ConvertEx.CanConvent(type))
                {
                    string value = ConvertEx.ObjectToString(obj, type);
                    idents[info.Name].Value = value;
                    continue;
                }

                if (type.IsList())
                {
                    Type[] genericTypes = type.GetGenericArguments();
                    if (genericTypes.Length != 1)
                    {
                        throw new ApplicationException("转换出错: " + type.FullName);
                    }

                    Type childtype = genericTypes[0];
                    IEnumerable en = obj as IEnumerable;
                    if (en == null)
                    {
                        throw new ApplicationException("转换出错: " + type.FullName);
                    }

                    idents[info.Name].Values.Clear();
                    foreach (object child in en)
                    {
                        if (ConvertEx.CanConvent(childtype))
                        {
                            idents[info.Name].Values.Add(ConvertEx.ObjectToString(child, childtype));
                        }
                        else
                        {
                            throw new ApplicationException("不支持此类型: " + type.FullName);
                        }
                    }

                    continue;
                }

                throw new ApplicationException("不支持此类型: " + type.FullName);
            }
        }
    }

    /// <summary>配置文件特性</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigFileAttribute : Attribute
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 配置文件
        /// </summary>
        public ConfigFileAttribute() : this(string.Empty, string.Empty)
        {
        }

        /// <summary>配置文件</summary>
        /// <param name="fileName">文件名</param>
        /// <param name="description">描述</param>
        public ConfigFileAttribute(string fileName, string description = "")
        {
            FileName = fileName;
            Description = description;
        }
    }

    /// <summary>
    /// 数据节点
    /// </summary>
    public class Ident
    {
        /// <summary>
        /// 节
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Index + " [" + Section + "] " + Key + " = ?";
        }

        /// <summary>
        /// 是否是列表类型
        /// </summary>
        public bool IsList { get; set; }

        /// <summary>
        /// 列表值
        /// </summary>
        public List<string> Values = new List<string>();
    }

    /// <summary>
    /// 配置属性节点特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigSectionAttribute : Attribute
    {
        /// <summary>
        /// 节
        /// </summary>
        public string Section { get; }

        /// <summary>属性节点特性</summary>
        /// <param name="section">节</param>
        public ConfigSectionAttribute(string section)
        {
            Section = section;
        }
    }

    /// <summary>
    /// 配置属性描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigPropertyAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 配置文件属性
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="unit">单位</param>
        /// <param name="description">描述</param>
        public ConfigPropertyAttribute(string caption, string description = "", string unit = "")
        {
            Caption = caption;
            Unit = unit;
            Description = description;
        }
    }

    /// <summary>
    /// 忽略此属性的配置存储
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigIgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// 配置序号描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigIndexAttribute : Attribute
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Show { get; }

        /// <summary>配置序号特性</summary>
        /// <param name="index">序号</param>
        /// <param name="show">是否显示</param>
        public ConfigIndexAttribute(int index, bool show = true)
        {
            Index = index;
            Show = show;
        }
    }
}