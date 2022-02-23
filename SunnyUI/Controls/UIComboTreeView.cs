/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIComboTreeView.cs
 * 文件说明: 树形列表框
 * 当前版本: V3.1
 * 创建日期: 2020-11-11
 *
 * 2021-07-29: V3.0.5 修复SelectedNode=null的问题
 * 2020-11-11: V3.0.0 增加文件说明
******************************************************************************/

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
    public class UIComboTreeView : UIDropControl, IToolTip
    {
        public UIComboTreeView()
        {
            InitializeComponent();
            fullControlSelect = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UIComboTreeView
            // 
            this.DropDownStyle = UIDropDownStyle.DropDownList;
            this.Name = "UIComboTreeView";
            this.Padding = new Padding(0, 0, 30, 0);
            this.ButtonClick += this.UIComboTreeView_ButtonClick;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Control ExToolTipControl()
        {
            return edit;
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

        [DefaultValue(false), Description("是否显示单选框,该属性与CanSelectRootNode互斥"), Category("SunnyUI")]
        public bool CheckBoxes
        {
            get => item.CheckBoxes;
            set
            {
                item.CheckBoxes = value;
                if (value)
                {
                    CanSelectRootNode = false;
                }
            }
        }

        [DefaultValue(false), Description("单选时是否可选择父节点,该属性与CheckBoxes互斥"), Category("SunnyUI")]
        public bool CanSelectRootNode
        {
            get => item.CanSelectRootNode;
            set
            {
                item.CanSelectRootNode = value;
                if (value)
                {
                    CheckBoxes = false;
                }
            }
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
                Text = value?.Text;
            }
        }

        public delegate void OnNodeSelected(object sender, TreeNode node);
        public delegate void OnNodesSelected(object sender, TreeNodeCollection nodes);

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

        private void UIComboTreeView_ButtonClick(object sender, EventArgs e)
        {
            ItemForm.Size = ItemSize;
            //item.TreeView.ExpandAll();
            item.CanSelectRootNode = CanSelectRootNode;
            item.Translate();
            item.SetDPIScale();
            ItemForm.Show(this);
        }

        [DefaultValue(typeof(Size), "250, 220"), Description("下拉弹框界面大小"), Category("SunnyUI")]
        public Size ItemSize { get; set; } = new Size(250, 220);
    }
}
