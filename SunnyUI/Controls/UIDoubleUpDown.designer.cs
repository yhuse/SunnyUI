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
            this.btnDec.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDec.ImageInterval = 1;
            this.btnDec.Location = new System.Drawing.Point(0, 0);
            this.btnDec.Margin = new System.Windows.Forms.Padding(0);
            this.btnDec.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDec.Name = "btnDec";
            this.btnDec.Padding = new System.Windows.Forms.Padding(26, 4, 0, 0);
            this.btnDec.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.LeftTop | Sunny.UI.UICornerRadiusSides.LeftBottom)));
            this.btnDec.Size = new System.Drawing.Size(29, 29);
            this.btnDec.Symbol = 61544;
            this.btnDec.TabIndex = 0;
            this.btnDec.TipsText = null;
            this.btnDec.Click += new System.EventHandler(this.btnDec_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAdd.ImageInterval = 1;
            this.btnAdd.Location = new System.Drawing.Point(87, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(26, 3, 0, 0);
            this.btnAdd.RadiusSides = ((Sunny.UI.UICornerRadiusSides)((Sunny.UI.UICornerRadiusSides.RightTop | Sunny.UI.UICornerRadiusSides.RightBottom)));
            this.btnAdd.Size = new System.Drawing.Size(29, 29);
            this.btnAdd.Symbol = 61543;
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TipsText = null;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlValue
            // 
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlValue.Location = new System.Drawing.Point(29, 0);
            this.pnlValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlValue.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.pnlValue.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.pnlValue.Size = new System.Drawing.Size(58, 29);
            this.pnlValue.TabIndex = 2;
            this.pnlValue.Text = "0";
            this.pnlValue.Click += new System.EventHandler(this.pnlValue_DoubleClick);
            // 
            // UIDoubleUpDown
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDec);
            this.MinimumSize = new System.Drawing.Size(100, 0);
            this.Name = "UIDoubleUpDown";
            this.Size = new System.Drawing.Size(116, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private UISymbolButton btnDec;
        private UISymbolButton btnAdd;
        private UIPanel pnlValue;
    }
}
