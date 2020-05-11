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
 * 文件名称: UAssembly.cs
 * 文件说明: 反射处理类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Reflection;

namespace Sunny.UI
{
    /// <summary>
    /// 反射处理类
    /// </summary>
    public static class AssemblyEx
    {
        /// <summary>
        /// 根据类名称获取当前进程的类型申明
        /// </summary>
        /// <param name="typename">类名</param>
        /// <returns>类型申明</returns>
        public static Type GetCurrentType(string typename)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetType(typename);
        }

        /// <summary>
        /// 根据类名称获取DLL类型申明
        /// </summary>
        /// <param name="dll">DLL名称</param>
        /// <param name="typename">类名</param>
        /// <returns>类型申明</returns>
        public static Type GetDllType(string dll, string typename)
        {
            Assembly assembly = Assembly.LoadFile(dll);
            return assembly.GetType(typename);
        }

        /// <summary>
        /// 获取当前进程的类型申明数组
        /// </summary>
        /// <returns>类型申明数组</returns>
        public static Type[] GetCurrentTypes()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes();
        }

        /// <summary>
        /// 获取DLL类型申明数组
        /// </summary>
        /// <param name="dll">DLL名称</param>
        /// <returns>类型申明数组</returns>
        public static Type[] GetDllTypes(string dll)
        {
            Assembly assembly = Assembly.LoadFile(dll);
            return assembly.GetExportedTypes();
        }

        /// <summary>
        /// 根据类型申明创建对象
        /// </summary>
        /// <param name="type">类型申明</param>
        /// <returns>对象</returns>
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}