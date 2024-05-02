/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIAnalogMeterRender.cs
 * 文件说明: 仪表渲染类
 * 文件作者: Luca Bonotto
 * 开源协议: CPOL
 * 引用地址: https://www.codeproject.com/Articles/24945/Analog-Meter
******************************************************************************/

/*
 * Creato da SharpDevelop.
 * Utente: lucabonotto
 * Data: 03/04/2008
 * Ora: 14.34
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#pragma warning disable 1591

namespace Sunny.UI
{
    /// <summary>
    /// Base class for the renderers of the analog meter
    /// </summary>
    public class LBAnalogMeterRenderer
    {
        #region Variables

        /// <summary>
        /// Control to render
        /// </summary>
        private UIAnalogMeter meter;

        #endregion Variables

        #region Properies

        public UIAnalogMeter AnalogMeter
        {
            set { this.meter = value; }
            get { return this.meter; }
        }

        #endregion Properies

        #region Virtual method

        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBody(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the scale of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the thresholds
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawThresholds(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Drawt the unit measure of the control
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawUM(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the current value in numerical form
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawValue(Graphics gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the needle
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawNeedle(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the needle cover at the center
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawNeedleCover(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        /// <summary>
        /// Draw the glass effect
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            return false;
        }

        #endregion Virtual method
    }

    /// <summary>
    /// Default renderer class for the analog meter
    /// </summary>
    public class LBDefaultAnalogMeterRenderer : LBAnalogMeterRenderer
    {
        public override bool DrawBackground(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public override bool DrawBody(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            Color bodyColor = this.AnalogMeter.BodyColor;
            Color cDark = LBColorManager.StepColor(bodyColor, 20);
            using LinearGradientBrush br1 = new LinearGradientBrush(rc, bodyColor, cDark, 45);
            Gr.FillEllipse(br1, rc);

            float drawRatio = this.AnalogMeter.GetDrawRatio();

            RectangleF _rc = rc;
            _rc.X += 3 * drawRatio;
            _rc.Y += 3 * drawRatio;
            _rc.Width -= 6 * drawRatio;
            _rc.Height -= 6 * drawRatio;

            using LinearGradientBrush br2 = new LinearGradientBrush(_rc, cDark, bodyColor, 45);
            Gr.FillEllipse(br2, _rc);

            return true;
        }

        public override bool DrawThresholds(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public override bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            PointF needleCenter = this.AnalogMeter.GetNeedleCenter();
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            float scaleDivisions = this.AnalogMeter.ScaleDivisions;
            float scaleSubDivisions = this.AnalogMeter.ScaleSubDivisions;
            float drawRatio = this.AnalogMeter.GetDrawRatio();
            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            Color scaleColor = this.AnalogMeter.ScaleColor;

            float cx = needleCenter.X;
            float cy = needleCenter.Y;
            float w = rc.Width;
            float h = rc.Height;

            float incr = LBMath.GetRadian((endAngle - startAngle) / ((scaleDivisions - 1) * (scaleSubDivisions + 1)));
            float currentAngle = LBMath.GetRadian(startAngle);
            float radius = (float)(w / 2 - (w * 0.08));
            float rulerValue = (float)minValue;

            using Pen pen = new Pen(scaleColor, (2 * drawRatio));
            using SolidBrush br = new SolidBrush(scaleColor);

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);
            int n = 0;
            for (; n < scaleDivisions; n++)
            {
                //Draw Thick Line
                ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
                ptEnd.X = (float)(cx + (radius - w / 20) * Math.Cos(currentAngle));
                ptEnd.Y = (float)(cy + (radius - w / 20) * Math.Sin(currentAngle));
                Gr.DrawLine(pen, ptStart, ptEnd);

                //Draw Strings
                using Font font = new Font(this.AnalogMeter.Font.FontFamily, (float)(6F * drawRatio));

                float tx = (float)(cx + (radius - (20 * drawRatio)) * Math.Cos(currentAngle));
                float ty = (float)(cy + (radius - (20 * drawRatio)) * Math.Sin(currentAngle));
                double val = Math.Round(rulerValue);
                String str = String.Format("{0,0:D}", (int)val);
                Gr.DrawString(str, font, scaleColor, new Rectangle((int)(tx - w), (int)(ty - h), (int)(w * 2), (int)(h * 2)), ContentAlignment.MiddleCenter);

                rulerValue += (float)((maxValue - minValue) / (scaleDivisions - 1));

                if (n == scaleDivisions - 1)
                    break;

                if (scaleDivisions <= 0)
                    currentAngle += incr;
                else
                {
                    for (int j = 0; j <= scaleSubDivisions; j++)
                    {
                        currentAngle += incr;
                        ptStart.X = (float)(cx + radius * Math.Cos(currentAngle));
                        ptStart.Y = (float)(cy + radius * Math.Sin(currentAngle));
                        ptEnd.X = (float)(cx + (radius - w / 50) * Math.Cos(currentAngle));
                        ptEnd.Y = (float)(cy + (radius - w / 50) * Math.Sin(currentAngle));
                        Gr.DrawLine(pen, ptStart, ptEnd);
                    }
                }
            }

            return true;
        }

        public override bool DrawUM(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public override bool DrawValue(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public override bool DrawNeedle(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            float w, h;
            w = rc.Width;
            h = rc.Height;

            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            double currValue = this.AnalogMeter.Value;
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            PointF needleCenter = this.AnalogMeter.GetNeedleCenter();

            float radius = (float)(w / 2 - (w * 0.12));
            float val = (float)(maxValue - minValue);

            val = (float)((100 * (currValue - minValue)) / val);
            val = ((endAngle - startAngle) * val) / 100;
            val += startAngle;

            float angle = LBMath.GetRadian(val);

            float cx = needleCenter.X;
            float cy = needleCenter.Y;

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);

            using GraphicsPath pth1 = new GraphicsPath();

            ptStart.X = cx;
            ptStart.Y = cy;
            angle = LBMath.GetRadian(val + 10);
            ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
            ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            ptStart = ptEnd;
            angle = LBMath.GetRadian(val);
            ptEnd.X = (float)(cx + radius * Math.Cos(angle));
            ptEnd.Y = (float)(cy + radius * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            ptStart = ptEnd;
            angle = LBMath.GetRadian(val - 10);
            ptEnd.X = (float)(cx + (w * .09F) * Math.Cos(angle));
            ptEnd.Y = (float)(cy + (w * .09F) * Math.Sin(angle));
            pth1.AddLine(ptStart, ptEnd);

            pth1.CloseFigure();

            using SolidBrush br = new SolidBrush(this.AnalogMeter.NeedleColor);
            using Pen pen = new Pen(this.AnalogMeter.NeedleColor);
            Gr.DrawPath(pen, pth1);
            Gr.FillPath(br, pth1);

            return true;
        }

        public override bool DrawNeedleCover(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            Color clr = this.AnalogMeter.NeedleColor;
            RectangleF _rc = rc;
            float drawRatio = this.AnalogMeter.GetDrawRatio();

            Color clr1 = Color.FromArgb(70, clr);

            _rc.Inflate(5 * drawRatio, 5 * drawRatio);

            using SolidBrush brTransp = new SolidBrush(clr1);
            Gr.FillEllipse(brTransp, _rc);

            clr1 = clr;
            Color clr2 = LBColorManager.StepColor(clr, 75);
            using LinearGradientBrush br1 = new LinearGradientBrush(rc, clr1, clr2, 45);
            Gr.FillEllipse(br1, rc);
            return true;
        }

        public override bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
                return false;

            if (this.AnalogMeter.ViewGlass == false)
                return true;

            Color clr1 = Color.FromArgb(40, 200, 200, 200);
            Color clr2 = Color.FromArgb(0, 200, 200, 200);
            using LinearGradientBrush br1 = new LinearGradientBrush(rc, clr1, clr2, 45);
            Gr.FillEllipse(br1, rc);

            return true;
        }
    }

    /// <summary>
    /// Manager for color
    /// </summary>
    public class LBColorManager : Object
    {
        public static double BlendColour(double fg, double bg, double alpha)
        {
            double result = bg + (alpha * (fg - bg));
            if (result < 0.0)
                result = 0.0;
            if (result > 255)
                result = 255;
            return result;
        }

        public static Color StepColor(Color clr, int alpha)
        {
            if (alpha == 100)
                return clr;

            byte a = clr.A;
            byte r = clr.R;
            byte g = clr.G;
            byte b = clr.B;
            float bg = 0;

            int _alpha = Math.Min(alpha, 200);
            _alpha = Math.Max(alpha, 0);
            double ialpha = ((double)(_alpha - 100.0)) / 100.0;

            if (ialpha > 100)
            {
                // blend with white
                bg = 255.0F;
                ialpha = 1.0F - ialpha;  // 0 = transparent fg; 1 = opaque fg
            }
            else
            {
                // blend with black
                bg = 0.0F;
                ialpha = 1.0F + ialpha;  // 0 = transparent fg; 1 = opaque fg
            }

            r = (byte)(LBColorManager.BlendColour(r, bg, ialpha));
            g = (byte)(LBColorManager.BlendColour(g, bg, ialpha));
            b = (byte)(LBColorManager.BlendColour(b, bg, ialpha));

            return Color.FromArgb(a, r, g, b);
        }
    }

    /// <summary>
    /// Mathematic Functions
    /// </summary>
    public class LBMath : Object
    {
        public static float GetRadian(float val)
        {
            return (float)(val * Math.PI / 180);
        }
    }
}