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
 * 文件名称: UIToolTip.cs
 * 文件说明: 提示
 * 当前版本: V2.2
 * 创建日期: 2020-07-21
 *
 * 2020-07-21: V2.2.6 增加控件
 * 2020-07-25: V2.2.6 更新绘制
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

        public UIToolTip(IContainer cont)
            : base(cont)
        {
            InitOwnerDraw();
        }

        [DefaultValue("ToolTip title"), Category("SunnyUI"), Description("标题")]
        public new string ToolTipTitle { get; set; } = "ToolTip title";

        [DefaultValue(typeof(Font), "微软雅黑, 9pt"), Description("字体"), Category("SunnyUI")]
        public Font Font { get; set; } = new Font("微软雅黑", 9);

        [DefaultValue(typeof(Font), "微软雅黑, 12pt"), Description("标题字体"), Category("SunnyUI")]
        public Font TitleFont { get; set; } = new Font("微软雅黑", 12);

        [DefaultValue(typeof(Color), "239, 239, 239"), Description("边框颜色"), Category("SunnyUI")]
        public Color RectColor { get; set; } = UIChartStyles.Dark.ForeColor;

        [DefaultValue(true), Description("自动大小"), Category("SunnyUI")]
        public bool AutoSize { get; set; } = true;

        [DefaultValue(typeof(Size), "100, 70"), Description("不自动缩放时大小"), Category("SunnyUI")]
        public Size Size { get; set; } = new Size(100, 70);

        public void SetToolTip(Control control, string description, string title, int symbol, int symbolSize,
            Color symbolColor)
        {
            if (title == null) title = string.Empty;

            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls[control].Title = title;
                ToolTipControls[control].Description = description;
                ToolTipControls[control].Symbol = symbol;
                ToolTipControls[control].SymbolSize = symbolSize;
                ToolTipControls[control].SymbolColor = symbolColor;
            }
            else
            {
                var ctrl = new ToolTipControl()
                {
                    Control = control,
                    Title = title,
                    Description = description,
                    Symbol = symbol,
                    SymbolSize = symbolSize,
                    SymbolColor = symbolColor
                };

                ToolTipControls.TryAdd(control, ctrl);
            }

            base.SetToolTip(control, description);
        }

        public void SetToolTip(Control control, string description, string title)
        {
            if (title == null) title = string.Empty;

            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls[control].Title = title;
                ToolTipControls[control].Description = description;
            }
            else
            {
                var ctrl = new ToolTipControl()
                {
                    Control = control,
                    Title = title,
                    Description = description
                };

                ToolTipControls.TryAdd(control, ctrl);
            }

            base.SetToolTip(control, description);
        }

        public new void SetToolTip(Control control, string description)
        {
            if (ToolTipControls.ContainsKey(control))
            {
                ToolTipControls[control].Title = string.Empty;
                ToolTipControls[control].Description = description;
            }
            else
            {
                var ctrl = new ToolTipControl
                {
                    Control = control,
                    Title = string.Empty,
                    Description = description
                };

                ToolTipControls.TryAdd(control, ctrl);
            }

            base.SetToolTip(control, description);
        }

        public void RemoveToolTip(Control control)
        {
            if (ToolTipControls.ContainsKey(control))
                ToolTipControls.TryRemove(control, out _);
        }

        public new string GetToolTip(Control control)
        {
            return ToolTipControls.ContainsKey(control) ? ToolTipControls[control].Description : "";
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
            if (ToolTipControls.ContainsKey(e.AssociatedControl))
            {
                var tooltip = ToolTipControls[e.AssociatedControl];

                if (tooltip.Description.IsValid())
                {
                    if (!AutoSize)
                    {
                        e.ToolTipSize = Size;
                    }
                    else
                    {
                        var bmp = new Bitmap(e.ToolTipSize.Width, e.ToolTipSize.Height);
                        var g = Graphics.FromImage(bmp);

                        int symbolWidth = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;
                        int symbolHeight = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;

                        SizeF titleSize = new SizeF(0, 0);
                        if (tooltip.Title.IsValid())
                        {
                            titleSize = g.MeasureString(tooltip.Title, TitleFont);
                        }

                        SizeF textSize = g.MeasureString(tooltip.Description, Font);
                        int allWidth = (int)Math.Max(textSize.Width, titleSize.Width) + 10;
                        if (symbolWidth > 0) allWidth = allWidth + symbolWidth + 5;
                        int allHeight = titleSize.Height > 0 ?
                            (int)titleSize.Height + (int)textSize.Height + 15 :
                            (int)textSize.Height + 10;
                        e.ToolTipSize = new Size(allWidth, allHeight);
                        bmp.Dispose();
                    }
                }
            }
        }

        private void ToolTipExDraw(object sender, DrawToolTipEventArgs e)
        {
            if (ToolTipControls.ContainsKey(e.AssociatedControl))
            {
                var tooltip = ToolTipControls[e.AssociatedControl];
                var bounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);

                e.Graphics.FillRectangle(BackColor, bounds);
                e.Graphics.DrawRectangle(RectColor, bounds);

                if (tooltip.Symbol > 0)
                {
                    e.Graphics.DrawFontImage(tooltip.Symbol, tooltip.SymbolSize, tooltip.SymbolColor, new Rectangle(5, 5, tooltip.SymbolSize, tooltip.SymbolSize));
                }

                int symbolWidth = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;
                int symbolHeight = tooltip.Symbol > 0 ? tooltip.SymbolSize : 0;
                SizeF titleSize = new SizeF(0, 0);
                if (tooltip.Title.IsValid())
                {
                    if (tooltip.Title.IsValid())
                    {
                        titleSize = e.Graphics.MeasureString(tooltip.Title, TitleFont);
                    }

                    e.Graphics.DrawString(tooltip.Title, TitleFont, ForeColor,
                        tooltip.Symbol > 0 ? tooltip.SymbolSize + 5 : 5, 5);
                }

                if (titleSize.Height > 0)
                {
                    e.Graphics.DrawLine(ForeColor,
                        symbolWidth == 0 ? 5 : symbolWidth + 5, 5 + titleSize.Height + 3,
                        e.Bounds.Width - 5, 5 + titleSize.Height + 3);
                }

                e.Graphics.DrawString(e.ToolTipText, Font, ForeColor,
                    tooltip.Symbol > 0 ? tooltip.SymbolSize + 5 : 5,
                    titleSize.Height > 0 ? 10 + titleSize.Height : 5);
            }
            else
            {
                e.Graphics.DrawString(e.ToolTipText, e.Font, ForeColor, 0, 0);
            }
        }

        public class ToolTipControl
        {
            public Control Control { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public int Symbol { get; set; } = 0;

            public int SymbolSize { get; set; } = 32;

            public Color SymbolColor { get; set; } = UIChartStyles.Dark.ForeColor;
        }
    }
}