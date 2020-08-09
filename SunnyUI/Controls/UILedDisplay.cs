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
 * 文件名称: UILedDisplay.cs
 * 文件说明: LED 显示器
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// LED显示屏
    /// </summary>
    public class UILedDisplay : Control
    {
        #region 组件设计器生成的代码

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private readonly IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);
        }

        #endregion 组件设计器生成的代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public UILedDisplay()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            base.BackColor = Color.Black;
            base.ForeColor = Color.Lime;
            CalcSize();
            Version = UIGlobal.Version;
        }

        public string Version { get; }

        private Color borderColor = Color.Black;
        private Color borderInColor = Color.Silver;
        private Color ledBackColor = Color.FromArgb(0, 0x33, 0);

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "0, 0, 0")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 内线颜色
        /// </summary>
        [Description("内线颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "0xC0, 0xC0, 0xC0")]
        public Color BorderInColor
        {
            get => borderInColor;
            set
            {
                borderInColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// LED背景色
        /// </summary>
        [Description("LED背景色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "0, 0x33, 0")]
        public Color LedBackColor
        {
            get => ledBackColor;
            set
            {
                ledBackColor = value;
                Invalidate();
            }
        }

        private int borderWidth = 1;
        private int borderInWidth = 1;
        private int intervalH = 2;
        private int intervalV = 5;
        private int intervalIn = 1;
        private int intervalOn = 2;
        private int charCount = 10;

        /// <summary>
        /// 边框宽度
        /// </summary>
        [DefaultValue(1), Description("边框宽度"), Category("SunnyUI")]
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                if (borderWidth != value)
                {
                    borderWidth = value;
                    CalcSize();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 内线宽度
        /// </summary>
        [DefaultValue(1), Description("内线宽度"), Category("SunnyUI")]
        public int BorderInWidth
        {
            get => borderInWidth;
            set
            {
                if (borderInWidth != value)
                {
                    borderInWidth = value;
                    CalcSize();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 左右边距
        /// </summary>
        [DefaultValue(2), Description("左右边距"), Category("SunnyUI")]
        public int IntervalH
        {
            get => intervalH;
            set
            {
                if (intervalH != value)
                {
                    intervalH = value;
                    CalcSize();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 上下边距
        /// </summary>
        [DefaultValue(5), Description("上下边距"), Category("SunnyUI")]
        public int IntervalV
        {
            get => intervalV;
            set
            {
                if (intervalV != value)
                {
                    intervalV = value;
                    CalcSize();
                    Invalidate();
                }
            }
        }

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
                    CalcSize();
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
                    CalcSize();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 显示字符个数
        /// </summary>
        [DefaultValue(10), Description("显示字符个数"), Category("SunnyUI")]
        public int CharCount
        {
            get => charCount;
            set
            {
                if (charCount != value)
                {
                    charCount = value;
                    CalcSize();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 计算大小
        /// </summary>
        public void CalcSize()
        {
            Width = BorderWidth * 2 + BorderInWidth * 2 + IntervalH * 2 + CharCount * IntervalOn * 5 +
                    CharCount * IntervalIn * 4 + (CharCount + 1) * IntervalOn + CharCount * 2 * IntervalIn;
            Height = BorderWidth * 2 + BorderInWidth * 2 + IntervalV * 2 + IntervalOn * 7 + IntervalIn * 6;
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int w = Width;
            int h = Height;

            //背景
            e.Graphics.FillRectangle(BackColor, 0, 0, w, h);
            //外边框
            e.Graphics.FillRectangle(BorderColor, 0, 0, w, BorderWidth);
            e.Graphics.FillRectangle(BorderColor, 0, h - BorderWidth, w, BorderWidth);
            e.Graphics.FillRectangle(BorderColor, 0, 0, BorderWidth, h);
            e.Graphics.FillRectangle(BorderColor, w - BorderWidth, 0, BorderWidth, h);
            //内边框
            e.Graphics.FillRectangle(BorderInColor, BorderWidth, BorderWidth, w - BorderWidth * 2, BorderInWidth);
            e.Graphics.FillRectangle(BorderInColor, BorderWidth, BorderWidth, BorderInWidth, h - BorderWidth * 2);
            e.Graphics.FillRectangle(BorderInColor, BorderWidth, h - BorderWidth - BorderInWidth, w - BorderWidth * 2, BorderInWidth);
            e.Graphics.FillRectangle(BorderInColor, w - BorderWidth - BorderInWidth, BorderWidth, BorderInWidth, h - BorderWidth * 2);

            int wc = CharCount * 5 + CharCount + 1;
            int hc = 7;
            for (int i = 0; i < wc; i++)
            {
                for (int j = 0; j < hc; j++)
                {
                    e.Graphics.FillRectangle(
                        LedBackColor,
                        BorderWidth + BorderInWidth + IntervalH + (IntervalOn + IntervalIn) * i,
                        BorderWidth + BorderInWidth + IntervalV + (IntervalOn + IntervalIn) * j,
                        IntervalOn,
                        IntervalOn);
                }
            }

            string str = Text.PadLeft(CharCount, ' ');
            str = str.Substring(0, CharCount);
            int idx = 0;
            foreach (char c in str)
            {
                int charStart = BorderWidth + BorderInWidth + IntervalH + IntervalOn + IntervalIn +
                                (IntervalOn + IntervalIn) * 6 * idx;
                byte[] bts = DotMasks.ContainsKey(c) ? DotMasks[c] : DotMasks[' '];
                for (int i = 0; i < 5; i++)
                {
                    byte bt = bts[i];
                    int btStart = charStart + (IntervalOn + IntervalIn) * i;
                    BitArray array = new BitArray(new[] { bt });
                    for (int j = 0; j < 7; j++)
                    {
                        bool bon = array[7 - j];
                        if (bon)
                        {
                            e.Graphics.FillRectangle(
                                ForeColor,
                                btStart,
                                BorderWidth + BorderInWidth + IntervalV + (IntervalOn + IntervalIn) * j,
                                IntervalOn,
                                IntervalOn);
                        }
                    }
                }

                idx++;
            }
        }

        /// <summary>
        /// 文字改变
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            CalcSize();
            Invalidate();
        }

        /// <summary>
        /// 增加LED字符
        /// </summary>
        /// <param name="key">字符</param>
        /// <param name="bytes">显示</param>
        public void Add(char key, byte[] bytes)
        {
            if (!DotMasks.ContainsKey(key))
            {
                DotMasks.Add(key, bytes);
            }
        }

        private readonly Dictionary<char, byte[]> DotMasks = new Dictionary<char, byte[]>
        {
            [' '] = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 },
            ['!'] = new byte[] { 0x00, 0x00, 0xFA, 0x00, 0x00 },
            ['\"'] = new byte[] { 0x00, 0xC0, 0x00, 0xC0, 0x00 },
            ['#'] = new byte[] { 0x28, 0x7C, 0x28, 0x7C, 0x28 },
            ['$'] = new byte[] { 0x24, 0x54, 0xFE, 0x54, 0x48 },
            ['%'] = new byte[] { 0x64, 0x68, 0x10, 0x2C, 0x4C },
            ['&'] = new byte[] { 0x6C, 0x92, 0xAA, 0x44, 0x0A },
            ['\''] = new byte[] { 0x00, 0x20, 0xC0, 0x00, 0x00 },
            ['('] = new byte[] { 0x00, 0x00, 0x7C, 0x82, 0x00 },
            [')'] = new byte[] { 0x00, 0x82, 0x7C, 0x00, 0x00 },
            ['*'] = new byte[] { 0x54, 0x38, 0xFE, 0x38, 0x54 },
            ['+'] = new byte[] { 0x10, 0x10, 0x7C, 0x10, 0x10 },
            [','] = new byte[] { 0x00, 0x02, 0x04, 0x00, 0x00 },
            ['-'] = new byte[] { 0x10, 0x10, 0x10, 0x10, 0x10 },
            ['.'] = new byte[] { 0x00, 0x00, 0x02, 0x00, 0x00 },
            ['/'] = new byte[] { 0x04, 0x08, 0x10, 0x20, 0x40 },
            ['0'] = new byte[] { 0x7C, 0x8A, 0x92, 0xA2, 0x7C },
            ['1'] = new byte[] { 0x00, 0x42, 0xFE, 0x02, 0x00 },
            ['2'] = new byte[] { 0x46, 0x8A, 0x92, 0x92, 0x62 },
            ['3'] = new byte[] { 0x44, 0x92, 0x92, 0x92, 0x6C },
            ['4'] = new byte[] { 0xF0, 0x10, 0x10, 0x10, 0xFE },
            ['5'] = new byte[] { 0xF4, 0x92, 0x92, 0x92, 0x8C },
            ['6'] = new byte[] { 0x7C, 0x92, 0x92, 0x92, 0x4C },
            ['7'] = new byte[] { 0xC0, 0x80, 0x8E, 0x90, 0xE0 },
            ['8'] = new byte[] { 0x6C, 0x92, 0x92, 0x92, 0x6C },
            ['9'] = new byte[] { 0x64, 0x92, 0x92, 0x92, 0x7C },
            [':'] = new byte[] { 0x00, 0x00, 0x24, 0x00, 0x00 },
            [';'] = new byte[] { 0x00, 0x02, 0x24, 0x00, 0x00 },
            ['<'] = new byte[] { 0x10, 0x28, 0x44, 0x82, 0x00 },
            ['='] = new byte[] { 0x28, 0x28, 0x28, 0x28, 0x28 },
            ['>'] = new byte[] { 0x00, 0x82, 0x44, 0x28, 0x10 },
            ['?'] = new byte[] { 0x40, 0x80, 0x9A, 0x90, 0x60 },
            ['@'] = new byte[] { 0x7C, 0x92, 0xAA, 0xBA, 0x70 },
            ['A'] = new byte[] { 0x7E, 0x90, 0x90, 0x90, 0x7E },
            ['B'] = new byte[] { 0xFE, 0x92, 0x92, 0x92, 0x6C },
            ['C'] = new byte[] { 0x7C, 0x82, 0x82, 0x82, 0x44 },
            ['D'] = new byte[] { 0xFE, 0x82, 0x82, 0x82, 0x7C },
            ['E'] = new byte[] { 0xFE, 0x92, 0x92, 0x92, 0x82 },
            ['F'] = new byte[] { 0xFE, 0x90, 0x90, 0x90, 0x80 },
            ['G'] = new byte[] { 0x7C, 0x82, 0x92, 0x92, 0x5C },
            ['H'] = new byte[] { 0xFE, 0x10, 0x10, 0x10, 0xFE },
            ['I'] = new byte[] { 0x82, 0x82, 0xFE, 0x82, 0x82 },
            ['J'] = new byte[] { 0x04, 0x02, 0x02, 0x02, 0xFC },
            ['K'] = new byte[] { 0xFE, 0x10, 0x28, 0x44, 0x82 },
            ['L'] = new byte[] { 0xFE, 0x02, 0x02, 0x02, 0x02 },
            ['M'] = new byte[] { 0xFE, 0x40, 0x20, 0x40, 0xFE },
            ['N'] = new byte[] { 0xFE, 0x20, 0x10, 0x08, 0xFE },
            ['O'] = new byte[] { 0x7C, 0x82, 0x82, 0x82, 0x7C },
            ['P'] = new byte[] { 0xFE, 0x90, 0x90, 0x90, 0x60 },
            ['Q'] = new byte[] { 0x7C, 0x82, 0x82, 0x86, 0x7E },
            ['R'] = new byte[] { 0xFE, 0x90, 0x90, 0x90, 0x6E },
            ['S'] = new byte[] { 0x64, 0x92, 0x92, 0x92, 0x4C },
            ['T'] = new byte[] { 0x80, 0x80, 0xFE, 0x80, 0x80 },
            ['U'] = new byte[] { 0xFC, 0x02, 0x02, 0x02, 0xFC },
            ['V'] = new byte[] { 0xE0, 0x18, 0x06, 0x18, 0xE0 },
            ['W'] = new byte[] { 0xFC, 0x02, 0x0C, 0x02, 0xFC },
            ['X'] = new byte[] { 0xC6, 0x28, 0x10, 0x28, 0xC6 },
            ['Y'] = new byte[] { 0xC0, 0x20, 0x1E, 0x20, 0xC0 },
            ['Z'] = new byte[] { 0x86, 0x8A, 0x92, 0xA2, 0xC2 },
            ['['] = new byte[] { 0x00, 0xFE, 0x82, 0x82, 0x00 },
            ['\\'] = new byte[] { 0x40, 0x20, 0x10, 0x08, 0x04 },
            [']'] = new byte[] { 0x00, 0x82, 0x82, 0xFE, 0x00 },
            ['^'] = new byte[] { 0x20, 0x40, 0x80, 0x40, 0x20 },
            ['_'] = new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02 },
            ['`'] = new byte[] { 0x00, 0x00, 0xC0, 0x20, 0x00 },
            ['°'] = new byte[] { 0x00, 0x00, 0x40, 0xA0, 0x40 },
            ['a'] = new byte[] { 0x04, 0x2A, 0x2A, 0x2A, 0x1E },
            ['b'] = new byte[] { 0xFE, 0x22, 0x22, 0x22, 0x1C },
            ['c'] = new byte[] { 0x1C, 0x22, 0x22, 0x22, 0x14 },
            ['d'] = new byte[] { 0x1C, 0x22, 0x22, 0x22, 0xFE },
            ['e'] = new byte[] { 0x1C, 0x2A, 0x2A, 0x2A, 0x18 },
            ['f'] = new byte[] { 0x10, 0x7E, 0x90, 0x90, 0x40 },
            ['g'] = new byte[] { 0x10, 0x2A, 0x2A, 0x2A, 0x1C },
            ['h'] = new byte[] { 0xFE, 0x20, 0x20, 0x20, 0x1E },
            ['i'] = new byte[] { 0x00, 0x22, 0xBE, 0x02, 0x00 },
            ['j'] = new byte[] { 0x00, 0x02, 0xBC, 0x00, 0x00 },
            ['k'] = new byte[] { 0xFE, 0x08, 0x08, 0x14, 0x22 },
            ['l'] = new byte[] { 0x00, 0x82, 0xFE, 0x02, 0x00 },
            ['m'] = new byte[] { 0x3E, 0x20, 0x3E, 0x20, 0x1E },
            ['n'] = new byte[] { 0x3E, 0x20, 0x20, 0x20, 0x1E },
            ['o'] = new byte[] { 0x1C, 0x22, 0x22, 0x22, 0x1C },
            ['p'] = new byte[] { 0x3E, 0x28, 0x28, 0x28, 0x10 },
            ['q'] = new byte[] { 0x10, 0x28, 0x28, 0x28, 0x3E },
            ['r'] = new byte[] { 0x3E, 0x20, 0x20, 0x20, 0x10 },
            ['s'] = new byte[] { 0x12, 0x2A, 0x2A, 0x2A, 0x24 },
            ['t'] = new byte[] { 0x20, 0xFC, 0x22, 0x22, 0x00 },
            ['u'] = new byte[] { 0x3C, 0x02, 0x02, 0x02, 0x3E },
            ['v'] = new byte[] { 0x38, 0x04, 0x02, 0x04, 0x38 },
            ['w'] = new byte[] { 0x3C, 0x02, 0x0C, 0x02, 0x3C },
            ['x'] = new byte[] { 0x22, 0x14, 0x08, 0x14, 0x22 },
            ['y'] = new byte[] { 0x22, 0x14, 0x08, 0x10, 0x20 },
            ['z'] = new byte[] { 0x22, 0x26, 0x2A, 0x32, 0x22 },
            ['{'] = new byte[] { 0x00, 0x10, 0x6C, 0x82, 0x00 },
            ['|'] = new byte[] { 0x00, 0x00, 0xFE, 0x00, 0x00 },
            ['}'] = new byte[] { 0x00, 0x82, 0x6C, 0x10, 0x00 },
            ['~'] = new byte[] { 0x40, 0x80, 0x40, 0x20, 0x40 },
            ['Κ'] = new byte[] { 0xFE, 0x10, 0x28, 0x44, 0x82 },
            ['Χ'] = new byte[] { 0xC6, 0x28, 0x10, 0x28, 0xC6 },
            ['Υ'] = new byte[] { 0xC0, 0x20, 0x1E, 0x20, 0xC0 },
            ['Μ'] = new byte[] { 0xFE, 0x40, 0x20, 0x40, 0xFE },
            ['Γ'] = new byte[] { 0xFE, 0x80, 0x80, 0x80, 0x80 },
            ['Ν'] = new byte[] { 0xFE, 0x20, 0x10, 0x08, 0xFE },
            ['Ξ'] = new byte[] { 0x82, 0x92, 0x92, 0x92, 0x82 },
            ['Ο'] = new byte[] { 0x7C, 0x82, 0x82, 0x82, 0x7C },
            ['Θ'] = new byte[] { 0x7C, 0x92, 0x92, 0x92, 0x7C },
            ['Π'] = new byte[] { 0xFE, 0x80, 0x80, 0x80, 0xFE },
            ['Ρ'] = new byte[] { 0xFE, 0x90, 0x90, 0x90, 0x60 },
            ['Ω'] = new byte[] { 0x7A, 0x8E, 0x80, 0x8E, 0x7A },
            ['Ψ'] = new byte[] { 0xF0, 0x08, 0xFE, 0x08, 0xF0 },
            ['Ι'] = new byte[] { 0x82, 0x82, 0xFE, 0x82, 0x82 },
            ['∞'] = new byte[] { 0x38, 0x44, 0x38, 0x44, 0x38 },
            ['Α'] = new byte[] { 0x7E, 0x90, 0x90, 0x90, 0x7E },
            ['Δ'] = new byte[] { 0x0E, 0x32, 0xC2, 0x32, 0x0E },
            ['Λ'] = new byte[] { 0x0E, 0x30, 0xC0, 0x30, 0x0E },
            ['Ε'] = new byte[] { 0xFE, 0x92, 0x92, 0x92, 0x82 },
            ['Η'] = new byte[] { 0xFE, 0x10, 0x10, 0x10, 0xFE },
            ['Φ'] = new byte[] { 0x38, 0x44, 0xFE, 0x44, 0x38 },
            ['Β'] = new byte[] { 0xFE, 0x92, 0x92, 0x92, 0x6C },
            ['Τ'] = new byte[] { 0x80, 0x80, 0xFE, 0x80, 0x80 },
            ['Ζ'] = new byte[] { 0x86, 0x8A, 0x92, 0xA2, 0xC2 },
            ['Σ'] = new byte[] { 0xC6, 0xAA, 0x92, 0x82, 0x82 },
            ['：'] = new byte[] { 0x00, 0x00, 0x24, 0x00, 0x00 },
        };

        /// <summary>
        /// 禁用该属性
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("禁用该属性！", true)]
        [DefaultValue(typeof(Image), "null")]
        public new Image BackgroundImage => null;

        /// <summary>
        /// 禁用该属性
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("禁用该属性！", true)]
        [DefaultValue(ImageLayout.Tile)]
        public new ImageLayout BackgroundImageLayout => ImageLayout.Tile;
    }
}