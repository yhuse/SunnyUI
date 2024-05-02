/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 文件名称: UIMainFrame.cs
 * 文件说明: 页面框架基类
 * 当前版本: V3.1
 * 创建日期: 2020-05-05
 *
 * 2020-05-05: V2.2.5 页面框架基类
 * 2021-08-17: V3.0.8 删除IFrame接口，移到父类UIForm
 * 2022-05-17: V3.1.9 修复了显示页面关闭按钮，移除最后一个页面出错的问题
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UIMainFrame : UIForm
    {
        public UIMainFrame()
        {
            InitializeComponent();
            MainContainer.TabVisible = false;
            MainContainer.BringToFront();
            MainContainer.TabPages.Clear();

            MainContainer.BeforeRemoveTabPage += MainContainer_BeforeRemoveTabPage;
            MainContainer.AfterRemoveTabPage += MainContainer_AfterRemoveTabPage;
        }

        [DefaultValue(false)]
        [Description("多页面框架时，包含UIPage，在点击Tab页关闭时关闭UIPage"), Category("SunnyUI")]
        public bool AutoClosePage
        {
            get => MainContainer.AutoClosePage;
            set => MainContainer.AutoClosePage = value;
        }

        private void MainContainer_AfterRemoveTabPage(object sender, int index)
        {
            AfterRemoveTabPage?.Invoke(this, index);
        }

        private bool MainContainer_BeforeRemoveTabPage(object sender, int index)
        {
            return BeforeRemoveTabPage == null || BeforeRemoveTabPage.Invoke(this, index);
        }

        public event UITabControl.OnBeforeRemoveTabPage BeforeRemoveTabPage;

        public event UITabControl.OnAfterRemoveTabPage AfterRemoveTabPage;


        protected override void OnShown(EventArgs e)
        {
            MainContainer.BringToFront();
            base.OnShown(e);
        }

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

        private void MainContainer_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (Selecting != null && e.TabPage != null)
            {
                List<UIPage> pages = e.TabPage.GetControls<UIPage>();
                Selecting?.Invoke(this, e, pages.Count == 0 ? null : pages[0]);
            }
        }

        public delegate void OnSelecting(object sender, TabControlCancelEventArgs e, UIPage page);

        [Description("页面选择事件"), Category("SunnyUI")]
        public event OnSelecting Selecting;
    }
}