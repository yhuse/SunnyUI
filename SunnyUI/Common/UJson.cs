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
 * 文件名称: UJson.cs
 * 文件说明: Json扩展类，不引用第三方组件实现简单的Json字符串和对象的转换
 * 当前版本: V3.1
 * 创建日期: 2020-10-21
 *
 * 2020-10-21: V2.2.9 增加文件说明
******************************************************************************/

using System;
using System.IO;
using System.Text;
#if NETFRAMEWORK
using System.Web.Script.Serialization;
#endif

namespace Sunny.UI
{
    /// <summary>
    /// Json扩展类
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// 反序列化字符串为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="input">字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(string input)
        {
            try
            {
#if NETFRAMEWORK
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Deserialize<T>(input);
#else
                return System.Text.Json.JsonSerializer.Deserialize<T>(input);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
        }

        /// <summary>
        /// 序列号对象为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        public static string Serialize(object obj)
        {
#if NETFRAMEWORK
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
#else
            return System.Text.Json.JsonSerializer.Serialize(obj);
#endif
        }

        /// <summary>
        /// 从文件读取字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filename">文件名</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>对象</returns>
        public static T DeserializeFromFile<T>(string filename, Encoding encoding)
        {
            string jsonStr = File.ReadAllText(filename, encoding);
            return Deserialize<T>(jsonStr);
        }

        /// <summary>
        /// 序列号对象为字符串并保存到文件
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件名</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>字符串</returns>
        public static string SerializeToFile(object obj, string filename, Encoding encoding)
        {
            string jsonStr = Serialize(obj);
            try
            {
                DirEx.CreateDir(Path.GetDirectoryName(filename));
                File.WriteAllText(filename, jsonStr, encoding);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return jsonStr;
        }
    }
}
