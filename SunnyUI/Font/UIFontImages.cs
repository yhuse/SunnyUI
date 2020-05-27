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
 * 文件名称: UIFontImages.cs
 * 文件说明: 字体图片属性窗体
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 字体图标编辑器
    /// </summary>
    public partial class UIFontImages : Form
    {
        private readonly ConcurrentQueue<Label> AwesomeLabels = new ConcurrentQueue<Label>();
        private readonly ConcurrentQueue<Label> ElegantLabels = new ConcurrentQueue<Label>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIFontImages()
        {
            InitializeComponent();
        }

        private void UIFontImages_Load(object sender, EventArgs e)
        {
            AddHighFreqImage();
            bg.RunWorkerAsync();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            while (AwesomeLabels.Count > 0)
            {
                if (AwesomeLabels.TryDequeue(out Label lbl))
                {
                    lpAwesome.Controls.Add(lbl);
                    int symbol = (int)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            while (ElegantLabels.Count > 0)
            {
                if (ElegantLabels.TryDequeue(out Label lbl))
                {
                    lpElegant.Controls.Add(lbl);
                    int symbol = (int)lbl.Tag;
                    toolTip.SetToolTip(lbl, symbol.ToString());
                }
            }

            timer.Start();
        }

        private void AddAwesomeImageEx()
        {
            Type t = typeof(FontAwesomeIcons);
            FieldInfo[] fis = t.GetFields();
            foreach (var fieldInfo in fis)
            {
                int value = fieldInfo.GetRawConstantValue().ToString().ToInt();
                AwesomeLabels.Enqueue(CreateLabel(value));
            }
        }

        private void AddElegantImageEx()
        {
            Type t = typeof(FontElegantIcons);
            FieldInfo[] fis = t.GetFields();
            foreach (var fieldInfo in fis)
            {
                int value = fieldInfo.GetRawConstantValue().ToString().ToInt();
                ElegantLabels.Enqueue(CreateLabel(value));
            }
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
            Label lbl = CreateLabel(icon);
            lpCustom.Controls.Add(lbl);
            int symbol = (int)lbl.Tag;
            toolTip.SetToolTip(lbl, symbol.ToString());
        }

        private Label CreateLabel(int icon)
        {
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.Size = new Size(32, 32);
            lbl.ForeColor = UIColor.Blue;

            FontImageHelper.AwesomeFont.ForeColor = UIFontColor.Primary;
            FontImageHelper.ElegantFont.ForeColor = UIFontColor.Primary;

            lbl.Image = icon >= 0xF000 ?
                        FontImageHelper.AwesomeFont.GetImage(icon, 28) :
                        FontImageHelper.ElegantFont.GetImage(icon, 28);
            lbl.ImageAlign = ContentAlignment.MiddleCenter;
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            lbl.Margin = new Padding(2);
            lbl.Click += lbl_DoubleClick;
            lbl.MouseEnter += Lbl_MouseEnter;
            lbl.MouseLeave += Lbl_MouseLeave;
            lbl.Tag = icon;
            return lbl;
        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            FontImageHelper.AwesomeFont.ForeColor = UIFontColor.Primary;
            FontImageHelper.ElegantFont.ForeColor = UIFontColor.Primary;
            int icon = (int)lbl.Tag;
            lbl.Image = icon >= 0xF000 ?
                        FontImageHelper.AwesomeFont.GetImage(icon, 28) :
                        FontImageHelper.ElegantFont.GetImage(icon, 28);
        }

        private void Lbl_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            FontImageHelper.AwesomeFont.ForeColor = UIColor.Blue;
            FontImageHelper.ElegantFont.ForeColor = UIColor.Blue;
            int icon = (int)lbl.Tag;
            lbl.Image = icon >= 0xF000 ?
                        FontImageHelper.AwesomeFont.GetImage(icon, 28) :
                        FontImageHelper.ElegantFont.GetImage(icon, 28);
        }

        /// <summary>
        /// 选中图标
        /// </summary>
        public int SelectSymbol { get; private set; }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label lbl) SelectSymbol = (int)lbl.Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            AddAwesomeImageEx();
            //scoreStep = 1;
            AddElegantImageEx();
        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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
                value = frm.SelectSymbol;
            frm.Dispose();
            return value;
        }
    }
}