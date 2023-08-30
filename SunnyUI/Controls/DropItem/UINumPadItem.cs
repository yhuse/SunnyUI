using System.ComponentModel;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UINumPadItem : UIDropDownItem
    {
        public UINumPadItem()
        {
            InitializeComponent();
        }
        private NumPadType numPadType = NumPadType.Text;

        [DefaultValue(NumPadType.Text)]
        [Description("小键盘类型"), Category("SunnyUI")]
        public NumPadType NumPadType
        {
            get => numPadType;
            set
            {
                numPadType = value;
                uiSymbolButton2.Enabled = true;
                uiSymbolButton3.Enabled = true;
                uiSymbolButton9.Enabled = true;

                uiSymbolButton9.Text = ".";
                uiSymbolButton9.Tag = 110;
                switch (numPadType)
                {
                    case NumPadType.Text:
                        break;
                    case NumPadType.Integer:
                        uiSymbolButton9.Enabled = false;
                        break;
                    case NumPadType.Double:
                        break;
                    case NumPadType.IDNumber:
                        uiSymbolButton2.Enabled = false;
                        uiSymbolButton3.Enabled = false;
                        uiSymbolButton9.Text = "X";
                        uiSymbolButton9.Tag = 88;
                        break;
                    default:
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            uiSymbolButton1 = new UISymbolButton();
            uiSymbolButton2 = new UISymbolButton();
            uiSymbolButton3 = new UISymbolButton();
            uiSymbolButton4 = new UISymbolButton();
            uiSymbolButton6 = new UISymbolButton();
            uiSymbolButton7 = new UISymbolButton();
            uiSymbolButton8 = new UISymbolButton();
            uiSymbolButton9 = new UISymbolButton();
            uiSymbolButton10 = new UISymbolButton();
            uiSymbolButton11 = new UISymbolButton();
            uiSymbolButton12 = new UISymbolButton();
            uiSymbolButton13 = new UISymbolButton();
            uiSymbolButton14 = new UISymbolButton();
            uiSymbolButton15 = new UISymbolButton();
            uiSymbolButton16 = new UISymbolButton();
            SuspendLayout();
            // 
            // uiSymbolButton1
            // 
            uiSymbolButton1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton1.Location = new Point(244, 13);
            uiSymbolButton1.MinimumSize = new Size(1, 1);
            uiSymbolButton1.Name = "uiSymbolButton1";
            uiSymbolButton1.Size = new Size(62, 65);
            uiSymbolButton1.Symbol = 362810;
            uiSymbolButton1.SymbolSize = 32;
            uiSymbolButton1.TabIndex = 0;
            uiSymbolButton1.Tag = "8";
            uiSymbolButton1.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton2
            // 
            uiSymbolButton2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton2.Location = new Point(244, 93);
            uiSymbolButton2.MinimumSize = new Size(1, 1);
            uiSymbolButton2.Name = "uiSymbolButton2";
            uiSymbolButton2.Size = new Size(62, 65);
            uiSymbolButton2.Symbol = 361543;
            uiSymbolButton2.TabIndex = 1;
            uiSymbolButton2.Tag = "107";
            uiSymbolButton2.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton3
            // 
            uiSymbolButton3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton3.Location = new Point(244, 173);
            uiSymbolButton3.MinimumSize = new Size(1, 1);
            uiSymbolButton3.Name = "uiSymbolButton3";
            uiSymbolButton3.Size = new Size(62, 65);
            uiSymbolButton3.Symbol = 361544;
            uiSymbolButton3.TabIndex = 2;
            uiSymbolButton3.Tag = "109";
            uiSymbolButton3.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton4
            // 
            uiSymbolButton4.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton4.Location = new Point(167, 253);
            uiSymbolButton4.MinimumSize = new Size(1, 1);
            uiSymbolButton4.Name = "uiSymbolButton4";
            uiSymbolButton4.Size = new Size(139, 65);
            uiSymbolButton4.Symbol = 0;
            uiSymbolButton4.TabIndex = 3;
            uiSymbolButton4.Tag = "13";
            uiSymbolButton4.Text = "Enter";
            uiSymbolButton4.Click += uiSymbolButton4_Click;
            // 
            // uiSymbolButton6
            // 
            uiSymbolButton6.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton6.Location = new Point(167, 173);
            uiSymbolButton6.MinimumSize = new Size(1, 1);
            uiSymbolButton6.Name = "uiSymbolButton6";
            uiSymbolButton6.Size = new Size(62, 65);
            uiSymbolButton6.Symbol = 0;
            uiSymbolButton6.TabIndex = 6;
            uiSymbolButton6.Tag = "51";
            uiSymbolButton6.Text = "3";
            uiSymbolButton6.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton7
            // 
            uiSymbolButton7.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton7.Location = new Point(167, 93);
            uiSymbolButton7.MinimumSize = new Size(1, 1);
            uiSymbolButton7.Name = "uiSymbolButton7";
            uiSymbolButton7.Size = new Size(62, 65);
            uiSymbolButton7.Symbol = 0;
            uiSymbolButton7.TabIndex = 5;
            uiSymbolButton7.Tag = "54";
            uiSymbolButton7.Text = "6";
            uiSymbolButton7.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton8
            // 
            uiSymbolButton8.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton8.Location = new Point(167, 13);
            uiSymbolButton8.MinimumSize = new Size(1, 1);
            uiSymbolButton8.Name = "uiSymbolButton8";
            uiSymbolButton8.Size = new Size(62, 65);
            uiSymbolButton8.Symbol = 0;
            uiSymbolButton8.SymbolSize = 32;
            uiSymbolButton8.TabIndex = 4;
            uiSymbolButton8.Tag = "57";
            uiSymbolButton8.Text = "9";
            uiSymbolButton8.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton9
            // 
            uiSymbolButton9.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton9.Location = new Point(90, 253);
            uiSymbolButton9.MinimumSize = new Size(1, 1);
            uiSymbolButton9.Name = "uiSymbolButton9";
            uiSymbolButton9.Size = new Size(62, 65);
            uiSymbolButton9.Symbol = 0;
            uiSymbolButton9.TabIndex = 11;
            uiSymbolButton9.Tag = "110";
            uiSymbolButton9.Text = ".";
            uiSymbolButton9.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton10
            // 
            uiSymbolButton10.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton10.Location = new Point(90, 173);
            uiSymbolButton10.MinimumSize = new Size(1, 1);
            uiSymbolButton10.Name = "uiSymbolButton10";
            uiSymbolButton10.Size = new Size(62, 65);
            uiSymbolButton10.Symbol = 0;
            uiSymbolButton10.TabIndex = 10;
            uiSymbolButton10.Tag = "50";
            uiSymbolButton10.Text = "2";
            uiSymbolButton10.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton11
            // 
            uiSymbolButton11.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton11.Location = new Point(90, 93);
            uiSymbolButton11.MinimumSize = new Size(1, 1);
            uiSymbolButton11.Name = "uiSymbolButton11";
            uiSymbolButton11.Size = new Size(62, 65);
            uiSymbolButton11.Symbol = 0;
            uiSymbolButton11.TabIndex = 9;
            uiSymbolButton11.Tag = "53";
            uiSymbolButton11.Text = "5";
            uiSymbolButton11.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton12
            // 
            uiSymbolButton12.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton12.Location = new Point(90, 13);
            uiSymbolButton12.MinimumSize = new Size(1, 1);
            uiSymbolButton12.Name = "uiSymbolButton12";
            uiSymbolButton12.Size = new Size(62, 65);
            uiSymbolButton12.Symbol = 0;
            uiSymbolButton12.SymbolSize = 32;
            uiSymbolButton12.TabIndex = 8;
            uiSymbolButton12.Tag = "56";
            uiSymbolButton12.Text = "8";
            uiSymbolButton12.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton13
            // 
            uiSymbolButton13.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton13.Location = new Point(13, 253);
            uiSymbolButton13.MinimumSize = new Size(1, 1);
            uiSymbolButton13.Name = "uiSymbolButton13";
            uiSymbolButton13.Size = new Size(62, 65);
            uiSymbolButton13.Symbol = 0;
            uiSymbolButton13.TabIndex = 15;
            uiSymbolButton13.Tag = "48";
            uiSymbolButton13.Text = "0";
            uiSymbolButton13.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton14
            // 
            uiSymbolButton14.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton14.Location = new Point(13, 173);
            uiSymbolButton14.MinimumSize = new Size(1, 1);
            uiSymbolButton14.Name = "uiSymbolButton14";
            uiSymbolButton14.Size = new Size(62, 65);
            uiSymbolButton14.Symbol = 0;
            uiSymbolButton14.TabIndex = 14;
            uiSymbolButton14.Tag = "49";
            uiSymbolButton14.Text = "1";
            uiSymbolButton14.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton15
            // 
            uiSymbolButton15.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton15.Location = new Point(13, 93);
            uiSymbolButton15.MinimumSize = new Size(1, 1);
            uiSymbolButton15.Name = "uiSymbolButton15";
            uiSymbolButton15.Size = new Size(62, 65);
            uiSymbolButton15.Symbol = 0;
            uiSymbolButton15.TabIndex = 13;
            uiSymbolButton15.Tag = "52";
            uiSymbolButton15.Text = "4";
            uiSymbolButton15.Click += uiSymbolButton16_Click;
            // 
            // uiSymbolButton16
            // 
            uiSymbolButton16.Font = new Font("宋体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            uiSymbolButton16.Location = new Point(13, 13);
            uiSymbolButton16.MinimumSize = new Size(1, 1);
            uiSymbolButton16.Name = "uiSymbolButton16";
            uiSymbolButton16.Size = new Size(62, 65);
            uiSymbolButton16.Symbol = 0;
            uiSymbolButton16.SymbolSize = 32;
            uiSymbolButton16.TabIndex = 12;
            uiSymbolButton16.Tag = "55";
            uiSymbolButton16.Text = "7";
            uiSymbolButton16.Click += uiSymbolButton16_Click;
            // 
            // UINumPadItem
            // 
            Controls.Add(uiSymbolButton13);
            Controls.Add(uiSymbolButton14);
            Controls.Add(uiSymbolButton15);
            Controls.Add(uiSymbolButton16);
            Controls.Add(uiSymbolButton9);
            Controls.Add(uiSymbolButton10);
            Controls.Add(uiSymbolButton11);
            Controls.Add(uiSymbolButton12);
            Controls.Add(uiSymbolButton6);
            Controls.Add(uiSymbolButton7);
            Controls.Add(uiSymbolButton8);
            Controls.Add(uiSymbolButton4);
            Controls.Add(uiSymbolButton3);
            Controls.Add(uiSymbolButton2);
            Controls.Add(uiSymbolButton1);
            Name = "UINumPadItem";
            Size = new Size(319, 331);
            ResumeLayout(false);
        }

        private UISymbolButton uiSymbolButton1;
        private UISymbolButton uiSymbolButton2;
        private UISymbolButton uiSymbolButton3;
        private UISymbolButton uiSymbolButton4;
        private UISymbolButton uiSymbolButton6;
        private UISymbolButton uiSymbolButton7;
        private UISymbolButton uiSymbolButton8;
        private UISymbolButton uiSymbolButton9;
        private UISymbolButton uiSymbolButton10;
        private UISymbolButton uiSymbolButton11;
        private UISymbolButton uiSymbolButton12;
        private UISymbolButton uiSymbolButton13;
        private UISymbolButton uiSymbolButton14;
        private UISymbolButton uiSymbolButton15;
        private UISymbolButton uiSymbolButton16;

        private void uiSymbolButton4_Click(object sender, System.EventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            DoValueChanged(this, btn.Tag.ToString().ToInt());
            Close();
        }

        private void uiSymbolButton16_Click(object sender, System.EventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            DoValueChanged(this, btn.Tag.ToString().ToInt());
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            fillColor = Color.White;
            foreColor = uiColor.DropDownPanelForeColor;
            RectColor = uiColor.RectColor;

            foreach (var item in this.GetControls<UISymbolButton>(true))
            {
                item.SetStyleColor(uiColor);
            }
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            foreach (var label in this.GetControls<UISymbolButton>()) label.SetDPIScale();
        }
    }
}
