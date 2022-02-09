namespace Sunny.UI.Demo
{
    public partial class FAvatar : UIPage
    {
        public FAvatar()
        {
            InitializeComponent();
        }

        private void uiAvatar4_Click(object sender, System.EventArgs e)
        {
            uiContextMenuStrip1.Show(uiAvatar4, 0, uiAvatar4.Height);
        }

        public override bool SetParam(int fromPageIndex, params object[] objects)
        {
            if (fromPageIndex == 1002 && objects.Length == 1)
            {
                uiLabel1.Text = objects[0].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
