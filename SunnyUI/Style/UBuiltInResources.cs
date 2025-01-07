/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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

using System.Globalization;

namespace Sunny.UI
{
    public class zh_CN_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.zh_CN;
    }

    public class en_US_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.en_US;
        public override string InfoTitle { get; set; } = "Info";
        public override string SuccessTitle { get; set; } = "Success";
        public override string WarningTitle { get; set; } = "Warning";
        public override string ErrorTitle { get; set; } = "Error";
        public override string AskTitle { get; set; } = "Query";
        public override string InputTitle { get; set; } = "Input";
        public override string SelectTitle { get; set; } = "Select";
        public override string CloseAll { get; set; } = "Close all";
        public override string OK { get; set; } = "OK";
        public override string Cancel { get; set; } = "Cancel";
        public override string GridNoData { get; set; } = "[ No data ]";
        public override string GridDataLoading { get; set; } = "Data loading, please wait...";
        public override string GridDataSourceException { get; set; } = "The data source must be DataTable or List";
        public override string SystemProcessing { get; set; } = "The system is processing, please wait...";
        public override string Monday { get; set; } = "MON";
        public override string Tuesday { get; set; } = "TUE";
        public override string Wednesday { get; set; } = "WED";
        public override string Thursday { get; set; } = "THU";
        public override string Friday { get; set; } = "FRI";
        public override string Saturday { get; set; } = "SAT";
        public override string Sunday { get; set; } = "SUN";
        public override string Prev { get; set; } = "Prev";
        public override string Next { get; set; } = "Next";
        public override string SelectPageLeft { get; set; } = "Page";
        public override string SelectPageRight { get; set; } = "";
        public override string January { get; set; } = "Jan.";
        public override string February { get; set; } = "Feb.";
        public override string March { get; set; } = "Mar.";
        public override string April { get; set; } = "Apr.";
        public override string May { get; set; } = "May";
        public override string June { get; set; } = "Jun.";
        public override string July { get; set; } = "Jul.";
        public override string August { get; set; } = "Aug.";
        public override string September { get; set; } = "Sep.";
        public override string October { get; set; } = "Oct.";
        public override string November { get; set; } = "Nov.";
        public override string December { get; set; } = "Dec.";
        public override string Today { get; set; } = "Today";
        public override string Search { get; set; } = "Search";
        public override string Clear { get; set; } = "Clear";
        public override string Open { get; set; } = "Open";
        public override string Save { get; set; } = "Save";
        public override string All { get; set; } = "All";
        public override string EditorCantEmpty { get; set; } = "The editor content cannot be empty.";
    }

    /// <summary>
    /// 多语言字符串定义
    /// </summary>
    public abstract class UIBuiltInResources
    {
        public abstract CultureInfo CultureInfo { get; }

        /// <summary>
        /// 提示
        /// </summary>
        public virtual string InfoTitle { get; set; } = "提示";

        /// <summary>
        /// 正确
        /// </summary>
        public virtual string SuccessTitle { get; set; } = "正确";

        /// <summary>
        /// 警告
        /// </summary>
        public virtual string WarningTitle { get; set; } = "警告";

        /// <summary>
        /// 错误
        /// </summary>
        public virtual string ErrorTitle { get; set; } = "错误";

        /// <summary>
        /// 提示
        /// </summary>
        public virtual string AskTitle { get; set; } = "提示";

        /// <summary>
        /// 输入
        /// </summary>
        public virtual string InputTitle { get; set; } = "输入";

        /// <summary>
        /// 选择
        /// </summary>
        public virtual string SelectTitle { get; set; } = "选择";

        /// <summary>
        /// 全部关闭
        /// </summary>
        public virtual string CloseAll { get; set; } = "全部关闭";

        /// <summary>
        /// 确定
        /// </summary>
        public virtual string OK { get; set; } = "确定";

        /// <summary>
        /// 取消
        /// </summary>
        public virtual string Cancel { get; set; } = "取消";

        /// <summary>
        /// [ 无数据 ]
        /// </summary>
        public virtual string GridNoData { get; set; } = "[ 无数据 ]";

        /// <summary>
        /// 数据加载中，请稍候...
        /// </summary>
        public virtual string GridDataLoading { get; set; } = "数据加载中，请稍候...";

        /// <summary>
        /// 数据源必须为DataTable或者List
        /// </summary>
        public virtual string GridDataSourceException { get; set; } = "数据源必须为DataTable或者List";

        /// <summary>
        /// "系统正在处理中，请稍候..."
        /// </summary>
        public virtual string SystemProcessing { get; set; } = "系统正在处理中，请稍候...";

        /// <summary>
        /// 星期一
        /// </summary>
        public virtual string Monday { get; set; } = "一";

        /// <summary>
        /// 星期二
        /// </summary>
        public virtual string Tuesday { get; set; } = "二";

        /// <summary>
        /// 星期三
        /// </summary>
        public virtual string Wednesday { get; set; } = "三";

        /// <summary>
        /// 星期四
        /// </summary>
        public virtual string Thursday { get; set; } = "四";

        /// <summary>
        /// 星期五
        /// </summary>
        public virtual string Friday { get; set; } = "五";

        /// <summary>
        /// 星期六
        /// </summary>
        public virtual string Saturday { get; set; } = "六";

        /// <summary>
        /// 星期日
        /// </summary>
        public virtual string Sunday { get; set; } = "日";

        /// <summary>
        /// 上一页
        /// </summary>
        public virtual string Prev { get; set; } = "上一页";

        /// <summary>
        /// 下一页
        /// </summary>
        public virtual string Next { get; set; } = "下一页";

        /// <summary>
        /// 第
        /// </summary>
        public virtual string SelectPageLeft { get; set; } = "第";

        /// <summary>
        /// 页
        /// </summary>
        public virtual string SelectPageRight { get; set; } = "页";

        /// <summary>
        /// 一月
        /// </summary>
        public virtual string January { get; set; } = "一月";

        /// <summary>
        /// 二月
        /// </summary>
        public virtual string February { get; set; } = "二月";

        /// <summary>
        /// 三月
        /// </summary>
        public virtual string March { get; set; } = "三月";

        /// <summary>
        /// 四月
        /// </summary>
        public virtual string April { get; set; } = "四月";

        /// <summary>
        /// 五月
        /// </summary>
        public virtual string May { get; set; } = "五月";

        /// <summary>
        /// 六月
        /// </summary>
        public virtual string June { get; set; } = "六月";

        /// <summary>
        /// 七月
        /// </summary>
        public virtual string July { get; set; } = "七月";

        /// <summary>
        /// 八月
        /// </summary>
        public virtual string August { get; set; } = "八月";

        /// <summary>
        /// 九月
        /// </summary>
        public virtual string September { get; set; } = "九月";

        /// <summary>
        /// 十月
        /// </summary>
        public virtual string October { get; set; } = "十月";

        /// <summary>
        /// 十一月
        /// </summary>
        public virtual string November { get; set; } = "十一月";

        /// <summary>
        /// 十二月
        /// </summary>
        public virtual string December { get; set; } = "十二月";

        /// <summary>
        /// 今天
        /// </summary>
        public virtual string Today { get; set; } = "今天";

        /// <summary>
        /// 搜索
        /// </summary>
        public virtual string Search { get; set; } = "搜索";

        /// <summary>
        /// 清除
        /// </summary>
        public virtual string Clear { get; set; } = "清除";

        public virtual string Open { get; set; } = "打开";
        public virtual string Save { get; set; } = "保存";

        public virtual string All { get; set; } = "全部";

        public virtual string EditorCantEmpty { get; set; } = "编辑框内容不能为空。";
    }
}