namespace Sunny.UI.Demo
{
    public partial class FTextBox : UIPage
    {
        public FTextBox()
        {
            InitializeComponent();
        }

        private void FTextBox_Shown(object sender, System.EventArgs e)
        {
            uiTextBox1.Focus();
        }
    }
}
