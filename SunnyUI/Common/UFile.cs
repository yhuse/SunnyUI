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
 * 文件名称: UFile.cs
 * 文件说明: 文件扩展类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// 文件扩展类
    /// </summary>
    public static class FileEx
    {
        /// <summary>
        /// 调用WINAPIf复制文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="targetFileName"></param>
        /// <param name="bFailIfExists"></param>
        /// <returns></returns>
        public static bool Copy(this string sourceFileName, string targetFileName, bool bFailIfExists = true)
        {
            if (File.Exists(sourceFileName))
            {
                return Win32.Kernel.CopyFile(sourceFileName, targetFileName, bFailIfExists);
            }

            return false;
        }

        /// <summary>
        /// 调用WINAPI删除文件
        /// </summary>
        /// <param name="lpFileName">文件</param>
        public static int DeleteFile(string lpFileName)
        {
            return Win32.Kernel.DeleteFile(lpFileName);
        }

        /// <summary>
        /// 打开文件夹对话框
        /// </summary>
        /// <param name="filename">返回True时，获取文件名</param>
        /// <param name="filter">过滤器</param>
        /// <param name="defaultExt">默认文件扩展名</param>
        /// <returns>打开是否成功</returns>
        public static bool OpenDialog(ref string filename, string filter = "", string defaultExt = "")
        {
            using OpenFileDialog od = new OpenFileDialog { Title = UILocalize.Open };

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
        public static bool SaveDialog(ref string filename, string filter = "", string defaultExt = "")
        {
            using SaveFileDialog od = new SaveFileDialog { Title = UILocalize.Save };
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
        /// 关闭进程
        /// </summary>
        /// <param name="filename">文件名</param>
        public static void CloseRunFile(string filename)
        {
            Process[] processes = Process.GetProcesses();
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processes)
            {
                if (!string.Equals(process.ProcessName, filename, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                process.Kill();
                process.Close();
            }
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        /// <param name="commandText">命令</param>
        /// <returns>结果</returns>
        public static string RunExeCommand(string commandText)
        {
            Process p = new Process(); //创建并实例化一个操作进程的类：Process
            p.StartInfo.FileName = "cmd.exe"; //设置要启动的应用程序
            p.StartInfo.UseShellExecute = false; //设置是否使用操作系统shell启动进程
            p.StartInfo.RedirectStandardInput = true; //指示应用程序是否从StandardInput流中读取
            p.StartInfo.RedirectStandardOutput = true; //将应用程序的输入写入到StandardOutput流中
            p.StartInfo.RedirectStandardError = true; //将应用程序的错误输出写入到StandardError流中
            p.StartInfo.CreateNoWindow = true; //是否在新窗口中启动进程
            string strOutput;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText); //将CMD命令写入StandardInput流中
                p.StandardInput.WriteLine("exit"); //将 exit 命令写入StandardInput流中
                strOutput = p.StandardOutput.ReadToEnd(); //读取所有输出的流的所有字符
                p.WaitForExit(); //无限期等待，直至进程退出
                p.Close(); //释放进程，关闭进程
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }

            return strOutput;
        }

        /// <summary>
        /// 获取不重复的临时文件名，createZeroSizeFile为真时创建0字节的文件
        /// </summary>
        /// <param name="createZeroSizeFile">是否创建文件</param>
        /// <returns>文件名</returns>
        public static string TempFileName(bool createZeroSizeFile = true)
        {
            string path = Path.GetTempFileName();
            if (!createZeroSizeFile)
            {
                File.Delete(path);
            }

            return path;
        }

        /// <summary>
        /// 运行文件，当文件已经运行时调至前台
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="bringToFrontIfRun">运行时调至前台</param>
        public static Process RunExe(this FileInfo file, bool bringToFrontIfRun = true)
        {
            if (file == null || !file.Exists)
            {
                return null;
            }

            if (bringToFrontIfRun)
            {
                foreach (Process oth in Process.GetProcesses())
                {
                    if (!string.Equals(oth.ProcessName, file.Name.Replace(file.Extension, ""), StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }

                    IntPtr hWnd = oth.MainWindowHandle;
                    Win32.User.ShowWindowAsync(hWnd, (int)ProcessWindowStyle.Maximized);
                    Win32.User.SetForegroundWindow(hWnd);
                    return oth;
                }
            }

            if (file.Directory != null)
            {
                Directory.SetCurrentDirectory(file.Directory.FullName.DealPath());
            }

            return Process.Start(file.FullName);
        }

        /// <summary>
        /// 文件信息
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>结果</returns>
        public static FileInfo FileInfo(string filename)
        {
            return File.Exists(filename) ? new FileInfo(filename) : null;
        }

        /// <summary>
        /// 程序是否为32位程序
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>结果</returns>
        public static bool IsPE32(this FileInfo file)
        {
            FileStream stream = File.OpenRead(file.FullName);
            //移动到 e_lfanew 的位置处
            stream.Seek(0x40 - 4, SeekOrigin.Begin);
            byte[] buf = new byte[4];
            stream.Read(buf, 0, buf.Length);
            //根据 e_lfanew 的值计算出Machine的位置
            int pos = BitConverter.ToInt32(buf, 0) + 4;
            stream.Seek(pos, SeekOrigin.Begin);
            buf = new byte[2];
            stream.Read(buf, 0, buf.Length);
            stream.Close();
            //得到Machine的值，0x14C为32位，0x8664为64位
            short machine = BitConverter.ToInt16(buf, 0);
            return machine == 0x14C;
        }

        /// <summary>
        /// 可执行文件是否运行
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>是否运行</returns>
        public static bool IsRun(this FileInfo file)
        {
            return file.Exists && Process.GetProcesses().Any(oth => string.Equals(oth.ProcessName, file.NameWithoutExt(), StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 可执行文件是否运行
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否运行</returns>
        public static bool IsRun(string filename)
        {
            FileInfo info = FileInfo(filename);
            return info == null ? false : info.IsRun();
        }

        /// <summary>
        /// 获取无后缀的文件名
        /// </summary>
        /// <param name="file">文件信息</param>
        /// <returns>文件名</returns>
        public static string NameWithoutExt(this FileInfo file)
        {
            return file == null ? string.Empty : file.Name.Replace(file.Extension, "");
        }

        /// <summary>
        /// 判断一个文件是否正在使用
        /// </summary>
        /// <param name="file">要判断文件的文件名</param>
        /// <returns> bool</returns>
        public static bool IsUse(this FileInfo file)
        {
            if (file == null || !file.Exists)
            {
                return false; //文件不存在则一定没有被使用
            }

            bool inUse = true;
            try
            {
                using (new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    inUse = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString(CultureInfo.InvariantCulture));
            }

            return inUse; //true表示正在使用,false没有使用
        }

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

        /// <summary>
        /// GZip压缩文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="outfile">输出</param>
        public static void GZipCompress(this FileInfo file, string outfile)
        {
            if (file == null || !file.Exists)
            {
                return;
            }

            // Get the stream of the source file.
            using (FileStream inFile = file.OpenRead())
            {
                // Prevent compressing hidden and already compressed files.
                if (!((File.GetAttributes(file.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & file.Extension != ".gz"))
                {
                    return;
                }

                // Create the compressed file.
                using (FileStream outFile = File.Create(outfile))
                {
                    using (GZipStream compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        // Copy the source file into the compression stream.
                        inFile.CopyTo(compress);
                    }
                }
            }
        }

        /// <summary>
        /// GZip解压缩文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="outfile">输出</param>
        public static void GZipDecompress(this FileInfo file, string outfile)
        {
            if (file == null || !file.Exists)
            {
                return;
            }

            // Get the stream of the source file.
            using (FileStream inFile = file.OpenRead())
            {
                // Get original file extension, for example
                // "doc" source report.doc.gz.
                string origName = outfile;
                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        // Copy the decompression stream into the output file.
                        decompress.CopyTo(outFile);
                    }
                }
            }
        }

        /// <summary>
        /// 加到主界面中初始化中，通过注册文件类型调用程序打开时，判断文件名并添加处理代码
        /// </summary>
        /// <returns>结果</returns>
        public static string GetCommandFile()
        {
            string command = Environment.CommandLine; //获取进程命令行参数
            string[] para = command.Split(new[] { '"' }, StringSplitOptions.RemoveEmptyEntries);
            //当打开文件的时候自动加载相关配置
            return para.Length >= 3 ? para[2] : string.Empty;
        }

        /// <summary>
        /// 尝试删除文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>结果</returns>
        public static bool TryDelete(string file)
        {
            FileInfo info = FileInfo(file);
            return info == null ? false : info.TryDelete();
        }

        /// <summary>
        /// 尝试删除文件
        /// </summary>
        /// <param name="fi">文件名</param>
        /// <returns>结果</returns>
        public static bool TryDelete(this FileInfo fi)
        {
            try
            {
                File.Delete(fi.FullName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>结果</returns>
        public static FileStream ReadStream(this FileInfo file)
        {
            FileStream fileStream = null;
            if (file.Exists)
            {
                fileStream = new FileStream(file.FullName, FileMode.Open);
                fileStream.Position = 0;
            }

            return fileStream;
        }

        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>结果</returns>
        public static bool CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!File.Exists(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);
                    //创建文件
                    FileStream fs = file.Create();
                    //关闭文件流
                    fs.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        /// <returns>结果</returns>
        public static bool CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!File.Exists(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);
                    //创建文件
                    FileStream fs = file.Create();
                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);
                    //关闭文件流
                    fs.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>结果</returns>
        public static int LineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中，返回行数
            return File.ReadAllLines(filePath).Length;
        }

        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="info">文件的绝对路径</param>
        /// <returns>结果</returns>
        public static int LineCount(this FileInfo info)
        {
            //将文本文件的各行读到一个字符串数组中，返回行数
            return File.ReadAllLines(info.FullName).Length;
        }

        private static readonly uint[] Crc32Table =
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA,
            0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
            0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,
            0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
            0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
            0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
            0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,
            0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
            0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,
            0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940,
            0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
            0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116,
            0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
            0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
            0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
            0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A,
            0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
            0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818,
            0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
            0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
            0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C,
            0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
            0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
            0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
            0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,
            0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
            0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086,
            0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4,
            0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
            0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,
            0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
            0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
            0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
            0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE,
            0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
            0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,
            0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252,
            0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
            0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60,
            0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
            0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
            0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
            0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04,
            0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
            0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,
            0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,
            0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
            0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E,
            0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
            0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
            0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
            0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,
            0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
            0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0,
            0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6,
            0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
            0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
            0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
        };

        /// <summary>
        /// 获取文件的CRC32标识
        /// </summary>
        /// <param name="fi">文件信息</param>
        /// <returns>CRC</returns>
        public static string CRC32(this FileInfo fi)
        {
            const string FOO = "-";
            if (!fi.Exists)
            {
                return FOO;
            }

            // 最大50M
            const int MAX_SIZE = 50 * 1024 * 1024;

            if (fi.Length >= MAX_SIZE)
            {
                return FOO;
            }

            uint crc = 0xFFFFFFFF;
            var bin = File.ReadAllBytes(fi.FullName);
            foreach (byte b in bin)
            {
                crc = ((crc >> 8) & 0x00FFFFFF) ^ Crc32Table[(crc ^ b) & 0xFF];
            }

            crc ^= 0xFFFFFFFF;
            return crc.ToString("X").PadLeft(8, '0');
        }

        /// <summary>
        /// 文件名是否有效
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns>是否有效</returns>
        public static bool IsValidFileName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return false;
            }

            string[] errorStr = { "/", "\\", ":", ",", "*", "?", "\"", "<", ">", "|" };
            foreach (var str in errorStr)
            {
                if (name.Contains(str))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取文件的Encoding
        /// </summary>
        private static Encoding GetEncoding(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using (var reader = new StreamReader(filename, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) // you need this!
                    reader.Read();

                return reader.CurrentEncoding;
            }
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