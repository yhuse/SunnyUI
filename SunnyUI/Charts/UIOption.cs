using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public class UIOption : IDisposable
    {
        public UITitle Title;

        public List<UISeries> Series = new List<UISeries>();

        public UILegend Legend;

        public void AddSeries(UISeries series)
        {
            Series.Add(series);
        }

        public void Dispose()
        {
            foreach (var series in Series)
            {
                series?.Dispose();
            }
   
            Series.Clear();
        }

        public int SeriesCount => Series.Count;
    }

    public class UILegend
    {

    }

   public class UITitle
    {
        public string Text { get; set; }

        public string SubText { get; set; }

        public UITextAlignment Left { get; set; }
    }

    public enum UITextAlignment
    {
        Left,
        Center,
        Right
    }

    public class UISeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type { get; set; }

        public int Radius { get; set; } = 50;

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UISeriesData> Data = new List<UISeriesData>();

        public void AddData(string name, double value)
        {
            Data.Add(new UISeriesData(name, value));
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class UICenter
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public UICenter() : this(50, 50)
        {
        }

        public UICenter(int left, int top)
        {
            Left = left;
            Top = top;
        }
    }

    public class UISeriesData
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public UISeriesData()
        {
        }

        public UISeriesData(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }

    public enum UISeriesType
    {
        Pie,
        Line,
        Bar
    }

    public enum UIChartStyleType
    {
        Default,
        Plain,
        Dark
    }

    public static class UIChartStyles
    {
        public static UIChartStyle Default = new UIDefaultChartStyle();

        public static UIChartStyle Plain = new UILightChartStyle();

        public static UIChartStyle Dark = new UIDarkChartStyle();

        public static UIChartStyle GetChartStyle(UIChartStyleType style)
        {
            if (style == UIChartStyleType.Default) return Default;
            if (style == UIChartStyleType.Dark) return Dark;
            return Plain;
        }
    }

    public class UIChartStyle
    {
        public virtual Color BackColor => Color.FromArgb(244, 244, 244);

        public virtual Color ForeColor => Color.FromArgb(54, 54, 54);

        public int ColorCount => 11;

        public virtual Color[] SeriesColor
        {
            get
            {
                return new[]
                {
                    Color.FromArgb(241,42,38),
                    Color.FromArgb(43,71,85),
                    Color.FromArgb(69,161,168),
                    Color.FromArgb(229,125,96),
                    Color.FromArgb(125,200,175),
                    Color.FromArgb(101,159,132),
                    Color.FromArgb(216,130,27),
                    Color.FromArgb(195,160,152),
                    Color.FromArgb(109,112,115),
                    Color.FromArgb(79,101,112),
                    Color.FromArgb(193,204,211)
                };
            }
        }
    }

    public class UIDefaultChartStyle : UIChartStyle
    {
    }

    public class UILightChartStyle : UIChartStyle
    {
        public override Color[] SeriesColor
        {
            get
            {
                return new[]
                {
                    Color.FromArgb(0,163,219),
                    Color.FromArgb(0,199,235),
                    Color.FromArgb(0,227,230),
                    Color.FromArgb(131,232,187),
                    Color.FromArgb(255,217,91),
                    Color.FromArgb(255,153,120),
                    Color.FromArgb(255,104,139),
                    Color.FromArgb(245,89,168),
                    Color.FromArgb(247,139,205),
                    Color.FromArgb(241,185,242),
                    Color.FromArgb(156,149,245)
                };
            }
        }
    }

    public class UIDarkChartStyle : UIChartStyle
    {
        public override Color BackColor => Color.FromArgb(54, 54, 54);

        public override Color ForeColor => Color.FromArgb(239, 239, 239);

        public override Color[] SeriesColor
        {
            get
            {
                return new[]
                {
                    Color.FromArgb(242,99,95),
                    Color.FromArgb(103,154,160),
                    Color.FromArgb(246,152,130),
                    Color.FromArgb(122,194,170),
                    Color.FromArgb(255,119,74),
                    Color.FromArgb(244,220,120),
                    Color.FromArgb(98,163,117),
                    Color.FromArgb(83,186,189),
                    Color.FromArgb(105,137,170),
                    Color.FromArgb(124,203,143),
                    Color.FromArgb(255,154,59)
                };
            }
        }
    }
}