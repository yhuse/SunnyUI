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
 * 文件名称: UIChart.cs
 * 文件说明: 图表基类
 * 当前版本: V3.1
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2020-09-10: V2.2.7 增加图表的边框线颜色设置
 * 2023-05-14: V3.3.6 重构DrawString函数
 * 2023-07-25: V3.4.1 Legend增加一种绘制直线的方法
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIChart : UIControl
    {
        public UIChart()
        {
            SetStyleFlags(true, false);
            ShowText = false;
            fillColor = UIChartStyles.Plain.BackColor;
            foreColor = UIChartStyles.Plain.ForeColor;
            Width = 400;
            Height = 300;

            SubFont = UIStyles.SubFont();
            LegendFont = UIStyles.SubFont();

            tip.Parent = this;
            tip.Height = 32;
            tip.Width = 200;
            tip.Left = 1;
            tip.Top = 1;
            tip.StyleCustomMode = true;
            tip.Style = UIStyle.Custom;
            tip.Font = UIStyles.SubFont();
            tip.RadiusSides = UICornerRadiusSides.None;
            tip.Visible = false;

            tip.FillColor = UIChartStyles.Plain.BackColor;
            tip.RectColor = UIChartStyles.Plain.ForeColor;
            tip.ForeColor = UIChartStyles.Plain.ForeColor;
            tip.Visible = false;
            tip.MouseEnter += Tip_MouseEnter;
            tip.VisibleChanged += Tip_VisibleChanged;
        }

        private void Tip_VisibleChanged(object sender, EventArgs e)
        {
            tip.Font = this.Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
        }

        private void Tip_MouseEnter(object sender, EventArgs e)
        {
            tip.Visible = false;
        }

        protected readonly UITransparentPanel tip = new UITransparentPanel();
        private UIChartStyleType chartStyleType = UIChartStyleType.Plain;

        [DefaultValue(UIChartStyleType.Plain)]
        public UIChartStyleType ChartStyleType
        {
            get => chartStyleType;
            set
            {
                chartStyleType = value;
                if (Style != UIStyle.Custom)
                {
                    fillColor = ChartStyle.BackColor;
                    foreColor = ChartStyle.ForeColor;
                }

                tip.FillColor = ChartStyle.BackColor;
                tip.RectColor = ChartStyle.ForeColor;
                tip.ForeColor = ChartStyle.ForeColor;

                Invalidate();
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "54, 54, 54")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "244, 244, 244")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.FillPath(fillColor, path);
        }

        private UIOption _option;

        [Browsable(false), DefaultValue(null)]
        protected UIOption BaseOption
        {
            get => _option;
            set
            {
                _option = value;
                Invalidate();
            }
        }

        public void SetOption(UIOption option)
        {
            BaseOption = option;
            CalcData();
        }

        protected virtual void CalcData()
        {
        }

        protected UIOption emptyOption;

        [Browsable(false)]
        protected UIOption EmptyOption
        {
            get
            {
                if (emptyOption == null)
                {
                    CreateEmptyOption();
                    CalcData();
                }

                return emptyOption;
            }
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawOption(e.Graphics);
        }

        protected virtual void DrawOption(Graphics g)
        {
        }

        protected virtual void CreateEmptyOption()
        {
        }

        protected UIChartStyle ChartStyle => UIChartStyles.GetChartStyle(ChartStyleType);

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = ChartStyle.BackColor;
            foreColor = ChartStyle.ForeColor;
        }

        [DefaultValue(8)]
        public int TextInterval { get; set; } = 8;

        [Browsable(true)]
        public Font SubFont { get; set; }

        [Browsable(true)]
        public Font LegendFont { get; set; }
        protected void DrawTitle(Graphics g, UITitle title)
        {
            if (title == null) return;
            if (!title.Text.IsValid()) return;
            Size sf = TextRenderer.MeasureText(title.Text, Font);
            g.DrawString(title.Text, Font, ChartStyle.ForeColor, new Rectangle(TextInterval, TextInterval, Width - TextInterval * 2, Height - TextInterval * 2), (StringAlignment)((int)title.Left), (StringAlignment)((int)title.Top));

            if (!title.SubText.IsValid()) return;
            using var TempFont = Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            g.DrawString(title.SubText, TempFont, ChartStyle.ForeColor, new Rectangle(TextInterval, TextInterval, Width - TextInterval * 2, Height - TextInterval * 2),
                (StringAlignment)((int)title.Left), (StringAlignment)((int)title.Top), 0, title.Top == UITopAlignment.Bottom ? -sf.Height : sf.Height);
        }

        protected void DrawLegend(Graphics g, UILegend legend)
        {
            if (legend == null) return;

            float totalHeight = 0;
            float totalWidth = 0;
            float maxWidth = 0;
            float oneHeight = 0;

            using var TempLegendFont = this.Font.DPIScaleFont(UIStyles.DefaultSubFontSize);
            foreach (var data in legend.Data)
            {
                Size sf = TextRenderer.MeasureText(data, TempLegendFont);
                totalHeight += sf.Height;
                totalWidth += sf.Width;
                totalWidth += 20;

                maxWidth = Math.Max(sf.Width, maxWidth);
                oneHeight = sf.Height;
            }

            float top = 0;
            float left = 0;

            if (legend.Orient == UIOrient.Horizontal)
            {
                if (legend.Left == UILeftAlignment.Left) left = TextInterval;
                if (legend.Left == UILeftAlignment.Center) left = (Width - totalWidth) / 2.0f;
                if (legend.Left == UILeftAlignment.Right) left = Width - totalWidth - TextInterval;

                if (legend.Top == UITopAlignment.Top) top = TextInterval;
                if (legend.Top == UITopAlignment.Center) top = (Height - oneHeight) / 2.0f;
                if (legend.Top == UITopAlignment.Bottom) top = Height - oneHeight - TextInterval;
            }

            if (legend.Orient == UIOrient.Vertical)
            {
                if (legend.Left == UILeftAlignment.Left) left = TextInterval;
                if (legend.Left == UILeftAlignment.Center) left = (Width - maxWidth) / 2.0f - 10;
                if (legend.Left == UILeftAlignment.Right) left = Width - maxWidth - TextInterval - 20;

                if (legend.Top == UITopAlignment.Top) top = TextInterval;
                if (legend.Top == UITopAlignment.Center) top = (Height - totalHeight) / 2.0f;
                if (legend.Top == UITopAlignment.Bottom) top = Height - totalHeight - TextInterval;
            }

            float startLeft = left;
            float startTop = top;
            for (int i = 0; i < legend.DataCount; i++)
            {
                var data = legend.Data[i];
                Size sf = TextRenderer.MeasureText(data, TempLegendFont);
                Color color = ChartStyle.GetColor(i);

                if (legend.Colors.Count > 0 && i >= 0 && i < legend.Colors.Count)
                    color = legend.Colors[i];

                if (legend.Orient == UIOrient.Horizontal)
                {
                    if (legend.Style == UILegendStyle.Rectangle)
                        g.FillRoundRectangle(color, (int)startLeft, (int)top + 1, 18, (int)oneHeight - 2, 5);
                    if (legend.Style == UILegendStyle.Line)
                        g.DrawLine(color, startLeft, top + oneHeight / 2, startLeft + 18, top + oneHeight / 2);

                    g.DrawString(data, TempLegendFont, color, new Rectangle((int)startLeft + 18, (int)top, Width, Height), ContentAlignment.TopLeft);
                    startLeft += 22;
                    startLeft += sf.Width;
                }

                if (legend.Orient == UIOrient.Vertical)
                {
                    if (legend.Style == UILegendStyle.Rectangle)
                        g.FillRoundRectangle(color, (int)left, (int)startTop + 1, 18, (int)oneHeight - 2, 5);
                    if (legend.Style == UILegendStyle.Line)
                        g.DrawLine(color, left, startTop, left + 18, startTop + oneHeight / 2);

                    g.DrawString(data, TempLegendFont, color, new Rectangle((int)left + 18, (int)startTop, Width, Height), ContentAlignment.TopLeft);
                    startTop += oneHeight;
                }
            }
        }
    }
}