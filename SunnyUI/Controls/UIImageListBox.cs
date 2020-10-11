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
 * 2020-05-21: V2.2.5 增加鼠标滑过高亮
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
        private readonly UIScrollBar bar = new UIScrollBar();

        public UIImageListBox()
        {
            InitializeComponent();
            ShowText = false;

            Padding = new Padding(2);

            bar.ValueChanged += Bar_ValueChanged;
            bar.Width = SystemInformation.VerticalScrollBarWidth + 2;
            bar.Parent = this;
            bar.Dock = DockStyle.None;
            bar.Style = UIStyle.Custom;
            bar.Visible = false;

            listbox.Parent = this;
            listbox.Dock = DockStyle.Fill;
            listbox.Show();
            listbox.Bar = bar;

            listbox.SelectedIndexChanged += Listbox_SelectedIndexChanged;
            listbox.SelectedValueChanged += Listbox_SelectedValueChanged;
            listbox.Click += Listbox_Click;
            listbox.DoubleClick += Listbox_DoubleClick;
            listbox.BeforeDrawItem += Listbox_BeforeDrawItem;
            listbox.MouseDown += Listbox_MouseDown;
            listbox.MouseUp += Listbox_MouseUp;
            listbox.MouseMove += Listbox_MouseMove;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            if (listbox != null)
            {
                listbox.BackColor = color;
            }

            if (bar != null)
            {
                bar.FillColor = color;
            }
        }

        private void Listbox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        private void Listbox_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        private void Listbox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public new event MouseEventHandler MouseMove;

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            listbox.Font = Font;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            bar.Top = 2;
            bar.Height = Height - 4;
            bar.Left = Width - bar.Width - 2;
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
                ItemDoubleClick?.Invoke(this, e);
        }

        private void Listbox_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
                ItemClick?.Invoke(this, e);
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
            SelectedValueChanged?.Invoke(this, e);
            Text = listbox.SelectedItem?.ToString();
        }

        private void Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        [DefaultValue(100)]
        [Description("列表项高度"), Category("SunnyUI")]
        public int ItemHeight
        {
            get => listbox.ItemHeight;
            set => listbox.ItemHeight = value;
        }

        [DefaultValue(4)]
        [Description("图片文字间间隔"), Category("SunnyUI")]
        public int ImageInterval
        {
            get => listbox.ImageInterval;
            set => listbox.ImageInterval = value;
        }

        [DefaultValue(true)]
        [Description("显示说明文字"), Category("SunnyUI")]
        public bool ShowDescription
        {
            get => listbox.ShowDescription;
            set => listbox.ShowDescription = value;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            if (bar != null)
            {
                bar.ForeColor = uiColor.PrimaryColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = Color.White;
            }

            hoverColor = uiColor.TreeViewHoverColor;
            if (listbox != null)
            {
                listbox.HoverColor = hoverColor;
                listbox.SetStyleColor(uiColor);
                listbox.BackColor = Color.White;
            }

            fillColor = Color.White;
        }

        private int LastCount;

        private int lastBarValue = -1;

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            if (listbox != null)
            {
                if (bar.Value != lastBarValue)
                {
                    ScrollBarInfo.SetScrollValue(listbox.Handle, bar.Value);
                    lastBarValue = bar.Value;
                }
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
        [Description("列表项"), Category("SunnyUI")]
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

        /// <summary>
        /// 增加图片
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="description">图片描述</param>
        public void AddImage(Bitmap image, string description = "")
        {
            AddImage(new ImageListItem(image, description));
        }

        public void SelectedFirst()
        {
            listbox.SelectedFirst();
        }

        [DefaultValue(typeof(Color), "80, 160, 255")]
        [Description("选中项背景颜色"), Category("SunnyUI")]
        public Color ItemSelectBackColor
        {
            get => listbox.ItemSelectBackColor;
            set => listbox.ItemSelectBackColor = value;
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("选中项字体颜色"), Category("SunnyUI")]
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

        private Color hoverColor = Color.FromArgb(155, 200, 255);

        [DefaultValue(typeof(Color), "155, 200, 255")]
        [Description("鼠标移上颜色"), Category("SunnyUI")]
        public Color HoverColor
        {
            get => hoverColor;
            set
            {
                hoverColor = value;
                listbox.HoverColor = hoverColor;
                _style = UIStyle.Custom;
            }
        }

        [ToolboxItem(false)]
        private sealed class ImageListBox : ListBox, IStyleInterface
        {
            private UIScrollBar bar;

            /// <summary>
            /// Tag字符串
            /// </summary>
            [DefaultValue(null)]
            [Description("获取或设置包含有关控件的数据的对象字符串"), Category("SunnyUI")]
            public string TagString { get; set; }

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
                if (Bar != null && Bar.Visible)
                {
                    if (Bar.Value != 0)
                    {
                        ScrollBarInfo.SetScrollValue(Handle, Bar.Value);
                    }
                }
                //SetScrollInfo();
            }

            public void SetScrollInfo()
            {
                if (Bar == null)
                {
                    return;
                }

                var si = ScrollBarInfo.GetInfo(Handle);
                if (si.ScrollMax > 0)
                {
                    Bar.Maximum = si.ScrollMax;
                    Bar.Visible = si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0;
                    Bar.Value = si.nPos;
                }
                else
                {
                    Bar.Visible = false;
                }
            }

            protected override void OnMeasureItem(MeasureItemEventArgs e)
            {
                e.ItemHeight += ItemHeight;
            }

            /// <summary>
            /// 自定义主题风格
            /// </summary>
            [DefaultValue(false)]
            [Description("获取或设置可以自定义主题风格"), Category("SunnyUI")]
            public bool StyleCustomMode { get; set; }

            public string Version { get; }

            private UIStyle _style = UIStyle.Blue;
            private Color _itemSelectBackColor = UIColor.Blue;
            private Color _itemSelectForeColor = Color.White;
            private int imageInterval = 4;
            private bool showDescription = true;

            [Browsable(false)]
            public int Count => Items.Count;

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

                if (Bar != null && Bar.Visible)
                {
                    var si = ScrollBarInfo.GetInfo(Handle);
                    int temp = Math.Abs(e.Delta / 120);
                    if (e.Delta > 10)
                    {
                        int nposnum = si.nPos - temp * SystemInformation.MouseWheelScrollLines;
                        ScrollBarInfo.SetScrollValue(Handle, nposnum >= si.nMin ? nposnum : 0);
                    }
                    else if (e.Delta < -10)
                    {
                        int nposnum = si.nPos + temp * SystemInformation.MouseWheelScrollLines;
                        ScrollBarInfo.SetScrollValue(Handle, nposnum <= si.ScrollMax ? nposnum : si.ScrollMax);
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

            /// <summary>
            /// 主题样式
            /// </summary>
            [DefaultValue(UIStyle.Blue), Description("主题样式"), Category("SunnyUI")]
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

            [Category("SunnyUI"), Description("The border color used to paint the control.")]
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

            [Category("SunnyUI"), Description("The border color used to paint the control.")]
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

            public void AddImage(Bitmap image, string description = "")
            {
                AddImage(new ImageListItem(image, description));
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

                bool otherState = e.State == DrawItemState.Grayed || e.State == DrawItemState.HotLight;
                if (!otherState)
                {
                    e.DrawBackground();
                }

                if (e.Index < 0 || e.Index >= Items.Count)
                {
                    return;
                }

                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                Color backColor = isSelected ? ItemSelectBackColor : BackColor;
                Color foreColor = isSelected ? ItemSelectForeColor : ForeColor;
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

                if (!otherState)
                {
                    e.Graphics.FillRectangle(BackColor, e.Bounds);
                    e.Graphics.FillRoundRectangle(backColor, rect, 5);
                }
                else
                {
                    if (e.State == DrawItemState.Grayed)
                    {
                        backColor = BackColor;
                        foreColor = ForeColor;
                    }

                    if (e.State == DrawItemState.HotLight)
                    {
                        backColor = HoverColor;
                        foreColor = ForeColor;
                    }

                    e.Graphics.FillRectangle(BackColor, e.Bounds);
                    e.Graphics.FillRoundRectangle(backColor, rect, 5);
                }

                Graphics g = e.Graphics;
                Matrix oldTransform = g.Transform;
                Matrix newTransform = oldTransform.Clone();
                newTransform.Translate(e.Bounds.X, e.Bounds.Y);
                g.Transform = newTransform;
                ImageListItem item = (ImageListItem)Items[e.Index];
                SizeF sf = g.MeasureString("ImageListBox", Font);
                int thumbnailSize = ShowDescription ? ((int)(ItemHeight - ImageInterval - sf.Height)) : (ItemHeight - ImageInterval * 2);

                if (item.Image != null)
                {
                    if (item.Image.Width <= thumbnailSize && item.Image.Height <= thumbnailSize)
                    {
                        g.DrawImage(item.Image, new Rectangle(ImageInterval, ImageInterval, item.Image.Width, item.Image.Height));
                    }
                    else
                    {
                        float scale = thumbnailSize * 1.0f / item.Image.Height;
                        g.DrawImage(item.Image, new Rectangle(ImageInterval, ImageInterval, (int)(item.Image.Width * scale), (int)(item.Image.Height * scale)));
                    }
                }

                if (ShowDescription && !string.IsNullOrEmpty(item.Description))
                {
                    g.DrawString(item.Description, e.Font, foreColor, new Point(ImageInterval, thumbnailSize + ImageInterval));
                }

                g.Transform = oldTransform;
            }

            private Color hoverColor = Color.FromArgb(155, 200, 255);

            [DefaultValue(typeof(Color), "155, 200, 255")]
            public Color HoverColor
            {
                get => hoverColor;
                set
                {
                    hoverColor = value;
                    _style = UIStyle.Custom;
                }
            }

            private int lastIndex = -1;
            private int mouseIndex = -1;

            [Browsable(false)]
            public int MouseIndex
            {
                get => mouseIndex;
                set
                {
                    if (mouseIndex != value)
                    {
                        if (lastIndex >= 0 && lastIndex != SelectedIndex)
                        {
                            OnDrawItem(new DrawItemEventArgs(this.CreateGraphics(), Font, GetItemRectangle(lastIndex), lastIndex, DrawItemState.Grayed));
                        }

                        mouseIndex = value;
                        if (mouseIndex >= 0 && mouseIndex != SelectedIndex)
                        {
                            OnDrawItem(new DrawItemEventArgs(this.CreateGraphics(), Font, GetItemRectangle(value), value, DrawItemState.HotLight));
                        }

                        lastIndex = mouseIndex;
                    }
                }
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                base.OnMouseMove(e);
                MouseIndex = IndexFromPoint(e.Location);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                MouseIndex = -1;
            }
        }

        public class ImageListItem : IDisposable
        {
            public string ImagePath { get; private set; }

            public string Description { get; set; }

            public Bitmap Image { get; private set; }

            public ImageListItem(string imagePath, string description = "")
            {
                if (File.Exists(imagePath))
                {
                    ImagePath = imagePath;
                    Image = new Bitmap(imagePath);
                }

                Description = description;
            }

            public ImageListItem(Bitmap image, string description = "")
            {
                ImagePath = "";
                Image = new Bitmap(image);
                Description = description;
            }

            public override string ToString()
            {
                return Description + ", " + ImagePath;
            }

            public void Dispose()
            {
                Image?.Dispose();
            }
        }
    }
}