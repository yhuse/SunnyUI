using Sunny.UI.Charts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIPieChart : UIChart
    {
        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            emptyOption = new UIOption();

            emptyOption.Title = new UITitle();
            emptyOption.Title.Text = "SunnyUI";
            emptyOption.Title.SubText = "PieChart";
            emptyOption.Title.Left = UITextAlignment.Center;

            var series = new UISeries();
            series.Name = "饼状图";
            series.Type = UISeriesType.Pie;
            series.Center = new UICenter(50, 50);
            series.Radius = 75;
            for (int i = 0; i < 5; i++)
            {
                series.AddData("Data" + i, (i + 1) * 20);
            }

            emptyOption.Series.Add(series);
        }

        protected override void DrawTitle(Graphics g, UITitle title)
        {
        }

        protected override void DrawSeries(Graphics g, List<UISeries> series)
        {
            if (series == null || series.Count == 0) return;

            for (int pieIndex = 0; pieIndex < series.Count; pieIndex++)
            {
                var pie = series[pieIndex];
                if (!Angles.ContainsKey(pieIndex))
                {
                    Angles.TryAdd(pieIndex, new ConcurrentDictionary<int, Angle>());
                }

                RectangleF rect = GetSeriesRect(pie);
                double all = 0;
                foreach (var data in pie.Data)
                {
                    all += data.Value;
                }

                if (all.IsZero()) return;
                float start = 0;
                for (int i = 0; i < pie.Data.Count; i++)
                {
                    float angle = (float)(pie.Data[i].Value * 360.0f / all);
                    Angles[pieIndex].AddOrUpdate(i, new Angle(start, angle));
                    start += angle;
                }

                for (int i = 0; i < pie.Data.Count; i++)
                {
                    Color color = ChartStyle.SeriesColor[i % ChartStyle.ColorCount];
                    RectangleF rectx = new RectangleF(rect.X - 10, rect.Y - 10, rect.Width + 20, rect.Width + 20);
                    g.FillPie(color, ActiveIndex == i ? rectx : rect, Angles[pieIndex][i].Start - 90, Angles[pieIndex][i].Sweep);
                }
            }
        }

        private readonly ConcurrentDictionary<int, ConcurrentDictionary<int, Angle>> Angles = new ConcurrentDictionary<int, ConcurrentDictionary<int, Angle>>();

        protected override void DrawLegend(Graphics g, UILegend legend)
        {
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            UIOption option = Option ?? EmptyOption;

            int index = -1;
            for (int pieIndex = 0; pieIndex < option.SeriesCount; pieIndex++)
            {
                RectangleF rect = GetSeriesRect(option.Series[pieIndex]);
                if (!e.Location.InRect(rect)) continue;

                PointF pf = new PointF( rect.Left +rect.Width/2.0f,rect.Top +rect.Height/2.0f);
                if (MathEx.CalcDistance(e.Location, pf) * 2 > rect.Width) continue;

                double az =MathEx.CalcAngle(e.Location,pf);
                for (int azIndex = 0; azIndex < option.Series[pieIndex].Data.Count; azIndex++)
                {
                    if (az >= Angles[pieIndex][azIndex].Start && az <= Angles[pieIndex][azIndex].Start + Angles[pieIndex][azIndex].Sweep)
                    {
                        index = azIndex;
                        break;
                    }
                }
            }

            ActiveIndex = index;
        }

       public double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        private int activeIndex = -1;

        [Browsable(false)]
        private int ActiveIndex
        {
            get => activeIndex;
            set
            {
                if (activeIndex != value)
                {
                    activeIndex = value;
                    Invalidate();
                }
            }
        }

        private RectangleF GetSeriesRect(UISeries series)
        {
            int left = series.Center.Left;
            int top = series.Center.Top;
            left = Width * left / 100;
            top = Height * top / 100;
            float halfRadius = Math.Min(Width, Height) * series.Radius / 200.0f;
            return new RectangleF(left - halfRadius, top - halfRadius, halfRadius * 2, halfRadius * 2);
        }

        public struct Angle
        {
            public float Start { get; set; }
            public float Sweep { get; set; }

            public Angle(float start, float sweep)
            {
                Start = start;
                Sweep = sweep;
            }
        }
    }
}