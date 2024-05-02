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
 * 文件名称: UHotKey.cs
 * 文件说明: 全局热键定义
 * 当前版本: V3.1
 * 创建日期: 2021-10-25
 *
 * 2021-10-25: V3.0.8 增加文件说明
******************************************************************************/

using System;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class HotKeyEventArgs : EventArgs
    {
        public readonly HotKey hotKey;
        public readonly DateTime dateTime;
        public HotKeyEventArgs(HotKey hotKey, DateTime time)
        {
            this.hotKey = hotKey;
            this.dateTime = time;
        }
    }

    public delegate void HotKeyEventHandler(object sender, HotKeyEventArgs e);

    public class HotKey : IEquatable<HotKey>
    {
        internal static int CalculateID(ModifierKeys modifiers, Keys key)
        {
            return (int)modifiers + ((int)key << 4);
        }

        public readonly ModifierKeys ModifierKey;
        public readonly Keys Key;
        public readonly int id;

        internal HotKey(ModifierKeys modifiers, Keys key)
        {
            this.ModifierKey = modifiers;
            this.Key = key;
            this.id = HotKey.CalculateID(modifiers, key);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is HotKey) && this.Equals(obj as HotKey);
        }

        public bool Equals(HotKey other)
        {
            return other != null && other.id == this.id;
        }

        public override string ToString()
        {
            return $"{ModifierKey}  {Key}";
        }
    }

    /// <summary>
    /// Specifies the set of modifier keys.
    /// flags enum, 4 bits, 0 == none
    /// 1111
    /// </summary>
    [Flags]
    public enum ModifierKeys
    {
        //
        // Summary:
        //     No modifiers are pressed.
        None = 0,
        //
        // Summary:
        //     The ALT key.
        Alt = 1,
        //
        // Summary:
        //     The CTRL key.
        Control = 2,
        //
        // Summary:
        //     The SHIFT key.
        Shift = 4,
        //
        // Summary:
        //     The Windows logo key.
        Windows = 8
    }
}
