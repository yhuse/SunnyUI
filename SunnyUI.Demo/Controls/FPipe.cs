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

            uiPipe4.Link(uiPipe2);
            uiPipe18.Link(uiPipe13);
            uiPipe8.Link(uiPipe9);
            uiPipe6.Link(uiPipe13);
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

        private void uiValve1_ActiveChanged(object sender, System.EventArgs e)
        {
            uiPipe8.Active = uiPipe9.Active = uiPipe7.Active = uiPipe10.Active = uiPipe12.Active = uiValve1.Active;
        }

        private void uiValve3_ActiveChanged(object sender, System.EventArgs e)
        {
            uiPipe20.Active = uiPipe3.Active = uiValve3.Active;
        }

        private void uiValve2_ActiveChanged(object sender, System.EventArgs e)
        {
            uiPipe5.Active = uiPipe11.Active = uiPipe1.Active = uiValve2.Active;
        }

        private void uiValve4_ActiveChanged(object sender, System.EventArgs e)
        {
            uiPipe22.Active = uiPipe15.Active = uiValve4.Active;
        }
    }
}
