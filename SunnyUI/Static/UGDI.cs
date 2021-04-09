/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UGDI.cs
 * 文件说明: GDI扩展类
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public static class GDIEx
    {
        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="region">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this Point point, Region region)
        {
            return region.IsVisible(point);
        }

        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="points">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this Point point, Point[] points)
        {
            using (GraphicsPath path = points.Path())
            {
                using (Region region = path.Region())
                {
                    return region.IsVisible(point);
                }
            }
        }

        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="points">区域范围</param>
        /// <returns>是否在区域内</returns>
        public static bool InRegion(this PointF point, PointF[] points)
        {
            using (GraphicsPath path = points.Path())
            {
                using (Region region = path.Region())
                {
                    return region.IsVisible(point);
                }
            }
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="points">点集合</param>
        /// <returns>路径</returns>
        public static GraphicsPath Path(this Point[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();
            path.AddPolygon(points);
            return path;
        }

        /// <summary>
        /// 创建路径
        /// </summary>
        /// <param name="points">点集合</param>
        /// <returns>路径</returns>
        public static GraphicsPath Path(this PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();
            path.AddPolygon(points);
            return path;
        }

        /// <summary>
        /// 创建区域
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>区域</returns>
        public static Region Region(this GraphicsPath path)
        {
            Region region = new Region();
            region.MakeEmpty();
            region.Union(path);
            return region;
        }

        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetFormRoundRectRegion(Form form, int rgnRadius)
        {
            if (form != null && form.FormBorderStyle == FormBorderStyle.None)
            {
                int region = Win32.GDI.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
                Win32.User.SetWindowRgn(form.Handle, region, true);
                Win32.GDI.DeleteObject(region);
            }
        }

        public static PointF Center(this Rectangle rect)
        {
            return new PointF(rect.Left + rect.Width / 2.0f, rect.Top + rect.Height / 2.0f);
        }

        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.Left + rect.Width / 2.0f, rect.Top + rect.Height / 2.0f);
        }

        public static Color Alpha(this Color color, int alpha)
        {
            alpha = Math.Max(0, alpha);
            alpha = Math.Min(255, alpha);
            return Color.FromArgb(alpha, color);
        }

        private static Graphics TempGraphics;

        /// <summary>
        /// 提供一个Graphics，常用于需要计算文字大小时
        /// </summary>
        /// <returns>大小</returns>
        public static Graphics Graphics()
        {
            if (TempGraphics == null)
            {
                Bitmap bmp = new Bitmap(1, 1);
                TempGraphics = bmp.Graphics();
            }

            return TempGraphics;
        }

        /// <summary>
        /// 计算文字大小
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <returns>大小</returns>
        public static SizeF MeasureString(this string text, Font font)
        {
            return Graphics().MeasureString(text, font);
        }

        /// <summary>
        /// 九宫切图背景填充，#，http://st233.com/blog.php?id=24
        /// 例如按钮是图片分成九个区域 然后只需要将四角填充到目标区域 其余的拉伸就可以了
        /// </summary>
        /// <param name="g"></param>
        /// <param name="img"></param>
        /// <param name="rect"></param>
        /// <param name="angleSize"></param>
        public static void DrawImageWithNineCut(this Graphics g, Image img, Rectangle rect, int angleSize = 5)
        {
            //填充四个角
            g.DrawImage(img, new Rectangle(rect.X, rect.Y, angleSize, angleSize),
                new Rectangle(0, 0, angleSize, angleSize), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.Right - angleSize, rect.Y, angleSize, angleSize),
                new Rectangle(img.Width - angleSize, 0, angleSize, angleSize), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.X, rect.Bottom - angleSize, angleSize, angleSize),
                new Rectangle(0, img.Height - angleSize, angleSize, angleSize), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.Right - angleSize, rect.Bottom - angleSize, angleSize, angleSize),
                new Rectangle(img.Width - angleSize, img.Height - angleSize, angleSize, angleSize), GraphicsUnit.Pixel);
            //四边
            g.DrawImage(img, new Rectangle(rect.X, rect.Y + angleSize, angleSize, rect.Height - angleSize * 2),
                new Rectangle(0, angleSize, angleSize, img.Height - angleSize * 2), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.X + angleSize, rect.Y, rect.Width - angleSize * 2, angleSize),
                new Rectangle(angleSize, 0, img.Width - angleSize * 2, angleSize), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.Right - angleSize, rect.Y + angleSize, angleSize, rect.Height - angleSize * 2),
                new Rectangle(img.Width - angleSize, angleSize, angleSize, img.Height - angleSize * 2), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(rect.X + angleSize, rect.Bottom - angleSize, rect.Width - angleSize * 2, angleSize),
                new Rectangle(angleSize, img.Height - angleSize, img.Width - angleSize * 2, angleSize), GraphicsUnit.Pixel);
            //中间
            g.DrawImage(img,
                new Rectangle(rect.X + angleSize, rect.Y + angleSize, rect.Width - angleSize * 2, rect.Height - angleSize * 2),
                new Rectangle(angleSize, angleSize, img.Width - angleSize * 2, img.Height - angleSize * 2), GraphicsUnit.Pixel);
        }

        public static void DrawImageWithNineCut(this Graphics g, Image img, int destWidth, int destHeight, int cutLeft, int cutRight, int cutTop, int cutBottom, int iZoom = 1)
        {
            iZoom = Math.Max(1, iZoom);

            //填充四个角
            g.DrawImage(img, new Rectangle(0, 0, cutLeft * iZoom, cutTop * iZoom),
                new Rectangle(0, 0, cutLeft, cutTop), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(destWidth - cutRight * iZoom, 0, cutRight * iZoom, cutTop * iZoom),
                new Rectangle(img.Width - cutRight, 0, cutRight, cutTop), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(0, destHeight - cutBottom * iZoom, cutLeft * iZoom, cutBottom * iZoom),
                new Rectangle(0, img.Height - cutBottom, cutLeft, cutBottom), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(destWidth - cutRight * iZoom, destHeight - cutBottom * iZoom, cutRight * iZoom, cutBottom * iZoom),
                new Rectangle(img.Width - cutRight, img.Height - cutBottom, cutRight, cutBottom), GraphicsUnit.Pixel);

            //四边
            g.DrawImage(img, new Rectangle(cutLeft * iZoom, 0, destWidth - (cutLeft + cutRight) * iZoom, cutTop * iZoom),
                new Rectangle(cutLeft, 0, img.Width - cutLeft - cutRight, cutTop), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(0, cutTop * iZoom, cutLeft * iZoom, destHeight - (cutTop + cutBottom) * iZoom),
                new Rectangle(0, cutTop, cutLeft, img.Height - cutTop - cutBottom), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(destWidth - cutRight * iZoom, cutTop * iZoom, cutRight * iZoom, destHeight - (cutTop + cutBottom) * iZoom),
                new Rectangle(img.Width - cutRight, cutTop, cutRight, img.Height - cutTop - cutBottom), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(cutLeft * iZoom, destHeight - cutBottom * iZoom, destWidth - (cutLeft + cutRight) * iZoom, cutBottom * iZoom),
                new Rectangle(cutLeft, img.Height - cutBottom, img.Width - cutLeft - cutRight, cutBottom), GraphicsUnit.Pixel);

            //中间
            g.DrawImage(img, new Rectangle(cutLeft * iZoom, cutTop * iZoom, destWidth - (cutLeft + cutRight) * iZoom, destHeight - (cutTop + cutBottom) * iZoom),
               new Rectangle(cutLeft, cutTop, img.Width - cutLeft - cutRight, img.Height - cutTop - cutBottom), GraphicsUnit.Pixel);
        }

        public static Color[] GradientColors(Color startColor, Color endColor, int count)
        {
            count = Math.Max(count, 2);
            Bitmap image = new Bitmap(1024, 3);
            Graphics g = image.Graphics();
            Brush br = new LinearGradientBrush(image.Bounds(), startColor, endColor, 0.0F);
            g.FillRectangle(br, image.Bounds());
            br.Dispose();
            g.Dispose();

            Color[] colors = new Color[count];
            colors[0] = startColor;
            colors[count - 1] = endColor;

            if (count > 2)
            {
                FastBitmap fb = new FastBitmap(image);
                fb.Lock();
                for (int i = 1; i < count - 1; i++)
                {
                    colors[i] = fb.GetPixel(image.Width * i / (count - 1), 1);
                }

                fb.Unlock();
                fb.Dispose();
            }

            image.Dispose();
            return colors;
        }

        public static bool InRect(this Point point, Rectangle rect)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        public static bool InRect(this Point point, RectangleF rect)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        public static bool InRect(this PointF point, Rectangle rect)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        public static bool InRect(this PointF point, RectangleF rect)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        public static void Smooth(this Graphics g, bool smooth = true)
        {
            if (smooth)
            {
                g.SetHighQuality();
            }
            else
            {
                g.SetDefaultQuality();
            }
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, int x, int y)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawString(text, font, br, x, y);
            }
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, RectangleF rect, StringFormat format)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawString(text, font, br, rect, format);
            }
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, Point pt)
        {
            g.DrawString(text, font, color, pt.X, pt.Y);
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, float x, float y)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawString(text, font, br, x, y);
            }
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, PointF pt)
        {
            g.DrawString(text, font, color, pt.X, pt.Y);
        }

        public static void DrawLines(this Graphics g, Color color, Point[] points, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLines(pen, points);
                g.Smooth(false);
            }
        }

        public static void DrawLines(this Graphics g, Color color, PointF[] points, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLines(pen, points);
                g.Smooth(false);
            }
        }

        public static void DrawCurve(this Graphics g, Color color, Point[] points, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawCurve(pen, points);
                g.Smooth(false);
            }
        }

        public static void DrawCurve(this Graphics g, Color color, PointF[] points, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawCurve(pen, points);
                g.Smooth(false);
            }
        }

        public static void DrawLine(this Graphics g, Color color, int x1, int y1, int x2, int y2, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLine(pen, x1, y1, x2, y2);
                g.Smooth(false);
            }
        }

        public static void DrawLine(this Graphics g, Color color, Point pt1, Point pt2, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLine(pen, pt1, pt2);
                g.Smooth(false);
            }
        }

        public static void DrawLine(this Graphics g, Color color, float x1, float y1, float x2, float y2, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLine(pen, x1, y1, x2, y2);
                g.Smooth(false);
            }
        }

        public static void DrawLine(this Graphics g, Color color, PointF pt1, PointF pt2, bool smooth = false, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawLine(pen, pt1, pt2);
                g.Smooth(false);
            }
        }

        public static void DrawArc(this Graphics g, Color color, int x, int y, int width, int height, int startAngle, int sweepAngle, bool smooth = true, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
                g.Smooth(false);
            }
        }

        public static void DrawArc(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            using (Pen pen = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
                g.Smooth(false);
            }
        }

        public static void FillPath(this Graphics g, Color color, GraphicsPath path, bool smooth = true)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                g.Smooth(smooth);
                g.FillPath(sb, path);
                g.Smooth(false);
            }
        }

        public static void DrawPath(this Graphics g, Color color, GraphicsPath path, bool smooth = true, float penWidth = 1)
        {
            using (Pen pn = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawPath(pn, path);
                g.Smooth(false);
            }
        }

        public static void FillEllipse(this Graphics g, Color color, Rectangle rect, bool smooth = true)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                g.Smooth(smooth);
                g.FillEllipse(sb, rect);
                g.Smooth(false);
            }
        }

        public static void DrawEllipse(this Graphics g, Color color, Rectangle rect, bool smooth = true, float penWidth = 1)
        {
            using (Pen pn = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawEllipse(pn, rect);
                g.Smooth(false);
            }
        }

        public static void FillEllipse(this Graphics g, Color color, RectangleF rect, bool smooth = true)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                g.Smooth(smooth);
                g.FillEllipse(sb, rect);
                g.Smooth(false);
            }
        }

        public static void DrawEllipse(this Graphics g, Color color, RectangleF rect, bool smooth = true, float penWidth = 1)
        {
            using (Pen pn = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawEllipse(pn, rect);
                g.Smooth(false);
            }
        }

        public static void FillEllipse(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = true)
        {
            g.FillEllipse(color, new Rectangle(left, top, width, height), smooth);
        }

        public static void DrawEllipse(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = true)
        {
            g.DrawEllipse(color, new Rectangle(left, top, width, height), smooth);
        }

        public static void FillEllipse(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = true)
        {
            g.FillEllipse(color, new RectangleF(left, top, width, height), smooth);
        }

        public static void DrawEllipse(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = true)
        {
            g.DrawEllipse(color, new RectangleF(left, top, width, height), smooth);
        }

        public static void FillRectangle(this Graphics g, Color color, Rectangle rect, bool smooth = false)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                g.Smooth(smooth);
                g.FillRectangle(sb, rect);
                g.Smooth(false);
            }
        }

        public static void DrawRectangle(this Graphics g, Color color, Rectangle rect, bool smooth = false, float penWidth = 1)
        {
            using (Pen pn = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawRectangle(pn, rect);
                g.Smooth(false);
            }
        }

        public static void FillRectangle(this Graphics g, Color color, RectangleF rect, bool smooth = false)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                g.Smooth(smooth);
                g.FillRectangle(sb, rect);
                g.Smooth(false);
            }
        }

        public static void DrawRectangle(this Graphics g, Color color, RectangleF rect, bool smooth = false, float penWidth = 1)
        {
            using (Pen pn = new Pen(color, penWidth))
            {
                g.Smooth(smooth);
                g.DrawRectangle(pn, rect.X, rect.Y, rect.Width, rect.Height);
                g.Smooth(false);
            }
        }

        public static void FillRectangle(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = false)
        {
            g.FillRectangle(color, new Rectangle(left, top, width, height), smooth);
        }

        public static void DrawRectangle(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = false)
        {
            g.DrawRectangle(color, new Rectangle(left, top, width, height), smooth);
        }

        public static void FillRectangle(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = false)
        {
            g.FillRectangle(color, new RectangleF(left, top, width, height), smooth);
        }

        public static void DrawRectangle(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = false)
        {
            g.DrawRectangle(color, new RectangleF(left, top, width, height), smooth);
        }

        public static void DrawRoundRectangle(this Graphics g, Pen pen, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            if (cornerRadius > 0)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.Smooth(smooth);
                    g.DrawPath(pen, path);
                    g.Smooth(false);
                }
            }
            else
            {
                g.Smooth(smooth);
                g.DrawRectangle(pen, rect);
                g.Smooth(false);
            }
        }

        public static void FillRoundRectangle(this Graphics g, Brush brush, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            if (cornerRadius > 0)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.Smooth(smooth);
                    g.FillPath(brush, path);
                    g.Smooth(false);
                }
            }
            else
            {
                g.Smooth(smooth);
                g.FillRectangle(brush, rect);
                g.Smooth(false);
            }
        }

        public static void DrawRoundRectangle(this Graphics g, Pen pen, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.DrawRoundRectangle(pen, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        public static void FillRoundRectangle(this Graphics g, Brush brush, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.FillRoundRectangle(brush, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        public static void DrawRoundRectangle(this Graphics g, Color color, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            if (cornerRadius > 0)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.DrawPath(color, path, smooth);
                }
            }
            else
            {
                g.DrawRectangle(color, rect, smooth);
            }
        }

        public static void FillRoundRectangle(this Graphics g, Color color, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            if (cornerRadius > 0)
            {
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
                {
                    g.FillPath(color, path, smooth);
                }
            }
            else
            {
                g.FillRectangle(color, rect, smooth);
            }
        }

        public static void DrawRoundRectangle(this Graphics g, Color color, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.DrawRoundRectangle(color, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        public static void FillRoundRectangle(this Graphics g, Color color, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.FillRoundRectangle(color, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        public static GraphicsPath CreateRoundedRectanglePath(this Graphics g, Rectangle rect, int radius, UICornerRadiusSides radiusSides)
        {
            return rect.CreateRoundedRectanglePath(radius, radiusSides);
        }

        public static GraphicsPath CreateRoundedRectanglePath(this Graphics g, Rectangle rect, int radius, bool cornerLeftTop = true, bool cornerRightTop = true, bool cornerRightBottom = true, bool cornerLeftBottom = true)
        {
            return rect.CreateRoundedRectanglePath(radius, cornerLeftTop, cornerRightTop, cornerRightBottom, cornerLeftBottom);
        }

        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int radius, UICornerRadiusSides radiusSides)
        {
            GraphicsPath path;

            if (radiusSides == UICornerRadiusSides.None || radius == 0)
            {
                Point[] points = new Point[] { new Point(0, 0), new Point(rect.Width, 0), new Point(rect.Width, rect.Height), new Point(0, rect.Height), new Point(0, 0), };
                path = points.Path();
            }
            else
            {
                //IsRadius为True时，显示左上圆角
                bool RadiusLeftTop = radiusSides.GetValue(UICornerRadiusSides.LeftTop);
                //IsRadius为True时，显示左下圆角
                bool RadiusLeftBottom = radiusSides.GetValue(UICornerRadiusSides.LeftBottom);
                //IsRadius为True时，显示右上圆角
                bool RadiusRightTop = radiusSides.GetValue(UICornerRadiusSides.RightTop);
                //IsRadius为True时，显示右下圆角
                bool RadiusRightBottom = radiusSides.GetValue(UICornerRadiusSides.RightBottom);
                path = rect.CreateRoundedRectanglePath(radius, RadiusLeftTop, RadiusRightTop, RadiusRightBottom, RadiusLeftBottom);
            }

            return path;
        }

        public static GraphicsPath CreateFanPath(this Graphics g, Point center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return center.CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        public static GraphicsPath CreateFanPath(this Graphics g, PointF center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return center.CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        public static GraphicsPath CreateFanPath(this Point center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return new PointF(center.X, center.Y).CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        public static GraphicsPath CreateFanPath(this PointF center, float d1, float d2, float startAngle, float sweepAngle)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(center.X - d1, center.Y - d1, d1 * 2, d1 * 2, startAngle, sweepAngle);
            path.AddArc(center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle + sweepAngle, -sweepAngle);
            path.AddArc(center.X - d1, center.Y - d1, d1 * 2, d1 * 2, startAngle, 0.1f);
            return path;
        }

        public static void DrawCross(this Graphics g, Color color, Point center, int size = 3)
        {
            g.DrawLine(color, center.X - size, center.Y, center.X + size, center.Y);
            g.DrawLine(color, center.X, center.Y - size, center.X, center.Y + size);
        }

        public static void DrawFan(this Graphics g, Color color, Point center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.DrawPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle);
            }
            else
            {
                GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.DrawPath(color, path, smooth);
                path.Dispose();
            }
        }

        public static void DrawFan(this Graphics g, Color color, PointF center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.DrawPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle);
            }
            else
            {
                GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.DrawPath(color, path, smooth);
                path.Dispose();
            }
        }

        public static void FillFan(this Graphics g, Color color, Point center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.FillPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle);
            }
            else
            {
                GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.FillPath(color, path, smooth);
                path.Dispose();
            }
        }

        public static void FillFan(this Graphics g, Color color, PointF center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.FillPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle);
            }
            else
            {
                GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.FillPath(color, path, smooth);
                path.Dispose();
            }
        }

        public static void FillPie(this Graphics g, Color color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.Smooth(smooth);
            using (Brush br = new SolidBrush(color))
            {
                g.FillPie(br, x, y, width, height, startAngle, sweepAngle);
            }

            g.Smooth(false);
        }

        public static void FillPie(this Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.FillPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);
        }

        public static void FillPie(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.Smooth(smooth);
            using (Brush br = new SolidBrush(color))
            {
                g.FillPie(br, x, y, width, height, startAngle, sweepAngle);
            }

            g.Smooth(false);
        }

        public static void FillPie(this Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.FillPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);
        }

        public static void DrawPie(this Graphics g, Color color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using (Pen pen = new Pen(color, penWidth))
            {
                g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
            }

            g.Smooth(false);
        }

        public static void DrawPoint(this Graphics g, Color color, int x, int y, float size)
        {
            g.FillEllipse(color, x - size / 2.0f, y - size / 2.0f, size, size);
        }

        public static void DrawPoint(this Graphics g, Color color, float x, float y, float size)
        {
            g.FillEllipse(color, x - size / 2.0f, y - size / 2.0f, size, size);
        }

        public static void DrawPoint(this Graphics g, Color color, Point point, float size)
        {
            g.DrawPoint(color, point.X, point.Y, size);
        }

        public static void DrawPoint(this Graphics g, Color color, PointF point, float size)
        {
            g.DrawPoint(color, point.X, point.Y, size);
        }

        public static void DrawPie(this Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.DrawPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);
        }

        public static void DrawPie(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using (Pen pen = new Pen(color, penWidth))
            {
                g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
            }

            g.Smooth(false);
        }

        public static void DrawPie(this Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.DrawPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);
        }

        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int radius, bool cornerLeftTop = true, bool cornerRightTop = true, bool cornerRightBottom = true, bool cornerLeftBottom = true)
        {
            GraphicsPath path = new GraphicsPath();
            if (cornerLeftTop)
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            else
                path.AddLine(new Point(rect.X, rect.Y + 1), new Point(rect.X, rect.Y));

            if (cornerRightTop)
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            else
                path.AddLine(new Point(rect.X + rect.Width - 1, rect.Y), new Point(rect.X + rect.Width, rect.Y));

            if (cornerRightBottom)
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            else
                path.AddLine(new Point(rect.X + rect.Width, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));

            if (cornerLeftBottom)
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            else
                path.AddLine(new Point(rect.X + 1, rect.Y + rect.Height), new Point(rect.X, rect.Y + rect.Height));

            path.CloseFigure();
            return path;
        }

        public static void DrawString(this Graphics g, string str, Font font, Color color, Size size, Padding padding, ContentAlignment align)
        {
            SizeF sf = g.MeasureString(str, font);
            using (Brush br = new SolidBrush(color))
            {
                switch (align)
                {
                    case ContentAlignment.MiddleCenter:
                        g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f,
                            padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f);
                        break;

                    case ContentAlignment.TopLeft:
                        g.DrawString(str, font, br, padding.Left, padding.Top);
                        break;

                    case ContentAlignment.TopCenter:
                        g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f, padding.Top);
                        break;

                    case ContentAlignment.TopRight:
                        g.DrawString(str, font, br, size.Width - sf.Width - padding.Right, padding.Top);
                        break;

                    case ContentAlignment.MiddleLeft:
                        g.DrawString(str, font, br, padding.Left, padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f);
                        break;

                    case ContentAlignment.MiddleRight:
                        g.DrawString(str, font, br, size.Width - sf.Width - padding.Right, padding.Top + (size.Height - sf.Height - padding.Top - padding.Bottom) / 2.0f);
                        break;

                    case ContentAlignment.BottomLeft:
                        g.DrawString(str, font, br, padding.Left, size.Height - sf.Height - padding.Bottom);
                        break;

                    case ContentAlignment.BottomCenter:
                        g.DrawString(str, font, br, padding.Left + (size.Width - sf.Width - padding.Left - padding.Right) / 2.0f, size.Height - sf.Height - padding.Bottom);
                        break;

                    case ContentAlignment.BottomRight:
                        g.DrawString(str, font, br, size.Width - sf.Width - padding.Right, size.Height - sf.Height - padding.Bottom);
                        break;
                }
            }
        }

        public static void DrawString(this Graphics g, string s, Font font, Color color,
            RectangleF rect, StringFormat format, float angle)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawStringRotateAtCenter(s, font, color, rect.Center(), (int)angle);
                //g.DrawString(s, font, br, layoutRectangle, format, angle);
            }
        }

        /// <summary>
        /// 以文字中心点为原点，旋转文字
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="centerPoint">文字中心点</param>
        /// <param name="angle">角度</param>
        public static void DrawStringRotateAtCenter(this Graphics g, string text, Font font, Color color, PointF centerPoint, float angle)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawStringRotateAtCenter(text, font, br, centerPoint, angle);
            }
        }

        /// <summary>
        /// 以文字中心点为原点，旋转文字
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="brush">笔刷</param>
        /// <param name="centerPoint">文字中心点</param>
        /// <param name="angle">角度</param>
        public static void DrawStringRotateAtCenter(this Graphics g, string text, Font font, Brush brush, PointF centerPoint, float angle)
        {
            SizeF sf = g.MeasureString(text, font);
            float x1 = centerPoint.X - sf.Width / 2.0f;
            float y1 = centerPoint.Y - sf.Height / 2.0f;

            // 把画板的原点(默认是左上角)定位移到文字中心
            g.TranslateTransform(x1 + sf.Width / 2, y1 + sf.Height / 2);
            // 旋转画板
            g.RotateTransform(angle);
            // 回退画板x,y轴移动过的距离
            g.TranslateTransform(-(x1 + sf.Width / 2), -(y1 + sf.Height / 2));
            g.DrawString(text, font, brush, x1, y1);

            //恢复
            g.TranslateTransform(x1 + sf.Width / 2, y1 + sf.Height / 2);
            g.RotateTransform(-angle);
            g.TranslateTransform(-(x1 + sf.Width / 2), -(y1 + sf.Height / 2));
        }

        /// <summary>
        /// 以旋转点为原点，旋转文字
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="brush">填充</param>
        /// <param name="rotatePoint">旋转点</param>
        /// <param name="format">布局方式</param>
        /// <param name="angle">角度</param>
        public static void DrawString(this Graphics g, string text, Font font, Brush brush, PointF rotatePoint, StringFormat format, float angle)
        {
            // Save the matrix
            Matrix mtxSave = g.Transform;

            Matrix mtxRotate = g.Transform;
            mtxRotate.RotateAt(angle, rotatePoint);
            g.Transform = mtxRotate;

            g.DrawString(text, font, brush, rotatePoint, format);

            // Reset the matrix
            g.Transform = mtxSave;
        }

        public static void DrawString(this Graphics g, string s, Font font, Color color, PointF rotatePoint, StringFormat format, float angle)
        {
            using (Brush br = new SolidBrush(color))
            {
                g.DrawString(s, font, br, rotatePoint, format, angle);
            }
        }

        /// <summary>
        /// 绘制根据矩形旋转文本
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="brush">填充</param>
        /// <param name="rect">局部矩形</param>
        /// <param name="format">布局方式</param>
        /// <param name="angle">角度</param>
        public static void DrawString(this Graphics g, string text, Font font, Brush brush, RectangleF rect, StringFormat format, float angle)
        {
            g.DrawStringRotateAtCenter(text, font, brush, rect.Center(), angle);
        }

        /// <summary>
        /// 两个矩阵是否重叠（边沿重叠，也认为是重叠）
        /// </summary>
        /// <param name="rc1">第一个矩阵的位置</param>
        /// <param name="rc2">第二个矩阵的位置</param>
        /// <returns></returns>
        public static bool IsOverlap(this Rectangle rc1, Rectangle rc2)
        {
            return rc1.X + rc1.Width > rc2.X &&
                   rc2.X + rc2.Width > rc1.X &&
                   rc1.Y + rc1.Height > rc2.Y &&
                   rc2.Y + rc2.Height > rc1.Y;
        }

        /// <summary>
        /// 两个矩阵是否重叠（边沿重叠，也认为是重叠）
        /// </summary>
        /// <param name="rc1">第一个矩阵的位置</param>
        /// <param name="rc2">第二个矩阵的位置</param>
        /// <returns></returns>
        public static bool IsOverlap(this RectangleF rc1, RectangleF rc2)
        {
            return rc1.X + rc1.Width > rc2.X &&
                   rc2.X + rc2.Width > rc1.X &&
                   rc1.Y + rc1.Height > rc2.Y &&
                   rc2.Y + rc2.Height > rc1.Y;
        }

        /// <summary>
        /// 两点创建一个矩形
        /// </summary>
        /// <param name="pf1">点1</param>
        /// <param name="pf2">点2</param>
        /// <returns>矩形</returns>
        public static RectangleF CreateRectangleF(this PointF pf1, PointF pf2)
        {
            return new RectangleF(Math.Min(pf1.X, pf2.X), Math.Min(pf1.Y, pf2.Y),
                Math.Abs(pf1.X - pf2.X), Math.Abs(pf1.Y - pf2.Y));
        }

        /// <summary>
        /// 两点创建一个矩形
        /// </summary>
        /// <param name="pf1">点1</param>
        /// <param name="pf2">点2</param>
        /// <returns>矩形</returns>
        public static Rectangle CreateRectangle(this Point pf1, Point pf2)
        {
            return new Rectangle(Math.Min(pf1.X, pf2.X), Math.Min(pf1.Y, pf2.Y),
                Math.Abs(pf1.X - pf2.X), Math.Abs(pf1.Y - pf2.Y));
        }

        public static double CalcY(PointF pf1, PointF pf2, double x)
        {
            if (pf1.Y.Equals(pf2.Y)) return pf1.Y;
            if (pf1.X.Equals(pf2.X)) return float.NaN;

            double a = (pf2.Y - pf1.Y) * 1.0 / (pf2.X - pf1.X);
            double b = pf1.Y - a * pf1.X;
            return a * x + b;
        }

        public static double CalcX(PointF pf1, PointF pf2, double y)
        {
            if (pf1.X.Equals(pf2.X)) return pf1.X;
            if (pf1.Y.Equals(pf2.Y)) return float.NaN;

            double a = (pf2.Y - pf1.Y) * 1.0 / (pf2.X - pf1.X);
            double b = pf1.Y - a * pf1.X;
            return (y - b) * 1.0 / a;
        }

        public static double CalcY(Point pf1, Point pf2, double x)
        {
            if (pf1.Y == pf2.Y) return pf1.Y;
            if (pf1.X == pf2.X) return double.NaN;

            double a = (pf2.Y - pf1.Y) * 1.0 / (pf2.X - pf1.X);
            double b = pf1.Y - a * pf1.X;
            return a * x + b;
        }

        public static double CalcX(Point pf1, Point pf2, double y)
        {
            if (pf1.Y == pf2.Y) return pf1.X;
            if (pf1.X == pf2.X) return double.NaN;

            double a = (pf2.Y - pf1.Y) * 1.0 / (pf2.X - pf1.X);
            double b = pf1.Y - a * pf1.X;
            return (y - b) * 1.0 / a;
        }


        public static void DrawTwoPoints(this Graphics g, Color color, float width, PointF pf1, PointF pf2, Rectangle rect)
        {
            using (Pen pen = new Pen(color, width))
            {
                DrawTwoPoints(g, pen, pf1, pf2, rect);
            }
        }

        public static void DrawTwoPoints(this Graphics g, Pen pen, PointF pf1, PointF pf2, Rectangle rect)
        {
            bool haveLargePixel = Math.Abs(pf1.X - pf2.X) >= rect.Width * 100 || Math.Abs(pf1.Y - pf2.Y) >= rect.Height * 100;

            //两点都在区域内
            if (pf1.InRect(rect) && pf2.InRect(rect))
            {
                g.DrawLine(pen, pf1, pf2);
                return;
            }

            //无大坐标像素
            if (!haveLargePixel)
            {
                g.DrawLine(pen, pf1, pf2);
                return;
            }

            //垂直线
            if (pf1.X.Equals(pf2.X))
            {
                if (pf1.X <= rect.Left) return;
                if (pf1.X >= rect.Right) return;
                if (pf1.Y <= rect.Top && pf2.Y <= rect.Top) return;
                if (pf1.Y >= rect.Bottom && pf2.Y >= rect.Bottom) return;

                float yy1 = Math.Min(pf1.Y, pf2.Y);
                float yy2 = Math.Max(pf1.Y, pf2.Y);
                if (yy1 <= rect.Top)
                {
                    if (yy2 <= rect.Bottom) g.DrawLine(pen, pf1.X, rect.Top, pf1.X, yy2);
                    else g.DrawLine(pen, pf1.X, rect.Top, pf1.X, rect.Bottom);
                }
                else
                {
                    if (yy2 <= rect.Bottom) g.DrawLine(pen, pf1.X, yy1, pf1.X, yy2);
                    else g.DrawLine(pen, pf1.X, yy1, pf1.X, rect.Bottom);
                }

                return;
            }

            //水平线
            if (pf1.Y.Equals(pf2.Y))
            {
                if (pf1.Y <= rect.Top) return;
                if (pf1.Y >= rect.Bottom) return;
                if (pf1.X <= rect.Left && pf2.X <= rect.Left) return;
                if (pf1.X >= rect.Right && pf2.X >= rect.Right) return;

                float xx1 = Math.Min(pf1.X, pf2.X);
                float xx2 = Math.Max(pf1.X, pf2.X);
                if (xx1 <= rect.Left)
                {
                    if (xx2 <= rect.Right) g.DrawLine(pen, rect.Left, pf1.Y, xx2, pf1.Y);
                    else g.DrawLine(pen, rect.Left, pf1.Y, rect.Right, pf1.Y);
                }
                else
                {
                    if (xx2 <= rect.Right) g.DrawLine(pen, xx1, pf1.Y, xx2, pf1.Y);
                    else g.DrawLine(pen, xx1, pf1.Y, rect.Right, pf1.Y);
                }

                return;
            }

            //判断两个区域是否相交
            RectangleF rect1 = pf1.CreateRectangleF(pf2);
            if (!rect1.IsOverlap(rect)) return;

            double x1 = CalcX(pf1, pf2, rect.Top);
            double x2 = CalcX(pf1, pf2, rect.Bottom);
            double y1 = CalcY(pf1, pf2, rect.Left);
            double y2 = CalcY(pf1, pf2, rect.Right);

            //判断线段是否和区域有交点
            bool isExist = x1.InRange(rect.Left, rect.Right) || x2.InRange(rect.Left, rect.Right) || y1.InRange(rect.Top, rect.Bottom) || y2.InRange(rect.Top, rect.Bottom);
            if (!isExist) return;

            List<PointF> TwoPoints = new List<PointF>();
            if (!pf1.InRect(rect) && !pf2.InRect(rect))
            {
                if (x1.InRange(rect.Left, rect.Right)) TwoPoints.Add(new PointF((float)x1, rect.Top));
                if (x2.InRange(rect.Left, rect.Right)) TwoPoints.Add(new PointF((float)x2, rect.Bottom));
                if (y1.InRange(rect.Top, rect.Bottom)) TwoPoints.Add(new PointF(rect.Left, (float)y1));
                if (y2.InRange(rect.Top, rect.Bottom)) TwoPoints.Add(new PointF(rect.Right, (float)y2));
            }
            else
            {
                PointF center = pf1.InRect(rect) ? pf1 : pf2;
                PointF border = pf2.InRect(rect) ? pf1 : pf2;
                TwoPoints.Add(center);
                if (border.X >= center.X)
                {
                    if (border.Y >= center.Y)
                    {
                        TwoPoints.Add(x2 <= rect.Right ? new PointF((float)x2, rect.Bottom) : new PointF(rect.Right, (float)y2));
                    }
                    else
                    {
                        TwoPoints.Add(x1 <= rect.Right ? new PointF((float)x1, rect.Top) : new PointF(rect.Right, (float)y2));
                    }
                }
                else
                {
                    if (border.Y >= center.Y)
                    {
                        TwoPoints.Add(x2 >= rect.Left ? new PointF((float)x2, rect.Bottom) : new PointF(rect.Left, (float)y1));
                    }
                    else
                    {
                        TwoPoints.Add(x1 >= rect.Left ? new PointF((float)x1, rect.Bottom) : new PointF(rect.Left, (float)y1));
                    }
                }
            }

            if (TwoPoints.Count == 2)
            {
                g.DrawLine(pen, TwoPoints[0], TwoPoints[1]);
            }
            else
            {
                Console.WriteLine(TwoPoints.Count);
            }

            TwoPoints.Clear();
        }
    }
}