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
 * 文件名称: UGDI.cs
 * 文件说明: GDI扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2021-08-19: V3.0.6 修复CreateRoundedRectanglePath参数radius=0时的错误 
 * 2021-08-20: V3.0.6 整理了一些GDI绘图的常用方法扩展 
******************************************************************************/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sunny.UI
{
    /// <summary>
    /// GDI扩展类
    /// </summary>
    public static class GDI
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
        /// 颜色是否是浅色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>是否是浅色</returns>
        public static bool IsLightColor(this Color color)
        {
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255 > 0.5;
        }

        /// <summary>
        /// 根据背景色判断前景色
        /// </summary>
        /// <param name="backColor">背景色</param>
        /// <returns>前景色</returns>
        public static Color ForeColor(Color backColor)
        {
            return backColor.IsLightColor() ? Color.Black : Color.White;
        }

        /// <summary>
        /// SizeF转Size
        /// </summary>
        /// <param name="size">SizeF</param>
        /// <returns>Size</returns>
        public static Size Size(this SizeF size)
        {
            return new Size(size.Width.RoundEx(), size.Height.RoundEx());
        }

        /// <summary>
        /// PointF转Point
        /// </summary>
        /// <param name="point">PointF</param>
        /// <returns>Point</returns>
        public static Point Point(this PointF point)
        {
            return new Point(point.X.RoundEx(), point.Y.RoundEx());
        }

        /// <summary>
        /// Size增加长宽
        /// </summary>
        /// <param name="size">Size</param>
        /// <param name="width">宽</param>
        /// <param name="height">长</param>
        /// <returns>结果</returns>
        public static Size Add(this Size size, int width, int height)
        {
            return new Size(size.Width + width, size.Height + height);
        }


        /// <summary>
        /// SizeF增加长宽
        /// </summary>
        /// <param name="size">SizeF</param>
        /// <param name="width">宽</param>
        /// <param name="height">长</param>
        /// <returns>结果</returns>
        public static SizeF Add(this SizeF size, float width, float height)
        {
            return new SizeF(size.Width + width, size.Height + height);
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
        /// 获取起始颜色到终止颜色之间的渐变颜色
        /// </summary>
        /// <param name="startColor">起始颜色</param>
        /// <param name="endColor">终止颜色</param>
        /// <param name="count">个数</param>
        /// <returns>渐变颜色列表</returns>
        public static Color[] GradientColors(this Color startColor, Color endColor, int count)
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

        /// <summary>
        /// 开启高质量绘图
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="smooth">是否平滑</param>
        /// <returns>绘图图面</returns>
        public static Graphics Smooth(this Graphics g, bool smooth = true)
        {
            if (smooth)
            {
                g.SetHighQuality();
            }
            else
            {
                g.SetDefaultQuality();
            }

            return g;
        }

        /// <summary>
        /// 创建圆角路径
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="rect">区域</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="radiusSides">圆角的方位</param>
        /// <returns>路径</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Graphics g, Rectangle rect, int radius, UICornerRadiusSides radiusSides)
        {
            return rect.CreateRoundedRectanglePath(radius, radiusSides);
        }

        /// <summary>
        /// 创建圆角路径
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="rect">区域</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="cornerLeftTop">左上角</param>
        /// <param name="cornerRightTop">右上角</param>
        /// <param name="cornerRightBottom">右下角</param>
        /// <param name="cornerLeftBottom">左下角</param>
        /// <returns>路径</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Graphics g, Rectangle rect, int radius, bool cornerLeftTop = true, bool cornerRightTop = true, bool cornerRightBottom = true, bool cornerLeftBottom = true)
        {
            return rect.CreateRoundedRectanglePath(radius, cornerLeftTop, cornerRightTop, cornerRightBottom, cornerLeftBottom);
        }

        /// <summary>
        /// 创建圆角路径
        /// </summary>
        /// <param name="rect">区域</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="radiusSides">圆角的方位</param>
        /// <param name="lineSize">线宽</param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int radius, UICornerRadiusSides radiusSides, int lineSize = 1)
        {
            GraphicsPath path;

            if (radiusSides == UICornerRadiusSides.None || radius == 0)
            {
                path = rect.GraphicsPath();
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
                path = rect.CreateRoundedRectanglePath(radius, RadiusLeftTop, RadiusRightTop, RadiusRightBottom, RadiusLeftBottom, lineSize);
            }

            return path;
        }

        /// <summary>
        /// 绘图路径
        /// </summary>
        /// <param name="rect">区域</param>
        /// <returns>路径</returns>
        public static GraphicsPath GraphicsPath(this Rectangle rect)
        {
            Point[] points = new Point[] {
                    new Point(rect.Left, rect.Top),
                    new Point(rect.Right, rect.Top),
                    new Point(rect.Right, rect.Bottom),
                    new Point(rect.Left, rect.Bottom),
                    new Point(rect.Left, rect.Top) };
            return points.Path();
        }

        /// <summary>
        /// 创建扇形绘图路径
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="center">圆心</param>
        /// <param name="d1">内径</param>
        /// <param name="d2">外径</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">终止角度</param>
        /// <returns>扇形绘图路径</returns>
        public static GraphicsPath CreateFanPath(this Graphics g, Point center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return center.CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        /// <summary>
        /// 创建扇形绘图路径
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="center">圆心</param>
        /// <param name="d1">内径</param>
        /// <param name="d2">外径</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">终止角度</param>
        /// <returns>扇形绘图路径</returns>
        public static GraphicsPath CreateFanPath(this Graphics g, PointF center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return center.CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        /// <summary>
        /// 创建扇形绘图路径
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="d1">内径</param>
        /// <param name="d2">外径</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">终止角度</param>
        /// <returns>扇形绘图路径</returns>
        public static GraphicsPath CreateFanPath(this Point center, float d1, float d2, float startAngle, float sweepAngle)
        {
            return new PointF(center.X, center.Y).CreateFanPath(d1, d2, startAngle, sweepAngle);
        }

        /// <summary>
        /// 创建扇形绘图路径
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="d1">内径</param>
        /// <param name="d2">外径</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">终止角度</param>
        /// <returns>扇形绘图路径</returns>
        public static GraphicsPath CreateFanPath(this PointF center, float d1, float d2, float startAngle, float sweepAngle)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(center.X - d1, center.Y - d1, d1 * 2, d1 * 2, startAngle, sweepAngle);
            path.AddArc(center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle + sweepAngle, -sweepAngle);
            path.AddArc(center.X - d1, center.Y - d1, d1 * 2, d1 * 2, startAngle, 0.1f);
            return path;
        }

        /// <summary>
        /// 创建圆角路径
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="rect">区域</param>
        /// <param name="radius">圆角大小</param>
        /// <param name="cornerLeftTop">左上角</param>
        /// <param name="cornerRightTop">右上角</param>
        /// <param name="cornerRightBottom">右下角</param>
        /// <param name="cornerLeftBottom">左下角</param>
        /// <param name="lineSize">线宽</param>
        /// <returns>路径</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int radius,
            bool cornerLeftTop = true, bool cornerRightTop = true, bool cornerRightBottom = true, bool cornerLeftBottom = true,
            int lineSize = 1)
        {
            GraphicsPath path = new GraphicsPath();

            if ((!cornerLeftTop && !cornerRightTop && !cornerRightBottom && !cornerLeftBottom) || radius <= 0)
            {
                path = rect.GraphicsPath();
            }
            else
            {
                radius *= lineSize;
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
            }

            return path;
        }

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g"></param>
        public static Graphics SetHighQuality(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            return g;
        }

        /// <summary>
        /// 设置GDI默认值
        /// </summary>
        /// <param name="g"></param>
        public static Graphics SetDefaultQuality(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
            g.CompositingQuality = CompositingQuality.Default;
            return g;
        }

        /// <summary>
        /// Color转HTML
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>HTML</returns>
        public static string ToHTML(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }

        /// <summary>
        /// HTML转Color
        /// </summary>
        /// <param name="htmlColor">HTML</param>
        /// <param name="alpha">透明度</param>
        /// <returns>Color</returns>
        public static Color ToColor(this string htmlColor, int alpha = 255)
        {
            return Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor));
        }

        /// <summary>
        /// 颜色是否为空
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>是否为空</returns>
        public static bool IsNullOrEmpty(this Color color)
        {
            return color == Color.Empty || color == Color.Transparent;
        }

        /// <summary>
        /// 画刷
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>画刷</returns>
        public static SolidBrush Brush(this Color color)
        {
            return new SolidBrush(color);
        }

        /// <summary>
        /// 画笔
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="size">线宽</param>
        /// <returns>画笔</returns>
        public static Pen Pen(this Color color, float size = 1)
        {
            return new Pen(color, size);
        }

        /// <summary>
        /// HTML颜色生成画刷
        /// </summary>
        /// <param name="htmlColor">HTML颜色</param>
        /// <param name="alpha">透明度</param>
        /// <returns>画刷</returns>
        public static SolidBrush Brush(this string htmlColor, int alpha = 255)
        {
            return new SolidBrush(Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor)));
        }

        /// <summary>
        /// HTML颜色生成画笔
        /// </summary>
        /// <param name="htmlColor">HTML颜色</param>
        /// <param name="alpha">透明度</param>
        /// <param name="size">线宽</param>
        /// <param name="startCap">起始线帽样式</param>
        /// <param name="endCap">结束线帽样式</param>
        /// <returns>画笔</returns>
        public static Pen Pen(this string htmlColor, int alpha = 255, float size = 1, LineCap startCap = LineCap.Custom, LineCap endCap = LineCap.Custom)
        {
            return new Pen(Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor)), size) { StartCap = startCap, EndCap = endCap };
        }

        /// <summary>
        /// 渐变画刷
        /// </summary>
        /// <param name="centerColor"></param>
        /// <param name="surroundColor"></param>
        /// <param name="point"></param>
        /// <param name="gp"></param>
        /// <param name="wrapMode"></param>
        /// <returns></returns>
        public static Brush GlowBrush(Color centerColor, Color[] surroundColor, PointF point, GraphicsPath gp, WrapMode wrapMode = WrapMode.Clamp)
        {
            return new PathGradientBrush(gp) { CenterColor = centerColor, SurroundColors = surroundColor, FocusScales = point, WrapMode = wrapMode };
        }

        /// <summary>
        /// 渐变画刷
        /// </summary>
        /// <param name="centerColor"></param>
        /// <param name="surroundColor"></param>
        /// <param name="point"></param>
        /// <param name="wrapMode"></param>
        /// <returns></returns>
        public static Brush GlowBrush(Color centerColor, Color[] surroundColor, PointF[] point, WrapMode wrapMode = WrapMode.Clamp)
        {
            return new PathGradientBrush(point) { CenterColor = centerColor, SurroundColors = surroundColor, WrapMode = wrapMode };
        }

        /// <summary>
        /// 文本布局
        /// </summary>
        /// <param name="horizontalAlignment">水平方向</param>
        /// <param name="verticalAlignment">垂直方向</param>
        /// <returns>文本布局</returns>
        public static StringFormat SetAlignment(StringAlignment horizontalAlignment = StringAlignment.Center, StringAlignment verticalAlignment = StringAlignment.Center)
        {
            return new StringFormat { Alignment = horizontalAlignment, LineAlignment = verticalAlignment };
        }
    }
}