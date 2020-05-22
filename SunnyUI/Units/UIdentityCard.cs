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
 * 文件名称: UIdentityCard.cs
 * 文件说明: 身份证校验类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    /// <summary>
    /// 身份证校验类
    /// </summary>
    public class IdentityCard
    {
        #region 属性

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public enum SexType
        {
            /// <summary>
            /// 男
            /// </summary>
            Man,

            /// <summary>
            /// 女
            /// </summary>
            Woman
        }

        /// <summary>
        /// 性别
        /// </summary>
        public SexType Sex { get; set; }

        /// <summary>
        /// 是否15位旧卡
        /// </summary>
        public bool IsOld { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string AreaNum { get; set; }

        #endregion 属性

        #region 验证

        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Valid(string card)
        {
            try
            {
                if (Parse(card) != null)
                {
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        #endregion 验证

        #region 分析

        private static readonly Dictionary<string, IdentityCard> Cache = new Dictionary<string, IdentityCard>();

        /// <summary>
        /// 使用身份证号码初始化
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>IdentityCard.</returns>
        public static IdentityCard Parse(string card)
        {
            if (string.IsNullOrEmpty(card))
            {
                return null;
            }

            if (card.Length != 15 && card.Length != 18)
            {
                return null;
            }

            //转为小写，18位身份证后面有个字母x
            card = card.ToLower();

            if (Cache.TryGetValue(card, out var ic))
            {
                return ic;
            }

            lock (Cache)
            {
                if (Cache.TryGetValue(card, out ic))
                {
                    return ic;
                }

                var idc = Parse2(card);
                Cache.Add(card, idc);
                return idc;
            }
        }

        private static IdentityCard Parse2(string card)
        {
            var area = card.Substring(0, 6);

            var idc = new IdentityCard
            {
                AreaNum = ParseArea(area)
            };

            if (card.Length == 15)
            {
                idc.ParseBirthdayAndSex15(card);
            }
            else if (card.Length == 18)
            {
                idc.ParseBirthdayAndSex18(card);

                //校验码验证  GB11643-1999标准
                var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                var Wi = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                var Ai = card.Remove(17).ToCharArray();
                var sum = 0;
                for (var i = 0; i < 17; i++)
                {
                    sum += Wi[i] * (Ai[i] - '0');
                }

                Math.DivRem(sum, 11, out var y);
                if (arrVarifyCode[y] != card.Substring(17, 1).ToLower())
                {
                    throw new Exception("验证码校验失败！");
                }
            }

            return idc;
        }

        #endregion 分析

        #region 分析地区

        private static readonly List<int> AreaList = new List<int>(new[] { 11, 22, 35, 44, 53, 12, 23, 36, 45, 54, 13, 31, 37, 46, 61, 14, 32, 41, 50, 62, 15, 33, 42, 51, 63, 21, 34, 43, 52, 64, 65, 71, 81, 82, 91 });

        private static string ParseArea(string area)
        {
            if (!int.TryParse(area, out var n))
            {
                throw new Exception("非法地区编码！");
            }

            var str = area.Substring(0, 2);
            if (!int.TryParse(str, out n))
            {
                throw new Exception("非法省份编码！");
            }

            if (!AreaList.Contains(n))
            {
                throw new Exception("没有找到该省份！");
            }

            return area;
        }

        #endregion 分析地区

        #region 分析生日、性别

        private void ParseBirthdayAndSex15(string card)
        {
            var str = card.Substring(6, 2);
            var n = int.Parse(str);
            n = n < 20 ? 20 : 19;

            var birth = n + card.Substring(6, 6).Insert(2, "-").Insert(5, "-");
            if (!DateTime.TryParse(birth, out var d))
            {
                throw new Exception("生日不正确！");
            }

            Birthday = d;

            //最后一位是性别
            n = Convert.ToInt32(card.Substring(card.Length - 1, 1));
            var man = n % 2 != 0;
            Sex = man ? SexType.Man : SexType.Woman;
        }

        private void ParseBirthdayAndSex18(string card)
        {
            var birth = card.Substring(6, 8).Insert(4, "-").Insert(7, "-");
            if (!DateTime.TryParse(birth, out var d))
            {
                throw new Exception("生日不正确！");
            }

            Birthday = d;

            //倒数第二位是性别
            var n = Convert.ToInt32(card.Substring(card.Length - 2, 1));
            var man = n % 2 != 0;
            Sex = man ? SexType.Man : SexType.Woman;
        }

        #endregion 分析生日、性别
    }
}