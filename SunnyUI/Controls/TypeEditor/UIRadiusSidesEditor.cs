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
 * 文件名称: UIRadiusSidesEditor.cs
 * 文件说明: 边框属性类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
#pragma warning disable SYSLIB0003 // 类型或成员已过时

namespace Sunny.UI
{
    /// <summary>
    /// 圆角位置
    /// </summary>
    [ComVisible(true)]
    [ToolboxItem(false)]
    [Editor("Sunny.UI.UIRadiusSidesEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
    [Flags]
    public enum UICornerRadiusSides
    {
        /// <summary>
        /// 四个角都有圆角。
        /// </summary>
        All = 15, // 0x0000000F

        /// <summary>
        /// 左下角有圆角
        /// </summary>
        LeftBottom = 8,

        /// <summary>
        /// 左上角有圆角
        /// </summary>
        LeftTop = 1,

        /// <summary>
        /// 右下角有圆角
        /// </summary>
        RightBottom = 4,

        /// <summary>
        /// 右上角有圆角
        /// </summary>
        RightTop = 2,

        /// <summary>
        /// 没有圆角
        /// </summary>
        None = 0,
    }

    /// <summary>
    ///   提供设置的编辑器 <see cref="P:System.Windows.Forms.ToolStripStatusLabel.RectSides" /> 属性。
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class UIRadiusSidesEditor : UIDropEditor
    {
        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <returns>窗体</returns>
        protected override UIDropEditorUI CreateUI()
        {
            return new UICornerRadiusSidesUI();
        }

        /// <summary>
        /// 属性窗体
        /// </summary>
        [ToolboxItem(false)]
        public class UICornerRadiusSidesUI : UIDropEditorUI
        {
            private TableLayoutPanel tableLayoutPanel1;
            private CheckBox allCheckBox;
            private CheckBox noneCheckBox;
            private CheckBox leftTopCheckBox;
            private CheckBox rightTopCheckBox;
            private CheckBox rightBottomCheckBox;
            private CheckBox leftBottomCheckBox;
            private Label splitterLabel;
            private bool allChecked;
            private bool noneChecked;

            /// <summary>
            /// 构造函数
            /// </summary>
            public UICornerRadiusSidesUI()
            {
                End();
                InitializeComponent();
                Size = PreferredSize;
            }

            /// <summary>
            /// 初始化数值
            /// </summary>
            /// <param name="value">数值</param>
            protected override void InitValue(object value)
            {
                UICornerRadiusSides sides = (UICornerRadiusSides)value;
                ResetCheckBoxState();
                if ((sides & UICornerRadiusSides.All) == UICornerRadiusSides.All)
                {
                    allCheckBox.Checked = true;
                    leftTopCheckBox.Checked = true;
                    rightTopCheckBox.Checked = true;
                    rightBottomCheckBox.Checked = true;
                    leftBottomCheckBox.Checked = true;
                    allCheckBox.Checked = true;
                }
                else
                {
                    noneCheckBox.Checked = sides == UICornerRadiusSides.None;
                    leftTopCheckBox.Checked = (sides & UICornerRadiusSides.LeftTop) == UICornerRadiusSides.LeftTop;
                    rightTopCheckBox.Checked = (sides & UICornerRadiusSides.RightTop) == UICornerRadiusSides.RightTop;
                    rightBottomCheckBox.Checked = (sides & UICornerRadiusSides.RightBottom) == UICornerRadiusSides.RightBottom;
                    leftBottomCheckBox.Checked = (sides & UICornerRadiusSides.LeftBottom) == UICornerRadiusSides.LeftBottom;
                }
            }

            /// <summary>
            /// 更新当前值
            /// </summary>
            protected override void UpdateCurrentValue()
            {
                if (!updateCurrentValue)
                    return;
                UICornerRadiusSides labelBorderSides = UICornerRadiusSides.None;
                if (allCheckBox.Checked)
                {
                    currentValue = labelBorderSides | UICornerRadiusSides.All;
                    allChecked = true;
                    noneChecked = false;
                }
                else
                {
                    if (noneCheckBox.Checked)
                        labelBorderSides |= UICornerRadiusSides.None;

                    if (leftTopCheckBox.Checked)
                        labelBorderSides |= UICornerRadiusSides.LeftTop;

                    if (rightTopCheckBox.Checked)
                        labelBorderSides |= UICornerRadiusSides.RightTop;

                    if (rightBottomCheckBox.Checked)
                        labelBorderSides |= UICornerRadiusSides.RightBottom;

                    if (leftBottomCheckBox.Checked)
                        labelBorderSides |= UICornerRadiusSides.LeftBottom;

                    if (labelBorderSides == UICornerRadiusSides.None)
                    {
                        allChecked = false;
                        noneChecked = true;
                        noneCheckBox.Checked = true;
                    }

                    if (labelBorderSides == UICornerRadiusSides.All)
                    {
                        allChecked = true;
                        noneChecked = false;
                        allCheckBox.Checked = true;
                    }

                    currentValue = labelBorderSides;
                }
            }

            /// <summary>
            /// 激活
            /// </summary>
            /// <param name="e"></param>
            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                noneCheckBox.Focus();
            }

            private void InitializeComponent()
            {
                ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BorderSidesEditor));
                Height = 163;
                tableLayoutPanel1 = new TableLayoutPanel();
                noneCheckBox = new CheckBox();
                allCheckBox = new CheckBox();
                leftTopCheckBox = new CheckBox();
                rightTopCheckBox = new CheckBox();
                leftBottomCheckBox = new CheckBox();
                rightBottomCheckBox = new CheckBox();
                splitterLabel = new Label();
                tableLayoutPanel1.SuspendLayout();
                SuspendLayout();
                componentResourceManager.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
                tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                tableLayoutPanel1.BackColor = SystemColors.Window;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
                tableLayoutPanel1.Controls.Add(noneCheckBox, 0, 0);
                tableLayoutPanel1.Controls.Add(allCheckBox, 0, 2);
                tableLayoutPanel1.Controls.Add(leftTopCheckBox, 0, 3);
                tableLayoutPanel1.Controls.Add(rightTopCheckBox, 0, 4);
                tableLayoutPanel1.Controls.Add(leftBottomCheckBox, 0, 5);
                tableLayoutPanel1.Controls.Add(rightBottomCheckBox, 0, 6);
                tableLayoutPanel1.Controls.Add(splitterLabel, 0, 1);
                tableLayoutPanel1.Name = "tableLayoutPanel1";
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.RowStyles.Add(new RowStyle());
                tableLayoutPanel1.Margin = new Padding(0);

                noneCheckBox.Name = "noneCheckBox";
                noneCheckBox.Margin = new Padding(3, 3, 3, 1);
                noneCheckBox.Text = "无(&N)";
                noneCheckBox.Height = 20;

                allCheckBox.Name = "allCheckBox";
                allCheckBox.Margin = new Padding(3, 3, 3, 1);
                allCheckBox.Text = "全部(&A)";
                allCheckBox.Height = 20;

                leftTopCheckBox.Margin = new Padding(20, 1, 3, 1);
                leftTopCheckBox.Name = "leftTopCheckBox";
                leftTopCheckBox.Text = "左上角(&LT)";
                leftTopCheckBox.Height = 20;

                rightTopCheckBox.Margin = new Padding(20, 1, 3, 1);
                rightTopCheckBox.Name = "rightTopCheckBox";
                rightTopCheckBox.Text = "右上角(&RT)";
                rightTopCheckBox.Height = 20;

                leftBottomCheckBox.Margin = new Padding(20, 1, 3, 1);
                leftBottomCheckBox.Name = "leftBottomCheckBox";
                leftBottomCheckBox.Text = "左下角(&LB)";
                leftBottomCheckBox.Height = 20;

                rightBottomCheckBox.Margin = new Padding(20, 1, 3, 1);
                rightBottomCheckBox.Name = "rightBottomCheckBox";
                rightBottomCheckBox.Text = "右下角(&RB)";
                rightBottomCheckBox.Height = 20;

                splitterLabel.BackColor = SystemColors.ControlDark;
                splitterLabel.Name = "splitterLabel";
                splitterLabel.Height = 1;

                Controls.Add(tableLayoutPanel1);
                Padding = new Padding(1, 1, 1, 1);
                AutoSizeMode = AutoSizeMode.GrowAndShrink;
                AutoScaleMode = AutoScaleMode.None;
                AutoScaleDimensions = new SizeF(6f, 13f);
                tableLayoutPanel1.ResumeLayout(false);
                tableLayoutPanel1.PerformLayout();
                ResumeLayout(false);
                PerformLayout();
                leftBottomCheckBox.CheckedChanged += leftBottomCheckBox_CheckedChanged;
                rightBottomCheckBox.CheckedChanged += rightBottomCheckBox_CheckedChanged;
                rightTopCheckBox.CheckedChanged += rightTopCheckBox_CheckedChanged;
                leftTopCheckBox.CheckedChanged += leftTopBox_CheckedChanged;
                noneCheckBox.CheckedChanged += noneCheckBox_CheckedChanged;
                allCheckBox.CheckedChanged += allCheckBox_CheckedChanged;
                noneCheckBox.Click += noneCheckBoxClicked;
                allCheckBox.Click += allCheckBoxClicked;
            }

            private void leftBottomCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                    noneCheckBox.Checked = false;
                else if (allCheckBox.Checked)
                    allCheckBox.Checked = false;
                UpdateCurrentValue();
            }

