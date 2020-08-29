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
 * 文件名称: UIGroupBox.cs
 * 文件说明: 组框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIGroupBox : UIPanel
    {
        public UIGroupBox()
        {
            InitializeComponent();
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(FillColor);
        }

        protected override void OnPaintRect(Graphics g, GraphicsPath path)
        {
            //IsRadius为True时，显示左上圆角
            bool RadiusLeftTop = RadiusSides.GetValue(UICornerRadiusSides.LeftTop);
            //IsRadius为True时，显示左下圆角
            bool RadiusLeftBottom = RadiusSides.GetValue(UICornerRadiusSides.LeftBottom);
            //IsRadius为True时，显示右上圆角
            bool RadiusRightTop = RadiusSides.GetValue(UICornerRadiusSides.RightTop);
            //IsRadius为True时，显示右下圆角
            bool RadiusRightBottom = RadiusSides.GetValue(UICornerRadiusSides.RightBottom);
            path = new Rectangle(0, TitleTop, Width - 1, Height - _titleTop - 1).CreateRoundedRectanglePath(Radius, RadiusLeftTop, RadiusRightTop, RadiusRightBottom, RadiusLeftBottom);
            base.OnPaintRect(g, path);
        }

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
                    Padding = new Padding(0, value + 16, 0, 0);
                    Invalidate();
                }
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