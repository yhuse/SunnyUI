namespace Sunny.UI
{
    partial class UIIntegerUpDown
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
            btnDec = new UISymbolButton();
            btnAdd = new UISymbolButton();
            pnlValue = new UIPanel();
            SuspendLayout();
            // 
            // btnDec
            // 
            btnDec.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDec.Dock = System.Windows.Forms.DockStyle.Left;
            btnDec.Font = new System.Drawing.Font("宋体", 12F);
            btnDec.ImageInterval = 1;
            btnDec.Location = new System.Drawing.Point(0, 0);
            btnDec.Margin = new System.Windows.Forms.Padding(0);
            btnDec.MinimumSize = new System.Drawing.Size(1, 1);
            btnDec.Name = "btnDec";
            btnDec.Padding = new System.Windows.Forms.Padding(26, 4, 0, 0);
            btnDec.RadiusSides = UICornerRadiusSides.LeftTop | UICornerRadiusSides.LeftBottom;
            btnDec.Size = new System.Drawing.Size(29, 29);
            btnDec.Symbol = 361544;
            btnDec.TabIndex = 0;
            btnDec.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnDec.TipsText = null;
            btnDec.Click += btnDec_Click;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            btnAdd.Font = new System.Drawing.Font("宋体", 12F);
            btnAdd.ImageInterval = 1;
            btnAdd.Location = new System.Drawing.Point(87, 0);
            btnAdd.Margin = new System.Windows.Forms.Padding(0);
            btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            btnAdd.Name = "btnAdd";
            btnAdd.Padding = new System.Windows.Forms.Padding(26, 3, 0, 0);
            btnAdd.RadiusSides = UICornerRadiusSides.RightTop | UICornerRadiusSides.RightBottom;
            btnAdd.Size = new System.Drawing.Size(29, 29);
            btnAdd.Symbol = 361543;
            btnAdd.TabIndex = 1;
            btnAdd.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            btnAdd.TipsText = null;
            btnAdd.Click += btnAdd_Click;
            // 
            // pnlValue
            // 
            pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlValue.Font = new System.Drawing.Font("宋体", 12F);
            pnlValue.Location = new System.Drawing.Point(29, 0);
            pnlValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlValue.MinimumSize = new System.Drawing.Size(1, 1);
            pnlValue.Name = "pnlValue";
            pnlValue.RadiusSides = UICornerRadiusSides.None;
            pnlValue.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            pnlValue.Size = new System.Drawing.Size(58, 29);
            pnlValue.TabIndex = 2;
            pnlValue.Text = "0";
            pnlValue.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            pnlValue.Click += pnlValue_DoubleClick;
            // 
            // UIIntegerUpDown
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(pnlValue);
            Controls.Add(btnAdd);
            Controls.Add(btnDec);
            MinimumSize = new System.Drawing.Size(100, 0);
            Name = "UIIntegerUpDown";
            Size = new System.Drawing.Size(116, 29);
            ResumeLayout(false);
        }

        #endregion

        private UISymbolButton btnDec;
        private UISymbolButton btnAdd;
        private UIPanel pnlValue;
    }
}
