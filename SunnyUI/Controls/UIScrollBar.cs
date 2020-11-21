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
 * 文件名称: UIScrollBar.cs
 * 文件说明: 滚动条
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
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
    public sealed class UIScrollBar : UIControl
    {
        public UIScrollBar()
        {
            Maximum = 100;
            up_state = value_state = down_state = DrawItemState.None;
            timer.Interval = 150;
            timer.Tick += TimerTick;
            Width = SystemInformation.VerticalScrollBarWidth + 2;
            Height = 300;
            ShowText = false;

            fillColor = UIColor.LightBlue;
            foreColor = UIColor.Blue;
            fillHoverColor = Color.FromArgb(111, 168, 255);
            fillPressColor = Color.FromArgb(74, 131, 229);
        }

        private int scrollValue;
        private int SmallChange = 1;
        private int LargeChange = 10;
        private int maximum = 100;
        private DrawItemState up_state, value_state, down_state;
        private DrawItemState up_state1, value_state1, down_state1;
        private bool dragMove;
        private int dragOffset;
        private int barHeight;
        private double percentValue;
        private readonly Timer timer = new Timer();
        private bool isScrollUp = true;
        private bool largeChange = true;

        public event EventHandler ValueChanged;

        ~UIScrollBar()
        {
            timer.Stop();
            timer.Dispose();
        }

        [DefaultValue(0)]
        [Description("当前值"), Category("SunnyUI")]
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

        [DefaultValue(100)]
        [Description("最大值"), Category("SunnyUI")]
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcValueArea();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (isScrollUp)
                ScrollUp(largeChange);
            else
                ScrollDown(largeChange);
        }

        private Rectangle GetUpRect()
        {
            var rect = new Rectangle(1, 1, Width - 2, 16);
            return rect;
        }

        private void CalcValueArea()
        {
            var centerHeight = GetValueClipRect().Height;

            barHeight = centerHeight / (maximum + 1);
            barHeight = Math.Max(30, barHeight);
            if (maximum == 0) maximum = 1;
            percentValue = ((double)centerHeight - barHeight) / maximum;
        }

        private Rectangle GetValueRect()
        {
            return new Rectangle(Width / 2 - 3, ValueToPos(scrollValue), 6, barHeight);
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
            var rect = new Rectangle(1, Height - 17, Width - 2, 16);
            return rect;
        }

        private Rectangle GetValueClipRect()
        {
            var clip = new Rectangle(1, 17, Width - 2, Height - 17 * 2);
            return clip;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(fillColor);
            DrawUpDownArrow(e.Graphics, up_state, GetUpRect(), true);
            DrawUpDownArrow(e.Graphics, down_state, GetDownRect(), false);
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
                rect = new Rectangle(rect.Left, MousePos - barHeight / 2, rect.Width, barHeight);
                if (rect.Top < 17)
                {
                    rect = new Rectangle(rect.Left, 17, rect.Width, barHeight);
                }

                if (rect.Bottom > Height - 17)
                {
                    rect = new Rectangle(rect.Left, Height - 17 - barHeight, rect.Width, barHeight);
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
            using (var pen = new Pen(clr_arrow, 2))
            {
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
            }

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
            ValueChanged?.Invoke(this, null);
            Invalidate();
        }

        public void ScrollUp(bool large)
        {
            SetValue(scrollValue - (large ? LargeChange : SmallChange));
        }

        public void ScrollDown(bool large)
        {
            SetValue(scrollValue + (large ? LargeChange : SmallChange));
        }

        private void StartScroll(bool up, bool large)
        {
            isScrollUp = up;
            largeChange = large;
            timer.Start();
        }

        private void StopScroll()
        {
            timer.Stop();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            up_state = value_state = down_state = DrawItemState.None;
            IsPress = true;
            var hit = HitState(e.X, e.Y);
            switch (hit)
            {
                case 1:
                    if (Value > 0)
                    {
                        up_state = DrawItemState.Selected;
                        ScrollUp(false);
                        StartScroll(true, false);
                    }
                    break;

                case 2:
                    value_state = DrawItemState.Selected;
                    dragMove = true;
                    dragOffset = GetValueRect().Y - e.Y;
                    break;

                case 3:
                    if (Value < Maximum)
                    {
                        down_state = DrawItemState.Selected;
                        ScrollDown(false);
                        StartScroll(false, false);
                    }
                    break;

                case 4:
                    if (Value > 0)
                    {
                        ScrollUp(true);
                        if (IsPress)
                        {
                            StartScroll(true, true);
                        }
                    }
                    break;

                case 5:
                    if (Value < Maximum)
                    {
                        ScrollDown(false);
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            IsPress = false;
            dragMove = false;
            StopScroll();
            Invalidate();
        }

        private int MousePos;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MousePos = e.Y;
            var hit = HitState(e.X, e.Y);
            up_state = value_state = down_state = DrawItemState.None;
            switch (hit)
            {
                case 1:
                    up_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;

                case 2:
                    value_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;

                case 3:
                    down_state = IsPress ? DrawItemState.Selected : DrawItemState.HotLight;
                    break;
            }

            if (dragMove)
            {
                var value = PosToValue(e.Y + dragOffset);
                SetValue(value);
                return;
            }

            if (StateChange())
            {
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            up_state = down_state = value_state = DrawItemState.None;
            Invalidate();
        }

        private bool StateChange()
        {
            var change = up_state != up_state1 || down_state != down_state1 || value_state != value_state1;
            up_state1 = up_state;
            value_state1 = value_state;
            down_state1 = down_state;
            return change;
        }

        private int HitState(int x, int y)
        {
            var rect_up = GetUpRect();
            var rect_down = GetDownRect();
            var rect_value = GetValueRect();
            var rect_value_up = new Rectangle(0, rect_up.Bottom, Width, rect_value.Top - rect_up.Bottom);
            var rect_value_down = new Rectangle(0, rect_value.Bottom, Width, rect_down.Top - rect_value.Bottom);
            if (rect_up.Contains(x, y))
                return 1;
            else if (rect_down.Contains(x, y))
                return 3;
            else if (rect_value.Contains(x, y) || value_state == DrawItemState.Selected)
                return 2;
            else if (rect_value_up.Contains(x, y))
                return 4;
            else if (rect_value_down.Contains(x, y))
                return 5;
            else
                return -1;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.PlainColor;
            foreColor = uiColor.ScrollBarForeColor;
            fillHoverColor = uiColor.ButtonFillHoverColor;
            fillPressColor = uiColor.ButtonFillPressColor;
            Invalidate();
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
        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        [DefaultValue(typeof(Color), "111, 168, 255")]
        [Description("鼠标移上颜色"), Category("SunnyUI")]
        public Color HoverColor
        {
            get => fillHoverColor;
            set => SetFillHoveColor(value);
        }

        [DefaultValue(typeof(Color), "74, 131, 229")]
        [Description("鼠标按下颜色"), Category("SunnyUI")]
        public Color PressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }
    }
}