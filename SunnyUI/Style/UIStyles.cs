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
 * 文件名称: UIStyles.cs
 * 文件说明: 主题样式管理类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-07-12: V3.0.5 增加紫色主题
 * 2021-07-18: V3.0.5 增加多彩主题，以颜色深色，文字白色为主
 * 2021-09-24: V3.0.7 修改默认字体的GdiCharSet
 * 2021-10-16: V3.0.8 增加系统DPI缩放自适应
 * 2023-08-28: V3.4.2 修改全局字体为系统默认字体
 * 2023-11-05: V3.5.2 重构主题
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Drawing.FontConverter;

namespace Sunny.UI
{
    /// <summary>
    /// 主题样式管理类
    /// </summary>
    public static class UIStyles
    {
        public static bool GlobalFont { get; set; } = false;

        public static bool GlobalRectangle { get; set; } = false;

        public static bool DPIScale { get; set; }

        public static bool ZoomScale { get; set; }

        [Editor("System.Drawing.Design.FontNameEditor", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(FontNameConverter))]
        public static string GlobalFontName { get; set; } = "宋体";

        public static int GlobalFontScale { get; set; } = 100;

        private static readonly ConcurrentDictionary<string, byte> FontCharSets = new ConcurrentDictionary<string, byte>();

        //GdiCharSet
        //一个字节值，该值指定使用此 Font 字符集的 GDI 字符集。 默认值为 1。
        //字符集	        值
        //ANSI	        0
        //DEFAULT	    1
        //象征	        2
        //SHIFTJIS	    128
        //HANGEUL	    129
        //HANGUL	    129
        //GB2312	    134
        //中国BIG5	    136
        //OEM	        255
        //JOHAB	        130
        //希伯来语	    177
        //阿拉伯语	    178
        //希腊语	        161
        //土耳其语	    162
        //越南语	        163
        //泰语	        222
        //EASTEUROPE	238
        //俄语	        204
        //MAC	        77
        //波罗的海	    186

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;
        }

        internal static byte GetGdiCharSet(string fontName)
        {
            if (FontCharSets.ContainsKey(fontName)) return FontCharSets[fontName];
            using Font font = new Font(fontName, 16);
            LOGFONT obj = new LOGFONT();
            font.ToLogFont(obj);
            FontCharSets.TryAdd(fontName, obj.lfCharSet);
            return obj.lfCharSet;
        }

        internal static float DefaultFontSize = 12;
        internal static float DefaultSubFontSize = 9;

        /// <summary>
        /// 默认字体
        /// </summary>
        internal static Font Font()
        {
            byte gdiCharSet = GetGdiCharSet(System.Drawing.SystemFonts.DefaultFont.Name);
            return new Font(familyName: System.Drawing.SystemFonts.DefaultFont.Name, DefaultFontSize, FontStyle.Regular, GraphicsUnit.Point, gdiCharSet);
        }

        /// <summary>
        /// 默认二级字体
        /// </summary>
        internal static Font SubFont()
        {
            byte gdiCharSet = GetGdiCharSet(System.Drawing.SystemFonts.DefaultFont.Name);
            return new Font(System.Drawing.SystemFonts.DefaultFont.Name, DefaultSubFontSize, FontStyle.Regular, GraphicsUnit.Point, gdiCharSet);
        }

        public static List<UIStyle> PopularStyles()
        {
            List<UIStyle> styles = new List<UIStyle>();
            foreach (UIStyle style in Enum.GetValues(typeof(UIStyle)))
            {
                if (style.Value() >= UIStyle.Blue.Value() && style.Value() < UIStyle.Colorful.Value())
                {
                    styles.Add(style);
                }
            }

            return styles;
        }

        public static readonly UIBaseStyle Inherited = new UIInheritedStyle();

        /// <summary>
        /// 自定义
        /// </summary>
        private static readonly UIBaseStyle Custom = new UICustomStyle();

        /// <summary>
        /// 蓝
        /// </summary>
        public static readonly UIBaseStyle Blue = new UIBlueStyle();

        /// <summary>
        /// 橙
        /// </summary>
        public static readonly UIBaseStyle Orange = new UIOrangeStyle();

        /// <summary>
        /// 灰
        /// </summary>
        public static readonly UIBaseStyle Gray = new UIGrayStyle();

