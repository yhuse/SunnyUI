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
 * 文件名称: UEnum.cs
 * 文件说明: 枚举扩展类
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
    /// 枚举扩展类
    /// </summary>
    public static class EnumEx
    {
        /// <summary>
        /// 枚举字段描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>字符串</returns>
        public static string DisplayText(this Enum value)
        {
            FieldInfo info = value.GetType().GetField(value.ToString());
            var attributes = info.GetCustomAttribute<DisplayTextAttribute>();
            return attributes != null ? attributes.DisplayText : value.ToString();
        }

        /// <summary>
        /// 将字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">TEnum</typeparam>
        /// <param name="dataToMatch">dataToMatch</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns>枚举类型</returns>
        public static TEnum ToEnum<TEnum>(this string dataToMatch, bool ignoreCase = true) where TEnum : struct
        {
            return dataToMatch.InEnum<TEnum>()()
                ? default
                : (TEnum)Enum.Parse(typeof(TEnum), dataToMatch, ignoreCase);
        }

        /// <summary>
        /// 判断字符串是否为枚举类型
        /// </summary>
        /// <typeparam name="TEnum">TEnum</typeparam>
        /// <param name="dataToCheck">dataToCheck</param>
        /// <returns>结果</returns>
        public static Func<bool> InEnum<TEnum>(this string dataToCheck) where TEnum : struct
        {
            return () => string.IsNullOrEmpty(dataToCheck) || !Enum.IsDefined(typeof(TEnum), dataToCheck);
        }
    }

    /// <summary>
    /// Specifies description for a member of the enum type for display to the UI
    /// </summary>
    /// <example>
    ///     <code>
    ///         enum OperatingSystem
    ///         {
    ///            [DisplayText("MS-DOS")]
    ///            MsDos,
    ///
    ///            [DisplayText("Windows 98")]
    ///            Win98,
    ///
    ///            [DisplayText("Windows XP")]
    ///            Xp,
    ///
    ///            [DisplayText("Windows Vista")]
    ///            Vista,
    ///
    ///            [DisplayText("Windows 7")]
    ///            Seven,
    ///         }
    ///
    ///         public string GetMyOSName()
    ///         {
    ///             var myOS = OperatingSystem.Seven;
    ///             return myOS.DisplayText();
    ///         }
    ///     </code>
    /// </example>
    /// <remarks>
    /// http://about.me/AlekseyNagovitsyn
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class DisplayTextAttribute : Attribute
    {
        /// <summary>
        /// The default value for the attribute <c>DisplayTextAttribute</c>, which is an empty string
        /// </summary>
        public static readonly DisplayTextAttribute Default = new DisplayTextAttribute();

        /// <summary>
        /// The value of this attribute
        /// </summary>
        public string DisplayText { get; }

        /// <summary>
        /// Initializes a new instance of the class <c>DisplayTextAttribute</c> with default value (empty string)
        /// </summary>
        public DisplayTextAttribute() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class <c>DisplayTextAttribute</c> with specified value
        /// </summary>
        /// <param name="text">The value of this attribute</param>
        public DisplayTextAttribute(string text)
        {
            DisplayText = text;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>结果</returns>
        public override bool Equals(object obj)
        {
            return obj is DisplayTextAttribute dsaObj && DisplayText.Equals(dsaObj.DisplayText);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>结果</returns>
        public override int GetHashCode()
        {
            return DisplayText.GetHashCode();
        }

        /// <summary>
        /// IsDefaultAttribute
        /// </summary>
        /// <returns>结果</returns>
        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }
    }
}