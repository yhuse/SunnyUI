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
 * 文件名称: UIBattery.cs
 * 文件说明: 电池电量图标
 * 当前版本: V3.1
 * 创建日期: 2020-06-04
 *
 * 2020-06-04: V2.2.5 增加文件
 * 2021-06-18: V3.0.4 修改可自定义背景色
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-11-16: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    /// <summary>
    /// 电池电量图标
    /// </summary>
    [DefaultProperty("Power")]
    [ToolboxItem(true)]
    public sealed class UIBattery : UIControl
    {
        private Color colorDanger = UIColor.Orange;

        private Color colorEmpty = UIColor.Red;

        private Color colorSafe = UIColor.Green;

        private bool multiColor = true;

        private int power = 100;

        private int symbolSize = 36;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIBattery()
        {
            SetStyleFlags(true, false);
            ShowRect = false;
            Width = 48;
            Height = 24;
            fillColor = UIStyles.Blue.BatteryFillColor;
        }

        /// <summary>
        /// 电量
        /// </summary>
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

        /// <summary>
        /// 字体图标大小
        /// </summary>
        [DefaultValue(36), Description("字体图标大小"), Category("SunnyUI")]
        public int SymbolSize
        {
            get => symbolSize;
            set
            {
                symbolSize = Math.Max(value, 16);
                symbolSize = Math.Min(value, 128);
                Invalidate();
            }
        }

        /// <summary>
        /// 多种颜色显示电量
        /// </summary>
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

        /// <summary>
        /// 电量为空颜色
        /// </summary>
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

        /// <summary>
        /// 电量少时颜色
        /// </summary>
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

        /// <summary>
        /// 电量安全颜色
        /// </summary>
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
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.BatteryFillColor;
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.FillPath(fillColor, path);
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
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