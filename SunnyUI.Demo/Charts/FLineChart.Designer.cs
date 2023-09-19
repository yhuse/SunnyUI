namespace Sunny.UI.Demo
{
    partial class FLineChart
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
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiImageButton3 = new Sunny.UI.UIImageButton();
            this.uiImageButton2 = new Sunny.UI.UIImageButton();
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.LineChart = new Sunny.UI.UILineChart();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbPoints = new Sunny.UI.UICheckBox();
            this.cbContainsNan = new Sunny.UI.UICheckBox();
            this.uiSymbolButton3 = new Sunny.UI.UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(348, 505);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton1.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton1.Symbol = 61953;
            this.uiSymbolButton1.TabIndex = 34;
            this.uiSymbolButton1.Text = "单Y轴";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // uiImageButton3
            // 
            this.uiImageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton3.Font = new System.Drawing.Font("宋体", 12F);
            this.uiImageButton3.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDarkStyle;
            this.uiImageButton3.Location = new System.Drawing.Point(242, 505);
            this.uiImageButton3.Name = "uiImageButton3";
            this.uiImageButton3.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton3.TabIndex = 33;
            this.uiImageButton3.TabStop = false;
            this.uiImageButton3.Text = "      Dark";
            this.uiImageButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiImageButton3.Click += new System.EventHandler(this.uiImageButton3_Click);
            // 
            // uiImageButton2
            // 
            this.uiImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiImageButton2.Image = global::Sunny.UI.Demo.Properties.Resources.ChartPlainStyle;
            this.uiImageButton2.Location = new System.Drawing.Point(136, 505);
            this.uiImageButton2.Name = "uiImageButton2";
            this.uiImageButton2.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton2.TabIndex = 32;
            this.uiImageButton2.TabStop = false;
            this.uiImageButton2.Text = "      Plain";
            this.uiImageButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiImageButton2.Click += new System.EventHandler(this.uiImageButton2_Click);
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiImageButton1.Image = global::Sunny.UI.Demo.Properties.Resources.ChartDefaultStyle;
            this.uiImageButton1.Location = new System.Drawing.Point(30, 505);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(100, 27);
            this.uiImageButton1.TabIndex = 31;
            this.uiImageButton1.TabStop = false;
            this.uiImageButton1.Text = "      Default";
            this.uiImageButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiImageButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiImageButton1.Click += new System.EventHandler(this.uiImageButton1_Click);
            // 
            // LineChart
            // 
            this.LineChart.Font = new System.Drawing.Font("宋体", 12F);
            this.LineChart.LegendFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineChart.Location = new System.Drawing.Point(30, 55);
            this.LineChart.MinimumSize = new System.Drawing.Size(1, 1);
            this.LineChart.Name = "LineChart";
            this.LineChart.Size = new System.Drawing.Size(670, 430);
            this.LineChart.SubFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineChart.TabIndex = 35;
            this.LineChart.Text = "uiLineChart1";
            this.LineChart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.LineChart.PointValue += new Sunny.UI.UILineChart.OnPointValue(this.LineChart_PointValue);
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiSymbolButton2.Location = new System.Drawing.Point(560, 505);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton2.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton2.Symbol = 61463;
            this.uiSymbolButton2.TabIndex = 36;
            this.uiSymbolButton2.Text = "实时";
            this.uiSymbolButton2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiSymbolButton2.Click += new System.EventHandler(this.uiSymbolButton2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbPoints
            // 
            this.cbPoints.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbPoints.Font = new System.Drawing.Font("宋体", 12F);
            this.cbPoints.Location = new System.Drawing.Point(348, 538);
            this.cbPoints.MinimumSize = new System.Drawing.Size(1, 1);
            this.cbPoints.Name = "cbPoints";
            this.cbPoints.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.cbPoints.Size = new System.Drawing.Size(95, 27);
            this.cbPoints.TabIndex = 37;
            this.cbPoints.Text = "只显示点";
            this.cbPoints.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbPoints.CheckedChanged += new System.EventHandler(this.uiCheckBox1_CheckedChanged);
            // 
            // cbContainsNan
            // 
            this.cbContainsNan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbContainsNan.Font = new System.Drawing.Font("宋体", 12F);
            this.cbContainsNan.Location = new System.Drawing.Point(571, 535);
            this.cbContainsNan.MinimumSize = new System.Drawing.Size(1, 1);
            this.cbContainsNan.Name = "cbContainsNan";
            this.cbContainsNan.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.cbContainsNan.Size = new System.Drawing.Size(95, 27);
            this.cbContainsNan.TabIndex = 38;
            this.cbContainsNan.Text = "包含Nan";
            this.cbContainsNan.Visible = false;
            this.cbContainsNan.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbContainsNan.CheckedChanged += new System.EventHandler(this.uiCheckBox2_CheckedChanged);
            // 
            // uiSymbolButton3
            // 
            this.uiSymbolButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton3.Font = new System.Drawing.Font("宋体", 12F);
            this.uiSymbolButton3.Location = new System.Drawing.Point(454, 505);
            this.uiSymbolButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton3.Name = "uiSymbolButton3";
            this.uiSymbolButton3.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton3.Size = new System.Drawing.Size(100, 27);
            this.uiSymbolButton3.Symbol = 61953;
            this.uiSymbolButton3.TabIndex = 39;
            this.uiSymbolButton3.Text = "双Y轴";
            this.uiSymbolButton3.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiSymbolButton3.Click += new System.EventHandler(this.uiSymbolButton3_Click_1);
            // 
            // FLineChart
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 574);
            this.Controls.Add(this.uiSymbolButton3);
            this.Controls.Add(this.cbContainsNan);
            this.Controls.Add(this.cbPoints);
            this.Controls.Add(this.uiSymbolButton2);
            this.Controls.Add(this.uiSymbolButton1);
            this.Controls.Add(this.uiImageButton3);
            this.Controls.Add(this.uiImageButton2);
            this.Controls.Add(this.uiImageButton1);
            this.Controls.Add(this.LineChart);
            this.Name = "FLineChart";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61953;
            this.Text = "LineChart";
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
        private UILineChart LineChart;
        private UISymbolButton uiSymbolButton2;
        private System.Windows.Forms.Timer timer1;
        private UICheckBox cbPoints;
        private UICheckBox cbContainsNan;
        private UISymbolButton uiSymbolButton3;
    }
}