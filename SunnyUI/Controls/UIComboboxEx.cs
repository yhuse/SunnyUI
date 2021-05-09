/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIComboBoxEx.cs
 * 文件说明: 组合框（继承自Combobox）
 * 当前版本: V3.0
 * 创建日期: 2021-02-20
 *
 * 2021-02-20: V3.0.1 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIComboboxEx : ComboBox, IStyleInterface
    {
        public UIComboboxEx()
        {
            Version = UIGlobal.Version;

            base.Font = UIFontColor.Font;
            base.ForeColor = UIFontColor.Primary;
            base.BackColor = Color.White;
            DrawMode = DrawMode.OwnerDrawFixed;
            //FlatStyle = FlatStyle.Flat;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.DoubleBuffered = true;
            this.DoubleBuffered();
            UpdateStyles();
            Width = 150;
        }

        private UIStyle _style = UIStyle.Blue;

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            Invalidate();
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetForeColor(Color value)
        {
            if (foreColor != value)
            {
                foreColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color RectColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="value">颜色</param>
        protected void SetRectColor(Color value)
        {
            if (rectColor != value)
            {
                rectColor = value;
                _style = UIStyle.Custom;
                Invalidate();
            }
        }

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public string Version { get; }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="style">主题样式</param>
        public void SetStyle(UIStyle style)
        {
            SetStyleColor(UIStyles.GetStyleColor(style));
            _style = style;
        }

        /// <summary>
        /// 设置主题样式颜色
        /// </summary>
        /// <param name="uiColor"></param>
        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            if (uiColor.IsCustom()) return;

            rectColor = uiColor.RectColor;
            foreColor = UIFontColor.Primary;
            _itemSelectBackColor = uiColor.ListItemSelectBackColor;
            _itemSelectForeColor = uiColor.ListItemSelectForeColor;
            Invalidate();
        }

        private Color _itemSelectBackColor = UIColor.Blue;
        private Color _itemSelectForeColor = Color.White;

        [Description("下拉框选中背景颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ItemSelectBackColor
        {
            get => _itemSelectBackColor;
            set
            {
                if (_itemSelectBackColor != value)
                {
                    _itemSelectBackColor = value;
                    _style = UIStyle.Custom;
                    if (DesignMode)
                        Invalidate();
                }
            }
        }

        [Description("下拉框选中字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color ItemSelectForeColor
        {
            get => _itemSelectForeColor;
            set
            {
                if (_itemSelectForeColor != value)
                {
                    _itemSelectForeColor = value;
                    _style = UIStyle.Custom;
                    if (DesignMode)
                        Invalidate();
                }
            }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        protected Color rectColor = UIStyles.Blue.RectColor;

        /// <summary>
        /// 字体颜色
        /// </summary>
        protected Color foreColor = UIFontColor.Primary;

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Color fColor = ForeColor;
            Color bColor = BackColor;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                fColor = ItemSelectForeColor;
                bColor = ItemSelectBackColor;
            }

            e.Graphics.FillRectangle(bColor, e.Bounds);
            if (e.Index >= 0)
            {
                e.Graphics.DrawString(GetItemText(Items[e.Index]), Font, fColor, e.Bounds.X + 1, e.Bounds.Y);
            }
        }

        private readonly Graphics graphics = null;
        private Graphics Graphics => graphics ?? CreateGraphics();

        protected override void WndProc(ref Message m)
        {
            if (IsDisposed || Disposing) return;

            switch (m.Msg)
            {
                //WM_PAINT = 0xf; 要求一个窗口重画自己,即Paint事件时
                //WM_CTLCOLOREDIT = 0x133;当一个编辑型控件将要被绘制时发送此消息给它的父窗口；
                //通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
                //windows消息值表,可参考:http://hi.baidu.com/dooy/blog/item/0e770a24f70e3b2cd407421b.html
                case Win32.User.WM_PAINT:
                    //case Win32.User.WM_CTLCOLOREDIT:
                    base.WndProc(ref m);
                    //Graphics.FillRectangle(BackColor, new Rectangle(0, 0, Width, Height));

                    Graphics.DrawRectangle(rectColor, 0, 0, Width - 1, Height - 1);

                    // if (Text.IsValid())
                    // {
                    //     SizeF sf = Graphics.MeasureString(Text, Font);
                    //     Graphics.DrawString(Text, Font, ForeColor, 0, (Height - sf.Height) / 2);
                    // }
                    //
                    // Graphics.DrawFontImage(61703, 24, rectColor, new Rectangle(Width - 28, 1, 28, Height - 2));
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
