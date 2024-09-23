#  简易的Json静态类
---
-  **Json** 
  简易的Json静态类库，可以在不引用NewtonJson即可简单处理Json对象。  
  当然如果有复杂需求，第三方库还是推荐NewtonJson。  
  另外在Net5，System.Text.Json的性能已经非常不错了，也可以尝试。  

- 原理  
  NetFramework：调用System.Web.Script.Serialization命名空间下的JavaScriptSerializer  
  NetCore：调用System.Text.Json.JsonSerializer  

- 函数  
  ~~~ 
  //将指定的Json字符串input转换为T类型的对象
  public static T Deserialize<T>(string input) 
    
  //将对象obj转换为Json字符串  
  public static string Serialize(object obj)  
  
  //从文件读取字符串转换为T类型的对象    
  public static T DeserializeFromFile<T>(string filename, Encoding encoding)  
  
  //将对象obj转换为Json字符串，并保存到文件  
  public static string SerializeToFile(object obj, string filename, Encoding encoding)  
  ~~~
  
  
  
  
