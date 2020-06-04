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

        [DefaultValue(100)]
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

        [DefaultValue(36)]
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

        [DefaultValue(true)]
        public bool MultiColor
        {
            get => multiColor;
            set
            {
                multiColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "230, 80, 80")]
        public Color ColorEmpty
        {
            get => colorEmpty;
            set
            {
                colorEmpty = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "220, 155, 40")]
        public Color ColorDanger
        {
            get => colorDanger;
            set
            {
                colorDanger = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "110, 190, 40")]
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
        [Category("自定义")]
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
        [Category("自定义")]
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