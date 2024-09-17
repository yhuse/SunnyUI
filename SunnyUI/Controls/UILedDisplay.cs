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
 * 文件名称: UILedDisplay.cs
 * 文件说明: LED显示屏
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// LED显示屏
    /// </summary>
    [DefaultProperty("Text")]
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

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

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
        private void CalcSize()
        {
            Width = BorderWidth * 2 + BorderInWidth * 2 + IntervalH * 2 + CharCount * IntervalOn * 5 +
                    CharCount * IntervalIn * 4 + (CharCount + 1) * IntervalOn + CharCount * 2 * IntervalIn;
            Height = BorderWidth * 2 + BorderInWidth * 2 + IntervalV * 2 + IntervalOn * 7 + IntervalIn * 6;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
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
                byte[] bts = UILedChars.Chars.ContainsKey(c) ? UILedChars.Chars[c] : UILedChars.Chars[' '];
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