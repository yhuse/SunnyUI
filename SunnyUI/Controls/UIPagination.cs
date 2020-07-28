/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIPagination.cs
 * 文件说明: 分页
 * 当前版本: V2.2
 * 创建日期: 2020-07-26
 *
 * 2020-07-15: V2.2.6 新增分页控件
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms; 

namespace Sunny.UI
{
    public class UIPagination : UIPanel
    {
        public delegate void OnPageChangeEventHandler(object sender, object pagingSource, int pageIndex, int count);

        private int activePage = 1;
        private UISymbolButton b0;
        private UISymbolButton b1;
        private UISymbolButton b10;
        private UISymbolButton b11;
        private UISymbolButton b12;
        private UISymbolButton b13;
        private UISymbolButton b14;
        private UISymbolButton b15;
        private UISymbolButton b16;
        private UISymbolButton b2;
        private UISymbolButton b3;
        private UISymbolButton b4;
        private UISymbolButton b5;
        private UISymbolButton b6;
        private UISymbolButton b7;
        private UISymbolButton b8;
        private UISymbolButton b9;
        private UISymbolButton btnSelect;

        private readonly ConcurrentDictionary<int, UISymbolButton> buttons =
            new ConcurrentDictionary<int, UISymbolButton>();

        private UIComboBox cb1;
        private CurrencyManager dataManager;

        private object dataSource;
        private UITextBox edtPage;

        private bool inSetDataConnection;
        private UIPanel p1;

        private int PageCount;

        private int pagerCount = 7;

        private int pageSize = 20;

        private int totalCount = 1000;
        private UILabel uiLabel1;
        private UILabel uiLabel2;

        public UIPagination()
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

            for (var i = 0; i < 17; i++)
            {
                buttons[i].MouseEnter += UIDataGridPage_MouseEnter;
                buttons[i].MouseLeave += UIDataGridPage_MouseLeave;
                buttons[i].Click += UIDataGridPage_Click;
            }
        }

        /// <summary>
        ///     总条目数
        /// </summary>
        [DefaultValue(1000)]
        [Description("总条目数")]
        [Category("SunnyUI")]
        public int TotalCount
        {
            get => totalCount;
            set
            {
                if (totalCount != value)
                {
                    totalCount = Math.Max(0, value);
                    SetShowButtons();
                    DataBind();
                }
            }
        }

        /// <summary>
        ///     每页显示条目个数
        /// </summary>
        [DefaultValue(20)]
        [Description("每页显示条目个数")]
        [Category("SunnyUI")]
        public int PageSize
        {
            get => pageSize;
            set
            {
                if (pageSize != value)
                {
                    pageSize = Math.Max(1, value);
                    SetShowButtons();
                    DataBind();
                }
            }
        }

