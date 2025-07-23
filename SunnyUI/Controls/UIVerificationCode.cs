/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 2023-05-16: V3.3.6 重构DrawString函数
 * 2022-05-28: V3.3.7 修改字体缩放时显示
 * 2024-12-12: V3.8.0 可以自定义颜色 #IBABW1
 * 2025-05-30: V3.8.4 修复验证码字符相同 #ICBL2X
 * 2025-07-23: V3.8.6 双击修改验证码，验证码采用Base32字母表，更容易识别
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxItem(true)]
    [Description("验证码控件")]
    public class UIVerificationCode : UIControl
    {
        public UIVerificationCode()
        {
            SetStyleFlags();
            fillColor = UIStyles.Blue.PlainColor;
            foreColor = UIStyles.Blue.RectColor;
            Width = 100;
            Height = 35;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bmp?.Dispose();
                _bmp = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = uiColor.PlainColor;
            foreColor = uiColor.RectColor;
        }

        /// <summary>
        /// 双击更新验证码
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            _bmp?.Dispose();
            _bmp = null;

            Invalidate();
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

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            base.OnPaintFill(g, path);

            _bmp ??= CreateImage(RandomChars(CodeLength));
            g.DrawImage(_bmp, Width / 2 - _bmp.Width / 2, 1);
        }

        private Bitmap _bmp;

        /// <summary>
        /// Base32字母表：该字母表排除了I、 L、O、U字母，目的是避免混淆和滥用，ULID规范的字符表
        /// https://www.crockford.com/base32.html
        /// </summary>
        private const string Base32 = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";

        /// <summary>
        /// 生成字母和数字的随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>结果</returns>
        private static string RandomChars(int length = 10)
        {
            return RandomBase(Base32.ToCharArray(), length);
        }

        private static string RandomBase(char[] pattern, int length)
        {
            if (pattern.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }

            string result = "";
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                result += pattern[random.Next(0, pattern.Length)];
            }

            return result;
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            if (Text != "") Text = "";
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
            byte gdiCharSet = UIStyles.GetGdiCharSet(Font.Name);
            using Font font = new Font(Font.Name, CodeFontSize, FontStyle.Bold, GraphicsUnit.Point, gdiCharSet);
            using Font fontex = font.DPIScaleFont(font.Size);
            Code = code;
            Size sf = TextRenderer.MeasureText(code, fontex);
            using Bitmap image = new Bitmap((int)sf.Width + 16, Height - 2);

            //创建画布
            Graphics g = Graphics.FromImage(image);
            Random random = new Random();

            //图片背景色
            g.Clear(fillColor);

            //画图片背景线
            for (int i = 0; i < 5; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(Color.Black, x1, y1, x2, y2, true);
            }

            //画图片的前景噪音点
            for (int i = 0; i < 30; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            using Brush br = new SolidBrush(foreColor);
            g.DrawString(code, fontex, br, image.Width / 2 - sf.Width / 2, image.Height / 2 - sf.Height / 2);
            return TwistImage(image, true, 3, 5);
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
            using Graphics graph = Graphics.FromImage(destBmp);
            using SolidBrush br = new SolidBrush(fillColor);
            graph.FillRectangle(br, 0, 0, destBmp.Width, destBmp.Height);
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
