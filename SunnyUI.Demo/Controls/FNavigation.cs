namespace Sunny.UI.Demo
{
    public partial class FNavigation : UIPage
    {
        public FNavigation()
        {
            InitializeComponent();
            uiNavBar1.SetNodeItem(uiNavBar1.Nodes[0].Nodes[0], new NavMenuItem(100));
            uiNavBar1.SetNodeItem(uiNavBar1.Nodes[0].Nodes[1], new NavMenuItem(101));
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
