using System.ComponentModel;

namespace Sunny.UI
{
    internal class UIComboTreeViewItem : UIDropDownItem, ITranslate
    {
        private UIPanel panel;
        private UISymbolButton btnCancel;
        private UISymbolButton btnOK;
        private UICheckBox uiCheckBox1;
        private UITreeView treeView;

        public UITreeView TreeView => treeView;

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            treeView.SetDPIScale();
            btnOK.SetDPIScale();
            btnCancel.SetDPIScale();
            uiCheckBox1.SetDPIScale();
        }

        public bool ShowSelectedAllCheckBox
        {
            get => uiCheckBox1.Visible;
            set => uiCheckBox1.Visible = value;
        }

        [DefaultValue(false)]
        public bool CheckBoxes
        {
            get => treeView.CheckBoxes;
            set
            {
                treeView.CheckBoxes = value;
                treeView.NodeClickChangeCheckBoxes = value;
                panel.Visible = CheckBoxes;
            }
        }

        public bool CanSelectRootNode { get; set; }

        public UIComboTreeViewItem()
        {
            InitializeComponent();
            Translate();
        }

        public void Translate()
        {
            btnOK.Text = UIStyles.CurrentResources.OK;
            btnCancel.Text = UIStyles.CurrentResources.Cancel;
            uiCheckBox1.Text = UIStyles.CurrentResources.All;
        }

        private void InitializeComponent()
        {
            treeView = new UITreeView();
            panel = new UIPanel();
            uiCheckBox1 = new UICheckBox();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.FillColor = System.Drawing.Color.White;
            treeView.Font = new System.Drawing.Font("宋体", 12F);
            treeView.LineColor = System.Drawing.Color.FromArgb(48, 48, 48);
            treeView.Location = new System.Drawing.Point(0, 0);
            treeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            treeView.MinimumSize = new System.Drawing.Size(1, 1);
            treeView.Name = "treeView";
            treeView.RadiusSides = UICornerRadiusSides.None;
            treeView.ScrollBarStyleInherited = false;
            treeView.ShowText = false;
            treeView.Size = new System.Drawing.Size(250, 176);
            treeView.TabIndex = 0;
            treeView.Text = "uiTreeView1";
            treeView.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            treeView.NodeMouseClick += TreeView_NodeMouseClick;
            // 
            // panel
            // 
            panel.Controls.Add(uiCheckBox1);
            panel.Controls.Add(btnCancel);
            panel.Controls.Add(btnOK);
            panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel.Font = new System.Drawing.Font("宋体", 12F);
            panel.Location = new System.Drawing.Point(0, 176);
            panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            panel.MinimumSize = new System.Drawing.Size(1, 1);
            panel.Name = "panel";
            panel.RadiusSides = UICornerRadiusSides.None;
            panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            panel.Size = new System.Drawing.Size(250, 44);
            panel.TabIndex = 1;
            panel.Text = null;
            panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            panel.Visible = false;
            // 
            // uiCheckBox1
            // 
            uiCheckBox1.BackColor = System.Drawing.Color.Transparent;
            uiCheckBox1.Font = new System.Drawing.Font("宋体", 12F);
            uiCheckBox1.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            uiCheckBox1.Location = new System.Drawing.Point(3, 8);
            uiCheckBox1.MinimumSize = new System.Drawing.Size(1, 1);
            uiCheckBox1.Name = "uiCheckBox1";
            uiCheckBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            uiCheckBox1.Size = new System.Drawing.Size(64, 29);
            uiCheckBox1.TabIndex = 2;
            uiCheckBox1.Text = "全选";
            uiCheckBox1.CheckedChanged += uiCheckBox1_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.Font = new System.Drawing.Font("宋体", 10.5F);
            btnCancel.Location = new System.Drawing.Point(159, 8);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(80, 29);
            btnCancel.StyleCustomMode = true;
            btnCancel.Symbol = 361453;
            btnCancel.SymbolOffset = new System.Drawing.Point(0, 1);
            btnCancel.SymbolSize = 22;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.Font = new System.Drawing.Font("宋体", 10.5F);
            btnOK.Location = new System.Drawing.Point(70, 8);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(80, 29);
            btnOK.SymbolOffset = new System.Drawing.Point(0, 1);
            btnOK.SymbolSize = 22;
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnOK.Click += btnOK_Click;
            // 
            // UIComboTreeViewItem
            // 
            Controls.Add(treeView);
            Controls.Add(panel);
            Name = "UIComboTreeViewItem";
            Size = new System.Drawing.Size(250, 220);
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void TreeView_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (!treeView.CheckBoxes)
            {
                if (e.Location.X > treeView.DrawLeft(e.Node))
                {
                    if (e.Node.Nodes.Count == 0 || CanSelectRootNode)
                    {
                        DoValueChanged(this, e.Node);
                        Close();
                    }
                }
            }
        }

        public override void SetStyleColor(UIBaseStyle style)
        {
            base.SetStyleColor(style);
            treeView.Style = style.Name;
            panel.Style = style.Name;
            btnOK.Style = style.Name;
            btnCancel.Style = style.Name;
            uiCheckBox1.Style = style.Name;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DoValueChanged(this, treeView.Nodes);
            Close();
        }

        private void uiCheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (uiCheckBox1.Checked)
                treeView.CheckedAll();
            else
                treeView.UnCheckedAll();
        }
    }
}
