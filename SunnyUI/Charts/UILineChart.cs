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
 * 文件名称: UILineChart.cs
 * 文件说明: 曲线图
 * 当前版本: V3.1
 * 创建日期: 2020-10-01
 *
 * 2020-10-01: V2.2.8 完成曲线图表
 * 2021-04-06: V3.0.2 增加鼠标框选放大，可多次放大，右键点击恢复一次，双击恢复默认
 * 2021-04-15: V3.0.3 有右键菜单时，取消恢复上次缩放，可在右键菜单增加节点，调用ZoomBack()方法
 * 2021-06-18: V3.0.4 显示鼠标点格式更新
 * 2021-07-22: V3.0.5 可自定义背景色，增加实时数据的Demo
 * 2021-08-23: V3.0.6 增加可只显示点的模式
 * 2021-10-02: V3.0.8 支持数据包括Nan，修改自定义最大值最小值为无穷时出错的问题
 * 2021-10-14: V3.0.8 修改图线显示超出范围的问题
 * 2021-12-30: V3.0.9 增加双Y坐标轴
 * 2021-12-31: V3.0.9 增加坐标线、图线边框等是否显示的设置
 * 2021-12-31: V3.0.9 增加自定义坐标轴刻度
 * 2021-12-31: V3.0.9 X轴支持字符串显示
 * 2022-01-06: V3.1.0 支持FillColor透明
 * 2022-01-09: V3.1.0 双坐标轴支持选区域缩放
 * 2022-02-09: V3.1.0 增加图线隐藏
 * 2022-04-19: V3.1.5 关闭Smooth绘制，数值差距大或者持续缩放会出错
 * 2022-07-11: V3.2.1 修改两个点时可以不显示连接线
 * 2022-07-26: V3.2.2 修复双Y轴数据点提示文字显示
 * 2022-07-30: V3.2.2 数据显示的小数位数重构调整至数据序列 Series.XAxisDecimalPlaces，YAxisDecimalPlaces
 * 2022-07-30: V3.2.2 数据显示的日期格式重构调整至数据序列 Series.XAxisDateTimeFormat
 * 2022-07-30: V3.2.2 坐标轴的小数位数重构调整至坐标轴标签 AxisLabel.DecimalPlaces
 * 2022-07-30: V3.2.2 坐标轴的日期格式重构调整至坐标轴标签 AxisLabel.DateTimeFormat
 * 2022-08-17: V3.2.3 修复数据全为Nan时绘制出错
 * 2022-09-19: V3.2.4 增加鼠标可框选缩放属性MouseZoom
 * 2023-03-26: V3.3.3 自定义X轴坐标时，点数据提示显示为原始值
 * 2023-04-23: V3.3.5 打开Smooth绘制，建议数据差距不大时可平滑绘制
 * 2023-05-12: V3.3.6 增加了一种开关量曲线的显示方式
 * 2023-05-14: V3.3.6 重构DrawString函数
 * 2023-06-06: V3.3.7 修复X轴文字重叠问题
 * 2023-07-02: V3.3.9 增加PointFormat，鼠标选中值显示格式化事件
 * 2023-07-02: V3.3.9 增加了数据沿Y轴变化时鼠标移动到数据点时显示数据点标签
 * 2023-07-14: V3.4.0 增加了坐标轴绘制时显示箭头，并在箭头处显示数量单位的功能
 * 2023-10-04: V3.5.0 增加了Y轴数据由上向下绘制
 * 2023-10-05: V3.5.0 增加了X轴和Y轴鼠标选择区域并返回选中范围
 * 2023-10-20: V3.5.1 增加了绘制线的DashStyle样式
 * 2023-11-22: V3.6.0 增加了区域选择范围相等时不执行事件
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UILineChart : UIChart
    {
        protected bool NeedDraw;

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData();
        }

        [Browsable(false)]
        public Point DrawOrigin => new Point(Option.Grid.Left, Option.YDataOrder == UIYDataOrder.Asc ? Height - Option.Grid.Bottom : Option.Grid.Top);

        [Browsable(false)]
        public Size DrawSize => new Size(Width - Option.Grid.Left - Option.Grid.Right, Height - Option.Grid.Top - Option.Grid.Bottom);

        [Browsable(false)]
        public Rectangle DrawRect => new Rectangle(Option.Grid.Left, Option.Grid.Top, DrawSize.Width, DrawSize.Height);

        /// <summary>
        /// 计算数据用于显示
        /// </summary>
        protected override void CalcData()
        {
            NeedDraw = false;
            if (Option?.Series == null || Option.Series.Count == 0) return;
            if (DrawSize.Width <= 0 || DrawSize.Height <= 0) return;
            CalcAxises();

            foreach (var series in Option.Series.Values)
            {
                if (series.IsY2)
                    series.CalcData(this, XScale, Y2Scale, Option.YDataOrder);
                else
                    series.CalcData(this, XScale, YScale, Option.YDataOrder);

                if (series is UISwitchLineSeries lineSeries)
                {
                    lineSeries.YOffsetPos = series.IsY2 ?
                        Y2Scale.CalcYPixel(lineSeries.YOffset, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder) :
                        YScale.CalcYPixel(lineSeries.YOffset, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                }
            }

            NeedDraw = true;
        }

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

        protected UIScale XScale;
        protected UIScale YScale;
        protected UIScale Y2Scale;

        protected void CalcAxises()
        {
            if (Option.XAxisType == UIAxisType.DateTime)
                XScale = new UIDateScale();
            else
                XScale = new UILinearScale();

            YScale = new UILinearScale();
            Y2Scale = new UILinearScale();

            //Y轴
            {
                Option.GetAllDataYRange(out double min, out double max);
                if (min > 0 && max > 0 && !Option.YAxis.Scale) min = 0;
                if (min < 0 && max < 0 && !Option.YAxis.Scale) max = 0;
                YScale.SetRange(min, max);
                if (!Option.YAxis.MaxAuto) YScale.Max = Option.YAxis.Max;
                if (!Option.YAxis.MinAuto) YScale.Min = Option.YAxis.Min;
                if (BaseArea == null && Option.YAxis.HaveCustomLabels)
                {
                    YScale.Max = Option.YAxis.CustomLabels.Stop;
                    YScale.Min = Option.YAxis.CustomLabels.Start;
                }

                if (YScale.Max.IsNanOrInfinity() || YScale.Min.IsNanOrInfinity())
                {
                    YScale.Max = max;
                    YScale.Min = min;
                }

                YScale.AxisChange();
            }

            //Y2轴
            if (Option.HaveY2)
            {
                Option.GetAllDataY2Range(out double min, out double max);
                if (min > 0 && max > 0 && !Option.Y2Axis.Scale) min = 0;
                if (min < 0 && max < 0 && !Option.Y2Axis.Scale) max = 0;
                Y2Scale.SetRange(min, max);
                if (!Option.Y2Axis.MaxAuto) Y2Scale.Max = Option.Y2Axis.Max;
                if (!Option.Y2Axis.MinAuto) Y2Scale.Min = Option.Y2Axis.Min;

                if (BaseArea == null && Option.Y2Axis.HaveCustomLabels)
                {
                    Y2Scale.Max = Option.Y2Axis.CustomLabels.Stop;
                    Y2Scale.Min = Option.Y2Axis.CustomLabels.Start;
                }

                if (Y2Scale.Max.IsNanOrInfinity() || Y2Scale.Min.IsNanOrInfinity())
                {
                    Y2Scale.Max = max;
                    Y2Scale.Min = min;
                }

                Y2Scale.AxisChange();
            }

            //X轴
            {
                Option.GetAllDataXRange(out double min, out double max);
                XScale.SetRange(min, max);
                if (!Option.XAxis.MaxAuto) XScale.Max = Option.XAxis.Max;
                if (!Option.XAxis.MinAuto) XScale.Min = Option.XAxis.Min;

                if (BaseArea == null && Option.XAxis.HaveCustomLabels)
                {
                    XScale.Max = Option.XAxis.CustomLabels.Stop;
                    XScale.Min = Option.XAxis.CustomLabels.Start;
                }

                XScale.AxisChange();
            }
        }

        /// <summary>
        /// 图表参数
        /// </summary>
        [Browsable(false), DefaultValue(null)]
        public UILineOption Option
        {
            get
            {
                UIOption option = BaseOption ?? EmptyOption;
                return (UILineOption)option;
            }
        }

        /// <summary>
        /// 默认创建空的图表参数
        /// </summary>
        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UILineOption option = new UILineOption();
            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "LineChart";

            var series = option.AddSeries(new UILineSeries("Line1"));
            for (int i = 0; i < 200; i++)
            {
                series.Add(i, 6 * Math.Sin(i * 3 * Math.PI / 180));
            }

            series = option.AddSeries(new UILineSeries("Line2"));
            for (int i = 0; i < 150; i++)
            {
                series.Add(i, 6 * Math.Cos(i * 5 * Math.PI / 180));
            }

            option.XAxis.Name = "数值";
            option.YAxis.Name = "数值";

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

            if (bmp == null || bmp.Width != Width || bmp.Height != Height)
            {
                bmp?.Dispose();
                bmp = new Bitmap(Width, Height);
            }

            if (bmpGreater == null || bmpGreater.Width != Width || bmpGreater.Height != Height)
            {
                bmpGreater?.Dispose();
                bmpGreater = new Bitmap(Width, Height);
            }

            if (bmpLess == null || bmpLess.Width != Width || bmpLess.Height != Height)
            {
                bmpLess?.Dispose();
                bmpLess = new Bitmap(Width, Height);
            }

            DrawTitle(g, Option.Title);
            DrawSeries(g);
            DrawLegend(g, Option.Legend);
            DrawAxis(g);
            DrawAxisScales(g);
            DrawPointSymbols(g);
            DrawOther(g);
        }

        private void DrawAxis(Graphics g)
        {
            if (Option.Grid.LeftShow)
                g.DrawLine(ForeColor, Option.Grid.Left, Option.Grid.Top, Option.Grid.Left, Height - Option.Grid.Bottom);
            if (Option.Grid.TopShow)
                g.DrawLine(ForeColor, Option.Grid.Left, Option.Grid.Top, Width - Option.Grid.Right, Option.Grid.Top);
            if (Option.Grid.RightShow)
                g.DrawLine(ForeColor, Width - Option.Grid.Right, Option.Grid.Top, Width - Option.Grid.Right, Height - Option.Grid.Bottom);
            if (Option.Grid.BottomShow)
                g.DrawLine(ForeColor, Option.Grid.Left, Height - Option.Grid.Bottom, Width - Option.Grid.Right, Height - Option.Grid.Bottom);

            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            float zeroPos = YScale.CalcYPixel(0, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
            if (zeroPos > Option.Grid.Top && zeroPos < Height - Option.Grid.Bottom)
            {
                if (Option.ShowZeroLine)
                {
                    g.DrawLine(ForeColor, DrawOrigin.X, zeroPos, DrawOrigin.X + DrawSize.Width, zeroPos);
                }

                if (Option.ShowZeroValue)
                {
                    g.DrawString("0", TempFont, ForeColor, new Rectangle(DrawOrigin.X - Option.YAxis.AxisTick.Length - Width, (int)zeroPos - Height, Width, Height * 2), ContentAlignment.MiddleRight);
                }
            }

            if (XScale == null || YScale == null || Y2Scale == null) return;

            if (Option.XAxis.ShowArrow)
            {
                Point pt1 = new Point(Width - Option.Grid.Right + Option.XAxis.ShowArrowSize, Height - Option.Grid.Bottom);
                g.DrawLine(ForeColor, Option.Grid.Left, Height - Option.Grid.Bottom, pt1.X, pt1.Y);
                Point pt2 = new Point(pt1.X - 10, pt1.Y - 4);
                Point pt3 = new Point(pt1.X - 10, pt1.Y + 4);
                g.FillPolygon(ForeColor, new PointF[] { pt1, pt2, pt3 });

                if (Option.XAxis.Unit.IsValid())
                {
                    g.DrawString(Option.XAxis.Unit, TempFont, ForeColor, new Rectangle(pt1.X - 200, pt1.Y, 400, 400), ContentAlignment.TopCenter);
                }
            }

            if (Option.YAxis.ShowArrow)
            {
                Point pt1 = new Point(Option.Grid.Left, Option.Grid.Top - Option.YAxis.ShowArrowSize);
                g.DrawLine(ForeColor, pt1.X, pt1.Y, Option.Grid.Left, Height - Option.Grid.Bottom);
                Point pt2 = new Point(pt1.X - 4, pt1.Y + 10);
                Point pt3 = new Point(pt1.X + 4, pt1.Y + 10);
                g.FillPolygon(ForeColor, new PointF[] { pt1, pt2, pt3 });

                if (Option.YAxis.Unit.IsValid())
                {
                    g.DrawString(Option.YAxis.Unit, TempFont, ForeColor, new Rectangle(pt1.X - 200, pt1.Y - 200, 200, 400), ContentAlignment.MiddleRight);
                }
            }

            if (Option.HaveY2 && Option.Y2Axis.ShowArrow)
            {
                Point pt1 = new Point(Width - Option.Grid.Right, Option.Grid.Top - Option.Y2Axis.ShowArrowSize);
                g.DrawLine(ForeColor, pt1.X, pt1.Y, Width - Option.Grid.Right, Height - Option.Grid.Bottom);
                Point pt2 = new Point(pt1.X - 4, pt1.Y + 10);
                Point pt3 = new Point(pt1.X + 4, pt1.Y + 10);
                g.FillPolygon(ForeColor, new PointF[] { pt1, pt2, pt3 });

                if (Option.Y2Axis.Unit.IsValid())
                {
                    g.DrawString(Option.Y2Axis.Unit, TempFont, ForeColor, new Rectangle(pt1.X, pt1.Y - 200, 200, 400), ContentAlignment.MiddleLeft);
                }
            }

            //X Tick           
            {
                double[] XLabels = Option.XAxis.HaveCustomLabels ? Option.XAxis.CustomLabels.LabelValues() : XScale.CalcLabels();
                float[] labels = XScale.CalcXPixels(XLabels, DrawOrigin.X, DrawSize.Width);

                float xr = 0;
                for (int i = 0; i < labels.Length; i++)
                {
                    float x = labels[i];
                    if (x < Option.Grid.Left || x > Width - Option.Grid.Right) continue;

                    if (Option.XAxis.AxisLabel.Show)
                    {
                        string label;
                        if (Option.XAxisType == UIAxisType.DateTime)
                        {
                            if (Option.XAxis.AxisLabel.DateTimeFormat.IsNullOrEmpty())
                                label = new DateTimeInt64(XLabels[i]).ToString(XScale.Format);
                            else
                                label = new DateTimeInt64(XLabels[i]).ToString(Option.XAxis.AxisLabel.DateTimeFormat);
                        }
                        else
                        {
                            if (Option.XAxis.AxisLabel.DecimalPlaces < 0)
                                label = XLabels[i].ToString(XScale.Format);
                            else
                                label = XLabels[i].ToString("F" + Option.XAxis.AxisLabel.DecimalPlaces);
                        }

                        if (Option.XAxis.HaveCustomLabels && Option.XAxis.CustomLabels.GetLabel(i).IsValid())
                        {
                            label = Option.XAxis.CustomLabels.GetLabel(i);
                        }

                        SizeF sf = TextRenderer.MeasureText(label, TempFont);
                        float xx = x - sf.Width / 2.0f;
                        if (xx > xr && xx + sf.Width < Width)
                        {
                            xr = xx + sf.Width;
                            if (Option.YDataOrder == UIYDataOrder.Asc)
                                g.DrawString(label, TempFont, ForeColor, new Rectangle((int)x - Width, DrawOrigin.Y + Option.XAxis.AxisTick.Length, Width * 2, Height), ContentAlignment.TopCenter);
                            else
                                g.DrawString(label, TempFont, ForeColor, new Rectangle((int)x - Width, 0, Width * 2, (int)(DrawOrigin.Y - Option.XAxis.AxisTick.Length)), ContentAlignment.BottomCenter);
                        }
                    }

                    if (Option.XAxis.AxisTick.Show)
                    {
                        if (Option.YDataOrder == UIYDataOrder.Asc)
                            g.DrawLine(ForeColor, x, DrawOrigin.Y, x, DrawOrigin.Y + Option.XAxis.AxisTick.Length);
                        else
                            g.DrawLine(ForeColor, x, DrawOrigin.Y, x, DrawOrigin.Y - Option.XAxis.AxisTick.Length);
                    }

                    if (x.Equals(DrawOrigin.X)) continue;
                    if (x.Equals(DrawOrigin.X + DrawSize.Width)) continue;

                    if (Option.XAxis.ShowGridLine)
                    {
                        using Pen pn = new Pen(ForeColor);
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, x, DrawOrigin.Y, x, Option.Grid.Top);
                    }
                }

                g.DrawString(Option.XAxis.Name, TempFont, ForeColor, new Rectangle(DrawOrigin.X, 0, DrawSize.Width, Height - 16), ContentAlignment.BottomCenter);
            }

            //Y Tick            
            {
                double[] YLabels = Option.YAxis.HaveCustomLabels ? Option.YAxis.CustomLabels.LabelValues() : YScale.CalcLabels();
                float[] labels = YScale.CalcYPixels(YLabels, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                float widthMax = 0;
                for (int i = 0; i < labels.Length; i++)
                {
                    float y = labels[i];
                    if (y < Option.Grid.Top || y > Height - Option.Grid.Bottom) continue;

                    string label;
                    if (Option.YAxis.AxisLabel.DecimalPlaces < 0)
                        label = YLabels[i].ToString(YScale.Format);
                    else
                        label = YLabels[i].ToString("F" + Option.YAxis.AxisLabel.DecimalPlaces);

                    Size sf = TextRenderer.MeasureText(label, TempFont);
                    widthMax = Math.Max(widthMax, sf.Width);

                    if (Option.YAxis.AxisLabel.Show)
                    {
                        g.DrawString(label, TempFont, ForeColor, new Rectangle(DrawOrigin.X - Option.YAxis.AxisTick.Length - Width, (int)y - Height, Width, Height * 2), ContentAlignment.MiddleRight);
                    }

                    if (Option.YAxis.AxisTick.Show)
                    {
                        g.DrawLine(ForeColor, DrawOrigin.X, y, DrawOrigin.X - Option.YAxis.AxisTick.Length, y);
                    }

                    if (y.Equals(DrawOrigin.Y)) continue;
                    if (y.Equals(DrawOrigin.X - DrawSize.Height)) continue;

                    if (Option.YAxis.ShowGridLine)
                    {
                        using Pen pn = new Pen(ForeColor);
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                        g.DrawLine(pn, DrawOrigin.X, y, Width - Option.Grid.Right, y);
                    }
                }

                Size sfName = TextRenderer.MeasureText(Option.YAxis.Name, TempFont);
                float xx = DrawOrigin.X - Option.YAxis.AxisTick.Length - widthMax - sfName.Height / 2.0f;
                float yy = Option.Grid.Top + DrawSize.Height / 2.0f;
                g.DrawRotateString(Option.YAxis.Name, TempFont, ForeColor, new PointF(xx, yy), 270);
            }

            //Y2 Tick
            if (Option.HaveY2)
            {
                double[] Y2Labels = Option.Y2Axis.HaveCustomLabels ? Option.Y2Axis.CustomLabels.LabelValues() : Y2Scale.CalcLabels();
                float[] labels = Y2Scale.CalcYPixels(Y2Labels, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                float widthMax = 0;
                for (int i = 0; i < labels.Length; i++)
                {
                    float y = labels[i];
                    if (y < Option.Grid.Top || y > Height - Option.Grid.Bottom) continue;

                    if (Option.Y2Axis.AxisLabel.Show)
                    {
                        string label;
                        if (Option.Y2Axis.AxisLabel.DecimalPlaces < 0)
                            label = Y2Labels[i].ToString(Y2Scale.Format);
                        else
                            label = Y2Labels[i].ToString("F" + Option.Y2Axis.AxisLabel.DecimalPlaces);

                        Size sf = TextRenderer.MeasureText(label, TempFont);
                        widthMax = Math.Max(widthMax, sf.Width);

                        if (y.Equals(DrawOrigin.Y) && Option.XAxis.ShowArrow)
                            g.DrawString(label, TempFont, ForeColor, new Rectangle(Width - Option.Grid.Right + Option.Y2Axis.AxisTick.Length, (int)y - Height, Width, Height * 2), ContentAlignment.MiddleLeft, 0, -sf.Height / 2);
                        else
                            g.DrawString(label, TempFont, ForeColor, new Rectangle(Width - Option.Grid.Right + Option.Y2Axis.AxisTick.Length, (int)y - Height, Width, Height * 2), ContentAlignment.MiddleLeft);
                    }

                    if (Option.Y2Axis.AxisTick.Show)
                    {
                        g.DrawLine(ForeColor, Width - Option.Grid.Right, y, Width - Option.Grid.Right + Option.YAxis.AxisTick.Length, y);
                    }
                }

                Size sfName = TextRenderer.MeasureText(Option.Y2Axis.Name, TempFont);
                float xx = Width - Option.Grid.Right + Option.Y2Axis.AxisTick.Length + widthMax + sfName.Height / 2.0f;
                float yy = Option.Grid.Top + DrawSize.Height / 2.0f;
                g.DrawRotateString(Option.Y2Axis.Name, TempFont, ForeColor, new PointF(xx, yy), 90);
            }
        }

        protected virtual void DrawSeries(Graphics g, Color color, UILineSeries series)
        {
            if (series.Points.Count == 0 || !series.Visible)
            {
                return;
            }

            if (series.Points.Count == 1)
            {
                g.DrawPoint(color, series.Points[0], 4);
                return;
            }

            if (series.ShowLine || series.Symbol == UILinePointSymbol.None)
            {
                using Pen pen = new Pen(color, series.Width);
                pen.DashStyle = series.DashStyle;
                if (series.DashPattern.IsValid()) pen.DashPattern = series.DashPattern;
                g.SetHighQuality();

                if (series is UISwitchLineSeries lineSeries)
                {
                    List<PointF> points = new List<PointF>();
                    points.Add(new PointF(lineSeries.Points[0].X, lineSeries.YOffsetPos));
                    points.AddRange(lineSeries.Points);
                    points.Add(new PointF(lineSeries.Points[series.Points.Count - 1].X, lineSeries.YOffsetPos));
                    using var path = points.ToArray().Path();
                    g.FillPath(color, path);
                    g.DrawLine(color, points[0], points[points.Count - 1]);
                    points.Clear();
                }
                else
                {
                    if (series.ContainsNan || !series.Smooth || series.Points.Count == 2 || ZoomAreas.Count > 5)
                    {
                        for (int i = 0; i < series.Points.Count - 1; i++)
                        {
                            g.DrawTwoPoints(pen, series.Points[i], series.Points[i + 1], DrawRect);
                        }
                    }
                    else
                    {
                        try
                        {
                            g.DrawCurve(pen, series.Points.ToArray());
                        }
                        catch
                        {
                            for (int i = 0; i < series.Points.Count - 1; i++)
                            {
                                g.DrawTwoPoints(pen, series.Points[i], series.Points[i + 1], DrawRect);
                            }
                        }
                    }
                }

                g.SetDefaultQuality();
            }
        }

        Bitmap bmp, bmpGreater, bmpLess;

        private void DrawSeries(Graphics g)
        {
            if (YScale == null) return;

            bmp?.Dispose();
            bmp = new Bitmap(Width, Height);

            int idx = 0;
            float wTop = Option.Grid.Top;
            float wBottom = Height - Option.Grid.Bottom;
            float wLeft = Option.Grid.Left;
            float wRight = Width - Option.Grid.Right;

            if ((Option.GreaterWarningArea == null && Option.LessWarningArea == null) || Option.HaveY2)
            {
                foreach (var series in Option.Series.Values)
                {
                    Color color = series.Color;
                    if (!series.CustomColor) color = ChartStyle.GetColor(idx);
                    using Graphics graphics = bmp.Graphics();
                    DrawSeries(graphics, color, series);

                    idx++;
                }
            }
            else
            {
                bmpGreater?.Dispose();
                bmpGreater = new Bitmap(Width, Height);

                bmpLess?.Dispose();
                bmpLess = new Bitmap(Width, Height);

                foreach (var series in Option.Series.Values)
                {
                    Color color = series.Color;
                    if (!series.CustomColor) color = ChartStyle.GetColor(idx);

                    using (Graphics graphics = bmp.Graphics())
                    {
                        DrawSeries(graphics, color, series);
                    }

                    if (Option.GreaterWarningArea != null)
                    {
                        using Graphics graphics = bmpGreater.Graphics();
                        DrawSeries(graphics, Option.GreaterWarningArea.Color, series);
                    }

                    if (Option.LessWarningArea != null)
                    {
                        using Graphics graphics = bmpLess.Graphics();
                        DrawSeries(graphics, Option.LessWarningArea.Color, series);
                    }

                    idx++;
                }

                if (Option.GreaterWarningArea != null)
                {
                    wTop = YScale.CalcYPixel(Option.GreaterWarningArea.Value, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                    if (wTop < Option.Grid.Top)
                    {
                        wTop = Option.Grid.Top;
                    }
                    else
                    {
                        if (wTop > Height - Option.Grid.Bottom)
                            wTop = Height - Option.Grid.Bottom;
                        g.DrawImage(bmpGreater, new Rectangle((int)wLeft, Option.Grid.Top, (int)(wRight - wLeft), (int)(wTop - Option.Grid.Top)),
                            new Rectangle((int)wLeft, Option.Grid.Top, (int)(wRight - wLeft), (int)(wTop - Option.Grid.Top)), GraphicsUnit.Pixel);
                    }
                }

                if (Option.LessWarningArea != null)
                {
                    wBottom = YScale.CalcYPixel(Option.LessWarningArea.Value, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                    if (wBottom > Height - Option.Grid.Bottom)
                    {
                        wBottom = Height - Option.Grid.Bottom;
                    }
                    else
                    {
                        if (wBottom < Option.Grid.Top)
                            wBottom = Option.Grid.Top;
                        g.DrawImage(bmpLess, new Rectangle((int)wLeft, (int)wBottom, (int)(wRight - wLeft), (int)(Height - Option.Grid.Bottom - wBottom)),
                            new Rectangle((int)wLeft, (int)wBottom, (int)(wRight - wLeft), (int)(Height - Option.Grid.Bottom - wBottom)), GraphicsUnit.Pixel);
                    }
                }
            }

            g.DrawImage(bmp, new Rectangle((int)wLeft, (int)wTop, (int)(wRight - wLeft), (int)(wBottom - wTop)),
             new Rectangle((int)wLeft, (int)wTop, (int)(wRight - wLeft), (int)(wBottom - wTop)), GraphicsUnit.Pixel);
        }

        private void DrawPointSymbols(Graphics g)
        {
            foreach (var series in Option.Series.Values)
            {
                if (series.Points.Count == 0 || !series.Visible) continue;

                Color color = series.Color;
                if (series.SymbolColor.IsValid()) color = series.SymbolColor;

                if (series.Symbol != UILinePointSymbol.None)
                {
                    using Brush br = new SolidBrush(FillColor);
                    using Brush br1 = new SolidBrush(color);
                    using Pen pn = new Pen(color, series.SymbolLineWidth);

                    foreach (var p in series.Points)
                    {
                        if (p.X < Option.Grid.Left || p.X > Width - Option.Grid.Right) continue;
                        if (p.Y < Option.Grid.Top || p.Y > Height - Option.Grid.Bottom) continue;
                        if (double.IsNaN(p.X) || double.IsNaN(p.Y)) continue;

                        switch (series.Symbol)
                        {
                            case UILinePointSymbol.Square:
                                g.FillRectangle(br, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                g.DrawRectangle(pn, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                break;
                            case UILinePointSymbol.Diamond:
                                {
                                    PointF pt1 = new PointF(p.X - series.SymbolSize, p.Y);
                                    PointF pt2 = new PointF(p.X, p.Y - series.SymbolSize);
                                    PointF pt3 = new PointF(p.X + series.SymbolSize, p.Y);
                                    PointF pt4 = new PointF(p.X, p.Y + series.SymbolSize);
                                    PointF[] pts = { pt1, pt2, pt3, pt4, pt1 };
                                    g.SetHighQuality();
                                    using GraphicsPath path = pts.Path();
                                    g.FillPath(br, path);
                                    g.DrawPath(pn, path);
                                }
                                break;
                            case UILinePointSymbol.Triangle:
                                {
                                    PointF pt1 = new PointF(p.X, p.Y - series.SymbolSize);
                                    PointF pt2 = new PointF(p.X - series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                    PointF pt3 = new PointF(p.X + series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                    PointF[] pts = { pt1, pt2, pt3, pt1 };
                                    g.SetHighQuality();
                                    using GraphicsPath path = pts.Path();
                                    g.FillPath(br, path);
                                    g.DrawPath(pn, path);
                                }
                                break;
                            case UILinePointSymbol.Circle:
                                g.SetHighQuality();
                                g.FillEllipse(br, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                g.DrawEllipse(pn, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                break;
                            case UILinePointSymbol.Round:
                                g.SetHighQuality();
                                g.FillEllipse(br1, p.X - series.SymbolSize, p.Y - series.SymbolSize, series.SymbolSize * 2, series.SymbolSize * 2);
                                break;
                            case UILinePointSymbol.Plus:
                                g.DrawLine(pn, p.X - series.SymbolSize, p.Y, p.X + series.SymbolSize, p.Y);
                                g.DrawLine(pn, p.X, p.Y - series.SymbolSize, p.X, p.Y + series.SymbolSize);
                                break;
                            case UILinePointSymbol.Star:
                                g.SetHighQuality();
                                g.DrawLine(pn, p.X, p.Y - series.SymbolSize, p.X, p.Y + series.SymbolSize);
                                g.DrawLine(pn, p.X - series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f,
                                    p.X + series.SymbolSize * 0.866f, p.Y - series.SymbolSize * 0.5f);
                                g.DrawLine(pn, p.X - series.SymbolSize * 0.866f, p.Y - series.SymbolSize * 0.5f,
                                    p.X + series.SymbolSize * 0.866f, p.Y + series.SymbolSize * 0.5f);
                                break;
                        }
                    }

                    g.SetDefaultQuality();
                }
            }
        }

        private void DrawAxisScales(Graphics g)
        {
            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            if (YScale != null)
            {
                foreach (var line in Option.YAxisScaleLines)
                {
                    float pos = YScale.CalcYPixel(line.Value, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);


                    if (pos <= Option.Grid.Top || pos >= Height - Option.Grid.Bottom) continue;

                    using Pen pn = new Pen(line.Color, line.Size);
                    if (line.DashDot)
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                    }

                    g.DrawLine(pn, DrawOrigin.X + 1, pos, Width - Option.Grid.Right - 1, pos);
                    g.DrawString(line.Name, TempFont, line.Color, new Rectangle(DrawOrigin.X + 4, (int)pos - 2 - Height, DrawSize.Width - 8, Height), (StringAlignment)((int)line.Left), StringAlignment.Far);
                }
            }

            if (Y2Scale != null)
            {
                foreach (var line in Option.Y2AxisScaleLines)
                {
                    float pos = Y2Scale.CalcYPixel(line.Value, DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                    if (pos <= Option.Grid.Top || pos >= Height - Option.Grid.Bottom) continue;

                    using Pen pn = new Pen(line.Color, line.Size);
                    if (line.DashDot)
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                    }

                    g.DrawLine(pn, DrawOrigin.X + 1, pos, Width - Option.Grid.Right - 1, pos);
                    g.DrawString(line.Name, TempFont, line.Color, new Rectangle(DrawOrigin.X + 4, (int)pos - 2 - Height, DrawSize.Width - 8, Height), (StringAlignment)((int)line.Left), StringAlignment.Far);
                }
            }

            int idx = 0;
            float xmin = -9999;
            float ymin = Option.Grid.Top + 4;
            if (XScale != null)
            {
                foreach (var line in Option.XAxisScaleLines)
                {
                    float pos = XScale.CalcXPixel(line.Value, DrawOrigin.X, DrawSize.Width);
                    if (pos <= Option.Grid.Left || pos >= Width - Option.Grid.Right) continue;

                    using Pen pn = new Pen(line.Color, line.Size);
                    if (line.DashDot)
                    {
                        pn.DashStyle = DashStyle.Dash;
                        pn.DashPattern = new float[] { 3, 3 };
                    }

                    g.DrawLine(pn, pos, Height - Option.Grid.Bottom - 1, pos, Option.Grid.Top + 1);

                    Size sf = TextRenderer.MeasureText(line.Name, TempFont);
                    float x = pos - sf.Width;
                    if (x < Option.Grid.Left) x = pos + 2;
                    float y;
                    if (x > xmin)
                        y = ymin;
                    else
                        y = ymin + sf.Height;

                    if (y > Height - Option.Grid.Bottom)
                    {
                        y = Option.Grid.Top + 4;
                    }

                    xmin = x + sf.Width;
                    ymin = y;
                    idx++;
                    g.DrawString(line.Name, TempFont, line.Color, new Rectangle((int)x, (int)y, Width, Height), ContentAlignment.TopLeft);
                }
            }
        }

        private readonly List<UILineSelectPoint> selectPoints = new List<UILineSelectPoint>();
        private readonly List<UILineSelectPoint> selectPointsTemp = new List<UILineSelectPoint>();

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!NeedDraw) return;

            if (!IsMouseDown)
            {
                selectPointsTemp.Clear();
                foreach (var series in Option.Series.Values)
                {
                    if (series.DataCount == 0) continue;
                    if (!series.Visible) continue;

                    if (series.GetNearestPoint(e.Location, 4, out double x, out double y, out int index))
                    {
                        UILineSelectPoint point = new UILineSelectPoint();
                        point.Series = series;
                        point.Index = index;
                        point.X = x;
                        point.Y = y;
                        point.Location = new Point((int)series.Points[index].X, (int)series.Points[index].Y);
                        selectPointsTemp.Add(point);
                    }
                }

                bool isNew = false;
                if (selectPointsTemp.Count != selectPoints.Count)
                {
                    isNew = true;
                }
                else
                {
                    Dictionary<string, UILineSelectPoint> points = selectPoints.ToDictionary(p => p.Series.Name);
                    foreach (var point in selectPointsTemp)
                    {
                        if (!points.ContainsKey(point.Series.Name))
                        {
                            isNew = true;
                            break;
                        }

                        if (points[point.Series.Name].Index != point.Index)
                        {
                            isNew = true;
                            break;
                        }
                    }
                }

                if (isNew)
                {
                    selectPoints.Clear();
                    StringBuilder sb = new StringBuilder();
                    int idx = 0;
                    Dictionary<int, UILineSelectPoint> dictionary = selectPointsTemp.ToDictionary(p => p.Series.Index);
                    List<UILineSelectPoint> points = dictionary.SortedValues();
                    foreach (var point in points)
                    {
                        selectPoints.Add(point);

                        if (idx > 0) sb.Append('\n');

                        if (PointFormat != null)
                        {
                            sb.Append(PointFormat(this, point));
                        }
                        else
                        {
                            sb.Append(point.Series.Name);
                            sb.Append('\n');
                            sb.Append(Option.XAxis.Name + ": ");

                            string customlabel = "";
                            if (customlabel.IsNullOrEmpty())
                            {
                                if (Option.XAxisType == UIAxisType.DateTime)
                                    sb.Append(new DateTimeInt64(point.X).ToString(point.Series.XAxisDateTimeFormat.IsValid() ? point.Series.XAxisDateTimeFormat : XScale.Format));
                                else
                                    sb.Append(point.X.ToString(point.Series.XAxisDecimalPlaces >= 0 ? "F" + point.Series.XAxisDecimalPlaces : XScale.Format));
                            }

                            sb.Append('\n');

                            if (point.Series.IsY2)
                                sb.Append(Option.Y2Axis.Name + ": " + point.Y.ToString(point.Series.YAxisDecimalPlaces >= 0 ? "F" + point.Series.YAxisDecimalPlaces : Y2Scale.Format));
                            else
                                sb.Append(Option.YAxis.Name + ": " + point.Y.ToString(point.Series.YAxisDecimalPlaces >= 0 ? "F" + point.Series.YAxisDecimalPlaces : YScale.Format));
                        }

                        idx++;
                    }

                    if (Option.ToolTip.Visible)
                    {
                        if (sb.ToString().IsNullOrEmpty())
                        {
                            tip.Visible = false;
                        }
                        else
                        {
                            using Graphics g = this.CreateGraphics();
                            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
                            Size sf = TextRenderer.MeasureText(sb.ToString(), TempFont);
                            tip.Size = new Size(sf.Width + 4, sf.Height + 4);

                            int x = e.Location.X + 15;
                            int y = e.Location.Y + 20;
                            if (e.Location.X + 15 + tip.Width > Width - Option.Grid.Right)
                                x = e.Location.X - tip.Width - 2;
                            if (e.Location.Y + 20 + tip.Height > Height - Option.Grid.Bottom)
                                y = e.Location.Y - tip.Height - 2;

                            tip.Left = x;
                            tip.Top = y;

                            tip.Text = sb.ToString();
                            if (!tip.Visible) tip.Visible = true;
                        }
                    }

                    if (selectPoints.Count > 0)
                    {
                        PointValue?.Invoke(this, selectPoints.ToArray());
                    }
                }
            }
            else
            {
                switch (MouseDownType)
                {
                    case UILineChartMouseDownType.Zoom:
                        if (MouseZoom && e.Button == MouseButtons.Left &&
                            e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                            e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                        {
                            StopPoint = e.Location;
                            Invalidate();
                        }
                        break;
                    case UILineChartMouseDownType.XArea:
                        if (e.Button == MouseButtons.Left &&
                            e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                            e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                        {
                            StopPoint = e.Location;
                            Invalidate();
                        }
                        break;
                    case UILineChartMouseDownType.YArea:
                        if (e.Button == MouseButtons.Left &&
                            e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                            e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                        {
                            StopPoint = e.Location;
                            Invalidate();
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        public delegate void OnPointValue(object sender, UILineSelectPoint[] points);

        public delegate string OnPointFormat(object sender, UILineSelectPoint point);

        public event OnPointValue PointValue;

        public event OnPointFormat PointFormat;

        private bool IsMouseDown;

        private Point StartPoint, StopPoint;

        public UILineChartMouseDownType MouseDownType { get; set; } = UILineChartMouseDownType.Zoom;

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            switch (MouseDownType)
            {
                case UILineChartMouseDownType.Zoom:
                    if (MouseZoom && e.Button == MouseButtons.Left &&
                        e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                        e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                    {
                        IsMouseDown = true;
                        StartPoint = StopPoint = e.Location;
                    }

                    if (MouseZoom && e.Button == MouseButtons.Right && ContextMenuStrip == null)
                    {
                        ZoomBack();
                    }
                    break;
                case UILineChartMouseDownType.XArea:
                    if (e.Button == MouseButtons.Left &&
                        e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                        e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                    {
                        IsMouseDown = true;
                        StartPoint = StopPoint = e.Location;
                    }

                    break;
                case UILineChartMouseDownType.YArea:
                    if (e.Button == MouseButtons.Left &&
                        e.X > Option.Grid.Left && e.X < Width - Option.Grid.Right &&
                        e.Y > Option.Grid.Top && e.Y < Height - Option.Grid.Bottom)
                    {
                        IsMouseDown = true;
                        StartPoint = StopPoint = e.Location;
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            switch (MouseDownType)
            {
                case UILineChartMouseDownType.Zoom:
                    if (MouseZoom && IsMouseDown)
                    {
                        IsMouseDown = false;
                        Invalidate();
                        Zoom();
                    }

                    break;
                case UILineChartMouseDownType.XArea:
                    if (IsMouseDown)
                    {
                        IsMouseDown = false;
                        Invalidate();
                        double XMin = XScale.CalcXPos(Math.Min(StartPoint.X, StopPoint.X), DrawOrigin.X, DrawSize.Width);
                        double XMax = XScale.CalcXPos(Math.Max(StartPoint.X, StopPoint.X), DrawOrigin.X, DrawSize.Width);
                        if (!XMax.EqualsDouble(XMin))
                            MouseAreaSelected?.Invoke(this, MouseDownType, XMin, XMax, "X");
                    }

                    break;
                case UILineChartMouseDownType.YArea:
                    if (IsMouseDown)
                    {
                        IsMouseDown = false;
                        Invalidate();

                        double y1 = YScale.CalcYPos(Math.Min(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                        double y2 = YScale.CalcYPos(Math.Max(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                        if (!y2.EqualsDouble(y1))
                            MouseAreaSelected?.Invoke(this, MouseDownType, Math.Min(y1, y2), Math.Max(y1, y2), "Y");

                        if (Option.HaveY2)
                        {
                            y1 = Y2Scale.CalcYPos(Math.Min(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                            y2 = Y2Scale.CalcYPos(Math.Max(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                            if (!y2.EqualsDouble(y1))
                                MouseAreaSelected?.Invoke(this, MouseDownType, Math.Min(y1, y2), Math.Max(y1, y2), "Y2");
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        [DefaultValue(true)]
        [Description("鼠标可框选缩放"), Category("SunnyUI")]
        public bool MouseZoom { get; set; } = true;

        public bool IsZoom { get; private set; }
        private readonly List<ZoomArea> ZoomAreas = new List<ZoomArea>();
        private ZoomArea BaseArea;

        private void CreateBaseArea()
        {
            IsZoom = true;

            if (BaseArea == null)
            {
                BaseArea = new ZoomArea();
                BaseArea.XMin = XScale.Min;
                BaseArea.XMax = XScale.Max;
                BaseArea.YMin = YScale.Min;
                BaseArea.YMax = YScale.Max;
                BaseArea.XMinAuto = Option.XAxis.MinAuto;
                BaseArea.XMaxAuto = Option.XAxis.MaxAuto;
                BaseArea.YMinAuto = Option.YAxis.MinAuto;
                BaseArea.YMaxAuto = Option.YAxis.MaxAuto;

                if (Option.HaveY2)
                {
                    BaseArea.Y2Min = Y2Scale.Min;
                    BaseArea.Y2Max = Y2Scale.Max;
                    BaseArea.Y2MinAuto = Option.Y2Axis.MinAuto;
                    BaseArea.Y2MaxAuto = Option.Y2Axis.MaxAuto;
                }
            }
        }

        private void Zoom()
        {
            if (Math.Abs(StartPoint.X - StopPoint.X) < 6 && Math.Abs(StartPoint.Y - StopPoint.Y) < 6) return;

            CreateBaseArea();

            var zoomArea = new ZoomArea();
            zoomArea.XMin = XScale.CalcXPos(Math.Min(StartPoint.X, StopPoint.X), DrawOrigin.X, DrawSize.Width);
            zoomArea.XMax = XScale.CalcXPos(Math.Max(StartPoint.X, StopPoint.X), DrawOrigin.X, DrawSize.Width);

            double y1 = YScale.CalcYPos(Math.Min(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
            double y2 = YScale.CalcYPos(Math.Max(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
            zoomArea.YMax = Math.Max(y1, y2);
            zoomArea.YMin = Math.Min(y1, y2);

            if (Option.HaveY2)
            {
                y1 = Y2Scale.CalcYPos(Math.Min(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                y2 = Y2Scale.CalcYPos(Math.Max(StartPoint.Y, StopPoint.Y), DrawOrigin.Y, DrawSize.Height, Option.YDataOrder);
                zoomArea.Y2Max = Math.Max(y1, y2);
                zoomArea.Y2Min = Math.Min(y1, y2);
            }

            AddZoomArea(zoomArea);
        }

        public const double MinInterval = 0.000005;
        public const double MaxInterval = int.MaxValue;

        private void AddZoomArea(ZoomArea zoomArea)
        {
            if (zoomArea.XMax - zoomArea.XMin <= MinInterval) return;
            if (zoomArea.YMax - zoomArea.YMin <= MinInterval) return;
            if (zoomArea.XMax - zoomArea.XMin >= MaxInterval) return;
            if (zoomArea.YMax - zoomArea.YMin >= MaxInterval) return;

            if (Option.HaveY2)
            {
                if (zoomArea.Y2Max - zoomArea.Y2Min <= MinInterval) return;
                if (zoomArea.Y2Max - zoomArea.Y2Min >= MaxInterval) return;
            }

            ZoomAreas.Add(zoomArea);
            Zoom(zoomArea);
        }

        private void Zoom(ZoomArea zoomArea)
        {
            Option.XAxis.Min = zoomArea.XMin;
            Option.XAxis.Max = zoomArea.XMax;
            Option.YAxis.Max = zoomArea.YMax;
            Option.YAxis.Min = zoomArea.YMin;

            Option.XAxis.MinAuto = zoomArea.XMinAuto;
            Option.XAxis.MaxAuto = zoomArea.XMaxAuto;
            Option.YAxis.MinAuto = zoomArea.YMinAuto;
            Option.YAxis.MaxAuto = zoomArea.YMaxAuto;

            if (Option.HaveY2)
            {
                Option.Y2Axis.Max = zoomArea.Y2Max;
                Option.Y2Axis.Min = zoomArea.Y2Min;
                Option.Y2Axis.MinAuto = zoomArea.Y2MinAuto;
                Option.Y2Axis.MaxAuto = zoomArea.Y2MaxAuto;
            }

            CalcData();
            Invalidate();
        }

        public void ZoomNormal()
        {
            if (!IsZoom) return;

            IsZoom = false;
            Option.XAxis.Min = BaseArea.XMin;
            Option.XAxis.Max = BaseArea.XMax;
            Option.YAxis.Min = BaseArea.YMin;
            Option.YAxis.Max = BaseArea.YMax;

            Option.XAxis.MinAuto = BaseArea.XMinAuto;
            Option.XAxis.MaxAuto = BaseArea.XMaxAuto;
            Option.YAxis.MinAuto = BaseArea.YMinAuto;
            Option.YAxis.MaxAuto = BaseArea.YMaxAuto;

            if (Option.HaveY2)
            {
                Option.Y2Axis.Min = BaseArea.Y2Min;
                Option.Y2Axis.Max = BaseArea.Y2Max;
                Option.Y2Axis.MinAuto = BaseArea.Y2MinAuto;
                Option.Y2Axis.MaxAuto = BaseArea.Y2MaxAuto;
            }

            BaseArea = null;
            CalcData();
            Invalidate();
        }

        public void ZoomBack()
        {
            if (!IsZoom) return;

            if (ZoomAreas.Count > 1)
            {
                ZoomAreas.RemoveAt(ZoomAreas.Count - 1);
                Zoom(ZoomAreas[ZoomAreas.Count - 1]);
            }
            else
            {
                ZoomAreas.Clear();
                ZoomNormal();
            }
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsMouseDown = false;
        }

        protected virtual void DrawOther(Graphics g)
        {
            if (IsMouseDown)
            {
                Color color = Color.FromArgb(50, UIColor.Blue);


                switch (MouseDownType)
                {
                    case UILineChartMouseDownType.Zoom:
                        g.FillRectangle(color,
                            Math.Min(StartPoint.X, StopPoint.X),
                            Math.Min(StartPoint.Y, StopPoint.Y),
                            Math.Abs(StopPoint.X - StartPoint.X),
                            Math.Abs(StopPoint.Y - StartPoint.Y));
                        g.DrawRectangle(UIColor.Blue,
                            Math.Min(StartPoint.X, StopPoint.X),
                            Math.Min(StartPoint.Y, StopPoint.Y),
                            Math.Abs(StopPoint.X - StartPoint.X),
                            Math.Abs(StopPoint.Y - StartPoint.Y));
                        break;
                    case UILineChartMouseDownType.XArea:
                        g.FillRectangle(color,
                            Math.Min(StartPoint.X, StopPoint.X),
                            Option.Grid.Top,
                            Math.Abs(StopPoint.X - StartPoint.X),
                            DrawSize.Height);
                        g.DrawRectangle(UIColor.Blue,
                            Math.Min(StartPoint.X, StopPoint.X),
                            Option.Grid.Top,
                            Math.Abs(StopPoint.X - StartPoint.X),
                            DrawSize.Height);
                        break;
                    case UILineChartMouseDownType.YArea:
                        g.FillRectangle(color,
                            Option.Grid.Left,
                            Math.Min(StartPoint.Y, StopPoint.Y),
                             DrawSize.Width,
                            Math.Abs(StopPoint.Y - StartPoint.Y));
                        g.DrawRectangle(UIColor.Blue,
                            Option.Grid.Left,
                            Math.Min(StartPoint.Y, StopPoint.Y),
                             DrawSize.Width,
                            Math.Abs(StopPoint.Y - StartPoint.Y));
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Left) ZoomNormal();
        }

        public event OnMouseAreaSelected MouseAreaSelected;
    }
}
