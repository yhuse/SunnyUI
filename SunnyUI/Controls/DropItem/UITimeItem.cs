/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: UIDateTimeItem.cs
 * 文件说明: 时间选择框弹出窗体
 * 当前版本: V3.1
 * 创建日期: 2020-05-29
 *
 * 2020-05-29: V2.2.5 创建
 * 2024-07-14: V3.6.7 修改时间界面水平分割线颜色和位置
******************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITimeItem : UIDropDownItem, ITranslate
    {

        #region InitializeComponent
        private UISymbolButton s1;
        private UISymbolButton m1;
        private UISymbolButton h1;
        private UISymbolButton s2;
        private UISymbolButton m2;
        private UISymbolButton h2;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UILabel hc;
        private UILabel mc;
        private UILabel sc;
        private UILabel st;
        private UILabel mt;
        private UILabel ht;
        private UILabel sb;
        private UILabel mb;
        private UILabel hb;

        private void InitializeComponent()
        {
            s1 = new UISymbolButton();
            m1 = new UISymbolButton();
            h1 = new UISymbolButton();
            s2 = new UISymbolButton();
            m2 = new UISymbolButton();
            h2 = new UISymbolButton();
            btnOK = new UISymbolButton();
            btnCancel = new UISymbolButton();
            hc = new UILabel();
            mc = new UILabel();
            sc = new UILabel();
            st = new UILabel();
            mt = new UILabel();
            ht = new UILabel();
            sb = new UILabel();
            mb = new UILabel();
            hb = new UILabel();
            SuspendLayout();
            // 
            // s1
            // 
            s1.BackColor = Color.Transparent;
            s1.Cursor = Cursors.Hand;
            s1.FillColor = Color.White;
            s1.FillHoverColor = Color.FromArgb(80, 160, 255);
            s1.Font = new Font("宋体", 12F);
            s1.ForeColor = Color.FromArgb(80, 160, 255);
            s1.Location = new Point(122, 4);
            s1.MinimumSize = new Size(1, 1);
            s1.Name = "s1";
            s1.Padding = new Padding(24, 0, 0, 0);
            s1.RadiusSides = UICornerRadiusSides.None;
            s1.RectHoverColor = Color.FromArgb(80, 160, 255);
            s1.RectSides = ToolStripStatusLabelBorderSides.None;
            s1.Size = new Size(30, 24);
            s1.Style = UIStyle.Custom;
            s1.StyleCustomMode = true;
            s1.Symbol = 61702;
            s1.TabIndex = 6;
            s1.Tag = "3";
            s1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            s1.Click += s1_Click;
            // 
            // m1
            // 
            m1.BackColor = Color.Transparent;
            m1.Cursor = Cursors.Hand;
            m1.FillColor = Color.White;
            m1.FillHoverColor = Color.FromArgb(80, 160, 255);
            m1.Font = new Font("宋体", 12F);
            m1.ForeColor = Color.FromArgb(80, 160, 255);
            m1.Location = new Point(69, 4);
            m1.MinimumSize = new Size(1, 1);
            m1.Name = "m1";
            m1.Padding = new Padding(24, 0, 0, 0);
            m1.RadiusSides = UICornerRadiusSides.None;
            m1.RectHoverColor = Color.FromArgb(80, 160, 255);
            m1.RectSides = ToolStripStatusLabelBorderSides.None;
            m1.Size = new Size(30, 24);
            m1.Style = UIStyle.Custom;
            m1.StyleCustomMode = true;
            m1.Symbol = 61702;
            m1.TabIndex = 5;
            m1.Tag = "2";
            m1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            m1.Click += m1_Click;
            // 
            // h1
            // 
            h1.BackColor = Color.Transparent;
            h1.Cursor = Cursors.Hand;
            h1.FillColor = Color.White;
            h1.FillHoverColor = Color.FromArgb(80, 160, 255);
            h1.Font = new Font("宋体", 12F);
            h1.ForeColor = Color.FromArgb(80, 160, 255);
            h1.Location = new Point(16, 4);
            h1.MinimumSize = new Size(1, 1);
            h1.Name = "h1";
            h1.Padding = new Padding(24, 0, 0, 0);
            h1.RadiusSides = UICornerRadiusSides.None;
            h1.RectHoverColor = Color.FromArgb(80, 160, 255);
            h1.RectSides = ToolStripStatusLabelBorderSides.None;
            h1.Size = new Size(30, 24);
            h1.Style = UIStyle.Custom;
            h1.StyleCustomMode = true;
            h1.Symbol = 61702;
            h1.TabIndex = 4;
            h1.Tag = "1";
            h1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            h1.Click += h1_Click;
            // 
            // s2
            // 
            s2.BackColor = Color.Transparent;
            s2.Cursor = Cursors.Hand;
            s2.FillColor = Color.White;
            s2.FillHoverColor = Color.FromArgb(80, 160, 255);
            s2.Font = new Font("宋体", 12F);
            s2.ForeColor = Color.FromArgb(80, 160, 255);
            s2.Location = new Point(122, 129);
            s2.MinimumSize = new Size(1, 1);
            s2.Name = "s2";
            s2.Padding = new Padding(24, 0, 0, 0);
            s2.RadiusSides = UICornerRadiusSides.None;
            s2.RectHoverColor = Color.FromArgb(80, 160, 255);
            s2.RectSides = ToolStripStatusLabelBorderSides.None;
            s2.Size = new Size(30, 24);
            s2.Style = UIStyle.Custom;
            s2.StyleCustomMode = true;
            s2.Symbol = 61703;
            s2.TabIndex = 9;
            s2.Tag = "6";
            s2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            s2.Click += s2_Click;
            // 
            // m2
            // 
            m2.BackColor = Color.Transparent;
            m2.Cursor = Cursors.Hand;
            m2.FillColor = Color.White;
            m2.FillHoverColor = Color.FromArgb(80, 160, 255);
            m2.Font = new Font("宋体", 12F);
            m2.ForeColor = Color.FromArgb(80, 160, 255);
            m2.Location = new Point(69, 129);
            m2.MinimumSize = new Size(1, 1);
            m2.Name = "m2";
            m2.Padding = new Padding(24, 0, 0, 0);
            m2.RadiusSides = UICornerRadiusSides.None;
            m2.RectHoverColor = Color.FromArgb(80, 160, 255);
            m2.RectSides = ToolStripStatusLabelBorderSides.None;
            m2.Size = new Size(30, 24);
            m2.Style = UIStyle.Custom;
            m2.StyleCustomMode = true;
            m2.Symbol = 61703;
            m2.TabIndex = 8;
            m2.Tag = "5";
            m2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            m2.Click += m2_Click;
            // 
            // h2
            // 
            h2.BackColor = Color.Transparent;
            h2.Cursor = Cursors.Hand;
            h2.FillColor = Color.White;
            h2.FillHoverColor = Color.FromArgb(80, 160, 255);
            h2.Font = new Font("宋体", 12F);
            h2.ForeColor = Color.FromArgb(80, 160, 255);
            h2.Location = new Point(16, 128);
            h2.MinimumSize = new Size(1, 1);
            h2.Name = "h2";
            h2.Padding = new Padding(24, 0, 0, 0);
            h2.RadiusSides = UICornerRadiusSides.None;
            h2.RectHoverColor = Color.FromArgb(80, 160, 255);
            h2.RectSides = ToolStripStatusLabelBorderSides.None;
            h2.Size = new Size(30, 24);
            h2.Style = UIStyle.Custom;
            h2.StyleCustomMode = true;
            h2.Symbol = 61703;
            h2.TabIndex = 7;
            h2.Tag = "4";
            h2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            h2.Click += h2_Click;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new Font("宋体", 12F);
            btnOK.Location = new Point(8, 162);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Padding = new Padding(28, 0, 0, 0);
            btnOK.Size = new Size(74, 27);
            btnOK.Style = UIStyle.Custom;
            btnOK.SymbolSize = 22;
            btnOK.TabIndex = 10;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("宋体", 12F);
            btnCancel.Location = new Point(87, 162);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(28, 0, 0, 0);
            btnCancel.Size = new Size(74, 27);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.Symbol = 361453;
            btnCancel.SymbolSize = 22;
            btnCancel.TabIndex = 11;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // hc
            // 
            hc.BackColor = Color.Transparent;
            hc.Font = new Font("宋体", 12F);
            hc.ForeColor = Color.FromArgb(48, 48, 48);
            hc.Location = new Point(9, 65);
            hc.Name = "hc";
            hc.Size = new Size(46, 27);
            hc.StyleCustomMode = true;
            hc.TabIndex = 12;
            hc.Text = "00";
            hc.TextAlign = ContentAlignment.MiddleCenter;
            hc.DoubleClick += hc_DoubleClick;
            // 
            // mc
            // 
            mc.BackColor = Color.Transparent;
            mc.Font = new Font("宋体", 12F);
            mc.ForeColor = Color.FromArgb(48, 48, 48);
            mc.Location = new Point(62, 65);
            mc.Name = "mc";
            mc.Size = new Size(46, 27);
            mc.StyleCustomMode = true;
            mc.TabIndex = 13;
            mc.Text = "00";
            mc.TextAlign = ContentAlignment.MiddleCenter;
            mc.DoubleClick += mc_DoubleClick;
            // 
            // sc
            // 
            sc.BackColor = Color.Transparent;
            sc.Font = new Font("宋体", 12F);
            sc.ForeColor = Color.FromArgb(48, 48, 48);
            sc.Location = new Point(115, 65);
            sc.Name = "sc";
            sc.Size = new Size(46, 27);
            sc.StyleCustomMode = true;
            sc.TabIndex = 14;
            sc.Text = "00";
            sc.TextAlign = ContentAlignment.MiddleCenter;
            sc.DoubleClick += sc_DoubleClick;
            // 
            // st
            // 
            st.BackColor = Color.Transparent;
            st.Font = new Font("宋体", 12F);
            st.ForeColor = Color.DarkGray;
            st.Location = new Point(115, 31);
            st.Name = "st";
            st.Size = new Size(46, 22);
            st.StyleCustomMode = true;
            st.TabIndex = 17;
            st.Text = "00";
            st.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mt
            // 
            mt.BackColor = Color.Transparent;
            mt.Font = new Font("宋体", 12F);
            mt.ForeColor = Color.DarkGray;
            mt.Location = new Point(62, 31);
            mt.Name = "mt";
            mt.Size = new Size(46, 22);
            mt.StyleCustomMode = true;
            mt.TabIndex = 16;
            mt.Text = "00";
            mt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ht
            // 
            ht.BackColor = Color.Transparent;
            ht.Font = new Font("宋体", 12F);
            ht.ForeColor = Color.DarkGray;
            ht.Location = new Point(9, 31);
            ht.Name = "ht";
            ht.Size = new Size(46, 22);
            ht.StyleCustomMode = true;
            ht.TabIndex = 15;
            ht.Text = "00";
            ht.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sb
            // 
            sb.BackColor = Color.Transparent;
            sb.Font = new Font("宋体", 12F);
            sb.ForeColor = Color.DarkGray;
            sb.Location = new Point(115, 103);
            sb.Name = "sb";
            sb.Size = new Size(46, 22);
            sb.StyleCustomMode = true;
            sb.TabIndex = 20;
            sb.Text = "00";
            sb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mb
            // 
            mb.BackColor = Color.Transparent;
            mb.Font = new Font("宋体", 12F);
            mb.ForeColor = Color.DarkGray;
            mb.Location = new Point(62, 103);
            mb.Name = "mb";
            mb.Size = new Size(46, 22);
            mb.StyleCustomMode = true;
            mb.TabIndex = 19;
            mb.Text = "00";
            mb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hb
            // 
            hb.BackColor = Color.Transparent;
            hb.Font = new Font("宋体", 12F);
            hb.ForeColor = Color.DarkGray;
            hb.Location = new Point(9, 103);
            hb.Name = "hb";
            hb.Size = new Size(46, 22);
            hb.StyleCustomMode = true;
            hb.TabIndex = 18;
            hb.Text = "00";
            hb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UITimeItem
            // 
            Controls.Add(sb);
            Controls.Add(mb);
            Controls.Add(hb);
            Controls.Add(st);
            Controls.Add(mt);
            Controls.Add(ht);
            Controls.Add(sc);
            Controls.Add(mc);
            Controls.Add(hc);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(s2);
            Controls.Add(m2);
            Controls.Add(h2);
            Controls.Add(s1);
            Controls.Add(m1);
            Controls.Add(h1);
            FillColor = Color.White;
            Name = "UITimeItem";
            Size = new Size(168, 200);
            Style = UIStyle.Custom;
            Paint += UITimeItem_Paint;
            ResumeLayout(false);
        }

        #endregion InitializeComponent

        public UITimeItem()
        {
            InitializeComponent();
            this.MouseWheel += UITimeItem_MouseWheel;
            Translate();
        }

        public void Translate()
        {
            btnOK.Text = UIStyles.CurrentResources.OK;
            btnCancel.Text = UIStyles.CurrentResources.Cancel;
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            btnOK.SetDPIScale();
            btnCancel.SetDPIScale();
            foreach (var label in this.GetControls<UILabel>()) label.SetDPIScale();

            if (SizeMultiple > 1)
            {
                foreach (Control item in this.Controls)
                {
                    if (!SizeMultipled)
                    {
                        item.Left = item.Left * SizeMultiple;
                        item.Top = item.Top * SizeMultiple;
                        item.Width = item.Width * SizeMultiple;
                        item.Height = item.Height * SizeMultiple;
                        if (item is ISymbol symbol) symbol.SymbolSize = (int)(symbol.SymbolSize * 1.5f);
                    }

                    item.Font = new Font(item.Font.FontFamily, item.Font.Size * 1.5f);
                }

                SizeMultipled = true;
            }
        }

        internal bool SizeMultipled = false;
        private int sizeMultiple = 1;
        public int SizeMultiple
        {
            get => sizeMultiple;
            set
            {
                if (value < 1) value = 1;
                if (value > 2) value = 2;

                sizeMultiple = value;
            }
        }

        private void UITimeItem_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (new Rectangle(ht.Left, ht.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    h2.PerformClick();
                }
                else if (new Rectangle(mt.Left, mt.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    m2.PerformClick();
                }
                else if (new Rectangle(st.Left, st.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    s2.PerformClick();
                }
            }
            else if (e.Delta > 0)
            {
                if (new Rectangle(ht.Left, ht.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    h1.PerformClick();
                }
                else if (new Rectangle(mt.Left, mt.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    m1.PerformClick();
                }
                else if (new Rectangle(st.Left, st.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    s1.PerformClick();
                }
            }
        }

        private DateTime time;

        public DateTime Time
        {
            get => time;
            set
            {
                time = value;
                Hour = time.Hour;
                Minute = time.Minute;
                Second = time.Second;
                ShowOther();
            }
        }

        private int hour;
        private int minute;
        private int second;

        public int Hour
        {
            get => hour;
            set
            {
                hour = value;
                hc.Text = hour.ToString();
            }
        }

        public int Minute
        {
            get => minute;
            set
            {
                minute = value;
                mc.Text = minute.ToString();
            }
        }

        public int Second
        {
            get => second;
            set
            {
                second = value;
                sc.Text = second.ToString();
            }
        }

        private void ShowOther()
        {
            ht.Text = (hour - 1 + 24).Mod(24).ToString();
            hb.Text = (hour + 1 + 24).Mod(24).ToString();
            mt.Text = (minute - 1 + 60).Mod(60).ToString();
            mb.Text = (minute + 1 + 60).Mod(60).ToString();
            st.Text = (second - 1 + 60).Mod(60).ToString();
            sb.Text = (second + 1 + 60).Mod(60).ToString();
        }

        private void h1_Click(object sender, EventArgs e)
        {
            Hour = (Hour - 1 + 24).Mod(24);
            ShowOther();
        }

        private void m1_Click(object sender, EventArgs e)
        {
            Minute = (Minute - 1 + 60).Mod(60);
            ShowOther();
        }

        private void s1_Click(object sender, EventArgs e)
        {
            Second = (Second - 1 + 60).Mod(60);
            ShowOther();
        }

        private void h2_Click(object sender, EventArgs e)
        {
            Hour = (Hour + 1 + 24).Mod(24);
            ShowOther();
        }

        private void m2_Click(object sender, EventArgs e)
        {
            Minute = (Minute + 1 + 60).Mod(60);
            ShowOther();
        }

        private void s2_Click(object sender, EventArgs e)
        {
            Second = (Second + 1 + 60).Mod(60);
            ShowOther();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            time = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, Hour, Minute, Second);
            DoValueChanged(this, time);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            btnOK.SetStyleColor(uiColor);
            btnCancel.SetStyleColor(uiColor);

            h1.SetStyleColor(uiColor);
            h2.SetStyleColor(uiColor);
            m1.SetStyleColor(uiColor);
            m2.SetStyleColor(uiColor);
            s1.SetStyleColor(uiColor);
            s2.SetStyleColor(uiColor);

            FillColor = Color.White;
            h1.FillColor = h2.FillColor = m1.FillColor = m2.FillColor = s1.FillColor = s2.FillColor = Color.White;

            RectColor = uiColor.RectColor;
            h1.ForeColor = h2.ForeColor = RectColor;
            m1.ForeColor = m2.ForeColor = RectColor;
            s1.ForeColor = s2.ForeColor = RectColor;
            h1.SymbolColor = h2.SymbolColor = RectColor;
            m1.SymbolColor = m2.SymbolColor = RectColor;
            s1.SymbolColor = s2.SymbolColor = RectColor;
        }

        private void hc_DoubleClick(object sender, EventArgs e)
        {
            Hour = 0;
            Minute = 0;
            Second = 0;
            ShowOther();
        }

        private void mc_DoubleClick(object sender, EventArgs e)
        {
            Minute = 0;
            Second = 0;
            ShowOther();
        }

        private void sc_DoubleClick(object sender, EventArgs e)
        {
            Second = 0;
            ShowOther();
        }

        private void UITimeItem_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Color.LightGray, 8 * SizeMultiple, (54 + 8 - 3) * SizeMultiple, (168 - 8) * SizeMultiple, (54 + 8 - 3) * SizeMultiple);
            e.Graphics.DrawLine(Color.LightGray, 8 * SizeMultiple, (88 + 8 + 3) * SizeMultiple, (168 - 8) * SizeMultiple, (88 + 8 + 3) * SizeMultiple);
        }
    }
}