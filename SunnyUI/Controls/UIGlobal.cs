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
 * 文件名称: UIGlobal.cs
 * 文件说明: 全局参数类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using Sunny.UI.Properties;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 全局参数类
    /// </summary>
    public static class UIGlobal
    {
        /// <summary>
        /// 版本
        /// </summary>
        public static string Version = Resources.Name + " " + Resources.Version;

        public static bool IsCustom(this UIStyle style)
        {
            return style.Equals(UIStyle.Custom);
        }

        public static bool IsValid(this UIStyle style)
        {
            return !style.IsCustom();
        }

        public static bool IsCustom(this UIBaseStyle style)
        {
            return style.Name.IsCustom();
        }

        public static bool IsValid(this UIBaseStyle style)
        {
            return !style.IsCustom();
        }

        public static void SetChildUIStyle(Control ctrl, UIStyle style)
        {
            List<Control> controls = ctrl.GetUIStyleControls("IStyleInterface");
            foreach (var control in controls)
            {
                if (control is IStyleInterface item)
                {
                    if (!item.StyleCustomMode)
                    {
                        item.Style = style;
                    }
                }
            }

            FieldInfo[] fieldInfo = ctrl.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var info in fieldInfo)
            {
                if (info.FieldType.Name == "UIContextMenuStrip")
                {
                    UIContextMenuStrip context = (UIContextMenuStrip)info.GetValue(ctrl);
                    if (context != null && !context.StyleCustomMode)
                    {
                        context.SetStyle(style);
                    }
                }
            }
        }

        /// <summary>
        /// 查找包含接口名称的控件列表
        /// </summary>
        /// <param name="ctrl">容器</param>
        /// <param name="interfaceName">接口名称</param>
        /// <returns>控件列表</returns>
        private static List<Control> GetUIStyleControls(this Control ctrl, string interfaceName)
        {
            List<Control> values = new List<Control>();

            foreach (Control obj in ctrl.Controls)
            {
                if (obj.GetType().GetInterface(interfaceName) != null)
                {
                    values.Add(obj);
                }

                if (obj is UIPage) continue;
                if (obj is UIPanel) continue;

                if (obj.Controls.Count > 0)
                {
                    values.AddRange(obj.GetUIStyleControls(interfaceName));
                }
            }

            return values;
        }

        public static void ReStart(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
    }
}