/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.Common.dll can be used for free under the MIT license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UConvertEx.cs
 * 文件说明: 对象转换扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-11-20: V3.0.0 增加文件说明
 ******************************************************************************/

using System;
using System.Drawing;

namespace Sunny.UI
{
    /// <summary>
    /// 对象转换扩展类
    /// </summary>
    public static class ConvertEx
    {
        /// <summary>
        /// 可以对象和字符串互相转换的类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool CanConvent(Type type)
        {
            bool result = false;
            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.String:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Boolean:
                case TypeCode.DateTime:
                    result = true;
                    break;

                case TypeCode.Object:
                    if (type == typeof(Point) ||
                        type == typeof(PointF) ||
                        type == typeof(Color) ||
                        type == typeof(Size) ||
                        type == typeof(SizeF))
                    {
                        result = true;
                    }

                    break;
            }

            return result;
        }

        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="type">类型</param>
        /// <param name="defaultobj">默认对象</param>
        /// <returns>对象</returns>
        public static object StringToObject(string str, Type type, object defaultobj)
        {
            object obj = defaultobj;

            if (type == typeof(string))
            {
                return str;
            }

            if (str.IsNullOrEmpty())
            {
                return defaultobj;
            }

            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Char:
                    obj = str.ToChar((char)defaultobj);
                    break;

                case TypeCode.SByte:
                    obj = str.ToSByte((sbyte)defaultobj);
                    break;

                case TypeCode.Byte:
                    obj = str.ToByte((byte)defaultobj);
                    break;

                case TypeCode.Int16:
                    obj = str.ToInt16((short)defaultobj);
                    break;

                case TypeCode.UInt16:
                    obj = str.ToUInt16((ushort)defaultobj);
                    break;

                case TypeCode.Int32:
                    obj = str.ToInt32((int)defaultobj);
                    break;

                case TypeCode.UInt32:
                    obj = str.ToUInt32((uint)defaultobj);
                    break;

                case TypeCode.Int64:
                    obj = str.ToInt64((long)defaultobj);
                    break;

                case TypeCode.UInt64:
                    obj = str.ToUInt64((ulong)defaultobj);
                    break;

                case TypeCode.Single:
                    obj = str.ToSingle((float)defaultobj);
                    break;

                case TypeCode.Double:
                    obj = str.ToDouble((double)defaultobj);
                    break;

                case TypeCode.Decimal:
                    obj = str.ToDecimal((decimal)defaultobj);
                    break;

                case TypeCode.Boolean:
                    if (str.ToUpper().Equals(bool.TrueString.ToUpper()))
                    {
                        obj = true;
                    }

                    if (str.ToUpper().Equals(bool.FalseString.ToUpper()))
                    {
                        obj = false;
                    }

                    break;

                case TypeCode.DateTime:
                    try
                    {
                        DateTime dt = str.ToDateTime(DateTimeEx.DateTimeFormat);
                        obj = dt;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    break;

                case TypeCode.Object:
                    obj = StringToObjectEx(str, type, defaultobj);
                    break;

                default:
                    throw new ApplicationException("不支持此类型: " + type.FullName);
            }

            if (type == typeof(DateTime))
            {
            }

            return obj;
        }

        /// <summary>
        /// 字符串转对象扩展
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="type">类型</param>
        /// <param name="defaultobj">默认对象</param>
        /// <returns>对象</returns>
        private static object StringToObjectEx(string str, Type type, object defaultobj)
        {
            object obj = defaultobj;

            if (type == typeof(Point))
            {
                string[] strs = str.ToUpper().Replace("X:", "").Replace("Y:", "").Trim().Split(";");

                if (strs.Length == 2)
                {
                    if (int.TryParse(strs[0], out var x) && int.TryParse(strs[1], out var y))
                    {
                        obj = new Point(x, y);
                    }
                }
            }
            else if (type == typeof(PointF))
            {
                string[] strs = str.ToUpper().Replace("X:", "").Replace("Y:", "").Trim().Split(";");

                if (strs.Length == 2)
                {
                    if (float.TryParse(strs[0], out var x) && float.TryParse(strs[1], out var y))
                    {
                        obj = new PointF(x, y);
                    }
                }
            }
            else if (type == typeof(Color))
            {
                string[] strs = str.ToUpper().Replace("A:", "").Replace("R:", "").Replace("G:", "").Replace("B:", "").Trim().Split(";");

                if (strs.Length == 4)
                {
                    if (int.TryParse(strs[0], out var a) && int.TryParse(strs[1], out var r) && int.TryParse(strs[2], out var g) && int.TryParse(strs[3], out var b))
                    {
                        if (a.InRange(0, 255) && r.InRange(0, 255) && g.InRange(0, 255) && b.InRange(0, 255))
                        {
                            obj = Color.FromArgb(a, r, g, b);
                        }
                    }
                }
            }
            else if (type == typeof(Size))
            {
                string[] strs = str.ToUpper().Replace("W:", "").Replace("H:", "").Trim().Split(";");

                if (strs.Length == 2)
                {
                    if (int.TryParse(strs[0], out var x) && int.TryParse(strs[1], out var y))
                    {
                        obj = new Size(x, y);
                    }
                }
            }
            else if (type == typeof(SizeF))
            {
                string[] strs = str.ToUpper().Replace("W:", "").Replace("H:", "").Trim().Split(";");

                if (strs.Length == 2)
                {
                    if (float.TryParse(strs[0], out var x) && float.TryParse(strs[1], out var y))
                    {
                        obj = new SizeF(x, y);
                    }
                }
            }
            else
            {
                throw new ApplicationException("不支持此类型: " + type.FullName);
            }

            return obj;
        }

        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>字符串</returns>
        public static string ObjectToString(object obj, Type type)
        {
            string value;

            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.String:
                    if (obj == null)
                    {
                        obj = "";
                    }

                    value = obj.ToString();
                    break;

                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    value = obj.ToString();
                    break;

                case TypeCode.Boolean:
                    value = (bool)obj ? bool.TrueString : bool.FalseString;
                    break;

                case TypeCode.DateTime:
                    value = ((DateTime)obj).ToString(DateTimeEx.DateTimeFormat);
                    break;

                case TypeCode.Object:
                    value = ObjectToStringEx(obj, type);
                    break;

                default:
                    throw new ApplicationException("不支持此类型: " + type.FullName);
            }

            return value;
        }

        /// <summary>
        /// 对象转字符串扩展
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>字符串</returns>
        private static string ObjectToStringEx(object obj, Type type)
        {
            string value;

            if (type == typeof(Point))
            {
                Point point = (Point)obj;
                value = "X:" + point.X + "; Y:" + point.Y;
            }
            else if (type == typeof(PointF))
            {
                PointF point = (PointF)obj;
                value = "X:" + point.X + "; Y:" + point.Y;
            }
            else if (type == typeof(Color))
            {
                Color color = (Color)obj;
                value = "A:" + color.A + "; R:" + color.R + "; G:" + color.G + "; B:" + color.B;
            }
            else if (type == typeof(Size))
            {
                Size size = (Size)obj;
                value = "W:" + size.Width + "; H:" + size.Height;
            }
            else if (type == typeof(SizeF))
            {
                SizeF size = (SizeF)obj;
                value = "W:" + size.Width + "; H:" + size.Height;
            }
            else
            {
                throw new ApplicationException("不支持此类型: " + type.FullName);
            }

            return value;
        }
    }
}
