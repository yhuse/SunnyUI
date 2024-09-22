# Ini配置文件类

---

看过 [IniFile - Ini文件读写类 ](/IniFile)，应该基本了解Ini文件的定义和读写了。  

- 写文件  
~~~
  IniFile ini = new IniFile("D:\\setup.ini");
  ini.Write("Setup", "Name", "Sunny");
  ini.Write("Setup", "Age", 18);
  ini.UpdateFile();  
~~~
- 读文件  
~~~
  IniFile ini = new IniFile("D:\\setup.ini");
  string name = ini.ReadString("Setup", "Name", "");
  int age = ini.ReadInt("Setup", "Age", 0);
~~~

可这样看起来似乎还是比较麻烦，得知道配置文件的位置，正确填写section，name的字符串值。  
有没有一个类，只要写了类的属性就可以直接用，而不需要关注读写。  
**IniConfig**就是这样的一个类，下面简单的介绍下IniConfig的用法。  

- 原理  
  通过反射读取和设置类的属性的值，并通过ini格式文件进行存储。  
  
- 示例  
有这样一个配置文件，保存服务器的地址和端口，软件名称，以及软件中显示天气需要的城市名称。  
配置文件类代码如下：  
~~~
    [ConfigFile("Config\\Setting.ini")]
    public class Setting : IniConfig<Setting>
    {
        [ConfigSection("Hello")]
        public string SoftName { get; set; }

        public string ServerIP { get; set; }

        public int ServerPort { get; set; }

        public string City { get; set; }

        public override void SetDefault()
        {
            base.SetDefault();
            SoftName = "XX软件";
            ServerIP = "192.168.1.2";
            ServerPort = 9090;
            City = "南京";
        }
    }
~~~
[ConfigFile("Config\\Setting.ini")]  
配置ini文件的位置，当前程序目录下Config目录下的Setting.ini。
*目录不存在时会自动创建目录 

[ConfigSection("Hello")]
设置SoftName所在的节点Section的名称，如果不设置则默认为Setup

public override void SetDefault()
当第一次运行时配置文件不存在时，设置配置的默认值，并保存至文件。

- 读取  
读取系统配置，并开始应用：  
~~~
        Setting.Current.Load();

        TcpClient client = new TcpClient();
        client.Connect(Setting.Current.ServerIP, Setting.Current.ServerPort);
~~~
Setting.Current.Load();  
读取配置信息，将配置文件Setting.ini里的值读取到类的属性中。  
这样Setting.Current.ServerIP和Setting.Current.ServerPort就可以直接用了。  
如果需要修改配置，修改Setting.ini，重新读取就可以了。
**注意：配置读取，属性的应用，都是用的Setting.Current，而不是Setting** 

- 保存  
系统修改配置，并保存到配置文件。  
例如系统中修改了获取天气的城市为重庆：  
~~~
        Setting.Current.City = "重庆";
        Setting.Current.Save();
~~~
修改Setting.Current的属性，并通过Setting.Current.Save()保存即可。  

- 其他  
通过IniConfig配置文件的应用，屏蔽ini文件常用的通过section，name的字符串值来获取参数的繁琐过程。   
1、建议读写的ini文件为IniConfig存储的文件。以免造成文件编码不统一。即先生成这个文件，再进行修改，而不是手动创建配置文件。    
2、配置读取，保存类的应用，都是用的Setting.Current，而不是Setting。   
3、支持的属性类型见[IniFile - Ini文件读写类 ](/IniFile)

