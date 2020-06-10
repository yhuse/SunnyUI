/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIGrid.cs
 * 文件说明: 表格
 * 当前版本: V2.2
 * 创建日期: 2020-04-15
 *
 * 2020-04-11: V2.2.2 增加UIGrid
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable 1591
// ReSharper disable All

namespace Sunny.UI
{
    [DefaultEvent("CellValueChanged")]
    [DefaultProperty("Columns")]
    [ToolboxItem(false)]
    public sealed class UIGrid : UIPanel
    {
        private UIRowsLayout RowsLayout;
        private UIPanel Header;
        private UIPanel Footer;
        private UIScrollBar ScrollBar;
        private UIPanel Cover;

        public UIGrid()
        {
            InitializeComponent();
            RowsLayout.DoubleBuffered();
            Header.DoubleBuffered();
            Footer.DoubleBuffered();
            ScrollBar.DoubleBuffered();

            ShowFooter = false;
            RowsLayout.AutoScroll = true;
            RowsLayout.MouseWheel += RowContainer_MouseWheel;

            StripeEvenColor = UIColor.White;
            StripeOddColor = UIStyles.Blue.PlainColor;
            RowSelectedColor = UIStyles.Blue.GridSelectedColor;

            HeaderFillColor = UIStyles.Blue.TitleColor;
            HeaderForeColor = UIStyles.Blue.TitleForeColor;

            columns.OnAppend += Columns_Changed;
            columns.OnDelete += Columns_Changed;
            RowsLayout.HorizontalScroll.Visible = false;//水平的显示
            RowsLayout.HorizontalScroll.Enabled = false;

            showWidth = Width;
        }

        private bool showLine;

        [DefaultValue(false)]
        public bool ShowLine
        {
            get => showLine;
            set
            {
                if (showLine != value)
                {
                    showLine = value;
                    foreach (var row in Rows)
                    {
                        row.RectSides = showLine
                            ? ToolStripStatusLabelBorderSides.Bottom
                            : ToolStripStatusLabelBorderSides.None;
                    }
                }
            }
        }

        private Color lineColor = Color.FromArgb(233, 236, 244);

        [DefaultValue(typeof(Color), "233, 236, 244")]
        public Color LineColor
        {
            get => lineColor;
            set
            {
                lineColor = value;
                foreach (var row in Rows)
                {
                    row.Invalidate();
                }
            }
        }

        private void Columns_Changed(object sender, UIGridColumn item, int index)
        {
            if (!columnAdding)
            {
                ClearRows();
                InitHeaderFooter();
            }
        }

        private int showWidth;

        private int ShowWidth
        {
            get => showWidth;
            set
            {
                showWidth = value;
                InitHeaderFooter();
            }
        }

        private void CalcCellBounds()
        {
            if (ColumnCount == 0) return;
            int fixedWidth = 0;
            int fillWeight = 0;
            foreach (var column in Columns)
            {
                if (column.SizeMode == UIGridColumnSizeMode.Fixed)
                    fixedWidth += column.Width;
                if (column.SizeMode == UIGridColumnSizeMode.Fill)
                    fillWeight += column.FillWeight;
            }

            int existWidth = ShowWidth - fixedWidth;
            int left = 0;
            foreach (var column in Columns)
            {
                int width = 0;
                if (column.SizeMode == UIGridColumnSizeMode.Fixed)
                    width = column.Width;
                if (column.SizeMode == UIGridColumnSizeMode.Fill)
                    width += column.FillWeight * existWidth / fillWeight;

                if (width <= 0)
                    column.Bounds = new Rectangle(left + 1, 1, 10, RowHeight - 3);
                else
                    column.Bounds = new Rectangle(left + 1, 1, width - 2, RowHeight - 3);

                left += width;
            }
        }

        public delegate void UIGridCellValueChanged(object sender, UIGridColumn column, UIGridRow row, object data, object value);

        public event UIGridCellValueChanged CellValueChanged;

        public event UIGridCellValueChanged CellButtonClick;

        public event UIGridCellValueChanged CellLinkClick;

        public event UIGridCellValueChanged CellDoubleClick;

        public void GridCellValueChange(object sender, UIGridColumn column, UIGridRow row, object data, object value)
        {
            CellValueChanged?.Invoke(this, column, row, data, value);
        }

        public void GridCellButtonClick(object sender, UIGridColumn column, UIGridRow row, object data, object value)
        {
            CellButtonClick?.Invoke(this, column, row, data, value);
        }

