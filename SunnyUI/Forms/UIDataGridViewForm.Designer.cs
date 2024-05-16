namespace Sunny.UI
{
    partial class UIDataGridViewForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            Grid = new UIDataGridView();
            Panel = new UIPanel();
            uiSymbolButton4 = new UISymbolButton();
            uiSymbolButton3 = new UISymbolButton();
            uiSymbolButton2 = new UISymbolButton();
            uiSymbolButton1 = new UISymbolButton();
            ((System.ComponentModel.ISupportInitialize)Grid).BeginInit();
            Panel.SuspendLayout();
            SuspendLayout();
            // 
            // Grid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(235, 243, 255);
            Grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            Grid.BackgroundColor = System.Drawing.Color.White;
            Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(155, 200, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            Grid.DefaultCellStyle = dataGridViewCellStyle3;
            Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            Grid.EnableHeadersVisualStyles = false;
            Grid.Font = new System.Drawing.Font("宋体", 12F);
            Grid.GridColor = System.Drawing.Color.FromArgb(80, 160, 255);
            Grid.Location = new System.Drawing.Point(0, 35);
            Grid.Name = "Grid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            Grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            Grid.RowTemplate.Height = 29;
            Grid.SelectedIndex = -1;
            Grid.Size = new System.Drawing.Size(1034, 511);
            Grid.StripeOddColor = System.Drawing.Color.FromArgb(235, 243, 255);
            Grid.TabIndex = 0;
            // 
            // Panel
            // 
            Panel.Controls.Add(uiSymbolButton4);
            Panel.Controls.Add(uiSymbolButton3);
            Panel.Controls.Add(uiSymbolButton2);
            Panel.Controls.Add(uiSymbolButton1);
            Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            Panel.Font = new System.Drawing.Font("宋体", 12F);
            Panel.Location = new System.Drawing.Point(0, 546);
            Panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Panel.MinimumSize = new System.Drawing.Size(1, 1);
            Panel.Name = "Panel";
            Panel.RadiusSides = UICornerRadiusSides.None;
            Panel.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            Panel.Size = new System.Drawing.Size(1034, 55);
            Panel.TabIndex = 1;
            Panel.Text = null;
            Panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiSymbolButton4
            // 
            uiSymbolButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            uiSymbolButton4.Font = new System.Drawing.Font("宋体", 12F);
            uiSymbolButton4.Location = new System.Drawing.Point(345, 10);
            uiSymbolButton4.MinimumSize = new System.Drawing.Size(1, 1);
            uiSymbolButton4.Name = "uiSymbolButton4";
            uiSymbolButton4.ShowFocusColor = true;
            uiSymbolButton4.Size = new System.Drawing.Size(100, 35);
            uiSymbolButton4.Symbol = 361639;
            uiSymbolButton4.TabIndex = 3;
            uiSymbolButton4.Text = "导出";
            uiSymbolButton4.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            uiSymbolButton4.Visible = false;
            // 
            // uiSymbolButton3
            // 
            uiSymbolButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            uiSymbolButton3.Font = new System.Drawing.Font("宋体", 12F);
            uiSymbolButton3.Location = new System.Drawing.Point(235, 10);
            uiSymbolButton3.MinimumSize = new System.Drawing.Size(1, 1);
            uiSymbolButton3.Name = "uiSymbolButton3";
            uiSymbolButton3.ShowFocusColor = true;
            uiSymbolButton3.Size = new System.Drawing.Size(100, 35);
            uiSymbolButton3.Symbol = 361544;
            uiSymbolButton3.TabIndex = 2;
            uiSymbolButton3.Text = "删除";
            uiSymbolButton3.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            // 
            // uiSymbolButton2
            // 
            uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            uiSymbolButton2.Font = new System.Drawing.Font("宋体", 12F);
            uiSymbolButton2.Location = new System.Drawing.Point(125, 10);
            uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            uiSymbolButton2.Name = "uiSymbolButton2";
            uiSymbolButton2.ShowFocusColor = true;
            uiSymbolButton2.Size = new System.Drawing.Size(100, 35);
            uiSymbolButton2.Symbol = 361508;
            uiSymbolButton2.TabIndex = 1;
            uiSymbolButton2.Text = "编辑";
            uiSymbolButton2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            // 
            // uiSymbolButton1
            // 
            uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            uiSymbolButton1.Font = new System.Drawing.Font("宋体", 12F);
            uiSymbolButton1.Location = new System.Drawing.Point(15, 10);
            uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            uiSymbolButton1.Name = "uiSymbolButton1";
            uiSymbolButton1.ShowFocusColor = true;
            uiSymbolButton1.Size = new System.Drawing.Size(100, 35);
            uiSymbolButton1.Symbol = 361543;
            uiSymbolButton1.TabIndex = 0;
            uiSymbolButton1.Text = "增加";
            uiSymbolButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            // 
            // UIDataGridViewForm
            // 
            AllowShowTitle = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1034, 601);
            Controls.Add(Grid);
            Controls.Add(Panel);
            Name = "UIDataGridViewForm";
            Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            ShowTitle = true;
            Text = "FDataGridView";
            ((System.ComponentModel.ISupportInitialize)Grid).EndInit();
            Panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        protected UIDataGridView Grid;
        protected UIPanel Panel;
        protected UISymbolButton uiSymbolButton4;
        protected UISymbolButton uiSymbolButton3;
        protected UISymbolButton uiSymbolButton2;
        protected UISymbolButton uiSymbolButton1;
    }
}