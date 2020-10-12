namespace Sunny.UI.Demo.Controls
{
    partial class FPieChart
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
            this.components = new System.ComponentModel.Container();
            this.uiLine1 = new Sunny.UI.UILine();
            this.PieChart = new Sunny.UI.UIPieChart();
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.uiImageButton2 = new Sunny.UI.UIImageButton();
            this.uiImageButton3 = new Sunny.UI.UIImageButton();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.PagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).BeginInit();
            this.SuspendLayout();
            // 
            // PagePanel
            // 
            this.PagePanel.Controls.Add(this.uiSymbolButton1);
            this.PagePanel.Controls.Add(this.uiImageButton3);
            this.PagePanel.Controls.Add(this.uiImageButton2);
            this.PagePanel.Controls.Add(this.uiImageButton1);
            this.PagePanel.Controls.Add(this.PieChart);
            this.PagePanel.Controls.Add(this.uiLine1);
            this.PagePanel.Size = new System.Drawing.Size(828, 517);
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 20);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(670, 20);
            this.uiLine1.TabIndex = 19;
            this.uiLine1.Text = "UIPieChart";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PieChart
            // 
            this.PieChart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.PieChart.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PieChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.PieChart.LegendFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PieChart.Location = new System.Drawing.Point(30, 48);
            this.PieChart.Name = "PieChart";
            this.PieChart.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.PieChart.Size = new System.Drawing.Size(670, 400);
            this.PieChart.SubFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PieChart.TabIndex = 20;
            this.PieChart.Text = "uiPieChart1";
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton1.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDefaultStyle;
            this.uiImageButton1.Location = new System.Drawing.Point(30, 466);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton1.TabIndex = 21;
            this.uiImageButton1.TabStop = false;
            this.uiImageButton1.Text = "      Default";
            this.uiImageButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton1.Click += new System.EventHandler(this.uiImageButton1_Click);
            // 
            // uiImageButton2
            // 
            this.uiImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton2.Image = global::Sunny.UI.Demo.Properties.Resources.ChartPlainStyle;
            this.uiImageButton2.Location = new System.Drawing.Point(136, 466);
            this.uiImageButton2.Name = "uiImageButton2";
            this.uiImageButton2.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton2.TabIndex = 22;
            this.uiImageButton2.TabStop = false;
            this.uiImageButton2.Text = "      Plain";
            this.uiImageButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton2.Click += new System.EventHandler(this.uiImageButton2_Click);
            // 
            // uiImageButton3
            // 
            this.uiImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiImageButton3.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDarkStyle;
            this.uiImageButton3.Location = new System.Drawing.Point(242, 466);
            this.uiImageButton3.Name = "uiImageButton3";
            this.uiImageButton3.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton3.TabIndex = 23;
            this.uiImageButton3.TabStop = false;
            this.uiImageButton3.Text = "      Dark";
            this.uiImageButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton3.Click += new System.EventHandler(this.uiImageButton3_Click);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(348, 466);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton1.Symbol = 61952;
            this.uiSymbolButton1.TabIndex = 24;
            this.uiSymbolButton1.Text = "数据";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // FPieChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 552);
            this.Name = "FPieChart";
            this.Symbol = 61952;
            this.Text = "PieChart";
            this.PagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private UILine uiLine1;
        private UIPieChart PieChart;
        private UIImageButton uiImageButton1;
        private UIImageButton uiImageButton3;
        private UIImageButton uiImageButton2;
        private UISymbolButton uiSymbolButton1;
    }
}