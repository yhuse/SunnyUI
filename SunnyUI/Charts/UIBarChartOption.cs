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
 * 文件名称: UIBarChartOption.cs
 * 文件说明: 柱状图配置类
 * 当前版本: V2.2
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UIBarOption : UIOption, IDisposable
    {
        public UIAxis XAxis { get; set; } = new UIAxis(UIAxisType.Category);

        public UIBarToolTip ToolTip { get; set; } = new UIBarToolTip();

        public UIAxis YAxis { get; set; } = new UIAxis(UIAxisType.Value);

        public List<UIBarSeries> Series = new List<UIBarSeries>();

        public UIChartGrid Grid = new UIChartGrid();

        public readonly List<UIScaleLine> XAxisScaleLines = new List<UIScaleLine>();

        public readonly List<UIScaleLine> YAxisScaleLines = new List<UIScaleLine>();

        /// <summary>
        /// BarChartEx用，固定每个序列Bar个数
        /// </summary>
        public int FixedSeriesCount { get; set; } = 0;

        /// <summary>
        /// BarChartEx用，自动调整为显示每个Bar等宽
        /// </summary>
        public bool AutoSizeBars { get; set; }

        public bool AutoSizeBarsCompact { get; set; }

        public float AutoSizeBarsCompactValue { get; set; } = 1.0f;

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

    public class UIBarToolTip : UIChartToolTip
    {
        public UIAxisPointer AxisPointer = new UIAxisPointer();

        public UIBarToolTip()
        {
            Formatter = "{{b}} : {{c}}";
            ValueFormat = "F0";
        }
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
        public UIAxis()
        {

        }

        public UIAxis(UIAxisType axisType)
        {
            Type = axisType;
        }

        public string Name { get; set; }

        public UIAxisType Type { get; }

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

        /// <summary>
        /// 坐标轴刻度
        /// </summary>
        public UIAxisTick AxisTick = new UIAxisTick();

        /// <summary>
        /// 坐标轴标签
        /// </summary>
        public UIAxisLabel AxisLabel = new UIAxisLabel();

        public bool MaxAuto { get; set; } = true;
        public bool MinAuto { get; set; } = true;

        public double Max { get; set; } = 100;
        public double Min { get; set; } = 0;

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

        public int Angle { get; set; } = 0;

        public string GetLabel(double value, int index, UIAxisType axisType = UIAxisType.Value)
        {
            switch (axisType)
            {
                case UIAxisType.Value:
                    return Formatter != null ? Formatter?.Invoke(value, index) : value.ToString("F" + DecimalCount);
                case UIAxisType.DateTime:
                    DateTimeInt64 dt = new DateTimeInt64((long)value);
                    return Formatter != null ? Formatter?.Invoke(dt, index) : (DateTimeFormat.IsNullOrEmpty() ? dt.ToString() : dt.ToString(DateTimeFormat));
                case UIAxisType.Category:
                    return Formatter != null ? Formatter?.Invoke(value, index) : value.ToString("F0");
            }

            return value.ToString("F2");
        }

        public string GetAutoLabel(double value, int decimalCount)
        {
            return value.ToString("F" + decimalCount);
        }

        /// <summary>
        /// 小数位个数，Formatter不为空时以Formatter为准
        /// </summary>
        public int DecimalCount { get; set; } = 0;

        /// <summary>
        /// 日期格式化字符串，Formatter不为空时以Formatter为准
        /// </summary>
        public string DateTimeFormat { get; set; } = "HH:mm";

        public bool AutoFormat { get; set; } = true;
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

        public int Distance { get; set; } = 0;
    }

    public class UIBarSeries : IDisposable
    {
        public string Name { get; set; }

        public int MaxWidth { get; set; } = int.MaxValue;

        public UISeriesType Type => UISeriesType.Bar;

        public readonly List<string> BarName = new List<string>();

        public readonly List<double> Data = new List<double>();

        public readonly List<Color> Colors = new List<Color>();

        public bool ShowBarName { get; set; }

        public bool ShowValue { get; set; }

        public float ShowValueFontSize { get; set; } = 0;

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

        public void AddData(string name, double value)
        {
            BarName.Add(name);
            AddData(value);
        }

        public void AddData(string name, double value, Color color)
        {
            BarName.Add(name);
            AddData(value, color);
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
            BarName.Clear();
            Data.Clear();
            Colors.Clear();
        }
    }
}