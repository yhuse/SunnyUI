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
 * 文件名称: UILight.cs
 * 文件说明: 提示灯
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-06-19: V3.0.4 增加方形显示，优化渐变色
 * 2021-08-07: V3.0.5 默认不显示灯光亮线
 * 2022-05-15: V3.1.8 增加文字显示
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-08-28: V3.4.2 恢复全局矩形设计时圆形灯光效果
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
            SetStyleFlags(true, false);
            ShowRect = false;
            base.ShowText = false;
            Radius = Width = Height = 35;
        }

        private UIShape sharpType = UIShape.Circle;

        /// <summary>
        /// 显示形状：圆形，正方形
        /// </summary>
        [DefaultValue(UIShape.Circle), Description("显示形状：圆形，正方形"), Category("SunnyUI")]
        public UIShape Shape
        {
            get => sharpType;
            set
            {
                if (sharpType != value)
                {
                    sharpType = value;
                    Invalidate();
                }
            }
        }

        private int interval = 500;

        /// <summary>
        /// 是否显示文字
        /// </summary>
        [Description("是否显示文字"), Category("SunnyUI")]
        [DefaultValue(false)]
        public new bool ShowText
        {
            get => base.ShowText;
            set => base.ShowText = value;
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            g.DrawString(Text, Font, ForeColor, ClientRectangle, TextAlign);
        }

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

                if (state == UILightState.Blink)
                {
                    if (timer == null)
                    {
                        timer = new Timer { Interval = interval };
                        timer.Tick += Timer_Tick;
                    }

                    blinkState = true;
                    timer.Start();
                }

                Invalidate();
            }
        }

        private bool blinkState;

        private void Timer_Tick(object sender, EventArgs e)
        {
            blinkState = !blinkState;
            Invalidate();
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            int ShowSize = Math.Min(Width, Height);

            Color color;
            if (State == UILightState.On)
                color = OnColor;
            else if (State == UILightState.Off)
                color = OffColor;
            else
                color = blinkState ? onColor : offColor;

            Color cColor;
            if (State == UILightState.On)
                cColor = OnCenterColor;
            else if (State == UILightState.Off)
                cColor = offCenterColor;
            else
                cColor = blinkState ? OnCenterColor : offCenterColor;


            if (Shape == UIShape.Circle)
            {
                if (Radius != ShowSize) Radius = ShowSize;
                using GraphicsPath CirclePath = new GraphicsPath();
                CirclePath.AddEllipse(2, 2, ShowSize - 4, ShowSize - 4);
                g.Smooth();

                if (ShowCenterColor)
                {
                    Color[] surroundColor = new Color[] { color };
                    using GraphicsPath path1 = ClientRectangle.CreateTrueRoundedRectanglePath(Height);
                    using PathGradientBrush gradientBrush = new PathGradientBrush(path1);
                    gradientBrush.CenterPoint = new PointF(ShowSize / 2.0f, ShowSize / 2.0f);
                    gradientBrush.CenterColor = cColor;
                    gradientBrush.SurroundColors = surroundColor;
                    g.FillPath(gradientBrush, CirclePath);
                }
                else
                {
                    g.FillPath(color, CirclePath);
                }

                if (ShowLightLine)
                {
                    int size = (ShowSize - 4) / 5;
                    g.DrawArc(cColor, size, size, ShowSize - size * 2, ShowSize - size * 2, 45, -155);
                }
            }

            if (Shape == UIShape.Square)
            {
                if (Radius != 0) Radius = 0;
                g.FillRoundRectangle(color, 2, 2, ShowSize - 4, ShowSize - 4, 5);

                if (ShowCenterColor)
                {
                    using GraphicsPath CirclePath = new GraphicsPath();
                    Point[] p = {
                        new Point(3,3),new Point(ShowSize-3,3),
                        new Point(ShowSize-3,ShowSize-3),new Point(3,ShowSize-3)
                    };

                    CirclePath.AddLines(p);
                    g.Smooth();

                    Color[] surroundColor = new Color[] { color };
                    using PathGradientBrush gradientBrush = new PathGradientBrush(path);
                    gradientBrush.CenterPoint = new PointF(ShowSize / 2.0f, ShowSize / 2.0f);
                    gradientBrush.CenterColor = cColor;
                    gradientBrush.SurroundColors = surroundColor;
                    g.FillPath(gradientBrush, CirclePath);
                }
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

        private bool showLightLine;

        [DefaultValue(false)]
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

        private Color centerColor = UIColor.White;

        [DefaultValue(typeof(Color), "White")]
        [Description("中心颜色"), Category("SunnyUI"), Browsable(false)]
        public Color CenterColor
        {
            get => centerColor;
            set
            {
                centerColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("中心颜色"), Category("SunnyUI")]
        public Color OnCenterColor
        {
            get => CenterColor;
            set => CenterColor = value;
        }


        private Color offCenterColor = UIColor.White;

        [DefaultValue(typeof(Color), "White")]
        [Description("中心颜色"), Category("SunnyUI")]
        public Color OffCenterColor
        {
            get => offCenterColor;
            set
            {
                offCenterColor = value;
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