using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [ToolboxItem(false)]
    public sealed class UIColorWheel : Control, IStyleInterface, IZoomScale
    {
        public event EventHandler SelectedColorChanged;

        private Color m_frameColor = UIColor.Blue;
        private HSLColor m_selectedColor = new HSLColor(Color.BlanchedAlmond);
        private PathGradientBrush m_brush;
        private readonly List<PointF> m_path = new List<PointF>();
        private readonly List<Color> m_colors = new List<Color>();
        private double m_wheelLightness = 0.5;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            m_brush?.Dispose();
        }

        public HSLColor SelectedHSLColor
        {
            get { return m_selectedColor; }
            set
            {
                if (m_selectedColor.Equals(value))
                    return;
                Invalidate(UIColorUtil.Rect(ColorSelectorRectangle));
                m_selectedColor = value;
                SelectedColorChanged?.Invoke(this, null);
                Refresh();//Invalidate(UIColorUtil.Rect(ColorSelectorRectangle));
            }
        }

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        [Browsable(false), DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
        public Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        public void SetZoomScale(float scale)
        {

        }

        private float DefaultFontSize = -1;

        public void SetDPIScale()
        {
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (DefaultFontSize < 0) DefaultFontSize = this.Font.Size;
            this.SetDPIScaleFont(DefaultFontSize);
        }

        public void SetLightness(double lightness)
        {
            m_selectedColor.Lightness = lightness;
            Invalidate(UIColorUtil.Rect(ColorSelectorRectangle));
            SelectedColorChanged?.Invoke(this, null);
            Refresh();//Invalidate(UIColorUtil.Rect(ColorSelectorRectangle));
        }

        public Color SelectedColor
        {
            get { return m_selectedColor.Color; }
            set
            {
                if (m_selectedColor.Color != value)
                    SelectedHSLColor = new HSLColor(value);
            }
        }

        public Color FrameColor
        {
            get => m_frameColor;
            set
            {
                m_frameColor = value;
                Invalidate();
            }
        }

        public UIColorWheel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            Width = Height = 148;
            Version = UIGlobal.Version;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width != Height)
            {
                Height = Width;
            }

            using SolidBrush b = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(b, ClientRectangle);

            RectangleF wheelRectangle = WheelRectangle;
            UIColorUtil.DrawFrame(e.Graphics, wheelRectangle, 6, m_frameColor);
            wheelRectangle = ColorWheelRectangle;
            PointF center = UIColorUtil.Center(wheelRectangle);
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            if (m_brush == null)
            {
                m_brush = new PathGradientBrush(m_path.ToArray(), WrapMode.Clamp);
                m_brush.CenterPoint = center;
                m_brush.CenterColor = Color.White;
                m_brush.SurroundColors = m_colors.ToArray();
            }

            e.Graphics.FillPie(m_brush, UIColorUtil.Rect(wheelRectangle), 0, 360);
            e.Graphics.DrawEllipse(BackColor, wheelRectangle);

            DrawColorSelector(e.Graphics);

            if (Focused)
            {
                RectangleF r = WheelRectangle;
                r.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(e.Graphics, UIColorUtil.Rect(r));
            }

            e.Graphics.Smooth(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            m_brush?.Dispose();
            m_brush = null;
            ReCalcWheelPoints();
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            PointF mousePoint = new PointF(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                SetColor(mousePoint);
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            PointF mousePoint = new PointF(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                SetColor(mousePoint);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            HSLColor c = SelectedHSLColor;
            double hue = c.Hue;
            int step = 1;
            if ((keyData & Keys.Control) == Keys.Control) step = 5;
            if ((keyData & Keys.Up) == Keys.Up) hue += step;
            if ((keyData & Keys.Down) == Keys.Down) hue -= step;
            if (hue >= 360) hue = 0;
            if (hue < 0) hue = 359;

            if (hue != c.Hue)
            {
                c.Hue = hue;
                SelectedHSLColor = c;
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private RectangleF ColorSelectorRectangle
        {
            get
            {
                HSLColor color = m_selectedColor;
                double angleR = color.Hue * Math.PI / 180;
                PointF center = UIColorUtil.Center(ColorWheelRectangle);
                double radius = Radius(ColorWheelRectangle);
                radius *= color.Saturation;
                double x = center.X + Math.Cos(angleR) * radius;
                double y = center.Y - Math.Sin(angleR) * radius;
                Rectangle selectorRectangle = new Rectangle(new Point((int)x, (int)y), new Size(0, 0));
                selectorRectangle.Inflate(12, 12);
                return selectorRectangle;
            }
        }

        private void DrawColorSelector(Graphics dc)
        {
            Rectangle r = UIColorUtil.Rect(ColorSelectorRectangle);
            PointF center = UIColorUtil.Center(r);
            Image image = SelectorImages.Image(SelectorImages.eIndexes.Donut);
            dc.DrawImageUnscaled(image, (int)(center.X - image.Width / 2.0), (int)(center.Y - image.Height / 2.0));
        }

        private RectangleF WheelRectangle
        {
            get
            {
                Rectangle r = ClientRectangle;
                r.Width -= 1;
                r.Height -= 1;
                return r;
            }
        }

        private RectangleF ColorWheelRectangle
        {
            get
            {
                RectangleF r = WheelRectangle;
                r.Inflate(-5, -5);
                return r;
            }
        }

        private float Radius(RectangleF r)
        {
            return Math.Min((r.Width / 2), (r.Height / 2));
        }

        private void ReCalcWheelPoints()
        {
            m_path.Clear();
            m_colors.Clear();

            PointF center = UIColorUtil.Center(ColorWheelRectangle);
            float radius = Radius(ColorWheelRectangle);
            double angle = 0;
            double fullCircle = 360;
            double step = 5;
            while (angle < fullCircle)
            {
                double angleR = angle * (Math.PI / 180);
                double x = center.X + Math.Cos(angleR) * radius;
                double y = center.Y - Math.Sin(angleR) * radius;
                m_path.Add(new PointF((float)x, (float)y));
                m_colors.Add(new HSLColor(angle, 1, m_wheelLightness).Color);
                angle += step;
            }
        }

        private void SetColor(PointF mousePoint)
        {
            if (WheelRectangle.Contains(mousePoint) == false)
                return;

            PointF center = UIColorUtil.Center(ColorWheelRectangle);
            double radius = Radius(ColorWheelRectangle);
            double dx = Math.Abs(mousePoint.X - center.X);
            double dy = Math.Abs(mousePoint.Y - center.Y);
            double angle = Math.Atan(dy / dx) / Math.PI * 180;
            double dist = Math.Pow((Math.Pow(dx, 2) + (Math.Pow(dy, 2))), 0.5);
            double saturation = dist / radius;
            //if (dist > radius + 5) // give 5 pixels slack
            //	return;
            if (dist < 6)
                saturation = 0; // snap to center

            if (mousePoint.X < center.X)
                angle = 180 - angle;
            if (mousePoint.Y > center.Y)
                angle = 360 - angle;

            SelectedHSLColor = new HSLColor(angle, saturation, SelectedHSLColor.Lightness);
        }

        private UIStyle _style = UIStyle.Inherited;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Inherited), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        private void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style == UIStyle.Inherited ? UIStyle.Inherited : UIStyle.Custom;
        }

        public void SetInheritedStyle(UIStyle style)
        {
            SetStyle(style);
            _style = UIStyle.Inherited;
        }

        /// <summary>
        /// 设置主题样式颜色
        /// </summary>
        /// <param name="uiColor"></param>
        public void SetStyleColor(UIBaseStyle uiColor)
        {
            FrameColor = uiColor.ColorWheelFrameColor;
            BackColor = uiColor.ColorWheelBackColor;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false), Browsable(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version { get; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}