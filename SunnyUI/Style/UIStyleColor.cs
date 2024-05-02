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
 * 文件名称: UIStyleColor.cs
 * 文件说明: 控件样式定义类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-11-05: V3.5.2 重构主题
******************************************************************************/

using System.Drawing;

namespace Sunny.UI
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class UIBaseStyle
    {
        public virtual UIStyle Name { get; protected set; }

        public virtual Color PrimaryColor { get; protected set; }
        public virtual Color RegularColor { get; protected set; }
        public virtual Color SecondaryColor { get; protected set; }
        public virtual Color PlainColor { get; protected set; }

        public virtual Color FillDisableColor { get; protected set; }
        public virtual Color RectDisableColor { get; protected set; }
        public virtual Color ForeDisableColor { get; protected set; }

        public virtual Color RectColor { get; protected set; }

        //Button
        public virtual Color ButtonFillColor { get; protected set; }
        public virtual Color ButtonFillHoverColor { get; protected set; }
        public virtual Color ButtonFillPressColor { get; protected set; }
        public virtual Color ButtonFillSelectedColor { get; protected set; }
        public virtual Color ButtonFillColor2 { get; protected set; }
        public virtual Color ButtonFillLightColor { get; protected set; }
        public virtual Color ButtonForeLightColor { get; protected set; }

        public virtual Color ButtonForeColor { get; protected set; }
        public virtual Color ButtonForeHoverColor { get; protected set; }
        public virtual Color ButtonForePressColor { get; protected set; }
        public virtual Color ButtonForeSelectedColor { get; protected set; }

        public virtual Color ButtonRectColor { get; protected set; }
        public virtual Color ButtonRectHoverColor { get; protected set; }
        public virtual Color ButtonRectPressColor { get; protected set; }
        public virtual Color ButtonRectSelectedColor { get; protected set; }

        //Battery
        public virtual Color BatteryFillColor { get; protected set; }

        //Avatar
        public virtual Color AvatarFillColor { get; protected set; }
        public virtual Color AvatarForeColor { get; protected set; }

        //ImageButton
        public virtual Color ImageButtonForeColor { get; protected set; }

        //Breadcrumb
        public virtual Color BreadcrumbUnSelectedColor { get; protected set; }

        //CheckBox
        public virtual Color CheckBoxColor { get; protected set; }
        public virtual Color CheckBoxForeColor { get; protected set; }

        //Logo
        public virtual Color LogoForeColor { get; protected set; }
        public virtual Color LogoFillColor { get; protected set; }

        //Line
        public virtual Color LineForeColor { get; protected set; }
        public virtual Color LineFillColor { get; protected set; }
        public virtual Color LineRectColor { get; protected set; }

        //TrackBar
        public virtual Color TrackBarFillColor { get; protected set; }
        public virtual Color TrackBarForeColor { get; protected set; }
        public virtual Color TrackBarRectColor { get; protected set; }
        public virtual Color TrackDisableColor { get; protected set; }

        //Switch
        public virtual Color SwitchActiveColor { get; protected set; }
        public virtual Color SwitchInActiveColor { get; protected set; }
        public virtual Color SwitchFillColor { get; protected set; }
        public virtual Color SwitchRectDisableColor { get; protected set; }

        //Label
        public virtual Color LabelForeColor { get; protected set; }

        //LabelRotate
        public virtual Color LabelRotateFrameColor { get; protected set; }
        public virtual Color LabelRotateForeColor { get; protected set; }

        //ColorWheel
        public virtual Color ColorWheelFrameColor { get; protected set; }
        public virtual Color ColorWheelBackColor { get; protected set; }

        //ContextMenu
        public virtual Color ContextMenuColor { get; protected set; }
        public virtual Color ContextMenuSelectedColor { get; protected set; }
        public virtual Color ContextMenuForeColor { get; protected set; }

        //ScrollBar
        public virtual Color ScrollBarFillColor { get; protected set; }
        public virtual Color ScrollBarForeColor { get; protected set; }
        public virtual Color ScrollBarFillHoverColor { get; protected set; }
        public virtual Color ScrollBarFillPressColor { get; protected set; }

        //ProcessBar
        public virtual Color ProcessBarFillColor { get; protected set; }
        public virtual Color ProcessBarForeColor { get; protected set; }
        public virtual Color ProcessBackColor { get; protected set; }

        //SmoothLabel
        public virtual Color SmoothLabelForeColor { get; protected set; }
        public virtual Color SmoothLabelRectColor { get; protected set; }

        //ScrollingText
        public virtual Color ScrollingTextFillColor { get; protected set; }
        public virtual Color ScrollingTextForeColor { get; protected set; }

        //LedLabel
        public virtual Color LedLabelForeColor { get; protected set; }

        //UIMarkLabel
        public virtual Color MarkLabelForeColor { get; protected set; }

        //Page
        public virtual Color PageBackColor { get; protected set; }
        public virtual Color PageRectColor { get; protected set; }
        public virtual Color PageForeColor { get; protected set; }
        public virtual Color PageTitleFillColor { get; protected set; }
        public virtual Color PageTitleForeColor { get; protected set; }

        //Form
        public virtual Color FormTitleColor { get; protected set; }
        public virtual Color FormTitleForeColor { get; protected set; }
        public virtual Color FormForeColor { get; protected set; }
        public virtual Color FormRectColor { get; protected set; }
        public virtual Color FormControlBoxForeColor { get; protected set; }
        public virtual Color FormControlBoxFillHoverColor { get; protected set; }
        public virtual Color FormControlBoxCloseFillHoverColor { get; protected set; }
        public virtual Color FormBackColor { get; protected set; }

        //ProgressIndicator
        public virtual Color ProgressIndicatorColor { get; protected set; }

        //NavBar
        public virtual Color NavBarMenuSelectedColor { get; protected set; }

        //NavMenu
        public virtual Color NavMenuMenuSelectedColor { get; protected set; }

        //TabControl
        public virtual Color TabControlTabSelectedColor { get; protected set; }
        public virtual Color TabControlBackColor { get; protected set; }

        //Panel
        public virtual Color PanelForeColor { get; protected set; }
        public virtual Color PanelRectColor { get; protected set; }
        public virtual Color PanelFillColor { get; protected set; }
        public virtual Color PanelFillColor2 { get; protected set; }
        public virtual Color PanelTitleColor { get; protected set; }
        public virtual Color PanelTitleForeColor { get; protected set; }

        //DropDownControl
        public virtual Color DropDownControlColor { get; protected set; }
        public virtual Color DropDownPanelFillColor { get; protected set; }
        public virtual Color DropDownPanelForeColor { get; protected set; }

        //ListBox
        public virtual Color ListItemSelectBackColor { get; protected set; }
        public virtual Color ListItemSelectForeColor { get; protected set; }
        public virtual Color ListItemHoverColor { get; protected set; }
        public virtual Color ListBarFillColor { get; protected set; }
        public virtual Color ListBarForeColor { get; protected set; }
        public virtual Color ListBackColor { get; protected set; }
        public virtual Color ListForeColor { get; protected set; }

        //TreeView
        public virtual Color TreeViewSelectedForeColor { get; protected set; }
        public virtual Color TreeViewSelectedColor { get; protected set; }
        public virtual Color TreeViewHoverColor { get; protected set; }
        public virtual Color TreeViewBarFillColor { get; protected set; }
        public virtual Color TreeViewBarForeColor { get; protected set; }
        public virtual Color TreeViewForeColor { get; protected set; }
        public virtual Color TreeViewBackColor { get; protected set; }
        public virtual Color TreeViewLineColor { get; protected set; }

        //TextBox
        public virtual Color EditorBackColor { get; protected set; }

        //DataGridView
        public virtual Color GridSelectedColor { get; protected set; }
        public virtual Color GridSelectedForeColor { get; protected set; }
        public virtual Color GridStripeEvenColor { get; protected set; }
        public virtual Color GridStripeOddColor { get; protected set; }
        public virtual Color GridLineColor { get; protected set; }
        public virtual Color GridTitleColor { get; protected set; }
        public virtual Color GridTitleForeColor { get; protected set; }
        public virtual Color GridForeColor { get; protected set; }
        public virtual Color GridBarFillColor { get; protected set; }
        public virtual Color GridBarForeColor { get; protected set; }

        //DataGridViewFooter
        public virtual Color DataGridViewFooterForeColor { get; protected set; }

        //Pagination
        public virtual Color PaginationForeColor { get; protected set; }

        //FlowLayoutPanel
        public virtual Color FlowLayoutPanelBarFillColor { get; protected set; }
        public virtual Color FlowLayoutPanelBarForeColor { get; protected set; }

        //SplitContainer
        public virtual Color SplitContainerArrowColor { get; protected set; }

        //RoundProcess
        public virtual Color RoundProcessForeColor2 { get; protected set; }

        protected Color ForeColor { get; set; }

        public UIBaseStyle DropDownStyle { get; protected set; }

        public virtual UIBaseStyle Init(Color color, UIStyle style, Color reverseColor, Color foreColor)
        {
            Name = style;

            FillDisableColor = Color.FromArgb(244, 244, 244);
            RectDisableColor = Color.FromArgb(173, 178, 181);
            ForeDisableColor = Color.FromArgb(109, 109, 103);
            ForeColor = foreColor;

            PrimaryColor = color;
            RectColor = color;
            Color[] colors = Color.White.GradientColors(PrimaryColor, 16);
            Color[] colors1 = PrimaryColor.GradientColors(Color.Black, 16);

            PlainColor = colors[1];
            SecondaryColor = colors[5];
            RegularColor = colors[10];

            SplitContainerArrowColor = PrimaryColor;

            GridSelectedColor = colors[3];
            GridSelectedForeColor = foreColor;
            GridStripeEvenColor = colors[0];
            GridStripeOddColor = PlainColor;
            GridLineColor = colors[13];
            GridTitleColor = PrimaryColor;
            GridTitleForeColor = reverseColor;
            GridForeColor = foreColor;
            GridBarFillColor = PlainColor;
            GridBarForeColor = PrimaryColor;

            FormTitleColor = PrimaryColor;
            FormTitleForeColor = reverseColor;
            FormForeColor = foreColor;
            FormRectColor = PrimaryColor;
            FormControlBoxFillHoverColor = colors[12];
            FormControlBoxCloseFillHoverColor = UIColor.Red;
            FormBackColor = PlainColor;
            FormControlBoxForeColor = Color.White;

            PanelForeColor = foreColor;
            PanelRectColor = PrimaryColor;
            PanelFillColor = PlainColor;
            PanelFillColor2 = PlainColor;
            PanelTitleColor = PrimaryColor;
            PanelTitleForeColor = reverseColor;

            ButtonFillColor = PrimaryColor;
            ButtonFillHoverColor = colors[12];
            ButtonFillPressColor = colors1[3];
            ButtonFillSelectedColor = colors1[3];
            ButtonFillColor2 = ButtonFillColor;
            ButtonFillLightColor = PlainColor;
            ButtonForeLightColor = PrimaryColor;

            ButtonForeColor = reverseColor;
            ButtonForeHoverColor = reverseColor;
            ButtonForePressColor = reverseColor;
            ButtonForeSelectedColor = reverseColor;

            ButtonRectColor = PrimaryColor;
            ButtonRectHoverColor = colors[12];
            ButtonRectSelectedColor = colors1[3];
            ButtonRectPressColor = colors1[3];

            BatteryFillColor = PlainColor;

            BreadcrumbUnSelectedColor = colors[6];

            AvatarFillColor = Color.Silver;
            AvatarForeColor = PrimaryColor;

            ImageButtonForeColor = foreColor;

            CheckBoxColor = PrimaryColor;
            CheckBoxForeColor = foreColor;

            LogoForeColor = foreColor;
            LogoFillColor = PrimaryColor;

            LineForeColor = foreColor;
            LineFillColor = PlainColor;
            LineRectColor = PrimaryColor;

            TrackBarFillColor = PlainColor;
            TrackBarForeColor = PrimaryColor;
            TrackBarRectColor = PrimaryColor;
            TrackDisableColor = Color.Silver;

            SwitchActiveColor = PrimaryColor;
            SwitchInActiveColor = Color.Gray;
            SwitchFillColor = Color.White;
            SwitchRectDisableColor = RectDisableColor;

            LabelForeColor = foreColor;

            LabelRotateFrameColor = ButtonRectColor;
            LabelRotateForeColor = foreColor;

            ColorWheelFrameColor = ButtonRectColor;
            ColorWheelBackColor = PlainColor;

            ContextMenuColor = PlainColor;
            ContextMenuSelectedColor = PrimaryColor;
            ContextMenuForeColor = foreColor;

            ScrollBarFillColor = PlainColor;
            ScrollBarForeColor = PrimaryColor;
            ScrollBarFillHoverColor = ButtonFillHoverColor;
            ScrollBarFillPressColor = ButtonFillPressColor;

            ProcessBarFillColor = PlainColor;
            ProcessBarForeColor = PrimaryColor;
            ProcessBackColor = colors[6];

            SmoothLabelForeColor = ButtonForeColor;
            SmoothLabelRectColor = ButtonRectColor;

            ScrollingTextFillColor = PlainColor;
            ScrollingTextForeColor = PrimaryColor;

            LedLabelForeColor = PrimaryColor;
            MarkLabelForeColor = PrimaryColor;

            PageBackColor = PlainColor;
            PageRectColor = PrimaryColor;
            PageForeColor = foreColor;
            PageTitleFillColor = Color.FromArgb(76, 76, 76);
            PageTitleForeColor = Color.White;

            ProgressIndicatorColor = PrimaryColor;

            NavBarMenuSelectedColor = PrimaryColor;

            TabControlTabSelectedColor = PrimaryColor;
            TabControlBackColor = PlainColor;

            DropDownControlColor = foreColor;
            DropDownPanelFillColor = PlainColor;
            DropDownPanelForeColor = foreColor;

            ListItemSelectBackColor = PrimaryColor;
            ListItemSelectForeColor = PlainColor;
            ListItemHoverColor = colors[3];
            ListBarFillColor = PlainColor;
            ListBarForeColor = PrimaryColor;
            ListBackColor = Color.White;
            ListForeColor = foreColor;

            TreeViewSelectedColor = PrimaryColor;
            TreeViewSelectedForeColor = Color.White;
            TreeViewHoverColor = colors[3];
            TreeViewBarFillColor = PlainColor;
            TreeViewBarForeColor = PrimaryColor;
            TreeViewForeColor = foreColor;
            TreeViewBackColor = Color.White;
            TreeViewLineColor = foreColor;

            EditorBackColor = UIColor.White;

            NavMenuMenuSelectedColor = PrimaryColor;

            DataGridViewFooterForeColor = foreColor;

            PaginationForeColor = PrimaryColor;

            FlowLayoutPanelBarFillColor = PlainColor;
            FlowLayoutPanelBarForeColor = PrimaryColor;

            RoundProcessForeColor2 = Color.Black;

            DropDownStyle = this;

            return this;
        }

        public virtual void LoadFromFile()
        {
        }

        public override string ToString()
        {
            return Name.DisplayText();
        }

        public virtual bool BuiltIn => true;
    }

    public class UIPurpleStyle : UIBaseStyle
    {
        public UIPurpleStyle()
        {
            base.Init(UIColor.Purple, UIStyle.Purple, Color.White, UIFontColor.Primary);
        }
    }

    public class UIColorfulStyle : UIBaseStyle
    {
        public UIColorfulStyle()
        {
            base.Init(Color.FromArgb(0, 190, 172), UIStyle.Colorful, Color.White, UIFontColor.Primary);
        }

        public void Init(Color styleColor, Color foreColor)
        {
            Init(styleColor, UIStyle.Colorful, foreColor, UIFontColor.Primary);
        }
    }

    public class UICustomStyle : UIBlueStyle
    {
        public override UIStyle Name => UIStyle.Custom;
    }

    public class UIInheritedStyle : UIBaseStyle
    {
        public UIInheritedStyle()
        {
            base.Init(UIColor.Blue, UIStyle.Inherited, Color.White, UIFontColor.Primary);
        }
    }

    public class UIBlueStyle : UIBaseStyle
    {
        public UIBlueStyle()
        {
            base.Init(UIColor.Blue, UIStyle.Blue, Color.White, UIFontColor.Primary);
        }
    }

    public class UIGreenStyle : UIBaseStyle
    {
        public UIGreenStyle()
        {
            base.Init(UIColor.Green, UIStyle.Green, Color.White, UIFontColor.Primary);
        }
    }

    public class UIRedStyle : UIBaseStyle
    {
        public UIRedStyle()
        {
            base.Init(UIColor.Red, UIStyle.Red, Color.White, UIFontColor.Primary);
        }
    }

    public class UIOrangeStyle : UIBaseStyle
    {
        public UIOrangeStyle()
        {
            base.Init(UIColor.Orange, UIStyle.Orange, Color.White, UIFontColor.Primary);
        }
    }

    public class UIGrayStyle : UIBaseStyle
    {
        public UIGrayStyle()
        {
            base.Init(UIColor.Gray, UIStyle.Gray, Color.White, UIFontColor.Primary);
        }
    }

    public class UIDarkBlueStyle : UIBaseStyle
    {
        public UIDarkBlueStyle()
        {
            base.Init(UIColor.Blue, UIStyle.DarkBlue, Color.White, UIFontColor.White);

            PrimaryColor = UIColor.DarkBlue;
            RectColor = Color.FromArgb(18, 58, 92);
            PlainColor = UIColor.DarkBlue;
            Color ForeColor = Color.White;

            Color[] colors = Color.White.GradientColors(PrimaryColor, 16);
            Color[] colors1 = PrimaryColor.GradientColors(Color.Black, 16);

            FormTitleColor = colors1[2];
            FormTitleForeColor = ForeColor;
            FormForeColor = ForeColor;
            FormRectColor = RectColor;
            FormControlBoxFillHoverColor = colors[12];
            FormControlBoxCloseFillHoverColor = UIColor.Red;
            FormBackColor = PrimaryColor;
            FormControlBoxForeColor = ForeColor;

            ButtonFillLightColor = UIStyles.Blue.PlainColor;
            ButtonForeLightColor = UIStyles.Blue.PrimaryColor;

            PageBackColor = PlainColor;
            PageRectColor = PrimaryColor;
            PageForeColor = ForeColor;
            PageTitleFillColor = Color.FromArgb(76, 76, 76);
            PageTitleForeColor = ForeColor;

            AvatarFillColor = Color.Silver;
            AvatarForeColor = PrimaryColor;

            LabelForeColor = ForeColor;

            LineForeColor = ForeColor;
            LineFillColor = PlainColor;
            LineRectColor = ForeColor;

            CheckBoxForeColor = ForeColor;

            ContextMenuColor = Color.FromArgb(18, 58, 92);
            ContextMenuSelectedColor = Color.FromArgb(80, 160, 255);
            ContextMenuForeColor = ForeColor;

            PanelForeColor = ForeColor;
            PanelRectColor = RectColor;
            PanelFillColor = PlainColor;
            PanelFillColor2 = PlainColor;
            PanelTitleColor = FormTitleColor;
            PanelTitleForeColor = ForeColor;

            TrackBarFillColor = PlainColor;
            TrackBarForeColor = UIColor.Blue;
            TrackBarRectColor = UIColor.Blue;
            TrackDisableColor = Color.Silver;

            BreadcrumbUnSelectedColor = Color.Silver;

            ImageButtonForeColor = ForeColor;

            BatteryFillColor = PrimaryColor;

            DataGridViewFooterForeColor = ForeColor;

            GridSelectedColor = colors[13];
            GridSelectedForeColor = ForeColor;
            GridStripeEvenColor = PlainColor;
            GridStripeOddColor = PlainColor;
            GridLineColor = RectColor;
            GridTitleColor = Color.FromArgb(16, 45, 92);
            GridTitleForeColor = ForeColor;
            GridForeColor = ForeColor;
            GridBarFillColor = PlainColor;
            GridBarForeColor = ForeColor;

            TreeViewSelectedColor = PrimaryColor;
            TreeViewHoverColor = colors[13];
            TreeViewBarFillColor = PlainColor;
            TreeViewBarForeColor = ForeColor;
            TreeViewForeColor = ForeColor;
            TreeViewSelectedForeColor = ForeColor;
            TreeViewBackColor = PlainColor;
            TreeViewLineColor = ForeColor;

            PaginationForeColor = ForeColor;

            FlowLayoutPanelBarFillColor = PlainColor;
            FlowLayoutPanelBarForeColor = ForeColor;

            ListItemSelectBackColor = UIColor.Blue;
            ListItemSelectForeColor = ForeColor;
            ListItemHoverColor = colors[13];
            ListBarFillColor = PlainColor;
            ListBarForeColor = ForeColor;
            ListBackColor = PlainColor;
            ListForeColor = ForeColor;

            ScrollBarFillColor = PlainColor;
            ScrollBarForeColor = ForeColor;

            ScrollingTextFillColor = PlainColor;
            ScrollingTextForeColor = ForeColor;

            LogoForeColor = UIFontColor.Primary;

            DropDownControlColor = ForeColor;
            DropDownPanelFillColor = UIColor.LightBlue;
            DropDownPanelForeColor = UIFontColor.Primary;

            RoundProcessForeColor2 = Color.White;

            DropDownStyle = UIStyles.Blue;

            SplitContainerArrowColor = UIColor.Blue;
        }
    }

    public class UIBlackStyle : UIBaseStyle
    {
        public UIBlackStyle()
        {
            base.Init(UIColor.Blue, UIStyle.Black, Color.White, UIFontColor.Primary);

            PrimaryColor = Color.FromArgb(24, 24, 24);
            RectColor = Color.FromArgb(18, 58, 92);
            PlainColor = Color.FromArgb(24, 24, 24);
            Color ForeColor = Color.White;

            Color[] colors = Color.White.GradientColors(PrimaryColor, 16);
            Color[] colors1 = PrimaryColor.GradientColors(Color.Black, 16);

            FormTitleColor = colors1[2];
            FormTitleForeColor = ForeColor;
            FormForeColor = ForeColor;
            FormRectColor = RectColor;
            FormControlBoxFillHoverColor = colors[12];
            FormControlBoxCloseFillHoverColor = UIColor.Red;
            FormBackColor = PrimaryColor;
            FormControlBoxForeColor = ForeColor;

            ButtonFillLightColor = UIStyles.Blue.PlainColor;
            ButtonForeLightColor = UIStyles.Blue.PrimaryColor;

            PageBackColor = PlainColor;
            PageRectColor = PrimaryColor;
            PageForeColor = ForeColor;
            PageTitleFillColor = Color.FromArgb(76, 76, 76);
            PageTitleForeColor = ForeColor;

            AvatarFillColor = Color.Silver;
            AvatarForeColor = PrimaryColor;

            LabelForeColor = ForeColor;

            LineForeColor = ForeColor;
            LineFillColor = PlainColor;
            LineRectColor = ForeColor;

            CheckBoxForeColor = ForeColor;

            ContextMenuColor = Color.FromArgb(18, 58, 92);
            ContextMenuSelectedColor = Color.FromArgb(80, 160, 255);
            ContextMenuForeColor = ForeColor;

            PanelForeColor = ForeColor;
            PanelRectColor = RectColor;
            PanelFillColor = PlainColor;
            PanelFillColor2 = PlainColor;
            PanelTitleColor = FormTitleColor;
            PanelTitleForeColor = ForeColor;

            TrackBarFillColor = PlainColor;
            TrackBarForeColor = UIColor.Blue;
            TrackBarRectColor = UIColor.Blue;
            TrackDisableColor = Color.Silver;

            BreadcrumbUnSelectedColor = Color.Silver;

            ImageButtonForeColor = ForeColor;

            BatteryFillColor = PrimaryColor;

            DataGridViewFooterForeColor = ForeColor;

            GridSelectedColor = colors[13];
            GridSelectedForeColor = ForeColor;
            GridStripeEvenColor = PlainColor;
            GridStripeOddColor = PlainColor;
            GridLineColor = RectColor;
            GridTitleColor = Color.FromArgb(16, 45, 92);
            GridTitleForeColor = ForeColor;
            GridForeColor = ForeColor;
            GridBarFillColor = PlainColor;
            GridBarForeColor = ForeColor;

            TreeViewSelectedColor = PrimaryColor;
            TreeViewHoverColor = colors[13];
            TreeViewBarFillColor = PlainColor;
            TreeViewBarForeColor = ForeColor;
            TreeViewForeColor = ForeColor;
            TreeViewSelectedForeColor = ForeColor;
            TreeViewBackColor = PlainColor;
            TreeViewLineColor = ForeColor;

            PaginationForeColor = ForeColor;

            FlowLayoutPanelBarFillColor = PlainColor;
            FlowLayoutPanelBarForeColor = ForeColor;

            ListItemSelectBackColor = UIColor.Blue;
            ListItemSelectForeColor = ForeColor;
            ListItemHoverColor = colors[13];
            ListBarFillColor = PlainColor;
            ListBarForeColor = ForeColor;
            ListBackColor = PlainColor;
            ListForeColor = ForeColor;

            ScrollBarFillColor = PlainColor;
            ScrollBarForeColor = ForeColor;

            ScrollingTextFillColor = PlainColor;
            ScrollingTextForeColor = ForeColor;

            LogoForeColor = UIFontColor.Primary;

            DropDownControlColor = ForeColor;
            DropDownPanelFillColor = UIColor.LightBlue;
            DropDownPanelForeColor = UIFontColor.Primary;

            RoundProcessForeColor2 = Color.White;

            DropDownStyle = UIStyles.Blue;

            SplitContainerArrowColor = UIColor.Blue;
        }
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}