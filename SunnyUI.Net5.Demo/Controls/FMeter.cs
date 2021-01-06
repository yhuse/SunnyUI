namespace Sunny.UI.Demo
{
    public partial class FMeter : UITitlePage
    {
        public FMeter()
        {
            InitializeComponent();
        }

        private int value;

        public override void Init()
        {
            uiLedStopwatch1.Active = true;
            value = 0;
            timer1.ReStart();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            value++;
            uiRoundMeter1.Angle = value * 10;
            uiRoundMeter2.Angle = value * 10;
            uiAnalogMeter1.Value = value;
            uiBattery1.Power = value;
        }
    }
}