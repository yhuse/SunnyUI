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
 * 文件名称: UIDateItem.cs
 * 文件说明: 日期选择框弹出窗体
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
 * 2020-05-29: V2.2.5 重写
******************************************************************************/

using System;
using System.Drawing;

namespace Sunny.UI
{
    public sealed class UIDateItem : UIDropDownItem
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
        private UILabel m12;
        private UILabel m11;
        private UILabel m10;
        private UILabel m9;
        private UILabel m8;
        private UILabel m7;
        private UILabel m6;
        private UILabel m5;
        private UILabel m4;
        private UILabel m3;
        private UILabel m2;
        private UILabel m1;
        private UILabel d36;
        private UILabel d29;
        private UILabel d22;
        private UILabel d15;
        private UILabel d8;
        private UILabel d1;
        private UILabel uiLabel1;
        private UILine uiLine1;
        private UILabel d42;
        private UILabel d35;
        private UILabel d28;
        private UILabel d21;
        private UILabel d14;
        private UILabel d7;
        private UILabel d41;
        private UILabel d34;
        private UILabel d27;
        private UILabel d20;
        private UILabel d13;
        private UILabel d6;
        private UILabel d40;
        private UILabel d33;
        private UILabel d26;
        private UILabel d19;
        private UILabel d12;
        private UILabel d5;
        private UILabel d39;
        private UILabel d32;
        private UILabel d25;
        private UILabel d18;
        private UILabel d11;
        private UILabel d4;
        private UILabel d38;
        private UILabel d31;
        private UILabel d24;
        private UILabel d17;
        private UILabel d10;
        private UILabel d3;
        private UILabel d37;
        private UILabel d30;
        private UILabel d23;
        private UILabel d16;
        private UILabel d9;
        private UILabel d2;
        private UILabel uiLabel13;
        private UILabel uiLabel12;
        private UILabel uiLabel11;
        private UILabel uiLabel10;
        private UILabel uiLabel9;
        private UILabel uiLabel7;
        private UILabel y10;
        private UILabel y9;
        private UILabel y8;
        private UILabel y7;
        private UILabel y6;
        private UILabel y5;
        private UILabel y4;
        private UILabel y3;
        private UILabel y2;
        private UILabel y1;
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
            this.y10 = new Sunny.UI.UILabel();
            this.y9 = new Sunny.UI.UILabel();
            this.y8 = new Sunny.UI.UILabel();
            this.y7 = new Sunny.UI.UILabel();
            this.y6 = new Sunny.UI.UILabel();
            this.y5 = new Sunny.UI.UILabel();
            this.y4 = new Sunny.UI.UILabel();
            this.y3 = new Sunny.UI.UILabel();
            this.y2 = new Sunny.UI.UILabel();
            this.y1 = new Sunny.UI.UILabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.p2 = new Sunny.UI.UIPanel();
            this.m12 = new Sunny.UI.UILabel();
            this.m11 = new Sunny.UI.UILabel();
            this.m10 = new Sunny.UI.UILabel();
            this.m9 = new Sunny.UI.UILabel();
            this.m8 = new Sunny.UI.UILabel();
            this.m7 = new Sunny.UI.UILabel();
            this.m6 = new Sunny.UI.UILabel();
            this.m5 = new Sunny.UI.UILabel();
            this.m4 = new Sunny.UI.UILabel();
            this.m3 = new Sunny.UI.UILabel();
            this.m2 = new Sunny.UI.UILabel();
            this.m1 = new Sunny.UI.UILabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.p3 = new Sunny.UI.UIPanel();
            this.d42 = new Sunny.UI.UILabel();
            this.d35 = new Sunny.UI.UILabel();
            this.d28 = new Sunny.UI.UILabel();
            this.d21 = new Sunny.UI.UILabel();
            this.d14 = new Sunny.UI.UILabel();
            this.d7 = new Sunny.UI.UILabel();
            this.d41 = new Sunny.UI.UILabel();
            this.d34 = new Sunny.UI.UILabel();
            this.d27 = new Sunny.UI.UILabel();
            this.d20 = new Sunny.UI.UILabel();
            this.d13 = new Sunny.UI.UILabel();
            this.d6 = new Sunny.UI.UILabel();
            this.d40 = new Sunny.UI.UILabel();
            this.d33 = new Sunny.UI.UILabel();
            this.d26 = new Sunny.UI.UILabel();
            this.d19 = new Sunny.UI.UILabel();
            this.d12 = new Sunny.UI.UILabel();
            this.d5 = new Sunny.UI.UILabel();
            this.d39 = new Sunny.UI.UILabel();
            this.d32 = new Sunny.UI.UILabel();
            this.d25 = new Sunny.UI.UILabel();
            this.d18 = new Sunny.UI.UILabel();
            this.d11 = new Sunny.UI.UILabel();
            this.d4 = new Sunny.UI.UILabel();
            this.d38 = new Sunny.UI.UILabel();
            this.d31 = new Sunny.UI.UILabel();
            this.d24 = new Sunny.UI.UILabel();
            this.d17 = new Sunny.UI.UILabel();
            this.d10 = new Sunny.UI.UILabel();
            this.d3 = new Sunny.UI.UILabel();
            this.d37 = new Sunny.UI.UILabel();
            this.d30 = new Sunny.UI.UILabel();
            this.d23 = new Sunny.UI.UILabel();
            this.d16 = new Sunny.UI.UILabel();
            this.d9 = new Sunny.UI.UILabel();
            this.d2 = new Sunny.UI.UILabel();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.d36 = new Sunny.UI.UILabel();
            this.d29 = new Sunny.UI.UILabel();
            this.d22 = new Sunny.UI.UILabel();
            this.d15 = new Sunny.UI.UILabel();
            this.d8 = new Sunny.UI.UILabel();
            this.d1 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLine1 = new Sunny.UI.UILine();
            this.TopPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.p1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.p2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.p3.SuspendLayout();
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
            this.TopPanel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.TopPanel.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.TopPanel.Size = new System.Drawing.Size(461, 31);
            this.TopPanel.Style = Sunny.UI.UIStyle.Custom;
            this.TopPanel.StyleCustomMode = true;
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Text = "2020-05-05";
            this.TopPanel.Click += new System.EventHandler(this.TopPanel_Click);
            // 
            // b4
            // 
            this.b4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b4.BackColor = System.Drawing.Color.Transparent;
            this.b4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b4.FillColor = System.Drawing.Color.White;
            this.b4.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b4.ImageInterval = 0;
            this.b4.Location = new System.Drawing.Point(427, 4);
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
            this.b3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b3.ImageInterval = 0;
            this.b3.Location = new System.Drawing.Point(391, 4);
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
            this.b2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b2.ImageInterval = 0;
            this.b2.Location = new System.Drawing.Point(40, 4);
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
            this.b1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.b1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.b1.ImageInterval = 0;
            this.b1.Location = new System.Drawing.Point(4, 4);
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
            this.TabControl.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.TabControl.ItemSize = new System.Drawing.Size(150, 40);
            this.TabControl.Location = new System.Drawing.Point(0, 31);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(461, 317);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.Style = Sunny.UI.UIStyle.Custom;
            this.TabControl.TabIndex = 1;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.p1);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(461, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // p1
            // 
            this.p1.Controls.Add(this.y10);
            this.p1.Controls.Add(this.y9);
            this.p1.Controls.Add(this.y8);
            this.p1.Controls.Add(this.y7);
            this.p1.Controls.Add(this.y6);
            this.p1.Controls.Add(this.y5);
            this.p1.Controls.Add(this.y4);
            this.p1.Controls.Add(this.y3);
            this.p1.Controls.Add(this.y2);
            this.p1.Controls.Add(this.y1);
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.FillColor = System.Drawing.Color.White;
            this.p1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.p1.Location = new System.Drawing.Point(0, 0);
            this.p1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p1.Name = "p1";
            this.p1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p1.Size = new System.Drawing.Size(461, 277);
            this.p1.Style = Sunny.UI.UIStyle.Custom;
            this.p1.TabIndex = 0;
            this.p1.Text = null;
            // 
            // y10
            // 
            this.y10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y10.Location = new System.Drawing.Point(75, 112);
            this.y10.Name = "y10";
            this.y10.Size = new System.Drawing.Size(64, 51);
            this.y10.Style = Sunny.UI.UIStyle.Custom;
            this.y10.StyleCustomMode = true;
            this.y10.TabIndex = 33;
            this.y10.Tag = "10";
            this.y10.Text = "十月";
            this.y10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y10.Click += new System.EventHandler(this.y1_Click);
            // 
            // y9
            // 
            this.y9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y9.Location = new System.Drawing.Point(4, 112);
            this.y9.Name = "y9";
            this.y9.Size = new System.Drawing.Size(64, 51);
            this.y9.Style = Sunny.UI.UIStyle.Custom;
            this.y9.StyleCustomMode = true;
            this.y9.TabIndex = 32;
            this.y9.Tag = "9";
            this.y9.Text = "九月";
            this.y9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y9.Click += new System.EventHandler(this.y1_Click);
            // 
            // y8
            // 
            this.y8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y8.Location = new System.Drawing.Point(216, 58);
            this.y8.Name = "y8";
            this.y8.Size = new System.Drawing.Size(64, 51);
            this.y8.Style = Sunny.UI.UIStyle.Custom;
            this.y8.StyleCustomMode = true;
            this.y8.TabIndex = 31;
            this.y8.Tag = "8";
            this.y8.Text = "八月";
            this.y8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y8.Click += new System.EventHandler(this.y1_Click);
            // 
            // y7
            // 
            this.y7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y7.Location = new System.Drawing.Point(147, 58);
            this.y7.Name = "y7";
            this.y7.Size = new System.Drawing.Size(64, 51);
            this.y7.Style = Sunny.UI.UIStyle.Custom;
            this.y7.StyleCustomMode = true;
            this.y7.TabIndex = 30;
            this.y7.Tag = "7";
            this.y7.Text = "七月";
            this.y7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y7.Click += new System.EventHandler(this.y1_Click);
            // 
            // y6
            // 
            this.y6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y6.Location = new System.Drawing.Point(75, 58);
            this.y6.Name = "y6";
            this.y6.Size = new System.Drawing.Size(64, 51);
            this.y6.Style = Sunny.UI.UIStyle.Custom;
            this.y6.StyleCustomMode = true;
            this.y6.TabIndex = 29;
            this.y6.Tag = "6";
            this.y6.Text = "六月";
            this.y6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y6.Click += new System.EventHandler(this.y1_Click);
            // 
            // y5
            // 
            this.y5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y5.Location = new System.Drawing.Point(4, 58);
            this.y5.Name = "y5";
            this.y5.Size = new System.Drawing.Size(64, 51);
            this.y5.Style = Sunny.UI.UIStyle.Custom;
            this.y5.StyleCustomMode = true;
            this.y5.TabIndex = 28;
            this.y5.Tag = "5";
            this.y5.Text = "五月";
            this.y5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y5.Click += new System.EventHandler(this.y1_Click);
            // 
            // y4
            // 
            this.y4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y4.Location = new System.Drawing.Point(216, 4);
            this.y4.Name = "y4";
            this.y4.Size = new System.Drawing.Size(64, 51);
            this.y4.Style = Sunny.UI.UIStyle.Custom;
            this.y4.StyleCustomMode = true;
            this.y4.TabIndex = 27;
            this.y4.Tag = "4";
            this.y4.Text = "四月";
            this.y4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y4.Click += new System.EventHandler(this.y1_Click);
            // 
            // y3
            // 
            this.y3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y3.Location = new System.Drawing.Point(147, 4);
            this.y3.Name = "y3";
            this.y3.Size = new System.Drawing.Size(64, 51);
            this.y3.Style = Sunny.UI.UIStyle.Custom;
            this.y3.StyleCustomMode = true;
            this.y3.TabIndex = 26;
            this.y3.Tag = "3";
            this.y3.Text = "三月";
            this.y3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y3.Click += new System.EventHandler(this.y1_Click);
            // 
            // y2
            // 
            this.y2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y2.Location = new System.Drawing.Point(75, 4);
            this.y2.Name = "y2";
            this.y2.Size = new System.Drawing.Size(64, 51);
            this.y2.Style = Sunny.UI.UIStyle.Custom;
            this.y2.StyleCustomMode = true;
            this.y2.TabIndex = 25;
            this.y2.Tag = "2";
            this.y2.Text = "二月";
            this.y2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y2.Click += new System.EventHandler(this.y1_Click);
            // 
            // y1
            // 
            this.y1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.y1.Location = new System.Drawing.Point(4, 4);
            this.y1.Name = "y1";
            this.y1.Size = new System.Drawing.Size(64, 51);
            this.y1.Style = Sunny.UI.UIStyle.Custom;
            this.y1.StyleCustomMode = true;
            this.y1.TabIndex = 24;
            this.y1.Tag = "1";
            this.y1.Text = "一月";
            this.y1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.y1.Click += new System.EventHandler(this.y1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.p2);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(461, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.m12);
            this.p2.Controls.Add(this.m11);
            this.p2.Controls.Add(this.m10);
            this.p2.Controls.Add(this.m9);
            this.p2.Controls.Add(this.m8);
            this.p2.Controls.Add(this.m7);
            this.p2.Controls.Add(this.m6);
            this.p2.Controls.Add(this.m5);
            this.p2.Controls.Add(this.m4);
            this.p2.Controls.Add(this.m3);
            this.p2.Controls.Add(this.m2);
            this.p2.Controls.Add(this.m1);
            this.p2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p2.FillColor = System.Drawing.Color.White;
            this.p2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.p2.Location = new System.Drawing.Point(0, 0);
            this.p2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p2.Name = "p2";
            this.p2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p2.Size = new System.Drawing.Size(461, 277);
            this.p2.Style = Sunny.UI.UIStyle.Custom;
            this.p2.TabIndex = 1;
            this.p2.Text = null;
            // 
            // m12
            // 
            this.m12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m12.Location = new System.Drawing.Point(216, 112);
            this.m12.Name = "m12";
            this.m12.Size = new System.Drawing.Size(64, 51);
            this.m12.Style = Sunny.UI.UIStyle.Custom;
            this.m12.StyleCustomMode = true;
            this.m12.TabIndex = 23;
            this.m12.Tag = "12";
            this.m12.Text = "十二月";
            this.m12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m12.Click += new System.EventHandler(this.m1_Click);
            // 
            // m11
            // 
            this.m11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m11.Location = new System.Drawing.Point(147, 112);
            this.m11.Name = "m11";
            this.m11.Size = new System.Drawing.Size(64, 51);
            this.m11.Style = Sunny.UI.UIStyle.Custom;
            this.m11.StyleCustomMode = true;
            this.m11.TabIndex = 22;
            this.m11.Tag = "11";
            this.m11.Text = "十一月";
            this.m11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m11.Click += new System.EventHandler(this.m1_Click);
            // 
            // m10
            // 
            this.m10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m10.Location = new System.Drawing.Point(75, 112);
            this.m10.Name = "m10";
            this.m10.Size = new System.Drawing.Size(64, 51);
            this.m10.Style = Sunny.UI.UIStyle.Custom;
            this.m10.StyleCustomMode = true;
            this.m10.TabIndex = 21;
            this.m10.Tag = "10";
            this.m10.Text = "十月";
            this.m10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m10.Click += new System.EventHandler(this.m1_Click);
            // 
            // m9
            // 
            this.m9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m9.Location = new System.Drawing.Point(4, 112);
            this.m9.Name = "m9";
            this.m9.Size = new System.Drawing.Size(64, 51);
            this.m9.Style = Sunny.UI.UIStyle.Custom;
            this.m9.StyleCustomMode = true;
            this.m9.TabIndex = 20;
            this.m9.Tag = "9";
            this.m9.Text = "九月";
            this.m9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m9.Click += new System.EventHandler(this.m1_Click);
            // 
            // m8
            // 
            this.m8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m8.Location = new System.Drawing.Point(216, 58);
            this.m8.Name = "m8";
            this.m8.Size = new System.Drawing.Size(64, 51);
            this.m8.Style = Sunny.UI.UIStyle.Custom;
            this.m8.StyleCustomMode = true;
            this.m8.TabIndex = 19;
            this.m8.Tag = "8";
            this.m8.Text = "八月";
            this.m8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m8.Click += new System.EventHandler(this.m1_Click);
            // 
            // m7
            // 
            this.m7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m7.Location = new System.Drawing.Point(147, 58);
            this.m7.Name = "m7";
            this.m7.Size = new System.Drawing.Size(64, 51);
            this.m7.Style = Sunny.UI.UIStyle.Custom;
            this.m7.StyleCustomMode = true;
            this.m7.TabIndex = 18;
            this.m7.Tag = "7";
            this.m7.Text = "七月";
            this.m7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m7.Click += new System.EventHandler(this.m1_Click);
            // 
            // m6
            // 
            this.m6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m6.Location = new System.Drawing.Point(75, 58);
            this.m6.Name = "m6";
            this.m6.Size = new System.Drawing.Size(64, 51);
            this.m6.Style = Sunny.UI.UIStyle.Custom;
            this.m6.StyleCustomMode = true;
            this.m6.TabIndex = 17;
            this.m6.Tag = "6";
            this.m6.Text = "六月";
            this.m6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m6.Click += new System.EventHandler(this.m1_Click);
            // 
            // m5
            // 
            this.m5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m5.Location = new System.Drawing.Point(4, 58);
            this.m5.Name = "m5";
            this.m5.Size = new System.Drawing.Size(64, 51);
            this.m5.Style = Sunny.UI.UIStyle.Custom;
            this.m5.StyleCustomMode = true;
            this.m5.TabIndex = 16;
            this.m5.Tag = "5";
            this.m5.Text = "五月";
            this.m5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m5.Click += new System.EventHandler(this.m1_Click);
            // 
            // m4
            // 
            this.m4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m4.Location = new System.Drawing.Point(216, 4);
            this.m4.Name = "m4";
            this.m4.Size = new System.Drawing.Size(64, 51);
            this.m4.Style = Sunny.UI.UIStyle.Custom;
            this.m4.StyleCustomMode = true;
            this.m4.TabIndex = 15;
            this.m4.Tag = "4";
            this.m4.Text = "四月";
            this.m4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m4.Click += new System.EventHandler(this.m1_Click);
            // 
            // m3
            // 
            this.m3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m3.Location = new System.Drawing.Point(147, 4);
            this.m3.Name = "m3";
            this.m3.Size = new System.Drawing.Size(64, 51);
            this.m3.Style = Sunny.UI.UIStyle.Custom;
            this.m3.StyleCustomMode = true;
            this.m3.TabIndex = 14;
            this.m3.Tag = "3";
            this.m3.Text = "三月";
            this.m3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m3.Click += new System.EventHandler(this.m1_Click);
            // 
            // m2
            // 
            this.m2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m2.Location = new System.Drawing.Point(75, 4);
            this.m2.Name = "m2";
            this.m2.Size = new System.Drawing.Size(64, 51);
            this.m2.Style = Sunny.UI.UIStyle.Custom;
            this.m2.StyleCustomMode = true;
            this.m2.TabIndex = 13;
            this.m2.Tag = "2";
            this.m2.Text = "二月";
            this.m2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m2.Click += new System.EventHandler(this.m1_Click);
            // 
            // m1
            // 
            this.m1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.m1.Location = new System.Drawing.Point(4, 4);
            this.m1.Name = "m1";
            this.m1.Size = new System.Drawing.Size(64, 51);
            this.m1.Style = Sunny.UI.UIStyle.Custom;
            this.m1.StyleCustomMode = true;
            this.m1.TabIndex = 12;
            this.m1.Tag = "1";
            this.m1.Text = "一月";
            this.m1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m1.Click += new System.EventHandler(this.m1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.p3);
            this.tabPage3.Location = new System.Drawing.Point(0, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(461, 277);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // p3
            // 
            this.p3.Controls.Add(this.d42);
            this.p3.Controls.Add(this.d35);
            this.p3.Controls.Add(this.d28);
            this.p3.Controls.Add(this.d21);
            this.p3.Controls.Add(this.d14);
            this.p3.Controls.Add(this.d7);
            this.p3.Controls.Add(this.d41);
            this.p3.Controls.Add(this.d34);
            this.p3.Controls.Add(this.d27);
            this.p3.Controls.Add(this.d20);
            this.p3.Controls.Add(this.d13);
            this.p3.Controls.Add(this.d6);
            this.p3.Controls.Add(this.d40);
            this.p3.Controls.Add(this.d33);
            this.p3.Controls.Add(this.d26);
            this.p3.Controls.Add(this.d19);
            this.p3.Controls.Add(this.d12);
            this.p3.Controls.Add(this.d5);
            this.p3.Controls.Add(this.d39);
            this.p3.Controls.Add(this.d32);
            this.p3.Controls.Add(this.d25);
            this.p3.Controls.Add(this.d18);
            this.p3.Controls.Add(this.d11);
            this.p3.Controls.Add(this.d4);
            this.p3.Controls.Add(this.d38);
            this.p3.Controls.Add(this.d31);
            this.p3.Controls.Add(this.d24);
            this.p3.Controls.Add(this.d17);
            this.p3.Controls.Add(this.d10);
            this.p3.Controls.Add(this.d3);
            this.p3.Controls.Add(this.d37);
            this.p3.Controls.Add(this.d30);
            this.p3.Controls.Add(this.d23);
            this.p3.Controls.Add(this.d16);
            this.p3.Controls.Add(this.d9);
            this.p3.Controls.Add(this.d2);
            this.p3.Controls.Add(this.uiLabel13);
            this.p3.Controls.Add(this.uiLabel12);
            this.p3.Controls.Add(this.uiLabel11);
            this.p3.Controls.Add(this.uiLabel10);
            this.p3.Controls.Add(this.uiLabel9);
            this.p3.Controls.Add(this.uiLabel7);
            this.p3.Controls.Add(this.d36);
            this.p3.Controls.Add(this.d29);
            this.p3.Controls.Add(this.d22);
            this.p3.Controls.Add(this.d15);
            this.p3.Controls.Add(this.d8);
            this.p3.Controls.Add(this.d1);
            this.p3.Controls.Add(this.uiLabel1);
            this.p3.Controls.Add(this.uiLine1);
            this.p3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p3.FillColor = System.Drawing.Color.White;
            this.p3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.p3.Location = new System.Drawing.Point(0, 0);
            this.p3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.p3.Name = "p3";
            this.p3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.p3.Size = new System.Drawing.Size(461, 277);
            this.p3.Style = Sunny.UI.UIStyle.Custom;
            this.p3.TabIndex = 2;
            this.p3.Text = null;
            // 
            // d42
            // 
            this.d42.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d42.Location = new System.Drawing.Point(244, 145);
            this.d42.Name = "d42";
            this.d42.Size = new System.Drawing.Size(36, 19);
            this.d42.Style = Sunny.UI.UIStyle.Custom;
            this.d42.StyleCustomMode = true;
            this.d42.TabIndex = 49;
            this.d42.Text = "uiLabel44";
            this.d42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d42.Click += new System.EventHandler(this.d1_Click);
            // 
            // d35
            // 
            this.d35.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d35.Location = new System.Drawing.Point(244, 122);
            this.d35.Name = "d35";
            this.d35.Size = new System.Drawing.Size(36, 19);
            this.d35.Style = Sunny.UI.UIStyle.Custom;
            this.d35.StyleCustomMode = true;
            this.d35.TabIndex = 48;
            this.d35.Text = "uiLabel45";
            this.d35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d35.Click += new System.EventHandler(this.d1_Click);
            // 
            // d28
            // 
            this.d28.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d28.Location = new System.Drawing.Point(244, 99);
            this.d28.Name = "d28";
            this.d28.Size = new System.Drawing.Size(36, 19);
            this.d28.Style = Sunny.UI.UIStyle.Custom;
            this.d28.StyleCustomMode = true;
            this.d28.TabIndex = 47;
            this.d28.Text = "uiLabel46";
            this.d28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d28.Click += new System.EventHandler(this.d1_Click);
            // 
            // d21
            // 
            this.d21.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d21.Location = new System.Drawing.Point(244, 76);
            this.d21.Name = "d21";
            this.d21.Size = new System.Drawing.Size(36, 19);
            this.d21.Style = Sunny.UI.UIStyle.Custom;
            this.d21.StyleCustomMode = true;
            this.d21.TabIndex = 46;
            this.d21.Text = "uiLabel47";
            this.d21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d21.Click += new System.EventHandler(this.d1_Click);
            // 
            // d14
            // 
            this.d14.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d14.Location = new System.Drawing.Point(244, 53);
            this.d14.Name = "d14";
            this.d14.Size = new System.Drawing.Size(36, 19);
            this.d14.Style = Sunny.UI.UIStyle.Custom;
            this.d14.StyleCustomMode = true;
            this.d14.TabIndex = 45;
            this.d14.Text = "uiLabel48";
            this.d14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d14.Click += new System.EventHandler(this.d1_Click);
            // 
            // d7
            // 
            this.d7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d7.Location = new System.Drawing.Point(244, 30);
            this.d7.Name = "d7";
            this.d7.Size = new System.Drawing.Size(36, 19);
            this.d7.Style = Sunny.UI.UIStyle.Custom;
            this.d7.StyleCustomMode = true;
            this.d7.TabIndex = 44;
            this.d7.Text = "uiLabel49";
            this.d7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d7.Click += new System.EventHandler(this.d1_Click);
            // 
            // d41
            // 
            this.d41.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d41.Location = new System.Drawing.Point(204, 145);
            this.d41.Name = "d41";
            this.d41.Size = new System.Drawing.Size(36, 19);
            this.d41.Style = Sunny.UI.UIStyle.Custom;
            this.d41.StyleCustomMode = true;
            this.d41.TabIndex = 43;
            this.d41.Text = "uiLabel38";
            this.d41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d41.Click += new System.EventHandler(this.d1_Click);
            // 
            // d34
            // 
            this.d34.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d34.Location = new System.Drawing.Point(204, 122);
            this.d34.Name = "d34";
            this.d34.Size = new System.Drawing.Size(36, 19);
            this.d34.Style = Sunny.UI.UIStyle.Custom;
            this.d34.StyleCustomMode = true;
            this.d34.TabIndex = 42;
            this.d34.Text = "uiLabel39";
            this.d34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d34.Click += new System.EventHandler(this.d1_Click);
            // 
            // d27
            // 
            this.d27.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d27.Location = new System.Drawing.Point(204, 99);
            this.d27.Name = "d27";
            this.d27.Size = new System.Drawing.Size(36, 19);
            this.d27.Style = Sunny.UI.UIStyle.Custom;
            this.d27.StyleCustomMode = true;
            this.d27.TabIndex = 41;
            this.d27.Text = "uiLabel40";
            this.d27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d27.Click += new System.EventHandler(this.d1_Click);
            // 
            // d20
            // 
            this.d20.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d20.Location = new System.Drawing.Point(204, 76);
            this.d20.Name = "d20";
            this.d20.Size = new System.Drawing.Size(36, 19);
            this.d20.Style = Sunny.UI.UIStyle.Custom;
            this.d20.StyleCustomMode = true;
            this.d20.TabIndex = 40;
            this.d20.Text = "uiLabel41";
            this.d20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d20.Click += new System.EventHandler(this.d1_Click);
            // 
            // d13
            // 
            this.d13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d13.Location = new System.Drawing.Point(204, 53);
            this.d13.Name = "d13";
            this.d13.Size = new System.Drawing.Size(36, 19);
            this.d13.Style = Sunny.UI.UIStyle.Custom;
            this.d13.StyleCustomMode = true;
            this.d13.TabIndex = 39;
            this.d13.Text = "uiLabel42";
            this.d13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d13.Click += new System.EventHandler(this.d1_Click);
            // 
            // d6
            // 
            this.d6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d6.Location = new System.Drawing.Point(204, 30);
            this.d6.Name = "d6";
            this.d6.Size = new System.Drawing.Size(36, 19);
            this.d6.Style = Sunny.UI.UIStyle.Custom;
            this.d6.StyleCustomMode = true;
            this.d6.TabIndex = 38;
            this.d6.Text = "uiLabel43";
            this.d6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d6.Click += new System.EventHandler(this.d1_Click);
            // 
            // d40
            // 
            this.d40.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d40.Location = new System.Drawing.Point(164, 145);
            this.d40.Name = "d40";
            this.d40.Size = new System.Drawing.Size(36, 19);
            this.d40.Style = Sunny.UI.UIStyle.Custom;
            this.d40.StyleCustomMode = true;
            this.d40.TabIndex = 37;
            this.d40.Text = "uiLabel32";
            this.d40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d40.Click += new System.EventHandler(this.d1_Click);
            // 
            // d33
            // 
            this.d33.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d33.Location = new System.Drawing.Point(164, 122);
            this.d33.Name = "d33";
            this.d33.Size = new System.Drawing.Size(36, 19);
            this.d33.Style = Sunny.UI.UIStyle.Custom;
            this.d33.StyleCustomMode = true;
            this.d33.TabIndex = 36;
            this.d33.Text = "uiLabel33";
            this.d33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d33.Click += new System.EventHandler(this.d1_Click);
            // 
            // d26
            // 
            this.d26.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d26.Location = new System.Drawing.Point(164, 99);
            this.d26.Name = "d26";
            this.d26.Size = new System.Drawing.Size(36, 19);
            this.d26.Style = Sunny.UI.UIStyle.Custom;
            this.d26.StyleCustomMode = true;
            this.d26.TabIndex = 35;
            this.d26.Text = "uiLabel34";
            this.d26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d26.Click += new System.EventHandler(this.d1_Click);
            // 
            // d19
            // 
            this.d19.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d19.Location = new System.Drawing.Point(164, 76);
            this.d19.Name = "d19";
            this.d19.Size = new System.Drawing.Size(36, 19);
            this.d19.Style = Sunny.UI.UIStyle.Custom;
            this.d19.StyleCustomMode = true;
            this.d19.TabIndex = 34;
            this.d19.Text = "uiLabel35";
            this.d19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d19.Click += new System.EventHandler(this.d1_Click);
            // 
            // d12
            // 
            this.d12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d12.Location = new System.Drawing.Point(164, 53);
            this.d12.Name = "d12";
            this.d12.Size = new System.Drawing.Size(36, 19);
            this.d12.Style = Sunny.UI.UIStyle.Custom;
            this.d12.StyleCustomMode = true;
            this.d12.TabIndex = 33;
            this.d12.Text = "uiLabel36";
            this.d12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d12.Click += new System.EventHandler(this.d1_Click);
            // 
            // d5
            // 
            this.d5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d5.Location = new System.Drawing.Point(164, 30);
            this.d5.Name = "d5";
            this.d5.Size = new System.Drawing.Size(36, 19);
            this.d5.Style = Sunny.UI.UIStyle.Custom;
            this.d5.StyleCustomMode = true;
            this.d5.TabIndex = 32;
            this.d5.Text = "uiLabel37";
            this.d5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d5.Click += new System.EventHandler(this.d1_Click);
            // 
            // d39
            // 
            this.d39.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d39.Location = new System.Drawing.Point(124, 145);
            this.d39.Name = "d39";
            this.d39.Size = new System.Drawing.Size(36, 19);
            this.d39.Style = Sunny.UI.UIStyle.Custom;
            this.d39.StyleCustomMode = true;
            this.d39.TabIndex = 31;
            this.d39.Text = "uiLabel26";
            this.d39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d39.Click += new System.EventHandler(this.d1_Click);
            // 
            // d32
            // 
            this.d32.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d32.Location = new System.Drawing.Point(124, 122);
            this.d32.Name = "d32";
            this.d32.Size = new System.Drawing.Size(36, 19);
            this.d32.Style = Sunny.UI.UIStyle.Custom;
            this.d32.StyleCustomMode = true;
            this.d32.TabIndex = 30;
            this.d32.Text = "uiLabel27";
            this.d32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d32.Click += new System.EventHandler(this.d1_Click);
            // 
            // d25
            // 
            this.d25.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d25.Location = new System.Drawing.Point(124, 99);
            this.d25.Name = "d25";
            this.d25.Size = new System.Drawing.Size(36, 19);
            this.d25.Style = Sunny.UI.UIStyle.Custom;
            this.d25.StyleCustomMode = true;
            this.d25.TabIndex = 29;
            this.d25.Text = "uiLabel28";
            this.d25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d25.Click += new System.EventHandler(this.d1_Click);
            // 
            // d18
            // 
            this.d18.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d18.Location = new System.Drawing.Point(124, 76);
            this.d18.Name = "d18";
            this.d18.Size = new System.Drawing.Size(36, 19);
            this.d18.Style = Sunny.UI.UIStyle.Custom;
            this.d18.StyleCustomMode = true;
            this.d18.TabIndex = 28;
            this.d18.Text = "uiLabel29";
            this.d18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d18.Click += new System.EventHandler(this.d1_Click);
            // 
            // d11
            // 
            this.d11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d11.Location = new System.Drawing.Point(124, 53);
            this.d11.Name = "d11";
            this.d11.Size = new System.Drawing.Size(36, 19);
            this.d11.Style = Sunny.UI.UIStyle.Custom;
            this.d11.StyleCustomMode = true;
            this.d11.TabIndex = 27;
            this.d11.Text = "uiLabel30";
            this.d11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d11.Click += new System.EventHandler(this.d1_Click);
            // 
            // d4
            // 
            this.d4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d4.Location = new System.Drawing.Point(124, 30);
            this.d4.Name = "d4";
            this.d4.Size = new System.Drawing.Size(36, 19);
            this.d4.Style = Sunny.UI.UIStyle.Custom;
            this.d4.StyleCustomMode = true;
            this.d4.TabIndex = 26;
            this.d4.Text = "uiLabel31";
            this.d4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d4.Click += new System.EventHandler(this.d1_Click);
            // 
            // d38
            // 
            this.d38.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d38.Location = new System.Drawing.Point(84, 145);
            this.d38.Name = "d38";
            this.d38.Size = new System.Drawing.Size(36, 19);
            this.d38.Style = Sunny.UI.UIStyle.Custom;
            this.d38.StyleCustomMode = true;
            this.d38.TabIndex = 25;
            this.d38.Text = "uiLabel20";
            this.d38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d38.Click += new System.EventHandler(this.d1_Click);
            // 
            // d31
            // 
            this.d31.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d31.Location = new System.Drawing.Point(84, 122);
            this.d31.Name = "d31";
            this.d31.Size = new System.Drawing.Size(36, 19);
            this.d31.Style = Sunny.UI.UIStyle.Custom;
            this.d31.StyleCustomMode = true;
            this.d31.TabIndex = 24;
            this.d31.Text = "uiLabel21";
            this.d31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d31.Click += new System.EventHandler(this.d1_Click);
            // 
            // d24
            // 
            this.d24.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d24.Location = new System.Drawing.Point(84, 99);
            this.d24.Name = "d24";
            this.d24.Size = new System.Drawing.Size(36, 19);
            this.d24.Style = Sunny.UI.UIStyle.Custom;
            this.d24.StyleCustomMode = true;
            this.d24.TabIndex = 23;
            this.d24.Text = "uiLabel22";
            this.d24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d24.Click += new System.EventHandler(this.d1_Click);
            // 
            // d17
            // 
            this.d17.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d17.Location = new System.Drawing.Point(84, 76);
            this.d17.Name = "d17";
            this.d17.Size = new System.Drawing.Size(36, 19);
            this.d17.Style = Sunny.UI.UIStyle.Custom;
            this.d17.StyleCustomMode = true;
            this.d17.TabIndex = 22;
            this.d17.Text = "uiLabel23";
            this.d17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d17.Click += new System.EventHandler(this.d1_Click);
            // 
            // d10
            // 
            this.d10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d10.Location = new System.Drawing.Point(84, 53);
            this.d10.Name = "d10";
            this.d10.Size = new System.Drawing.Size(36, 19);
            this.d10.Style = Sunny.UI.UIStyle.Custom;
            this.d10.StyleCustomMode = true;
            this.d10.TabIndex = 21;
            this.d10.Text = "uiLabel24";
            this.d10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d10.Click += new System.EventHandler(this.d1_Click);
            // 
            // d3
            // 
            this.d3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d3.Location = new System.Drawing.Point(84, 30);
            this.d3.Name = "d3";
            this.d3.Size = new System.Drawing.Size(36, 19);
            this.d3.Style = Sunny.UI.UIStyle.Custom;
            this.d3.StyleCustomMode = true;
            this.d3.TabIndex = 20;
            this.d3.Text = "uiLabel25";
            this.d3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d3.Click += new System.EventHandler(this.d1_Click);
            // 
            // d37
            // 
            this.d37.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d37.Location = new System.Drawing.Point(44, 145);
            this.d37.Name = "d37";
            this.d37.Size = new System.Drawing.Size(36, 19);
            this.d37.Style = Sunny.UI.UIStyle.Custom;
            this.d37.StyleCustomMode = true;
            this.d37.TabIndex = 19;
            this.d37.Text = "uiLabel14";
            this.d37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d37.Click += new System.EventHandler(this.d1_Click);
            // 
            // d30
            // 
            this.d30.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d30.Location = new System.Drawing.Point(44, 122);
            this.d30.Name = "d30";
            this.d30.Size = new System.Drawing.Size(36, 19);
            this.d30.Style = Sunny.UI.UIStyle.Custom;
            this.d30.StyleCustomMode = true;
            this.d30.TabIndex = 18;
            this.d30.Text = "uiLabel15";
            this.d30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d30.Click += new System.EventHandler(this.d1_Click);
            // 
            // d23
            // 
            this.d23.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d23.Location = new System.Drawing.Point(44, 99);
            this.d23.Name = "d23";
            this.d23.Size = new System.Drawing.Size(36, 19);
            this.d23.Style = Sunny.UI.UIStyle.Custom;
            this.d23.StyleCustomMode = true;
            this.d23.TabIndex = 17;
            this.d23.Text = "uiLabel16";
            this.d23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d23.Click += new System.EventHandler(this.d1_Click);
            // 
            // d16
            // 
            this.d16.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d16.Location = new System.Drawing.Point(44, 76);
            this.d16.Name = "d16";
            this.d16.Size = new System.Drawing.Size(36, 19);
            this.d16.Style = Sunny.UI.UIStyle.Custom;
            this.d16.StyleCustomMode = true;
            this.d16.TabIndex = 16;
            this.d16.Text = "uiLabel17";
            this.d16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d16.Click += new System.EventHandler(this.d1_Click);
            // 
            // d9
            // 
            this.d9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d9.Location = new System.Drawing.Point(44, 53);
            this.d9.Name = "d9";
            this.d9.Size = new System.Drawing.Size(36, 19);
            this.d9.Style = Sunny.UI.UIStyle.Custom;
            this.d9.StyleCustomMode = true;
            this.d9.TabIndex = 15;
            this.d9.Text = "uiLabel18";
            this.d9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d9.Click += new System.EventHandler(this.d1_Click);
            // 
            // d2
            // 
            this.d2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d2.Location = new System.Drawing.Point(44, 30);
            this.d2.Name = "d2";
            this.d2.Size = new System.Drawing.Size(36, 19);
            this.d2.Style = Sunny.UI.UIStyle.Custom;
            this.d2.StyleCustomMode = true;
            this.d2.TabIndex = 14;
            this.d2.Text = "uiLabel19";
            this.d2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d2.Click += new System.EventHandler(this.d1_Click);
            // 
            // uiLabel13
            // 
            this.uiLabel13.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel13.Location = new System.Drawing.Point(244, 4);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(36, 19);
            this.uiLabel13.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel13.TabIndex = 13;
            this.uiLabel13.Text = "六";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel12
            // 
            this.uiLabel12.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel12.Location = new System.Drawing.Point(204, 4);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(36, 19);
            this.uiLabel12.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel12.TabIndex = 12;
            this.uiLabel12.Text = "五";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel11
            // 
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel11.Location = new System.Drawing.Point(164, 4);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(36, 19);
            this.uiLabel11.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel11.TabIndex = 11;
            this.uiLabel11.Text = "四";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel10
            // 
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel10.Location = new System.Drawing.Point(124, 4);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(36, 19);
            this.uiLabel10.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel10.TabIndex = 10;
            this.uiLabel10.Text = "三";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel9
            // 
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel9.Location = new System.Drawing.Point(84, 4);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(36, 19);
            this.uiLabel9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel9.TabIndex = 9;
            this.uiLabel9.Text = "二";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(44, 4);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(36, 19);
            this.uiLabel7.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel7.TabIndex = 8;
            this.uiLabel7.Text = "一";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // d36
            // 
            this.d36.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d36.Location = new System.Drawing.Point(4, 145);
            this.d36.Name = "d36";
            this.d36.Size = new System.Drawing.Size(36, 19);
            this.d36.Style = Sunny.UI.UIStyle.Custom;
            this.d36.StyleCustomMode = true;
            this.d36.TabIndex = 6;
            this.d36.Text = "uiLabel8";
            this.d36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d36.Click += new System.EventHandler(this.d1_Click);
            // 
            // d29
            // 
            this.d29.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d29.Location = new System.Drawing.Point(4, 122);
            this.d29.Name = "d29";
            this.d29.Size = new System.Drawing.Size(36, 19);
            this.d29.Style = Sunny.UI.UIStyle.Custom;
            this.d29.StyleCustomMode = true;
            this.d29.TabIndex = 5;
            this.d29.Text = "uiLabel5";
            this.d29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d29.Click += new System.EventHandler(this.d1_Click);
            // 
            // d22
            // 
            this.d22.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d22.Location = new System.Drawing.Point(4, 99);
            this.d22.Name = "d22";
            this.d22.Size = new System.Drawing.Size(36, 19);
            this.d22.Style = Sunny.UI.UIStyle.Custom;
            this.d22.StyleCustomMode = true;
            this.d22.TabIndex = 4;
            this.d22.Text = "uiLabel6";
            this.d22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d22.Click += new System.EventHandler(this.d1_Click);
            // 
            // d15
            // 
            this.d15.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d15.Location = new System.Drawing.Point(4, 76);
            this.d15.Name = "d15";
            this.d15.Size = new System.Drawing.Size(36, 19);
            this.d15.Style = Sunny.UI.UIStyle.Custom;
            this.d15.StyleCustomMode = true;
            this.d15.TabIndex = 3;
            this.d15.Text = "uiLabel3";
            this.d15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d15.Click += new System.EventHandler(this.d1_Click);
            // 
            // d8
            // 
            this.d8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d8.Location = new System.Drawing.Point(4, 53);
            this.d8.Name = "d8";
            this.d8.Size = new System.Drawing.Size(36, 19);
            this.d8.Style = Sunny.UI.UIStyle.Custom;
            this.d8.StyleCustomMode = true;
            this.d8.TabIndex = 2;
            this.d8.Text = "uiLabel4";
            this.d8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d8.Click += new System.EventHandler(this.d1_Click);
            // 
            // d1
            // 
            this.d1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.d1.Location = new System.Drawing.Point(4, 30);
            this.d1.Name = "d1";
            this.d1.Size = new System.Drawing.Size(36, 19);
            this.d1.Style = Sunny.UI.UIStyle.Custom;
            this.d1.StyleCustomMode = true;
            this.d1.TabIndex = 1;
            this.d1.Text = "uiLabel2";
            this.d1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.d1.Click += new System.EventHandler(this.d1_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(4, 4);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(36, 19);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "日";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLine1
            // 
            this.uiLine1.FillColor = System.Drawing.Color.White;
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.LineColor = System.Drawing.Color.Silver;
            this.uiLine1.Location = new System.Drawing.Point(8, 19);
            this.uiLine1.MinimumSize = new System.Drawing.Size(16, 16);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(268, 16);
            this.uiLine1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine1.StyleCustomMode = true;
            this.uiLine1.TabIndex = 7;
            // 
            // UIDateItem
            // 
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.TopPanel);
            this.FillColor = System.Drawing.Color.White;
            this.Name = "UIDateItem";
            this.Size = new System.Drawing.Size(461, 348);
            this.Style = Sunny.UI.UIStyle.Custom;
            this.TopPanel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.p1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.p2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.p3.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        public UIDateItem()
        {
            InitializeComponent();
            Width = 284;
            Height = 200;
            TabControl.TabVisible = false;

            for (int i = 1; i <= 10; i++)
            {
                var label = this.GetControl<UILabel>("y" + i);
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;
            }

            for (int i = 1; i <= 12; i++)
            {
                var label = this.GetControl<UILabel>("m" + i);
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;
            }

            for (int i = 1; i <= 42; i++)
            {
                var label = this.GetControl<UILabel>("d" + i);
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            UILabel label = (UILabel)sender;
            if (label.Tag.ToString().Length <= 2)
            {
                label.ForeColor = UIFontColor.Primary;
            }
            else
            {
                DateTime lblDate = (DateTime)label.Tag;
                label.ForeColor = (lblDate.Year == Year && lblDate.Month == Month)
                    ? UIFontColor.Primary
                    : Color.DarkGray;

                if (lblDate.DateString().Equals(date.DateString()))
                {
                    label.ForeColor = UIColor.Blue;
                }
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            UILabel label = (UILabel)sender;
            label.ForeColor = UIColor.Blue;
        }

        private void TopPanel_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = tabPage1;
        }

        private void d1_Click(object sender, EventArgs e)
        {
            date = (DateTime)((UILabel)sender).Tag;
            DoValueChanged(this, Date);
            CloseParent();
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
                for (int i = 1; i <= 10; i++)
                {
                    UILabel label = tabPage1.GetControl<UILabel>("y" + i);
                    label.Text = (iy + i - 1).ToString();
                }
            }
        }

        public int Month { get; set; }

        private void SetYearMonth(int iYear, int iMonth)
        {
            DateTime dt = new DateTime(iYear, iMonth, 1);
            int week = (int)dt.DayOfWeek;

            DateTime dtBegin = week == 0 ? dt.AddDays(-7) : dt.AddDays(-week);
            for (int i = 1; i <= 42; i++)
            {
                UILabel label = tabPage3.GetControl<UILabel>("d" + i);
                DateTime lblDate = dtBegin.AddDays(i - 1);
                label.Text = lblDate.Day.ToString();
                label.Tag = lblDate;
                label.ForeColor = (lblDate.Year == iYear && lblDate.Month == iMonth)
                    ? UIFontColor.Primary
                    : Color.DarkGray;

                if (lblDate.DateString().Equals(date.DateString()))
                {
                    label.ForeColor = UIColor.Blue;
                }
            }

            TopPanel.Text = Year + "年" + Month + "月";
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            b2.Visible = b3.Visible = TabControl.SelectedIndex == 2;
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    int iy = year / 10 * 10;
                    TopPanel.Text = iy + "年 - " + (iy + 9) + "年";
                    for (int i = 1; i <= 10; i++)
                    {
                        UILabel label = tabPage1.GetControl<UILabel>("y" + i);
                        label.Text = (iy + i - 1).ToString();
                    }
                    break;

                case 1:
                    TopPanel.Text = Year + "年";
                    break;

                case 2:
                    TopPanel.Text = Year + "年" + Month + "月";
                    break;
            }
        }

        private void y1_Click(object sender, EventArgs e)
        {
            Year = ((UILabel)sender).Text.ToInt();
            TabControl.SelectedTab = tabPage2;
        }

        private void m1_Click(object sender, EventArgs e)
        {
            Month = ((UILabel)sender).Tag.ToString().ToInt();
            SetYearMonth(Year, Month);
            TabControl.SelectedTab = tabPage3;
        }

        private void b1_Click(object sender, EventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Year = year / 10 * 10;
                    Year -= 10;
                    TopPanel.Text = Year + "年 - " + (Year + 9) + "年";
                    for (int i = 1; i <= 10; i++)
                    {
                        UILabel label = tabPage1.GetControl<UILabel>("y" + i);
                        label.Text = (Year + i - 1).ToString();
                    }
                    break;

                case 1:
                    Year -= 1;
                    TopPanel.Text = Year + "年";
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
                    Year += 10;
                    TopPanel.Text = Year + "年 - " + (Year + 9) + "年";
                    for (int i = 1; i <= 10; i++)
                    {
                        UILabel label = tabPage1.GetControl<UILabel>("y" + i);
                        label.Text = (Year + i - 1).ToString();
                    }
                    break;

                case 1:
                    Year += 1;
                    TopPanel.Text = Year + "年";
                    break;

                case 2:
                    Year += 1;
                    SetYearMonth(Year, Month);
                    break;
            }
        }

        public override void SetRectColor(Color color)
        {
            base.SetRectColor(color);
            RectColor = color;
            b1.ForeColor = b2.ForeColor = b3.ForeColor = b4.ForeColor = color;
            TopPanel.RectColor = p1.RectColor = p2.RectColor = p3.RectColor = color;
        }
    }
}