using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public class UIBarOption : UIOption, IDisposable
    {
        public UICategoryAxis XAxis { get; set; } = new UICategoryAxis();

        public UIBarToolTip ToolTip { get; set; }

        public UIValueAxis YAxis { get; set; } = new UIValueAxis();

        public List<UIBarSeries> Series = new List<UIBarSeries>();

        public UIChartGrid Grid = new UIChartGrid();

        public readonly List<UIScaleLine> XAxisScaleLines = new List<UIScaleLine>();

        public readonly List<UIScaleLine> YAxisScaleLines = new List<UIScaleLine>();

        public void AddSeries(UIBarSeries series)
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

    public class UIBarToolTip
    {
        public string Formatter { get; set; } = "{{b}} : {{c}}";

        public string ValueFormat { get; set; } = "F0";

        public UIAxisPointer AxisPointer = new UIAxisPointer();
    }

    public class UIAxisPointer
    {
        public UIAxisPointerType Type { get; set; } = UIAxisPointerType.Line;
    }

    public enum UIAxisPointerType
    {
        Line, Shadow
    }

    public class UIAxis
    {
        public string Name { get; set; }

        public UIAxisType Type { get; set; }

        /// <summary>
        /// 坐标轴的分割段数，需要注意的是这个分割段数只是个预估值
        /// 最后实际显示的段数会在这个基础上根据分割后坐标轴刻度显示的易读程度作调整。
        /// 在类目轴中无效。
        /// </summary>
        public int SplitNumber { get; set; } = 5;

        /// <summary>
        /// 是否是脱离 0 值比例。设置成 true 后坐标刻度不会强制包含零刻度。在双数值轴的散点图中比较有用。
        /// </summary>
        public bool Scale { get; set; }

        public UIAxisTick AxisTick = new UIAxisTick();

        public UIAxisLabel AxisLabel = new UIAxisLabel();

        public bool MaxAuto { get; set; } = true;
        public bool MinAuto { get; set; } = true;

        public double Max { get; set; } = 100;
        public double Min { get; set; } = 0;

    }

    public class UICategoryAxis : UIAxis
    {
        public UICategoryAxis()
        {
            Type = UIAxisType.Category;
        }

        public List<string> Data = new List<string>();

        public void Clear()
        {
            Data.Clear();
        }
    }

    public class UIAxisLabel
    {
        /// <summary>
        /// 是否显示刻度标签。
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// 坐标轴刻度的显示间隔，在类目轴中有效。默认同 axisLabel.interval 一样。
        /// 默认会采用标签不重叠的策略间隔显示标签。
        /// 可以设置成 0 强制显示所有标签。
        /// 如果设置为 1，表示『隔一个标签显示一个标签』，如果值为 2，表示隔两个标签显示一个标签，以此类推。
        /// </summary>
        public int Interval { get; set; } = 0;

        public delegate string DoFormatter(double value, int index);

        public event DoFormatter Formatter;

        public string GetLabel(double value, int index)
        {
            return Formatter != null ? Formatter?.Invoke(value, index) : value.ToString("F" + DecimalCount);
        }

        /// <summary>
        /// 小数位个数，Formatter不为空时以Formatter为准
        /// </summary>
        public int DecimalCount { get; set; } = 0;
    }

    public class UIAxisTick
    {
        /// <summary>
        /// 类目轴中在为 true 的时候有效，可以保证刻度线和标签对齐。
        /// </summary>
        public bool AlignWithLabel { get; set; }

        /// <summary>
        /// 是否显示坐标轴刻度。
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// 坐标轴刻度的长度。
        /// </summary>
        public int Length { get; set; } = 5;

        /// <summary>
        /// 坐标轴刻度的显示间隔，在类目轴中有效。默认同 axisLabel.interval 一样。
        /// 默认会采用标签不重叠的策略间隔显示标签。
        /// 可以设置成 0 强制显示所有标签。
        /// 如果设置为 1，表示『隔一个标签显示一个标签』，如果值为 2，表示隔两个标签显示一个标签，以此类推。
        /// </summary>
        public int Interval { get; set; } = 0;
    }

    public class UIValueAxis : UIAxis
    {
        public UIValueAxis()
        {
            Type = UIAxisType.Value;
        }
    }

    public class UIBarSeries : IDisposable
    {
        public string Name { get; set; }

        public int MaxWidth { get; set; } = int.MaxValue;

        public UISeriesType Type => UISeriesType.Bar;

        public readonly List<double> Data = new List<double>();

        public readonly List<Color> Colors = new List<Color>();

        public void AddData(double value)
        {
            Data.Add(value);

            if (DataColorChange != null)
            {
                Colors.Add(DataColorChange.Invoke(value));
            }
        }

        public void AddData(double value, Color color)
        {
            Data.Add(value);
            Colors.Add(color);
        }

        public void Dispose()
        {
            Data.Clear();
            Colors.Clear();
        }

        public delegate Color OnDataColorChangeEventHandler(double data);

        public event OnDataColorChangeEventHandler DataColorChange;

        public bool HaveCustomColor(int index)
        {
            return Colors.Count > 0 && index >= 0 && index < Colors.Count;
        }

        public void Clear()
        {
            Data.Clear();
            Colors.Clear();
        }
    }
}