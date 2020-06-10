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
 * 文件名称: UITitlePanel.cs
 * 文件说明: 带标题面板
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
    public partial class UITitlePanel : UIPanel
    {
        private int _titleHeight = 35;

        [Description("面板高度"), Category("自定义")]
        [DefaultValue(35)]
        public int TitleHeight
        {
            get => _titleHeight;
            set
            {
                _titleHeight = value;
                Padding = new Padding(0, value, 0, 0);
                Invalidate();
            }
        }

        public UITitlePanel()
        {
            InitializeComponent();
            ShowText = false;
            foreColor = Color.White;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            titleColor = uiColor.TitleColor;
            foreColor = uiColor.TitleForeColor;
            Invalidate();
        }

        private HorizontalAlignment textAlign = HorizontalAlignment.Center;

        /// <summary>
        /// 文字对齐方向
        /// </summary>
        [DefaultValue(HorizontalAlignment.Center)]
        public HorizontalAlignment TextAlign
        {
            get => textAlign;
            set
            {
                textAlign = value;
                Invalidate();
            }
        }

        private Color titleColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color TitleColor
        {
            get => titleColor;
            set
            {
                titleColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            base.OnPaintFill(g, path);
            //IsRadius为True时，显示左上圆角
            bool RadiusLeftTop = RadiusSides.GetValue(UICornerRadiusSides.LeftTop);
            //IsRadius为True时，显示右上圆角
            bool RadiusRightTop = RadiusSides.GetValue(UICornerRadiusSides.RightTop);
            path = GetTitleFillPath(Radius, TitleHeight, RadiusLeftTop, RadiusRightTop);

            Color color = Enabled ? TitleColor : UIDisableColor.Fill;
            g.FillPath(color, path);

            color = Enabled ? ForeColor : UIFontColor.Regular;
            SizeF sf = g.MeasureString(Text, Font);
            switch (TextAlign)
            {
                case HorizontalAlignment.Left:
                    g.DrawString(Text, Font, color, _titleInterval, (TitleHeight - sf.Height) / 2.0f);
                    break;

                case HorizontalAlignment.Center:
                    g.DrawString(Text, Font, color, (Width - sf.Width) / 2.0f, (TitleHeight - sf.Height) / 2.0f);
                    break;

                case HorizontalAlignment.Right:
                    g.DrawString(Text, Font, color, Width - _titleInterval - sf.Width, (TitleHeight - sf.Height) / 2.0f);
                    break;
            }
        }

        private int _titleInterval = 10;

        [DefaultValue(10)]
        public int TitleInterval
        {
            get => _titleInterval;
            set
            {
                _titleInterval = value;
                Invalidate();
            }
        }

        protected GraphicsPath GetTitleFillPath(int radius, int height, bool cornerLeftTop = true, bool cornerRightTop = true)
        {
            Rectangle rect = ClientRectangle;
            GraphicsPath graphicsPath = new GraphicsPath();
            if (radius > 0 && ShowRadius)
            {
                if (cornerLeftTop)
                    graphicsPath.AddArc(0, 0, radius, radius, 180f, 90f);
                else
                    graphicsPath.AddLine(new Point(0, 1), new Point(0, 0));

                if (cornerRightTop)
                    graphicsPath.AddArc(rect.Width - radius - 1, 0, radius, radius, 270f, 90f);
                else
                    graphicsPath.AddLine(new Point(rect.Width - 1 - 1, 0), new Point(rect.Width - 1, 0));

                graphicsPath.AddLine(new Point(rect.Width - 1, radius), new Point(rect.Width - 1, height));
                graphicsPath.AddLine(new Point(radius, height), new Point(0, height));

                graphicsPath.CloseFigure();
            }
            else
            {
                Point[] points = new Point[] { new Point(0, 0), new Point(rect.Width - 1, 0), new Point(rect.Width - 1, height), new Point(0, height), new Point(0, 0), };
                graphicsPath = points.Path();
            }

            return graphicsPath;
        }
    }
}