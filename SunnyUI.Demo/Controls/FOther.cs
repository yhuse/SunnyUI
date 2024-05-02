namespace Sunny.UI.Demo
{
    public partial class FOther : UIPage
    {
        public FOther()
        {
            InitializeComponent();
            uiToolTip1.SetToolTip(uiLabel2, "赠人玫瑰手有余香", "SunnyUI");
            uiToolTip1.SetToolTip(uiLabel3, "赠人玫瑰手有余香" + '\n' + "赠人玫瑰手有余香",
                "SunnyUI", 61530, 32, UIColor.Green);
        }

        private void uiCalendar1_OnDateTimeChanged(object sender, UIDateTimeArgs e)
        {
            this.ShowInfoTip(uiCalendar1.Date.DateString());
        }
    }
}
