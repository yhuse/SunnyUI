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
 * 文件名称: UIStyleColor.cs
 * 文件说明: 控件样式定义类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System.Drawing;

#pragma warning disable 1591
//ButtonFillSelectedColor

namespace Sunny.UI
{
    public abstract class UIBaseStyle
    {
        public abstract UIStyle Name { get; }

        public abstract Color PrimaryColor { get; }
        public abstract Color RegularColor { get; }
        public abstract Color SecondaryColor { get; }
        public abstract Color PlainColor { get; }

        public abstract Color RectColor { get; }
        public abstract Color RectHoverColor { get; }
        public abstract Color RectPressColor { get; }

        public abstract Color RectSelectedColor { get; } 
        public abstract Color ButtonForeSelectedColor { get; }
        public abstract Color ButtonFillSelectedColor { get; }

        public abstract Color ButtonFillColor { get; }
        public abstract Color ButtonFillHoverColor { get; }
        public abstract Color ButtonFillPressColor { get; }

        public abstract Color ButtonForeColor { get; }
        public abstract Color ButtonForeHoverColor { get; }
        public abstract Color ButtonForePressColor { get; }

        public virtual Color FillDisableColor => Color.FromArgb(244, 244, 244);
        public virtual Color RectDisableColor => Color.FromArgb(173, 178, 181);
        public virtual Color ForeDisableColor => Color.FromArgb(109, 109, 103);

        public virtual Color LabelForeColor => UIFontColor.Primary;

        public virtual Color AvatarFillColor => Color.Silver;
        public virtual Color AvatarForeColor => PrimaryColor;

        public virtual Color CheckBoxColor => PrimaryColor;
        public virtual Color CheckBoxForeColor => LabelForeColor;
        public virtual Color PanelForeColor => LabelForeColor;

        public virtual Color DropDownControlColor => PanelForeColor;

        public abstract Color TitleColor { get; }
        public abstract Color TitleForeColor { get; }

        public virtual Color MenuSelectedColor => UIColor.Blue;

        public virtual Color GridSelectedColor => Color.FromArgb(155, 200, 255);

        public virtual Color GridSelectedForeColor => UIFontColor.Primary;
        public virtual Color GridStripeEvenColor => Color.White;
        public virtual Color GridStripeOddColor => PlainColor;

        public virtual Color GridLineColor => Color.FromArgb(233, 236, 244);

        public virtual Color ListItemSelectBackColor => PrimaryColor;
        public virtual Color ListItemSelectForeColor => PlainColor;

        public virtual Color LineForeColor => UIFontColor.Primary;

        public virtual Color ContextMenuColor => PlainColor;

        public virtual Color ProgressIndicatorColor => PrimaryColor;

        public virtual Color ProcessBarFillColor => PlainColor;

        public virtual Color ProcessBarForeColor => PrimaryColor;

        public virtual Color ScrollBarForeColor => PrimaryColor;

        public virtual Color SwitchActiveColor => PrimaryColor;

        public virtual Color SwitchInActiveColor => Color.Silver;

        public virtual Color SwitchFillColor => Color.White;

        public virtual Color TrackBarFillColor => PlainColor;

        public virtual Color TrackBarForeColor => PrimaryColor;

        public virtual Color TrackBarRectColor => PrimaryColor;

        public virtual Color TrackDisableColor => Color.Silver;

        public virtual Color PageTitleFillColor => Color.FromArgb(76, 76, 76);

        public virtual Color PageTitleForeColor => Color.White;

        public virtual Color TreeViewSelectedColor => PrimaryColor;

        public virtual Color TreeViewHoverColor => GridSelectedColor;

        public virtual bool BuiltIn => true;

        public virtual void LoadFromFile()
        {
        }

        public override string ToString()
        {
            return Name.DisplayText();
        }
    }

    public class UICustomStyle : UIBlueStyle
    {
        public override UIStyle Name => UIStyle.Custom;
    }

