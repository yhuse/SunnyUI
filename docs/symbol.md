# 字体图标

---

SunnyUI的字体图标目前主要有两个：

- FontAwesome
  [https://fontawesome.com/](https://fontawesome.com/search?m=free&o=r)    
  [https://github.com/FortAwesome/Font-Awesome](https://github.com/FortAwesome/Font-Awesome)    
  
  ![enter image description here](./assets/164019_23b5d08a_416720.png)

  
  
- ElegantIcons.ttf V1.0 https://www.elegantthemes.com/blog/resources/elegant-icon-font
  ![enter image description here](./assets/164032_fea5d167_416720.png)

  
  
  这两个都是目前Web开发常用的字体图标，SunnyUI经过精心研发，将他们用于.Net Winform开发，省去了到处找图标的麻烦。
  SunnyUI常用字体图标的控件为UISymbolButton和UISymbolLabel:
  ![输入图片说明](./assets/a6ea766f_416720.png)
  
  ![输入图片说明](./assets/a13d2418_416720.png)
  
  
  
  字体图标的选择方法是设置UISymbolButton和UISymbolLabel的以下属性：
  Symbol：字体图标（int）
  SymbolSize：字体图标的大小（int）
  ![enter image description here](./assets/164128_b3ef97c6_416720.png)
  
  
  
  点击Symbol右侧的按钮：
  ![输入图片说明](./assets/04b7d3ee_416720.png)
  
  鼠标移到图标上，显示的数字为Symbol字符，点击图标即可设置UISymbolButton的图标。
  
   **.Net6、.Net7，Symbol没有右侧的点击按钮**     
  ![输入图片说明](./assets/104206_49ce26f7_416720.png)
  
  
  暂时认为是.Net6、.Net7的Winform的问题，可参考 [https://github.com/dotnet/winforms/issues/6193](https://github.com/dotnet/winforms/issues/6193)    
  建议将项目的运行环境换成.NetFramework。
  
  另外SunnyUI还有多处用到了字体图标。
  例如：
  ![输入图片说明](./assets/aaeec26f_416720.png)
  
  左侧边栏的UINavMenu的图标，上侧UINavBar的图标，UICombobox的下拉图标，UIDatePicker的日期选择图标等等。