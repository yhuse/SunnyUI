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
 * 文件名称: UINavMenuHelper.cs
 * 文件说明: 导航菜单帮助类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/


using System;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class NavMenuHelper
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
            Items[node].TipsText = tips;
            node.TreeView.Invalidate();
        }

        public void SetPageIndex(TreeNode node, int index)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].PageIndex = index;
        }

        public void SetGuid(TreeNode node, Guid guid)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].PageGuid = guid;
        }

        public void SetSymbol(TreeNode node, int symbol, int symbolSize = 32)
        {
            if (node == null) return;

            CreateIfNotExist(node);
            Items[node].Symbol = symbol;
            Items[node].SymbolSize = symbolSize;
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
            return CreateTabIfNotExists(new NavMenuItem(pageIndex));
        }

        private TabPage CreateTabIfNotExists(Guid guid)
        {
            return CreateTabIfNotExists(new NavMenuItem(guid));
        }

        public void SelectPage(int pageIndex)
        {
            if (pageIndex < 0) return;
            foreach (var item in PageItems)
            {
                if (item.Value.PageIndex == pageIndex && item.Key != null)
                {
                    if (tabControl.TabPages.Contains(item.Key))
                        tabControl.SelectTab(item.Key);
                }
            }
        }

        public void SelectPage(Guid guid)
        {
            if (guid == Guid.Empty) return;
            foreach (var item in PageItems)
            {
                if (item.Value.PageGuid == guid && item.Key != null)
                {
                    if (tabControl.TabPages.Contains(item.Key))
                        tabControl.SelectTab(item.Key);
                }
            }
        }
    }

    public class NavMenuItem
    {
        public string Text { get; set; }

        public int ImageIndex { get; set; } = -1;

        public int SelectedImageIndex { get; set; } = -1;

        public int Symbol { get; set; }

        public int SymbolSize { get; set; } = 24;

        public int PageIndex { get; set; }

        public string TipsText { get; set; }

        public bool Enabled { get; set; } = true;

        public object Tag { get; set; }

        public Guid PageGuid { get; set; } = Guid.Empty;

        public bool AlwaysOpen { get; set; } = false;

        public NavMenuItem()
        {
        }

        public NavMenuItem(UIPage page)
        {
            Text = page.Text;
            PageIndex = page.PageIndex;
            PageGuid = page.PageGuid;
            AlwaysOpen = page.AlwaysOpen;
        }

        public NavMenuItem(string text, int pageIndex)
        {
            PageIndex = pageIndex;
            Text = text;
        }

        public NavMenuItem(int pageIndex)
        {
            PageIndex = pageIndex;
        }

        public NavMenuItem(Guid guid)
        {
            PageGuid = guid;
        }
    }
}