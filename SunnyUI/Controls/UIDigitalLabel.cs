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
 * 文件名称: UIDigitalLabel.cs
 * 文件说明: 冷液晶显示LCD标签
 * 当前版本: V3.6.1
 * 创建日期: 2023-12-01
 *
 * 2023-12-01: V3.6.1 增加文件说明
 * 2024-01-23: V3.6.3 更新绘制
******************************************************************************/

/******************************************************************************
 * sa-digital-number.ttf
 * Digital Numbers Fonts是一种固定宽度（web）字体，采用冷液晶显示（LCD）样式。
 * 依据SIL Open Font License 1.1授权协议免费公开。
 * https://github.com/s-a/digital-numbers-font
 * Copyright (c) 2015, Stephan Ahlf (stephan.ahlf@googlemail.com)
 * This Font Software is licensed under the SIL Open Font License, Version 1.1.
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ToolboxItem(true)]
    public class UIDigitalLabel : UIControl
    {
        public UIDigitalLabel()
        {
            SetStyleFlags();
            Size = new Size(208, 42);
            TextAlign = HorizontalAlignment.Right;
            ShowText = ShowRect = ShowFill = false;
            ForeColor = Color.Lime;
            BackColor = Color.Black;
        }

        private double digitalValue;

        [Description("浮点数"), Category("SunnyUI")]
        [DefaultValue(typeof(double), "0")]
        public double Value
        {
            get => digitalValue;
            set
            {
                digitalValue = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        public event EventHandler ValueChanged;

        private int digitalSize = 24;

        [Description("LCD字体大小"), Category("SunnyUI")]
        [DefaultValue(24)]
        public int DigitalSize
        {
            get => digitalSize;
            set
            {
                digitalSize = Math.Max(9, value);
                Invalidate();
            }
        }

        private int decimalPlaces = 2;

        [Description("浮点数，显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(2)]
        public int DecimalPlaces
        {
            get => decimalPlaces;
            set
            {
                decimalPlaces = Math.Max(0, value);
                Invalidate();
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using Font font = DigitalFont.Instance.GetFont(DigitalSize);
            using Brush br = new SolidBrush(ForeColor);

            string text = Value.ToString("F" + DecimalPlaces);
            SizeF sf = e.Graphics.MeasureString(text, font);
            float y = (Height - sf.Height) / 2.0f + 1 + Padding.Top;
            float x = Padding.Left;
            switch (TextAlign)
            {
                case HorizontalAlignment.Right:
                    x = Width - sf.Width - Padding.Right;
                    break;
                case HorizontalAlignment.Center:
                    x = (Width - sf.Width) / 2.0f;
                    break;
            }

            e.Graphics.DrawString(text, font, br, x, y);
        }

        private HorizontalAlignment textAlign = HorizontalAlignment.Right;

        /// <summary>
        /// 文字对齐方向
        /// </summary>
        [Description("文字对齐方向"), Category("SunnyUI")]
        [DefaultValue(HorizontalAlignment.Right)]
        public new HorizontalAlignment TextAlign
        {
            get => textAlign;
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    Invalidate();
                }
            }
        }

        private Point textOffset = new Point(0, 0);

        [Description("文字偏移"), Category("SunnyUI")]
        [DefaultValue(typeof(Point), "0, 0")]
        public Point TextOffset
        {
            get => textOffset;
            set
            {
                textOffset = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {

        }
    }
}