        /// <summary>
        ///     页码按钮的数量，当总页数超过该值时会折叠
        ///     大于等于 5 且小于等于 13 的奇数
        /// </summary>
        [DefaultValue(7)]
        [Description("页码按钮的数量，当总页数超过该值时会折叠，大于等于5且小于等于13的奇数")]
        [Category("SunnyUI")]
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
                    DataBind();
                }
            }
        }

        /// <summary>
        ///     选中页面
        /// </summary>
        [DefaultValue(1)]
        [Description("选中页面")]
        [Category("SunnyUI")]
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
                    DataBind();
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowJumpButton
        {
            get => p1.Visible;
            set => p1.Visible = value;
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get => dataSource;
            set
            {
                if (value != null)
                    if (!(value is DataTable || value is IList))
                        throw new Exception(UILocalize.GridDataSourceException);

                SetDataConnection(value, new BindingMemberInfo(""));
                dataSource = value;
                activePage = 1;
                TotalCount = dataManager?.List.Count ?? 0;
            }
        }

        [Browsable(false)] public object PageDataSource { get; private set; }

        private void UIDataGridPage_Click(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
            btn.BringToFront();
            if (btn.TagString.IsValid())
                ActivePage += btn.Tag.ToString().ToInt();
            else
                ActivePage = btn.Tag.ToString().ToInt();
        }

        private void UIDataGridPage_MouseLeave(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
            if (btn.TagString == "<<" || btn.TagString == ">>")
            {
                btn.Symbol = 0;
                btn.Text = @"···";
            }
        }

        private void UIDataGridPage_MouseEnter(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
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
            b0 = new UISymbolButton();
            b1 = new UISymbolButton();
            b3 = new UISymbolButton();
            b2 = new UISymbolButton();
            b7 = new UISymbolButton();
            b6 = new UISymbolButton();
            b5 = new UISymbolButton();
            b4 = new UISymbolButton();
            b15 = new UISymbolButton();
            b14 = new UISymbolButton();
            b13 = new UISymbolButton();
            b12 = new UISymbolButton();
            b11 = new UISymbolButton();
            b10 = new UISymbolButton();
            b9 = new UISymbolButton();
            b8 = new UISymbolButton();
            b16 = new UISymbolButton();
            p1 = new UIPanel();
            btnSelect = new UISymbolButton();
            uiLabel2 = new UILabel();
            edtPage = new UITextBox();
            uiLabel1 = new UILabel();
            cb1 = new UIComboBox();
            p1.SuspendLayout();
            SuspendLayout();
            //
            // b0
            //
            b0.Cursor = Cursors.Hand;
            b0.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b0.ImageAlign = ContentAlignment.MiddleLeft;
            b0.Location = new Point(3, 3);
            b0.Name = "b0";
            b0.Padding = new Padding(5, 0, 5, 0);
            b0.RadiusSides = UICornerRadiusSides.LeftTop | UICornerRadiusSides.LeftBottom;
            b0.Size = new Size(75, 29);
            b0.Symbol = 61700;
            b0.TabIndex = 0;
            b0.Tag = "-1";
            b0.TagString = "<";
            b0.Text = "上一页";
            b0.TextAlign = ContentAlignment.MiddleRight;
            //
            // b1
            //
            b1.Cursor = Cursors.Hand;
            b1.Font = new Font("微软雅黑", 12F);
            b1.Location = new Point(77, 3);
            b1.Name = "b1";
            b1.RadiusSides = UICornerRadiusSides.None;
            b1.Size = new Size(29, 29);
            b1.Symbol = 0;
            b1.TabIndex = 1;
            b1.Text = "0";
            //
            // b3
            //
            b3.Cursor = Cursors.Hand;
            b3.Font = new Font("微软雅黑", 12F);
            b3.Location = new Point(133, 3);
            b3.Name = "b3";
            b3.RadiusSides = UICornerRadiusSides.None;
            b3.Size = new Size(29, 29);
            b3.Symbol = 0;
            b3.TabIndex = 3;
            b3.Text = "0";
            //
            // b2
            //
            b2.Cursor = Cursors.Hand;
            b2.Font = new Font("微软雅黑", 12F);
            b2.Location = new Point(105, 3);
            b2.Name = "b2";
            b2.RadiusSides = UICornerRadiusSides.None;
            b2.Size = new Size(29, 29);
            b2.Symbol = 0;
            b2.TabIndex = 2;
            b2.Text = "0";
            //
            // b7
            //
            b7.Cursor = Cursors.Hand;
            b7.Font = new Font("微软雅黑", 12F);
            b7.Location = new Point(245, 3);
            b7.Name = "b7";
            b7.RadiusSides = UICornerRadiusSides.None;
            b7.Size = new Size(29, 29);
            b7.Symbol = 0;
            b7.TabIndex = 7;
            b7.Text = "0";
            //
            // b6
            //
            b6.Cursor = Cursors.Hand;
            b6.Font = new Font("微软雅黑", 12F);
            b6.Location = new Point(217, 3);
            b6.Name = "b6";
            b6.RadiusSides = UICornerRadiusSides.None;
            b6.Size = new Size(29, 29);
            b6.Symbol = 0;
            b6.TabIndex = 6;
            b6.Text = "0";
            //
            // b5
            //
            b5.Cursor = Cursors.Hand;
            b5.Font = new Font("微软雅黑", 12F);
            b5.Location = new Point(189, 3);
            b5.Name = "b5";
            b5.RadiusSides = UICornerRadiusSides.None;
            b5.Size = new Size(29, 29);
            b5.Symbol = 0;
            b5.TabIndex = 5;
            b5.Text = "0";
            //
            // b4
            //
            b4.Cursor = Cursors.Hand;
            b4.Font = new Font("微软雅黑", 12F);
            b4.Location = new Point(161, 3);
            b4.Name = "b4";
            b4.RadiusSides = UICornerRadiusSides.None;
            b4.Size = new Size(29, 29);
            b4.Symbol = 0;
            b4.TabIndex = 4;
            b4.Text = "0";
            //
            // b15
            //
            b15.Cursor = Cursors.Hand;
            b15.Font = new Font("微软雅黑", 12F);
            b15.Location = new Point(469, 3);
            b15.Name = "b15";
            b15.RadiusSides = UICornerRadiusSides.None;
            b15.Size = new Size(29, 29);
            b15.Symbol = 0;
            b15.TabIndex = 15;
            b15.Text = "0";
            //
            // b14
            //
            b14.Cursor = Cursors.Hand;
            b14.Font = new Font("微软雅黑", 12F);
            b14.Location = new Point(441, 3);
            b14.Name = "b14";
            b14.RadiusSides = UICornerRadiusSides.None;
            b14.Size = new Size(29, 29);
            b14.Symbol = 0;
            b14.TabIndex = 14;
            b14.Text = "0";
            //
            // b13
            //
            b13.Cursor = Cursors.Hand;
            b13.Font = new Font("微软雅黑", 12F);
            b13.Location = new Point(413, 3);
            b13.Name = "b13";
            b13.RadiusSides = UICornerRadiusSides.None;
            b13.Size = new Size(29, 29);
            b13.Symbol = 0;
            b13.TabIndex = 13;
            b13.Text = "0";
            //
            // b12
            //
            b12.Cursor = Cursors.Hand;
            b12.Font = new Font("微软雅黑", 12F);
            b12.Location = new Point(385, 3);
            b12.Name = "b12";
            b12.RadiusSides = UICornerRadiusSides.None;
            b12.Size = new Size(29, 29);
            b12.Symbol = 0;
            b12.TabIndex = 12;
            b12.Text = "0";
            //
            // b11
            //
            b11.Cursor = Cursors.Hand;
            b11.Font = new Font("微软雅黑", 12F);
            b11.Location = new Point(357, 3);
            b11.Name = "b11";
            b11.RadiusSides = UICornerRadiusSides.None;
            b11.Size = new Size(29, 29);
            b11.Symbol = 0;
            b11.TabIndex = 11;
            b11.Text = "0";
            //
            // b10
            //
            b10.Cursor = Cursors.Hand;
            b10.Font = new Font("微软雅黑", 12F);
            b10.Location = new Point(329, 3);
            b10.Name = "b10";
            b10.RadiusSides = UICornerRadiusSides.None;
            b10.Size = new Size(29, 29);
            b10.Symbol = 0;
            b10.TabIndex = 10;
            b10.Text = "0";
            //
            // b9
            //
            b9.Cursor = Cursors.Hand;
            b9.Font = new Font("微软雅黑", 12F);
            b9.Location = new Point(301, 3);
            b9.Name = "b9";
            b9.RadiusSides = UICornerRadiusSides.None;
            b9.Size = new Size(29, 29);
            b9.Symbol = 0;
            b9.TabIndex = 9;
            b9.Text = "0";
            //
            // b8
            //
            b8.Cursor = Cursors.Hand;
            b8.Font = new Font("微软雅黑", 12F);
            b8.Location = new Point(273, 3);
            b8.Name = "b8";
            b8.RadiusSides = UICornerRadiusSides.None;
            b8.Size = new Size(29, 29);
            b8.Symbol = 0;
            b8.TabIndex = 8;
            b8.Text = "0";
            //
            // b16
            //
            b16.Cursor = Cursors.Hand;
            b16.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b16.ImageAlign = ContentAlignment.MiddleRight;
            b16.Location = new Point(497, 3);
            b16.Name = "b16";
            b16.Padding = new Padding(5, 0, 5, 0);
            b16.RadiusSides = UICornerRadiusSides.RightTop | UICornerRadiusSides.RightBottom;
            b16.Size = new Size(75, 29);
            b16.Symbol = 61701;
            b16.TabIndex = 16;
            b16.Tag = "1";
            b16.TagString = ">";
            b16.Text = "下一页";
            b16.TextAlign = ContentAlignment.MiddleLeft;
            b16.LocationChanged += b16_LocationChanged;
            //
            // p1
            //
            p1.Controls.Add(btnSelect);
            p1.Controls.Add(uiLabel2);
            p1.Controls.Add(edtPage);
            p1.Controls.Add(uiLabel1);
            p1.Font = new Font("微软雅黑", 12F);
            p1.Location = new Point(579, 3);
            p1.Margin = new Padding(4, 5, 4, 5);
            p1.Name = "p1";
            p1.RectSides = ToolStripStatusLabelBorderSides.None;
            p1.Size = new Size(191, 29);
            p1.TabIndex = 17;
            p1.Text = null;
            p1.LocationChanged += p1_LocationChanged;
            //
            // btnSelect
            //
            btnSelect.Cursor = Cursors.Hand;
            btnSelect.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnSelect.Location = new Point(127, 0);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(61, 29);
            btnSelect.Symbol = 0;
            btnSelect.TabIndex = 3;
            btnSelect.Text = "确定";
            btnSelect.Click += btnSelect_Click;
            //
            // uiLabel2
            //
            uiLabel2.AutoSize = true;
            uiLabel2.BackColor = Color.Transparent;
            uiLabel2.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLabel2.Location = new Point(100, 4);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(23, 20);
            uiLabel2.TabIndex = 2;
            uiLabel2.Text = "页";
            uiLabel2.TextAlign = ContentAlignment.MiddleLeft;
            //
            // edtPage
            //
            edtPage.Cursor = Cursors.IBeam;
            edtPage.DoubleValue = 10D;
            edtPage.FillColor = Color.White;
            edtPage.Font = new Font("微软雅黑", 12F);
            edtPage.HasMinimum = true;
            edtPage.IntValue = 10;
            edtPage.Location = new Point(43, 0);
            edtPage.Margin = new Padding(4, 5, 4, 5);
            edtPage.Maximum = 2147483647D;
            edtPage.Minimum = 1D;
            edtPage.Name = "edtPage";
            edtPage.Padding = new Padding(5);
            edtPage.Size = new Size(53, 29);
            edtPage.TabIndex = 1;
            edtPage.Text = "10";
            edtPage.TextAlignment = ContentAlignment.BottomCenter;
            edtPage.Type = UITextBox.UIEditType.Integer;
            //
            // uiLabel1
            //
            uiLabel1.AutoSize = true;
            uiLabel1.BackColor = Color.Transparent;
            uiLabel1.Font = new Font("微软雅黑", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLabel1.Location = new Point(3, 4);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(37, 20);
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "到第";
            uiLabel1.TextAlign = ContentAlignment.MiddleLeft;
            //
            // cb1
            //
            cb1.DropDownStyle = UIDropDownStyle.DropDownList;
            cb1.FillColor = Color.White;
            cb1.Font = new Font("微软雅黑", 12F);
            cb1.Items.AddRange(new object[]
            {
                "20条/页",
                "50条/页",
                "100条/页",
                "200条/页"
            });
            cb1.Location = new Point(906, 3);
            cb1.Margin = new Padding(4, 5, 4, 5);
            cb1.MinimumSize = new Size(63, 0);
            cb1.Name = "cb1";
            cb1.Padding = new Padding(0, 0, 30, 0);
            cb1.Size = new Size(103, 29);
            cb1.TabIndex = 19;
            cb1.TextAlignment = ContentAlignment.MiddleLeft;
            cb1.Visible = false;
            //
            // UIPagination
            //
            Controls.Add(cb1);
            Controls.Add(p1);
            Controls.Add(b16);
            Controls.Add(b15);
            Controls.Add(b14);
            Controls.Add(b13);
            Controls.Add(b12);
            Controls.Add(b11);
            Controls.Add(b10);
            Controls.Add(b9);
            Controls.Add(b8);
            Controls.Add(b7);
            Controls.Add(b6);
            Controls.Add(b5);
            Controls.Add(b4);
            Controls.Add(b3);
            Controls.Add(b2);
            Controls.Add(b1);
            Controls.Add(b0);
            Name = "UIPagination";
            RectSides = ToolStripStatusLabelBorderSides.None;
            Size = new Size(1100, 35);
            p1.ResumeLayout(false);
            p1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion InitializeComponent

        private void SetShowButton(int buttonIdx, int pageIdx, int activeIdx)
        {
            buttons[buttonIdx].Symbol = 0;
            buttons[buttonIdx].Text = pageIdx.ToString();
            buttons[buttonIdx].Tag = pageIdx;
            buttons[buttonIdx].Visible = true;
            buttons[buttonIdx].TagString = "";
            buttons[buttonIdx].Selected = activeIdx == pageIdx;
            if (buttons[buttonIdx].Selected) buttons[buttonIdx].BringToFront();
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
            for (var i = beginIdx; i < 16; i++) buttons[i].Visible = false;
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
                b16.Left = b1.Right - 1;
                return;
            }

            if (PageCount <= PagerCount + 2)
            {
                for (var i = 1; i <= PageCount; i++) SetShowButton(i, i, activePage);

                b16.Left = buttons[PageCount].Right - 1;
                SetHideButton(PageCount + 1);
            }
            else
            {
                //左
                var leftShow = PagerCount / 2 + 1 + 2;
                if (activePage <= leftShow)
                {
                    for (var i = 1; i <= leftShow; i++) SetShowButton(i, i, activePage);

                    SetShowButton(leftShow + 1, PagerCount - 2, ">>");
                    SetShowButton(leftShow + 2, PageCount, activePage);
                    SetHideButton(leftShow + 3);
                    b16.Left = buttons[leftShow + 2].Right - 1;
                    return;
                }

                //右
                var rightShow = PageCount - (PagerCount / 2 + 1) - 1;
                if (activePage >= rightShow)
                {
                    SetShowButton(1, 1, activePage);
                    SetShowButton(2, 2 - PagerCount, "<<");

                    var idx = 3;
                    for (var i = rightShow; i <= PageCount; i++)
                    {
                        SetShowButton(idx, i, activePage);
                        idx++;
                    }

                    b16.Left = buttons[idx - 1].Right - 1;
                    SetHideButton(idx);
                    return;
                }

                //中
                SetShowButton(1, 1, activePage);
                SetShowButton(2, 2 - PagerCount, "<<");
                var cIdx = 3;
                var sIdx = (PagerCount - 2) / 2;
                for (var i = cIdx; i <= PagerCount; i++)
                {
                    SetShowButton(cIdx, activePage - sIdx + (i - 3), activePage);
                    cIdx++;
                }

                SetShowButton(cIdx, PagerCount - 2, ">>");
                SetShowButton(cIdx + 1, PageCount, activePage);
                b16.Left = buttons[cIdx + 1].Right - 1;
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

            btnSelect.FillColor = uiColor.PlainColor;
            btnSelect.ForeColor = uiColor.RectColor;
            btnSelect.FillSelectedColor = uiColor.ButtonFillColor;
        }

        private void b16_LocationChanged(object sender, EventArgs e)
        {
            p1.Left = b16.Right + 3;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ActivePage = edtPage.IntValue;
        }

        private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember)
        {
            var dataSourceChanged = dataSource != newDataSource;

            if (inSetDataConnection) return;

            try
            {
                if (dataSourceChanged)
                {
                    inSetDataConnection = true;
                    CurrencyManager newDataManager = null;
                    if (newDataSource != null && newDataSource != Convert.DBNull)
                        newDataManager = (CurrencyManager)BindingContext[newDataSource, newDisplayMember.BindingPath];

                    dataManager = newDataManager;
                }
            }
            finally
            {
                inSetDataConnection = false;
            }
        }

        public void DataBind()
        {
            if (dataSource == null)
            {
                PageChanged?.Invoke(this, dataSource, 1, 0);
                return;
            }

            var objects = new List<object>();
            var iBegin = PageSize * (ActivePage - 1);
            for (var i = iBegin; i < iBegin + PageSize; i++)
                if (i < TotalCount)
                    objects.Add(dataManager.List[i]);

            PageDataSource = objects;
            PageChanged?.Invoke(this, objects, activePage, objects.Count);
        }

        public event OnPageChangeEventHandler PageChanged;

        private void p1_LocationChanged(object sender, EventArgs e)
        {
            cb1.Left = p1.Right;
        }
    }
}