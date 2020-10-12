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
 * 文件名称: UIChart.cs
 * 文件说明: 图表基类
 * 当前版本: V2.2
 * 创建日期: 2020-06-06
 *
 * 2020-06-06: V2.2.5 增加文件说明
 * 2020-09-10: V2.2.7 增加图表的边框线颜色设置
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
            ShowText = false;
            fillColor = UIChartStyles.Plain.BackColor;
            foreColor = UIChartStyles.Plain.ForeColor;
            Width = 400;
            Height = 300;

            tip.Parent = this;
            tip.Height = 32;
            tip.Width = 200;
            tip.Left = 1;
            tip.Top = 1;
            tip.StyleCustomMode = true;
            tip.Style = UIStyle.Custom;
            tip.Font = UIFontColor.SubFont;
            tip.RadiusSides = UICornerRadiusSides.None;
            tip.Visible = false;

            tip.FillColor = UIChartStyles.Plain.BackColor;
            tip.RectColor = UIChartStyles.Plain.ForeColor;
            tip.ForeColor = UIChartStyles.Plain.ForeColor;
            tip.Visible = false;
            tip.MouseEnter += Tip_MouseEnter;
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
        [DefaultValue(typeof(Color), "White")]
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
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (tip != null && !tip.Font.Equals(legendFont))
            {
                tip.Font = legendFont;
            }

            DrawOption(e.Graphics);
        }

        protected virtual void DrawOption(Graphics g)
        {
        }

        protected virtual void CreateEmptyOption()
        {
        }

        protected UIChartStyle ChartStyle => UIChartStyles.GetChartStyle(ChartStyleType);

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = ChartStyle.BackColor;
            foreColor = ChartStyle.ForeColor;
        }

        [DefaultValue(8)]
        public int TextInterval { get; set; } = 8;

        private Font subFont = UIFontColor.SubFont;

        [DefaultValue(typeof(Font), "微软雅黑, 9pt")]
        public Font SubFont
        {
            get => subFont;
            set
            {
                subFont = value;
                Invalidate();
            }
        }

        private Font legendFont = UIFontColor.SubFont;

        [DefaultValue(typeof(Font), "微软雅黑, 9pt")]
        public Font LegendFont
        {
            get => legendFont;
            set
            {
                legendFont = value;
                if (tip != null) tip.Font = subFont;
                Invalidate();
            }
        }

        protected void DrawTitle(Graphics g, UITitle title)
        {
            if (title == null) return;
            SizeF sf = g.MeasureString(title.Text, Font);
            float left = 0;
            switch (title.Left)
            {
                case UILeftAlignment.Left: left = TextInterval; break;
                case UILeftAlignment.Center: left = (Width - sf.Width) / 2.0f; break;
                case UILeftAlignment.Right: left = Width - TextInterval - sf.Width; break;
            }

            float top = 0;
            switch (title.Top)
            {
                case UITopAlignment.Top: top = TextInterval; break;
                case UITopAlignment.Center: top = (Height - sf.Height) / 2.0f; break;
                case UITopAlignment.Bottom: top = Height - TextInterval - sf.Height; break;
            }

            g.DrawString(title.Text, Font, ChartStyle.ForeColor, left, top);

            SizeF sfs = g.MeasureString(title.SubText, SubFont);
            switch (title.Left)
            {
                case UILeftAlignment.Left: left = TextInterval; break;
                case UILeftAlignment.Center: left = (Width - sfs.Width) / 2.0f; break;
                case UILeftAlignment.Right: left = Width - TextInterval - sf.Width; break;
            }
            switch (title.Top)
            {
                case UITopAlignment.Top: top = top + sf.Height; break;
                case UITopAlignment.Center: top = top + sf.Height; break;
                case UITopAlignment.Bottom: top = top - sf.Height; break;
            }

            g.DrawString(title.SubText, SubFont, ChartStyle.ForeColor, left, top);
        }

        protected void DrawLegend(Graphics g, UILegend legend)
        {
            if (legend == null) return;

            float totalHeight = 0;
            float totalWidth = 0;
            float maxWidth = 0;
            float oneHeight = 0;

            foreach (var data in legend.Data)
            {
                SizeF sf = g.MeasureString(data, LegendFont);
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
                SizeF sf = g.MeasureString(data, LegendFont);
                Color color = ChartStyle.GetColor(i);

                if (legend.Colors.Count > 0 && i >= 0 && i < legend.Colors.Count)
                    color = legend.Colors[i];

                if (legend.Orient == UIOrient.Horizontal)
                {
                    g.FillRoundRectangle(color, (int)startLeft, (int)top + 1, 18, (int)oneHeight - 2, 5);
                    g.DrawString(data, LegendFont, color, startLeft + 20, top);
                    startLeft += 22;
                    startLeft += sf.Width;
                }

                if (legend.Orient == UIOrient.Vertical)
                {
                    g.FillRoundRectangle(color, (int)left, (int)startTop + 1, 18, (int)oneHeight - 2, 5);
                    g.DrawString(data, LegendFont, color, left + 20, startTop);
                    startTop += oneHeight;
                }
            }
        }
    }
}