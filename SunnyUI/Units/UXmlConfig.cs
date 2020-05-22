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
 * 文件名称: UXmlConfig.cs
 * 文件说明: XML 配置文件类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Sunny.UI
{
    /// <summary>
    /// XML 配置文件类
    /// </summary>
    /// <typeparam name="TConfig">类型</typeparam>
    public class XmlConfig<TConfig> : BaseConfig<TConfig> where TConfig : XmlConfig<TConfig>, new()
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

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlElement root = doc.DocumentElement;   //获取根节点
                foreach (Ident ident in idents.Values)
                {
                    if (root != null)
                    {
                        var elements = root.GetElementsByTagName(ident.Key);
                        if (elements.Count == 1)
                        {
                            if (ident.IsList)
                            {
                                ident.Values.Clear();
                                foreach (XmlNode node in elements[0].ChildNodes)
                                {
                                    ident.Values.Add(node.InnerText);
                                }
                            }
                            else
                            {
                                ident.Value = elements[0].InnerText;
                            }
                        }
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
            ConfigHelper.SaveConfigValue(Current, idents);

            List<string> strs = new List<string> { "<?xml version=" + "\"" + "1.0" + "\"" + " encoding=" + "\"" + "utf-8" + "\"" + "?> ", "<!--" + Description + "--> " };

            strs.Add("<" + GetType().Name + ">");

            SortedList<int, Ident> slist = new SortedList<int, Ident>();
            foreach (var ident in idents.Values)
            {
                slist.Add(ident.Index, ident);
            }

            foreach (var ident in slist.Values)
            {
                if (!ident.Description.IsNullOrEmpty())
                {
                    strs.Add("    <!--" + ident.Description + "-->");
                }

                if (!ident.IsList)
                {
                    strs.Add("    <" + ident.Key + ">" + ident.Value + "</" + ident.Key + ">");
                }
                else
                {
                    strs.Add("    <" + ident.Key + ">");
                    foreach (string value in ident.Values)
                    {
                        strs.Add("        <Value>" + value + "</Value>");
                    }

                    strs.Add("    </" + ident.Key + ">");
                }
            }

            strs.Add("</" + GetType().Name + ">");
            DirEx.CreateDir(Path.GetDirectoryName(filename));
            File.WriteAllLines(filename, strs.ToArray(), Encoding.UTF8);
        }

        #endregion 加载
    }
}