
namespace Sunny.UI.Demo
{
    partial class FTransfer
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
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiTransfer1 = new Sunny.UI.UITransfer();
            this.SuspendLayout();
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 55);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 21;
            this.uiLine1.Text = "UITransfer";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTransfer1
            // 
            this.uiTransfer1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiTransfer1.ItemsLeft.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.uiTransfer1.ItemsRight.AddRange(new object[] {
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.uiTransfer1.Location = new System.Drawing.Point(30, 90);
            this.uiTransfer1.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.uiTransfer1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTransfer1.Name = "uiTransfer1";
            this.uiTransfer1.Padding = new System.Windows.Forms.Padding(1);
            this.uiTransfer1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiTransfer1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiTransfer1.Size = new System.Drawing.Size(427, 370);
            this.uiTransfer1.TabIndex = 20;
            this.uiTransfer1.Text = "uiTransfer1";
            this.uiTransfer1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FTransfer1
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 568);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiTransfer1);
            this.Name = "FTransfer1";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61516;
            this.Text = "Transfer";
            this.ResumeLayout(false);

        }

        #endregion

        private UILine uiLine1;
        private UITransfer uiTransfer1;
    }
}