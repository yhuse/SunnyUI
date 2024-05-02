using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FMain : UIForm2
    {
        public FMain()
        {
            InitializeComponent();

            int pageIndex = 1000;

            //uiNavBar1设置节点，也可以在Nodes属性里配置
            uiNavBar1.Nodes.Add("控件");
            uiNavBar1.Nodes.Add("窗体");
            uiNavBar1.Nodes.Add("图表");
            uiNavBar1.Nodes.Add("工控");
            uiNavBar1.Nodes.Add("主题");
            uiNavBar1.SetNodePageIndex(uiNavBar1.Nodes[0], pageIndex);
            uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[0], 61451);
            TreeNode parent = uiNavMenu1.CreateNode("控件", 61451, 24, pageIndex);

            //通过设置PageIndex关联，节点文字、图标由相应的Page的Text、Symbol提供
            uiNavMenu1.CreateChildNode(parent, AddPage(new FAvatar(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FButton(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FCheckBox(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FCombobox(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FContextMenuStrip(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FDataGridView(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FFlowLayoutPanel(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FHeaderButton(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FLabel(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FLine(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FListBox(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FNavigation(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FPanel(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FProcess(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FRadioButton(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FScrollBar(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FSplitContainer(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FTabControl(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FTextBox(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FTransfer(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FTreeView(), ++pageIndex));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FOther(), ++pageIndex));
            //示例设置某个节点的小红点提示
            uiNavMenu1.ShowTips = true;
            uiNavMenu1.SetNodeTipsText(uiNavMenu1.Nodes[0], "6", Color.Red, Color.White);
            uiNavMenu1.SetNodeTipsText(parent.Nodes[1], " ", Color.Lime, Color.White);

            pageIndex = 2000;
            uiNavBar1.SetNodePageIndex(uiNavBar1.Nodes[1], pageIndex);
            uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[1], 61818);
            parent = uiNavMenu1.CreateNode("窗体", 61818, 24, pageIndex);
            //通过设置GUID关联，节点字体图标和大小由UIPage设置
            uiNavMenu1.CreateChildNode(parent, AddPage(new FDialogs(), Guid.NewGuid()));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FEditor(), Guid.NewGuid()));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FFrames(), Guid.NewGuid()));

            pageIndex = 3000;
            uiNavBar1.SetNodePageIndex(uiNavBar1.Nodes[2], pageIndex);
            uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[2], 61950);
            parent = uiNavMenu1.CreateNode("图表", 61950, 24, pageIndex);
            //直接关联（默认自动生成GUID）
            uiNavMenu1.CreateChildNode(parent, AddPage(new FBarChart()));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FDoughnutChart()));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FLineChart()));
            uiNavMenu1.CreateChildNode(parent, AddPage(new FPieChart()));

            pageIndex = 4000;
            uiNavBar1.SetNodePageIndex(uiNavBar1.Nodes[3], pageIndex);
            uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[3], 362614);
            parent = uiNavMenu1.CreateNode("工控", 362614, 24, pageIndex);
            //直接关联（默认自动生成GUID）

            uiNavMenu1.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FPipe")));
            uiNavMenu1.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FMeter")));
            uiNavMenu1.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FLed")));
            uiNavMenu1.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FLight")));
            uiNavMenu1.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FSwitch")));

            uiNavBar1.SetNodeSymbol(uiNavBar1.Nodes[4], 61502);
            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                uiNavBar1.CreateChildNode(uiNavBar1.Nodes[4], style.DisplayText(), style.Value());
            }

            var node = uiNavBar1.CreateChildNode(uiNavBar1.Nodes[4], "字体图标", 99999);
            uiNavBar1.SetNodeSymbol(node, 558426);
            node = uiNavBar1.CreateChildNode(uiNavBar1.Nodes[4], "多彩主题", UIStyle.Colorful.Value());
            uiNavBar1.SetNodeSymbol(node, 558295);
            //左侧导航主节点关联页面
            uiNavMenu1.CreateNode(AddPage(new FSymbols()));
            uiNavMenu1.CreateNode(AddPage(new FColorful()));

            //直接增加一个页面，不在左侧列表显示
            //AddPage(new FColorful());
            AddPage(new FCommon());

            //选中第一个节点
            uiNavMenu1.SelectPage(1002);

            uiPanel2.Text = Text = Version;
            RegisterHotKey(UI.ModifierKeys.Shift, Keys.F8);

            //根据页面类型获取页面
            FButton page = GetPage<FButton>();
            if (page != null)
                page.Text.WriteConsole();

            //根据页面索引获取页面
            UIPage page1 = GetPage(1002);
            if (page1 != null)
                page1.Text.WriteConsole();

            timer1.Start();
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName)
        {
            Type o = Type.GetType(fullName);
            dynamic obj = Activator.CreateInstance(o, true);
            return (T)obj;//类型转换并返回
        }

        private void uiNavBar1_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {
            switch (menuIndex)
            {
                case 4:
                    UIStyle style = (UIStyle)pageIndex;
                    if (pageIndex < UIStyle.Colorful.Value())
                        StyleManager.Style = style;
                    else
                        uiNavMenu1.SelectPage(pageIndex);
                    break;
                default:
                    uiNavMenu1.SelectPage(pageIndex);
                    break;
            }
        }

        private void Form1_PageSelected(object sender, UIPageEventArgs e)
        {
            if (e.Page != null)
                Console.WriteLine(e.Page.Text);
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show(Version, "关于", Style, UIMessageBoxButtons.OK, false);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitee.com/yhuse/SunnyUI");
        }

        private void Form1_HotKeyEventHandler(object sender, HotKeyEventArgs e)
        {
            if (e.hotKey.ModifierKey == UI.ModifierKeys.Shift && e.hotKey.Key == Keys.F8)
            {
                this.ShowInfoTip("您按下了全局系统热键 Shift+F8");
            }
        }

        private void Form1_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            Text = e.Value.ToString();
            SendParamToPage(1001, "传值给页面");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            uiPanel3.Text = DateTime.Now.DateTimeString();
        }
    }
}
