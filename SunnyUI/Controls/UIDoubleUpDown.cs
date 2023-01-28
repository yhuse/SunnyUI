/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UIDoubleUpDown.cs
 * 文件说明: 数字上下选择框
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-08-14: V2.2.7 增加字体调整
 * 2020-12-10: V3.0.9 增加Readonly属性
 * 2022-01-28: V3.1.0 修正默认值不为0时，编辑值为0的问题
 * 2022-02-07: V3.1.0 增加圆角控制
 * 2022-02-24: V3.1.1 可以设置按钮大小和颜色
 * 2022-05-05: V3.1.8 增加禁止输入属性
 * 2022-09-16: V3.2.4 增加是否可以双击输入属性
 * 2022-11-12: V3.2.8 修改浮点数大小离开判断为实时输入判断
 * 2022-11-12: V3.2.8 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性
 * 2023-01-28: V3.3.1 修改文本框数据输入数据变更事件为MouseLeave
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public sealed partial class UIDoubleUpDown : UIPanel, IToolTip
    {
        public delegate void OnValueChanged(object sender, double value);

        public UIDoubleUpDown()
        {
            InitializeComponent();

            SetStyleFlags();

            ShowText = false;
            edit.Type = UITextBox.UIEditType.Double;
            edit.Parent = pnlValue;
            edit.Visible = false;
            edit.BorderStyle = BorderStyle.None;
            edit.MouseLeave += Edit_Leave;
            pnlValue.Paint += PnlValue_Paint;
        }

        [DefaultValue(false)]
        [Description("禁止输入"), Category("SunnyUI")]
        public bool ForbidInput { get; set; }

        /// <summary>
        /// 需要额外设置ToolTip的控件
        /// </summary>
        /// <returns>控件</returns>
        public Control ExToolTipControl()
        {
            return pnlValue;
        }

        private void PnlValue_Paint(object sender, PaintEventArgs e)
        {
            if (Enabled)
            {
                e.Graphics.DrawLine(RectColor, 0, 0, pnlValue.Width, 0);
                e.Graphics.DrawLine(RectColor, 0, Height - 1, pnlValue.Width, Height - 1);
            }
            else
            {
                e.Graphics.DrawLine(RectDisableColor, 0, 0, pnlValue.Width, 0);
                e.Graphics.DrawLine(RectDisableColor, 0, Height - 1, pnlValue.Width, Height - 1);
            }
        }

        private void Edit_Leave(object sender, EventArgs e)
        {
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
                Value = edit.DoubleValue;
            }
        }

        /// <summary>
        /// 重载字体变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (pnlValue != null)
            {
                pnlValue.IsScaled = true;
                pnlValue.Font = Font;
            }

            if (edit != null)
            {
                edit.IsScaled = true;
                edit.Font = Font;
            }
        }

        public event OnValueChanged ValueChanged;

        private double _value;

        [DefaultValue(0D)]
        [Description("选中数值"), Category("SunnyUI")]
        public double Value
        {
            get => _value;
            set
            {
                value = edit.CheckMaxMin(value);
                if (_value != value)
                {
                    _value = value;
                    pnlValue.Text = _value.ToString("F" + decLength);
                    ValueChanged?.Invoke(this, _value);
                }
            }
        }

        private int decLength = 1;

        [Description("显示文字小数位数"), Category("SunnyUI")]
        [DefaultValue(1)]
        public int DecimalPlaces
        {
            get => decLength;
            set
            {
                decLength = Math.Max(value, 0);
                pnlValue.Text = _value.ToString("F" + decLength);
            }
        }

        private double step = 0.1;

        [DefaultValue(0.1)]
        [Description("步进值"), Category("SunnyUI")]
        public double Step
        {
            get => step;
            set => step = Math.Abs(value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ForbidInput) return;
            if (ReadOnly) return;

            Value += Step;
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
            }
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            if (ReadOnly) return;

            Value -= Step;
            if (edit.Visible)
            {
                edit.Visible = false;
                pnlValue.FillColor = pnlColor;
            }
        }

        [DefaultValue(typeof(double), "2147483647")]
        [Description("最大值"), Category("SunnyUI")]
        public double Maximum
        {
            get => edit.MaxValue;
            set => edit.MaxValue = value;
        }

        [DefaultValue(typeof(double), "-2147483648")]
        [Description("最小值"), Category("SunnyUI")]
        public double Minimum
        {
            get => edit.MinValue;
            set => edit.MinValue = value;
        }

        [DefaultValue(true)]
        [Description("是否可以双击输入"), Category("SunnyUI")]
        public bool Inputable { get; set; } = true;

        private readonly UIEdit edit = new UIEdit();
        private Color pnlColor;
        private void pnlValue_DoubleClick(object sender, EventArgs e)
        {
            if (ReadOnly) return;
            if (!Inputable) return;

            edit.Left = 1;
            edit.Top = (pnlValue.Height - edit.Height) / 2;
            edit.Width = pnlValue.Width - 2;
            pnlColor = pnlValue.FillColor;
            pnlValue.FillColor = Color.White;
            edit.TextAlign = HorizontalAlignment.Center;
            edit.DecLength = decLength;
            edit.DoubleValue = Value;
            edit.BringToFront();
            edit.Visible = true;
            edit.Focus();
            edit.SelectAll();
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly { get; set; }

        protected override void OnRadiusSidesChange()
        {
            if (btnDec == null || btnAdd == null) return;

            btnDec.RadiusSides =
                 (RadiusSides.HasFlag(UICornerRadiusSides.LeftTop) ? UICornerRadiusSides.LeftTop : UICornerRadiusSides.None) |
                 (RadiusSides.HasFlag(UICornerRadiusSides.LeftBottom) ? UICornerRadiusSides.LeftBottom : UICornerRadiusSides.None);
            btnAdd.RadiusSides =
                (RadiusSides.HasFlag(UICornerRadiusSides.RightTop) ? UICornerRadiusSides.RightTop : UICornerRadiusSides.None) |
                (RadiusSides.HasFlag(UICornerRadiusSides.RightBottom) ? UICornerRadiusSides.RightBottom : UICornerRadiusSides.None);
        }

        protected override void OnRadiusChanged(int value)
        {
            if (btnDec == null || btnAdd == null) return;
            btnDec.Radius = btnAdd.Radius = value;
        }

        /// <summary>
        /// 重载控件尺寸变更
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Height < UIGlobal.EditorMinHeight) Height = UIGlobal.EditorMinHeight;
            if (Height > UIGlobal.EditorMaxHeight) Height = UIGlobal.EditorMaxHeight;
        }

        protected override void AfterSetRectColor(Color color)
        {
            base.AfterSetRectColor(color);
            if (btnAdd == null || btnDec == null) return;
            btnAdd.FillColor = btnDec.FillColor = color;
            btnAdd.RectColor = btnDec.RectColor = color;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            if (pnlValue == null) return;
            pnlValue.FillColor = color;
        }

        private int buttonWidth = 29;
        [DefaultValue(29)]
        public int ButtonWidth
        {
            get => buttonWidth;
            set
            {
                buttonWidth = Math.Max(value, 29);
                if (btnAdd == null || btnDec == null) return;
                btnAdd.Width = btnDec.Width = buttonWidth;
            }
        }

        public override Color ForeColor { get => pnlValue.ForeColor; set => pnlValue.ForeColor = value; }
    }
}