
namespace Sunny.UI.Demo
{
    partial class FCommon
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
            this.uiMiniPagination1 = new Sunny.UI.UIMiniPagination();
            this.SuspendLayout();
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Location = new System.Drawing.Point(30, 60);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 0;
            this.uiButton1.Text = "Mapper";
            this.uiButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // uiMiniPagination1
            // 
            this.uiMiniPagination1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiMiniPagination1.Location = new System.Drawing.Point(43, 149);
            this.uiMiniPagination1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiMiniPagination1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiMiniPagination1.Name = "uiMiniPagination1";
            this.uiMiniPagination1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiMiniPagination1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiMiniPagination1.ShowText = false;
            this.uiMiniPagination1.Size = new System.Drawing.Size(433, 40);
            this.uiMiniPagination1.TabIndex = 1;
            this.uiMiniPagination1.Text = "uiMiniPagination1";
            this.uiMiniPagination1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiMiniPagination1.TotalCount = 1000;
            // 
            // FCommon
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uiMiniPagination1);
            this.Controls.Add(this.uiButton1);
            this.Name = "FCommon";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.PageIndex = 5000;
            this.ShowTitle = true;
            this.Symbol = 62098;
            this.Text = "类库";
            this.ResumeLayout(false);

        }

        #endregion

        private UIButton uiButton1;
        private UIMiniPagination uiMiniPagination1;
    }
}