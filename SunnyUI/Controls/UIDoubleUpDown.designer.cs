namespace Sunny.UI
{
    partial class UIDoubleUpDown
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDec = new Sunny.UI.UISymbolButton();
            this.btnAdd = new Sunny.UI.UISymbolButton();
            this.pnlValue = new Sunny.UI.UIPanel();
            this.SuspendLayout();
            // 
            // btnDec
            // 
            this.btnDec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDec.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDec.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnDec.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btnDec.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.btnDec.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.btnDec.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDec.ForeColor = System.Drawing.Color.White;
            this.btnDec.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(103)))));
            this.btnDec.ForeHoverColor = System.Drawing.Color.White;
            this.btnDec.ForePressColor = System.Drawing.Color.White;
            this.btnDec.ImageInterval = 1;
            this.btnDec.Location = new System.Drawing.Point(0, 0);
            this.btnDec.Margin = new System.Windows.Forms.Padding(0);
            this.btnDec.Name = "btnDec";
            this.btnDec.Padding = new System.Windows.Forms.Padding(26, 0, 0, 0);
            this.btnDec.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.LeftTop | Sunny.UI.UICornerRadiusSides.LeftBottom)));
            this.btnDec.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnDec.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.btnDec.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.btnDec.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.btnDec.Size = new System.Drawing.Size(29, 29);
            this.btnDec.Style = Sunny.UI.UIStyle.Blue;
            this.btnDec.Symbol = 61544;
            this.btnDec.TabIndex = 0;
            this.btnDec.Text = null;
            this.btnDec.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDec.TipsText = null;
            this.btnDec.Click += new System.EventHandler(this.btnDec_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnAdd.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.btnAdd.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.btnAdd.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(103)))));
            this.btnAdd.ForeHoverColor = System.Drawing.Color.White;
            this.btnAdd.ForePressColor = System.Drawing.Color.White;
            this.btnAdd.ImageInterval = 1;
            this.btnAdd.Location = new System.Drawing.Point(87, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(26, 0, 0, 0);
            this.btnAdd.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.RightTop | Sunny.UI.UICornerRadiusSides.RightBottom)));
            this.btnAdd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnAdd.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.btnAdd.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.btnAdd.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(131)))), ((int)(((byte)(229)))));
            this.btnAdd.Size = new System.Drawing.Size(29, 29);
            this.btnAdd.Style = Sunny.UI.UIStyle.Blue;
            this.btnAdd.Symbol = 61543;
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = null;
            this.btnAdd.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnAdd.TipsText = null;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlValue
            // 
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.pnlValue.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.pnlValue.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlValue.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(103)))));
            this.pnlValue.Location = new System.Drawing.Point(29, 0);
            this.pnlValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.pnlValue.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.pnlValue.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.pnlValue.RadiusSides = UICornerRadiusSides.None;
            this.pnlValue.Size = new System.Drawing.Size(58, 29);
            this.pnlValue.Style = Sunny.UI.UIStyle.Blue;
            this.pnlValue.TabIndex = 2;
            this.pnlValue.Text = "0";
            // 
            // UIIntegerUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDec);
            this.MinimumSize = new System.Drawing.Size(100, 0);
            this.Name = "UIIntegerUpDown";
            this.Size = new System.Drawing.Size(116, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private UISymbolButton btnDec;
        private UISymbolButton btnAdd;
        private UIPanel pnlValue;
    }
}
