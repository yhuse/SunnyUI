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
 * 文件名称: UITransfer.cs
 * 文件说明: 穿梭框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-08-14: V2.2.7 增加左右列表项个数变化事件
 * 2021-07-18: V3.0.5 新增两个事件，可获取左侧、右侧Item点击事件
 * 2021-08-08: V3.0.5 增加了显示多个移动的属性
 * 2023-02-04: V3.3.1 支持鼠标框选和Shift，Ctrl多选移动
 * 2023-05-25: V3.3.7 增加列表框字体可调整
 * 2023-05-25: V3.3.7 增加列表框列表项高度可调整
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 穿梭框
    /// </summary>
    [DefaultProperty("ItemsLeft")]
    public sealed partial class UITransfer : UIPanel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UITransfer()
        {
            InitializeComponent();
            ShowText = false;
            SetStyleFlags(true, false);

            l1.ItemsCountChange += L1_ItemsCountChange;
            l2.ItemsCountChange += L2_ItemsCountChange;
            l1.ZoomScaleDisabled = l2.ZoomScaleDisabled = true;
            b1.ZoomScaleDisabled = b2.ZoomScaleDisabled = b3.ZoomScaleDisabled = b4.ZoomScaleDisabled = true;
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            l1.SetDPIScale();
            l2.SetDPIScale();
        }

        [DefaultValue(true)]
        [Description("显示多选按钮"), Category("SunnyUI")]
        public bool ShowMulti
        {
            get => b1.Visible;
            set => b1.Visible = b4.Visible = value;
        }

        private void L2_ItemsCountChange(object sender, EventArgs e)
        {
            ItemsRightCountChange?.Invoke(this, e);
        }

        private void L1_ItemsCountChange(object sender, EventArgs e)
        {
            ItemsLeftCountChange?.Invoke(this, e);
        }

        public event EventHandler ItemsLeftCountChange;
        public event EventHandler ItemsRightCountChange;
        /// <summary>
        /// 左侧列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("左侧列表"), Category("SunnyUI")]
        public ListBox.ObjectCollection ItemsLeft => l1.Items;

        /// <summary>
        /// 右侧列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("右侧列表"), Category("SunnyUI")]
        public ListBox.ObjectCollection ItemsRight => l2.Items;

        [Browsable(false)]
        public ListBox ListBoxLeft => l1.ListBox;

        [Browsable(false)]
        public ListBox ListBoxRight => l2.ListBox;

        public delegate void ItemChange(object sender, object item);

        public event ItemChange ItemAdd;
        public event ItemChange ItemRemove;

        private void b1_Click(object sender, EventArgs e)
        {
            foreach (object item in l1.Items)
            {
                l2.Items.Add(item);
                ItemAdd?.Invoke(this, item);
            }

            l1.Items.Clear();
            if (l2.Items.Count > 0)
            {
                l2.SelectedIndex = l2.Items.Count - 1;
            }
        }

        private void b2_Click(object sender, EventArgs e)
        {
            if (l1.Items.Count > 0 && l1.SelectedItems != null && l1.SelectedItems.Count > 0)
            {
                int idx = l1.SelectedIndices[l1.SelectedIndices.Count - 1];
                object[] items = new object[l1.SelectedItems.Count];
                for (int i = 0; i < l1.SelectedItems.Count; i++)
                {
                    items[i] = l1.SelectedItems[i];
                }

                foreach (var item in items)
                {
                    l2.Items.Add(item);
                    ItemAdd?.Invoke(this, item);
                    l1.Items.Remove(item);
                }

                l2.ClearSelected();
                if (l2.Items.Count > 0)
                {
                    l2.SelectedIndex = l2.Items.Count - 1;
                }

                if (idx >= l1.Items.Count) idx = l1.Items.Count;
                if (l1.Items.Count > 0)
                {
                    l1.SelectedIndex = Math.Max(0, idx - 1);
                }
            }
        }

        private void b3_Click(object sender, EventArgs e)
        {
            if (l2.Items.Count > 0 && l2.SelectedItems != null && l2.SelectedItems.Count > 0)
            {
                int idx = l2.SelectedIndices[l2.SelectedIndices.Count - 1];
                object[] items = new object[l2.SelectedItems.Count];
                for (int i = 0; i < l2.SelectedItems.Count; i++)
                {
                    items[i] = l2.SelectedItems[i];
                }

                foreach (var item in items)
                {
                    l1.Items.Add(item);
                    ItemRemove?.Invoke(this, item);
                    l2.Items.Remove(item);
                }

                l1.ClearSelected();
                if (l1.Items.Count > 0)
                {
                    l1.SelectedIndex = l1.Items.Count - 1;
                }

                if (idx >= l2.Items.Count) idx = l2.Items.Count;
                if (l2.Items.Count > 0)
                {
                    l2.SelectedIndex = Math.Max(0, idx - 1);
                }
            }
        }

        private void b4_Click(object sender, EventArgs e)
        {
            foreach (object item in l2.Items)
            {
                l1.Items.Add(item);
                ItemRemove?.Invoke(this, item);
            }

            l2.Items.Clear();
            if (l1.Items.Count > 0)
            {
                l1.SelectedIndex = l1.Items.Count - 1;
            }
        }

        private void l1_DoubleClick(object sender, EventArgs e)
        {
            b2_Click(null, null);
        }

        private void l2_DoubleClick(object sender, EventArgs e)
        {
            b3_Click(null, null);
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (b1 == null || b2 == null) return;
            if (b3 == null || b4 == null) return;
            if (l1 == null || l2 == null) return;

            l1.Width = l2.Width = Width / 2 - 40;
            b1.Left = b2.Left = b3.Left = b4.Left = (Width - b1.Width) / 2;
            b2.Top = Height / 2 - 8 - b2.Height;
            b1.Top = b2.Top - 16 - b1.Height;
            b3.Top = b2.Bottom + 16;
            b4.Top = b3.Bottom + 16;
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor"></param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (l1 == null || l2 == null) return;
            if (b1 == null || b2 == null || b3 == null || b4 == null) return;

            b1.SetStyleColor(uiColor);
            b2.SetStyleColor(uiColor);
            b3.SetStyleColor(uiColor);
            b4.SetStyleColor(uiColor);
            l1.SetStyleColor(uiColor);
            l2.SetStyleColor(uiColor);

            l1.BackColor = fillColor;
            l2.BackColor = fillColor;
        }

        /// <summary>
        /// 圆角切换事件
        /// </summary>
        /// <param name="value">圆角值</param>
        protected override void OnRadiusChanged(int value)
        {
            base.OnRadiusChanged(value);
            if (l1 == null || l2 == null) return;

            l1.Radius = value;
            l2.Radius = value;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (RadiusSides != UICornerRadiusSides.None)
                RadiusSides = UICornerRadiusSides.None;
            if (RectSides != ToolStripStatusLabelBorderSides.None)
                RectSides = ToolStripStatusLabelBorderSides.None;
        }

        public event EventHandler ItemsLeftClick;
        public event EventHandler ItemsRightClick;
        private void l1_ItemClick(object sender, EventArgs e)
        {
            ItemsLeftClick?.Invoke(this, e);
        }

        private void l2_ItemClick(object sender, EventArgs e)
        {
            ItemsRightClick?.Invoke(this, e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (DefaultFontSize < 0 && l1 != null) l1.Font = this.Font;
            if (DefaultFontSize < 0 && l2 != null) l2.Font = this.Font;
        }

        private int itemHeight = 25;

        [DefaultValue(25)]
        public int ItemHeight
        {
            get => itemHeight;
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    if (l1 == null || l2 == null) return;
                    l1.ItemHeight = l2.ItemHeight = itemHeight;
                }
            }
        }
    }
}