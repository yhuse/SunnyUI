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
 * 文件名称: UIDateItem.cs
 * 文件说明: 日期选择框弹出窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-29: V2.2.5 重写
 * 2020-07-04: V2.2.6 重写下拉窗体，缩短创建时间
 * 2023-03-26: V3.3.3 修复下拉框鼠标移动位置的Bug
 * 2023-05-13: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UIDateItem : UIDropDownItem, ITranslate
    {
        #region InitializeComponent

        private UITabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UIPanel p1;
        private UISymbolButton b4;
        private UISymbolButton b3;
        private UISymbolButton b2;
        private UISymbolButton b1;
        private UIPanel p2;
        private System.Windows.Forms.TabPage tabPage3;
        private UIPanel p3;
        private UIPanel TopPanel;

        private void InitializeComponent()
        {
            TopPanel = new UIPanel();
            b4 = new UISymbolButton();
            b3 = new UISymbolButton();
            b2 = new UISymbolButton();
            b1 = new UISymbolButton();
            TabControl = new UITabControl();
            tabPage1 = new TabPage();
            p1 = new UIPanel();
            tabPage2 = new TabPage();
            p2 = new UIPanel();
            tabPage3 = new TabPage();
            p3 = new UIPanel();
            TopPanel.SuspendLayout();
            TabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // TopPanel
            // 
            TopPanel.Controls.Add(b4);
            TopPanel.Controls.Add(b3);
            TopPanel.Controls.Add(b2);
            TopPanel.Controls.Add(b1);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.FillColor = Color.White;
            TopPanel.Font = new Font("宋体", 12F);
            TopPanel.Location = new Point(0, 0);
            TopPanel.Margin = new Padding(4, 5, 4, 5);
            TopPanel.MinimumSize = new Size(1, 1);
            TopPanel.Name = "TopPanel";
            TopPanel.RadiusSides = UICornerRadiusSides.None;
            TopPanel.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right;
            TopPanel.Size = new Size(461, 31);
            TopPanel.Style = UIStyle.Custom;
            TopPanel.StyleCustomMode = true;
            TopPanel.TabIndex = 0;
            TopPanel.Text = "2020-05-05";
            TopPanel.TextAlignment = ContentAlignment.MiddleCenter;
            TopPanel.Click += TopPanel_Click;
            // 
            // b4
            // 
            b4.BackColor = Color.Transparent;
            b4.Cursor = Cursors.Hand;
            b4.FillColor = Color.White;
            b4.FillHoverColor = Color.FromArgb(80, 160, 255);
            b4.Font = new Font("宋体", 12F);
            b4.ForeColor = Color.FromArgb(80, 160, 255);
            b4.Location = new Point(427, 4);
            b4.MinimumSize = new Size(1, 1);
            b4.Name = "b4";
            b4.Padding = new Padding(24, 0, 0, 0);
            b4.RadiusSides = UICornerRadiusSides.None;
            b4.RectHoverColor = Color.FromArgb(80, 160, 255);
            b4.RectSides = ToolStripStatusLabelBorderSides.None;
            b4.Size = new Size(30, 24);
            b4.Style = UIStyle.Custom;
            b4.StyleCustomMode = true;
            b4.Symbol = 61697;
            b4.TabIndex = 3;
            b4.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b4.Click += b4_Click;
            // 
            // b3
            // 
            b3.BackColor = Color.Transparent;
            b3.Cursor = Cursors.Hand;
            b3.FillColor = Color.White;
            b3.FillHoverColor = Color.FromArgb(80, 160, 255);
            b3.Font = new Font("宋体", 12F);
            b3.ForeColor = Color.FromArgb(80, 160, 255);
            b3.Location = new Point(391, 4);
            b3.MinimumSize = new Size(1, 1);
            b3.Name = "b3";
            b3.Padding = new Padding(24, 0, 0, 0);
            b3.RadiusSides = UICornerRadiusSides.None;
            b3.RectHoverColor = Color.FromArgb(80, 160, 255);
            b3.RectSides = ToolStripStatusLabelBorderSides.None;
            b3.Size = new Size(30, 24);
            b3.Style = UIStyle.Custom;
            b3.StyleCustomMode = true;
            b3.Symbol = 61701;
            b3.TabIndex = 2;
            b3.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b3.Click += b3_Click;
            // 
            // b2
            // 
            b2.BackColor = Color.Transparent;
            b2.Cursor = Cursors.Hand;
            b2.FillColor = Color.White;
            b2.FillHoverColor = Color.FromArgb(80, 160, 255);
            b2.Font = new Font("宋体", 12F);
            b2.ForeColor = Color.FromArgb(80, 160, 255);
            b2.Location = new Point(40, 4);
            b2.MinimumSize = new Size(1, 1);
            b2.Name = "b2";
            b2.Padding = new Padding(24, 0, 0, 0);
            b2.RadiusSides = UICornerRadiusSides.None;
            b2.RectHoverColor = Color.FromArgb(80, 160, 255);
            b2.RectSides = ToolStripStatusLabelBorderSides.None;
            b2.Size = new Size(30, 24);
            b2.Style = UIStyle.Custom;
            b2.StyleCustomMode = true;
            b2.Symbol = 61700;
            b2.TabIndex = 1;
            b2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b2.Click += b2_Click;
            // 
            // b1
            // 
            b1.BackColor = Color.Transparent;
            b1.Cursor = Cursors.Hand;
            b1.FillColor = Color.White;
            b1.FillHoverColor = Color.FromArgb(80, 160, 255);
            b1.Font = new Font("宋体", 12F);
            b1.ForeColor = Color.FromArgb(80, 160, 255);
            b1.Location = new Point(4, 4);
            b1.MinimumSize = new Size(1, 1);
            b1.Name = "b1";
            b1.Padding = new Padding(24, 0, 0, 0);
            b1.RadiusSides = UICornerRadiusSides.None;
            b1.RectHoverColor = Color.FromArgb(80, 160, 255);
            b1.RectSides = ToolStripStatusLabelBorderSides.None;
            b1.Size = new Size(30, 24);
            b1.Style = UIStyle.Custom;
            b1.StyleCustomMode = true;
            b1.Symbol = 61696;
            b1.TabIndex = 0;
            b1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b1.Click += b1_Click;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(tabPage1);
            TabControl.Controls.Add(tabPage2);
            TabControl.Controls.Add(tabPage3);
            TabControl.Dock = DockStyle.Fill;
            TabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl.Font = new Font("宋体", 12F);
            TabControl.ItemSize = new Size(150, 40);
            TabControl.Location = new Point(0, 31);
            TabControl.MainPage = "";
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(461, 317);
            TabControl.SizeMode = TabSizeMode.Fixed;
            TabControl.Style = UIStyle.Custom;
            TabControl.TabIndex = 1;
            TabControl.TabUnSelectedForeColor = Color.FromArgb(240, 240, 240);
            TabControl.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(p1);
            tabPage1.Location = new Point(0, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(461, 277);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            p1.Dock = DockStyle.Fill;
            p1.FillColor = Color.White;
            p1.Font = new Font("宋体", 12F);
            p1.Location = new Point(0, 0);
            p1.Margin = new Padding(4, 5, 4, 5);
            p1.MinimumSize = new Size(1, 1);
            p1.Name = "p1";
            p1.RadiusSides = UICornerRadiusSides.None;
            p1.Size = new Size(461, 277);
            p1.Style = UIStyle.Custom;
            p1.TabIndex = 0;
            p1.Text = null;
            p1.TextAlignment = ContentAlignment.MiddleCenter;
            p1.Paint += p1_Paint;
            p1.MouseClick += p1_MouseClick;
            p1.MouseLeave += p1_MouseLeave;
            p1.MouseMove += p1_MouseMove;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(p2);
            tabPage2.Location = new Point(0, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(200, 60);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            p2.Dock = DockStyle.Fill;
            p2.FillColor = Color.White;
            p2.Font = new Font("宋体", 12F);
            p2.Location = new Point(0, 0);
            p2.Margin = new Padding(4, 5, 4, 5);
            p2.MinimumSize = new Size(1, 1);
            p2.Name = "p2";
            p2.RadiusSides = UICornerRadiusSides.None;
            p2.Size = new Size(200, 60);
            p2.Style = UIStyle.Custom;
            p2.TabIndex = 1;
            p2.Text = null;
            p2.TextAlignment = ContentAlignment.MiddleCenter;
            p2.Paint += p2_Paint;
            p2.MouseClick += p2_MouseClick;
            p2.MouseLeave += p2_MouseLeave;
            p2.MouseMove += p2_MouseMove;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(p3);
            tabPage3.Location = new Point(0, 40);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(200, 60);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            p3.Dock = DockStyle.Fill;
            p3.FillColor = Color.White;
            p3.Font = new Font("宋体", 12F);
            p3.Location = new Point(0, 0);
            p3.Margin = new Padding(4, 5, 4, 5);
            p3.MinimumSize = new Size(1, 1);
            p3.Name = "p3";
            p3.RadiusSides = UICornerRadiusSides.None;
            p3.Size = new Size(200, 60);
            p3.Style = UIStyle.Custom;
            p3.TabIndex = 2;
            p3.Text = null;
            p3.TextAlignment = ContentAlignment.MiddleCenter;
            p3.Paint += p3_Paint;
            p3.MouseClick += p3_MouseClick;
            p3.MouseLeave += p3_MouseLeave;
            p3.MouseMove += p3_MouseMove;
            // 
            // UIDateItem
            // 
            Controls.Add(TabControl);
            Controls.Add(TopPanel);
            FillColor = Color.White;
            Name = "UIDateItem";
            Size = new Size(461, 348);
            Style = UIStyle.Custom;
            TopPanel.ResumeLayout(false);
            TabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion InitializeComponent

        private readonly List<string> months = new List<string>();
        private readonly List<int> years = new List<int>();
        private readonly List<DateTime> days = new List<DateTime>();

        public UIDateItem()
        {
            InitializeComponent();
            Size = new Size(284, 200);
            TabControl.TabVisible = false;
            Translate();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            TopPanel.SetDPIScale();

            if (SizeMultiple > 1)
            {
                TopPanel.Height = 31 * SizeMultiple;
                foreach (Control item in TopPanel.Controls)
                {
                    if (!SizeMultipled)
                    {
                        item.Left = item.Left * SizeMultiple;
                        item.Top = item.Top * SizeMultiple;
                        item.Width = item.Width * SizeMultiple;
                        item.Height = item.Height * SizeMultiple;
                        if (item is ISymbol symbol) symbol.SymbolSize = (int)(symbol.SymbolSize * 1.5f);
                    }
                }

                TopPanel.Font = new Font(TopPanel.Font.FontFamily, TopPanel.Font.Size * 1.5f);
                SizeMultipled = true;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (b1 == null) return;
            b4.Left = TopPanel.Width - b1.Left - b4.Width;
            b3.Left = TopPanel.Width - b2.Left - b3.Width;
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

        public DateTime max = DateTime.MaxValue;
        public DateTime min = DateTime.MinValue;

        public void Translate()
        {
            months.Clear();
            months.Add(UIStyles.CurrentResources.January);
            months.Add(UIStyles.CurrentResources.February);
            months.Add(UIStyles.CurrentResources.March);
            months.Add(UIStyles.CurrentResources.April);
            months.Add(UIStyles.CurrentResources.May);
            months.Add(UIStyles.CurrentResources.June);
            months.Add(UIStyles.CurrentResources.July);
            months.Add(UIStyles.CurrentResources.August);
            months.Add(UIStyles.CurrentResources.September);
            months.Add(UIStyles.CurrentResources.October);
            months.Add(UIStyles.CurrentResources.November);
            months.Add(UIStyles.CurrentResources.December);
        }

        private void TopPanel_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex > 0)
            {
                if (TabControl.SelectedIndex == 2)
                {
                    if (new DateTime(Year, Month, 1) > max) return;
                    if (new DateTime(Year, Month, 1).EndOfMonth() < min) return;
                }

                if (TabControl.SelectedIndex == 1)
                {
                    if (Year < min.Year) return;
                    if (Year > max.Year) return;
                }

                TabControl.SelectedIndex--;
                activeDay = -1;
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                //TabControl.SelectPage(2);
                Year = date.Year;
                Month = date.Month;
                SetYearMonth(Year, Month);
                activeDay = -1;

                switch (ShowType)
                {
                    case UIDateType.YearMonthDay:
                        TabControl.SelectedTab = tabPage3;
                        break;
                    case UIDateType.YearMonth:
                        TabControl.SelectedTab = tabPage2;
                        break;
                    case UIDateType.Year:
                        TabControl.SelectedTab = tabPage1;
                        break;
                }

                TabControl_SelectedIndexChanged(TabControl, EventArgs.Empty);
            }
        }

        [DefaultValue(UIDateType.YearMonthDay)]
        [Description("日期显示类型"), Category("SunnyUI")]
        public UIDateType ShowType { get; set; }

        private int year;

        public int Year
        {
            get => year;
            set
            {
                year = value;
                int iy = year / 10 * 10;
                SetYears(iy);
            }
        }

        public int Month { get; set; }

        private void SetYearMonth(int iYear, int iMonth)
        {
            days.Clear();
            DateTime dt = new DateTime(iYear, iMonth, 1);
            int week = (int)dt.DayOfWeek;

            bool maxToEnd = false;
            DateTime dtBegin = week == 0 ? dt.AddDays(-7) : dt.AddDays(-week);
            for (int i = 1; i <= 42; i++)
            {
                try
                {
                    if (!maxToEnd && dtBegin.AddDays(i - 1).Date.Equals(DateTime.MaxValue.Date))
                    {
                        maxToEnd = true;
                    }

                    if (!maxToEnd)
                    {
                        DateTime lblDate = dtBegin.AddDays(i - 1);
                        days.Add(lblDate);
                    }
                    else
                    {
                        days.Add(DateTime.MaxValue.Date);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            p3.Invalidate();
            TopPanel.Text = Year + " - " + Month.ToString("D2");
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            b2.Visible = b3.Visible = TabControl.SelectedIndex == 2;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    int iy = year / 10 * 10;
                    SetYears(iy);
                    break;

                case 1:
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    TopPanel.Text = Year + " - " + Month.ToString("D2");
                    break;
            }
        }

        private void SetYears(int iy)
        {
            TopPanel.Text = iy + " - " + (iy + 9);

            years.Clear();
            years.Add(iy - 1);
            for (int i = 1; i <= 10; i++)
            {
                years.Add(iy + i - 1);
            }

            years.Add(iy + 10);
            activeYear = -1;
            p1.Invalidate();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Year = year / 10 * 10;
                    Year -= 10;
                    SetYears(Year);
                    break;

                case 1:
                    Year -= 1;
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    Year -= 1;
                    SetYearMonth(Year, Month);
                    break;
            }
        }

        private void b2_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(Year, Month, 1);
            dt = dt.AddMonths(-1);
            Year = dt.Year;
            Month = dt.Month;
            SetYearMonth(Year, Month);
        }

        private void b3_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(Year, Month, 1);
            if (dt.Year == DateTime.MaxValue.Year && dt.Month == DateTime.MaxValue.Month) return;
            dt = dt.AddMonths(1);
            Year = dt.Year;
            Month = dt.Month;
            SetYearMonth(Year, Month);
        }

        private void b4_Click(object sender, EventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Year = year / 10 * 10;
                    if (year == 9990) return;
                    Year += 10;
                    SetYears(Year);
                    break;

                case 1:
                    if (Year == DateTime.MaxValue.Year) return;
                    Year += 1;
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    if (Year == DateTime.MaxValue.Year) return;
                    Year += 1;
                    SetYearMonth(Year, Month);
                    break;
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            b1.SetStyleColor(uiColor);
            b2.SetStyleColor(uiColor);
            b3.SetStyleColor(uiColor);
            b4.SetStyleColor(uiColor);
            fillColor = Color.White;
            foreColor = uiColor.DropDownPanelForeColor;

            b1.FillColor = b2.FillColor = b3.FillColor = b4.FillColor = TopPanel.FillColor;
            RectColor = uiColor.RectColor;
            b1.SymbolColor = b2.SymbolColor = b3.SymbolColor = b4.SymbolColor = uiColor.RectColor;
            b1.ForeColor = b2.ForeColor = b3.ForeColor = b4.ForeColor = uiColor.RectColor;
            TopPanel.RectColor = p1.RectColor = p2.RectColor = p3.RectColor = uiColor.RectColor;
        }

        private void p2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            for (int i = 0; i < 12; i++)
            {
                int width = p2.Width / 4;
                int height = p2.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);

                Color color = ForeColor;
                if (new DateTime(Year, i + 1, 1) > max || new DateTime(Year, i + 1, 1).EndOfMonth() < min)
                {
                    color = Color.DarkGray;
                }
                else
                {
                    if (i + 1 == Month) color = PrimaryColor;
                    if (i + 1 == Month) color = PrimaryColor;
                    if (i == activeMonth) color = PrimaryColor;
                    if (i == activeMonth) color = PrimaryColor;
                }

                e.Graphics.DrawString(months[i], font, color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
            }
        }

        private void p2_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p2.Width / 4;
            int height = p2.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            Month = x + y * 4 + 1;
            if (Month <= 0 || Month > 12) return;

            if (new DateTime(Year, Month, 1) > max) return;
            if (new DateTime(Year, Month, 1).EndOfMonth() < min) return;

            SetYearMonth(Year, Month);
            activeMonth = -1;

            if (ShowType == UIDateType.YearMonth)
            {
                date = new DateTime(Year, Month, 1);
                DoValueChanged(this, Date);
                Close();
            }
            else
            {
                TabControl.SelectedTab = tabPage3;
            }
        }

        private int activeMonth = -1;

        private void p2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p2.Width / 4;
            int height = p2.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int im = x + y * 4;

            if (im + 1 <= 0 || im + 1 > 12) return;
            if (new DateTime(Year, im + 1, 1) > max) return;
            if (new DateTime(Year, im + 1, 1).EndOfMonth() < min) return;

            if (activeMonth != im)
            {
                activeMonth = im;
                p2.Invalidate();
            }
        }

        private int activeYear = -1;

        public bool ShowToday { get; set; }

        private void p1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            for (int i = 0; i < 12; i++)
            {
                int width = p1.Width / 4;
                int height = p1.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);

                Color color = (i == 0 || i == 11) ? Color.DarkGray : ForeColor;
                if (years[i] < min.Year || years[i] > max.Year)
                {
                    color = Color.DarkGray;
                }
                else
                {
                    if (years[i] == Year) color = PrimaryColor;
                    if (years[i] == Year) color = PrimaryColor;
                    if (i == activeYear) color = PrimaryColor;
                    if (i == activeYear) color = PrimaryColor;
                }

                if (years[i] != 10000)
                {
                    e.Graphics.DrawString(years[i].ToString(), font, color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
                }
            }
        }

        private void p1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p1.Width / 4;
            int height = p1.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int iy = x + y * 4;

            if (iy < 0) return;
            if (iy >= 12) return;
            if (years[iy] < min.Year) return;
            if (years[iy] > max.Year) return;

            if (activeYear != iy)
            {
                activeYear = iy;
                p1.Invalidate();
            }
        }

        private void p1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p1.Width / 4;
            int height = p1.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int iy = x + y * 4;
            if (iy < 0 || iy >= 12) return;
            if (years[iy] < min.Year) return;
            if (years[iy] > max.Year) return;

            Year = years[iy] > 9999 ? 9999 : years[iy];
            activeYear = -1;

            if (ShowType == UIDateType.Year)
            {
                date = new DateTime(Year, 1, 1);
                DoValueChanged(this, Date);
                Close();
            }
            else
            {
                TabControl.SelectedTab = tabPage2;
                p2.Invalidate();
            }
        }

        private void p3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int width = p3.Width / 7;
            int height = (p3.Height - 30 * SizeMultiple) / 6;
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            string[] weeks = { UIStyles.CurrentResources.Sunday, UIStyles.CurrentResources.Monday, UIStyles.CurrentResources.Tuesday, UIStyles.CurrentResources.Wednesday, UIStyles.CurrentResources.Thursday, UIStyles.CurrentResources.Friday, UIStyles.CurrentResources.Saturday };
            for (int i = 0; i < weeks.Length; i++)
            {
                e.Graphics.DrawString(weeks[i], font, ForeColor, new Rectangle(width * i, 4 * SizeMultiple, width, 19 * SizeMultiple), ContentAlignment.MiddleCenter);
            }

            e.Graphics.DrawLine(Color.LightGray, 8, 30 * SizeMultiple - 4, p3.Width - 8, 30 * SizeMultiple - 4);

            bool maxDrawer = false;
            for (int i = 0; i < 42; i++)
            {
                int left = width * (i % 7);
                int top = height * (i / 7);
                Color color = (days[i].Month == Month) ? ForeColor : Color.DarkGray;
                color = (days[i].DateString() == date.DateString()) ? b3.SymbolColor : color;
                if (days[i] < min) color = Color.DarkGray;
                if (days[i] > max) color = Color.DarkGray;

                if (days[i].DateString() == date.DateString())
                {
                    e.Graphics.DrawRectangle(b3.SymbolColor, new Rectangle(left + 1, top + 30 * SizeMultiple + 1, width - 2, height - 2));
                }

                if (!maxDrawer)
                {
                    e.Graphics.DrawString(days[i].Day.ToString(), font, i == activeDay ? PrimaryColor : color, new Rectangle(left, top + 30 * SizeMultiple, width, height), ContentAlignment.MiddleCenter);
                }

                if (!maxDrawer && days[i].Date.Equals(DateTime.MaxValue.Date))
                {
                    maxDrawer = true;
                }
            }

            if (ShowToday)
            {
                using Font SubFont = this.Font.DPIScaleFont(SizeMultiple == 1 ? 10.5f : 15.75f);
                e.Graphics.FillRectangle(p3.FillColor, p3.Width - width * 4 + 1, p3.Height - height + 1, width * 4 - 2, height - 2);
                e.Graphics.DrawString(UIStyles.CurrentResources.Today + "  " + DateTime.Now.DateString(), SubFont, isToday ? b3.SymbolColor : Color.DarkGray, new Rectangle(p3.Width - width * 4, p3.Height - height - 1, Width, height), ContentAlignment.MiddleLeft);
                SizeF sf = TextRenderer.MeasureText(UIStyles.CurrentResources.Today, SubFont);
                e.Graphics.DrawRectangle(b3.SymbolColor, new Rectangle(p3.Width - width * 4 + 1, p3.Height - height + 1, (int)sf.Width - 2, height - 4));
            }
        }

        private int activeDay = -1;
        private bool isToday;

        private void p3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Location.Y <= 30 * SizeMultiple)
            {
                activeDay = -1;
                p3.Invalidate();
                return;
            }

            int width = p3.Width / 7;
            int height = (p3.Height - 30 * SizeMultiple) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30 * SizeMultiple) / height;
            int iy = x + y * 7;

            if (iy.InRange(0, days.Count - 1))
            {
                if (days[iy] < min) return;
                if (days[iy] > max) return;
            }
            else
            {
                return;
            }

            bool istoday = ShowToday && e.Location.Y > p3.Top + p3.Height - height && e.Location.X > p3.Left + width * 3;

            if (activeDay != iy || istoday != isToday)
            {
                isToday = istoday;
                activeDay = iy;
                p3.Invalidate();
            }
        }

        private void p3_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Location.Y <= 30 * SizeMultiple) return;
            int width = p3.Width / 7;
            int height = (p3.Height - 30 * SizeMultiple) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30 * SizeMultiple) / height;
            int id = x + y * 7;
            if (id < 0 || id >= 42) return;

            if (ShowToday && e.Location.Y > p3.Height - height && e.Location.X > p3.Width - width * 4)
            {
                if (DateTime.Now.Date < min) return;
                if (DateTime.Now.Date > max) return;
                date = DateTime.Now.Date;
            }
            else
            {
                if (days[id].Date < min) return;
                if (days[id].Date > max) return;
                date = days[id].Date;
            }

            DoValueChanged(this, Date);
            Close();
        }

        public Color PrimaryColor { get; set; } = UIColor.Blue;

        private void p1_MouseLeave(object sender, EventArgs e)
        {
            activeYear = -1;
            p1.Invalidate();
        }

        private void p2_MouseLeave(object sender, EventArgs e)
        {
            activeMonth = -1;
            p2.Invalidate();
        }

        private void p3_MouseLeave(object sender, EventArgs e)
        {
            activeDay = -1;
            p3.Invalidate();
        }
    }
}