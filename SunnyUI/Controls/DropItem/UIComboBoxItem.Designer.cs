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
            listBox = new UIListBox();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.BackColor = System.Drawing.Color.Transparent;
            listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox.FillColor = System.Drawing.Color.White;
            listBox.Font = new System.Drawing.Font("宋体", 12F);
            listBox.HoverColor = System.Drawing.Color.FromArgb(155, 200, 255);
            listBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(235, 243, 255);
            listBox.Location = new System.Drawing.Point(0, 0);
            listBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            listBox.MinimumSize = new System.Drawing.Size(1, 1);
            listBox.Name = "listBox";
            listBox.Padding = new System.Windows.Forms.Padding(2);
            listBox.Radius = 0;
            listBox.ShowText = false;
            listBox.Size = new System.Drawing.Size(184, 210);
            listBox.TabIndex = 3;
            listBox.Text = null;
            listBox.KeyDown += listBox_KeyDown;
            listBox.Click += ListBox_Click;
            listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
            // 
            // UIComboBoxItem
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(listBox);
            Name = "UIComboBoxItem";
            Size = new System.Drawing.Size(184, 210);
            ResumeLayout(false);
        }

        #endregion

        private UIListBox listBox;
    }
}
