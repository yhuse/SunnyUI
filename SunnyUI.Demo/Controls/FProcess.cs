namespace Sunny.UI.Demo
{
    public partial class FProcess : UITitlePage
    {
        public FProcess()
        {
            InitializeComponent();
        }

        private int value;

        public override void Init()
        {
            value = 0;
            timer1.ReStart();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            value++;
            uiTrackBar2.Value = uiTrackBar1.Value = value;
            uiProcessBar2.Value = uiProcessBar1.Value = value;
            uiRoundProcess2.Value = uiRoundProcess1.Value = value;
        }
    }
}
