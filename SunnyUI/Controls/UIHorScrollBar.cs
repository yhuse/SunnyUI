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
 * 文件名称: UIHorScrollBar.cs
 * 文件说明: 水平滚动条
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-07-18: V2.2.6 新增水平滚动条
 * 2022-03-19: V3.1.1 重构主题配色
 * 2022-11-13: V3.2.8 增加了可设置水平滚动条高度的属性
 * 2024-04-29: V3.6.5 修改Bug
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ToolboxItem(true)]
    public sealed class UIHorScrollBar : UIControl
    {
        public UIHorScrollBar()
        {
            SetStyleFlags(true, false);
            Maximum = 100;
            left_state = value_state = right_state = DrawItemState.None;
            timer = new Timer();
            timer.Interval = 150;
            timer.Tick += TimerTick;
            Width = 300;
            Height = SystemInformation.HorizontalScrollBarHeight + 2;
            ShowText = false;

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();
        }

        private int scrollValue;
        private int SmallChange = 1;
        private int LargeChange = 10;
        private int maximum;
        private DrawItemState left_state, value_state, right_state;
        private DrawItemState left_state1, value_state1, right_state1;
        private bool dragMove;
        private int dragOffset;
        private int barWidth;
        private double percentValue;
        private readonly Timer timer;
        private bool isScrollUp = true;
        private bool largeChange = true;

        public event EventHandler ValueChanged;

        [Description("滚动条当前值"), Category("SunnyUI")]
        [DefaultValue(0)]
        public int Value
        {
            get => scrollValue;
            set
            {
                scrollValue = Math.Min(maximum, value);
                scrollValue = Math.Max(scrollValue, 0);
                Invalidate();
            }
        }

        [Description("滚动条最大值"), Category("SunnyUI")]
        [DefaultValue(100)]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = Math.Max(value, 1);
                SmallChange = value / 50;
                SmallChange = Math.Max(1, SmallChange);

                LargeChange = value / 10;
                LargeChange = Math.Max(1, LargeChange);
                CalcValueArea();
                Invalidate();
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcValueArea();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (isScrollUp)
                ScrollLeft(largeChange);
            else
                ScrollRight(largeChange);
        }

        private Rectangle GetUpRect()
        {
            var rect = new Rectangle(1, 1, 16, Height - 2);
            return rect;
        }

        private void CalcValueArea()
        {
            var centerWidth = GetValueClipRect().Width;
            barWidth = centerWidth / (maximum + 1);
            barWidth = Math.Max(30, barWidth);
            percentValue = ((double)centerWidth - barWidth) / maximum;
        }

        private Rectangle GetValueRect()
        {
            int h = Math.Min(Height - 2, FillHeight);
            return new Rectangle(ValueToPos(scrollValue), Height / 2 - h / 2, barWidth, h);
        }

        private int ValueToPos(int value)
        {
            return (int)(value * percentValue) + 17;
        }

        private int PosToValue(int pos)
        {
            var value = (int)((pos - 17) / percentValue);
            if (value < 0)
                value = 0;
            if (value > maximum)
                value = maximum;
            return value;
        }

        private Rectangle GetDownRect()
        {
            return new Rectangle(Width - 17, 1, 16, Height - 2);
        }

        private Rectangle GetValueClipRect()
        {
            return new Rectangle(17, 1, Width - 17 * 2, Height - 2);
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(fillColor);
            DrawUpDownArrow(e.Graphics, left_state, GetUpRect(), true);
            DrawUpDownArrow(e.Graphics, right_state, GetDownRect(), false);
            DrawValueBar(e.Graphics, value_state);
        }

        private void DrawValueBar(Graphics g, DrawItemState state)
        {
            var rect = GetValueRect();

            Color clr = foreColor;
            if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            {
                clr = fillHoverColor;
            }

            if ((state & DrawItemState.Selected) == DrawItemState.Selected)
            {
                clr = fillPressColor;
            }

            if (dragMove)
            {
                rect = new Rectangle(MousePos - barWidth / 2, rect.Top, barWidth, rect.Height);
                if (rect.Left < 17)
                {
                    rect = new Rectangle(17, rect.Top, barWidth, rect.Height);
                }

                if (rect.Right > Width - 17)
                {
                    rect = new Rectangle(Width - 17 - barWidth, rect.Top, barWidth, rect.Height);
                }
            }

            g.SetHighQuality();
            g.FillRoundRectangle(clr, rect, 5);
            g.SetDefaultQuality();
        }

        private void DrawUpDownArrow(Graphics g, DrawItemState state, Rectangle rect, bool isUp)
        {
            Color clr_arrow = foreColor;

            if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
            {
                clr_arrow = fillHoverColor;
            }

            if ((state & DrawItemState.Selected) == DrawItemState.Selected)
            {
                clr_arrow = fillPressColor;
            }

            g.FillRectangle(fillColor, rect);
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

        public void SetValue(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > maximum)
            {
                value = maximum;
            }

            scrollValue = value;
            ValueChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }

        public void ScrollLeft(bool large)
        {
            SetValue(scrollValue - (large ? LargeChange : SmallChange));
        }

        public void ScrollRight(bool large)
        {
            SetValue(scrollValue + (large ? LargeChange : SmallChange));
        }

        private void StartScroll(bool up, bool large)
        {
            isScrollUp = up;
            largeChange = large;
            //timer.Start();
        }

        private void StopScroll()
        {
            timer.Stop();
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            left_state = value_state = right_state = DrawItemState.None;
            IsPress = true;
            var hit = HitState(e.X, e.Y);
            switch (hit)
            {
                case 1:
                    if (Value > 0)
                    {
                        left_state = DrawItemState.Selected;
                        ScrollLeft(true);
                        StartScroll(true, false);
                    }
                    break;

                case 2:
                    value_state = DrawItemState.Selected;
                    dragMove = true;
                    dragOffset = GetValueRect().X - e.X;
                    break;

                case 3:
                    if (Value < Maximum)
                    {
                        right_state = DrawItemState.Selected;
                        ScrollRight(true);
                        StartScroll(false, false);
                    }
                    break;

                case 4:
                    if (Value > 0)
                    {
                        ScrollLeft(true);
                        if (IsPress)
                        {
                            StartScroll(true, true);
                        }
                    }
                    break;

                case 5:
                    if (Value < Maximum)
                    {
                        ScrollRight(true);
                        if (IsPress)
                        {
                            StartScroll(false, true);
                        }
                    }
                    break;
            }

            if (StateChange())
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 重载鼠标抬起事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsPress = false;
            dragMove = false;
            StopScroll();
            Invalidate();
        }

        private int MousePos;

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            MousePos = e.X;
            var hit = HitState(e.X, e.Y);
            left_state = value_state = right_state = DrawItemState.None;
            switch (hit)
            {
                case 1:
                    left_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;

                case 2:
                    value_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;

                case 3:
                    right_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;
            }

            if (dragMove)
            {
                var value = PosToValue(e.X + dragOffset);
                SetValue(value);
                return;
            }

            if (StateChange())
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 重载鼠标离开事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            left_state = right_state = value_state = DrawItemState.None;
            Invalidate();
        }

        private bool StateChange()
        {
            var change = left_state != left_state1 || right_state != right_state1 || value_state != value_state1;
            left_state1 = left_state;
            value_state1 = value_state;
            right_state1 = right_state;
            return change;
        }

        private int HitState(int x, int y)
        {
            var rect_left = GetUpRect();
            var rect_right = GetDownRect();
            var rect_value = GetValueRect();
            var rect_value_left = new Rectangle(rect_left.Right, 0, rect_value.Left - rect_left.Right, Height);
            var rect_value_right = new Rectangle(rect_value.Left, 0, rect_right.Left - rect_value.Right, Height);
            if (rect_left.Contains(x, y))
                return 1;
            else if (rect_right.Contains(x, y))
                return 3;
            else if (rect_value.Contains(x, y) || value_state == DrawItemState.Selected)
                return 2;
            else if (rect_value_left.Contains(x, y))
                return 4;
            else if (rect_value_right.Contains(x, y))
                return 5;
            else
                return -1;
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