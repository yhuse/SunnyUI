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
 * 文件名称: UISelectForm.cs
 * 文件说明: 下拉选择窗体
 * 当前版本: V3.1
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 增加文件
******************************************************************************/

using System.Collections;

namespace Sunny.UI
{
    /// <summary>
    /// 下拉选择窗体
    /// </summary>
    public sealed partial class UISelectForm : UIEditForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UISelectForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置下拉框内容
        /// </summary>
        /// <param name="items"></param>
        public void SetItems(IList items)
        {
            ComboBox.Items.Clear();

            foreach (var item in items)
            {
                ComboBox.Items.Add(item);
            }
        }

        public string Description
        {
            get => label.Text;
            set => label.Text = value;
        }

        public string Title
        {
            get => Text;
            set => Text = value;
        }

        /// <summary>
        /// 选择框索引
        /// </summary>
        public int SelectedIndex
        {
            get => ComboBox.SelectedIndex;
            set => ComboBox.SelectedIndex = value;
        }
    }
}