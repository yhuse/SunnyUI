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
 * 文件名称: UISignal.cs
 * 文件说明: 信号强度显示
 * 当前版本: V3.1
 * 创建日期: 2021-06-19
 *
 * 2021-06-19: V3.0.4 增加文件说明
 * 2021-06-20: V3.0.4 调整默认显示高度
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Level")]
    [ToolboxItem(true)]
    public class UISignal : UIControl
    {
        public UISignal()
        {
            SetStyleFlags(true, false);
            Width = Height = 35;
            ShowText = ShowRect = false;
        }

        private int lineWidth = 3;

        [DefaultValue(3)]
        [Description("线宽"), Category("SunnyUI")]
        public int LineWidth
        {
            get => lineWidth;
            set
            {
                lineWidth = value;
                Invalidate();
            }
        }

        private int lineInterval = 2;

        [DefaultValue(2)]
        [Description("线间隔"), Category("SunnyUI")]
        public int LineInterval
        {
            get => lineInterval;
            set
            {
                lineInterval = value;
                Invalidate();
            }
        }

        private int lineHeight = 4;

        [DefaultValue(4)]
        [Description("线高"), Category("SunnyUI")]
        public int LineHeight
        {
            get => lineHeight;
            set
            {
                lineHeight = value;
                Invalidate();
            }
        }

        private int level = 5;

        [DefaultValue(5)]
        [Description("线高"), Category("SunnyUI")]
        public int Level
        {
            get => level;
            set
            {
                level = Math.Max(0, value);
                level = Math.Min(5, level);
                Invalidate();
            }
        }

        private Color onColor = Color.FromArgb(80, 160, 255);

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("有信号颜色"), Category("SunnyUI")]
        public Color OnColor
        {
            get => onColor;
            set
            {
                onColor = value;
                Invalidate();
            }
        }

        private Color offColor = Color.Silver;

        [DefaultValue(typeof(Color), "Silver")]
        [Description("无信号颜色"), Category("SunnyUI")]
        public Color OffColor
        {
            get => offColor;
            set
            {
                offColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            //
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int left = (Width - lineWidth * 5 - lineInterval * 4) / 2;
            int top = (Height - lineHeight * 5) / 2;
            int bottom = top + lineHeight * 5;

            for (int i = 1; i <= 5; i++)
            {
                Color color = level >= i ? onColor : offColor;
                top = bottom - lineHeight * i;
                e.Graphics.FillRectangle(color, left, top, lineWidth, lineHeight * i);
                left += lineWidth + lineInterval;
            }
        }
    }
}
