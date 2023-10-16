namespace Sunny.UI
{
    partial class UIProcessIndicatorForm
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
            components = new System.ComponentModel.Container();
            uiProgressIndicator1 = new UIProgressIndicator();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // uiProgressIndicator1
            // 
            uiProgressIndicator1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            uiProgressIndicator1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            uiProgressIndicator1.Location = new System.Drawing.Point(5, 5);
            uiProgressIndicator1.MinimumSize = new System.Drawing.Size(1, 1);
            uiProgressIndicator1.Name = "uiProgressIndicator1";
            uiProgressIndicator1.Size = new System.Drawing.Size(190, 190);
            uiProgressIndicator1.TabIndex = 0;
            uiProgressIndicator1.Text = "uiProgressIndicator1";
            uiProgressIndicator1.Tick += uiProgressIndicator1_Tick;
            // 
            // UIProcessIndicatorForm
            // 
            AllowShowTitle = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(200, 200);
            Controls.Add(uiProgressIndicator1);
            Name = "UIProcessIndicatorForm";
            Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            ShowTitle = false;
            Text = "UIProcessIndicatorForm";
            ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private UIProgressIndicator uiProgressIndicator1;
        private System.Windows.Forms.Timer timer1;
    }
}