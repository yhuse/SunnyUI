namespace Sunny.UI.Demo
{
    public partial class FAvatar : UIPage
    {
        public FAvatar()
        {
            InitializeComponent();
        }

        private void uiAvatar4_Click(object sender, System.EventArgs e)
        {
            uiContextMenuStrip1.Show(uiAvatar4, 0, uiAvatar4.Height);
        }
    }
}
