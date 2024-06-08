/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIMessageForm2.cs
 * 文件说明: 消息提示窗体2
 * 当前版本: V3.6
 * 创建日期: 2024-05-16
 *
 * 2024-05-16: V3.6.6 增加文件说明
 * 2024-06-08: V3.6.6 统一配色
******************************************************************************/

using System.Drawing;

namespace Sunny.UI
{
    public partial class UIMessageForm2 : UIForm2
    {
        public UIMessageForm2(string title, string message, UINotifierType noteType, UIMessageDialogButtons defaultButton = UIMessageDialogButtons.Cancel)
        {
            InitializeComponent();
            Text = title;
            label1.Text = message;
            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;

            foreColor = Color.Black;


            if (noteType != UINotifierType.Ask)
            {
                btnOK.Left = btnCancel.Left;
                btnCancel.Visible = false;
                btnOK.TabIndex = 0;
            }
            else
            {
                if (defaultButton == UIMessageDialogButtons.Cancel)
                    btnCancel.TabIndex = 0;
                else
                    btnOK.TabIndex = 0;
            }

            switch (noteType)
            {
                case UINotifierType.ERROR:
                    btnOK.Style = Style = UIStyle.Red;
                    Symbol = 361527;
                    SymbolColor = UIStyles.Red.ButtonFillColor;
                    break;

                case UINotifierType.INFO:
                    Symbol = 361530;
                    SymbolColor = UIStyles.ActiveStyleColor.ButtonFillColor;
                    break;

                case UINotifierType.WARNING:
                    btnOK.Style = Style = UIStyle.Orange;
                    Symbol = 361553;
                    SymbolColor = UIStyles.Orange.ButtonFillColor;
                    break;

                case UINotifierType.OK:
                    btnOK.Style = Style = UIStyle.Green;
                    Symbol = 361528;
                    SymbolColor = UIStyles.Green.ButtonFillColor;
                    break;

                case UINotifierType.Ask:
                    Symbol = 361529;
                    SymbolColor = UIStyles.ActiveStyleColor.ButtonFillColor;
                    break;
            }
        }

        int Symbol = 361528;
        Color SymbolColor = UIStyles.Green.ButtonFillColor;
        private Color Color;
        private Color foreColor;

        public string Message { get; set; }
        private void UIMessageForm2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color[] colors = Color.White.GradientColors(TitleColor, 16);
            Color = colors[2];
            if (Style == UIStyle.Inherited && (UIStyles.Style == UIStyle.DarkBlue || UIStyles.Style == UIStyle.Black))
            {
                Color[] colors1 = UIStyles.ActiveStyleColor.PrimaryColor.GradientColors(Color.Black, 16);
                Color = colors1[2];
                foreColor = Color.White;
            }

            int height = (190 - 48 + TitleHeight) + label1.Height;
            if (height > 210) Height = height;
            e.Graphics.FillRectangle(Color, new RectangleF(0, Height - 76, Width, 76));
            e.Graphics.DrawFontImage(Symbol, 72, SymbolColor, new RectangleF(28, 64, 64, 64));
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            delay--;
            Text = text + " [" + delay + "]";

            if (delay <= 0) Close();
        }

        int delay = 0;

        public int Delay
        {
            set
            {
                if (value > 0)
                {
                    delay = value / 1000;
                    timer1.Start();
                }
            }
        }

        string text = "";

        private void UIMessageForm2_Shown(object sender, System.EventArgs e)
        {
            if (delay <= 0) return;
            if (text == "") text = Text;
            Text = text + " [" + delay + "]";
        }

        private void UIMessageForm2_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            timer1.Stop();
        }
    }
}
