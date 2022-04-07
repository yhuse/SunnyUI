/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: IFrame.cs
 * 文件说明: 页面框架接口
 * 当前版本: V3.1
 * 创建日期: 2020-12-01
 *
 * 2021-12-01: V3.0.9 创建文档
******************************************************************************/

using System;
using System.Collections.Generic;

namespace Sunny.UI
{
    public interface IFrame
    {
        UITabControl MainTabControl { get; }

        UIPage AddPage(UIPage page, int index);

        UIPage AddPage(UIPage page, Guid guid);

        UIPage AddPage(UIPage page);

        bool SelectPage(int pageIndex);

        bool SelectPage(Guid guid);

        UIPage GetPage(int pageIndex);

        UIPage GetPage(Guid guid);

        bool TopMost { get; set; }

        bool RemovePage(int pageIndex);

        bool RemovePage(Guid guid);

        void FeedbackFormPage(int fromPageIndex, params object[] objects);

        bool ExistPage(int index);

        bool ExistPage(Guid guid);

        bool SetParamToPage(int toPageIndex, int fromPageIndex, params object[] objects);

        bool SetParamToPage(Guid toPageGuid, Guid fromPageGuid, params object[] objects);

        void Init();

        void Final();

        T GetPage<T>() where T : UIPage;

        List<T> GetPages<T>() where T : UIPage;
    }
}
