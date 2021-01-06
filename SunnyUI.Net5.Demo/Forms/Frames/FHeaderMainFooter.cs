namespace Sunny.UI.Demo
{
    public partial class FHeaderMainFooter : UIHeaderMainFooterFrame
    {
        public FHeaderMainFooter()
        {
            InitializeComponent();

            //设置关联
            Header.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new FTitlePage1(), 1001);
            AddPage(new FTitlePage2(), 1002);
            AddPage(new FTitlePage3(), 1003);

            //设置Header节点索引
            Header.CreateNode("Page1", 1001);
            Header.CreateNode("Page2", 1002);
            Header.CreateNode("Page3", 1003);

            //显示默认界面
            Header.SelectedIndex = 0;
        }

        private void Header_MenuItemClick(string text, int menuIndex, int pageIndex)
        {
            Footer.Text = "PageIndex: " + pageIndex;
        }
    }
}
