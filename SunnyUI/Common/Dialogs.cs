/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
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
 * 文件名称: Dialogs.cs
 * 文件说明: 对话框扩展类，用于选择文件夹、打开文件、保存文件等操作。
 * 当前版本: V3.8
 * 创建日期: 2025-06-16
 *
 * 2025-06-16: V3.8.5 增加文件说明
******************************************************************************/

using System.IO;
using System.Windows.Forms;

namespace Sunny.UI;

/// <summary>
/// 对话框扩展类，用于选择文件夹、打开文件、保存文件等操作。
/// </summary>
public static class Dialogs
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
        using FolderBrowserDialog fd = new FolderBrowserDialog();
        fd.Description = desc;
        fd.ShowNewFolderButton = showNewButton;
        fd.SelectedPath = dir;
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
        using FolderBrowserDialogEx fd = new FolderBrowserDialogEx();
        fd.Description = desc;
        fd.DirectoryPath = dir;
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
    /// 打开文件夹对话框
    /// </summary>
    /// <param name="filename">返回True时，获取文件名</param>
    /// <param name="filter">过滤器</param>
    /// <param name="defaultExt">默认文件扩展名</param>
    /// <returns>打开是否成功</returns>
    public static bool OpenFileDialog(ref string filename, string filter = "", string defaultExt = "")
    {
        using OpenFileDialog od = new OpenFileDialog();
        od.Title = UIStyles.CurrentResources.Open;

        try
        {
            od.FileName = filename;
            od.Filter = filter;
        }
        catch
        {
            od.Filter = "";
        }

        od.DefaultExt = defaultExt;
        bool isOk = od.ShowDialog() == DialogResult.OK;
        if (isOk) filename = od.FileName;
        return isOk;
    }

    /// <summary>
    /// 保存文件对话框
    /// </summary>
    /// <param name="filename">返回True时，获取文件名</param>
    /// <param name="filter">过滤器</param>
    /// <param name="defaultExt">默认文件扩展名</param>
    /// <returns>保存是否成功</returns>
    public static bool SaveFileDialog(ref string filename, string filter = "", string defaultExt = "")
    {
        using SaveFileDialog od = new SaveFileDialog();
        od.Title = UIStyles.CurrentResources.Save;
        try
        {
            od.FileName = filename;
            od.Filter = filter;
        }
        catch
        {
            od.Filter = "";
        }

        od.DefaultExt = defaultExt;
        bool isOk = od.ShowDialog() == DialogResult.OK;
        if (isOk) filename = od.FileName;
        return isOk;
    }
}