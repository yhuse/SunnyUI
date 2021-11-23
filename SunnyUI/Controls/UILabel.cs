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
 * 文件名称: UILabel.cs
 * 文件说明: 标签
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-23: V2.2.4 增加UISymbolLabel
 * 2020-04-25: V2.2.4 更新主题配置类
 * 2020-11-12: V3.0.8 增加文字旋转角度
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class UILabel : Label, IStyleInterface
    {
        public UILabel()
        {
            base.Font = UIFontColor.Font;
            Version = UIGlobal.Version;
            base.TextAlign = ContentAlignment.MiddleLeft;
            ForeColorChanged += UILabel_ForeColorChanged;
        }

        private int angle;

        [DefaultValue(0), Category("SunnyUI"), Description("居中时旋转角度")]
        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public bool IsScaled { get; set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        private void UILabel_ForeColorChanged(object sender, EventArgs e)
        {
            _style = UIStyle.Custom;
        }

        private Color foreColor = UIStyles.GetStyleColor(UIStyle.Blue).LabelForeColor;

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "48, 48, 48")]
        public override Color ForeColor
        {
            get => foreColor;
            set
            {
                foreColor = value;
                Invalidate();
            }
        }

        public string Version { get; }

        public void SetStyle(UIStyle style)
        {
            UIBaseStyle uiColor = UIStyles.GetStyleColor(style);
            if (!uiColor.IsCustom()) SetStyleColor(uiColor);
            _style = style;
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public virtual void SetStyleColor(UIBaseStyle uiColor)
        {
            ForeColor = uiColor.LabelForeColor;
            Invalidate();
        }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            _style = UIStyle.Custom;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (TextAlign == ContentAlignment.MiddleCenter && Angle != 0 && !AutoSize)
            {
                e.Graphics.DrawStringRotateAtCenter(Text, Font, ForeColor, this.ClientRectangle.Center(), Angle);
            }
            else
            {
                base.OnPaint(e);
            }
        }
    }

    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public sealed class UILinkLabel : LinkLabel, IStyleInterface
    {
        public UILinkLabel()
        {
            Font = UIFontColor.Font;
            LinkBehavior = LinkBehavior.AlwaysUnderline;
            Version = UIGlobal.Version;

            ActiveLinkColor = UIColor.Orange;
            VisitedLinkColor = UIColor.Red;
            LinkColor = UIColor.Blue;
        }

        [Browsable(false)]
        public bool IsScaled { get; private set; }

        public void SetDPIScale()
        {
            if (!IsScaled)
            {
                this.SetDPIScaleFont();
                IsScaled = true;
            }
        }

        /// <summary>
        /// Tag字符串
        /// </summary>
        [DefaultValue(null)]
        [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
        public string TagString { get; set; }

        public string Version { get; }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode { get; set; }

        public void SetStyle(UIStyle style)
        {
            UIBaseStyle uiColor = UIStyles.GetStyleColor(style);
            if (!uiColor.IsCustom()) SetStyleColor(uiColor);
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            ForeColor = uiColor.LabelForeColor;
            LinkColor = uiColor.LabelForeColor;
            Invalidate();
        }

        private UIStyle _style = UIStyle.Blue;

        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => _style;
            set => SetStyle(value);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            _style = UIStyle.Custom;
        }
    }
}