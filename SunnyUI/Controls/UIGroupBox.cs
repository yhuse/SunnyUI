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
 * 文件名称: UIGroupBox.cs
 * 文件说明: 组框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2022-05-30: V3.1.9 修复Padding设置
 * 2023-05-13: V3.3.6 重构DrawString函数
 * 2023-07-11: V3.4.0 解决BackColor,FillColor设置为透明时，标题下面会出现横线
 * 2023-07-19: V3.4.1 解决BackColor,FillColor设置为透明时，文本位置与边框线重叠的问题
 * 2024-03-22: V3.6.5 修复Enabled为false时标题行框线绘制颜色
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultProperty("Text")]
    public partial class UIGroupBox : UIPanel
    {
        public UIGroupBox()
        {
            InitializeComponent();
            TextAlignment = ContentAlignment.MiddleLeft;
            TextAlignmentChange += UIGroupBox_TextAlignmentChange;
            SetStyleFlags(true, false);
        }

        private void UIGroupBox_TextAlignmentChange(object sender, ContentAlignment alignment)
        {
            Invalidate();
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(FillColor);
        }

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            if (RectSides == ToolStripStatusLabelBorderSides.None)
            {
                return;
            }

            var rect = new Rectangle(0, TitleTop, Width - 1, Height - _titleTop - 1);
            if (Text.IsValid())
            {
                using var path1 = rect.CreateRoundedRectanglePathWithoutTop(Radius, RadiusSides, RectSize);
                g.DrawPath(GetRectColor(), path1, true, RectSize);
            }
            else
            {
                using var path1 = rect.CreateRoundedRectanglePath(Radius, RadiusSides, RectSize);
                g.DrawPath(GetRectColor(), path1, true, RectSize);
            }
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Size size = TextRenderer.MeasureText(Text, Font);
            g.DrawString(Text, Font, ForeColor, FillColor, new Rectangle(TitleInterval, TitleTop - size.Height / 2, Width - TitleInterval * 2, size.Height), TextAlignment);

            int textLeft = TitleInterval;
            switch (TextAlignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    textLeft = (Width - size.Width) / 2 - 1;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    textLeft = (Width - TitleInterval - size.Width) - 2;
                    break;
            }

            if (RectSides.GetValue(ToolStripStatusLabelBorderSides.Top))
            {
                if (RadiusSides.GetValue(UICornerRadiusSides.LeftTop) && !UIStyles.GlobalRectangle)
                {
                    g.DrawLine(GetRectColor(), Radius / 2 * RectSize, TitleTop, textLeft, TitleTop, true, RectSize);
                }
                else
                {
                    g.DrawLine(GetRectColor(), 0, TitleTop, textLeft, TitleTop, true, RectSize);
                }

                if (RadiusSides.GetValue(UICornerRadiusSides.RightTop) && !UIStyles.GlobalRectangle)
                {
                    g.DrawLine(GetRectColor(), textLeft + size.Width, TitleTop, Width - Radius / 2 * RectSize, TitleTop, true, RectSize);
                }
                else
                {
                    g.DrawLine(GetRectColor(), textLeft + size.Width, TitleTop, Width, TitleTop, true, RectSize);
                }
            }
        }

        private int _titleTop = 16;

        [DefaultValue(16)]
        [Description("标题高度"), Category("SunnyUI")]
        public int TitleTop
        {
            get => _titleTop;
            set
            {
                if (_titleTop != value)
                {
                    _titleTop = value;
                    Padding = new Padding(Padding.Left, Math.Max(value + 16, Padding.Top), Padding.Right, Padding.Bottom);
                    Invalidate();
                }
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            if (Padding.Top != Math.Max(TitleTop + 16, Padding.Top))
            {
                Padding = new Padding(Padding.Left, Math.Max(TitleTop + 16, Padding.Top), Padding.Right, Padding.Bottom);
            }
        }

        private int _titleInterval = 10;

        [DefaultValue(10)]
        [Description("标题显示间隔"), Category("SunnyUI")]
        public int TitleInterval
        {
            get => _titleInterval;
            set
            {
                if (_titleInterval != value)
                {
                    _titleInterval = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(HorizontalAlignment.Left)]
        [Description("文字显示位置"), Category("SunnyUI")]
        [Browsable(false)]
        public HorizontalAlignment TitleAlignment { get; set; }
    }
}