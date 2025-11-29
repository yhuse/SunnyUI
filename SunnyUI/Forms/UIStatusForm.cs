/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: UIStatusForm.cs
 * 文件说明: 进度提示窗体
 * 当前版本: V3.1
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 增加文件
 * 2025-09-15: V3.8.8 进度提示框增加取消功能。#IA5Q0Z
 * 2025-11-29: V3.9.0 优化多行文本显示时的布局调整，支持任意行数的文本内容显示。
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed partial class UIStatusForm : UIForm
    {
        public UIStatusForm()
        {
            InitializeComponent();
            Text = UIStyles.CurrentResources.InfoTitle;
            Description = UIStyles.CurrentResources.SystemProcessing;
            timer1.Start();
        }

        public UIStatusForm(int max, string desc, int decimalPlaces = 1)
        {
            InitializeComponent();
            Text = UIStyles.CurrentResources.InfoTitle;
            Maximum = max;
            Description = desc;
            Value = 0;
            DecimalPlaces = decimalPlaces;
            timer1.Start();
        }

        [DefaultValue(100)]
        public int Maximum
        {
            get => processBar.Maximum;
            set => processBar.Maximum = value;
        }

        [DefaultValue(0)]
        public int Value
        {
            get => processBar.Value;
            set => processBar.Value = value;
        }

        [DefaultValue(1)]
        public int Step
        {
            get => processBar.Step;
            set => processBar.Step = value;
        }

        [DefaultValue(true)]
        public bool ShowValue
        {
            get => processBar.ShowValue;
            set => processBar.ShowValue = value;
        }

        /// <summary>
        /// 进度到达最大值时自动隐藏
        /// </summary>
        [DefaultValue(true)]
        public bool MaxAutoHide { get; set; } = true;

        private void processBar_ValueChanged(object sender, int value)
        {
            if (MaxAutoHide && value == Maximum)
            {
                Close();
            }
        }

        public void Show(string title, string desc, int max = 100, int value = 0)
        {
            Text = title;
            labelDescription.Text = desc;
            processBar.Maximum = max;
            processBar.Value = value;
            Show();
        }

        public string Description
        {
            get => labelDescription.Text;
            set => labelDescription.Text = value;
        }

        public int DecimalPlaces
        {
            get => processBar.DecimalPlaces;
            set => processBar.DecimalPlaces = value;
        }

        private delegate void SetTextHandler(string text);

        public void SetDescription(string text)
        {
            if (labelDescription.InvokeRequired)
            {
                Invoke(new SetTextHandler(SetDescription), text);
            }
            else
            {
                labelDescription.Text = text;

                // 动态调整布局以适应多行文本
                AdjustLayoutForMultiLineText();

                labelDescription.Invalidate();
            }
        }

        /// <summary>
        /// 根据文本内容动态调整布局，确保多行文本（任意行数）不被进度条遮住
        /// <para>实现方式：</para>
        /// <para>1. 计算文本实际需要的高度（考虑换行，支持任意行数）</para>
        /// <para>2. 动态调整 labelDescription 的高度以适应文本内容</para>
        /// <para>3. 动态调整 processBar 的位置，确保在 labelDescription 下方</para>
        /// <para>4. 自动增加窗体高度以容纳所有内容（无论多少行）</para>
        /// <para>5. 单行文本时恢复默认布局</para>
        /// </summary>
        private void AdjustLayoutForMultiLineText()
        {
            try
            {
                if (string.IsNullOrEmpty(labelDescription.Text))
                {
                    // 空文本时恢复默认布局
                    RestoreDefaultLayout();
                    return;
                }

                // 计算文本实际需要的高度（考虑换行，支持任意行数）
                // labelDescription 的可用宽度：窗体宽度 - 左右边距（32*2）
                int labelWidth = this.ClientSize.Width - 32 * 2;
                if (labelWidth <= 0)
                {
                    labelWidth = 409 - 32 * 2; // 使用默认宽度
                }

                // 使用 TextRenderer.MeasureText 计算文本高度（更准确，支持换行和任意行数）
                // 使用 int.MaxValue 作为高度限制，确保能够测量所有行
                Size textSize = TextRenderer.MeasureText(
                    labelDescription.Text,
                    labelDescription.Font,
                    new Size(labelWidth, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.NoPrefix);

                // 计算单行文本的高度（用于判断是否需要调整）
                int singleLineHeight = TextRenderer.MeasureText("A", labelDescription.Font).Height;

                // 如果文本高度超过单行高度，说明是多行文本，需要调整布局
                if (textSize.Height > singleLineHeight)
                {
                    // 计算 labelDescription 需要的实际高度（加上一些边距以确保显示完整）
                    const int verticalPadding = 4; // 上下各2像素边距
                    int labelHeight = textSize.Height + verticalPadding;

                    // 设置 labelDescription 的高度和宽度（取消 AutoSize 的固定高度限制）
                    labelDescription.AutoSize = false;
                    labelDescription.Width = labelWidth;
                    labelDescription.Height = labelHeight;

                    // 调整 processBar 的位置，确保在 labelDescription 下方，留出适当间距
                    const int spacing = 10; // labelDescription 和 processBar 之间的间距
                    int newProcessBarTop = labelDescription.Top + labelHeight + spacing;

                    // 计算窗体需要的最小高度
                    // 顶部边距（55） + labelDescription高度 + 间距 + processBar高度 + 底部边距
                    const int topMargin = 55; // labelDescription 的 Top 位置
                    const int bottomMargin = 20; // 底部边距
                    int minFormHeight = topMargin + labelHeight + spacing + processBar.Height + bottomMargin;

                    // 确保窗体高度足够容纳所有内容
                    if (minFormHeight > this.ClientSize.Height)
                    {
                        // 调整窗体高度
                        this.ClientSize = new Size(this.ClientSize.Width, minFormHeight);
                    }

                    // 调整 processBar 的位置
                    processBar.Top = newProcessBarTop;
                }
                else
                {
                    // 单行文本，恢复默认布局
                    RestoreDefaultLayout();
                }
            }
            catch
            {
                // 如果调整失败，尝试恢复默认布局
                try
                {
                    RestoreDefaultLayout();
                }
                catch
                {
                    // 如果恢复也失败，保持当前状态
                }
            }
        }

        /// <summary>
        /// 恢复默认布局（单行文本时的布局）
        /// </summary>
        private void RestoreDefaultLayout()
        {
            // 恢复 labelDescription 的 AutoSize
            labelDescription.AutoSize = true;

            // 恢复 processBar 的默认位置
            processBar.Top = 91;

            // 恢复窗体的默认高度
            this.ClientSize = new Size(473, 153);
        }

        private delegate void StepItHandler(int step);

        public void StepIt(int step = 1)
        {
            if (processBar.InvokeRequired)
            {
                Invoke(new StepItHandler(StepIt), step);
            }
            else
            {
                processBar.Step = step;
                processBar.StepIt();
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (!UIFormServiceHelper.StatusFormServiceShow)
            {
                timer1.Stop();
                Close();
            }
        }

        private void UIStatusForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            timer1.Stop();
            UIFormServiceHelper.StatusFormServiceShow = false;
        }
    }
}
