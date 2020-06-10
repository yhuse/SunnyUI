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
******************************************************************************/

using System.Collections.Generic;
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

        private void Tip_MouseEnter(object sender, System.EventArgs e)
        {
            tip.Visible = false;
        }

        private int decimalNumber;

        [DefaultValue(0),Description("显示数据格式化小数点后位数")]
        public int DecimalNumber
        {
            get => decimalNumber;
            set
            {
                if (decimalNumber != value)
                {
                    decimalNumber = value;
                    Invalidate();
                }
            }
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

        private UIOption _option;

        [Browsable(false)]
        public UIOption Option
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
            Option = option;
            CalcData(option);
        }

        protected virtual void CalcData(UIOption o)
        {
        }

        protected UIOption emptyOption;

        protected UIOption EmptyOption
        {
            get
            {
                if (emptyOption == null)
                {
                    CreateEmptyOption();
                    CalcData(emptyOption);
                }

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
            if (o.Series.Count > 0) DrawSeries(g,o, o.Series);
            if (o.Legend != null) DrawLegend(g, o.Legend);
        }

        protected virtual void DrawTitle(Graphics g, UITitle title)
        {
        }

        protected virtual void DrawSeries(Graphics g,UIOption o, List<UISeries> series)
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

        [DefaultValue(8)]
        public int TextInterval { get; set; } = 8;

        public Font subFont = UIFontColor.SubFont;

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

        public Font legendFont = UIFontColor.SubFont;

        [DefaultValue(typeof(Font), "微软雅黑, 9pt")]
        public Font LegendFont
        {
            get => legendFont;
            set
            {
                legendFont = value;
                Invalidate();
            }
        }
    }
}