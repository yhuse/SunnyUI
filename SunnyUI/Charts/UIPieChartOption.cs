using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    public class UIPieOption : UIOption, IDisposable
    {
        public List<UIPieSeries> Series = new List<UIPieSeries>();

        public UIPieToolTip ToolTip;

        public void AddSeries(UIPieSeries series)
        {
            Series.Clear();
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

    public class UIDoughnutOption : UIOption, IDisposable
    {
        public List<UIDoughnutSeries> Series = new List<UIDoughnutSeries>();

        public UIPieToolTip ToolTip;

        public void AddSeries(UIDoughnutSeries series)
        {
            Series.Clear();
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

    public class UIPieToolTip
    {
        public string Formatter { get; set; } = "{{a}}" + '\n' + "{{b}} : {{c}} ({{d}}%)";

        public string ValueFormat { get; set; } = "F0";
    }

    public class UIPieSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type => UISeriesType.Pie;

        public int Radius { get; set; } = 70;

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

        public UIPieSeriesLabel Label = new UIPieSeriesLabel();

        public void AddData(string name, double value)
        {
            Data.Add(new UIPieSeriesData(name, value));
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class RadiusInOut
    {
        public int Inner { get; set; }

        public int Outer { get; set; }

        public RadiusInOut(int inner,int outer)
        {
            Inner = inner;
            Outer = outer;
        }
    }

    public class UIDoughnutSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type { get; set; }

        public RadiusInOut Radius { get; set; } = new RadiusInOut(50,70);

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

        public UIPieSeriesLabel Label = new UIPieSeriesLabel();

        public void AddData(string name, double value)
        {
            Data.Add(new UIPieSeriesData(name, value));
        }

        public void Dispose()
        {
            Data.Clear();
        }
    }

    public class UIPieSeriesData
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public UIPieSeriesData()
        {
        }

        public UIPieSeriesData(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }

    public class UIPieSeriesLabel
    {
        public bool Show { get; set; } = false;

        public UIPieSeriesLabelPosition Position { get; set; } = UIPieSeriesLabelPosition.Center;

        public string Formatter { get; set; } = "{{b}}";
    }

    public enum UIPieSeriesLabelPosition
    {
        Outside,
        Inside,
        Center
    }
}