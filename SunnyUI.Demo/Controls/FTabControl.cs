namespace Sunny.UI.Demo
{
    public partial class FTabControl : UITitlePage
    {
        public FTabControl()
        {
            InitializeComponent();
        }

        private bool uiTabControl1_BeforeRemoveTabPage(object sender, int index)
        {
            return this.ShowAskDialog("Do you want to close the tab : " + uiTabControl1.TabPages[index].Text + "?");
        }
    }
}
