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
 * 文件名称: UIFontImages.cs
 * 文件说明: 字体图片属性窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-01-28: V3.1.0 增加搜索框，搜索结果标红显示
 * 2023-04-23: V3.3.5 增加搜索结果显示页面
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 字体图标编辑器
    /// </summary>
    internal partial class UIFontImages : Form, ISymbol
    {
        private readonly ConcurrentQueue<Label> FontAwesomeV4Labels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> ElegantIconsLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> FontAwesomeV6SolidLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> FontAwesomeV6BrandsLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> FontAwesomeV6RegularLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> MaterialIconsLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> SearchLabels = new ConcurrentQueue<Label>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIFontImages()
        {
            InitializeComponent();
            lblResult.DoubleBuffered();
            lpAwesome.DoubleBuffered();
            lpElegant.DoubleBuffered();
            lpV6Brands.DoubleBuffered();
            lpV6Regular.DoubleBuffered();
            lpV6Solid.DoubleBuffered();
            lpMaterialIcons.DoubleBuffered();
        }

        private void UIFontImages_Load(object sender, EventArgs e)
        {
            AddHighFreqImage();
            bg1.RunWorkerAsync();
            bg2.RunWorkerAsync();
            bg3.RunWorkerAsync();
            bg4.RunWorkerAsync();
            bg5.RunWorkerAsync();
            bg6.RunWorkerAsync();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            while (!FontAwesomeV4Labels.IsEmpty)
            {
                if (FontAwesomeV4Labels.TryDequeue(out Label lbl))
                {
                    lpAwesome.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (!ElegantIconsLabels.IsEmpty)
            {
                if (ElegantIconsLabels.TryDequeue(out Label lbl))
                {
                    lpElegant.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (!FontAwesomeV6SolidLabels.IsEmpty)
            {
                if (FontAwesomeV6SolidLabels.TryDequeue(out Label lbl))
                {
                    lpV6Solid.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (!FontAwesomeV6RegularLabels.IsEmpty)
            {
                if (FontAwesomeV6RegularLabels.TryDequeue(out Label lbl))
                {
                    lpV6Regular.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (!FontAwesomeV6BrandsLabels.IsEmpty)
            {
                if (FontAwesomeV6BrandsLabels.TryDequeue(out Label lbl))
                {
                    lpV6Brands.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (!MaterialIconsLabels.IsEmpty)
            {
                if (MaterialIconsLabels.TryDequeue(out Label lbl))
                {
                    lpMaterialIcons.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            timer.Start();
        }

        private void AddHighFreqImage()
        {
            AddLabel(FontAwesomeIcons.fa_check);
            AddLabel(FontAwesomeIcons.fa_close);

            AddLabel(FontAwesomeIcons.fa_ellipsis_h);
            AddLabel(FontAwesomeIcons.fa_file);
            AddLabel(FontAwesomeIcons.fa_file_o);
            AddLabel(FontAwesomeIcons.fa_save);
            AddLabel(FontAwesomeIcons.fa_folder);
            AddLabel(FontAwesomeIcons.fa_folder_o);
            AddLabel(FontAwesomeIcons.fa_folder_open);
            AddLabel(FontAwesomeIcons.fa_folder_open_o);

            AddLabel(FontAwesomeIcons.fa_plus);
            AddLabel(FontAwesomeIcons.fa_edit);
            AddLabel(FontAwesomeIcons.fa_minus);
            AddLabel(FontAwesomeIcons.fa_refresh);

            AddLabel(FontAwesomeIcons.fa_exclamation);
            AddLabel(FontAwesomeIcons.fa_exclamation_circle);
            AddLabel(FontAwesomeIcons.fa_warning);
            AddLabel(FontAwesomeIcons.fa_info);

            AddLabel(FontAwesomeIcons.fa_info_circle);
            AddLabel(FontAwesomeIcons.fa_check_circle);
            AddLabel(FontAwesomeIcons.fa_check_circle_o);
            AddLabel(FontAwesomeIcons.fa_times_circle);
            AddLabel(FontAwesomeIcons.fa_times_circle_o);
            AddLabel(FontAwesomeIcons.fa_question);
            AddLabel(FontAwesomeIcons.fa_question_circle);
            AddLabel(FontAwesomeIcons.fa_question_circle_o);
            AddLabel(FontAwesomeIcons.fa_ban);

            AddLabel(FontAwesomeIcons.fa_toggle_left);
            AddLabel(FontAwesomeIcons.fa_toggle_right);
            AddLabel(FontAwesomeIcons.fa_toggle_up);
            AddLabel(FontAwesomeIcons.fa_toggle_down);

            AddLabel(FontAwesomeIcons.fa_lock);
            AddLabel(FontAwesomeIcons.fa_unlock);
            AddLabel(FontAwesomeIcons.fa_unlock_alt);

            AddLabel(FontAwesomeIcons.fa_cog);
            AddLabel(FontAwesomeIcons.fa_cogs);

            AddLabel(FontAwesomeIcons.fa_window_minimize);
            AddLabel(FontAwesomeIcons.fa_window_maximize);
            AddLabel(FontAwesomeIcons.fa_window_restore);
            AddLabel(FontAwesomeIcons.fa_window_close);
            AddLabel(FontAwesomeIcons.fa_window_close_o);

            AddLabel(FontAwesomeIcons.fa_user);
            AddLabel(FontAwesomeIcons.fa_user_o);
            AddLabel(FontAwesomeIcons.fa_user_circle);
            AddLabel(FontAwesomeIcons.fa_user_circle_o);
            AddLabel(FontAwesomeIcons.fa_user_plus);
            AddLabel(FontAwesomeIcons.fa_user_times);

            AddLabel(FontAwesomeIcons.fa_tag);
            AddLabel(FontAwesomeIcons.fa_tags);

            AddLabel(FontAwesomeIcons.fa_plus_circle);
            AddLabel(FontAwesomeIcons.fa_plus_square);
            AddLabel(FontAwesomeIcons.fa_plus_square_o);

            AddLabel(FontAwesomeIcons.fa_minus_circle);
            AddLabel(FontAwesomeIcons.fa_minus_square);
            AddLabel(FontAwesomeIcons.fa_minus_square_o);

            AddLabel(FontAwesomeIcons.fa_search);
            AddLabel(FontAwesomeIcons.fa_search_minus);
            AddLabel(FontAwesomeIcons.fa_search_plus);

            AddLabel(FontAwesomeIcons.fa_bar_chart);
            AddLabel(FontAwesomeIcons.fa_area_chart);
            AddLabel(FontAwesomeIcons.fa_line_chart);
            AddLabel(FontAwesomeIcons.fa_pie_chart);
            AddLabel(FontAwesomeIcons.fa_photo);

            AddLabel(FontAwesomeIcons.fa_power_off);
            AddLabel(FontAwesomeIcons.fa_print);
            AddLabel(FontAwesomeIcons.fa_bars);

            AddLabel(FontAwesomeIcons.fa_sign_in);
            AddLabel(FontAwesomeIcons.fa_sign_out);

            AddLabel(FontAwesomeIcons.fa_play);
            AddLabel(FontAwesomeIcons.fa_pause);
            AddLabel(FontAwesomeIcons.fa_stop);
            AddLabel(FontAwesomeIcons.fa_fast_backward);
            AddLabel(FontAwesomeIcons.fa_backward);
            AddLabel(FontAwesomeIcons.fa_forward);
            AddLabel(FontAwesomeIcons.fa_fast_forward);
            AddLabel(FontAwesomeIcons.fa_eject);
        }

        private void AddLabel(int icon)
        {
            Label lbl = CreateLabel(icon, UISymbolType.FontAwesomeV4);
            lpCustom.Controls.Add(lbl);
            SymbolValue symbol = (SymbolValue)lbl.Tag;
            toolTip.SetToolTip(lbl, symbol.ToString());
        }

        public class SymbolValue
        {
            /// <summary>
            /// 字体图标
            /// </summary>
            public int Symbol { get; set; }

            public UISymbolType SymbolType { get; set; }

            public string Name { get; set; }

            public bool IsRed { get; set; }

            public override string ToString()
            {
                if (Name.IsValid())
                    return Name + Environment.NewLine + (((int)SymbolType) * 100000 + Symbol).ToString();
                else
                    return (((int)SymbolType) * 100000 + Symbol).ToString();
            }
        }

        private Label CreateLabel(int icon, UISymbolType symbolType)
        {
            Label lbl = new Label
            {
                AutoSize = false,
                Size = new Size(32, 32),
                ForeColor = UIColor.Blue,
                Image = FontImageHelper.CreateImage(icon + (int)symbolType * 100000, 28, UIFontColor.Primary),
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(2)
            };

            lbl.DoubleBuffered();
            lbl.Click += lbl_DoubleClick;
            lbl.MouseEnter += Lbl_MouseEnter;
            lbl.MouseLeave += Lbl_MouseLeave;
            lbl.Tag = new SymbolValue() { Symbol = icon, SymbolType = symbolType };
            return lbl;
        }

        private Label CreateLabel(string name, int icon, UISymbolType symbolType)
        {
            Label lbl = new Label
            {
                AutoSize = false,
                Size = new Size(32, 32),
                ForeColor = UIColor.Blue,
                Image = FontImageHelper.CreateImage(icon + (int)symbolType * 100000, 28, UIFontColor.Primary),
                ImageAlign = ContentAlignment.MiddleCenter,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(2)
            };

            lbl.Click += lbl_DoubleClick;
            lbl.MouseEnter += Lbl_MouseEnter;
            lbl.MouseLeave += Lbl_MouseLeave;
            lbl.Tag = new SymbolValue() { Name = name.Replace("fa_", "").Replace("ma_", ""), Symbol = icon, SymbolType = symbolType };
            return lbl;
        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            SymbolValue symbol = (SymbolValue)lbl.Tag;
            if (symbol.IsRed) return;
            lbl.Image = FontImageHelper.CreateImage(symbol.Symbol + (int)symbol.SymbolType * 100000, 28, UIFontColor.Primary);
        }

        private void Lbl_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            SymbolValue symbol = (SymbolValue)lbl.Tag;
            if (symbol.IsRed) return;
            lbl.Image = FontImageHelper.CreateImage(symbol.Symbol + (int)symbol.SymbolType * 100000, 28, UIColor.Blue);
        }

        /// <summary>
        /// 字体图标
        /// </summary>
        public int Symbol { get; set; }

        public UISymbolType SymbolType { get; set; }

        /// <summary>
        /// 字体图标的偏移位置
        /// </summary>
        public Point SymbolOffset { get; set; }

        /// <summary>
        /// 字体图标大小
        /// </summary>
        public int SymbolSize { get; set; } = 28;

        /// <summary>
        /// 字体图标旋转角度
        /// </summary>
        public int SymbolRotate { get; set; } = 0;

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                SymbolValue symbol = (SymbolValue)lbl.Tag;
                SymbolType = symbol.SymbolType;
                Symbol = ((int)symbol.SymbolType) * 100000 + symbol.Symbol;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LoadLabels(Type type, ConcurrentQueue<Label> labels, UISymbolType symbolType, string filter = "")
        {
            ConcurrentDictionary<int, FieldInfo> dic = new ConcurrentDictionary<int, FieldInfo>();
            foreach (var fieldInfo in type.GetFields())
            {
                var obj = fieldInfo.GetRawConstantValue();
                if (obj is int value)
                {
                    dic.TryAdd(value, fieldInfo);
                }
            }

            List<int> list = dic.Keys.ToList();
            list.Sort();

            foreach (var value in list)
            {
                if (filter == "")
                    labels.Enqueue(CreateLabel(dic[value].Name, value, symbolType));
                else if (dic[value].Name.ToUpper().Contains(filter.ToUpper()))
                    labels.Enqueue(CreateLabel(dic[value].Name, value, symbolType));
            }

            dic.Clear();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            //public const int FontAwesomeV4Count = 786; 
            LoadLabels(typeof(FontAwesomeIcons), FontAwesomeV4Labels, UISymbolType.FontAwesomeV4);
        }

        private void bg2_DoWork(object sender, DoWorkEventArgs e)
        {
            //public const int ElegantIconsCount = 360;
            LoadLabels(typeof(FontElegantIcons), ElegantIconsLabels, UISymbolType.FontAwesomeV4);
        }

        private void bg3_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLabels(typeof(FontAweSomeV6Brands), FontAwesomeV6BrandsLabels, UISymbolType.FontAwesomeV6Brands);
        }

        private void bg4_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLabels(typeof(FontAweSomeV6Regular), FontAwesomeV6RegularLabels, UISymbolType.FontAwesomeV6Regular);
        }

        private void bg5_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLabels(typeof(FontAweSomeV6Solid), FontAwesomeV6SolidLabels, UISymbolType.FontAwesomeV6Solid);
        }

        private void bg6_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLabels(typeof(MaterialIcons), MaterialIconsLabels, UISymbolType.MaterialIcons);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IsNullOrEmpty()) return;
            lblResult.Controls.Clear();
            SearchLabels.Clear();

            LoadLabels(typeof(FontAwesomeIcons), SearchLabels, UISymbolType.FontAwesomeV4, textBox1.Text);
            LoadLabels(typeof(FontElegantIcons), SearchLabels, UISymbolType.FontAwesomeV4, textBox1.Text);
            LoadLabels(typeof(FontAweSomeV6Brands), SearchLabels, UISymbolType.FontAwesomeV6Brands, textBox1.Text);
            LoadLabels(typeof(FontAweSomeV6Regular), SearchLabels, UISymbolType.FontAwesomeV6Regular, textBox1.Text);
            LoadLabels(typeof(FontAweSomeV6Solid), SearchLabels, UISymbolType.FontAwesomeV6Solid, textBox1.Text);
            LoadLabels(typeof(MaterialIcons), SearchLabels, UISymbolType.MaterialIcons, textBox1.Text);

            label1.Text = SearchLabels.Count + " results.";
            while (!SearchLabels.IsEmpty)
            {
                if (SearchLabels.TryDequeue(out Label lbl))
                {
                    lblResult.Controls.Add(lbl);
                    SymbolValue symbol = (SymbolValue)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            tabControl1.SelectedTab = tabPage7;
        }

        private void UIFontImages_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }

    /// <summary>
    /// 字体图标属性编辑器
    /// </summary>
    public class UIImagePropertyEditor : UITypeEditor
    {
        /// <summary>
        /// GetEditStyle
        /// </summary>
        /// <param name="context">context</param>
        /// <returns>UITypeEditorEditStyle</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            //指定为模式窗体属性编辑器类型
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// EditValue
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="provider">provider</param>
        /// <param name="value">value</param>
        /// <returns>object</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //打开属性编辑器修改数据
            UIFontImages frm = new UIFontImages();
            if (frm.ShowDialog() == DialogResult.OK)
                value = frm.Symbol;
            frm.Dispose();
            return value;
        }
    }
}