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
 * 文件名称: UIStyleManager.cs
 * 文件说明: 主题样式管理类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.ComponentModel;

namespace Sunny.UI
{
    /// <summary>
    /// 主题样式管理类
    /// </summary>
    public class UIStyleManager : Component
    {
        /// <summary>
        /// 主题样式
        /// </summary>
        [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
        public UIStyle Style
        {
            get => UIStyles.Style;
            set
            {
                if (UIStyles.Style != value && value != UIStyle.Custom)
                {
                    UIStyles.SetStyle(value);
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIStyleManager()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        public UIStyleManager(IContainer container) : this()
        {
            container.Add(this);
        }
    }
}