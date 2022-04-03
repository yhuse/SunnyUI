using System;
using System.Threading;
using System.Windows.Forms;

namespace Sunny.UI
{
    public class UIWaitFormService
    {
        private static bool IsRun;
        public static void ShowWaitForm(string desc = "系统正在处理中，请稍候...")
        {
            if (IsRun) return;
            IsRun = true;
            Instance.CreateForm(desc);
        }

        public static void HideWaitForm()
        {
            if (!IsRun) return;
            Instance.CloseForm();
            IsRun = false;
        }

        public static void SetDescription(string desc)
        {
            if (!IsRun) return;
            Instance.SetFormDescription(desc);
        }

        private static UIWaitFormService _instance;
        private static readonly object syncLock = new object();

        private static UIWaitFormService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        _instance = new UIWaitFormService();
                    }
                }

                return _instance;
            }
        }

        private Thread thread;
        private UIWaitForm form;

        private void CreateForm(string desc)
        {
            CloseForm();
            thread = new Thread(delegate ()
            {
                form = new UIWaitForm(desc);
                form.Render();
                if (IsRun) Application.Run(form);
            });

            if (IsRun)
                thread.Start();
            else
                CloseForm();
        }

        private void CloseForm()
        {
            if (form != null) form.NeedClose = true;
        }

        private void SetFormDescription(string desc)
        {
            try
            {
                form?.SetDescription(desc);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    public class UIStatusFormService
    {
        private static bool IsRun;
        public static void ShowStatusForm(int maximum = 100, string desc = "系统正在处理中，请稍候...", int decimalCount = 1)
        {
            if (IsRun) return;
            Instance.CreateForm(maximum, desc, decimalCount);
            IsRun = true;
        }

        public static void HideStatusForm()
        {
            if (!IsRun) return;
            Instance.CloseForm();
            IsRun = false;
        }

        public static void SetDescription(string desc)
        {
            if (!IsRun) return;
            Instance.SetFormDescription(desc);
        }

        public static void StepIt()
        {
            if (!IsRun) return;
            Instance.SetFormStepIt();
        }

        private static UIStatusFormService _instance;
        private static readonly object syncLock = new object();

        private static UIStatusFormService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        _instance = new UIStatusFormService();
                    }
                }

                return _instance;
            }
        }

        private Thread thread;
        private UIStatusForm form;

        private void CreateForm(int max, string desc, int decimalCount = 1)
        {
            CloseForm();
            thread = new Thread(delegate ()
            {
                form = new UIStatusForm(max, desc, decimalCount);
                form.Render();
                form.VisibleChanged += WaitForm_VisibleChanged;
                Application.Run(form);
                IsRun = false;
            });

            thread.Start();
        }

        private void WaitForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!form.Visible)
            {
                form.Close();
            }
        }

        private void CloseForm()
        {
            form?.StepIt(form.Maximum);
        }

        private void SetFormDescription(string desc)
        {
            try
            {
                form?.SetDescription(desc);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SetFormStepIt()
        {
            try
            {
                form?.StepIt();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}