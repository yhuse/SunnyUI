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
 * 文件名称: UIStyle.cs
 * 文件说明: 控件样式定义类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    internal interface IStyleInterface
    {
        UIStyle Style { get; set; }

        bool StyleCustomMode { get; set; }

        string Version { get; }

        string TagString { get; set; }
    }

    /// <summary>
    /// 主题样式
    /// </summary>
    public enum UIStyle
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [DisplayText("Custom")]
        Custom = 0,

        /// <summary>
        /// 蓝
        /// </summary>
        [DisplayText("Blue")]
        Blue = 1,

        /// <summary>
        /// 绿
        /// </summary>
        [DisplayText("Green")]
        Green = 2,

        /// <summary>
        /// 橙
        /// </summary>
        [DisplayText("Orange")]
        Orange = 3,

        /// <summary>
        /// 红
        /// </summary>
        [DisplayText("Red")]
        Red = 4,

        /// <summary>
        /// 灰
        /// </summary>
        [DisplayText("Gray")]
        Gray = 5,

        /// <summary>
        /// 白
        /// </summary>
        [DisplayText("White")]
        White = 6,

        /// <summary>
        /// 深蓝
        /// </summary>
        [DisplayText("DarkBlue")]
        DarkBlue = 7,

        /// <summary>
        /// 黑
        /// </summary>
        [DisplayText("Black")]
        Black = 8,

        /// <summary>
        /// Office蓝
        /// </summary>
        [DisplayText("Office2010Blue")]
        Office2010Blue = 101,

        /// <summary>
        /// Office银
        /// </summary>
        [DisplayText("Office2010Silver")]
        Office2010Silver = 102,

        /// <summary>
        /// Office黑
        /// </summary>
        [DisplayText("Office2010Black")]
        Office2010Black = 103,

        /// <summary>
        /// 浅蓝
        /// </summary>
        [DisplayText("LightBlue")]
        LightBlue = 201,

        /// <summary>
        /// 浅绿
        /// </summary>
        [DisplayText("LightGreen")]
        LightGreen = 202,

        /// <summary>
        /// 浅橙
        /// </summary>
        [DisplayText("LightOrange")]
        LightOrange = 203,

        /// <summary>
        /// 浅红
        /// </summary>
        [DisplayText("LightRed")]
        LightRed = 204,

        /// <summary>
        /// 浅灰
        /// </summary>
        [DisplayText("LightGray")]
        LightGray = 205
    }

    /// <summary>
    /// 主题样式管理类
    /// </summary>
    public static class UIStyles
    {
        public static List<UIStyle> PopularStyles()
        {
            List<UIStyle> styles = new List<UIStyle>();
            foreach (UIStyle style in Enum.GetValues(typeof(UIStyle)))
            {
                if (style.Value() >= UIStyle.Blue.Value() && style.Value() <= UIStyle.Office2010Black.Value())
                {
                    styles.Add(style);
                }
            }

            return styles;
        }

        /// <summary>
        /// 自定义
        /// </summary>
        public static UIBaseStyle Custom = new UICustomStyle();

        /// <summary>
        /// 白
        /// </summary>
        public static UIBaseStyle White = new UIWhiteStyle();

        /// <summary>
        /// 蓝
        /// </summary>
        public static UIBaseStyle Blue = new UIBlueStyle();

        /// <summary>
        /// 浅蓝
        /// </summary>
        public static UIBaseStyle LightBlue = new UILightBlueStyle();

        /// <summary>
        /// 橙
        /// </summary>
        public static UIBaseStyle Orange = new UIOrangeStyle();

        /// <summary>
        /// 浅橙
        /// </summary>
        public static UIBaseStyle LightOrange = new UILightOrangeStyle();

        /// <summary>
        /// 灰
        /// </summary>
        public static UIBaseStyle Gray = new UIGrayStyle();

        /// <summary>
        /// 浅灰
        /// </summary>
        public static UIBaseStyle LightGray = new UILightGrayStyle();

        /// <summary>
        /// 绿
        /// </summary>
        public static UIBaseStyle Green = new UIGreenStyle();

        /// <summary>
        /// 浅绿
        /// </summary>
        public static UIBaseStyle LightGreen = new UILightGreenStyle();

        /// <summary>
        /// 红
        /// </summary>
        public static UIBaseStyle Red = new UIRedStyle();

        /// <summary>
        /// 浅红
        /// </summary>
        public static UIBaseStyle LightRed = new UILightRedStyle();

        /// <summary>
        /// 深蓝
        /// </summary>
        public static UIBaseStyle DarkBlue = new UIDarkBlueStyle();

        /// <summary>
        /// 黑
        /// </summary>
        public static UIBaseStyle Black = new UIBlackStyle();

        /// <summary>
        /// Office蓝
        /// </summary>
        public static UIBaseStyle Office2010Blue = new UIOffice2010BlueStyle();

        /// <summary>
        /// Office银
        /// </summary>
        public static UIBaseStyle Office2010Silver = new UIOffice2010SilverStyle();

        /// <summary>
        /// Office黑
        /// </summary>
        public static UIBaseStyle Office2010Black = new UIOffice2010BlackStyle();

        private static readonly ConcurrentDictionary<UIStyle, UIBaseStyle> Styles = new ConcurrentDictionary<UIStyle, UIBaseStyle>();
        private static readonly ConcurrentDictionary<Guid, UIForm> Forms = new ConcurrentDictionary<Guid, UIForm>();
        private static readonly ConcurrentDictionary<Guid, UIPage> Pages = new ConcurrentDictionary<Guid, UIPage>();

        /// <summary>
        /// 菜单颜色集合
        /// </summary>
        public static readonly ConcurrentDictionary<UIMenuStyle, UIMenuColor> MenuColors = new ConcurrentDictionary<UIMenuStyle, UIMenuColor>();

        static UIStyles()
        {
            AddStyle(Custom);
            AddStyle(Blue);
            AddStyle(LightBlue);
            AddStyle(Orange);
            AddStyle(LightOrange);
            AddStyle(Gray);
            AddStyle(LightGray);
            AddStyle(Green);
            AddStyle(LightGreen);
            AddStyle(Red);
            AddStyle(LightRed);
            AddStyle(DarkBlue);
            AddStyle(Black);
            AddStyle(White);
            AddStyle(Office2010Blue);
            AddStyle(Office2010Silver);
            AddStyle(Office2010Black);

            MenuColors.TryAdd(UIMenuStyle.Custom, new UIMenuCustomColor());
            MenuColors.TryAdd(UIMenuStyle.Black, new UIMenuBlackColor());
            MenuColors.TryAdd(UIMenuStyle.White, new UIMenuWhiteColor());
        }

        /// <summary>
        /// 主题样式整数值
        /// </summary>
        /// <param name="style">主题样式</param>
        /// <returns>整数值</returns>
        public static int Value(this UIStyle style)
        {
            return (int)style;
        }

        /// <summary>
        /// 注册窗体
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="form">窗体</param>
        public static bool Register(Guid guid, UIForm form)
        {
            if (!Forms.ContainsKey(guid))
            {
                Forms.TryAddOrUpdate(guid, form);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="page">页面</param>
        public static bool Register(Guid guid, UIPage page)
        {
            if (!Pages.ContainsKey(guid))
            {
                Pages.TryAddOrUpdate(guid, page);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册窗体
        /// </summary>
        /// <param name="form">窗体</param>
        public static bool Register(this UIForm form)
        {
            if (!Forms.ContainsKey(form.Guid))
            {
                Forms.TryAddOrUpdate(form.Guid, form);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <param name="page">页面</param>
        public static bool Register(this UIPage page)
        {
            if (!Pages.ContainsKey(page.Guid))
            {
                Pages.TryAddOrUpdate(page.Guid, page);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 反注册窗体
        /// </summary>
        /// <param name="form">窗体</param>
        public static void UnRegister(this UIForm form)
        {
            Forms.TryRemove(form.Guid, out _);
        }

        /// <summary>
        /// 反注册页面
        /// </summary>
        /// <param name="page">页面</param>
        public static void UnRegister(this UIPage page)
        {
            Pages.TryRemove(page.Guid, out _);
        }

        /// <summary>
        /// 反注册窗体
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="form">窗体</param>
        public static void UnRegister(Guid guid, UIForm form)
        {
            Forms.TryRemove(guid, out _);
        }

        /// <summary>
        /// 反注册页面
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="page">页面</param>
        public static void UnRegister(Guid guid, UIPage page)
        {
            Pages.TryRemove(guid, out _);
        }

        /// <summary>
        /// 获取主题样式
        /// </summary>
        /// <param name="style">主题样式名称</param>
        /// <returns>主题样式</returns>
        public static UIBaseStyle GetStyleColor(UIStyle style)
        {
            if (Styles.ContainsKey(style))
            {
                return Styles[style];
            }
            else
            {
                Style = UIStyle.Blue;
                return Styles[Style];
            }
        }

        public static UIBaseStyle ActiveStyleColor
        {
            get => GetStyleColor(Style);
        }

        private static void AddStyle(UIBaseStyle uiColor)
        {
            if (Styles.ContainsKey(uiColor.Name))
            {
                MessageBox.Show(uiColor.Name + " is already exist.");
            }

            Styles.TryAdd(uiColor.Name, uiColor);
        }

        /// <summary>
        /// 主题样式
        /// </summary>
        public static UIStyle Style { get; private set; } = UIStyle.Blue;

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        public static void SetStyle(UIStyle style)
        {
            Style = style;

            foreach (var form in Forms.Values)
            {
                form.Style = style;
            }

            foreach (var page in Pages.Values)
            {
                page.Style = style;
            }
        }
    }

    /// <summary>
    /// 主题颜色
    /// </summary>
    public static class UIColor
    {
        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly Color Blue = Color.FromArgb(80, 160, 255);

        /// <summary>
        /// 绿
        /// </summary>
        public static readonly Color Green = Color.FromArgb(110, 190, 40);

        /// <summary>
        /// 红
        /// </summary>
        public static readonly Color Red = Color.FromArgb(230, 80, 80);

        /// <summary>
        /// 灰
        /// </summary>
        public static readonly Color Gray = Color.FromArgb(140, 140, 140);

        /// <summary>
        /// 橙
        /// </summary>
        public static readonly Color Orange = Color.FromArgb(220, 155, 40);

        /// <summary>
        /// 深蓝
        /// </summary>
        public static readonly Color DarkBlue = Color.FromArgb(15, 40, 70);

        /// <summary>
        /// 白
        /// </summary>
        public static readonly Color White = Color.White;

        /// <summary>
        /// 黑
        /// </summary>
        public static readonly Color Black = Color.Black;

        /// <summary>
        /// 透明
        /// </summary>
        public static readonly Color Transparent = Color.Transparent;

        /// <summary>
        /// 浅蓝
        /// </summary>
        public static readonly Color LightBlue = Color.FromArgb(235, 243, 255);

        /// <summary>
        /// 浅绿
        /// </summary>
        public static readonly Color LightGreen = Color.FromArgb(239, 248, 232);

        /// <summary>
        /// 浅红
        /// </summary>
        public static readonly Color LightRed = Color.FromArgb(251, 238, 238);

        /// <summary>
        /// 浅灰
        /// </summary>
        public static readonly Color LightGray = Color.FromArgb(242, 242, 244);

        /// <summary>
        /// 浅橙
        /// </summary>
        public static readonly Color LightOrange = Color.FromArgb(251, 245, 233);

        /// <summary>
        /// 中蓝
        /// </summary>
        public static readonly Color RegularBlue = Color.FromArgb(216, 233, 255);

        /// <summary>
        /// 中绿
        /// </summary>
        public static readonly Color RegularGreen = Color.FromArgb(224, 242, 210);

        /// <summary>
        /// 中红
        /// </summary>
        public static readonly Color RegularRed = Color.FromArgb(248, 222, 222);

        /// <summary>
        /// 中灰
        /// </summary>
        public static readonly Color RegularGray = Color.FromArgb(230, 230, 232);

        /// <summary>
        /// 中橙
        /// </summary>
        public static readonly Color RegularOrange = Color.FromArgb(247, 234, 210);
    }

    /// <summary>
    /// 不可用颜色
    /// </summary>
    public static class UIDisableColor
    {
        /// <summary>
        /// 填充色
        /// </summary>
        public static readonly Color Fill = UIFontColor.Plain;

        /// <summary>
        /// 字体色
        /// </summary>
        public static readonly Color Fore = UIFontColor.Regular;
    }

    /// <summary>
    /// 字体颜色
    /// </summary>
    public static class UIFontColor
    {
        /// <summary>
        /// 默认字体
        /// </summary>
        public static readonly Font Font = new Font("微软雅黑", 12);

        /// <summary>
        /// 默认字体
        /// </summary>
        public static readonly Font SubFont = new Font("微软雅黑", 9);

        /// <summary>
        /// 主要颜色
        /// </summary>
        public static readonly Color Primary = Color.FromArgb(48, 48, 48);

        /// <summary>
        /// 正常颜色
        /// </summary>
        public static readonly Color Regular = Color.FromArgb(96, 96, 96);

        /// <summary>
        /// 次要颜色
        /// </summary>
        public static readonly Color Secondary = Color.FromArgb(144, 144, 144);

        /// <summary>
        /// 其他颜色
        /// </summary>
        public static readonly Color Plain = Color.Silver;
    }

    /// <summary>
    /// 边框颜色
    /// </summary>
    public static class UIRectColorColor
    {
        /// <summary>
        /// 主要颜色
        /// </summary>
        public static readonly Color Primary = Color.FromArgb(0xDC, 0xDF, 0xE6);

        /// <summary>
        /// 正常颜色
        /// </summary>
        public static readonly Color Regular = Color.FromArgb(0xE4, 0xE7, 0xED);

        /// <summary>
        /// 次要颜色
        /// </summary>
        public static readonly Color Secondary = Color.FromArgb(0xEB, 0xEE, 0xF5);

        /// <summary>
        /// 其他颜色
        /// </summary>
        public static readonly Color Plain = Color.FromArgb(0xF2, 0xF6, 0xFC);
    }

    /// <summary>
    /// 背景色
    /// </summary>
    public static class UIBackgroundColor
    {
        /// <summary>
        /// 白
        /// </summary>
        public static readonly Color White = UIColor.White;

        /// <summary>
        /// 黑
        /// </summary>
        public static readonly Color Black = UIColor.Black;

        /// <summary>
        /// 透明色
        /// </summary>
        public static readonly Color Transparent = Color.Transparent;
    }

    public static class UIStyleHelper
    {
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

        public static void SetChildUIStyle(this UIPanel ctrl, UIStyle style)
        {
            SetControlChildUIStyle(ctrl, style);
        }

        public static void SetChildUIStyle(this UIForm ctrl, UIStyle style)
        {
            SetControlChildUIStyle(ctrl, style);
        }

        public static void SetChildUIStyle(this UIPage ctrl, UIStyle style)
        {
            SetControlChildUIStyle(ctrl, style);
        }

        private static void SetControlChildUIStyle(Control ctrl, UIStyle style)
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
        public static List<Control> GetUIStyleControls(this Control ctrl, string interfaceName)
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
    }
}