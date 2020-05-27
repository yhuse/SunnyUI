namespace Sunny.UI.Demo
{
    public partial class FHeaderAsideMainFooter : UIHeaderAsideMainFooterFrame
    {
        public FHeaderAsideMainFooter()
        {
            InitializeComponent();

            //设置关联
            Aside.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new FTitlePage1(), 1001);
            AddPage(new FTitlePage2(), 1002);
            AddPage(new FTitlePage3(), 1003);

            //设置Header节点索引
            Aside.CreateNode("Page1", 1001);
            Aside.CreateNode("Page2", 1002);
            Aside.CreateNode("Page3", 1003);

            //显示默认界面
            Aside.SelectFirst();
        }

        private void Aside_MenuItemClick(System.Windows.Forms.TreeNode node, NavMenuItem item, int pageIndex)
        {
            Footer.Text = "PageIndex: " + pageIndex;
        }
    }
}
