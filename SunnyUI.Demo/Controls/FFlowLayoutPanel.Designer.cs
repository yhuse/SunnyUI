
namespace Sunny.UI.Demo
{
    partial class FFlowLayoutPanel
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
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiFlowLayoutPanel1 = new Sunny.UI.UIFlowLayoutPanel();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.uiButton3 = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton1.Location = new System.Drawing.Point(30, 462);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 11;
            this.uiButton1.Text = "增加";
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiFlowLayoutPanel1
            // 
            this.uiFlowLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiFlowLayoutPanel1.Location = new System.Drawing.Point(30, 55);
            this.uiFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiFlowLayoutPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiFlowLayoutPanel1.Name = "uiFlowLayoutPanel1";
            this.uiFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.uiFlowLayoutPanel1.Size = new System.Drawing.Size(354, 390);
            this.uiFlowLayoutPanel1.TabIndex = 10;
            this.uiFlowLayoutPanel1.Text = "`";
            this.uiFlowLayoutPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton2.Location = new System.Drawing.Point(284, 462);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Size = new System.Drawing.Size(100, 35);
            this.uiButton2.TabIndex = 12;
            this.uiButton2.Text = "清除";
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // uiButton3
            // 
            this.uiButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton3.Enabled = false;
            this.uiButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiButton3.Location = new System.Drawing.Point(157, 462);
            this.uiButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton3.Name = "uiButton3";
            this.uiButton3.Size = new System.Drawing.Size(100, 35);
            this.uiButton3.TabIndex = 13;
            this.uiButton3.Text = "移除";
            this.uiButton3.Click += new System.EventHandler(this.uiButton3_Click);
            // 
            // FFlowLayoutPanel
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 539);
            this.Controls.Add(this.uiButton3);
            this.Controls.Add(this.uiButton2);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.uiFlowLayoutPanel1);
            this.Name = "FFlowLayoutPanel";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61590;
            this.Text = "FlowLayoutPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private UIButton uiButton1;
        private UIFlowLayoutPanel uiFlowLayoutPanel1;
        private UIButton uiButton2;
        private UIButton uiButton3;
    }
}