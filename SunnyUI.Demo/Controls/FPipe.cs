using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FPipe : UIPage
    {
        public FPipe()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Color[] surroundColor = new Color[] { Color.Silver };
            GraphicsPath CirclePath = new GraphicsPath();
            CirclePath.AddEllipse(0, 0, 36, 38);
            PathGradientBrush gradientBrush = new PathGradientBrush(CirclePath);
            gradientBrush.CenterPoint = new PointF(18, 18);
            gradientBrush.CenterColor = Color.White;
            gradientBrush.SurroundColors = surroundColor;
            e.Graphics.SetHighQuality();
            e.Graphics.FillPath(gradientBrush, CirclePath);
            e.Graphics.SetDefaultQuality();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            foreach (var pipe in this.GetControls<UIPipe>())
            {
                pipe.Invalidate();
            }
        }
    }
}