    public class UIOffice2010BlueStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Office2010Blue;
        public override Color PrimaryColor => Color.FromArgb(120, 148, 182);
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => Color.FromArgb(207, 221, 238);
        public override Color ButtonFillColor => Color.FromArgb(217, 230, 243);
        public override Color ButtonFillHoverColor => Color.FromArgb(249, 226, 137);
        public override Color ButtonFillPressColor => Color.FromArgb(255, 228, 137);
        public override Color ButtonForeColor => Color.FromArgb(30, 57, 91);
        public override Color ButtonForeHoverColor => Color.FromArgb(30, 57, 91);
        public override Color ButtonForePressColor => Color.FromArgb(30, 57, 91);
        public override Color RectColor => Color.FromArgb(180, 192, 211);
        public override Color RectHoverColor => Color.FromArgb(238, 201, 88);
        public override Color RectPressColor => Color.FromArgb(194, 118, 43);
        public override Color TitleColor => Color.FromArgb(191, 210, 233);
        public override Color TitleForeColor => Color.FromArgb(30, 57, 91);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
    }

    public class UIOffice2010SilverStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Office2010Silver;
        public override Color PrimaryColor => Color.FromArgb(139, 144, 151);
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => Color.FromArgb(224, 228, 233);
        public override Color ButtonFillColor => Color.FromArgb(247, 248, 249);
        public override Color ButtonFillHoverColor => Color.FromArgb(249, 226, 137);
        public override Color ButtonFillPressColor => Color.FromArgb(255, 228, 137);
        public override Color ButtonForeColor => Color.FromArgb(46, 46, 46);
        public override Color ButtonForeHoverColor => Color.FromArgb(46, 46, 46);
        public override Color ButtonForePressColor => Color.FromArgb(46, 46, 46);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => Color.FromArgb(139, 144, 151);
        public override Color RectHoverColor => Color.FromArgb(238, 201, 88);
        public override Color RectPressColor => Color.FromArgb(194, 118, 43);
        public override Color TitleColor => Color.Silver;
        public override Color TitleForeColor => Color.FromArgb(46, 46, 46);
    }

    public class UIOffice2010BlackStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Office2010Black;
        public override Color PrimaryColor => Color.FromArgb(49, 49, 49);
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => Color.FromArgb(211, 211, 211);
        public override Color ButtonFillColor => Color.FromArgb(211, 211, 211);
        public override Color ButtonFillHoverColor => Color.FromArgb(249, 226, 137);
        public override Color ButtonFillPressColor => Color.FromArgb(255, 228, 137);
        public override Color ButtonForeColor => Color.Black;
        public override Color ButtonForeHoverColor => Color.FromArgb(70, 70, 70);
        public override Color ButtonForePressColor => Color.FromArgb(70, 70, 70);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => Color.FromArgb(145, 145, 145);
        public override Color RectHoverColor => Color.FromArgb(238, 201, 88);
        public override Color RectPressColor => Color.FromArgb(194, 118, 43);
        public override Color AvatarFillColor => Color.FromArgb(148, 148, 148);
        public override Color TitleColor => Color.FromArgb(118, 118, 118);
        public override Color TitleForeColor => Color.Black;
    }

    public class UIBlueStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Blue;
        public override Color PrimaryColor => UIColor.Blue;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightBlue;
        public override Color ButtonFillColor => UIColor.Blue;
        public override Color ButtonFillHoverColor => Color.FromArgb(111, 168, 255);
        public override Color ButtonFillPressColor => Color.FromArgb(74, 131, 229);
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Blue;
        public override Color RectHoverColor => Color.FromArgb(111, 168, 255);
        public override Color RectPressColor => Color.FromArgb(74, 131, 229);
        public override Color TitleColor => UIColor.Blue;
        public override Color TitleForeColor => Color.White;
    }

    public class UILightBlueStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.LightBlue;
        public override Color PrimaryColor => UIColor.Blue;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightBlue;
        public override Color ButtonFillColor => UIColor.LightBlue;
        public override Color ButtonFillHoverColor => UIColor.Blue;
        public override Color ButtonFillPressColor => Color.FromArgb(74, 131, 229);
        public override Color ButtonForeColor => UIColor.Blue;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Blue;
        public override Color RectHoverColor => UIColor.Blue;
        public override Color RectPressColor => Color.FromArgb(74, 131, 229);
        public override Color TitleColor => UIColor.Blue;
        public override Color TitleForeColor => Color.White;
    }

    public class UIGreenStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Green;
        public override Color PrimaryColor => UIColor.Green;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightGreen;
        public override Color ButtonFillColor => UIColor.Green;
        public override Color ButtonFillHoverColor => Color.FromArgb(136, 202, 81);
        public override Color ButtonFillPressColor => Color.FromArgb(100, 168, 35);
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Green;
        public override Color RectHoverColor => Color.FromArgb(136, 202, 81);
        public override Color RectPressColor => Color.FromArgb(100, 168, 35);
        public override Color TitleColor => UIColor.Green;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Green;
        public override Color GridSelectedColor => Color.FromArgb(173, 227, 123);
    }

    public class UILightGreenStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.LightGreen;
        public override Color PrimaryColor => UIColor.Green;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightGreen;
        public override Color ButtonFillColor => UIColor.LightGreen;
        public override Color ButtonFillHoverColor => UIColor.Green;
        public override Color ButtonFillPressColor => Color.FromArgb(100, 168, 35);
        public override Color ButtonForeColor => UIColor.Green;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Green;
        public override Color RectHoverColor => UIColor.Green;
        public override Color RectPressColor => Color.FromArgb(100, 168, 35);
        public override Color TitleColor => UIColor.Green;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Green;
        public override Color GridSelectedColor => Color.FromArgb(173, 227, 123);
    }

    public class UIRedStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Red;
        public override Color PrimaryColor => UIColor.Red;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightRed;
        public override Color ButtonFillColor => UIColor.Red;
        public override Color ButtonFillHoverColor => Color.FromArgb(232, 127, 128);
        public override Color ButtonFillPressColor => Color.FromArgb(202, 87, 89);
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Red;
        public override Color RectHoverColor => Color.FromArgb(232, 127, 128);
        public override Color RectPressColor => Color.FromArgb(202, 87, 89);
        public override Color TitleColor => UIColor.Red;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Red;
        public override Color GridSelectedColor => Color.FromArgb(241, 160, 160);
    }

    public class UILightRedStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.LightRed;
        public override Color PrimaryColor => UIColor.Red;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightRed;
        public override Color ButtonFillColor => UIColor.LightRed;
        public override Color ButtonFillHoverColor => UIColor.Red;
        public override Color ButtonFillPressColor => Color.FromArgb(202, 87, 89);
        public override Color ButtonForeColor => UIColor.Red;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Red;
        public override Color RectHoverColor => UIColor.Red;
        public override Color RectPressColor => Color.FromArgb(202, 87, 89);
        public override Color TitleColor => UIColor.Red;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Red;
        public override Color GridSelectedColor => Color.FromArgb(241, 160, 160);
    }

    public class UIOrangeStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Orange;
        public override Color PrimaryColor => UIColor.Orange;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightOrange;
        public override Color ButtonFillColor => UIColor.Orange;
        public override Color ButtonFillHoverColor => Color.FromArgb(223, 174, 86);
        public override Color ButtonFillPressColor => Color.FromArgb(192, 137, 43);
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Orange;
        public override Color RectHoverColor => Color.FromArgb(223, 174, 86);
        public override Color RectPressColor => Color.FromArgb(192, 137, 43);
        public override Color TitleColor => UIColor.Orange;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Orange;
        public override Color GridSelectedColor => Color.FromArgb(238, 207, 151);
    }

    public class UILightOrangeStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.LightOrange;
        public override Color PrimaryColor => UIColor.Orange;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightOrange;
        public override Color ButtonFillColor => UIColor.LightOrange;
        public override Color ButtonFillHoverColor => UIColor.Orange;
        public override Color ButtonFillPressColor => Color.FromArgb(192, 137, 43);
        public override Color ButtonForeColor => UIColor.Orange;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectColor => UIColor.Orange;
        public override Color RectHoverColor => UIColor.Orange;
        public override Color RectPressColor => Color.FromArgb(192, 137, 43);
        public override Color TitleColor => UIColor.Orange;
        public override Color TitleForeColor => Color.White;
        public override Color MenuSelectedColor => UIColor.Orange;
        public override Color GridSelectedColor => Color.FromArgb(238, 207, 151);
    }

    public class UIGrayStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Gray;
        public override Color PrimaryColor => UIColor.Gray;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightGray;
        public override Color ButtonFillColor => UIColor.Gray;
        public override Color ButtonFillHoverColor => Color.FromArgb(158, 160, 165);
        public override Color ButtonFillPressColor => Color.FromArgb(121, 123, 129);
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Gray;
        public override Color RectHoverColor => Color.FromArgb(158, 160, 165);
        public override Color RectPressColor => Color.FromArgb(121, 123, 129);
        public override Color TitleColor => UIColor.Gray;
        public override Color TitleForeColor => Color.White;
        public override Color GridSelectedColor => Color.Silver;
    }

    public class UILightGrayStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.LightGray;
        public override Color PrimaryColor => UIColor.Gray;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightGray;
        public override Color ButtonFillColor => UIColor.LightGray;
        public override Color ButtonFillHoverColor => UIColor.Gray;
        public override Color ButtonFillPressColor => Color.FromArgb(121, 123, 129);
        public override Color ButtonForeColor => UIColor.Gray;
        public override Color ButtonForeHoverColor => Color.White;
        public override Color ButtonForePressColor => Color.White;
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => UIColor.Gray;
        public override Color RectHoverColor => UIColor.Gray;
        public override Color RectPressColor => Color.FromArgb(121, 123, 129);
        public override Color TitleColor => UIColor.Gray;
        public override Color TitleForeColor => Color.White;
        public override Color GridSelectedColor => Color.Silver;
    }

    public class UIWhiteStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.White;
        public override Color PrimaryColor => Color.FromArgb(216, 219, 227);
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => Color.White;
        public override Color ButtonFillColor => Color.White;
        public override Color ButtonFillHoverColor => UIColor.LightBlue;
        public override Color ButtonFillPressColor => UIColor.LightBlue;
        public override Color ButtonForeColor => Color.FromArgb(0x60, 0x62, 0x66);
        public override Color ButtonForeHoverColor => UIColor.Blue;
        public override Color ButtonForePressColor => Color.FromArgb(74, 131, 229);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => Color.FromArgb(216, 219, 227);
        public override Color RectHoverColor => Color.FromArgb(197, 222, 255);
        public override Color RectPressColor => Color.FromArgb(74, 131, 229);
        public override Color AvatarFillColor => Color.FromArgb(130, 130, 130);
        public override Color TitleColor => Color.FromArgb(216, 219, 227);
        public override Color TitleForeColor => Color.FromArgb(0x60, 0x62, 0x66);
    }

    public class UIDarkBlueStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.DarkBlue;
        public override Color PrimaryColor => UIColor.DarkBlue;
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => UIColor.LightGray;
        public override Color ButtonFillColor => UIColor.DarkBlue;
        public override Color ButtonFillHoverColor => Color.FromArgb(190, 230, 253);
        public override Color ButtonFillPressColor => Color.FromArgb(169, 217, 242);
        public override Color ButtonForeColor => Color.FromArgb(130, 130, 130);
        public override Color ButtonForeHoverColor => Color.FromArgb(130, 130, 130);
        public override Color ButtonForePressColor => Color.FromArgb(130, 130, 130);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => Color.FromArgb(130, 130, 130);
        public override Color RectHoverColor => Color.FromArgb(130, 130, 130);
        public override Color RectPressColor => Color.FromArgb(130, 130, 130);
        public override Color TitleColor => Color.FromArgb(130, 130, 130);
        public override Color TitleForeColor => Color.White;
    }

    public class UIBlackStyle : UIBaseStyle
    {
        public override UIStyle Name => UIStyle.Black;
        public override Color PrimaryColor => Color.FromArgb(24, 24, 24);
        public override Color RegularColor => Color.FromArgb(120, 148, 182);
        public override Color SecondaryColor => Color.FromArgb(120, 148, 182);
        public override Color PlainColor => Color.FromArgb(24, 24, 24);
        public override Color ButtonFillColor => UIColor.DarkBlue;
        public override Color ButtonFillHoverColor => UIColor.RegularBlue;
        public override Color ButtonFillPressColor => UIColor.LightBlue;
        public override Color ButtonForeColor => Color.White;
        public override Color ButtonForeHoverColor => Color.FromArgb(130, 130, 130);
        public override Color ButtonForePressColor => Color.FromArgb(130, 130, 130);
        public override Color RectSelectedColor => RectPressColor;
        public override Color ButtonForeSelectedColor => ButtonForePressColor;
        public override Color ButtonFillSelectedColor => ButtonFillPressColor;
        public override Color RectColor => Color.FromArgb(130, 130, 130);
        public override Color RectHoverColor => Color.FromArgb(130, 130, 130);
        public override Color RectPressColor => Color.FromArgb(130, 130, 130);
        public override Color LabelForeColor => UIFontColor.Plain;
        public override Color DropDownControlColor => UIFontColor.Primary;
        public override Color CheckBoxColor => UIColor.Blue;

        public override Color TitleColor => Color.FromArgb(130, 130, 130);
        public override Color TitleForeColor => Color.White;
        public override Color LineForeColor => UIFontColor.Plain;
        public override Color ContextMenuColor => UIColor.RegularGray;

        public override Color GridStripeOddColor => UIColor.RegularGray;
        public override Color GridSelectedColor => UIFontColor.Plain;

        public override Color GridSelectedForeColor => UIColor.White;

        public override Color ListItemSelectBackColor => UIColor.Blue;
        public override Color ListItemSelectForeColor => UIColor.LightBlue;

        public override Color ProgressIndicatorColor => UIColor.Blue;

        public override Color ProcessBarFillColor => PlainColor;

        public override Color ProcessBarForeColor => UIColor.RegularGray;

        public override Color ScrollBarForeColor => UIColor.RegularGray;

        public override Color SwitchActiveColor => UIColor.DarkBlue;

        public override Color SwitchInActiveColor => UIFontColor.Plain;

        public override Color SwitchFillColor => Color.White;

        public override Color TrackBarForeColor => UIColor.Blue;

        public override Color TrackBarRectColor => UIColor.Blue;

        public override Color TrackDisableColor => Color.Silver;

        public override Color TreeViewSelectedColor => UIFontColor.Secondary;

        public override Color TreeViewHoverColor => UIFontColor.Plain;
    }
}