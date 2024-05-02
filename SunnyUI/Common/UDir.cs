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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 目录扩展类
    /// </summary>
    public static class DirEx
    {
        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="dir">目录的绝对路径</param>
        public static bool CreateDir(string dir)
        {
            if (dir.IsNullOrEmpty())
            {
                throw new ArgumentNullException(dir, "目录不能为空。");
            }

            if (!Path.IsPathRooted(dir))
            {
                throw new ArgumentException(dir, "不包含根路径。");
            }

            //
            //
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return Directory.Exists(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 返回当前基目录
        /// </summary>
        /// <returns>基目录</returns>
        public static string CurrentDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory.DealPath();
        }
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

        /// <summary>
        /// 获取指定目录中的匹配项（文件或目录）
        /// </summary>
        /// <param name="dir">要搜索的目录</param>
        /// <param name="regexPattern">项名模式（正则）。null表示忽略模式匹配，返回所有项</param>
        /// <param name="depth">递归深度。负数表示不限，0表示仅顶级</param>
        /// <param name="throwEx">是否抛异常</param>
        /// <returns>结果</returns>
        public static string[] GetFileSystemEntries(string dir, string regexPattern = null, int depth = 0, bool throwEx = false)
        {
            List<string> lst = new List<string>();

            try
            {
                foreach (string item in Directory.GetFileSystemEntries(dir))
                {
                    try
                    {
                        string filename = Path.GetFileName(item);

                        if (regexPattern == null || Regex.IsMatch(filename, regexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
                        {
                            lst.Add(item);
                        }

                        //递归
                        if (depth != 0 && (File.GetAttributes(item) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            lst.AddRange(GetFileSystemEntries(item, regexPattern, depth - 1, throwEx));
                        }
                    }
                    catch
                    {
                        if (throwEx)
                        {
                            throw;
                        }
                    }
                }
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
            }

            return lst.ToArray();
        }

        /// <summary>
        /// 获取指定目录中的匹配文件
        /// </summary>
        /// <param name="dir">要搜索的目录</param>
        /// <param name="regexPattern">文件名模式（正则）。null表示忽略模式匹配，返回所有文件</param>
        /// <param name="depth">递归深度。负数表示不限，0表示仅顶级</param>
        /// <param name="throwEx">是否抛异常</param>
        /// <returns>结果</returns>
        public static string[] GetFiles(string dir, string regexPattern = null, int depth = 0, bool throwEx = false)
        {
            List<string> lst = new List<string>();

            try
            {
                foreach (string item in Directory.GetFileSystemEntries(dir))
                {
                    try
                    {
                        bool isFile = (File.GetAttributes(item) & FileAttributes.Directory) != FileAttributes.Directory;
                        string filename = Path.GetFileName(item);

                        if (isFile && (regexPattern == null || Regex.IsMatch(filename, regexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline)))
                        {
                            lst.Add(item);
                        }

                        //递归
                        if (depth != 0 && !isFile)
                        {
                            lst.AddRange(GetFiles(item, regexPattern, depth - 1, throwEx));
                        }
                    }
                    catch
                    {
                        if (throwEx)
                        {
                            throw;
                        }
                    }
                }
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
            }

            return lst.ToArray();
        }

        /// <summary>
        /// 获取指定目录中的匹配目录
        /// </summary>
        /// <param name="dir">要搜索的目录</param>
        /// <param name="regexPattern">目fu录名模式（正则）。null表示忽略模式匹配，返回所有目录</param>
        /// <param name="depth">递归深度。负数表示不限，0表示仅顶级</param>
        /// <param name="throwEx">是否抛异常</param>
        /// <returns>结果</returns>
        public static string[] GetDirectories(string dir, string regexPattern = null, int depth = 0, bool throwEx = false)
        {
            List<string> lst = new List<string>();

            try
            {
                foreach (string item in Directory.GetDirectories(dir))
                {
                    try
                    {
                        string filename = Path.GetFileName(item);
                        if (filename == null)
                        {
                            continue;
                        }

                        if (regexPattern == null || Regex.IsMatch(filename, regexPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline))
                        {
                            lst.Add(item);
                        }

                        //递归
                        if (depth != 0)
                        {
                            lst.AddRange(GetDirectories(item, regexPattern, depth - 1, throwEx));
                        }
                    }
                    catch
                    {
                        if (throwEx)
                        {
                            throw;
                        }
                    }
                }
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
            }

            return lst.ToArray();
        }
    }
}