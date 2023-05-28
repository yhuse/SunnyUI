using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    internal class ColorSlider : LabelRotate
    {
        public event EventHandler SelectedValueChanged;

        private Orientation m_orientation = Orientation.Vertical;

        public Orientation Orientation
        {
            get { return m_orientation; }
            set { m_orientation = value; }
        }

        public enum eNumberOfColors
        {
            Use2Colors,
            Use3Colors,
        }

        private eNumberOfColors m_numberOfColors = eNumberOfColors.Use3Colors;

        public eNumberOfColors NumberOfColors
        {
            get { return m_numberOfColors; }
            set { m_numberOfColors = value; }
        }

        public enum eValueOrientation
        {
            MinToMax,
            MaxToMin,
        }

        private eValueOrientation m_valueOrientation = eValueOrientation.MinToMax;

        public eValueOrientation ValueOrientation
        {
            get { return m_valueOrientation; }
            set { m_valueOrientation = value; }
        }

        private float m_percent = 0;

        public float Percent
        {
            get { return m_percent; }
            set
            {
                // ok so it is not really percent, but a value between 0 - 1.
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                if (value != m_percent)
                {
                    m_percent = value;
                    if (SelectedValueChanged != null)
                        SelectedValueChanged(this, null);
                    Invalidate();
                }
            }
        }

        private Color m_color1 = Color.Black;
        private Color m_color2 = Color.FromArgb(255, 127, 127, 127);
        private Color m_color3 = Color.White;

        public Color Color1
        {
            get { return m_color1; }
            set { m_color1 = value; }
        }

        public Color Color2
        {
            get { return m_color2; }
            set { m_color2 = value; }
        }

        public Color Color3
        {
            get { return m_color3; }
            set { m_color3 = value; }
        }

        private Padding m_barPadding = new Padding(12, 5, 24, 10);

        public Padding BarPadding
        {
            get { return m_barPadding; }
            set
            {
                m_barPadding = value;
                Invalidate();
            }
        }

        public ColorSlider()
        {
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
            base.OnPaint(e);
            DrawColorBar(e.Graphics);

            if (Focused)
            {
                RectangleF lr = ClientRectangleF;
                lr.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(e.Graphics, UIColorUtil.Rect(lr));
            }
        }

        /// <summary>
        /// 重载鼠标移动事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            PointF mousepoint = new PointF(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                SetPercent(mousepoint);
        }

        /// <summary>
        /// 重载鼠标按下事件
        /// </summary>
        /// <param name="e">鼠标参数</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            PointF mousepoint = new PointF(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                SetPercent(mousepoint);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            float percent = Percent * 100;
            int step = 0;
            if ((keyData & Keys.Up) == Keys.Up)
                step = 1;
            if ((keyData & Keys.Down) == Keys.Down)
                step = -1;
            if ((keyData & Keys.Control) == Keys.Control)
                step *= 5;
            if (step != 0)
            {
                SetPercent((float)Math.Round(percent + step));
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected virtual void SetPercent(float percent)
        {
            Percent = percent / 100;
        }

        protected virtual void SetPercent(PointF mousepoint)
        {
            RectangleF cr = ClientRectangleF;
            RectangleF br = BarRectangle;
            mousepoint.X += cr.X - br.X;
            mousepoint.Y += cr.Y - br.Y;
            Percent = GetPercentSet(BarRectangle, Orientation, mousepoint);
            Refresh();
        }

        protected RectangleF BarRectangle
        {
            get
            {
                RectangleF r = ClientRectangle;
                r.X += BarPadding.Left;
                r.Width -= BarPadding.Right;
                r.Y += BarPadding.Top;
                r.Height -= BarPadding.Bottom;
                return r;
            }
        }

        protected float GetPercentSet(RectangleF r, Orientation orientation, PointF mousepoint)
        {
            float percentSet = 0;
            if (orientation == Orientation.Vertical)
            {
                if (m_valueOrientation == eValueOrientation.MaxToMin)
                    percentSet = 1 - ((mousepoint.Y - r.Y / r.Height) / r.Height);
                else
                    percentSet = mousepoint.Y / r.Height;
            }
            if (orientation == Orientation.Horizontal)
                if (m_valueOrientation == eValueOrientation.MaxToMin)
                    percentSet = 1 - ((mousepoint.X - r.X / r.Width) / r.Width);
                else
                    percentSet = (mousepoint.X / r.Width);
            if (percentSet < 0)
                percentSet = 0;
            if (percentSet > 100)
                percentSet = 100;
            return percentSet;
        }

        protected void DrawSelector(Graphics dc, RectangleF r, Orientation orientation, float percentSet)
        {
            using Pen pen = new Pen(Color.CadetBlue);
            percentSet = Math.Max(0, percentSet);
            percentSet = Math.Min(1, percentSet);
            if (orientation == Orientation.Vertical)
            {
                float selectorY = (float)Math.Floor(r.Top + (r.Height - (r.Height * percentSet)));
                if (m_valueOrientation == eValueOrientation.MaxToMin)
                    selectorY = (float)Math.Floor(r.Top + (r.Height - (r.Height * percentSet)));
                else
                    selectorY = (float)Math.Floor(r.Top + (r.Height * percentSet));

                dc.DrawLine(pen, r.X, selectorY, r.Right, selectorY);

                Image image = SelectorImages.Image(SelectorImages.eIndexes.Right);
                float xpos = r.Right;
                float ypos = selectorY - image.Height / 2;
                dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);

                image = SelectorImages.Image(SelectorImages.eIndexes.Left);
                xpos = r.Left - image.Width;
                dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
            }
            if (orientation == Orientation.Horizontal)
            {
                float selectorX = 0;
                if (m_valueOrientation == eValueOrientation.MaxToMin)
                    selectorX = (float)Math.Floor(r.Left + (r.Width - (r.Width * percentSet)));
                else
                    selectorX = (float)Math.Floor(r.Left + (r.Width * percentSet));

                dc.DrawLine(pen, selectorX, r.Top, selectorX, r.Bottom);

                Image image = SelectorImages.Image(SelectorImages.eIndexes.Up);
                float xpos = selectorX - image.Width / 2;
                float ypos = r.Bottom;
                dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);

                image = SelectorImages.Image(SelectorImages.eIndexes.Down);
                ypos = r.Top - image.Height;
                dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
            }
        }

        protected void DrawColorBar(Graphics dc)
        {
            RectangleF lr = BarRectangle;
            if (m_numberOfColors == eNumberOfColors.Use2Colors)
                UIColorUtil.Draw2ColorBar(dc, lr, Orientation, m_color1, m_color2);
            else
                UIColorUtil.Draw3ColorBar(dc, lr, Orientation, m_color1, m_color2, m_color3);
            DrawSelector(dc, lr, Orientation, (float)Percent);
        }
    }

    internal class HSLColorSlider : ColorSlider
    {
        private HSLColor m_selectedColor = new HSLColor();

        public HSLColor SelectedHSLColor
        {
            get { return m_selectedColor; }
            set
            {
                if (m_selectedColor.Equals(value))
                    return;
                m_selectedColor = value;
                value.Lightness = 0.5;
                Color2 = Color.FromArgb(255, value.Color);
                Percent = (float)m_selectedColor.Lightness;
                Refresh();//Invalidate(UIColorUtil.Rect(BarRectangle));
            }
        }

        protected override void SetPercent(PointF mousepoint)
        {
            base.SetPercent(mousepoint);
            m_selectedColor.Lightness = Percent;
            Refresh();
        }

        protected override void SetPercent(float percent)
        {
            base.SetPercent(percent);
            m_selectedColor.Lightness = percent / 100;
            SelectedHSLColor = m_selectedColor;
        }
    }
}