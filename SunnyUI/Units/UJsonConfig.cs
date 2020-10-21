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
 * 文件名称: UJsonConfig.cs
 * 文件说明: Json配置文件类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.9 增加文件说明
******************************************************************************/

using System;
using System.IO;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// JSON 配置文件类
    /// </summary>
    /// <typeparam name="TConfig">类型</typeparam>
    public class JsonConfig<TConfig> : BaseConfig<TConfig> where TConfig : JsonConfig<TConfig>, new()
    {
        /// <summary>加载指定配置文件</summary>
        /// <param name="filename">文件名</param>
        /// <returns>结果</returns>
        public override bool Load(string filename)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                filename = ConfigFile;
            }

            if (filename.IsNullOrWhiteSpace())
            {
                throw new ApplicationException($"未指定{typeof(TConfig).Name}的配置文件路径！");
            }

            if (!File.Exists(filename))
            {
                return false;
            }

            try
            {
                current = Json.DeserializeFromFile<TConfig>(filename, Encoding.Default);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>保存到配置文件中去</summary>
        /// <param name="filename">文件名</param>
        public override void Save(string filename)
        {
            if (filename.IsNullOrWhiteSpace())
            {
                filename = ConfigFile;
            }

            if (filename.IsNullOrWhiteSpace())
            {
                throw new ApplicationException($"未指定{typeof(TConfig).Name}的配置文件路径！");
            }

            Json.SerializeToFile(current, filename, Encoding.Default);
        }
    }
}