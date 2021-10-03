namespace Sunny.UI
{
    public static class UIOther
    {
        public static bool IsNan(this double d)
        {
            return double.IsNaN(d);
        }

        public static bool IsNan(this float d)
        {
            return float.IsNaN(d);
        }

        public static bool IsInfinity(this double d)
        {
            return double.IsInfinity(d);
        }

        public static bool IsInfinity(this float d)
        {
            return float.IsInfinity(d);
        }

        public static bool IsNanOrInfinity(this double d)
        {
            return d.IsNan() || d.IsInfinity();
        }

        public static bool IsNanOrInfinity(this float d)
        {
            return d.IsNan() || d.IsInfinity();
        }
    }
}
