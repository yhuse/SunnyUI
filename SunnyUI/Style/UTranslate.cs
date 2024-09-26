/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.Common.dll can be used for free under the MIT license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UTranslate.cs
 * 文件说明: 多语翻译接口
 * 当前版本: V3.1
 * 创建日期: 2021-07-23
 *
 * 2021-07-23: V3.0.5 增加文件说明
******************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Sunny.UI
{
    /// <summary>
    /// 内置资源多语翻译接口
    /// </summary>
    public interface ITranslate
    {
        /// <summary>
        /// 多语翻译接口
        /// </summary>
        void Translate();
    }

    /// <summary>
    /// 界面显示控件多语翻译接口
    /// </summary>
    public interface IFormTranslator
    {
        string[] FormTranslatorProperties { get; }

        bool ShowBuiltInResources { get; set; }
    }

    public interface ICodeTranslator
    {
        void Load(IniFile ini);
    }

    public abstract class BaseCodeTranslator : ICodeTranslator
    {
        public void Load(IniFile ini)
        {

        }
    }

    public static class TranslateHelper
    {
        private static List<string> Ingores =
            [
            "UIContextMenuStrip",
            "UIStyleManager",
            "UILogo",
            "UIPagination",
            "UIMiniPagination"
            ];

        private static List<string> Needs =
            [
            "System.Windows.Forms.ToolStripMenuItem"
            ];

        public static void LoadCsproj(string csprojFile)
        {
            List<string> files = new List<string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(csprojFile);
            if (xml.DocumentElement == null) return;

            foreach (XmlNode group in xml.DocumentElement.ChildNodes)
            {
                if (group.Name != "ItemGroup") continue;

                foreach (XmlNode node in group.ChildNodes)
                {
                    if (node.Name != "Compile") continue;
                    if (node.Attributes == null) continue;
                    if (node.Attributes["Include"] == null) continue;
                    if (node.ChildNodes.Count == 1 && node.ChildNodes[0].Name == "SubType" && node.ChildNodes[0].InnerText == "Form")
                    {
                        string fileName = Path.Combine(Path.GetDirectoryName(csprojFile) ?? string.Empty, node.Attributes["Include"].InnerText);
                        string path = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName) + ".Designer.cs");
                        if (File.Exists(path))
                        {
                            files.Add(path);
                        }
                    }
                }
            }

            Dir.CreateDir(Dir.CurrentDir() + "Language");
            List<string> names = new List<string>();
            Dictionary<string, string> ctrladds = new Dictionary<string, string>();
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);
                names.Clear();
                ctrladds.Clear();
                bool isStart = false;
                string inifile = "";
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("namespace"))
                    {
                        inifile = line.Trim().Replace("namespace", "").Trim() + ".";
                    }

                    if (line.Trim().Contains("partial class"))
                    {
                        inifile += line.Trim().Replace("partial class", "").Trim();
                    }

                    if (line.Contains("private void InitializeComponent()")) isStart = true;

                    if (isStart)
                    {
                        if (line.Contains(".Controls.Add"))
                            ctrladds.Add(
                                line.Between("(", ")"),
                                line.SplitBeforeLast(".Controls.Add").Replace("this.", "").Trim());
                    }

                    if (isStart && line.Trim().StartsWith("private") && line.Trim().EndsWith(";"))
                    {
                        string[] items = line.Trim().Split(" ");
                        string name = items[2].Replace(";", "");
                        string classname = items[1];

                        if (Ingores.Contains(classname)) continue;

                        if (classname.SplitLast(".").StartsWith("UI") || Needs.Contains(classname))
                        {
                            ctrladds.TryGetValue("this." + name, out string parent);
                            if (parent.IsValid())
                                names.Add($"{parent},{classname},{name}");
                            else
                                names.Add($"{classname},{name}");
                        }
                    }
                }

                foreach (var line in lines)
                {
                    for (int i = 0; i < names.Count; i++)
                    {
                        if (names[i].SplitSeparatorCount(",") >= 1) continue;
                        string add = $".Controls.Add(this.{names[i]});";
                        if (line.Contains(add))
                        {
                            names[i] = line.Replace(add, "").Replace("this.", "").Trim() + "," + names[i];
                        }
                    }
                }

                if (names.Count > 0)
                {
                    inifile = Dir.CurrentDir() + "Language\\" + inifile + ".ini";
                    IniFile ini = new IniFile(inifile, System.Text.Encoding.UTF8);
                    string section = "Info";
                    const string warning = "注意：请先关闭应用程序，然后再修改此文档。否则修改可能会应用程序生成代码覆盖。";
                    if (ini.Read(section, "Warning", "") != warning)
                        ini.Write(section, "Warning", warning);
                    ini.Write(section, "Controls", string.Join(";", names.ToArray()));
                    ini.UpdateFile();
                }
            }
        }

        public struct CtrlInfo
        {
            public string Name;
            public string ClassName;
            public string Parent;
        }

        public static void TranslateOther(this Form form)
        {
            if (!UIStyles.MultiLanguageSupport) return;
            if (!(form is UIBaseForm || form is UIPage)) return;
            Dir.CreateDir(Dir.CurrentDir() + "Language");

            string thisFullName = form.GetType().FullName;
            string section = "Info";
            string inifile = Dir.CurrentDir() + "Language\\" + thisFullName + ".ini";
            if (!File.Exists(inifile)) return;
            IniFile ini = new IniFile(Dir.CurrentDir() + "Language\\" + thisFullName + ".ini", System.Text.Encoding.UTF8);
            string controls = ini.Read(section, "Controls", "");
            if (controls.IsNullOrEmpty()) return;

            string key = UIStyles.CultureInfo.LCID.ToString() + ".DisplayName";
            if (ini.Read(section, key, "") != UIStyles.CultureInfo.DisplayName)
                ini.Write(section, key, UIStyles.CultureInfo.DisplayName);
            key = UIStyles.CultureInfo.LCID.ToString() + ".EnglishName";
            if (ini.Read(section, key, "") != UIStyles.CultureInfo.EnglishName)
                ini.Write(section, UIStyles.CultureInfo.LCID.ToString() + ".EnglishName", UIStyles.CultureInfo.EnglishName);

            Dictionary<string, CtrlInfo> Ctrls2 = new Dictionary<string, CtrlInfo>();
            Dictionary<string, CtrlInfo> Ctrls3 = new Dictionary<string, CtrlInfo>();
            foreach (var item in controls.Split(';'))
            {
                string[] strs = item.Split(",");
                if (strs.Length == 0) continue;

                if (strs.Length == 2)
                {
                    Ctrls2.Add(strs[1], new CtrlInfo() { ClassName = strs[0], Name = strs[1], Parent = "" });
                }

                if (strs.Length == 3)
                {
                    Ctrls3.Add(strs[2], new CtrlInfo() { ClassName = strs[1], Name = strs[2], Parent = strs[0] });
                }
            }

            var formControls = form.GetTranslateControls<IFormTranslator>().Where(p => p.FormTranslatorProperties != null); ;
            section = UIStyles.CultureInfo.LCID + ".Form";
            foreach (var control in formControls)
            {
                if (control.ShowBuiltInResources) continue;
                Control ctrl = (Control)control;
                if (ctrl.Name.IsNullOrEmpty()) continue;
                if (Ctrls3.NotContainsKey(ctrl.Name)) continue;
                if (ctrl.Parent.Name == form.Name || ctrl.Parent.Name == Ctrls3[ctrl.Name].Parent)
                {
                    foreach (var propertyName in control.FormTranslatorProperties)
                    {
                        PropertyInfo pt = ctrl.GetType().GetProperty(propertyName);
                        if (pt == null || !pt.CanWrite) continue;

                        key = ctrl.Name + "." + propertyName;
                        string langStr = ini.Read(section, key, "");
                        string ctrlStr = pt.GetValue(ctrl, null)?.ToString();

                        if (langStr.IsNullOrEmpty())
                        {
                            if (ctrlStr.IsValid()) ini.Write(section, key, ctrlStr);
                        }
                        else
                        {
                            pt.SetValue(ctrl, langStr, null);
                        }
                    }
                }
            }

            ini.Dispose();
        }
    }
}
