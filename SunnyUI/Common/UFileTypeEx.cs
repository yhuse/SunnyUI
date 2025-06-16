using Microsoft.Win32;

namespace Sunny.UI
{
    public static class FileTypeEx
    {
        /// <summary>
        /// 使文件类型与对应的图标及应用程序关联起来。
        /// </summary>
        /// <param name="regInfo">regInfo</param>
        public static void RegisterFileType(this FileTypeRegInfo regInfo)
        {
            if (regInfo.FileTypeRegistered())
            {
                return;
            }

            string relationName = regInfo.ExtendName.Right(regInfo.ExtendName.Length - 1).ToUpper() + "_FileType";

            RegistryKey fileTypeKey = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);
            if (fileTypeKey != null)
            {
                fileTypeKey.SetValue("", relationName);
                fileTypeKey.Close();
            }

            RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(relationName);
            if (relationKey == null)
            {
                return;
            }

            relationKey.SetValue("", regInfo.Description);

            RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");
            if (iconKey != null)
            {
                iconKey.SetValue("", regInfo.IcoPath);
                iconKey.Close();
            }

            RegistryKey shellKey = relationKey.CreateSubKey("Shell");
            if (shellKey != null)
            {
                RegistryKey openKey = shellKey.CreateSubKey("Open");
                if (openKey != null)
                {
                    RegistryKey commandKey = openKey.CreateSubKey("Command");
                    if (commandKey != null)
                    {
                        commandKey.SetValue("", regInfo.ExePath + " %1");
                        commandKey.Close();
                    }

                    openKey.Close();
                }

                shellKey.Close();
            }

            relationKey.Close();
        }

        /// <summary>
        /// 得到指定文件类型关联信息
        /// </summary>
        /// <param name="regInfo">regInfo</param>
        /// <returns>结果</returns>
        public static FileTypeRegInfo GetFileTypeRegInfo(this FileTypeRegInfo regInfo)
        {
            string relationName = regInfo.ExtendName.Right(regInfo.ExtendName.Length - 1).ToUpper() + "_FileType";
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName);
            if (relationKey == null)
            {
                return regInfo;
            }

            regInfo.Description = relationKey.GetValue("").ToString();

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
            if (iconKey != null)
            {
                regInfo.IcoPath = iconKey.GetValue("").ToString();
                RegistryKey shellKey = relationKey.OpenSubKey("Shell");
                if (shellKey != null)
                {
                    RegistryKey openKey = shellKey.OpenSubKey("Open");
                    if (openKey != null)
                    {
                        RegistryKey commandKey = openKey.OpenSubKey("Command");
                        if (commandKey != null)
                        {
                            string temp = commandKey.GetValue("").ToString();
                            regInfo.ExePath = temp.Left(temp.Length - 3);
                            commandKey.Close();
                        }

                        openKey.Close();
                    }

                    shellKey.Close();
                }

                iconKey.Close();
            }

            relationKey.Close();
            return regInfo;
        }

        /// <summary>
        /// FileTypeRegistered 指定文件类型是否已经注册
        /// </summary>
        /// <param name="regInfo">regInfo</param>
        /// <returns>结果</returns>
        public static bool FileTypeRegistered(this FileTypeRegInfo regInfo)
        {
            FileTypeRegInfo info = new FileTypeRegInfo(regInfo.ExtendName).GetFileTypeRegInfo();
            return info != null && info.ExePath == regInfo.ExePath;
        }
    }


    /// <summary>
    /// 文件类型注册信息
    /// </summary>
    public class FileTypeRegInfo
    {
        /// <summary>
        /// 目标类型文件的扩展名，例如".xcf"
        /// </summary>
        public string ExtendName;

        /// <summary>
        /// 目标文件类型说明，例如"XCodeFactory项目文件"
        /// </summary>
        public string Description;

        /// <summary>
        /// 目标类型文件关联的图标
        /// </summary>
        public string IcoPath;

        /// <summary>
        /// 打开目标类型文件的应用程序
        /// </summary>
        public string ExePath;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileTypeRegInfo()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="extendName">扩展名</param>
        public FileTypeRegInfo(string extendName)
        {
            ExtendName = extendName;
        }
    }
}
