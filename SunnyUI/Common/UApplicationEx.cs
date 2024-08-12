using Microsoft.Win32;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sunny.UI
{
    public static class ApplicationEx
    {
        private static string StartUpPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        //获取GUID
        public static Guid AppGuid()
        {
            Assembly asm = Assembly.GetEntryAssembly();
            object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
            return new Guid((attr[0] as GuidAttribute).Value);
        }

        public static string Folder(this Environment.SpecialFolder specialFolder)
        {
            return Environment.GetFolderPath(specialFolder).DealPath();
        }

        public static string FolderWithApplication(this Environment.SpecialFolder specialFolder, bool createIfNotExists = true)
        {
            string dir = (specialFolder.Folder() + Application.ProductName).DealPath();
            if (createIfNotExists) Dir.CreateDir(dir);
            return dir;
        }

        /// <summary>
        /// 用作当前漫游用户的应用程序特定数据的公共储存库的目录。 漫游用户在网络上的多台计算机上工作。 漫游用户的配置文件保留在网络服务器上，并在用户登录时加载到系统中。
        /// C:\Users\{YourUserName}\AppData\Roaming\{Application.ProductName}\
        /// </summary>
        /// <returns></returns>
        public static string ApplicationDataFolder() => Environment.SpecialFolder.ApplicationData.FolderWithApplication();

        /// <summary>
        /// 用作当前非漫游用户使用的应用程序特定数据的公共储存库的目录。
        /// C:\Users\{YourUserName}\AppData\Local\{Application.ProductName}\
        /// </summary>
        /// <returns></returns>
        public static string LocalApplicationDataFolder() => Environment.SpecialFolder.LocalApplicationData.FolderWithApplication();

        /// <summary>
        /// 用作所有用户使用的应用程序特定数据的公共储存库的目录。
        /// C:\ProgramData \{Application.ProductName}\
        /// </summary>
        /// <returns></returns>
        public static string CommonApplicationDataFolder() => Environment.SpecialFolder.CommonApplicationData.FolderWithApplication();

        /// <summary>
        /// 增加当前程序到开机自动运行
        /// </summary>
        /// <param name="arguments"></param>
        public static void AddToStartup(string arguments)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartUpPath, true))
            {
                key.SetValue(Application.ProductName, "\"" + Application.ExecutablePath + "\" " + arguments);
            }
        }

        /// <summary>
        /// 增加当前程序到开机自动运行
        /// </summary>
        public static void AddToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartUpPath, true))
            {
                key.SetValue(Application.ProductName, "\"" + Application.ExecutablePath + "\"");
            }
        }

        /// <summary>
        /// 从开机自动运行移除当前程序
        /// </summary>
        public static void RemoveFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartUpPath, true))
            {
                key.DeleteValue(Application.ProductName, false);
            }
        }

        /// <summary>
        /// 判断当前程序是否开机自动运行
        /// </summary>
        /// <returns></returns>
        public static bool StartupEnabled()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartUpPath, false))
            {
                return key.GetValue(Application.ProductName) != null;
            }
        }

        /// <summary>
        /// 检查并更新当前程序开机自动运行路径
        /// </summary>
        public static void CheckAndUpdateStartupPath()
        {
            if (StartupEnabled())
            {
                string oldValue;
                string arg = string.Empty;

                //Read Argument From Registry Key Value
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartUpPath, true))
                {
                    oldValue = key.GetValue(Application.ProductName).ToString();
                    if (oldValue.StartsWith("\""))
                    {
                        arg = string.Join("\"", oldValue.Split('\"').Skip(2)).Trim();
                    }
                    else if (oldValue.Contains(" "))
                    {
                        arg = string.Join(" ", oldValue.Split(' ').Skip(1));
                    }
                }

                if (string.IsNullOrEmpty(arg))
                    AddToStartup();
                else
                    AddToStartup(arg);
            }
        }
    }
}
