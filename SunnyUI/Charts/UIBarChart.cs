using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIBarChart : UIChart
    {
        private bool NeedDraw;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData(BarOption);
        }

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
            if (o.XAxis.Data.Count == 0) return;

            NeedDraw = true;
            DrawBarWidth = DrawSize.Width * 1.0f / o.XAxis.Data.Count;

            double min = Double.MaxValue;
            double max = double.MinValue;
            foreach (var series in o.Series)
            {
                min = Math.Min(min, series.Data.Min());
                max = Math.Max(max, series.Data.Max());
            }

            bool minZero = false;
            bool maxZero = false;
            if (min > 0 && max > 0 && !o.YAxis.Scale)
            {
                min = 0;
                minZero = true;
            }

            if (min < 0 && max < 0 && !o.YAxis.Scale)
            {
                max = 0;
                maxZero = true;
            }

            UIChartHelper.CalcDegreeScale(min, max, o.YAxis.SplitNumber,
                out int start, out int end, out double interval);

            YAxisStart = start;
            YAxisEnd = end;
            YAxisInterval = interval;

            float x1 = 100.0f / ((o.XAxis.Data.Count * 2) + o.XAxis.Data.Count + 1);
            x1 = DrawSize.Width * x1 / 100.0f / o.SeriesCount;
            float x2 = x1 * 2;

            for (int i = 0; i < o.SeriesCount; i++)
            {
                float barX = DrawOrigin.X;
                var series = o.Series[i];
                Bars.TryAdd(i, new List<BarInfo>());

                for (int j = 0; j < series.Data.Count; j++)
                {
                    if (minZero)
                    {
                        float h = (float)(DrawSize.Height * series.Data[j] / (end * interval));
                        Bars[i].Add(new BarInfo()
                        {
                            Rect = new RectangleF(
                            barX + x1 * (i + 1) + x2 * i,
                            DrawOrigin.Y - h,
                            x2, h)
                        });
                    }
                    else if (maxZero)
                    {
                    }
                    else
                    {
                    }

                    barX += DrawBarWidth;
                }
            }
        }

        private Point DrawOrigin;
        private Size DrawSize;
        private float DrawBarWidth;
        private int YAxisStart;
        private int YAxisEnd;
        private double YAxisInterval;
        private readonly ConcurrentDictionary<int, List<BarInfo>> Bars = new ConcurrentDictionary<int, List<BarInfo>>();

        [Browsable(false)]
        private UIBarOption BarOption
        {
            get
            {
                UIOption option = Option ?? EmptyOption;
                return (UIBarOption)option;
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

            var series = new UIBarSeries();
            series.Name = "Bar1";
            series.AddData(1);
            series.AddData(5);
            series.AddData(2);
            series.AddData(4);
            series.AddData(3);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(2);
            series.AddData(1);
            series.AddData(5);
            series.AddData(3);
            series.AddData(4);
            option.Series.Add(series);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            emptyOption = option;
        }

        protected override void DrawOption(Graphics g)
        {
            if (BarOption == null) return;
            if (!NeedDraw) return;
            DrawAxis(g);
            DrawTitle(g, BarOption.Title);
            DrawSeries(g, BarOption.Series);
            DrawLegend(g, BarOption.Legend);
        }

        private void DrawAxis(Graphics g)
        {
            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            g.DrawLine(ChartStyle.ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));

            if (BarOption.XAxis.AxisTick.Show)
            {
                float start;

                if (BarOption.XAxis.AxisTick.AlignWithLabel)
                {
                    start = DrawOrigin.X + DrawBarWidth / 2.0f;
                    for (int i = 0; i < BarOption.XAxis.Data.Count; i++)
                    {
                        g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length);
                        start += DrawBarWidth;
                    }
                }
                else
                {
                    start = DrawOrigin.X;
                    for (int i = 0; i <= BarOption.XAxis.Data.Count; i++)
                    {
                        g.DrawLine(ChartStyle.ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length);
                        start += DrawBarWidth;
                    }
                }
            }

            if (BarOption.XAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.X + DrawBarWidth / 2.0f;
                foreach (var data in BarOption.XAxis.Data)
                {
                    SizeF sf = g.MeasureString(data, SubFont);
                    g.DrawString(data, SubFont, ChartStyle.ForeColor, start - sf.Width / 2.0f, DrawOrigin.Y + BarOption.XAxis.AxisTick.Length);
                    start += DrawBarWidth;
                }
            }

            if (BarOption.YAxis.AxisTick.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    g.DrawLine(ChartStyle.ForeColor, DrawOrigin.X, start, DrawOrigin.X - BarOption.YAxis.AxisTick.Length, start);
                    start -= DrawBarHeight;
                }
            }

            if (BarOption.YAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.Y;
                float DrawBarHeight = DrawSize.Height * 1.0f / (YAxisEnd - YAxisStart);
                int idx = 0;
                for (int i = YAxisStart; i <= YAxisEnd; i++)
                {
                    string label = BarOption.YAxis.AxisLabel.GetLabel(i * YAxisInterval, idx);
                    SizeF sf = g.MeasureString(label, SubFont);
                    g.DrawString(label, SubFont, ChartStyle.ForeColor, DrawOrigin.X - BarOption.YAxis.AxisTick.Length - sf.Width, start - sf.Height / 2.0f);
                    start -= DrawBarHeight;
                }
            }
        }

        private void DrawSeries(Graphics g, List<UIBarSeries> series)
        {
            if (series == null || series.Count == 0) return;

            for (int i = 0; i < Bars.Count; i++)
            {
                var bars = Bars[i];
                foreach (var info in bars)
                {
                    g.FillRectangle(ChartStyle.SeriesColor[i], info.Rect);
                }
            }
        }

        internal class BarInfo
        {
            public RectangleF Rect { get; set; }
        }
    }
}