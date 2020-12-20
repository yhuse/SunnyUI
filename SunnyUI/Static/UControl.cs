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
 * 文件名称: UControl.cs
 * 文件说明: 界面控件扩展类
 * 当前版本: V2.2
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 界面控件扩展类
    /// </summary>
    public static class ControlEx
    {
        public static void ReStart(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        public static Point LocationOnScreen(this Control ctrl)
        {
            Point point = new Point(0, 0);
            do
            {
                point.Offset(ctrl.Location);
                ctrl = ctrl.Parent;
            }
            while (ctrl != null);

            return point;
        }

        public static Form RootForm(this Control ctrl)
        {
            if (ctrl == null) return null;
            while (ctrl.Parent != null)
            {
                ctrl = ctrl.Parent;
            }

            return ctrl as Form;
        }

        public static Form GetParentForm(this Control ctrl)
        {
            while (!IsForm(ctrl.Parent))
            {
                ctrl = ctrl.Parent;
            }

            return ctrl.Parent as Form;
        }

        private static bool IsForm(Control ctrl)
        {
            return ctrl is Form;
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int BorderSize(this Form form)
        {
            return (form.Width - form.ClientSize.Width) / 2;
        }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int TitleHeight(this Form form)
        {
            return (form.Height - form.ClientSize.Height) - form.BorderSize();
        }

        private static Point LocationOnClient(this Control c)
        {
            Point point = new Point(0, 0);
            for (; c.Parent != null; c = c.Parent)
            {
                point.Offset(c.Location);
            }

            return point;
        }

        public static Point SetLocationToForm(this Control ctrl, Point offset)
        {
            Point pt = ctrl.LocationOnClient();
            pt.Offset(offset);
            return pt;
        }

        public static Point SetLocationToWindow(this Form form, Control ctrl, Point offset)
        {
            Point pt = ctrl.SetLocationToForm(offset);
            pt.Offset(form.Location);
            pt.Offset(form.BorderSize(), form.TitleHeight());
            return pt;
        }

        public static void ShowContextMenuStrip(this Control ctrl, ContextMenuStrip menu, Point offset)
        {
            //设置显示的位置为鼠标所在的位置
            menu.Show(ctrl, offset);
        }

        public static void ShowContextMenuStrip(this Control ctrl, ContextMenuStrip menu, int offsetX, int offsetY)
        {
            menu.Show(ctrl, offsetX, offsetY);
        }

        //https://www.codeproject.com/Tips/1264882/Extension-Methods-for-Multiple-Control-Tags
        /// <summary>
        /// Control的Tag属性扩展，设置多个对象
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="objects">对象列表</param>
        public static void InitTag(this Control control, params object[] objects)
        {
            control.Tag = objects;
        }

        /// <summary>
        /// Control的Tag属性扩展，根据索引获取对象
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="index">索引</param>
        /// <returns>对象</returns>
        public static object GetTag(this Control control, int index)
        {
            if (control.Tag == null || control.Tag.GetType() != typeof(object[]))
            {
                return null;
            }

            var objects = control.Tag as object[];
            if (objects == null || index < 0 || index >= objects.Length)
            {
                return null;
            }

            return objects[index];
        }

        /// <summary>
        /// Control的Tag属性扩展，根据索引更新对象
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="index">索引</param>
        /// <param name="value">对象</param>
        public static void SetTag(this Control control, int index, object value)
        {
            if (control.Tag == null || control.Tag.GetType() != typeof(object[]))
            {
                return;
            }

            var objects = control.Tag as object[];
            if (objects == null || index < 0 || index >= objects.Length)
            {
                return;
            }

            objects[index] = value;
        }

        /// <summary>
        /// 修改控件或窗体的边框，例如 TextBox 或是 Form 窗体
        /// </summary>
        /// <param name="m">消息</param>
        /// <param name="control">控件对象</param>
        /// <param name="width">边框像素</param>
        /// <param name="color">边框颜色</param>
        public static void ResetBorderColor(Message m, Control control, int width, Color color)
        {
            //根据颜色和边框像素取得一条线
            using (Pen pen = new Pen(color, width))
            {
                //得到当前的句柄
                IntPtr hDC = (IntPtr)Win32.User.GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0)
                {
                    return;
                }
                //绘制边框
                Graphics g = Graphics.FromHdc(hDC);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawRectangle(pen, 0, 0, control.Width - width, control.Height - width);
                //释放
                Win32.User.ReleaseDC(m.HWnd, hDC);
            }
        }

        /// <summary>
        /// 根据名称获取控件
        /// </summary>
        /// <param name="ctrl">容器</param>
        /// <param name="childName">控件名</param>
        /// <returns>结果</returns>
        public static Control GetControl(this Control ctrl, string childName)
        {
            //if the input control's name equals the input controlName,return the control
            if (ctrl.Name == childName)
            {
                return ctrl;
            }

            return ctrl.Controls.Count == 0 ? null : (from Control subCtrl in ctrl.Controls select GetControl(subCtrl, childName)).FirstOrDefault(tb => tb != null);
        }

        /// <summary>
        /// 根据名称获取控件
        /// </summary>
        /// <param name="ctrl">容器</param>
        /// <param name="childName">控件名</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>结果</returns>
        public static T GetControl<T>(this Control ctrl, string childName) where T : Control
        {
            if (ctrl.Name == childName)
            {
                return ctrl as T;
            }

            Control result = ctrl.GetControl(childName);
            return result as T;
        }

        /// <summary>
        /// 获取所有类型为T的控件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ctrl">容器</param>
        /// <param name="includeChild"></param>
        /// <returns>结果</returns>
        public static List<T> GetControls<T>(this Control ctrl, bool includeChild = false) where T : Control
        {
            List<T> values = new List<T>();

            foreach (Control obj in ctrl.Controls)
            {
                if (obj is T control)
                {
                    values.Add(control);
                }

                if (includeChild && obj.Controls.Count > 0)
                {
                    values.AddRange(obj.GetControls<T>(true));
                }
            }

            return values;
        }

        /// <summary>
        /// 查找包含名称的控件列表
        /// </summary>
        /// <param name="ctrl">容器</param>
        /// <param name="mask">控件名包含字符串</param>
        /// <returns>控件列表</returns>
        public static List<Control> GetControls(this Control ctrl, string mask)
        {
            List<Control> values = new List<Control>();

            foreach (Control obj in ctrl.Controls)
            {
                if (obj.Name.Contains(mask))
                {
                    values.Add(obj);
                }
            }

            return values;
        }

        /// <summary>
        /// 查找包含接口名称的控件列表
        /// </summary>
        /// <param name="ctrl">容器</param>
        /// <param name="interfaceName">接口名称</param>
        /// <param name="includeChild"></param>
        /// <returns>控件列表</returns>
        public static List<Control> GetInterfaceControls(this Control ctrl, string interfaceName, bool includeChild = false)
        {
            List<Control> values = new List<Control>();

            foreach (Control obj in ctrl.Controls)
            {
                if (obj.GetType().GetInterface(interfaceName) != null)
                {
                    values.Add(obj);
                }

                if (includeChild && obj.Controls.Count > 0)
                {
                    values.AddRange(obj.GetInterfaceControls(interfaceName, true));
                }
            }

            return values;
        }

        /// <summary>
        /// 控件保存为图片
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static Bitmap SaveToImage(this Control ctrl)
        {
            IntPtr hdc = (IntPtr)Win32.User.GetWindowDC(ctrl.Handle);
            IntPtr bitmap = (IntPtr)Win32.GDI.CreateCompatibleBitmap(hdc, ctrl.Width, ctrl.Height);
            IntPtr compatibleDc = (IntPtr)Win32.GDI.CreateCompatibleDC(hdc);
            Win32.GDI.SelectObject(compatibleDc, bitmap);
            Win32.GDI.PrintWindow(ctrl.Handle, compatibleDc, 0);
            Bitmap bmp = Image.FromHbitmap(bitmap);
            Win32.GDI.DeleteDC(hdc);       //删除用过的对象
            Win32.GDI.DeleteDC(compatibleDc);       //删除用过的对象
            return bmp;
        }

        /// <summary>
        /// 控件保存为图片
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="filename">文件名</param>
        /// <param name="format">图片格式</param>
        public static void SaveToImage(this Control ctrl, string filename, ImageFormat format)
        {
            try
            {
                using (var bmp = ctrl.SaveToImage())
                {
                    bmp.Save(filename, format);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 控件不可对用户交互做出响应
        /// </summary>
        /// <param name="ctrl"></param>
        public static void SetDisabled(this Control ctrl)
        {
            ctrl.Enabled = false;
        }

        /// <summary>
        /// 控件可对用户交互做出响应
        /// </summary>
        /// <param name="ctrl"></param>
        public static void SetEnabled(this Control ctrl)
        {
            ctrl.Enabled = true;
        }

        /// <summary>
        /// 移除按钮点击事件
        /// </summary>
        /// <param name="button">按钮</param>
        public static void RemoveClickEvent(this Button button)
        {
            FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
            if (f1 == null)
            {
                return;
            }

            object obj = f1.GetValue(button);
            PropertyInfo pi = button.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi != null)
            {
                EventHandlerList list = (EventHandlerList)pi.GetValue(button, null);
                list.RemoveHandler(obj, list[obj]);
            }
        }

        /// <summary>
        /// 双缓冲显示
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="doubleBuffered">if set to <c>true</c> [doubleBuffered].</param>
        public static void DoubleBuffered(this Control control, bool doubleBuffered = true)
        {
            var propertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(control, true, null);
            }
        }
    }
}