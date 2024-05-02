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
 * 文件名称: UINavMenuHelper.cs
 * 文件说明: 导航菜单帮助类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2022-04-14: V3.1.3 重构扩展函数
 * 2022-11-29: V3.3.0 重构RemovePage方法
 * 2023-10-26: V3.5.1 字体图标增加旋转角度参数SymbolRotate
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    internal class NavMenuHelper
    {
        public NavMenuItem this[TreeNode node]
        {
            get
            {
                if (node == null) return null;
                return Items.ContainsKey(node) ? Items[node] : null;
            }
        }

        public void Clear()
        {
            Items.Clear();
        }

        private readonly ConcurrentDictionary<TreeNode, NavMenuItem> Items = new ConcurrentDictionary<TreeNode, NavMenuItem>();

        public string GetTipsText(TreeNode node)
        {
            return this[node] == null ? string.Empty : Items[node].TipsText;
        }

        public Guid GetGuid(TreeNode node)
        {
            return this[node] == null ? Guid.Empty : Items[node].PageGuid;
        }

        public int GetSymbol(TreeNode node)
        {
            return this[node] == null ? 0 : Items[node].Symbol;
        }

        public int GetSymbolSize(TreeNode node)
        {
            return this[node] == null ? 0 : Items[node].SymbolSize;
        }

        public Point GetSymbolOffset(TreeNode node)
        {
            return this[node] == null ? new Point(0, 0) : Items[node].SymbolOffset;
        }

        public int GetSymbolRotate(TreeNode node)
        {
            return this[node] == null ? 0 : Items[node].SymbolRotate;
        }

        public int GetPageIndex(TreeNode node)
        {
            return this[node] == null ? -1 : Items[node].PageIndex;
        }

        public object GetTag(TreeNode node)
        {
            return this[node] == null ? null : Items[node].Tag;
        }

        public void SetTipsText(TreeNode node, string tips)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].TipsCustom = false;
            Items[node].TipsText = tips;
            node.TreeView.Invalidate();
        }

        public void SetTipsText(TreeNode node, string tips, Color tipsBackColor, Color tipsForeColor)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].TipsCustom = true;
            Items[node].TipsText = tips;
            Items[node].TipsBackColor = tipsBackColor;
            Items[node].TipsForeColor = tipsForeColor;
            node.TreeView.Invalidate();
        }

        public void SetPageIndex(TreeNode node, int index)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].PageIndex = index;
        }

        public void SetPageGuid(TreeNode node, Guid guid)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].PageGuid = guid;
        }

        public void SetSymbol(TreeNode node, int symbol, int symbolSize = 32, int symbolRotate = 0)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].Symbol = symbol;
            Items[node].SymbolSize = symbolSize;
            Items[node].SymbolRotate = symbolRotate;
            node.TreeView.Invalidate();
        }

        public void SetSymbol(TreeNode node, int symbol, Point symbolOffset, int symbolSize = 32, int symbolRotate = 0)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].Symbol = symbol;
            Items[node].SymbolSize = symbolSize;
            Items[node].SymbolOffset = symbolOffset;
            Items[node].SymbolRotate = symbolRotate;
            node.TreeView.Invalidate();
        }

        private void CreateIfNotExist(TreeNode node)
        {
            if (node == null) return;

            if (!Items.ContainsKey(node))
            {
                NavMenuItem menuItem = new NavMenuItem();
                Items.TryAdd(node, menuItem);
            }
        }

        public void Add(TreeNode node, NavMenuItem item)
        {
            if (node == null || item == null) return;

            if (this[node] == null)
            {
                Items.TryAdd(node, item);
            }
            else
            {
                Items[node] = item;
            }

            node.ImageIndex = item.ImageIndex;
            node.SelectedImageIndex = item.SelectedImageIndex;
            node.Tag = item;
        }

        public TreeNode GetTreeNode(int pageIndex)
        {
            foreach (var pair in Items)
            {
                if (pair.Value.PageIndex == pageIndex)
                {
                    return pair.Key;
                }
            }

            return null;
        }

        public TreeNode GetTreeNode(Guid guid)
        {
            foreach (var pair in Items)
            {
                if (pair.Value.PageGuid == guid)
                {
                    return pair.Key;
                }
            }

            return null;
        }
    }

    public class UITabControlHelper
    {
        private readonly ConcurrentDictionary<TabPage, NavMenuItem> PageItems = new ConcurrentDictionary<TabPage, NavMenuItem>();

        private readonly UITabControl tabControl;

        public UITabControlHelper(UITabControl ctrl)
        {
            tabControl = ctrl;
        }

        public NavMenuItem this[int index]
        {
            get
            {
                if (index < 0 || index >= tabControl.TabPages.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                TabPage page = tabControl.TabPages[index];
                return PageItems.ContainsKey(page) ? PageItems[page] : null;
            }
        }

        public void SetTipsText(int pageIndex, string tipsText)
        {
            TabPage tabPage = CreateTabIfNotExists(pageIndex);
            tabControl.SetTipsText(tabPage, tipsText);
        }

        public void SetTipsText(Guid pageGuid, string tipsText)
        {
            TabPage tabPage = CreateTabIfNotExists(pageGuid);
            tabControl.SetTipsText(tabPage, tipsText);
        }

        public UIPage AddPage(int pageIndex, UIPage page)
        {
            page.PageIndex = pageIndex;
            return AddPage(page);
        }

        public UIPage AddPage(Guid pageGuid, UIPage page)
        {
            page.PageGuid = pageGuid;
            return AddPage(page);
        }

        public UIPage AddPage(UIPage page)
        {
            if (page.PageGuid == Guid.Empty && page.PageIndex < 0)
            {
                page.PageGuid = Guid.NewGuid();
            }

            TabPage tabPage = CreateTabIfNotExists(new NavMenuItem(page));
            page.Dock = DockStyle.Fill;
            page.TabPage = tabPage;
            tabPage.Controls.Add(page);
            tabPage.Text = page.Text;
            page.Show();

            return page;
        }

        public void AddPages(params UIPage[] pages)
        {
            foreach (var page in pages)
            {
                AddPage(page);
            }
        }

        public void AddPage(int index, UITabControlMenu page)
        {
            TabPage tabPage = CreateTabIfNotExists(index);
            tabPage.Controls.Add(page);
            page.Dock = DockStyle.Fill;
            page.Show();
        }

        public void AddPage(int index, UITabControl page)
        {
            TabPage tabPage = CreateTabIfNotExists(index);
            tabPage.Controls.Add(page);
            page.Dock = DockStyle.Fill;
            page.Show();
        }

        public void AddPage(Guid guid, UITabControlMenu page)
        {
            TabPage tabPage = CreateTabIfNotExists(guid);
            tabPage.Controls.Add(page);
            page.Dock = DockStyle.Fill;
            page.Show();
        }

        public void AddPage(Guid guid, UITabControl page)
        {
            TabPage tabPage = CreateTabIfNotExists(guid);
            tabPage.Controls.Add(page);
            page.Dock = DockStyle.Fill;
            page.Show();
        }

        private TabPage CreateTabIfNotExists(NavMenuItem item)
        {
            if (item == null) return null;
            if (item.PageIndex < 0 && item.PageGuid == Guid.Empty) return null;

            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                TabPage page = tabControl.TabPages[i];

                if (!PageItems.ContainsKey(page))
                {
                    if (page.Controls.Count == 0)
                    {
                        PageItems.TryAdd(page, item);
                        return page;
                    }
                }
                else
                {
                    if (item.PageGuid != Guid.Empty)
                    {
                        if (PageItems[page].PageGuid == item.PageGuid) return page;
                    }
                    else
                    {
                        if (item.PageIndex >= 0 && PageItems[page].PageIndex == item.PageIndex) return page;
                    }
                }
            }

            TabPage newPage = new TabPage();
            newPage.SuspendLayout();
            newPage.Text = "tabPage" + tabControl.TabPages.Count;
            tabControl.Controls.Add(newPage);
            PageItems.TryAdd(newPage, item);
            newPage.ResumeLayout();
            return newPage;
        }

        private TabPage CreateTabIfNotExists(int pageIndex)
        {
            return CreateTabIfNotExists(new NavMenuItem("", pageIndex));
        }

        private TabPage CreateTabIfNotExists(Guid guid)
        {
            return CreateTabIfNotExists(new NavMenuItem("", guid));
        }

        public event TabPageAndUIPageEventHandler TabPageAndUIPageChanged;

        public bool SelectPage(int pageIndex)
        {
            if (pageIndex < 0)
            {
                return false;
            }

            List<UIPage> pages = tabControl.SelectedTab.GetControls<UIPage>();
            if (pages.Count == 1)
            {
                bool isCancel = pages[0].OnPageDeselecting();
                if (isCancel) return false;
            }

            foreach (var item in PageItems)
            {
                if (item.Value.PageIndex == pageIndex && item.Key != null)
                {
                    if (tabControl.TabPages.Contains(item.Key))
                    {
                        tabControl.SelectTab(item.Key);
                        TabPageAndUIPageChanged?.Invoke(this, new TabPageAndUIPageArgs(item.Key, item.Value.PageIndex, item.Value.PageGuid));
                        return true;
                    }
                }
            }

            return false;
        }

        public bool SelectPage(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return false;
            }

            List<UIPage> pages = tabControl.SelectedTab.GetControls<UIPage>();
            if (pages.Count == 1)
            {
                bool isCancel = pages[0].OnPageDeselecting();
                if (isCancel) return false;
            }

            foreach (var item in PageItems)
            {
                if (item.Value.PageGuid == guid && item.Key != null)
                {
                    if (tabControl.TabPages.Contains(item.Key))
                    {
                        tabControl.SelectTab(item.Key);
                        TabPageAndUIPageChanged?.Invoke(this, new TabPageAndUIPageArgs(item.Key, item.Value.PageIndex, item.Value.PageGuid));
                        return true;
                    }
                }
            }

            return false;
        }

        public UIPage GetPage(int pageIndex)
        {
            var pages = GetPages<UIPage>();
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].PageIndex == pageIndex)
                    return pages[i];
            }

            return null;
        }

        public T GetPage<T>() where T : UIPage
        {
            List<T> result = GetPages<T>();
            return result.Count > 0 ? result[0] : null;
        }

        public List<T> GetPages<T>() where T : UIPage
        {
            List<T> result = new List<T>();
            foreach (var item in PageItems)
            {
                if (item.Key != null)
                {
                    var tabPage = item.Key;
                    var pages = tabPage.GetControls<T>();
                    for (int i = 0; i < pages.Count; i++)
                    {
                        if (pages[i] is T pg)
                            result.Add(pg);
                    }
                }
            }

            return result;
        }

        public UIPage GetPage(Guid guid)
        {
            if (guid == Guid.Empty) return null;
            var pages = GetPages<UIPage>();
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].PageGuid == guid)
                    return pages[i];
            }

            return null;
        }

        public void RemoveAllPages(bool keepMainPage = true)
        {
            var pages = GetPages<UIPage>();
            foreach (var page in pages)
            {
                if (keepMainPage && page.TabPage?.Text == tabControl.MainPage) continue;
                RemovePage(page.PageIndex);
            }
        }

        public bool RemovePage(int pageIndex)
        {
            foreach (var item in PageItems)
            {
                if (item.Value.PageIndex == pageIndex)
                {
                    UIPage page = GetPage(pageIndex);
                    if (page != null)
                    {
                        TabPage tabpage = page.TabPage;
                        page.Final();
                        page.Dispose();
                        page = null;

                        if (tabpage != null)
                        {
                            tabpage.Parent = null;
                            tabpage.Dispose();
                            tabpage = null;
                        }
                    }

                    PageItems.TryRemove(item.Key, out _);
                    return true;
                }
            }

            return false;
        }

        public bool RemovePage(Guid guid)
        {
            if (guid == Guid.Empty) return false;
            foreach (var item in PageItems)
            {
                if (item.Value.PageGuid == guid)
                {
                    UIPage page = GetPage(guid);
                    if (page != null)
                    {
                        TabPage tabpage = page.TabPage;
                        page.Dispose();
                        page = null;

                        if (tabpage != null)
                        {
                            tabpage.Parent = null;
                            tabpage.Dispose();
                            tabpage = null;
                        }
                    }

                    PageItems.TryRemove(item.Key, out _);
                    return true;
                }
            }

            return false;
        }
    }

    public class NavMenuItem : ISymbol
    {
        public string Text { get; set; }

        public int ImageIndex { get; set; } = -1;

        public int SelectedImageIndex { get; set; } = -1;

        /// <summary>
        /// 字体图标
        /// </summary>
        public int Symbol { get; set; }

        /// <summary>
        /// 字体图标大小
        /// </summary>
        public int SymbolSize { get; set; } = 24;

        /// <summary>
        /// 字体图标的偏移位置
        /// </summary>
        public Point SymbolOffset { get; set; } = new Point(0, 0);

        /// <summary>
        /// 字体图标旋转角度
        /// </summary>
        public int SymbolRotate { get; set; } = 0;

        public int PageIndex { get; set; }

        public string TipsText { get; set; }

        public bool TipsCustom { get; set; }

        public Color TipsBackColor { get; set; }

        public Color TipsForeColor { get; set; }

        public bool Enabled { get; set; } = true;

        public object Tag { get; set; }

        public Guid PageGuid { get; set; } = Guid.Empty;

        public bool AlwaysOpen { get; set; }


        public NavMenuItem()
        {
        }

        public NavMenuItem(UIPage page)
        {
            Text = page.Text;
            PageIndex = page.PageIndex;
            PageGuid = page.PageGuid;
            Symbol = page.Symbol;
            SymbolSize = page.SymbolSize;
            SymbolOffset = page.SymbolOffset;
            SymbolRotate = page.SymbolRotate;
            AlwaysOpen = page.AlwaysOpen;
        }

        public NavMenuItem(string text, int pageIndex)
        {
            PageIndex = pageIndex;
            Text = text;
        }

        public NavMenuItem(string text, Guid guid)
        {
            PageGuid = guid;
            Text = text;
        }
    }

    public class TabPageAndUIPageArgs : EventArgs
    {
        public TabPage TabPage { get; set; }

        public int PageIndex { get; set; }
        public Guid PageGuid { get; set; }

        public TabPageAndUIPageArgs(TabPage tabPage, int pageIndex, Guid pageGuid)
        {
            TabPage = tabPage;
            PageIndex = pageIndex;
            PageGuid = pageGuid;
        }
    }

    public delegate void TabPageAndUIPageEventHandler(object sender, TabPageAndUIPageArgs e);
}