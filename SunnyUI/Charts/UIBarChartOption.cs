/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 文件名称: UIBarChartOption.cs
 * 文件说明: 柱状图配置类
 * 当前版本: V3.1
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2022-11-25: V3.2.2 重构对象
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    /// <summary>
    /// 柱状图配置类
    /// </summary>
    public sealed class UIBarOption : UIOption, IDisposable
    {
        /// <summary>
        /// X轴
        /// </summary>
        public UIAxis XAxis { get; private set; } = new UIAxis(UIAxisType.Category);

        /// <summary>
        /// 工具提示
        /// </summary>
        public UIBarToolTip ToolTip { get; set; } = new UIBarToolTip();

        /// <summary>
        /// Y轴
        /// </summary>
        public UIAxis YAxis { get; private set; } = new UIAxis(UIAxisType.Value);

        /// <summary>
        /// 序列
        /// </summary>
        public readonly List<UIBarSeries> Series = new List<UIBarSeries>();

        /// <summary>
        /// 绘图表格
        /// </summary>
        public readonly UIChartGrid Grid = new UIChartGrid();

        /// <summary>
        /// X轴自定义刻度线
        /// </summary>
        public readonly List<UIScaleLine> XAxisScaleLines = new List<UIScaleLine>();

        /// <summary>
        /// Y轴自定义刻度线
        /// </summary>
        public readonly List<UIScaleLine> YAxisScaleLines = new List<UIScaleLine>();

        /// <summary>
        /// 显示值
        /// </summary>
        public bool ShowValue { get; set; }

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

        public int BarInterval { get; set; } = -1;

        public void AddSeries(UIBarSeries series)
        {
            if (series == null)
            {
                throw new NullReferenceException("series 不能为空");
            }

            if (series.Name.IsNullOrEmpty())
            {
                throw new NullReferenceException("series.Name 不能为空");
            }

            Series.Add(series);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            foreach (var series in Series)
            {
                series?.Dispose();
            }

            Series.Clear();
        }

        public int SeriesCount => Series.Count;

        public UIBarSeries this[string seriesName]
        {
            get
            {
                foreach (var item in Series)
                {
                    if (item.Name == seriesName) return item;
                }

                return null;
            }
        }

        public bool ShowFullRect { get; set; }
    }

    public class UIBarToolTip : UIChartToolTip
    {
        public UIAxisPointer AxisPointer = new UIAxisPointer();

        public UIBarToolTip()
        {
            Formatter = "{{b}} : {{c}}";
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

    public class UIBarSeries : IDisposable
    {
        public UIBarSeries(int decimalPlaces = 0)
        {
            DecimalPlaces = decimalPlaces;
        }

        public string Name { get; set; }

        public int MaxWidth { get; set; } = int.MaxValue;

        public UISeriesType Type => UISeriesType.Bar;

        internal readonly List<string> BarName = new List<string>();

        internal readonly List<double> Data = new List<double>();

        internal readonly List<Color> Colors = new List<Color>();

        private int _decimalPlaces = 0;
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set => _decimalPlaces = Math.Max(0, value);
        }

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
            if (name.IsNullOrEmpty())
            {
                throw new NullReferenceException("name 不能为空");
            }

            BarName.Add(name);
            AddData(value);
        }

        public void AddData(string name, double value, Color color)
        {
            if (name.IsNullOrEmpty())
            {
                throw new NullReferenceException("name 不能为空");
            }

            BarName.Add(name);
            AddData(value, color);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            Clear();
        }

        public void Update(int index, double value)
        {
            if (Data.Count == 0) return;
            if (index >= 0 && index < Data.Count)
            {
                Data[index] = value;
            }
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