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
 * 文件名称: UIDPIScale.cs
 * 文件说明: DPI自适应类
 * 当前版本: V3.1
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
        private static float dpiScale = -1;

        public static float DPIScale()
        {
            if (dpiScale < 0)
            {
                using Bitmap bmp = new Bitmap(1, 1);
                using Graphics g = bmp.Graphics();
                dpiScale = g.DpiX / 96.0f;
            }

            return UIStyles.GlobalFont ? dpiScale * 100.0f / UIStyles.GlobalFontScale : dpiScale;
        }

        public static bool NeedSetDPIFont()
        {
            return UIStyles.DPIScale && (DPIScale() > 1 || UIStyles.GlobalFont);
        }

        internal static Font DPIScaleFont(this Font font, float fontSize)
        {
            if (fontSize <= 0) return font;
            if (UIStyles.DPIScale)
            {
                if (UIStyles.GlobalFont)
                {
                    byte gdiCharSet = UIStyles.GetGdiCharSet(UIStyles.GlobalFontName);
                    return new Font(UIStyles.GlobalFontName, fontSize / DPIScale(), font.Style, font.Unit, gdiCharSet);
                }
                else
                {
                    return new Font(font.FontFamily, fontSize / DPIScale(), font.Style, font.Unit, font.GdiCharSet);
                }
            }
            else
            {
                return new Font(font.FontFamily, fontSize, font.Style, font.Unit, font.GdiCharSet);
            }
        }

        internal static void SetDPIScaleFont(this Control control, float fontSize)
        {
            if (!UIDPIScale.NeedSetDPIFont()) return;
            if (control is IStyleInterface ctrl)
            {
                control.Font = SetDPIScaleFont(control.Font, fontSize);
            }
        }

        internal static Font SetDPIScaleFont(this Font font, float fontSize) => UIDPIScale.NeedSetDPIFont() ? font.DPIScaleFont(fontSize) : font;

        internal static List<IStyleInterface> GetAllDPIScaleControls(this Control control)
        {
            var list = new List<IStyleInterface>();
            foreach (Control con in control.Controls)
            {
                if (con is IStyleInterface ctrl) list.Add(ctrl);

                if (con is UITextBox) continue;
                if (con is UIDropControl) continue;
                if (con is UIListBox) continue;
                if (con is UIImageListBox) continue;
                if (con is UIPagination) continue;
                if (con is UIRichTextBox) continue;
                if (con is UITreeView) continue;

                if (con.Controls.Count > 0)
                {
                    list.AddRange(GetAllDPIScaleControls(con));
                }
            }

            return list;
        }

        internal static List<Control> GetAllZoomScaleControls(this Control control)
        {
            var list = new List<Control>();
            foreach (Control con in control.Controls)
            {
                if (con is IZoomScale)
                    list.Add(con);

                if (con is UITextBox) continue;
                if (con is UIDropControl) continue;
                if (con is UIListBox) continue;
                if (con is UIImageListBox) continue;
                if (con is UIPagination) continue;
                if (con is UIRichTextBox) continue;
                if (con is UITreeView) continue;

                if (con.Controls.Count > 0)
                {
                    list.AddRange(GetAllZoomScaleControls(con));
                }
            }

            return list;
        }
    }
}
