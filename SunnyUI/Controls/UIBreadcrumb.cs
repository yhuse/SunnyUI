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
 * 文件名称: UIBreadcrumb.cs
 * 文件说明: 面包屑导航条
 * 当前版本: V3.1
 * 创建日期: 2021-04-10
 *
 * 2021-04-10: V3.0.2 增加文件说明
 * 2022-01-26: V3.1.0 增加两端对齐，AlignBothEnds
 * 2022-01-26: V3.1.0 增加未选中步骤文字颜色
 * 2022-03-19: V3.1.1 重构主题配色
 * 2023-05-12: V3.3.6 重构DrawString函数
 * 2023-09-17: V3.4.2 增加Readonly，禁用鼠标点击，可通过代码设置ItemIndex
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
    /// <summary>
    /// 面包屑导航条
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("ItemIndexChanged")]
    [DefaultProperty("ItemIndex")]
    public class UIBreadcrumb : UIControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
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

        private bool alignBothEnds;

        /// <summary>
        /// 显示时两端对齐
        /// </summary>
        [DefaultValue(false)]
        [Description("显示时两端对齐"), Category("SunnyUI")]
        public bool AlignBothEnds
        {
            get => alignBothEnds;
            set
            {
                if (alignBothEnds != value)
                {
                    alignBothEnds = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 步骤值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        public delegate void OnValueChanged(object sender, int value);

        /// <summary>
        /// 步骤值变化事件
        /// </summary>
        public event OnValueChanged ItemIndexChanged;

        /// <summary>
        /// 步骤条目列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, " + AssemblyRefEx.SystemDesign, typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Description("列表项"), Category("SunnyUI")]
        public UIObjectCollection Items => items;

        private readonly UIObjectCollection items = new UIObjectCollection();

        private readonly ConcurrentDictionary<int, Point[]> ClickArea = new ConcurrentDictionary<int, Point[]>();

        /// <summary>
        /// 步骤个数
        /// </summary>
        [Browsable(false)]
        public int Count => Items.Count;

        private int itemIndex = -1;

        /// <summary>
        /// 当前节点索引
        /// </summary>
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

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            float width = 0;
            if (Items.Count == 0)
            {
                Size sf = TextRenderer.MeasureText(Text, Font);
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

                using Brush br = new SolidBrush(SelectedColor);
                g.FillPolygon(br, points.ToArray());
                g.DrawString(Text, Font, ForeColor, ClientRectangle, ContentAlignment.MiddleCenter);
            }
            else
            {
                foreach (var item in Items)
                {
                    Size sf = TextRenderer.MeasureText(item.ToString(), Font);
                    width = Math.Max(width, sf.Width);
                }

                width = width + Height + 6;
                if (itemWidth < width) itemWidth = (int)width;

                int begin = 0;
                int index = 0;
                foreach (var item in Items)
                {
                    List<PointF> points = new List<PointF>();

                    if (index == 0 && AlignBothEnds)
                    {
                        points.Add(new PointF(begin + 3, 0));
                        points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, 0));
                        points.Add(new PointF(begin + itemWidth - 3, Height / 2.0f));
                        points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, Height));
                        points.Add(new PointF(begin + 3, Height));
                        points.Add(new PointF(begin + 3, 0));
                    }
                    else if (index == Items.Count - 1 && AlignBothEnds)
                    {
                        points.Add(new PointF(begin + 3, 0));
                        points.Add(new PointF(begin + itemWidth - 3, 0));
                        points.Add(new PointF(begin + itemWidth - 3, Height));
                        points.Add(new PointF(begin + 3, Height));
                        points.Add(new PointF(begin + 3 + Height / 2.0f, Height / 2.0f));
                        points.Add(new PointF(begin + 3, 0));
                    }
                    else
                    {
                        points.Add(new PointF(begin + 3, 0));
                        points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, 0));
                        points.Add(new PointF(begin + itemWidth - 3, Height / 2.0f));
                        points.Add(new PointF(begin + itemWidth - 3 - Height / 2.0f, Height));
                        points.Add(new PointF(begin + 3, Height));
                        points.Add(new PointF(begin + 3 + Height / 2.0f, Height / 2.0f));
                        points.Add(new PointF(begin + 3, 0));
                    }

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

                    using Brush br = new SolidBrush(index <= ItemIndex ? SelectedColor : UnSelectedColor);
                    g.FillPolygon(br, points.ToArray());

                    g.DrawString(item.ToString(), Font, index <= ItemIndex ? ForeColor : UnSelectedForeColor,
                        new Rectangle(begin, 0, itemWidth, Height), ContentAlignment.MiddleCenter);
                    begin = begin + itemWidth - 3 - Height / 2 + Interval;
                    index++;
                }
            }
        }

        private int itemWidth;

        /// <summary>
        /// 节点宽度
        /// </summary>
        [DefaultValue(160)]
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

        /// <summary>
        /// 节点间隔
        /// </summary>
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
        /// 已选节点颜色
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
        /// 未选节点颜色
        /// </summary>
        [Description("未选节点颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "185, 217, 255")]
        public Color UnSelectedColor
        {
            get => rectColor;
            set => SetRectColor(value);
        }

        private Color unSelectedForeColor = Color.White;

        /// <summary>
        /// 未选节点文字颜色
        /// </summary>
        [Description("未选节点文字颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color UnSelectedForeColor
        {
            get => unSelectedForeColor;
            set
            {
                if (unSelectedForeColor != value)
                {
                    unSelectedForeColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色")]
        [Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            unSelectedForeColor = uiColor.ButtonForeColor;
            rectColor = uiColor.BreadcrumbUnSelectedColor;
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (Readonly) return;
            foreach (var pair in ClickArea)
            {
                if (e.Location.InRegion(pair.Value))
                {
                    ItemIndex = pair.Key;
                    break;
                }
            }
        }

        [DefaultValue(false)]
        [Description("禁用鼠标点击，可通过代码设置ItemIndex")]
        [Category("SunnyUI")]
        public bool Readonly { get; set; }
    }
}