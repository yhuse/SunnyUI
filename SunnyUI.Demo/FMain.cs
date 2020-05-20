using Sunny.UI.Demo.Forms;
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
            TreeNode parent = Aside.CreateNode("Controls", pageIndex);
            Aside.CreateChildNode(parent, AddPage(new FButton(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FLabel(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FCheckBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FRadioButton(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTextBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FDataGridView(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FListBox(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTreeView(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FNavigation(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTabControl(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FLine(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FPanel(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FTransfer(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FAvatar(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FContextMenuStrip(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FMeter(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FOther(), ++pageIndex));

            pageIndex = 2000;
            Header.SetNodePageIndex(Header.Nodes[1], pageIndex);
            parent = Aside.CreateNode("Forms", pageIndex);
            Aside.CreateChildNode(parent, AddPage(new FDialogs(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FEditor(), ++pageIndex));
            Aside.CreateChildNode(parent, AddPage(new FFrames(), ++pageIndex));

            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                Header.CreateChildNode(Header.Nodes[2], style.DisplayText(), style.Value());
            }

            Aside.SelectFirst();
        }

        private void Header_MenuItemClick(string text, int menuIndex, int pageIndex)
        {
            switch (menuIndex)
            {
                case 0:
                case 1:
                    Aside.SelectPage(pageIndex);
                    break;

                case 2:
                    UIStyle style = (UIStyle)pageIndex;
                    StyleManager.Style = style;
                    break;
            }
        }
    }
}