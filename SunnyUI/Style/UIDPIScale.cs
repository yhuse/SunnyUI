/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2022 ShenYongHua(沈永华).
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
        public static float DPIScale()
        {
            return GDI.Graphics().DpiX / 96.0f / (UIStyles.FontSize / 12.0f);
        }

        public static bool DPIScaleIsOne()
        {
            return DPIScale().EqualsFloat(1);
        }

        internal static float DPIScaleFontSize(this Font font)
        {
            if (UIStyles.DPIScale)
                return font.Size / DPIScale();
            else
                return font.Size;
        }

        internal static Font DPIScaleFont(this Font font)
        {
            return DPIScaleFont(font, font.Size);
        }

        internal static Font DPIScaleFont(this Font font, float fontSize)
        {
            if (UIStyles.DPIScale)
            {
                if (font.GdiCharSet == 134)
                    return new Font(font.FontFamily, fontSize / DPIScale(), font.Style, font.Unit, font.GdiCharSet);
                else
                    return new Font(font.FontFamily, fontSize / DPIScale());
            }
            else
            {
                if (font.GdiCharSet == 134)
                    return new Font(font.FontFamily, fontSize, font.Style, font.Unit, font.GdiCharSet);
                else
                    return new Font(font.FontFamily, fontSize);
            }
        }

        internal static void SetDPIScaleFont(this Control control)
        {
            if (!UIStyles.DPIScale) return;
            if (!UIDPIScale.DPIScaleIsOne())
            {
                if (control is IStyleInterface ctrl)
                {
                    if (!ctrl.IsScaled)
                        control.Font = control.Font.DPIScaleFont();
                }
            }
        }

        internal static List<Control> GetAllDPIScaleControls(this Control control)
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

        internal static int Calc(int size, float scale)
        {
            return (int)(size * scale + 0.5);
        }

        internal static void SetControlScale(Control control, float scale)
        {
            if (scale.EqualsFloat(0)) return;

            if (control is IStyleInterface ctrl)
            {
                if (ctrl.ForbidControlScale)
                {
                    return;
                }

                if (control is IControlScale sc)
                {
                    sc.SetControlScale(scale);
                }

                if (control.Dock == DockStyle.Fill)
                {
                    return;
                }

                var rect = ctrl.DesignedRect;
                switch (control.Dock)
                {
                    case DockStyle.None:
                        control.Height = Calc(rect.Height, scale);
                        control.Width = Calc(rect.Width, scale);

                        if (control.Parent != null)
                        {
                            if ((control.Anchor & AnchorStyles.Left) == AnchorStyles.Left)
                            {
                                control.Left = Calc(rect.XInterval, scale);
                            }

                            if ((control.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
                            {
                                int right = Calc(rect.XInterval, scale);
                                control.Left = control.Parent.Width - right - control.Width;
                            }

                            if ((control.Anchor & AnchorStyles.Top) == AnchorStyles.Top)
                            {
                                if (control.Parent is UIForm form && form.ShowTitle)
                                    control.Top = Calc(rect.YInterval - form.TitleHeight, scale) + form.TitleHeight;
                                else
                                    control.Top = Calc(rect.YInterval, scale);
                            }

                            if ((control.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                            {
                                int bottom = Calc(rect.YInterval, scale);
                                control.Top = control.Parent.Height - bottom - control.Height;
                            }
                        }

                        break;
                    case DockStyle.Top:
                        control.Height = Calc(rect.Height, scale);
                        break;
                    case DockStyle.Bottom:
                        control.Height = Calc(rect.Height, scale);
                        break;
                    case DockStyle.Left:
                        control.Width = Calc(rect.Width, scale);
                        break;
                    case DockStyle.Right:
                        control.Width = Calc(rect.Width, scale);
                        break;
                    case DockStyle.Fill:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
