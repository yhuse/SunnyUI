/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 文件名称: UIFormHelper.cs
 * 文件说明: 多语言字符串定义
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-04-19: V2.2.0 增加文件说明
 * 2021-07-24: V3.0.5 内置字符串已经处理完国际化
******************************************************************************/

namespace Sunny.UI
{
    /// <summary>
    /// 多语言字符串定义
    /// </summary>
    public static class UILocalize
    {
        /// <summary>
        /// 提示
        /// </summary>
        public static string InfoTitle = "提示";

        /// <summary>
        /// 正确
        /// </summary>
        public static string SuccessTitle = "正确";

        /// <summary>
        /// 警告
        /// </summary>
        public static string WarningTitle = "警告";

        /// <summary>
        /// 错误
        /// </summary>
        public static string ErrorTitle = "错误";

        /// <summary>
        /// 提示
        /// </summary>
        public static string AskTitle = "提示";

        /// <summary>
        /// 输入
        /// </summary>
        public static string InputTitle = "输入";

        /// <summary>
        /// 选择
        /// </summary>
        public static string SelectTitle = "选择";

        /// <summary>
        /// 全部关闭
        /// </summary>
        public static string CloseAll = "全部关闭";

        /// <summary>
        /// 确定
        /// </summary>
        public static string OK = "确定";

        /// <summary>
        /// 取消
        /// </summary>
        public static string Cancel = "取消";

        /// <summary>
        /// [ 无数据 ]
        /// </summary>
        public static string GridNoData = "[ 无数据 ]";

        /// <summary>
        /// 数据加载中，请稍候...
        /// </summary>
        public static string GridDataLoading = "数据加载中，请稍候...";

        /// <summary>
        /// 数据源必须为DataTable或者List
        /// </summary>
        public static string GridDataSourceException = "数据源必须为DataTable或者List";

        /// <summary>
        /// "系统正在处理中，请稍候..."
        /// </summary>
        public static string SystemProcessing = "系统正在处理中，请稍候...";

        /// <summary>
        /// 星期一
        /// </summary>
        public static string Monday = "一";

        /// <summary>
        /// 星期二
        /// </summary>
        public static string Tuesday = "二";

        /// <summary>
        /// 星期三
        /// </summary>
        public static string Wednesday = "三";

        /// <summary>
        /// 星期四
        /// </summary>
        public static string Thursday = "四";

        /// <summary>
        /// 星期五
        /// </summary>
        public static string Friday = "五";

        /// <summary>
        /// 星期六
        /// </summary>
        public static string Saturday = "六";

        /// <summary>
        /// 星期日
        /// </summary>
        public static string Sunday = "日";

        /// <summary>
        /// 上一页
        /// </summary>
        public static string Prev = "上一页";

        /// <summary>
        /// 下一页
        /// </summary>
        public static string Next = "下一页";

        /// <summary>
        /// 第
        /// </summary>
        public static string SelectPageLeft = "第";

        /// <summary>
        /// 页
        /// </summary>
        public static string SelectPageRight = "页";

        /// <summary>
        /// 一月
        /// </summary>
        public static string January = "一月";

        /// <summary>
        /// 二月
        /// </summary>
        public static string February = "二月";

        /// <summary>
        /// 三月
        /// </summary>
        public static string March = "三月";

        /// <summary>
        /// 四月
        /// </summary>
        public static string April = "四月";

        /// <summary>
        /// 五月
        /// </summary>
        public static string May = "五月";

        /// <summary>
        /// 六月
        /// </summary>
        public static string June = "六月";

        /// <summary>
        /// 七月
        /// </summary>
        public static string July = "七月";

        /// <summary>
        /// 八月
        /// </summary>
        public static string August = "八月";

        /// <summary>
        /// 九月
        /// </summary>
        public static string September = "九月";

        /// <summary>
        /// 十月
        /// </summary>
        public static string October = "十月";

        /// <summary>
        /// 十一月
        /// </summary>
        public static string November = "十一月";

        /// <summary>
        /// 十二月
        /// </summary>
        public static string December = "十二月";

        /// <summary>
        /// 今天
        /// </summary>
        public static string Today = "今天";

        /// <summary>
        /// 搜索
        /// </summary>
        public static string Search = "搜索";

