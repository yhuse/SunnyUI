using System.Drawing;

namespace Sunny.UI.Demo
{
    public partial class FColorful : UIPage
    {
        public FColorful()
        {
            InitializeComponent();
        }

        private void uiPanel1_Click(object sender, System.EventArgs e)
        {
            var panel = (UIPanel)sender;
            UIStyles.InitColorful(panel.FillColor, Color.White);
        }
    }
}
