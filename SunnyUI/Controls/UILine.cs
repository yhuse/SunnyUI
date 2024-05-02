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
 * 文件名称: UILine.cs
 * 文件说明: 分割线
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-01-05: V3.0.9 增加线的样式，支持透明背景
 * 2022-01-10: V3.1.0 修复了文本为空不显示的问题
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-11-26: V3.2.9 水平方向文字不居中时，可设置线条渐变色
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-11-16: V3.5.2 重构主题
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UILine : UIControl
    {
        public UILine()
        {
            SetStyleFlags(true, false);
            Size = new Size(360, 29);
            MinimumSize = new Size(1, 1);
            ForeColor = UIStyles.Blue.LineForeColor;
            fillColor = UIColor.Transparent;
            fillColor2 = UIStyles.Blue.PanelFillColor2;
            BackColor = UIColor.Transparent;
            rectColor = UIStyles.Blue.LineRectColor;
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            rectColor = uiColor.LineRectColor;
            ForeColor = uiColor.LineForeColor;
            fillColor2 = uiColor.PanelFillColor2;
            fillColor = UIColor.Transparent;
            BackColor = UIColor.Transparent;
        }

        private UILineCap startCap = UILineCap.None;

        [DefaultValue(UILineCap.None), Category("SunnyUI")]
        public UILineCap StartCap
        {
            get => startCap;
            set
            {
                startCap = value;
                Invalidate();
            }
        }

        private UILineCap endCap = UILineCap.None;

        [DefaultValue(UILineCap.None), Category("SunnyUI")]
        public UILineCap EndCap
        {
            get => endCap;
            set
            {
                endCap = value;
                Invalidate();
            }
        }

        private int lineCapSize = 6;

        [DefaultValue(6), Category("SunnyUI")]
        public int LineCapSize
        {
            get => lineCapSize;
            set
            {
                lineCapSize = value;
                Invalidate();
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

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            //
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            SizeF sf = new SizeF(0, 0);
            float x = 0;
            using Pen pen = new Pen(rectColor, lineSize);
            if (LineDashStyle != UILineDashStyle.None)
            {
                pen.DashStyle = (DashStyle)((int)LineDashStyle);
            }

            if (Direction == LineDirection.Horizontal)
            {
                if (Text.IsValid())
                {
                    sf = TextRenderer.MeasureText(Text, Font);
                    switch (TextAlign)
                    {
                        case ContentAlignment.TopLeft:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, (Height - lineSize) / 2), ContentAlignment.BottomLeft); break;
                        case ContentAlignment.TopCenter:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, (Height - lineSize) / 2), ContentAlignment.BottomCenter); break;
                        case ContentAlignment.TopRight:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, (Height - lineSize) / 2), ContentAlignment.BottomRight); break;
                        case ContentAlignment.MiddleLeft:
                            x = Padding.Left + TextInterval;
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), TextAlign); break;
                        case ContentAlignment.MiddleCenter:
                            x = (Width - sf.Width) / 2 - 2;
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), TextAlign); break;
                        case ContentAlignment.MiddleRight:
                            x = Width - sf.Width - TextInterval - 4 - Padding.Right;
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, 0, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), TextAlign); break;
                        case ContentAlignment.BottomLeft:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, (Height + lineSize) / 2, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), ContentAlignment.TopLeft); break;
                        case ContentAlignment.BottomCenter:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, (Height + lineSize) / 2, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), ContentAlignment.TopCenter); break;
                        case ContentAlignment.BottomRight:
                            g.DrawString(Text, Font, ForeColor, new Rectangle(Padding.Left + TextInterval + 2, (Height + lineSize) / 2, Width - Padding.Left - textInterval - 2 - Padding.Right - textInterval - 2, Height), ContentAlignment.TopRight); break;
                    }
                }

                int top = Height / 2;
                if (Text.IsValid())
                {
                    switch (TextAlign)
                    {
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.MiddleRight:
                            g.DrawLine(pen, Padding.Left, top, x, top);
                            g.DrawLine(pen, x + sf.Width + 2, top, Width - 2 - Padding.Left - Padding.Right, top);
                            break;
                        default:
                            if (LineColorGradient)
                            {
                                top = (Height - lineSize) / 2;
                                using LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(Width, 0), LineColor, LineColor2);
                                g.FillRectangle(br, new Rectangle(Padding.Left, top, Width - 2 - Padding.Left - Padding.Right, LineSize));
                            }
                            else
                            {
                                g.DrawLine(pen, Padding.Left, top, Width - 2 - Padding.Left - Padding.Right, top);
                            }
                            break;
                    }
                }
                else
                {
                    if (LineColorGradient)
                    {
                        top = (Height - lineSize) / 2;
                        using LinearGradientBrush br = new LinearGradientBrush(new Point(0, 0), new Point(Width, 0), LineColor, LineColor2);
                        g.FillRectangle(br, new Rectangle(Padding.Left, top, Width - 2 - Padding.Left - Padding.Right, LineSize));
                    }
                    else
                    {
                        g.DrawLine(pen, Padding.Left, top, Width - 2 - Padding.Left - Padding.Right, top);
                    }
                }

                switch (startCap)
                {
                    case UILineCap.Square:
                        top = Height / 2 - LineCapSize - 1;
                        g.FillRectangle(rectColor, new RectangleF(0, top, LineCapSize * 2, LineCapSize * 2));
                        break;
                    case UILineCap.Diamond:
                        break;
                    case UILineCap.Triangle:
                        break;
                    case UILineCap.Circle:
                        top = Height / 2 - LineCapSize - 1;
                        g.FillEllipse(rectColor, new RectangleF(0, top, LineCapSize * 2, LineCapSize * 2));
                        break;
                }

                switch (endCap)
                {
                    case UILineCap.Square:
                        top = Height / 2 - LineCapSize;
                        if (lineSize.Mod(2) == 1) top -= 1;
                        g.FillRectangle(rectColor, new RectangleF(Width - lineCapSize * 2 - 1, top, LineCapSize * 2, LineCapSize * 2));
                        break;
                    case UILineCap.Diamond:
                        break;
                    case UILineCap.Triangle:
                        break;
                    case UILineCap.Circle:
                        top = Height / 2 - LineCapSize - 1;
                        g.FillEllipse(rectColor, new RectangleF(Width - lineCapSize * 2 - 1, top, LineCapSize * 2, LineCapSize * 2));
                        break;
                }
            }
            else
            {
                int left = (Width - lineSize) / 2;
                g.DrawLine(pen, left, Padding.Top, left, Height - Padding.Top - Padding.Bottom);
            }
        }

        UILineDashStyle lineDashStyle = UILineDashStyle.None;
        [Description("线的样式"), Category("SunnyUI")]
        [DefaultValue(UILineDashStyle.None)]
        public UILineDashStyle LineDashStyle
        {
            get => lineDashStyle;
            set
            {
                if (lineDashStyle != value)
                {
                    lineDashStyle = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 线颜色
        /// </summary>
        [Description("线颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color LineColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 线颜色2
        /// </summary>
        [Description("线颜色2"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color LineColor2
        {
            get => fillColor2;
            set => SetFillColor2(value);
        }

        [Description("线颜色渐变"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool LineColorGradient
        {
            get => fillColorGradient;
            set
            {
                if (fillColorGradient != value)
                {
                    fillColorGradient = value;
                    Invalidate();
                }
            }
        }
    }
}