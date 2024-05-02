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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 字体图标编辑器
    /// </summary>
    internal partial class UIFontImages : Form, ISymbol
    {
        private static UISymbolPanel customSymbolPanel;
        private static UISymbolPanel fontAwesomeV4;
        private static UISymbolPanel elegantIcons;
        private static UISymbolPanel fontAweSomeV6Regular;
        private static UISymbolPanel fontAweSomeV6Solid;
        private static UISymbolPanel fontAweSomeV6Brands;
        private static UISymbolPanel materialIcons;
        private static UISymbolPanel searchSymbolPanel;

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

            customSymbolPanel = new UISymbolPanel();
            fontAwesomeV4 = new UISymbolPanel(typeof(FontAwesomeIcons), UISymbolType.FontAwesomeV4);
            elegantIcons = new UISymbolPanel(typeof(FontElegantIcons), UISymbolType.FontAwesomeV4);
            fontAweSomeV6Regular = new UISymbolPanel(typeof(FontAweSomeV6Regular), UISymbolType.FontAwesomeV6Regular);
            fontAweSomeV6Solid = new UISymbolPanel(typeof(FontAweSomeV6Solid), UISymbolType.FontAwesomeV6Solid);
            fontAweSomeV6Brands = new UISymbolPanel(typeof(FontAweSomeV6Brands), UISymbolType.FontAwesomeV6Brands);
            materialIcons = new UISymbolPanel(typeof(MaterialIcons), UISymbolType.MaterialIcons);
            searchSymbolPanel = new UISymbolPanel();

            customSymbolPanel.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAwesomeV4.ValueChanged += CustomSymbolPanel_ValueChanged;
            elegantIcons.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Brands.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Regular.ValueChanged += CustomSymbolPanel_ValueChanged;
            fontAweSomeV6Solid.ValueChanged += CustomSymbolPanel_ValueChanged;
            materialIcons.ValueChanged += CustomSymbolPanel_ValueChanged;
            searchSymbolPanel.ValueChanged += CustomSymbolPanel_ValueChanged;
        }

        private void CustomSymbolPanel_ValueChanged(object sender, SymbolValue symbol)
        {
            SymbolType = symbol.SymbolType;
            Symbol = ((int)symbol.SymbolType) * 100000 + symbol.Symbol;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void UIFontImages_Load(object sender, EventArgs e)
        {
            AddHighFreqImage();
        }

        private void AddHighFreqImage()
        {
            customSymbolPanel.RowCount = 20;
            AddLabel(FontAwesomeIcons.fa_check, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_close, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_ellipsis_h, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_file, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_file_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_save, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_folder, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_folder_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_folder_open, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_folder_open_o, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_plus, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_edit, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_minus, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_refresh, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_exclamation, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_exclamation_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_warning, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_info, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_info_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_check_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_check_circle_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_times_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_times_circle_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_question, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_question_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_question_circle_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_ban, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_toggle_left, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_toggle_right, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_toggle_up, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_toggle_down, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_lock, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_unlock, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_unlock_alt, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_cog, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_cogs, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_window_minimize, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_window_maximize, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_window_restore, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_window_close, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_window_close_o, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_user, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_user_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_user_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_user_circle_o, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_user_plus, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_user_times, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_tag, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_tags, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_plus_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_plus_square, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_plus_square_o, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_minus_circle, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_minus_square, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_minus_square_o, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_search, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_search_minus, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_search_plus, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_bar_chart, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_area_chart, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_line_chart, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_pie_chart, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_photo, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_power_off, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_print, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_bars, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_sign_in, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_sign_out, UISymbolType.FontAwesomeV4);

            AddLabel(FontAwesomeIcons.fa_play, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_pause, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_stop, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_fast_backward, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_backward, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_forward, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_fast_forward, UISymbolType.FontAwesomeV4);
            AddLabel(FontAwesomeIcons.fa_eject, UISymbolType.FontAwesomeV4);
            customSymbolPanel.Invalidate();
        }

        private void AddLabel(int icon, UISymbolType symbolType)
        {
            customSymbolPanel.Add(new SymbolValue(icon, "", symbolType));
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

        private void button1_Click(object sender, EventArgs e)
        {
            string filter = textBox1.Text.Trim();
            fontAwesomeV4.Filter = filter;
            elegantIcons.Filter = filter;
            fontAweSomeV6Regular.Filter = filter;
            fontAweSomeV6Solid.Filter = filter;
            fontAweSomeV6Brands.Filter = filter;
            materialIcons.Filter = filter;
            searchSymbolPanel.Clear();
            if (filter.IsNullOrEmpty()) return;

            for (int i = 0; i < fontAwesomeV4.SymbolCount; i++)
            {
                var value = fontAwesomeV4.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < elegantIcons.SymbolCount; i++)
            {
                var value = elegantIcons.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Regular.SymbolCount; i++)
            {
                var value = fontAweSomeV6Regular.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Solid.SymbolCount; i++)
            {
                var value = fontAweSomeV6Solid.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < fontAweSomeV6Brands.SymbolCount; i++)
            {
                var value = fontAweSomeV6Brands.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            for (int i = 0; i < materialIcons.SymbolCount; i++)
            {
                var value = materialIcons.Get(i);
                if (value.Name.ToUpper().Contains(filter.ToUpper()))
                    searchSymbolPanel.Add(value);
            }

            searchSymbolPanel.Invalidate();
            tabControl1.SelectedTab = tabPage7;
        }

        private void UIFontImages_Shown(object sender, EventArgs e)
        {
            lpCustom.Controls.Add(customSymbolPanel);
            lpV6Solid.Controls.Add(fontAweSomeV6Solid);
            lpV6Regular.Controls.Add(fontAweSomeV6Regular);
            lpV6Brands.Controls.Add(fontAweSomeV6Brands);
            lpElegant.Controls.Add(elegantIcons);
            lpMaterialIcons.Controls.Add(materialIcons);
            lblResult.Controls.Add(searchSymbolPanel);
            lpAwesome.Controls.Add(fontAwesomeV4);
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