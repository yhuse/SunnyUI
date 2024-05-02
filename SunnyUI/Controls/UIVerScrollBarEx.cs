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
 * 文件名称: UIVerScrollBarEx.cs
 * 文件说明: 垂直滚动条
 * 当前版本: V3.1
 * 创建日期: 2020-08-29
 *
 * 2020-08-29: V2.2.7 新增垂直滚动条
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-11-03: V3.2.6 增加了可设置垂直滚动条宽度的属性
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIVerScrollBarEx : UIControl
    {
        public UIVerScrollBarEx()
        {
            SetStyleFlags(true, false);
            ShowText = false;
            ShowRect = false;
            Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;

            fillColor = UIStyles.Blue.ScrollBarFillColor;
            foreColor = UIStyles.Blue.ScrollBarForeColor;
            fillHoverColor = UIStyles.Blue.ScrollBarFillHoverColor;
            fillPressColor = UIStyles.Blue.ScrollBarFillPressColor;
        }

        private int fillWidth = 6;

        [DefaultValue(6)]
        public int FillWidth
        {
            get => fillWidth;
            set
            {
                fillWidth = Math.Max(6, value);
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
                thisValue = value.GetUpperLimit(Maximum - BoundsHeight);
                Invalidate();
            }
        }

        private int boundsHeight = 10;
        [DefaultValue(10)]
        public int BoundsHeight
        {
            get => boundsHeight;
            set
            {
                boundsHeight = value.GetLowerLimit(1);
                Invalidate();
            }
        }

        public int LeftButtonPos => 16;

        public int RightButtonPos => Height - 16;

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

            int top = 16 + Value * (Height - 32) / Maximum;
            int height = BoundsHeight * (Height - 32) / Maximum;

            g.SetHighQuality();
            int w = Math.Max(6, fillWidth);
            g.FillRoundRectangle(clr, new Rectangle(Width / 2 - w / 2, top, w, height), 5);
            g.SetDefaultQuality();
        }

        private Rectangle GetUpRect()
        {
            var rect = new Rectangle(1, 1, Width - 2, 16);
            return rect;
        }

        private Rectangle GetDownRect()
        {
            var rect = new Rectangle(1, Height - 17, Width - 2, 16);
            return rect;
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
                int value = (Value - LargeChange).GetLimit(0, Maximum - BoundsHeight);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

            if (inRightArea)
            {
                int value = (Value + LargeChange).GetLimit(0, Maximum - BoundsHeight);
                Value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }

            if (inCenterArea)
            {
                int y = BoundsHeight * (Height - 32) / Maximum;
                int value = (e.Location.Y - y / 2) * maximum / (Height - 32);
                value = value.GetLimit(0, Maximum - BoundsHeight);
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
                pt1 = new Point(Width / 2 - 4, Height - 16 / 2 - 4);
                pt2 = new Point(Width / 2, Height - 16 / 2);
                pt3 = new Point(Width / 2 + 4, Height - 16 / 2 - 4);
            }
            else
            {
                pt1 = new Point(Width / 2 - 4, 16 / 2 + 4 - 1);
                pt2 = new Point(Width / 2, 16 / 2 - 1);
                pt3 = new Point(Width / 2 + 4, 16 / 2 + 4 - 1);
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
            inLeftArea = e.Location.Y < LeftButtonPos;
            inRightArea = e.Location.Y > RightButtonPos;
            inCenterArea = e.Location.Y >= LeftButtonPos && e.Location.Y <= RightButtonPos;

            if (inCenterArea && IsPress)
            {
                int y = BoundsHeight * (Height - 32) / Maximum;
                int value = (e.Location.Y - y / 2) * maximum / (Height - 32);
                value = value.GetLimit(0, Maximum - BoundsHeight);
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
