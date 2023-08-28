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
            this.b1 = new Sunny.UI.UISymbolButton();
            this.b2 = new Sunny.UI.UISymbolButton();
            this.b3 = new Sunny.UI.UISymbolButton();
            this.b4 = new Sunny.UI.UISymbolButton();
            this.l1 = new Sunny.UI.UIListBox();
            this.l2 = new Sunny.UI.UIListBox();
            this.SuspendLayout();
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b1.Location = new System.Drawing.Point(228, 85);
            this.b1.Margin = new System.Windows.Forms.Padding(0);
            this.b1.MinimumSize = new System.Drawing.Size(1, 1);
            this.b1.Name = "b1";
            this.b1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.b1.Size = new System.Drawing.Size(44, 32);
            this.b1.Symbol = 61697;
            this.b1.TabIndex = 3;
            this.b1.TipsText = null;
            this.b1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b2.Location = new System.Drawing.Point(228, 135);
            this.b2.Margin = new System.Windows.Forms.Padding(0);
            this.b2.MinimumSize = new System.Drawing.Size(1, 1);
            this.b2.Name = "b2";
            this.b2.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.b2.Size = new System.Drawing.Size(44, 32);
            this.b2.Symbol = 61701;
            this.b2.TabIndex = 4;
            this.b2.TipsText = null;
            this.b2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // b3
            // 
            this.b3.BackColor = System.Drawing.Color.Transparent;
            this.b3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b3.Location = new System.Drawing.Point(228, 183);
            this.b3.Margin = new System.Windows.Forms.Padding(0);
            this.b3.MinimumSize = new System.Drawing.Size(1, 1);
            this.b3.Name = "b3";
            this.b3.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.b3.Size = new System.Drawing.Size(44, 32);
            this.b3.Symbol = 61700;
            this.b3.TabIndex = 5;
            this.b3.TipsText = null;
            this.b3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.b3.Click += new System.EventHandler(this.b3_Click);
            // 
            // b4
            // 
            this.b4.BackColor = System.Drawing.Color.Transparent;
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b4.Location = new System.Drawing.Point(228, 233);
            this.b4.Margin = new System.Windows.Forms.Padding(0);
            this.b4.MinimumSize = new System.Drawing.Size(1, 1);
            this.b4.Name = "b4";
            this.b4.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.b4.Size = new System.Drawing.Size(44, 32);
            this.b4.Symbol = 61696;
            this.b4.TabIndex = 6;
            this.b4.TipsText = null;
            this.b4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.b4.Click += new System.EventHandler(this.b4_Click);
            // 
            // l1
            // 
            this.l1.BackColor = System.Drawing.Color.Transparent;
            this.l1.Dock = System.Windows.Forms.DockStyle.Left;
            this.l1.FillColor = System.Drawing.Color.White;
            this.l1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.l1.Location = new System.Drawing.Point(1, 1);
            this.l1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.l1.MinimumSize = new System.Drawing.Size(1, 1);
            this.l1.Name = "l1";
            this.l1.Padding = new System.Windows.Forms.Padding(2);
            this.l1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.l1.ShowText = false;
            this.l1.Size = new System.Drawing.Size(210, 348);
            this.l1.TabIndex = 7;
            this.l1.Text = null;
            this.l1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.l1.Click += new System.EventHandler(this.l1_ItemClick);
            this.l1.DoubleClick += new System.EventHandler(this.l1_DoubleClick);
            // 
            // l2
            // 
            this.l2.BackColor = System.Drawing.Color.Transparent;
            this.l2.Dock = System.Windows.Forms.DockStyle.Right;
            this.l2.FillColor = System.Drawing.Color.White;
            this.l2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.l2.Location = new System.Drawing.Point(289, 1);
            this.l2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.l2.MinimumSize = new System.Drawing.Size(1, 1);
            this.l2.Name = "l2";
            this.l2.Padding = new System.Windows.Forms.Padding(2);
            this.l2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.l2.ShowText = false;
            this.l2.Size = new System.Drawing.Size(210, 348);
            this.l2.TabIndex = 8;
            this.l2.Text = null;
            this.l2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.l2.Click += new System.EventHandler(this.l2_ItemClick);
            this.l2.DoubleClick += new System.EventHandler(this.l2_DoubleClick);
            // 
            // UITransfer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.l2);
            this.Controls.Add(this.l1);
            this.Controls.Add(this.b4);
            this.Controls.Add(this.b3);
            this.Controls.Add(this.b2);
            this.Controls.Add(this.b1);
            this.Margin = new System.Windows.Forms.Padding(7, 9, 7, 9);
            this.Name = "UITransfer";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Size = new System.Drawing.Size(500, 350);
            this.ResumeLayout(false);

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
