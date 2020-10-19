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
 * 文件名称: UImage.cs
 * 文件说明: 图像扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Sunny.UI
{
    public enum UIShape
    {
        Circle,
        Square
    }

    /// <summary>
    /// 图像扩展类
    /// </summary>
    public static class ImageEx
    {
        public static Bitmap ChangeOpacity(Image img, float opacity)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = bmp.Graphics();
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = opacity;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }

        public static ImageList GetToolbarImageList(Type type, Image bitmap, Size imageSize, Color transparentColor)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = imageSize;
            imageList.TransparentColor = transparentColor;
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth24Bit;
            return imageList;
        }

        public static Bitmap Split(this Image image, int size, UIShape shape)
        {
            //截图画板
            Bitmap result = new Bitmap(size, size);
            Graphics g = System.Drawing.Graphics.FromImage(result);
            //创建截图路径（类似Ps里的路径）
            GraphicsPath path = new GraphicsPath();

            if (shape == UIShape.Circle)
            {
                path.AddEllipse(0, 0, size, size);//圆形
            }

            if (shape == UIShape.Square)
            {
                path.Dispose();
                path = new Rectangle(0, 0, size, size).CreateRoundedRectanglePath(5);//圆形
            }

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

        public static Graphics Graphics(this Image image)
        {
            return System.Drawing.Graphics.FromImage(image);
        }

        /// <summary>
        /// 图像水平翻转
        /// </summary>
        /// <param name="image">原来图像</param>
        /// <returns></returns>
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
        /// <param name="image">原来图像</param>
        /// <returns></returns>
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

        public static bool IsNullOrEmpty(this Color color)
        {
            return color == Color.Empty || color == Color.Transparent;
        }

        public static bool IsValid(this Color color)
        {
            return !color.IsNullOrEmpty();
        }

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g"></param>
        public static void SetHighQuality(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 设置GDI默认值
        /// </summary>
        /// <param name="g"></param>
        public static void SetDefaultQuality(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
            g.CompositingQuality = CompositingQuality.Default;
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

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = System.Drawing.Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 0, 0);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
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

            tmp.Dispose();

            return dst;
        }

        /// <summary>
        /// 设置递进颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="alpha">alpha</param>
        /// <returns>颜色</returns>
        public static Color StepColor(this Color color, int alpha)
        {
            if (alpha == 100)
            {
                return color;
            }

            byte a = color.A;
            byte r = color.R;
            byte g = color.G;
            byte b = color.B;
            float bg;

            int _alpha = Math.Max(alpha, 0);
            double d = (_alpha - 100.0) / 100.0;

            if (d > 100)
            {
                // blend with white
                bg = 255.0F;
                d = 1.0F - d;  // 0 = transparent fg; 1 = opaque fg
            }
            else
            {
                // blend with black
                bg = 0.0F;
                d = 1.0F + d;  // 0 = transparent fg; 1 = opaque fg
            }

            r = (byte)(BlendColor(r, bg, d));
            g = (byte)(BlendColor(g, bg, d));
            b = (byte)(BlendColor(b, bg, d));

            return Color.FromArgb(a, r, g, b);
        }

        private static double BlendColor(double fg, double bg, double alpha)
        {
            double result = bg + (alpha * (fg - bg));
            if (result < 0.0)
            {
                result = 0.0;
            }

            if (result > 255)
            {
                result = 255;
            }

            return result;
        }

        /// <summary>
        /// 文件转换为Byte数组
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>结果</returns>
        public static byte[] FileToBytes(this string filename)
        {
            if (!File.Exists(filename))
            {
                return null;
            }

            return File.ReadAllBytes(filename);
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
            if (bmp == null)
            {
                return null;
            }

            Bitmap b = new Bitmap(newW, newH);
            using (Graphics g = System.Drawing.Graphics.FromImage(b))
            {
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            }

            return b;
        }

        /// <summary>
        /// Byte数组保存为文件
        /// </summary>
        /// <param name="bytes">bytes</param>
        /// <param name="filename">文件名</param>
        public static void ToFile(this byte[] bytes, string filename)
        {
            File.WriteAllBytes(filename, bytes);
        }

        /// <summary>
        /// Serializes the image in an byte array
        /// </summary>
        /// <param name="image">Instance value.</param>
        /// <param name="format">Specifies the format of the image.</param>
        /// <returns>The image serialized as byte array.</returns>
        public static byte[] ToBytes(this Image image, ImageFormat format)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, format ?? image.RawFormat);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Converts to image.
        /// </summary>
        /// <param name="bytes">The byte array in.</param>
        /// <returns>结果</returns>
        public static Image ToImage(this byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
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
            using (Graphics g = System.Drawing.Graphics.FromImage(scaledBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bitmap, 0, 0, width, height);
            }

            return scaledBitmap;
        }

        /// <summary>
        /// 从URL获取图像
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Image</returns>
        public static Image GetImageFromUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url));
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

            using (Graphics g = System.Drawing.Graphics.FromImage(rotatedBmp))
            {
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
            }

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
    }
}