using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Sunny.UI
{
    public static class ApplicationEx
    {
        private static readonly string StartUpPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        /// <summary>
        /// 获取当前应用程序的 GUID
        /// </summary>
        /// <returns>应用程序的 GUID</returns>
        public static Guid AppGuid()
        {
            var asm = Assembly.GetEntryAssembly();
            if (asm == null) return Guid.Empty;
            object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
            return attr.Length > 0 && attr[0] is GuidAttribute ga ? new Guid(ga.Value) : Guid.Empty;
        }

        /// <summary>
        /// 获取指定特殊文件夹的路径，并处理路径格式
        /// </summary>
        /// <param name="specialFolder">特殊文件夹枚举</param>
        /// <returns>处理后的文件夹路径</returns>
        public static string Folder(this Environment.SpecialFolder specialFolder) => Environment.GetFolderPath(specialFolder).DealPath();

        /// <summary>
        /// 获取指定特殊文件夹下的应用程序文件夹路径，并在必要时创建该文件夹
        /// </summary>
        /// <param name="specialFolder">特殊文件夹枚举</param>
        /// <param name="createIfNotExists">如果文件夹不存在，是否创建</param>
        /// <returns>应用程序文件夹路径</returns>
        public static string FolderWithApplication(this Environment.SpecialFolder specialFolder, bool createIfNotExists = true)
        {
            var dir = (specialFolder.Folder() + Application.ProductName).DealPath();
            if (createIfNotExists) Dir.CreateDir(dir);
            return dir;
        }

        /// <summary>
        /// 获取当前漫游用户的应用程序特定数据的公共储存库的目录
        /// C:\Users\{YourUserName}\AppData\Roaming\{Application.ProductName}\
        /// </summary>
        /// <returns>应用程序数据文件夹路径</returns>
        public static string ApplicationDataFolder() => Environment.SpecialFolder.ApplicationData.FolderWithApplication();

        /// <summary>
        /// 获取当前非漫游用户使用的应用程序特定数据的公共储存库的目录
        /// C:\Users\{YourUserName}\AppData\Local\{Application.ProductName}\
        /// </summary>
        /// <returns>本地应用程序数据文件夹路径</returns>
        public static string LocalApplicationDataFolder() => Environment.SpecialFolder.LocalApplicationData.FolderWithApplication();

        /// <summary>
        /// 获取所有用户使用的应用程序特定数据的公共储存库的目录
        /// C:\ProgramData \{Application.ProductName}\
        /// </summary>
        /// <returns>公共应用程序数据文件夹路径</returns>
        public static string CommonApplicationDataFolder() => Environment.SpecialFolder.CommonApplicationData.FolderWithApplication();

        /// <summary>
        /// 增加当前程序到开机自动运行
        /// </summary>
        /// <param name="arguments">启动参数</param>
        public static void AddToStartup(string arguments)
        {
            using var key = Registry.CurrentUser.OpenSubKey(StartUpPath, true);
            key?.SetValue(Application.ProductName, $"\"{Application.ExecutablePath}\" {arguments}");
        }

        /// <summary>
        /// 增加当前程序到开机自动运行
        /// </summary>
        public static void AddToStartup()
        {
            using var key = Registry.CurrentUser.OpenSubKey(StartUpPath, true);
            key?.SetValue(Application.ProductName, $"\"{Application.ExecutablePath}\"");
        }

        /// <summary>
        /// 从开机自动运行移除当前程序
        /// </summary>
        public static void RemoveFromStartup()
        {
            using var key = Registry.CurrentUser.OpenSubKey(StartUpPath, true);
            if (Application.ProductName != null) key?.DeleteValue(Application.ProductName, false);
        }

        /// <summary>
        /// 判断当前程序是否开机自动运行
        /// </summary>
        /// <returns>是否开机自动运行</returns>
        public static bool StartupEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(StartUpPath, false);
            return key != null && key.GetValue(Application.ProductName) != null;
        }

        /// <summary>
        /// 检查并更新当前程序开机自动运行路径
        /// </summary>
        public static void CheckAndUpdateStartupPath()
        {
            if (!StartupEnabled()) return;

            string arg = string.Empty;
            // 从注册表键值中读取启动参数
            using var key = Registry.CurrentUser.OpenSubKey(StartUpPath, true);
            if (key == null) return;
            string oldValue = key.GetValue(Application.ProductName)?.ToString();
            if (oldValue == null) return;
            if (oldValue.StartsWith("\""))
            {
                arg = string.Join("\"", oldValue.Split('\"').Skip(2)).Trim();
            }
            else if (oldValue.Contains(" "))
            {
                arg = string.Join(" ", oldValue.Split(' ').Skip(1));
            }

            if (string.IsNullOrEmpty(arg))
                AddToStartup();
            else
                AddToStartup(arg);
        }
    }
}
