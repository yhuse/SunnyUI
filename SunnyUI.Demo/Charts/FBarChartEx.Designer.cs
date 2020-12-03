namespace Sunny.UI.Demo
{
    partial class FBarChartEx
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
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiImageButton3 = new Sunny.UI.UIImageButton();
            this.uiImageButton2 = new Sunny.UI.UIImageButton();
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.uiLine1 = new Sunny.UI.UILine();
            this.BarChart = new Sunny.UI.UIBarChartEx();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.PagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiSymbolButton2);
            this.PagePanel.Controls.Add(this.uiSymbolButton1);
            this.PagePanel.Controls.Add(this.uiImageButton3);
            this.PagePanel.Controls.Add(this.uiImageButton2);
            this.PagePanel.Controls.Add(this.uiImageButton1);
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Controls.Add(this.BarChart);
            this.PagePanel.Size = new System.Drawing.Size(886, 508);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(348, 466);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton1.Symbol = 61568;
            this.uiSymbolButton1.TabIndex = 34;
            this.uiSymbolButton1.Text = "数据";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // uiImageButton3
            // 
            this.uiImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton3.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDarkStyle;
            this.uiImageButton3.Location = new System.Drawing.Point(242, 466);
            this.uiImageButton3.Name = "uiImageButton3";
            this.uiImageButton3.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton3.TabIndex = 33;
            this.uiImageButton3.TabStop = false;
            this.uiImageButton3.Text = "      Dark";
            this.uiImageButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton3.Click += new System.EventHandler(this.uiImageButton3_Click);
            // 
            // uiImageButton2
            // 
            this.uiImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton2.Image = global::Sunny.UI.Demo.Properties.Resources.ChartPlainStyle;
            this.uiImageButton2.Location = new System.Drawing.Point(136, 466);
            this.uiImageButton2.Name = "uiImageButton2";
            this.uiImageButton2.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton2.TabIndex = 32;
            this.uiImageButton2.TabStop = false;
            this.uiImageButton2.Text = "      Plain";
            this.uiImageButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton2.Click += new System.EventHandler(this.uiImageButton2_Click);
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton1.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDefaultStyle;
            this.uiImageButton1.Location = new System.Drawing.Point(30, 466);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton1.TabIndex = 31;
            this.uiImageButton1.TabStop = false;
            this.uiImageButton1.Text = "      Default";
            this.uiImageButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton1.Click += new System.EventHandler(this.uiImageButton1_Click);
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 30;
            this.uiLine1.Text = "UIBarChartEx";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BarChart
            // 
            this.BarChart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.BarChart.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.BarChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.BarChart.Location = new System.Drawing.Point(30, 48);
            this.BarChart.MinimumSize = new System.Drawing.Size(1, 1);
            this.BarChart.Name = "BarChart";
            this.BarChart.Size = new System.Drawing.Size(670, 400);
            this.BarChart.TabIndex = 35;
            this.BarChart.Text = "uiBarChartEx1";
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton2.Location = new System.Drawing.Point(456, 466);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton2.Symbol = 61568;
            this.uiSymbolButton2.TabIndex = 36;
            this.uiSymbolButton2.Text = "数据";
            this.uiSymbolButton2.Click += new System.EventHandler(this.uiSymbolButton2_Click);
            // 
            // FBarChartEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 543);
            this.Name = "FBarChartEx";
            this.Symbol = 61568;
            this.Text = "BarChartEx";
            this.PagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UISymbolButton uiSymbolButton1;
        private UIImageButton uiImageButton3;
        private UIImageButton uiImageButton2;
        private UIImageButton uiImageButton1;
        private UILine uiLine1;
        private UIBarChartEx BarChart;
        private UISymbolButton uiSymbolButton2;
    }
}