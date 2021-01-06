namespace Sunny.UI.Demo
{
    public partial class FFlowLayoutPanel : UITitlePage
    {
        public FFlowLayoutPanel()
        {
            InitializeComponent();


        }

        public override void Init()
        {
            base.Init();
            uiFlowLayoutPanel1.Clear();
            index = 0;

            for (int i = 0; i < 30; i++)
            {
                uiButton1.PerformClick();
            }
        }

        private int index;
        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            UIButton btn = new UIButton();
            btn.Text = "Button" + index++.ToString("D2");
            uiFlowLayoutPanel1.AddControl(btn);
        }
    }
}
