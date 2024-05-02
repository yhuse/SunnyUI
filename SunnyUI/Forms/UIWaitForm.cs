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
 * 文件名称: UIWaitForm.cs
 * 文件说明: 等待窗体
 * 当前版本: V3.1
 * 创建日期: 2020-10-13
 *
 * 2020-10-13: V3.0.0 增加文件说明
******************************************************************************/

namespace Sunny.UI
{
    public sealed partial class UIWaitForm : UIForm
    {
        public UIWaitForm()
        {
            InitializeComponent();
            base.Text = UILocalize.InfoTitle;
            SetDescription(UILocalize.SystemProcessing);
        }

        public UIWaitForm(string desc)
        {
            InitializeComponent();
            base.Text = UILocalize.InfoTitle;
            SetDescription(desc);
        }

        private delegate void SetTextHandler(string text);

        public void SetDescription(string text)
        {
            if (labelDescription.InvokeRequired)
            {
                Invoke(new SetTextHandler(SetDescription), text);
            }
            else
            {
                labelDescription.Text = text;
                labelDescription.Invalidate();
            }
        }

        private void Bar_Tick(object sender, System.EventArgs e)
        {
            if (UIFormServiceHelper.WaitFormServiceClose)
            {
                UIFormServiceHelper.WaitFormServiceClose = false;
                Close();
            }
        }
    }
}
