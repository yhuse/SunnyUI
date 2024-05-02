/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIMiniPagination.cs
 * 文件说明: 分页
 * 当前版本: V3.1
 * 创建日期: 2023-02-19
 *
 * 2023-02-19: V3.3.2 新增迷你分页控件，只有分页按钮，无其他
 * 2023-06-14: V3.3.9 按钮图标位置修正
 * 2023-06-27: V3.3.9 内置按钮关联值由Tag改为TagString
 * 2023-08-30: V3.4.2 左右跳转按钮的文字换成字体图标
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
    public class UIMiniPagination : UIPanel
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

        private readonly ConcurrentDictionary<int, UISymbolButton> buttons = new ConcurrentDictionary<int, UISymbolButton>();
        private readonly ConcurrentDictionary<UISymbolButton, int> buttonTags = new ConcurrentDictionary<UISymbolButton, int>();
        private CurrencyManager dataManager;

        private object dataSource;

        private bool inSetDataConnection;

        /// <summary>
        /// 总页数
        /// </summary>
        [Browsable(false)]
        [Description("总页数"), Category("SunnyUI")]
        public int PageCount { get; private set; }

        private int pagerCount = 9;

        private int pageSize = 20;

        private int totalCount = 1000;

        public UIMiniPagination()
        {
            InitializeComponent();

            SetStyleFlags(true, false);

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
                buttons[i].Size = new System.Drawing.Size(32, 32);
            }

            buttonTags.TryAdd(b0, -1);
            buttonTags.TryAdd(b16, 1);
            for (var i = 0; i < 17; i++)
            {
                if (buttonTags.NotContainsKey(buttons[i]))
                    buttonTags.TryAdd(buttons[i], 0);
            }

            TotalCount = 1000;
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (DefaultFontSize < 0)
            {
                foreach (var item in this.GetControls<UISymbolButton>(true))
                    item.Font = Font;
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            foreach (var button in buttons.Values)
            {
                button.SetStyleColor(uiColor);
                button.FillColor = uiColor.PlainColor;
                button.SymbolColor = button.ForeColor = uiColor.PaginationForeColor;
                button.FillSelectedColor = uiColor.ButtonFillColor;
                button.BackColor = Color.Transparent;
            }
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            foreach (var item in this.GetControls<UISymbolButton>(true)) item.SetDPIScale();
            foreach (var item in this.GetControls<UITextBox>(true)) item.SetDPIScale();
            foreach (var item in this.GetControls<UIComboBox>(true)) item.SetDPIScale();
            foreach (var item in this.GetControls<UILabel>(true)) item.SetDPIScale();
        }

        private int buttonInterval = 8;

        [DefaultValue(8)]
        [Description("按钮间隔")]
        [Category("SunnyUI")]
        public int ButtonInterval
        {
            get => buttonInterval;
            set
            {
                buttonInterval = Math.Max(0, value);
                buttonInterval = Math.Min(5, value);
                SetShowButtons();
            }
        }

        /// <summary>
        ///     总条目数
        /// </summary>
        [Description("总条目数")]
        [Category("SunnyUI")]
        public int TotalCount
        {
            get => totalCount;
            set
            {
                totalCount = Math.Max(0, value);
                SetShowButtons();
                if (ActivePage > PageCount)
                    ActivePage = 1;
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
        [DefaultValue(9)]
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
                activePage = Math.Max(1, value);
                //edtPage.IntValue = activePage;
                SetShowButtons();
                DataBind();
            }
        }

        public event EventHandler DataSourceChanged;

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        [Description("数据源"), Category("SunnyUI")]
        public object DataSource
        {
            get => dataSource;
            set
            {
                if (value != null)
                {
                    if (!(value is DataTable || value is IList))
                    {
                        throw new Exception(UILocalize.GridDataSourceException);
                    }
                }

                SetDataConnection(value, new BindingMemberInfo(""));
                dataSource = value;
                activePage = 1;
                TotalCount = dataManager?.List.Count ?? 0;
                DataSourceChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(false)]
        public object PageDataSource { get; private set; }

        private void UIDataGridPage_Click(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
            btn.BringToFront();
            if (btn.TagString.IsValid())
                ActivePage += buttonTags[btn];
            else
                ActivePage = buttonTags[btn];
        }

        private void UIDataGridPage_MouseLeave(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
            if (btn.TagString == "<<" || btn.TagString == ">>")
            {
                btn.Symbol = 361761;
                btn.Text = "";
            }
        }

        private void UIDataGridPage_MouseEnter(object sender, EventArgs e)
        {
            var btn = (UISymbolButton)sender;
            if (btn.TagString == "<<")
            {
                btn.Symbol = 361696;
                btn.Text = "";
            }

            if (btn.TagString == ">>")
            {
                btn.Symbol = 361697;
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
            this.SuspendLayout();
            // 
            // b0
            // 
            this.b0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b0.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b0.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.b0.Location = new System.Drawing.Point(3, 4);
            this.b0.MinimumSize = new System.Drawing.Size(1, 1);
            this.b0.Name = "b0";
            this.b0.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.b0.Radius = 2;
            this.b0.Size = new System.Drawing.Size(32, 32);
            this.b0.Symbol = 61700;
            this.b0.TabIndex = 0;
            this.b0.TagString = "<";
            this.b0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.b0.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b1
            // 
            this.b1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b1.Location = new System.Drawing.Point(81, 4);
            this.b1.MinimumSize = new System.Drawing.Size(1, 1);
            this.b1.Name = "b1";
            this.b1.Radius = 2;
            this.b1.Size = new System.Drawing.Size(29, 29);
            this.b1.Symbol = 0;
            this.b1.TabIndex = 1;
            this.b1.Text = "0";
            this.b1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b3
            // 
            this.b3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b3.Location = new System.Drawing.Point(145, 4);
            this.b3.MinimumSize = new System.Drawing.Size(1, 1);
            this.b3.Name = "b3";
            this.b3.Radius = 2;
            this.b3.Size = new System.Drawing.Size(29, 29);
            this.b3.Symbol = 0;
            this.b3.TabIndex = 3;
            this.b3.Text = "0";
            this.b3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b2
            // 
            this.b2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b2.Location = new System.Drawing.Point(113, 4);
            this.b2.MinimumSize = new System.Drawing.Size(1, 1);
            this.b2.Name = "b2";
            this.b2.Radius = 2;
            this.b2.Size = new System.Drawing.Size(29, 29);
            this.b2.Symbol = 0;
            this.b2.TabIndex = 2;
            this.b2.Text = "0";
            this.b2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b7
            // 
            this.b7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b7.Location = new System.Drawing.Point(273, 4);
            this.b7.MinimumSize = new System.Drawing.Size(1, 1);
            this.b7.Name = "b7";
            this.b7.Radius = 2;
            this.b7.Size = new System.Drawing.Size(29, 29);
            this.b7.Symbol = 0;
            this.b7.TabIndex = 7;
            this.b7.Text = "0";
            this.b7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b6
            // 
            this.b6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b6.Location = new System.Drawing.Point(241, 4);
            this.b6.MinimumSize = new System.Drawing.Size(1, 1);
            this.b6.Name = "b6";
            this.b6.Radius = 2;
            this.b6.Size = new System.Drawing.Size(29, 29);
            this.b6.Symbol = 0;
            this.b6.TabIndex = 6;
            this.b6.Text = "0";
            this.b6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b5
            // 
            this.b5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b5.Location = new System.Drawing.Point(209, 4);
            this.b5.MinimumSize = new System.Drawing.Size(1, 1);
            this.b5.Name = "b5";
            this.b5.Radius = 2;
            this.b5.Size = new System.Drawing.Size(29, 29);
            this.b5.Symbol = 0;
            this.b5.TabIndex = 5;
            this.b5.Text = "0";
            this.b5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b4
            // 
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b4.Location = new System.Drawing.Point(177, 4);
            this.b4.MinimumSize = new System.Drawing.Size(1, 1);
            this.b4.Name = "b4";
            this.b4.Radius = 2;
            this.b4.Size = new System.Drawing.Size(29, 29);
            this.b4.Symbol = 0;
            this.b4.TabIndex = 4;
            this.b4.Text = "0";
            this.b4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b15
            // 
            this.b15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b15.Location = new System.Drawing.Point(529, 4);
            this.b15.MinimumSize = new System.Drawing.Size(1, 1);
            this.b15.Name = "b15";
            this.b15.Radius = 2;
            this.b15.Size = new System.Drawing.Size(29, 29);
            this.b15.Symbol = 0;
            this.b15.TabIndex = 15;
            this.b15.Text = "0";
            this.b15.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b14
            // 
            this.b14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b14.Location = new System.Drawing.Point(497, 4);
            this.b14.MinimumSize = new System.Drawing.Size(1, 1);
            this.b14.Name = "b14";
            this.b14.Radius = 2;
            this.b14.Size = new System.Drawing.Size(29, 29);
            this.b14.Symbol = 0;
            this.b14.TabIndex = 14;
            this.b14.Text = "0";
            this.b14.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b13
            // 
            this.b13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b13.Location = new System.Drawing.Point(465, 4);
            this.b13.MinimumSize = new System.Drawing.Size(1, 1);
            this.b13.Name = "b13";
            this.b13.Radius = 2;
            this.b13.Size = new System.Drawing.Size(29, 29);
            this.b13.Symbol = 0;
            this.b13.TabIndex = 13;
            this.b13.Text = "0";
            this.b13.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b12
            // 
            this.b12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b12.Location = new System.Drawing.Point(433, 4);
            this.b12.MinimumSize = new System.Drawing.Size(1, 1);
            this.b12.Name = "b12";
            this.b12.Radius = 2;
            this.b12.Size = new System.Drawing.Size(29, 29);
            this.b12.Symbol = 0;
            this.b12.TabIndex = 12;
            this.b12.Text = "0";
            this.b12.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b11
            // 
            this.b11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b11.Location = new System.Drawing.Point(401, 4);
            this.b11.MinimumSize = new System.Drawing.Size(1, 1);
            this.b11.Name = "b11";
            this.b11.Radius = 2;
            this.b11.Size = new System.Drawing.Size(29, 29);
            this.b11.Symbol = 0;
            this.b11.TabIndex = 11;
            this.b11.Text = "0";
            this.b11.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b10
            // 
            this.b10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b10.Location = new System.Drawing.Point(369, 4);
            this.b10.MinimumSize = new System.Drawing.Size(1, 1);
            this.b10.Name = "b10";
            this.b10.Radius = 2;
            this.b10.Size = new System.Drawing.Size(29, 29);
            this.b10.Symbol = 0;
            this.b10.TabIndex = 10;
            this.b10.Text = "0";
            this.b10.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b9
            // 
            this.b9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b9.Location = new System.Drawing.Point(337, 4);
            this.b9.MinimumSize = new System.Drawing.Size(1, 1);
            this.b9.Name = "b9";
            this.b9.Radius = 2;
            this.b9.Size = new System.Drawing.Size(29, 29);
            this.b9.Symbol = 0;
            this.b9.TabIndex = 9;
            this.b9.Text = "0";
            this.b9.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b8
            // 
            this.b8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b8.Location = new System.Drawing.Point(305, 4);
            this.b8.MinimumSize = new System.Drawing.Size(1, 1);
            this.b8.Name = "b8";
            this.b8.Radius = 2;
            this.b8.Size = new System.Drawing.Size(29, 29);
            this.b8.Symbol = 0;
            this.b8.TabIndex = 8;
            this.b8.Text = "0";
            this.b8.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // b16
            // 
            this.b16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b16.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.b16.Location = new System.Drawing.Point(561, 4);
            this.b16.MinimumSize = new System.Drawing.Size(1, 1);
            this.b16.Name = "b16";
            this.b16.Padding = new System.Windows.Forms.Padding(6, 0, 5, 0);
            this.b16.Radius = 2;
            this.b16.Size = new System.Drawing.Size(32, 32);
            this.b16.Symbol = 61701;
            this.b16.TabIndex = 16;
            this.b16.TagString = ">";
            this.b16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.b16.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.b16.LocationChanged += new System.EventHandler(this.b16_LocationChanged);
            // 
            // UIMiniPagination
            // 
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
            this.Name = "UIMiniPagination";
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Size = new System.Drawing.Size(641, 40);
            this.ResumeLayout(false);

        }

        #endregion InitializeComponent

        private void SetShowButton(int buttonIdx, int pageIdx, int activeIdx)
        {
            buttons[buttonIdx].Symbol = 0;
            buttonTags[buttons[buttonIdx]] = pageIdx;
            buttons[buttonIdx].Visible = true;
            buttons[buttonIdx].TagString = "";
            buttons[buttonIdx].Selected = activeIdx == pageIdx;
            SetButtonWidth(buttons[buttonIdx], pageIdx.ToString());
            buttons[buttonIdx].Left = buttons[buttonIdx - 1].Right + buttonInterval - 1;
            if (buttons[buttonIdx].Selected) buttons[buttonIdx].BringToFront();
        }

        private void SetShowButton(int buttonIdx, int addCount, string tagString)
        {
            buttons[buttonIdx].Symbol = 361761;
            buttons[buttonIdx].Text = "";
            buttons[buttonIdx].SymbolOffset = new Point(-1, 1);
            buttonTags[buttons[buttonIdx]] = addCount;
            buttons[buttonIdx].Visible = true;
            buttons[buttonIdx].TagString = tagString;
            buttons[buttonIdx].Selected = false;
            buttons[buttonIdx].Left = buttons[buttonIdx - 1].Right + buttonInterval - 1;
        }

        private void SetButtonWidth(UISymbolButton button, string text)
        {
            if (button.Text != text) button.Text = text;
            int len = 32;
            if (button.Text.Length >= 3) len = 36;
            if (button.Text.Length >= 4) len = 44;
            if (button.Text.Length >= 4) len = 52;
            if (button.Width != len) button.Width = len;
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
            //edtPage.HasMaximum = true;
            //edtPage.Maximum = PageCount;

            if (activePage >= PageCount)
            {
                activePage = PageCount;
                b16.Enabled = false;
            }
            else
            {
                b16.Enabled = true;
            }
            if (activePage <= 1)
            {
                activePage = 1;
                b0.Enabled = false;
            }
            else
            {
                b0.Enabled = true;
            }

            b1.Left = b0.Right + buttonInterval - 1;
            //edtPage.IntValue = activePage;

            if (TotalCount == 0)
            {
                PageCount = 1;
                activePage = 1;
                SetShowButton(1, 1, 1);
                SetHideButton(2);
                b16.Left = b1.Right + buttonInterval - 1;
                Width = b16.Right + 8;
                return;
            }

            if (PageCount <= PagerCount + 2)
            {
                for (var i = 1; i <= PageCount; i++)
                    SetShowButton(i, i, activePage);

                b16.Left = buttons[PageCount].Right + buttonInterval - 1;
                Width = b16.Right + 8;
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
                    b16.Left = buttons[leftShow + 2].Right + buttonInterval - 1;
                    Width = b16.Right + 8;
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

                    b16.Left = buttons[idx - 1].Right + buttonInterval - 1;
                    Width = b16.Right + 8;
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
                b16.Left = buttons[cIdx + 1].Right + buttonInterval - 1;
                Width = b16.Right + 8;
                SetHideButton(cIdx + 2);
            }
        }

        private void b16_LocationChanged(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //ActivePage = edtPage.IntValue;
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
                PageChanged?.Invoke(this, dataSource, activePage, PageSize);
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
    }
}
