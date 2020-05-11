namespace Sunny.UI.Demo
{
    public partial class FRadioButton : UITitlePage
    {
        public FRadioButton()
        {
            InitializeComponent();
        }

        private void uiRadioButtonGroup1_ValueChanged(object sender, int index, string text)
        {
            this.ShowInfoDialog("SelectedIndex: " + index + ", SelectedText: " + text);
        }
    }
}