namespace Sunny.UI.Demo
{
    public partial class FAsideHeaderMain : UIAsideHeaderMainFrame
    {
        public FAsideHeaderMain()
        {
            InitializeComponent();

            //设置关联
            Aside.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new FPage1(), 1001);
            AddPage(new FPage2(), 1002);
            AddPage(new FPage3(), 1003);

            //设置Header节点索引
            Aside.CreateNode("Page1", 1001);
            Aside.CreateNode("Page2", 1002);
            Aside.CreateNode("Page3", 1003);

            //显示默认界面
            Aside.SelectFirst();
        }

        private void Aside_MenuItemClick(System.Windows.Forms.TreeNode node, NavMenuItem item, int pageIndex)
        {
            Header.Text = "PageIndex: " + pageIndex;
        }
    }
}
