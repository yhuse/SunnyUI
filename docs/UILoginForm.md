# UILoginForm
---
-  **UILoginForm** 
登录窗体基类。

- 默认属性：Text
- 默认事件：OnLogin
- 属性列表

| 属性        | 说明     | 类型     |  默认值   |
|-----------|--------|--------|-------|
| Style | 主题样式  | UIStyle  |  Blue     |
| StyleCustomMode | 获取或设置可以自定义主题风格   | bool  | false |
| Title | 顶部标题  | string | -   | 
| SubText | 底部文字描述  | string | -    |
| LoginImage | 背景图片  | UILoginImage |  - |
| UserName | 用户名 | string |  -|
| Password | 密码 | string |  -|
| IsLogin | 登录是否成功| bool |  -|
| Version | 版本  | string  |  -     |
| TagString | 获取或设置包含有关控件的数据的对象字符串   | string | -   | 



- 事件  
  ButtonLoginClick：确定按钮点击事件，有此事件时不执行OnLogin。需要手动给IsLogin赋值。  
  ButtonCancelClick：取消按钮点击事件。  
  OnLogin：登录事件。ButtonLoginClick为空时才执行此时间，返回值为IsLogin。  

  

- 创建窗体    
  项目引用SunnyUI.dll和SunnyUI.Common.dll，或者从Nuget引用SunnyUI。详见[安装](/install)  
  创建窗体：参考[UIForm](/UIForm)创建窗体  
  切记把窗体的AutoScaleMode从Font设置为None，否则可能出现因为屏幕分辨率而导致的窗体变形。

  

- 为什么继承的窗体，上面有小锁，能解开吗，还有就是继承的窗体，有些控件属性都设置不了，怎么办？
  首先得了解窗体继承的概念，和类的继承是差不多的，窗体上有小锁的其实就是类似于类继承中父类的某个Private属性
  为了保证继承窗体的UI设计，上面的某些控件是不让修改和移动的，所以有锁。
  在使用时，父窗体一般都将其所用的控件的属性和事件进行了封装，可以正常使用。
  举例：
    ![输入图片说明](./assets/011a9a81_416720.png)

  

- 继承的登录窗体的登录按钮有锁，但其点击事件已封装到父类的事件。
  需要选中窗体，查看其事件：
  ![输入图片说明](./assets/105850_f7800c4b_416720.png)

  

- 通过代码创建
  也可以不用创建窗体，直接通过代码创建，来实现登录过程

```c#
    UILoginForm frm = new UILoginForm();
    frm.ShowInTaskbar = true;
    frm.Text = "Login";
    frm.Title = "SunnyUI.Net Login Form";
    frm.SubText = Version;
    frm.OnLogin += Frm_OnLogin;
    frm.LoginImage = UILoginForm.UILoginImage.Login2;
    frm.ShowDialog();
    if (frm.IsLogin)
    {
        UIMessageTip.ShowOk("登录成功");
    }

    frm.Dispose();
```
  在Frm_OnLogin事件里通过与数据库比对或者调用接口判断登录是否成功。例如：  
```c#
    private bool Frm_OnLogin(string userName, string password)
    {
        return userName == "admin" && password == "admin";
    }
```