        /// <summary>
        /// 绿
        /// </summary>
        public static readonly UIBaseStyle Green = new UIGreenStyle();

        /// <summary>
        /// 红
        /// </summary>
        public static readonly UIBaseStyle Red = new UIRedStyle();

        /// <summary>
        /// 深蓝
        /// </summary>
        public static readonly UIBaseStyle DarkBlue = new UIDarkBlueStyle();

        /// <summary>
        /// 黑
        /// </summary>
        public static readonly UIBaseStyle Black = new UIBlackStyle();

        /// <summary>
        /// 紫
        /// </summary>
        public static readonly UIBaseStyle Purple = new UIPurpleStyle();

        /// <summary>
        /// 多彩
        /// </summary>
        private static readonly UIColorfulStyle Colorful = new UIColorfulStyle();

        public static void InitColorful(Color styleColor, Color foreColor)
        {
            Colorful.Init(styleColor, foreColor);
            SetStyle(UIStyle.Colorful);
        }

        internal static readonly ConcurrentDictionary<UIStyle, UIBaseStyle> Styles = new ConcurrentDictionary<UIStyle, UIBaseStyle>();
        internal static readonly ConcurrentDictionary<Guid, UIBaseForm> Forms = new ConcurrentDictionary<Guid, UIBaseForm>();
        internal static readonly ConcurrentDictionary<Guid, UIPage> Pages = new ConcurrentDictionary<Guid, UIPage>();

        /// <summary>
        /// 菜单颜色集合
        /// </summary>
        public static readonly ConcurrentDictionary<UIMenuStyle, UIMenuColor> MenuColors = new ConcurrentDictionary<UIMenuStyle, UIMenuColor>();

        static UIStyles()
        {
            AddStyle(Inherited);
            AddStyle(Custom);
            AddStyle(Blue);
            AddStyle(Orange);
            AddStyle(Gray);
            AddStyle(Green);
            AddStyle(Red);
            AddStyle(DarkBlue);

            AddStyle(new UIBaseStyle().Init(UIColor.LayuiGreen, UIStyle.LayuiGreen, Color.White, UIFontColor.Primary));
            AddStyle(new UIBaseStyle().Init(UIColor.LayuiRed, UIStyle.LayuiRed, Color.White, UIFontColor.Primary));
            AddStyle(new UIBaseStyle().Init(UIColor.LayuiOrange, UIStyle.LayuiOrange, Color.White, UIFontColor.Primary));

            AddStyle(Black);
            AddStyle(Purple);

            AddStyle(Colorful);

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
        /// <param name="form">窗体</param>
        public static bool Register(this UIBaseForm form)
        {
            if (!Forms.ContainsKey(form.Guid))
            {
                Forms.Upsert(form.Guid, form);
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
                Pages.Upsert(page.Guid, page);
                return true;
            }

            return false;
        }

        public static List<T> GetPages<T>() where T : UIPage
        {
            List<T> result = new List<T>();
            foreach (var page in Pages)
            {
                if (page is T pg)
                    result.Add(pg);
            }

            return result;
        }

        /// <summary>
        /// 反注册窗体
        /// </summary>
        /// <param name="form">窗体</param>
        public static void UnRegister(this UIBaseForm form)
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

            Style = UIStyle.Blue;
            return Styles[Style];
        }

        public static UIBaseStyle ActiveStyleColor => GetStyleColor(Style);

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
        public static UIStyle Style { get; private set; } = UIStyle.Inherited;

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        public static void SetStyle(UIStyle style)
        {
            if (style != UIStyle.Colorful && Style == style) return;
            Style = style;
            if (!style.IsValid()) return;

            foreach (var form in Forms.Values)
            {
                form.SetInheritedStyle(style);
            }

            foreach (var page in Pages.Values)
            {
                page.SetInheritedStyle(style);
            }
        }

        public static void Render()
        {
            SetStyle(Style);
        }

        public static void SetDPIScale()
        {
            foreach (var form in Forms.Values)
            {
                if (UIDPIScale.NeedSetDPIFont())
                    form.SetDPIScale();
            }

            foreach (var page in Pages.Values)
            {
                if (UIDPIScale.NeedSetDPIFont())
                    page.SetDPIScale();
            }
        }

        public static void Translate()
        {
            foreach (var form in Forms.Values)
            {
                form.Translate();
            }
        }
    }
}
