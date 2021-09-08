namespace Sunny.UI.Demo
{
    public partial class FMeter : UIPage
    {
        public FMeter()
        {
            InitializeComponent();
        }

        private int value;

        public override void Init()
        {
            value = 0;
            uiMillisecondTimer1.ReStart();
        }

        private void uiMillisecondTimer1_Tick(object sender, System.EventArgs e)
        {
            value++;
            uiRoundMeter1.Angle = value * 10;
            uiRoundMeter2.Angle = value * 10;
            uiAnalogMeter1.Value = value;
        }
    }
}
