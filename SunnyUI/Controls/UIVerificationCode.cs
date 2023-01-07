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
 * 文件名称: UIVerificationCode.cs
 * 文件说明: 验证码控件
 * 当前版本: V3.1
 * 创建日期: 2022-06-11
 *
 * 2022-06-11: V3.1.9 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    public class UIVerificationCode : UIControl
    {
        public UIVerificationCode()
        {
            SetStyleFlags();
            fillColor = UIStyles.Blue.PlainColor;
            Width = 100;
            Height = 35;
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.PlainColor;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Invalidate();
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            base.OnPaintFill(g, path);

            var bmp = CreateImage(RandomEx.RandomChars(CodeLength));
            g.DrawImage(bmp, Width / 2 - bmp.Width / 2, 1);
            bmp.Dispose();
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            if (Text != "") Text = "";
            //base.OnPaintFore(g, path);
        }

        [DefaultValue(4)]
        [Description("验证码长度"), Category("SunnyUI")]
        public int CodeLength { get; set; } = 4;

        [DefaultValue(18)]
        [Description("验证码字体大小"), Category("SunnyUI")]
        public int CodeFontSize { get; set; } = 18;

        [DefaultValue(null)]
        [Description("验证码文字"), Category("SunnyUI")]
        public string Code { get; private set; }

        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="code">验证码表达式</param>
        private Bitmap CreateImage(string code)
        {
            Font font = new Font(Font.Name, CodeFontSize);
            Code = code;
            SizeF sf = GDI.MeasureString(code, font);

            Bitmap image = new Bitmap((int)sf.Width + 16, Height - 2);

            //创建画布
            Graphics g = Graphics.FromImage(image);
            Random random = new Random();

            //图片背景色
            g.Clear(fillColor);

            //画图片背景线
            for (int i = 0; i < 6; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            //画图片的前景噪音点
            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            g.DrawString(code, font, rectColor, image.Width / 2 - sf.Width / 2, image.Height / 2 - sf.Height / 2);
            var imageex = TwistImage(image, true, 5, 5);
            font.Dispose();
            image.Dispose();
            return imageex;
        }

        ///<summary>
        ///正弦曲线Wave扭曲图片
        ///</summary>
        ///<param name="srcBmp">图片路径</param>
        ///<param name="bXDir">如果扭曲则选择为True</param>
        ///<param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        ///<param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        ///<returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(fillColor), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (Math.PI * 2 * (double)j) / dBaseAxisLen : (Math.PI * 2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);
                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
    }
}
