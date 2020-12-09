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
 * 文件名称: UIBarChartEx.cs
 * 文件说明: 柱状图
 * 当前版本: V2.2
 * 创建日期: 2020-09-26
 *
 * 2020-09-26: V2.2.8 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UIBarChartEx : UIBarChart
    {
        protected override void CalcData()
        {
            Bars.Clear();
            NeedDraw = false;
            if (Option == null || Option.Series == null || Option.SeriesCount == 0) return;

            DrawOrigin = new Point(Option.Grid.Left, Height - Option.Grid.Bottom);
            DrawSize = new Size(Width - Option.Grid.Left - Option.Grid.Right,
                Height - Option.Grid.Top - Option.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            if (Option.Series.Count == 0) return;

            NeedDraw = true;
            DrawBarWidth = DrawSize.Width * 1.0f / Option.Series.Count;

            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var series in Option.Series)
            {
                min = Math.Min(min, series.Data.Min());
                max = Math.Max(max, series.Data.Max());
            }

            if (min > 0 && max > 0 && !Option.YAxis.Scale)
            {
                min = 0;
            }

            if (min < 0 && max < 0 && !Option.YAxis.Scale)
            {
                max = 0;
            }

            if (!Option.YAxis.MaxAuto) max = Option.YAxis.Max;
            if (!Option.YAxis.MinAuto) min = Option.YAxis.Min;

            if ((max - min).IsZero() && min.IsZero())
            {
                max = 100;
                min = 0;
            }
            else
            {
                if (max > 0) min = 0;
                else max = 0;
            }

            CalcDegreeScale(min, max, Option.YAxis.SplitNumber,
                out int start, out int end, out double interval, out int decimalCount);

            YAxisStart = start;
            YAxisEnd = end;
            YAxisInterval = interval;
            YAxisDecimalCount = decimalCount;
            float barX = DrawOrigin.X;

            if (Option.AutoSizeBars)
            {
                //每个柱子等宽
                float x1 = DrawSize.Width * 1.0f / DataCount / 4;
                float x2 = x1 * 2;

                if (Option.AutoSizeBarsCompact)
                {
                    //紧凑
                    for (int i = 0; i < Option.SeriesCount; i++)
                    {
                        var series = Option.Series[i];
                        Bars.TryAdd(i, new List<BarInfo>());
                        float sx = barX + x1 * ((series.Data.Count * 4 - (series.Data.Count * 2 + (series.Data.Count - 1) * Option.AutoSizeBarsCompactValue)) / 2.0f) + x1;

                        for (int j = 0; j < series.Data.Count; j++)
                        {
                            Color color = ChartStyle.GetColor(i);
                            if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                                color = series.Colors[j];

                            float ww = Math.Min(x2, series.MaxWidth);
                            float xx = sx - ww / 2.0f;

                            if (YAxisStart >= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (series.Data[j] - start * interval) / ((end - start) * interval)));

                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, DrawOrigin.Y - h, ww, h),
                                    Color = color
                                });
                            }
                            else if (YAxisEnd <= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (end * interval - series.Data[j]) / ((end - start) * interval)));
                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, Option.Grid.Top + 1, ww, h - 1),
                                    Color = color
                                });
                            }
                            else
                            {
                                float lowH = 0;
                                float highH = 0;
                                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                                float lowV = 0;
                                float highV = 0;
                                for (int k = YAxisStart; k <= YAxisEnd; k++)
                                {
                                    if (k < 0) lowH += DrawBarHeight;
                                    if (k > 0) highH += DrawBarHeight;
                                    if (k < 0) lowV += (float)YAxisInterval;
                                    if (k > 0) highV += (float)YAxisInterval;
                                }

                                if (series.Data[j] >= 0)
                                {
                                    float h = Math.Abs((float)(highH * series.Data[j] / highV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH - h, ww, h),
                                        Color = color
                                    });
                                }
                                else
                                {
                                    float h = Math.Abs((float)(lowH * series.Data[j] / lowV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH + 1, ww, h - 1),
                                        Color = color
                                    });
                                }
                            }

                            sx += x2 + x1 * Option.AutoSizeBarsCompactValue;
                        }

                        barX += x2 * 2 * series.Data.Count;
                    }
                }
                else
                {
                    //宽松
                    for (int i = 0; i < Option.SeriesCount; i++)
                    {
                        var series = Option.Series[i];
                        Bars.TryAdd(i, new List<BarInfo>());

                        for (int j = 0; j < series.Data.Count; j++)
                        {
                            Color color = ChartStyle.GetColor(i);
                            if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                                color = series.Colors[j];

                            float xx = barX + DrawSize.Width * 1.0f / DataCount / 2;
                            float ww = Math.Min(x2, series.MaxWidth);
                            xx -= ww / 2.0f;

                            if (YAxisStart >= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (series.Data[j] - start * interval) / ((end - start) * interval)));

                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, DrawOrigin.Y - h, ww, h),
                                    Color = color
                                });
                            }
                            else if (YAxisEnd <= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (end * interval - series.Data[j]) / ((end - start) * interval)));
                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, Option.Grid.Top + 1, ww, h - 1),
                                    Color = color
                                });
                            }
                            else
                            {
                                float lowH = 0;
                                float highH = 0;
                                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                                float lowV = 0;
                                float highV = 0;
                                for (int k = YAxisStart; k <= YAxisEnd; k++)
                                {
                                    if (k < 0) lowH += DrawBarHeight;
                                    if (k > 0) highH += DrawBarHeight;
                                    if (k < 0) lowV += (float)YAxisInterval;
                                    if (k > 0) highV += (float)YAxisInterval;
                                }

                                if (series.Data[j] >= 0)
                                {
                                    float h = Math.Abs((float)(highH * series.Data[j] / highV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH - h, ww, h),
                                        Color = color
                                    });
                                }
                                else
                                {
                                    float h = Math.Abs((float)(lowH * series.Data[j] / lowV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH + 1, ww, h - 1),
                                        Color = color
                                    });
                                }
                            }

                            barX += DrawSize.Width * 1.0f / DataCount;
                        }
                    }
                }
            }
            else
            {
                //每个序列等宽
                if (Option.AutoSizeBarsCompact)
                {
                    //紧凑

                    float CompactWidth = DrawBarWidth / 4.0f * Option.AutoSizeBarsCompactValue;

                    for (int i = 0; i < Option.SeriesCount; i++)
                    {
                        var series = Option.Series[i];
                        float x1 = DrawBarWidth / series.Data.Count / 4.0f;
                        float x2 = x1 * 2;
                        float ww = Math.Min(x2, series.MaxWidth);

                        float sx = barX + (DrawBarWidth - ww * series.Data.Count - (series.Data.Count - 1) * CompactWidth) / 2.0f;

                        Bars.TryAdd(i, new List<BarInfo>());
                        for (int j = 0; j < series.Data.Count; j++)
                        {
                            Color color = ChartStyle.GetColor(i);
                            if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                                color = series.Colors[j];

                            float xx = sx;

                            if (YAxisStart >= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (series.Data[j] - start * interval) / ((end - start) * interval)));

                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, DrawOrigin.Y - h, ww, h),
                                    Color = color
                                });
                            }
                            else if (YAxisEnd <= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (end * interval - series.Data[j]) / ((end - start) * interval)));
                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, Option.Grid.Top + 1, ww, h - 1),
                                    Color = color
                                });
                            }
                            else
                            {
                                float lowH = 0;
                                float highH = 0;
                                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                                float lowV = 0;
                                float highV = 0;
                                for (int k = YAxisStart; k <= YAxisEnd; k++)
                                {
                                    if (k < 0) lowH += DrawBarHeight;
                                    if (k > 0) highH += DrawBarHeight;
                                    if (k < 0) lowV += (float)YAxisInterval;
                                    if (k > 0) highV += (float)YAxisInterval;
                                }

                                if (series.Data[j] >= 0)
                                {
                                    float h = Math.Abs((float)(highH * series.Data[j] / highV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH - h, ww, h),
                                        Color = color
                                    });
                                }
                                else
                                {
                                    float h = Math.Abs((float)(lowH * series.Data[j] / lowV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH + 1, ww, h - 1),
                                        Color = color
                                    });
                                }
                            }

                            sx += ww + CompactWidth;
                        }

                        barX += DrawBarWidth;
                    }
                }
                else
                {
                    //宽松
                    for (int i = 0; i < Option.SeriesCount; i++)
                    {
                        var series = Option.Series[i];
                        float x1;
                        if (Option.FixedSeriesCount > 0)
                            x1 = DrawBarWidth / (Option.FixedSeriesCount * 2 + Option.FixedSeriesCount + 1);
                        else
                            x1 = DrawBarWidth / (series.Data.Count * 2 + series.Data.Count + 1);

                        float x2 = x1 * 2;

                        Bars.TryAdd(i, new List<BarInfo>());
                        for (int j = 0; j < series.Data.Count; j++)
                        {
                            Color color = ChartStyle.GetColor(i);
                            if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                                color = series.Colors[j];

                            float xx;
                            if (Option.FixedSeriesCount > 0)
                                xx = barX + DrawBarWidth / (series.Data.Count * 2 + series.Data.Count + 1) * ((j + 1) * 3 - 1);
                            else
                                xx = barX + x1 * ((j + 1) * 3 - 1);

                            float ww = Math.Min(x2, series.MaxWidth);
                            xx -= ww / 2.0f;

                            if (YAxisStart >= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (series.Data[j] - start * interval) / ((end - start) * interval)));

                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, DrawOrigin.Y - h, ww, h),
                                    Color = color
                                });
                            }
                            else if (YAxisEnd <= 0)
                            {
                                float h = Math.Abs((float)(DrawSize.Height * (end * interval - series.Data[j]) / ((end - start) * interval)));
                                Bars[i].Add(new BarInfo()
                                {
                                    Rect = new RectangleF(xx, Option.Grid.Top + 1, ww, h - 1),
                                    Color = color
                                });
                            }
                            else
                            {
                                float lowH = 0;
                                float highH = 0;
                                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                                float lowV = 0;
                                float highV = 0;
                                for (int k = YAxisStart; k <= YAxisEnd; k++)
                                {
                                    if (k < 0) lowH += DrawBarHeight;
                                    if (k > 0) highH += DrawBarHeight;
                                    if (k < 0) lowV += (float)YAxisInterval;
                                    if (k > 0) highV += (float)YAxisInterval;
                                }

                                if (series.Data[j] >= 0)
                                {
                                    float h = Math.Abs((float)(highH * series.Data[j] / highV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH - h, ww, h),
                                        Color = color
                                    });
                                }
                                else
                                {
                                    float h = Math.Abs((float)(lowH * series.Data[j] / lowV));
                                    Bars[i].Add(new BarInfo()
                                    {
                                        Rect = new RectangleF(xx, DrawOrigin.Y - lowH + 1, ww, h - 1),
                                        Color = color
                                    });
                                }
                            }
                        }

                        barX += DrawBarWidth;
                    }
                }
            }

            if (Option.ToolTip != null)
            {
                for (int i = 0; i < Option.Series.Count; i++)
                {
                    var series = Option.Series[i];
                    string str = Option.Series[i].Name;
                    for (int j = 0; j < series.Data.Count; j++)
                    {
                        str += '\n';
                        if (series.BarName.Count > 0 && j < series.BarName.Count)
                            str += series.BarName[j] + " : ";

                        str += series.Data[j].ToString(Option.ToolTip.ValueFormat);
                    }

                    Bars[i][0].Tips = str;
                }
            }
        }

        protected override void DrawSeries(Graphics g, List<UIBarSeries> series)
        {
            if (series == null || series.Count == 0) return;

            for (int i = 0; i < Bars.Count; i++)
            {
                var bars = Bars[i];
                for (int j = 0; j < bars.Count; j++)
                {
                    g.FillRectangle(bars[j].Color, bars[j].Rect);
                    var s = Option.Series[i];
                    if (s.ShowBarName)
                    {
                        if (s.BarName.Count > 0 && j < s.BarName.Count)
                        {
                            SizeF sf = g.MeasureString(s.BarName[j], SubFont);
                            if (s.Data[j] >= 0)
                                g.DrawString(s.BarName[j], SubFont, ChartStyle.ForeColor,
                                    bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                    bars[j].Rect.Bottom + 1);
                            else
                                g.DrawString(s.BarName[j], SubFont, ChartStyle.ForeColor,
                                    bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                    bars[j].Rect.Top - sf.Height);
                        }
                    }

                    if (s.ShowValue)
                    {
                        Font fontShow = null;
                        if (s.ShowValueFontSize > 0) fontShow = new Font(SubFont.Name, s.ShowValueFontSize);

                        string value = s.Data[j].ToString("F" + Option.YAxis.AxisLabel.DecimalCount);
                        SizeF sf = g.MeasureString(value, fontShow ?? SubFont);
                        if (s.Data[j] < 0)
                            g.DrawString(value, fontShow ?? SubFont, bars[j].Color,
                                bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                bars[j].Rect.Bottom + 1);
                        else
                            g.DrawString(value, fontShow ?? SubFont, bars[j].Color,
                                bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                bars[j].Rect.Top - sf.Height);

                        fontShow?.Dispose();
                    }
                }
            }

            for (int i = 0; i < Option.Series.Count; i++)
            {
                Bars[i][0].Size = g.MeasureString(Bars[i][0].Tips, SubFont);
            }
        }

        private int DataCount
        {
            get
            {
                int dataCount = 0;
                for (int i = 0; i < Option.SeriesCount; i++)
                {
                    dataCount += Option.Series[i].Data.Count;
                }

                return dataCount;
            }
        }

        protected override void DrawAxis(Graphics g)
        {
            if (YAxisStart >= 0) g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            if (YAxisEnd <= 0) g.DrawLine(ChartStyle.ForeColor, new Point(DrawOrigin.X, Option.Grid.Top), new Point(DrawOrigin.X + DrawSize.Width, Option.Grid.Top));

            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));

            //绘制X轴刻度
            if (Option.XAxis.AxisTick.Show)
            {
                float start;

                if (Option.XAxis.AxisTick.AlignWithLabel)
                {
                    if (Option.AutoSizeBars)
                    {
                        start = DrawOrigin.X;
                        for (int i = 0; i < Option.Series.Count; i++)
                        {
                            float w = DrawSize.Width * Option.Series[i].Data.Count * 1.0f / DataCount;
                            g.DrawLine(ChartStyle.ForeColor, start + (int)(w / 2), DrawOrigin.Y, start + (int)(w / 2), DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                            start += (int)w;
                        }
                    }
                    else
                    {
                        start = DrawOrigin.X + DrawBarWidth / 2.0f;
                        for (int i = 0; i < Option.Series.Count; i++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                            start += DrawBarWidth;
                        }
                    }
                }
                else
                {
                    start = DrawOrigin.X;

                    if (Option.AutoSizeBars)
                    {
                        for (int i = 0; i < Option.Series.Count; i++)
                        {
                            float w = DrawSize.Width * Option.Series[i].Data.Count * 1.0f / DataCount;
                            g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                            start += (int)w;
                        }

                        g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X + DrawSize.Width, DrawOrigin.Y, DrawOrigin.X + DrawSize.Width, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                    }
                    else
                    {
                        for (int i = 0; i <= Option.Series.Count; i++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                            start += DrawBarWidth;
                        }
                    }
                }
            }

            //绘制X轴标签
            if (Option.XAxis.AxisLabel.Show)
            {
                if (Option.AutoSizeBars)
                {
                    float start = DrawOrigin.X;
                    foreach (var data in Option.Series)
                    {
                        float w = DrawSize.Width * data.Data.Count * 1.0f / DataCount;
                        SizeF sf = g.MeasureString(data.Name, SubFont);
                        if (Option.XAxis.AxisLabel.Angle != 0)
                            g.DrawString(data.Name, SubFont, ChartStyle.ForeColor, new PointF(start + w / 2.0f - 10, DrawOrigin.Y + Option.Grid.Bottom / 2.0f),
                                new StringFormat() { Alignment = StringAlignment.Center }, (3600 - Option.XAxis.AxisLabel.Angle) % 360);
                        else
                            g.DrawString(data.Name, SubFont, ChartStyle.ForeColor, start + w / 2.0f - sf.Width / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length + Option.XAxis.AxisTick.Distance);
                        start += w;
                    }
                }
                else
                {
                    float start = DrawOrigin.X + DrawBarWidth / 2.0f;
                    foreach (var data in Option.Series)
                    {
                        SizeF sf = g.MeasureString(data.Name, SubFont);
                        if (Option.XAxis.AxisLabel.Angle != 0)
                            g.DrawString(data.Name, SubFont, ChartStyle.ForeColor, new PointF(start - 10, DrawOrigin.Y + Option.Grid.Bottom / 2.0f),
                            new StringFormat() { Alignment = StringAlignment.Center }, (3600 - Option.XAxis.AxisLabel.Angle) % 360);
                        else
                            g.DrawString(data.Name, SubFont, ChartStyle.ForeColor, start - sf.Width / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length + Option.XAxis.AxisTick.Distance);
                        start += DrawBarWidth;
                    }
                }

                SizeF sfName = g.MeasureString(Option.XAxis.Name, SubFont);
                g.DrawString(Option.XAxis.Name, SubFont, ChartStyle.ForeColor, DrawOrigin.X + (DrawSize.Width - sfName.Width) / 2.0f, DrawOrigin.Y + Option.XAxis.AxisTick.Length + Option.XAxis.AxisTick.Distance + sfName.Height);
            }

            //绘制Y轴刻度
            if (Option.YAxis.AxisTick.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, DrawOrigin.X - Option.YAxis.AxisTick.Length, start);

                    if (i != 0)
                    {
                        using (Pen pn = new Pen(ChartStyle.ForeColor))
                        {
                            pn.DashStyle = DashStyle.Dash;
                            pn.DashPattern = new float[] { 3, 3 };
                            g.DrawLine(pn, DrawOrigin.X, start, Width - Option.Grid.Right, start);
                        }
                    }
                    else
                    {
                        g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, Width - Option.Grid.Right, start);

                        // float lineStart = DrawOrigin.X;
                        // for (int j = 0; j <= BarOption.Series.Count; j++)
                        // {
                        //     g.DrawLine(ChartStyle.ForeColor, lineStart, start, lineStart, start + BarOption.XAxis.AxisTick.Length);
                        //     lineStart += DrawBarWidth;
                        // }
                    }

                    start -= DrawBarHeight;
                }
            }

            //绘制Y轴标签
            if (Option.YAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                int idx = 0;
                float wmax = 0;

                if (Option.YAxis.AxisLabel.AutoFormat)
                    Option.YAxis.AxisLabel.DecimalCount = YAxisDecimalCount;

                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    string label = Option.YAxis.AxisLabel.GetLabel(i * YAxisInterval, idx);
                    SizeF sf = g.MeasureString(label, SubFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - Option.YAxis.AxisTick.Length - sf.Width, start - sf.Height / 2.0f);
                    start -= DrawBarHeight;
                }

                SizeF sfname = g.MeasureString(Option.YAxis.Name, SubFont);
                int x = (int)(DrawOrigin.X - Option.YAxis.AxisTick.Length - wmax - sfname.Height);
                int y = (int)(Option.Grid.Top + (DrawSize.Height - sfname.Width) / 2);
                g.DrawString(Option.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(x, y),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (!Option.ToolTip.Visible) return;
                if (e.Location.X > Option.Grid.Left &&
                    e.Location.X < Width - Option.Grid.Right &&
                    e.Location.Y > Option.Grid.Top &&
                    e.Location.Y < Height - Option.Grid.Bottom)
                {
                    if (Option.AutoSizeBars)
                    {
                        float startX = DrawOrigin.X;
                        for (int i = 0; i < Option.Series.Count; i++)
                        {
                            float w = DrawSize.Width * Option.Series[i].Data.Count * 1.0f / DataCount;
                            if (e.Location.X >= startX && e.Location.X < startX + w)
                            {
                                SelectIndex = i;
                                break;
                            }

                            startX += w;
                        }
                    }
                    else
                    {
                        SelectIndex = (int)((e.Location.X - Option.Grid.Left) / DrawBarWidth);
                    }
                }
                else
                {
                    SelectIndex = -1;
                }

                if (SelectIndex >= 0 && Bars.Count > 0)
                {
                    if (tip.Text != Bars[selectIndex][0].Tips)
                    {
                        tip.Text = Bars[selectIndex][0].Tips;
                        tip.Size = new Size((int)Bars[selectIndex][0].Size.Width + 4, (int)Bars[selectIndex][0].Size.Height + 4);
                    }

                    int x = e.Location.X + 15;
                    int y = e.Location.Y + 20;
                    if (e.Location.X + 15 + tip.Width > Width - Option.Grid.Right)
                        x = e.Location.X - tip.Width - 2;
                    if (e.Location.Y + 20 + tip.Height > Height - Option.Grid.Bottom)
                        y = e.Location.Y - tip.Height - 2;

                    tip.Left = x;
                    tip.Top = y;
                    if (!tip.Visible) tip.Visible = Bars[selectIndex][0].Tips.IsValid();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        protected override void DrawToolTip(Graphics g)
        {
            if (selectIndex < 0) return;

            if (Option.AutoSizeBars)
            {
                float startX = DrawOrigin.X;

                for (int i = 0; i < Option.SeriesCount; i++)
                {
                    float w = DrawSize.Width * Option.Series[i].Data.Count * 1.0f / DataCount;

                    if (i == selectIndex)
                    {
                        if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Line)
                        {
                            float x = startX + w / 2.0f;
                            g.DrawLine(ChartStyle.ToolTipShadowColor, x, DrawOrigin.Y, x, Option.Grid.Top);
                        }

                        if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow)
                        {
                            g.FillRectangle(ChartStyle.ToolTipShadowColor, startX, Option.Grid.Top, w, Height - Option.Grid.Top - Option.Grid.Bottom);
                        }
                    }

                    startX += w;
                }
            }
            else
            {
                base.DrawToolTip(g);
            }
        }

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChartEx";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("Bar1");
            option.Legend.AddData("Bar2");
            option.Legend.AddData("Bar3");

            var series = new UIBarSeries();
            series.Name = "Bar1";
            series.AddData(1);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(2);
            series.AddData(3);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(4);
            series.AddData(5);
            series.AddData(6);
            option.Series.Add(series);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            option.ToolTip = new UIBarToolTip();
            option.ToolTip.AxisPointer.Type = UIAxisPointerType.Shadow;

            emptyOption = option;
        }
    }
}
