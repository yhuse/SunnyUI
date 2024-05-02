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
 * 文件名称: UIMillisecondTimer.cs
 * 文件说明: 毫秒定时器
 * 当前版本: V3.1
 * 创建日期: 2021-08-15
 *
 * 2021-08-15: V3.0.6 增加文件说明
******************************************************************************/

using Sunny.UI.Win32;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    [DefaultEvent("Tick")]
    [DefaultProperty("Interval")]
    public class UIMillisecondTimer : Component
    {
        public event EventHandler Tick;

        /// <devdoc>
        /// <para>Initializes a new instance of the UIMillisecondTimer. />
        /// class.</para>
        /// </devdoc>
        public UIMillisecondTimer()
        {
            int result = WinMM.timeGetDevCaps(ref TimeCaps, Marshal.SizeOf(TimeCaps));
            if (result != WinMM.TIMERR_NOERROR)
            {
                throw new Exception("毫秒定时器初始化失败");
            }

            Version = UIGlobal.Version;
            interval = 50;
            SetEventCallback = DoSetEventCallback;
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the UIMillisecondTimer class with the specified container.</para>
        /// </devdoc>
        public UIMillisecondTimer(IContainer container) : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Add(this);
        }

        public static bool CanUse()
        {
            TIMECAPS timeCaps = new TIMECAPS();
            int result = WinMM.timeGetDevCaps(ref timeCaps, Marshal.SizeOf(timeCaps));
            return result != WinMM.TIMERR_NOERROR;
        }

        protected override void Dispose(bool disposing)
        {
            Stop();
            base.Dispose(disposing);
        }

        private void DoSetEventCallback(int uTimerID, uint uMsg, uint dwUser, UIntPtr dw1, UIntPtr dw2)
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }

        [
            Localizable(false),
            Bindable(true),
            DefaultValue(null),
            TypeConverter(typeof(StringConverter))
        ]
        public object Tag { get; set; }

        [DefaultValue(null)]
        public string TagString { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; }

        private readonly TIMECAPS TimeCaps;

        private int interval;

        /// <devdoc>
        ///    <para>Occurs when the specified timer interval has elapsed and the timer is enabled.</para>
        /// </devdoc>
        [DefaultValue(50)]
        public int Interval
        {
            get => interval;
            set
            {
                if (interval == value || value < TimeCaps.wPeriodMin || value > TimeCaps.wPeriodMax)
                    return;

                interval = value;

                if (Enabled)
                {
                    ReStart();
                }
            }
        }

        private bool enabled;

        /// <devdoc>
        ///    <para> Indicates whether the timer is running.</para>
        /// </devdoc>
        [DefaultValue(false)]
        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value) return;

                if (!enabled)
                {
                    int result = WinMM.timeSetEvent(interval, Math.Min(1, TimeCaps.wPeriodMin), SetEventCallback, 0, WinMM.TIME_MS);
                    if (result == 0)
                    {
                        throw new Exception("毫秒定时器启动失败");
                    }

                    TimerID = result;
                }
                else
                {
                    if (TimerID > 0)
                    {
                        WinMM.timeKillEvent(TimerID);
                        TimerID = 0;
                    }
                }

                enabled = value;
            }
        }

        private readonly WinMM.TimerSetEventCallback SetEventCallback;
        private int TimerID;

        /// <summary>
        /// 开启定时器
        /// </summary>
        public void Start()
        {
            Enabled = true;
        }

        /// <summary>
        /// 重启定时器
        /// </summary>
        public void ReStart()
        {
            Enabled = false;
            Enabled = true;
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void Stop()
        {
            Enabled = false;
        }

    }
}
