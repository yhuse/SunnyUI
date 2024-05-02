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
 * 文件名称: UImage.cs
 * 文件说明: 图像扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 形状
    /// </summary>
    public enum UIShape
    {
        /// <summary>
        /// 圆形
        /// </summary>
        Circle,

        /// <summary>
        /// 方形
        /// </summary>
        Square
    }

    /// <summary>
    /// 图像扩展类
    /// </summary>
    public static class ImageEx
    {
        /// <summary>
        /// 获取打开文件对话框所有的图片类型过滤条件
        /// ------
        /// All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|
        /// BMP Files: (*.BMP;*.DIB;*.RLE)|*.BMP;*.DIB;*.RLE|
        /// JPEG Files: (*.JPG;*.JPEG;*.JPE;*.JFIF)|*.JPG;*.JPEG;*.JPE;*.JFIF|
        /// GIF Files: (*.GIF)|*.GIF|
        /// TIFF Files: (*.TIF;*.TIFF)|*.TIF;*.TIFF|
        /// PNG Files: (*.PNG)|*.PNG|
        /// All Files|*.*
        /// ------
        /// </summary>
        /// <returns>图片过滤字符串</returns>
        public static string GetImageFilter()
        {
            StringBuilder allImageExtensions = new StringBuilder();
            string separator = "";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            Dictionary<string, string> images = new Dictionary<string, string>();
            foreach (ImageCodecInfo codec in codecs)
            {
                allImageExtensions.Append(separator);
                allImageExtensions.Append(codec.FilenameExtension);
                separator = ";";
                images.Add(string.Format("{0} Files: ({1})", codec.FormatDescription, codec.FilenameExtension), codec.FilenameExtension);
            }

            StringBuilder sb = new StringBuilder();
            if (allImageExtensions.Length > 0)
            {
                sb.AppendFormat("{0}|{1}", "All Images", allImageExtensions.ToString());
            }

            images.Add("All Files", "*.*");
            foreach (KeyValuePair<string, string> image in images)
            {
                sb.AppendFormat("|{0}|{1}", image.Key, image.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 根据文件名利用内存流生成图片，加载后文件不占用
        /// </summary>
        /// <param name="path">文件名</param>
        /// <returns>图片</returns>
        public static Image FromFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(path);
                    return System.Drawing.Image.FromStream(new MemoryStream(bytes));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置图片不透明度
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="opacity">不透明度</param>
        /// <returns>图片</returns>
        public static Bitmap ChangeOpacity(Image img, float opacity)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            using Graphics graphics = bmp.Graphics();
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = opacity;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            return bmp;
        }

        /// <summary>
        /// 获取工具栏图片列表
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="imageSize">单图大小</param>
        /// <param name="transparentColor">透明度</param>
        /// <returns>图片列表</returns>
        public static ImageList GetToolbarImageList(Image bitmap, Size imageSize, Color transparentColor)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = imageSize;
            imageList.TransparentColor = transparentColor;
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth24Bit;
            return imageList;
        }

        /// <summary>
        /// 图片截取
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="size">大小</param>
        /// <param name="shape">形状</param>
        /// <returns>图片</returns>
        public static Bitmap Split(this Image image, int size, UIShape shape)
        {
            //截图画板
            Bitmap result = new Bitmap(size, size);
            using Graphics g = System.Drawing.Graphics.FromImage(result);
            g.SetHighQuality();

            if (shape == UIShape.Circle)
            {
                //创建截图路径（类似Ps里的路径）
                using GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, size, size);//圆形
                //设置画板的截图路径
                g.SetClip(path);
            }

            if (shape == UIShape.Square)
            {
                using GraphicsPath path = new Rectangle(0, 0, size, size).CreateRoundedRectanglePath(5);//圆形
                //设置画板的截图路径
                g.SetClip(path);
            }

            //对图片进行截图
            g.DrawImage(image, 0, 0);
            return result;
        }

        /// <summary>
        /// 图片截取
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="path">路径</param>
        /// <returns>图片</returns>
        public static Bitmap Split(this Image image, GraphicsPath path)
        {
            //截图画板
            Bitmap result = new Bitmap(image.Width, image.Height);
            Graphics g = System.Drawing.Graphics.FromImage(result);
            g.SetHighQuality();
            //设置画板的截图路径
            g.SetClip(path);
            //对图片进行截图
            g.DrawImage(image, 0, 0);
            //保存截好的图
            g.Dispose();
            path.Dispose();

            return result;
        }

        /// <summary>
        /// 图片的绘图图元
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>绘图图元</returns>
        public static Graphics Graphics(this Image image)
        {
            return System.Drawing.Graphics.FromImage(image);
        }

        /// <summary>
        /// 图像水平翻转
        /// </summary>
        /// <param name="image">原图像</param>
        /// <returns>图像</returns>
        public static Bitmap HorizontalFlip(this Bitmap image)
        {
            try
            {
                var width = image.Width;
                var height = image.Height;
                Graphics g = System.Drawing.Graphics.FromImage(image);
                Rectangle rect = new Rectangle(0, 0, width, height);
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.DrawImage(image, rect);
                return image;
            }
            catch (Exception)
            {
                return image;
            }
        }

        /// <summary>
        /// 图像垂直翻转
        /// </summary>
        /// <param name="image">原图像</param>
        /// <returns>图像</returns>
        public static Bitmap VerticalFlip(this Bitmap image)
        {
            try
            {
                var width = image.Width;
                var height = image.Height;
                Graphics g = System.Drawing.Graphics.FromImage(image);
                Rectangle rect = new Rectangle(0, 0, width, height);
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                g.DrawImage(image, rect);
                return image;
            }
            catch (Exception)
            {
                return image;
            }
        }

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="bmp">图片</param>
        /// <param name="angle">角度</param>
        /// <param name="bkColor">背景色</param>
        /// <returns>图片</returns>
        public static Bitmap Rotate(this Image bmp, float angle, Color bkColor)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            PixelFormat pf = bkColor == Color.Transparent ? PixelFormat.Format32bppArgb : bmp.PixelFormat;

            using Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = System.Drawing.Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 0, 0);
            g.Dispose();

            using GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            RectangleF rct = path.GetBounds(matrix);

            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = System.Drawing.Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();

            return dst;
        }

        /// <summary>
        /// 缩放图像
        /// </summary>
        /// <param name="bmp">原图片</param>
        /// <param name="newW">宽度</param>
        /// <param name="newH">高度</param>
        /// <returns>新图片</returns>
        public static Bitmap ResizeImage(this Image bmp, int newW, int newH)
        {
            if (bmp == null) return null;

            Bitmap b = new Bitmap(newW, newH);
            using Graphics g = System.Drawing.Graphics.FromImage(b);
            // 插值算法的质量
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);

            return b;
        }

        /// <summary>
        /// Serializes the image in an byte array
        /// </summary>
        /// <param name="image">Instance value.</param>
        /// <param name="format">Specifies the format of the image.</param>
        /// <returns>The image serialized as byte array.</returns>
        public static byte[] ToBytes(this Image image, ImageFormat format)
        {
            if (image == null) return null;

            using MemoryStream stream = new MemoryStream();
            image.Save(stream, format ?? image.RawFormat);
            return stream.ToArray();
        }

        /// <summary>
        /// Converts to image.
        /// </summary>
        /// <param name="bytes">The byte array in.</param>
        /// <returns>结果</returns>
        public static Image ToImage(this byte[] bytes)
        {
            using MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        /// <summary>
        /// Gets the bounds of the image in pixels
        /// </summary>
        /// <param name="image">Instance value.</param>
        /// <returns>A rectangle that has the same height and width as given image.</returns>
        public static Rectangle Bounds(this Image image)
        {
            return new Rectangle(0, 0, image.Width, image.Height);
        }

        /// <summary>
        /// Gets the rectangle that surrounds the given point by a specified distance.
        /// </summary>
        /// <param name="p">Instance value.</param>
        /// <param name="distance">Distance that will be used to surround the point.</param>
        /// <returns>Rectangle that surrounds the given point by a specified distance.</returns>
        public static Rectangle Surround(this Point p, int distance)
        {
            return new Rectangle(p.X - distance, p.Y - distance, distance * 2, distance * 2);
        }

        /// <summary>
        /// 	Scales the bitmap to the passed target size without respecting the aspect.
        /// </summary>
        /// <param name = "bitmap">The source bitmap.</param>
        /// <param name = "size">The target size.</param>
        /// <returns>The scaled bitmap</returns>
        /// <example>
        /// 	<code>
        /// 		var bitmap = new Bitmap("image.png");
        /// 		var thumbnail = bitmap.ScaleToSize(100, 100);
        /// 	</code>
        /// </example>
        public static Bitmap ScaleToSize(this Bitmap bitmap, Size size)
        {
            return bitmap.ScaleToSize(size.Width, size.Height);
        }

        /// <summary>
        /// 	Scales the bitmap to the passed target size without respecting the aspect.
        /// </summary>
        /// <param name = "bitmap">The source bitmap.</param>
        /// <param name = "width">The target width.</param>
        /// <param name = "height">The target height.</param>
        /// <returns>The scaled bitmap</returns>
        /// <example>
        /// 	<code>
        /// 		var bitmap = new Bitmap("image.png");
        /// 		var thumbnail = bitmap.ScaleToSize(100, 100);
        /// 	</code>
        /// </example>
        public static Bitmap ScaleToSize(this Bitmap bitmap, int width, int height)
        {
            var scaledBitmap = new Bitmap(width, height);
            using Graphics g = System.Drawing.Graphics.FromImage(scaledBitmap);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bitmap, 0, 0, width, height);
            return scaledBitmap;
        }

        /// <summary>
        /// 从URL获取图像
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Image</returns>
        public static Image GetImageFromUrl(string url)
        {
#pragma warning disable SYSLIB0014 // 类型或成员已过时
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url));
#pragma warning restore SYSLIB0014 // 类型或成员已过时
            req.Method = "GET";
            req.UserAgent = " Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "image/png, image/svg+xml, image/*;q=0.8, */*;q=0.5";
            req.Headers.Add("X-HttpWatch-RID", " 46990-10314");
            req.Headers.Add("Accept-Language", "zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3");

            Image image = null;

            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                if (stream != null)
                {
                    image = Image.FromStream(stream);
                    stream.Dispose();
                }

                resp.Close();
                return image;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == WebExceptionStatus.Timeout)
                {
                    return null;
                }

                return null;
            }
        }

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="angle">角度</param>
        /// <returns>图片</returns>
        public static Bitmap RotateAngle(this Image image, float angle)
        {
            if (image == null)
            {
                return null;
            }

            const double Pi2 = Math.PI / 2.0;

            double oldWidth = image.Width;
            double oldHeight = image.Height;

            double theta = angle * Math.PI / 180.0;
            double locked_theta = theta;

            while (locked_theta < 0.0)
            {
                locked_theta += 2 * Math.PI;
            }

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            if ((locked_theta >= 0.0 && locked_theta < Pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + Pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            double newWidth = adjacentTop + oppositeBottom;
            double newHeight = adjacentBottom + oppositeTop;

            int nWidth = (int)Math.Ceiling(newWidth);
            int nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using Graphics g = System.Drawing.Graphics.FromImage(rotatedBmp);
            Point[] points;

            if (locked_theta >= 0.0 && locked_theta < Pi2)
            {
                points = new[]
                {
                        new Point((int) oppositeBottom, 0),
                        new Point(nWidth, (int) oppositeTop),
                        new Point(0, (int) adjacentBottom)
                };
            }
            else if (locked_theta >= Pi2 && locked_theta < Math.PI)
            {
                points = new[]
                {
                        new Point(nWidth, (int) oppositeTop),
                        new Point((int) adjacentTop, nHeight),
                        new Point((int) oppositeBottom, 0)
                };
            }
            else if (locked_theta >= Math.PI && locked_theta < (Math.PI + Pi2))
            {
                points = new[]
                {
                        new Point((int) adjacentTop, nHeight),
                        new Point(0, (int) adjacentBottom),
                        new Point(nWidth, (int) oppositeTop)
                };
            }
            else
            {
                points = new[]
                {
                        new Point(0, (int) adjacentBottom),
                        new Point((int) oppositeBottom, 0),
                        new Point((int) adjacentTop, nHeight)
                };
            }

            g.DrawImage(image, points);
            return rotatedBmp;
        }

        /// <summary>
        /// 转换Image为Icon
        /// https://www.cnblogs.com/ahdung/p/ConvertToIcon.html
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static Icon ToIcon(this Image image)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream msImg = new MemoryStream(), msIco = new MemoryStream())
            {
                image.Save(msImg, ImageFormat.Png);

                using (var bin = new BinaryWriter(msIco))
                {
                    //写图标头部
                    bin.Write((short)0);           //0-1保留
                    bin.Write((short)1);           //2-3文件类型。1=图标, 2=光标
                    bin.Write((short)1);           //4-5图像数量（图标可以包含多个图像）

                    bin.Write((byte)image.Width);  //6图标宽度
                    bin.Write((byte)image.Height); //7图标高度
                    bin.Write((byte)0);            //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
                    bin.Write((byte)0);            //9保留。必须为0
                    bin.Write((short)0);           //10-11调色板
                    bin.Write((short)32);          //12-13位深
                    bin.Write((int)msImg.Length);  //14-17位图数据大小
                    bin.Write(22);                 //18-21位图数据起始字节

                    //写图像数据
                    bin.Write(msImg.ToArray());

                    bin.Flush();
                    bin.Seek(0, SeekOrigin.Begin);
                    return new Icon(msIco);
                }
            }
        }

        /// <summary>
        /// 从Base64数据绘图
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="base64Image">Base64</param>
        /// <param name="rect">区域</param>
        public static void DrawImageFromBase64(this Graphics g, string base64Image, Rectangle rect)
        {
            using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
            using (var image = Image.FromStream(ms))
            {
                g.DrawImage(image, rect);
                image.Dispose();
            }
        }

        /// <summary>
        /// 绘制透明图片
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="alpha">透明度</param>
        /// <param name="image">图片</param>
        /// <param name="rect">区域</param>
        public static void DrawTransparentImage(this Graphics g, float alpha, Image image, Rectangle rect)
        {
            var colorMatrix = new ColorMatrix { Matrix33 = alpha };
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            g.DrawImage(image, new Rectangle(rect.X, rect.Y, image.Width, image.Height), rect.X, rect.Y, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            imageAttributes.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="bodyColor"></param>
        /// <param name="strokeColor"></param>
        /// <param name="strokeThickness"></param>
        public static void DrawStrokedRectangle(this Graphics g, Rectangle rect, Color bodyColor, Color strokeColor, int strokeThickness = 1)
        {
            using var bodyBrush = new SolidBrush(bodyColor);
            var x = strokeThickness == 1 ? 0 : strokeThickness;
            var y = strokeThickness == 1 ? 0 : strokeThickness;
            var h = strokeThickness == 1 ? 1 : strokeThickness + 1;
            var w = strokeThickness == 1 ? 1 : strokeThickness + 1;
            var newRect = new Rectangle(rect.X + x, rect.Y + y, rect.Width - w, rect.Height - h);
            using var strokePen = new Pen(strokeColor, strokeThickness);
            g.FillRectangle(bodyBrush, newRect);
            g.DrawRectangle(strokePen, newRect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="bodyColor"></param>
        /// <param name="strokeColor"></param>
        /// <param name="strokeThickness"></param>
        public static void DrawStrokedEllipse(this Graphics g, Rectangle rect, Color bodyColor, Color strokeColor, int strokeThickness = 1)
        {
            using var bodyBrush = new SolidBrush(bodyColor);
            var x = strokeThickness == 1 ? 0 : strokeThickness;
            var y = strokeThickness == 1 ? 0 : strokeThickness;
            var h = strokeThickness == 1 ? 1 : strokeThickness + 1;
            var w = strokeThickness == 1 ? 1 : strokeThickness + 1;
            var newRect = new Rectangle(rect.X + x, rect.Y + y, rect.Width - w, rect.Height - h);
            using var strokePen = new Pen(strokeColor, strokeThickness);
            g.FillEllipse(bodyBrush, newRect);
            g.DrawEllipse(strokePen, newRect);
        }

        /// <summary>
        /// 图片转Baase64字符串
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>字符串</returns>
        public static string ToBase64(this Image image)
        {
            using (var toBase64 = new MemoryStream())
            {
                image.Save(toBase64, image.RawFormat);
                image.Dispose();
                return Convert.ToBase64String(toBase64.ToArray());
            }
        }

        /// <summary>
        /// Base64字符串转图片
        /// </summary>
        /// <param name="base64Image">字符串</param>
        /// <returns>图片</returns>
        public static Image ToImage(string base64Image)
        {
            using (var toImage = new System.IO.MemoryStream(Convert.FromBase64String(base64Image)))
            {
                return Image.FromStream(toImage);
            }
        }

        /// <summary>
        /// 图片存为图标
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="size">大小</param>
        /// <returns>图标</returns>
        public static Icon SaveToIcon(this Image img, int size = 16)
        {
            byte[] pngiconheader = new byte[] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            using Bitmap bmp = new Bitmap(img, new Size(size, size));
            byte[] png;
            using (System.IO.MemoryStream fs = new System.IO.MemoryStream())
            {
                bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Position = 0;
                png = fs.ToArray();
            }

            using (System.IO.MemoryStream fs = new System.IO.MemoryStream())
            {
                if (size >= 256) size = 0;
                pngiconheader[6] = (byte)size;
                pngiconheader[7] = (byte)size;
                pngiconheader[14] = (byte)(png.Length & 255);
                pngiconheader[15] = (byte)(png.Length / 256);
                pngiconheader[18] = (byte)(pngiconheader.Length);

                fs.Write(pngiconheader, 0, pngiconheader.Length);
                fs.Write(png, 0, png.Length);
                fs.Position = 0;
                return new Icon(fs);
            }
        }
    }
}