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
 * 文件名称: UIImageListBox.cs
 * 文件说明: 图片列表框
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-04-25: V2.2.4 更新主题配置类
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ItemClick")]
    public sealed partial class UIImageListBox : UIPanel
    {
        private readonly ImageListBox listbox = new ImageListBox();
        private readonly UIPanel panel = new UIPanel();
        private readonly UIScrollBar bar = new UIScrollBar();

        public UIImageListBox()
        {
            InitializeComponent();
            ShowText = false;

            Padding = new Padding(2);

            panel.Radius = 0;
            panel.RadiusSides = UICornerRadiusSides.None;
            panel.RectSides = ToolStripStatusLabelBorderSides.None;
            panel.Parent = this;
            panel.Width = 0;
            panel.Dock = DockStyle.Right;
            panel.Show();

            bar.ValueChanged += Bar_ValueChanged;
            bar.Parent = panel;
            bar.Dock = DockStyle.Fill;
            bar.Style = UIStyle.Custom;
            bar.Show();

            listbox.Parent = this;
            listbox.Dock = DockStyle.Fill;
            listbox.Show();
            listbox.panel = panel;
            listbox.Bar = bar;

            panel.SendToBack();

            listbox.SelectedIndexChanged += Listbox_SelectedIndexChanged;
            listbox.SelectedValueChanged += Listbox_SelectedValueChanged;
            listbox.Click += Listbox_Click;
            listbox.DoubleClick += Listbox_DoubleClick;
            listbox.BeforeDrawItem += Listbox_BeforeDrawItem;
        }

        private void Listbox_BeforeDrawItem(object sender, ListBox.ObjectCollection items, DrawItemEventArgs e)
        {
            if (Items.Count != LastCount)
            {
                listbox.SetScrollInfo();
                LastCount = Items.Count;
                ItemsCountChange?.Invoke(this, null);
            }
        }

        private void Listbox_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                ItemDoubleClick?.Invoke(sender, e);
        }

        private void Listbox_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                ItemClick?.Invoke(sender, e);
        }

        [Browsable(false)]
        public int Count => Items.Count;

        public event EventHandler ItemClick;

        public event EventHandler ItemDoubleClick;

        public event EventHandler ItemsCountChange;

        public event EventHandler SelectedIndexChanged;

        public event EventHandler SelectedValueChanged;

        private void Listbox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedValueChanged?.Invoke(sender, e);
            Text = listbox.SelectedItem?.ToString();
        }

        private void Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        [DefaultValue(100)]
        public int ItemHeight
        {
            get => listbox.ItemHeight;
            set => listbox.ItemHeight = value;
        }

        [DefaultValue(4)]
        public int ImageInterval
        {
            get => listbox.ImageInterval;
            set => listbox.ImageInterval = value;
        }

        [DefaultValue(true)]
        public bool ShowDescription
        {
            get => listbox.ShowDescription;
            set => listbox.ShowDescription = value;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            if (panel != null)
            {
                panel.FillColor = Color.White;
            }

            if (bar != null)
            {
                bar.ForeColor = uiColor.PrimaryColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = Color.White;
            }

            listbox?.SetStyleColor(uiColor);
        }

        private int LastCount;

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            if (listbox != null)
            {
                ScrollBarInfo.SetScrollValue(listbox.Handle, bar.Value);
            }
        }

        protected override void OnRadiusChanged(int value)
        {
            base.OnRadiusChanged(value);
            Padding = new Padding(Math.Max(2, value / 2));
        }

        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            g.Clear(Color.White);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [Browsable(false)]
        public ListBox.ObjectCollection Items => listbox.Items;

        /// <summary>
        /// 增加图片
        /// </summary>
        /// <param name="record">图片对象</param>
        public void AddImage(ImageListItem record)
        {
            Items.Add(record);
        }

        /// <summary>
        /// 增加图片
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="description">图片描述</param>
        public void AddImage(string imagePath, string description = "")
        {
            AddImage(new ImageListItem(imagePath, description));
        }

        public void SelectedFirst()
        {
            listbox.SelectedFirst();
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ItemSelectBackColor
        {
            get => listbox.ItemSelectBackColor;
            set => listbox.ItemSelectBackColor = value;
        }

        [DefaultValue(typeof(Color), "White")]
        public Color ItemSelectForeColor
        {
            get => listbox.ItemSelectForeColor;
            set => listbox.ItemSelectForeColor = value;
        }

        [Browsable(false)]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get => listbox.SelectedIndex;
            set => listbox.SelectedIndex = value;
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public ImageListItem SelectedItem
        {
            get => listbox.SelectedItem as ImageListItem;
            set => listbox.SelectedItem = value;
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public object SelectedValue
        {
            get => listbox.SelectedValue;
            set => listbox.SelectedValue = value;
        }

        private sealed class ImageListBox : ListBox, IStyleInterface
        {
            private UIScrollBar bar;

            [DefaultValue(null)]
            public string TagString { get; set; }

            public UIPanel panel { get; set; }

            public UIScrollBar Bar
            {
                get => bar;
                set
                {
                    bar = value;
                    SetScrollInfo();
                }
            }

            public ImageListBox()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.DoubleBuffer, true);
                UpdateStyles();

                BorderStyle = BorderStyle.None;
                ForeColor = UIFontColor.Primary;
                IntegralHeight = false;
                ItemHeight = 100;
                DrawMode = DrawMode.OwnerDrawFixed;
                Version = UIGlobal.Version;
                SetScrollInfo();
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                if (!PanelVisible)
                {
                    SetScrollInfo();
                }
            }

            public void SetScrollInfo()
            {
                if (Bar == null || panel == null)
                {
                    return;
                }

                PanelVisible = true;
                var si = ScrollBarInfo.GetInfo(Handle);
                if (si.ScrollMax > 0)
                {
                    Bar.Maximum = si.ScrollMax;
                    panel.Width = (si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0) ? SystemInformation.VerticalScrollBarWidth + 2 : 0;
                    panel.SendToBack();
                    Bar.Value = si.nPos;
                }
                else
                {
                    panel.Width = 0;
                }

                PanelVisible = false;
            }

            private bool PanelVisible;

            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                e.ItemHeight = e.ItemHeight + ItemHeight;
            }

            [DefaultValue(false)]
            public bool StyleCustomMode { get; set; }

            public string Version { get; }

            private UIStyle _style = UIStyle.Blue;
            private Color _itemSelectBackColor = UIColor.Blue;
            private Color _itemSelectForeColor = Color.White;
            private int imageInterval = 4;
            private bool showDescription = true;

            [Browsable(false)]
            public int Count => Items.Count;

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                //隐藏滚动条
                ScrollBarInfo.ShowScrollBar(Handle, 3, false);//0:horizontal,1:vertical,3:both
            }

            public void SelectedFirst()
            {
                if (Count > 0 && SelectedIndex < 0)
                {
                    SelectedIndex = 0;
                }
            }

            protected override void OnMouseWheel(MouseEventArgs e)
            {
                base.OnMouseWheel(e);

                if (panel.Width > 0)
                {
                    var si = ScrollBarInfo.GetInfo(Handle);
                    if (e.Delta > 10)
                    {
                        if (si.nPos > 0)
                        {
                            ScrollBarInfo.ScrollUp(Handle);
                        }
                    }
                    else if (e.Delta < -10)
                    {
                        if (si.nPos < si.ScrollMax)
                        {
                            ScrollBarInfo.ScrollDown(Handle);
                        }
                    }
                }

                SetScrollInfo();
            }

            [DefaultValue(4)]
            public int ImageInterval
            {
                get => imageInterval;
                set
                {
                    if (imageInterval != value)
                    {
                        imageInterval = value;
                        Invalidate();
                    }
                }
            }

            [DefaultValue(true)]
            public bool ShowDescription
            {
                get => showDescription;
                set
                {
                    if (showDescription != value)
                    {
                        showDescription = value;
                        Invalidate();
                    }
                }
            }

            [DefaultValue(UIStyle.Blue)]
            public UIStyle Style
            {
                get => _style;
                set => SetStyle(value);
            }

            public void SetStyle(UIStyle style)
            {
                SetStyleColor(UIStyles.GetStyleColor(style));
                _style = style;
            }

            public void SetStyleColor(UIBaseStyle uiColor)
            {
                if (uiColor.IsCustom()) return;

                ItemSelectBackColor = uiColor.ListItemSelectBackColor;
                ItemSelectForeColor = uiColor.ListItemSelectForeColor;

                Invalidate();
            }

            [Category("Appearance"), Description("The border color used to paint the control.")]
            public Color ItemSelectBackColor
            {
                get => _itemSelectBackColor;
                set
                {
                    if (_itemSelectBackColor != value)
                    {
                        _itemSelectBackColor = value;
                        _style = UIStyle.Custom;
                        if (DesignMode)
                            Invalidate();
                    }
                }
            }

            [Category("Appearance"), Description("The border color used to paint the control.")]
            public Color ItemSelectForeColor
            {
                get => _itemSelectForeColor;
                set
                {
                    if (_itemSelectForeColor != value)
                    {
                        _itemSelectForeColor = value;
                        _style = UIStyle.Custom;
                        if (DesignMode)
                            Invalidate();
                    }
                }
            }

            /// <summary>
            /// 增加图片
            /// </summary>
            /// <param name="record">图片对象</param>
            public void AddImage(ImageListItem record)
            {
                Items.Add(record);
            }

            /// <summary>
            /// 增加图片
            /// </summary>
            /// <param name="imagePath">图片路径</param>
            /// <param name="description">图片描述</param>
            public void AddImage(string imagePath, string description = "")
            {
                AddImage(new ImageListItem(imagePath, description));
            }

            public delegate void OnBeforeDrawItem(object sender, ObjectCollection items, DrawItemEventArgs e);

            public event OnBeforeDrawItem BeforeDrawItem;

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                base.OnDrawItem(e);
                BeforeDrawItem?.Invoke(this, Items, e);
                if (Items.Count == 0)
                {
                    return;
                }

                e.DrawBackground();
                if (e.Index < 0 || e.Index >= Items.Count)
                {
                    return;
                }

                Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? ItemSelectBackColor : BackColor;
                Color foreColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? ItemSelectForeColor : ForeColor;
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

                e.Graphics.FillRectangle(BackColor, e.Bounds);
                e.Graphics.FillRoundRectangle(backColor, rect, 5);

                Graphics g = e.Graphics;
                Matrix oldTransform = g.Transform;
                Matrix newTransform = oldTransform.Clone();
                newTransform.Translate(e.Bounds.X, e.Bounds.Y);
                g.Transform = newTransform;
                ImageListItem item = (ImageListItem)Items[e.Index];
                SizeF sf = g.MeasureString("ImageListBox", Font);
                int thumbnailSize = ShowDescription ? ((int)(ItemHeight - ImageInterval - sf.Height)) : (ItemHeight - ImageInterval * 2);

                if (File.Exists(item.ImagePath))
                {
                    Image image = new Bitmap(item.ImagePath);

                    if (image.Width <= thumbnailSize && image.Height <= thumbnailSize)
                    {
                        g.DrawImage(image, new Rectangle(ImageInterval, ImageInterval, image.Width, image.Height));
                    }
                    else
                    {
                        float scale = thumbnailSize * 1.0f / image.Height;
                        g.DrawImage(image, new Rectangle(ImageInterval, ImageInterval, (int)(image.Width * scale), (int)(image.Height * scale)));
                    }

                    image.Dispose();
                }

                if (ShowDescription && !string.IsNullOrEmpty(item.Description))
                {
                    g.DrawString(item.Description, e.Font, foreColor, new Point(ImageInterval, thumbnailSize + ImageInterval));
                }

                g.Transform = oldTransform;
            }
        }

        public class ImageListItem
        {
            public string ImagePath { get; set; }

            public string Description { get; set; }

            public ImageListItem(string imagePath, string description = "")
            {
                ImagePath = imagePath;
                Description = description;
            }

            public override string ToString()
            {
                return ImagePath;
            }
        }
    }
}