            private void rightBottomCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                    noneCheckBox.Checked = false;
                else if (allCheckBox.Checked)
                    allCheckBox.Checked = false;
                UpdateCurrentValue();
            }

            private void rightTopCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                    noneCheckBox.Checked = false;
                else if (allCheckBox.Checked)
                    allCheckBox.Checked = false;
                UpdateCurrentValue();
            }

            private void leftTopBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                    noneCheckBox.Checked = false;
                else if (allCheckBox.Checked)
                    allCheckBox.Checked = false;
                UpdateCurrentValue();
            }

            private void noneCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                {
                    allCheckBox.Checked = false;
                    leftTopCheckBox.Checked = false;
                    rightTopCheckBox.Checked = false;
                    rightBottomCheckBox.Checked = false;
                    leftBottomCheckBox.Checked = false;
                }
                UpdateCurrentValue();
            }

            private void allCheckBox_CheckedChanged(object sender, EventArgs e)
            {
                if (((CheckBox)sender).Checked)
                {
                    noneCheckBox.Checked = false;
                    leftTopCheckBox.Checked = true;
                    rightTopCheckBox.Checked = true;
                    rightBottomCheckBox.Checked = true;
                    leftBottomCheckBox.Checked = true;
                }
                UpdateCurrentValue();
            }

            private void noneCheckBoxClicked(object sender, EventArgs e)
            {
                if (!noneChecked)
                    return;
                noneCheckBox.Checked = true;
            }

            private void allCheckBoxClicked(object sender, EventArgs e)
            {
                if (!allChecked)
                    return;
                allCheckBox.Checked = true;
            }

            private void ResetCheckBoxState()
            {
                allCheckBox.Checked = false;
                noneCheckBox.Checked = false;
                leftTopCheckBox.Checked = false;
                rightTopCheckBox.Checked = false;
                rightBottomCheckBox.Checked = false;
                leftBottomCheckBox.Checked = false;
            }
        }
    }

    /// <summary>
    /// 圆角位置属性编辑器帮助类
    /// </summary>
    public static class UICornerRadiusSidesHelper
    {
        /// <summary>
        /// 获取圆角是否设置
        /// </summary>
        /// <param name="sides">所有圆角</param>
        /// <param name="side">当前圆角</param>
        /// <returns>是否有当前圆角</returns>
        public static bool GetValue(this UICornerRadiusSides sides, UICornerRadiusSides side)
        {
            return (sides & side) == side;
        }

        /// <summary>
        /// 获取边框是否设置
        /// </summary>
        /// <param name="sides">所有边框</param>
        /// <param name="side">当前边框</param>
        /// <returns>是否有当前边框</returns>
        public static bool GetValue(this ToolStripStatusLabelBorderSides sides, ToolStripStatusLabelBorderSides side)
        {
            return (sides & side) == side;
        }
    }
}