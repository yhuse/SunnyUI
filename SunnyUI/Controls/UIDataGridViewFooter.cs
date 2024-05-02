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
 * 文件名称: UIDataGridViewFooter
 * 文件说明: DataGridView页脚，可做统计显示
 * 当前版本: V3.1
 * 创建日期: 2021-04-20
 *
 * 2021-04-20: V3.0.3 增加文件说明
 * 2021-09-24: V3.0.7 文字显示方向与Column列显示方向一致
 * 2021-11-22: V3.0.9 修复一处可能不显示的问题 
 * 2022-09-05: V3.2.3 重构文字显示
 * 2023-05-15: V3.3.6 重构DrawString函数
******************************************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    public sealed class UIDataGridViewFooter : UIControl
    {
        public UIDataGridViewFooter()
        {
            SetStyleFlags(true, false, true);
            Height = 29;
            RadiusSides = UICornerRadiusSides.None;
            //Font = UIStyles.Font();
            foreColor = UIStyles.Blue.DataGridViewFooterForeColor;
            fillColor = UIStyles.Blue.PlainColor;
            rectColor = UIStyles.Blue.RectColor;
        }

        private UIDataGridView dgv;
        public UIDataGridView DataGridView
        {
            get => dgv;
            set
            {
                dgv = value;
                if (dgv != null)
                {
                    dgv.Paint += Dgv_Paint;
                }
            }
        }

        private void Dgv_Paint(object sender, PaintEventArgs e)
        {
            Invalidate();
        }

        public void Clear()
        {
            dictionary.Clear();
            Invalidate();
        }

        private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public string this[string name]
        {
            get => dictionary.ContainsKey(name) ? dictionary[name] : "";
            set
            {
                if (dictionary.NotContainsKey(name))
                    dictionary.Add(name, value);
                else
                    dictionary[name] = value;

                Invalidate();
            }
        }

        /// <summary>
        /// 绘制前景颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFore(Graphics g, GraphicsPath path)
        {
            if (dgv != null && dgv.ColumnCount > 0 && dgv.RowCount > 0)
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    bool ShowGridLine = dgv.CellBorderStyle == DataGridViewCellBorderStyle.Single;
                    Rectangle rect = dgv.GetColumnDisplayRectangle(column.Index, false);
                    if (rect.Width == 0) continue;
                    rect = new Rectangle(rect.Right - column.Width, rect.Top, column.Width, rect.Height);
                    int minleft = ShowGridLine ? 1 : 0;

                    if (rect.Left == minleft && rect.Width == 0) continue;
                    if (rect.Left >= minleft && ShowGridLine)
                    {
                        g.DrawLine(dgv.GridColor, rect.Left - minleft, 0, rect.Left - minleft, Height);
                        g.DrawLine(dgv.GridColor, rect.Right - minleft, 0, rect.Right - minleft, Height);
                    }

                    string str = this[column.Name];
                    if (str.IsNullOrEmpty()) continue;

                    var align = column.DefaultCellStyle.Alignment;
                    if (rect.Left == 0 && rect.Width == 0) continue;
                    switch (align)
                    {
                        case DataGridViewContentAlignment.NotSet:
                        case DataGridViewContentAlignment.TopLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.BottomLeft:
                            if (rect.Left == minleft && rect.Width < column.Width)
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Width - column.Width, 0, Width, Height), ContentAlignment.MiddleLeft);
                            }
                            else
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Left, 0, Width, Height), ContentAlignment.MiddleLeft);
                            }

                            break;
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.BottomCenter:
                            if (rect.Left == minleft && rect.Width < column.Width)
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Width - column.Width, 0, column.Width, Height), ContentAlignment.MiddleCenter);
                            }
                            else
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Left, 0, column.Width, Height), ContentAlignment.MiddleCenter);
                            }

                            break;
                        case DataGridViewContentAlignment.TopRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.BottomRight:
                            if (rect.Left == minleft && rect.Width < column.Width)
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Left, 0, column.Width, Height), ContentAlignment.MiddleRight);
                            }
                            else
                            {
                                g.DrawString(str, Font, ForeColor, new Rectangle(rect.Left, 0, column.Width, Height), ContentAlignment.MiddleRight);
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            foreColor = uiColor.DataGridViewFooterForeColor;
            fillColor = uiColor.PlainColor;
            rectColor = uiColor.RectColor;
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "243, 249, 255")]
        public Color FillColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }
    }
}
