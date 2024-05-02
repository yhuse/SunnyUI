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
 * 文件名称: UIComboDataGridViewItem.cs
 * 文件说明: 表格选择框弹出窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2023-03-29: V3.3.3 增加多语翻译
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIComboDataGridViewItem : UIDropDownItem, ITranslate
    {
        private UIPanel panel;
        private UISymbolButton btnCancel;
        private UISymbolButton btnOK;
        private UIPanel pFilter;
        private UISymbolButton btnSearch;
        private UITextBox edtFilter;
        private UISymbolButton btnClear;
        private UIDataGridView dataGridView;

        public event OnComboDataGridViewFilterChanged ComboDataGridViewFilterChanged;

        public UIComboDataGridViewItem()
        {
            InitializeComponent();
            Translate();
            dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            edtFilter.TextChanged += EdtFilter_TextChanged;
        }

        [DefaultValue(true), Description("过滤框输入逐一过滤"), Category("SunnyUI")]
        public bool Filter1by1 { get; set; } = true;

        public bool TrimFilter { get; set; }

        private void EdtFilter_TextChanged(object sender, EventArgs e)
        {
            if (!Filter1by1) return;
            btnSearch_Click(null, null);
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dataGridView.MultiSelect)
                btnOK.PerformClick();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            edtFilter.SetDPIScale();
            btnSearch.SetDPIScale();
            btnClear.SetDPIScale();
            btnOK.SetDPIScale();
            btnCancel.SetDPIScale();
            dataGridView.SetDPIScale();
        }

        public void Translate()
        {
            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
            btnClear.Text = UILocalize.Clear;
            btnSearch.Text = UILocalize.Search;
        }

        public bool ShowButtons
        {
            get => panel.Visible;
            set => panel.Visible = value;
        }

        public string FilterColumnName { get; set; }

        public UIDataGridView DataGridView => dataGridView;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            panel = new UIPanel();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            dataGridView = new UIDataGridView();
            pFilter = new UIPanel();
            btnClear = new UISymbolButton();
            btnSearch = new UISymbolButton();
            edtFilter = new UITextBox();
            panel.SuspendLayout();
            ((ISupportInitialize)dataGridView).BeginInit();
            pFilter.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(btnCancel);
            panel.Controls.Add(btnOK);
            panel.Dock = DockStyle.Bottom;
            panel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            panel.Location = new System.Drawing.Point(0, 289);
            panel.Margin = new Padding(4, 5, 4, 5);
            panel.MinimumSize = new System.Drawing.Size(1, 1);
            panel.Name = "panel";
            panel.RadiusSides = UICornerRadiusSides.None;
            panel.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            panel.Size = new System.Drawing.Size(569, 44);
            panel.TabIndex = 2;
            panel.Text = null;
            panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            panel.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FillColor = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.FillColor2 = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(235, 115, 115);
            btnCancel.FillPressColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Location = new System.Drawing.Point(478, 8);
            btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.RectColor = System.Drawing.Color.FromArgb(230, 80, 80);
            btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(235, 115, 115);
            btnCancel.RectPressColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(184, 64, 64);
            btnCancel.Size = new System.Drawing.Size(80, 29);
            btnCancel.Style = UIStyle.Red;
            btnCancel.StyleCustomMode = true;
            btnCancel.Symbol = 61453;
            btnCancel.TabIndex = 1;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Location = new System.Drawing.Point(389, 8);
            btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(80, 29);
            btnOK.TabIndex = 0;
            btnOK.Text = "确定";
            btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOK.Click += btnOK_Click;
            // 
            // dataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(243, 249, 255);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(243, 249, 255);
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridView.GridColor = System.Drawing.Color.FromArgb(104, 173, 255);
            dataGridView.Location = new System.Drawing.Point(0, 44);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(243, 249, 255);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(220, 236, 255);
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(48, 48, 48);
            dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.ScrollBarRectColor = System.Drawing.Color.FromArgb(80, 160, 255);
            dataGridView.SelectedIndex = -1;
            dataGridView.Size = new System.Drawing.Size(569, 245);
            dataGridView.TabIndex = 3;
            dataGridView.KeyDown += dataGridView_KeyDown;
            // 
            // pFilter
            // 
            pFilter.Controls.Add(btnClear);
            pFilter.Controls.Add(btnSearch);
            pFilter.Controls.Add(edtFilter);
            pFilter.Dock = DockStyle.Top;
            pFilter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pFilter.Location = new System.Drawing.Point(0, 0);
            pFilter.Margin = new Padding(4, 5, 4, 5);
            pFilter.MinimumSize = new System.Drawing.Size(1, 1);
            pFilter.Name = "pFilter";
            pFilter.RadiusSides = UICornerRadiusSides.None;
            pFilter.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right;
            pFilter.Size = new System.Drawing.Size(569, 44);
            pFilter.TabIndex = 4;
            pFilter.Text = null;
            pFilter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnClear.Location = new System.Drawing.Point(478, 8);
            btnClear.MinimumSize = new System.Drawing.Size(1, 1);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(80, 29);
            btnClear.Symbol = 61666;
            btnClear.SymbolSize = 22;
            btnClear.TabIndex = 2;
            btnClear.Text = "清除";
            btnClear.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnClear.Click += btnClear_Click;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnSearch.Location = new System.Drawing.Point(389, 8);
            btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(80, 29);
            btnSearch.Symbol = 61442;
            btnSearch.TabIndex = 1;
            btnSearch.Text = "搜索";
            btnSearch.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnSearch.Click += btnSearch_Click;
            // 
            // edtFilter
            // 
            edtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtFilter.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            edtFilter.Cursor = Cursors.IBeam;
            edtFilter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            edtFilter.Location = new System.Drawing.Point(17, 10);
            edtFilter.Margin = new Padding(4, 5, 4, 5);
            edtFilter.MinimumSize = new System.Drawing.Size(1, 16);
            edtFilter.Name = "edtFilter";
            edtFilter.Padding = new Padding(5);
            edtFilter.ShowText = false;
            edtFilter.Size = new System.Drawing.Size(363, 25);
            edtFilter.TabIndex = 0;
            edtFilter.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            edtFilter.Watermark = "";
            edtFilter.KeyDown += edtFilter_KeyDown;
            // 
            // UIComboDataGridViewItem
            // 
            Controls.Add(dataGridView);
            Controls.Add(pFilter);
            Controls.Add(panel);
            Name = "UIComboDataGridViewItem";
            Size = new System.Drawing.Size(569, 333);
            panel.ResumeLayout(false);
            ((ISupportInitialize)dataGridView).EndInit();
            pFilter.ResumeLayout(false);
            ResumeLayout(false);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            pFilter.SetStyleColor(uiColor.DropDownStyle);
            dataGridView.SetStyleColor(uiColor.DropDownStyle);
            panel.SetStyleColor(uiColor.DropDownStyle);
            edtFilter.SetStyleColor(uiColor.DropDownStyle);
            btnOK.SetStyleColor(uiColor.DropDownStyle);
            btnSearch.SetStyleColor(uiColor.DropDownStyle);
            btnClear.SetStyleColor(uiColor.DropDownStyle);
        }

        private void DataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView.RowCount > 0)
            {
                dataGridView.SelectedIndex = 0;
            }
            else
            {
                dataGridView.ClearSelection();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //UIMessageTip.ShowOk(dataGridView.SelectedIndex.ToString(), 3000);
            //UConsole.WriteConsole("所选行:"+dataGridView.SelectedIndex.ToString(),e,sender);
            if (dataGridView.RowCount > 0 && dataGridView.SelectedIndex >= 0)
            {
                if (dataGridView.MultiSelect)
                {
                    DoValueChanged(this, dataGridView.SelectedRows);
                }
                else
                {
                    if (ShowFilter)
                        DoValueChanged(this, dataGridView.SelectedRows.Count > 0 ? dataGridView.SelectedRows[0] : null);
                    else
                        DoValueChanged(this, dataGridView.SelectedIndex);
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool ShowFilter
        {
            get => pFilter.Visible;
            set => pFilter.Visible = value;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filter = "";
            string filterText = edtFilter.Text;
            if (TrimFilter)
                filterText = filterText.Trim();

            if (filterText.IsNullOrEmpty())
            {
                filter = "";
            }
            else
            {
                if (FilterColumnName.IsValid())
                {
                    string str = FilterColumnName + " like '%" + filterText + "%'";
                    filter = str;
                }
                else
                {
                    List<string> strings = new List<string>();
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        if (column.Visible && column.DataPropertyName.IsValid())
                        {
                            strings.Add(column.DataPropertyName + " like '%" + filterText + "%'");
                        }
                    }

                    filter = string.Join(" or ", strings);
                }
            }

            filter = filter.Replace("*", "[*]");
            if (dataGridView.DataSource is DataTable table)
            {
                try
                {
                    table.DefaultView.RowFilter = filter;
                }
                catch (Exception ex)
                {
                    UIMessageTip.ShowError(ex.Message);
                }
            }

            ComboDataGridViewFilterChanged?.Invoke(this, new UIComboDataGridViewArgs(filterText, dataGridView.RowCount));
        }

        public void ClearFilter()
        {
            btnClear_Click(null, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            edtFilter.Text = "";
            btnSearch.PerformClick();
            dataGridView.SelectedIndex = -1;
            dataGridView.ClearSelection();
            DoValueChanged(this, null);
        }

        public override void InitShow()
        {
            if (ShowFilter)
            {
                edtFilter.Focus();
                edtFilter.SelectAll();
            }
            else
            {
                btnOK.Focus();
            }
        }

        private void edtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Filter1by1 && e.KeyData == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }

            if (e.KeyData == Keys.Down)
            {
                dataGridView.Focus();
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataGridView_CellDoubleClick(this, null);
            }
        }
    }
}