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
 * 文件名称: UIProcessBar.cs
 * 文件说明: 进度条
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-19: V2.2.5 增加数值变化事件
 * 2021-08-07: V3.0.5 增加垂直方向的进度显示
 * 2021-08-14: V3.0.6 修改不显示百分比时，显示数值
 * 2021-10-14: V3.0.8 调整最小高度为3
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-09-05: V3.2.3 修改最大值至少为1
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-09-05: V3.4.2 修复值计算过程中的Int越界问题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public sealed class UIProcessBar : UIControl
    {
        private int maximum = 100;

        public delegate void OnValueChanged(object sender, int value);

        public event OnValueChanged ValueChanged;

        public UIProcessBar()
        {
            SetStyleFlags(true, false);
            MinimumSize = new Size(70, 3);
            Size = new Size(300, 29);
            ShowText = false;

            fillColor = UIColor.LightBlue;
            foreColor = UIColor.Blue;
        }
        private UILine.LineDirection direction = UILine.LineDirection.Horizontal;

        [DefaultValue(UILine.LineDirection.Horizontal)]
        [Description("线条方向"), Category("SunnyUI")]
        public UILine.LineDirection Direction
        {
            get => direction;
            set
            {
                if (direction != value)
                {
                    direction = value;
                    Invalidate();
                }
            }
        }

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
                    ValueChanged?.Invoke(this, posValue);
                    Invalidate();
                }
            }
        }

        private bool showValue = true;

        [DefaultValue(true)]
        [Description("显示进度值"), Category("SunnyUI")]
        public bool ShowValue
        {
            get => showValue;
            set
            {
                showValue = value;
                Invalidate();
            }
        }

        [DefaultValue(1)]
        [Description("步进值"), Category("SunnyUI")]
        public int Step { get; set; } = 1;

        public void StepIt()
        {
            Value = Math.Min(Value + Step, Maximum);
        }

        private Bitmap image;
        private int imageRadius;

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float processSize;
            string processText;

            if (Direction == UILine.LineDirection.Horizontal)
                processSize = posValue * 1.0f / Maximum * Width;
            else
                processSize = posValue * 1.0f / Maximum * Height;

            if (ShowPercent)
                processText = (posValue * 100.0 / maximum).ToString("F" + decimalCount) + "%";
            else
                processText = posValue.ToString();

            Size sf = TextRenderer.MeasureText(processText, Font);
            bool canShow = Height > sf.Height + 4;

            if (ShowValue && canShow)
            {
                DrawString(e.Graphics, processText, Font, foreColor, Size, Padding, TextAlign);
            }

            if (image == null || image.Width != Width || image.Height != Height || imageRadius != Radius)
            {
                image?.Dispose();
                image = new Bitmap(Width, Height);
                imageRadius = Radius;
            }

            using Graphics g = image.Graphics();
            g.Clear(Color.Transparent);
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            g.SetHighQuality();
            g.FillRoundRectangle(rectColor, rect, Radius);
            g.DrawRoundRectangle(rectColor, rect, Radius);
            if (ShowValue && canShow)
            {
                DrawString(g, processText, Font, fillColor, Size, Padding, TextAlign);
            }

            if (Direction == UILine.LineDirection.Horizontal)
            {
                e.Graphics.DrawImage(image,
                    new RectangleF(0, 0, processSize, image.Height),
                    new RectangleF(0, 0, processSize, image.Height),
                    GraphicsUnit.Pixel);
            }
            else
            {
                e.Graphics.DrawImage(image,
                    new RectangleF(0, image.Height - processSize, image.Width, processSize),
                    new RectangleF(0, image.Height - processSize, image.Width, processSize),
                    GraphicsUnit.Pixel);
            }
        }

        private void DrawString(Graphics g, string str, Font font, Color color, Size size, Padding padding, ContentAlignment align, int offsetX = 0, int offsetY = 0)
        {
            if (str.IsNullOrEmpty()) return;
            Size sf = TextRenderer.MeasureText(str, font);
            using Brush br = color.Brush();
            switch (align)
            {
                case ContentAlignment.MiddleCenter:
                    g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f + offsetX,
                        padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f + offsetY);
                    break;

                case ContentAlignment.TopLeft:
                    g.DrawString(str, font, br, padding.Left + offsetX, padding.Top + offsetY);
                    break;

                case ContentAlignment.TopCenter:
                    g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f + offsetX, padding.Top + offsetY);
                    break;

                case ContentAlignment.TopRight:
                    g.DrawString(str, font, br, size.Width - sf.Width - padding.Right + offsetX, padding.Top + offsetY);
                    break;

                case ContentAlignment.MiddleLeft:
                    g.DrawString(str, font, br, padding.Left + offsetX, padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f + offsetY);
                    break;

                case ContentAlignment.MiddleRight:
                    g.DrawString(str, font, br, size.Width - sf.Width - padding.Right + offsetX, padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f + offsetY);
                    break;

                case ContentAlignment.BottomLeft:
                    g.DrawString(str, font, br, padding.Left + offsetX, size.Height - sf.Height - padding.Bottom + offsetY);
                    break;

                case ContentAlignment.BottomCenter:
                    g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f + offsetX, size.Height - sf.Height - padding.Bottom + offsetY);
                    break;

                case ContentAlignment.BottomRight:
                    g.DrawString(str, font, br, size.Width - sf.Width - padding.Right + offsetX, size.Height - sf.Height - padding.Bottom + offsetY);
                    break;
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        private bool showPercent = true;

        [Description("显示文字百分比"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ShowPercent
        {
            get => showPercent;
            set
            {
                showPercent = value;
                Invalidate();
            }
        }

        [Description("显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(1)]
        public int DecimalPlaces
        {
            get => decimalCount;
            set => decimalCount = Math.Max(value, 0);
        }

        private int decimalCount = 1;

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.ProcessBarFillColor;
            foreColor = uiColor.ProcessBarForeColor;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }
    }
}