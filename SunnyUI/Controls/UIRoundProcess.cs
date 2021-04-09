/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
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
 * 文件名称: UIRoundProcess.cs
 * 文件说明: 圆形进度条
 * 当前版本: V3.0
 * 创建日期: 2021-04-08
 *
 * 2021-04-08: V3.0.2 增加文件说明
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 圆形滚动条
    /// </summary>
    [ToolboxItem(false)]
    public class UIRoundProcess : UIControl
    {
        public UIRoundProcess()
        {
            Size = new Size(120, 120);
            Inner = 30;
            Outer = 50;

            ProcessColor = UIColor.Blue;
            ProcessBackColor = Color.FromArgb(155, 200, 255);
            base.BackColor = Color.Transparent;
            ShowText = false;
            ShowRect = false;
            ShowFill = false;
        }

        public int Inner { get; set; }

        public int Outer { get; set; }

        public Color ProcessColor { get; set; }

        public Color ProcessBackColor { get; set; }

        public int Value { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillFan(ProcessBackColor, ClientRectangle.Center(), Inner, Outer, 0, 360);
            e.Graphics.FillFan(ProcessColor, ClientRectangle.Center(), Inner, Outer, -90, Value / 100.0f * 360);
        }
    }
}
