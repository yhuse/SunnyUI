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
 * 文件名称: UIComboBoxItem.cs
 * 文件说明: 组合框弹出窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Drawing;

namespace Sunny.UI
{
    /// <summary>
    /// 组合框弹出窗体
    /// </summary>
    public partial class UIComboBoxItem : UIDropDownItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIComboBoxItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 列表框
        /// </summary>
        public UIListBox ListBox => listBox;

        private void ListBox_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
            {
                BeforeListClick?.Invoke(listBox.SelectedIndex, EventArgs.Empty);
                Close();
            }
        }

        public event EventHandler BeforeListClick;

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public override void SetRectColor(Color color)
        {
            //listBox.ItemSelectBackColor = color;
        }

        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public override void SetFillColor(Color color)
        {
            //ListBox.ItemSelectForeColor = color;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoValueChanged(this, ListBox.SelectedValue);
        }

        private void listBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter || e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                Close();
            }
        }
    }
}