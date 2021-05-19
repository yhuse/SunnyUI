/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIBreadcrumb.cs
 * 文件说明: 面包屑导航条
 * 当前版本: V3.0
 * 创建日期: 2021-04-10
 *
 * 2021-04-10: V3.0.2 增加文件说明
******************************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [ToolboxItem(true)]
    [DefaultEvent("ItemIndexChanged")]
    [DefaultProperty("ItemIndex")]
    public class UIBreadcrumb : UIControl
    {
        public UIBreadcrumb()
        {
            items.CountChange += Items_CountChange;
            SetStyleFlags(true, false);
            ShowText = false;
            ShowRect = false;
            Height = 29;
            ItemWidth = 120;

            fillColor = UIColor.Blue;
            rectColor = Color.FromArgb(155, 200, 255);
            foreColor = Color.White;
        }

        private void Items_CountChange(object sender, EventArgs e)
        {
            Invalidate();
        }

        public delegate void OnValueChanged(object sender, int value);

        public event OnValueChanged ItemIndexChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public UIObjectCollection Items => items;

        private readonly UIObjectCollection items = new UIObjectCollection();

        private readonly ConcurrentDictionary<int, Point[]> ClickArea = new ConcurrentDictionary<int, Point[]>();

        [Browsable(false)]
        public int Count => Items.Count;

        private int itemIndex = -1;

        [DefaultValue(-1)]
        [Description("当前节点索引"), Category("SunnyUI")]
        public int ItemIndex
        {
            get => itemIndex;
            set
            {
                if (Count == 0)
                {
                    itemIndex = 0;
                }
                else
                {
                    itemIndex = Math.Max(-1, value);
                    itemIndex = Math.Min(Count - 1, value);
                    ItemIndexChanged?.Invoke(this, itemIndex);
                    Invalidate();
                }
            }
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            float width = 0;
            if (Items.Count == 0)
            {
                SizeF sf = g.MeasureString("Item0", Font);
                width = sf.Width + Height + 6;
                if (itemWidth < width) itemWidth = (int)width;
                List<PointF> points = new List<PointF>();
                points.Add(new PointF(3, 0));
                points.Add(new PointF(Width - 3 - Height / 2.0f, 0));
                points.Add(new PointF(Width - 3, Height / 2.0f));
                points.Add(new PointF(Width - 3 - Height / 2.0f, Height));
                points.Add(new PointF(3, Height));
                points.Add(new PointF(3 + Height / 2.0f, Height / 2.0f));
                points.Add(new PointF(3, 0));

                using (Brush br = new SolidBrush(SelectedColor))
                {
                    g.FillPolygon(br, points.ToArray());
                }

                g.DrawString("Item0", Font, ForeColor, (Width - sf.Width) / 2.0f, (Height - sf.Height) / 2.0f);
            }
            else
            {
                foreach (var item in Items)
                {
                    SizeF sf = g.MeasureString(item.ToString(), Font);
                    width = Math.Max(width, sf.Width);
                }

                width = width + Height + 6;
                if (itemWidth < width) itemWidth = (int)width;

                float begin = 0;
                int index = 0;
                foreach (var item in Items)
                {
                    SizeF sf = g.MeasureString(item.ToString(), Font);
                    List<PointF> points = new List<PointF>();
                    points.Add(new PointF(begin + 3, 0));
                    points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, 0));
                    points.Add(new PointF(begin + itemWidth - 3, Height / 2.0f));
                    points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, Height));
                    points.Add(new PointF(begin + 3, Height));
                    points.Add(new PointF(begin + 3 + Height / 2.0f, Height / 2.0f));
                    points.Add(new PointF(begin + 3, 0));

                    Point[] pts = new Point[points.Count];
                    for (int i = 0; i < points.Count; i++)
                    {
                        pts[i] = new Point((int)points[i].X, (int)points[i].Y);
                    }

                    if (!ClickArea.ContainsKey(index))
                    {
                        ClickArea.TryAdd(index, pts);
                    }
                    else
                    {
                        ClickArea[index] = pts;
                    }

                    using (Brush br = new SolidBrush(index <= ItemIndex ? SelectedColor : UnSelectedColor))
                    {
                        g.FillPolygon(br, points.ToArray());
                    }

                    g.DrawString(item.ToString(), Font, ForeColor, begin + (itemWidth - sf.Width) / 2.0f, (Height - sf.Height) / 2.0f);

                    begin = begin + itemWidth - 3 - Height / 2.0f + Interval;
                    index++;
                }
            }
        }

        private int itemWidth;

        [DefaultValue(120)]
        [Description("节点宽度"), Category("SunnyUI")]
        public int ItemWidth
        {
            get => itemWidth;
            set
            {
                itemWidth = value;
                Invalidate();
            }
        }

        private int interval = 1;

        [DefaultValue(1)]
        [Description("节点间隔"), Category("SunnyUI")]
        public int Interval
        {
            get => interval;
            set
            {
                interval = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     已选节点颜色
        /// </summary>
        [Description("已选节点颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color SelectedColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        ///     未选节点颜色
        /// </summary>
        [Description("未选节点颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "155, 200, 255")]
        public Color UnSelectedColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        /// <summary>
        ///     字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            fillColor = uiColor.PrimaryColor;
            foreColor = uiColor.ButtonForeColor;
            rectColor = uiColor.GridSelectedColor;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            foreach (var pair in ClickArea)
            {
                if (e.Location.InRegion(pair.Value))
                {
                    ItemIndex = pair.Key;
                    break;
                }
            }
        }
    }
}
