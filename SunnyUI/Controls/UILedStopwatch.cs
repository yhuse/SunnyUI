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
 * 文件名称: UILedStopwatch.cs
 * 文件说明: LED计时器
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;

namespace Sunny.UI
{
    /// <summary>
    /// LED计时器
    /// </summary>
    [DefaultEvent("TimerTick")]
    [DefaultProperty("Text")]
    public sealed class UILedStopwatch : UILedDisplay
    {
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 当定时器启动后，Text变化时触发一次
        /// </summary>
        public event EventHandler TimerTick;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UILedStopwatch()
        {
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        ~UILedStopwatch()
        {
            timer.Stop();
            timer.Dispose();
        }

        public enum TimeShowType
        {
            mmss,
            mmssfff,
            hhmmss
        }

        [DefaultValue(TimeShowType.mmss)]
        [Description("显示方式"), Category("SunnyUI")]
        public TimeShowType ShowType { get; set; } = TimeShowType.mmss;

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan = DateTime.Now - StartTime;
            string text = "";
            switch (ShowType)
            {
                case TimeShowType.mmss:
                    text = TimeSpan.Minutes.ToString("D2") + ":" + TimeSpan.Seconds.ToString("D2");
                    break;

                case TimeShowType.mmssfff:
                    text = TimeSpan.Minutes.ToString("D2") + ":" + TimeSpan.Seconds.ToString("D2") + "." + TimeSpan.Milliseconds.ToString("D3");
                    break;

                case TimeShowType.hhmmss:
                    text = TimeSpan.Hours.ToString("D2") + ":" + TimeSpan.Minutes.ToString("D2") + ":" + TimeSpan.Seconds.ToString("D2");
                    break;
            }

            if (text != Text)
            {
                Text = text;
                TimerTick?.Invoke(this, e);
            }
        }

        /// <summary>
        /// OnCreateControl
        /// </summary>
        protected override void OnCreateControl()
        {
            Text = "00:00";
        }

        /// <summary>
        /// 计时
        /// </summary>
        [Browsable(false)]
        public TimeSpan TimeSpan { get; private set; }

        private DateTime StartTime;

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            Text = "00:00";
            StartTime = DateTime.Now;
            timer.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// 是否开始工作
        /// </summary>
        [Browsable(false)]
        public bool IsWorking => timer.Enabled;

        private bool _active;

        [DefaultValue(false), Description("是否开始工作"), Category("SunnyUI")]
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (_active)
                    Start();
                else
                    Stop();
            }
        }
    }
}