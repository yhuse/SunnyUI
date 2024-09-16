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

using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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
        public static void TranslateOther(this Form form)
        {
            if (!UIStyles.MultiLanguageSupport) return;
            if (!(form is UIBaseForm || form is UIPage)) return;

            string thisFullName = form.GetType().FullName;
            string section = "Info";
            var formControls = form.GetInterfaceControls<IFormTranslator>(true, false).Where(p => p.FormTranslatorProperties != null);
            IniFile ini = new IniFile(Dir.CurrentDir() + "Language\\" + thisFullName + ".ini", System.Text.Encoding.UTF8);
            ini.Write(section, "Warning", "注意：请先关闭应用程序，然后再修改此文档。否则修改可能会应用程序生成代码覆盖。");
            ini.Write(section, "Information", "提示：此节为代码自动生成，无需修改。");
            ini.Write(section, UIStyles.CultureInfo.LCID.ToString() + ".DisplayName", UIStyles.CultureInfo.DisplayName);
            ini.Write(section, UIStyles.CultureInfo.LCID.ToString() + ".EnglishName", UIStyles.CultureInfo.EnglishName);

            section = UIStyles.CultureInfo.LCID + ".Form";
            foreach (var control in formControls)
            {
                if (control.ShowBuiltInResources) continue;

                foreach (var propertyName in control.FormTranslatorProperties)
                {
                    Control ctrl = (Control)control;
                    if (ctrl.Name.IsNullOrEmpty()) continue;
                    PropertyInfo pt = ctrl.GetType().GetProperty(propertyName);
                    if (pt == null || !pt.CanWrite) continue;

                    string key = ctrl.Name + "." + propertyName;

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

            ini.Dispose();
        }
    }
}
