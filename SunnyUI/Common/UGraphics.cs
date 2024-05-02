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
 * 文件名称: UGraphics.cs
 * 文件说明: GDI扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-08-20
 *
 * 2021-08-20: V3.0.6 整理了一些GDI绘图的常用方法扩展
 * 2023-03-28: V3.3.4 重构了一遍绘图方法
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// GDI扩展类
    /// </summary>
    public static class GraphicsEx
    {
        public static void DrawString(this Graphics g, string text, Font font, Color color, Rectangle rect, ContentAlignment alignment, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            rect.Offset(offsetX, offsetY);
            Size size = TextRenderer.MeasureText(text, font);
            int left = 0, top = 0;

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    left = rect.Left + 1;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    left = rect.Left + (rect.Width - size.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    left = rect.Left + rect.Width - size.Width - 1;
                    break;
            }

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    top = rect.Top + 1;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:

                    top = rect.Top + (rect.Height - size.Height) / 2;
                    break;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    top = rect.Top + rect.Height - size.Height - 1;
                    break;
            }

            TextRenderer.DrawText(g, text, font, new Point(left, top), color);
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, Color backColor, Rectangle rect, ContentAlignment alignment, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            rect.Offset(offsetX, offsetY);
            Size size = TextRenderer.MeasureText(text, font);
            int left = 0, top = 0;

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    left = rect.Left + 1;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    left = rect.Left + (rect.Width - size.Width) / 2;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    left = rect.Left + rect.Width - size.Width - 1;
                    break;
            }

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    top = rect.Top + 1;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:

                    top = rect.Top + (rect.Height - size.Height) / 2;
                    break;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    top = rect.Top + rect.Height - size.Height - 1;
                    break;
            }

            TextRenderer.DrawText(g, text, font, new Point(left, top), color, backColor);
        }


        public static void DrawString(this Graphics g, string text, Font font, Color color, Rectangle rect, StringAlignment alignment, StringAlignment lineAlignment, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            rect.Offset(offsetX, offsetY);
            Size size = TextRenderer.MeasureText(text, font);
            int left = 0, top = 0;

            switch (alignment)
            {
                case StringAlignment.Near:
                    left = rect.Left + 1;
                    break;
                case StringAlignment.Center:
                    left = rect.Left + (rect.Width - size.Width) / 2;
                    break;
                case StringAlignment.Far:
                    left = rect.Left + rect.Width - size.Width - 1;
                    break;
            }

            switch (lineAlignment)
            {
                case StringAlignment.Near:
                    top = rect.Top + 1;
                    break;
                case StringAlignment.Center:
                    top = rect.Top + (rect.Height - size.Height) / 2;
                    break;
                case StringAlignment.Far:
                    top = rect.Top + rect.Height - size.Height - 1;
                    break;
            }

            TextRenderer.DrawText(g, text, font, new Point(left, top), color);
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, Color backColor, Rectangle rect, StringAlignment alignment, StringAlignment lineAlignment, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            rect.Offset(offsetX, offsetY);
            Size size = TextRenderer.MeasureText(text, font);
            int left = 0, top = 0;

            switch (alignment)
            {
                case StringAlignment.Near:
                    left = rect.Left + 1;
                    break;
                case StringAlignment.Center:
                    left = rect.Left + (rect.Width - size.Width) / 2;
                    break;
                case StringAlignment.Far:
                    left = rect.Left + rect.Width - size.Width - 1;
                    break;
            }

            switch (lineAlignment)
            {
                case StringAlignment.Near:
                    top = rect.Top + 1;
                    break;
                case StringAlignment.Center:
                    top = rect.Top + (rect.Height - size.Height) / 2;
                    break;
                case StringAlignment.Far:
                    top = rect.Top + rect.Height - size.Height - 1;
                    break;
            }

            TextRenderer.DrawText(g, text, font, new Point(left, top), color, backColor);
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, Rectangle rect, HorizontalAlignment horizontalAlignment, int offsetX = 0, int offsetY = 0)
        {
            StringAlignment alignment = StringAlignment.Center;
            if (horizontalAlignment == HorizontalAlignment.Left) alignment = StringAlignment.Near;
            if (horizontalAlignment == HorizontalAlignment.Right) alignment = StringAlignment.Far;
            g.DrawString(text, font, color, rect, alignment, StringAlignment.Center, offsetX, offsetY);
        }

        public static void DrawString(this Graphics g, string text, Font font, Color color, Color backColor, Rectangle rect, HorizontalAlignment horizontalAlignment, int offsetX = 0, int offsetY = 0)
        {
            StringAlignment alignment = StringAlignment.Center;
            if (horizontalAlignment == HorizontalAlignment.Left) alignment = StringAlignment.Near;
            if (horizontalAlignment == HorizontalAlignment.Right) alignment = StringAlignment.Far;
            g.DrawString(text, font, color, backColor, rect, alignment, StringAlignment.Center, offsetX, offsetY);
        }

        /// <summary>
        /// 绘制字符串
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        public static void DrawString(this Graphics g, string text, Font font, Color color, float x, float y)
        {
            if (text.IsNullOrEmpty()) return;
            using Brush br = color.Brush();
            g.DrawString(text, font, br, x, y);
        }

        /// <summary>
        /// 绘制字符串
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>   
        /// <param name="borderColor">描边颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        public static void DrawString(this Graphics g, string text, Font font, Color color, Color borderColor, float x, float y)
        {
            g.DrawString(text, font, borderColor, x - 1, y - 1);
            g.DrawString(text, font, borderColor, x - 1, y);
            g.DrawString(text, font, borderColor, x - 1, y + 1);
            g.DrawString(text, font, borderColor, x, y - 1);
            g.DrawString(text, font, borderColor, x, y + 1);
            g.DrawString(text, font, borderColor, x + 1, y - 1);
            g.DrawString(text, font, borderColor, x + 1, y);
            g.DrawString(text, font, borderColor, x + 1, y + 1);
            g.DrawString(text, font, color, x, y);
        }

        /// <summary>
        /// 以文字中心点为原点，旋转文字
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="centerPoint">文字中心点</param>
        /// <param name="angle">角度</param>
        public static void DrawRotateString(this Graphics g, string text, Font font, Color color, PointF centerPoint, float angle, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            using Brush br = color.Brush();
            g.DrawRotateString(text, font, br, centerPoint, angle, offsetX, offsetY);
        }

        /// <summary>
        /// 以文字中心点为原点，旋转文字
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="brush">笔刷</param>
        /// <param name="centerPoint">文字中心点</param>
        /// <param name="angle">角度</param>
        private static void DrawRotateString(this Graphics g, string text, Font font, Brush brush, PointF centerPoint, float angle, int offsetX = 0, int offsetY = 0)
        {
            if (text.IsNullOrEmpty()) return;
            SizeF sf = TextRenderer.MeasureText(text, font);
            float x1 = centerPoint.X - sf.Width / 2.0f + offsetX;
            float y1 = centerPoint.Y - sf.Height / 2.0f + offsetY;

            // 把画板的原点(默认是左上角)定位移到文字中心
            g.TranslateTransform(x1 + sf.Width / 2.0f, y1 + sf.Height / 2.0f);
            // 旋转画板
            g.RotateTransform(angle);
            // 回退画板x,y轴移动过的距离
            g.TranslateTransform(-(x1 + sf.Width / 2.0f), -(y1 + sf.Height / 2.0f));
            g.DrawString(text, font, brush, x1, y1);

            //恢复
            g.TranslateTransform(x1 + sf.Width / 2.0f, y1 + sf.Height / 2.0f);
            g.RotateTransform(-angle);
            g.TranslateTransform(-(x1 + sf.Width / 2.0f), -(y1 + sf.Height / 2.0f));
        }

        /// <summary>
        /// 以旋转点为原点，旋转文字
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="brush">填充</param>
        /// <param name="rotatePoint">旋转点</param>
        /// <param name="format">布局方式</param>
        /// <param name="angle">角度</param>
        private static void DrawRotateString(this Graphics g, string text, Font font, Brush brush, PointF rotatePoint, StringFormat format, float angle)
        {
            if (text.IsNullOrEmpty()) return;
            // Save the matrix
            Matrix mtxSave = g.Transform;
            Matrix mtxRotate = g.Transform;
            mtxRotate.RotateAt(angle, rotatePoint);
            g.Transform = mtxRotate;
            g.DrawString(text, font, brush, rotatePoint, format);

            // Reset the matrix
            g.Transform = mtxSave;
        }

        /// <summary>
        /// 绘制字符串
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="text">文字</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="rotatePoint">旋转点</param>
        /// <param name="format">格式</param>
        /// <param name="angle">角度</param>
        public static void DrawRotateString(this Graphics g, string text, Font font, Color color, PointF rotatePoint, StringFormat format, float angle)
        {
            if (text.IsNullOrEmpty()) return;
            using Brush br = color.Brush();
            g.DrawRotateString(text, font, br, rotatePoint, format, angle);
        }

        /// <summary>
        /// 绘制多条直线连接
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLines(this Graphics g, Color color, Point[] points, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawLines(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制多条直线连接
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLines(this Graphics g, Color color, PointF[] points, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawLines(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawCurve(this Graphics g, Color color, Point[] points, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawCurve(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawCurve(this Graphics g, Color color, PointF[] points, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawCurve(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x1">点1水平位置</param>
        /// <param name="y1">点1垂直位置</param>
        /// <param name="x2">点2水平位置</param>
        /// <param name="y2">点2垂直位置</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLine(this Graphics g, Color color, int x1, int y1, int x2, int y2, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawLine(pen, x1, y1, x2, y2);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLine(this Graphics g, Color color, Point pt1, Point pt2, bool smooth = false, float penWidth = 1)
        => g.DrawLine(color, pt1.X, pt1.Y, pt2.X, pt2.Y, smooth, penWidth);

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x1">点1水平位置</param>
        /// <param name="y1">点1垂直位置</param>
        /// <param name="x2">点2水平位置</param>
        /// <param name="y2">点2垂直位置</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLine(this Graphics g, Color color, float x1, float y1, float x2, float y2, bool smooth = false, float penWidth = 1)
        {
            if (y1.IsNanOrInfinity() || y2.IsNanOrInfinity() || x1.IsNanOrInfinity() || x2.IsNanOrInfinity()) return;
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawLine(pen, x1, y1, x2, y2);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawLine(this Graphics g, Color color, PointF pt1, PointF pt2, bool smooth = false, float penWidth = 1)
        => g.DrawLine(color, pt1.X, pt1.Y, pt2.X, pt2.Y, smooth, penWidth);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawArc(this Graphics g, Color color, int x, int y, int width, int height, int startAngle, int sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制弧线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawArc(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制弧线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawArc(this Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
         => g.DrawArc(color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, smooth, penWidth);

        /// <summary>
        /// 绘制弧线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawArc(this Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
         => g.DrawArc(color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, smooth, penWidth);

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x1">点1水平位置</param>
        /// <param name="y1">点1垂直位置</param>
        /// <param name="x2">点2水平位置</param>
        /// <param name="y2">点2垂直位置</param>
        /// <param name="x3">点3水平位置</param>
        /// <param name="y3">点3垂直位置</param>
        /// <param name="x4">点4水平位置</param>
        /// <param name="y4">点4垂直位置</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawBezier(this Graphics g, Color color, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <param name="pt3">点3</param>
        /// <param name="pt4">点4</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawBezier(this Graphics g, Color color, PointF pt1, PointF pt2, PointF pt3, PointF pt4, bool smooth = true, float penWidth = 1)
            => g.DrawBezier(color, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y, smooth, penWidth);

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="pt1">点1</param>
        /// <param name="pt2">点2</param>
        /// <param name="pt3">点3</param>
        /// <param name="pt4">点4</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawBezier(this Graphics g, Color color, Point pt1, Point pt2, Point pt3, Point pt4, bool smooth = true, float penWidth = 1)
          => g.DrawBezier(color, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y, smooth, penWidth);

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawBeziers(this Graphics g, Color color, PointF[] points, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawBeziers(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawBeziers(this Graphics g, Color color, Point[] points, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawBeziers(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制闭合曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawClosedCurve(this Graphics g, Color color, Point[] points, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawClosedCurve(pen, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制闭合曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="tension">张力</param>
        /// <param name="fillmode">填充模式</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawClosedCurve(this Graphics g, Color color, Point[] points, float tension, FillMode fillmode, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawClosedCurve(pen, points, tension, fillmode);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充闭合曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        public static void FillClosedCurve(this Graphics g, Color color, Point[] points, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillClosedCurve(sb, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充闭合曲线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="fillmode">填充模式</param>
        /// <param name="tension">张力</param>
        /// <param name="smooth">平滑</param>
        public static void FillClosedCurve(this Graphics g, Color color, Point[] points, FillMode fillmode, float tension, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillClosedCurve(sb, points, fillmode, tension);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充路径
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="path">路径</param>
        /// <param name="smooth">平滑</param>
        public static void FillPath(this Graphics g, Color color, GraphicsPath path, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillPath(sb, path);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制路径
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="path">路径</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPath(this Graphics g, Color color, GraphicsPath path, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawPath(pn, path);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        public static void FillPolygon(this Graphics g, Color color, PointF[] points, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillPolygon(sb, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="fillMode">填充模式</param>
        /// <param name="smooth">平滑</param>
        public static void FillPolygon(this Graphics g, Color color, PointF[] points, FillMode fillMode, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillPolygon(sb, points, fillMode);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        public static void FillPolygon(this Graphics g, Color color, Point[] points, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillPolygon(sb, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="fillMode">填充模式</param>
        /// <param name="smooth">平滑</param>
        public static void FillPolygon(this Graphics g, Color color, Point[] points, FillMode fillMode, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillPolygon(sb, points, fillMode);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPolygon(this Graphics g, Color color, PointF[] points, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawPolygon(pn, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="points">点列表</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPolygon(this Graphics g, Color color, Point[] points, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawPolygon(pn, points);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        public static void FillEllipse(this Graphics g, Color color, Rectangle rect, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillEllipse(sb, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawEllipse(this Graphics g, Color color, Rectangle rect, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawEllipse(pn, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        public static void FillEllipse(this Graphics g, Color color, RectangleF rect, bool smooth = true)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillEllipse(sb, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawEllipse(this Graphics g, Color color, RectangleF rect, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawEllipse(pn, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        public static void FillEllipse(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = true)
         => g.FillEllipse(color, new Rectangle(left, top, width, height), smooth);

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawEllipse(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = true, float penWidth = 1)
         => g.DrawEllipse(color, new Rectangle(left, top, width, height), smooth, penWidth);

        /// <summary>
        /// 填充椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        public static void FillEllipse(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = true)
         => g.FillEllipse(color, new RectangleF(left, top, width, height), smooth);

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawEllipse(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = true, float penWidth = 1)
        => g.DrawEllipse(color, new RectangleF(left, top, width, height), smooth, penWidth);

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangle(this Graphics g, Color color, Rectangle rect, bool smooth = false)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillRectangle(sb, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangle(this Graphics g, Color color, Rectangle rect, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawRectangle(pn, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangle(this Graphics g, Color color, RectangleF rect, bool smooth = false)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillRectangle(sb, rect);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangle(this Graphics g, Color color, RectangleF rect, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawRectangle(pn, rect.X, rect.Y, rect.Width, rect.Height);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangle(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = false)
        => g.FillRectangle(color, new Rectangle(left, top, width, height), smooth);

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangle(this Graphics g, Color color, int left, int top, int width, int height, bool smooth = false, float penWidth = 1)
        => g.DrawRectangle(color, new Rectangle(left, top, width, height), smooth, penWidth);

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangle(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = false)
        => g.FillRectangle(color, new RectangleF(left, top, width, height), smooth);

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangle(this Graphics g, Color color, float left, float top, float width, float height, bool smooth = false, float penWidth = 1)
        => g.DrawRectangle(color, new RectangleF(left, top, width, height), smooth, penWidth);

        /// <summary>
        /// 填充矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rects">多个矩形</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangles(this Graphics g, Color color, Rectangle[] rects, bool smooth = false)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillRectangles(sb, rects);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充多个矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rects">多个矩形</param>
        /// <param name="smooth">平滑</param>
        public static void FillRectangles(this Graphics g, Color color, RectangleF[] rects, bool smooth = false)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillRectangles(sb, rects);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制图形形状
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="region">图形形状</param>
        /// <param name="smooth">平滑</param>
        public static void FillRegion(this Graphics g, Color color, Region region, bool smooth = false)
        {
            g.Smooth(smooth);
            using SolidBrush sb = color.Brush();
            g.FillRegion(sb, region);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制多个矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rects">多个矩形</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangles(this Graphics g, Color color, Rectangle[] rects, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawRectangles(pn, rects);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制多个矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rects">多个矩形</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRectangles(this Graphics g, Color color, RectangleF[] rects, bool smooth = false, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pn = color.Pen(penWidth);
            g.DrawRectangles(pn, rects);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="pen">画笔</param>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void DrawRoundRectangle(this Graphics g, Pen pen, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            g.Smooth(smooth);
            if (!UIStyles.GlobalRectangle && cornerRadius > 0)
            {
                using GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius);
                g.DrawPath(pen, path);
            }
            else
            {
                g.DrawRectangle(pen, rect);
            }

            g.Smooth(false);
        }

        /// <summary>
        /// 填充圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="brush">画刷</param>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void FillRoundRectangle(this Graphics g, Brush brush, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            g.Smooth(smooth);
            if (!UIStyles.GlobalRectangle && cornerRadius > 0)
            {
                using GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius);
                g.FillPath(brush, path);
            }
            else
            {
                g.FillRectangle(brush, rect);
            }

            g.Smooth(false);
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="pen">画笔</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void DrawRoundRectangle(this Graphics g, Pen pen, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.DrawRoundRectangle(pen, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        /// <summary>
        /// 填充圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="brush">画刷</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void FillRoundRectangle(this Graphics g, Brush brush, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        {
            g.FillRoundRectangle(brush, new Rectangle(left, top, width, height), cornerRadius, smooth);
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRoundRectangle(this Graphics g, Color color, Rectangle rect, int cornerRadius, bool smooth = true, float penWidth = 1)
        {
            if (!UIStyles.GlobalRectangle && cornerRadius > 0)
            {
                using GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius);
                g.DrawPath(color, path, smooth, penWidth);
            }
            else
            {
                g.DrawRectangle(color, rect, smooth, penWidth);
            }
        }

        /// <summary>
        /// 填充圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void FillRoundRectangle(this Graphics g, Color color, Rectangle rect, int cornerRadius, bool smooth = true)
        {
            if (!UIStyles.GlobalRectangle && cornerRadius > 0)
            {
                using GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius);
                g.FillPath(color, path, smooth);
            }
            else
            {
                g.FillRectangle(color, rect, smooth);
            }
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawRoundRectangle(this Graphics g, Color color, int left, int top, int width, int height, int cornerRadius, bool smooth = true, float penWidth = 1)
        => g.DrawRoundRectangle(color, new Rectangle(left, top, width, height), cornerRadius, smooth, penWidth);

        /// <summary>
        /// 填充圆角矩形
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="cornerRadius">圆角大小</param>
        /// <param name="smooth">平滑</param>
        public static void FillRoundRectangle(this Graphics g, Color color, int left, int top, int width, int height, int cornerRadius, bool smooth = true)
        => g.FillRoundRectangle(color, new Rectangle(left, top, width, height), cornerRadius, smooth);

        /// <summary>
        /// 绘制十字线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="crossSize">十字线大小</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawCross(this Graphics g, Color color, Point center, int crossSize = 3, float penWidth = 1)
        {
            g.DrawLine(color, center.X - crossSize, center.Y, center.X + crossSize, center.Y, false, penWidth);
            g.DrawLine(color, center.X, center.Y - crossSize, center.X, center.Y + crossSize, false, penWidth);
        }

        /// <summary>
        /// 绘制扇面区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="d1">距离1</param>
        /// <param name="d2">距离2</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawFan(this Graphics g, Color color, Point center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            if (d1.Equals(0))
            {
                g.DrawPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle, smooth, penWidth);
            }
            else
            {
                using GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.DrawPath(color, path, smooth, penWidth);
            }
        }

        /// <summary>
        /// 绘制扇面区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="d1">距离1</param>
        /// <param name="d2">距离2</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawFan(this Graphics g, Color color, PointF center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            if (d1.Equals(0))
            {
                g.DrawPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle, smooth, penWidth);
            }
            else
            {
                using GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.DrawPath(color, path, smooth, penWidth);
            }
        }

        /// <summary>
        /// 填充扇面区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="d1">距离1</param>
        /// <param name="d2">距离2</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillFan(this Graphics g, Color color, Point center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.FillPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle, smooth);
            }
            else
            {
                using GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.FillPath(color, path, smooth);
            }
        }

        /// <summary>
        /// 填充扇面区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="d1">距离1</param>
        /// <param name="d2">距离2</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillFan(this Graphics g, Color color, PointF center, float d1, float d2, float startAngle, float sweepAngle, bool smooth = true)
        {
            if (d1.Equals(0))
            {
                g.FillPie(color, center.X - d2, center.Y - d2, d2 * 2, d2 * 2, startAngle, sweepAngle, smooth);
            }
            else
            {
                using GraphicsPath path = g.CreateFanPath(center, d1, d2, startAngle, sweepAngle);
                g.FillPath(color, path, smooth);
            }
        }

        /// <summary>
        /// 填充扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillPie(this Graphics g, Color color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.Smooth(smooth);
            using Brush br = color.Brush();
            g.FillPie(br, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillPie(this Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool smooth = true)
        => g.FillPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);

        /// <summary>
        /// 填充扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillPie(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true)
        {
            g.Smooth(smooth);
            using Brush br = color.Brush();
            g.FillPie(br, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 填充扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        public static void FillPie(this Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool smooth = true)
        => g.FillPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth);

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="size">大小</param>
        public static void DrawPoint(this Graphics g, Color color, int x, int y, float size)
        => g.FillEllipse(color, x - size / 2.0f, y - size / 2.0f, size, size);

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="size">大小</param>
        public static void DrawPoint(this Graphics g, Color color, float x, float y, float size)
        => g.FillEllipse(color, x - size / 2.0f, y - size / 2.0f, size, size);

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="point">点</param>
        /// <param name="size">大小</param>
        public static void DrawPoint(this Graphics g, Color color, Point point, float size)
        => g.DrawPoint(color, point.X, point.Y, size);

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="point">点</param>
        /// <param name="size">大小</param>
        public static void DrawPoint(this Graphics g, Color color, PointF point, float size)
        => g.DrawPoint(color, point.X, point.Y, size);

        /// <summary>
        /// 绘制扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPie(this Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        => g.DrawPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth, penWidth);

        /// <summary>
        /// 绘制扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPie(this Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 绘制扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="rect">区域</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPie(this Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        => g.DrawPie(color, rect.Left, rect.Top, rect.Width, rect.Height, startAngle, sweepAngle, smooth, penWidth);

        /// <summary>
        /// 绘制扇形区域
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="startAngle">起始角度</param>
        /// <param name="sweepAngle">扫过角度</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawPie(this Graphics g, Color color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool smooth = true, float penWidth = 1)
        {
            g.Smooth(smooth);
            using Pen pen = color.Pen(penWidth);
            g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
            g.Smooth(false);
        }

        /// <summary>
        /// 九宫切图背景填充，#，http://st233.com/blog.php?id=24
        /// 例如按钮是图片分成九个区域 然后只需要将四角填充到目标区域 其余的拉伸就可以了
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="img">图片</param>
        /// <param name="rect">区域</param>
        /// <param name="angleSize">角度</param>
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
            g.DrawImage(img, new Rectangle(rect.X + angleSize, rect.Y + angleSize, rect.Width - angleSize * 2, rect.Height - angleSize * 2),
                new Rectangle(angleSize, angleSize, img.Width - angleSize * 2, img.Height - angleSize * 2), GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 九宫切图背景填充
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="img">图片</param>
        /// <param name="destWidth">目标宽度</param>
        /// <param name="destHeight">目标高度</param>
        /// <param name="cutLeft">裁剪左侧大小</param>
        /// <param name="cutRight">裁剪右侧大小</param>
        /// <param name="cutTop">裁剪顶部大小</param>
        /// <param name="cutBottom">裁剪底部大小</param>
        /// <param name="iZoom">缩放比例</param>
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

        /// <summary>
        /// 绘制两点连线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="pf1">点1</param>
        /// <param name="pf2">点2</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawTwoPoints(this Graphics g, Color color, PointF pf1, PointF pf2, Rectangle rect, bool smooth = true, float penWidth = 1)
        {
            using Pen pen = color.Pen(penWidth);
            DrawTwoPoints(g, pen, pf1, pf2, rect, smooth);
        }

        /// <summary>
        /// 绘制两点连线
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="pen">画笔</param>
        /// <param name="pf1">点1</param>
        /// <param name="pf2">点2</param>
        /// <param name="rect">区域</param>
        /// <param name="smooth">平滑</param>
        public static void DrawTwoPoints(this Graphics g, Pen pen, PointF pf1, PointF pf2, Rectangle rect, bool smooth = true)
        {
            if (pf1.X.IsNan() || pf1.Y.IsNan() || pf2.X.IsNan() || pf2.Y.IsNan()) return;
            bool haveLargePixel = Math.Abs(pf1.X - pf2.X) >= rect.Width * 100 || Math.Abs(pf1.Y - pf2.Y) >= rect.Height * 100;

            //两点都在区域内
            if (pf1.InRect(rect) && pf2.InRect(rect))
            {
                g.Smooth(smooth);
                g.DrawLine(pen, pf1, pf2);
                g.Smooth(false);
                return;
            }

            //无大坐标像素
            if (!haveLargePixel)
            {
                g.Smooth(smooth);
                g.DrawLine(pen, pf1, pf2);
                g.Smooth(false);
                return;
            }

            //垂直线
            if (pf1.X.Equals(pf2.X))
            {
                if (pf1.X <= rect.Left) return;
                if (pf1.X >= rect.Right) return;
                if (pf1.Y <= rect.Top && pf2.Y <= rect.Top) return;
                if (pf1.Y >= rect.Bottom && pf2.Y >= rect.Bottom) return;

                g.Smooth(smooth);
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

                g.Smooth(false);
                return;
            }

            //水平线
            if (pf1.Y.Equals(pf2.Y))
            {
                if (pf1.Y <= rect.Top) return;
                if (pf1.Y >= rect.Bottom) return;
                if (pf1.X <= rect.Left && pf2.X <= rect.Left) return;
                if (pf1.X >= rect.Right && pf2.X >= rect.Right) return;

                g.Smooth(smooth);
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

                g.Smooth(false);
                return;
            }

            //判断两个区域是否相交
            RectangleF rect1 = pf1.CreateRectangleF(pf2);
            if (!rect1.IsOverlap(rect)) return;

            double x1 = Drawing.CalcX(pf1, pf2, rect.Top);
            double x2 = Drawing.CalcX(pf1, pf2, rect.Bottom);
            double y1 = Drawing.CalcY(pf1, pf2, rect.Left);
            double y2 = Drawing.CalcY(pf1, pf2, rect.Right);

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
                g.Smooth(smooth);
                g.DrawLine(pen, TwoPoints[0], TwoPoints[1]);
                g.Smooth(false);
            }
            else
            {
                Console.WriteLine(TwoPoints.Count);
            }

            TwoPoints.Clear();
        }

        /// <summary>
        /// 以center为中心，绘制箭头，正北0°，顺时针0°到359°
        /// </summary>
        /// <param name="g">绘图图元</param>
        /// <param name="color">颜色</param>
        /// <param name="center">中心点</param>
        /// <param name="arrowSize">箭头尺寸</param>
        /// <param name="arrowDir">箭头方向</param>
        /// <param name="penWidth">笔宽</param>
        public static void DrawArrow(this Graphics g, Color color, PointF center, float arrowSize, float arrowDir, float penWidth = 1)
        {
            using Pen pen = color.Pen(penWidth);
            PointF pfStart = new PointF(Convert.ToSingle(center.X + arrowSize * Math.Sin(arrowDir.Rad()) / 2),
                Convert.ToSingle(center.Y - arrowSize * Math.Cos(arrowDir.Rad()) / 2));
            PointF pfEnd = new PointF(Convert.ToSingle(center.X - arrowSize * Math.Sin(arrowDir.Rad()) / 2),
                Convert.ToSingle(center.Y + arrowSize * Math.Cos(arrowDir.Rad()) / 2));

            double dAngle = arrowDir + 180 + 25;
            PointF pfArrow1 = new PointF(Convert.ToSingle(pfStart.X + arrowSize * Math.Sin(dAngle.Rad()) / 2),
                Convert.ToSingle(pfStart.Y - arrowSize * Math.Cos(dAngle.Rad()) / 2));

            dAngle = arrowDir + 180 - 25;
            PointF pfArrow2 = new PointF(Convert.ToSingle(pfStart.X + arrowSize * Math.Sin(dAngle.Rad()) / 2),
                Convert.ToSingle(pfStart.Y - arrowSize * Math.Cos(dAngle.Rad()) / 2));

            PointF[] pfPoints = { pfArrow1, pfStart, pfEnd, pfStart, pfArrow2 };
            g.DrawLines(pen, pfPoints);
        }
    }
}
