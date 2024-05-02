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
 * 文件名称: UIHorScrollBarEx.cs
 * 文件说明: 水平滚动条
 * 当前版本: V3.1
 * 创建日期: 2020-08-29
 *
 * 2020-08-29: V2.2.7 新增水平滚动条
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-11-13: V3.2.8 增加了可设置水平滚动条高度的属性
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIHorScrollBarEx : UIControl
    {
        public UIHorScrollBarEx()
        {
            SetStyleFlags(true, false);
            ShowText = false;
            ShowRect = false;
            Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;

            fillColor = UIStyles.Blue.ScrollBarFillColor;
            foreColor = UIStyles.Blue.ScrollBarForeColor;
            fillHoverColor = UIStyles.Blue.ScrollBarFillHoverColor;
            fillPressColor = UIStyles.Blue.ScrollBarFillPressColor;
        }

        private int fillHeight = 6;

        [DefaultValue(6)]
        public int FillHeight
        {
            get => fillHeight;
            set
            {
                fillHeight = Math.Max(6, value);
                Invalidate();
            }
        }

        private int maximum = 100;
        [DefaultValue(100)]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value.GetLowerLimit(2);
                Invalidate();
            }
        }
        [DefaultValue(10)]
        public int LargeChange { get; set; } = 10;

        private int thisValue;
        public event EventHandler ValueChanged;

        [DefaultValue(0)]
        public int Value
        {
            get => thisValue;
            set
            {
                thisValue = value.GetLowerLimit(0);
                thisValue = value.GetUpperLimit(Maximum - BoundsWidth);
                Invalidate();
            }
        }

        private int boundsWidth = 10;
        [DefaultValue(10)]
        public int BoundsWidth
        {
            get => boundsWidth;
            set
            {
                boundsWidth = value.GetLowerLimit(1);
                Invalidate();
            }
        }

        public int LeftButtonPos => 16;

        public int RightButtonPos => Width - 16;

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            base.OnPaintFill(g, path);
            g.Clear(fillColor);

            DrawUpDownArrow(g, true);
            DrawUpDownArrow(g, false);

            DrawValueBar(g);
        }

        private void DrawValueBar(Graphics g)
        {
            Color clr = foreColor;
            if (inCenterArea && IsPress)
            {
                clr = fillPressColor;
            }

            int left = 16 + Value * (Width - 32) / Maximum;
            int width = BoundsWidth * (Width - 32) / Maximum;

            g.SetHighQuality();
            int h = Math.Min(Height, FillHeight);
            g.FillRoundRectangle(clr, new Rectangle(left, Height / 2 - h / 2, width, h), 5);
            g.SetDefaultQuality();
        }

        private Rectangle GetUpRect()
        {
            var rect = new Rectangle(1, 1, 16, Height - 2);
            return rect;
        }

        private Rectangle GetDownRect()
        {
            return new Rectangle(Width - 17, 1, 16, Height - 2);
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;

            if (inLeftArea)
            {
                int value = (Value - LargeChange).GetLimit(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

            if (inRightArea)
            {
                int value = (Value + LargeChange).GetLimit(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

            if (inCenterArea)
            {
                int x = BoundsWidth * (Width - 32) / Maximum;
                int value = (e.Location.X - x / 2) * maximum / (Width - 32);
                value = value.GetLimit(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsPress = false;
            Invalidate();
        }

        /// <summary>
        /// 重载鼠标进入事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        private void DrawUpDownArrow(Graphics g, bool isUp)
        {
            Color clr_arrow = foreColor;
            if ((inLeftArea || inRightArea) && IsPress)
            {
                clr_arrow = fillPressColor;
            }

            g.FillRectangle(fillColor, isUp ? GetUpRect() : GetDownRect());
            g.SetHighQuality();
            using var pen = new Pen(clr_arrow, 2);
            Point pt1, pt2, pt3;
            if (!isUp)
            {
                pt1 = new Point(Width - 16 / 2 - 4, Height / 2 - 4);
                pt2 = new Point(Width - 16 / 2, Height / 2);
                pt3 = new Point(Width - 16 / 2 - 4, Height / 2 + 4);
            }
            else
            {
                pt1 = new Point(16 / 2 + 4 - 1, Height / 2 - 4);
                pt2 = new Point(16 / 2 - 1, Height / 2);
                pt3 = new Point(16 / 2 + 4 - 1, Height / 2 + 4);
            }

            g.DrawLines(pen, new[] { pt1, pt2, pt3 });
            g.SetDefaultQuality();
        }

        private bool inLeftArea, inRightArea, inCenterArea;

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            inLeftArea = e.Location.X < LeftButtonPos;
            inRightArea = e.Location.X > RightButtonPos;
            inCenterArea = e.Location.X >= LeftButtonPos && e.Location.X <= RightButtonPos;

            if (inCenterArea && IsPress)
            {
                int x = BoundsWidth * (Width - 32) / Maximum;
                int value = (e.Location.X - x / 2) * maximum / (Width - 32);
                value = value.GetLimit(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            fillColor = uiColor.ScrollBarFillColor;
            foreColor = uiColor.ScrollBarForeColor;
            fillHoverColor = uiColor.ScrollBarFillHoverColor;
            fillPressColor = uiColor.ScrollBarFillPressColor;
        }


        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        [Description("鼠标移上颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "115, 179, 255")]
        public Color HoverColor
        {
            get => fillHoverColor;
            set => SetFillHoverColor(value);
        }

        [Description("鼠标按下颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "64, 128, 204")]
        public Color PressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }
    }
}
