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
 * 文件名称: UIFormHelper.cs
 * 文件说明: 多语言字符串定义
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-04-19: V2.2 增加文件说明
******************************************************************************/

namespace Sunny.UI
{
    public static class UILocalize
    {
        public static string InfoTitle = "提示";
        public static string SuccessTitle = "正确";
        public static string WarningTitle = "警告";
        public static string ErrorTitle = "错误";
        public static string AskTitle = "提示";
        public static string InputTitle = "输入";
        public static string SelectTitle = "选择";
        public static string CloseAll = "全部关闭";
        public static string OK = "确定";
        public static string Cancel = "取消";
        public static string GridNoData = "[ 无数据 ]";
        public static string GridDataLoading = "数据加载中 ......";
        public static string GridDataSourceException = "数据源必须为DataTable或者List";
    }

    public static class UILocalizeHelper
    {
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
            UILocalize.GridDataLoading = "Data loading ......";
            UILocalize.GridDataSourceException = "The data source must be DataTable or List";
        }

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
            UILocalize.GridDataLoading = "数据加载中 ......";
            UILocalize.GridDataSourceException = "数据源必须为DataTable或者List";
        }
    }
}