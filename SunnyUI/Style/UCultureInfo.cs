using System.Globalization;

namespace Sunny.UI
{
    public static class CultureInfos
    {
        //语言文件对应ID网址
        //https://docs.microsoft.com/en-us/previous-versions/windows/embedded/ms912047(v=winembedded.10)?redirectedfrom=MSDN
        //https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c
        //ID语言 ID语言
        //1025 阿拉伯语
        //1041 日语
        //1028 繁体中文
        //1042 朝鲜语
        //1029 捷克语
        //1043 荷兰语
        //1030 丹麦语
        //1044 挪威语
        //1031 德语
        //1045 波兰语
        //1032 希腊语
        //1046 葡萄牙语 - 巴西
        //1033 英语
        //1049 俄语
        //1034 西班牙语
        //1053 瑞典语
        //1035 芬兰语
        //1054 泰语
        //1036 法语
        //1055 土耳其语
        //1037 希伯来语
        //2052 简体中文
        //1038 匈牙利语
        //2070 葡萄牙语
        //1040 意大利语

        /// <summary>
        /// 2052 简体中文
        /// </summary>
        public const int LCID_zh_CN = 2052;

        /// <summary>
        /// 1028 繁体中文
        /// </summary>
        public const int LCID_zh_TW = 1028;

        /// <summary>
        /// 1033 英语
        /// </summary>
        public const int LCID_en_US = 1033;

        /// <summary>
        /// 2052 简体中文
        /// </summary>
        public static readonly CultureInfo zh_CN = CultureInfo.GetCultureInfo(LCID_zh_CN);

        /// <summary>
        /// 1028 繁体中文
        /// </summary>
        public static readonly CultureInfo zh_TW = CultureInfo.GetCultureInfo(LCID_zh_TW);

        /// <summary>
        /// 1033 英语
        /// </summary>
        public static readonly CultureInfo en_US = CultureInfo.GetCultureInfo(LCID_en_US);
    }
}
