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
 * 文件名称: UIDatePicker.cs
 * 文件说明: 颜色选择框
 * 当前版本: V2.2
 * 创建日期: 2020-05-29
 *
 * 2020-05-31: V2.2.5 增加文件
 ******************************************************************************
 * 文件名称: Color picker with color wheel and eye dropper
 * 文件说明: Color picker with color wheel and eye dropper
 * 文件作者: jkristia
 * 开源协议: CPOL
 * 引用地址: https://www.codeproject.com/Articles/21965/Color-Picker-with-Color-Wheel-and-Eye-Dropper
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIColorPicker : UIDropControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // UIColorPicker
            //
            this.DropDownStyle = UIDropDownStyle.DropDownList;
            this.Name = "UIColorPicker";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.ButtonClick += new System.EventHandler(this.UIColorPicker_ButtonClick);
            this.PaintOther += new System.Windows.Forms.PaintEventHandler(this.UIColorPicker_PaintOther);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public delegate void OnColorChanged(object sender, Color value);

        public event OnColorChanged ValueChanged;

        public UIColorPicker()
        {
            InitializeComponent();
            ShowText = false;
            Value = UIColor.Blue;
        }

        private void UIColorPicker_ButtonClick(object sender, EventArgs e)
        {
            item.SelectedColor = Value;
            ItemForm.Show(this);
        }

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            Value = (Color)value;
            Invalidate();
            ValueChanged?.Invoke(this, Value);
        }

        private readonly UIColorItem item = new UIColorItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        private Color selectColor;

        [DefaultValue(typeof(Color), "80, 159, 254")]
        [Description("选中颜色"), Category("SunnyUI")]
        public Color Value
        {
            get => selectColor;
            set
            {
                selectColor = value;
                item.SelectedColor = value;
                Invalidate();
            }
        }

        private void UIColorPicker_PaintOther(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var pathColor = e.Graphics.CreateRoundedRectanglePath(new Rectangle(3, 3, Width - 32, Height - 7), 5,
                UICornerRadiusSides.All);
            e.Graphics.FillPath(Value, pathColor);

            if (DropDownStyle != UIDropDownStyle.DropDownList)
                DropDownStyle = UIDropDownStyle.DropDownList;
        }
    }
}