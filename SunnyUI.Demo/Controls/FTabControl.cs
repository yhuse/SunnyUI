namespace Sunny.UI.Demo
{
    public partial class FTabControl : UIPage
    {
        public FTabControl()
        {
            InitializeComponent();

            uiTabControl1.SetTipsText(tabPage2, "6");
        }

        private bool uiTabControl1_BeforeRemoveTabPage(object sender, int index)
        {
            return this.ShowAskDialog("Do you want to close the tab : " + uiTabControl1.TabPages[index].Text + "?");
        }
    }
}
