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
 * 文件名称: UIDateTimeItem.cs
 * 文件说明: 日期选择框弹出窗体
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 日期选择框弹出窗体
    /// </summary>
    public partial class UIDateTimeControl : UIDropDownItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIDateTimeControl()
        {
            InitializeComponent();
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DoValueChanged(this, monthCalendar.SelectionStart);
            CloseParent();
        }

        /// <summary>
        /// 日期选择控件
        /// </summary>
        public MonthCalendar MonthCalendar => monthCalendar;
    }
}