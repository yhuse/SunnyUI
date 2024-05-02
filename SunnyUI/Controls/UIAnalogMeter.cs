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
 * 文件名称: UIAnalogMeter.cs
 * 文件说明: 仪表
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable 1591

namespace Sunny.UI
{
    /// <summary>
    /// Class for the analog meter control
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class UIAnalogMeter : UIControl
    {
        #region Enumerator

        public enum AnalogMeterStyle
        {
            Circular = 0,
        };

        #endregion Enumerator

        #region Properties variables

        private AnalogMeterStyle meterStyle;
        private Color needleColor;
        private Color scaleColor;
        private bool viewGlass;
        private double currValue;
        private double minValue;
        private double maxValue;
        private int scaleDivisions;
        private int scaleSubDivisions;
        private LBAnalogMeterRenderer renderer;

        #endregion Properties variables

        #region Class variables

        protected PointF needleCenter;
        protected RectangleF drawRect;
        protected RectangleF glossyRect;
        protected RectangleF needleCoverRect;
        protected float startAngle;
        protected float endAngle;
        protected float drawRatio;
        protected LBAnalogMeterRenderer defaultRenderer;

        #endregion Class variables

        #region Costructors

        public UIAnalogMeter()
        {
            SetStyleFlags(true, false, true);

            // Properties initialization
            needleColor = Color.Yellow;
            scaleColor = Color.White;
            meterStyle = AnalogMeterStyle.Circular;
            viewGlass = false;
            startAngle = 135;
            endAngle = 405;
            minValue = 0;
            maxValue = 100;
            currValue = 0;
            scaleDivisions = 11;
            scaleSubDivisions = 4;
            renderer = null;

            // Set the styles for drawing
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            // Create the default renderer
            defaultRenderer = new LBDefaultAnalogMeterRenderer();
            defaultRenderer.AnalogMeter = this;

            Width = Height = 180;
        }

        #endregion Costructors

        #region Properties

        [
            Category("SunnyUI"),
            Description("Style of the control"),
            DefaultValue(AnalogMeterStyle.Circular)
        ]
        public AnalogMeterStyle MeterStyle
        {
            get { return meterStyle; }
            set
            {
                meterStyle = value;
                Invalidate();
            }
        }

        [
            Category("SunnyUI"),
            Description("Color of the body of the control"),
            DefaultValue(typeof(Color), "80, 160, 255")
        ]
        public Color BodyColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        [
            Category("SunnyUI"),
            Description("Color of the needle"),
            DefaultValue(typeof(Color), "Yellow")
        ]
        public Color NeedleColor
        {
            get { return needleColor; }
            set
            {
                needleColor = value;
                Invalidate();
            }
        }

        [
            Category("SunnyUI"),
            Description("Show or hide the glass effect"),
            DefaultValue(false)
        ]
        public bool ViewGlass
        {
            get { return viewGlass; }
            set
            {
                viewGlass = value;
                Invalidate();
            }
        }

        [
            Category("SunnyUI"),
            Description("Color of the scale of the control"),
            DefaultValue(typeof(Color), "White")
        ]
        public Color ScaleColor
        {
            get { return scaleColor; }
            set
            {
                scaleColor = value;
                Invalidate();
            }
        }

        public event EventHandler ValueChanged;

        [
            Category("Behavior"),
            Description("Value of the data"),
            DefaultValue(0)
        ]
        public double Value
        {
            get { return currValue; }
            set
            {
                double val = value;
                if (val > maxValue)
                    val = maxValue;

                if (val < minValue)
                    val = minValue;

                if (currValue != val)
                {
                    currValue = val;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [
            Category("Behavior"),
            Description("Minimum value of the data"),
            DefaultValue(0)
        ]
        public double MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                Invalidate();
            }
        }

        [
            Category("Behavior"),
            Description("Maximum value of the data"),
            DefaultValue(100)
        ]
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                Invalidate();
            }
        }

        [
            Category("SunnyUI"),
            Description("Number of the scale divisions"),
            DefaultValue(11)
        ]
        public int ScaleDivisions
        {
            get { return scaleDivisions; }
            set
            {
                scaleDivisions = value;
                CalculateDimensions();
                Invalidate();
            }
        }

