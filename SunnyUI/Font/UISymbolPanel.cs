using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(false)]
    public class UISymbolPanel : Control
    {
        public UISymbolPanel()
        {
            SetStyleFlags();
        }

        public UISymbolPanel(Type fonttype, UISymbolType symbolType, int columnCount = 24)
        {
            SetStyleFlags();
            LoadFont(fonttype, symbolType, columnCount);
        }

        private Color selectedColor = Color.Red;
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    Invalidate();
                }
            }
        }

        public void LoadFont(Type fonttype, UISymbolType symbolType, int columnCount = 24)
        {
            ColumnCount = columnCount;
            ConcurrentDictionary<int, FieldInfo> dic = new ConcurrentDictionary<int, FieldInfo>();
            foreach (var fieldInfo in fonttype.GetFields())
            {
                var obj = fieldInfo.GetRawConstantValue();
                if (obj is int value)
                {
                    dic.TryAdd(value, fieldInfo);
                }
            }

            RowCount = dic.Count / ColumnCount + 1;

            List<int> list = dic.Keys.ToList();
            list.Sort();
            for (int i = 0; i < GridCount; i++)
            {
                if (i >= list.Count) break;
                Add(new SymbolValue(list[i], dic[list[i]].Name.Replace("fa_", "").Replace("ma_", ""), symbolType));
            }

            dic.Clear();
        }

        protected void SetStyleFlags(bool supportTransparent = true, bool selectable = true, bool resizeRedraw = false)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            if (supportTransparent) SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (selectable) SetStyle(ControlStyles.Selectable, true);
            if (resizeRedraw) SetStyle(ControlStyles.ResizeRedraw, true);
            base.DoubleBuffered = true;
            UpdateStyles();
        }

        private int symbolSize = 32;
        public int SymbolSize
        {
            get => symbolSize;
            set
            {
                symbolSize = Math.Max(24, value);
                CalcSize();
                Invalidate();
            }
        }

        private int column = 24;
        public int ColumnCount
        {
            get => column;
            set
            {
                column = Math.Max(1, value);
                CalcSize();
                Invalidate();
            }
        }

        private int row = 4;
        public int RowCount
        {
            get => row;
            set
            {
                row = Math.Max(1, value);
                CalcSize();
                Invalidate();
            }
        }

        private void CalcSize()
        {
            int width = symbolSize * column;
            int height = symbolSize * row;

            if (Width != width) Width = width;
            if (Height != height) Height = height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CalcSize();

            for (int i = 0; i < GridCount; i++)
            {
                if (i >= Symbols.Count) break;
                int ir = i / ColumnCount;
                int ic = i % ColumnCount;

                Rectangle rect = new Rectangle(ic * symbolSize, ir * symbolSize, symbolSize, symbolSize);
                SymbolValue symbol = Symbols[i];
                Color color = ForeColor;
                if (Filter.IsValid() && symbol.Name.ToUpper().Contains(Filter.ToUpper())) color = SelectedColor;
                if (i == SelectedIndex) color = SelectedColor;
                e.Graphics.DrawFontImage(symbol.Value, 28, color, rect);
            }

            //if (SelectedIndex >= 0 && SelectedIndex < Symbols.Count)
            //{
            //    string str = Symbols[SelectedIndex].ToString();
            //    Size size = TextRenderer.MeasureText(str, Font);
            //    int ir = SelectedIndex / ColumnCount;
            //    int ic = SelectedIndex % ColumnCount;
            //
            //    Rectangle rect = new Rectangle(ic * symbolSize, ir * symbolSize, symbolSize, symbolSize);
            //    if (rect.Right + size.Width > Width)
            //    {
            //        rect = new Rectangle(ic * symbolSize - size.Width, ir * symbolSize - 2, size.Width, symbolSize + 2);
            //        e.Graphics.FillRectangle(Color.White.Alpha(220), rect);
            //        e.Graphics.DrawString(str, Font, Color.Red, rect, ContentAlignment.MiddleCenter);
            //    }
            //    else
            //    {
            //        rect = new Rectangle(ic * symbolSize + symbolSize, ir * symbolSize - 2, size.Width, symbolSize + 2);
            //        e.Graphics.FillRectangle(Color.White.Alpha(220), rect);
            //        e.Graphics.DrawString(str, Font, Color.Red, rect, ContentAlignment.MiddleCenter);
            //    }
            //}
        }

        public int SelectedValue { get => SelectedIndex >= 0 && SelectedIndex < Symbols.Count ? Symbols[SelectedIndex].Value : 0; }

        public int SelectedIndex { get; private set; } = -1;

        public int GridCount => ColumnCount * RowCount;

        private readonly List<SymbolValue> Symbols = new List<SymbolValue>();

        public int SymbolCount => Symbols.Count;

        public SymbolValue Get(int index)
        {
            return Symbols[index];
        }

        public void Clear()
        {
            Symbols.Clear();
        }

        public void Add(SymbolValue symbol)
        {
            if (Symbols.Count >= GridCount)
                RowCount++;
            Symbols.Add(symbol);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SelectedIndex = -1;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int ir = e.Location.Y / SymbolSize;
            int ic = e.Location.X / SymbolSize % ColumnCount;
            int index = ir * ColumnCount + ic;
            if (index < 0) return;
            if (index >= Symbols.Count) return;
            if (index != SelectedIndex)
            {
                SelectedIndex = index;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (SelectedIndex >= 0 && SelectedIndex < Symbols.Count)
                ValueChanged?.Invoke(this, Symbols[SelectedIndex]);
        }

        public event OnSymbolValueChanged ValueChanged;

        private string filter = "";
        public string Filter { get => filter; set { filter = value; Invalidate(); } }
    }

    public delegate void OnSymbolValueChanged(object sender, SymbolValue value);
}