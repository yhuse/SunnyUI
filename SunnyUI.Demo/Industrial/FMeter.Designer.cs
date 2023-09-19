
namespace Sunny.UI.Demo
{
    partial class FMeter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMeter));
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiAnalogMeter1 = new Sunny.UI.UIAnalogMeter();
            this.uiLine9 = new Sunny.UI.UILine();
            this.uiRoundMeter2 = new Sunny.UI.UIRoundMeter();
            this.uiLine6 = new Sunny.UI.UILine();
            this.uiRoundMeter1 = new Sunny.UI.UIRoundMeter();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiKnob2 = new Sunny.UI.UIKnob();
            this.uiKnob3 = new Sunny.UI.UIKnob();
            this.uiKnob4 = new Sunny.UI.UIKnob();
            this.uiKnob1 = new Sunny.UI.UIKnob();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiAnalogMeter1);
            this.uiPanel1.Controls.Add(this.uiLine9);
            this.uiPanel1.Controls.Add(this.uiRoundMeter2);
            this.uiPanel1.Controls.Add(this.uiLine6);
            this.uiPanel1.Controls.Add(this.uiRoundMeter1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor2 = System.Drawing.Color.CornflowerBlue;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(800, 504);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 69;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiAnalogMeter1
            // 
            this.uiAnalogMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiAnalogMeter1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiAnalogMeter1.Location = new System.Drawing.Point(381, 61);
            this.uiAnalogMeter1.MaxValue = 100D;
            this.uiAnalogMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAnalogMeter1.MinValue = 0D;
            this.uiAnalogMeter1.Name = "uiAnalogMeter1";
            this.uiAnalogMeter1.Renderer = null;
            this.uiAnalogMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiAnalogMeter1.Style = Sunny.UI.UIStyle.Custom;
            this.uiAnalogMeter1.TabIndex = 73;
            this.uiAnalogMeter1.Text = "uiAnalogMeter1";
            this.uiAnalogMeter1.Value = 0D;
            this.uiAnalogMeter1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine9
            // 
            this.uiLine9.BackColor = System.Drawing.Color.Transparent;
            this.uiLine9.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine9.Location = new System.Drawing.Point(381, 20);
            this.uiLine9.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine9.Name = "uiLine9";
            this.uiLine9.Size = new System.Drawing.Size(319, 20);
            this.uiLine9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine9.TabIndex = 72;
            this.uiLine9.Text = "UIAnalogMeter";
            this.uiLine9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine9.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiRoundMeter2
            // 
            this.uiRoundMeter2.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.AngleImage")));
            this.uiRoundMeter2.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter2.BackgroundImage")));
            this.uiRoundMeter2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter2.Font = new System.Drawing.Font("宋体", 12F);
            this.uiRoundMeter2.Location = new System.Drawing.Point(186, 56);
            this.uiRoundMeter2.MeterType = Sunny.UI.UIRoundMeter.TMeterType.Wind;
            this.uiRoundMeter2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter2.Name = "uiRoundMeter2";
            this.uiRoundMeter2.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter2.Style = Sunny.UI.UIStyle.Custom;
            this.uiRoundMeter2.TabIndex = 71;
            this.uiRoundMeter2.Text = "uiRoundMeter2";
            this.uiRoundMeter2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine6
            // 
            this.uiLine6.BackColor = System.Drawing.Color.Transparent;
            this.uiLine6.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine6.Location = new System.Drawing.Point(30, 20);
            this.uiLine6.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine6.Name = "uiLine6";
            this.uiLine6.Size = new System.Drawing.Size(319, 20);
            this.uiLine6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine6.TabIndex = 70;
            this.uiLine6.Text = "UIRoundMeter";
            this.uiLine6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiRoundMeter1
            // 
            this.uiRoundMeter1.AngleImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.AngleImage")));
            this.uiRoundMeter1.BackColor = System.Drawing.Color.Transparent;
            this.uiRoundMeter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uiRoundMeter1.BackgroundImage")));
            this.uiRoundMeter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.uiRoundMeter1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiRoundMeter1.Location = new System.Drawing.Point(30, 56);
            this.uiRoundMeter1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRoundMeter1.Name = "uiRoundMeter1";
            this.uiRoundMeter1.Size = new System.Drawing.Size(140, 140);
            this.uiRoundMeter1.Style = Sunny.UI.UIStyle.Custom;
            this.uiRoundMeter1.TabIndex = 69;
            this.uiRoundMeter1.Text = "uiRoundMeter1";
            this.uiRoundMeter1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLine1
            // 
            this.uiLine1.BackColor = System.Drawing.Color.Transparent;
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F);
            this.uiLine1.Location = new System.Drawing.Point(30, 276);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(319, 20);
            this.uiLine1.TabIndex = 71;
            this.uiLine1.Text = "UIKnob";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLine1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiKnob2
            // 
            this.uiKnob2.EndAngle = 440F;
            this.uiKnob2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.uiKnob2.KnobBackColor = System.Drawing.Color.Black;
            this.uiKnob2.KnobPointerStyle = Sunny.UI.UIKnob.KnobPointerStyles.line;
            this.uiKnob2.LargeChange = 5;
            this.uiKnob2.Location = new System.Drawing.Point(208, 326);
            this.uiKnob2.Maximum = 100;
            this.uiKnob2.Minimum = -100;
            this.uiKnob2.Name = "uiKnob2";
            this.uiKnob2.PointerColor = System.Drawing.Color.SlateBlue;
            this.uiKnob2.ScaleColor = System.Drawing.Color.Black;
            this.uiKnob2.ScaleDivisions = 21;
            this.uiKnob2.ScaleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.uiKnob2.ScaleSubDivisions = 4;
            this.uiKnob2.ShowLargeScale = true;
            this.uiKnob2.ShowSmallScale = false;
            this.uiKnob2.Size = new System.Drawing.Size(150, 150);
            this.uiKnob2.SmallChange = 1;
            this.uiKnob2.StartAngle = 100F;
            this.uiKnob2.TabIndex = 1;
            this.uiKnob2.Value = -30;
            this.uiKnob2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiKnob3
            // 
            this.uiKnob3.EndAngle = 405F;
            this.uiKnob3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.uiKnob3.KnobBackColor = System.Drawing.Color.White;
            this.uiKnob3.KnobPointerStyle = Sunny.UI.UIKnob.KnobPointerStyles.line;
            this.uiKnob3.LargeChange = 5;
            this.uiKnob3.Location = new System.Drawing.Point(386, 326);
            this.uiKnob3.Maximum = 100;
            this.uiKnob3.Minimum = 0;
            this.uiKnob3.Name = "uiKnob3";
            this.uiKnob3.PointerColor = System.Drawing.Color.SlateBlue;
            this.uiKnob3.ScaleColor = System.Drawing.Color.Black;
            this.uiKnob3.ScaleDivisions = 11;
            this.uiKnob3.ScaleFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiKnob3.ScaleFontAutoSize = false;
            this.uiKnob3.ScaleSubDivisions = 1;
            this.uiKnob3.ShowLargeScale = true;
            this.uiKnob3.ShowSmallScale = true;
            this.uiKnob3.Size = new System.Drawing.Size(150, 150);
            this.uiKnob3.SmallChange = 2;
            this.uiKnob3.StartAngle = 135F;
            this.uiKnob3.TabIndex = 2;
            this.uiKnob3.Value = 0;
            this.uiKnob3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiKnob4
            // 
            this.uiKnob4.DrawDivInside = true;
            this.uiKnob4.EndAngle = 360F;
            this.uiKnob4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.uiKnob4.KnobBackColor = System.Drawing.Color.Gray;
            this.uiKnob4.KnobPointerStyle = Sunny.UI.UIKnob.KnobPointerStyles.circle;
            this.uiKnob4.LargeChange = 5;
            this.uiKnob4.Location = new System.Drawing.Point(564, 326);
            this.uiKnob4.Maximum = 10;
            this.uiKnob4.Minimum = 0;
            this.uiKnob4.Name = "uiKnob4";
            this.uiKnob4.PointerColor = System.Drawing.Color.White;
            this.uiKnob4.ScaleColor = System.Drawing.Color.Black;
            this.uiKnob4.ScaleDivisions = 11;
            this.uiKnob4.ScaleFont = new System.Drawing.Font("Bauhaus 93", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiKnob4.ScaleFontAutoSize = false;
            this.uiKnob4.ScaleSubDivisions = 4;
            this.uiKnob4.ShowLargeScale = true;
            this.uiKnob4.ShowSmallScale = false;
            this.uiKnob4.Size = new System.Drawing.Size(150, 150);
            this.uiKnob4.SmallChange = 1;
            this.uiKnob4.StartAngle = 180F;
            this.uiKnob4.TabIndex = 3;
            this.uiKnob4.Value = 3;
            this.uiKnob4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiKnob1
            // 
            this.uiKnob1.EndAngle = 405F;
            this.uiKnob1.KnobBackColor = System.Drawing.Color.White;
            this.uiKnob1.KnobPointerStyle = Sunny.UI.UIKnob.KnobPointerStyles.circle;
            this.uiKnob1.LargeChange = 5;
            this.uiKnob1.Location = new System.Drawing.Point(30, 326);
            this.uiKnob1.Maximum = 100;
            this.uiKnob1.Minimum = 0;
            this.uiKnob1.Name = "uiKnob1";
            this.uiKnob1.PointerColor = System.Drawing.Color.SlateBlue;
            this.uiKnob1.ScaleColor = System.Drawing.Color.Black;
            this.uiKnob1.ScaleDivisions = 11;
            this.uiKnob1.ScaleFont = new System.Drawing.Font("宋体", 9F);
            this.uiKnob1.ScaleSubDivisions = 4;
            this.uiKnob1.ShowLargeScale = true;
            this.uiKnob1.ShowSmallScale = false;
            this.uiKnob1.Size = new System.Drawing.Size(150, 150);
            this.uiKnob1.SmallChange = 1;
            this.uiKnob1.StartAngle = 135F;
            this.uiKnob1.TabIndex = 72;
            this.uiKnob1.Value = 0;
            this.uiKnob1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FMeter
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 539);
            this.Controls.Add(this.uiKnob1);
            this.Controls.Add(this.uiKnob4);
            this.Controls.Add(this.uiKnob3);
            this.Controls.Add(this.uiKnob2);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FMeter";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Symbol = 61668;
            this.Text = "Meter";
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UIPanel uiPanel1;
        private UIAnalogMeter uiAnalogMeter1;
        private UILine uiLine9;
        private UIRoundMeter uiRoundMeter2;
        private UILine uiLine6;
        private UIRoundMeter uiRoundMeter1;
        private UILine uiLine1;
        private UIKnob uiKnob2;
        private UIKnob uiKnob3;
        private UIKnob uiKnob4;
        private UIKnob uiKnob1;
    }
}