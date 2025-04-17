/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 2024-08-07: V3.6.8 窗体移除时调用Close()
******************************************************************************/

using System;
using System.Collections.Concurrent;
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

    public enum NodeTextAlign
    {
        Left,
        Center,
        TextAreaCenter
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
}