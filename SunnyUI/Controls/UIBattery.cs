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
 * 文件名称: UIBattery.cs
 * 文件说明: 电池电量图标
 * 当前版本: V2.2
 * 创建日期: 2020-06-04
 *
 * 2020-06-04: V2.2.5 增加文件
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIBattery : UIControl
    {
        private Color colorDanger = UIColor.Orange;

        private Color colorEmpty = UIColor.Red;

        private Color colorSafe = UIColor.Green;

        private bool multiColor = true;

        private int power = 100;

        private int symbolSize = 36;

        public UIBattery()
        {
            ShowRect = false;
            fillColor = UIStyles.Blue.PlainColor;
            Width = 48;
            Height = 24;
        }

        [DefaultValue(100), Description("电量"), Category("SunnyUI")]
        public int Power
        {
            get => power;
            set
            {
                value = Math.Min(100, Math.Max(0, value));
                power = value;
                Invalidate();
            }
        }

        [DefaultValue(36), Description("图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => symbolSize;
            set
            {
                symbolSize = Math.Max(value, 16);
                symbolSize = Math.Min(value, 64);
                Invalidate();
            }
        }

        [DefaultValue(true), Description("多种颜色"), Category("SunnyUI")]
        public bool MultiColor
        {
            get => multiColor;
            set
            {
                multiColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "230, 80, 80"), Description("电量为空颜色"), Category("SunnyUI")]
        public Color ColorEmpty
        {
            get => colorEmpty;
            set
            {
                colorEmpty = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "220, 155, 40"), Description("电量少时颜色"), Category("SunnyUI")]
        public Color ColorDanger
        {
            get => colorDanger;
            set
            {
                colorDanger = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "110, 190, 40"), Description("电量安全颜色"), Category("SunnyUI")]
        public Color ColorSafe
        {
            get => colorSafe;
            set
            {
                colorSafe = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        ///     填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.PlainColor;
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.FillPath(fillColor, path);
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            var fa_battery_empty = 0xf244;
            var fa_battery_quarter = 0xf243;
            var fa_battery_half = 0xf242;
            var fa_battery_three_quarters = 0xf241;
            var fa_battery_full = 0xf240;

            int ShowSymbol;
            var color = GetForeColor();
            if (Power > 90)
            {
                ShowSymbol = fa_battery_full;
                if (multiColor) color = ColorSafe;
            }
            else if (Power > 62.5)
            {
                ShowSymbol = fa_battery_three_quarters;
                if (multiColor) color = ColorSafe;
            }
            else if (Power > 37.5)
            {
                ShowSymbol = fa_battery_half;
                if (multiColor) color = ColorSafe;
            }
            else if (Power > 10)
            {
                ShowSymbol = fa_battery_quarter;
                if (multiColor) color = ColorDanger;
            }
            else
            {
                ShowSymbol = fa_battery_empty;
                if (multiColor) color = ColorEmpty;
            }

            g.DrawFontImage(ShowSymbol, SymbolSize, color, new Rectangle(0, 0, Width, Height));
        }
    }
}