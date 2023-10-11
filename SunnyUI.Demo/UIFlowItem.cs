using System.Drawing;

namespace Sunny.UI.Demo
{
    public partial class UIFlowItem : UIUserControl
    {
        public UIFlowItem()
        {
            InitializeComponent();
        }

        private void UIFlowItem_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Color.Lime, new Rectangle(10, 10, 20, 20));
            e.Graphics.DrawString(Text, Font, ForeColor, new Rectangle(35, 0, Width, 40), ContentAlignment.MiddleLeft);
            e.Graphics.DrawLine(ForeColor, 10, 40, Width - 20, 40);
            e.Graphics.DrawString("Hello SunnyUI !", Font, ForeColor, new Rectangle(10, 40, Width, Height - 40), ContentAlignment.MiddleLeft);
        }
    }
}
