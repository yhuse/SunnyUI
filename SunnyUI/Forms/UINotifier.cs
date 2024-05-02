/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UINotifier.cs
 * 文件说明: 信息提示框
 * 文件作者: boschi84
 * 开源协议: CPOL
 * 引用地址: https://www.codeproject.com/Articles/1118187/Smart-UINotifier-for-Executables-Toast-for-NET
******************************************************************************/

//
//      UINotifier.cs
//
//      This project was born in the 2009 for a University application.
//      Then it was resurrected in the 2015 to be part of an old system that cannot handle the
//      3.5 framework and grows up to include more features. This is not a professional work, so
//      the code quality it's something like "let's do something quickly".
//      If you are looking for something professional, you can do it by yourself and of course share it!
//
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI
{
    public sealed partial class UINotifier : Form
    {
        public void SetDPIScale()
        {
            Font = UIDPIScale.DPIScaleFont(Font, Font.Size);

            noteTitle.Font = noteTitle.Font.DPIScaleFont(noteTitle.Font.Size);
            noteContent.Font = noteContent.Font.DPIScaleFont(noteContent.Font.Size);
            noteDate.Font = noteDate.Font.DPIScaleFont(noteDate.Font.Size);

            foreach (var control in this.GetAllDPIScaleControls())
            {
                control.SetDPIScale();
            }
        }

        #region GLOBALS

        private class NoteLocation                                              // Helper class to handle Note position
        {
            internal int X;
            internal int Y;

            internal Point initialLocation;                             // Mouse bar drag helpers
            internal bool mouseIsDown;

            public NoteLocation(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private static readonly ConcurrentDictionary<short, UINotifier> Notes = new ConcurrentDictionary<short, UINotifier>(); // Keep a list of the opened Notifiers

        private NoteLocation noteLocation;                              // Note position
        private short ID;                    // Note ID
        private string Description;                   // Note default Description
        private string Title;           // Note default Title
        private UINotifierType _uiNotifierType;            // Note default Type

        private readonly bool IsDialog;                                  // Note is Dialog
        private BackDialogStyle backDialogStyle = BackDialogStyle.None; // DialogNote default background

        private Color HoverColor = Color.FromArgb(0, 0, 0, 0);               // Default Color for hover
        private Color LeaveColor = Color.FromArgb(0, 0, 0, 0);               // Default Color for leave

        private readonly int Timeout;                    // Temporary note: timeout
        private AutoResetEvent timerResetEvent;                 // Temporary note: reset event

        private readonly Form InApplication;                                      // In App UINotifier: the note is bind to the specified container

        #endregion GLOBALS

        #region CONSTRUCTOR & DISPLAY

        //-------------------------------------------------------------------------------------------------------------------------------
        // Default constructor
        //-------------------------------------------------------------------------------------------------------------------------------
        private UINotifier(string dsc, UINotifierType type, string title, bool isDialog = false, int timeout_ms = 0, Form insideMe = null)
        {
            IsDialog = isDialog;
            Description = dsc;
            _uiNotifierType = type;
            Title = title;
            Timeout = timeout_ms;
            InApplication = insideMe;

            InitializeComponent();
            if (Notes.IsEmpty)
                ID = 1;
            else
                ID = (short)(Notes.Keys.Max() + 1);                                                       // Set the Note ID

            if (insideMe != null && !inAppNoteExists())                 // Register the drag and resize events
            {
                insideMe.LocationChanged += inApp_LocationChanged;
                insideMe.SizeChanged += inApp_LocationChanged;
            }

            foreach (Control c in Controls)                             // Make all the note area draggable
            {
                if (c is UISymbolLabel || c.Name == "icon")
                {
                    c.MouseDown += OnMouseDown;
                    c.MouseUp += OnMouseUp;
                    c.MouseMove += OnMouseMove;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Handle the drag  drop and resize location of the notes
        //-------------------------------------------------------------------------------------------------------------------------------
        private void inApp_LocationChanged(object sender, EventArgs e)
        {
            foreach (var note in Notes.Values)
            {
                if (note.InApplication != null)
                {
                    NoteLocation ln = adjustLocation(note);
                    note.Left = ln.X;
                    note.Top = ln.Y;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // On load form operations
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            BackColor = Color.Blue;                               // Initial default graphics
            TransparencyKey = Color.FromArgb(128, 128, 128);            // Initial default graphics
            FormBorderStyle = FormBorderStyle.None;                     // Initial default graphics

            Tag = "__Notifier|" + ID.ToString("X4");               // Save the note identification in the Tag field

            setNotifier(Description, _uiNotifierType, Title);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Create the Note and handle its location
        //-------------------------------------------------------------------------------------------------------------------------------
        private void setNotifier(string description, UINotifierType noteType, string title, bool isUpdate = false)
        {
            Title = title;
            Description = description;
            _uiNotifierType = noteType;

            noteTitle.Text = title;                                // Fill the UINotifier data title
            noteContent.Text = description;                          // Fill the UINotifier data description
            noteDate.Text = DateTime.Now + "";                    // Fill the UINotifier data Timestamp

            #region ADJUST COLORS

            switch (noteType)
            {
                case UINotifierType.ERROR:
                    icon.Symbol = 61527;
                    icon.SymbolColor = UIStyles.Red.ButtonFillColor;
                    LeaveColor = UIStyles.Red.ButtonFillColor;
                    HoverColor = UIStyles.Red.ButtonFillHoverColor;
                    break;

                case UINotifierType.INFO:
                    icon.Symbol = 61530;
                    icon.SymbolColor = UIStyles.Blue.ButtonFillColor;
                    LeaveColor = UIStyles.Blue.ButtonFillColor;
                    HoverColor = UIStyles.Blue.ButtonFillHoverColor;
                    break;

                case UINotifierType.WARNING:
                    icon.Symbol = 61553;
                    icon.SymbolColor = UIStyles.Orange.ButtonFillColor;
                    LeaveColor = UIStyles.Orange.ButtonFillColor;
                    HoverColor = UIStyles.Orange.ButtonFillHoverColor;
                    break;

                case UINotifierType.OK:
                    icon.Symbol = 61528;
                    icon.SymbolColor = UIStyles.Green.ButtonFillColor;
                    LeaveColor = UIStyles.Green.ButtonFillColor;
                    HoverColor = UIStyles.Green.ButtonFillHoverColor;
                    break;
            }

            buttonClose.BackColor = LeaveColor;                              // Init colors
            buttonMenu.BackColor = LeaveColor;
            noteTitle.BackColor = LeaveColor;

            buttonClose.MouseHover += (s, e) =>                    // Mouse hover
            {
                buttonClose.BackColor = HoverColor;
                buttonMenu.BackColor = HoverColor;
                noteTitle.BackColor = HoverColor;
            };
            buttonMenu.MouseHover += (s, e) =>
            {
                buttonMenu.BackColor = HoverColor;
                buttonClose.BackColor = HoverColor;
                noteTitle.BackColor = HoverColor;
            }; noteTitle.MouseHover += (s, e) =>
            {
                buttonMenu.BackColor = HoverColor;
                buttonClose.BackColor = HoverColor;
                noteTitle.BackColor = HoverColor;
            };

            buttonClose.MouseLeave += (s, e) =>                    // Mouse leave
            {
                buttonClose.BackColor = LeaveColor;
                buttonMenu.BackColor = LeaveColor;
                noteTitle.BackColor = LeaveColor;
            };
            buttonMenu.MouseLeave += (s, e) =>
            {
                buttonMenu.BackColor = LeaveColor;
                buttonClose.BackColor = LeaveColor;
                noteTitle.BackColor = LeaveColor;
            };
            noteTitle.MouseLeave += (s, e) =>
            {
                buttonMenu.BackColor = LeaveColor;
                buttonClose.BackColor = LeaveColor;
                noteTitle.BackColor = LeaveColor;
            };

            #endregion ADJUST COLORS

            #region DIALOG NOTE

            if (IsDialog)
            {
                Button ok_button = new Button();                     // Dialog note comes with a simple Ok button
                ok_button.FlatStyle = FlatStyle.Flat;
                ok_button.BackColor = LeaveColor;
                ok_button.ForeColor = Color.White;
                Size = new Size(Size.Width,              // Resize the note to contain the button
                                               Size.Height + 50);
                ok_button.Size = new Size(120, 40);
                ok_button.Location = new Point(Size.Width / 2 - ok_button.Size.Width / 2,
                                                Size.Height - 50);
                ok_button.Text = UILocalize.OK;
                ok_button.Click += onOkButtonClick;
                Controls.Add(ok_button);

                noteDate.Location = new Point(noteDate.Location.X,    // Shift down the date location
                                                noteDate.Location.Y + 44);

                noteLocation = new NoteLocation(Left, Top);      // Default Center Location
            }

            #endregion DIALOG NOTE

            #region NOTE LOCATION

            if (!IsDialog && !isUpdate)
            {
                NoteLocation location = adjustLocation(this);           // Set the note location

                Left = location.X;                                      // UINotifier position X
                Top = location.Y;                                      // UINotifier position Y
            }

            #endregion NOTE LOCATION
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Find a valid position for the note into the note area:
        // 1. Inside the Screen (support multiple screens)
        // 2. Inside the father application (if specified)
        //-------------------------------------------------------------------------------------------------------------------------------
        private NoteLocation adjustLocation(UINotifier note)
        {
            Rectangle notesArea;
            int nColumn = 0, xShift = 25;                                                     // Custom note overlay
            //  x_Shift     = Width + 5;                                         // Full visible note (no overlay)
            bool add = false;

            if (InApplication != null && InApplication.WindowState == FormWindowState.Normal)        // Get the available notes area, based on the type of note location
            {
                notesArea = InApplication.Bounds;
            }
            else
            {
                notesArea = new Rectangle(Screen.GetWorkingArea(note).Left,
                                          Screen.GetWorkingArea(note).Top,
                                          Screen.GetWorkingArea(note).Width,
                                          Screen.GetWorkingArea(note).Height);
            }

            int nMaxRows = notesArea.Height / Height;                                  // Max number of rows in the available space
            int nMaxColumns = notesArea.Width / xShift;                                  // Max number of columns in the available space

            noteLocation = new NoteLocation(notesArea.Width +                        // Initial Position X
                                            notesArea.Left -
                                            Width,
                                            notesArea.Height +                        // Initial Position Y
                                            notesArea.Top -
                                            Height);

            while (nMaxRows > 0 && !add)                                              // Check the latest available position (no overlap)
            {
                for (int nRow = 1; nRow <= nMaxRows; nRow++)
                {
                    noteLocation.Y = notesArea.Height +
                                        notesArea.Top -
                                        Height * nRow;

                    if (!isLocationAlreadyUsed(noteLocation, note))
                    {
                        add = true; break;
                    }

                    if (nRow == nMaxRows)                                            // X shift if no more column space
                    {
                        nColumn++;
                        nRow = 0;

                        noteLocation.X = notesArea.Width +
                                          notesArea.Left -
                                          Width - xShift * nColumn;
                    }

                    if (nColumn >= nMaxColumns)                                      // Last exit condition: the screen is full of note
                    {
                        add = true; break;
                    }
                }
            }

            noteLocation.initialLocation = new Point(noteLocation.X,                  // Init the initial Location, for drag & drop
                                                     noteLocation.Y);
            return noteLocation;
        }

        #endregion CONSTRUCTOR & DISPLAY

        #region EVENTS

        //-------------------------------------------------------------------------------------------------------------------------------
        // Close event for the note
        //-------------------------------------------------------------------------------------------------------------------------------
        private void onCloseClick(object sender, EventArgs e)
        {
            if (e == null || ((MouseEventArgs)e).Button != MouseButtons.Right)
            {
                closeMe();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Show the menu (for the menu button) event
        //-------------------------------------------------------------------------------------------------------------------------------
        private void onMenuClick(object sender, EventArgs e)
        {
            closeAllToolStripMenuItem.Font = menu.Font.DPIScaleFont(menu.Font.Size);
            menu.Show(buttonMenu, new Point(0, buttonMenu.Height));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Close all the notes event
        //-------------------------------------------------------------------------------------------------------------------------------
        private void onMenuCloseAllClick(object sender, EventArgs e)
        {
            CloseAll();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Dialog note Only (Ok button click event)
        //-------------------------------------------------------------------------------------------------------------------------------
        private void onOkButtonClick(object sender, EventArgs e)
        {
            onCloseClick(null, null);        // It is the same as the close operation event
        }

        #endregion EVENTS

        #region HELPERS

        //-------------------------------------------------------------------------------------------------------------------------------
        // Close the note event
        //-------------------------------------------------------------------------------------------------------------------------------
        private void closeMe()
        {
            Notes.TryRemove(this.ID, out _);
            Close();

            if (Notes.Count == 0)
                ID = 0;                                                 // Reset the ID counter if no notes is displayed
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Check if a note with an inApp capabilities is set
        //-------------------------------------------------------------------------------------------------------------------------------
        private bool inAppNoteExists()
        {
            foreach (var note in Notes.Values)
            {
                if (note.InApplication != null)
                    return true;
            }

            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // check if the specified location (X, Y) is already used by another note
        //-------------------------------------------------------------------------------------------------------------------------------
        private bool isLocationAlreadyUsed(NoteLocation location, UINotifier note)
        {
            foreach (var p in Notes.Values)
                if (p.Left == location.X && p.Top == location.Y)
                {
                    if (note.InApplication != null && p.ID == note.ID)
                        return false;
                    return true;
                }
            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Close all the notes
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void CloseAll()
        {
            foreach (var note in Notes.Values)
            {
                note.closeMe();
            }

            Notes.Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Event used to draw a right side close icon
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OnPaint(object sender, PaintEventArgs e)
        {
            var image = Properties.Resources.close;

            if (image != null)
            {
                var g = e.Graphics;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image,
                    buttonClose.Width - image.Width,
                    buttonClose.Height - image.Height - 2,
                    image.Width,
                    image.Height);
            }
        }

        #endregion HELPERS

        #region NOTE CREATION AND MODIFY

        //-------------------------------------------------------------------------------------------------------------------------------
        // Show the note: it is the startup of the creation process of the note
        //-------------------------------------------------------------------------------------------------------------------------------
        public static short Show(string desc, UINotifierType type = UINotifierType.INFO, string title = "Notifier",
                                 bool isDialog = false, int timeout = 0, Form inApp = null, EventHandler clickevent = null)
        {
            if (NotifierAlreadyPresent(desc, type, title, isDialog, out var updated_note_id, out var updated_note_occurence))
            {
                Update(updated_note_id, desc, type, "[" + ++updated_note_occurence + "] " + title);
            }
            else
            {
                UINotifier not = new UINotifier(desc,                                   // Instantiate the Note
                                            type,
                                            title,
                                            isDialog,
                                            timeout,
                                            inApp);
                if (clickevent != null)
                    not.ItemClick = clickevent;
                not.SetDPIScale();
                not.Show();                                                         // Show the note

                if (not.Timeout >= 500)                                          // Start auto close timer (if any)
                {
                    not.timerResetEvent = new AutoResetEvent(false);

                    BackgroundWorker timer = new BackgroundWorker();
                    timer.DoWork += timer_DoWork;
                    timer.RunWorkerCompleted += timer_RunWorkerCompleted;
                    timer.RunWorkerAsync(not);                                      // Timer (temporary notes)
                }

                Notes.TryAdd(not.ID, not);                                                     // Add to our collection of Notifiers
                updated_note_id = not.ID;
            }

            return updated_note_id;                                                 // Return the current ID of the created/updated Note
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Check if the note is already present
        // Point out the ID and the occurence of the already present note
        //-------------------------------------------------------------------------------------------------------------------------------
        private static bool NotifierAlreadyPresent(string desc, UINotifierType type, string title, bool isDialog,
                                                   out short updated_note_id, out short updated_note_occurence)
        {
            updated_note_id = 0;
            updated_note_occurence = 0;

            foreach (var note in Notes.Values)
            {
                short occurence = 0;
                string filteredTitle = note.Title;
                int index = filteredTitle.IndexOf(']');

                if (index > 0)
                {
                    string numberOccurence = filteredTitle.Substring(0, index);              // Get occurrence from title
                    numberOccurence = numberOccurence.Trim(' ', ']', '[');
                    Int16.TryParse(numberOccurence, out occurence);

                    if (occurence > 1)                                                      // This will fix the note counter due to the
                        --occurence;                                                        // displayed note number that starts from "[2]"

                    filteredTitle = filteredTitle.Substring(index + 1).Trim();
                }

                if (note.Tag != null &&                                             // Get the node
                    note.Description == desc &&
                    note.IsDialog == isDialog &&
                    filteredTitle == title &&
                    note._uiNotifierType == type)
                {
                    string hex_id = note.Tag.ToString().Split('|')[1];             // Get UINotifier ID
                    short id = Convert.ToInt16(hex_id, 16);
                    updated_note_id = id;
                    updated_note_occurence = ++occurence;
                    return true;
                }
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Update the note with the new content. Reset the timeout if any
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void Update(short ID, string desc, UINotifierType noteType, string title)
        {
            foreach (var note in Notes.Values)
            {
                if (note.Tag != null &&                                     // Get the node
                    note.Tag.Equals("__Notifier|" + ID.ToString("X4")))
                {
                    note.timerResetEvent?.Set();
                    UINotifier myNote = note;
                    myNote.setNotifier(desc, noteType, title, true);        // Set the new note content
                }
            }
        }

        #endregion NOTE CREATION AND MODIFY

        #region TIMER

        //-------------------------------------------------------------------------------------------------------------------------------
        // Background Worker to handle the timeout of the note
        //-------------------------------------------------------------------------------------------------------------------------------
        private static void timer_DoWork(object sender, DoWorkEventArgs e)
        {
            UINotifier not = (UINotifier)e.Argument;
            bool timedOut = false;
            while (!timedOut)
            {
                if (!not.timerResetEvent.WaitOne(not.Timeout))
                    timedOut = true;                                        // Time is out
            }
            e.Result = e.Argument;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Background Worker to handle the timeout event
        //-------------------------------------------------------------------------------------------------------------------------------
        private static void timer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UINotifier not = (UINotifier)e.Result;
            not.closeMe();                                                  // Close the note
        }

        #endregion TIMER

        #region DIALOG NOTE CREATION

        //-------------------------------------------------------------------------------------------------------------------------------
        // Show a Dialog note: with faded background if specified
        //-------------------------------------------------------------------------------------------------------------------------------
        public static DialogResult ShowDialog(string content, UINotifierType type = UINotifierType.INFO, string title = "Notifier",
            BackDialogStyle backDialogStyle = BackDialogStyle.FadedScreen, Form application = null)
        {
            Form back = null;
            int backBorder = 200;
            bool orgTopMostSettings = false;

            if (backDialogStyle == BackDialogStyle.FadedApplication && application == null)
                backDialogStyle = BackDialogStyle.FadedScreen;

            if (backDialogStyle != BackDialogStyle.None)
            {
                back = new Form();                                              // Create the fade background
                back.FormBorderStyle = FormBorderStyle.None;
                back.BackColor = Color.FromArgb(0, 0, 0);
                back.Opacity = 0.6;
                back.ShowInTaskbar = false;
            }

            UINotifier note = new UINotifier(content, type, title, true);      // Instantiate the UINotifier form
            note.SetDPIScale();
            note.backDialogStyle = backDialogStyle;

            switch (note.backDialogStyle)
            {
                case BackDialogStyle.None:
                    if (application != null)                                    // Set the startup position
                    {
                        note.Owner = application;
                        note.StartPosition = FormStartPosition.CenterParent;
                    }
                    else
                    {
                        note.StartPosition = FormStartPosition.CenterScreen;
                    }

                    break;

                case BackDialogStyle.FadedScreen:
                    if (back != null && application != null)
                    {
                        back.Location = new Point(-backBorder, -backBorder);
                        back.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width + backBorder,
                                                          Screen.PrimaryScreen.WorkingArea.Height + backBorder);

                        back.Show(application);
                        back.TopMost = true;
                        note.StartPosition = FormStartPosition.CenterScreen;    // Set the startup position
                    }

                    break;

                case BackDialogStyle.FadedApplication:
                    if (back != null && application != null)
                    {
                        orgTopMostSettings = application.TopMost;
                        application.TopMost = true;
                        back.StartPosition = FormStartPosition.Manual;
                        back.Size = application.Size;
                        back.Location = application.Location;
                        back.Show(application);
                        back.TopMost = true;
                        note.StartPosition = FormStartPosition.CenterParent;    // Set the startup position
                    }

                    break;
            }

            Notes.TryAdd(note.ID, note);                                                    // Add to our collection of Notifiers
            note.ShowInTaskbar = false;
            note.ShowDialog();

            back?.Close();

            if (application != null)                                            // restore app window top most property
                application.TopMost = orgTopMostSettings;

            return DialogResult.OK;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Show a Dialog note: fast creation
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void ShowDialog(string content, string title = "Notifier", UINotifierType type = UINotifierType.INFO)
        {
            ShowDialog(content, type, title);
        }

        #endregion DIALOG NOTE CREATION

        #region DRAG NOTE

        //-------------------------------------------------------------------------------------------------------------------------------
        // Handle the dragging event: change the position of the note
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (noteLocation.mouseIsDown)
            {
                int xDiff = noteLocation.initialLocation.X - e.Location.X;      // Get the difference between the two points
                int yDiff = noteLocation.initialLocation.Y - e.Location.Y;

                int x = Location.X - xDiff;                                // Set the new point
                int y = Location.Y - yDiff;

                noteLocation.X = x;                                             // Update the location
                noteLocation.Y = y;
                Location = new Point(x, y);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Handle the mouse down event
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            noteLocation.initialLocation = e.Location;
            noteLocation.mouseIsDown = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        // Handle the mouse up event
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            noteLocation.mouseIsDown = false;
        }

        #endregion DRAG NOTE

        private void UINotifier_Shown(object sender, EventArgs e)
        {
            closeAllToolStripMenuItem.Text = UILocalize.CloseAll;
        }

        private void noteContent_Click(object sender, EventArgs e)
        {
            if (ItemClick != null)
            {
                ItemClick.Invoke(this, e);
                closeMe();
            }
        }

        public event EventHandler ItemClick;
    }   // Close Class

    /// <summary>
    /// 窗体背景风格
    /// </summary>
    public enum BackDialogStyle
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 全屏
        /// </summary>
        FadedScreen,

        /// <summary>
        /// 当前窗体
        /// </summary>
        FadedApplication
    }

    /// <summary>
    /// 类型
    /// </summary>
    public enum UINotifierType
    {
        /// <summary>
        /// 信息
        /// </summary>
        INFO,

        /// <summary>
        /// 警告
        /// </summary>
        WARNING,

        /// <summary>
        /// 错误
        /// </summary>
        ERROR,

        /// <summary>
        /// 正确
        /// </summary>
        OK
    }
}