namespace Sunny.UI.Demo.Controls
{
    public partial class FPieChart : UITitlePage
    {
        public FPieChart()
        {
            InitializeComponent();
        }

        private void uiImageButton1_Click(object sender, System.EventArgs e)
        {
            PieChart.ChartStyleType = UIChartStyleType.Default;
        }

        private void uiImageButton2_Click(object sender, System.EventArgs e)
        {
            PieChart.ChartStyleType = UIChartStyleType.Plain;
        }

        private void uiImageButton3_Click(object sender, System.EventArgs e)
        {
            PieChart.ChartStyleType = UIChartStyleType.Dark;
        }
    }
}