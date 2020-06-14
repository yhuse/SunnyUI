using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    public class UIPieOption : UIOption, IDisposable
    {
        public UITitle Title;

        public List<UIPieSeries> Series = new List<UIPieSeries>();

        public UIPieLegend Legend;

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

    public class UIDoughnutPieOption : UIOption, IDisposable
    {
        public UITitle Title;

        public List<UIDoughnutPieSeries> Series = new List<UIDoughnutPieSeries>();

        public UIPieLegend Legend;

        public UIPieToolTip ToolTip;

        public void AddSeries(UIDoughnutPieSeries series)
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

    public class UIPieLegend
    {
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;

        public UIOrient Orient { get; set; } = UIOrient.Vertical;

        public readonly List<string> Data = new List<string>();

        public int DataCount => Data.Count;

        public void AddData(string data)
        {
            Data.Add(data);
        }
    }

    public class UIPieSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type => UISeriesType.Pie;

        public int Radius { get; set; } = 70;

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

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

    public class UIDoughnutPieSeries : IDisposable
    {
        public string Name { get; set; }

        public UISeriesType Type { get; set; }

        public RadiusInOut Radius { get; set; } = new RadiusInOut(50,70);

        public UICenter Center { get; set; } = new UICenter(50, 50);

        public readonly List<UIPieSeriesData> Data = new List<UIPieSeriesData>();

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
}