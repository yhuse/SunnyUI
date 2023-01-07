/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
 * 文件名称: UMapper.cs
 * 文件说明: 轻量级的对象映射框架，可以映射值类型（包括Struct），和以值类型构成的List和数组。
 * 当前版本: V3.1
 * 创建日期: 2021-09-30
 *
 * 2021-09-30: V3.0.7 增加文件说明
******************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sunny.UI
{
    /// <summary>
    /// 轻量级的对象映射框架
    /// </summary>
    public static class Mapper
    {
        private static void Execute<T1, T2>(T1 source, T2 dest)
            where T1 : class
            where T2 : class
        {
            if (source == null || dest == null)
            {
                return;
            }

            var listSource = source.GetType().GetNeedProperties().ToDictionary(prop => prop.Name);
            var listDest = dest.GetType().GetNeedProperties().ToDictionary(prop => prop.Name);

            foreach (var item in listDest)
            {
                if (listSource.NotContainsKey(item.Key)) continue;

                var sourceInfo = listSource[item.Key];
                Type sourceType = sourceInfo.PropertyType;
                object sourceValue = sourceInfo.GetValue(source, null);

                var destInfo = item.Value;
                Type destType = item.Value.PropertyType;

                if (!sourceType.Equals(destType)) continue;
                if (sourceType.IsValueType)
                {
                    //Console.WriteLine("ValueType: " + item.Key + ", " + sourceType.FullName);
                    destInfo.SetValue(dest, sourceValue, null);
                }
                else
                {
                    if (sourceType == typeof(string))
                    {
                        //Console.WriteLine("String: " + item.Key + ", " + sourceType.FullName);
                        destInfo.SetValue(dest, sourceValue, null);
                        continue;
                    }

                    if (sourceType.IsList())
                    {
                        //Console.WriteLine("List: " + item.Key + ", " + sourceType.FullName);
                        Type[] sourceTypes = sourceType.GetGenericArguments();
                        Type[] destTypes = destType.GetGenericArguments();
                        if (sourceTypes.Length != 1) continue;
                        if (destTypes.Length != 1) continue;
                        if (!sourceTypes[0].Equals(destTypes[0])) continue;

                        if (sourceValue == null)
                        {
                            destInfo.SetValue(dest, null, null);
                        }
                        else
                        {
                            Type typeDataList = typeof(List<>).MakeGenericType(destTypes[0]);
                            MethodInfo AddInfo = typeDataList.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
                            if (AddInfo == null) continue;

                            if (sourceTypes[0].IsValueType || sourceTypes[0] == typeof(string))
                            {
                                object listvalue = typeDataList.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, new object[] { });
                                IEnumerable sourceList = sourceValue as IEnumerable;
                                foreach (var listItem in sourceList)
                                {
                                    AddInfo.Invoke(listvalue, new[] { listItem });
                                }

                                destInfo.SetValue(dest, listvalue, null);
                            }
                            else
                            {
                                //暂时不考虑
                            }
                        }

                        continue;
                    }

                    if (sourceType.IsArray)
                    {
                        //Console.WriteLine("Array: " + item.Key + ", " + sourceType.FullName);
                        if (sourceValue == null)
                        {
                            destInfo.SetValue(dest, null, null);
                        }
                        else
                        {
                            ICollection sourceList = sourceValue as ICollection;
                            Type type = sourceType.GetElementType();
                            var array = Array.CreateInstance(type, sourceList.Count);

                            int index = 0;
                            foreach (var listItem in sourceList)
                            {
                                array.SetValue(listItem, index++);
                            }

                            destInfo.SetValue(dest, array, null);
                        }

                        continue;
                    }

                    if (sourceType.IsDictionary())
                    {

                        continue;
                    }

                    //类没有无参构造函数的话，创建有问题
                    //if (sourceType.IsClass)
                    //{
                    //    if (sourceValue == null)
                    //    {
                    //        destInfo.SetValue(dest, null, null);
                    //    }
                    //    else
                    //    {
                    //        object obj = Activator.CreateInstance(sourceType, null);
                    //        obj.MapperFrom(sourceValue);
                    //        destInfo.SetValue(dest, obj, null);
                    //    }
                    //
                    //    continue;
                    //}
                }
            }
        }

        /// <summary>
        /// T1映射到T2
        /// </summary>
        /// <typeparam name="T1">T1</typeparam>
        /// <typeparam name="T2">T2</typeparam>
        /// <param name="source">源</param>
        /// <param name="dest">目标</param>
        public static void MapperTo<T1, T2>(this T1 source, T2 dest)
            where T1 : class
            where T2 : class
        {
            Execute(source, dest);
        }

        /// <summary>
        /// T1从T2映射
        /// </summary>
        /// <typeparam name="T1">T1</typeparam>
        /// <typeparam name="T2">T2</typeparam>
        /// <param name="source">源</param>
        /// <param name="dest">目标</param>
        public static void MapperFrom<T1, T2>(this T1 dest, T2 source)
            where T1 : class
            where T2 : class
        {
            Execute(source, dest);
        }

        /// <summary>
        /// 获取数组的类类型
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns>类类型</returns>
        public static Type GetArrayElementType(this Type t)
        {
            if (!t.IsArray) return null;
            string name = t.FullName.Replace("[]", string.Empty);
            return t.Assembly.GetType(name);
        }
    }
}
