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
 * 文件名称: UIRoundProcess.cs
 * 文件说明: 圆形进度条
 * 当前版本: V3.1
 * 创建日期: 2021-04-08
 *
 * 2021-04-08: V3.0.2 增加文件说明
 * 2021-10-18: V3.0.8 增加显示小数位数
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-07-12: V3.4.0 内圈尺寸小的时候更新配色
 * 2023-07-15: V3.4.0 增加起始角度和扫描角度
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 圆形滚动条
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class UIRoundProcess : UIControl
    {
        public UIRoundProcess()
        {
            SetStyleFlags(true, false);
            Size = new Size(120, 120);
            Inner = 30;
            Outer = 50;

            fillColor = UIStyles.Blue.ProcessBarForeColor;
            foreColor = UIStyles.Blue.ProcessBarForeColor;
            rectColor = UIStyles.Blue.ProcessBackColor;

            ShowText = false;
            ShowRect = false;
        }

        private int startAngle = 0;

        [Description("起始角度，正北为0，顺时针0到360°"), Category("SunnyUI")]
        [DefaultValue(0)]
        public int StartAngle
        {
            get => startAngle;
            set
            {
                startAngle = value;
                Invalidate();
            }
        }

        private int sweepAngle = 360;

        [Description("扫描角度，范围0到360°"), Category("SunnyUI")]
        [DefaultValue(360)]
        public int SweepAngle
        {
            get => sweepAngle;
            set
            {
                sweepAngle = Math.Max(1, value);
                sweepAngle = Math.Min(360, value);
                Invalidate();
            }
        }

        [Description("显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(1)]
        public int DecimalPlaces
        {
            get => decimalCount;
            set
            {
                decimalCount = Math.Max(value, 0);
                Text = (posValue * 100.0 / maximum).ToString("F" + decimalCount) + "%";
            }
        }

        private int decimalCount = 1;

        private int maximum = 100;

        [DefaultValue(100)]
        [Description("最大值"), Category("SunnyUI")]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = Math.Max(1, value);
                Invalidate();
            }
        }

        private int inner = 30;
        private int outer = 50;

        [Description("内径")]
        [Category("SunnyUI")]
        [DefaultValue(30)]
        public int Inner
        {
            get => inner;
            set
            {
                inner = Math.Max(value, 0);
                Invalidate();
            }
        }

        [Description("外径")]
        [Category("SunnyUI")]
        [DefaultValue(50)]
        public int Outer
        {
            get => outer;
            set
            {
                outer = Math.Max(value, 5);
                Invalidate();
            }
        }

        /// <summary>
        ///     进度条前景色
        /// </summary>
        [Description("进度条前景色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ProcessColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        ///     进度条背景色
        /// </summary>
        [Description("进度条背景色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "185, 217, 255")]
        public Color ProcessBackColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        private int posValue;

        [DefaultValue(0)]
        [Description("当前位置"), Category("SunnyUI")]
        public int Value
        {
            get => posValue;
            set
            {
                value = Math.Max(value, 0);
                value = Math.Min(value, maximum);
                if (posValue != value)
                {
                    posValue = value;
                    Text = (posValue * 100.0 / maximum).ToString("F" + decimalCount) + "%";
                    ValueChanged?.Invoke(this, posValue);
                    Invalidate();
                }
            }
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            if (ShowText)
            {
                Size size = TextRenderer.MeasureText(Text, Font);
                if (Inner * 2 < size.Width - 4)
                    g.DrawString(Text, Font, ForeColor2, new Rectangle(0, 0, Width, Height), ContentAlignment.MiddleCenter);
                else
                    g.DrawString(Text, Font, ForeColor, new Rectangle(0, 0, Width, Height), ContentAlignment.MiddleCenter);
            }
        }

        private Color foreColor2 = Color.Black;
        public Color ForeColor2
        {
            get => foreColor2;
            set
            {
                foreColor2 = value;
                Invalidate();
            }
        }

        public delegate void OnValueChanged(object sender, int value);

        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            int iin = Math.Min(inner, outer);
            int iou = Math.Max(inner, outer);
            if (iin == iou)
            {
                iou = iin + 1;
            }

            inner = iin;
            outer = iou;

            g.FillFan(ProcessBackColor, ClientRectangle.Center(), Inner, Outer, StartAngle - 90, SweepAngle);
            g.FillFan(ProcessColor, ClientRectangle.Center(), Inner, Outer, StartAngle - 90, Value * 1.0f / Maximum * SweepAngle);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillColor = uiColor.ProcessBarForeColor;
            foreColor = uiColor.ProcessBarForeColor;
            rectColor = uiColor.ProcessBackColor;
            foreColor2 = uiColor.RoundProcessForeColor2;
        }

        [DefaultValue(false)]
        public bool ShowProcess
        {
            get => ShowText;
            set => ShowText = value;
        }
    }
}
