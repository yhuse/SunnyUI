using System.ComponentModel;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UITableLayoutPanel : TableLayoutPanel, IStyleInterface
    {
        public UITableLayoutPanel()
        {
            Version = UIGlobal.Version;
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

        protected UIStyle _style = UIStyle.Blue;

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
            UIStyleHelper.SetChildUIStyle(this, style);
            _style = style;
        }

        public void SetStyleColor(UIBaseStyle uiColor)
        {
            //
        }
    }
}
