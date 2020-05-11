namespace Sunny.UI.Demo
{
    public partial class FOther : UITitlePage
    {
        public FOther()
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
            uiTrackBar1.Value = value;
            uiProcessBar1.Value = value;
            uiScrollBar1.Value = value;
        }
    }
}