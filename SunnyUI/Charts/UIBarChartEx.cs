using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIBarChartEx : UIBarChart
    {
        protected override void CalcData(UIOption option)
        {
            Bars.Clear();
            NeedDraw = false;
            UIBarOption o = (UIBarOption)option;
            if (o == null || o.Series == null || o.SeriesCount == 0) return;

            DrawOrigin = new Point(BarOption.Grid.Left, Height - BarOption.Grid.Bottom);
            DrawSize = new Size(Width - BarOption.Grid.Left - BarOption.Grid.Right,
                Height - BarOption.Grid.Top - BarOption.Grid.Bottom);

            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            if (o.Series.Count == 0) return;

            NeedDraw = true;
            DrawBarWidth = DrawSize.Width * 1.0f / o.Series.Count;

            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var series in o.Series)
            {
                min = Math.Min(min, series.Data.Min());
                max = Math.Max(max, series.Data.Max());
            }

            if (min > 0 && max > 0 && !o.YAxis.Scale)
            {
                min = 0;
            }

            if (min < 0 && max < 0 && !o.YAxis.Scale)
            {
                max = 0;
            }

            if (!o.YAxis.MaxAuto) max = o.YAxis.Max;
            if (!o.YAxis.MinAuto) min = o.YAxis.Min;

            if ((max - min).IsZero())
            {
                max = 100;
                min = 0;
            }

            UIChartHelper.CalcDegreeScale(min, max, o.YAxis.SplitNumber,
                out int start, out int end, out double interval);

            YAxisStart = start;
            YAxisEnd = end;
            YAxisInterval = interval;
            float barX = DrawOrigin.X;

            for (int i = 0; i < o.SeriesCount; i++)
            {
                var series = o.Series[i];
                float x1;
                if (BarOption.FixedSeriesCount > 0)
                    x1 = DrawBarWidth / (BarOption.FixedSeriesCount * 2 + BarOption.FixedSeriesCount + 1);
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
                    if (BarOption.FixedSeriesCount > 0)
                        xx = barX + DrawBarWidth / (series.Data.Count * 2 + series.Data.Count + 1) * ((j + 1) * 3 - 1);
                    else
                        xx = barX + x1 * ((j + 1) * 3 - 1);

                    float ww = Math.Min(x2, series.MaxWidth);
                    xx = xx - ww / 2.0f;

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
                            Rect = new RectangleF(xx, BarOption.Grid.Top + 1, ww, h - 1),
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

                        // lowH.ConsoleWriteLine();
                        // highH.ConsoleWriteLine();

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

            if (BarOption.ToolTip != null)
            {
                for (int i = 0; i < BarOption.Series.Count; i++)
                {
                    var series = BarOption.Series[i];
                    string str = BarOption.Series[i].Name;
                    for (int j = 0; j < series.Data.Count; j++)
                    {
                        str += '\n';
                        if (series.BarName.Count > 0 && j < series.BarName.Count)
                            str += series.BarName[j] + " : ";

                        str += series.Data[j].ToString(BarOption.ToolTip.ValueFormat);
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
                    var s = BarOption.Series[i];
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
                        string value = s.Data[j].ToString("F" + BarOption.XAxis.AxisLabel.DecimalCount);
                        SizeF sf = g.MeasureString(value, SubFont);
                        if (s.Data[j] < 0)
                            g.DrawString(value, SubFont, bars[j].Color,
                                bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                bars[j].Rect.Bottom + 1);
                        else
                            g.DrawString(value, SubFont, bars[j].Color,
                                bars[j].Rect.Left + bars[j].Rect.Width / 2 - sf.Width / 2,
                                bars[j].Rect.Top - sf.Height);
                    }
                }
            }

            for (int i = 0; i < BarOption.Series.Count; i++)
            {
                Bars[i][0].Size = g.MeasureString(Bars[i][0].Tips, SubFont);
            }
        }

        protected override void DrawAxis(Graphics g)
        {
            if (YAxisStart >= 0) g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            if (YAxisEnd <= 0) g.DrawLine(ChartStyle.ForeColor, new Point(DrawOrigin.X, BarOption.Grid.Top), new Point(DrawOrigin.X + DrawSize.Width, BarOption.Grid.Top));

            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));

            if (BarOption.XAxis.AxisTick.Show)
            {
                float start;

                if (BarOption.XAxis.AxisTick.AlignWithLabel)
                {
                    start = DrawOrigin.X + DrawBarWidth / 2.0f;
                    for (int i = 0; i < BarOption.Series.Count; i++)
                    {
                        g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length);
                        start += DrawBarWidth;
                    }
                }
                else
                {
                    bool haveZero = false;
                    for (int i = YAxisStart; i <= YAxisEnd; i++)
                    {
                        if (i == 0)
                        {
                            haveZero = true;
                            break;
                        }
                    }

                    if (!haveZero)
                    {
                        start = DrawOrigin.X;
                        for (int i = 0; i <= BarOption.Series.Count; i++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length);
                            start += DrawBarWidth;
                        }
                    }
                }
            }

            if (BarOption.XAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.X + DrawBarWidth / 2.0f;
                foreach (var data in BarOption.Series)
                {
                    SizeF sf = g.MeasureString(data.Name, SubFont);
                    g.DrawString(data.Name, SubFont, ChartStyle.ForeColor, start - sf.Width / 2.0f, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length + BarOption.XAxis.AxisTick.Distance);
                    start += DrawBarWidth;
                }

                SizeF sfName = g.MeasureString(BarOption.XAxis.Name, SubFont);
                g.DrawString(BarOption.XAxis.Name, SubFont, ChartStyle.ForeColor, DrawOrigin.X + (DrawSize.Width - sfName.Width) / 2.0f, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length + BarOption.XAxis.AxisTick.Distance + sfName.Height);
            }

            if (BarOption.YAxis.AxisTick.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, DrawOrigin.X - BarOption.YAxis.AxisTick.Length, start);

                    if (i != 0)
                    {
                        using (Pen pn = new Pen(ChartStyle.ForeColor))
                        {
                            pn.DashStyle = DashStyle.Dash;
                            pn.DashPattern = new float[] { 3, 3 };
                            g.DrawLine(pn, DrawOrigin.X, start, Width - BarOption.Grid.Right, start);
                        }
                    }
                    else
                    {
                        g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, Width - BarOption.Grid.Right, start);

                        float lineStart = DrawOrigin.X;
                        for (int j = 0; j <= BarOption.Series.Count; j++)
                        {
                            g.DrawLine(ChartStyle.ForeColor, lineStart, start, lineStart, start + BarOption.XAxis.AxisTick.Length);
                            lineStart += DrawBarWidth;
                        }
                    }

                    start -= DrawBarHeight;
                }
            }

            if (BarOption.YAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                int idx = 0;
                float wmax = 0;
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    string label = BarOption.YAxis.AxisLabel.GetLabel(i * YAxisInterval, idx);
                    SizeF sf = g.MeasureString(label, SubFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - BarOption.YAxis.AxisTick.Length - sf.Width, start - sf.Height / 2.0f);
                    start -= DrawBarHeight;
                }

                SizeF sfname = g.MeasureString(BarOption.YAxis.Name, SubFont);
                int x = (int)(DrawOrigin.X - BarOption.YAxis.AxisTick.Length - wmax - sfname.Height);
                int y = (int)(BarOption.Grid.Top + (DrawSize.Height - sfname.Width) / 2);
                g.DrawString(BarOption.YAxis.Name, SubFont, ChartStyle.ForeColor, new Point(x, y),
                    new StringFormat() { Alignment = StringAlignment.Center }, 270);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (BarOption.ToolTip == null) return;
                if (e.Location.X > BarOption.Grid.Left && e.Location.X < Width - BarOption.Grid.Right
                                                       && e.Location.Y > BarOption.Grid.Top &&
                                                       e.Location.Y < Height - BarOption.Grid.Bottom)
                {
                    SelectIndex = (int)((e.Location.X - BarOption.Grid.Left) / DrawBarWidth);
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
                    if (e.Location.X + 15 + tip.Width > Width - BarOption.Grid.Right)
                        x = e.Location.X - tip.Width - 2;
                    if (e.Location.Y + 20 + tip.Height > Height - BarOption.Grid.Bottom)
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

        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChart";

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
