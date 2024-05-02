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
 * 文件名称: UIDateTimeItem.cs
 * 文件说明: 日期选择框弹出窗体
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-29: V2.2.5 重写
 * 2020-07-08: V2.2.6 重写下拉窗体，缩短创建时间
 * 2023-05-13: V3.3.6 重构DrawString函数
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UIDateTimeItem : UIDropDownItem, ITranslate
    {
        #region InitializeComponent

        private UITabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UIPanel p1;
        private UISymbolButton b4;
        private UISymbolButton b3;
        private UISymbolButton b2;
        private UISymbolButton b1;
        private UIPanel p2;
        private System.Windows.Forms.TabPage tabPage3;
        private UIPanel p3;
        private UILabel sb;
        private UILabel mb;
        private UILabel hb;
        private UILabel st;
        private UILabel mt;
        private UILabel ht;
        private UILabel sc;
        private UILabel mc;
        private UILabel hc;
        private UISymbolButton btnCancel;
        private UISymbolButton btnOK;
        private UISymbolButton s2;
        private UISymbolButton mm2;
        private UISymbolButton h2;
        private UISymbolButton s1;
        private UISymbolButton mm1;
        private UISymbolButton h1;
        private UILine uiLine2;
        private UILine uiLine3;
        private UIPanel uiPanel1;
        private UIPanel TopPanel;

        private void InitializeComponent()
        {
            this.TopPanel = new Sunny.UI.UIPanel();
            this.b4 = new Sunny.UI.UISymbolButton();
            this.b3 = new Sunny.UI.UISymbolButton();
            this.b2 = new Sunny.UI.UISymbolButton();
            this.b1 = new Sunny.UI.UISymbolButton();
            this.TabControl = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.p1 = new Sunny.UI.UIPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.p2 = new Sunny.UI.UIPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.p3 = new Sunny.UI.UIPanel();
            this.sb = new Sunny.UI.UILabel();
            this.mb = new Sunny.UI.UILabel();
            this.hb = new Sunny.UI.UILabel();
            this.st = new Sunny.UI.UILabel();
            this.mt = new Sunny.UI.UILabel();
            this.ht = new Sunny.UI.UILabel();
            this.sc = new Sunny.UI.UILabel();
            this.mc = new Sunny.UI.UILabel();
            this.hc = new Sunny.UI.UILabel();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.s2 = new Sunny.UI.UISymbolButton();
            this.mm2 = new Sunny.UI.UISymbolButton();
            this.h2 = new Sunny.UI.UISymbolButton();
            this.s1 = new Sunny.UI.UISymbolButton();
            this.mm1 = new Sunny.UI.UISymbolButton();
            this.h1 = new Sunny.UI.UISymbolButton();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiLine3 = new Sunny.UI.UILine();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.TopPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.b4);
            this.TopPanel.Controls.Add(this.b3);
            this.TopPanel.Controls.Add(this.b2);
            this.TopPanel.Controls.Add(this.b1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.FillColor = System.Drawing.Color.White;
            this.TopPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.TopPanel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.TopPanel.Size = new System.Drawing.Size(284, 31);
            this.TopPanel.Style = Sunny.UI.UIStyle.Custom;
            this.TopPanel.StyleCustomMode = true;
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Text = "2020-05-05";
            this.TopPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.TopPanel.Click += new System.EventHandler(this.TopPanel_Click);
            // 
            // b4
            // 
            this.b4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b4.BackColor = System.Drawing.Color.Transparent;
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.FillColor = System.Drawing.Color.White;
            this.b4.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.ImageInterval = 0;
            this.b4.Location = new System.Drawing.Point(250, 4);
            this.b4.MinimumSize = new System.Drawing.Size(1, 1);
            this.b4.Name = "b4";
            this.b4.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b4.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b4.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b4.Size = new System.Drawing.Size(30, 24);
            this.b4.Style = Sunny.UI.UIStyle.Custom;
            this.b4.StyleCustomMode = true;
            this.b4.Symbol = 61697;
            this.b4.TabIndex = 3;
            this.b4.Click += new System.EventHandler(this.b4_Click);
            // 
            // b3
            // 
            this.b3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b3.BackColor = System.Drawing.Color.Transparent;
            this.b3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b3.FillColor = System.Drawing.Color.White;
            this.b3.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.ImageInterval = 0;
            this.b3.Location = new System.Drawing.Point(214, 4);
            this.b3.MinimumSize = new System.Drawing.Size(1, 1);
            this.b3.Name = "b3";
            this.b3.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b3.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b3.Size = new System.Drawing.Size(30, 24);
            this.b3.Style = Sunny.UI.UIStyle.Custom;
            this.b3.StyleCustomMode = true;
            this.b3.Symbol = 61701;
            this.b3.TabIndex = 2;
            this.b3.Click += new System.EventHandler(this.b3_Click);
            // 
            // b2
            // 
            this.b2.BackColor = System.Drawing.Color.Transparent;
            this.b2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b2.FillColor = System.Drawing.Color.White;
            this.b2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.ImageInterval = 0;
            this.b2.Location = new System.Drawing.Point(40, 4);
            this.b2.MinimumSize = new System.Drawing.Size(1, 1);
            this.b2.Name = "b2";
            this.b2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b2.Size = new System.Drawing.Size(30, 24);
            this.b2.Style = Sunny.UI.UIStyle.Custom;
            this.b2.StyleCustomMode = true;
            this.b2.Symbol = 61700;
            this.b2.TabIndex = 1;
            this.b2.Click += new System.EventHandler(this.b2_Click);
            // 
            // b1
            // 
            this.b1.BackColor = System.Drawing.Color.Transparent;
            this.b1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b1.FillColor = System.Drawing.Color.White;
            this.b1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.b1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.ImageInterval = 0;
            this.b1.Location = new System.Drawing.Point(4, 4);
            this.b1.MinimumSize = new System.Drawing.Size(1, 1);
            this.b1.Name = "b1";
            this.b1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.b1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.b1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.b1.Size = new System.Drawing.Size(30, 24);
            this.b1.Style = Sunny.UI.UIStyle.Custom;
            this.b1.StyleCustomMode = true;
            this.b1.Symbol = 61696;
            this.b1.TabIndex = 0;
            this.b1.Click += new System.EventHandler(this.b1_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabControl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TabControl.ItemSize = new System.Drawing.Size(150, 40);
            this.TabControl.Location = new System.Drawing.Point(0, 31);
            this.TabControl.MainPage = "";
            this.TabControl.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(284, 204);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.Style = Sunny.UI.UIStyle.Custom;
            this.TabControl.TabIndex = 1;
            this.TabControl.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.p1);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(284, 164);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.FillColor = System.Drawing.Color.White;
            this.p1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p1.MinimumSize = new System.Drawing.Size(1, 1);
            this.p1.Name = "p1";
            this.p1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p1.Size = new System.Drawing.Size(284, 164);
            this.p1.Style = Sunny.UI.UIStyle.Custom;
            this.p1.TabIndex = 0;
            this.p1.Text = null;
            this.p1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p1.Paint += new System.Windows.Forms.PaintEventHandler(this.p1_Paint);
            this.p1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p1_MouseClick);
            this.p1.MouseLeave += new System.EventHandler(this.p1_MouseLeave);
            this.p1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p1_MouseMove);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.p2);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 60);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.FillColor = System.Drawing.Color.White;
            this.p2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p2.Location = new System.Drawing.Point(0, 0);
            this.p2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p2.MinimumSize = new System.Drawing.Size(1, 1);
            this.p2.Name = "p2";
            this.p2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p2.Size = new System.Drawing.Size(200, 60);
            this.p2.Style = Sunny.UI.UIStyle.Custom;
            this.p2.TabIndex = 1;
            this.p2.Text = null;
            this.p2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p2.Paint += new System.Windows.Forms.PaintEventHandler(this.p2_Paint);
            this.p2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p2_MouseClick);
            this.p2.MouseLeave += new System.EventHandler(this.p2_MouseLeave);
            this.p2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p2_MouseMove);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.p3);
            this.tabPage3.Location = new System.Drawing.Point(0, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(200, 60);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.FillColor = System.Drawing.Color.White;
            this.p3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.p3.Location = new System.Drawing.Point(0, 0);
            this.p3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p3.MinimumSize = new System.Drawing.Size(1, 1);
            this.p3.Name = "p3";
            this.p3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p3.Size = new System.Drawing.Size(200, 60);
            this.p3.Style = Sunny.UI.UIStyle.Custom;
            this.p3.TabIndex = 2;
            this.p3.Text = null;
            this.p3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.p3.Paint += new System.Windows.Forms.PaintEventHandler(this.p3_Paint);
            this.p3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.p3_MouseClick);
            this.p3.MouseLeave += new System.EventHandler(this.p3_MouseLeave);
            this.p3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p3_MouseMove);
            // 
            // sb
            // 
            this.sb.BackColor = System.Drawing.Color.Transparent;
            this.sb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sb.ForeColor = System.Drawing.Color.DarkGray;
            this.sb.Location = new System.Drawing.Point(398, 103);
            this.sb.Name = "sb";
            this.sb.Size = new System.Drawing.Size(46, 22);
            this.sb.StyleCustomMode = true;
            this.sb.TabIndex = 39;
            this.sb.Text = "00";
            this.sb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mb
            // 
            this.mb.BackColor = System.Drawing.Color.Transparent;
            this.mb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mb.ForeColor = System.Drawing.Color.DarkGray;
            this.mb.Location = new System.Drawing.Point(345, 103);
            this.mb.Name = "mb";
            this.mb.Size = new System.Drawing.Size(46, 22);
            this.mb.StyleCustomMode = true;
            this.mb.TabIndex = 38;
            this.mb.Text = "00";
            this.mb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hb
            // 
            this.hb.BackColor = System.Drawing.Color.Transparent;
            this.hb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.hb.ForeColor = System.Drawing.Color.DarkGray;
            this.hb.Location = new System.Drawing.Point(292, 103);
            this.hb.Name = "hb";
            this.hb.Size = new System.Drawing.Size(46, 22);
            this.hb.StyleCustomMode = true;
            this.hb.TabIndex = 37;
            this.hb.Text = "00";
            this.hb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // st
            // 
            this.st.BackColor = System.Drawing.Color.Transparent;
            this.st.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.st.ForeColor = System.Drawing.Color.DarkGray;
            this.st.Location = new System.Drawing.Point(398, 31);
            this.st.Name = "st";
            this.st.Size = new System.Drawing.Size(46, 22);
            this.st.StyleCustomMode = true;
            this.st.TabIndex = 36;
            this.st.Text = "00";
            this.st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mt
            // 
            this.mt.BackColor = System.Drawing.Color.Transparent;
            this.mt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mt.ForeColor = System.Drawing.Color.DarkGray;
            this.mt.Location = new System.Drawing.Point(345, 31);
            this.mt.Name = "mt";
            this.mt.Size = new System.Drawing.Size(46, 22);
            this.mt.StyleCustomMode = true;
            this.mt.TabIndex = 35;
            this.mt.Text = "00";
            this.mt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ht
            // 
            this.ht.BackColor = System.Drawing.Color.Transparent;
            this.ht.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ht.ForeColor = System.Drawing.Color.DarkGray;
            this.ht.Location = new System.Drawing.Point(292, 31);
            this.ht.Name = "ht";
            this.ht.Size = new System.Drawing.Size(46, 22);
            this.ht.StyleCustomMode = true;
            this.ht.TabIndex = 34;
            this.ht.Text = "00";
            this.ht.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sc
            // 
            this.sc.BackColor = System.Drawing.Color.Transparent;
            this.sc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sc.Location = new System.Drawing.Point(398, 65);
            this.sc.Name = "sc";
            this.sc.Size = new System.Drawing.Size(46, 27);
            this.sc.StyleCustomMode = true;
            this.sc.TabIndex = 33;
            this.sc.Text = "00";
            this.sc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sc.DoubleClick += new System.EventHandler(this.sc_DoubleClick);
            // 
            // mc
            // 
            this.mc.BackColor = System.Drawing.Color.Transparent;
            this.mc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mc.Location = new System.Drawing.Point(345, 65);
            this.mc.Name = "mc";
            this.mc.Size = new System.Drawing.Size(46, 27);
            this.mc.StyleCustomMode = true;
            this.mc.TabIndex = 32;
            this.mc.Text = "00";
            this.mc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mc.DoubleClick += new System.EventHandler(this.mc_DoubleClick);
            // 
            // hc
            // 
            this.hc.BackColor = System.Drawing.Color.Transparent;
            this.hc.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.hc.Location = new System.Drawing.Point(292, 65);
            this.hc.Name = "hc";
            this.hc.Size = new System.Drawing.Size(46, 27);
            this.hc.StyleCustomMode = true;
            this.hc.TabIndex = 31;
            this.hc.Text = "00";
            this.hc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.hc.DoubleClick += new System.EventHandler(this.hc_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(370, 162);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(74, 27);
            this.btnCancel.Style = Sunny.UI.UIStyle.Custom;
            this.btnCancel.Symbol = 0;
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOK.Location = new System.Drawing.Point(291, 162);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.Size = new System.Drawing.Size(74, 27);
            this.btnOK.Style = Sunny.UI.UIStyle.Custom;
            this.btnOK.Symbol = 0;
            this.btnOK.TabIndex = 29;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // s2
            // 
            this.s2.BackColor = System.Drawing.Color.Transparent;
            this.s2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.s2.FillColor = System.Drawing.Color.White;
            this.s2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.s2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.ImageInterval = 0;
            this.s2.Location = new System.Drawing.Point(405, 129);
            this.s2.MinimumSize = new System.Drawing.Size(1, 1);
            this.s2.Name = "s2";
            this.s2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.s2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.s2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.s2.Size = new System.Drawing.Size(30, 24);
            this.s2.Style = Sunny.UI.UIStyle.Custom;
            this.s2.StyleCustomMode = true;
            this.s2.Symbol = 61703;
            this.s2.TabIndex = 28;
            this.s2.Tag = "6";
            this.s2.Click += new System.EventHandler(this.s2_Click);
            // 
            // mm2
            // 
            this.mm2.BackColor = System.Drawing.Color.Transparent;
            this.mm2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mm2.FillColor = System.Drawing.Color.White;
            this.mm2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mm2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm2.ImageInterval = 0;
            this.mm2.Location = new System.Drawing.Point(352, 129);
            this.mm2.MinimumSize = new System.Drawing.Size(1, 1);
            this.mm2.Name = "mm2";
            this.mm2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.mm2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.mm2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.mm2.Size = new System.Drawing.Size(30, 24);
            this.mm2.Style = Sunny.UI.UIStyle.Custom;
            this.mm2.StyleCustomMode = true;
            this.mm2.Symbol = 61703;
            this.mm2.TabIndex = 27;
            this.mm2.Tag = "5";
            this.mm2.Click += new System.EventHandler(this.mm2_Click);
            // 
            // h2
            // 
            this.h2.BackColor = System.Drawing.Color.Transparent;
            this.h2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.h2.FillColor = System.Drawing.Color.White;
            this.h2.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.h2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.ImageInterval = 0;
            this.h2.Location = new System.Drawing.Point(299, 128);
            this.h2.MinimumSize = new System.Drawing.Size(1, 1);
            this.h2.Name = "h2";
            this.h2.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.h2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.h2.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.h2.Size = new System.Drawing.Size(30, 24);
            this.h2.Style = Sunny.UI.UIStyle.Custom;
            this.h2.StyleCustomMode = true;
            this.h2.Symbol = 61703;
            this.h2.TabIndex = 26;
            this.h2.Tag = "4";
            this.h2.Click += new System.EventHandler(this.h2_Click);
            // 
            // s1
            // 
            this.s1.BackColor = System.Drawing.Color.Transparent;
            this.s1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.s1.FillColor = System.Drawing.Color.White;
            this.s1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.s1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.ImageInterval = 0;
            this.s1.Location = new System.Drawing.Point(405, 4);
            this.s1.MinimumSize = new System.Drawing.Size(1, 1);
            this.s1.Name = "s1";
            this.s1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.s1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.s1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.s1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.s1.Size = new System.Drawing.Size(30, 24);
            this.s1.Style = Sunny.UI.UIStyle.Custom;
            this.s1.StyleCustomMode = true;
            this.s1.Symbol = 61702;
            this.s1.TabIndex = 25;
            this.s1.Tag = "3";
            this.s1.Click += new System.EventHandler(this.s1_Click);
            // 
            // mm1
            // 
            this.mm1.BackColor = System.Drawing.Color.Transparent;
            this.mm1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mm1.FillColor = System.Drawing.Color.White;
            this.mm1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm1.ImageInterval = 0;
            this.mm1.Location = new System.Drawing.Point(352, 4);
            this.mm1.MinimumSize = new System.Drawing.Size(1, 1);
            this.mm1.Name = "mm1";
            this.mm1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.mm1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.mm1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.mm1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.mm1.Size = new System.Drawing.Size(30, 24);
            this.mm1.Style = Sunny.UI.UIStyle.Custom;
            this.mm1.StyleCustomMode = true;
            this.mm1.Symbol = 61702;
            this.mm1.TabIndex = 24;
            this.mm1.Tag = "2";
            this.mm1.Click += new System.EventHandler(this.mm1_Click);
            // 
            // h1
            // 
            this.h1.BackColor = System.Drawing.Color.Transparent;
            this.h1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.h1.FillColor = System.Drawing.Color.White;
            this.h1.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.h1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.ImageInterval = 0;
            this.h1.Location = new System.Drawing.Point(299, 4);
            this.h1.MinimumSize = new System.Drawing.Size(1, 1);
            this.h1.Name = "h1";
            this.h1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.h1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.h1.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.h1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.h1.Size = new System.Drawing.Size(30, 24);
            this.h1.Style = Sunny.UI.UIStyle.Custom;
            this.h1.StyleCustomMode = true;
            this.h1.Symbol = 61702;
            this.h1.TabIndex = 23;
            this.h1.Tag = "1";
            this.h1.Click += new System.EventHandler(this.h1_Click);
            // 
            // uiLine2
            // 
            this.uiLine2.BackColor = System.Drawing.Color.Transparent;
            this.uiLine2.FillColor = System.Drawing.Color.White;
            this.uiLine2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLine2.LineColor = System.Drawing.Color.Silver;
            this.uiLine2.LineDashStyle = Sunny.UI.UILineDashStyle.None;
            this.uiLine2.Location = new System.Drawing.Point(289, 88);
            this.uiLine2.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiLine2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiLine2.Size = new System.Drawing.Size(157, 16);
            this.uiLine2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine2.StyleCustomMode = true;
            this.uiLine2.TabIndex = 22;
            // 
            // uiLine3
            // 
            this.uiLine3.BackColor = System.Drawing.Color.Transparent;
            this.uiLine3.FillColor = System.Drawing.Color.White;
            this.uiLine3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiLine3.LineColor = System.Drawing.Color.Silver;
            this.uiLine3.LineDashStyle = Sunny.UI.UILineDashStyle.None;
            this.uiLine3.Location = new System.Drawing.Point(289, 54);
            this.uiLine3.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine3.Name = "uiLine3";
            this.uiLine3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiLine3.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiLine3.Size = new System.Drawing.Size(157, 16);
            this.uiLine3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine3.StyleCustomMode = true;
            this.uiLine3.TabIndex = 21;
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.Color.White;
            this.uiPanel1.Controls.Add(this.TabControl);
            this.uiPanel1.Controls.Add(this.TopPanel);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(284, 235);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 3;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIDateTimeItem
            // 
            this.Controls.Add(this.sb);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.mb);
            this.Controls.Add(this.hb);
            this.Controls.Add(this.h1);
            this.Controls.Add(this.st);
            this.Controls.Add(this.uiLine3);
            this.Controls.Add(this.mt);
            this.Controls.Add(this.uiLine2);
            this.Controls.Add(this.ht);
            this.Controls.Add(this.mm1);
            this.Controls.Add(this.sc);
            this.Controls.Add(this.s1);
            this.Controls.Add(this.mc);
            this.Controls.Add(this.h2);
            this.Controls.Add(this.hc);
            this.Controls.Add(this.mm2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.s2);
            this.Controls.Add(this.btnOK);
            this.FillColor = System.Drawing.Color.White;
            this.Name = "UIDateTimeItem";
            this.Size = new System.Drawing.Size(452, 235);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.StyleCustomMode = true;
            this.TopPanel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion InitializeComponent

        private readonly List<string> months = new List<string>();
        private readonly List<int> years = new List<int>();
        private readonly List<DateTime> days = new List<DateTime>();
        public Color PrimaryColor { get; set; } = UIColor.Blue;
        public bool ShowToday { get; set; }

        public UIDateTimeItem()
        {
            InitializeComponent();
            this.MouseWheel += UITimeItem_MouseWheel;
            Width = 452;
            Height = 200;
            TabControl.TabVisible = false;
            Translate();
        }

        public override void SetDPIScale()
        {
            base.SetDPIScale();
            if (DesignMode) return;
            if (!UIDPIScale.NeedSetDPIFont()) return;

            TopPanel.SetDPIScale();
            btnOK.SetDPIScale();
            btnCancel.SetDPIScale();
            foreach (var label in this.GetControls<UILabel>()) label.SetDPIScale();
        }

        public void Translate()
        {
            months.Clear();
            months.Add(UILocalize.January);
            months.Add(UILocalize.February);
            months.Add(UILocalize.March);
            months.Add(UILocalize.April);
            months.Add(UILocalize.May);
            months.Add(UILocalize.June);
            months.Add(UILocalize.July);
            months.Add(UILocalize.August);
            months.Add(UILocalize.September);
            months.Add(UILocalize.October);
            months.Add(UILocalize.November);
            months.Add(UILocalize.December);

            btnOK.Text = UILocalize.OK;
            btnCancel.Text = UILocalize.Cancel;
        }

        private int activeDay = -1;
        private int activeYear = -1;
        private int activeMonth = -1;

        private void TopPanel_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex > 0)
            {
                TabControl.SelectedIndex--;
                activeDay = -1;
            }
        }

        private void SetYears(int iy)
        {
            TopPanel.Text = iy + " - " + (iy + 9);

            years.Clear();
            years.Add(iy - 1);
            for (int i = 1; i <= 10; i++)
            {
                years.Add(iy + i - 1);
            }

            years.Add(iy + 10);
            activeYear = -1;
            p1.Invalidate();
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                TabControl.SelectPage(2);
                Year = date.Year;
                Month = date.Month;
                SetYearMonth(Year, Month);
                TabControl.SelectedTab = tabPage3;

                Hour = date.Hour;
                Minute = date.Minute;
                Second = date.Second;
                ShowOther();
            }
        }

        private int year;

        public int Year
        {
            get => year;
            set
            {
                year = value;
                int iy = year / 10 * 10;
                SetYears(iy);
            }
        }

        public int Month { get; set; }

        private void SetYearMonth(int iYear, int iMonth)
        {
            days.Clear();
            DateTime dt = new DateTime(iYear, iMonth, 1);
            int week = (int)dt.DayOfWeek;

            bool maxToEnd = false;
            DateTime dtBegin = week == 0 ? dt.AddDays(-7) : dt.AddDays(-week);
            for (int i = 1; i <= 42; i++)
            {
                try
                {
                    if (!maxToEnd && dtBegin.AddDays(i - 1).Date.Equals(DateTime.MaxValue.Date))
                    {
                        maxToEnd = true;
                    }

                    if (!maxToEnd)
                    {
                        DateTime lblDate = dtBegin.AddDays(i - 1);
                        days.Add(lblDate);
                    }
                    else
                    {
                        days.Add(DateTime.MaxValue.Date);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            p3.Invalidate();
            TopPanel.Text = Year + " - " + Month.ToString("D2"); ;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            b2.Visible = b3.Visible = TabControl.SelectedIndex == 2;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    int iy = year / 10 * 10;
                    SetYears(iy);
                    break;

                case 1:
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    TopPanel.Text = Year + " - " + Month.ToString("D2"); ;
                    break;
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Year = year / 10 * 10;
                    Year -= 10;
                    SetYears(Year);
                    break;

                case 1:
                    Year -= 1;
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    Year -= 1;
                    SetYearMonth(Year, Month);
                    break;
            }
        }

        private void b2_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(Year, Month, 1);
            dt = dt.AddMonths(-1);
            Year = dt.Year;
            Month = dt.Month;
            SetYearMonth(Year, Month);
        }

        private void b3_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(Year, Month, 1);
            if (dt.Year == DateTime.MaxValue.Year && dt.Month == DateTime.MaxValue.Month) return;
            dt = dt.AddMonths(1);
            Year = dt.Year;
            Month = dt.Month;
            SetYearMonth(Year, Month);
        }

        private void b4_Click(object sender, EventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Year = year / 10 * 10;
                    if (year == 9990) return;
                    Year += 10;
                    SetYears(Year);
                    break;

                case 1:
                    if (Year == DateTime.MaxValue.Year) return;
                    Year += 1;
                    TopPanel.Text = Year.ToString();
                    break;

                case 2:
                    if (Year == DateTime.MaxValue.Year) return;
                    Year += 1;
                    SetYearMonth(Year, Month);
                    break;
            }
        }

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            btnOK.SetStyleColor(uiColor);
            btnCancel.SetStyleColor(uiColor);

            fillColor = Color.White;
            foreColor = uiColor.DropDownPanelForeColor;

            b1.SetStyleColor(uiColor);
            b2.SetStyleColor(uiColor);
            b3.SetStyleColor(uiColor);
            b4.SetStyleColor(uiColor);

            h1.SetStyleColor(uiColor);
            h2.SetStyleColor(uiColor);
            mm1.SetStyleColor(uiColor);
            mm2.SetStyleColor(uiColor);
            s1.SetStyleColor(uiColor);
            s2.SetStyleColor(uiColor);

            b1.FillColor = b2.FillColor = b3.FillColor = b4.FillColor = TopPanel.FillColor;
            h1.FillColor = h2.FillColor = mm1.FillColor = mm2.FillColor = s1.FillColor = s2.FillColor = TopPanel.FillColor;

            RectColor = uiColor.RectColor;
            b1.ForeColor = b2.ForeColor = b3.ForeColor = b4.ForeColor = RectColor;
            b1.SymbolColor = b2.SymbolColor = b3.SymbolColor = b4.SymbolColor = RectColor;
            TopPanel.RectColor = p1.RectColor = p2.RectColor = p3.RectColor = RectColor;
            h1.ForeColor = h2.ForeColor = RectColor;
            mm1.ForeColor = mm2.ForeColor = RectColor;
            s1.ForeColor = s2.ForeColor = RectColor;
            h1.SymbolColor = h2.SymbolColor = RectColor;
            mm1.SymbolColor = mm2.SymbolColor = RectColor;
            s1.SymbolColor = s2.SymbolColor = RectColor;
        }

        private void UITimeItem_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (new Rectangle(ht.Left, ht.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    h2.PerformClick();
                }
                else if (new Rectangle(mt.Left, mt.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    mm2.PerformClick();
                }
                else if (new Rectangle(st.Left, st.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    s2.PerformClick();
                }
            }
            else if (e.Delta > 0)
            {
                if (new Rectangle(ht.Left, ht.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    h1.PerformClick();
                }
                else if (new Rectangle(mt.Left, mt.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    mm1.PerformClick();
                }
                else if (new Rectangle(st.Left, st.Top, ht.Width, hb.Bottom - ht.Top).Contains(e.X, e.Y))
                {
                    s1.PerformClick();
                }
            }
        }

        private void h1_Click(object sender, EventArgs e)
        {
            Hour = (Hour - 1 + 24).Mod(24);
            ShowOther();
        }

        private void mm1_Click(object sender, EventArgs e)
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

        private void mm2_Click(object sender, EventArgs e)
        {
            Minute = (Minute + 1 + 60).Mod(60);
            ShowOther();
        }

        private void s2_Click(object sender, EventArgs e)
        {
            Second = (Second + 1 + 60).Mod(60);
            ShowOther();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DateTime time = new DateTime(Date.Year, Date.Month, Date.Day, Hour, Minute, Second);
            DoValueChanged(this, time);
            Close();
        }

        private void hc_DoubleClick(object sender, EventArgs e)
        {
            Hour = 0;
            Minute = 0;
            Second = 0;
            ShowOther();
        }

        private void mc_DoubleClick(object sender, EventArgs e)
        {
            Minute = 0;
            Second = 0;
            ShowOther();
        }

        private void sc_DoubleClick(object sender, EventArgs e)
        {
            Second = 0;
            ShowOther();
        }

        private void p2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                int width = p2.Width / 4;
                int height = p2.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);
                if (i + 1 == Month)
                {
                    e.Graphics.DrawString(months[i], Font, PrimaryColor, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
                }
                else
                {
                    e.Graphics.DrawString(months[i], Font, i == activeMonth ? PrimaryColor : ForeColor, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
                }
            }
        }

        private void p2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p2.Width / 4;
            int height = p2.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int im = x + y * 4;
            if (activeMonth != im)
            {
                activeMonth = im;
                p2.Invalidate();
            }
        }

        private void p2_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p2.Width / 4;
            int height = p2.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            Month = x + y * 4 + 1;
            if (Month <= 0 || Month > 12) return;
            SetYearMonth(Year, Month);
            activeMonth = -1;
            TabControl.SelectedTab = tabPage3;
        }

        private void p1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                int width = p1.Width / 4;
                int height = p1.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);
                Color color = (i == 0 || i == 11) ? Color.DarkGray : ForeColor;
                e.Graphics.DrawString(years[i].ToString(), Font, (i == activeYear || years[i] == Year) ? PrimaryColor : color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
            }
        }

        private void p1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p1.Width / 4;
            int height = p1.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int iy = x + y * 4;
            if (activeYear != iy)
            {
                activeYear = iy;
                p1.Invalidate();
            }
        }

        private void p1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int width = p1.Width / 4;
            int height = p1.Height / 3;
            int x = e.Location.X / width;
            int y = e.Location.Y / height;
            int iy = x + y * 4;
            if (iy < 0 || iy >= 12) return;
            Year = years[iy] > 9999 ? 9999 : years[iy];
            activeYear = -1;
            TabControl.SelectedTab = tabPage2;
            p2.Invalidate();
        }

        private void p3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int width = p3.Width / 7;
            int height = (p3.Height - 30) / 6;
            string[] weeks = { UILocalize.Sunday, UILocalize.Monday, UILocalize.Tuesday, UILocalize.Wednesday, UILocalize.Thursday, UILocalize.Friday, UILocalize.Saturday };
            for (int i = 0; i < weeks.Length; i++)
            {
                e.Graphics.DrawString(weeks[i], Font, ForeColor, new Rectangle(width * i, 4, width, 19), ContentAlignment.MiddleCenter);
            }

            e.Graphics.DrawLine(Color.DarkGray, 8, 26, 268, 26);

            bool maxDrawer = false;
            for (int i = 0; i < 42; i++)
            {
                int left = width * (i % 7);
                int top = height * (i / 7);

                Color color = (days[i].Month == Month) ? ForeColor : Color.DarkGray;
                color = (days[i].DateString() == date.DateString()) ? PrimaryColor : color;

                if (!maxDrawer)
                {
                    e.Graphics.DrawString(days[i].Day.ToString(), Font, i == activeDay ? PrimaryColor : color, new Rectangle(left, top + 30, width, height), ContentAlignment.MiddleCenter);
                }

                if (!maxDrawer && days[i].Date.Equals(DateTime.MaxValue.Date))
                {
                    maxDrawer = true;
                }
            }

            if (ShowToday)
            {
                using Font SubFont = this.Font.DPIScaleFont(10.5f);
                e.Graphics.FillRectangle(p3.FillColor, p3.Width - width * 4 + 1, p3.Height - height + 1, width * 4 - 2, height - 2);
                e.Graphics.FillRoundRectangle(PrimaryColor, new Rectangle((int)(p3.Width - width * 4 + 6), p3.Height - height + 3, 8, height - 10), 3);
                e.Graphics.DrawString(UILocalize.Today + ": " + DateTime.Now.DateString(), SubFont, isToday ? PrimaryColor : Color.DarkGray, new Rectangle(p3.Width - width * 4 + 17, p3.Height - height - 1, Width, height), ContentAlignment.MiddleLeft);
            }
        }

        private bool isToday;

        private void p3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Location.Y <= 30)
            {
                activeDay = -1;
                p3.Invalidate();
                return;
            }

            int width = p3.Width / 7;
            int height = (p3.Height - 30) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30) / height;
            int iy = x + y * 7;
            bool istoday = ShowToday && e.Location.Y > p3.Top + p3.Height - height && e.Location.X > p3.Left + width * 3;

            if (activeDay != iy || istoday != isToday)
            {
                isToday = istoday;
                activeDay = iy;
                p3.Invalidate();
            }
        }

        private void p3_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Location.Y <= 30) return;
            int width = p3.Width / 7;
            int height = (p3.Height - 30) / 6;
            int x = e.Location.X / width;
            int y = (e.Location.Y - 30) / height;
            int id = x + y * 7;
            if (id < 0 || id >= 42) return;
            date = days[id].Date;
            if (ShowToday && e.Location.Y > p3.Height - height && e.Location.X > p3.Width - width * 4)
            {
                date = DateTime.Now.Date;
            }

            date = new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second);
            DoValueChanged(this, Date);
            //CloseParent();
        }

        private void p1_MouseLeave(object sender, EventArgs e)
        {
            activeYear = -1;
            p1.Invalidate();
        }

        private void p2_MouseLeave(object sender, EventArgs e)
        {
            activeMonth = -1;
            p2.Invalidate();
        }

        private void p3_MouseLeave(object sender, EventArgs e)
        {
            activeDay = -1;
            p3.Invalidate();
        }
    }
}