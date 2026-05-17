/******************************************************************************
 * 2024-12-11: 增加联合国六种语言、繁体中文、日语
******************************************************************************/
using System.Collections.Generic;
using System.Globalization;

namespace Sunny.UI
{
    public class CultureInfos_zh_CN : CultureInfosOfLanguage
    {
        public override CultureInfo CultureInfo => CultureInfos.zh_CN;
    }

    public abstract class CultureInfosOfLanguage
    {
        public abstract CultureInfo CultureInfo { get; }
        /// <summary>
        /// 语言名称检索表
        /// </summary>
        public virtual Dictionary<int,string> LCIDNAME { get; set; } = new Dictionary<int, string>() 
        {
            {CultureInfos.LCID_zh_CN, "简体中文" },
            {CultureInfos.LCID_zh_TW, "繁体中文" },
            {CultureInfos.LCID_en_US, "English" },
            {CultureInfos.LCID_ja_JP, "日本語" },
            {CultureInfos.LCID_es_ES_tradnl, "Español" },              //西班牙语
            {CultureInfos.LCID_fr_FR, "Français" },                    //法语
            {CultureInfos.LCID_ru_RU, "Русский язык" },                //俄语
            {CultureInfos.LCID_ar_SA, "بالعربية" },                        //阿拉伯语
        };
    }
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
        /// 1041 日语 日本
        /// </summary>
        public const int LCID_ja_JP = 1041;

        /// <summary>
        /// 1034 西班牙语 西班牙
        /// </summary>
        public const int LCID_es_ES_tradnl = 1034;

        /// <summary>
        /// 1025 阿拉伯语 沙特阿拉伯
        /// </summary>
        public const int LCID_ar_SA = 1025;

        /// <summary>
        /// 1036 法语 法国
        /// </summary>
        public const int LCID_fr_FR = 1036;

        /// <summary>
        /// 1049 俄语 俄罗斯
        /// </summary>
        public const int LCID_ru_RU = 1049;

        /// <summary>
        /// 2052 简体中文 中国大陆
        /// </summary>
        public const int LCID_zh_CN = 2052;

        /// <summary>
        /// 1028 繁体中文 中国台湾
        /// </summary>
        public const int LCID_zh_TW = 1028;

        /// <summary>
        /// 1033 英语 美国
        /// </summary>
        public const int LCID_en_US = 1033;

        /// <summary>
        /// 1041 日语 日本
        /// </summary>
        public static readonly CultureInfo ja_JP = CultureInfo.GetCultureInfo(LCID_ja_JP);

        /// <summary>
        /// 1034 西班牙语 西班牙
        /// </summary>
        public static readonly CultureInfo es_ES_tradnl = CultureInfo.GetCultureInfo(LCID_es_ES_tradnl);

        /// <summary>
        /// 1025 阿拉伯语 沙特阿拉伯
        /// </summary>
        public static readonly CultureInfo ar_SA = CultureInfo.GetCultureInfo(LCID_ar_SA);

        /// <summary>
        /// 1036 法语 法国
        /// </summary>
        public static readonly CultureInfo fr_FR = CultureInfo.GetCultureInfo(LCID_fr_FR);

        /// <summary>
        /// 1049 俄语 俄罗斯
        /// </summary>
        public static readonly CultureInfo ru_RU = CultureInfo.GetCultureInfo(LCID_ru_RU);

        /// <summary>
        /// 2052 简体中文 中国大陆
        /// </summary>
        public static readonly CultureInfo zh_CN = CultureInfo.GetCultureInfo(LCID_zh_CN);

        /// <summary>
        /// 1028 繁体中文 中国台湾
        /// </summary>
        public static readonly CultureInfo zh_TW = CultureInfo.GetCultureInfo(LCID_zh_TW);

        /// <summary>
        /// 1033 英语 美国
        /// </summary>
        public static readonly CultureInfo en_US = CultureInfo.GetCultureInfo(LCID_en_US);

    }
}
