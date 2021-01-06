namespace Sunny.UI.Demo
{
    public partial class FOther : UITitlePage
    {
        public FOther()
        {
            InitializeComponent();

            uiToolTip1.SetToolTip(uiLabel2, "赠人玫瑰手有余香", "SunnyUI");
            uiToolTip1.SetToolTip(uiLabel3, "赠人玫瑰手有余香" + '\n' + "赠人玫瑰手有余香",
                "SunnyUI", 61530, 32, UIColor.Green);
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
            uiTrackBar1.Value = value;
            uiProcessBar2.Value = uiProcessBar1.Value = value;
        }
    }
}