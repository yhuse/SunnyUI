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
 * 文件名称: UIProgressIndicator.cs
 * 文件说明: 进度指示器
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIProgressIndicator : UIControl
    {
        private readonly Timer timer = new Timer();

        public UIProgressIndicator()
        {
            Width = Height = 100;

            timer.Interval = 200;
            timer.Tick += timer_Tick;
            timer.Start();

            ShowText = false;
            ShowRect = false;

            foreColor = UIColor.Blue;
        }

        ~UIProgressIndicator()
        {
            timer.Stop();
            timer.Dispose();
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            foreColor = uiColor.ProgressIndicatorColor;
            ClearImage();
            Invalidate();
        }

        private int Index;

        private Image image;

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            int circleSize = Math.Min(Width, Height).Div(6);

            if (image == null)
            {
                image = new Bitmap(Width, Height);
                Graphics ig = image.Graphics();
                for (int i = 0; i < 8; i++)
                {
                    Point pt = GetPoint(i, circleSize);
                    ig.FillEllipse(Color.FromArgb(192, foreColor), pt.X, pt.Y, circleSize, circleSize);
                }

                ig.Dispose();
            }

            g.DrawImage(image, 0, 0);

            int idx = Index.Mod(8);
            g.FillEllipse(foreColor, GetPoint(idx, circleSize).X, GetPoint(idx, circleSize).Y, circleSize, circleSize);

            idx = (Index + 8 - 1).Mod(8);
            g.FillEllipse(Color.FromArgb(224, foreColor), GetPoint(idx, circleSize).X, GetPoint(idx, circleSize).Y, circleSize, circleSize);
        }

        private Point GetPoint(int index, int circleSize)
        {
            int len = Math.Min(Width, Height) / 2 - 2 - circleSize;
            int lenX = (int)(len * 0.707);
            int centerX = Width / 2 - circleSize / 2;
            int centerY = Height / 2 - circleSize / 2;

            switch (index)
            {
                case 0: return new Point(centerX, centerY - len);
                case 1: return new Point(centerX + lenX, centerY - lenX);
                case 2: return new Point(centerX + len, centerY);
                case 3: return new Point(centerX + lenX, centerY + lenX);
                case 4: return new Point(centerX, centerY + len);
                case 5: return new Point(centerX - lenX, centerY + lenX);
                case 6: return new Point(centerX - len, centerY);
                case 7: return new Point(centerX - lenX, centerY - lenX);
            }

            return new Point(centerX, centerY);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            Index++;
        }

        private void ClearImage()
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ClearImage();
            Invalidate();
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
    }
}