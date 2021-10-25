using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