        [
            Category("SunnyUI"),
            Description("Number of the scale subdivisions"),
            DefaultValue(4)
        ]
        public int ScaleSubDivisions
        {
            get { return scaleSubDivisions; }
            set
            {
                scaleSubDivisions = value;
                CalculateDimensions();
                Invalidate();
            }
        }

        [Browsable(false)]
        public LBAnalogMeterRenderer Renderer
        {
            get { return renderer; }
            set
            {
                renderer = value;
                if (renderer != null)
                    renderer.AnalogMeter = this;
                Invalidate();
            }
        }

        #endregion Properties

        #region Public methods

        public float GetDrawRatio()
        {
            return drawRatio;
        }

        public float GetStartAngle()
        {
            return startAngle;
        }

        public float GetEndAngle()
        {
            return endAngle;
        }

        public PointF GetNeedleCenter()
        {
            return needleCenter;
        }

        #endregion Public methods

        #region Events delegates

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // Calculate dimensions
            CalculateDimensions();
            Invalidate();
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width <= 0 || Height <= 0) return;
            RectangleF _rc = new RectangleF(0, 0, Width, Height);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (renderer == null)
            {
                defaultRenderer.DrawBackground(e.Graphics, _rc);
                defaultRenderer.DrawBody(e.Graphics, drawRect);
                defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                defaultRenderer.DrawUM(e.Graphics, drawRect);
                defaultRenderer.DrawValue(e.Graphics, drawRect);
                defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                defaultRenderer.DrawNeedleCover(e.Graphics, needleCoverRect);
                defaultRenderer.DrawGlass(e.Graphics, glossyRect);
            }
            else
            {
                if (Renderer.DrawBackground(e.Graphics, _rc) == false)
                    defaultRenderer.DrawBackground(e.Graphics, _rc);
                if (Renderer.DrawBody(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawBody(e.Graphics, drawRect);
                if (Renderer.DrawThresholds(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawThresholds(e.Graphics, drawRect);
                if (Renderer.DrawDivisions(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawDivisions(e.Graphics, drawRect);
                if (Renderer.DrawUM(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawUM(e.Graphics, drawRect);
                if (Renderer.DrawValue(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawValue(e.Graphics, drawRect);
                if (Renderer.DrawNeedle(e.Graphics, drawRect) == false)
                    defaultRenderer.DrawNeedle(e.Graphics, drawRect);
                if (Renderer.DrawNeedleCover(e.Graphics, needleCoverRect) == false)
                    defaultRenderer.DrawNeedleCover(e.Graphics, needleCoverRect);
                if (Renderer.DrawGlass(e.Graphics, glossyRect) == false)
                    defaultRenderer.DrawGlass(e.Graphics, glossyRect);
            }
        }

        #endregion Events delegates

        #region Virtual functions

        protected virtual void CalculateDimensions()
        {
            // Rectangle
            float x = 0;
            float y = 0;
            float w = Width;
            float h = Height;

            // Calculate ratio
            drawRatio = (Math.Min(w, h)) / 200;
            if (drawRatio < 0.00000001)
                drawRatio = 1;

            // Draw rectangle
            drawRect.X = x;
            drawRect.Y = y;
            drawRect.Width = w - 2;
            drawRect.Height = h - 2;

            if (w < h)
                drawRect.Height = w;
            else if (w > h)
                drawRect.Width = h;

            if (drawRect.Width < 10)
                drawRect.Width = 10;
            if (drawRect.Height < 10)
                drawRect.Height = 10;

            // Calculate needle center
            needleCenter.X = drawRect.X + (drawRect.Width / 2);
            needleCenter.Y = drawRect.Y + (drawRect.Height / 2);

            // Needle cover rect
            needleCoverRect.X = needleCenter.X - (20 * drawRatio);
            needleCoverRect.Y = needleCenter.Y - (20 * drawRatio);
            needleCoverRect.Width = 40 * drawRatio;
            needleCoverRect.Height = 40 * drawRatio;

            // Glass effect rect
            glossyRect.X = drawRect.X + (20 * drawRatio);
            glossyRect.Y = drawRect.Y + (10 * drawRatio);
            glossyRect.Width = drawRect.Width - (40 * drawRatio);
            glossyRect.Height = needleCenter.Y + (30 * drawRatio);
        }

        #endregion Virtual functions
    }
}