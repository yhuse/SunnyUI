namespace Sunny.UI
{
    sealed partial class UITextBox
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
                CloseTips();
                components.Dispose();
            }

            edit?.Dispose();
            bar?.Dispose();
            btn?.Dispose();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // UITextBox
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Cursor = System.Windows.Forms.Cursors.IBeam;
            Name = "UITextBox";
            Padding = new System.Windows.Forms.Padding(5);
            Size = new System.Drawing.Size(250, 29);
            ResumeLayout(false);

        }

        #endregion
    }
}
