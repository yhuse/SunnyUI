namespace Sunny.UI.Demo
{
    public partial class FNavigation : UIPage
    {
        public FNavigation()
        {
            InitializeComponent();
        }

        private void uiNavBar1_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {
            UIMessageTip.ShowOk(itemText + ", " + menuIndex + ", " + pageIndex);
        }

        private void uiNavMenu1_MenuItemClick(System.Windows.Forms.TreeNode node, NavMenuItem item, int pageIndex)
        {
            UIMessageTip.ShowOk(node.Text + ", " + pageIndex);
        }
    }
}
