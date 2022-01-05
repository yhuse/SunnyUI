/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// 字体图片帮助类
    /// </summary>
    public static class FontImageHelper
    {
        /// <summary>
        /// AwesomeFont
        /// </summary>
        public static readonly FontImages FontAwesomeV4;

        /// <summary>
        /// ElegantFont
        /// </summary>
        public static readonly FontImages ElegantIcons;

        /// <summary>
        /// FontAwesomeV5Brands
        /// </summary>
        public static readonly FontImages FontAwesomeV5Brands;

        /// <summary>
        /// FontAwesomeV5Regular
        /// </summary>
        public static readonly FontImages FontAwesomeV5Regular;

        /// <summary>
        /// FontAwesomeV5Solid
        /// </summary>
        public static readonly FontImages FontAwesomeV5Solid;

        public const int FontAwesomeV4Count = 786;
        public const int ElegantIconsCount = 360;
        public const int FontAwesomeV5RegularCount = 151;
        public const int FontAwesomeV5SolidCount = 1001;
        public const int FontAwesomeV5BrandsCount = 457;

        /// <summary>
        /// 构造函数
        /// </summary>
        static FontImageHelper()
        {
            FontAwesomeV4 = new FontImages(ReadFontFileFromResource("Sunny.UI.Font.FontAwesome.ttf"));
            ElegantIcons = new FontImages(ReadFontFileFromResource("Sunny.UI.Font.ElegantIcons.ttf"));
            FontAwesomeV5Brands = new FontImages(ReadFontFileFromResource("Sunny.UI.Font.fa-brands-400.ttf"));
            FontAwesomeV5Regular = new FontImages(ReadFontFileFromResource("Sunny.UI.Font.fa-regular-400.ttf"));
            FontAwesomeV5Solid = new FontImages(ReadFontFileFromResource("Sunny.UI.Font.fa-solid-900.ttf"));
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
        public static SizeF GetFontImageSize(this Graphics graphics, int symbol, int symbolSize)
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
            RectangleF rect, int xOffset = 0, int yOffSet = 0)
        {
            SizeF sf = graphics.GetFontImageSize(symbol, symbolSize);
            graphics.DrawFontImage(symbol, symbolSize, color, rect.Left + ((rect.Width - sf.Width) / 2.0f).RoundEx(),
                rect.Top + ((rect.Height - sf.Height) / 2.0f).RoundEx(), xOffset, yOffSet);
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
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color,
            float left, float top, int xOffset = 0, int yOffSet = 0)
        {
            //字体
            Font font = GetFont(symbol, symbolSize);
            if (font == null)
            {
                return;
            }

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

            using (Graphics g = image.Graphics())
            {
                SizeF sf = g.GetFontImageSize(symbol, size);
                g.DrawFontImage(symbol, size, color, (image.Width - sf.Width) / 2.0f, (image.Height - sf.Height) / 2.0f);
            }

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
                        return FontAwesomeV4.GetFont(symbolValue, imageSize);
                    else
                        return ElegantIcons.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Brands:
                    return FontAwesomeV5Brands.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Regular:
                    return FontAwesomeV5Regular.GetFont(symbolValue, imageSize);
                case UISymbolType.FontAwesomeV5Solid:
                    return FontAwesomeV5Solid.GetFont(symbolValue, imageSize);
                default:
                    if (symbol > 0xF000)
                        return FontAwesomeV4.GetFont(symbolValue, imageSize);
                    else
                        return ElegantIcons.GetFont(symbolValue, imageSize);
            }
        }
    }

    /// <summary>
    /// 字体图标图片
    /// </summary>
    public class FontImages
    {
        private readonly PrivateFontCollection ImageFont;
        private readonly Dictionary<int, Font> Fonts = new Dictionary<int, Font>();
        private const int MinFontSize = 8;
        private const int MaxFontSize = 88;
        private readonly IntPtr memoryFont = IntPtr.Zero;

        public FontImages(byte[] buffer)
        {
            ImageFont = new PrivateFontCollection();
            memoryFont = Marshal.AllocCoTaskMem(buffer.Length);
            Marshal.Copy(buffer, 0, memoryFont, buffer.Length);
            ImageFont.AddMemoryFont(memoryFont, buffer.Length);
            Loaded = true;
            LoadDictionary();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filename">字体文件名</param>
        public FontImages(string filename)
        {
            if (File.Exists(filename))
            {
                ImageFont = new PrivateFontCollection();
                ImageFont.AddFontFile(filename);
                Loaded = true;
                LoadDictionary();
            }
        }

        /// <summary>
        /// 字体加载完成标志
        /// </summary>
        public bool Loaded
        {
            get;
        }

        private void LoadDictionary()
        {
            int size = MinFontSize;
            for (int i = 0; i <= MaxFontSize - MinFontSize; i++)
            {
                Fonts.Add(size, GetFont(size));
                size += 1;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~FontImages()
        {
            foreach (var font in Fonts.Values)
            {
                font.Dispose();
            }

            if (memoryFont != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(memoryFont);
            }

            Fonts.Clear();
        }

        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="iconText">图标</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>字体</returns>
        public Font GetFont(int iconText, int imageSize)
        {
            int item = GetFontSize(iconText, imageSize);
            if (Fonts.ContainsKey(item))
            {
                return Fonts[GetFontSize(iconText, imageSize)];
            }

            return null;
        }

        /// <summary>
        /// 获取字体大小
        /// </summary>
        /// <param name="symbol">图标</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>字体大小</returns>
        public int GetFontSize(int symbol, int imageSize)
        {
            using (Bitmap bitmap = new Bitmap(48, 48))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                return BinarySearch(graphics, MinFontSize, MaxFontSize, symbol, imageSize);
            }
        }

        public int BinarySearch(Graphics graphics, int low, int high, int symbol, int imageSize)
        {
            int mid = (low + high) / 2;
            Font font = Fonts[mid];
            SizeF sf = GetIconSize(symbol, graphics, font);
            if (low >= high)
            {
                return mid;
            }

            if (sf.Width < imageSize && sf.Height < imageSize)
            {
                return BinarySearch(graphics, mid + 1, high, symbol, imageSize);
            }

            return BinarySearch(graphics, low, mid - 1, symbol, imageSize);
        }

        private Size GetIconSize(int iconText, Graphics graphics, Font font)
        {
            string text = char.ConvertFromUtf32(iconText);
            return graphics.MeasureString(text, font).ToSize();
        }

        public Icon ToIcon(Bitmap srcBitmap, int size)
        {
            if (srcBitmap == null)
            {
                throw new ArgumentNullException(nameof(srcBitmap));
            }

            Icon icon;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new Bitmap(srcBitmap, new Size(size, size)).Save(memoryStream, ImageFormat.Png);
                Stream stream = new MemoryStream();
                BinaryWriter binaryWriter = new BinaryWriter(stream);
                if (stream.Length <= 0L)
                {
                    return null;
                }

                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)0);
                binaryWriter.Write((short)32);
                binaryWriter.Write((int)memoryStream.Length);
                binaryWriter.Write(22);
                binaryWriter.Write(memoryStream.ToArray());
                binaryWriter.Flush();
                binaryWriter.Seek(0, SeekOrigin.Begin);
                icon = new Icon(stream);
                stream.Dispose();
            }

            return icon;
        }

        private Font GetFont(float size)
        {
            return Loaded ? new Font(ImageFont.Families[0], size, FontStyle.Regular, GraphicsUnit.Point) : null;
        }
    }
}