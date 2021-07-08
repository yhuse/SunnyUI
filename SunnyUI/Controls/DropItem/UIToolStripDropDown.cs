using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIToolStripDropDown
    {
        private UIDropDown itemForm;

        public UIToolStripDropDown(UIDropDownItem item)
        {
            itemForm = new UIDropDown(item);
            itemForm.ValueChanged += ItemForm_ValueChanged;
            itemForm.VisibleChanged += ItemForm_VisibleChanged;
            itemForm.Closed += ItemForm_Closed;
            itemForm.Opened += ItemForm_Opened;
            itemForm.Opening += ItemForm_Opening;
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
            itemForm.Show(control, area, offset);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="size">大小</param>
        public void Show(Control control, Size size)
        {
            itemForm.Show(control, size);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="size">大小</param>
        public void Show(Control control, Size size, Point offset)
        {
            itemForm.Show(control, size, offset);
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">Control</param>
        public void Show(Control control)
        {
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
}
