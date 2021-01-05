using System;

namespace Sunny.UI.Demo
{
    public partial class FAvatar : UITitlePage
    {
        public FAvatar()
        {
            InitializeComponent();
        }

        private void uiAvatar4_Click(object sender, EventArgs e)
        {
            uiContextMenuStrip1.Show(uiAvatar4, 0, uiAvatar4.Height);
        }
    }
}