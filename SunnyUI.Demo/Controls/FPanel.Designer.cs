
namespace Sunny.UI.Demo
{
    partial class FPanel
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
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiTitlePanel2 = new Sunny.UI.UITitlePanel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(30, 55);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(270, 180);
            this.uiPanel1.TabIndex = 7;
            this.uiPanel1.Text = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel2
            // 
            this.uiTitlePanel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTitlePanel2.ForeColor = System.Drawing.Color.White;
            this.uiTitlePanel2.Location = new System.Drawing.Point(331, 250);
            this.uiTitlePanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel2.Name = "uiTitlePanel2";
            this.uiTitlePanel2.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel2.ShowCollapse = true;
            this.uiTitlePanel2.Size = new System.Drawing.Size(270, 180);
            this.uiTitlePanel2.TabIndex = 9;
            this.uiTitlePanel2.Text = "uiTitlePanel2";
            this.uiTitlePanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiGroupBox1.Location = new System.Drawing.Point(331, 39);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(270, 196);
            this.uiGroupBox1.TabIndex = 8;
            this.uiGroupBox1.Text = "uiGroupBox1";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTitlePanel1.ForeColor = System.Drawing.Color.White;
            this.uiTitlePanel1.Location = new System.Drawing.Point(30, 250);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.uiTitlePanel1.Size = new System.Drawing.Size(270, 180);
            this.uiTitlePanel1.TabIndex = 6;
            this.uiTitlePanel1.Text = "uiTitlePanel1";
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPanel
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 494);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.uiTitlePanel2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiTitlePanel1);
            this.Name = "FPanel";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61590;
            this.Text = "Panel";
            this.ResumeLayout(false);

        }

        #endregion

        private UIPanel uiPanel1;
        private UITitlePanel uiTitlePanel2;
        private UIGroupBox uiGroupBox1;
        private UITitlePanel uiTitlePanel1;
    }
}