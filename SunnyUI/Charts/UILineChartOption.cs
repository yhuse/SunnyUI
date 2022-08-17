/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UILineChartOption.cs
 * 文件说明: 曲线图设置类
 * 当前版本: V3.1
 * 创建日期: 2020-10-01
 *
 * 2020-10-01: V2.2.8 完成曲线图表设置类
 * 2022-07-15: V3.2.1 增加移除线的操作
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
        public bool ShowZeroLine { get; set; } = true;

        public bool ShowZeroValue { get; set; } = false;

        public UIAxis XAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UIAxis YAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public UIAxis Y2Axis { get; set; } = new UIAxis(UIAxisType.Value);

        public UILineToolTip ToolTip { get; set; } = new UILineToolTip();

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            Clear();
        }

        public UIChartGrid Grid = new UIChartGrid();

        public UIAxisType XAxisType { get; set; } = UIAxisType.Value;

        public ConcurrentDictionary<string, UILineSeries> Series = new ConcurrentDictionary<string, UILineSeries>();

        public readonly List<UIScaleLine> XAxisScaleLines = new List<UIScaleLine>();

        public readonly List<UIScaleLine> YAxisScaleLines = new List<UIScaleLine>();

        public readonly List<UIScaleLine> Y2AxisScaleLines = new List<UIScaleLine>();

        public UILineWarningArea GreaterWarningArea { get; set; }
        public UILineWarningArea LessWarningArea { get; set; }

        public UILineSeries AddSeries(UILineSeries series)
        {
            if (series == null) return null;
            if (series.Name.IsNullOrEmpty()) return null;
            if (ExistsSeries(series.Name)) return series;

            series.Index = Series.Count;
            Series.TryAdd(series.Name, series);
            return series;
        }

        public UILineSeries AddSeries(string seriesName, bool isY2 = false)
        {
            if (seriesName.IsNullOrEmpty()) return null;
            if (ExistsSeries(seriesName)) return Series[seriesName];

            UILineSeries series = new UILineSeries(seriesName, isY2);
            AddSeries(series);
            return series;
        }

        public bool ExistsSeries(string seriesName)
        {
            return seriesName.IsValid() && Series.ContainsKey(seriesName);
        }

        public void RemoveSeries(string seriesName)
        {
            if (ExistsSeries(seriesName))
            {
                Clear(seriesName);
                Series.TryRemove(seriesName, out _);
            }
        }

        public void AddData(string seriesName, double x, double y)
        {
            if (!Series.ContainsKey(seriesName)) return;
            Series[seriesName].Add(x, y);
        }

        public void AddData(string seriesName, DateTime x, double y)
        {
            if (!Series.ContainsKey(seriesName)) return;
            Series[seriesName].Add(x, y);
        }

        public void AddData(string seriesName, List<double> x, List<double> y)
        {
            if (x.Count != y.Count) return;
            for (int i = 0; i < x.Count; i++)
            {
                AddData(seriesName, x[i], y[i]);
            }
        }

        public void AddData(string seriesName, List<DateTime> x, List<double> y)
        {
            if (x.Count != y.Count) return;
            for (int i = 0; i < x.Count; i++)
            {
                AddData(seriesName, x[i], y[i]);
            }
        }

        public void AddData(string seriesName, double[] x, double[] y)
        {
            if (x.Length != y.Length) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddData(seriesName, x[i], y[i]);
            }
        }

        public void AddData(string seriesName, DateTime[] x, double[] y)
        {
            if (x.Length != y.Length) return;
            for (int i = 0; i < x.Length; i++)
            {
                AddData(seriesName, x[i], y[i]);
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

        public void Clear(string seriesName)
        {
            if (Series.ContainsKey(seriesName))
            {
                Series[seriesName].Clear();
            }
        }

        public int AllDataCount(bool isY2)
        {
            int cnt = 0;
            foreach (var series in Series.Values)
            {
                if (series.IsY2 != isY2) continue;
                cnt += series.DataCount;
            }

            return cnt;
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

        public bool HaveY2
        {
            get
            {
                foreach (var series in Series.Values)
                {
                    if (series.IsY2) return true;
                }

                return false;
            }
        }

        public void GetAllDataYRange(out double min, out double max)
        {
            if (AllDataCount(false) == 0)
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
                    if (series.IsY2) continue;
                    if (series.DataCount > 0)
                    {
                        if (series.ContainsNan)
                        {
                            foreach (var d in series.YData)
                            {
                                if (d.IsNan() || d.IsInfinity()) continue;
                                min = Math.Min(min, d);
                                max = Math.Max(max, d);
                            }
                        }
                        else
                        {
                            min = Math.Min(min, series.YData.Min());
                            max = Math.Max(max, series.YData.Max());
                        }
                    }
                }

                if (min > max)
                {
                    min = 0;
                    max = 1;
                }
            }
        }

        public void GetAllDataY2Range(out double min, out double max)
        {
            if (!HaveY2)
            {
                min = 0;
                max = 1;
            }
            else
            {
                if (AllDataCount(true) == 0)
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
                        if (!series.IsY2) continue;
                        if (series.DataCount > 0)
                        {
                            if (series.ContainsNan)
                            {
                                foreach (var d in series.YData)
                                {
                                    if (d.IsNan() || d.IsInfinity()) continue;
                                    min = Math.Min(min, d);
                                    max = Math.Max(max, d);
                                }
                            }
                            else
                            {
                                min = Math.Min(min, series.YData.Min());
                                max = Math.Max(max, series.YData.Max());
                            }
                        }
                    }

                    if (min > max)
                    {
                        min = 0;
                        max = 1;
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
                        if (series.ContainsNan)
                        {
                            foreach (var d in series.XData)
                            {
                                if (d.IsNan() || d.IsInfinity()) continue;
                                min = Math.Min(min, d);
                                max = Math.Max(max, d);
                            }
                        }
                        else
                        {
                            min = Math.Min(min, series.XData.Min());
                            max = Math.Max(max, series.XData.Max());
                        }
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
        public UILineSeries(string name, bool isY2 = false)
        {
            Name = name;
            Color = UIColor.Blue;
            IsY2 = isY2;
        }

        public UILineSeries(string name, Color color, bool isY2 = false)
        {
            Name = name;
            Color = color;
            CustomColor = true;
            IsY2 = isY2;
        }

        public void SetValueFormat(int xAxisDecimalPlaces, int yAxisDecimalPlaces)
        {
            XAxisDecimalPlaces = xAxisDecimalPlaces;
            YAxisDecimalPlaces = yAxisDecimalPlaces;
        }

        public void SetValueFormat(string xAxisDateTimeFormat, int yAxisDecimalPlaces)
        {
            XAxisDateTimeFormat = xAxisDateTimeFormat;
            YAxisDecimalPlaces = yAxisDecimalPlaces;
        }

        public void ClearValueFormat()
        {
            _dateTimeFormat = "";
            _xAxisDecimalPlaces = -1;
            _yAxisDecimalPlaces = -1;
        }

        private int _xAxisDecimalPlaces = -1;
        public int XAxisDecimalPlaces
        {
            get => _xAxisDecimalPlaces;
            set => _xAxisDecimalPlaces = Math.Max(0, value);
        }

        private int _yAxisDecimalPlaces = -1;
        public int YAxisDecimalPlaces
        {
            get => _yAxisDecimalPlaces;
            set => _yAxisDecimalPlaces = Math.Max(0, value);
        }

        private string _dateTimeFormat = "";

        public string XAxisDateTimeFormat
        {
            get => _dateTimeFormat;
            set
            {
                try
                {
                    DateTime.Now.ToString(value);
                    _dateTimeFormat = value;
                }
                catch
                {
                    _dateTimeFormat = "";
                }
            }
        }

        public int Index { get; set; }
        public string Name { get; private set; }

        public float Width { get; set; } = 2;
        public Color Color { get; set; }

        public UILinePointSymbol Symbol { get; set; } = UILinePointSymbol.None;

        /// <summary>
        /// 字体图标大小
        /// </summary>
        public int SymbolSize { get; set; } = 4;

        public int SymbolLineWidth { get; set; } = 1;

        /// <summary>
        /// 字体图标颜色
        /// </summary>
        public Color SymbolColor { get; set; } = Color.Empty;

        public bool CustomColor { get; set; }

        public bool Smooth { get; set; }

        public bool ShowLine { get; set; } = true;

        public bool ContainsNan { get; private set; }

        public bool IsY2 { get; private set; }

        public bool Visible { get; set; } = true;

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

        public bool GetNearestPointX(Point p, int offset, out double x, out double y, out int index)
        {
            x = 0;
            y = 0;
            index = -1;
            if (PointsX.Count == 0) return false;

            index = PointsX.BinarySearchNearIndex(p.X);
            if (p.X >= PointsX[index] - offset && p.X <= PointsX[index] + offset)
            {
                x = XData[index];
                y = YData[index];
                return true;
            }

            return false;
        }

        public void Clear()
        {
            ContainsNan = false;
            SymbolColor = Color.Empty;

            XData.Clear();
            YData.Clear();
            ClearPoints();
        }

        private void ClearPoints()
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

        public void CalcData(UILineChart chart, UIScale XScale, UIScale YScale)
        {
            ClearPoints();
            float[] x = XScale.CalcXPixels(XData.ToArray(), chart.DrawOrigin.X, chart.DrawSize.Width);
            float[] y = YScale.CalcYPixels(YData.ToArray(), chart.DrawOrigin.Y, chart.DrawSize.Height);
            AddPoints(x, y);
        }

        public void Add(double x, double y)
        {
            XData.Add(x);
            if (y.IsInfinity()) y = double.NaN;
            if (y.IsNan()) ContainsNan = true;
            YData.Add(y);
        }

        public void Add(DateTime x, double y)
        {
            DateTimeInt64 t = new DateTimeInt64(x);
            XData.Add(t);
            if (y.IsInfinity()) y = double.NaN;
            if (y.IsNan()) ContainsNan = true;
            YData.Add(y);
        }

        public void Add(string x, double y)
        {
            int cnt = XData.Count;
            XData.Add(cnt);
            if (y.IsInfinity()) y = double.NaN;
            if (y.IsNan()) ContainsNan = true;
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
        Star,
        Round
    }

    public struct UILineSelectPoint
    {
        public int Index { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public Point Location { get; set; }

        public UILineSeries Series { get; set; }
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
