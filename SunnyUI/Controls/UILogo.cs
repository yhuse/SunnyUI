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
 * 文件名称: UILogo.cs
 * 文件说明: SunnyUI LOGO
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-03-19: V3.1.1 重构主题配色
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

#pragma warning disable 1591

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public sealed class UILogo : UIControl
    {
        [ToolboxItem(true)]
        public UILogo()
        {
            SetStyleFlags(true, false);
            ShowText = ShowRect = false;
            Width = 300;
            Height = 80;

            MinimumSize = MaximumSize = new Size(300, 80);

            foreColor = UIFontColor.Primary;
            fillColor = UIColor.Blue;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.Clear(BackColor);
            g.SetHighQuality();
            int x = 9;
            int y = 9;
            int n1 = 66;
            int n3 = -20;

            g.FillPie(UIColor.Blue, x, y, n1, n1, -30, 60);
            g.FillPie(UIColor.Gray, x, y, n1, n1, 30, 60);
            g.FillPie(UIColor.Red, x, y, n1, n1, 90, 60);
            g.FillPie(UIColor.Orange, x, y, n1, n1, 150, 60);
            g.FillPie(Color.FromArgb(255, 196, 0), x, y, n1, n1, 210, 60);
            g.FillPie(UIColor.Green, x, y, n1, n1, 270, 60);

            g.FillEllipse(BackColor, x - n3, y - n3, n1 + n3 * 2, n1 + n3 * 2);
            g.SetHighQuality();
            int len = 60;
            x = x + 34;
            y = y + 34;
            using Pen pen = new Pen(BackColor, 2);
            g.DrawLine(pen, x, y, x - len, y - len / 2 - 4);
            g.DrawLine(pen, x, y, x - len, y + len / 2 + 4);

            g.DrawLine(pen, x, y, x + len, y - len / 2 - 4);
            g.DrawLine(pen, x, y, x + len, y + len / 2 + 4);
            g.SetDefaultQuality();

            g.DrawLine(pen, x, y, x, y - 60);
            g.DrawLine(pen, x, y, x, y + 60);

            //S
            DrawVerticalLine(g, ForeColor, 88, 22, 6, 17, true);
            DrawVerticalLine(g, ForeColor, 109, 22, 6, 10, true);
            DrawVerticalLine(g, ForeColor, 109, 37, 6, 22);
            DrawVerticalLine(g, ForeColor, 88, 46, 6, 13);
            DarwHorizontalLine(g, ForeColor, 94, 22, 15, 2, -3, true);
            DarwHorizontalLine(g, ForeColor, 94, 37, 15, 2, -3, true);
            DarwHorizontalLine(g, ForeColor, 94, 57, 15, 2, -2);
            //u
            DrawVerticalLine(g, ForeColor, 123, 33, 6, 26);
            DrawVerticalLine(g, ForeColor, 142, 33, 6, 26);
            DarwHorizontalLine(g, ForeColor, 129, 57, 13, 2, -2);
            //n
            DrawVerticalLine(g, ForeColor, 156, 33, 6, 26);
            DrawVerticalLine(g, ForeColor, 175, 33, 6, 26);
            DarwHorizontalLine(g, ForeColor, 162, 33, 13, 2, -3, true);
            //n
            DrawVerticalLine(g, ForeColor, 189, 33, 6, 26);
            DrawVerticalLine(g, ForeColor, 208, 33, 6, 26);
            DarwHorizontalLine(g, ForeColor, 195, 33, 13, 2, -3, true);
            //y
            DrawVerticalLine(g, ForeColor, 222, 33, 6, 26, true);
            DrawVerticalLine(g, ForeColor, 241, 33, 6, 41);
            DarwHorizontalLine(g, ForeColor, 228, 57, 13, 2, -2, true);
            DrawVerticalLine(g, ForeColor, 222, 64, 6, 10);
            DarwHorizontalLine(g, ForeColor, 228, 72, 13, 2, -2);
            //U
            DrawVerticalLine(g, FillColor, 255, 22, 6, 37);
            DrawVerticalLine(g, FillColor, 274, 22, 6, 37);
            DarwHorizontalLine(g, FillColor, 261, 57, 13, 2, -2);
            //I
            DrawVerticalLine(g, FillColor, 288, 22, 6, 37);
        }

        public void SaveLogo(string fileName)
        {
            this.SaveToImage(fileName, ImageFormat.Png);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            foreColor = uiColor.LogoForeColor;
            fillColor = uiColor.LogoFillColor;
        }

        /// <summary>
        /// 画垂直线
        /// </summary>
        private void DrawVerticalLine(Graphics g, Color color, int left, int top, int width, int height, bool showBottomShadow = false)
        {
            g.FillRectangle(color, left, top, width, height);
            g.DrawLine(Color.FromArgb(60, color), left - 1, top - 1, left - 1 + width, top - 1);
            g.DrawLine(color, left - 1, top, left + 1, top);
            g.DrawLine(Color.FromArgb(60, color), left - 2, top - 1, left - 2, top);
            g.DrawLine(Color.FromArgb(60, color), left - 1, top + 1, left - 1, top + height - 1);
            g.DrawLine(Color.FromArgb(130, color), left + width, top, left + width, top + height - 1);
            if (showBottomShadow)
            {
                g.DrawLine(Color.FromArgb(130, color), left, top + height, left + width, top + height);
            }
        }

        /// <summary>
        /// 画水平线
        /// </summary>
        private void DarwHorizontalLine(Graphics g, Color color, int left, int top, int width, int height, int interval = 0, bool showBottomShadow = false)
        {
            g.FillRectangle(color, left, top, width, height);
            g.DrawLine(Color.FromArgb(60, color), left, top - 1, left + width + interval, top - 1);
            if (showBottomShadow)
                g.DrawLine(Color.FromArgb(130, color), left + 1, top + height, left + width, top + height);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
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
    }
}