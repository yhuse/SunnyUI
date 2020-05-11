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
 * 文件名称: UIniConfig.cs
 * 文件说明: INI 配置文件类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace Sunny.UI
{
    /// <summary>
    /// INI 配置文件类
    /// </summary>
    /// <typeparam name="TConfig">类型</typeparam>
    public class IniConfig<TConfig> : BaseConfig<TConfig> where TConfig : IniConfig<TConfig>, new()
    {
        #region 加载

        /// <summary>加载指定配置文件</summary>
        /// <param name="filename">文件名</param>
        /// <returns>结果</returns>
        public override bool Load(string filename)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                filename = DirEx.CurrentDir() + ConfigFile;
            }

            if (filename.IsNullOrWhiteSpace())
            {
                throw new ApplicationException($"未指定{typeof(TConfig).Name}的配置文件路径！");
            }

            if (!File.Exists(filename))
            {
                return false;
            }

            try
            {
                ConcurrentDictionary<string, Ident> idents = ConfigHelper.InitIdents(current);
                foreach (var ident in idents.Values)
                {
                    if (ident.Section.IsNullOrEmpty())
                    {
                        ident.Section = "Setup";
                    }
                }

                IniFile ini = new IniFile(filename);
                foreach (var ident in idents.Values)
                {
                    if (ident.IsList)
                    {
                        ident.Values.Clear();
                        NameValueCollection list = new NameValueCollection();
                        ini.GetSectionValues(ident.Section + "-" + ident.Key, list);
                        foreach (var pair in list)
                        {
                            ident.Values.Add(ini.Read(ident.Section + "-" + ident.Key, pair.ToString(), ""));
                        }
                    }
                    else
                    {
                        ident.Value = ini.Read(ident.Section, ident.Key, "");
                    }
                }

                ConfigHelper.LoadConfigValue(current, idents);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>保存到配置文件中去</summary>
        /// <param name="filename">文件名</param>
        public override void Save(string filename)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                filename = DirEx.CurrentDir() + ConfigFile;
            }

            if (filename.IsNullOrWhiteSpace())
            {
                throw new ApplicationException($"未指定{typeof(TConfig).Name}的配置文件路径！");
            }

            ConcurrentDictionary<string, Ident> idents = ConfigHelper.InitIdents(current);
            foreach (var ident in idents.Values)
            {
                if (ident.Section.IsNullOrEmpty())
                {
                    ident.Section = "Setup";
                }
            }

            ConfigHelper.SaveConfigValue(Current, idents);
            List<string> strs = new List<string> { ";<!--" + Description + "-->", "" };
            Dictionary<string, List<Ident>> listidents = new Dictionary<string, List<Ident>>();
            foreach (var ident in idents.Values)
            {
                string section = ident.IsList ? ident.Section + "-" + ident.Key : ident.Section;

                if (!listidents.ContainsKey(section))
                {
                    listidents.Add(section, new List<Ident>());
                }

                listidents[section].Add(ident);
            }

            foreach (var values in listidents)
            {
                strs.Add("[" + values.Key + "]");

                SortedList<int, Ident> slist = new SortedList<int, Ident>();
                foreach (var ident in values.Value)
                {
                    slist.Add(ident.Index, ident);
                }

                foreach (var ident in slist.Values)
                {
                    if (!ident.Description.IsNullOrEmpty())
                    {
                        strs.Add(";<!--" + ident.Description + "-->");
                    }

                    if (ident.IsList)
                    {
                        for (int i = 0; i < ident.Values.Count; i++)
                        {
                            strs.Add("Value" + i + "=" + ident.Values[i]);
                        }
                    }
                    else
                    {
                        strs.Add(ident.Key + "=" + ident.Value);
                    }
                }

                strs.Add("");
            }

            listidents.Clear();
            DirEx.CreateDir(Path.GetDirectoryName(filename));
            File.WriteAllLines(filename, strs.ToArray(), IniBase.IniEncoding);
        }

        #endregion 加载
    }
}