using System.ComponentModel;

namespace Sunny.UI
{
    public partial class UIWaitForm : UIForm
    {
        public UIWaitForm()
        {
            InitializeComponent();
            base.Text = UILocalize.InfoTitle;
            SetDescription(UILocalize.SystemProcessing);
        }

        public UIWaitForm(string desc)
        {
            InitializeComponent();
            base.Text = UILocalize.InfoTitle;
            SetDescription(desc);
        }

        private delegate void SetTextHandler(string text);

        public void SetDescription(string text)
        {
            if (labelDescription.InvokeRequired)
            {
                Invoke(new SetTextHandler(SetDescription), text);
            }
            else
            {
                labelDescription.Text = text;
                labelDescription.Invalidate();
            }
        }

        [DefaultValue(false), Browsable(false)]
        public bool NeedClose { get; set; }

        private void Bar_Tick(object sender, System.EventArgs e)
        {
            if (NeedClose) Close();
        }
    }
}
