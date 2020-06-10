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
 * 文件名称: UTemplate.cs
 * 文件说明: 模版引擎
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Text.RegularExpressions;

namespace Sunny.UI
{
    /// <summary>
    /// 模版引擎
    /// </summary>
    public class UITemplate
    {
        private string Content { get; set; }

        /// <summary>
        /// 模版引擎
        /// </summary>
        /// <param name="content"></param>
        public UITemplate(string content)
        {
            Content = content;
        }

        /// <summary>
        /// 设置变量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public UITemplate Set(string key, string value)
        {
            Content = Content.Replace("{{" + key + "}}", value);
            return this;
        }

        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            var mc = Regex.Matches(Content, @"\{\{.+?\}\}");
            foreach (Match m in mc)
            {
                throw new ArgumentException($"模版变量{m.Value}未被使用");
            }

            return Content;
        }
    }
}