using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    public interface IZoomScale
    {
        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="scale">缩放比例</param>
        void SetZoomScale(float scale);

        /// <summary>
        /// 控件缩放前在其容器里的位置
        /// </summary>
        Rectangle ZoomScaleRect { get; set; }

        /// <summary>
        /// 禁止控件跟随窗体缩放
        /// </summary>
        bool ZoomScaleDisabled { get; set; }
    }

    public static class UIZoomScale
    {
        public static Rectangle Create(Control control)
        {
            if (control is IZoomScale ctrl)
            {
                if (ctrl.ZoomScaleRect.Width > 0 || ctrl.ZoomScaleRect.Height > 0)
                {
                    return ctrl.ZoomScaleRect;
                }
                else
                {
                    int Width = control.Width;
                    int Height = control.Height;
                    int XInterval = 0;
                    int YInterval = 0;
                    if (control.Parent != null)
                    {
                        if ((control.Anchor & AnchorStyles.Left) == AnchorStyles.Left)
                        {
                            XInterval = control.Left;
                        }

                        if ((control.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
                        {
                            XInterval = control.Parent.Width - control.Right;
                        }

                        if ((control.Anchor & AnchorStyles.Top) == AnchorStyles.Top)
                        {
                            YInterval = control.Top;
                        }

                        if ((control.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                        {
                            YInterval = control.Parent.Height - control.Bottom;
                        }
                    }

                    return new Rectangle(XInterval, YInterval, Width, Height);
                }
            }
            else
            {
                return new Rectangle(control.Left, control.Top, control.Width, control.Height);
            }
        }

        public static int Calc(int size, float scale)
        {
            return (int)(size * scale + 0.5);
        }

        public static Size Calc(Size size, float scale)
        {
            return new Size(Calc(size.Width, scale), Calc(size.Height, scale));
        }

        /// <summary>
        /// 设置控件缩放比例
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="scale">缩放比例</param>
        internal static void SetZoomScale(Control control, float scale)
        {
            if (scale.EqualsFloat(0)) return;

            if (control is IZoomScale ctrl)
            {
                if (ctrl.ZoomScaleDisabled)
                {
                    return;
                }

                //设置控件的缩放参数
                ctrl.ZoomScaleRect = UIZoomScale.Create(control);

                //设置自定义的缩放，例如UIAvatar
                ctrl.SetZoomScale(scale);

                if (control.Dock == DockStyle.Fill)
                {
                    return;
                }

                var rect = ctrl.ZoomScaleRect;
                switch (control.Dock)
                {
                    case DockStyle.None:
                        control.Height = Calc(rect.Height, scale);
                        control.Width = Calc(rect.Width, scale);

                        if (control.Parent != null)
                        {
                            if ((control.Anchor & AnchorStyles.Left) == AnchorStyles.Left)
                            {
                                control.Left = Calc(rect.Left, scale);
                            }

                            if ((control.Anchor & AnchorStyles.Right) == AnchorStyles.Right)
                            {
                                int right = Calc(rect.Left, scale);
                                control.Left = control.Parent.Width - right - control.Width;
                            }

                            if ((control.Anchor & AnchorStyles.Top) == AnchorStyles.Top)
                            {
                                if (control.Parent is UIForm form && form.ShowTitle)
                                    control.Top = Calc(rect.Top - form.TitleHeight, scale) + form.TitleHeight;
                                else
                                    control.Top = Calc(rect.Top, scale);
                            }

                            if ((control.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                            {
                                int bottom = Calc(rect.Top, scale);
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
