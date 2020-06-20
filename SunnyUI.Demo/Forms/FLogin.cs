using System;

namespace Sunny.UI.Demo.Forms
{
    public partial class FLogin : UILoginForm
    {
        public FLogin()
        {
            InitializeComponent();
        }

        private void FLogin_ButtonLoginClick(object sender, EventArgs e)
        {
            if (UserName == "admin" && Password == "admin")
            {
                IsLogin = true;
                Close();
            }
        }
    }
}