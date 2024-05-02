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
 * 文件名称: UIDropEditor.cs
 * 文件说明: 控件属性编辑面板基类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
#pragma warning disable SYSLIB0003 // 类型或成员已过时

namespace Sunny.UI
{
    /// <summary>
    ///   提供设置的编辑器 <see cref="P:System.Windows.Forms.ToolStripStatusLabel.RectSides" /> 属性。
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public abstract class UIDropEditor : UITypeEditor
    {
        protected UIDropEditorUI EditorUI;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (service != null)
                {
                    if (EditorUI == null)
                    {
                        EditorUI = CreateUI();
                    }

                    EditorUI.Start(service, value);
                    service.DropDownControl(EditorUI);
                    if (EditorUI.Value != null)
                    {
                        value = EditorUI.Value;
                    }

                    EditorUI.End();
                }
            }

            return value;
        }

        protected abstract UIDropEditorUI CreateUI();

        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    public abstract class UIDropEditorUI : UserControl
    {
        private IWindowsFormsEditorService edSvc;

        protected object currentValue;
        protected bool updateCurrentValue;

        public object Value => currentValue;

        public IWindowsFormsEditorService EditorService => edSvc;

        public void Start(IWindowsFormsEditorService editorService, object value)
        {
            edSvc = editorService;
            currentValue = value;
            InitValue(value);
            updateCurrentValue = true;
        }

        protected abstract void InitValue(object value);

        protected abstract void UpdateCurrentValue();

        public void End()
        {
            edSvc = null;
            currentValue = null;
            updateCurrentValue = false;
        }
    }
}