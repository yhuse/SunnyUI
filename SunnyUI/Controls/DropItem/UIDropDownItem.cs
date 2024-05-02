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
 * 文件名称: UIDropDownItem.cs
 * 文件说明: 下拉框弹出窗体基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.ComponentModel;
using System.Drawing;

namespace Sunny.UI
{
    /// <summary>
    /// 下拉框弹出窗体基类
    /// </summary>
    [ToolboxItem(false)]
    public partial class UIDropDownItem : UIPanel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UIDropDownItem()
        {
            InitializeComponent();
            SetStyleFlags(true, false);
        }

        /// <summary>
        /// 数值切换事件定义
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="value">数值</param>
        public delegate void OnValueChanged(object sender, object value);

        /// <summary>
        /// 数值切换事件
        /// </summary>
        public event OnValueChanged ValueChanged;

        /// <summary>
        /// 数值切换事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="value">数值</param>
        protected void DoValueChanged(object sender, object value)
        {
            ValueChanged?.Invoke(this, value);
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public virtual void SetRectColor(Color color)
        {
        }

        /// <summary>
        /// 设置填充颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public virtual void SetFillColor(Color color)
        {
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="color">颜色</param>
        public virtual void SetForeColor(Color color)
        {
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        public void Close()
        {
            UIDropDown parent = (UIDropDown)Parent;
            parent?.Close();
        }

        public virtual void InitShow()
        {

        }
    }
}