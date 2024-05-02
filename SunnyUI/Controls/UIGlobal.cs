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
 * 文件名称: UIGlobal.cs
 * 文件说明: 全局参数类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Reflection;

namespace Sunny.UI
{
    /// <summary>
    /// 全局参数类
    /// </summary>
    public static class UIGlobal
    {
        //public const string Version = "SunnyUI.Net V3.2.6";

        /// <summary>
        /// 版本
        /// </summary>
        public static string Version = Assembly.GetExecutingAssembly().GetName().Name + " V" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public const int EditorMinHeight = 20;
        public const int EditorMaxHeight = 60;
    }

    public interface IHideDropDown
    {
        public void HideDropDown();
    }

    public class UIDateTimeArgs : EventArgs
    {
        public DateTime DateTime { get; set; }

        public UIDateTimeArgs()
        {

        }

        public UIDateTimeArgs(DateTime datetime)
        {
            DateTime = datetime;
        }
    }

    public class UITextBoxSelectionArgs : EventArgs
    {
        public int SelectionStart { get; set; }

        public string Text { get; set; }
    }

    public delegate void OnSelectionChanged(object sender, UITextBoxSelectionArgs e);

    public delegate void OnDateTimeChanged(object sender, UIDateTimeArgs e);

    public delegate void OnCancelEventArgs(object sender, CancelEventArgs e);

    public enum NumPadType
    {
        Text,
        Integer,
        Double,
        IDNumber
    }
}