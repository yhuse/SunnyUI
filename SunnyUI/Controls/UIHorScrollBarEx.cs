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
 * 文件名称: UIHorScrollBarEx.cs
 * 文件说明: 水平滚动条
 * 当前版本: V2.2
 * 创建日期: 2020-08-29
 *
 * 2020-08-29: V2.2.7 新增水平滚动条
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
            ShowText = false;
            ShowRect = false;
            Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;

            fillColor = UIColor.LightBlue;
            foreColor = UIColor.Blue;
            fillHoverColor = Color.FromArgb(111, 168, 255);
            fillPressColor = Color.FromArgb(74, 131, 229);
        }

        private int maximum = 100;
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value.CheckLowerLimit(2);
                Invalidate();
            }
        }

        public int LargeChange { get; set; } = 10;

        private int thisValue;
        public event EventHandler ValueChanged;

        public int Value
        {
            get => thisValue;
            set
            {
                thisValue = value.CheckLowerLimit(0);
                thisValue = value.CheckUpperLimit(Maximum - BoundsWidth);
                Invalidate();
            }
        }

        private int boundsWidth = 10;
        public int BoundsWidth
        {
            get => boundsWidth;
            set
            {
                boundsWidth = value.CheckLowerLimit(1);
                Invalidate();
            }
        }

        public int LeftButtonPos => 16;

        public int RightButtonPos => Width - 16;

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
            g.FillRoundRectangle(clr, new Rectangle(left, Height / 2 - 3, width, 6), 5);
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


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsPress = true;

            if (inLeftArea)
            {
                int value = (Value - LargeChange).CheckRange(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, null);
            }

            if (inRightArea)
            {
                int value = (Value + LargeChange).CheckRange(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, null);
            }

            if (inCenterArea)
            {
                int x = BoundsWidth * (Width - 32) / Maximum;
                int value = (e.Location.X - x / 2) * maximum / (Width - 32);
                value = value.CheckRange(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, null);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsPress = false;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsPress = false;
            Invalidate();
        }

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
            using (var pen = new Pen(clr_arrow, 2))
            {
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
            }

            g.SetDefaultQuality();
        }

        private bool inLeftArea, inRightArea, inCenterArea;

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
                value = value.CheckRange(0, Maximum - BoundsWidth);
                Value = value;
                ValueChanged?.Invoke(this, null);
            }
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

        [Description("鼠标移上颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "111, 168, 255")]
        public Color HoverColor
        {
            get => fillHoverColor;
            set => SetFillHoveColor(value);
        }

        [Description("鼠标按下颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "74, 131, 229")]
        public Color PressColor
        {
            get => fillPressColor;
            set => SetFillPressColor(value);
        }
    }
}
