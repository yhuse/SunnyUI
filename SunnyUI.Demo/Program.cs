using System;
using System.Windows.Forms;

namespace Sunny.UI.Demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //增加一个繁体中文的内置资源配置
            UIStyles.BuiltInResources.TryAdd(CultureInfos.zh_TW.LCID, new zh_TW_Resources());
            //从项目方案生成多语言配置文件，生成文件在可执行文件夹的Language目录下
            //生成后界面没有修改的情况下，可注释掉下一行，只运行一次即可
            //TranslateHelper.LoadCsproj(@"D:\Source\SunnyUI\SunnyUI.Demo\SunnyUI.Demo.csproj");

            Application.Run(new FMain());
        }
    }
}
