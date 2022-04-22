namespace Sunny.UI.Demo
{
    public partial class FCustomMain : UIForm
    {
        public FCustomMain()
        {
            InitializeComponent();

            //新建一个窗体，改继承Form为UIForm
            //窗体上只要放一个UITabControl，并关联到UIForm，这样多页面框架就已经打好了，其余的可以自由发挥
            //窗体上如果只有一个UITabControl，也会自动关联，超过一个需要手动关联
            MainTabControl = uiTabControl1;

            //有三个UIPage，分别为：
            //FPage1，其属性PageIndex为1001，切记要设置PageIndex，就靠这个做关联的，整数，唯一
            //FPage2，其属性PageIndex为1002
            //FPage3，其属性PageIndex为1003

            //设置FTitlePage1为主页面，不能被关闭
            var mainPage = new FPage1();
            uiTabControl1.MainPage = mainPage.Text = "主页";
            AddPage(mainPage);
        }

        private void uiButton1_Click(object sender, System.EventArgs e)
        {
            //因为主页面已经加进去了，直接选取即可
            SelectPage(1001);
        }

        private void uiButton2_Click(object sender, System.EventArgs e)
        {
            if (!ExistPage(1002))
            {
                AddPage(new FPage2());
            }

            SelectPage(1002);
        }

        private void uiButton3_Click(object sender, System.EventArgs e)
        {
            if (!ExistPage(1003))
            {
                AddPage(new FPage3());
            }

            SelectPage(1003);
        }
    }
}
