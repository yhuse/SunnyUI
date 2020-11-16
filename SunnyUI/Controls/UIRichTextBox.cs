using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    public sealed class UIRichTextBox : UIPanel
    {
        private UIScrollBar bar;
        private RichTextBox edit;

        public UIRichTextBox()
        {
            InitializeComponent();

            ShowText = false;

            edit.MouseWheel += OnMouseWheel;
            edit.TextChanged += Edit_TextChanged;
            edit.KeyDown += EditOnKeyDown;
            edit.KeyUp += EditOnKeyUp;
            edit.KeyPress += EditOnKeyPress;

            bar.Parent = this;
            bar.Style = UIStyle.Custom;
            bar.Visible = false;
            bar.ValueChanged += Bar_ValueChanged;
            bar.MouseEnter += Bar_MouseEnter;
            SizeChange();

            edit.DragDrop += Edit_DragDrop;
            edit.DragEnter += Edit_DragEnter;
            edit.DragLeave += Edit_DragLeave;
            edit.DragOver += Edit_DragOver;
            edit.GiveFeedback += Edit_GiveFeedback;
            edit.QueryContinueDrag += Edit_QueryContinueDrag;
            edit.LinkClicked += Edit_LinkClicked;
            edit.Protected += Edit_Protected;
            edit.SelectionChanged += Edit_SelectionChanged;

            edit.ScrollBars = RichTextBoxScrollBars.Vertical;
        }

        protected override void OnContextMenuStripChanged(EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
            edit.ContextMenuStrip = ContextMenuStrip;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (edit != null) edit.Font = Font;
        }

        private bool showScrollBar = true;

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly
        {
            get => edit.ReadOnly;
            set => edit.ReadOnly = value;
        }

        private void Edit_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(sender, e);
        }

        private void Edit_Protected(object sender, EventArgs e)
        {
            Protected?.Invoke(sender, e);
        }

        private void Edit_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            LinkClicked?.Invoke(sender, e);
        }

        private void Edit_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            QueryContinueDrag?.Invoke(sender, e);
        }

        private void Edit_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            GiveFeedback?.Invoke(sender, e);
        }

        private void Edit_DragOver(object sender, DragEventArgs e)
        {
            DragOver?.Invoke(sender, e);
        }

        private void Edit_DragLeave(object sender, EventArgs e)
        {
            DragLeave?.Invoke(sender, e);
        }

        private void Edit_DragEnter(object sender, DragEventArgs e)
        {
            DragEnter?.Invoke(sender, e);
        }

        private void Edit_DragDrop(object sender, DragEventArgs e)
        {
            DragDrop?.Invoke(sender, e);
        }

        public new event KeyEventHandler KeyDown;

        public new event KeyEventHandler KeyUp;

        public new event KeyPressEventHandler KeyPress;

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            edit.Focus();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            ActiveControl = edit;
        }

        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);
            if (uiColor.IsCustom()) return;

            edit.BackColor = fillColor = Color.White;
            edit.ForeColor = foreColor = UIFontColor.Primary;

            if (bar != null)
            {
                bar.ForeColor = uiColor.PrimaryColor;
                bar.HoverColor = uiColor.ButtonFillHoverColor;
                bar.PressColor = uiColor.ButtonFillPressColor;
                bar.FillColor = Color.White;
            }

            Invalidate();
        }

        protected override void AfterSetForeColor(Color color)
        {
            base.AfterSetForeColor(color);
            edit.ForeColor = color;
        }

        protected override void AfterSetFillColor(Color color)
        {
            base.AfterSetFillColor(color);
            edit.BackColor = color;
        }

        private void EditOnKeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress?.Invoke(sender, e);
        }

        private void EditOnKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        private void EditOnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        [Category("SunnyUI"), Browsable(true), DefaultValue(""), Description("文字")]
        public override string Text
        {
            get => edit.Text;
            set => edit.Text = value;
        }

        [Browsable(true)]
        public new event EventHandler TextChanged;

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, e);
            SetScrollInfo();
        }

        private void Bar_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void Bar_ValueChanged(object sender, EventArgs e)
        {
            if (edit != null)
            {
                ScrollBarInfo.SetScrollValue(edit.Handle, bar.Value);
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (bar != null && bar.Visible && edit != null)
            {
                var si = ScrollBarInfo.GetInfo(edit.Handle);
                if (e.Delta > 10)
                {
                    if (si.nPos > 0)
                    {
                        ScrollBarInfo.ScrollUp(edit.Handle);
                    }
                }
                else if (e.Delta < -10)
                {
                    if (si.nPos < si.ScrollMax)
                    {
                        ScrollBarInfo.ScrollDown(edit.Handle);
                    }
                }
            }

            SetScrollInfo();
        }

        public void SetScrollInfo()
        {
            if (bar == null)
            {
                return;
            }

            var si = ScrollBarInfo.GetInfo(edit.Handle);
            if (si.ScrollMax > 0)
            {
                bar.Maximum = si.ScrollMax;
                bar.Visible = showScrollBar && (si.ScrollMax > 0 && si.nMax > 0 && si.nPage > 0);
                bar.Value = si.nPos;
            }
            else
            {
                bar.Visible = false;
            }
        }

        private void SizeChange()
        {
            bar.Top = 2;
            bar.Width = ScrollBarInfo.VerticalScrollBarWidth();
            bar.Left = Width - bar.Width - 1;
            bar.Height = Height - 4;
            bar.BringToFront();
            SetScrollInfo();
        }

        private void InitializeComponent()
        {
            this.edit = new System.Windows.Forms.RichTextBox();
            this.bar = new Sunny.UI.UIScrollBar();
            this.SuspendLayout();
            //
            // edit
            //
            this.edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edit.Location = new System.Drawing.Point(2, 2);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(266, 176);
            this.edit.TabIndex = 0;
            this.edit.Text = "";
            //
            // bar
            //
            this.bar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.bar.Location = new System.Drawing.Point(247, 4);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(19, 173);
            this.bar.Style = Sunny.UI.UIStyle.Custom;
            this.bar.TabIndex = 2;
            this.bar.Text = "uiScrollBar1";
            //
            // UIRichTextBox
            //
            this.Controls.Add(this.bar);
            this.Controls.Add(this.edit);
            this.FillColor = System.Drawing.Color.White;
            this.Name = "UIRichTextBox";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.FontChanged += new System.EventHandler(this.UIRichTextBox_FontChanged);
            this.SizeChanged += new System.EventHandler(this.UIRichTextBox_SizeChanged);
            this.ResumeLayout(false);
        }

        private void UIRichTextBox_SizeChanged(object sender, EventArgs e)
        {
            SizeChange();
        }

        private void UIRichTextBox_FontChanged(object sender, EventArgs e)
        {
            edit.Font = Font;
        }

        #region TextBoxBase

        [DefaultValue(false)]
        public bool AcceptsTab
        {
            get => edit.AcceptsTab;
            set => edit.AcceptsTab = value;
        }

        [DefaultValue(true)]
        public bool ShortcutsEnabled
        {
            get => edit.ShortcutsEnabled;
            set => edit.ShortcutsEnabled = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get => edit.CanUndo;
        }

        [DefaultValue(true)]
        public bool HideSelection
        {
            get => edit.HideSelection;
            set => edit.HideSelection = value;
        }

        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            MergableProperty(false),
            Localizable(true),
            Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))
        ]
        public string[] Lines
        {
            get => edit.Lines;
            set => edit.Lines = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get => edit.Modified;
            set => edit.Modified = value;
        }

        [
            Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public int PreferredHeight
        {
            get => edit.PreferredHeight;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get => edit.SelectedText;
            set => edit.SelectedText = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => edit.SelectionLength;
            set => edit.SelectionLength = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => edit.SelectionStart;
            set => edit.SelectionStart = value;
        }

        [Browsable(false)]
        public int TextLength
        {
            get => edit.TextLength;
        }

        public void AppendText(string text)
        {
            edit.AppendText(text);
        }

        public void ClearUndo()
        {
            edit.ClearUndo();
        }

        public void Copy()
        {
            edit.Copy();
        }

        public void Cut()
        {
            edit.Cut();
        }

        public void Paste()
        {
            edit.Paste();
        }

        public char GetCharFromPosition(Point pt)
        {
            return edit.GetCharFromPosition(pt);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            return edit.GetCharIndexFromPosition(pt);
        }

        public int GetLineFromCharIndex(int index)
        {
            return edit.GetLineFromCharIndex(index);
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return edit.GetPositionFromCharIndex(index);
        }

        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return edit.GetFirstCharIndexFromLine(lineNumber);
        }

        public int GetFirstCharIndexOfCurrentLine()
        {
            return edit.GetFirstCharIndexOfCurrentLine();
        }

        public void DeselectAll()
        {
            edit.DeselectAll();
        }

        public void Undo()
        {
            edit.Undo();
        }

        #endregion TextBoxBase

        #region RichTextBox

        [Browsable(false)]
        public override bool AllowDrop
        {
            get => edit.AllowDrop;
            set => edit.AllowDrop = value;
        }

        [DefaultValue(false)]
        public bool AutoWordSelection
        {
            get => edit.AllowDrop;
            set => edit.AllowDrop = value;
        }

        [DefaultValue(0), Localizable(true)]
        public int BulletIndent
        {
            get => edit.BulletIndent;
            set => edit.BulletIndent = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanRedo
        {
            get => edit.CanRedo;
        }

        [DefaultValue(true)]
        public bool DetectUrls
        {
            get => edit.DetectUrls;
            set => edit.DetectUrls = value;
        }

        [DefaultValue(false)]
        public bool EnableAutoDragDrop
        {
            get => edit.EnableAutoDragDrop;
            set => edit.EnableAutoDragDrop = value;
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public RichTextBoxLanguageOptions LanguageOption
        {
            get => edit.LanguageOption;
            set => edit.LanguageOption = value;
        }

        [DefaultValue(int.MaxValue)]
        public int MaxLength
        {
            get => edit.MaxLength;
            set => edit.MaxLength = value;
        }

        [DefaultValue(true)]
        public bool Multiline
        {
            get => edit.Multiline;
            set => edit.Multiline = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RedoActionName
        {
            get => edit.RedoActionName;
        }

        [DefaultValue(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool RichTextShortcutsEnabled
        {
            get => edit.RichTextShortcutsEnabled;
            set => edit.RichTextShortcutsEnabled = value;
        }

        [DefaultValue(0), Localizable(true)]
        public int RightMargin
        {
            get => edit.RightMargin;
            set => edit.RightMargin = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
        public string Rtf
        {
            get => edit.Rtf;
            set => edit.Rtf = value;
        }

        [DefaultValue(HorizontalAlignment.Left), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HorizontalAlignment SelectionAlignment
        {
            get => edit.SelectionAlignment;
            set => edit.SelectionAlignment = value;
        }

        [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SelectionBullet
        {
            get => edit.SelectionBullet;
            set => edit.SelectionBullet = value;
        }

        [DefaultValue(0), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionCharOffset
        {
            get => edit.SelectionCharOffset;
            set => edit.SelectionCharOffset = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectionColor
        {
            get => edit.SelectionColor;
            set => edit.SelectionColor = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectionBackColor
        {
            get => edit.SelectionBackColor;
            set => edit.SelectionBackColor = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font SelectionFont
        {
            get => edit.SelectionFont;
            set => edit.SelectionFont = value;
        }

        [DefaultValue(0), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionHangingIndent
        {
            get => edit.SelectionHangingIndent;
            set => edit.SelectionHangingIndent = value;
        }

        [DefaultValue(0), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionIndent
        {
            get => edit.SelectionIndent;
            set => edit.SelectionIndent = value;
        }

        [DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SelectionProtected
        {
            get => edit.SelectionProtected;
            set => edit.SelectionProtected = value;
        }

        [DefaultValue(""), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedRtf
        {
            get => edit.SelectedRtf;
            set => edit.SelectedRtf = value;
        }

        [DefaultValue(0), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionRightIndent
        {
            get => edit.SelectionRightIndent;
            set => edit.SelectionRightIndent = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int[] SelectionTabs
        {
            get => edit.SelectionTabs;
            set => edit.SelectionTabs = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBoxSelectionTypes SelectionType
        {
            get => edit.SelectionType;
        }

        [DefaultValue(false)]
        public bool ShowSelectionMargin
        {
            get => edit.ShowSelectionMargin;
            set => edit.ShowSelectionMargin = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UndoActionName
        {
            get => edit.UndoActionName;
        }

        [DefaultValue(1.0f), Localizable(true)]
        public float ZoomFactor
        {
            get => edit.ZoomFactor;
            set => edit.ZoomFactor = value;
        }

        public bool CanPaste(DataFormats.Format clipFormat)
        {
            return edit.CanPaste(clipFormat);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
        {
            edit.DrawToBitmap(bitmap, targetBounds);
        }

        public int Find(string str)
        {
            return edit.Find(str);
        }

        public int Find(string str, RichTextBoxFinds options)
        {
            return edit.Find(str, options);
        }

        public int Find(string str, int start, RichTextBoxFinds options)
        {
            return edit.Find(str, start, options);
        }

        public int Find(string str, int start, int end, RichTextBoxFinds options)
        {
            return edit.Find(str, start, end, options);
        }

        public int Find(char[] characterSet)
        {
            return edit.Find(characterSet);
        }

        public int Find(char[] characterSet, int start)
        {
            return edit.Find(characterSet, start);
        }

        public int Find(char[] characterSet, int start, int end)
        {
            return edit.Find(characterSet, start, end);
        }

        public void LoadFile(string path)
        {
            edit.LoadFile(path);
        }

        public void LoadFile(string path, RichTextBoxStreamType fileType)
        {
            edit.LoadFile(path, fileType);
        }

        public void LoadFile(Stream data, RichTextBoxStreamType fileType)
        {
            edit.LoadFile(data, fileType);
        }

        public void Paste(DataFormats.Format clipFormat)
        {
            edit.Paste(clipFormat);
        }

        public void Redo()
        {
            edit.Redo();
        }

        public void SaveFile(string path)
        {
            edit.SaveFile(path);
        }

        public void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            edit.SaveFile(path, fileType);
        }

        public void SaveFile(Stream data, RichTextBoxStreamType fileType)
        {
            edit.SaveFile(data, fileType);
        }

        [Browsable(false)]
        public new event DragEventHandler DragDrop;

        [Browsable(false)]
        public new event DragEventHandler DragEnter;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DragLeave;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event DragEventHandler DragOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event GiveFeedbackEventHandler GiveFeedback;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new event QueryContinueDragEventHandler QueryContinueDrag;

        public event LinkClickedEventHandler LinkClicked;

        public event EventHandler Protected;

        public event EventHandler SelectionChanged;

        #endregion RichTextBox

        #region TextBoxBase
        public void ScrollToCaret()
        {
            edit.ScrollToCaret();
        }

        public void Select(int start, int length)
        {
            edit.Select(start, length);
        }

        public void SelectAll()
        {
            edit.SelectAll();
        }

        #endregion
    }
}