using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FMain : UIHeaderAsideMainFrame
    {
        public FMain()
        {
            InitializeComponent();
            int pageIndex = 1000;
            Header.SetNodePageIndex(Header.Nodes[0], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[0], 61451);
            TreeNode parent = Aside.CreateNode("控件", 61451, 24, pageIndex);
            //通过设置PageIndex关联，节点文字、图标由相应的Page的Text、Symbol提供
            Aside.CreateChildNode(parent, AddPage(new FAvatar(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FButton(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FCheckBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FCombobox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FContextMenuStrip(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FDataGridView(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FFlowLayoutPanel(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FHeaderButton(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FLabel(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FLine(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FListBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FNavigation(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FPanel(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FProcess(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FRadioButton(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FScrollBar(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FSplitContainer(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTabControl(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTextBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTransfer(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTreeView(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FOther(), ++pageIndex));
            //示例设置某个节点的小红点提示
            Aside.ShowTips = true;
            Aside.SetNodeTipsText(Aside.Nodes[0], "6", Color.Red, Color.White);
            Aside.SetNodeTipsText(parent.Nodes[1], " ", Color.Lime, Color.White);

            pageIndex = 2000;
            Header.SetNodePageIndex(Header.Nodes[1], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[1], 61818);
            parent = Aside.CreateNode("窗体", 61818, 24, pageIndex);
            //通过设置GUID关联，节点字体图标和大小由UIPage设置
            Aside.CreateChildNode(parent, AddPage(new FDialogs(), Guid.NewGuid()));
            Aside.CreateChildNode(parent, AddPage(new FEditor(), Guid.NewGuid()));
            Aside.CreateChildNode(parent, AddPage(new FFrames(), Guid.NewGuid()));

            pageIndex = 3000;
            Header.SetNodePageIndex(Header.Nodes[2], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[2], 61950);
            parent = Aside.CreateNode("图表", 61950, 24, pageIndex);
            //直接关联（默认自动生成GUID）
            Aside.CreateChildNode(parent, AddPage(new FBarChart()));
            Aside.CreateChildNode(parent, AddPage(new FDoughnutChart()));
            Aside.CreateChildNode(parent, AddPage(new FLineChart()));
            Aside.CreateChildNode(parent, AddPage(new FPieChart()));

            pageIndex = 4000;
            Header.SetNodePageIndex(Header.Nodes[3], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[3], 362614);
            parent = Aside.CreateNode("工控", 362614, 24, pageIndex);
            //直接关联（默认自动生成GUID）

            Aside.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FPipe")));
            Aside.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FMeter")));
            Aside.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FLed")));
            Aside.CreateChildNode(parent, AddPage(CreateInstance<UIPage>("Sunny.UI.Demo.FLight")));

            Header.SetNodeSymbol(Header.Nodes[4], 61502);
            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                Header.CreateChildNode(Header.Nodes[4], style.DisplayText(), style.Value());
            }

            Header.CreateChildNode(Header.Nodes[4], "多彩主题", UIStyle.Colorful.Value());
            //直接增加一个页面，不在左侧列表显示
            AddPage(new FColorful());
            AddPage(new FCommon());

            //选中第一个节点
            Aside.SelectPage(1002);

            Text = Version;
            RegisterHotKey(UI.ModifierKeys.Shift, Keys.F8);

            //根据页面类型获取页面
            FButton page = GetPage<FButton>();
            if (page != null)
                page.Text.WriteConsole();

            //根据页面索引获取页面
            UIPage page1 = GetPage(1002);
            if (page1 != null)
                page1.Text.WriteConsole();
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

        private void Header_MenuItemClick(string text, int menuIndex, int pageIndex)
        {
            switch (menuIndex)
            {
                case 4:
                    UIStyle style = (UIStyle)pageIndex;
                    if (style != UIStyle.Colorful)
                        StyleManager.Style = style;
                    else
                        SelectPage(pageIndex);

                    break;
                default:
                    Aside.SelectPage(pageIndex);
                    break;
            }
        }

        private void FMain_Selecting(object sender, TabControlCancelEventArgs e, UIPage page)
        {
            if (page != null)
                Console.WriteLine(page.Text);
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show(Version, "关于", Style, UIMessageBoxButtons.OK, false);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitee.com/yhuse/SunnyUI");
        }

        private void FMain_HotKeyEventHandler(object sender, HotKeyEventArgs e)
        {
            if (e.hotKey.ModifierKey == UI.ModifierKeys.Shift && e.hotKey.Key == Keys.F8)
            {
                ShowInfoTip("您按下了全局系统热键 Shift+F8");
            }
        }
    }
}