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
 * 文件名称: UILineChartOption.cs
 * 文件说明: 曲线图设置类
 * 当前版本: V2.2
 * 创建日期: 2020-10-01
 *
 * 2020-10-01: V2.2.8 完成曲线图表设置类
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Sunny.UI
{
    public sealed class UILineOption : UIOption, IDisposable
    {
        public UIAxis XAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UIAxis YAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UILineToolTip ToolTip { get; set; } = new UILineToolTip();

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

        public UILineWarningArea GreaterWarningArea { get; set; }
        public UILineWarningArea LessWarningArea { get; set; }

        public UILineSeries AddSeries(UILineSeries series)
        {
            if (series.Name.IsNullOrEmpty()) return null;
            series.Index = Series.Count;
            Series.TryAdd(series.Name, series);
            return series;
        }

        public UILineSeries AddSeries(string name)
        {
            if (name.IsNullOrEmpty()) return null;
            UILineSeries series = new UILineSeries(name);
            AddSeries(series);
            return series;
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

        public int AllDataCount()
        {
            int cnt = 0;
            foreach (var series in Series.Values)
            {
                cnt += series.DataCount;
            }

            return cnt;
        }

        public void GetAllDataYRange(out double min, out double max)
        {
            if (AllDataCount() == 0)
            {
                min = 0;
                max = 1;
            }
            else
            {
                min = double.MaxValue;
                max = double.MinValue;
                foreach (var series in Series.Values)
                {
                    if (series.DataCount > 0)
                    {
                        min = Math.Min(min, series.YData.Min());
                        max = Math.Max(max, series.YData.Max());
                    }
                }
            }
        }

        public void GetAllDataXRange(out double min, out double max)
        {
            if (AllDataCount() == 0)
            {
                min = 0;
                max = 1;
            }
            else
            {
                min = double.MaxValue;
                max = double.MinValue;
                foreach (var series in Series.Values)
                {
                    if (series.DataCount > 0)
                    {
                        min = Math.Min(min, series.XData.Min());
                        max = Math.Max(max, series.XData.Max());
                    }
                }
            }
        }
    }

    public class UILineToolTip : UIChartToolTip
    {

    }

    public class UILineSeries
    {
        public int Index { get; set; }
        public string Name { get; private set; }

        public float Width { get; set; } = 2;
        public Color Color { get; set; }

        public UILinePointSymbol Symbol { get; set; } = UILinePointSymbol.None;
        public int SymbolSize { get; set; } = 4;

        public int SymbolLineWidth { get; set; } = 1;

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

        private readonly List<double> PointsX = new List<double>();

        private readonly List<double> PointsY = new List<double>();

        public int DataCount => XData.Count;

        public bool GetNearestPoint(Point p, int offset, out double x, out double y, out int index)
        {
            x = 0;
            y = 0;
            index = -1;
            if (PointsX.Count == 0) return false;

            index = PointsX.BinarySearchNearIndex(p.X);
            if (p.X >= PointsX[index] - offset && p.X <= PointsX[index] + offset &&
                p.Y >= PointsY[index] - offset && p.Y <= PointsY[index] + offset)
            {
                x = XData[index];
                y = YData[index];
                return true;
            }

            return false;
        }

        public void Clear()
        {
            XData.Clear();
            YData.Clear();
            ClearPoints();
        }

        public void ClearPoints()
        {
            Points.Clear();
            PointsX.Clear();
            PointsY.Clear();
        }

        public void AddPoint(PointF point)
        {
            Points.Add(point);
            PointsX.Add(point.X);
            PointsY.Add(point.Y);
        }

        public void AddPoints(float[] x, float[] y)
        {
            if (x.Length != y.Length) return;
            if (x.Length == 0) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddPoint(new PointF(x[i], y[i]));
            }
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

    public struct UILineSelectPoint
    {
        public int SeriesIndex { get; set; }
        public string Name { get; set; }

        public int Index { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public Point Location { get; set; }
    }

    public class UILineWarningArea
    {
        public double Value { get; set; }

        public Color Color { get; set; } = Color.Red;

        public UILineWarningArea()
        {

        }

        public UILineWarningArea(double value)
        {
            Value = value;
        }

        public UILineWarningArea(double value, Color color)
        {
            Value = value;
            Color = color;
        }
    }
}
