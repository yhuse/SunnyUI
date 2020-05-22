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
 * 文件名称: UDataReader.cs
 * 文件说明: 数据读取扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Data;

namespace Sunny.UI
{
    /// <summary>
    /// 数据读取扩展类
    /// </summary>
    public static class DataReaderEx
    {
        /// <summary>
        /// Returns true if the database connection is in the specified state.
        /// </summary>
        /// <param name="connection">数据连接</param>
        /// <param name="state">连接状态</param>
        /// <returns>结果</returns>
        public static bool IsInState(this IDbConnection connection, ConnectionState state)
        {
            return (connection != null && (connection.State & state) == state);
        }

        /// <summary>
        /// Open the Database connection if not already opened.
        /// </summary>
        /// <param name="connection">数据连接</param>
        public static void OpenIfNot(this IDbConnection connection)
        {
            if (!connection.IsInState(ConnectionState.Open))
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Gets the record value casted to the specified data type or the data types default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this IDataReader reader, string field)
        {
            return reader.Get(field, default(T));
        }

        /// <summary>
        /// 	Gets the record value casted to the specified data type or the specified default value.
        /// </summary>
        /// <typeparam name = "T">The return data type</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static T Get<T>(this IDataReader reader, string field, T defaultValue)
        {
            var value = reader[field];
            if (value == DBNull.Value)
            {
                return defaultValue;
            }

            return (value is T variable) ? variable : defaultValue;
        }

        /// <summary>
        /// 	Gets the record value casted as byte array.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static byte[] GetBytes(this IDataReader reader, string field)
        {
            return (reader[field] as byte[]);
        }

        /// <summary>
        /// 	Gets the record value casted as string or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static string GetString(this IDataReader reader, string field)
        {
            return reader.GetString(field, null);
        }

        /// <summary>
        /// 	Gets the record value casted as string or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static string GetString(this IDataReader reader, string field, string defaultValue)
        {
            var value = reader[field];
            return (value is string s ? s : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as Guid or Guid.Empty.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Guid GetGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is Guid guid ? guid : Guid.Empty);
        }

        /// <summary>
        /// 	Gets the record value casted as Guid? or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Guid? GetNullableGuid(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is Guid ? (Guid?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or DateTime.Minimum.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this IDataReader reader, string field)
        {
            return reader.GetDateTime(field, DateTime.MinValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTime GetDateTime(this IDataReader reader, string field, DateTime defaultValue)
        {
            var value = reader[field];
            return (value is DateTime time ? time : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTime or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTime? GetNullableDateTime(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is DateTime ? (DateTime?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or DateTime.Minimum.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field)
        {
            return new DateTimeOffset(reader.GetDateTime(field), TimeSpan.Zero);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset GetDateTimeOffset(this IDataReader reader, string field, DateTimeOffset defaultValue)
        {
            var dt = reader.GetDateTime(field);
            return (dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as DateTimeOffset (UTC) or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataReader reader, string field)
        {
            var dt = reader.GetNullableDateTime(field);
            return (dt != null ? (DateTimeOffset?)new DateTimeOffset(dt.Value, TimeSpan.Zero) : null);
        }

        /// <summary>
        /// 	Gets the record value casted as int or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this IDataReader reader, string field)
        {
            return reader.GetInt32(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as int or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static int GetInt32(this IDataReader reader, string field, int defaultValue)
        {
            var value = reader[field];
            return (value is int i ? i : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as int or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static int? GetNullableInt32(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is int ? (int?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as long or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this IDataReader reader, string field)
        {
            return reader.GetInt64(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as long or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static long GetInt64(this IDataReader reader, string field, int defaultValue)
        {
            var value = reader[field];
            return (value is long l ? l : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as long or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static long? GetNullableInt64(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is long ? (long?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or 0.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this IDataReader reader, string field)
        {
            return reader.GetDecimal(field, 0);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static decimal GetDecimal(this IDataReader reader, string field, long defaultValue)
        {
            var value = reader[field];
            return (value is decimal d ? d : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as decimal or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static decimal? GetNullableDecimal(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is decimal ? (decimal?)value : null);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or false.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this IDataReader reader, string field)
        {
            return reader.GetBoolean(field, false);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static bool GetBoolean(this IDataReader reader, string field, bool defaultValue)
        {
            var value = reader[field];
            return (value is bool b ? b : defaultValue);
        }

        /// <summary>
        /// 	Gets the record value casted as bool or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static bool? GetNullableBoolean(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value is bool ? (bool?)value : null);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this IDataReader reader, string field)
        {
            return reader.GetType(field, null);
        }

        /// <summary>
        /// 	Gets the record value as Type class instance or the specified default value.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static Type GetType(this IDataReader reader, string field, Type defaultValue)
        {
            string classType = reader.GetString(field);
            if (classType.IsNullOrEmpty())
            {
                return defaultValue;
            }

            Type type = Type.GetType(classType);
            return type ?? defaultValue;
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this IDataReader reader, string field)
        {
            return reader.GetTypeInstance(field, null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "defaultValue">The default value.</param>
        /// <returns>The record value</returns>
        public static object GetTypeInstance(this IDataReader reader, string field, Type defaultValue)
        {
            var type = reader.GetType(field, defaultValue);
            return (type != null ? Activator.CreateInstance(type) : null);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or null.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstance<T>(this IDataReader reader, string field) where T : class
        {
            return (reader.GetTypeInstance(field, null) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or the specified default type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <param name = "type">The type.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field, Type type) where T : class
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? Activator.CreateInstance(type) as T);
        }

        /// <summary>
        /// 	Gets the record value as class instance from a type name or an instance from the specified type.
        /// </summary>
        /// <typeparam name = "T">The type to be casted to</typeparam>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>The record value</returns>
        public static T GetTypeInstanceSafe<T>(this IDataReader reader, string field) where T : class, new()
        {
            var instance = (reader.GetTypeInstance(field, null) as T);
            return (instance ?? new T());
        }

        /// <summary>
        /// 	Determines whether the record value is DBNull.Value
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "field">The name of the record field.</param>
        /// <returns>
        /// 	<c>true</c> if the value is DBNull.Value; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(this IDataReader reader, string field)
        {
            var value = reader[field];
            return (value == DBNull.Value);
        }

        /// <summary>
        /// Reads all all records from a data reader and performs an action for each.
        /// </summary>
        /// <param name = "reader">The data reader.</param>
        /// <param name = "action">The action to be performed.</param>
        /// <returns>The count of actions that were performed. </returns>
        public static int ReadAll(this IDataReader reader, Action<IDataReader> action)
        {
            var count = 0;
            while (reader.Read())
            {
                action(reader);
                count++;
            }

            return count;
        }

        /// <summary>
        /// 获取要查找的字段的索引
        /// </summary>
        /// <param name="record">记录</param>
        /// <param name="name">字段名</param>
        /// <returns>索引</returns>
        public static int IndexOf(this IDataRecord record, string name)
        {
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (string.Compare(record.GetName(i), name, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}