        public void GridCellLinkClick(object sender, UIGridColumn column, UIGridRow row, object data, object value)
        {
            CellLinkClick?.Invoke(this, column, row, data, value);
        }

        public void GridCellDoubleClick(object sender, UIGridColumn column, UIGridRow row, object data, object value)
        {
            CellDoubleClick?.Invoke(sender, column, row, data, value);
        }

        private void RowsLayout_SizeChanged(object sender, EventArgs e)
        {
            CalcCellBounds();
            ScrollBar.Top = RowsLayout.Top;
            ScrollBar.Height = RowsLayout.Height;
            Cover.Location = RowsLayout.Location;
            Cover.Size = RowsLayout.Size;

            int height = Rows.Count * RowHeight;
            ShowWidth = height > RowsLayout.Height ? Width - ScrollBarInfo.VerticalScrollBarWidth() - 2 : Width - 2;
            if (Rows.Count > 0)
            {
                flicker.FreezePainting(RowsLayout, true);

                foreach (var row in Rows)
                {
                    if (row.Width != ShowWidth)
                    {
                        row.Width = ShowWidth;
                    }
                }

                flicker.FreezePainting(RowsLayout, false);
            }
        }

        /// <summary>
        /// 显示斑马条纹
        /// </summary>
        [Description("显示斑马条纹")]
        [DefaultValue(true)]
        public bool Stripe { get; set; } = true;

        [DefaultValue(typeof(Color), "White")]
        public Color StripeEvenColor { get; set; }

        [DefaultValue(typeof(Color), "235, 243, 255")]
        public Color StripeOddColor { get; set; }

        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color RowSelectedColor { get; set; }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            StripeEvenColor = uiColor.GridStripeEvenColor;
            StripeOddColor = uiColor.GridStripeOddColor;
            RowSelectedColor = uiColor.GridSelectedColor;

            HeaderFillColor = uiColor.TitleColor;
            HeaderForeColor = uiColor.TitleForeColor;
            Header.RectColor = uiColor.RectColor;

            RowsLayout.RectColor = uiColor.RectColor;
            Footer.RectColor = uiColor.RectColor;

            LineColor = uiColor.GridLineColor;

            ScrollBar.SetStyleColor(uiColor);

            flicker.FreezePainting(RowsLayout, true);

            foreach (var row in Rows)
            {
                row.SetStyleColor(uiColor);
            }

            flicker.FreezePainting(RowsLayout, false);
        }

        private ListEx<UIGridColumn> columns = new ListEx<UIGridColumn>();

