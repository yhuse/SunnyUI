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
 * 文件名称: UIMarkLabel.cs
 * 文件说明: 带颜色标签
 * 当前版本: V3.1
 * 创建日期: 2021-03-07
 *
 * 2021-03-07: V3.0.2 增加文件说明
 * 2022-03-19: V3.1.1 重构主题配色
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public sealed class UIMarkLabel : UILabel
    {
        public UIMarkLabel()
        {
            Padding = new Padding(MarkSize + 2, 0, 0, 0);
            markColor = UIStyles.Blue.MarkLabelForeColor;
        }

        private bool autoSize;

        [Browsable(true)]
        [Description("自动大小"), Category("SunnyUI")]
        public override bool AutoSize
        {
            get => autoSize;
            set
            {
                autoSize = value;
                Invalidate();
            }
        }

        private int markSize = 3;

        [Description("标签大小"), Category("SunnyUI"), DefaultValue(3)]
        public int MarkSize
        {
            get => markSize;
            set
            {
                markSize = value;
                Invalidate();
            }
        }

        private UIMarkPos markPos = UIMarkPos.Left;

        [Description("标签位置"), Category("SunnyUI"), DefaultValue(UIMarkPos.Left)]
        public UIMarkPos MarkPos
        {
            get => markPos;
            set
            {
                markPos = value;

                switch (markPos)
                {
                    case UIMarkPos.Left: Padding = new Padding(MarkSize + 2, 0, 0, 0); break;
                    case UIMarkPos.Right: Padding = new Padding(0, 0, MarkSize + 2, 0); break;
                    case UIMarkPos.Top: Padding = new Padding(0, MarkSize + 2, 0, 0); break;
                    case UIMarkPos.Bottom: Padding = new Padding(0, 0, 0, MarkSize + 2); break;
                }

                Invalidate();
            }
        }

        public enum UIMarkPos
        {
            Left,
            Top,
            Right,
            Bottom
        }

        private Color markColor;

        [Description("标签颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color MarkColor
        {
            get => markColor;
            set
            {
                markColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            markColor = uiColor.MarkLabelForeColor;
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Size TextSize = TextRenderer.MeasureText(Text, Font);
            if (autoSize && Dock == DockStyle.None)
            {
                int width = (MarkPos == UIMarkPos.Left || MarkPos == UIMarkPos.Right) ?
                    TextSize.Width + MarkSize + 2 : TextSize.Width;
                int height = (MarkPos == UIMarkPos.Top || MarkPos == UIMarkPos.Bottom) ?
                    TextSize.Height + MarkSize + 2 : TextSize.Height;

                if (Width != width) Width = width;
                if (Height != height) Height = height;
            }

            switch (markPos)
            {
                case UIMarkPos.Left: e.Graphics.FillRectangle(MarkColor, 0, 0, MarkSize, Height); break;
                case UIMarkPos.Right: e.Graphics.FillRectangle(MarkColor, Width - MarkSize, 0, MarkSize, Height); break;
                case UIMarkPos.Top: e.Graphics.FillRectangle(MarkColor, 0, 0, Width, MarkSize); break;
                case UIMarkPos.Bottom: e.Graphics.FillRectangle(MarkColor, 0, Height - MarkSize, Width, MarkSize); break;
            }
        }
    }
}
