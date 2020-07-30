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
 * 文件名称: UIOption.cs
 * 文件说明: 图表设置类
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
    public abstract class UIOption
    {
        public UITitle Title;
        public UILegend Legend;
    }

    public class UIScaleLine
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Left;

        public float Size { get; set; } = 1.0f;
    }

    public class UILegend
    {
        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;

        public UIOrient Orient { get; set; } = UIOrient.Vertical;

        public readonly List<string> Data = new List<string>();

        public readonly List<Color> Colors = new List<Color>();

        public int DataCount => Data.Count;

        public void AddData(string data)
        {
            Data.Add(data);
        }

        public void AddData(string data, Color color)
        {
            Data.Add(data);
            Colors.Add(color);
        }

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

    public class UIChartGrid
    {
        public int Left { get; set; } = 60;
        public int Right { get; set; } = 60;
        public int Top { get; set; } = 60;
        public int Bottom { get; set; } = 60;
    }

    public enum UIOrient
    {
        Vertical,
        Horizontal
    }

    public enum UIAxisType
    {
        Value,
        Category,
        Time,
        Log
    }

    public class UITitle
    {
        public string Text { get; set; } = "UIPieChart";

        public string SubText { get; set; } = "";

        public UILeftAlignment Left { get; set; } = UILeftAlignment.Center;

        public UITopAlignment Top { get; set; } = UITopAlignment.Top;
    }

    public enum UILeftAlignment
    {
        Left,
        Center,
        Right
    }

    public enum UITopAlignment
    {
        Top,
        Center,
        Bottom
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
        Dark,
        LiveChart
    }

    public static class UIChartStyles
    {
        public static UIChartStyle Default = new UIDefaultChartStyle();

        public static UIChartStyle Plain = new UILightChartStyle();

        public static UIChartStyle Dark = new UIDarkChartStyle();

        public static UIChartStyle LiveChart = new UILiveChartStyle();

        public static UIChartStyle GetChartStyle(UIChartStyleType style)
        {
            if (style == UIChartStyleType.Default) return Default;
            if (style == UIChartStyleType.Dark) return Dark;
            if (style == UIChartStyleType.LiveChart) return LiveChart;
            return Plain;
        }
    }

    public static class UIChartHelper
    {
        /// <summary>
        /// 计算刻度
        /// 起始值必须小于结束值
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <param name="expect_num">期望刻度数量，实际数接近此数</param>
        /// <param name="degree_start">刻度起始值，须乘以间隔使用</param>
        /// <param name="degree_end">刻度结束值，须乘以间隔使用</param>
        /// <param name="degree_gap">刻度间隔</param>
        public static void CalcDegreeScale(double start, double end, int expect_num,
            out int degree_start, out int degree_end, out double degree_gap)
        {
            if (start >= end)
            {
                throw new Exception("起始值必须小于结束值");
            }

            double differ = end - start;
            double differ_gap = differ / (expect_num - 1); //35, 4.6, 0.27

            double exponent = Math.Log10(differ_gap) - 1; //0.54, -0.34, -1.57
            int _exponent = (int)exponent; //0, 0=>-1, -1=>-2
            if (exponent < 0 && Math.Abs(exponent) > 1e-8)
            {
                _exponent--;
            }

            int step = (int)(differ_gap / Math.Pow(10, _exponent)); //35, 46, 27
            int[] fix_steps = new int[] { 10, 20, 25, 50, 100 };
            int fix_step = 10; //25, 50, 25
            for (int i = fix_steps.Length - 1; i >= 1; i--)
            {
                if (step > (fix_steps[i] + fix_steps[i - 1]) / 2)
                {
                    fix_step = fix_steps[i];
                    break;
                }
            }

            degree_gap = fix_step * Math.Pow(10, _exponent); //25, 5, 0.25

            double start1 = start / degree_gap;
            int start2 = (int)start1;
            if (start1 < 0 && Math.Abs(start1 - start2) > 1e-8)
            {
                start2--;
            }

            degree_start = start2;

            double end1 = end / degree_gap;
            int end2 = (int)end1;
            if (end1 >= 0 && Math.Abs(end1 - end2) > 1e-8)
            {
                end2++;
            }

            degree_end = end2;
        }

        /// <summary>
        /// 计算刻度
        /// 起始值必须小于结束值
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <param name="expect_num">期望刻度数量，实际数接近此数</param>
        /// <returns>刻度列表</returns>
        public static double[] CalcDegreeScale(double start, double end, int expect_num)
        {
            if (start >= end)
            {
                throw new Exception("起始值必须小于结束值");
            }

            CalcDegreeScale(start, end, expect_num, out int degree_start, out int degree_end,
                out double degree_gap);

            double[] list = new double[degree_end - degree_start + 1];
            for (int i = degree_start; i <= degree_end; i++)
            {
                list[i - degree_start] = i * degree_gap;
            }

            return list;
        }
    }
}