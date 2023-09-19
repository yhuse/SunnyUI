using System;
using System.Drawing;
using System.Drawing.Text;

namespace Sunny.UI.Demo
{
    public partial class FColorful : UIPage
    {
        public FColorful()
        {
            InitializeComponent();
            uiPanel11.FillColor = uiPanel11.RectColor = RandomColor.GetColor(ColorScheme.Random, Luminosity.Bright);
            uiLabel2.Text = "RGB: " + uiPanel11.FillColor.R + ", " + uiPanel11.FillColor.G + ", " + uiPanel11.FillColor.B;

            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                cbFont.Items.Add(family.Name);
            }

            cbFont.SelectedIndex = cbFont.Items.IndexOf(SystemFonts.DefaultFont.Name);
        }

        private void uiPanel1_Click(object sender, System.EventArgs e)
        {
            var panel = (UIPanel)sender;
            UIStyles.InitColorful(panel.FillColor, Color.White);
        }

        private void uiPanel11_Click(object sender, System.EventArgs e)
        {
            uiPanel11.FillColor = uiPanel11.RectColor = RandomColor.GetColor(ColorScheme.Random, Luminosity.Bright);
            uiLabel2.Text = "RGB: " + uiPanel11.FillColor.R + ", " + uiPanel11.FillColor.G + ", " + uiPanel11.FillColor.B;
            UIStyles.InitColorful(uiPanel11.FillColor, Color.White);
        }

        public static System.Drawing.Color GetRandomColor()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int int_Red = random.Next(255);
            int int_Green = random.Next(255);
            int int_Blue = random.Next(255);
            int_Blue = (int_Red + int_Green > 380) ? int_Red + int_Green - 380 : int_Blue;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return GetDarkerColor(System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue));
        }

        public static Color GetDarkerColor(Color color)
        {
            const int max = 255;
            int increase = new Random(Guid.NewGuid().GetHashCode()).Next(30, 220); //还可以根据需要调整此处的值 
            int r = Math.Abs(Math.Min(color.R - increase, max));
            int g = Math.Abs(Math.Min(color.G - increase, max));
            int b = Math.Abs(Math.Min(color.B - increase, max));
            return Color.FromArgb(r, g, b);
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            UIStyles.DPIScale = true;
            UIStyles.GlobalFont = true;
            UIStyles.GlobalFontName = cbFont.Text;
            UIStyles.GlobalFontScale = uiTrackBar1.Value;
            UIStyles.SetDPIScale();
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            uiLabel5.Text = uiTrackBar1.Value.ToString();
            UIStyles.GlobalFontScale = uiTrackBar1.Value;
        }
    }
}
