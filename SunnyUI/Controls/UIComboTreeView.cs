/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 2021-11-11: V3.0.0 增加文件说明
 * 2022-05-15: V3.0.8 显示CheckBoxes时自己选中节点文字可切换状态
 * 2022-06-16: V3.2.0 增加下拉框宽度、高度
 * 2022-07-12: V3.2.1 修复CanSelectRootNode时可以展开子节点
 * 2022-11-30: V3.3.0 增加Clear方法
 * 2023-02-04: V3.3.1 下拉框增加显示全选选择框
 * 2023-04-02: V3.3.4 显示清除按钮
 * 2023-06-12: V3.3.8 修复使用清空按钮后，再次打开下拉框，上次的选择内容还是存在
 * 2024-03-22: V3.6.5 增加ShowDropDown()
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
            CreateInstance();
            DropDownWidth = 250;
            DropDownHeight = 220;
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

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            item?.Dispose();
            base.Dispose(disposing);
        }

        [DefaultValue(false)]
        [Description("显示清除按钮"), Category("SunnyUI")]
        public bool ShowClearButton
        {
            get => showClearButton;
            set => showClearButton = value;
        }

        [DefaultValue(true)]
        [Description("下拉框显示全选选择框"), Category("SunnyUI")]
        public bool ShowSelectedAllCheckBox { get; set; } = true;

        [DefaultValue(250)]
        [Description("下拉框宽度"), Category("SunnyUI")]
        public int DropDownWidth { get; set; }

        [DefaultValue(220)]
        [Description("下拉框高度"), Category("SunnyUI")]
        public int DropDownHeight { get; set; }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return edit;
        }

        public override void Clear()
        {
            base.Clear();
            TreeView.Nodes.Clear();
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

        /// <summary>
        /// 创建对象
        /// </summary>
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

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
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

        public void ShowDropDown()
        {
            UIComboTreeView_ButtonClick(this, EventArgs.Empty);
        }

        private void UIComboTreeView_ButtonClick(object sender, EventArgs e)
        {
            if (NeedDrawClearButton)
            {
                NeedDrawClearButton = false;
                Text = "";
                TreeView.SelectedNode = null;
                TreeView.UnCheckedAll();
                Invalidate();
                return;
            }

            ItemForm.Size = ItemSize;
            //item.TreeView.ExpandAll();
            item.CanSelectRootNode = CanSelectRootNode;
            item.Translate();
            item.SetDPIScale();
            //ItemForm.Show(this);
            int width = DropDownWidth < Width ? Width : DropDownWidth;
            width = Math.Max(250, width);
            item.ShowSelectedAllCheckBox = ShowSelectedAllCheckBox;
            ItemForm.Show(this, new Size(width, DropDownHeight));
        }

        [DefaultValue(typeof(Size), "250, 220"), Description("下拉弹框界面大小"), Category("SunnyUI")]
        public Size ItemSize { get; set; } = new Size(250, 220);
    }
}
