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

        private void uiTextBox1_ButtonClick(object sender, System.EventArgs e)
        {
            ShowInfoTip("您点击了编辑框的按钮。");
        }
    }
}
