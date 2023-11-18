
namespace Sunny.UI.Demo
{
    partial class FSplitContainer
    {
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
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点3");
            this.uiSplitContainer1 = new Sunny.UI.UISplitContainer();
            this.uiNavMenuEx1 = new Sunny.UI.UINavMenu();
            this.uiSplitContainer2 = new Sunny.UI.UISplitContainer();
            this.uiListBox1 = new Sunny.UI.UIListBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).BeginInit();
            this.uiSplitContainer1.Panel1.SuspendLayout();
            this.uiSplitContainer1.Panel2.SuspendLayout();
            this.uiSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer2)).BeginInit();
            this.uiSplitContainer2.Panel1.SuspendLayout();
            this.uiSplitContainer2.Panel2.SuspendLayout();
            this.uiSplitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiSplitContainer1
            // 
            this.uiSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSplitContainer1.Location = new System.Drawing.Point(0, 35);
            this.uiSplitContainer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.uiSplitContainer1.Name = "uiSplitContainer1";
            // 
            // uiSplitContainer1.Panel1
            // 
            this.uiSplitContainer1.Panel1.Controls.Add(this.uiNavMenuEx1);
            // 
            // uiSplitContainer1.Panel2
            // 
            this.uiSplitContainer1.Panel2.Controls.Add(this.uiSplitContainer2);
            this.uiSplitContainer1.Size = new System.Drawing.Size(800, 415);
            this.uiSplitContainer1.SplitterDistance = 266;
            this.uiSplitContainer1.SplitterWidth = 11;
            this.uiSplitContainer1.TabIndex = 0;
            // 
            // uiNavMenuEx1
            // 
            this.uiNavMenuEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiNavMenuEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiNavMenuEx1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.uiNavMenuEx1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiNavMenuEx1.FullRowSelect = true;
            this.uiNavMenuEx1.ItemHeight = 50;
            this.uiNavMenuEx1.Location = new System.Drawing.Point(0, 0);
            this.uiNavMenuEx1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavMenuEx1.Name = "uiNavMenuEx1";
            treeNode1.Name = "节点4";
            treeNode1.Text = "节点4";
            treeNode2.Name = "节点5";
            treeNode2.Text = "节点5";
            treeNode3.Name = "节点6";
            treeNode3.Text = "节点6";
            treeNode4.Name = "节点0";
            treeNode4.Text = "节点0";
            treeNode5.Name = "节点1";
            treeNode5.Text = "节点1";
            treeNode6.Name = "节点2";
            treeNode6.Text = "节点2";
            treeNode7.Name = "节点3";
            treeNode7.Text = "节点3";
            this.uiNavMenuEx1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            this.uiNavMenuEx1.ShowLines = false;
            this.uiNavMenuEx1.Size = new System.Drawing.Size(266, 415);
            this.uiNavMenuEx1.TabIndex = 0;
            this.uiNavMenuEx1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavMenuEx1.NodeRightSymbolClick += new Sunny.UI.UINavMenu.OnNodeRightSymbolClick(this.uiNavMenuEx1_NodeRightSymbolClick);
            // 
            // uiSplitContainer2
            // 
            this.uiSplitContainer2.CollapsePanel = Sunny.UI.UISplitContainer.UICollapsePanel.Panel2;
            this.uiSplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.uiSplitContainer2.MinimumSize = new System.Drawing.Size(20, 20);
            this.uiSplitContainer2.Name = "uiSplitContainer2";
            this.uiSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // uiSplitContainer2.Panel1
            // 
            this.uiSplitContainer2.Panel1.Controls.Add(this.uiListBox1);
            // 
            // uiSplitContainer2.Panel2
            // 
            this.uiSplitContainer2.Panel2.Controls.Add(this.uiButton1);
            this.uiSplitContainer2.Size = new System.Drawing.Size(523, 415);
            this.uiSplitContainer2.SplitterDistance = 244;
            this.uiSplitContainer2.SplitterWidth = 11;
            this.uiSplitContainer2.TabIndex = 0;
            // 
            // uiListBox1
            // 
            this.uiListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiListBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiListBox1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiListBox1.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiListBox1.Location = new System.Drawing.Point(0, 0);
            this.uiListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiListBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiListBox1.Name = "uiListBox1";
            this.uiListBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiListBox1.ShowText = false;
            this.uiListBox1.Size = new System.Drawing.Size(523, 244);
            this.uiListBox1.TabIndex = 0;
            this.uiListBox1.Text = "uiListBox1";
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiButton1.Location = new System.Drawing.Point(29, 29);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 0;
            this.uiButton1.Text = "清除";
            this.uiButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // FSplitContainer
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiSplitContainer1);
            this.Name = "FSplitContainer";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 361512;
            this.Text = "SplitContainer";
            this.uiSplitContainer1.Panel1.ResumeLayout(false);
            this.uiSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).EndInit();
            this.uiSplitContainer1.ResumeLayout(false);
            this.uiSplitContainer2.Panel1.ResumeLayout(false);
            this.uiSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer2)).EndInit();
            this.uiSplitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISplitContainer uiSplitContainer1;
        private Sunny.UI.UISplitContainer uiSplitContainer2;
        private Sunny.UI.UINavMenu uiNavMenuEx1;
        private Sunny.UI.UIListBox uiListBox1;
        private Sunny.UI.UIButton uiButton1;
    }
}

