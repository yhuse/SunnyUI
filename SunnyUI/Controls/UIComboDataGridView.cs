/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIComboDataGridView.cs
 * 文件说明: 表格列表框
 * 当前版本: V3.0
 * 创建日期: 2021-09-01
 *
 * 2020-09-01: V3.0.6 增加文件说明
 * 2021-11-05: V3.0.8 增加过滤
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static Sunny.UI.UIDataGridView;

namespace Sunny.UI
{
    [DefaultProperty("ValueChanged")]
    [ToolboxItem(true)]
    public class UIComboDataGridView : UIDropControl, IToolTip
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UIComboTreeView
            // 
            this.DropDownStyle = UIDropDownStyle.DropDownList;
            this.Name = "UIComboTreeView";
            this.Padding = new Padding(0, 0, 30, 0);
            this.ButtonClick += UIComboDataGridView_ButtonClick;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void UIComboDataGridView_ButtonClick(object sender, EventArgs e)
        {
            item.FilterColomnName = FilterColomnName;
            item.ShowFilter = ShowFilter;
            ItemForm.Size = ItemSize;
            item.ShowButtons = true;
            item.SetDPIScale();
            item.Translate();
            ItemForm.Show(this);
        }

        [DefaultValue(typeof(Size), "320, 240"), Description("下拉弹框界面大小"), Category("SunnyUI")]
        public Size ItemSize { get; set; } = new Size(320, 240);

        public UIComboDataGridView()
        {
            InitializeComponent();
            fullControlSelect = true;
        }

        public Control ExToolTipControl()
        {
            return edit;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (item != null) item.DataGridView.Font = Font;
        }

        public bool ShowFilter { get; set; }

        private readonly UIComboDataGridViewItem item = new UIComboDataGridViewItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        public UIDataGridView DataGridView => item.DataGridView;

        public event OnSelectIndexChange SelectIndexChange;

        public delegate void OnValueChanged(object sender, object value);

        public event OnValueChanged ValueChanged;

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            if (ShowFilter)
                ValueChanged?.Invoke(this, value);
            else
                SelectIndexChange(this, value.ToString().ToInt());
        }

        public string FilterColomnName { get; set; }
    }
}
