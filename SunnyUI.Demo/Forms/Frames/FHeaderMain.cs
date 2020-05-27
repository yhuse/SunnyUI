namespace Sunny.UI.Demo
{
    public partial class FHeaderMain : UIHeaderMainFrame
    {
        public FHeaderMain()
        {
            InitializeComponent();

            //设置关联
            Header.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new FTitlePage1(), 1001);
            AddPage(new FTitlePage2(), 1002);
            AddPage(new FTitlePage3(), 1003);

            //设置Header节点索引
            Header.SetNodePageIndex(Header.Nodes[0], 1001);
            Header.SetNodePageIndex(Header.Nodes[1], 1002);
            Header.SetNodePageIndex(Header.Nodes[2], 1003);

            //显示默认界面
            Header.SelectedIndex = 0;
        }
    }
}