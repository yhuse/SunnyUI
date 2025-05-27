using System;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UIColorItem : UIDropDownItem, ITranslate
    {
        public UIColorItem()
        {
            InitializeComponent();
            Translate();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            m_opacitySlider.SetDPIScale();
            m_colorBar.SetDPIScale();
            btnOK.SetDPIScale();
            btnCancel.SetDPIScale();
            foreach (var label in this.GetControls<UILabel>()) label.SetDPIScale();
            foreach (var label in this.GetControls<UITextBox>()) label.SetDPIScale();
        }

        public void Translate()
        {
            btnOK.Text = UIStyles.CurrentResources.OK;
            btnCancel.Text = UIStyles.CurrentResources.Cancel;
        }

        private LabelRotate m_colorSample;
        private UITextBox edtA;
        private UITextBox edtR;
        private UITextBox edtG;
        private UITextBox edtB;
        private UILabel lblA;
        private UILabel lblR;
        private UILabel lblG;
        private UILabel lblB;
        private UIColorWheel m_colorWheel;
        private HSLColorSlider m_colorBar;
        private ColorSlider m_opacitySlider;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UIColorTable m_colorTable;

        private void InitializeComponent()
        {
            m_colorTable = new UIColorTable();
            m_colorSample = new LabelRotate();
            edtA = new UITextBox();
            edtR = new UITextBox();
            edtG = new UITextBox();
            edtB = new UITextBox();
            lblA = new UILabel();
            lblR = new UILabel();
            lblG = new UILabel();
            lblB = new UILabel();
            m_colorWheel = new UIColorWheel();
            m_colorBar = new HSLColorSlider();
            m_opacitySlider = new ColorSlider();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            SuspendLayout();
            // 
            // m_colorTable
            // 
            m_colorTable.BackColor = Color.Transparent;
            m_colorTable.Colors = new Color[]
    {
    Color.Black,
    Color.DarkGreen,
    Color.DarkSlateGray,
    Color.Purple,
    Color.Maroon,
    Color.Teal,
    Color.Green,
    Color.Olive,
    Color.Navy,
    Color.Indigo,
    Color.MidnightBlue,
    Color.DarkRed,
    Color.DarkMagenta,
    Color.DarkBlue,
    Color.DarkCyan,
    Color.DarkOliveGreen,
    Color.SaddleBrown,
    Color.ForestGreen,
    Color.OliveDrab,
    Color.SeaGreen,
    Color.DarkGoldenrod,
    Color.DarkSlateBlue,
    Color.MediumBlue,
    Color.Sienna,
    Color.Brown,
    Color.DarkTurquoise,
    Color.DimGray,
    Color.LightSeaGreen,
    Color.DarkViolet,
    Color.Firebrick,
    Color.MediumVioletRed,
    Color.MediumSeaGreen,
    Color.Crimson,
    Color.Chocolate,
    Color.MediumSpringGreen,
    Color.Goldenrod,
    Color.SteelBlue,
    Color.LawnGreen,
    Color.DarkOrchid,
    Color.Orange,
    Color.LimeGreen,
    Color.Yellow,
    Color.Gold,
    Color.Red,
    Color.Magenta,
    Color.Lime,
    Color.SpringGreen,
    Color.YellowGreen,
    Color.Chartreuse,
    Color.DeepSkyBlue,
    Color.Aqua,
    Color.OrangeRed,
    Color.Blue,
    Color.DarkOrange,
    Color.CadetBlue,
    Color.Cyan,
    Color.Fuchsia,
    Color.Gray,
    Color.SlateGray,
    Color.Peru,
    Color.BlueViolet,
    Color.LightSlateGray,
    Color.DeepPink,
    Color.MediumTurquoise,
    Color.DodgerBlue,
    Color.Turquoise,
    Color.RoyalBlue,
    Color.SlateBlue,
    Color.MediumOrchid,
    Color.DarkKhaki,
    Color.IndianRed,
    Color.GreenYellow,
    Color.MediumAquamarine,
    Color.Tomato,
    Color.DarkSeaGreen,
    Color.Orchid,
    Color.RosyBrown,
    Color.PaleVioletRed,
    Color.MediumPurple,
    Color.Coral,
    Color.CornflowerBlue,
    Color.DarkGray,
    Color.SandyBrown,
    Color.MediumSlateBlue,
    Color.Tan,
    Color.DarkSalmon,
    Color.BurlyWood,
    Color.HotPink,
    Color.Salmon,
    Color.Violet,
    Color.LightCoral,
    Color.SkyBlue,
    Color.LightSalmon,
    Color.Khaki,
    Color.Plum,
    Color.LightGreen,
    Color.Aquamarine,
    Color.Silver,
    Color.LightSkyBlue,
    Color.LightSteelBlue,
    Color.LightBlue,
    Color.PaleGreen,
    Color.PowderBlue,
    Color.Thistle,
    Color.PaleGoldenrod,
    Color.PaleTurquoise,
    Color.LightGray,
    Color.Wheat,
    Color.NavajoWhite,
    Color.Moccasin,
    Color.LightPink,
    Color.PeachPuff,
    Color.Gainsboro,
    Color.Pink,
    Color.Bisque,
    Color.LightGoldenrodYellow,
    Color.LemonChiffon,
    Color.BlanchedAlmond,
    Color.Beige,
    Color.AntiqueWhite,
    Color.PapayaWhip,
    Color.Cornsilk,
    Color.LightYellow,
    Color.LightCyan,
    Color.Lavender,
    Color.Linen,
    Color.MistyRose,
    Color.OldLace,
    Color.WhiteSmoke,
    Color.SeaShell,
    Color.Azure,
    Color.Honeydew,
    Color.Ivory,
    Color.LavenderBlush,
    Color.FloralWhite,
    Color.AliceBlue,
    Color.MintCream,
    Color.GhostWhite,
    Color.Snow,
    Color.White
    };
            m_colorTable.Cols = 16;
            m_colorTable.FieldSize = new Size(12, 12);
            m_colorTable.FrameColor = Color.FromArgb(80, 160, 255);
            m_colorTable.Location = new Point(10, 8);
            m_colorTable.Name = "m_colorTable";
            m_colorTable.Padding = new System.Windows.Forms.Padding(8);
            m_colorTable.RotatePointAlignment = ContentAlignment.MiddleCenter;
            m_colorTable.SelectedItem = Color.Black;
            m_colorTable.Size = new Size(253, 148);
            m_colorTable.Style = UIStyle.Custom;
            m_colorTable.TabIndex = 0;
            m_colorTable.Text = "colorTable1";
            m_colorTable.TextAlign = ContentAlignment.MiddleLeft;
            m_colorTable.TextAngle = 0F;
            m_colorTable.SelectedIndexChanged += m_colorTable_SelectedIndexChanged;
            // 
            // m_colorSample
            // 
            m_colorSample.FrameColor = Color.FromArgb(80, 160, 255);
            m_colorSample.Location = new Point(10, 163);
            m_colorSample.Name = "m_colorSample";
            m_colorSample.RotatePointAlignment = ContentAlignment.MiddleCenter;
            m_colorSample.Size = new Size(253, 26);
            m_colorSample.Style = UIStyle.Custom;
            m_colorSample.TabIndex = 1;
            m_colorSample.TabStop = false;
            m_colorSample.TextAlign = ContentAlignment.MiddleLeft;
            m_colorSample.TextAngle = 0F;
            m_colorSample.Paint += m_colorSample_Paint;
            // 
            // edtA
            // 
            edtA.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtA.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            edtA.Location = new Point(29, 197);
            edtA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtA.Maximum = 255D;
            edtA.Minimum = 0D;
            edtA.MinimumSize = new Size(1, 16);
            edtA.Name = "edtA";
            edtA.Padding = new System.Windows.Forms.Padding(5);
            edtA.ShowText = false;
            edtA.Size = new Size(41, 26);
            edtA.Style = UIStyle.Custom;
            edtA.TabIndex = 2;
            edtA.Text = "0";
            edtA.TextAlignment = ContentAlignment.MiddleLeft;
            edtA.Type = UITextBox.UIEditType.Integer;
            edtA.Watermark = "";
            edtA.LostFocus += edtA_TextChanged;
            // 
            // edtR
            // 
            edtR.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtR.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            edtR.Location = new Point(93, 197);
            edtR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtR.Maximum = 255D;
            edtR.Minimum = 0D;
            edtR.MinimumSize = new Size(1, 16);
            edtR.Name = "edtR";
            edtR.Padding = new System.Windows.Forms.Padding(5);
            edtR.ShowText = false;
            edtR.Size = new Size(41, 26);
            edtR.Style = UIStyle.Custom;
            edtR.TabIndex = 3;
            edtR.Text = "0";
            edtR.TextAlignment = ContentAlignment.MiddleLeft;
            edtR.Type = UITextBox.UIEditType.Integer;
            edtR.Watermark = "";
            edtR.LostFocus += edtR_TextChanged;
            // 
            // edtG
            // 
            edtG.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtG.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            edtG.Location = new Point(158, 197);
            edtG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtG.Maximum = 255D;
            edtG.Minimum = 0D;
            edtG.MinimumSize = new Size(1, 16);
            edtG.Name = "edtG";
            edtG.Padding = new System.Windows.Forms.Padding(5);
            edtG.ShowText = false;
            edtG.Size = new Size(41, 26);
            edtG.Style = UIStyle.Custom;
            edtG.TabIndex = 4;
            edtG.Text = "0";
            edtG.TextAlignment = ContentAlignment.MiddleLeft;
            edtG.Type = UITextBox.UIEditType.Integer;
            edtG.Watermark = "";
            edtG.LostFocus += edtG_TextChanged;
            // 
            // edtB
            // 
            edtB.Cursor = System.Windows.Forms.Cursors.IBeam;
            edtB.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            edtB.Location = new Point(222, 197);
            edtB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            edtB.Maximum = 255D;
            edtB.Minimum = 0D;
            edtB.MinimumSize = new Size(1, 16);
            edtB.Name = "edtB";
            edtB.Padding = new System.Windows.Forms.Padding(5);
            edtB.ShowText = false;
            edtB.Size = new Size(41, 26);
            edtB.Style = UIStyle.Custom;
            edtB.TabIndex = 5;
            edtB.Text = "0";
            edtB.TextAlignment = ContentAlignment.MiddleLeft;
            edtB.Type = UITextBox.UIEditType.Integer;
            edtB.Watermark = "";
            edtB.LostFocus += edtB_TextChanged;
            // 
            // lblA
            // 
            lblA.AutoSize = true;
            lblA.BackColor = Color.Transparent;
            lblA.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblA.ForeColor = Color.FromArgb(48, 48, 48);
            lblA.Location = new Point(9, 200);
            lblA.Name = "lblA";
            lblA.Size = new Size(14, 14);
            lblA.Style = UIStyle.Custom;
            lblA.TabIndex = 6;
            lblA.Text = "A";
            lblA.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblR
            // 
            lblR.AutoSize = true;
            lblR.BackColor = Color.Transparent;
            lblR.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblR.ForeColor = Color.FromArgb(48, 48, 48);
            lblR.Location = new Point(74, 200);
            lblR.Name = "lblR";
            lblR.Size = new Size(14, 14);
            lblR.Style = UIStyle.Custom;
            lblR.TabIndex = 7;
            lblR.Text = "R";
            lblR.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblG
            // 
            lblG.AutoSize = true;
            lblG.BackColor = Color.Transparent;
            lblG.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblG.ForeColor = Color.FromArgb(48, 48, 48);
            lblG.Location = new Point(138, 200);
            lblG.Name = "lblG";
            lblG.Size = new Size(14, 14);
            lblG.Style = UIStyle.Custom;
            lblG.TabIndex = 8;
            lblG.Text = "G";
            lblG.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblB
            // 
            lblB.AutoSize = true;
            lblB.BackColor = Color.Transparent;
            lblB.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblB.ForeColor = Color.FromArgb(48, 48, 48);
            lblB.Location = new Point(203, 200);
            lblB.Name = "lblB";
            lblB.Size = new Size(14, 14);
            lblB.Style = UIStyle.Custom;
            lblB.TabIndex = 9;
            lblB.Text = "B";
            lblB.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // m_colorWheel
            // 
            m_colorWheel.BackColor = Color.Transparent;
            m_colorWheel.FrameColor = Color.FromArgb(80, 160, 255);
            m_colorWheel.Location = new Point(269, 8);
            m_colorWheel.Name = "m_colorWheel";
            m_colorWheel.SelectedColor = Color.FromArgb(254, 235, 205);
            m_colorWheel.Size = new Size(148, 148);
            m_colorWheel.Style = UIStyle.Custom;
            m_colorWheel.TabIndex = 10;
            m_colorWheel.Text = "colorWheel1";
            m_colorWheel.SelectedColorChanged += m_colorWheel_SelectedColorChanged;
            // 
            // m_colorBar
            // 
            m_colorBar.BackColor = Color.Transparent;
            m_colorBar.BarPadding = new System.Windows.Forms.Padding(12, 5, 32, 10);
            m_colorBar.Color1 = Color.Black;
            m_colorBar.Color2 = Color.FromArgb(127, 127, 127);
            m_colorBar.Color3 = Color.White;
            m_colorBar.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            m_colorBar.FrameColor = Color.FromArgb(80, 160, 255);
            m_colorBar.Location = new Point(422, 8);
            m_colorBar.Name = "m_colorBar";
            m_colorBar.NumberOfColors = ColorSlider.eNumberOfColors.Use3Colors;
            m_colorBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            m_colorBar.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            m_colorBar.Percent = 0F;
            m_colorBar.RotatePointAlignment = ContentAlignment.MiddleRight;
            m_colorBar.Size = new Size(45, 148);
            m_colorBar.Style = UIStyle.Custom;
            m_colorBar.TabIndex = 11;
            m_colorBar.Text = "Lightness";
            m_colorBar.TextAlign = ContentAlignment.BottomCenter;
            m_colorBar.TextAngle = 270F;
            m_colorBar.ValueOrientation = ColorSlider.eValueOrientation.MaxToMin;
            m_colorBar.SelectedValueChanged += m_colorBar_SelectedValueChanged;
            // 
            // m_opacitySlider
            // 
            m_opacitySlider.BackColor = Color.Transparent;
            m_opacitySlider.BarPadding = new System.Windows.Forms.Padding(60, 12, 80, 25);
            m_opacitySlider.Color1 = Color.White;
            m_opacitySlider.Color2 = Color.Black;
            m_opacitySlider.Color3 = Color.Black;
            m_opacitySlider.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            m_opacitySlider.ForeColor = Color.Black;
            m_opacitySlider.FrameColor = Color.FromArgb(80, 160, 255);
            m_opacitySlider.Location = new Point(269, 163);
            m_opacitySlider.Name = "m_opacitySlider";
            m_opacitySlider.NumberOfColors = ColorSlider.eNumberOfColors.Use2Colors;
            m_opacitySlider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            m_opacitySlider.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            m_opacitySlider.Percent = 1F;
            m_opacitySlider.RotatePointAlignment = ContentAlignment.MiddleCenter;
            m_opacitySlider.Size = new Size(198, 26);
            m_opacitySlider.Style = UIStyle.Custom;
            m_opacitySlider.TabIndex = 1;
            m_opacitySlider.Text = "Opacity";
            m_opacitySlider.TextAlign = ContentAlignment.MiddleLeft;
            m_opacitySlider.TextAngle = 0F;
            m_opacitySlider.ValueOrientation = ColorSlider.eValueOrientation.MinToMax;
            m_opacitySlider.SelectedValueChanged += m_opacitySlider_SelectedValueChanged;
            // 
            // btnOK
            // 
            btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOK.Font = new Font("宋体", 12F);
            btnOK.Location = new Point(269, 197);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnOK.Size = new Size(95, 26);
            btnOK.Style = UIStyle.Custom;
            btnOK.SymbolOffset = new Point(0, 1);
            btnOK.SymbolSize = 22;
            btnOK.TabIndex = 12;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnCancel.Font = new Font("宋体", 12F);
            btnCancel.Location = new Point(372, 197);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            btnCancel.Size = new Size(95, 26);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.Symbol = 361453;
            btnCancel.SymbolOffset = new Point(0, 1);
            btnCancel.SymbolSize = 22;
            btnCancel.TabIndex = 13;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // UIColorItem
            // 
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(m_opacitySlider);
            Controls.Add(m_colorBar);
            Controls.Add(m_colorWheel);
            Controls.Add(lblB);
            Controls.Add(lblG);
            Controls.Add(lblR);
            Controls.Add(lblA);
            Controls.Add(edtB);
            Controls.Add(edtG);
            Controls.Add(edtR);
            Controls.Add(edtA);
            Controls.Add(m_colorSample);
            Controls.Add(m_colorTable);
            FillColor = Color.White;
            Name = "UIColorItem";
            Size = new Size(476, 233);
            Style = UIStyle.Custom;
            ResumeLayout(false);
            PerformLayout();
        }

        private void m_colorTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color selcol = (Color)m_colorTable.SelectedItem;
            if (selcol != m_selectedColor)
            {
                lockColorTable = true;
                m_colorWheel.SelectedColor = selcol;
                lockColorTable = false;
                m_colorSample.Invalidate();
            }
        }

        private Color m_selectedColor = Color.AntiqueWhite;

        private float m_opacity = 1;

        private bool lockColorTable = false;

        public Color SelectedColor
        {
            get { return Color.FromArgb((int)Math.Floor(255f * m_opacity), m_selectedColor); }
            set
            {
                m_opacity = (float)value.A / 255f;
                value = Color.FromArgb(255, value);
                m_colorWheel.SelectedColor = value;
                if (m_colorTable.ColorExist(value) == false)
                    m_colorTable.SetCustomColor(value);
                m_colorTable.SelectedItem = value;
                m_opacitySlider.Percent = m_opacity;
            }
        }

        private void m_colorBar_SelectedValueChanged(object sender, EventArgs e)
        {
            m_colorWheel.SetLightness(m_colorBar.SelectedHSLColor.Lightness);
        }

        private void UpdateInfo()
        {
            Color c = Color.FromArgb((int)Math.Floor(255f * m_opacity), m_selectedColor);
            string info = string.Format("{0} aRGB({1}, {2}, {3}, {4})", m_colorWheel.SelectedHSLColor.ToString(), c.A, c.R, c.G, c.B);
            updateBox = true;
            edtA.IntValue = c.A;
            edtR.IntValue = c.R;
            edtG.IntValue = c.G;
            edtB.IntValue = c.B;
            //m_infoLabel.Text = info;
            updateBox = false;
        }

        private void m_opacitySlider_SelectedValueChanged(object sender, EventArgs e)
        {
            m_opacity = Math.Max(0, m_opacitySlider.Percent);
            m_opacity = Math.Min(1, m_opacitySlider.Percent);
            m_colorSample.Refresh();
            UpdateInfo();
        }

        private void m_colorWheel_SelectedColorChanged(object sender, EventArgs e)
        {
            Color selcol = m_colorWheel.SelectedColor;
            if (selcol != m_selectedColor)
            {
                m_selectedColor = selcol;
                m_colorSample.Refresh();
                if (lockColorTable == false && selcol != m_colorTable.SelectedItem)
                    m_colorTable.SetCustomColor(selcol);
            }

            m_colorBar.SelectedHSLColor = m_colorWheel.SelectedHSLColor;
            UpdateInfo();
        }

        private bool updateBox = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DoValueChanged(this, SelectedColor);
            Close();
        }

        private void m_colorSample_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle r = m_colorSample.ClientRectangle;
            r.Inflate(-4, -4);

            int width = r.Width;
            r.Width /= 2;

            Color c = Color.FromArgb((int)Math.Floor(255f * m_opacity), m_selectedColor);
            e.Graphics.FillRectangle(c, r);

            r.X += r.Width;

            e.Graphics.FillRectangle(Color.White, r);
            c = Color.FromArgb(255, m_selectedColor);
            e.Graphics.FillRectangle(c, r);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            RectColor = uiColor.RectColor;
            BackColor = uiColor.PlainColor;
            btnOK.SetStyleColor(uiColor);
            btnCancel.SetStyleColor(uiColor);
            m_colorTable.SetStyleColor(uiColor);
            m_colorWheel.SetStyleColor(uiColor);
            m_colorBar.SetStyleColor(uiColor);
            m_colorSample.SetStyleColor(uiColor);
            m_opacitySlider.SetStyleColor(uiColor);
            edtA.SetStyleColor(uiColor);
            edtR.SetStyleColor(uiColor);
            edtG.SetStyleColor(uiColor);
            edtB.SetStyleColor(uiColor);
            lblA.ForeColor = lblR.ForeColor = lblG.ForeColor = lblB.ForeColor = uiColor.PanelForeColor;
        }

        private void edtR_TextChanged(object sender, EventArgs e)
        {
            if (updateBox) return;
            SelectedColor = Color.FromArgb(SelectedColor.A, edtR.IntValue, SelectedColor.G, SelectedColor.B);
        }

        private void edtG_TextChanged(object sender, EventArgs e)
        {
            if (updateBox) return;
            SelectedColor = Color.FromArgb(SelectedColor.A, SelectedColor.R, edtG.IntValue, SelectedColor.B);
        }

        private void edtB_TextChanged(object sender, EventArgs e)
        {
            if (updateBox) return;
            SelectedColor = Color.FromArgb(SelectedColor.A, SelectedColor.R, SelectedColor.G, edtB.IntValue);
        }

        private void edtA_TextChanged(object sender, EventArgs e)
        {
            if (updateBox) return;
            SelectedColor = Color.FromArgb(edtA.IntValue, SelectedColor);
            UpdateInfo();
        }
    }
}