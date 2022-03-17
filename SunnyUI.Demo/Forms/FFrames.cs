using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    public partial class FFrames : UIPage
    {
        public FFrames()
        {
            InitializeComponent();
        }

        private void ShowFrame(UIMainFrame frame)
        {
            frame.WindowState = FormWindowState.Maximized;
            frame.ShowDialog();
            frame.Dispose();
        }

        private void btnHM_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FHeaderMain());
        }

        private void btnHMF_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FHeaderMainFooter());
        }

        private void btnHAM_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FHeaderAsideMain());
        }

        private void btnHAMF_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FHeaderAsideMainFooter());
        }

        private void btnAM_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FAsideMain());
        }

        private void btnAHM_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FAsideHeaderMain());
        }

        private void btnAHMF_Click(object sender, System.EventArgs e)
        {
            ShowFrame(new FAsideHeaderMainFooter());
        }

        private void uiSymbolButton1_Click(object sender, System.EventArgs e)
        {
            FCustomMain main = new FCustomMain();
            main.ShowDialog();
            main.Dispose();
        }
    }
}