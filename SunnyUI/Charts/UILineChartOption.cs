using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI.Charts
{
    public sealed class UILineOption : UIOption, IDisposable
    {
        public UIAxis XAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UIAxis YAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UIBarToolTip ToolTip { get; set; }

        public void Dispose()
        {
            Clear();
        }

        public UIChartGrid Grid = new UIChartGrid();

        public UIAxisType XAxisType { get; set; } = UIAxisType.Value;

        public UIAxisType YAxisType { get; set; } = UIAxisType.Value;

        public ConcurrentDictionary<string, UILineSeries> Series = new ConcurrentDictionary<string, UILineSeries>();

        public readonly List<UIScaleLine> XAxisScaleLines = new List<UIScaleLine>();

        public readonly List<UIScaleLine> YAxisScaleLines = new List<UIScaleLine>();
        public void AddSeries(UILineSeries series)
        {
            if (series.Name.IsNullOrEmpty()) return;
            Series.TryAdd(series.Name, series);
        }

        public void AddData(string name, double x, double y)
        {
            if (!Series.ContainsKey(name)) return;
            Series[name].Add(x, y);
        }

        public void AddData(string name, DateTime x, double y)
        {
            if (!Series.ContainsKey(name)) return;
            Series[name].Add(x, y);
        }

        public void AddData(string name, string x, double y)
        {
            if (!Series.ContainsKey(name)) return;
            Series[name].Add(x, y);
        }

        public void AddData(string name, List<double> x, List<double> y)
        {
            if (x.Count != y.Count) return;
            for (int i = 0; i < x.Count; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void AddData(string name, List<DateTime> x, List<double> y)
        {
            if (x.Count != y.Count) return;
            for (int i = 0; i < x.Count; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void AddData(string name, List<string> x, List<double> y)
        {
            if (x.Count != y.Count) return;
            for (int i = 0; i < x.Count; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void AddData(string name, double[] x, double[] y)
        {
            if (x.Length != y.Length) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void AddData(string name, DateTime[] x, double[] y)
        {
            if (x.Length != y.Length) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void AddData(string name, string[] x, double[] y)
        {
            if (x.Length != y.Length) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddData(name, x[i], y[i]);
            }
        }

        public void Clear()
        {
            foreach (var series in Series.Values)
            {
                series.Clear();
            }

            Series.Clear();
        }

        public void Clear(string name)
        {
            if (Series.ContainsKey(name))
            {
                Series[name].Clear();
            }
        }

        public void SetLabels(string[] labels)
        {
            XAxis.Clear();
            if (XAxis.Type == UIAxisType.Category)
            {
                foreach (var label in labels)
                {
                    AddLabel(label);
                }
            }
        }

        public void AddLabel(string label)
        {
            if (XAxis.Type == UIAxisType.Category)
            {
                XAxis.Data.Add(label);
            }
        }
    }

    public class UILineSeries
    {
        public string Name { get; private set; }

        public float Width { get; set; } = 1;
        public Color Color { get; set; }

        public UILinePointSymbol Symbol { get; set; } = UILinePointSymbol.None;
        public int SymbolSize { get; set; } = 1;

        public Color SymbolColor { get; set; }

        public bool CustomColor { get; set; }

        public bool Smooth { get; set; }

        public UILineSeries(string name)
        {
            Name = name;
            Color = UIColor.Blue;
        }

        public UILineSeries(string name, Color color)
        {
            Name = name;
            Color = color;
            CustomColor = true;
        }

        public readonly List<double> XData = new List<double>();

        public readonly List<double> YData = new List<double>();

        public readonly List<PointF> Points = new List<PointF>();

        public int DataCount => XData.Count;

        public void Clear()
        {
            XData.Clear();
            YData.Clear();
            Points.Clear();
        }

        public void Add(double x, double y)
        {
            XData.Add(x);
            YData.Add(y);
        }

        public void Add(DateTime x, double y)
        {
            DateTimeInt64 t = new DateTimeInt64(x);
            XData.Add(t);
            YData.Add(y);
        }

        public void Add(string x, double y)
        {
            int cnt = XData.Count;
            XData.Add(cnt);
            YData.Add(y);
        }
    }

    public enum UILinePointSymbol
    {
        None,
        Square,
        Diamond,
        Triangle,
        Circle,
        Plus,
        Star
    }
}
