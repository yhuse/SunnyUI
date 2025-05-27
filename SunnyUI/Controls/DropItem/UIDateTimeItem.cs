/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 2024-07-14: V3.6.7 修改时间界面水平分割线颜色和位置
 * 2024-07-25: V3.6.8 修改单击选择日期后立即刷新，双击可选择并关闭下拉框
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        private UIPanel uiPanel1;
        private UIPanel TopPanel;

        private void InitializeComponent()
        {
            TopPanel = new UIPanel();
            b4 = new UISymbolButton();
            b3 = new UISymbolButton();
            b2 = new UISymbolButton();
            b1 = new UISymbolButton();
            TabControl = new UITabControl();
            tabPage1 = new TabPage();
            p1 = new UIPanel();
            tabPage2 = new TabPage();
            p2 = new UIPanel();
            tabPage3 = new TabPage();
            p3 = new UIPanel();
            sb = new UILabel();
            mb = new UILabel();
            hb = new UILabel();
            st = new UILabel();
            mt = new UILabel();
            ht = new UILabel();
            sc = new UILabel();
            mc = new UILabel();
            hc = new UILabel();
            btnCancel = new UISymbolButton();
            btnOK = new UISymbolButton();
            s2 = new UISymbolButton();
            mm2 = new UISymbolButton();
            h2 = new UISymbolButton();
            s1 = new UISymbolButton();
            mm1 = new UISymbolButton();
            h1 = new UISymbolButton();
            uiPanel1 = new UIPanel();
            TopPanel.SuspendLayout();
            TabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            uiPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // TopPanel
            // 
            TopPanel.Controls.Add(b4);
            TopPanel.Controls.Add(b3);
            TopPanel.Controls.Add(b2);
            TopPanel.Controls.Add(b1);
            TopPanel.Dock = DockStyle.Top;
            TopPanel.FillColor = Color.White;
            TopPanel.Font = new Font("宋体", 12F);
            TopPanel.Location = new Point(0, 0);
            TopPanel.Margin = new Padding(4, 5, 4, 5);
            TopPanel.MinimumSize = new Size(1, 1);
            TopPanel.Name = "TopPanel";
            TopPanel.RadiusSides = UICornerRadiusSides.None;
            TopPanel.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right;
            TopPanel.Size = new Size(284, 31);
            TopPanel.Style = UIStyle.Custom;
            TopPanel.StyleCustomMode = true;
            TopPanel.TabIndex = 0;
            TopPanel.Text = "2020-05-05";
            TopPanel.TextAlignment = ContentAlignment.MiddleCenter;
            TopPanel.Click += TopPanel_Click;
            // 
            // b4
            // 
            b4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b4.BackColor = Color.Transparent;
            b4.Cursor = Cursors.Hand;
            b4.FillColor = Color.White;
            b4.FillHoverColor = Color.FromArgb(80, 160, 255);
            b4.Font = new Font("宋体", 12F);
            b4.ForeColor = Color.FromArgb(80, 160, 255);
            b4.Location = new Point(250, 4);
            b4.MinimumSize = new Size(1, 1);
            b4.Name = "b4";
            b4.Padding = new Padding(24, 0, 0, 0);
            b4.RadiusSides = UICornerRadiusSides.None;
            b4.RectHoverColor = Color.FromArgb(80, 160, 255);
            b4.RectSides = ToolStripStatusLabelBorderSides.None;
            b4.Size = new Size(30, 24);
            b4.Style = UIStyle.Custom;
            b4.StyleCustomMode = true;
            b4.Symbol = 61697;
            b4.TabIndex = 3;
            b4.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b4.Click += b4_Click;
            // 
            // b3
            // 
            b3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            b3.BackColor = Color.Transparent;
            b3.Cursor = Cursors.Hand;
            b3.FillColor = Color.White;
            b3.FillHoverColor = Color.FromArgb(80, 160, 255);
            b3.Font = new Font("宋体", 12F);
            b3.ForeColor = Color.FromArgb(80, 160, 255);
            b3.Location = new Point(214, 4);
            b3.MinimumSize = new Size(1, 1);
            b3.Name = "b3";
            b3.Padding = new Padding(24, 0, 0, 0);
            b3.RadiusSides = UICornerRadiusSides.None;
            b3.RectHoverColor = Color.FromArgb(80, 160, 255);
            b3.RectSides = ToolStripStatusLabelBorderSides.None;
            b3.Size = new Size(30, 24);
            b3.Style = UIStyle.Custom;
            b3.StyleCustomMode = true;
            b3.Symbol = 61701;
            b3.TabIndex = 2;
            b3.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b3.Click += b3_Click;
            // 
            // b2
            // 
            b2.BackColor = Color.Transparent;
            b2.Cursor = Cursors.Hand;
            b2.FillColor = Color.White;
            b2.FillHoverColor = Color.FromArgb(80, 160, 255);
            b2.Font = new Font("宋体", 12F);
            b2.ForeColor = Color.FromArgb(80, 160, 255);
            b2.Location = new Point(40, 4);
            b2.MinimumSize = new Size(1, 1);
            b2.Name = "b2";
            b2.Padding = new Padding(24, 0, 0, 0);
            b2.RadiusSides = UICornerRadiusSides.None;
            b2.RectHoverColor = Color.FromArgb(80, 160, 255);
            b2.RectSides = ToolStripStatusLabelBorderSides.None;
            b2.Size = new Size(30, 24);
            b2.Style = UIStyle.Custom;
            b2.StyleCustomMode = true;
            b2.Symbol = 61700;
            b2.TabIndex = 1;
            b2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b2.Click += b2_Click;
            // 
            // b1
            // 
            b1.BackColor = Color.Transparent;
            b1.Cursor = Cursors.Hand;
            b1.FillColor = Color.White;
            b1.FillHoverColor = Color.FromArgb(80, 160, 255);
            b1.Font = new Font("宋体", 12F);
            b1.ForeColor = Color.FromArgb(80, 160, 255);
            b1.Location = new Point(4, 4);
            b1.MinimumSize = new Size(1, 1);
            b1.Name = "b1";
            b1.Padding = new Padding(24, 0, 0, 0);
            b1.RadiusSides = UICornerRadiusSides.None;
            b1.RectHoverColor = Color.FromArgb(80, 160, 255);
            b1.RectSides = ToolStripStatusLabelBorderSides.None;
            b1.Size = new Size(30, 24);
            b1.Style = UIStyle.Custom;
            b1.StyleCustomMode = true;
            b1.Symbol = 61696;
            b1.TabIndex = 0;
            b1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            b1.Click += b1_Click;
            // 
            // TabControl
            // 
            TabControl.Controls.Add(tabPage1);
            TabControl.Controls.Add(tabPage2);
            TabControl.Controls.Add(tabPage3);
            TabControl.Dock = DockStyle.Fill;
            TabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabControl.Font = new Font("宋体", 12F);
            TabControl.ItemSize = new Size(150, 40);
            TabControl.Location = new Point(0, 31);
            TabControl.MainPage = "";
            TabControl.MenuStyle = UIMenuStyle.Custom;
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(284, 204);
            TabControl.SizeMode = TabSizeMode.Fixed;
            TabControl.Style = UIStyle.Custom;
            TabControl.TabIndex = 1;
            TabControl.TabUnSelectedForeColor = Color.FromArgb(240, 240, 240);
            TabControl.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(p1);
            tabPage1.Location = new Point(0, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(284, 164);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            p1.Dock = DockStyle.Fill;
            p1.FillColor = Color.White;
            p1.Font = new Font("宋体", 12F);
            p1.Location = new Point(0, 0);
            p1.Margin = new Padding(4, 5, 4, 5);
            p1.MinimumSize = new Size(1, 1);
            p1.Name = "p1";
            p1.RadiusSides = UICornerRadiusSides.None;
            p1.Size = new Size(284, 164);
            p1.Style = UIStyle.Custom;
            p1.TabIndex = 0;
            p1.Text = null;
            p1.TextAlignment = ContentAlignment.MiddleCenter;
            p1.Paint += p1_Paint;
            p1.MouseClick += p1_MouseClick;
            p1.MouseLeave += p1_MouseLeave;
            p1.MouseMove += p1_MouseMove;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(p2);
            tabPage2.Location = new Point(0, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(200, 60);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            p2.Dock = DockStyle.Fill;
            p2.FillColor = Color.White;
            p2.Font = new Font("宋体", 12F);
            p2.Location = new Point(0, 0);
            p2.Margin = new Padding(4, 5, 4, 5);
            p2.MinimumSize = new Size(1, 1);
            p2.Name = "p2";
            p2.RadiusSides = UICornerRadiusSides.None;
            p2.Size = new Size(200, 60);
            p2.Style = UIStyle.Custom;
            p2.TabIndex = 1;
            p2.Text = null;
            p2.TextAlignment = ContentAlignment.MiddleCenter;
            p2.Paint += p2_Paint;
            p2.MouseClick += p2_MouseClick;
            p2.MouseLeave += p2_MouseLeave;
            p2.MouseMove += p2_MouseMove;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(p3);
            tabPage3.Location = new Point(0, 40);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(200, 60);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            p3.Dock = DockStyle.Fill;
            p3.FillColor = Color.White;
            p3.Font = new Font("宋体", 12F);
            p3.Location = new Point(0, 0);
            p3.Margin = new Padding(4, 5, 4, 5);
            p3.MinimumSize = new Size(1, 1);
            p3.Name = "p3";
            p3.RadiusSides = UICornerRadiusSides.None;
            p3.Size = new Size(200, 60);
            p3.Style = UIStyle.Custom;
            p3.TabIndex = 2;
            p3.Text = null;
            p3.TextAlignment = ContentAlignment.MiddleCenter;
            p3.Paint += p3_Paint;
            p3.MouseClick += p3_MouseClick;
            p3.MouseDoubleClick += P3_MouseDoubleClick;
            p3.MouseLeave += p3_MouseLeave;
            p3.MouseMove += p3_MouseMove;
            // 
            // sb
            // 
            sb.BackColor = Color.Transparent;
            sb.Font = new Font("宋体", 12F);
            sb.ForeColor = Color.DarkGray;
            sb.Location = new Point(398, 103);
            sb.Name = "sb";
            sb.Size = new Size(46, 22);
            sb.StyleCustomMode = true;
            sb.TabIndex = 39;
            sb.Text = "00";
            sb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mb
            // 
            mb.BackColor = Color.Transparent;
            mb.Font = new Font("宋体", 12F);
            mb.ForeColor = Color.DarkGray;
            mb.Location = new Point(345, 103);
            mb.Name = "mb";
            mb.Size = new Size(46, 22);
            mb.StyleCustomMode = true;
            mb.TabIndex = 38;
            mb.Text = "00";
            mb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // hb
            // 
            hb.BackColor = Color.Transparent;
            hb.Font = new Font("宋体", 12F);
            hb.ForeColor = Color.DarkGray;
            hb.Location = new Point(292, 103);
            hb.Name = "hb";
            hb.Size = new Size(46, 22);
            hb.StyleCustomMode = true;
            hb.TabIndex = 37;
            hb.Text = "00";
            hb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // st
            // 
            st.BackColor = Color.Transparent;
            st.Font = new Font("宋体", 12F);
            st.ForeColor = Color.DarkGray;
            st.Location = new Point(398, 31);
            st.Name = "st";
            st.Size = new Size(46, 22);
            st.StyleCustomMode = true;
            st.TabIndex = 36;
            st.Text = "00";
            st.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mt
            // 
            mt.BackColor = Color.Transparent;
            mt.Font = new Font("宋体", 12F);
            mt.ForeColor = Color.DarkGray;
            mt.Location = new Point(345, 31);
            mt.Name = "mt";
            mt.Size = new Size(46, 22);
            mt.StyleCustomMode = true;
            mt.TabIndex = 35;
            mt.Text = "00";
            mt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ht
            // 
            ht.BackColor = Color.Transparent;
            ht.Font = new Font("宋体", 12F);
            ht.ForeColor = Color.DarkGray;
            ht.Location = new Point(292, 31);
            ht.Name = "ht";
            ht.Size = new Size(46, 22);
            ht.StyleCustomMode = true;
            ht.TabIndex = 34;
            ht.Text = "00";
            ht.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // sc
            // 
            sc.BackColor = Color.Transparent;
            sc.Font = new Font("宋体", 12F);
            sc.ForeColor = Color.FromArgb(48, 48, 48);
            sc.Location = new Point(398, 65);
            sc.Name = "sc";
            sc.Size = new Size(46, 27);
            sc.StyleCustomMode = true;
            sc.TabIndex = 33;
            sc.Text = "00";
            sc.TextAlign = ContentAlignment.MiddleCenter;
            sc.DoubleClick += sc_DoubleClick;
            // 
            // mc
            // 
            mc.BackColor = Color.Transparent;
            mc.Font = new Font("宋体", 12F);
            mc.ForeColor = Color.FromArgb(48, 48, 48);
            mc.Location = new Point(345, 65);
            mc.Name = "mc";
            mc.Size = new Size(46, 27);
            mc.StyleCustomMode = true;
            mc.TabIndex = 32;
            mc.Text = "00";
            mc.TextAlign = ContentAlignment.MiddleCenter;
            mc.DoubleClick += mc_DoubleClick;
            // 
            // hc
            // 
            hc.BackColor = Color.Transparent;
            hc.Font = new Font("宋体", 12F);
            hc.ForeColor = Color.FromArgb(48, 48, 48);
            hc.Location = new Point(292, 65);
            hc.Name = "hc";
            hc.Size = new Size(46, 27);
            hc.StyleCustomMode = true;
            hc.TabIndex = 31;
            hc.Text = "00";
            hc.TextAlign = ContentAlignment.MiddleCenter;
            hc.DoubleClick += hc_DoubleClick;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("宋体", 12F);
            btnCancel.Location = new Point(370, 162);
            btnCancel.MinimumSize = new Size(1, 1);
            btnCancel.Name = "btnCancel";
            btnCancel.Padding = new Padding(28, 0, 0, 0);
            btnCancel.Size = new Size(74, 27);
            btnCancel.Style = UIStyle.Custom;
            btnCancel.Symbol = 361453;
            btnCancel.SymbolSize = 22;
            btnCancel.TabIndex = 30;
            btnCancel.Text = "取消";
            btnCancel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Cursor = Cursors.Hand;
            btnOK.Font = new Font("宋体", 12F);
            btnOK.Location = new Point(291, 162);
            btnOK.MinimumSize = new Size(1, 1);
            btnOK.Name = "btnOK";
            btnOK.Padding = new Padding(28, 0, 0, 0);
            btnOK.Size = new Size(74, 27);
            btnOK.Style = UIStyle.Custom;
            btnOK.SymbolSize = 22;
            btnOK.TabIndex = 29;
            btnOK.Text = "确定";
            btnOK.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            btnOK.Click += btnOK_Click;
            // 
            // s2
            // 
            s2.BackColor = Color.Transparent;
            s2.Cursor = Cursors.Hand;
            s2.FillColor = Color.White;
            s2.FillHoverColor = Color.FromArgb(80, 160, 255);
            s2.Font = new Font("宋体", 12F);
            s2.ForeColor = Color.FromArgb(80, 160, 255);
            s2.Location = new Point(405, 129);
            s2.MinimumSize = new Size(1, 1);
            s2.Name = "s2";
            s2.Padding = new Padding(24, 0, 0, 0);
            s2.RadiusSides = UICornerRadiusSides.None;
            s2.RectHoverColor = Color.FromArgb(80, 160, 255);
            s2.RectSides = ToolStripStatusLabelBorderSides.None;
            s2.Size = new Size(30, 24);
            s2.Style = UIStyle.Custom;
            s2.StyleCustomMode = true;
            s2.Symbol = 61703;
            s2.TabIndex = 28;
            s2.Tag = "6";
            s2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            s2.Click += s2_Click;
            // 
            // mm2
            // 
            mm2.BackColor = Color.Transparent;
            mm2.Cursor = Cursors.Hand;
            mm2.FillColor = Color.White;
            mm2.FillHoverColor = Color.FromArgb(80, 160, 255);
            mm2.Font = new Font("宋体", 12F);
            mm2.ForeColor = Color.FromArgb(80, 160, 255);
            mm2.Location = new Point(352, 129);
            mm2.MinimumSize = new Size(1, 1);
            mm2.Name = "mm2";
            mm2.Padding = new Padding(24, 0, 0, 0);
            mm2.RadiusSides = UICornerRadiusSides.None;
            mm2.RectHoverColor = Color.FromArgb(80, 160, 255);
            mm2.RectSides = ToolStripStatusLabelBorderSides.None;
            mm2.Size = new Size(30, 24);
            mm2.Style = UIStyle.Custom;
            mm2.StyleCustomMode = true;
            mm2.Symbol = 61703;
            mm2.TabIndex = 27;
            mm2.Tag = "5";
            mm2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            mm2.Click += mm2_Click;
            // 
            // h2
            // 
            h2.BackColor = Color.Transparent;
            h2.Cursor = Cursors.Hand;
            h2.FillColor = Color.White;
            h2.FillHoverColor = Color.FromArgb(80, 160, 255);
            h2.Font = new Font("宋体", 12F);
            h2.ForeColor = Color.FromArgb(80, 160, 255);
            h2.Location = new Point(299, 128);
            h2.MinimumSize = new Size(1, 1);
            h2.Name = "h2";
            h2.Padding = new Padding(24, 0, 0, 0);
            h2.RadiusSides = UICornerRadiusSides.None;
            h2.RectHoverColor = Color.FromArgb(80, 160, 255);
            h2.RectSides = ToolStripStatusLabelBorderSides.None;
            h2.Size = new Size(30, 24);
            h2.Style = UIStyle.Custom;
            h2.StyleCustomMode = true;
            h2.Symbol = 61703;
            h2.TabIndex = 26;
            h2.Tag = "4";
            h2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            h2.Click += h2_Click;
            // 
            // s1
            // 
            s1.BackColor = Color.Transparent;
            s1.Cursor = Cursors.Hand;
            s1.FillColor = Color.White;
            s1.FillHoverColor = Color.FromArgb(80, 160, 255);
            s1.Font = new Font("宋体", 12F);
            s1.ForeColor = Color.FromArgb(80, 160, 255);
            s1.Location = new Point(405, 4);
            s1.MinimumSize = new Size(1, 1);
            s1.Name = "s1";
            s1.Padding = new Padding(24, 0, 0, 0);
            s1.RadiusSides = UICornerRadiusSides.None;
            s1.RectHoverColor = Color.FromArgb(80, 160, 255);
            s1.RectSides = ToolStripStatusLabelBorderSides.None;
            s1.Size = new Size(30, 24);
            s1.Style = UIStyle.Custom;
            s1.StyleCustomMode = true;
            s1.Symbol = 61702;
            s1.TabIndex = 25;
            s1.Tag = "3";
            s1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            s1.Click += s1_Click;
            // 
            // mm1
            // 
            mm1.BackColor = Color.Transparent;
            mm1.Cursor = Cursors.Hand;
            mm1.FillColor = Color.White;
            mm1.FillHoverColor = Color.FromArgb(80, 160, 255);
            mm1.Font = new Font("宋体", 12F);
            mm1.ForeColor = Color.FromArgb(80, 160, 255);
            mm1.Location = new Point(352, 4);
            mm1.MinimumSize = new Size(1, 1);
            mm1.Name = "mm1";
            mm1.Padding = new Padding(24, 0, 0, 0);
            mm1.RadiusSides = UICornerRadiusSides.None;
            mm1.RectHoverColor = Color.FromArgb(80, 160, 255);
            mm1.RectSides = ToolStripStatusLabelBorderSides.None;
            mm1.Size = new Size(30, 24);
            mm1.Style = UIStyle.Custom;
            mm1.StyleCustomMode = true;
            mm1.Symbol = 61702;
            mm1.TabIndex = 24;
            mm1.Tag = "2";
            mm1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            mm1.Click += mm1_Click;
            // 
            // h1
            // 
            h1.BackColor = Color.Transparent;
            h1.Cursor = Cursors.Hand;
            h1.FillColor = Color.White;
            h1.FillHoverColor = Color.FromArgb(80, 160, 255);
            h1.Font = new Font("宋体", 12F);
            h1.ForeColor = Color.FromArgb(80, 160, 255);
            h1.Location = new Point(299, 4);
            h1.MinimumSize = new Size(1, 1);
            h1.Name = "h1";
            h1.Padding = new Padding(24, 0, 0, 0);
            h1.RadiusSides = UICornerRadiusSides.None;
            h1.RectHoverColor = Color.FromArgb(80, 160, 255);
            h1.RectSides = ToolStripStatusLabelBorderSides.None;
            h1.Size = new Size(30, 24);
            h1.Style = UIStyle.Custom;
            h1.StyleCustomMode = true;
            h1.Symbol = 61702;
            h1.TabIndex = 23;
            h1.Tag = "1";
            h1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            h1.Click += h1_Click;
            // 
            // uiPanel1
            // 
            uiPanel1.BackColor = Color.White;
            uiPanel1.Controls.Add(TabControl);
            uiPanel1.Controls.Add(TopPanel);
            uiPanel1.Dock = DockStyle.Left;
            uiPanel1.FillColor = Color.White;
            uiPanel1.Font = new Font("宋体", 12F);
            uiPanel1.Location = new Point(0, 0);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.RadiusSides = UICornerRadiusSides.None;
            uiPanel1.Size = new Size(284, 235);
            uiPanel1.Style = UIStyle.Custom;
            uiPanel1.StyleCustomMode = true;
            uiPanel1.TabIndex = 3;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // UIDateTimeItem
            // 
            Controls.Add(sb);
            Controls.Add(uiPanel1);
            Controls.Add(mb);
            Controls.Add(hb);
            Controls.Add(h1);
            Controls.Add(st);
            Controls.Add(mt);
            Controls.Add(ht);
            Controls.Add(mm1);
            Controls.Add(sc);
            Controls.Add(s1);
            Controls.Add(mc);
            Controls.Add(h2);
            Controls.Add(hc);
            Controls.Add(mm2);
            Controls.Add(btnCancel);
            Controls.Add(s2);
            Controls.Add(btnOK);
            FillColor = Color.White;
            Name = "UIDateTimeItem";
            Size = new Size(452, 235);
            Style = UIStyle.Custom;
            StyleCustomMode = true;
            Paint += UIDateTimeItem_Paint;
            TopPanel.ResumeLayout(false);
            TabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            uiPanel1.ResumeLayout(false);
            ResumeLayout(false);
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

            if (SizeMultiple > 1)
            {
                TopPanel.Height = 31 * SizeMultiple;
                foreach (Control item in TopPanel.Controls)
                {
                    if (!SizeMultipled)
                    {
                        item.Left = item.Left * SizeMultiple;
                        item.Top = item.Top * SizeMultiple;
                        item.Width = item.Width * SizeMultiple;
                        item.Height = item.Height * SizeMultiple;
                        if (item is ISymbol symbol) symbol.SymbolSize = (int)(symbol.SymbolSize * 1.5f);
                    }
                }

                TopPanel.Font = new Font(TopPanel.Font.FontFamily, TopPanel.Font.Size * 1.5f);

                foreach (Control item in this.Controls)
                {
                    if (item.Parent == TopPanel) return;
                    if (item == TopPanel) return;
                    if (!SizeMultipled)
                    {
                        item.Left = item.Left * SizeMultiple;
                        item.Top = item.Top * SizeMultiple;
                        item.Width = item.Width * SizeMultiple;
                        item.Height = item.Height * SizeMultiple;
                        if (item is ISymbol symbol) symbol.SymbolSize = (int)(symbol.SymbolSize * 1.5f);
                    }

                    item.Font = new Font(item.Font.FontFamily, item.Font.Size * 1.5f);
                }


                SizeMultipled = true;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (b1 == null) return;
            b4.Left = TopPanel.Width - b1.Left - b4.Width;
            b3.Left = TopPanel.Width - b2.Left - b3.Width;
        }

        internal bool SizeMultipled = false;

        private int sizeMultiple = 1;
        public int SizeMultiple
        {
            get => sizeMultiple;
            set
            {
                if (value < 1) value = 1;
                if (value > 2) value = 2;

                sizeMultiple = value;
            }
        }

        public void Translate()
        {
            months.Clear();
            months.Add(UIStyles.CurrentResources.January);
            months.Add(UIStyles.CurrentResources.February);
            months.Add(UIStyles.CurrentResources.March);
            months.Add(UIStyles.CurrentResources.April);
            months.Add(UIStyles.CurrentResources.May);
            months.Add(UIStyles.CurrentResources.June);
            months.Add(UIStyles.CurrentResources.July);
            months.Add(UIStyles.CurrentResources.August);
            months.Add(UIStyles.CurrentResources.September);
            months.Add(UIStyles.CurrentResources.October);
            months.Add(UIStyles.CurrentResources.November);
            months.Add(UIStyles.CurrentResources.December);

            btnOK.Text = UIStyles.CurrentResources.OK;
            btnCancel.Text = UIStyles.CurrentResources.Cancel;
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
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            for (int i = 0; i < 12; i++)
            {
                int width = p2.Width / 4;
                int height = p2.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);
                if (i + 1 == Month)
                {
                    e.Graphics.DrawString(months[i], font, PrimaryColor, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
                }
                else
                {
                    e.Graphics.DrawString(months[i], font, i == activeMonth ? PrimaryColor : ForeColor, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
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
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            for (int i = 0; i < 12; i++)
            {
                int width = p1.Width / 4;
                int height = p1.Height / 3;
                int left = width * (i % 4);
                int top = height * (i / 4);
                Color color = (i == 0 || i == 11) ? Color.DarkGray : ForeColor;
                e.Graphics.DrawString(years[i].ToString(), font, (i == activeYear || years[i] == Year) ? PrimaryColor : color, new Rectangle(left, top, width, height), ContentAlignment.MiddleCenter);
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
            int height = (p3.Height - 30 * SizeMultiple) / 6;
            using Font font = SizeMultiple == 1 ? this.Font : new Font(this.Font.FontFamily, this.Font.Size * 1.5f);
            string[] weeks = { UIStyles.CurrentResources.Sunday, UIStyles.CurrentResources.Monday, UIStyles.CurrentResources.Tuesday, UIStyles.CurrentResources.Wednesday, UIStyles.CurrentResources.Thursday, UIStyles.CurrentResources.Friday, UIStyles.CurrentResources.Saturday };
            for (int i = 0; i < weeks.Length; i++)
            {
                e.Graphics.DrawString(weeks[i], font, ForeColor, new Rectangle(width * i, 4 * SizeMultiple, width, 19 * SizeMultiple), ContentAlignment.MiddleCenter);
            }

            e.Graphics.DrawLine(Color.LightGray, 8, 30 * SizeMultiple - 4, p3.Width - 8, 30 * SizeMultiple - 4);

            bool maxDrawer = false;
            for (int i = 0; i < 42; i++)
            {
                int left = width * (i % 7);
                int top = height * (i / 7);
                Color color = (days[i].Month == Month) ? ForeColor : Color.DarkGray;
                color = (days[i].DateString() == date.DateString()) ? b3.SymbolColor : color;

                if (days[i].DateString() == date.DateString())
                {
                    e.Graphics.DrawRectangle(b3.SymbolColor, new Rectangle(left + 1, top + 30 * SizeMultiple + 1, width - 2, height - 2));
                }

                if (!maxDrawer)
                {
                    e.Graphics.DrawString(days[i].Day.ToString(), font, i == activeDay ? PrimaryColor : color, new Rectangle(left, top + 30 * SizeMultiple, width, height), ContentAlignment.MiddleCenter);
                }

                if (!maxDrawer && days[i].Date.Equals(DateTime.MaxValue.Date))
                {
                    maxDrawer = true;
                }
            }

            if (ShowToday)
            {
                using Font SubFont = this.Font.DPIScaleFont(SizeMultiple == 1 ? 10.5f : 15.75f);
                e.Graphics.FillRectangle(p3.FillColor, p3.Width - width * 4 + 1, p3.Height - height + 1, width * 4 - 2, height - 2);
                e.Graphics.DrawString(UIStyles.CurrentResources.Today + "  " + DateTime.Now.DateString(), SubFont, isToday ? b3.SymbolColor : Color.DarkGray, new Rectangle(p3.Width - width * 4, p3.Height - height - 1, Width, height), ContentAlignment.MiddleLeft);
                SizeF sf = TextRenderer.MeasureText(UIStyles.CurrentResources.Today, SubFont);
                e.Graphics.DrawRectangle(b3.SymbolColor, new Rectangle(p3.Width - width * 4 + 1, p3.Height - height + 1, (int)sf.Width - 2, height - 4));
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
                DoValueChanged(this, Date);
                Close();
            }

            date = new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second);
            DoValueChanged(this, Date);
            p3.Invalidate();
            //CloseParent();
        }

        private void P3_MouseDoubleClick(object sender, MouseEventArgs e)
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
                DoValueChanged(this, Date);
                Close();
            }

            date = new DateTime(date.Year, date.Month, date.Day, Hour, Minute, Second);
            DoValueChanged(this, Date);
            p3.Invalidate();
            Close();
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

        private void UIDateTimeItem_Paint(object sender, PaintEventArgs e)
        {
            if (SizeMultiple == 1)
            {
                e.Graphics.DrawLine(Color.LightGray, 292 * 1, 57 * SizeMultiple, 444 * SizeMultiple, 57 * SizeMultiple);
                e.Graphics.DrawLine(Color.LightGray, 292 * 1, 101 * SizeMultiple, 444 * SizeMultiple, 101 * SizeMultiple);
            }
            else
            {
                e.Graphics.DrawLine(Color.LightGray, 292 * 2, 59 * SizeMultiple, 444 * SizeMultiple, 59 * SizeMultiple);
                e.Graphics.DrawLine(Color.LightGray, 292 * 2, 99 * SizeMultiple, 444 * SizeMultiple, 99 * SizeMultiple);

            }
        }
    }
}