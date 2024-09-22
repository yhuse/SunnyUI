# Ini文件读写类
---
-  **IniFile** 
  Ini文件读写类，基类为IniBase，已经处理过中文读写。
  **建议读写的ini文件为IniFile存储的文件。以免造成文件编码不统一。** 

- Ini文件格式
  程序没有任何配置文件，那么它对外是全封闭的，一旦程序需要修改一些参数必须要修改程序代码本身并重新编译，为了让程序出厂后还能根据需要进行必要的配置，所以要用配置文件；配置文件有很多种，如INI配置文件，XML配置文件，cfg配置文件，还有就是可以使用系统注册表等。  
  INI ”就是英文 “initialization”的头三个字母的缩写；当然INI file的后缀名也不一定是".ini"也可以是".cfg"，".conf ”或者是".txt"。  
  INI文件的格式很简单，最基本的三个要素是：parameters，sections和comments。  
  
  什么是parameters？  
  INI所包含的最基本的“元素”就是parameter；每一个parameter都有一个name和一个value，如下所示：  
  name = value  
  
  什么是sections？   
  所有的parameters都是以sections为单位结合在一起的。所有的section名称都是独占一行，并且sections名字都被方括号包围着（[ and ])。在section声明后的所有parameters都是属于该section。对于一个section没有明显的结束标志符，一个section的开始就是上一个section的结束，或者是end of the file。Sections一般情况下不能被nested，当然特殊情况下也可以实现sections的嵌套。 
  section如下所示：  
  [section]  
  
  什么是comments？  
  在INI文件中注释语句是以分号“；”开始的。所有的所有的注释语句不管多长都是独占一行直到结束的。在分号和行结束符之间的所有内容都是被忽略的。  
  注释实例如下：  
  ；comments text  

- IniFile类支持的数据类型
  Windows API（WritePrivateProfileString）支持的类型为string类型，通过类型转换扩展了以下类型：  
  bool，byte，byte[]，char，Color，Datetime，decimal，double，float，int，  
  long，Point，PointF，sbyte，short，Size，SizeF，uint，ulong，ushort，  
  Struct*

- 写文件
~~~
  IniFile ini = new IniFile("D:\\setup.ini");
  ini.Write("Setup", "Name", "Sunny");
  ini.Write("Setup", "Age", 18);
  ini.UpdateFile();
~~~
  ini配置文件被写到D:\setup.ini    
   **注意，ini文件名必须是完全路径，不能是相对路径**    
  如果需要在程序可执行目录下生成ini文件，可用以下方法：    
~~~
  IniFile ini = new IniFile(DirEx.CurrentDir() + "Setup.ini");
~~~
  打开此文件
~~~
;<!--配置文件-->
[Setup]
Name=Sunny
Age=18
~~~
  注意，如果写的文件在C盘，系统为win7以上，请确保所运行的程序有相应的权限可以在C盘文件夹写文件。
  Write函数主要包括三个参数，分别对应Ini格式的section，name，value

- 读文件
~~~
  IniFile ini = new IniFile("D:\\setup.ini");
  string name = ini.ReadString("Setup", "Name", "");
  int age = ini.ReadInt("Setup", "Age", 0);
~~~
  Read函数主要包括三个参数，分别对应Ini格式的section，name，Default（如配置文件有值则从配置文件取值，如没有取Default值）

- 其他函数  
  获取指定的Section名称中的所有Key  
  public string[] GetKeys(string section)    
  
  从Ini文件中，读取所有的Sections的名称  
  public string[] Sections  
  
  读取指定的Section的所有Value到列表中  
  public void GetSectionValues(string section, NameValueCollection values)  
  
  清除某个Section  
  public void EraseSection(string section)  
  
  删除某个Section下的键  
  public void DeleteKey(string section, string key)  
  
  检查某个Section下的某个键值是否存在  
  public bool KeyExists(string section, string key)  