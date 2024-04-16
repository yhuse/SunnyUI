using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIFormService
    {
        protected Thread thread;
        public bool IsRun => thread != null && thread.ThreadState == ThreadState.Running;
    }

    public static class UIFormServiceHelper
    {
        private static UIWaitFormService WaitFormService;
        private static UIProcessIndicatorFormService ProcessFormService;
        private static UIStatusFormService StatusFormService;

        static UIFormServiceHelper()
        {
            WaitFormService = new UIWaitFormService();
            ProcessFormService = new UIProcessIndicatorFormService();
            StatusFormService = new UIStatusFormService();
        }

        /// <summary>
        /// 显示等待提示窗
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="size"></param>
        public static void ShowProcessForm(this Form owner, int size = 200, bool showrect = true)
        {
            if (ProcessFormService.IsRun) return;
            ProcessFormServiceClose = false;
            ProcessFormService.CreateForm(size, showrect);
        }

        internal static bool ProcessFormServiceClose;

        /// <summary>
        /// 隐藏等待提示窗
        /// </summary>
        public static void HideProcessForm(this Form owner)
        {
            ProcessFormServiceClose = true;
        }

        /// <summary>
        /// 显示等待提示窗
        /// </summary>
        /// <param name="desc">描述文字</param>
        public static void ShowWaitForm(this Form owner, string desc = "系统正在处理中，请稍候...")
        {
            if (WaitFormService.IsRun) return;
            WaitFormServiceClose = false;
            WaitFormService.CreateForm(desc);
        }

        internal static bool WaitFormServiceClose;

        /// <summary>
        /// 隐藏等待提示窗
        /// </summary>
        public static void HideWaitForm(this Form owner)
        {
            WaitFormServiceClose = true;
        }

        /// <summary>
        /// 设置等待提示窗描述文字
        /// </summary>
        /// <param name="desc">描述文字</param>
        public static void SetWaitFormDescription(this Form owner, string desc)
        {
            if (!WaitFormService.IsRun) return;
            WaitFormService.SetDescription(desc);
        }

        /// <summary>
        /// 显示进度提示窗
        /// </summary>
        /// <param name="desc">描述文字</param>
        /// <param name="maximum">最大进度值</param>
        /// <param name="decimalCount">显示进度条小数个数</param>
        public static void ShowStatusForm(this Form owner, int maximum = 100, string desc = "系统正在处理中，请稍候...", int decimalCount = 1)
        {
            if (StatusFormService.IsRun) return;
            StatusFormServiceClose = false;
            StatusFormService.CreateForm(maximum, desc, decimalCount);
        }

        internal static bool StatusFormServiceClose;

        /// <summary>
        /// 隐藏进度提示窗
        /// </summary>
        public static void HideStatusForm(this Form owner)
        {
            StatusFormServiceClose = true;
        }

        /// <summary>
        /// 设置进度提示窗步进值加1
        /// </summary>
        public static void SetStatusFormStepIt(this Form owner, int step = 1)
        {
            if (!StatusFormService.IsRun) return;
            StatusFormService.SetFormStepIt(step);
        }

        /// <summary>
        /// 设置进度提示窗描述文字
        /// </summary>
        /// <param name="desc">描述文字</param>
        public static void SetStatusFormDescription(this Form owner, string desc)
        {
            if (!StatusFormService.IsRun) return;
            StatusFormService.SetFormDescription(desc);
        }
    }

    public class UIWaitFormService : UIFormService
    {
        private UIWaitForm form;

        public void CreateForm(string desc)
        {
            thread = new Thread(delegate ()
            {
                form = new UIWaitForm(desc);
                form.ShowInTaskbar = false;
                form.TopMost = true;
                form.Render();
                if (IsRun) Application.Run(form);
            });

            thread.Start();
        }

        public void SetDescription(string desc)
        {
            try
            {
                form?.SetDescription(desc);
            }
            catch
            {
            }
        }
    }

    public class UIProcessIndicatorFormService : UIFormService
    {
        private UIProcessIndicatorForm form;

        public void CreateForm(int size = 200, bool showRect = true)
        {
            thread = new Thread(delegate ()
            {
                form = new UIProcessIndicatorForm();
                form.ShowRect = showRect;
                form.Size = new System.Drawing.Size(size, size);
                form.ShowInTaskbar = false;
                form.TopMost = true;
                form.Render();
                Application.Run(form);
            });

            thread.Start();
        }
    }

    public class UIStatusFormService : UIFormService
    {
        private UIStatusForm form;

        public void CreateForm(int max, string desc, int decimalCount = 1)
        {
            thread = new Thread(delegate ()
            {
                form = new UIStatusForm(max, desc, decimalCount);
                form.ShowInTaskbar = false;
                form.TopMost = true;
                form.Render();
                Application.Run(form);
            });

            thread.Start();
        }

        public void SetFormDescription(string desc)
        {
            try
            {
                form?.SetDescription(desc);
            }
            catch
            {
            }
        }

        public void SetFormStepIt(int step = 1)
        {
            try
            {
                form?.StepIt(step);
            }
            catch
            {
            }
        }
    }
}