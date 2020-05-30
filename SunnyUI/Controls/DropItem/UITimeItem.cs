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
 * 文件名称: UIDateTimeItem.cs
 * 文件说明: 时间选择框弹出窗体
 * 当前版本: V2.2
 * 创建日期: 2020-05-29
 *
 * 2020-05-29: V2.2.5 创建
******************************************************************************/

using System;
using System.Diagnostics.SymbolStore;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UITimeItem : UIDropDownItem
    {
        private UILine uiLine1;
        private UISymbolButton s1;
        private UISymbolButton m1;
        private UISymbolButton h1;
        private UISymbolButton s2;
        private UISymbolButton m2;
        private UISymbolButton h2;
        private UISymbolButton btnOK;
        private UISymbolButton btnCancel;
        private UILabel hc;
        private UILabel mc;
        private UILabel sc;
        private UILabel st;
        private UILabel mt;
        private UILabel ht;
        private UILabel sb;
        private UILabel mb;
        private UILabel hb;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer timer2;
        private UILine uiLine2;

        public UITimeItem()
        {
            InitializeComponent();
            this.MouseWheel += UITimeItem_MouseWheel;
        }

        private void UITimeItem_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (new Rectangle(hc.Location, hc.Size).Contains(e.X, e.Y))
                {
                    h1_Click(null, null);
                }
                else if (new Rectangle(mc.Location, mc.Size).Contains(e.X, e.Y))
                {
                    m1_Click(null, null);
                }
                else if (new Rectangle(sc.Location, sc.Size).Contains(e.X, e.Y))
                {
                    s1_Click(null, null);
                }
            }
            else if (e.Delta > 0)
            {
                if (new Rectangle(hc.Location, hc.Size).Contains(e.X, e.Y))
                {
                    h2_Click(null, null);
                }
                else if (new Rectangle(mc.Location, hc.Size).Contains(e.X, e.Y))
                {
                    m2_Click(null, null);
                }
                else if (new Rectangle(sc.Location, sc.Size).Contains(e.X, e.Y))
                {
                    s2_Click(null, null);
                }
            }
        }

        private DateTime time;

        public DateTime Time
        {
            get => time;
            set
            {
                time = value;
                Hour = time.Hour;
                Minute = time.Minute;
                Second = time.Second;
                ShowOther();
            }
        }

        private int hour;
        private int minute;
        private int second;

        public int Hour
        {
            get => hour;
            set
            {
                hour = value;
                hc.Text = hour.ToString();
            }
        }

        public int Minute
        {
            get => minute;
            set
            {
                minute = value;
                mc.Text = minute.ToString();
            }
        }

        public int Second
        {
            get => second;
            set
            {
                second = value;
                sc.Text = second.ToString();
            }
        }

        private void ShowOther()
        {
            ht.Text = (hour - 1 + 24).Mod(24).ToString();
            hb.Text = (hour + 1 + 24).Mod(24).ToString();
            mt.Text = (minute - 1 + 60).Mod(60).ToString();
            mb.Text = (minute + 1 + 60).Mod(60).ToString();
            st.Text = (second - 1 + 60).Mod(60).ToString();
            sb.Text = (second + 1 + 60).Mod(60).ToString();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLine2 = new Sunny.UI.UILine();
            this.s1 = new Sunny.UI.UISymbolButton();
            this.m1 = new Sunny.UI.UISymbolButton();
            this.h1 = new Sunny.UI.UISymbolButton();
            this.s2 = new Sunny.UI.UISymbolButton();
            this.m2 = new Sunny.UI.UISymbolButton();
            this.h2 = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.hc = new Sunny.UI.UILabel();
            this.mc = new Sunny.UI.UILabel();
            this.sc = new Sunny.UI.UILabel();
            this.st = new Sunny.UI.UILabel();
            this.mt = new Sunny.UI.UILabel();
            this.ht = new Sunny.UI.UILabel();
            this.sb = new Sunny.UI.UILabel();
            this.mb = new Sunny.UI.UILabel();
            this.hb = new Sunny.UI.UILabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // uiLine1
            //
            this.uiLine1.FillColor = System.Drawing.Color.White;
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.LineColor = System.Drawing.Color.Silver;
            this.uiLine1.Location = new System.Drawing.Point(11, 60);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(157, 16);
            this.uiLine1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine1.StyleCustomMode = true;
            this.uiLine1.TabIndex = 2;
            //
            // uiLine2
            //
            this.uiLine2.FillColor = System.Drawing.Color.White;
            this.uiLine2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine2.LineColor = System.Drawing.Color.Silver;
            this.uiLine2.Location = new System.Drawing.Point(11, 94);
            this.uiLine2.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(157, 16);
            this.uiLine2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine2.StyleCustomMode = true;
            this.uiLine2.TabIndex = 3;
            //
            // s1
            //
            this.s1.BackColor = System.Drawing.Color.Transparent;
            this.s1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.s1.FillColor = System.Drawing.Color.White;
            this.s1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.s1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.ImageInterval = 0;
            this.s1.Location = new System.Drawing.Point(127, 13);
            this.s1.Name = "s1";
            this.s1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.s1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.s1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.s1.Size = new System.Drawing.Size(30, 24);
            this.s1.Style = Sunny.UI.UIStyle.Custom;
            this.s1.StyleCustomMode = true;
            this.s1.Symbol = 61702;
            this.s1.TabIndex = 6;
            this.s1.Tag = "3";
            this.s1.Click += new System.EventHandler(this.s1_Click);
            this.s1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.s1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // m1
            //
            this.m1.BackColor = System.Drawing.Color.Transparent;
            this.m1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m1.FillColor = System.Drawing.Color.White;
            this.m1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m1.ImageInterval = 0;
            this.m1.Location = new System.Drawing.Point(74, 13);
            this.m1.Name = "m1";
            this.m1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.m1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.m1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.m1.Size = new System.Drawing.Size(30, 24);
            this.m1.Style = Sunny.UI.UIStyle.Custom;
            this.m1.StyleCustomMode = true;
            this.m1.Symbol = 61702;
            this.m1.TabIndex = 5;
            this.m1.Tag = "2";
            this.m1.Click += new System.EventHandler(this.m1_Click);
            this.m1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.m1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // h1
            //
            this.h1.BackColor = System.Drawing.Color.Transparent;
            this.h1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.h1.FillColor = System.Drawing.Color.White;
            this.h1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.h1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.ImageInterval = 0;
            this.h1.Location = new System.Drawing.Point(21, 13);
            this.h1.Name = "h1";
            this.h1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.h1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.h1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.h1.Size = new System.Drawing.Size(30, 24);
            this.h1.Style = Sunny.UI.UIStyle.Custom;
            this.h1.StyleCustomMode = true;
            this.h1.Symbol = 61702;
            this.h1.TabIndex = 4;
            this.h1.Tag = "1";
            this.h1.Click += new System.EventHandler(this.h1_Click);
            this.h1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.h1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // s2
            //
            this.s2.BackColor = System.Drawing.Color.Transparent;
            this.s2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.s2.FillColor = System.Drawing.Color.White;
            this.s2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.s2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.ImageInterval = 0;
            this.s2.Location = new System.Drawing.Point(127, 132);
            this.s2.Name = "s2";
            this.s2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.s2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.s2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.s2.Size = new System.Drawing.Size(30, 24);
            this.s2.Style = Sunny.UI.UIStyle.Custom;
            this.s2.StyleCustomMode = true;
            this.s2.Symbol = 61703;
            this.s2.TabIndex = 9;
            this.s2.Tag = "6";
            this.s2.Click += new System.EventHandler(this.s2_Click);
            this.s2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.s2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // m2
            //
            this.m2.BackColor = System.Drawing.Color.Transparent;
            this.m2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m2.FillColor = System.Drawing.Color.White;
            this.m2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m2.ImageInterval = 0;
            this.m2.Location = new System.Drawing.Point(74, 132);
            this.m2.Name = "m2";
            this.m2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.m2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.m2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.m2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.m2.Size = new System.Drawing.Size(30, 24);
            this.m2.Style = Sunny.UI.UIStyle.Custom;
            this.m2.StyleCustomMode = true;
            this.m2.Symbol = 61703;
            this.m2.TabIndex = 8;
            this.m2.Tag = "5";
            this.m2.Click += new System.EventHandler(this.m2_Click);
            this.m2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.m2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // h2
            //
            this.h2.BackColor = System.Drawing.Color.Transparent;
            this.h2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.h2.FillColor = System.Drawing.Color.White;
            this.h2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.h2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.ImageInterval = 0;
            this.h2.Location = new System.Drawing.Point(21, 131);
            this.h2.Name = "h2";
            this.h2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.h2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.h2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.h2.Size = new System.Drawing.Size(30, 24);
            this.h2.Style = Sunny.UI.UIStyle.Custom;
            this.h2.StyleCustomMode = true;
            this.h2.Symbol = 61703;
            this.h2.TabIndex = 7;
            this.h2.Tag = "4";
            this.h2.Click += new System.EventHandler(this.h2_Click);
            this.h2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.h1_MouseDown);
            this.h2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.h1_MouseUp);
            //
            // btnOK
            //
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnOK.Location = new System.Drawing.Point(13, 166);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.Size = new System.Drawing.Size(74, 27);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCancel.Location = new System.Drawing.Point(92, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // hc
            //
            this.hc.BackColor = System.Drawing.Color.Transparent;
            this.hc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hc.Location = new System.Drawing.Point(20, 73);
            this.hc.Name = "hc";
            this.hc.Size = new System.Drawing.Size(35, 22);
            this.hc.StyleCustomMode = true;
            this.hc.TabIndex = 12;
            this.hc.Text = "00";
            this.hc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // mc
            //
            this.mc.BackColor = System.Drawing.Color.Transparent;
            this.mc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mc.Location = new System.Drawing.Point(73, 73);
            this.mc.Name = "mc";
            this.mc.Size = new System.Drawing.Size(35, 22);
            this.mc.StyleCustomMode = true;
            this.mc.TabIndex = 13;
            this.mc.Text = "00";
            this.mc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // sc
            //
            this.sc.BackColor = System.Drawing.Color.Transparent;
            this.sc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.sc.Location = new System.Drawing.Point(126, 73);
            this.sc.Name = "sc";
            this.sc.Size = new System.Drawing.Size(35, 22);
            this.sc.StyleCustomMode = true;
            this.sc.TabIndex = 14;
            this.sc.Text = "00";
            this.sc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // st
            //
            this.st.BackColor = System.Drawing.Color.Transparent;
            this.st.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.st.ForeColor = System.Drawing.Color.DarkGray;
            this.st.Location = new System.Drawing.Point(126, 40);
            this.st.Name = "st";
            this.st.Size = new System.Drawing.Size(35, 22);
            this.st.StyleCustomMode = true;
            this.st.TabIndex = 17;
            this.st.Text = "00";
            this.st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // mt
            //
            this.mt.BackColor = System.Drawing.Color.Transparent;
            this.mt.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mt.ForeColor = System.Drawing.Color.DarkGray;
            this.mt.Location = new System.Drawing.Point(73, 40);
            this.mt.Name = "mt";
            this.mt.Size = new System.Drawing.Size(35, 22);
            this.mt.StyleCustomMode = true;
            this.mt.TabIndex = 16;
            this.mt.Text = "00";
            this.mt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ht
            //
            this.ht.BackColor = System.Drawing.Color.Transparent;
            this.ht.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ht.ForeColor = System.Drawing.Color.DarkGray;
            this.ht.Location = new System.Drawing.Point(20, 40);
            this.ht.Name = "ht";
            this.ht.Size = new System.Drawing.Size(35, 22);
            this.ht.StyleCustomMode = true;
            this.ht.TabIndex = 15;
            this.ht.Text = "00";
            this.ht.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // sb
            //
            this.sb.BackColor = System.Drawing.Color.Transparent;
            this.sb.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.sb.ForeColor = System.Drawing.Color.DarkGray;
            this.sb.Location = new System.Drawing.Point(126, 106);
            this.sb.Name = "sb";
            this.sb.Size = new System.Drawing.Size(35, 22);
            this.sb.StyleCustomMode = true;
            this.sb.TabIndex = 20;
            this.sb.Text = "00";
            this.sb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // mb
            //
            this.mb.BackColor = System.Drawing.Color.Transparent;
            this.mb.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mb.ForeColor = System.Drawing.Color.DarkGray;
            this.mb.Location = new System.Drawing.Point(73, 106);
            this.mb.Name = "mb";
            this.mb.Size = new System.Drawing.Size(35, 22);
            this.mb.StyleCustomMode = true;
            this.mb.TabIndex = 19;
            this.mb.Text = "00";
            this.mb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // hb
            //
            this.hb.BackColor = System.Drawing.Color.Transparent;
            this.hb.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.hb.ForeColor = System.Drawing.Color.DarkGray;
            this.hb.Location = new System.Drawing.Point(20, 106);
            this.hb.Name = "hb";
            this.hb.Size = new System.Drawing.Size(35, 22);
            this.hb.StyleCustomMode = true;
            this.hb.TabIndex = 18;
            this.hb.Text = "00";
            this.hb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // timer1
            //
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer_Tick);
            //
            // timer2
            //
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            //
            // UITimeItem
            //
            this.Controls.Add(this.sb);
            this.Controls.Add(this.mb);
            this.Controls.Add(this.hb);
            this.Controls.Add(this.st);
            this.Controls.Add(this.mt);
            this.Controls.Add(this.ht);
            this.Controls.Add(this.sc);
            this.Controls.Add(this.mc);
            this.Controls.Add(this.hc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.s2);
            this.Controls.Add(this.m2);
            this.Controls.Add(this.h2);
            this.Controls.Add(this.s1);
            this.Controls.Add(this.m1);
            this.Controls.Add(this.h1);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.uiLine1);
            this.FillColor = System.Drawing.Color.White;
            this.Name = "UITimeItem";
            this.Size = new System.Drawing.Size(179, 200);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.ResumeLayout(false);
        }

        private void h1_Click(object sender, EventArgs e)
        {
            Hour = (Hour - 1 + 24).Mod(24);
            ShowOther();
        }

        private void m1_Click(object sender, EventArgs e)
        {
            Minute = (Minute - 1 + 60).Mod(60);
            ShowOther();
        }

        private void s1_Click(object sender, EventArgs e)
        {
            Second = (Second - 1 + 60).Mod(60);
            ShowOther();
        }

        private void h2_Click(object sender, EventArgs e)
        {
            Hour = (Hour + 1 + 24).Mod(24);
            ShowOther();
        }

        private void m2_Click(object sender, EventArgs e)
        {
            Minute = (Minute + 1 + 60).Mod(60);
            ShowOther();
        }

        private void s2_Click(object sender, EventArgs e)
        {
            Second = (Second + 1 + 60).Mod(60);
            ShowOther();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, Second);
            DoValueChanged(this, time);
            CloseParent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseParent();
        }

        private int buttonIndex;
        private bool isEnd;

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (buttonIndex)
            {
                case 1: h1_Click(null, null); break;
                case 2: m1_Click(null, null); break;
                case 3: s1_Click(null, null); break;
                case 4: h2_Click(null, null); break;
                case 5: m2_Click(null, null); break;
                case 6: s2_Click(null, null); break;
            }

            if (isEnd) timer2.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (!isEnd) timer2.Start();
        }

        private void h1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isEnd = false;
            buttonIndex = ((UISymbolButton)sender).Tag.ToString().ToInt();
            timer1.Start();
        }

        private void h1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            timer1.Stop();
            isEnd = true;
        }

        public override void SetStyle(UIBaseStyle style)
        {
            base.SetStyle(style);
            btnOK.SetStyleColor(style);
            btnCancel.SetStyleColor(style);
        }

        public override void SetRectColor(Color color)
        {
            base.SetRectColor(color);
            RectColor = color;
            h1.ForeColor = h2.ForeColor = color;
            m1.ForeColor = m2.ForeColor = color;
            s1.ForeColor = s2.ForeColor = color;
        }
    }
}