using Sunny.UI.Win32;
using System.Collections.Generic;
using System.Linq;

namespace Sunny.UI
{
    /// <summary>
    /// 其他
    /// </summary>
    public static class UIOther
    {
        /// <summary>
        /// 值不为数字
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>不为数字</returns>
        public static bool IsNan(this double d)
        {
            return double.IsNaN(d);
        }

        /// <summary>
        /// 值不为数字
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>不为数字</returns>
        public static bool IsNan(this float d)
        {
            return float.IsNaN(d);
        }

        /// <summary>
        /// 值负无穷或者正无穷
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>负无穷或者正无穷</returns>
        public static bool IsInfinity(this double d)
        {
            return double.IsInfinity(d);
        }

        /// <summary>
        /// 值负无穷或者正无穷
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>负无穷或者正无穷</returns>
        public static bool IsInfinity(this float d)
        {
            return float.IsInfinity(d);
        }

        /// <summary>
        /// 值不为数字或者负无穷或者正无穷
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>不为数字或者负无穷或者正无穷</returns>
        public static bool IsNanOrInfinity(this double d)
        {
            return d.IsNan() || d.IsInfinity();
        }

        /// <summary>
        /// 值不为数字或者负无穷或者正无穷
        /// </summary>
        /// <param name="d">值</param>
        /// <returns>不为数字或者负无穷或者正无穷</returns>
        public static bool IsNanOrInfinity(this float d)
        {
            return d.IsNan() || d.IsInfinity();
        }

        /// <summary>
        /// 自然排序
        /// </summary>
        /// <param name="strs">字符串列表</param>
        /// <returns>自然排序结果</returns>
        /// var names = new [] { "2.log", "10.log", "1.log" };
        /// 排序结果：
        /// 1.log
        /// 2.log
        /// 10.log
        public static IOrderedEnumerable<string> NatualOrdering(this IEnumerable<string> strs)
        {
            if (strs == null) return null;
            return strs.OrderBy(s => s, new NatualOrderingComparer());
        }
    }
}
