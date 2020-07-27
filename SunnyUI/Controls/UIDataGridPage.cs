using System;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public class UIDataGridPage : UIPanel
    {
        private UISymbolButton b1;
        private UISymbolButton b3;
        private UISymbolButton b2;
        private UISymbolButton b7;
        private UISymbolButton b6;
        private UISymbolButton b5;
        private UISymbolButton b4;
        private UISymbolButton b15;
        private UISymbolButton b14;
        private UISymbolButton b13;
        private UISymbolButton b12;
        private UISymbolButton b11;
        private UISymbolButton b10;
        private UISymbolButton b9;
        private UISymbolButton b8;
        private UISymbolButton b16;
        private UIPanel p1;
        private UILabel uiLabel2;
        private UITextBox edtPage;
        private UILabel uiLabel1;
        private UIComboBox cb1;
        private UISymbolButton b0;
        private ConcurrentDictionary<int, UISymbolButton> buttons = new ConcurrentDictionary<int, UISymbolButton>();

        public UIDataGridPage()
        {
            InitializeComponent();

            ShowText = false;
            buttons.TryAdd(0, b0);
            buttons.TryAdd(1, b1);
            buttons.TryAdd(2, b2);
            buttons.TryAdd(3, b3);
            buttons.TryAdd(4, b4);
            buttons.TryAdd(5, b5);
            buttons.TryAdd(6, b6);
            buttons.TryAdd(7, b7);
            buttons.TryAdd(8, b8);
            buttons.TryAdd(9, b9);
            buttons.TryAdd(10, b10);
            buttons.TryAdd(11, b11);
            buttons.TryAdd(12, b12);
            buttons.TryAdd(13, b13);
            buttons.TryAdd(14, b14);
            buttons.TryAdd(15, b15);
            buttons.TryAdd(16, b16);

            for (int i = 0; i < 17; i++)
            {
                buttons[i].MouseEnter += UIDataGridPage_MouseEnter;
                buttons[i].MouseLeave += UIDataGridPage_MouseLeave;
                buttons[i].Click += UIDataGridPage_Click;
            }
        }

        private void UIDataGridPage_Click(object sender, EventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            if (btn.TagString.IsValid())
            {
                ActivePage += btn.Tag.ToString().ToInt();
            }
            else
            {
                ActivePage = btn.Tag.ToString().ToInt();
            }
        }

        private void UIDataGridPage_MouseLeave(object sender, EventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            if (btn.TagString == "<<" || btn.TagString == ">>")
            {
                btn.Symbol = 0;
                btn.Text = @"···";
            }
        }

        private void UIDataGridPage_MouseEnter(object sender, EventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            if (btn.TagString == "<<")
            {
                btn.Symbol = 61696;
                btn.Text = "";
            }

            if (btn.TagString == ">>")
            {
                btn.Symbol = 61697;
                btn.Text = "";
            }
        }

        #region InitializeComponent

        private void InitializeComponent()
        {
            this.b0 = new Sunny.UI.UISymbolButton();
            this.b1 = new Sunny.UI.UISymbolButton();
            this.b3 = new Sunny.UI.UISymbolButton();
            this.b2 = new Sunny.UI.UISymbolButton();
            this.b7 = new Sunny.UI.UISymbolButton();
            this.b6 = new Sunny.UI.UISymbolButton();
            this.b5 = new Sunny.UI.UISymbolButton();
            this.b4 = new Sunny.UI.UISymbolButton();
            this.b15 = new Sunny.UI.UISymbolButton();
            this.b14 = new Sunny.UI.UISymbolButton();
            this.b13 = new Sunny.UI.UISymbolButton();
            this.b12 = new Sunny.UI.UISymbolButton();
            this.b11 = new Sunny.UI.UISymbolButton();
            this.b10 = new Sunny.UI.UISymbolButton();
            this.b9 = new Sunny.UI.UISymbolButton();
            this.b8 = new Sunny.UI.UISymbolButton();
            this.b16 = new Sunny.UI.UISymbolButton();
            this.p1 = new Sunny.UI.UIPanel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.edtPage = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.cb1 = new Sunny.UI.UIComboBox();
            this.p1.SuspendLayout();
            this.SuspendLayout();
            // 
            // b0
            // 
            this.b0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b0.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b0.Location = new System.Drawing.Point(3, 3);
            this.b0.Name = "b0";
            this.b0.Size = new System.Drawing.Size(29, 29);
            this.b0.Symbol = 61700;
            this.b0.TabIndex = 0;
            this.b0.Tag = "-1";
            this.b0.TagString = "<";
            // 
            // b1
            // 
            this.b1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b1.Location = new System.Drawing.Point(35, 3);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(29, 29);
            this.b1.Symbol = 0;
            this.b1.TabIndex = 1;
            this.b1.Text = "0";
            // 
            // b3
            // 
            this.b3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b3.Location = new System.Drawing.Point(99, 3);
            this.b3.Name = "b3";
            this.b3.Size = new System.Drawing.Size(29, 29);
            this.b3.Symbol = 0;
            this.b3.TabIndex = 3;
            this.b3.Text = "0";
            // 
            // b2
            // 
            this.b2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b2.Location = new System.Drawing.Point(67, 3);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(29, 29);
            this.b2.Symbol = 0;
            this.b2.TabIndex = 2;
            this.b2.Text = "0";
            // 
            // b7
            // 
            this.b7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b7.Location = new System.Drawing.Point(227, 3);
            this.b7.Name = "b7";
            this.b7.Size = new System.Drawing.Size(29, 29);
            this.b7.Symbol = 0;
            this.b7.TabIndex = 7;
            this.b7.Text = "0";
            // 
            // b6
            // 
            this.b6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b6.Location = new System.Drawing.Point(195, 3);
            this.b6.Name = "b6";
            this.b6.Size = new System.Drawing.Size(29, 29);
            this.b6.Symbol = 0;
            this.b6.TabIndex = 6;
            this.b6.Text = "0";
            // 
            // b5
            // 
            this.b5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b5.Location = new System.Drawing.Point(163, 3);
            this.b5.Name = "b5";
            this.b5.Size = new System.Drawing.Size(29, 29);
            this.b5.Symbol = 0;
            this.b5.TabIndex = 5;
            this.b5.Text = "0";
            // 
            // b4
            // 
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b4.Location = new System.Drawing.Point(131, 3);
            this.b4.Name = "b4";
            this.b4.Size = new System.Drawing.Size(29, 29);
            this.b4.Symbol = 0;
            this.b4.TabIndex = 4;
            this.b4.Text = "0";
            // 
            // b15
            // 
            this.b15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b15.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b15.Location = new System.Drawing.Point(483, 3);
            this.b15.Name = "b15";
            this.b15.Size = new System.Drawing.Size(29, 29);
            this.b15.Symbol = 0;
            this.b15.TabIndex = 15;
            this.b15.Text = "0";
            // 
            // b14
            // 
            this.b14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b14.Location = new System.Drawing.Point(451, 3);
            this.b14.Name = "b14";
            this.b14.Size = new System.Drawing.Size(29, 29);
            this.b14.Symbol = 0;
            this.b14.TabIndex = 14;
            this.b14.Text = "0";
            // 
            // b13
            // 
            this.b13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b13.Location = new System.Drawing.Point(419, 3);
            this.b13.Name = "b13";
            this.b13.Size = new System.Drawing.Size(29, 29);
            this.b13.Symbol = 0;
            this.b13.TabIndex = 13;
            this.b13.Text = "0";
            // 
            // b12
            // 
            this.b12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b12.Location = new System.Drawing.Point(387, 3);
            this.b12.Name = "b12";
            this.b12.Size = new System.Drawing.Size(29, 29);
            this.b12.Symbol = 0;
            this.b12.TabIndex = 12;
            this.b12.Text = "0";
            // 
            // b11
            // 
            this.b11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b11.Location = new System.Drawing.Point(355, 3);
            this.b11.Name = "b11";
            this.b11.Size = new System.Drawing.Size(29, 29);
            this.b11.Symbol = 0;
            this.b11.TabIndex = 11;
            this.b11.Text = "0";
            // 
            // b10
            // 
            this.b10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b10.Location = new System.Drawing.Point(323, 3);
            this.b10.Name = "b10";
            this.b10.Size = new System.Drawing.Size(29, 29);
            this.b10.Symbol = 0;
            this.b10.TabIndex = 10;
            this.b10.Text = "0";
            // 
            // b9
            // 
            this.b9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b9.Location = new System.Drawing.Point(291, 3);
            this.b9.Name = "b9";
            this.b9.Size = new System.Drawing.Size(29, 29);
            this.b9.Symbol = 0;
            this.b9.TabIndex = 9;
            this.b9.Text = "0";
            // 
            // b8
            // 
            this.b8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b8.Location = new System.Drawing.Point(259, 3);
            this.b8.Name = "b8";
            this.b8.Size = new System.Drawing.Size(29, 29);
            this.b8.Symbol = 0;
            this.b8.TabIndex = 8;
            this.b8.Text = "0";
            // 
            // b16
            // 
            this.b16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b16.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b16.Location = new System.Drawing.Point(515, 3);
            this.b16.Name = "b16";
            this.b16.Size = new System.Drawing.Size(29, 29);
            this.b16.Symbol = 61701;
            this.b16.TabIndex = 16;
            this.b16.Tag = "1";
            this.b16.TagString = ">";
            this.b16.LocationChanged += new System.EventHandler(this.b16_LocationChanged);
            // 
            // p1
            // 
            this.p1.Controls.Add(this.uiLabel2);
            this.p1.Controls.Add(this.edtPage);
            this.p1.Controls.Add(this.uiLabel1);
            this.p1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.p1.Location = new System.Drawing.Point(549, 3);
            this.p1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p1.Name = "p1";
            this.p1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.p1.Size = new System.Drawing.Size(126, 29);
            this.p1.TabIndex = 17;
            this.p1.Text = null;
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(100, 4);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(26, 21);
            this.uiLabel2.TabIndex = 2;
            this.uiLabel2.Text = "页";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtPage
            // 
            this.edtPage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtPage.DoubleValue = 10D;
            this.edtPage.FillColor = System.Drawing.Color.White;
            this.edtPage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.edtPage.HasMinimum = true;
            this.edtPage.IntValue = 10;
            this.edtPage.Location = new System.Drawing.Point(45, 0);
            this.edtPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtPage.Maximum = 2147483647D;
            this.edtPage.Minimum = 1D;
            this.edtPage.Name = "edtPage";
            this.edtPage.Padding = new System.Windows.Forms.Padding(5);
            this.edtPage.Size = new System.Drawing.Size(53, 29);
            this.edtPage.TabIndex = 1;
            this.edtPage.Text = "10";
            this.edtPage.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.edtPage.Type = Sunny.UI.UITextBox.UIEditType.Integer;
            this.edtPage.Leave += new System.EventHandler(this.edtPage_Leave);
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(3, 4);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(42, 21);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "前往";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb1
            // 
            this.cb1.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cb1.FillColor = System.Drawing.Color.White;
            this.cb1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cb1.Items.AddRange(new object[] {
            "10条/页",
            "20条/页",
            "50条/页",
            "100条/页",
            "200条/页"});
            this.cb1.Location = new System.Drawing.Point(683, 3);
            this.cb1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb1.MinimumSize = new System.Drawing.Size(63, 0);
            this.cb1.Name = "cb1";
            this.cb1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.cb1.Size = new System.Drawing.Size(103, 29);
            this.cb1.TabIndex = 19;
            this.cb1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cb1.Visible = false;
            // 
            // UIDataGridPage
            // 
            this.Controls.Add(this.cb1);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.b16);
            this.Controls.Add(this.b15);
            this.Controls.Add(this.b14);
            this.Controls.Add(this.b13);
            this.Controls.Add(this.b12);
            this.Controls.Add(this.b11);
            this.Controls.Add(this.b10);
            this.Controls.Add(this.b9);
            this.Controls.Add(this.b8);
            this.Controls.Add(this.b7);
            this.Controls.Add(this.b6);
            this.Controls.Add(this.b5);
            this.Controls.Add(this.b4);
            this.Controls.Add(this.b3);
            this.Controls.Add(this.b2);
            this.Controls.Add(this.b1);
            this.Controls.Add(this.b0);
            this.Name = "UIDataGridPage";
            this.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Size = new System.Drawing.Size(1100, 35);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion InitializeComponent

        private int totalCount = 1000;

        /// <summary>
        /// 总条目数
        /// </summary>
        [DefaultValue(1000), Description("总条目数")]
        public int TotalCount
        {
            get => totalCount;
            set
            {
                if (totalCount != value)
                {
                    totalCount = Math.Max(0, value);
                    SetShowButtons();
                }
            }
        }

        private int pageSize = 20;

        /// <summary>
        /// 每页显示条目个数
        /// </summary>
        [DefaultValue(20),Description("每页显示条目个数")]
        public int PageSize
        {
            get => pageSize;
            set
            {
                if (pageSize != value)
                {
                    pageSize = Math.Max(1, value);
                    SetShowButtons();
                }
            }
        }

        private int pagerCount = 7;

        /// <summary>
        /// 页码按钮的数量，当总页数超过该值时会折叠
        /// 大于等于 5 且小于等于 13 的奇数
        /// </summary>
        [DefaultValue(7),Description("页码按钮的数量，当总页数超过该值时会折叠，大于等于5且小于等于13的奇数")]
        public int PagerCount
        {
            get => pagerCount;
            set
            {
                if (pagerCount != value)
                {
                    pagerCount = Math.Max(5, value);
                    pagerCount = Math.Min(13, pagerCount);
                    if (pagerCount.Mod(2) == 0)
                        pagerCount = pagerCount - 1;

                    SetShowButtons();
                }
            }
        }

        private int PageCount;
        private int activePage=1;

        /// <summary>
        /// 选中页面
        /// </summary>
        [DefaultValue(1), Description("选中页面")]
        public int ActivePage
        {
            get => activePage;
            set
            {
                if (activePage != value)
                {
                    activePage = Math.Max(1, value);
                    edtPage.IntValue = activePage;
                    SetShowButtons();
                }
            }
        }

        private void SetShowButton(int buttonIdx, int pageIdx, int activeIdx)
        {
            buttons[buttonIdx].Symbol = 0;
            buttons[buttonIdx].Text = pageIdx.ToString();
            buttons[buttonIdx].Tag = pageIdx;
            buttons[buttonIdx].Visible = true;
            buttons[buttonIdx].TagString = "";
            buttons[buttonIdx].Selected = activeIdx == pageIdx;
        }

        private void SetShowButton(int buttonIdx, int addCount, string tagString)
        {
            buttons[buttonIdx].Symbol = 0;
            buttons[buttonIdx].Text = @"···";
            buttons[buttonIdx].Tag = addCount;
            buttons[buttonIdx].Visible = true;
            buttons[buttonIdx].TagString = tagString;
            buttons[buttonIdx].Selected = false;
        }

        private void SetHideButton(int beginIdx)
        {
            for (int i = beginIdx; i < 16; i++)
            {
                buttons[i].Visible = false;
            }
        }

        private void SetShowButtons()
        {
            b0.Visible = true;
            b16.Visible = true;

            PageCount = TotalCount.Mod(PageSize) == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
            edtPage.HasMaximum = true;
            edtPage.Maximum = PageCount;

            if (activePage > PageCount) activePage = PageCount;
            if (activePage < 1) activePage = 1;
            edtPage.IntValue = activePage;

            if (TotalCount == 0)
            {
                PageCount = 1;
                activePage = 1;
                SetShowButton(1, 1, 1);
                SetHideButton(2);
                b16.Left = b1.Right + 3;
                return;
            }

            if (PageCount <= PagerCount + 2)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    SetShowButton(i, i, activePage);
                }

                b16.Left = buttons[PageCount].Right + 3;
                SetHideButton(PageCount + 1);
            }
            else
            {
                //左
                int leftShow = PagerCount / 2 + 1 + 2;
                if (activePage <= leftShow)
                {
                    for (int i = 1; i <= leftShow; i++)
                    {
                        SetShowButton(i, i, activePage);
                    }

                    SetShowButton(leftShow + 1, PagerCount - 2, ">>");
                    SetShowButton(leftShow + 2, PageCount, activePage);
                    SetHideButton(leftShow + 3);
                    b16.Left = buttons[leftShow + 2].Right + 3;
                    return;
                }

                //右
                int rightShow = PageCount - (PagerCount / 2 + 1) - 1;
                if (activePage >= rightShow)
                {
                    SetShowButton(1, 1, activePage);
                    SetShowButton(2, 2 - PagerCount, "<<");

                    int idx = 3;
                    for (int i = rightShow; i <= PageCount; i++)
                    {
                        SetShowButton(idx, i, activePage);
                        idx++;
                    }

                    b16.Left = buttons[idx - 1].Right + 3;
                    SetHideButton(idx);
                    return;
                }

                //中
                SetShowButton(1, 1, activePage);
                SetShowButton(2, 2 - PagerCount, "<<");
                int cIdx = 3;
                int sIdx = (PagerCount - 2) / 2;
                for (int i = cIdx; i <= PagerCount; i++)
                {
                    SetShowButton(cIdx, activePage - sIdx + (i - 3), activePage);
                    cIdx++;
                }

                SetShowButton(cIdx, PagerCount - 2, ">>");
                SetShowButton(cIdx + 1, PageCount, activePage);
                b16.Left = buttons[cIdx + 1].Right + 3;
                SetHideButton(cIdx + 2);
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            foreach (var button in buttons.Values)
            {
                button.FillColor = uiColor.PlainColor;
                button.ForeColor = uiColor.RectColor;
                button.FillSelectedColor = uiColor.ButtonFillColor;
            }
        }

        private void b16_LocationChanged(object sender, EventArgs e)
        {
            p1.Left = b16.Right + 3;
        }

        private void edtPage_Leave(object sender, EventArgs e)
        {
            ActivePage = edtPage.IntValue;
        }
    }
}