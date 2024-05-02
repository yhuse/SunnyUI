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
 * 文件名称: UIWaitingBar.cs
 * 文件说明: 等待滚动条控件
 * 当前版本: V3.1
 * 创建日期: 2020-07-20
 *
 * 2020-07-20: V2.2.6 新增等待滚动条控件
 * 2022-03-19: V3.1.1 重构主题配色
 * 2024-02-23: V3.6.3 修改可自定义配色
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIWaitingBar : UIControl
    {
        private readonly Timer timer;

        public UIWaitingBar()
        {
            SetStyleFlags(true, false);
            MinimumSize = new Size(70, 23);
            Size = new Size(300, 29);
            ShowText = false;

            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();

            fillColor = UIStyles.Blue.ProcessBarFillColor;
            foreColor = UIStyles.Blue.ProcessBarForeColor;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer?.Stop();
            timer?.Dispose();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRoundRectangle(foreColor, (int)dz + 1, 1, sliderWidth, Height - 3, Radius);
        }

        //d是度数，不是弧度
        private double d;

        private double dz;

        private int blockCount = 20;

        [DefaultValue(20)]
        [Description("显示块个数，此数越大，移动速度越快"), Category("SunnyUI")]
        public int BlockCount
        {
            get => blockCount;
            set => blockCount = Math.Max(10, value);
        }

        public event EventHandler Tick;

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

            Tick?.Invoke(this, e);
        }

        [DefaultValue(200)]
        [Description("移动显示时间间隔"), Category("SunnyUI")]
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
        [Description("滑块宽度"), Category("SunnyUI")]
        public int SliderWidth
        {
            get => sliderWidth;
            set
            {
                sliderWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.ProcessBarFillColor;
            foreColor = uiColor.ProcessBarForeColor;
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("前景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }
    }
}