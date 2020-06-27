using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public class UIScrollingText : UIControl
    {
        private readonly Timer timer = new Timer();
        private int XPos = int.MinValue;
        private int XPos1 = int.MaxValue;
        private int interval = 200;
        private int TextWidth = Int32.MinValue;

        public UIScrollingText()
        {
            fillColor = UIStyles.Blue.PlainColor;
            foreColor = UIStyles.Blue.RectColor;
            Reset();

            timer.Interval = interval;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Reset()
        {
            XPos = int.MinValue;
            XPos1 = int.MaxValue;
        }

        ~UIScrollingText()
        {
            timer.Stop();
        }

        [DefaultValue(200),Description("刷新间隔")]
        public int Interval
        {
            get => interval;
            set
            {
                interval = Math.Max(value,50);
                timer.Stop();
                timer.Interval = interval;
                timer.Start();
            }
        }

        private int offset = 10;

        [DefaultValue(10),Description("偏移量")]
        public int Offset
        {
            get => offset;
            set => offset = Math.Max(2, value);
        } 

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (XPos == Int32.MinValue)
            {
                Invalidate();
            }
            else
            {
                if (ScrollingType == UIScrollingType.RightToLeft)
                {
                    XPos -= Offset;
                    if (XPos + TextWidth < 0)
                    {
                        XPos = XPos1 - Offset;
                        XPos1 = int.MaxValue;
                    }
                }

                if (ScrollingType == UIScrollingType.LeftToRight)
                {
                    XPos += Offset;
                    if (XPos > Width)
                    {
                        XPos = XPos1 + Offset;
                        XPos1 = int.MaxValue;
                    }
                }

                Invalidate();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
            base.OnClick(e);
        }

        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            SizeF sf = g.MeasureString(Text, Font);
            int y = (int)((Height - sf.Height) / 2);

            if (XPos == int.MinValue)
            {
                XPos = (int)((Width - sf.Width) / 2);
                TextWidth = (int)sf.Width;
            }

            g.DrawString(Text, Font, ForeColor, XPos, y);

            if (ScrollingType == UIScrollingType.LeftToRight)
            {
                if (TextWidth <= Width)
                {
                    if (XPos + TextWidth > Width)
                    {
                        XPos1 = XPos - Width;
                        g.DrawString(Text, Font, ForeColor, XPos1, y);
                    }
                }
                else
                {
                    if (XPos > 0)
                    {
                        if (XPos1 == int.MaxValue)
                            XPos1 = Offset - TextWidth;
                        else
                            XPos1 += Offset;

                        g.DrawString(Text, Font, ForeColor, XPos1, y);
                    }
                }
            }

            if (ScrollingType == UIScrollingType.RightToLeft)
            {
                if (TextWidth <= Width)
                {
                    if (XPos < 0)
                    {
                        XPos1 = Width + XPos;
                        g.DrawString(Text, Font, ForeColor, XPos1, y);
                    }
                }
                else
                {
                    if (XPos + TextWidth < Width - Offset)
                    {
                        if (XPos1 == int.MaxValue)
                            XPos1 = Width - Offset;
                        else
                            XPos1 -= Offset;

                        g.DrawString(Text, Font, ForeColor, XPos1, y);
                    }
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Reset();
            base.OnSizeChanged(e);
        }

        private UIScrollingType scrollingType;

        [DefaultValue(UIScrollingType.RightToLeft), Description("滚动方向")]
        public UIScrollingType ScrollingType
        {
            get => scrollingType;
            set
            {
                scrollingType = value;
                Reset();
                Invalidate();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Reset();
            base.OnTextChanged(e);
        }

        public enum UIScrollingType
        {
            RightToLeft,
            LeftToRight
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.PlainColor;
            foreColor = uiColor.RectColor;
            Invalidate();
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        [DefaultValue(typeof(Color), "244, 244, 244")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
        }

        [DefaultValue(typeof(Color), "173, 178, 181")]
        public Color RectDisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        [DefaultValue(typeof(Color), "109, 109, 103")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }
    }
}