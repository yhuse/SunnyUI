/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
 * 文件名称: UIColorPicker.cs
 * 文件说明: 颜色选择框
 * 当前版本: V3.1
 * 创建日期: 2020-05-29
 *
 * 2020-05-31: V2.2.5 增加文件
 * 2021-03-13: V3.0.2 增加单击事件以选中颜色
 * 2022-03-10: V3.1.1 修复选中颜色不显示
 ******************************************************************************
 * 文件名称: UIColorPicker.cs
 * 文件说明: Color picker with color wheel and eye dropper
 * 文件作者: jkristia
 * 开源协议: CPOL
 * 引用地址: https://www.codeproject.com/Articles/21965/Color-Picker-with-Color-Wheel-and-Eye-Dropper
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultProperty("ValueChanged")]
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
            this.Padding = new Padding(0, 0, 30, 0);
            this.ButtonClick += this.UIColorPicker_ButtonClick;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public delegate void OnColorChanged(object sender, Color value);

        public event OnColorChanged ValueChanged;

        public UIColorPicker()
        {
            InitializeComponent();
            Value = UIColor.Blue;
        }

        private void UIColorPicker_ButtonClick(object sender, EventArgs e)
        {
            item.SelectedColor = Value;
            item.Translate();
            item.SetDPIScale();
            ItemForm.Show(this);
        }

        [DefaultValue(false)]
        [Description("整个控件点击下拉选择颜色"), Category("SunnyUI")]
        public bool FullControlSelect
        {
            get => fullControlSelect;
            set => fullControlSelect = value;
        }

        protected override void ItemForm_ValueChanged(object sender, object value)
        {
            if (Value != (Color)value)
            {
                Value = (Color)value;
                Invalidate();
                ValueChanged?.Invoke(this, Value);
            }
        }

        private readonly UIColorItem item = new UIColorItem();

        protected override void CreateInstance()
        {
            ItemForm = new UIDropDown(item);
        }

        private Color selectColor;

        [DefaultValue(typeof(Color), "80, 160, 255")]
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

        protected override void OnPaintFore(Graphics g, System.Drawing.Drawing2D.GraphicsPath path)
        {
            base.OnPaintFore(g, path);
            if (Text.IsValid()) Text = "";
            var pathColor = g.CreateRoundedRectanglePath(new Rectangle(3, 3, Width - 32, Height - 7), 3, UICornerRadiusSides.All);
            g.FillPath(Value, pathColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DropDownStyle != UIDropDownStyle.DropDownList)
                DropDownStyle = UIDropDownStyle.DropDownList;
        }
    }
}