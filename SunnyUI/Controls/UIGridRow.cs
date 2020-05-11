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
 * 文件名称: UIGridColumn.cs
 * 文件说明: 表格行定义类
 * 当前版本: V2.2
 * 创建日期: 2020-04-15
 *
 * 2020-04-11: V2.2.2 增加UIGrid
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable 1591

// ReSharper disable All

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public sealed partial class UIGridRow : UIPanel
    {
        public UIGrid Grid { get; }

        private object data;
        public readonly List<Control> Cells = new List<Control>();

        private bool selected;

        public UIGridRow(UIGrid grid)
        {
            InitializeComponent();
            this.DoubleBuffered();

            Grid = grid;
            AddCells();
            Height = grid.RowHeight;
            RadiusSides = UICornerRadiusSides.None;
            RectSides = ToolStripStatusLabelBorderSides.None;
            MouseEnter += Control_MouseEnter;
            MouseLeave += Control_MouseLeave;
        }

        private UIGridRowState state = UIGridRowState.None;

        public UIGridRowState State
        {
            get => state;
            set
            {
                state = value;
                FillColor = GetRowColor();
            }
        }

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                if (State == UIGridRowState.None)
                {
                    FillColor = GetRowColor();
                }
            }
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            FillColor = GetRowColor();

            foreach (var cell in Cells)
            {
                if (cell is IStyleInterface inCell)
                {
                    inCell.Style = uiColor.Name;
                }
            }
        }

        public object Data
        {
            get => data;
            set
            {
                data = value;
                UpdateData();
            }
        }

        private void ClearCells()
        {
            foreach (Control control in Cells)
            {
                control.Visible = false;
                control.Dispose();
            }

            Cells.Clear();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            foreach (var cell in Cells)
            {
                UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
                cell.Bounds = column.Bounds;
            }
        }

        private Point MousePoint;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MousePoint = e.Location;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            UIGridColumn SelectedColumn = null;
            foreach (var column in Grid.Columns)
            {
                if (MousePoint.X >= column.Bounds.Left && MousePoint.Y <= column.Bounds.Right)
                {
                    SelectedColumn = column;
                    break;
                }
            }

            if (SelectedColumn != null)
            {
                if (!ExistDataPropertyName(SelectedColumn.DataPropertyName))
                {
                    return;
                }

                object value = GetValue(SelectedColumn);
                if (value != null)
                {
                    Grid.GridCellDoubleClick(Grid, SelectedColumn, this, data, value);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var column in Grid.Columns)
            {
                if (column.ColumnType == UIGridColumnType.Label)
                {
                    if (!ExistDataPropertyName(column.DataPropertyName))
                    {
                        continue;
                    }

                    object value = GetValue(column);
                    string showText = column.DoFormat(value);

                    Color color = UIStyles.GetStyleColor(column.Style).PanelForeColor;
                    SizeF sf = e.Graphics.MeasureString(showText, Font);
                    if (column.TextAlignment == StringAlignment.Near)
                        e.Graphics.DrawString(showText, Font, color, column.Bounds.X, 1 + (column.Bounds.Height - sf.Height) / 2.0f);
                    if (column.TextAlignment == StringAlignment.Center)
                        e.Graphics.DrawString(showText, Font, color, column.Bounds.X + (column.Bounds.Width - sf.Width) / 2.0f, 1 + (column.Bounds.Height - sf.Height) / 2.0f);
                    if (column.TextAlignment == StringAlignment.Far)
                        e.Graphics.DrawString(showText, Font, color, column.Bounds.Right - sf.Width - 1, 1 + (column.Bounds.Height - sf.Height) / 2.0f);
                }
            }

            if (Grid.ShowLine)
            {
                e.Graphics.DrawLine(Grid.LineColor, 0, Height - 1, Width, Height - 1);
            }
        }

        private object GetValue(UIGridColumn column)
        {
            object value = null;
            if (data is DataRow)
            {
                DataRow row = data as DataRow;
                value = row[column.DataPropertyName];
            }
            else
            {
                var cell = data.GetType().GetProperty(column.DataPropertyName);
                if (cell != null)
                {
                    value = cell.GetValue(data, null);
                }
            }

            return value;
        }

        private bool ExistDataPropertyName(string dataPropertyName)
        {
            if (dataPropertyName.IsNullOrEmpty()) return false;

            if (data is DataRow)
            {
                DataRow row = data as DataRow;
                return row.Table.Columns.Contains(dataPropertyName) && row[dataPropertyName] != null &&
                       row[dataPropertyName].ToString() != "";
            }
            else
            {
                var cell = data.GetType().GetProperty(dataPropertyName);
                return cell != null;
            }
        }

        private bool IsUpdateData;

        private void UpdateData()
        {
            IsUpdateData = true;
            foreach (var cell in Cells)
            {
                UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());

                if (!ExistDataPropertyName(column.DataPropertyName) || column.ColumnType == UIGridColumnType.Label)
                {
                    continue;
                }

                object value = GetValue(column);
                string showText = column.DoFormat(value);

                switch (column.ColumnType)
                {
                    case UIGridColumnType.CheckBox:
                        UICheckBox checkBox = (UICheckBox)cell;
                        if (value is int intValue)
                        {
                            checkBox.Checked = intValue == 1;
                        }

                        if (value is bool boolValue)
                        {
                            checkBox.Checked = boolValue;
                        }

                        break;

                    case UIGridColumnType.Combobox:
                        UIComboBox comboBox = (UIComboBox)cell;
                        List<object> items = column.GetComboboxItems();
                        for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
                        {
                            if (value.ToString().Equals(items[itemIndex].ToString()))
                            {
                                comboBox.SelectedIndex = itemIndex;
                            }
                        }

                        break;

                    case UIGridColumnType.Image:
                        PictureBox picture = (PictureBox)cell;
                        if (value is int imageIndex)
                        {
                            if (Grid.ImageList != null && imageIndex >= 0 &&
                                imageIndex < Grid.ImageList.Images.Count)
                            {
                                picture.Image = Grid.ImageList.Images[imageIndex];
                            }
                        }

                        break;

                    case UIGridColumnType.LinkLabel:
                        UILinkLabel linkLabel = (UILinkLabel)cell;
                        linkLabel.Text = showText;

                        break;

                    case UIGridColumnType.TextBox:
                        UITextBox textBox = (UITextBox)cell;
                        cell.Text = showText;
                        break;

                    case UIGridColumnType.IntTextBox:
                        UITextBox intTextBox = (UITextBox)cell;
                        intTextBox.IntValue = showText.ToInt();

                        break;

                    case UIGridColumnType.DoubleTextBox:
                        UITextBox doubleTextBox = (UITextBox)cell;
                        doubleTextBox.DoubleValue = showText.ToDouble();

                        break;
                }
            }

            IsUpdateData = false;
        }

        private void AddCells()
        {
            if (Grid.ColumnCount == 0)
            {
                return;
            }

            foreach (var column in Grid.Columns)
            {
                switch (column.ColumnType)
                {
                    case UIGridColumnType.Button:
                        UIButton button = new UIButton();
                        button.Style = column.Style;
                        button.StyleCustomMode = column.StyleCustomMode;
                        button.Text = column.ButtonText;
                        button.BackColor = Color.Transparent;
                        button.Click += Button_Click;
                        AddControl(button, column);
                        break;

                    case UIGridColumnType.CheckBox:
                        UICheckBox checkBox = new UICheckBox();
                        checkBox.Style = column.Style;
                        checkBox.StyleCustomMode = column.StyleCustomMode;
                        checkBox.BackColor = Color.Transparent;
                        checkBox.ReadOnly = column.ReadOnly;
                        checkBox.ValueChanged += CheckBox_ValueChanged;
                        AddControl(checkBox, column);
                        break;

                    case UIGridColumnType.Combobox:
                        UIComboBox comboBox = new UIComboBox();
                        comboBox.Style = column.Style;
                        comboBox.StyleCustomMode = column.StyleCustomMode;
                        comboBox.BackColor = Color.Transparent;
                        comboBox.DropDownStyle = UIDropDownStyle.DropDownList;

                        List<object> items = column.GetComboboxItems();
                        foreach (object item in items)
                        {
                            comboBox.Items.Add(item);
                        }

                        comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                        AddControl(comboBox, column);
                        break;

                    case UIGridColumnType.Image:
                        PictureBox picture = new PictureBox();
                        picture.BackColor = Color.Transparent;
                        picture.SizeMode = PictureBoxSizeMode.CenterImage;
                        AddControl(picture, column);
                        break;

                    case UIGridColumnType.LinkLabel:
                        UILinkLabel linkLabel = new UILinkLabel();
                        linkLabel.Style = column.Style;
                        linkLabel.StyleCustomMode = column.StyleCustomMode;
                        linkLabel.BackColor = Color.Transparent;
                        if (column.TextAlignment == StringAlignment.Near)
                            linkLabel.TextAlign = ContentAlignment.MiddleLeft;
                        if (column.TextAlignment == StringAlignment.Center)
                            linkLabel.TextAlign = ContentAlignment.MiddleCenter;
                        if (column.TextAlignment == StringAlignment.Far)
                            linkLabel.TextAlign = ContentAlignment.MiddleRight;

                        linkLabel.LinkClicked += LinkLabel_LinkClicked;
                        AddControl(linkLabel, column);
                        break;

                    case UIGridColumnType.TextBox:
                        UITextBox textBox = new UITextBox();
                        textBox.Style = column.Style;
                        textBox.StyleCustomMode = column.StyleCustomMode;
                        textBox.BackColor = Color.Transparent;
                        textBox.ReadOnly = column.ReadOnly;
                        textBox.MouseLeave += TextBox_MouseLeave;
                        AddControl(textBox, column);
                        break;

                    case UIGridColumnType.IntTextBox:
                        UITextBox intTextBox = new UITextBox();
                        intTextBox.Style = column.Style;
                        intTextBox.StyleCustomMode = column.StyleCustomMode;
                        intTextBox.Type = UITextBox.UIEditType.Integer;
                        intTextBox.BackColor = Color.Transparent;
                        intTextBox.ReadOnly = column.ReadOnly;
                        intTextBox.MouseLeave += IntTextBox_MouseLeave;
                        AddControl(intTextBox, column);
                        break;

                    case UIGridColumnType.DoubleTextBox:
                        UITextBox doubleTextBox = new UITextBox();
                        doubleTextBox.Style = column.Style;
                        doubleTextBox.StyleCustomMode = column.StyleCustomMode;
                        doubleTextBox.Type = UITextBox.UIEditType.Double;
                        doubleTextBox.BackColor = Color.Transparent;
                        doubleTextBox.ReadOnly = column.ReadOnly;
                        doubleTextBox.MouseLeave += DoubleTextBox_MouseLeave;
                        doubleTextBox.DecLength = column.TextBoxDecimals;
                        AddControl(doubleTextBox, column);
                        break;
                }
            }
        }

        private void CheckBox_ValueChanged(object sender, bool value)
        {
            if (IsUpdateData) return;

            UICheckBox cell = (UICheckBox)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellValueChange(Grid, column, this, data, cell.Checked);
        }

        private void ComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (IsUpdateData) return;

            UIComboBox cell = (UIComboBox)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellValueChange(Grid, column, this, data, cell.SelectedItem);
        }

        private void TextBox_MouseLeave(object sender, System.EventArgs e)
        {
            if (IsUpdateData) return;

            UITextBox cell = (UITextBox)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellValueChange(Grid, column, this, data, cell.Text);
        }

        private void IntTextBox_MouseLeave(object sender, System.EventArgs e)
        {
            if (IsUpdateData) return;

            UITextBox cell = (UITextBox)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellValueChange(Grid, column, this, data, cell.IntValue);
        }

        private void DoubleTextBox_MouseLeave(object sender, System.EventArgs e)
        {
            if (IsUpdateData) return;

            UITextBox cell = (UITextBox)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellValueChange(Grid, column, this, data, cell.DoubleValue);
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UILinkLabel cell = (UILinkLabel)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellLinkClick(Grid, column, this, data, cell.Text);
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            UIButton cell = (UIButton)sender;
            UIGridColumn column = Grid.GetColumnByGuid(cell.Tag.ToString());
            Grid.GridCellButtonClick(Grid, column, this, data, cell.Text);
        }

        private void Label_Click(object sender, System.EventArgs e)
        {
            Control ctrl = ((Control)sender).Parent;
            while (ctrl != null)
            {
                if (ctrl is UIRowsLayout panel)
                {
                    if (!panel.Focused)
                        panel.Focus();
                    break;
                }

                ctrl = ctrl.Parent;
            }

            Grid.SelectedRow = this;
        }

        private void AddControl(Control control, UIGridColumn column)
        {
            Cells.Add(control);
            control.Parent = this;
            control.Bounds = column.Bounds;
            control.Margin = new Padding(1);
            control.Tag = column.Guid;
            control.Click += Label_Click;
            control.MouseEnter += Control_MouseEnter;
            control.MouseLeave += Control_MouseLeave;
        }

        private void Control_MouseLeave(object sender, System.EventArgs e)
        {
            FillColor = GetRowColor();
        }

        private void Control_MouseEnter(object sender, System.EventArgs e)
        {
            if (State == UIGridRowState.None)
            {
                FillColor = Grid.RowSelectedColor;
            }
        }

        private int rowIndex;

        public int RowIndex
        {
            get => rowIndex;
            set
            {
                rowIndex = value;
                FillColor = GetRowColor();
            }
        }

        private Color GetRowColor()
        {
            switch (State)
            {
                case UIGridRowState.Info:
                    return UIStyles.Gray.GridSelectedColor;

                case UIGridRowState.Success:
                    return UIStyles.Green.GridSelectedColor;

                case UIGridRowState.Warning:
                    return UIStyles.Orange.GridSelectedColor;

                case UIGridRowState.Error:
                    return UIStyles.Red.GridSelectedColor;

                case UIGridRowState.None:
                default:
                    if (Selected)
                    {
                        return Grid.RowSelectedColor;
                    }

                    if (Grid.Stripe)
                    {
                        return rowIndex.IsEven() ? Grid.StripeEvenColor : Grid.StripeOddColor;
                    }
                    else
                    {
                        return Color.White;
                    }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // UIGridRow
            //
            this.Name = "UIGridRow";
            this.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.Size = new System.Drawing.Size(543, 32);
            this.Click += new System.EventHandler(this.Label_Click);
            this.ResumeLayout(false);
        }
    }

    public enum UIGridRowState
    {
        None,
        Info,
        Success,
        Warning,
        Error
    }
}