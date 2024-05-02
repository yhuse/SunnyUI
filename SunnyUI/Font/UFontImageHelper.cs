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
 * 文件名称: UFontImage.cs
 * 文件说明: 字体图片帮助类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-21: V2.2.5 调整从资源文件中加载字体，不用另存为文件。
 *                    感谢：麦壳饼 https://gitee.com/maikebing
 * 2021-06-15: V3.0.4 增加FontAwesomeV5的字体图标，重构代码
 * 2021-06-15: V3.3.5 增加FontAwesomeV6的字体图标，重构代码
 * 2023-05-16: V3.3.6 重构DrawFontImage函数
 * 2022-05-17: V3.3.7 修复了一个窗体属性编辑器图标显示不全的问题
 * 2023-10-25: V3.5.1 MaterialIcons字体图标调大3个像素
******************************************************************************/

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace Sunny.UI
{
    /// <summary>
    /// 字体图片帮助类
    /// </summary>
    public static class FontImageHelper
    {
        public static readonly Dictionary<UISymbolType, FontImages> Fonts = new Dictionary<UISymbolType, FontImages>();

        /// <summary>
        /// 构造函数
        /// </summary>
        static FontImageHelper()
        {
            Fonts.Add(UISymbolType.FontAwesomeV4, new FontImages(UISymbolType.FontAwesomeV4, ReadFontFileFromResource("Sunny.UI.Font.FontAwesome.ttf")));
            Fonts.Add(UISymbolType.ElegantIcons, new FontImages(UISymbolType.ElegantIcons, ReadFontFileFromResource("Sunny.UI.Font.ElegantIcons.ttf")));
            Fonts.Add(UISymbolType.FontAwesomeV6Brands, new FontImages(UISymbolType.FontAwesomeV6Brands, ReadFontFileFromResource("Sunny.UI.Font.fa-brands-400.ttf")));
            Fonts.Add(UISymbolType.FontAwesomeV6Regular, new FontImages(UISymbolType.FontAwesomeV6Regular, ReadFontFileFromResource("Sunny.UI.Font.fa-regular-400.ttf")));
            Fonts.Add(UISymbolType.FontAwesomeV6Solid, new FontImages(UISymbolType.FontAwesomeV6Solid, ReadFontFileFromResource("Sunny.UI.Font.fa-solid-900.ttf")));
            Fonts.Add(UISymbolType.MaterialIcons, new FontImages(UISymbolType.MaterialIcons, ReadFontFileFromResource("Sunny.UI.Font.MaterialIcons-Regular.ttf")));
        }

        private static byte[] ReadFontFileFromResource(string name)
        {
            byte[] buffer = null;
            Stream fontStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            if (fontStream != null)
            {
                buffer = new byte[fontStream.Length];
                fontStream.Read(buffer, 0, (int)fontStream.Length);
                fontStream.Close();
            }

            return buffer;
        }

        /// <summary>
        /// 获取字体大小
        /// </summary>
        /// <param name="graphics">GDI绘图</param>
        /// <param name="symbol">字符</param>
        /// <param name="symbolSize">大小</param>
        /// <returns>字体大小</returns>
        internal static SizeF GetFontImageSize(this Graphics graphics, int symbol, int symbolSize)
        {
            Font font = GetFont(symbol, symbolSize);
            if (font == null)
            {
                return new SizeF(0, 0);
            }

            return graphics.MeasureString(char.ConvertFromUtf32(symbol), font);
        }

        private static UISymbolType GetSymbolType(int symbol)
        {
            return (UISymbolType)symbol.Div(100000);
        }

        private static int GetSymbolValue(int symbol)
        {
            return symbol.Mod(100000);
        }

        /// <summary>
        /// 绘制字体图片
        /// </summary>
        /// <param name="graphics">GDI绘图</param>
        /// <param name="symbol">字符</param>
        /// <param name="symbolSize">大小</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="xOffset">左右偏移</param>
        /// <param name="yOffSet">上下偏移</param>
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color,
            RectangleF rect, int xOffset = 0, int yOffSet = 0, int angle = 0)
        {
            SizeF sf = graphics.GetFontImageSize(symbol, symbolSize);
            float left = rect.Left + ((rect.Width - sf.Width) / 2.0f).RoundEx();
            float top = rect.Top + ((rect.Height - sf.Height) / 2.0f).RoundEx();
            //graphics.DrawFontImage(symbol, symbolSize, color, left, top + 1, xOffset, yOffSet);

            // 把画板的原点(默认是左上角)定位移到文字中心
            graphics.TranslateTransform(left + sf.Width / 2, top + sf.Height / 2);
            // 旋转画板
            graphics.RotateTransform(angle);
            // 回退画板x,y轴移动过的距离
            graphics.TranslateTransform(-(left + sf.Width / 2), -(top + sf.Height / 2));

            graphics.DrawFontImage(symbol, symbolSize, color, left, top, xOffset, yOffSet);

            graphics.TranslateTransform(left + sf.Width / 2, top + sf.Height / 2);
            graphics.RotateTransform(-angle);
            graphics.TranslateTransform(-(left + sf.Width / 2), -(top + sf.Height / 2));
        }

        /// <summary>
        /// 绘制字体图片
        /// </summary>
        /// <param name="graphics">GDI绘图</param>
        /// <param name="symbol">字符</param>
        /// <param name="symbolSize">大小</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左</param>
        /// <param name="top">上</param>
        /// <param name="xOffset">左右偏移</param>
        /// <param name="yOffSet">上下偏移</param>
        private static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color,
            float left, float top, int xOffset = 0, int yOffSet = 0)
        {
            Font font = GetFont(symbol, symbolSize);
            if (font == null) return;

            var symbolValue = GetSymbolValue(symbol);
            string text = char.ConvertFromUtf32(symbolValue);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.DrawString(text, font, color, left + xOffset, top + yOffSet);
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.InterpolationMode = InterpolationMode.Default;
        }

        /// <summary>
        /// 创建图片
        /// </summary>
        /// <param name="symbol">字符</param>
        /// <param name="size">大小</param>
        /// <param name="color">颜色</param>
        /// <returns>图片</returns>
        public static Image CreateImage(int symbol, int size, Color color)
        {
            Bitmap image = new Bitmap(size, size);
            using Graphics g = image.Graphics();
            SizeF sf = g.GetFontImageSize(symbol, size);
            g.DrawFontImage(symbol, size, color, (image.Width - sf.Width) / 2.0f, (image.Height - sf.Height) / 2.0f);
            return image;
        }

        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="symbol">字符</param>
        /// <param name="imageSize">大小</param>
        /// <returns>字体</returns>
        public static Font GetFont(int symbol, int imageSize)
        {
            var symbolType = GetSymbolType(symbol);
            var symbolValue = GetSymbolValue(symbol);
            switch (symbolType)
            {
                case UISymbolType.FontAwesomeV4:
                    if (symbol > 0xF000)
                        return Fonts[UISymbolType.FontAwesomeV4].GetFont(symbolValue, imageSize);
                    else
                        return Fonts[UISymbolType.ElegantIcons].GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV6Brands:
                    return Fonts[UISymbolType.FontAwesomeV6Brands].GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV6Regular:
                    return Fonts[UISymbolType.FontAwesomeV6Regular].GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV6Solid:
                    return Fonts[UISymbolType.FontAwesomeV6Solid].GetFont(symbolValue, imageSize);
                case UISymbolType.MaterialIcons:
                    return Fonts[UISymbolType.MaterialIcons].GetFont(symbolValue, imageSize, 3);
                default:
                    return null;
            }
        }
    }
}