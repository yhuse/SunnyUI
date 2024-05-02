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
 * 文件名称: UIToolStripDropDown.cs
 * 文件说明: 弹窗管理类
 * 当前版本: V3.1
 * 创建日期: 2021-07-10
 *
 * 2021-07-10: V3.0.4 增加文件说明
 * 2021-12-27: V3.0.9 增加一个显示方法
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIToolStripDropDown
    {
        private UIDropDown itemForm;

        public UIToolStripDropDown(UIDropDownItem item, bool autoClose = true)
        {
            itemForm = new UIDropDown(item);
            itemForm.ValueChanged += ItemForm_ValueChanged;
            itemForm.VisibleChanged += ItemForm_VisibleChanged;
            itemForm.Closed += ItemForm_Closed;
            itemForm.Opened += ItemForm_Opened;
            itemForm.Opening += ItemForm_Opening;
            itemForm.AutoClose = autoClose;
        }

        public Size ItemSize => itemForm.Size;

        public Rectangle ItemBounds => itemForm.Bounds;

        public void Close()
        {
            itemForm.Close();
        }

        private void ItemForm_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Opening?.Invoke(this, e);
        }

        private void ItemForm_Opened(object sender, EventArgs e)
        {
            Opened?.Invoke(this, e);
        }

        public event ToolStripDropDownClosedEventHandler Closed;
        public event UIDropDown.OnValueChanged ValueChanged;
        public event EventHandler VisibleChanged;
        public event EventHandler Opened;
        public event CancelEventHandler Opening;

        private void ItemForm_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        private void ItemForm_VisibleChanged(object sender, EventArgs e)
        {
            VisibleChanged?.Invoke(this, e);
        }

        private void ItemForm_ValueChanged(object sender, object value)
        {
            ValueChanged?.Invoke(this, value);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="area">区域</param>
        public void Show(Control control, Rectangle area, Point offset)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control, area, offset);
        }

        public void Show(Control control, Point offset)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control, offset);
        }

        public void Show(Control control, int x, int y)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control, x, y);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="size">大小</param>
        public void Show(Control control, Size size)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control, size);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="size">大小</param>
        public void Show(Control control, Size size, Point offset)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control, size, offset);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        public void Show(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            itemForm.Show(control);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="area">区域</param>
        public void Show(Rectangle area)
        {
            itemForm.Show(area);
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetRectColor(Color color)
        {
            itemForm.SetRectColor(color);
        }

        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetFillColor(Color color)
        {
            itemForm.SetFillColor(color);
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public void SetForeColor(Color color)
        {
            itemForm.SetForeColor(color);
        }

        public void SetStyle(UIBaseStyle style)
        {
            itemForm.SetStyle(style);
        }
    }

    public delegate void OnComboDataGridViewFilterChanged(object sender, UIComboDataGridViewArgs e);

    public class UIComboDataGridViewArgs : EventArgs
    {
        public string FilterText { get; set; }
        public int FilterCount { get; set; }

        public UIComboDataGridViewArgs()
        {

        }

        public UIComboDataGridViewArgs(string filterText, int filterCount)
        {
            FilterText = filterText;
            FilterCount = filterCount;
        }
    }

    public enum UIDropDownStyle
    {
        /// <summary>
        /// 通过单击下箭头指定显示列表，并指定文本部分可编辑。 这表示用户可以输入新的值，而不仅限于选择列表中现有的值。
        /// </summary>
        DropDown,
        /// <summary>
        /// 通过单击下箭头指定显示列表，并指定文本部分不可编辑。 这表示用户不能输入新的值。 只能选择列表中已有的值。
        /// </summary>
        DropDownList
    }
}
