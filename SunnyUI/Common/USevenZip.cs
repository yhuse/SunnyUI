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
 * 文件名称: USevenZip.cs
 * 文件说明: 7-zip 文件压缩解压类（需要 7z.exe 和 7z.dll）
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.Collections.Generic;
using System.Diagnostics;

namespace Sunny.UI
{
    /// <summary>
    /// 7-zip Helper（需要 7z.exe 和 7z.dll）
    /// </summary>
    public static class SevenZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">被压缩文件的文件名</param>
        /// <param name="zipFileName">压缩后文件名</param>
        /// <param name="password">密码</param>
        /// <returns>返回值为true:成功  false:失败</returns>
        public static bool ZipFile(string fileName, string zipFileName, string password = "")
        {
            try
            {
                string zipPara = " a \"" + zipFileName + "\" \"" + fileName.Trim() + "\""; //压缩文件的命令行参数
                if (password != "")
                {
                    zipPara += " -p\"" + password + "\"" + " -mhe";
                }

                return SeventZPrcess(zipPara);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileNames">被压缩文件的文件列表</param>
        /// <param name="zipFileName">压缩后文件名</param>
        /// <param name="password">密码</param>
        /// <returns>返回值为true:成功  false:失败</returns>
        public static bool ZipFile(List<string> fileNames, string zipFileName, string password = "")
        {
            try
            {
                //获取压缩文件并将其添加到命令行参数中
                for (int i = 0; i < fileNames.Count; i++)
                {
                    string zipPara = " a \"" + zipFileName + "\" \"" + fileNames[i].Trim() + "\""; //压缩文件的命令行参数
                    if (password != "")
                    {
                        zipPara += " -p\"" + password + "\"" + " -mhe";
                    }

                    if (!SeventZPrcess(zipPara))
                    {
                        return false;
                    }
                }

                //启动进程并调用7Z
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dirname">被压缩文件的文件夹名</param>
        /// <param name="zipFileName">压缩后文件名</param>
        /// <param name="containdir">包含文件夹</param>
        /// <param name="password">密码</param>
        /// <returns>返回值为true:成功  false:失败</returns>
        public static bool ZipDir(string dirname, string zipFileName, bool containdir = false, string password = "")
        {
            try
            {
                string zipPara = " a \"" + zipFileName + "\" \"" + dirname.Trim().DealPath() + (containdir ? "" : "*") + "\""; //压缩文件的命令行参数
                if (password != "")
                {
                    zipPara += " -p\"" + password + "\"" + " -mhe";
                }

                return SeventZPrcess(zipPara);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解压缩文件（解压后不带目录）
        /// </summary>
        /// <param name="zipFileName">压缩文件名</param>
        /// <param name="unZipPath">解压缩后文件的路径</param>
        /// <param name="password">密码</param>
        /// <returns>返回值true:成功  false:失败</returns>
        public static bool UnZipFile(string zipFileName, string unZipPath, string password = "")
        {
            try
            {
                string arguments = " e -y \"" + zipFileName + "\" -o\"" + unZipPath + "\"";
                if (password != "")
                {
                    arguments += " -p\"" + password + "\"";
                }

                return SeventZPrcess(arguments);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解压缩文件（解压后带目录）
        /// </summary>
        /// <param name="zipFileName">压缩文件名</param>
        /// <param name="unZipPath">解压缩后文件的路径</param>
        /// <param name="password">密码</param>
        /// <returns>返回值true:成功  false:失败</returns>
        public static bool UnZipFileWithPath(string zipFileName, string unZipPath, string password = "")
        {
            try
            {
                string arguments = " x -y \"" + zipFileName + "\" -o\"" + unZipPath + "\"";
                if (password != "")
                {
                    arguments += " -p\"" + password + "\"";
                }

                return SeventZPrcess(arguments);
            }
            catch
            {
                return false;
            }
        }

        private static bool SeventZPrcess(string arguments)
        {
            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //隐藏压缩窗口
            process.StartInfo.FileName = DirEx.CurrentDir() + "7z.exe";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.Arguments = arguments;
            process.Start();
            process.WaitForExit();
            if (process.HasExited)
            {
                int iExitCode = process.ExitCode;
                process.Close();
                if (iExitCode != 0 && iExitCode != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}