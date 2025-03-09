namespace Sunny.UI.Demo
{
    public partial class FLight : UIPage
    {
        public FLight()
        {
            InitializeComponent();
        }

        public enum LightState
        {
            Green = 1,
            Yellow = 2,
            Red = 3
        }

        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            uiStatusBox1.Status = (int)LightState.Green;
        }

        private void uiButton2_Click(object sender, System.EventArgs e)
        {
            uiStatusBox1.Status = (int)LightState.Yellow;
        }

        private void uiButton3_Click(object sender, System.EventArgs e)
        {
            uiStatusBox1.Status = (int)LightState.Red;
        }
    }
}
