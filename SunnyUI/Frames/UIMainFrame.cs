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
 * 文件名称: UIMainFrame.cs
 * 文件说明: 页面框架基类
 * 当前版本: V2.2
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 页面框架基类
******************************************************************************/

using System;
using System.ComponentModel;

namespace Sunny.UI
{
    public partial class UIMainFrame : UIForm
    {
        public UIMainFrame()
        {
            InitializeComponent();
            MainContainer.TabVisible = false;
            MainContainer.BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            MainContainer.BringToFront();
            base.OnShown(e);
        }

        public UIPage AddPage(UIPage page, int index)
        {
            page.PageIndex = index;
            MainContainer.AddPage(page);
            return page;
        }

        public UIPage AddPage(UIPage page, Guid guid)
        {
            page.PageGuid = guid;
            MainContainer.AddPage(page);
            return page;
        }

        public UIPage AddPage(UIPage page)
        {
            MainContainer.AddPage(page);
            return page;
        }

        public void SelectPage(int pageIndex)
        {
            MainContainer.SelectPage(pageIndex);
        }

        protected UITabControl MainTabControl => MainContainer;

        public bool TabVisible
        {
            get => MainContainer.TabVisible;
            set => MainContainer.TabVisible = value;
        }

        public bool TabShowCloseButton
        {
            get => MainContainer.ShowCloseButton;
            set => MainContainer.ShowCloseButton = value;
        }

        public bool TabShowActiveCloseButton
        {
            get => MainContainer.ShowActiveCloseButton;
            set => MainContainer.ShowActiveCloseButton = value;
        }
    }
}