/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

        bool MultiLanguageSupport { get; set; }
    }

    public static class TranslateHelper
    {
        private static List<string> Includes = [
            //SunnyUI Controls
            "UIButton",
            "UISymbolButton",
            "UIGroupBox",
            "UICheckBox",
            "UIDatePicker",
            "UIDateTimePicker",
            "UIHeaderButton",
            "UIImageButton",
            "UILabel",
            "UILinkLabel",
            "UICheckBoxGroup",
            "UIRadioButtonGroup",
            "UILine",
            "UIPanel",
            "UIRadioButton",
            "UIScrollingText",
            "UISmoothLabel",
            "UISwitch",
            "UISymbolLabel",
            "UITimePicker",
            "UITurnSwitch",
            "UITitlePanel",
            //Native Controls
            //"Button",
            "ToolStripMenuItem"//,
            //"CheckBox",
            //"RadioButton",
            //"GroupBox",
            //"Label",
            //"LinkLabel"
            ];

        private static Dictionary<string, string> NativeControlsProperty = new Dictionary<string, string>()
        {
            ["Button"] = "Text",
            ["CheckBox"] = "Text",
            ["RadioButton"] = "Text",
            ["GroupBox"] = "Text",
            ["Label"] = "Text",
            ["LinkLabel"] = "Text"
        };

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
                string strnamespace = "";
                foreach (var line in lines)
                {
                    if (line.Trim().StartsWith("namespace"))
                    {
                        strnamespace = inifile = line.Trim().Replace("namespace", "").Trim();
                    }

                    if (line.Trim().Contains("partial class"))
                    {
                        inifile += "." + line.Trim().Replace("partial class", "").Trim();
                    }

                    if (line.Contains("private void InitializeComponent()")) isStart = true;

                    if (!isStart) continue;

                    if (line.Contains(".Controls.Add"))
                    {
                        ctrladds.Add(line.Between("(", ")"),
                            line.SplitBeforeLast(".Controls.Add").Replace("this.", "").Trim());
                    }

                    if (line.Trim().StartsWith("private") && line.Trim().EndsWith(";"))
                    {
                        string[] items = line.Trim().Split(" ");
                        string name = items[2].Replace(";", "");
                        string classname = items[1];

                        if (!Includes.Contains(classname.SplitLast("."))) continue;

                        ctrladds.TryGetValue("this." + name, out string parent);
                        if (parent.IsValid())
                            names.Add($"{parent},{classname},{name}");
                        else
                            names.Add($"{classname},{name}");
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
                    ini.UpdateFile();

                    ini.EraseSection("Controls");
                    ini.Write("Controls", "Count", names.Count);
                    for (int i = 0; i < names.Count; i++)
                    {
                        ini.Write("Controls", "Control" + i, names[i]);
                    }
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
            string fullName = Dir.CurrentDir() + "Language\\" + thisFullName + ".ini";
            if (!File.Exists(fullName)) return;
            IniFile ini = new IniFile(Dir.CurrentDir() + "Language\\" + thisFullName + ".ini", System.Text.Encoding.UTF8);
            string readString = ini.ReadString("Controls", "Count", "");
            if (!int.TryParse(readString, out int count)) return;
            if (count == 0) return;

            string key = UIStyles.CultureInfo.LCID.ToString() + ".Name";
            if (ini.Read(section, key, "") != UIStyles.CultureInfo.Name)
                ini.Write(section, key, UIStyles.CultureInfo.Name);

            Dictionary<string, CtrlInfo> Ctrls2 = new Dictionary<string, CtrlInfo>();
            Dictionary<string, CtrlInfo> Ctrls3 = new Dictionary<string, CtrlInfo>();
            for (int i = 0; i < count; i++)
            {
                string item = ini.Read("Controls", "Control" + i, "");
                if (item.IsNullOrEmpty()) continue;
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
            section = UIStyles.CultureInfo.LCID + ".FormResources";
            foreach (var control in formControls)
            {
                if (!control.MultiLanguageSupport) continue;
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

            FieldInfo[] fieldInfo = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var info in fieldInfo)
            {
                if (info.FieldType.Name == "UIContextMenuStrip" || info.FieldType.Name == "ContextMenuStrip")
                {
                    ContextMenuStrip context = (ContextMenuStrip)info.GetValue(form);
                    foreach (var item in context.Items)
                    {
                        if (item is ToolStripMenuItem tsmi && Ctrls2.Keys.Contains(tsmi.Name))
                        {
                            key = tsmi.Name + ".Text";
                            string langStr = ini.Read(section, key, "");
                            string ctrlStr = tsmi.Text;
                            if (langStr.IsNullOrEmpty())
                            {
                                if (ctrlStr.IsValid()) ini.Write(section, key, ctrlStr);
                            }
                            else
                            {
                                tsmi.Text = langStr;
                            }
                        }
                    }
                }
            }

            ini.UpdateFile();
            ini.Dispose();
        }
    }

    public class IniCodeTranslator<TConfig> where TConfig : IniCodeTranslator<TConfig>, new()
    {
        /// <summary>
        /// 当前实例。通过置空可以使其重新加载。
        /// </summary>
        public static TConfig Current
        {
            get
            {
                if (current != null) return current;
                current = new TConfig();
                current.SetDefault();
                return current;
            }
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        public virtual void SetDefault()
        {
        }

        public static void Load(Form form) => Current.LoadResources(form);

        /// <summary>
        /// 实体对象
        /// </summary>
        private static TConfig current;

        private void LoadResources(Form form)
        {
            if (!UIStyles.MultiLanguageSupport) return;
            if (!(form is UIBaseForm || form is UIPage)) return;
            Dir.CreateDir(Dir.CurrentDir() + "Language");

            string thisFullName = form.GetType().FullName;

            string filename = Dir.CurrentDir() + "Language\\" + thisFullName + ".ini";
            bool exists = File.Exists(filename);

            try
            {
                IniFile ini = new IniFile(filename, Encoding.UTF8);
                string section = "Info";
                if (!exists)
                {
                    const string warning = "注意：请先关闭应用程序，然后再修改此文档。否则修改可能会应用程序生成代码覆盖。";
                    ini.Write(section, "Warning", warning);
                }

                string key = UIStyles.CultureInfo.LCID.ToString() + ".Name";
                if (ini.Read(section, key, "") != UIStyles.CultureInfo.Name)
                    ini.Write(section, key, UIStyles.CultureInfo.Name);

                ConcurrentDictionary<string, Ident> idents = InitIdents(current);

                foreach (var ident in idents.Values)
                {
                    ident.Section = UIStyles.CultureInfo.LCID + ".CodeResources";

                    if (ini.KeyExists(ident.Section, ident.Key))
                    {
                        ident.Value = ini.Read(ident.Section, ident.Key, "");
                        ident.Show = true;
                    }
                    else
                    {
                        ini.Write(ident.Section, ident.Key, ident.Value);
                        ident.Show = false;
                    }
                }

                ini.UpdateFile();
                LoadConfigValue(current, idents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private ConcurrentDictionary<string, Ident> InitIdents<T>(T config)
        {
            ConcurrentDictionary<string, Ident> concurrentDictionary = new ConcurrentDictionary<string, Ident>();
            foreach (PropertyInfo needProperty in config.GetType().GetNeedProperties())
            {
                Ident ident = new Ident
                {
                    Key = needProperty.Name,
                    Show = true,
                    Description = needProperty.Description(),
                    IsList = needProperty.PropertyType.IsList(),
                    Value = needProperty.GetValue(config, null).ToString()
                };

                ConfigSectionAttribute customAttribute = needProperty.GetCustomAttribute<ConfigSectionAttribute>();
                ident.Section = ((customAttribute != null) ? customAttribute.Section : "");
                ConfigPropertyAttribute customAttribute2 = needProperty.GetCustomAttribute<ConfigPropertyAttribute>();
                ident.Caption = ((customAttribute2 != null) ? customAttribute2.Caption : "");
                ident.Unit = ((customAttribute2 != null) ? customAttribute2.Unit : "");
                ident.Description = ((customAttribute2 != null) ? customAttribute2.Description : "");
                ConfigIndexAttribute customAttribute3 = needProperty.GetCustomAttribute<ConfigIndexAttribute>();
                ident.Index = customAttribute3?.Index ?? (32767 + concurrentDictionary.Count);
                ident.Show = customAttribute3?.Show ?? true;
                if (ident.Description.IsNullOrEmpty())
                {
                    ident.Description = needProperty.DisplayName() ?? needProperty.Description();
                }

                if (ident.Description.IsNullOrEmpty())
                {
                    ident.Description = "";
                }

                if (!concurrentDictionary.ContainsKey(ident.Key))
                {
                    concurrentDictionary.TryAdd(ident.Key, ident);
                }
            }

            return concurrentDictionary;
        }

        private void LoadConfigValue<T>(T config, ConcurrentDictionary<string, Ident> idents)
        {
            foreach (PropertyInfo needProperty in config.GetType().GetNeedProperties())
            {
                object value = needProperty.GetValue(config, null);
                if (idents.TryGetValue(needProperty.Name, out Ident ident) && ident.Show)
                {
                    Type propertyType = needProperty.PropertyType;
                    if (propertyType == typeof(string))
                    {
                        object value2 = idents[needProperty.Name].Value;
                        needProperty.SetValue(config, Convert.ChangeType((value2 == null) ? value : value2, propertyType), null);
                        continue;
                    }

                    if (ConvertEx.CanConvent(propertyType))
                    {
                        object value3 = ConvertEx.StringToObject(idents[needProperty.Name].Value, propertyType, value);
                        needProperty.SetValue(config, Convert.ChangeType(value3, propertyType), null);
                        continue;
                    }

                    throw new ApplicationException("不支持此类型: " + propertyType.FullName);
                }
            }
        }
    }
}
