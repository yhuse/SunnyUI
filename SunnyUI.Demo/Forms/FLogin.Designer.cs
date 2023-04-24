namespace Sunny.UI.Demo
{
    partial class FLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLogin));
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "SunnyUI.Net Login Form";
            // 
            // lblSubText
            // 
            this.lblSubText.Text = "SunnyUI.Net";
            // 
            // FLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(750, 450);
            this.FocusControl = Sunny.UI.UILoginFormFocusControl.Password;
            this.LoginImage = Sunny.UI.UILoginForm.UILoginImage.Login6;
            this.Name = "FLogin";
            this.SubText = "SunnyUI.Net";
            this.Text = "SunnyUI.Net Login Form";
            this.Title = "SunnyUI.Net Login Form";
            this.UserName = "admin";
            this.ButtonLoginClick += new System.EventHandler(this.FLogin_ButtonLoginClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}