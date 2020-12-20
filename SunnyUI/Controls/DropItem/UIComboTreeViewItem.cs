using System.ComponentModel;

namespace Sunny.UI
{
    public class UIComboTreeViewItem : UIDropDownItem
    {
        private UIPanel panel;
        private UISymbolButton btnCancel;
        private UISymbolButton btnOK;
        private UITreeView treeView;

        public UITreeView TreeView => treeView;

        [DefaultValue(false)]
        public bool CheckBoxes
        {
            get => treeView.CheckBoxes;
            set
            {
                treeView.CheckBoxes = value;
                panel.Visible = CheckBoxes;
            }
        }

        public UIComboTreeViewItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.treeView = new Sunny.UI.UITreeView();
            this.panel = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FillColor = System.Drawing.Color.White;
            this.treeView.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView.MinimumSize = new System.Drawing.Size(1, 1);
            this.treeView.Name = "treeView";
            this.treeView.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.treeView.SelectedNode = null;
            this.treeView.Size = new System.Drawing.Size(250, 176);
            this.treeView.TabIndex = 0;
            this.treeView.Text = "uiTreeView1";
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnCancel);
            this.panel.Controls.Add(this.btnOK);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.panel.Location = new System.Drawing.Point(0, 176);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.panel.Name = "panel";
            this.panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.panel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.panel.Size = new System.Drawing.Size(250, 44);
            this.panel.TabIndex = 1;
            this.panel.Text = null;
            this.panel.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(159, 8);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(127)))), ((int)(((byte)(128)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(87)))), ((int)(((byte)(89)))));
            this.btnCancel.Size = new System.Drawing.Size(80, 29);
            this.btnCancel.Style = Sunny.UI.UIStyle.Red;
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(70, 8);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // UIComboTreeViewItem
            // 
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.panel);
            this.Name = "UIComboTreeViewItem";
            this.Size = new System.Drawing.Size(250, 220);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void TreeView_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (!treeView.CheckBoxes && e.Node.Nodes.Count == 0)
            {
                DoValueChanged(this, e.Node);
                CloseParent();
            }
        }

        public override void SetStyle(UIBaseStyle style)
        {
            treeView.Style = style.Name;
            panel.Style = style.Name;
            btnOK.Style = style.Name;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            CloseParent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DoValueChanged(this, treeView.Nodes);
            CloseParent();
        }
    }
}
