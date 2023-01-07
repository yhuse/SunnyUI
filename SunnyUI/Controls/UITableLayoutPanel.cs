/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UITableLayoutPanel.cs
 * 文件说明: 动态布局面板
 * 当前版本: V3.1
 * 创建日期: 2021-07-18
 *
 * 2021-07-18: V3.0.5 增加文件说明
 * 2021-07-18: V3.0.5 更新了放在TableLayoutPanel里控件的自定义颜色问题
******************************************************************************/

using System.ComponentModel;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed class UITableLayoutPanel : TableLayoutPanel, IStyleInterface
    {
        public UITableLayoutPanel()
        {
            Version = UIGlobal.Version;
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

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is IStyleInterface ctrl)
            {
                if (!ctrl.StyleCustomMode) ctrl.Style = Style;
            }

            UIStyleHelper.SetRawControlStyle(e, Style);
        }

        /// <summary>
        /// 自定义主题风格
        /// </summary>
        [DefaultValue(false)]
        [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
        public bool StyleCustomMode
        {
            get; set;
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

        public string Version
        {
            get;
        }

        public string TagString
        {
            get; set;
        }

        public void SetStyle(UIStyle style)
        {
            this.SuspendLayout();
            UIStyleHelper.SetChildUIStyle(this, style);
            _style = style;
            this.ResumeLayout();
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            //
        }
    }
}
