using System.ComponentModel;

namespace Sunny.UI
{
    public partial class UIProcessIndicatorForm : UIForm
    {
        public UIProcessIndicatorForm()
        {
            InitializeComponent();
            timer1.Start();
            uiProgressIndicator1.Active = true;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (NeedClose)
            {
                uiProgressIndicator1.Active = false;
                Close();
            }
        }

        [DefaultValue(false), Browsable(false)]
        public bool NeedClose { get; set; }
    }
}
