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
 * 文件名称: UIToolTip.cs
 * 文件说明: 提示
 * 当前版本: V3.1
 * 创建日期: 2020-07-21
 *
 * 2020-07-21: V2.2.6 增加控件
 * 2020-07-25: V2.2.6 更新绘制
 * 2021-08-16: V3.0.6 增加ToolTip接口，解决类似UITextBox这类的组合控件无法显示ToolTip的问题
 * 2021-12-09: V3.0.9 修复默认显示
 * 2023-05-14: V3.3.6 重构DrawString函数
 * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ProvideProperty("ToolTip", typeof(Control))]
    [DefaultEvent("Popup")]
    [ToolboxItemFilter("System.Windows.Forms")]
    public class UIToolTip : ToolTip
    {
        private readonly ConcurrentDictionary<Control, ToolTipControl> ToolTipControls =
            new ConcurrentDictionary<Control, ToolTipControl>();

        public UIToolTip()
        {
            InitOwnerDraw();
        }

        public UIToolTip(IContainer cont) : base(cont)
        {
            InitOwnerDraw();
        }

        [DefaultValue(typeof(Font), "宋体, 9pt"), Description("字体"), Category("SunnyUI")]
        public Font Font { get; set; } = new Font("宋体", 9);

        [DefaultValue(typeof(Font), "宋体, 12pt"), Description("标题字体"), Category("SunnyUI")]
        public Font TitleFont { get; set; } = new Font("宋体", 12);

        [DefaultValue(typeof(Color), "239, 239, 239"), Description("边框颜色"), Category("SunnyUI")]
        public Color RectColor { get; set; } = UIChartStyles.Dark.ForeColor;

        [DefaultValue(true), Description("自动大小"), Category("SunnyUI")]
        public bool AutoSize { get; set; } = true;

        [DefaultValue(typeof(Size), "100, 70"), Description("不自动缩放时大小"), Category("SunnyUI")]
        public Size Size { get; set; } = new Size(100, 70);

        public new void SetToolTip(Control control, string caption)
        {
            base.SetToolTip(control, caption);
            if (control is IToolTip toolTip)
            {
                base.SetToolTip(toolTip.ExToolTipControl(), caption);
            }
        }

        public void SetToolTip(Control control, string caption, string title, int symbol, int symbolSize,
            Color symbolColor, int symbolRotate = 0)
        {
            if (title == null) title = string.Empty;

            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls[control].Title = title;
                ToolTipControls[control].ToolTipText = caption;
                ToolTipControls[control].Symbol = symbol;
                ToolTipControls[control].SymbolSize = symbolSize;
                ToolTipControls[control].SymbolColor = symbolColor;
                ToolTipControls[control].SymbolRotate = symbolRotate;
            }
            else
            {
                var ctrl = new ToolTipControl()
                {
                    Control = control,
                    Title = title,
                    ToolTipText = caption,
                    Symbol = symbol,
                    SymbolSize = symbolSize,
                    SymbolColor = symbolColor,
                    SymbolRotate = symbolRotate
                };

                ToolTipControls.TryAdd(control, ctrl);
            }

            if (control is IToolTip toolTip)
            {
                SetToolTip(toolTip.ExToolTipControl(), caption, title, symbol, symbolSize, symbolColor, symbolRotate);
            }

            base.SetToolTip(control, caption);
        }

        public void SetToolTip(Control control, string caption, string title)
        {
            if (title == null) title = string.Empty;

            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls[control].Title = title;
                ToolTipControls[control].ToolTipText = caption;
            }
            else
            {
                var ctrl = new ToolTipControl()
                {
                    Control = control,
                    Title = title,
                    ToolTipText = caption
                };

                ToolTipControls.TryAdd(control, ctrl);
            }

            if (control is IToolTip toolTip)
            {
                SetToolTip(toolTip.ExToolTipControl(), caption, title);
            }

            base.SetToolTip(control, caption);
        }

        public void RemoveToolTip(Control control)
        {
            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls.TryRemove(control, out _);
            }

            if (control is IToolTip toolTip)
            {
                RemoveToolTip(toolTip.ExToolTipControl());
            }
        }

        private void InitOwnerDraw()
        {
            OwnerDraw = true;
            Draw += ToolTipExDraw;
            Popup += UIToolTip_Popup;

            BackColor = UIChartStyles.Dark.BackColor;
            ForeColor = UIChartStyles.Dark.ForeColor;
            RectColor = UIChartStyles.Dark.ForeColor;
        }

        private void UIToolTip_Popup(object sender, PopupEventArgs e)
        {
            using var TempFont = Font.DPIScaleFont(Font.Size);
            using var TempTitleFont = TitleFont.DPIScaleFont(TitleFont.Size);

            if (ToolTipControls.ContainsKey(e.AssociatedControl))
            {
                var tooltip = ToolTipControls[e.AssociatedControl];

                if (tooltip.ToolTipText.IsValid())
                {
                    if (!AutoSize)
                    {
                        e.ToolTipSize = Size;
                    }
                    else
                    {
                        int symbolWidth = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;
                        Size titleSize = new Size(0, 0);
                        if (tooltip.Title.IsValid())
                        {
                            titleSize = TextRenderer.MeasureText(tooltip.Title, TempTitleFont);
                        }

                        Size textSize = TextRenderer.MeasureText(tooltip.ToolTipText, TempFont);
                        int allWidth = Math.Max(textSize.Width, titleSize.Width) + 10;
                        if (symbolWidth > 0) allWidth = allWidth + symbolWidth + 5;
                        int allHeight = titleSize.Height > 0 ? titleSize.Height + textSize.Height + 15 : textSize.Height + 10;
                        e.ToolTipSize = new Size(allWidth, allHeight);
                    }
                }
            }
            else
            {
                Size sf = TextRenderer.MeasureText(GetToolTip(e.AssociatedControl), TempFont);
                e.ToolTipSize = sf.Add(10, 10);
            }
        }

        private void ToolTipExDraw(object sender, DrawToolTipEventArgs e)
        {
            var bounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            e.Graphics.FillRectangle(BackColor, bounds);
            e.Graphics.DrawRectangle(RectColor, bounds);
            using var TempFont = Font.DPIScaleFont(Font.Size);
            using var TempTitleFont = TitleFont.DPIScaleFont(TitleFont.Size);

            if (ToolTipControls.ContainsKey(e.AssociatedControl))
            {
                var tooltip = ToolTipControls[e.AssociatedControl];
                if (tooltip.Symbol > 0)
                {
                    e.Graphics.DrawFontImage(tooltip.Symbol, tooltip.SymbolSize, tooltip.SymbolColor, new Rectangle(5, 5, tooltip.SymbolSize, tooltip.SymbolSize));
                }

                int symbolWidth = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;
                SizeF titleSize = new SizeF(0, 0);
                if (tooltip.Title.IsValid())
                {
                    if (tooltip.Title.IsValid())
                    {
                        titleSize = TextRenderer.MeasureText(tooltip.Title, TempTitleFont);
                    }

                    e.Graphics.DrawString(tooltip.Title, TempTitleFont, ForeColor, new Rectangle(tooltip.Symbol > 0 ? tooltip.SymbolSize + 5 : 5, 5, bounds.Width, bounds.Height), ContentAlignment.TopLeft);
                }

                if (titleSize.Height > 0)
                {
                    e.Graphics.DrawLine(ForeColor, symbolWidth == 0 ? 5 : symbolWidth + 5, 5 + titleSize.Height + 3,
                        e.Bounds.Width - 5, 5 + titleSize.Height + 3);
                }

                e.Graphics.DrawString(e.ToolTipText, TempFont, ForeColor, new Rectangle(tooltip.Symbol > 0 ? tooltip.SymbolSize + 5 : 5, titleSize.Height > 0 ? 10 + (int)titleSize.Height : 5, bounds.Width, bounds.Height), ContentAlignment.TopLeft);
            }
            else
            {
                e.Graphics.DrawString(e.ToolTipText, TempFont, ForeColor, new Rectangle(5, 5, bounds.Width, bounds.Height), ContentAlignment.TopLeft);
            }
        }

        public class ToolTipControl : ISymbol
        {
            public Control Control { get; set; }
            public string Title { get; set; }
            public string ToolTipText { get; set; }

            /// <summary>
            /// 字体图标
            /// </summary>
            public int Symbol { get; set; }

            /// <summary>
            /// 字体图标大小
            /// </summary>
            public int SymbolSize { get; set; } = 32;

            /// <summary>
            /// 字体图标的偏移位置
            /// </summary>
            public Point SymbolOffset { get; set; } = new Point(0, 0);

            /// <summary>
            /// 字体图标旋转角度
            /// </summary>
            public int SymbolRotate { get; set; } = 0;

            /// <summary>
            /// 字体图标颜色
            /// </summary>
            public Color SymbolColor { get; set; } = UIChartStyles.Dark.ForeColor;
        }
    }
}