        /// <summary>
        /// 清除
        /// </summary>
        public static string Clear = "清除";

        public static string Open = "打开";
        public static string Save = "保存";
    }

    /// <summary>
    /// 多语言字符串帮助类
    /// </summary>
    public static class UILocalizeHelper
    {
        /// <summary>
        /// 设置为英文
        /// </summary>
        public static void SetEN()
        {
            UILocalize.InfoTitle = "Info";
            UILocalize.SuccessTitle = "Success";
            UILocalize.WarningTitle = "Warning";
            UILocalize.ErrorTitle = "Error";
            UILocalize.AskTitle = "Query";
            UILocalize.InputTitle = "Input";
            UILocalize.SelectTitle = "Select";
            UILocalize.CloseAll = "Close all";
            UILocalize.OK = "OK";
            UILocalize.Cancel = "Cancel";
            UILocalize.GridNoData = "[ No data ]";
            UILocalize.GridDataLoading = "Data loading, please wait...";
            UILocalize.GridDataSourceException = "The data source must be DataTable or List";
            UILocalize.SystemProcessing = "The system is processing, please wait...";

            UILocalize.Monday = "MON";
            UILocalize.Tuesday = "TUE";
            UILocalize.Wednesday = "WED";
            UILocalize.Thursday = "THU";
            UILocalize.Friday = "FRI";
            UILocalize.Saturday = "SAT";
            UILocalize.Sunday = "SUN";

            UILocalize.Prev = "Previous";
            UILocalize.Next = "Next";
            UILocalize.SelectPageLeft = "Page";
            UILocalize.SelectPageRight = "";

            UILocalize.January = "Jan.";
            UILocalize.February = "Feb.";
            UILocalize.March = "Mar.";
            UILocalize.April = "Apr.";
            UILocalize.May = "May";
            UILocalize.June = "Jun.";
            UILocalize.July = "Jul.";
            UILocalize.August = "Aug.";
            UILocalize.September = "Sep.";
            UILocalize.October = "Oct.";
            UILocalize.November = "Nov.";
            UILocalize.December = "Dec.";

            UILocalize.Today = "Today";

            UILocalize.Search = "Search";
            UILocalize.Clear = "Clear";

            UILocalize.Open = "Open";
            UILocalize.Save = "Save";

            UIStyles.Translate();
        }

        /// <summary>
        /// 设置为中文
        /// </summary>
        public static void SetCH()
        {
            UILocalize.InfoTitle = "提示";
            UILocalize.SuccessTitle = "正确";
            UILocalize.WarningTitle = "警告";
            UILocalize.ErrorTitle = "错误";
            UILocalize.AskTitle = "提示";
            UILocalize.InputTitle = "输入";
            UILocalize.SelectTitle = "选择";
            UILocalize.CloseAll = "全部关闭";
            UILocalize.OK = "确定";
            UILocalize.Cancel = "取消";
            UILocalize.GridNoData = "[ 无数据 ]";
            UILocalize.GridDataLoading = "数据加载中，请稍候...";
            UILocalize.GridDataSourceException = "数据源必须为DataTable或者List";
            UILocalize.SystemProcessing = "系统正在处理中，请稍候...";

            UILocalize.Monday = "一";
            UILocalize.Tuesday = "二";
            UILocalize.Wednesday = "三";
            UILocalize.Thursday = "四";
            UILocalize.Friday = "五";
            UILocalize.Saturday = "六";
            UILocalize.Sunday = "日";

            UILocalize.Prev = "上一页";
            UILocalize.Next = "下一页";

            UILocalize.SelectPageLeft = "第";
            UILocalize.SelectPageRight = "页";

            UILocalize.January = "一月";
            UILocalize.February = "二月";
            UILocalize.March = "三月";
            UILocalize.April = "四月";
            UILocalize.May = "五月";
            UILocalize.June = "六月";
            UILocalize.July = "七月";
            UILocalize.August = "八月";
            UILocalize.September = "九月";
            UILocalize.October = "十月";
            UILocalize.November = "十一月";
            UILocalize.December = "十二月";

            UILocalize.Today = "今天";

            UILocalize.Search = "搜索";
            UILocalize.Clear = "清除";

            UILocalize.Open = "打开";
            UILocalize.Save = "保存";

            UIStyles.Translate();
        }
    }
}