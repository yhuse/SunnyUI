/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2023 ShenYongHua(沈永华).
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
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            Height = 150;
            Width = 150;
            ShowText = false;
            ShowRect = false;

            inActiveColor = Color.Gray;
            fillColor = Color.White;

            rectColor = UIStyles.Blue.SwitchActiveColor;
            fillColor = UIStyles.Blue.SwitchFillColor;
            inActiveColor = UIStyles.Blue.SwitchInActiveColor;
            rectDisableColor = UIStyles.Blue.SwitchRectDisableColor;
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

        private Color inActiveColor;

        [DefaultValue(typeof(Color), "Gray")]
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

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("填充颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public Color ButtonColor
        {
            get => fillColor;
            set => SetFillColor(value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("打开颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "80, 160, 255")]
        public Color ActiveColor
        {
            get => rectColor;
            set => SetRectColor(value);
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

        /// <summary>
        /// 设置主题样式
        /// </summary>
        /// <param name="uiColor">主题样式</param>
        public override void SetStyleColor(UIBaseStyle uiColor)
        {
            base.SetStyleColor(uiColor);

            rectColor = uiColor.SwitchActiveColor;
            fillColor = uiColor.SwitchFillColor;
            inActiveColor = uiColor.SwitchInActiveColor;
            rectDisableColor = uiColor.SwitchRectDisableColor;
        }

        [Description("不可用颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "173, 178, 181")]
        public Color DisabledColor
        {
            get => rectDisableColor;
            set => SetRectDisableColor(value);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private int backSize = 100;
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
            g.FillEllipse(rectColor, new Rectangle(center.X - backInnerSize / 2, center.Y - backInnerSize / 2, backInnerSize, backInnerSize));

            if (Active)
            {
                int size1 = 10;
                int size2 = 6;
                PointF pt1 = center.CalcAzRangePoint(size1, 45);
                PointF pt2 = center.CalcAzRangePoint(size1, 225);
                PointF pt3 = pt1.CalcAzRangePoint(BackSize / 2 + size2, 315);
                PointF pt4 = pt2.CalcAzRangePoint(BackSize / 2 + size2, 315);
                PointF pt5 = pt1.CalcAzRangePoint(BackSize / 2 + size2, 135);
                PointF pt6 = pt2.CalcAzRangePoint(BackSize / 2 + size2, 135);
                g.FillPolygon(Color.Silver, new PointF[] { pt3, pt4, pt6, pt5 });

                pt1 = center.CalcAzRangePoint(BackSize / 2 - size2, 315);
                g.FillEllipse(Color.Lime, pt1.X - size2, pt1.Y - size2, size2 * 2, size2 * 2);
            }
            else
            {
                int size1 = 10;
                int size2 = 6;
                PointF pt1 = center.CalcAzRangePoint(size1, 135);
                PointF pt2 = center.CalcAzRangePoint(size1, 315);
                PointF pt3 = pt1.CalcAzRangePoint(BackSize / 2 + size2, 45);
                PointF pt4 = pt2.CalcAzRangePoint(BackSize / 2 + size2, 45);
                PointF pt5 = pt1.CalcAzRangePoint(BackSize / 2 + size2, 225);
                PointF pt6 = pt2.CalcAzRangePoint(BackSize / 2 + size2, 225);
                g.FillPolygon(Color.Silver, new PointF[] { pt3, pt4, pt6, pt5 });

                pt1 = center.CalcAzRangePoint(BackSize / 2 - size2, 45);
                g.FillEllipse(Color.Red, pt1.X - size2, pt1.Y - size2, size2 * 2, size2 * 2);
            }
        }
    }
}
