using System.ComponentModel;

namespace Sunny.UI
{
    public class UIComboTreeViewItem : UIDropDownItem, ITranslate
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
            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
        }

        private void InitializeComponent()
        {
            this.treeView = new Sunny.UI.UITreeView();
            this.panel = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.uiCheckBox1 = new Sunny.UI.UICheckBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.FillColor = System.Drawing.Color.White;
            this.treeView.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView.MinimumSize = new System.Drawing.Size(1, 1);
            this.treeView.Name = "treeView";
            this.treeView.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.treeView.ShowText = false;
            this.treeView.Size = new System.Drawing.Size(250, 176);
            this.treeView.TabIndex = 0;
            this.treeView.Text = "uiTreeView1";
            this.treeView.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.treeView.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_NodeMouseClick);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.uiCheckBox1);
            this.panel.Controls.Add(this.btnCancel);
            this.panel.Controls.Add(this.btnOK);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel.Visible = false;
            this.panel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(159, 8);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Size = new System.Drawing.Size(80, 29);
            this.btnCancel.Style = Sunny.UI.UIStyle.Red;
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(70, 8);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // uiCheckBox1
            // 
            this.uiCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.uiCheckBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiCheckBox1.Location = new System.Drawing.Point(3, 8);
            this.uiCheckBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox1.Name = "uiCheckBox1";
            this.uiCheckBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBox1.Size = new System.Drawing.Size(64, 29);
            this.uiCheckBox1.TabIndex = 2;
            this.uiCheckBox1.Text = "全选";
            this.uiCheckBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiCheckBox1.CheckedChanged += new System.EventHandler(this.uiCheckBox1_CheckedChanged);
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
