/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIDataGridView.cs
 * 文件说明: 表格
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 功能增强、美化
 * 2020-07-15: V2.2.6 更改默认配置为原生
 * 2020-07-18: V2.2.6 重绘水平滚动条
 * 2020-08-22: V2.2.7 更新了水平和垂直滚动条的显示，优化滚动效果
 * 2020-08-28: V2.2.7 调整水平滚动条
 * 2021-03-25: V3.0.2 修改垂直滚动条和原版一致，并增加翻页方式滚动
 * 2021-04-01: V3.0.2 编辑输入时，用Enter键代替Tab键跳到下一个单元格
 * 2021-04-29: V3.0.3 设置数据行头部颜色
 * 2021-05-22: V3.0.4 增加了一个RowHeight，默认23
 * 2021-06-27: V3.0.4 自定义单元格颜色
 * 2022-01-21: V3.1.0 更新单选时选中值SelectedIndex值
 * 2022-04-16: V3.1.3 增加滚动条的颜色设置
 * 2022-04-26: V3.1.8 解决原生控件DataSource绑定List，并且List为空，出现”索引-1没有值“错误
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIDataGridView : DataGridView, IStyleInterface, IZoomScale
    {
        private readonly UIScrollBar VBar = new UIScrollBar();
        private readonly UIHorScrollBarEx HBar = new UIHorScrollBarEx();

        public UIDataGridView()
        {
            BackgroundColor = UIColor.White;
            GridColor = UIColor.Blue;
            base.Font = UIFontColor.Font();
            base.DoubleBuffered = true;

            VBar.Parent = this;
            VBar.Visible = false;
            HBar.FillColor = VBar.FillColor = UIColor.LightBlue;
            VBar.ForeColor = UIColor.Blue;
            VBar.StyleCustomMode = true;
            VBar.ValueChanged += VBarValueChanged;
            VBar.ShowLeftLine = true;

            HBar.Parent = this;
            HBar.Visible = false;
            HBar.ForeColor = UIColor.Blue;
            HBar.StyleCustomMode = true;
            HBar.ValueChanged += HBar_ValueChanged;

            SetBarPosition();

            //支持自定义标题行风格
            EnableHeadersVisualStyles = false;

            //标题行风格
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.BackColor = UIColor.Blue;
            ColumnHeadersDefaultCellStyle.ForeColor = UIColor.White;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            ColumnHeadersDefaultCellStyle.Font = UIFontColor.Font();

            //行头部颜色
            RowHeadersDefaultCellStyle.BackColor = UIColor.LightBlue;
            RowHeadersDefaultCellStyle.ForeColor = UIFontColor.Primary;
            RowHeadersDefaultCellStyle.SelectionBackColor = UIColor.Blue;
            RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            RowHeadersDefaultCellStyle.Font = UIFontColor.Font();

            RowsDefaultCellStyle.Font = UIFontColor.Font();
            DefaultCellStyle.Font = UIFontColor.Font();

            //标题行行高，与OnColumnAdded事件配合
            ColumnHeadersHeight = 32;

            //设置奇偶数行颜色
            StripeEvenColor = UIColor.White;
            StripeOddColor = UIColor.LightBlue;

            VerticalScrollBar.ValueChanged += VerticalScrollBar_ValueChanged;
            HorizontalScrollBar.ValueChanged += HorizontalScrollBar_ValueChanged;
            VerticalScrollBar.VisibleChanged += VerticalScrollBar_VisibleChanged;
            HorizontalScrollBar.VisibleChanged += HorizontalScrollBar_VisibleChanged;
        }

        [
            DefaultValue(null),
            RefreshProperties(RefreshProperties.Repaint),
            AttributeProvider(typeof(IListSource)),
            Description("提示 DataGridView 控件的数据源。")
        ]
        public new object DataSource
        {
            get => base.DataSource;
            set
            {
                //解决原生控件DataSource绑定List，并且List为空，出现”索引-1没有值“错误。
                if (value is IList list && list.Count == 0) return;
                base.DataSource = value;
            }
        }

        [DefaultValue(false), Category("SunnyUI"), Description("禁止控件跟随窗体缩放")]
        public bool ZoomScaleDisabled { get; set; }

        [Browsable(false)]
        public Rectangle ZoomScaleRect { get; set; }

        public virtual void SetZoomScale(float scale)
        {

        }

        [Browsable(false)]
        public bool IsScaled { get; private set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                if (ColumnHeadersDefaultCellStyle.Font != null)
                    ColumnHeadersDefaultCellStyle.Font = ColumnHeadersDefaultCellStyle.Font.DPIScaleFont();
                if (RowHeadersDefaultCellStyle.Font != null)
                    RowHeadersDefaultCellStyle.Font = RowHeadersDefaultCellStyle.Font.DPIScaleFont();
                if (DefaultCellStyle.Font != null)
                    DefaultCellStyle.Font = DefaultCellStyle.Font.DPIScaleFont();
                if (RowsDefaultCellStyle.Font != null)
                    RowsDefaultCellStyle.Font = RowsDefaultCellStyle.Font.DPIScaleFont();
                IsScaled = true;
            }
        }

        private readonly Dictionary<string, CellStyle> CellStyles = new Dictionary<string, CellStyle>();

        public class CellStyle
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public Color BackColor { get; set; }

            public Color ForeColor { get; set; }

            public CellStyle(int row, int col, Color backColor, Color foreColor)
            {
                Row = row;
                Col = col;
                BackColor = backColor;
                ForeColor = foreColor;
            }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (CellStyles.Count > 0 && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                object obj = e.Value;
                if (obj == null) return;

                string key = e.RowIndex + "_" + e.ColumnIndex;
                if (CellStyles.ContainsKey(key))
                {
                    e.CellStyle.ForeColor = CellStyles[key].ForeColor;
                    e.CellStyle.BackColor = CellStyles[key].BackColor;
                    e.CellStyle.SelectionForeColor = CellStyles[key].ForeColor;
                    e.CellStyle.SelectionBackColor = CellStyles[key].BackColor;
                }
            }
        }

        public void SetCellStyle(int row, int col, Color backColor, Color foreColor)
        {
            SetCellStyle(new CellStyle(row, col, backColor, foreColor));
        }

        public void SetCellStyle(CellStyle style)
        {
            string key = style.Row + "_" + style.Col;
            if (CellStyles.ContainsKey(key))
                CellStyles[key] = style;
            else
                CellStyles.Add(key, style);
        }

        public void ClearCellStyles()
        {
            CellStyles.Clear();
        }

        public void ClearCellStyle(int row, int col)
        {
            string key = row + "_" + col;
            if (CellStyles.ContainsKey(key))
            {
                CellStyles.Remove(key);
            }
        }

        [Description("行高"), Category("SunnyUI")]
        [DefaultValue(23)]
        public int RowHeight
        {
            get => RowTemplate.Height;
            set
            {
                if (value > 23)
                {
                    RowTemplate.Height = Math.Max(23, value);
                    RowTemplate.MinimumHeight = Math.Max(23, value);
                    AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                }
            }
        }

        /*
        private bool showRowIndex;

        [Description("显示行号"), Category("SunnyUI")]
        [DefaultValue(false)]
        public bool ShowRowIndex
        {
            get => showRowIndex;
            set
            {
                showRowIndex = value;
                if (value) RowHeadersVisible = true;
                Invalidate();
            }
        }

        protected override void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
        {
            base.OnRowStateChanged(rowIndex, e);
            if (ShowRowIndex) e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }
        */

        private void HorizontalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            SetScrollInfo();
        }

        public void Init()
        {
            //自动生成行
            AutoGenerateColumns = false;

            //列占满行
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //禁止调整数据行行高
            AllowUserToResizeRows = false;

            //允许调整标题行行宽
            AllowUserToResizeColumns = true;

            //禁用最后一行空白，自动新增行
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;

            //不显示表格线
            CellBorderStyle = DataGridViewCellBorderStyle.None;

            //禁止行多选
            MultiSelect = false;

            //不显示数据行标题
            RowHeadersVisible = false;

            //禁止只读
            //ReadOnly = false;

            //行选
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void VerticalScrollBar_ValueChanged(object sender, EventArgs e)
        {
            VBar.Value = FirstDisplayedScrollingRowIndex;
            VerticalScrollBarChanged?.Invoke(this, e);
        }

        private void VBarValueChanged(object sender, EventArgs e)
        {
            FirstDisplayedScrollingRowIndex = VBar.Value;
        }

        private void HorizontalScrollBar_ValueChanged(object sender, EventArgs e)
        {
            HBar.Value = HorizontalScrollBar.Value;
        }

        private void HBar_ValueChanged(object sender, EventArgs e)
        {
            HorizontalScrollBar.Value = HBar.Value;
            HorizontalScrollingOffset = HBar.Value;
            HorizontalScrollBarChanged?.Invoke(this, e);
        }

        public event EventHandler HorizontalScrollBarChanged;

        public event EventHandler VerticalScrollBarChanged;

        public void SetScrollInfo()
        {
            if (VBar == null || HBar == null)
            {
                return;
            }

            if (RowCount > DisplayedRowCount(false))
            {
                VBar.Maximum = RowCount - DisplayedRowCount(false);
                VBar.Value = FirstDisplayedScrollingRowIndex;
                VBar.Visible = true;
            }
            else
            {
                VBar.Visible = false;
            }

            if (HorizontalScrollBar.Visible)
            {
                HBar.Maximum = HorizontalScrollBar.Maximum;
                HBar.Value = HorizontalScrollBar.Value;
                HBar.BoundsWidth = HorizontalScrollBar.LargeChange;
                HBar.LargeChange = HorizontalScrollBar.LargeChange;//.Maximum / VisibleColumnCount();
                HBar.Visible = true;
            }
            else
            {
                HBar.Visible = false;
            }

            SetBarPosition();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ShowRect)
            {
                Color color = RectColor;
                color = Enabled ? color : UIDisableColor.Fill;
                e.Graphics.DrawRectangle(color, new Rectangle(0, 0, Width - 1, Height - 1));
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (VBar.Visible && ScrollMode == UIDataGridViewScrollMode.Page)
            {
                if (e.Delta > 10)
                {
                    var lineCount = Rows.GetLastRow(DataGridViewElementStates.Displayed) - FirstDisplayedScrollingRowIndex;
                    VBar.SetValue(VBar.Value - lineCount + 3);
                }
                else if (e.Delta < -10)
                {
                    var lineCount = FirstDisplayedScrollingRowIndex - Rows.GetLastRow(DataGridViewElementStates.Displayed);
                    VBar.SetValue(VBar.Value - lineCount - 3);
                }
            }
        }

        [Description("垂直滚动条滚动方式"), Category("SunnyUI")]
        [DefaultValue(UIDataGridViewScrollMode.Normal)]
        public UIDataGridViewScrollMode ScrollMode { get; set; } = UIDataGridViewScrollMode.Normal;

        public enum UIDataGridViewScrollMode
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal,
            /// <summary>
            /// 翻页
            /// </summary>
            Page
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            SetScrollInfo();
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            SetScrollInfo();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollInfo();
            SetBarPosition();
        }

        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            base.OnColumnStateChanged(e);
            SetScrollInfo();
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            base.OnColumnRemoved(e);
            SetScrollInfo();
        }

        private void SetBarPosition()
        {
            if (VBar == null || HBar == null)
            {
                return;
            }

            if (ShowRect)
            {
                VBar.Left = Width - ScrollBarInfo.VerticalScrollBarWidth() - 2;
                VBar.Top = 1;
                VBar.Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;
                VBar.Height = Height - 2;
                VBar.BringToFront();

                HBar.Left = 2;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;
                HBar.Width = Width - (VBar.Visible ? VBar.Width : 0) - 2;
                HBar.Top = Height - HBar.Height - 2;
                HBar.BringToFront();
            }
            else
            {
                VBar.Left = Width - ScrollBarInfo.VerticalScrollBarWidth() - 1;
                VBar.Top = 0;
                VBar.Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;
                VBar.Height = Height;
                VBar.BringToFront();

                HBar.Left = 0;
                HBar.Height = ScrollBarInfo.HorizontalScrollBarHeight() + 1;
                HBar.Width = Width - (VBar.Visible ? VBar.Width : 0);
                HBar.Top = Height - HBar.Height;
                HBar.BringToFront();
            }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            //设置可调整标题行行高
            if (ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.AutoSize)
            {
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }

            SetScrollInfo();
        }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("偶数行显示颜色"), Category("SunnyUI")]
        public Color StripeEvenColor
        {
            get => RowsDefaultCellStyle.BackColor;
            set
            {
                RowsDefaultCellStyle.BackColor = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "243, 249, 255")]
        [Description("奇数行显示颜色"), Category("SunnyUI")]
        public Color StripeOddColor
        {
            get => AlternatingRowsDefaultCellStyle.BackColor;
            set
            {
                AlternatingRowsDefaultCellStyle.BackColor = value;
                Invalidate();
            }
        }

        public void SetStyle(UIStyle style)
        {
            if (!style.IsCustom())
            {
                SetStyleColor(style.Colors());
                Invalidate();
            }

            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            BackgroundColor = uiColor.PlainColor;

            //列头部颜色
            ColumnHeadersDefaultCellStyle.BackColor = uiColor.GridTitleColor;
            ColumnHeadersDefaultCellStyle.ForeColor = uiColor.GridTitleForeColor;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = uiColor.GridTitleColor;

            //行头部颜色
            RowHeadersDefaultCellStyle.BackColor = uiColor.PlainColor;
            RowHeadersDefaultCellStyle.ForeColor = uiColor.GridForeColor;
            RowHeadersDefaultCellStyle.SelectionBackColor = uiColor.RectColor;
            RowHeadersDefaultCellStyle.SelectionForeColor = uiColor.GridForeColor;

            //数据单元格选中颜色
            DefaultCellStyle.SelectionBackColor = uiColor.GridSelectedColor;
            DefaultCellStyle.SelectionForeColor = uiColor.GridSelectedForeColor;
            DefaultCellStyle.BackColor = uiColor.GridStripeEvenColor;
            DefaultCellStyle.ForeColor = uiColor.GridForeColor;

            //数据行选中颜色            
            RowsDefaultCellStyle.SelectionBackColor = uiColor.GridSelectedColor;
            RowsDefaultCellStyle.SelectionForeColor = uiColor.GridSelectedForeColor;
            RowsDefaultCellStyle.ForeColor = uiColor.GridForeColor;

            GridColor = uiColor.GridLineColor;
            RectColor = uiColor.RectColor;
            RowsDefaultCellStyle.BackColor = uiColor.GridStripeEvenColor;
            AlternatingRowsDefaultCellStyle.BackColor = uiColor.GridStripeOddColor;

            StripeEvenColor = uiColor.GridStripeEvenColor;
            StripeOddColor = uiColor.GridStripeOddColor;

            HBar.FillColor = VBar.FillColor = uiColor.GridBarFillColor;
            HBar.ForeColor = VBar.ForeColor = uiColor.GridBarForeColor;
            VBar.RectColor = uiColor.RectColor;

            if (HBar != null)
            {
                HBar.ForeColor = uiColor.PrimaryColor;
                HBar.HoverColor = uiColor.ButtonFillHoverColor;
                HBar.PressColor = uiColor.ButtonFillPressColor;
                HBar.FillColor = uiColor.GridBarFillColor;
                scrollBarColor = uiColor.PrimaryColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }

            if (VBar != null)
            {
                VBar.ForeColor = uiColor.PrimaryColor;
                VBar.HoverColor = uiColor.ButtonFillHoverColor;
                VBar.PressColor = uiColor.ButtonFillPressColor;
                VBar.FillColor = uiColor.GridBarFillColor;
                scrollBarColor = uiColor.PrimaryColor;
                scrollBarBackColor = uiColor.GridBarFillColor;
            }
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version => UIGlobal.Version;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ShowRect
        {
            get => BorderStyle == BorderStyle.FixedSingle;
            set
            {
                BorderStyle = value ? BorderStyle.FixedSingle : BorderStyle.None;
                Invalidate();
            }
        }

        /// <summary>
        /// 是否显示表格线
        /// </summary>
        [Description("是否显示表格线"), Category("SunnyUI")]
        [DefaultValue(true)]
        public bool ShowGridLine
        {
            get => CellBorderStyle == DataGridViewCellBorderStyle.Single;
            set
            {
                if (value && CellBorderStyle != DataGridViewCellBorderStyle.Single)
                    CellBorderStyle = DataGridViewCellBorderStyle.Single;
                VBar.ShowLeftLine = CellBorderStyle == DataGridViewCellBorderStyle.Single;
            }
        }

        private Color _rectColor = UIColor.Blue;

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("边框颜色"), Category("SunnyUI")]
        public Color RectColor
        {
            get => _rectColor;
            set
            {
                if (_rectColor != value)
                {
                    _rectColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public int SelectedIndex
        {
            get
            {
                return CurrentRow != null ? CurrentRow.Index : -1;
            }
            set
            {
                //BindingContext[DataSource].Position = value;
                if (Rows.Count == 0)
                {
                    return;
                }

                if (value >= 0 && value < Rows.Count)
                {
                    foreach (DataGridViewRow row in SelectedRows)
                    {
                        row.Selected = false;
                    }

                    Rows[value].Selected = true;
                    FirstDisplayedScrollingRowIndex = value;

                    if (selectedIndex >= 0 && selectedIndex <= Rows.Count)
                        jumpIndex = selectedIndex;

                    selectedIndex = value;
                    SelectIndexChange?.Invoke(this, value);
                }
            }
        }

        private int jumpIndex = -1;

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            SetScrollInfo();
        }

        public delegate void OnSelectIndexChange(object sender, int index);

        public event OnSelectIndexChange SelectIndexChange;

        protected override void OnRowEnter(DataGridViewCellEventArgs e)
        {
            base.OnRowEnter(e);

            if (e.RowIndex == jumpIndex)
            {
                jumpIndex = -1;
                return;
            }

            if (selectedIndex != e.RowIndex)
            {
                selectedIndex = e.RowIndex;
                SelectIndexChange?.Invoke(this, e.RowIndex);
            }
        }

        private int selectedIndex = -1;

        public DataGridViewColumn AddColumn(string columnName, string dataPropertyName, int fillWeight = 100, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter, bool readOnly = true)
        {
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = columnName;
            column.DataPropertyName = dataPropertyName;
            column.Name = columnName;
            column.ReadOnly = readOnly;
            column.FillWeight = fillWeight;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.DefaultCellStyle.Alignment = alignment;
            Columns.Add(column);
            return column;
        }

        public DataGridViewColumn AddCheckBoxColumn(string columnName, string dataPropertyName, int fillWeight = 100, bool readOnly = true)
        {
            DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            column.HeaderText = columnName;
            column.DataPropertyName = dataPropertyName;
            column.Name = columnName;
            column.ReadOnly = readOnly;
            column.FillWeight = fillWeight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(column);
            return column;
        }

        public DataGridViewColumn AddButtonColumn(string columnName, string dataPropertyName, int fillWeight = 100, bool readOnly = true)
        {
            DataGridViewColumn column = new DataGridViewButtonColumn();
            column.HeaderText = columnName;
            column.DataPropertyName = dataPropertyName;
            column.Name = columnName;
            column.ReadOnly = readOnly;
            column.FillWeight = fillWeight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(column);
            return column;
        }

        public void ClearRows()
        {
            if (DataSource != null)
            {
                DataSource = null;
            }

            Rows.Clear();
        }

        public void ClearColumns()
        {
            Columns.Clear();
        }

        public void ClearAll()
        {
            ClearRows();
            ClearColumns();
        }

        public int AddRow(params object[] values)
        {
            return Rows.Add(values);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (EnterAsTab)
            {
                Keys key = (keyData & Keys.KeyCode);
                if (key == Keys.Enter)
                {
                    //交由自定义控件处理
                    return false;
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (EnterAsTab)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    return this.ProcessTabKey(e.KeyData);
                }
            }

            return base.ProcessDataGridViewKey(e);
        }

        [DefaultValue(false)]
        [Description("编辑输入时，用Enter键代替Tab键跳到下一个单元格"), Category("SunnyUI")]
        public bool EnterAsTab { get; set; }

        private Color scrollBarColor = Color.FromArgb(80, 160, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ScrollBarColor
        {
            get => scrollBarColor;
            set
            {
                scrollBarColor = value;
                HBar.HoverColor = HBar.PressColor = HBar.ForeColor = value;
                VBar.HoverColor = VBar.PressColor = VBar.ForeColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        private Color scrollBarBackColor = Color.FromArgb(243, 249, 255);

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color ScrollBarBackColor
        {
            get => scrollBarBackColor;
            set
            {
                scrollBarBackColor = value;
                HBar.FillColor = value;
                VBar.FillColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }
    }

    public static class UIDataGridViewHelper
    {
        public static DataGridViewColumn SetFixedMode(this DataGridViewColumn column, int width)
        {
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Width = width;
            return column;
        }

        public static DataGridViewColumn SetSortMode(this DataGridViewColumn column, DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.Automatic)
        {
            column.SortMode = sortMode;
            return column;
        }

        public static bool IsDBNull(this DataGridViewCell cell)
        {
            return cell.Value is DBNull;
        }
    }
}