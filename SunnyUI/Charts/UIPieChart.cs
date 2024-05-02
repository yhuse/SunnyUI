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
 * 文件名称: UIPieChart.cs
 * 文件说明: 饼状图
 * 当前版本: V3.1
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2021-07-22: V3.0.5 增加更新数据的方法
 * 2022-07-29: V3.2.2 数据显示的小数位数重构调整至Option.DecimalPlaces
 * 2023-05-14: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIPieChart : UIChart
    {
        /// <summary>
        /// 默认创建空的图表参数
        /// </summary>
        protected override void CreateEmptyOption()
        {
            if (emptyOption != null) return;

            UIPieOption option = new UIPieOption();

            option.Title = new UITitle();
            option.Title.Text = "SunnyUI";
            option.Title.SubText = "PieChart";

            var series = new UIPieSeries();
            series.Name = "饼状图";
            series.Center = new UICenter(50, 55);
            series.Radius = 70;
            for (int i = 0; i < 5; i++)
            {
                series.AddData("Data" + i, (i + 1) * 20);
            }

            option.Series.Add(series);
            emptyOption = option;
        }

        public void Update(string seriesName, string name, double value)
        {
            var series = Option[seriesName];
            if (series != null)
            {
                series.Update(name, value);
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcData();
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

        /// <summary>
        /// 绘制图表参数
        /// </summary>
        /// <param name="g">绘制图面</param>
        protected override void DrawOption(Graphics g)
        {
            if (Option == null) return;
            DrawTitle(g, Option.Title);
            DrawSeries(g, Option.Series);
            DrawLegend(g, Option.Legend);
        }

        private bool AllIsZero;

        /// <summary>
        /// 计算数据用于显示
        /// </summary>
        protected override void CalcData()
        {
            Angles.Clear();
            if (Option == null || Option.Series == null || Option.Series.Count == 0) return;

            for (int pieIndex = 0; pieIndex < Option.Series.Count; pieIndex++)
            {
                var pie = Option.Series[pieIndex];
                Angles.TryAdd(pieIndex, new ConcurrentDictionary<int, Angle>());

                double all = 0;
                foreach (var data in pie.Data)
                {
                    all += data.Value;
                }

                AllIsZero = all.IsZero();
                if (all.IsZero()) return;
                float start = 0;
                for (int i = 0; i < pie.Data.Count; i++)
                {
                    float angle = (float)(pie.Data[i].Value * 360.0f / all);
                    float percent = (float)(pie.Data[i].Value * 100.0f / all);
                    string text = "";
                    if (Option.ToolTip != null)
                    {
                        try
                        {
                            UITemplate template = new UITemplate(Option.ToolTip.Formatter);
                            template.Set("a", pie.Name);
                            template.Set("b", pie.Data[i].Name);
                            template.Set("c", pie.Data[i].Value.ToString("F" + Option.DecimalPlaces));
                            template.Set("d", percent.ToString("F2"));
                            text = template.Render();
                        }
                        catch
                        {
                            text = pie.Data[i].Name + " : " + pie.Data[i].Value.ToString("F" + Option.DecimalPlaces) + "(" + percent.ToString("F2") + "%)";
                            if (pie.Name.IsValid()) text = pie.Name + '\n' + text;
                        }
                    }

                    Angles[pieIndex].Upsert(i, new Angle(start, angle, text));
                    start += angle;
                }
            }
        }

        private void DrawSeries(Graphics g, List<UIPieSeries> series)
        {
            if (series == null || series.Count == 0) return;

            if (AllIsZero)
            {
                if (series.Count > 0)
                {
                    RectangleF rect = GetSeriesRect(series[0]);
                    g.DrawEllipse(Color.Red, rect);
                }

                return;
            }

            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            for (int pieIndex = 0; pieIndex < series.Count; pieIndex++)
            {
                var pie = series[pieIndex];
                RectangleF rect = GetSeriesRect(pie);

                for (int azIndex = 0; azIndex < pie.Data.Count; azIndex++)
                {
                    Color color = ChartStyle.GetColor(azIndex);
                    UIPieSeriesData data = pie.Data[azIndex];
                    if (data.StyleCustomMode) color = data.Color;
                    RectangleF rectx = new RectangleF(rect.X - 10, rect.Y - 10, rect.Width + 20, rect.Width + 20);
                    g.FillPie(color, (ActivePieIndex == pieIndex && ActiveAzIndex == azIndex) ? rectx : rect, Angles[pieIndex][azIndex].Start - 90, Angles[pieIndex][azIndex].Sweep);
                    Angles[pieIndex][azIndex].TextSize = TextRenderer.MeasureText(Angles[pieIndex][azIndex].Text, TempFont);

                    if (pie.Label.Show)
                    {
                        double az = Angles[pieIndex][azIndex].Start + Angles[pieIndex][azIndex].Sweep / 2;
                        double x = Math.Abs(Math.Sin(az * Math.PI / 180));
                        double y = Math.Abs(Math.Cos(az * Math.PI / 180));

                        string name = Option.Legend != null ? Option.Legend.Data[azIndex] + " : " : "";
                        if (pie.Data[azIndex].Value > 0)
                        {
                            string text = name + pie.Data[azIndex].Value.ToString("F" + Option.DecimalPlaces);
                            Size sf = TextRenderer.MeasureText(text, TempFont);
                            int added = 9;
                            float left = 0, top = 0;
                            if (az >= 0 && az < 90)
                            {
                                left = (float)(DrawCenter(pie).X + RadiusSize(pie) * x + added);
                                top = (float)(DrawCenter(pie).Y - RadiusSize(pie) * y - sf.Height - added);
                            }
                            else if (az >= 90 && az < 180)
                            {
                                left = (float)(DrawCenter(pie).X + RadiusSize(pie) * x + added);
                                top = (float)(DrawCenter(pie).Y + RadiusSize(pie) * y + added);
                            }
                            else if (az >= 180 && az < 270)
                            {
                                left = (float)(DrawCenter(pie).X - RadiusSize(pie) * x - added) - sf.Width;
                                top = (float)(DrawCenter(pie).Y + RadiusSize(pie) * y + added);
                            }
                            else
                            {
                                left = (float)(DrawCenter(pie).X - RadiusSize(pie) * x - added) - sf.Width;
                                top = (float)(DrawCenter(pie).Y - RadiusSize(pie) * y) - sf.Height - added;
                            }

                            if (pie.Data[azIndex].Value > 0)
                                g.DrawString(text, TempFont, color, new Rectangle((int)left, (int)top, Width, Height), ContentAlignment.TopLeft);
                        }
                    }
                }
            }
        }

        private readonly ConcurrentDictionary<int, ConcurrentDictionary<int, Angle>> Angles = new ConcurrentDictionary<int, ConcurrentDictionary<int, Angle>>();

        /// <summary>
        /// 图表参数
        /// </summary>
        [Browsable(false), DefaultValue(null)]
        public UIPieOption Option
        {
            get
            {
                UIOption option = BaseOption ?? EmptyOption;
                return (UIPieOption)option;
            }

            // set
            // {
            //     SetOption(value);
            // }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Option.SeriesCount == 0)
            {
                SetPieAndAzIndex(-1, -1);
                return;
            }

            if (AllIsZero) return;

            for (int pieIndex = 0; pieIndex < Option.SeriesCount; pieIndex++)
            {
                RectangleF rect = GetSeriesRect(Option.Series[pieIndex]);
                if (!e.Location.InRect(rect)) continue;

                PointF pf = new PointF(rect.Left + rect.Width / 2.0f, rect.Top + rect.Height / 2.0f);
                if (Drawing.CalcDistance(e.Location, pf) * 2 > rect.Width) continue;

                double az = Drawing.CalcAngle(e.Location, pf);
                for (int azIndex = 0; azIndex < Option.Series[pieIndex].Data.Count; azIndex++)
                {
                    Angle angle = Angles[pieIndex][azIndex];
                    if (az >= angle.Start && az <= angle.Start + angle.Sweep)
                    {
                        SetPieAndAzIndex(pieIndex, azIndex);
                        if (tip.Text != angle.Text)
                        {
                            tip.Text = angle.Text;
                            tip.Size = new Size((int)angle.TextSize.Width + 4, (int)angle.TextSize.Height + 4);
                        }

                        if (az >= 0 && az < 90)
                        {
                            tip.Top = e.Location.Y + 20;
                            tip.Left = e.Location.X - tip.Width;
                        }
                        else if (az >= 90 && az < 180)
                        {
                            tip.Left = e.Location.X - tip.Width;
                            tip.Top = e.Location.Y - tip.Height - 2;
                        }
                        else if (az >= 180 && az < 270)
                        {
                            tip.Left = e.Location.X;
                            tip.Top = e.Location.Y - tip.Height - 2;
                        }
                        else if (az >= 270 && az < 360)
                        {
                            tip.Left = e.Location.X + 15;
                            tip.Top = e.Location.Y + 20;
                        }

                        if (Option.ToolTip.Visible)
                        {
                            if (!tip.Visible) tip.Visible = angle.Text.IsValid();
                        }

                        return;
                    }
                }
            }

            SetPieAndAzIndex(-1, -1);
            tip.Visible = false;
        }

        private int ActiveAzIndex = -1;
        private int ActivePieIndex = -1;

        private void SetPieAndAzIndex(int pieIndex, int azIndex)
        {
            if (ActivePieIndex != pieIndex || ActiveAzIndex != azIndex)
            {
                ActivePieIndex = pieIndex;
                ActiveAzIndex = azIndex;
                Invalidate();
            }
        }

        private RectangleF GetSeriesRect(UIPieSeries series)
        {
            int left = series.Center.Left;
            int top = series.Center.Top;
            left = Width * left / 100;
            top = Height * top / 100;
            float halfRadius = Math.Min(Width, Height) * series.Radius / 200.0f;
            return new RectangleF(left - halfRadius, top - halfRadius, halfRadius * 2, halfRadius * 2);
        }

        private Point DrawCenter(UIPieSeries series)
        {
            int left = series.Center.Left;
            int top = series.Center.Top;
            left = Width * left / 100;
            top = Height * top / 100;
            return new Point(left, top);
        }

        private float RadiusSize(UIPieSeries series)
        {
            return Math.Min(Width, Height) * series.Radius / 200.0f;
        }

        private class Angle
        {
            public float Start { get; set; }
            public float Sweep { get; set; }

            public Angle(float start, float sweep, string text)
            {
                Start = start;
                Sweep = sweep;
                Text = text;
            }

            public string Text { get; set; }

            public Size TextSize { get; set; }
        }
    }
}