namespace Sunny.UI.Demo
{
    partial class FHeaderMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Page1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Page2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Page3");
            this.uiLogo1 = new Sunny.UI.UILogo();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Controls.Add(this.uiLogo1);
            this.Header.Location = new System.Drawing.Point(0, 35);
            treeNode1.Name = "节点0";
            treeNode1.Text = "Page1";
            treeNode2.Name = "节点1";
            treeNode2.Text = "Page2";
            treeNode3.Name = "节点2";
            treeNode3.Text = "Page3";
            this.Header.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.Header.Size = new System.Drawing.Size(1024, 110);
            // 
            // uiLogo1
            // 
            this.uiLogo1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLogo1.Location = new System.Drawing.Point(15, 15);
            this.uiLogo1.MaximumSize = new System.Drawing.Size(300, 80);
            this.uiLogo1.MinimumSize = new System.Drawing.Size(300, 80);
            this.uiLogo1.Name = "uiLogo1";
            this.uiLogo1.Size = new System.Drawing.Size(300, 80);
            this.uiLogo1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLogo1.TabIndex = 0;
            this.uiLogo1.Text = "uiLogo1";
            // 
            // FHeaderMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Name = "FHeaderMain";
            this.Text = "FHeaderMain";
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UILogo uiLogo1;
    }
}