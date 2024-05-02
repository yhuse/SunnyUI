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
 * 文件名称: UIBarChart.cs
 * 文件说明: 柱状图
 * 当前版本: V3.1
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2020-08-21: V2.2.7 可设置柱状图最小宽度
 * 2021-07-22: V3.0.5 增加更新数据的方法
 * 2021-01-01: V3.0.9 增加柱子上显示数值
 * 2022-03-08: V3.1.1 增加X轴文字倾斜
 * 2022-05-27: V3.1.9 重写Y轴坐标显示
 * 2022-07-29: V3.2.2 数据显示的小数位数重构调整至数据序列 Series.DecimalPlaces
 * 2022-07-30: V3.2.2 坐标轴的小数位数重构调整至坐标轴标签 AxisLabel.DecimalPlaces
 * 2022-08-10: V3.2.2 修复Y轴显示名称
 * 2022-08-17: V3.2.3 增加数据可为Nan
 * 2022-09-07: V3.2.3 Option.YAxis.ShowGridLine为false时，不显示水平表格虚线
 * 2023-05-10: V3.3.6 Option.ShowFullRect为true时，绘制右侧和上侧的边框实线
 * 2023-05-13: V3.3.6 Option.BarInterval,设置Bar之间间隔，默认-1，自动计算间隔
 * 2023-05-14: V3.3.6 重构DrawString函数
 * 2023-06-06: V3.3.7 修复Y轴文字居中
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 柱状图
    /// </summary>
    [ToolboxItem(true)]
    public class UIBarChart : UIChart
    {
        private bool NeedDraw;

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData();
        }

        /// <summary>
        /// 刷新显示
        /// </summary>
        public override void Refresh()
        {
            if (Option != null) SetOption(Option);
            CalcData();

            if (InvokeRequired)
            {
                Invoke(new Action(base.Refresh));
            }
            else
            {
                base.Refresh();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="seriesName">序列名称</param>
        /// <param name="index">序号</param>
        /// <param name="value">值</param>
        public void Update(string seriesName, int index, double value)
        {
            var series = Option[seriesName];
            if (series != null)
            {
                series.Update(index, value);
            }
        }

        UILinearScale YScale = new UILinearScale();

        /// <summary>
        /// 计算数据用于显示
        /// </summary>
        protected override void CalcData()
        {
            Bars.Clear();
            NeedDraw = false;
            if (Option == null || Option.Series == null || Option.SeriesCount == 0) return;
            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            if (Option.XAxis.Data.Count == 0) return;

            NeedDraw = true;
            DrawBarWidth = DrawSize.Width * 1.0f / Option.XAxis.Data.Count;

            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var series in Option.Series)
            {
                if (series.Data.Count > 0)
                {
                    for (int i = 0; i < series.Data.Count; i++)
                    {
                        if (series.Data[i].IsNanOrInfinity()) continue;
                        min = Math.Min(min, series.Data[i]);
                        max = Math.Max(max, series.Data[i]);
                    }
                }
            }

            if (min > max)
            {
                min = 0;
                max = 1;
            }

            if (min > 0 && max > 0 && !Option.YAxis.Scale) min = 0;
            if (min < 0 && max < 0 && !Option.YAxis.Scale) max = 0;
            if (!Option.YAxis.MaxAuto) max = Option.YAxis.Max;
            if (!Option.YAxis.MinAuto) min = Option.YAxis.Min;

            if (Option.YAxis.MaxAuto && Option.YAxis.MinAuto)
            {
                if (min > 0) min = 0;
                if (max < 0) max = 0;

                if (min.IsZero() && !max.IsZero())
                {
                    max = max * 1.2;
                }

                if (max.IsZero() && !min.IsZero())
                {
                    min = min * 1.2;
                }

                if (!max.IsZero() && !min.IsZero())
                {
                    max = max * 1.2;
                    min = min * 1.2;
                }
            }

            if ((max - min).IsZero())
            {
                if (min.IsZero())
                {
                    max = 100;
                    min = 0;
                }
                else if (max > 0)
                {
                    max = max * 2;
                    min = 0;
                }
                else
                {
                    max = 0;
                    min = min * 2;
                }
            }

            YScale.Max = max;
            YScale.Min = min;
            YScale.AxisChange();

            YAxisStart = YScale.Min;
            YAxisEnd = YScale.Max;
            YAxisInterval = YScale.Step;
            double[] YLabels = YScale.CalcLabels();
            float[] labels = YScale.CalcYPixels(YLabels, DrawOrigin.Y, DrawSize.Height);
            YAxisDecimalCount = YScale.Format.Replace("F", "").ToInt();

            float x1 = DrawBarWidth / (Option.SeriesCount * 2 + Option.SeriesCount + 1);
            float x2 = x1 * 2;

            if (Option.BarInterval < 0 || Option.BarInterval > x1 || Option.SeriesCount == 1)
            {
                for (int i = 0; i < Option.SeriesCount; i++)
                {
                    float barX = DrawOrigin.X;
                    var series = Option.Series[i];
                    Bars.TryAdd(i, new List<BarInfo>());
                    for (int j = 0; j < series.Data.Count; j++)
                    {
                        Color color = ChartStyle.GetColor(i);
                        if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                            color = series.Colors[j];

                        float xx = barX + x1 * (i + 1) + x2 * i + x1;
                        float ww = Math.Min(x2, series.MaxWidth);
                        xx -= ww / 2.0f;

                        float YZeroPos = YScale.CalcYPixel(0, DrawOrigin.Y, DrawSize.Height);
                        float VPos = YScale.CalcYPixel(series.Data[j], DrawOrigin.Y, DrawSize.Height);

                        if (VPos <= YZeroPos)
                        {
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, VPos, ww, (YZeroPos - VPos)),
                                Value = series.Data[j],
                                Color = color,
                                Top = true,
                                Series = series,
                            });
                        }
                        else
                        {
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, YZeroPos, ww, (VPos - YZeroPos)),
                                Value = series.Data[j],
                                Color = color,
                                Top = false,
                                Series = series,
                            });
                        }

                        barX += DrawBarWidth;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Option.SeriesCount; i++)
                {
                    float barX = DrawOrigin.X;
                    var series = Option.Series[i];
                    Bars.TryAdd(i, new List<BarInfo>());
                    for (int j = 0; j < series.Data.Count; j++)
                    {
                        Color color = ChartStyle.GetColor(i);
                        if (series.Colors.Count > 0 && j >= 0 && j < series.Colors.Count)
                            color = series.Colors[j];

                        float ww = Math.Min(x2, series.MaxWidth);
                        float xl = (DrawBarWidth - Option.SeriesCount * ww - (Option.SeriesCount - 1) * Option.BarInterval) / 2.0f;
                        float xx = barX + xl + i * ww + i * Option.BarInterval;
                        float YZeroPos = YScale.CalcYPixel(0, DrawOrigin.Y, DrawSize.Height);
                        float VPos = YScale.CalcYPixel(series.Data[j], DrawOrigin.Y, DrawSize.Height);

                        if (VPos <= YZeroPos)
                        {
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, VPos, ww, (YZeroPos - VPos)),
                                Value = series.Data[j],
                                Color = color,
                                Top = true,
                                Series = series,
                            });
                        }
                        else
                        {
                            Bars[i].Add(new BarInfo()
                            {
                                Rect = new RectangleF(xx, YZeroPos, ww, (VPos - YZeroPos)),
                                Value = series.Data[j],
                                Color = color,
                                Top = false,
                                Series = series,
                            });
                        }

                        barX += DrawBarWidth;
                    }
                }
            }

            if (Option.ToolTip != null)
            {
                for (int i = 0; i < Option.XAxis.Data.Count; i++)
                {
                    string str = Option.XAxis.Data[i];
                    foreach (var series in Option.Series)
                    {
                        str += '\n';
                        str += series.Name + " : " + series.Data[i].ToString("F" + series.DecimalPlaces);
                    }

                    Bars[0][i].Tips = str;
                }
            }
        }

        private Point DrawOrigin => new Point(Option.Grid.Left, Height - Option.Grid.Bottom);
        private Size DrawSize => new Size(Width - Option.Grid.Left - Option.Grid.Right, Height - Option.Grid.Top - Option.Grid.Bottom);
        private Rectangle DrawRect => new Rectangle(Option.Grid.Left, Option.Grid.Top, DrawSize.Width, DrawSize.Height);

        private int selectIndex = -1;
        private float DrawBarWidth;
        private double YAxisStart;
        private double YAxisEnd;
        private double YAxisInterval;
        private int YAxisDecimalCount;
        private readonly ConcurrentDictionary<int, List<BarInfo>> Bars = new ConcurrentDictionary<int, List<BarInfo>>();

        [DefaultValue(-1), Browsable(false)]
        private int SelectIndex
        {
            get => selectIndex;
            set
            {
                if (Option.ToolTip != null && selectIndex != value)
                {
                    selectIndex = value;
                    Invalidate();
                }

                if (selectIndex < 0) tip.Visible = false;
            }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            try
            {
                if (!Option.ToolTip.Visible) return;
                if (e.Location.X > Option.Grid.Left && e.Location.X < Width - Option.Grid.Right
                                                       && e.Location.Y > Option.Grid.Top &&
                                                       e.Location.Y < Height - Option.Grid.Bottom)
                {
                    SelectIndex = (int)((e.Location.X - Option.Grid.Left) / DrawBarWidth);
                }
                else
                {
                    SelectIndex = -1;
                }

                if (SelectIndex >= 0 && Bars.Count > 0)
                {
                    if (tip.Text != Bars[0][selectIndex].Tips)
                    {
                        tip.Text = Bars[0][selectIndex].Tips;
                        tip.Size = new Size(Bars[0][selectIndex].Size.Width + 4, Bars[0][selectIndex].Size.Height + 4);
                    }

                    int x = e.Location.X + 15;
                    int y = e.Location.Y + 20;
                    if (e.Location.X + 15 + tip.Width > Width - Option.Grid.Right)
                        x = e.Location.X - tip.Width - 2;
                    if (e.Location.Y + 20 + tip.Height > Height - Option.Grid.Bottom)
                        y = e.Location.Y - tip.Height - 2;

                    tip.Left = x;
                    tip.Top = y;
                    if (!tip.Visible) tip.Visible = Bars[0][selectIndex].Tips.IsValid();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// 图表参数
        /// </summary>
        [Browsable(false), DefaultValue(null)]
        public UIBarOption Option
        {
            get
            {
                UIOption option = BaseOption ?? EmptyOption;
                return (UIBarOption)option;
            }
        }

        /// <summary>
        /// 默认创建空的图表参数
        /// </summary>
        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UIBarOption option = new UIBarOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "BarChart";

            //设置Legend
            option.Legend = new UILegend();
            option.Legend.Orient = UIOrient.Horizontal;
            option.Legend.Top = UITopAlignment.Top;
            option.Legend.Left = UILeftAlignment.Left;
            option.Legend.AddData("Bar1");
            option.Legend.AddData("Bar2");

            var series = new UIBarSeries();
            series.Name = "Bar1";
            series.AddData(1);
            series.AddData(5);
            series.AddData(2);
            series.AddData(4);
            series.AddData(3);
            option.Series.Add(series);

            series = new UIBarSeries();
            series.Name = "Bar2";
            series.AddData(2);
            series.AddData(1);
            series.AddData(5);
            series.AddData(3);
            series.AddData(4);
            option.Series.Add(series);

            option.XAxis.Data.Add("Mon");
            option.XAxis.Data.Add("Tue");
            option.XAxis.Data.Add("Wed");
            option.XAxis.Data.Add("Thu");
            option.XAxis.Data.Add("Fri");

            option.ToolTip = new UIBarToolTip();
            option.ToolTip.AxisPointer.Type = UIAxisPointerType.Shadow;

            emptyOption = option;
        }

        /// <summary>
        /// 绘制图表参数
        /// </summary>
        /// <param name="g">绘制图面</param>
        protected override void DrawOption(Graphics g)
        {
            if (Option == null) return;
            if (!NeedDraw) return;

            if (Option.ToolTip != null && Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow) DrawToolTip(g);

            DrawSeries(g, Option.Series);
            DrawAxis(g);
            DrawTitle(g, Option.Title);
            if (Option.ToolTip != null && Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Line) DrawToolTip(g);

            DrawLegend(g, Option.Legend);
            DrawAxisScales(g);
        }

        /// <summary>
        /// 绘制工具提示
        /// </summary>
        /// <param name="g">绘制图面</param>
        protected virtual void DrawToolTip(Graphics g)
        {
            if (selectIndex < 0) return;
            if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Line)
            {
                float x = DrawOrigin.X + SelectIndex * DrawBarWidth + DrawBarWidth / 2.0f;
                g.DrawLine(ChartStyle.ToolTipShadowColor, x, DrawOrigin.Y, x, Option.Grid.Top);
            }

            if (Option.ToolTip.AxisPointer.Type == UIAxisPointerType.Shadow)
            {
                float x = DrawOrigin.X + SelectIndex * DrawBarWidth;
                g.FillRectangle(ChartStyle.ToolTipShadowColor, x, Option.Grid.Top, DrawBarWidth, Height - Option.Grid.Top - Option.Grid.Bottom);
            }
        }

        /// <summary>
        /// 绘制坐标轴
        /// </summary>
        /// <param name="g">绘制图面</param>
        protected virtual void DrawAxis(Graphics g)
        {
            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            g.FillRectangle(FillColor, Option.Grid.Left, 1, Width - Option.Grid.Left - Option.Grid.Right, Option.Grid.Top);
            g.FillRectangle(FillColor, Option.Grid.Left, Height - Option.Grid.Bottom, Width - Option.Grid.Left - Option.Grid.Right, Option.Grid.Bottom - 1);

            if (YAxisStart >= 0) g.DrawLine(ForeColor, DrawOrigin, new Point(DrawOrigin.X + DrawSize.Width, DrawOrigin.Y));
            if (YAxisEnd <= 0) g.DrawLine(ForeColor, new Point(DrawOrigin.X, Option.Grid.Top), new Point(DrawOrigin.X + DrawSize.Width, Option.Grid.Top));

            g.DrawLine(ForeColor, DrawOrigin, new Point(DrawOrigin.X, DrawOrigin.Y - DrawSize.Height));
            g.DrawLine(ForeColor, DrawOrigin, new Point(Width - Option.Grid.Right, DrawOrigin.Y));

            if (Option.XAxis.AxisTick.Show)
            {
                float start = DrawOrigin.X + DrawBarWidth / 2.0f;
                for (int i = 0; i < Option.XAxis.Data.Count; i++)
                {
                    g.DrawLine(ForeColor, start, DrawOrigin.Y, start, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                    start += DrawBarWidth;
                }
            }

            if (Option.XAxis.AxisLabel.Show)
            {
                float start = DrawOrigin.X;
                foreach (var data in Option.XAxis.Data)
                {
                    int angle = (Option.XAxis.AxisLabel.Angle + 36000) % 360;
                    if (angle > 0 && angle <= 90)
                        g.DrawRotateString(data, TempFont, ForeColor, new PointF(start, DrawOrigin.Y + Option.XAxis.AxisTick.Length),
                            new StringFormat() { Alignment = StringAlignment.Far }, (3600 - Option.XAxis.AxisLabel.Angle) % 360);
                    else
                        g.DrawString(data, TempFont, ForeColor, new Rectangle((int)start, DrawOrigin.Y + Option.XAxis.AxisTick.Length, (int)DrawBarWidth, Height), ContentAlignment.TopCenter);

                    start += DrawBarWidth;
                }

                if (Option.XAxis.Name.IsValid())
                {
                    g.DrawString(Option.XAxis.Name, TempFont, ForeColor, new Rectangle(DrawOrigin.X, 0, DrawSize.Width, Height - 16), ContentAlignment.BottomCenter);
                }
            }

            if (Option.ShowFullRect)
            {
                g.DrawRectangle(ForeColor, Option.Grid.Left, Option.Grid.Top, DrawSize.Width, DrawSize.Height);
            }

            double[] YLabels = YScale.CalcLabels();
            float[] labels = YScale.CalcYPixels(YLabels, DrawOrigin.Y, DrawSize.Height);
            float wmax = 0;
            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] > DrawOrigin.Y) continue;
                if (labels[i] < Option.Grid.Top) continue;
                if (Option.YAxis.AxisTick.Show)
                {
                    g.DrawLine(ForeColor, DrawOrigin.X, labels[i], DrawOrigin.X - Option.YAxis.AxisTick.Length, labels[i]);

                    if (YLabels[i].IsNanOrInfinity()) continue;
                    if (!Option.YAxis.ShowGridLine) continue;
                    if (!YLabels[i].EqualsDouble(0))
                    {
                        using Pen pn = new Pen(ForeColor);
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, DrawOrigin.X, labels[i], Width - Option.Grid.Right, labels[i]);
                    }
                    else
                    {
                        g.DrawLine(ForeColor, DrawOrigin.X, labels[i], Width - Option.Grid.Right, labels[i]);
                    }
                }

                if (Option.YAxis.AxisLabel.Show)
                {
                    string label = YLabels[i].ToString(Option.YAxis.AxisLabel.DecimalPlaces >= 0 ? "F" + Option.YAxis.AxisLabel.DecimalPlaces : YScale.Format);
                    Size sf = TextRenderer.MeasureText(label, TempFont);
                    wmax = Math.Max(wmax, sf.Width);
                    g.DrawString(label, TempFont, ForeColor, new Rectangle(DrawOrigin.X - Option.YAxis.AxisTick.Length - Width, (int)labels[i] - Height, Width, Height * 2), ContentAlignment.MiddleRight);
                }
            }

            if (Option.YAxis.AxisLabel.Show && Option.YAxis.Name.IsValid())
            {
                Size sfName = TextRenderer.MeasureText(Option.YAxis.Name, TempFont);
                float xx = DrawOrigin.X - Option.YAxis.AxisTick.Length - wmax - sfName.Height / 2.0f;
                float yy = Option.Grid.Top + DrawSize.Height / 2.0f;
                g.DrawRotateString(Option.YAxis.Name, TempFont, ForeColor, new PointF(xx, yy), 270);
            }
        }

        private void DrawAxisScales(Graphics g)
        {
            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            foreach (var line in Option.YAxisScaleLines)
            {
                double ymin = YAxisStart * YAxisInterval;
                double ymax = YAxisEnd * YAxisInterval;
                float pos = YScale.CalcYPixel(line.Value, DrawOrigin.Y, DrawSize.Height);
                if (pos <= Option.Grid.Top || pos >= Height - Option.Grid.Bottom) continue;
                using Pen pn = new Pen(line.Color, line.Size);
                g.DrawLine(pn, DrawOrigin.X, pos, Width - Option.Grid.Right, pos);
                g.DrawString(line.Name, TempFont, line.Color, new Rectangle(DrawOrigin.X + 4, (int)pos - 2 - Height, DrawSize.Width - 8, Height), (StringAlignment)((int)line.Left), StringAlignment.Far);
            }
        }

        /// <summary>
        /// 绘制序列
        /// </summary>
        /// <param name="g">绘制图面</param>
        /// <param name="series">序列</param>
        protected virtual void DrawSeries(Graphics g, List<UIBarSeries> series)
        {
            if (series == null || series.Count == 0) return;
            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            for (int i = 0; i < Bars.Count; i++)
            {
                var bars = Bars[i];
                foreach (var info in bars)
                {
                    g.FillRectangle(info.Color, info.Rect);

                    if (Option.ShowValue)
                    {
                        string value = info.Value.ToString("F" + info.Series.DecimalPlaces);
                        if (info.Top)
                        {
                            g.DrawString(value, TempFont, info.Color, new Rectangle((int)info.Rect.Center().X - Width, (int)info.Rect.Top - Height, Width * 2, Height), ContentAlignment.BottomCenter);
                        }
                        else
                        {
                            g.DrawString(value, TempFont, info.Color, new Rectangle((int)info.Rect.Center().X - Width, (int)info.Rect.Bottom, Width * 2, Height), ContentAlignment.TopCenter);
                        }
                    }
                }
            }

            for (int i = 0; i < Option.XAxis.Data.Count; i++)
            {
                Bars[0][i].Size = TextRenderer.MeasureText(Bars[0][i].Tips, TempFont);
            }
        }


        private class BarInfo
        {
            public RectangleF Rect { get; set; }

            public string Tips { get; set; }

            public Size Size { get; set; }

            public Color Color { get; set; }

            public double Value { get; set; }

            public bool Top { get; set; }

            public UIBarSeries Series { get; set; }
        }
    }
}