using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FMeter : UIPage
    {
        public FMeter()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            value++;
            uiRoundMeter1.Angle = value * 10;
            uiRoundMeter2.Angle = value * 10;
            uiAnalogMeter1.Value = value;

            uiThermometer1.Value = value;
            uiThermometer2.Value = value;
        }

        Timer timer = new Timer();
        private int value;

        public override void Init()
        {
            value = 0;
            timer.ReStart();
        }
    }
}
