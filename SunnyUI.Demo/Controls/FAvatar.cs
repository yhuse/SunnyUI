namespace Sunny.UI.Demo
{
    public partial class FAvatar : UIPage
    {
        public FAvatar()
        {
            InitializeComponent();
        }

        public override void Init()
        {
            base.Init();
            uiGifAvatar1.Active = true;
        }

        public override void Final()
        {
            base.Final();
            uiGifAvatar1.Active = false;
        }

        private void uiAvatar4_Click(object sender, System.EventArgs e)
        {
            uiContextMenuStrip1.Show(uiAvatar4, 0, uiAvatar4.Height);
        }

        private void FAvatar_ReceiveParams(object sender, UIPageParamsArgs e)
        {
            if (e.SourcePage != null && e.SourcePage.PageIndex == 1002)
            {
                //来自页面1002的传值
                uiLabel1.Text = e.Value.ToString();
                e.Handled = true;
            }

            if (e.SourcePage == null)
            {
                //来自页面框架的传值
                uiLabel4.Text = e.Value.ToString();
                e.Handled = true;
            }
        }
    }
}
