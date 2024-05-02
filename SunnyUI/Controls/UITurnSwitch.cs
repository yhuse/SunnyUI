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
 * 文件名称: UITurnSwitch.cs
 * 文件说明: 旋转开关
 * 当前版本: V3.3
 * 创建日期: 2023-07-05
 *
 * 2023-07-05: V3.3.9 增加文件说明
 * 2023-07-06: V3.3.9 调整配色，增加自定义角度
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Active")]
    [ToolboxItem(true)]
    public class UITurnSwitch : UIControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="value">开关值</param>
        public delegate void OnValueChanged(object sender, bool value);

        public UITurnSwitch()
        {
            SetStyleFlags();
            Height = 160;
            Width = 160;
            ShowText = false;
            ShowRect = false;
        }

        [DefaultValue(false)]
        [Description("是否只读"), Category("SunnyUI")]
        public bool ReadOnly { get; set; }

        public event OnValueChanged ValueChanged;

        public event EventHandler ActiveChanged;

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get => foreColor;
            set => SetForeColor(value);
        }

        private bool activeValue;

        [DefaultValue(false)]
        [Description("是否打开"), Category("SunnyUI")]
        public bool Active
        {
            get => activeValue;
            set
            {
                if (!ReadOnly && activeValue != value)
                {
                    activeValue = value;
                    ValueChanged?.Invoke(this, value);
                    ActiveChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        private string activeText = "开";

        [DefaultValue("开")]
        [Description("打开文字"), Category("SunnyUI")]
        public string ActiveText
        {
            get => activeText;
            set
            {
                activeText = value;
                Invalidate();
            }
        }

        private string inActiveText = "关";

        [DefaultValue("关")]
        [Description("关闭文字"), Category("SunnyUI")]
        public string InActiveText
        {
            get => inActiveText;
            set
            {
                inActiveText = value;
                Invalidate();
            }
        }

        private Color inActiveColor = Color.Red;

        [DefaultValue(typeof(Color), "Red")]
        [Description("关闭颜色"), Category("SunnyUI")]
        public Color InActiveColor
        {
            get => inActiveColor;
            set
            {
                inActiveColor = value;
                Invalidate();
            }
        }

        private Color activeColor = Color.Lime;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("打开颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Lime")]
        public Color ActiveColor
        {
            get => activeColor;
            set
            {
                activeColor = value;
                Invalidate();
            }
        }

        private int inActiveAngle = 315;

        [DefaultValue(315)]
        [Description("开关关闭角度"), Category("SunnyUI")]
        public int InActiveAngle
        {
            get => inActiveAngle;
            set
            {
                inActiveAngle = value;
                Invalidate();
            }
        }

        private int activeAngle = 45;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("开关打开角度"), Category("SunnyUI")]
        [DefaultValue(45)]
        public int ActiveAngle
        {
            get => activeAngle;
            set
            {
                activeAngle = value;
                Invalidate();
            }
        }

        private Color backColor = Color.Silver;

        /// <summary>
        /// 填充颜色
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "Silver")]
        public Color FillColor
        {
            get => backColor;
            set
            {
                backColor = value;
                Invalidate();
            }
        }

        private Color handleColor = Color.DarkGray;

        /// <summary>
        /// 按钮把手颜色
        /// </summary>
        [Description("按钮把手颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color HandleColor
        {
            get => handleColor;
            set
            {
                handleColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="e">参数</param>
        protected override void OnClick(EventArgs e)
        {
            ActiveChange();
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (!UseDoubleClick)
            {
                ActiveChange();
                base.OnClick(e);
            }
            else
            {
                base.OnDoubleClick(e);
            }
        }

        public event OnCancelEventArgs ActiveChanging;

        private void ActiveChange()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (ActiveChanging != null)
            {
                ActiveChanging?.Invoke(this, e);
            }

            if (!e.Cancel)
            {
                Active = !Active;
            }
        }

        private int backSize = 100;

        [Description("开关尺寸"), Category("SunnyUI")]
        [DefaultValue(100)]
        public int BackSize
        {
            get => backSize;
            set
            {
                backSize = value;
                Invalidate();
            }
        }

        private int backInnerSize = 80;

        [Description("开关内圈尺寸"), Category("SunnyUI")]
        [DefaultValue(80)]
        public int BackInnerSize
        {
            get => backInnerSize;
            set
            {
                backInnerSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 绘制填充颜色
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="path">绘图路径</param>
        protected override void OnPaintFill(Graphics g, GraphicsPath path)
        {
            Color color = Active ? ActiveColor : InActiveColor;
            if (!Enabled) color = rectDisableColor;

            Point center = new Point(Width / 2, Height / 2);
            g.FillEllipse(rectColor, new Rectangle(center.X - BackSize / 2, center.Y - BackSize / 2, BackSize, BackSize));
            int size = backSize - 10;
            g.FillEllipse(Color.White, new Rectangle(center.X - size / 2, center.Y - size / 2, size, size));
            g.FillEllipse(FillColor, new Rectangle(center.X - backInnerSize / 2, center.Y - backInnerSize / 2, backInnerSize, backInnerSize));

            int size2 = 6;
            using Pen pn = rectColor.Pen(2);
            PointF pt;
            if (Active)
            {
                PointF[] points = GetHandles(ActiveAngle);
                g.FillPolygon(HandleColor, points);
                g.DrawPolygon(pn, points);
                pt = center.CalcAzRangePoint(BackSize / 2 - size2, ActiveAngle);
            }
            else
            {
                PointF[] points = GetHandles(InActiveAngle);
                g.FillPolygon(HandleColor, points);
                g.DrawPolygon(pn, points);
                pt = center.CalcAzRangePoint(BackSize / 2 - size2, InActiveAngle);
            }

            g.FillEllipse(color, pt.X - size2, pt.Y - size2, size2 * 2, size2 * 2);
            Size sz = TextRenderer.MeasureText(ActiveText, Font);
            pt = center.CalcAzRangePoint(BackSize / 2 + size2 + 4 + sz.Width / 2, ActiveAngle);
            g.DrawString(ActiveText, Font, ActiveColor, new Rectangle((int)(pt.X - sz.Width / 2), (int)(pt.Y - sz.Height / 2), sz.Width, sz.Height), ContentAlignment.MiddleCenter);

            sz = TextRenderer.MeasureText(InActiveText, Font);
            pt = center.CalcAzRangePoint(BackSize / 2 + size2 + 4 + sz.Width / 2, InActiveAngle);
            g.DrawString(InActiveText, Font, InActiveColor, new Rectangle((int)(pt.X - sz.Width / 2), (int)(pt.Y - sz.Height / 2), sz.Width, sz.Height), ContentAlignment.MiddleCenter);

        }

        private PointF[] GetHandles(int angle)
        {
            int size1 = 10;
            int size2 = 4;
            Point center = new Point(Width / 2, Height / 2);
            PointF pt1 = center.CalcAzRangePoint(size1, angle - 90);
            PointF pt2 = center.CalcAzRangePoint(size1, angle + 90);
            PointF pt3 = pt1.CalcAzRangePoint(BackSize / 2 + size2, angle);
            PointF pt4 = pt2.CalcAzRangePoint(BackSize / 2 + size2, angle);
            PointF pt5 = pt1.CalcAzRangePoint(BackSize / 2 + size2, angle + 180);
            PointF pt6 = pt2.CalcAzRangePoint(BackSize / 2 + size2, angle + 180);

            PointF pt11 = center.CalcAzRangePoint(size1 - 2, angle - 90);
            PointF pt12 = center.CalcAzRangePoint(size1 - 2, angle + 90);
            PointF pt13 = pt11.CalcAzRangePoint(BackSize / 2 + size2 + 2, angle);
            PointF pt14 = pt12.CalcAzRangePoint(BackSize / 2 + size2 + 2, angle);
            PointF pt15 = pt11.CalcAzRangePoint(BackSize / 2 + size2 + 2, angle + 180);
            PointF pt16 = pt12.CalcAzRangePoint(BackSize / 2 + size2 + 2, angle + 180);

            return new PointF[] { pt3, pt13, pt14, pt4, pt6, pt16, pt15, pt5 };
        }
    }
}
