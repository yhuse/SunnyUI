namespace Sunny.UI.Demo
{
    public partial class FTextBox : UIPage
    {
        public FTextBox()
        {
            InitializeComponent();

            uiTextBox1.SetTipsText(uiToolTip1, "Hello World.");
            uiTextBox5.SetTipsText(uiToolTip1, "Hello World.");
        }

        private void FTextBox_Shown(object sender, System.EventArgs e)
        {
            uiTextBox1.Focus();
        }

        private void uiTextBox1_ButtonClick(object sender, System.EventArgs e)
        {
            this.ShowInfoTip("您点击了编辑框的按钮。");
        }
    }
}
