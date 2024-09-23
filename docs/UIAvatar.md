-  **UIAvatar** 
头像

- 默认属性：Symbol
- 默认事件：Click
- 属性列表

| 属性        | 说明     | 类型     |  默认值   |
|-----------|--------|--------|-------|
| Style | 主题样式  | UIStyle  |  Blue     |
| StyleCustomMode | 获取或设置可以自定义主题风格   | bool  | false |
| AvatarSize | 头像大小  | int  | 60 |
| Icon | 显示方式  | UIIcon |  Symbol |
| Symbol| 字体图标  | int  | 61452     |
| SymbolColor | 图标颜色  | Color  | -     |
| SymbolSize| 字体图标大小  | int  | 45 |
| Text  |获取或设置显示的文本  | string | -   | 
| Image| 图片  | Image| -|
| Shape| 显示形状  | UIShape| Circle |
| OffsetX| 水平偏移 | int  | 0|
| OffsetY| 垂直偏移 | int  | 0|
| ForeColor | 字体颜色   | Color  | -   |
| FillColor | 填充颜色   | Color  | -   |
| TagString | 获取或设置包含有关控件的数据的对象字符串   | string | -   | 
| Version | 版本  | string  |  -     |

- 字体图标
![输入图片说明](https://images.gitee.com/uploads/images/2021/0416/232533_5e3bba9d_416720.png "屏幕截图.png")
 设置Symbol属性
![输入图片说明](https://images.gitee.com/uploads/images/2021/0127/213545_4603d7c9_416720.png "11.png")
点击Symbol右侧的按钮：
![输入图片说明](https://images.gitee.com/uploads/images/2021/0127/213636_ee4259fe_416720.png "12.png")
 [[原创][开源] SunnyUI.Net 字体图标 ](https://www.cnblogs.com/yhuse/p/SunnyUI_FontImage.html)https://www.cnblogs.com/yhuse/p/SunnyUI_FontImage.html<br/>

- 主题风格
 **主题**  https://gitee.com/yhuse/SunnyUI/wikis/pages?sort_id=3739705&doc_id=1022550<br/>

- 主题设置
  设置Style属性调用系统自带主题，如果需要自定义颜色，就是更改颜色属性后，把控件的Style设置为Custom，StyleCustomMode设置为True
  StyleCustomMode就是接受用户自定义颜色的意思。

- 显示方式
  设置Icon属性
  显示方式：图片（Image）、字体图标（Symbol）、文字（Text）
![输入图片说明](https://images.gitee.com/uploads/images/2021/0416/232638_fd30df5f_416720.png "屏幕截图.png")

- 显示形状
  设置Shape属性
  圆形（Circle）：
![输入图片说明](https://images.gitee.com/uploads/images/2021/0416/232755_71dc172b_416720.png "屏幕截图.png")
  方形（Square）：
![输入图片说明](https://images.gitee.com/uploads/images/2021/0416/232813_0ae7f901_416720.png "屏幕截图.png")