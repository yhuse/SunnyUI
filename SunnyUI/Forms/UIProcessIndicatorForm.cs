namespace Sunny.UI
{
    public partial class UIProcessIndicatorForm : UIForm
    {
        public UIProcessIndicatorForm()
        {
            InitializeComponent();
            timer1.Start();
            uiProgressIndicator1.Active = true;
        }

        private void uiProgressIndicator1_Tick(object sender, System.EventArgs e)
        {
            if (UIFormServiceHelper.ProcessFormServiceClose)
            {
                UIFormServiceHelper.ProcessFormServiceClose = false;
                uiProgressIndicator1.Active = false;
                Close();
            }
        }
    }
}
