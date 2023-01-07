/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UIGroupBox.cs
 * 文件说明: 组框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2022-05-30: V3.1.9 修复Padding设置
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultProperty("Text")]
    public partial class UIGroupBox : UIPanel
    {
        public UIGroupBox()
        {
            InitializeComponent();
            SetStyleFlags(true, false);
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(FillColor);
        }

        /// <summary>
        /// 绘制边框颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            path = new Rectangle(0, TitleTop, Width - 1, Height - _titleTop - 1).CreateRoundedRectanglePath(Radius, RadiusSides);
            base.OnPaintRect(g, path);
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            SizeF sf = g.MeasureString(Text, Font);

            float left = TitleInterval;
            if (TitleAlignment == HorizontalAlignment.Right)
                left = Width - TitleInterval - sf.Width;
            if (TitleAlignment == HorizontalAlignment.Center)
                left = (Width - sf.Width) / 2.0f;

            float top = TitleTop - sf.Height / 2.0f;
            g.FillRectangle(FillColor, left - 2, top, sf.Width + 2, sf.Height);
            g.DrawString(Text, Font, ForeColor, left, top);
        }

        private int _titleTop = 16;

        [DefaultValue(16)]
        [Description("标题高度"), Category("SunnyUI")]
        public int TitleTop
        {
            get => _titleTop;
            set
            {
                if (_titleTop != value)
                {
                    _titleTop = value;
                    Padding = new Padding(Padding.Left, Math.Max(value + 16, Padding.Top), Padding.Right, Padding.Bottom);
                    Invalidate();
                }
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            if (Padding.Top != Math.Max(TitleTop + 16, Padding.Top))
            {
                Padding = new Padding(Padding.Left, Math.Max(TitleTop + 16, Padding.Top), Padding.Right, Padding.Bottom);
            }
        }

        private int _titleInterval = 10;

        [DefaultValue(10)]
        [Description("标题显示间隔"), Category("SunnyUI")]
        public int TitleInterval
        {
            get => _titleInterval;
            set
            {
                if (_titleInterval != value)
                {
                    _titleInterval = value;
                    Invalidate();
                }
            }
        }

        public HorizontalAlignment titleAlignment = HorizontalAlignment.Left;

        [DefaultValue(HorizontalAlignment.Left)]
        [Description("文字显示位置"), Category("SunnyUI")]
        public HorizontalAlignment TitleAlignment
        {
            get => titleAlignment;
            set
            {
                if (titleAlignment != value)
                {
                    titleAlignment = value;
                    Invalidate();
                }
            }
        }
    }
}