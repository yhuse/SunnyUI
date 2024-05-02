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
 * 文件名称: UBmp.cs
 * 文件说明: 24bit真彩色位图数据结构类
 * 当前版本: V3.1
 * 创建日期: 2021-12-15
 *
 * 2021-12-15: V3.0.9 增加文件说明
******************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// 24bit 真彩色位图文件头部结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct BmpHead
    {
        /// <summary>
        /// 识别位图的类型：‘BM’
        /// </summary>
        public ushort Head { get; set; }

        /// <summary>
        /// 表示的整个文件的大小
        /// </summary>
        public uint FileSize { get; set; }

        /// <summary>
        /// 保留，必须设置为0
        /// </summary>
        public uint Reserved { get; set; }

        /// <summary>
        /// 从文件开始到位图数据开始之间的数据(bitmap data)之间的偏移量, 常数0x36
        /// </summary>
        public uint BitmapDataOffset { get; set; }

        /// <summary>
        /// 位图信息头(Bitmap Info Header)的长度，用来描述位图的颜色、压缩方法等，常数0x28
        /// </summary>
        public uint BitmapHeaderSize { get; set; }

        /// <summary>
        /// 位图的宽度，以象素为单位
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// 位图的高度，以象素为单位
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// 位图的位面数（注：该值将总是1）
        /// </summary>
        public ushort Planes { get; set; }

        /// <summary>
        /// 每个象素的位数，24 - 24bit 真彩色位图
        /// </summary>
        public ushort BitsPerPixel { get; set; }

        /// <summary>
        /// 0 - 不压缩 (使用BI_RGB表示)
        /// </summary>
        public uint Compression { get; set; }

        /// <summary>
        /// 用字节数表示的位图数据的大小。该数必须是4的倍数
        /// </summary>
        public uint BitmapDataSize { get; set; }

        /// <summary>
        /// 用象素/米表示的水平分辨率 0
        /// </summary>
        public uint HResolution { get; set; }

        /// <summary>
        /// 用象素/米表示的垂直分辨率 0
        /// </summary>
        public uint VResolution { get; set; }

        /// <summary>
        /// 位图使用的颜色数。 0
        /// </summary>
        public uint Colors { get; set; }

        /// <summary>
        /// 指定重要的颜色数。 0
        /// </summary>
        public uint ImportantColors { get; set; }

        /// <summary>
        /// 以图片初始化类
        /// </summary>
        /// <param name="bitmap">图片</param>
        public void Init(Bitmap bitmap)
        {
            Head = 0x4D42;
            Width = (uint)bitmap.Width;
            Height = (uint)bitmap.Height;
            BitmapDataOffset = 0x36;
            BitmapHeaderSize = 0x28;
            Planes = 0x01;
            BitsPerPixel = 0x18;

            //这行要注意，每行数据为宽*高*3，再补上宽度除4取余数
            BitmapDataSize = (uint)(bitmap.Width * bitmap.Height * 3 + bitmap.Height * (bitmap.Width % 4));
            FileSize = BitmapDataOffset + BitmapDataSize;
        }

        /// <summary>
        /// 以宽高初始化类
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void Init(int width, int height)
        {
            Head = 0x4D42;
            Width = (uint)width;
            Height = (uint)height;
            BitmapDataOffset = 0x36;
            BitmapHeaderSize = 0x28;
            Planes = 0x01;
            BitsPerPixel = 0x18;

            //这行要注意，每行数据为宽*高*3，再补上宽度除4取余数
            BitmapDataSize = (uint)(width * height * 3 + height * (width % 4));
            FileSize = BitmapDataOffset + BitmapDataSize;
        }
    }

    /// <summary>
    /// 24bit 真彩色位图文件
    /// </summary>
    public class BmpFile
    {
        BmpHead head;

        byte[] data;

        /// <summary>
        /// 慢于 bitmap.Save(XX,ImageFormat.Bmp)，只是为了解释BMP文件数据格式
        /// </summary>
        /// <param name="bitmap"></param>
        public BmpFile(Bitmap bitmap)
        {
            head = new BmpHead();
            head.Init(bitmap);
            data = new byte[head.FileSize];
            Array.Copy(head.ToBytes(), 0, data, 0, (int)head.BitmapDataOffset);

            var sourceArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);

            byte[] tmp = new byte[bitmap.Width * bitmap.Height * 4];
            Marshal.Copy(bitmapData.Scan0, tmp, 0, tmp.Length);
            bitmap.UnlockBits(bitmapData);

            //BMP文件的数据从左下角开始，每行向上。System.Drawing.Bitmap数据是从左上角开始，每行向下
            int idx = (int)head.BitmapDataOffset;
            for (int i = 0; i < bitmap.Height; i++)
            {
                int offset = bitmap.Height - 1 - i;
                offset *= bitmap.Width * 4;

                for (int j = 0; j < bitmap.Width; j++)
                {
                    Array.Copy(tmp, offset + j * 4, Data, idx, 3);
                    idx += 3;
                }

                idx += bitmap.Width % 4;
            }
        }

        /// <summary>
        /// 从图像数据创建Bmp图片
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="bmpData">数据，从左上角开始，为每个点的B、G、R循环</param>
        public BmpFile(int width, int height, byte[] bmpData)
        {
            head = new BmpHead();
            head.Init(width, height);
            data = new byte[head.FileSize];
            Array.Copy(head.ToBytes(), 0, data, 0, (int)head.BitmapDataOffset);
            if (bmpData.Length != width * height * 3) return;

            //BMP文件的数据从左下角开始，每行向上。System.Drawing.Bitmap数据是从左上角开始，每行向下
            int idx = (int)head.BitmapDataOffset;
            for (int i = 0; i < height; i++)
            {
                int offset = height - 1 - i;
                offset *= width * 3;
                Array.Copy(bmpData, offset, data, idx, width * 3);
                idx += width * 3;
            }
        }

        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] Data => data;

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SaveToFile(string fileName)
        {
            File.WriteAllBytes(fileName, data);
        }

        /// <summary>
        /// 图片
        /// </summary>
        /// <returns>图片</returns>
        public Bitmap Bitmap()
        {
            MemoryStream ms = new MemoryStream(data);
            ms.Position = 0;
            return new Bitmap(ms);
        }
    }
}
