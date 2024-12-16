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
 * 2024-12-11: v3.1   增加多语言翻译 联合国语言、繁体中文和日语
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
    }
    /// <summary>
    /// 繁体中文
    /// </summary>
    public class zh_TW_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.zh_TW;
        public override string SuccessTitle { get; set; } = "正確";
        public override string ErrorTitle { get; set; } = "錯誤";
        public override string InputTitle { get; set; } = "輸入";
        public override string SelectTitle { get; set; } = "選擇";
        public override string CloseAll { get; set; } = "全部關閉";
        public override string OK { get; set; } = "確定";
        public override string GridNoData { get; set; } = "[ 無數據 ]";
        public override string GridDataLoading { get; set; } = "數據加載中，請稍候...";
        public override string GridDataSourceException { get; set; } = "數據源必須爲DataTable或者List";
        public override string SystemProcessing { get; set; } = "系統正在處理中，請稍候...";
        public override string Prev { get; set; } = "上一頁";
        public override string Next { get; set; } = "下一頁";
        public override string SelectPageRight { get; set; } = "頁";
        public override string Open { get; set; } = "打開";
    }
    /// <summary>
    /// 西班牙语
    /// </summary>
    public class es_ES_tradnl_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.es_ES_tradnl;
        public override string InfoTitle { get; set; } = "Info";
        public override string SuccessTitle { get; set; } = "éxito";
        public override string WarningTitle { get; set; } = "Aviso";
        public override string ErrorTitle { get; set; } = "Error";
        public override string AskTitle { get; set; } = "Pregunta";
        public override string InputTitle { get; set; } = "Entrada";
        public override string SelectTitle { get; set; } = "Selección";
        public override string CloseAll { get; set; } = "Todos cerrados";
        public override string OK { get; set; } = "Vale";
        public override string Cancel { get; set; } = "Cancelar";
        public override string GridNoData { get; set; } = "[ sin datos ]";
        public override string GridDataLoading { get; set; } = "Cargando datos, por favor Espere...";
        public override string GridDataSourceException { get; set; } = "La fuente de datos debe ser datatable o List";
        public override string SystemProcessing { get; set; } = "El sistema está en proceso, por favor Espere...";
        public override string Monday { get; set; } = "Lun";
        public override string Tuesday { get; set; } = "Mar";
        public override string Wednesday { get; set; } = "Mié";
        public override string Thursday { get; set; } = "Jue";
        public override string Friday { get; set; } = "Vie";
        public override string Saturday { get; set; } = "Sáb";
        public override string Sunday { get; set; } = "Dom";
        public override string Prev { get; set; } = "Anterior";
        public override string Next { get; set; } = "Siguiente";
        public override string SelectPageLeft { get; set; } = "Página";
        public override string SelectPageRight { get; set; } = "";
        public override string January { get; set; } = "Eno.";
        public override string February { get; set; } = "Fbro.";
        public override string March { get; set; } = "Mzo.";
        public override string April { get; set; } = "Ab.";
        public override string May { get; set; } = "Mayo";
        public override string June { get; set; } = "Jun.";
        public override string July { get; set; } = "Jul.";
        public override string August { get; set; } = "Agto.";
        public override string September { get; set; } = "Sbre.";
        public override string October { get; set; } = "Obre.";
        public override string November { get; set; } = "Nbre.";
        public override string December { get; set; } = "Dbre.";
        public override string Today { get; set; } = "Hoy.";
        public override string Search { get; set; } = "Buscar";
        public override string Clear { get; set; } = "Eliminar";
        public override string Open { get; set; } = "Abrir";
        public override string Save { get; set; } = "Guardar";
        public override string All { get; set; } = "Todo";
    }
    /// <summary>
    /// 法语
    /// </summary>
    public class fr_FR_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.fr_FR;
        public override string InfoTitle { get; set; } = "Info";
        public override string SuccessTitle { get; set; } = "Succès";
        public override string WarningTitle { get; set; } = "Avertissement";
        public override string ErrorTitle { get; set; } = "Erreur";
        public override string AskTitle { get; set; } = "Enquête";
        public override string InputTitle { get; set; } = "Entrée";
        public override string SelectTitle { get; set; } = "Sélection";
        public override string CloseAll { get; set; } = "Tous fermés";
        public override string OK { get; set; } = "Bon";
        public override string Cancel { get; set; } = "Annuler‌";
        public override string GridNoData { get; set; } = "[ pas de données ]";
        public override string GridDataLoading { get; set; } = "Chargement des données, veuillez patienter...";
        public override string GridDataSourceException { get; set; } = "La source de données doit être datatable ou List";
        public override string SystemProcessing { get; set; } = "Le système est en cours de traitement, veuillez patienter...";
        public override string Monday { get; set; } = "Lun";
        public override string Tuesday { get; set; } = "Mar";
        public override string Wednesday { get; set; } = "Mer";
        public override string Thursday { get; set; } = "Jeu";
        public override string Friday { get; set; } = "Ven";
        public override string Saturday { get; set; } = "Sam";
        public override string Sunday { get; set; } = "Dim";
        public override string Prev { get; set; } = "Précédent";
        public override string Next { get; set; } = "Suivant";
        public override string SelectPageLeft { get; set; } = "Page";
        public override string SelectPageRight { get; set; } = "";
        public override string January { get; set; } = "Jan.";
        public override string February { get; set; } = "Fév.";
        public override string March { get; set; } = "Mar.";
        public override string April { get; set; } = "Avr.";
        public override string May { get; set; } = "May";
        public override string June { get; set; } = "Jun.";
        public override string July { get; set; } = "Jul.";
        public override string August { get; set; } = "Aug.";
        public override string September { get; set; } = "Sep.";
        public override string October { get; set; } = "Oct.";
        public override string November { get; set; } = "Nov.";
        public override string December { get; set; } = "Dec.";
        public override string Today { get; set; } = "Aujourd'hui";
        public override string Search { get; set; } = "Rechercher";
        public override string Clear { get; set; } = "Effacer";
        public override string Open { get; set; } = "Ouvrir";
        public override string Save { get; set; } = "Sauvegarder";
        public override string All { get; set; } = "Tous";
    }
    /// <summary>
    /// 俄语
    /// </summary>
    public class ru_RU_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.ru_RU;
        public override string InfoTitle { get; set; } = "Информация";
        public override string SuccessTitle { get; set; } = "Успех";
        public override string WarningTitle { get; set; } = "предупреждение";
        public override string ErrorTitle { get; set; } = "Ошибка";
        public override string AskTitle { get; set; } = "Запрос";
        public override string InputTitle { get; set; } = "Ввод";
        public override string SelectTitle { get; set; } = "Выбор";
        public override string CloseAll { get; set; } = "Закрыть все";
        public override string OK { get; set; } = "Ладно";
        public override string Cancel { get; set; } = "Отменить";
        public override string GridNoData { get; set; } = "[ Данные отсутствуют ]";
        public override string GridDataLoading { get; set; } = "Загружаются данные, пожалуйста, подождите немного...";
        public override string GridDataSourceException { get; set; } = "Источник данных должен быть DataTable или List";
        public override string SystemProcessing { get; set; } = "Система находится в процессе обработки, пожалуйста, подождите...";
        public override string Monday { get; set; } = "Пн";
        public override string Tuesday { get; set; } = "Вт";
        public override string Wednesday { get; set; } = "Ср";
        public override string Thursday { get; set; } = "Чт";
        public override string Friday { get; set; } = "Пт";
        public override string Saturday { get; set; } = "Сб";
        public override string Sunday { get; set; } = "Вс";
        public override string Prev { get; set; } = "Предыдущий";
        public override string Next { get; set; } = "Следующий";
        public override string SelectPageLeft { get; set; } = "Страница";
        public override string SelectPageRight { get; set; } = "";
        public override string January { get; set; } = "Янв";
        public override string February { get; set; } = "Фев";
        public override string March { get; set; } = "Мар";
        public override string April { get; set; } = "Апр";
        public override string May { get; set; } = "Май";
        public override string June { get; set; } = "Июн";
        public override string July { get; set; } = "Июл";
        public override string August { get; set; } = "Авг";
        public override string September { get; set; } = "Сен";
        public override string October { get; set; } = "Окт";
        public override string November { get; set; } = "Ноя";
        public override string December { get; set; } = "Дек";
        public override string Today { get; set; } = "Сегодня";
        public override string Search { get; set; } = "Поиск";
        public override string Clear { get; set; } = "Очистить";
        public override string Open { get; set; } = "Открыть";
        public override string Save { get; set; } = "Сохранить";
        public override string All { get; set; } = "Все";
    }
    /// <summary>
    /// 阿拉伯语
    /// </summary>
    public class ar_SA_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.ar_SA;
        public override string InfoTitle { get; set; } = "معلومات";
        public override string SuccessTitle { get; set; } = "النجاح";
        public override string WarningTitle { get; set; } = "حذر";
        public override string ErrorTitle { get; set; } = "خطأ";
        public override string AskTitle { get; set; } = "استعلم عن";
        public override string InputTitle { get; set; } = "المدخلات";
        public override string SelectTitle { get; set; } = "مختار";
        public override string CloseAll { get; set; } = "إغلاق جميع";
        public override string OK { get; set; } = "حَسَناً";
        public override string Cancel { get; set; } = "ألغى";
        public override string GridNoData { get; set; } = "[ لا توجد بيانات ]";
        public override string GridDataLoading { get; set; } = "تحميل البيانات ، الرجاء الانتظار ...";
        public override string GridDataSourceException { get; set; } = "مصدر البيانات يجب أن تكون دیتاتیبل أو قائمة";
        public override string SystemProcessing { get; set; } = "الرجاء الانتظار بينما النظام في العملية ...";
        public override string Monday { get; set; } = "الأحد";
        public override string Tuesday { get; set; } = "السبت";
        public override string Wednesday { get; set; } = "الجمعة";
        public override string Thursday { get; set; } = "الخميس";
        public override string Friday { get; set; } = "الأربعاء";
        public override string Saturday { get; set; } = "الثلاثاء";
        public override string Sunday { get; set; } = "الاثنين";
        public override string Prev { get; set; } = "آخر";
        public override string Next { get; set; } = "التالي";
        public override string SelectPageLeft { get; set; } = "الصفحة";
        public override string SelectPageRight { get; set; } = "";
        public override string January { get; set; } = "يناير";
        public override string February { get; set; } = "فبراير";
        public override string March { get; set; } = "مارس";
        public override string April { get; set; } = "ابريل";
        public override string May { get; set; } = "مايو";
        public override string June { get; set; } = "يونيو";
        public override string July { get; set; } = "يوليو";
        public override string August { get; set; } = "اغسطس";
        public override string September { get; set; } = "سبتمبر";
        public override string October { get; set; } = "اكتوبر";
        public override string November { get; set; } = "نوفمبر";
        public override string December { get; set; } = "ديسمبر";
        public override string Today { get; set; } = "اليوم";
        public override string Search { get; set; } = "بحث";
        public override string Clear { get; set; } = "مسح";
        public override string Open { get; set; } = "فتح";
        public override string Save { get; set; } = "حفظ";
        public override string All { get; set; } = "كامل";
    }
    /// <summary>
    /// 日语
    /// </summary>
    public class ja_JP_Resources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => CultureInfos.ja_JP;
        public override string InfoTitle { get; set; } = "メッセージ";
        public override string SuccessTitle { get; set; } = "正しい";
        public override string WarningTitle { get; set; } = "警告";
        public override string ErrorTitle { get; set; } = "エラー";
        public override string AskTitle { get; set; } = "ヒント";
        public override string InputTitle { get; set; } = "インプット";
        public override string SelectTitle { get; set; } = "せんたく";
        public override string CloseAll { get; set; } = "すべて閉じる";
        public override string OK { get; set; } = "確認";
        public override string Cancel { get; set; } = "キャンセル";
        public override string GridNoData { get; set; } = "[ データなし ]";
        public override string GridDataLoading { get; set; } = "データ・ロード中、お待ちください...";
        public override string GridDataSourceException { get; set; } = "データソースはDataTableまたはListでなければなりません";
        public override string SystemProcessing { get; set; } = "システムは処理中です。お待ちください…";
        public override string Monday { get; set; } = "月";
        public override string Tuesday { get; set; } = "火";
        public override string Wednesday { get; set; } = "水";
        public override string Thursday { get; set; } = "木";
        public override string Friday { get; set; } = "金";
        public override string Saturday { get; set; } = "土";
        public override string Sunday { get; set; } = "日";
        public override string Prev { get; set; } = "前のページ";
        public override string Next { get; set; } = "次のページ";
        public override string SelectPageLeft { get; set; } = "";
        public override string SelectPageRight { get; set; } = "ページ";
        public override string January { get; set; } = "むつ";
        public override string February { get; set; } = "さらぎ";
        public override string March { get; set; } = "やよい";
        public override string April { get; set; } = "うづ";
        public override string May { get; set; } = "さつ";
        public override string June { get; set; } = "みなづ";
        public override string July { get; set; } = "ふみづ";
        public override string August { get; set; } = "はづ";
        public override string September { get; set; } = "ながつ";
        public override string October { get; set; } = "かんなづ";
        public override string November { get; set; } = "しもつ";
        public override string December { get; set; } = "しわす";
        public override string Today { get; set; } = "今日";
        public override string Search { get; set; } = "検索";
        public override string Clear { get; set; } = "パージ";
        public override string Open { get; set; } = "開く";
        public override string Save { get; set; } = "保存";
        public override string All { get; set; } = "すべて";
    }
    /// <summary>
    /// 多语言字符串定义
    /// </summary>
    public abstract class UIBuiltInResources
    {
        public abstract CultureInfo CultureInfo { get; }

        /// <summary>
        /// 消息
        /// </summary>
        public virtual string InfoTitle { get; set; } = "消息";

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
    }
}
