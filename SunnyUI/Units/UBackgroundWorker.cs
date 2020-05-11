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
 * 文件名称: UBackgroundWorker.cs
 * 文件说明: 多线程运行扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Threading;

namespace Sunny.UI
{
    /// <summary>
    /// 多线程运行扩展类
    /// </summary>
    public abstract class BackgroundWorkerEx
    {
        private readonly BackgroundWorker _worker;

        /// <summary>
        /// 是否只运行一次
        /// </summary>
        public bool RunOnce { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BackgroundWorkerEx()
        {
            _worker = new BackgroundWorker { WorkerSupportsCancellation = true };
            _worker.DoWork += WorkerDoWork;
            _worker.RunWorkerCompleted += RunWorkerCompleted;
        }

        /// <summary>
        /// 是否运行
        /// </summary>
        public bool IsStart { get; private set; }

        /// <summary>
        /// 每次运行时间间隔
        /// </summary>
        public uint WorkerDelay { get; set; } = 100;

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DoCompleted();
            OnCompleted?.Invoke(sender, e);
        }

        /// <summary>
        /// 运行结束调用事件
        /// </summary>
        public event EventHandler OnCompleted;

        /// <summary>
        /// 运行事件，需重载
        /// </summary>
        protected abstract void DoWorker();

        /// <summary>
        /// 运行结束
        /// </summary>
        protected virtual void DoCompleted()
        {
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~BackgroundWorkerEx()
        {
            BeforeStop();
            FinalStop();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (RunOnce)
            {
                DoOnce();
            }
            else
            {
                while (IsStart)
                {
                    //如果用户申请取消
                    if (_worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    DoWorker();
                    Thread.Sleep((int)WorkerDelay);
                    Thread.Yield();
                }
            }
        }

        private void DoOnce()
        {
            DoWorker();
            IsStart = false;
            BeforeStop();
            FinalStop();
        }

        /// <summary>
        /// 运行前调用
        /// </summary>
        protected virtual void InitStart()
        {
        }

        /// <summary>
        /// 停止后调用
        /// </summary>
        protected virtual void FinalStop()
        {
        }

        /// <summary>
        /// 停止前调用
        /// </summary>
        protected virtual void BeforeStop()
        {
        }

        /// <summary>
        /// 开始运行线程
        /// </summary>
        public void Start()
        {
            if (!IsStart)
            {
                InitStart();
                IsStart = true;
                _worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 结束运行线程
        /// </summary>
        public void Stop()
        {
            if (IsStart)
            {
                BeforeStop();
                IsStart = false;
                _worker.CancelAsync();
                FinalStop();
            }
        }
    }
}