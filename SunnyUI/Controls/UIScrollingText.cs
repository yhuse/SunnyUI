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
 * 文件名称: UIScrollingText.cs
 * 文件说明: 滚动文字
 * 当前版本: V3.1
 * 创建日期: 2020-06-29
 *
 * 2020-06-29: V2.2.6 新增控件
 * 2021-07-16: V3.0.5 增加属性控制开启滚动
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-02-23: V3.3.2 重写滚动逻辑
 * 2023-05-12: V3.3.6 重构DrawString函数
******************************************************************************/

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
        private readonly Timer timer;
        private int XPos = 0;
        private int XPos1 = 0;
        private int interval = 200;
        private int TextWidth = 0;

        public UIScrollingText()
        {
            SetStyleFlags(true, false);
            fillColor = UIStyles.Blue.ScrollingTextFillColor;
            foreColor = UIStyles.Blue.ScrollingTextForeColor;
            Reset();

            timer = new Timer();
            timer.Interval = interval;
            timer.Tick += Timer_Tick;
        }

        [DefaultValue(false), Description("是否滚动"), Category("SunnyUI")]
        public bool Active
        {
            get => timer.Enabled;
            set
            {
                timer.Enabled = value;
                if (!value)
                {
                    Reset();
                }
            }
        }

        [Browsable(false), DefaultValue(false), Description("点击暂停滚动"), Category("SunnyUI")]
        public bool ClickPause
        {
            get; set;
        }

        private void Reset()
        {
            XPos = 0;
            XPos1 = 0;
            Invalidate();
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

        [DefaultValue(200), Description("刷新间隔"), Category("SunnyUI")]
        public int Interval
        {
            get => interval;
            set
            {
                interval = Math.Max(value, 50);
                timer.Stop();
                timer.Interval = interval;
                timer.Start();
            }
        }

        private int offset = 10;

        [DefaultValue(10), Description("偏移量"), Category("SunnyUI")]
        public int Offset
        {
            get => offset;
            set => offset = Math.Max(2, value);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ScrollingType == UIScrollingType.RightToLeft)
            {
                XPos -= Offset;
                if (XPos + TextWidth < 0)
                {
                    XPos = XPos1;
                    XPos -= Offset;
                }
            }

            if (ScrollingType == UIScrollingType.LeftToRight)
            {
                XPos += Offset;
                if (XPos > Width)
                {
                    XPos = XPos1;
                    XPos += Offset;
                }
            }

            Invalidate();
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            Size sf = TextRenderer.MeasureText(Text, Font);
            if (TextWidth != sf.Width)
            {
                XPos = 0;
                TextWidth = sf.Width;
            }

            if (ScrollingType == UIScrollingType.LeftToRight)
            {
                if (XPos + TextWidth > Width && TextWidth < Width - offset)
                {
                    XPos1 = XPos - Width + offset;
                    g.DrawString(Text, Font, ForeColor, new Rectangle(XPos1, 0, Width, Height), ContentAlignment.MiddleLeft);
                }
                else
                {
                    XPos1 = -TextWidth + offset;
                }

                g.DrawString(Text, Font, ForeColor, new Rectangle(XPos, 0, Width, Height), ContentAlignment.MiddleLeft);
            }

            if (ScrollingType == UIScrollingType.RightToLeft)
            {
                if (XPos < 0 && TextWidth < Width - offset)
                {
                    XPos1 = Width + XPos - offset;
                    g.DrawString(Text, Font, ForeColor, new Rectangle(XPos1, 0, Width, Height), ContentAlignment.MiddleLeft);
                }
                else
                {
                    XPos1 = Width - offset;
                }

                g.DrawString(Text, Font, ForeColor, new Rectangle(XPos, 0, Width, Height), ContentAlignment.MiddleLeft);
            }
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            Reset();
            base.OnSizeChanged(e);
        }

        private UIScrollingType scrollingType;

        [DefaultValue(UIScrollingType.RightToLeft), Description("滚动方向"), Category("SunnyUI")]
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.ScrollingTextFillColor;
            foreColor = uiColor.ScrollingTextForeColor;
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

        [DefaultValue(typeof(Color), "244, 244, 244")]
        [Description("不可用时填充颜色"), Category("SunnyUI")]
        public Color FillDisableColor
        {
            get => fillDisableColor;
            set => SetFillDisableColor(value);
        }

        [DefaultValue(typeof(Color), "173, 178, 181")]
        [Description("不可用时边框颜色"), Category("SunnyUI")]
        public Color RectDisableColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        [DefaultValue(typeof(Color), "109, 109, 103")]
        [Description("不可用时字体颜色"), Category("SunnyUI")]
        public Color ForeDisableColor
        {
            get => foreDisableColor;
            set => SetForeDisableColor(value);
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
    }
}