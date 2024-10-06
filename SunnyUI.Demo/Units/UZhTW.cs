using System.Globalization;

namespace Sunny.UI.Demo
{
    /// <summary>
    /// 多語言字符串定義
    /// </summary>
    public class zh_TW_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo { get; } = CultureInfos.zh_TW;
        public override string InfoTitle { get; set; } = "提示";
        public override string SuccessTitle { get; set; } = "正確";
        public override string WarningTitle { get; set; } = "警告";
        public override string ErrorTitle { get; set; } = "錯誤";
        public override string AskTitle { get; set; } = "提示";
        public override string InputTitle { get; set; } = "輸入";
        public override string SelectTitle { get; set; } = "選擇";
        public override string CloseAll { get; set; } = "全部關閉";
        public override string OK { get; set; } = "確定";
        public override string Cancel { get; set; } = "取消";
        public override string GridNoData { get; set; } = "[ 無資料 ]";
        public override string GridDataLoading { get; set; } = "資料加載中，請稍候...";
        public override string GridDataSourceException { get; set; } = "資料源必須為DataTable或者List";
        public override string SystemProcessing { get; set; } = "系統正在處理中，請稍候...";
        public override string Monday { get; set; } = "一";
        public override string Tuesday { get; set; } = "二";
        public override string Wednesday { get; set; } = "三";
        public override string Thursday { get; set; } = "四";
        public override string Friday { get; set; } = "五";
        public override string Saturday { get; set; } = "六";
        public override string Sunday { get; set; } = "日";
        public override string Prev { get; set; } = "上一頁";
        public override string Next { get; set; } = "下一頁";
        public override string SelectPageLeft { get; set; } = "第";
        public override string SelectPageRight { get; set; } = "頁";
        public override string January { get; set; } = "一月";
        public override string February { get; set; } = "二月";
        public override string March { get; set; } = "三月";
        public override string April { get; set; } = "四月";
        public override string May { get; set; } = "五月";
        public override string June { get; set; } = "六月";
        public override string July { get; set; } = "七月";
        public override string August { get; set; } = "八月";
        public override string September { get; set; } = "九月";
        public override string October { get; set; } = "十月";
        public override string November { get; set; } = "十一月";
        public override string December { get; set; } = "十二月";
        public override string Today { get; set; } = "今天";
        public override string Search { get; set; } = "搜尋";
        public override string Clear { get; set; } = "清除";
        public override string Open { get; set; } = "打開";
        public override string Save { get; set; } = "保存";
        public override string All { get; set; } = "全部";
    }
}
