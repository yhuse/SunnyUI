using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("NodeSelected")]
    [DefaultProperty("Nodes")]
    [ToolboxItem(true)]
    public class UIComboTreeView : UIDropControl
    {
        public UIComboTreeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UIComboTreeView
            // 
            this.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.Name = "UIComboTreeView";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.ButtonClick += new System.EventHandler(this.UIComboTreeView_ButtonClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (item != null) item.TreeView.Font = Font;
        }

        [Browsable(false)]
        public UITreeView TreeView => item.TreeView;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        [Description("显示节点集合"), Category("SunnyUI")]
        public TreeNodeCollection Nodes => item.TreeView.Nodes;

        [DefaultValue(false), Description("是否显示单选框"), Category("SunnyUI")]
        public bool CheckBoxes
        {
            get => item.CheckBoxes;
            set => item.CheckBoxes = value;
        }

        [DefaultValue(false), Description("是否显示连线"), Category("SunnyUI")]
        public bool ShowLines
        {
            get => item.TreeView.ShowLines;
            set => item.TreeView.ShowLines = value;
        }

        private readonly UIComboTreeViewItem item = new UIComboTreeViewItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        [Browsable(false), DefaultValue(null)]
        public TreeNode SelectedNode
        {
            get => item.TreeView.SelectedNode;
            set
            {
                item.TreeView.SelectedNode = value;
                Text = value.Text;
            }
        }

        public delegate void OnNodeSelected(object sender, TreeNode node);
        public delegate void OnNodesSelected(object sender, TreeNodeCollection node);

        public event OnNodeSelected NodeSelected;
        public event OnNodesSelected NodesSelected;

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            if (!CheckBoxes)
            {
                TreeNode node = (TreeNode)value;
                Text = node.Text;
                NodeSelected?.Invoke(this, node);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (TreeNode node in Nodes)
                {
                    if (node.Checked) sb.Append(node.Text + "; ");
                    AddChildNodeText(node, sb);
                }

                Text = sb.ToString();
                NodesSelected?.Invoke(this, Nodes);
            }

            Invalidate();
        }

        private void AddChildNodeText(TreeNode node, StringBuilder sb)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Checked)
                        sb.Append(child.Text + "; ");

                    if (child.Nodes.Count > 0)
                        AddChildNodeText(child, sb);
                }
            }
        }

        private void UIComboTreeView_ButtonClick(object sender, System.EventArgs e)
        {
            ItemForm.Size = ItemSize;
            item.TreeView.ExpandAll();
            ItemForm.Show(this);
        }

        [DefaultValue(typeof(Size), "250, 220"), Description("下拉弹框界面大小"), Category("SunnyUI")]
        public Size ItemSize { get; set; } = new Size(250, 220);
    }
}
