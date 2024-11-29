using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UITabControlHelper
    {
        private readonly ConcurrentDictionary<TabPage, UIPage> PageItems = new();

        private readonly UITabControl tabControl;

        public UITabControlHelper(UITabControl ctrl)
        {
            tabControl = ctrl;
        }

        public void SetTipsText(int pageIndex, string tipsText)
        {
            TabPage tabPage = GetTabPage(pageIndex);
            if (tabPage != null) tabControl.SetTipsText(tabPage, tipsText);
        }

        public void SetTipsText(Guid pageGuid, string tipsText)
        {
            TabPage tabPage = GetTabPage(pageGuid);
            if (tabPage != null) tabControl.SetTipsText(tabPage, tipsText);
        }

        public UIPage AddPage(UIPage uiPage)
        {
            if (uiPage == null) throw new ArgumentNullException("This UIPage can't be empty.");

            if (uiPage.PageGuid == Guid.Empty && uiPage.PageIndex < 0)
            {
                uiPage.PageGuid = Guid.NewGuid();
            }

            if (PageItems != null && PageItems.Count > 0)
            {
                if (PageItems.Values.Where(p => p == uiPage).FirstOrDefault() != null)
                {
                    throw new ArgumentException("This UIPage is already exists.");
                }

                if (uiPage.PageGuid != Guid.Empty)
                {
                    if (GetPage(uiPage.PageGuid) != null)
                    {
                        throw new ArgumentException("This UIPage is already exists.");
                    }
                }
                else if (uiPage.PageIndex >= 0)
                {
                    if (GetPage(uiPage.PageIndex) != null)
                    {
                        throw new ArgumentException("This UIPage is already exists.");
                    }
                }
            }

            TabPage tabPage = CreateTabIfNotExists(uiPage);
            uiPage.Dock = DockStyle.Fill;
            uiPage.TabPage = tabPage;
            tabPage.Controls.Add(uiPage);
            tabPage.Text = uiPage.Text;
            uiPage.Show();

            return uiPage;
        }

        private TabPage CreateTabIfNotExists(UIPage uiPage)
        {
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Controls.Count == 0)
                {
                    PageItems.TryAdd(tabPage, uiPage);
                    return tabPage;
                }
            }

            TabPage newPage = new TabPage();
            newPage.SuspendLayout();
            tabControl.Controls.Add(newPage);
            PageItems.TryAdd(newPage, uiPage);
            newPage.ResumeLayout();
            return newPage;
        }

        private TabPage GetTabPage(int pageIndex) => GetPage(pageIndex)?.TabPage;

        private TabPage GetTabPage(Guid pageGuid) => GetPage(pageGuid)?.TabPage;

        public event EventHandler<TabPageAndUIPageArgs> TabPageAndUIPageChanged;

        public bool SelectPage(int pageIndex)
        {
            if (pageIndex < 0) return false;

            if (tabControl.SelectedTab != null && PageItems.TryGetValue(tabControl.SelectedTab, out var fromPage))
            {
                bool isCancel = fromPage.OnPageDeselecting();
                if (isCancel) return false;
            }

            TabPage tabPage = GetTabPage(pageIndex);
            if (tabPage != null && PageItems.TryGetValue(tabPage, out var toPage))
            {
                toPage.Translate();
                tabControl.SelectTab(tabPage);
                TabPageAndUIPageChanged?.Invoke(this, new TabPageAndUIPageArgs(tabPage, toPage));
                return true;
            }

            return false;
        }

        public bool SelectPage(Guid pageGuid)
        {
            if (pageGuid == Guid.Empty) return false;

            if (tabControl.SelectedTab != null && PageItems.TryGetValue(tabControl.SelectedTab, out var fromPage))
            {
                bool isCancel = fromPage.OnPageDeselecting();
                if (isCancel) return false;
            }

            TabPage tabPage = GetTabPage(pageGuid);
            if (tabPage != null && PageItems.TryGetValue(tabPage, out var toPage))
            {
                toPage.Translate();
                tabControl.SelectTab(tabPage);
                TabPageAndUIPageChanged?.Invoke(this, new TabPageAndUIPageArgs(tabPage, toPage));
                return true;
            }

            return false;
        }

        public UIPage GetPage(TabPage tabPage)
        {
            if (tabControl.SelectedTab != null && PageItems.TryGetValue(tabControl.SelectedTab, out var uiPage))
                return uiPage;
            return null;
        }

        public UIPage GetPage(int pageIndex) =>
            PageItems.Values.Where(p => p.PageIndex == pageIndex).FirstOrDefault();

        public UIPage GetPage(Guid pageGuid) =>
            PageItems.Values.Where(p => p.PageGuid == pageGuid).FirstOrDefault();

        public T GetPage<T>() where T : UIPage => GetPages<T>().FirstOrDefault();

        public List<T> GetPages<T>() where T : UIPage
        {
            List<T> result = new List<T>();
            foreach (var item in PageItems.Values)
            {
                if (item is T pg) result.Add(pg);
            }

            return result;
        }

        public void RemoveAllPages(bool keepMainPage = true)
        {
            var pages = PageItems.Values;
            foreach (var page in pages)
            {
                if (keepMainPage && page.TabPage?.Text == tabControl.MainPage) continue;
                RemovePage(page);
            }
        }

        public bool RemovePage(UIPage uiPage)
        {
            if (uiPage == null) return false;
            TabPage tabPage = uiPage.TabPage;
            PageItems.TryRemove(tabPage, out _);
            if (tabPage != null)
            {
                tabPage.Parent = null;
                tabPage.Dispose();
                tabPage = null;
            }

            uiPage.Final();
            uiPage.Close();
            uiPage.Dispose();
            uiPage = null;

            return true;
        }

        public bool RemovePage(int pageIndex) => RemovePage(GetPage(pageIndex));

        public bool RemovePage(Guid pageGuid) => RemovePage(GetPage(pageGuid));
    }

    public class TabPageAndUIPageArgs : EventArgs
    {
        public TabPage TabPage { get; set; }

        public UIPage UIPage { get; set; }

        public TabPageAndUIPageArgs(TabPage tabPage, UIPage uiPage)
        {
            TabPage = tabPage;
            UIPage = uiPage;
        }
    }
}
