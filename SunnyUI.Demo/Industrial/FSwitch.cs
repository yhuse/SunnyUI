namespace Sunny.UI.Demo
{
    public partial class FSwitch : UIPage
    {
        public FSwitch()
        {
            InitializeComponent();
        }

        private void uiTurnSwitch1_ValueChanged(object sender, bool value)
        {
            this.ShowInfoTip(value.ToString());
        }
    }
}
