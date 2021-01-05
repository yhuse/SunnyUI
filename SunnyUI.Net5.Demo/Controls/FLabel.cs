using System.Diagnostics;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FLabel : UITitlePage
    {
        public FLabel()
        {
            InitializeComponent();
        }

        private void uiLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(uiLinkLabel1.Text);
        }
    }
}