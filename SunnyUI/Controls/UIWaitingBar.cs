using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIWaitingBar : UIControl
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public UIWaitingBar()
        {
            MinimumSize = new Size(70, 23);
            Size = new Size(300, 29);
            ShowText = false;

            PaintOther += UIWaitingBar_PaintOther;

            fillColor = UIColor.LightBlue;
            foreColor = UIColor.Blue;
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void UIWaitingBar_PaintOther(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRoundRectangle(rectColor, (int)dz + 1, 1, sliderWidth, Height - 3, Radius);
        }

        //d是度数，不是弧度
        private double d;

        private double dz;

        private int blockCount = 20;

        [DefaultValue(20)]
        public int BlockCount
        {
            get => blockCount;
            set => blockCount = Math.Max(10, value);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //移动距离要减去滑块本身的宽度
            double dMoveDistance = Width - SliderWidth - 3;
            //需要变化的次数
            double dStep = dMoveDistance / blockCount;
            //每次变化所增加的度数
            double dPer = 180.0 / dStep;

            d += dPer;
            if (d > 360)
            {
                //一个周期是360度
                d = 0;
            }

            //通过公式：弧度=度*π/180，将度数i转为Math.Sin()所需要的弧度数
            dz = dMoveDistance * (1 + Math.Sin((d - 90) * Math.PI / 180)) / 2;

            Invalidate();
        }

        [DefaultValue(200)]
        public int Interval
        {
            get => timer.Interval;
            set
            {
                timer.Stop();
                timer.Interval = Math.Max(50, value);
                timer.Start();
            }
        }

        private int sliderWidth = 60;

        [DefaultValue(60)]
        public int SliderWidth
        {
            get => sliderWidth;
            set
            {
                sliderWidth = value;
                Invalidate();
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.ProcessBarFillColor;
            foreColor = uiColor.ProcessBarForeColor;
            Invalidate();
        }
    }
}