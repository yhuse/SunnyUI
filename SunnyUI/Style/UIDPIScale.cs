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
 * 文件名称: UIDPIScale.cs
 * 文件说明: DPI自适应类
 * 当前版本: V3.0
 * 创建日期: 2021-12-01
 *
 * 2021-12-01: V3.0.9 增加文件说明
******************************************************************************/

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public static class UIDPIScale
    {
        public static float DPIScale(this Control control)
        {
            return control.CreateGraphics().DpiX / 96.0f;
        }

        public static float DPIScaleFontSize(this Control control, Font font)
        {
            if (UIStyles.DPIScale)
                return font.Size / control.DPIScale();
            else
                return font.Size;
        }

        public static float DPIScaleFontSize(this Font font)
        {
            return font.Size.DPIScaleFontSize();
        }

        public static float DPIScaleFontSize(this float fontSize)
        {
            using Control control = new();
            if (UIStyles.DPIScale)
                return fontSize / control.DPIScale();
            else
                return fontSize;
        }

        public static Font DPIScaleFont(this Control control, Font font)
        {
            return control.DPIScaleFont(font, font.Size);
        }

        public static Font DPIScaleFont(this Control control, Font font, float fontSize)
        {
            if (UIStyles.DPIScale)
            {
                if (font.GdiCharSet == 134)
                    return new Font(font.FontFamily, fontSize / control.DPIScale(), font.Style, font.Unit, font.GdiCharSet);
                else
                    return new Font(font.FontFamily, fontSize / control.DPIScale());
            }
            else
            {
                if (font.GdiCharSet == 134)
                    return new Font(font.FontFamily, fontSize, font.Style, font.Unit, font.GdiCharSet);
                else
                    return new Font(font.FontFamily, fontSize);
            }
        }

        public static Font DPIScaleFont(this Font font)
        {
            if (UIStyles.DPIScale)
            {
                using Control control = new();
                return control.DPIScaleFont(font);
            }
            else
            {
                return font;
            }
        }

        public static void SetDPIScaleFont(this Control control)
        {
            if (!UIStyles.DPIScale) return;
            if (!control.DPIScale().EqualsFloat(1))
            {
                if (control is IStyleInterface ctrl)
                {
                    if (!ctrl.IsScaled)
                        control.Font = control.DPIScaleFont(control.Font);
                }
            }
        }

        public static List<Control> GetAllDPIScaleControls(this Control control)
        {
            var list = new List<Control>();
            foreach (Control con in control.Controls)
            {
                list.Add(con);

                if (con is UITextBox) continue;
                if (con is UIDropControl) continue;
                if (con is UIListBox) continue;
                if (con is UIImageListBox) continue;
                if (con is UIPagination) continue;
                if (con is UIRichTextBox) continue;
                if (con is UITreeView) continue;
                if (con is UINavBar) continue;

                if (con.Controls.Count > 0)
                {
                    list.AddRange(GetAllDPIScaleControls(con));
                }
            }

            return list;
        }
    }
}
