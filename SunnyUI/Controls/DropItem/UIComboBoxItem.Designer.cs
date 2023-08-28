namespace Sunny.UI
{
    partial class UIComboBoxItem
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
            this.listBox = new Sunny.UI.UIListBox();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.BackColor = System.Drawing.Color.Transparent;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FillColor = System.Drawing.Color.White;
            this.listBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBox.FormatString = "";
            this.listBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.listBox.Location = new System.Drawing.Point(0, 0);
            this.listBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.listBox.Name = "listBox";
            this.listBox.Padding = new System.Windows.Forms.Padding(2);
            this.listBox.Radius = 0;
            this.listBox.Size = new System.Drawing.Size(184, 210);
            this.listBox.TabIndex = 3;
            this.listBox.Text = null;
            this.listBox.Click += new System.EventHandler(this.ListBox_Click);
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            // 
            // UIComboBoxItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.listBox);
            this.Name = "UIComboBoxItem";
            this.Size = new System.Drawing.Size(184, 210);
            this.ResumeLayout(false);

        }

        #endregion

        private UIListBox listBox;
    }
}