        [TypeConverter(typeof(System.ComponentModel.CollectionConverter))]//指定编辑器特性
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]//设定序列化特性
        public ListEx<UIGridColumn> Columns
        {
            get { return columns; }
            set
            {
                ClearRows();
                columns = value;
                columns.OnAppend += Columns_Changed;
                columns.OnDelete += Columns_Changed;
                InitHeaderFooter();
            }
        }

        public UIGridColumn GetColumnByGuid(string guid)
        {
            if (columns == null) return null;
            foreach (var column in Columns)
            {
                if (column.Guid.Equals(guid))
                    return column;
            }

            return null;
        }

        public UIGridColumn GetColumnByName(string name)
        {
            if (columns == null) return null;
            foreach (var column in Columns)
            {
                if (column.Name.Equals(name))
                    return column;
            }

            return null;
        }

        public UIGridColumn GetColumnByHeaderText(string headerText)
        {
            if (columns == null) return null;
            foreach (var column in Columns)
            {
                if (column.HeaderText.Equals(headerText))
                    return column;
            }

            return null;
        }

        public UIGridColumn GetColumnByDataPropertyName(string dataPropertyName)
        {
            if (columns == null) return null;
            foreach (var column in Columns)
            {
                if (column.DataPropertyName.Equals(dataPropertyName))
                    return column;
            }

            return null;
        }

        public UIGridColumn this[int index]
        {
            get
            {
                if (columns == null || columns.Count == 0) return null;
                if (index < 0 || index >= ColumnCount) return null;
                return columns[index];
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetScrollInfo();
        }

        [Browsable(false)]
        public int ColumnCount => Columns == null ? 0 : Columns.Count;

        private void SetScrollInfo()
        {
            if (ScrollBar == null)
            {
                return;
            }

            var si = ScrollBarInfo.GetInfo(RowsLayout.Handle);

            if (si.ScrollMax > 0)
            {
                ScrollBar.Maximum = si.ScrollMax;
                ShowScrollBar = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
                ScrollBar.Value = si.nPos;
            }
            else
            {
                ShowScrollBar = false;
            }

            ScrollBar.Top = RowsLayout.Top;
            ScrollBar.Height = RowsLayout.Height;
            ScrollBar.Width = ScrollBarInfo.VerticalScrollBarWidth() + 1;
            ScrollBar.Left = Width - ScrollBar.Width;
        }

        private void RowContainer_MouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ShowScrollBar)
            {
                var si = ScrollBarInfo.GetInfo(Handle);
                if (e.Delta > 10)
                {
                    if (si.nPos > 0)
                    {
                        ScrollBarInfo.ScrollUp(Handle);
                    }
                }
                else if (e.Delta < -10)
                {
                    if (si.nPos < si.ScrollMax)
                    {
                        ScrollBarInfo.ScrollDown(Handle);
                    }
                }
            }

            SetScrollInfo();
        }

        [DefaultValue(false)]
        [Browsable(false)]
        public bool ShowScrollBar
        {
            get => ScrollBar.Visible;
            set
            {
                ScrollBar.Visible = value;
                ScrollBar.BringToFront();
            }
        }

        [DefaultValue(false)]
        public bool ShowFooter
        {
            get => Footer.Visible;
            set => Footer.Visible = value;
        }

        [DefaultValue(true)]
        public bool ShowHeader
        {
            get => Header.Visible;
            set => Header.Visible = value;
        }

        [DefaultValue(typeof(Color), "64, 158, 255")]
        public Color HeaderFillColor
        {
            get => Header.FillColor;
            set
            {
                Header.FillColor = value;
                Header.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        public Color HeaderForeColor
        {
            get => Header.ForeColor;
            set
            {
                Header.ForeColor = value;
                Header.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "32, 32, 32")]
        public Color FooterForeColor
        {
            get => Footer.ForeColor;
            set
            {
                Footer.ForeColor = value;
                Footer.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        public Color FooterFillColor
        {
            get => Footer.FillColor;
            set
            {
                Footer.FillColor = value;
                Footer.Invalidate();
            }
        }

        [DefaultValue(35)]
        public int HeaderHeight
        {
            get => Header.Height;
            set
            {
                Header.Height = value;
            }
        }

        /// <summary>
        /// 类型为Image时图片列表
        /// </summary>
        [DefaultValue(null)]
        public ImageList ImageList { get; set; }

        public UIGridColumn AddColumn(UIGridColumn column)
        {
            Columns.Add(column);
            if (!columnAdding)
            {
                InitHeaderFooter();
            }

            return column;
        }

        public void BeginAddColumn()
        {
            columnAdding = true;
        }

        private bool columnAdding;

        public void EndAddColumn()
        {
            columnAdding = false;
            InitHeaderFooter();
        }

        public void InitHeaderFooter()
        {
            CalcCellBounds();
            Header.Invalidate();
            Footer.Invalidate();
        }

        public UIGridColumn AddColumn(string name, string headerText, string dataPropertyName, UIGridColumnType columnType = UIGridColumnType.Label, UIGridColumnSizeMode sizeMode = UIGridColumnSizeMode.Fill, int fillWeight = 100)
        {
            return AddColumn(new UIGridColumn(name, headerText, dataPropertyName, columnType, sizeMode, fillWeight));
        }

        private AvoidControlFlicker flicker = new AvoidControlFlicker();

        private string noData = UILocalize.GridNoData;

        public string NoData
        {
            get => noData;
            set
            {
                noData = value;
                if (dataSource == null)
                {
                    Cover.Text = value;
                }
            }
        }

        public string DataLoading { get; set; } = UILocalize.GridDataLoading;

        public void BeginUpdate()
        {
            flicker.FreezePainting(RowsLayout, true);
            Cover.Text = DataLoading;
            Application.DoEvents();
        }

        public void EndUpdate()
        {
            flicker.FreezePainting(RowsLayout, false);
            Application.DoEvents();
            SetScrollInfo();
            Cover.Visible = false;
            Cover.SendToBack();
        }

        private void InitLayoutPanel(TableLayoutPanel layout)
        {
            layout.ColumnCount = ColumnCount;
            layout.ColumnStyles.Clear();
            foreach (var column in Columns)
            {
                SizeType sizeType = column.SizeMode == UIGridColumnSizeMode.Fixed
                    ? SizeType.Absolute
                    : SizeType.Percent;
                int width = column.SizeMode == UIGridColumnSizeMode.Fixed ? column.Width : column.FillWeight;
                layout.ColumnStyles.Add(new ColumnStyle(sizeType, width));
            }
        }

        private void HeaderCell_Click(object sender, EventArgs e)
        {
            if (!RowsLayout.Focused)
                RowsLayout.Focus();
        }

        public void UpdateFooterText(UIGridColumn column, string footerText)
        {
            Footer.Invalidate();
        }

        [DefaultValue(32)]
        public int RowHeight { get; set; } = 32;

        private object dataSource;

        [Browsable(false)]
        [DefaultValue(null)]
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (value != null)
                {
                    if (!(value is DataTable || value.GetType().IsList()))
                    {
                        throw new Exception(UILocalize.GridDataSourceException);
                    }
                }

                dataSource = value;
                if (dataSource != null)
                {
                    LoadDataSource();
                    SelectedFirst();
                }
            }
        }

        public void ClearRows()
        {
            DataSource = null;
            foreach (var row in Rows)
            {
                row.Dispose();
            }

            Rows.Clear();

            Cover.Location = RowsLayout.Location;
            Cover.Size = RowsLayout.Size;
            Cover.BringToFront();
            Cover.Visible = true;
        }

        public void ClearColumns()
        {
            Columns.Clear();
            ClearRows();
        }

        public void SelectedFirst()
        {
            if (Rows.Count > 0)
            {
                SelectedRow = Rows[0];
            }
        }

        private UIGridRow selectedRow;

        [Browsable(false)]
        [DefaultValue(null)]
        public UIGridRow SelectedRow
        {
            get => selectedRow;
            set
            {
                if (Rows.Count == 0)
                {
                    selectedRow = null;
                    return;
                }

                if (value == null)
                {
                    selectedRow = null;
                    return;
                }

                if (selectedRow == value)
                {
                    return;
                }

                if (selectedRow != null)
                {
                    selectedRow.Selected = false;
                }

                selectedRow = value;
                selectedRow.Selected = true;
                flicker.FreezePainting(RowsLayout, true);
                RowsLayout.ScrollControlIntoView(value);
                flicker.FreezePainting(RowsLayout, false);
            }
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedRowIndex
        {
            get => selectedRow == null ? -1 : selectedRow.RowIndex;
            set
            {
                if (Rows.Count > 0 && value >= 0 && value < Rows.Count)
                {
                    SelectedRow = Rows[value];
                }
            }
        }

        public List<UIGridRow> SelectedRows => GetSelectedRows();

        private List<UIGridRow> GetSelectedRows()
        {
            return null;
        }

        [Browsable(false)]
        private int DataCount
        {
            get
            {
                int count = 0;
                if (dataSource == null)
                {
                    return count;
                }

                if (dataSource is DataTable)
                {
                    count = (dataSource as DataTable).Rows.Count;
                }

                if (dataSource.GetType().IsList())
                {
                    count = (dataSource as IList).Count;
                }

                return count;
            }
        }

        private void LoadDataSource()
        {
            if (IsDesignMode)
            {
                return;
            }

            if (DataCount == 0)
            {
                return;
            }

            BeginUpdate();

            if (Rows.Count > DataCount)
            {
                for (int i = DataCount; i < Rows.Count; i++)
                {
                    Rows[i].Dispose();
                }

                Rows.RemoveRange(DataCount, Rows.Count - DataCount);
            }

            int height = DataCount * RowHeight;
            ShowWidth = height > RowsLayout.Height ? Width - ScrollBarInfo.VerticalScrollBarWidth() - 2 : Width - 2;

            for (int dataIndex = 0; dataIndex < DataCount; dataIndex++)
            {
                object data = null;

                if (dataSource is DataTable)
                {
                    data = (dataSource as DataTable).Rows[dataIndex];
                }
                if (dataSource.GetType().IsList())
                {
                    data = (dataSource as IList)[dataIndex];
                }

                if (dataIndex < Rows.Count)
                {
                    Rows[dataIndex].RowIndex = dataIndex;
                    Rows[dataIndex].Data = data;
                    Rows[dataIndex].Width = ShowWidth;
                }
                else
                {
                    UIGridRow row = new UIGridRow(this);
                    row.Data = data;
                    row.RowIndex = dataIndex;
                    row.Left = 1;
                    row.Top = dataIndex * RowHeight;
                    row.Height = RowHeight;
                    RowsLayout.Controls.Add(row);
                    row.Width = ShowWidth;
                    Rows.Add(row);
                }
            }

            EndUpdate();
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (RowsLayout != null)
            {
                flicker.FreezePainting(RowsLayout, true);
                ScrollBarInfo.SetScrollValue(RowsLayout.Handle, ScrollBar.Value);
                flicker.FreezePainting(RowsLayout, false);
            }
        }

        public readonly List<UIGridRow> Rows = new List<UIGridRow>();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                Prev();
            }
            else if (keyData == Keys.Down)
            {
                Next();
            }
            else if (keyData == Keys.Home)
            {
                First();
            }
            else if (keyData == Keys.End)
            {
                Last();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Last()
        {
            if (Rows.Count > 0)
            {
                SelectedRow = Rows[Rows.Count - 1];
            }
        }

        private void First()
        {
            if (Rows.Count > 0)
            {
                SelectedRow = Rows[0];
            }
        }

        private void Next()
        {
            if (Rows.Count > 0)
            {
                if (SelectedRow != null && SelectedRow.RowIndex < Rows.Count - 1)
                    SelectedRow = Rows[SelectedRow.RowIndex + 1];
            }
        }

        private void Prev()
        {
            if (Rows.Count > 0)
            {
                if (SelectedRow != null && SelectedRow.RowIndex > 0)
                    SelectedRow = Rows[SelectedRow.RowIndex - 1];
            }
        }

        #region InitializeComponent

        private void InitializeComponent()
        {
            this.ScrollBar = new Sunny.UI.UIScrollBar();
            this.RowsLayout = new Sunny.UI.UIRowsLayout();
            this.Footer = new Sunny.UI.UIPanel();
            this.Header = new Sunny.UI.UIPanel();
            this.Cover = new Sunny.UI.UIPanel();
            this.SuspendLayout();
            //
            // ScrollBar
            //
            this.ScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollBar.BackColor = System.Drawing.Color.Transparent;
            this.ScrollBar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ScrollBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.ScrollBar.Location = new System.Drawing.Point(780, 37);
            this.ScrollBar.Name = "ScrollBar";
            this.ScrollBar.Size = new System.Drawing.Size(19, 406);
            this.ScrollBar.Style = Sunny.UI.UIStyle.Custom;
            this.ScrollBar.StyleCustomMode = true;
            this.ScrollBar.TabIndex = 18;
            this.ScrollBar.ValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
            this.ScrollBar.Click += new System.EventHandler(this.HeaderCell_Click);
            //
            // RowsLayout
            //
            this.RowsLayout.BackColor = System.Drawing.Color.Transparent;
            this.RowsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RowsLayout.FillColor = System.Drawing.Color.White;
            this.RowsLayout.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.RowsLayout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RowsLayout.Location = new System.Drawing.Point(0, 35);
            this.RowsLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RowsLayout.Name = "RowsLayout";
            this.RowsLayout.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RowsLayout.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.RowsLayout.Size = new System.Drawing.Size(800, 410);
            this.RowsLayout.Style = Sunny.UI.UIStyle.Custom;
            this.RowsLayout.StyleCustomMode = true;
            this.RowsLayout.TabIndex = 13;
            this.RowsLayout.Text = null;
            this.RowsLayout.SizeChanged += new System.EventHandler(this.RowsLayout_SizeChanged);
            //
            // Footer
            //
            this.Footer.BackColor = System.Drawing.Color.Transparent;
            this.Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Footer.FillColor = System.Drawing.Color.White;
            this.Footer.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Footer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Footer.Location = new System.Drawing.Point(0, 445);
            this.Footer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Footer.Name = "Footer";
            this.Footer.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Footer.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.Footer.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Footer.Size = new System.Drawing.Size(800, 35);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            this.Footer.StyleCustomMode = true;
            this.Footer.TabIndex = 11;
            this.Footer.Text = null;
            this.Footer.PaintOther += new System.Windows.Forms.PaintEventHandler(this.Footer_Paint);
            this.Footer.Click += new System.EventHandler(this.HeaderCell_Click);
            //
            // Header
            //
            this.Header.BackColor = System.Drawing.Color.Transparent;
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.Header.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Header.Name = "Header";
            this.Header.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Header.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.Header.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.Header.Size = new System.Drawing.Size(800, 35);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            this.Header.StyleCustomMode = true;
            this.Header.TabIndex = 12;
            this.Header.Text = null;
            this.Header.PaintOther += new System.Windows.Forms.PaintEventHandler(this.Header_Paint);
            this.Header.Click += new System.EventHandler(this.HeaderCell_Click);
            //
            // Cover
            //
            this.Cover.FillColor = System.Drawing.Color.White;
            this.Cover.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Cover.Location = new System.Drawing.Point(0, 35);
            this.Cover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Cover.Name = "Cover";
            this.Cover.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Cover.Size = new System.Drawing.Size(800, 410);
            this.Cover.Style = Sunny.UI.UIStyle.Custom;
            this.Cover.StyleCustomMode = true;
            this.Cover.TabIndex = 20;
            this.Cover.Text = "[ 无数据 ]";
            //
            // UIGrid
            //
            this.Controls.Add(this.Cover);
            this.Controls.Add(this.ScrollBar);
            this.Controls.Add(this.RowsLayout);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Name = "UIGrid";
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.Size = new System.Drawing.Size(800, 480);
            this.ResumeLayout(false);
        }

        #endregion InitializeComponent

        private void Header_Paint(object sender, PaintEventArgs e)
        {
            if (ColumnCount == 0) return;

            foreach (var column in Columns)
            {
                if (column.HeaderText.IsNullOrEmpty()) continue;

                SizeF sf = e.Graphics.MeasureString(column.HeaderText, Font);
                if (column.HeaderTextAlignment == StringAlignment.Near)
                    e.Graphics.DrawString(column.HeaderText, Font, HeaderForeColor, column.Bounds.X, (HeaderHeight - sf.Height) / 2.0f);
                if (column.HeaderTextAlignment == StringAlignment.Center)
                    e.Graphics.DrawString(column.HeaderText, Font, HeaderForeColor, column.Bounds.X + (column.Bounds.Width - sf.Width) / 2.0f, (HeaderHeight - sf.Height) / 2.0f);
                if (column.HeaderTextAlignment == StringAlignment.Far)
                    e.Graphics.DrawString(column.HeaderText, Font, HeaderForeColor, column.Bounds.Right - sf.Width - 1, (HeaderHeight - sf.Height) / 2.0f);
            }
        }

        private void Footer_Paint(object sender, PaintEventArgs e)
        {
            if (ColumnCount == 0) return;

            foreach (var column in Columns)
            {
                if (column.FooterText.IsNullOrEmpty()) continue;

                SizeF sf = e.Graphics.MeasureString(column.FooterText, Font);
                if (column.FooterTextAlignment == StringAlignment.Near)
                    e.Graphics.DrawString(column.FooterText, Font, FooterForeColor, column.Bounds.X, (HeaderHeight - sf.Height) / 2.0f);
                if (column.FooterTextAlignment == StringAlignment.Center)
                    e.Graphics.DrawString(column.FooterText, Font, FooterForeColor, column.Bounds.X + (column.Bounds.Width - sf.Width) / 2.0f, (HeaderHeight - sf.Height) / 2.0f);
                if (column.FooterTextAlignment == StringAlignment.Far)
                    e.Graphics.DrawString(column.FooterText, Font, FooterForeColor, column.Bounds.Right - sf.Width - 1, (HeaderHeight - sf.Height) / 2.0f);
            }
        }
    }

    [ToolboxItem(false)]
    public sealed class UIRowsLayout : UIPanel
    {
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (IsDisposed || Disposing) return;
            ScrollBarInfo.ShowScrollBar(Handle, 0, false);//0:horizontal,1:vertical,3:both
        }
    }

    public class AvoidControlFlicker
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessageA")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private int _paintFrozen;

        public void FreezePainting(Control control, bool isToFreeze)
        {
            if (control == null)
            {
                return;
            }

            if (isToFreeze && control.IsHandleCreated && control.Visible)
            {
                if (0 == _paintFrozen++)
                {
                    SendMessage(control.Handle, 0x000B, 0, 0);
                }
            }

            if (!isToFreeze)
            {
                if (0 == _paintFrozen)
                {
                    return;
                }

                if (0 == --_paintFrozen)
                {
                    SendMessage(control.Handle, 0x000B, 1, 0);
                    control.Invalidate(true);
                }
            }
        }
    }
}