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
 * 文件名称: UKeyScope.cs
 * 文件说明: 整数范围及集合
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sunny.UI
{
    /// <summary>
    /// KeyScope 用于表示一个整数范围。显示为：3..12
    /// </summary>
    public class KeyScope
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyScope()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="theMin">最小值</param>
        /// <param name="theMax">最大值</param>
        public KeyScope(int theMin, int theMax)
        {
            Min = theMin;
            Max = theMax;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scopeStr">字符串，以逗号分隔</param>
        public KeyScope(string scopeStr)
        {
            SetScopeStr(scopeStr);
        }

        private void SetScopeStr(string str)
        {
            str = str.Replace("..", ",");
            string[] parts = str.Split(',');
            Min = parts[0].Trim().ToInt(int.MinValue);
            Max = parts[1].Trim().ToInt(int.MaxValue);
        }

        /// <summary>
        /// ScopeString 以..分隔，如"10..1000"
        /// </summary>
        public string ScopeString
        {
            get => $"{Min}..{Max}";
            set => SetScopeStr(value);
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max { get; set; } = int.MinValue;

        /// <summary>
        /// 最小值
        /// </summary>
        public int Min { get; set; } = int.MaxValue;

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="val">整数</param>
        /// <returns>是否包含</returns>
        public bool Contains(int val)
        {
            return Min <= val && val <= Max;
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="scope">范围</param>
        /// <returns>是否包含</returns>
        public bool Contains(KeyScope scope)
        {
            return scope.Min >= Min && scope.Max <= Max;
        }

        /// <summary>
        /// 交叉
        /// </summary>
        /// <param name="scope">范围</param>
        /// <returns>是否交叉</returns>
        public bool Intersect(KeyScope scope)
        {
            for (int i = Min; i < Max; i++)
            {
                if (scope.Contains(i))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return ScopeString;
        }
    }

    /// <summary>
    /// KeyScopes 用于表示多个整数范围和一组离散的整数值
    /// </summary>
    public class KeyScopes
    {
        private readonly SortedSet<int> array = new SortedSet<int>();

        /// <summary>
        /// 最小值
        /// </summary>
        /// <returns>最小值</returns>
        public int Min => array.Min;

        /// <summary>
        /// 最大值
        /// </summary>
        /// <returns>最大值</returns>
        public int Max => array.Max;

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        public void Add(int value)
        {
            if (!array.Contains(value))
            {
                array.Add(value);
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        public void Add(KeyScope value)
        {
            for (int i = value.Min; i <= value.Max; i++)
            {
                Add(i);
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值，以..分隔的字符串</param>
        public void Add(string value)
        {
            KeyScope scope = new KeyScope(value);
            Add(scope);
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="value">整数</param>
        /// <returns>是否包含</returns>
        public bool Contains(int value)
        {
            return array.Contains(value);
        }

        /// <summary>
        /// 字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (array.Count == 0) return String.Empty;

            List<int> list = array.ToList();

            if (array.Count == 1) return list[0].ToString();
            if (array.Count == 2) return list[0].ToString() + list[1].ToString();

            int idx = 0;
            int min = Min;

            List<string> sb = new List<string>();

            while (idx < list.Count - 1)
            {
                idx++;
                if (list[idx] - list[idx - 1] > 1)
                {
                    if (min == list[idx - 1])
                        sb.Add(min.ToString());
                    else
                        sb.Add(min + ".." + list[idx - 1]);

                    min = list[idx];
                }
            }

            if (min == list[idx])
                sb.Add(min.ToString());
            else
                sb.Add(min + ".." + list[idx]);

            return string.Join(", ", sb.ToArray());
        }
    }
}