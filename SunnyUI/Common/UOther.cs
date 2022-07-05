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
    }
}
