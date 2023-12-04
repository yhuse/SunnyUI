namespace Sunny.UI.Demo
{
    public partial class FLed : UIPage
    {
        public FLed()
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
            uiBattery1.Power = value;
            uiLedDisplay1.Text = value + " Ω";
            uiDigitalLabel2.Value += 0.1;
        }
    }
}
