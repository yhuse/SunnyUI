using Sunny.UI.Demo.Forms;
using System.Windows.Forms;
using Sunny.UI.Demo.Controls;

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
            TreeNode parent = Aside.CreateNode("Controls", 61451, 24, pageIndex);
            Aside.CreateChildNode(parent, 61640, 24, AddPage(new FButton(), ++pageIndex));
            Aside.CreateChildNode(parent, 61490, 24, AddPage(new FLabel(), ++pageIndex));
            Aside.CreateChildNode(parent, 61770, 24, AddPage(new FCheckBox(), ++pageIndex));
            Aside.CreateChildNode(parent, 61842, 24, AddPage(new FRadioButton(), ++pageIndex));
            Aside.CreateChildNode(parent, 61962, 24, AddPage(new FTextBox(), ++pageIndex));
            Aside.CreateChildNode(parent, 61776, 24, AddPage(new FCombobox(), ++pageIndex));
            Aside.CreateChildNode(parent, 61646, 24, AddPage(new FDataGridView(), ++pageIndex));
            Aside.CreateChildNode(parent, 61474, 24, AddPage(new FListBox(), ++pageIndex));
            Aside.CreateChildNode(parent, 61499, 24, AddPage(new FTreeView(), ++pageIndex));
            Aside.CreateChildNode(parent, 61912, 24, AddPage(new FNavigation(), ++pageIndex));
            Aside.CreateChildNode(parent, 61716, 24, AddPage(new FTabControl(), ++pageIndex));
            Aside.CreateChildNode(parent, 61544, 24, AddPage(new FLine(), ++pageIndex));
            Aside.CreateChildNode(parent, 61590, 24, AddPage(new FPanel(), ++pageIndex));
            Aside.CreateChildNode(parent, 61516, 24, AddPage(new FTransfer(), ++pageIndex));
            Aside.CreateChildNode(parent, 61447, 24, AddPage(new FAvatar(), ++pageIndex));
            Aside.CreateChildNode(parent, 62104, 24, AddPage(new FContextMenuStrip(), ++pageIndex));
            Aside.CreateChildNode(parent, 61668, 24, AddPage(new FMeter(), ++pageIndex));
            Aside.CreateChildNode(parent, 62173, 24, AddPage(new FOther(), ++pageIndex));

            pageIndex = 2000;
            Header.SetNodePageIndex(Header.Nodes[1], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[1], 61818);
            parent = Aside.CreateNode("Forms", 61818, 24, pageIndex);
            Aside.CreateChildNode(parent, 62160, 24, AddPage(new FDialogs(), ++pageIndex));
            Aside.CreateChildNode(parent, 61508, 24, AddPage(new FEditor(), ++pageIndex));
            Aside.CreateChildNode(parent, 61674, 24, AddPage(new FFrames(), ++pageIndex));

            pageIndex = 3000;
            Header.SetNodePageIndex(Header.Nodes[2], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[2], 61950);
            parent = Aside.CreateNode("Forms", 61950, 24, pageIndex);
            Aside.CreateChildNode(parent, 61952, 24, AddPage(new FPieChart(), ++pageIndex));

            Header.SetNodeSymbol(Header.Nodes[3], 61502);
            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                Header.CreateChildNode(Header.Nodes[3], style.DisplayText(), style.Value());
            }

            Aside.SelectFirst();
        }

        private void Header_MenuItemClick(string text, int menuIndex, int pageIndex)
        {
            switch (menuIndex)
            {
                case 0:
                case 1:
                case 2:
                    Aside.SelectPage(pageIndex);
                    break;

                case 3:
                    UIStyle style = (UIStyle)pageIndex;
                    StyleManager.Style = style;
                    break;
            }
        }
    }
}