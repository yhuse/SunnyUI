using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
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

        public UISymbolType SymbolType { get; }

        public FontImages(UISymbolType symbolType, byte[] buffer)
        {
            SymbolType = symbolType;
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
        public FontImages(UISymbolType symbolType, string filename)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException(filename);

            SymbolType = symbolType;
            ImageFont = new PrivateFontCollection();
            ImageFont.AddFontFile(filename);
            Loaded = true;
            LoadDictionary();
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
        public Font GetFont(int iconText, int imageSize, int offset = 0)
        {
            int item = GetFontSize(iconText, imageSize);
            if (Fonts.ContainsKey(item + offset))
                return Fonts[item + offset];
            else if (Fonts.ContainsKey(item))
                return Fonts[item];
            else
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
            using Bitmap bitmap = new Bitmap(48, 48);
            using Graphics graphics = Graphics.FromImage(bitmap);
            return BinarySearch(graphics, MinFontSize, MaxFontSize, symbol, imageSize);
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
