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
 * 文件名称: UIGrid.cs
 * 文件说明: 表格
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 功能增强、美化
 * 2020-07-15: V2.2.6 更改默认配置为原生
 * 2020-07-18: V2.2.6 重绘水平滚动条
 * 2020-08-22: V2.2.7 更新了水平和垂直滚动条的显示，优化滚动效果。
 * 2020-08-28: V2.2.7 调整水平滚动条
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIDataGridView : DataGridView, IStyleInterface
    {
        private readonly UIScrollBar VBar = new UIScrollBar();
        private readonly UIHorScrollBarEx HBar = new UIHorScrollBarEx();

        public UIDataGridView()
        {
            BackgroundColor = UIColor.White;
            GridColor = UIColor.Blue;
            base.Font = UIFontColor.Font;
            base.DoubleBuffered = true;

            VBar.Parent = this;
            VBar.Visible = false;
            HBar.FillColor = VBar.FillColor = UIColor.LightBlue;
            VBar.ForeColor = UIColor.Blue;
            VBar.StyleCustomMode = true;
            VBar.ValueChanged += VBarValueChanged;

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

            //标题行行高，与OnColumnAdded事件配合
            ColumnHeadersHeight = 32;

            //数据行行高
            RowTemplate.Height = 29;
            RowTemplate.MinimumHeight = 29;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            //设置奇偶数行颜色
            StripeEvenColor = UIColor.White;
            StripeOddColor = UIColor.LightBlue;

            VerticalScrollBar.ValueChanged += VerticalScrollBar_ValueChanged;
            HorizontalScrollBar.ValueChanged += HorizontalScrollBar_ValueChanged;
            VerticalScrollBar.VisibleChanged += VerticalScrollBar_VisibleChanged;
            HorizontalScrollBar.VisibleChanged += HorizontalScrollBar_VisibleChanged;
        }

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
        }

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
            if (VBar.Visible)
            {
                if (e.Delta > 10)
                {
                    VBar.SetValue(VBar.Value - VBar.Maximum / 20);
                }
                else if (e.Delta < -10)
                {
                    VBar.SetValue(VBar.Value + VBar.Maximum / 20);
                }
            }
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

        [DefaultValue(typeof(Color), "235, 243, 255")]
        [Description("奇数行显示颜色"), Category("SunnyUI")]
        public Color StripeOddColor
        {
            get => AlternatingRowsDefaultCellStyle.BackColor;
            set
            {
                AlternatingRowsDefaultCellStyle.BackColor = value;
                HBar.FillColor = VBar.FillColor = value;
                Invalidate();
            }
        }

        public void SetStyle(UIStyle style)
        {
            if (!style.Equals(UIStyle.Custom))
            {
                SetStyleColor(UIStyles.GetStyleColor(style));
            }

            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            //标题行颜色
            ColumnHeadersDefaultCellStyle.BackColor = uiColor.TitleColor;
            ColumnHeadersDefaultCellStyle.ForeColor = uiColor.TitleForeColor;

            //数据行选中颜色
            DefaultCellStyle.SelectionBackColor = uiColor.GridSelectedColor;
            DefaultCellStyle.SelectionForeColor = uiColor.GridSelectedForeColor;

            GridColor = RectColor = uiColor.RectColor;
            RowsDefaultCellStyle.BackColor = UIColor.White;
            AlternatingRowsDefaultCellStyle.BackColor = UIColor.LightBlue;

            StripeEvenColor = uiColor.GridStripeEvenColor;
            StripeOddColor = uiColor.GridStripeOddColor;

            HBar.FillColor = VBar.FillColor = uiColor.GridStripeOddColor;
            HBar.ForeColor = VBar.ForeColor = uiColor.PrimaryColor;

            Invalidate();
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
        [DefaultValue(false)]
        public bool ShowGridLine
        {
            get => CellBorderStyle == DataGridViewCellBorderStyle.Single;
            set => CellBorderStyle = value ? DataGridViewCellBorderStyle.Single : DataGridViewCellBorderStyle.None;
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

        private int selectedIndex = -1;

        [Browsable(false)]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (Rows.Count == 0)
                {
                    selectedIndex = -1;
                    return;
                }

                if (value >= 0 && value < Rows.Count)
                {
                    Rows[value].Selected = true;
                    selectedIndex = value;
                    FirstDisplayedScrollingRowIndex = value;
                    SelectIndexChange?.Invoke(this, value);
                }
                else
                {
                    selectedIndex = -1;
                }
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            SetScrollInfo();
            selectedIndex = -1;
        }

        public delegate void OnSelectIndexChange(object sender, int index);

        public event OnSelectIndexChange SelectIndexChange;

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);

            if (e.RowIndex >= 0 && selectedIndex != e.RowIndex)
            {
                selectedIndex = e.RowIndex;
                SelectIndexChange?.Invoke(this, e.RowIndex);
            }
        }

        protected override void OnGridColorChanged(EventArgs e)
        {
            base.OnGridColorChanged(e);
            _style = UIStyle.Custom;
        }

        protected override void OnDefaultCellStyleChanged(EventArgs e)
        {
            base.OnDefaultCellStyleChanged(e);
            _style = UIStyle.Custom;
        }

        protected override void OnColumnDefaultCellStyleChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnDefaultCellStyleChanged(e);
            _style = UIStyle.Custom;
        }

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
    }
}