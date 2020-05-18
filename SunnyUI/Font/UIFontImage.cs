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
 * 文件名称: UFontImage.cs
 * 文件说明: 字体图片帮助类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

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
        public static FontImages AwesomeFont;

        /// <summary>
        /// ElegantFont
        /// </summary>
        public static FontImages ElegantFont;

        /// <summary>
        /// 构造函数
        /// </summary>
        static FontImageHelper()
        {
            string font = DirEx.CurrentDir() + "FontAwesome.ttf";
            CreateFontFile(font, "Sunny.UI.Font.FontAwesome.ttf");
            AwesomeFont = new FontImages(DirEx.CurrentDir() + "FontAwesome.ttf");

            font = DirEx.CurrentDir() + "ElegantIcons.ttf";
            CreateFontFile(font, "Sunny.UI.Font.ElegantIcons.ttf");
            ElegantFont = new FontImages(DirEx.CurrentDir() + "ElegantIcons.ttf");
        }

        /// <summary>
        /// 从系统资源中保存字体文件
        /// </summary>
        /// <param name="file">字体文件名</param>
        /// <param name="resource">资源名称</param>
        private static void CreateFontFile(string file, string resource)
        {
            if (!File.Exists(file))
            {
                Stream fontStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
                if (fontStream != null)
                {
                    byte[] buffer = new byte[fontStream.Length];
                    fontStream.Read(buffer, 0, (int)fontStream.Length);
                    fontStream.Close();

                    File.WriteAllBytes(file, buffer);
                }
            }
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

            Label Image = new Label();
            Image.AutoSize = true;
            Image.Font = font;
            Image.Text = char.ConvertFromUtf32(symbol);
            Size ImageSize = Image.PreferredSize;
            Image.Dispose();
            return ImageSize;
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
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color, float left, float top, int xOffset = 0, int yOffSet = 0)
        {
            //字体
            Font font = GetFont(symbol, symbolSize);
            if (font == null)
            {
                return;
            }

            string text = char.ConvertFromUtf32(symbol);
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString(text, font, color, left + xOffset, top + yOffSet);
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
        public static void DrawFontImage(this Graphics graphics, int symbol, int symbolSize, Color color, Rectangle rect, int xOffset = 0, int yOffSet = 0)
        {
            SizeF sf = graphics.GetFontImageSize(symbol, symbolSize);
            graphics.DrawFontImage(symbol, symbolSize, color, rect.Left + (rect.Width - sf.Width) / 2.0f,
                rect.Top + (rect.Height - sf.Height) / 2.0f, xOffset, yOffSet);
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
            Graphics g = image.Graphics();
            SizeF ImageSize = g.GetFontImageSize(symbol, size);
            Font font = GetFont(symbol, size);
            g.DrawString(char.ConvertFromUtf32(symbol), font, color, (size - ImageSize.Width) / 2.0f, (size - ImageSize.Height) / 2.0f);
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
            if (symbol > 0xF000)
                return AwesomeFont.GetFont(symbol, imageSize);
            else
                return ElegantFont.GetFont(symbol, imageSize);
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
        private const int MaxFontSize = 43;

        /// <summary>
        /// 大小
        /// </summary>
        public int IconSize { get; set; } = 128;

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get; set; } = Color.Transparent;

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; } = Color.Black;

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
        public bool Loaded { get; }

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

            Fonts.Clear();
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="iconText">序号</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>图标</returns>
        public Icon GetIcon(int iconText, int imageSize)
        {
            Bitmap image = GetImage(iconText, imageSize);
            return image != null ? ToIcon(image, IconSize) : null;
        }

        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="iconText">图标</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>字体</returns>
        public Font GetFont(int iconText, int imageSize)
        {
            return Fonts[GetFontSize(iconText, imageSize)];
        }

        /// <summary>
        /// 获取字体大小
        /// </summary>
        /// <param name="iconText">图标</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>字体大小</returns>
        public int GetFontSize(int iconText, int imageSize)
        {
            int size = MaxFontSize;
            using (Bitmap bitmap = new Bitmap(48, 48))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int i = 0; i <= (MaxFontSize - MinFontSize); i++)
                {
                    Font font = Fonts[size];
                    SizeF sf = GetIconSize(iconText, graphics, font);

                    if (sf.Width < imageSize && sf.Height < imageSize)
                    {
                        break;
                    }

                    size -= 1;
                }
            }

            return size;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="iconText">图标</param>
        /// <param name="imageSize">图标大小</param>
        /// <returns>图片</returns>
        public Bitmap GetImage(int iconText, int imageSize)
        {
            Font imageFont = Fonts[MinFontSize];
            SizeF textSize = new SizeF(imageSize, imageSize);
            using (Bitmap bitmap = new Bitmap(48, 48))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                int size = MaxFontSize;
                for (int i = 0; i <= MaxFontSize - MinFontSize; i++)
                {
                    Font font = Fonts[size];
                    SizeF sf = GetIconSize(iconText, graphics, font);

                    if (sf.Width < imageSize && sf.Height < imageSize)
                    {
                        imageFont = font;
                        textSize = sf;
                        break;
                    }

                    size -= 1;
                }
            }

            Size iconSize = textSize.ToSize();
            Bitmap srcImage = new Bitmap(iconSize.Width, iconSize.Height);
            using (Graphics graphics = Graphics.FromImage(srcImage))
            {
                string s = char.ConvertFromUtf32(iconText);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawString(s, imageFont, ForeColor, new PointF(0.0f, 0.0f));
            }

            Bitmap result = new Bitmap(imageSize, imageSize);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.DrawImage(srcImage, imageSize / 2.0f - textSize.Width / 2.0f, imageSize / 2.0f - textSize.Height / 2.0f);
            }

            srcImage.Dispose();
            return result;
        }

        private Size GetIconSize(int iconText, Graphics graphics, Font font)
        {
            string text = char.ConvertFromUtf32(iconText);
            return graphics.MeasureString(text, font).ToSize();
        }

        private Icon ToIcon(Bitmap srcBitmap, int size)
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