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
 * 文件名称: UJson.cs
 * 文件说明: Json扩展类，不引用第三方组件实现简单的Json字符串和对象的转换
 * 当前版本: V2.2
 * 创建日期: 2020-10-21
 *
 * 2020-10-21: V2.2.9 增加文件说明
******************************************************************************/

using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace Sunny.UI
{
    public static class Json
    {
        public static T Deserialize<T>(string input)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Deserialize<T>(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
        }

        public static string Serialize(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }

        public static T DeserializeFromFile<T>(string filename, Encoding encoding)
        {
            string jsonStr = File.ReadAllText(filename, encoding);
            return Deserialize<T>(jsonStr);
        }

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
