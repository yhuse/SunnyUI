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
 * 文件名称: UDir.cs
 * 文件说明: 目录扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.IO;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 目录扩展类
    /// </summary>
    public static class DirEx
    {
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="desc">说明</param>
        /// <param name="dir">返回true则path为选择文件夹路径</param>
        /// <param name="showNewButton">显示新建文件夹按钮</param>
        /// <returns>是否选择文件夹</returns>
        public static bool SelectDir(string desc, ref string dir, bool showNewButton = true)
        {
            using FolderBrowserDialog fd = new FolderBrowserDialog { Description = desc, ShowNewFolderButton = showNewButton, SelectedPath = dir };
            bool bOk = fd.ShowDialog() == DialogResult.OK;
            if (bOk) dir = fd.SelectedPath.DealPath();
            return bOk;
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="desc">说明</param>
        /// <param name="dir">返回true则path为选择文件夹路径</param>
        /// <returns>是否选择文件夹</returns>
        public static bool SelectDirEx(string desc, ref string dir)
        {
            using FolderBrowserDialogEx fd = new FolderBrowserDialogEx { Description = desc, DirectoryPath = dir };
            bool bOk = fd.ShowDialog(null) == DialogResult.OK;
            if (bOk) dir = fd.DirectoryPath.DealPath();
            return bOk;
        }

        /// <summary>
        /// 调用系统资源管理器打开文件夹，如果是文件则选中文件
        /// </summary>
        /// <param name="dir">文件夹</param>
        public static void OpenDir(string dir)
        {
            if (File.Exists(dir))
            {
                System.Diagnostics.Process.Start("Explorer.exe", @"/select," + dir);
            }

            if (Directory.Exists(dir))
            {
                System.Diagnostics.Process.Start("Explorer.exe", dir);
            }
        }
    }
}