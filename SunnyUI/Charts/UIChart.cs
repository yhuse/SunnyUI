using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI.Charts
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
        }

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

                Invalidate();
            }
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("自定义")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        ///     填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色")]
        [Category("自定义")]
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

        private UIOption option;

        [Browsable(false)]
        public UIOption Option
        {
            get => option;
            set
            {
                option = value;
                Invalidate();
            }
        }

        protected UIOption emptyOption;

        protected UIOption EmptyOption
        {
            get
            {
                if (emptyOption == null)
                    CreateEmptyOption();

                return emptyOption;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawOption(e.Graphics, Option ?? EmptyOption);
        }

        protected virtual void CreateEmptyOption()
        {
        }

        protected UIChartStyle ChartStyle => UIChartStyles.GetChartStyle(ChartStyleType);

        private void DrawOption(Graphics g, UIOption o)
        {
            if (o == null) return;
            if (o.Title != null) DrawTitle(g, o.Title);
            if (o.Series.Count > 0) DrawSeries(g, o.Series);
            if (o.Legend != null) DrawLegend(g, o.Legend);
        }

        protected virtual void DrawTitle(Graphics g, UITitle title)
        {
        }

        protected virtual void DrawSeries(Graphics g, List<UISeries> series)
        {
        }

        protected virtual void DrawLegend(Graphics g, UILegend legend)
        {
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = ChartStyle.BackColor;
            foreColor = ChartStyle.ForeColor;
        }
    }
}