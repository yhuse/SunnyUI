using System.Windows.Forms;

namespace Sunny.UI
{
    partial class UITransfer
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
            b1 = new UISymbolButton();
            b2 = new UISymbolButton();
            b3 = new UISymbolButton();
            b4 = new UISymbolButton();
            l1 = new UIListBox();
            l2 = new UIListBox();
            SuspendLayout();
            // 
            // b1
            // 
            b1.BackColor = System.Drawing.Color.Transparent;
            b1.Cursor = Cursors.Hand;
            b1.Font = new System.Drawing.Font("宋体", 12F);
            b1.Location = new System.Drawing.Point(228, 85);
            b1.Margin = new Padding(0);
            b1.MinimumSize = new System.Drawing.Size(1, 1);
            b1.Name = "b1";
            b1.Padding = new Padding(28, 0, 0, 0);
            b1.Size = new System.Drawing.Size(44, 32);
            b1.Symbol = 61697;
            b1.TabIndex = 3;
            b1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            b1.TipsText = null;
            b1.Click += b1_Click;
            // 
            // b2
            // 
            b2.BackColor = System.Drawing.Color.Transparent;
            b2.Cursor = Cursors.Hand;
            b2.Font = new System.Drawing.Font("宋体", 12F);
            b2.Location = new System.Drawing.Point(228, 135);
            b2.Margin = new Padding(0);
            b2.MinimumSize = new System.Drawing.Size(1, 1);
            b2.Name = "b2";
            b2.Padding = new Padding(28, 0, 0, 0);
            b2.Size = new System.Drawing.Size(44, 32);
            b2.Symbol = 61701;
            b2.TabIndex = 4;
            b2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            b2.TipsText = null;
            b2.Click += b2_Click;
            // 
            // b3
            // 
            b3.BackColor = System.Drawing.Color.Transparent;
            b3.Cursor = Cursors.Hand;
            b3.Font = new System.Drawing.Font("宋体", 12F);
            b3.Location = new System.Drawing.Point(228, 183);
            b3.Margin = new Padding(0);
            b3.MinimumSize = new System.Drawing.Size(1, 1);
            b3.Name = "b3";
            b3.Padding = new Padding(28, 0, 0, 0);
            b3.Size = new System.Drawing.Size(44, 32);
            b3.Symbol = 61700;
            b3.TabIndex = 5;
            b3.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            b3.TipsText = null;
            b3.Click += b3_Click;
            // 
            // b4
            // 
            b4.BackColor = System.Drawing.Color.Transparent;
            b4.Cursor = Cursors.Hand;
            b4.Font = new System.Drawing.Font("宋体", 12F);
            b4.Location = new System.Drawing.Point(228, 233);
            b4.Margin = new Padding(0);
            b4.MinimumSize = new System.Drawing.Size(1, 1);
            b4.Name = "b4";
            b4.Padding = new Padding(28, 0, 0, 0);
            b4.Size = new System.Drawing.Size(44, 32);
            b4.Symbol = 61696;
            b4.TabIndex = 6;
            b4.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            b4.TipsText = null;
            b4.Click += b4_Click;
            // 
            // l1
            // 
            l1.BackColor = System.Drawing.Color.Transparent;
            l1.Dock = DockStyle.Left;
            l1.FillColor = System.Drawing.Color.White;
            l1.Font = new System.Drawing.Font("宋体", 12F);
            l1.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            l1.ItemSelectForeColor = System.Drawing.Color.White;
            l1.Location = new System.Drawing.Point(1, 1);
            l1.Margin = new Padding(4, 5, 4, 5);
            l1.MinimumSize = new System.Drawing.Size(1, 1);
            l1.Name = "l1";
            l1.Padding = new Padding(2);
            l1.SelectionMode = SelectionMode.MultiExtended;
            l1.ShowText = false;
            l1.Size = new System.Drawing.Size(210, 348);
            l1.TabIndex = 7;
            l1.Text = null;
            l1.Click += l1_ItemClick;
            l1.DoubleClick += l1_DoubleClick;
            // 
            // l2
            // 
            l2.BackColor = System.Drawing.Color.Transparent;
            l2.Dock = DockStyle.Right;
            l2.FillColor = System.Drawing.Color.White;
            l2.Font = new System.Drawing.Font("宋体", 12F);
            l2.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            l2.ItemSelectForeColor = System.Drawing.Color.White;
            l2.Location = new System.Drawing.Point(289, 1);
            l2.Margin = new Padding(4, 5, 4, 5);
            l2.MinimumSize = new System.Drawing.Size(1, 1);
            l2.Name = "l2";
            l2.Padding = new Padding(2);
            l2.SelectionMode = SelectionMode.MultiExtended;
            l2.ShowText = false;
            l2.Size = new System.Drawing.Size(210, 348);
            l2.TabIndex = 8;
            l2.Text = null;
            l2.Click += l2_ItemClick;
            l2.DoubleClick += l2_DoubleClick;
            // 
            // UITransfer
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(l2);
            Controls.Add(l1);
            Controls.Add(b4);
            Controls.Add(b3);
            Controls.Add(b2);
            Controls.Add(b1);
            Margin = new Padding(7, 9, 7, 9);
            Name = "UITransfer";
            Padding = new Padding(1);
            RadiusSides = UICornerRadiusSides.None;
            RectSides = ToolStripStatusLabelBorderSides.None;
            Size = new System.Drawing.Size(500, 350);
            ResumeLayout(false);
        }

        #endregion
        private UISymbolButton b1;
        private UISymbolButton b2;
        private UISymbolButton b3;
        private UISymbolButton b4;
        private UIListBox l1;
        private UIListBox l2;
    }
}
