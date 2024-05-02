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
 * 文件名称: UIComboDataGridView.cs
 * 文件说明: 表格列表框
 * 当前版本: V3.1
 * 创建日期: 2021-09-01
 *
 * 2020-09-01: V3.0.6 增加文件说明
 * 2021-11-05: V3.0.8 增加过滤
 * 2022-03-22: V3.1.1 增加自动过滤、单元格双击选中
 * 2022-04-16: V3.1.3 增加行多选
 * 2022-06-16: V3.2.0 增加下拉框宽度、高度
 * 2022-06-19: V3.2.0 增加FilterChanged，输出过滤文字和记录条数
 * 2022-09-08: V3.2.3 增加过滤字异常判断
 * 2022-11-03: V3.2.6 过滤时删除字符串前面、后面的空格
 * 2022-11-18: V3.2.9 增加过滤框输入逐一过滤属性Filter1by1
 * 2022-11-18: V3.2.9 过滤框输入增加回车确认
 * 2022-11-30: V3.3.0 增加Clear方法
 * 2023-07-25: V3.4.1 过滤输入后，按键盘下键切换至DataGridView，选中数据后按回车可快捷选中数据
 * 2023-09-25: V3.5.0 增加ClearFilter，可以清除弹窗的搜索栏文字
 * 2024-03-22: V3.6.5 增加ShowDropDown()
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

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            item?.Dispose();
            base.Dispose(disposing);
        }

        [DefaultValue(true), Description("过滤框输入逐一过滤"), Category("SunnyUI")]
        public bool Filter1by1 { get; set; } = true;

        [DefaultValue(false)]
        [Description("过滤时删除字符串前面、后面的空格"), Category("SunnyUI")]
        public bool TrimFilter { get; set; }

        public event OnComboDataGridViewFilterChanged FilterChanged;

        [DefaultValue(500)]
        [Description("下拉框宽度"), Category("SunnyUI")]
        public int DropDownWidth { get; set; }

        [DefaultValue(300)]
        [Description("下拉框高度"), Category("SunnyUI")]
        public int DropDownHeight { get; set; }

        private void UIComboDataGridView_ButtonClick(object sender, EventArgs e)
        {
            item.TrimFilter = TrimFilter;
            item.FilterColumnName = FilterColumnName;
            item.ShowFilter = ShowFilter;
            ItemForm.Size = ItemSize;
            item.ShowButtons = true;
            item.SetDPIScale();
            item.Translate();
            item.Filter1by1 = Filter1by1;
            //ItemForm.Show(this);
            ItemForm.Show(this, new Size(DropDownWidth < Width ? Width : DropDownWidth, DropDownHeight));
            item.ComboDataGridViewFilterChanged += Item_ComboDataGridViewFilterChanged;
        }

        public void ShowDropDown()
        {
            UIComboDataGridView_ButtonClick(this, EventArgs.Empty);
        }

        public override void Clear()
        {
            base.Clear();
            DataGridView.DataSource = null;
        }

        public void ClearFilter()
        {
            item.ClearFilter();
        }

        private void Item_ComboDataGridViewFilterChanged(object sender, UIComboDataGridViewArgs e)
        {
            FilterChanged?.Invoke(this, e);
        }

        [DefaultValue(typeof(Size), "320, 240"), Description("下拉弹框界面大小"), Category("SunnyUI")]
        public Size ItemSize { get; set; } = new Size(320, 240);

        public UIComboDataGridView()
        {
            InitializeComponent();
            fullControlSelect = true;
            CreateInstance();
            DropDownWidth = 500;
            DropDownHeight = 300;
        }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return edit;
        }

        [DefaultValue(false)]
        public bool ShowFilter { get; set; }

        private readonly UIComboDataGridViewItem item = new UIComboDataGridViewItem();

        /// <summary>
        /// 创建对象
        /// </summary>
        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        public UIDataGridView DataGridView => item.DataGridView;

        public event OnSelectIndexChange SelectIndexChange;

        public delegate void OnValueChanged(object sender, object value);
        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="value">值</param>
        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            if (DataGridView.MultiSelect)
            {
                ValueChanged?.Invoke(this, value);
            }
            else
            {
                if (ShowFilter)
                    ValueChanged?.Invoke(this, value);
                else
                    SelectIndexChange(this, value.ToString().ToInt());
            }
        }

        [DefaultValue(null)]
        public string FilterColumnName { get; set; }
    }
}
