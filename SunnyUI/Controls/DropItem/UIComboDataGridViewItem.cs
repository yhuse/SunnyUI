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

        private void EdtFilter_TextChanged(object sender, System.EventArgs e)
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
            if (!IsScaled)
            {
                edtFilter.SetDPIScaleFont();
                btnSearch.SetDPIScaleFont();
                btnClear.SetDPIScaleFont();
                btnOK.SetDPIScaleFont();
                btnCancel.SetDPIScaleFont();
            }

            base.SetDPIScale();
        }

        public void Translate()
        {
            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel = new Sunny.UI.UIPanel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.dataGridView = new Sunny.UI.UIDataGridView();
            this.pFilter = new Sunny.UI.UIPanel();
            this.btnClear = new Sunny.UI.UISymbolButton();
            this.btnSearch = new Sunny.UI.UISymbolButton();
            this.edtFilter = new Sunny.UI.UITextBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnCancel);
            this.panel.Controls.Add(this.btnOK);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel.Location = new System.Drawing.Point(0, 289);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel.MinimumSize = new System.Drawing.Size(1, 1);
            this.panel.Name = "panel";
            this.panel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.panel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.panel.Size = new System.Drawing.Size(569, 44);
            this.panel.TabIndex = 2;
            this.panel.Text = null;
            this.panel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel.Visible = false;
            this.panel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(478, 8);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Size = new System.Drawing.Size(80, 29);
            this.btnCancel.Style = Sunny.UI.UIStyle.Red;
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(389, 8);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.dataGridView.Location = new System.Drawing.Point(0, 44);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dataGridView.SelectedIndex = -1;
            this.dataGridView.Size = new System.Drawing.Size(569, 245);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // pFilter
            // 
            this.pFilter.Controls.Add(this.btnClear);
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.edtFilter);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pFilter.MinimumSize = new System.Drawing.Size(1, 1);
            this.pFilter.Name = "pFilter";
            this.pFilter.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.pFilter.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.pFilter.Size = new System.Drawing.Size(569, 44);
            this.pFilter.TabIndex = 4;
            this.pFilter.Text = null;
            this.pFilter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pFilter.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClear.Location = new System.Drawing.Point(478, 8);
            this.btnClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 29);
            this.btnClear.Symbol = 61666;
            this.btnClear.SymbolSize = 22;
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清除";
            this.btnClear.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSearch.Location = new System.Drawing.Point(389, 8);
            this.btnSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 29);
            this.btnSearch.Symbol = 61442;
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "搜索";
            this.btnSearch.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // edtFilter
            // 
            this.edtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtFilter.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edtFilter.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edtFilter.Location = new System.Drawing.Point(17, 10);
            this.edtFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.edtFilter.MinimumSize = new System.Drawing.Size(1, 16);
            this.edtFilter.Name = "edtFilter";
            this.edtFilter.ShowText = false;
            this.edtFilter.Size = new System.Drawing.Size(363, 25);
            this.edtFilter.TabIndex = 0;
            this.edtFilter.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.edtFilter.Watermark = "";
            this.edtFilter.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.edtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtFilter_KeyDown);
            // 
            // UIComboDataGridViewItem
            // 
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.pFilter);
            this.Controls.Add(this.panel);
            this.Name = "UIComboDataGridViewItem";
            this.Size = new System.Drawing.Size(569, 333);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pFilter.ResumeLayout(false);
            this.ResumeLayout(false);

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

        private void DataGridView_DataSourceChanged(object sender, System.EventArgs e)
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

        private void btnOK_Click(object sender, System.EventArgs e)
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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        public bool ShowFilter
        {
            get => pFilter.Visible;
            set => pFilter.Visible = value;
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
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

        private void btnClear_Click(object sender, System.EventArgs e)
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
                btnSearch_Click(null, null);
        }
    }
}