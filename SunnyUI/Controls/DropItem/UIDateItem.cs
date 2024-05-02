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
            this.TopPanel = new Sunny.UI.UIPanel();
            this.b4 = new Sunny.UI.UISymbolButton();
            this.b3 = new Sunny.UI.UISymbolButton();
            this.b2 = new Sunny.UI.UISymbolButton();
            this.b1 = new Sunny.UI.UISymbolButton();
            this.TabControl = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.p1 = new Sunny.UI.UIPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.p2 = new Sunny.UI.UIPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.p3 = new Sunny.UI.UIPanel();
            this.TopPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.b4);
            this.TopPanel.Controls.Add(this.b3);
            this.TopPanel.Controls.Add(this.b2);
            this.TopPanel.Controls.Add(this.b1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.FillColor = System.Drawing.Color.White;
            this.TopPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.TopPanel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.TopPanel.Size = new System.Drawing.Size(461, 31);
            this.TopPanel.Style = Sunny.UI.UIStyle.Custom;
            this.TopPanel.StyleCustomMode = true;
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Text = "2020-05-05";
            this.TopPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.TopPanel.Click += new System.EventHandler(this.TopPanel_Click);
            // 
            // b4
            // 
            this.b4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b4.BackColor = System.Drawing.Color.Transparent;
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.FillColor = System.Drawing.Color.White;
            this.b4.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.ImageInterval = 0;
            this.b4.Location = new System.Drawing.Point(427, 4);
            this.b4.MinimumSize = new System.Drawing.Size(1, 1);
            this.b4.Name = "b4";
            this.b4.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b4.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b4.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b4.Size = new System.Drawing.Size(30, 24);
            this.b4.Style = Sunny.UI.UIStyle.Custom;
            this.b4.StyleCustomMode = true;
            this.b4.Symbol = 61697;
            this.b4.TabIndex = 3;
            this.b4.Click += new System.EventHandler(this.b4_Click);
            // 
            // b3
            // 
            this.b3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b3.BackColor = System.Drawing.Color.Transparent;
            this.b3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b3.FillColor = System.Drawing.Color.White;
            this.b3.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.ImageInterval = 0;
            this.b3.Location = new System.Drawing.Point(391, 4);
            this.b3.MinimumSize = new System.Drawing.Size(1, 1);
            this.b3.Name = "b3";
            this.b3.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b3.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b3.Size = new System.Drawing.Size(30, 24);
            this.b3.Style = Sunny.UI.UIStyle.Custom;
            this.b3.StyleCustomMode = true;
            this.b3.Symbol = 61701;
            this.b3.TabIndex = 2;
            this.b3.Click += new System.EventHandler(this.b3_Click);
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b2.FillColor = System.Drawing.Color.White;
            this.b2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.ImageInterval = 0;
            this.b2.Location = new System.Drawing.Point(40, 4);
            this.b2.MinimumSize = new System.Drawing.Size(1, 1);
            this.b2.Name = "b2";
            this.b2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b2.Size = new System.Drawing.Size(30, 24);
            this.b2.Style = Sunny.UI.UIStyle.Custom;
            this.b2.StyleCustomMode = true;
            this.b2.Symbol = 61700;
            this.b2.TabIndex = 1;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b1.FillColor = System.Drawing.Color.White;
            this.b1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.ImageInterval = 0;
            this.b1.Location = new System.Drawing.Point(4, 4);
            this.b1.MinimumSize = new System.Drawing.Size(1, 1);
            this.b1.Name = "b1";
            this.b1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b1.Size = new System.Drawing.Size(30, 24);
            this.b1.Style = Sunny.UI.UIStyle.Custom;
            this.b1.StyleCustomMode = true;
            this.b1.Symbol = 61696;
            this.b1.TabIndex = 0;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabControl.ItemSize = new System.Drawing.Size(150, 40);
            this.TabControl.Location = new System.Drawing.Point(0, 31);
            this.TabControl.MainPage = "";
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(461, 317);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.Style = Sunny.UI.UIStyle.Custom;
            this.TabControl.TabIndex = 1;
            this.TabControl.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.p1);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(461, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.FillColor = System.Drawing.Color.White;
            this.p1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p1.MinimumSize = new System.Drawing.Size(1, 1);
            this.p1.Name = "p1";
            this.p1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p1.Size = new System.Drawing.Size(461, 277);
            this.p1.Style = Sunny.UI.UIStyle.Custom;
            this.p1.TabIndex = 0;
            this.p1.Text = null;
            this.p1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p1.Paint += new System.Windows.Forms.PaintEventHandler(this.p1_Paint);
            this.p1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p1_MouseClick);
            this.p1.MouseLeave += new System.EventHandler(this.p1_MouseLeave);
            this.p1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p1_MouseMove);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.p2);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(461, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.FillColor = System.Drawing.Color.White;
            this.p2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p2.Location = new System.Drawing.Point(0, 0);
            this.p2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p2.MinimumSize = new System.Drawing.Size(1, 1);
            this.p2.Name = "p2";
            this.p2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p2.Size = new System.Drawing.Size(461, 277);
            this.p2.Style = Sunny.UI.UIStyle.Custom;
            this.p2.TabIndex = 1;
            this.p2.Text = null;
            this.p2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p2.Paint += new System.Windows.Forms.PaintEventHandler(this.p2_Paint);
            this.p2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p2_MouseClick);
            this.p2.MouseLeave += new System.EventHandler(this.p2_MouseLeave);
            this.p2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p2_MouseMove);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.p3);
            this.tabPage3.Location = new System.Drawing.Point(0, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(461, 277);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.FillColor = System.Drawing.Color.White;
            this.p3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p3.Location = new System.Drawing.Point(0, 0);
            this.p3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p3.MinimumSize = new System.Drawing.Size(1, 1);
            this.p3.Name = "p3";
            this.p3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p3.Size = new System.Drawing.Size(461, 277);
            this.p3.Style = Sunny.UI.UIStyle.Custom;
            this.p3.TabIndex = 2;
            this.p3.Text = null;
            this.p3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p3.Paint += new System.Windows.Forms.PaintEventHandler(this.p3_Paint);
            this.p3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p3_MouseClick);
            this.p3.MouseLeave += new System.EventHandler(this.p3_MouseLeave);
            this.p3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p3_MouseMove);
            // 
            // UIDateItem
            // 
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.TopPanel);
            this.FillColor = System.Drawing.Color.White;
            this.Name = "UIDateItem";
            this.Size = new System.Drawing.Size(461, 348);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.TopPanel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion InitializeComponent

        private readonly List<string> months = new List<string>();
        private readonly List<int> years = new List<int>();
        private readonly List<DateTime> days = new List<DateTime>();

        public UIDateItem()
        {
            InitializeComponent();
            Width = 284;
            Height = 200;
            TabControl.TabVisible = false;
            Translate();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            TopPanel.SetDPIScale();
        }

        public DateTime max = DateTime.MaxValue;
        public DateTime min = DateTime.MinValue;

        public void Translate()
        {
            months.Clear();
            months.Add(UILocalize.January);
            months.Add(UILocalize.February);
            months.Add(UILocalize.March);
            months.Add(UILocalize.April);
            months.Add(UILocalize.May);
            months.Add(UILocalize.June);
            months.Add(UILocalize.July);
            months.Add(UILocalize.August);
            months.Add(UILocalize.September);
            months.Add(UILocalize.October);
            months.Add(UILocalize.November);
            months.Add(UILocalize.December);
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

                e.Graphics.DrawString(months[i], Font, color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
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
                    e.Graphics.DrawString(years[i].ToString(), Font, color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
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
            int height = (p3.Height - 30) / 6;
            string[] weeks = { UILocalize.Sunday, UILocalize.Monday, UILocalize.Tuesday, UILocalize.Wednesday, UILocalize.Thursday, UILocalize.Friday, UILocalize.Saturday };
            for (int i = 0; i < weeks.Length; i++)
            {
                e.Graphics.DrawString(weeks[i], Font, ForeColor, new Rectangle(width * i, 4, width, 19), ContentAlignment.MiddleCenter);
            }

            e.Graphics.DrawLine(Color.DarkGray, 8, 26, 268, 26);

            bool maxDrawer = false;
            for (int i = 0; i < 42; i++)
            {
                int left = width * (i % 7);
                int top = height * (i / 7);
                Color color = (days[i].Month == Month) ? ForeColor : Color.DarkGray;
                color = (days[i].DateString() == date.DateString()) ? PrimaryColor : color;
                if (days[i] < min) color = Color.DarkGray;
                if (days[i] > max) color = Color.DarkGray;

                if (!maxDrawer)
                {
                    e.Graphics.DrawString(days[i].Day.ToString(), Font, i == activeDay ? PrimaryColor : color, new Rectangle(left, top + 30, width, height), ContentAlignment.MiddleCenter);
                }

                if (!maxDrawer && days[i].Date.Equals(DateTime.MaxValue.Date))
                {
                    maxDrawer = true;
                }
            }

            if (ShowToday)
            {
                using Font SubFont = this.Font.DPIScaleFont(10.5f);
                e.Graphics.FillRectangle(p3.FillColor, p3.Width - width * 4 + 1, p3.Height - height + 1, width * 4 - 2, height - 2);
                e.Graphics.FillRoundRectangle(PrimaryColor, new Rectangle(p3.Width - width * 4 + 6, p3.Height - height + 3, 8, height - 10), 3);
                e.Graphics.DrawString(UILocalize.Today + ": " + DateTime.Now.DateString(), SubFont, isToday ? PrimaryColor : Color.DarkGray, new Rectangle(p3.Width - width * 4 + 17, p3.Height - height - 1, Width, height), ContentAlignment.MiddleLeft);
            }
        }

        private int activeDay = -1;
        private bool isToday;

        private void p3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Location.Y <= 30)
            {
                activeDay = -1;
                p3.Invalidate();
                return;
            }

            int width = p3.Width / 7;
            int height = (p3.Height - 30) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30) / height;
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
            if (e.Location.Y <= 30) return;
            int width = p3.Width / 7;
            int height = (p3.Height - 30) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30) / height;
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