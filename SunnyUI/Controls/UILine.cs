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
 * 文件名称: UILine.cs
 * 文件说明: 分割线
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UILine : UIControl
    {
        public UILine()
        {
            Size = new Size(360, 29);
            MinimumSize = new Size(2, 2);
            foreColor = UIStyles.Blue.LineForeColor;
            fillColor = UIStyles.Blue.PlainColor;
        }

        public enum LineDirection
        {
            /// <summary>
            /// 水平的
            /// </summary>
            Horizontal,

            /// <summary>
            /// 竖直的
            /// </summary>
            Vertical
        }

        private LineDirection direction = LineDirection.Horizontal;

        [DefaultValue(LineDirection.Horizontal)]
        [Description("线条方向"), Category("SunnyUI")]
        public LineDirection Direction
        {
            get => direction;
            set
            {
                direction = value;
                Invalidate();
            }
        }

        private int lineSize = 1;

        [Description("线条宽度"), Category("SunnyUI")]
        [DefaultValue(1)]
        public int LineSize
        {
            get => lineSize;
            set
            {
                lineSize = Math.Max(1, value);
                Invalidate();
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.PlainColor;
            rectColor = uiColor.RectColor;
            foreColor = uiColor.LineForeColor;
            Invalidate();
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            g.Clear(fillColor);
            if (Direction == LineDirection.Horizontal)
            {
                int top = (Height - lineSize) / 2;
                g.FillRectangle(rectColor, Padding.Left, top, Width - Padding.Left - Padding.Right, lineSize);
                g.DrawLine(Color.FromArgb(50, fillColor), Padding.Left, top + lineSize, Width - Padding.Right - 1, top + lineSize);
            }
            else
            {
                int left = (Width - lineSize) / 2;
                g.FillRectangle(rectColor, left, Padding.Top, lineSize, Height - Padding.Top - Padding.Bottom);
                g.DrawLine(Color.FromArgb(50, fillColor), left + lineSize, Padding.Top, left + lineSize, Height - Padding.Bottom - 1);
            }
        }

        private int textInterval = 10;

        [DefaultValue(10)]
        [Description("文字边距间隔"), Category("SunnyUI")]
        public int TextInterval
        {
            get => textInterval;
            set
            {
                textInterval = value;
                Invalidate();
            }
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            if (Text.IsNullOrEmpty()) return;

            SizeF sf = g.MeasureString(Text, Font);

            if (Direction == LineDirection.Horizontal)
            {
                switch (TextAlign)
                {
                    case ContentAlignment.BottomLeft:
                        g.DrawString(Text, Font, foreColor, TextInterval + 2, (Height + lineSize) / 2.0f);
                        break;

                    case ContentAlignment.MiddleLeft:
                        g.FillRectangle(fillColor, TextInterval, 0, sf.Width + 3, Height);
                        g.DrawString(Text, Font, foreColor, TextInterval + 2, (Height - sf.Height) / 2);
                        break;

                    case ContentAlignment.TopLeft:
                        g.DrawString(Text, Font, foreColor, TextInterval + 2, (Height - lineSize) / 2.0f - sf.Height);
                        break;

                    case ContentAlignment.BottomCenter:
                        g.DrawString(Text, Font, foreColor, (Width - sf.Width) / 2, (Height + lineSize) / 2.0f);
                        break;

                    case ContentAlignment.MiddleCenter:
                        g.FillRectangle(fillColor, (Width - sf.Width) / 2 - 2, 0, sf.Width + 3, Height);
                        g.DrawString(Text, Font, foreColor, (Width - sf.Width) / 2, (Height - sf.Height) / 2);
                        break;

                    case ContentAlignment.TopCenter:
                        g.DrawString(Text, Font, foreColor, (Width - sf.Width) / 2, (Height - lineSize) / 2.0f - sf.Height);
                        break;

                    case ContentAlignment.BottomRight:
                        g.DrawString(Text, Font, foreColor, Width - sf.Width - TextInterval - 2, (Height + lineSize) / 2.0f);
                        break;

                    case ContentAlignment.MiddleRight:
                        g.FillRectangle(fillColor, Width - sf.Width - TextInterval - 4, 0, sf.Width + 3, Height);
                        g.DrawString(Text, Font, foreColor, Width - sf.Width - TextInterval - 2, (Height - sf.Height) / 2);
                        break;

                    case ContentAlignment.TopRight:
                        g.DrawString(Text, Font, foreColor, Width - sf.Width - TextInterval - 2, (Height - lineSize) / 2.0f - sf.Height);
                        break;
                }
            }

            //            if (Direction == LineDirection.Vertical)
            //            {
            //                StringFormat format = new StringFormat();
            //                format.FormatFlags = StringFormatFlags.DirectionVertical;
            //                g.DrawString(Text, Font, Brushes.Black, 15, 5, format);
            //            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color LineColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }
    }
}