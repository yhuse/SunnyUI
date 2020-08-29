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
 * 文件名称: UILight.cs
 * 文件说明: 提示灯
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public enum UILightState
    {
        On,
        Off,
        Blink
    }

    [ToolboxItem(true)]
    public sealed class UILight : UIControl
    {
        private Timer timer;

        public UILight()
        {
            ShowRect = false;
            ShowText = false;
            Radius = Width = Height = 35;
        }

        private int interval = 500;

        [DefaultValue(500), Description("显示间隔"), Category("SunnyUI")]
        public int Interval
        {
            get => interval;
            set
            {
                interval = Math.Max(100, value);
                interval = Math.Min(interval, 10000);
                if (timer != null)
                {
                    bool isRun = timer.Enabled;
                    timer.Stop();
                    timer.Interval = interval;
                    if (isRun)
                    {
                        timer.Start();
                    }
                }
            }
        }

        private UILightState state = UILightState.On;

        [DefaultValue(UILightState.On)]
        [Description("显示状态"), Category("SunnyUI")]
        public UILightState State
        {
            get => state;
            set
            {
                state = value;
                timer?.Stop();

                if (state == UILightState.On)
                {
                    showColor = onColor;
                }

                if (state == UILightState.Off)
                {
                    showColor = offColor;
                }

                if (state == UILightState.Blink)
                {
                    if (timer == null)
                    {
                        timer = new Timer { Interval = interval };
                        timer.Tick += Timer_Tick;
                    }

                    blinkState = true;
                    showColor = onColor;
                    timer.Start();
                }

                Invalidate();
            }
        }

        private Color showColor = UIColor.Green;
        private bool blinkState;

        private void Timer_Tick(object sender, EventArgs e)
        {
            blinkState = !blinkState;
            showColor = blinkState ? onColor : offColor;
            Invalidate();
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            if (Width != Height)
            {
                Width = Height;
            }

            Color color;
            if (State == UILightState.On)
                color = OnColor;
            else if (State == UILightState.Off)
                color = OffColor;
            else
                color = showColor;

            GraphicsPath CirclePath = new GraphicsPath();
            CirclePath.AddEllipse(2, 2, Width - 4, Height - 4);
            g.Smooth();

            if (ShowCenterColor)
            {
                Color[] surroundColor = new Color[] { color };
                PathGradientBrush gradientBrush = new PathGradientBrush(path);
                gradientBrush.CenterColor = centerColor;
                gradientBrush.SurroundColors = surroundColor;
                g.FillPath(gradientBrush, CirclePath);
                gradientBrush.Dispose();
            }
            else
            {
                g.FillPath(color, CirclePath);
            }

            CirclePath.Dispose();

            if (ShowLightLine)
            {
                int size = (Width - 4) / 5;
                g.DrawArc(centerColor, size, size, Width - size * 2, Height - size * 2, 45, -155);
            }
        }

        private bool showCenterColor = true;

        [DefaultValue(true)]
        [Description("是否显示中心颜色"), Category("SunnyUI")]
        public bool ShowCenterColor
        {
            get => showCenterColor;
            set
            {
                showCenterColor = value;
                Invalidate();
            }
        }

        private bool showLightLine = true;

        [DefaultValue(true)]
        [Description("显示灯光亮线"), Category("SunnyUI")]
        public bool ShowLightLine
        {
            get => showLightLine;
            set
            {
                showLightLine = value;
                Invalidate();
            }
        }

        private Color onColor = UIColor.Green;

        [DefaultValue(typeof(Color), "110, 190, 40")]
        [Description("打开状态颜色"), Category("SunnyUI")]
        public Color OnColor
        {
            get => onColor;
            set
            {
                onColor = value;
                Invalidate();
            }
        }

        private Color centerColor = UIColor.LightGreen;

        [DefaultValue(typeof(Color), "110, 190, 40")]
        [Description("中心颜色"), Category("SunnyUI")]
        public Color CenterColor
        {
            get => centerColor;
            set
            {
                centerColor = value;
                Invalidate();
            }
        }

        private Color offColor = UIColor.Gray;

        [DefaultValue(typeof(Color), "140, 140, 140")]
        [Description("关闭状态颜色"), Category("SunnyUI")]
        public Color OffColor
        {
            get => offColor;
            set
            {
                offColor = value;
                Invalidate();
            }
        }
    }
}