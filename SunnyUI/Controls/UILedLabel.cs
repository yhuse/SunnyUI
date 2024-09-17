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
 * 文件名称: UILedLabel.cs
 * 文件说明: LED标签
 * 当前版本: V3.1
 * 创建日期: 2021-04-11
 *
 * 2021-04-11: V3.0.2 增加文件说明
 * 2022-03-19: V3.1.1 重构主题配色
******************************************************************************/

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class UILedLabel : UIControl
    {
        public UILedLabel()
        {
            SetStyleFlags(true, false);
            ShowText = ShowRect = ShowFill = false;
            foreColor = UIStyles.Blue.LedLabelForeColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int width = CharCount * IntervalOn * 5 +
                        CharCount * IntervalIn * 4 + (CharCount + 1) * IntervalOn + CharCount * IntervalIn;
            int height = IntervalOn * 7 + IntervalIn * 6;

            float left = 0;
            float top = 0;
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    left = 0;
                    top = 0;
                    break;

                case ContentAlignment.TopCenter:
                    left = (Width - width) / 2.0f;
                    top = 0;
                    break;

                case ContentAlignment.TopRight:
                    left = Width - width;
                    top = 0;
                    break;

                case ContentAlignment.MiddleLeft:
                    left = 0;
                    top = (Height - height) / 2.0f;
                    break;

                case ContentAlignment.MiddleCenter:
                    left = (Width - width) / 2.0f;
                    top = (Height - height) / 2.0f;
                    break;

                case ContentAlignment.MiddleRight:
                    left = Width - width;
                    top = (Height - height) / 2.0f;
                    break;

                case ContentAlignment.BottomLeft:
                    left = 0;
                    top = Height - height;
                    break;

                case ContentAlignment.BottomCenter:
                    left = (Width - width) / 2.0f;
                    top = Height - height;
                    break;

                case ContentAlignment.BottomRight:
                    left = Width - width;
                    top = Height - height;
                    break;
            }

            int idx = 0;
            foreach (char c in Text)
            {
                float charStart = left + (IntervalOn + IntervalIn) * 6 * idx;
                byte[] bts = UILedChars.Chars.ContainsKey(c) ? UILedChars.Chars[c] : UILedChars.Chars[' '];
                for (int i = 0; i < 5; i++)
                {
                    byte bt = bts[i];
                    float btStart = charStart + (IntervalOn + IntervalIn) * i;
                    BitArray array = new BitArray(new[] { bt });
                    for (int j = 0; j < 7; j++)
                    {
                        bool bon = array[7 - j];
                        if (bon)
                        {
                            e.Graphics.FillRectangle(
                                ForeColor,
                                btStart,
                                 top + (IntervalOn + IntervalIn) * j,
                                IntervalOn,
                                IntervalOn);
                        }
                    }
                }

                idx++;
            }
        }

        private int intervalIn = 1;
        private int intervalOn = 2;

        private int CharCount => Text.Length;

        /// <summary>
        /// LED亮块间距
        /// </summary>
        [DefaultValue(1), Description("LED亮块间距"), Category("SunnyUI")]
        public int IntervalIn
        {
            get => intervalIn;
            set
            {
                if (intervalIn != value)
                {
                    intervalIn = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// LED亮块大小
        /// </summary>
        [DefaultValue(2), Description("LED亮块大小"), Category("SunnyUI")]
        public int IntervalOn
        {
            get => intervalOn;
            set
            {
                if (intervalOn != value)
                {
                    intervalOn = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            foreColor = uiColor.LedLabelForeColor;
        }
    }
}
