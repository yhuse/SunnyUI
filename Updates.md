\+ 增加    \* 修改    \- 删除    
    
#### 2023\-10\-27 V3.5.2    
\+  **字体图标增加旋转角度参数SymbolRotate**     
\* UINavBar: 修正下拉菜单文字显示垂直居中    
\* UINavBar: ImageList为空时，下拉菜单增加Symbol绘制    
\* UIContextMenuStrip: 修正文字显示垂直居中    
\* UIContextMenuStrip: 当右键菜单未绑定ImageList，并且ImageIndex>0时，将ImageIndex绑定为Symbol绘制    
\* UIIPTextBox: 修复UIIPTextBox在Readonly且光标位于小数点后面时，可以通过Backspace键删除前一位的问题    
\* UILineChart: 增加了绘制线的DashStyle样式    
\* UFontImage: MaterialIcons字体图标调大3个像素    
\* UITextBox: 修复在高DPI下，文字垂直不居中的问题    
\* UITextBox: 修复在某些字体不显示下划线的问题    
\* UITextBox: 多行显示时位置调整     
\* UIDropControl: 修复在高DPI下，文字垂直不居中的问题    
\* UIDropControl: 修复在某些字体不显示下划线的问题    
    
#### 2023\-10\-16 V3.5.1    
\+ 增加.Net8支持        
\+ 增加MaterialIcons字体图标    
\* 重构进度提示框和等待提示框，解决有时无法关闭的问题，并作为窗体的扩展方法使用    
\* 重构字体图标类    
\* UIForm: 增加一个在窗体显示后延时执行的事件    
\* IHideDropDown: 修改关闭下拉弹窗可能引起的出错    
\* UILineChart: 增加了X轴和Y轴鼠标选择区域并返回选中范围    
\* UILineChart: 增加了Y轴数据由上向下绘制    
\* UIComboDataGridView: 增加ClearFilter，可以清除弹窗的搜索栏文字    
    
#### 2023\-09\-19 V3.5.0   
 **注意：大版本更新，SunnyUI字体改为系统默认字体（中文Windows常见字体为宋体）**     
建议新项目使用此版本，如果3.5.0以下版本需要升级参照文档设置全局字体：    
[https://gitee.com/yhuse/SunnyUI/wikis/多页面框架/全局字体设置](https://gitee.com/yhuse/SunnyUI/wikis/%E5%A4%9A%E9%A1%B5%E9%9D%A2%E6%A1%86%E6%9E%B6/%E5%85%A8%E5%B1%80%E5%AD%97%E4%BD%93%E8%AE%BE%E7%BD%AE)   
 
\+ 重构全局字体设置逻辑，可以在运行时设置全局字体    
\+ 增加全局矩形设计，忽略圆角，UIStyleManager的GlobalRectangle属性    
\* UITextBox: 修复了Enabled为false时，自定义颜色，文字不显示的问题    
\* UIDropControl: 修改背景色后编辑框颜色修复    
\* UIStyles: 修改全局字体为系统默认：System.Drawing.SystemFonts.DefaultFont    
\* UILight: 恢复全局矩形设计时圆形灯光效果    
\* UIDropControl: 下拉框按钮图标增加编辑器    
\* UIProcessBar: 修复值计算过程中的Int越界问题    
\* UIBreadcrumb: 增加Readonly，禁用鼠标点击，可通过代码设置ItemIndex    
    
#### 2023\-08\-18 V3.4.2    
\* UIGroupBox: 解决BackColor,FillColor设置为透明时，文本位置与边框线重叠的问题    
\* UIForm: 修复页面切换时，第一个UIPage未执行Final事件的问题    
\* UIComboDataGridView: 过滤输入后，按键盘下键切换至DataGridView，选中数据后按回车可快捷选中数据    
\* UIChart: Legend增加一种绘制直线的方法    
\* UIForm: 默认提示弹窗TopMost为true    
\* UIFormHelper: 默认提示弹窗TopMost为true    
\* UIEditForm: 默认弹窗TopMost为true    
\* UIFormHelper: 提问弹窗增加可默认是确认或者取消按钮的选择    
\* UITitlePanel: 增加OnCollapsed事件    
\* UIniFile: 增加提示文件名必须为绝对路径，不能用相对路径    
\* UIniFile: 增加了文件绝对路径判断和文件夹是否存在判断    
\* UIComboBox: Items.Clear后，DropDownStyle为DropDown时，不清空Text    
\* UILineChartOption: 增加了GetDataPoint，可获取曲线上的数据值    
\* UITextBox: 修复了Enabled为false时，字体大小调整后，文字显示位置的问题    
    
#### 2023\-07\-18 V3.4.1    
\* UIGroupBox: 解决BackColor,FillColor设置为透明时，标题下面会出现横线    
\* UIDataGridView: 修复了有冻结行时垂直滚动条点击时出错的问题    
\* UITitlePanel: 删除Padding设置    
\* UIRoundProcess: 内圈尺寸小的时候更新配色    
\* UILineChart: 增加了坐标轴绘制时显示箭头，并在箭头处显示数量单位的功能    
\* UIRoundProcess: 增加起始角度和扫描角度    
\* UITextBox: 修复了Enabled为false时，PasswordChar失效的问题    
\+ UCompress: 增加数据帧压缩类    
    
#### 2023\-07\-09 V3.4.0    
\+ UITurnSwitch: 增加旋转开关    
\+ ScanerHook: 增加扫码枪键盘钩子类    
\* UITurnSwitch: 调整配色，增加自定义角度    
\* UIniFile: 将文件版本和文件编码写入文件头部    
\* UITextBox: 增加Enabled为false时，可修改文字颜色    
\* UIDropControl: 增加了下拉框控件的主动释放    
\* UIComboBox: 修改了几个对象的释放    
\* UILineChart: 增加了数据沿Y轴变化时鼠标移动到数据点时显示数据点标签    
\* UILineChart: 增加PointFormat，鼠标选中值显示格式化事件    
\* UITreeView: 屏蔽DrawMode属性，默认为OwnerDrawAll    
\* UIButton: 渐变色增加方向选择    
\* UIUserControl: 渐变色增加方向选择    
\* UIDataGridView: 增加了可设置水平滚动条宽度的属性，但可能会遮挡最下面数据行的数据，看情况使用    
\* UIComboBox: 增加过滤时忽略大小写    
\* UIMiniPagination: 内置按钮关联值由Tag改为TagString    
\* UIRadioButtonGroup: 内置条目关联值由Tag改为TagString    
    
#### 2023\-06\-14 V3.3.9    
\* UIComboTreeView: 修复使用清空按钮后，再次打开下拉框，上次的选择内容还是存在    
\* UIEdit: 修复输入范围判断的问题    
    
#### 2023\-06\-06 V3.3.8    
\+ **支持全局字体设置，UIStyleManager的GlobalFont属性设置为true后，设置GlobalFontName属性**     
\+ **支持全局文字缩放比例设置，UIStyleManager的GlobalFont属性设置为true后，设置GlobalFontScale属性**    
\* UILineChart: 修复X轴文字重叠问题    
\* UIBarChart: 修复Y轴文字居中    
\* UINavMenu: 增加PageGuid相关扩展方法    
\* UIVerificationCode: 修改字体缩放时显示    
\* UITransfer: 增加列表框字体可调整    
\* UIStyle: 更新获取Font.GdiCharSet 属性的方法    
\* UFontImage: 修复了一个窗体属性编辑器图标显示不全的问题    
\* 重构一些笔刷和画笔的资源释放    
\* 重构一些控件的Dispose事件    
\* 重构字体    
    
#### 2023\-05\-16 V3.3.7    
\* 重构DrawFontImage函数    
\* 重构MeasureString    
\* 重构DrawString函数    
\* UIDatePicker: 年、年月、年月日可单独设置格式化掩码    
\* UIDatePicker: 修复文字格式化显示问题    
\* UILineChart: 增加了一种开关量曲线的显示方式    
\* UIBarChart: Option.BarInterval,设置Bar之间间隔，默认-1，自动计算间隔    
\* UITitlePanel: 标题栏文字位置属性由TextAlign改为TextAlignment    
\* UIBarChart: Option.ShowFullRect为true时，绘制右侧和上侧的边框实线    
\* UIIPTextBox: 修复Enabled切换时背景色不一致的问题    
\* UIDropControl: 最小高度限制，以防丢失边框    
\* UILineChartOption: 增加了UpdateYData函数，按序号更新Y轴值    
\* UIImageButton: 增加调用点击事件PerformClick    
\* UITitlePanel: 增加了一个关闭按钮的属性，点击后隐藏控件    
    
#### 2023\-04\-24 V3.3.6    
\*  **FontAwesome: 字体图标从V5.15.2升级到V6.4.0**     
\* FDialogs: 增加了等待提示窗的示例    
\* UIEdit: 修复了最大值最小值范围判断的问题    
\* UICheckBoxGroup: 设置选择项ForeColor    
\* UILoginForm: 增加可选择显示时激活的控件    
\* UIChartOption: 图表的一些参数设置为只读或者私有设置    
\* UIRadioButtonGroup: 设置选择项ForeColor    
\* UILineChart: 打开Smooth绘制，建议数据差距不大时可平滑绘制    
\* UIFontImages: 增加搜索结果显示页面    
\* UIEditForm: 代码生成增加，Double类型增加小数点位数    
\* UISwitch: 增加ActiveChanging事件，在状态改变前可以进行判断    
    
#### 2023\-04\-08 V3.3.5    
\*  **UMessageTip: 解决了Release模式下GDI位图未释放的Bug**     
\* Demo: 重写FMain，从UIForm继承       
\* UITreeView: 修改LabelEdit属性    
\* 内置的一些容器增加关闭过滤下拉框的点击事件    
\* UINumPadTextBox: 增加了最大值、最小值等属性    
\* UGraphics: 重构了一遍绘图方法    
\* UIComboDataGridViewItem: 增加多语翻译    
\* UIComboTreeView: 显示清除按钮    
\* UControl: 修复关闭弹窗null的Bug    
\* UIDropControl: DropDownList时，显示水印文字    
       
#### 2023\-03\-26 V3.3.4   
\+ UINumPadTextBox: 增加模拟数字键盘输入框    
\- UIIntegerUpDown: 删除ForbidInput属性，使用Inputable属性    
\- UIDoubleUpDown: 删除ForbidInput属性，使用Inputable属性    
\* UIEdit: 修复了删除为空时小数位数和设置值不一致的问题    
\* UIPage: 调整了页面切换时Final和Init的顺序    
\* UITreeView: 增加MouseDoubleClick和MouseClick事件    
\* UIComboBox: 修改失去焦点自动关闭过滤下拉框    
\* UIPage: 重新梳理页面加载顺序    
\* UILineChart: 自定义X轴坐标时，点数据提示显示为原始值    
\* UIDateItem: 修复下拉框鼠标移动位置的Bug    
\* UINumPadTextBox: 增加默认事件ValueChanged，下键盘Enter事件相应此事件    
    
#### 2023\-03\-05 V3.3.3    
\+ UIMiniPagination: 新增迷你分页控件，只有分页按钮，无其他    
\* UINavMenu: 有子节点时，鼠标左键点击父级点展开/收缩，右键选中    
\* UITextBox: 有水印时，系统响应触摸屏增加了TouchPressClick属性，默认关闭    
\* FastLZ: 修改加载库    
\* UINavMenu: 增加可重绘DrawNode    
\* UIDataGridView: 加了一个简洁显示的模式    
\* 增加了Windows里阻止系统电源选项里的系统睡眠，屏幕关闭API    
\* UINavBar: 去除下拉菜单宽度调整    
\* UIScrollingText: 重写滚动逻辑    
\* UIPipe: 修复了管道宽度调大后水流不显示的问题    
\* UIForm: 修复PageSelected可能未显示选中页面的问题    
\* UIPage: 增加PageDeselecting，取消页面选择时增加判断    
\* UIPage: 取消设计期的Dock.Fill，改为运行时设置    
\* UIDropControl: 下拉框类型的ClearButton在尺寸变化时，重新计算位置    
    
#### 2023\-02\-07 V3.3.2    
\+ UIControl: 增加WIN10系统响应触摸屏的按下和弹起事件    
\* UIDropControl: 下拉框控件增加Tips小红点    
\* UITextBox: 增加Tips小红点    
\* UIComboBox: 增加清除按钮    
\* UIComboTreeView: 下拉框增加显示全选选择框    
\* UITransfer: 支持鼠标框选和Shift，Ctrl多选移动    
\* UINavMenu: 修复了鼠标离开事件    
\* UIIntegerUpDown: 修改文本框数据输入数据变更事件为MouseLeave    
\* UIDoubleUpDown: 修改文本框数据输入数据变更事件为MouseLeave    
\* UIFlowLayoutPanel: 增加AutoScroll属性    
\* UIFlowLayoutPanel: 修复只显示水平滚动条时，鼠标滚轮滚动水平滚动条不动的问题    
    
#### 2023\-01\-07 V3.3.1    
\+ UIProcessIndicatorForm: 增加等待窗体    
\* UIDataGridView: 增加可重载的函数    
\* UIRadioButton: 修复CheckedChanged事件    
\* UIEdit: 修复了一个最小值大于0是，显示类型为字符串Text为空仍有显示的问题    
\* UIProgressIndicator: 增加Active属性，是否激活动态显示    
\* UIStatusForm: 更改了进度提示框，不在任务栏显示，并置顶    
\* UISplitContainer: SplitterWidth值小的时不绘制箭头    
\* UISplitContainer: 去掉SplitterWidth限制    
\* UITreeView: 增加了可自定义行的颜色    
\* UIIPTextBox: 删除TextChanged事件，增加ValueChanged事件    
\* UIForm: 增加RemoveAllPages函数    
\* UIComboBox: 增加Clear方法    
\* UIComboDataGridView; 增加Clear方法    
\* UIComboTreeView: 增加Clear方法    
\* UINavMenuHelper: 重构RemovePage方法    
\* UIPieOption: 增加了一些为空的判断    
\* UIDoughnutOption: 增加了一些为空的判断    
\* UIBarOption: 增加了一些为空的判断    
\* UILineOption: 增加了一些为空的判断    
    
#### 2022\-11\-28 V3.3.0       
\+ .Net7支持    
\* UILine: 水平方向文字不居中时，可设置线条渐变色    
\* UILineChartOption: 增加了线的最大点数设置，以及移除点数的设置    
\* UILineChartOption: 重构对象    
\* UIBarChartOption: 重构对象    
\* UIPieChartOption: 重构对象    
\* UIFlowLayoutPanel: 增加Get方法以获取控件    
\* UIEditForm: UITextBox类型为Double的时候会导致CheckRange无法验证最大值    
\* UICheckBoxGroup: 修复未显示时切换节点文本为空的问题    
\* UIRadioButtonGroup: 修复未显示时切换节点文本为空的问题    
\* UIComboDataGridView: 过滤框输入增加回车确认    
\* UIComboDataGridView: 增加过滤框输入逐一过滤属性Filter1by1    
\* UITextBox: 增加MouseClick，MouseDoubleClick事件    
    
#### 2022\-11\-13 V3.2.9      
\+ .Net7支持    
**:star: 删除一些过时的方法**    
\- UIFlowLayoutPanel: 删除AddControl、RemoveControl方法    
\- UIDoubleUpDown: 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性    
\- UIIntegerUpDown: 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性    
\- UITextBox: 删除MaximumEnabled、MinimumEnabled、HasMaximum、HasMinimum属性    
**:star: 其他更新内容：**    
\* UIHorScrollBarEx: 增加了可设置水平滚动条高度的属性    
\* UIHorScrollBar: 增加了可设置水平滚动条高度的属性    
\* UIComboBox: 增加不显示过滤可以自动调整下拉框宽度    
\* UIFlowLayoutPanel: 增加滚动条背景色调整    
\* UIIntegerUpDown: 修改整数离开判断为实时输入判断    
\* UIDoubleUpDown: 修改浮点数大小离开判断为实时输入判断    
\* UITextBox: 修改整数、浮点数大小离开判断为实时输入判断    
\* UIDatePicker: 增加MaxDate,MinDate    
    
#### 2022\-11\-03 V3.2.8      
\* UIDataGridView: 增加了可设置垂直滚动条宽度的属性    
\* UITitlePanel: 箭头图标可设置颜色    
\* UIPage: 标题栏增加扩展按钮    
\* UITreeView: TreeNode支持imagekey绑定图标    
\* UIniFile: 增加读取字符串长度到4096，增加文件编码    
\* UIIPTextBox: 增加TextChanged事件    
\* UIVerScrollBarEx: 增加了可设置垂直滚动条宽度的属性    
\* UIImageListBox: 增加了可设置垂直滚动条宽度的属性    
\* UIListBox: 增加了可设置垂直滚动条宽度的属性    
\* UITreeView: 增加了可设置垂直滚动条宽度的属性    
\* UIComboBox: 过滤时删除字符串前面、后面的空格    
\* UIComboDataGridView: 过滤时删除字符串前面、后面的空格    
\* UINavMenu: 增加了可设置垂直滚动条宽度的属性    
\* UINavMenu: 重写了节点右侧图标的绘制    
\* UIFlowLayoutPanel: 增加了可设置垂直滚动条宽度的属性    
\* UIRichTextBox: 增加了可设置垂直滚动条宽度的属性    
\* UITextBox: 增加了可设置垂直滚动条宽度的属性    
    
#### 2022\-10\-03 V3.2.6       
\* UIniFile: 修改IniFile，恢复为WinApi读取，以保证与以前配置文件兼容    
    
#### 2022\-10\-01 V3.2.5    
\* UISwitch: 修复了Readonly时，双击还可以改变值的问题    
\* UILineChart: 增加鼠标可框选缩放属性MouseZoom    
\* UIIntegerUpDown: 增加是否可以双击输入属性    
\* UIDoubleUpDown: 增加是否可以双击输入属性    
\* UITextBox: 修改右侧Button可能不显示的问题    
\* UIDropControl: 支持自定义右键菜单    
\* UITextBox: 支持自定义右键菜单    
\* UIniFile: 修改IniFile，改WinApi读取为直接C#代码读取    
\- JsonConfig: 删除JsonConfig，Json可以直接对象序列化字符串保存。JsonConfig有点鸡肋了    
    
#### 2022\-09\-11 V3.2.4    
**:star: 重构多页面框架传值**     
\* UIForm: 重构多页面框架传值：删除SetParamToPage    
\* UIForm: 重构多页面框架传值：框架发送给页面 SendParamToPage 函数    
\* UIForm: 重构多页面框架传值：接收页面传值 ReceiveParams 事件    
\* UIPage: 重构多页面框架传值：删除SetParam，FeedbackToFrame    
\* UIPage: 重构多页面框架传值：页面发送给框架 SendParamToFrame 函数    
\* UIPage: 重构多页面框架传值：页面发送给框架 SendParamToPage 函数    
\* UIPage: 重构多页面框架传值：接收框架、页面传值 ReceiveParams 事件    
**:star: UIListBox: 修复Click，DoubleClick事件**         
\* UIListBox: 修复Click，DoubleClick事件，替换ItemClick，ItemDoubleClick    
**:star: 其他更新内容：**     
\* UIForm: 修复继承页面可响应WM_HOTKEY消息    
\* UIComboDataGridView: 增加过滤字异常判断    
\* UIBarChart: Option.YAxis.ShowGridLine为false时，不显示水平表格虚线    
\* 下拉框控件文字位置微调，和文本框显示位置一致    
\* UITextBox: 修复了无水印文字时，光标有时不显示的问题    
\* UIDataGridViewFooter: 重构文字显示    
\* UIProcessBar: 修改最大值至少为1    
\* UIImageListBox: 增加了一些事件    
\* UIForm: 重构页面添加、选择、删除事件    
\* UIComboBox: 下拉框边框可设置颜色    
\* UIButton: 增加同一个容器的相同GroupIndex的按钮控件的Selected单选    
\* UINavMenu: 修复选中节点右侧图标前景色    
\* UIBarChart: 增加数据可为Nan    
\* UILineChart: 修复数据全为Nan时绘制出错    
\* 增加UIKnob的Demo    
        
#### 2022\-08\-16 V3.2.3
**:star: 关于图表数据显示格式化已经重构，需重点关注，可参考Demo：**     
\* UILineChart: 数据显示的小数位数重构调整至数据序列 Series.XAxisDecimalPlaces，YAxisDecimalPlaces    
\* UILineChart: 数据显示的日期格式重构调整至数据序列 Series.XAxisDateTimeFormat    
\* UILineChart: 坐标轴的小数位数重构调整至坐标轴标签 AxisLabel.DecimalPlaces    
\* UILineChart: 坐标轴的日期格式重构调整至坐标轴标签 AxisLabel.DateTimeFormat    
\* UIBarChart: 数据显示的小数位数重构调整至数据序列 Series.DecimalPlaces    
\* UIBarChart: 坐标轴的小数位数重构调整至坐标轴标签 AxisLabel.DecimalPlaces    
\* UIDoughnutChart: 数据显示的小数位数重构调整至Option.DecimalPlaces    
\* UIPieChart: 数据显示的小数位数重构调整至Option.DecimalPlaces    
\* UIDoubleUpDown, UIProcessBar, UITextBox: 小数位数统一改名为DecimalPlaces    
**:star: UITabControlMenu需要重新设置ItemSize：**  
\* UITabControlMenu: 重写ItemSize，将宽、高调整为正常显示    
**:star: 其他更新内容：**     
\* UITreeView: 去掉窗体控件的默认设计器    
\* UINavBar: 删除界面此控件的编辑器    
\+ UIKnob: 新增控件    
\* UIBarChart: 修复Y轴显示名称    
\* IniFile: 读数据缓存增加到2048，但还是不建议Ini文件保存过长的数据    
\* UILineChart: 修复双Y轴时，数据为空，刷新出错的问题    
\* UITextBox: 修改了描述错别字    
\* UIChartOption: 清理一些无用的属性    
\* UIDataGridView: 修复了ScrollBars为None时仍然显示滚动条的问题    
\* UITextBox: 修复了有水印文字时，不响应Click和DoubleClick事件的问题    
\* UILineChart: 修复双Y轴数据点提示文字显示    
\* UIGifAvatar: 重写图片刷新流程，减少内存及GC    
\* UIForm: 多页面框架增加程序关闭时调用UIPage的Final和FormClosed事件    
\+ 增加LineAweSome字体图标，测试下看看效果    
\- 删除LineAweSome字体图标，经过测试显示效果不理想    
            
#### 2022\-07\-25 V3.2.2    
\* UIForm: 多页面框架增加PageAdded，PageSelected，PageRemoved事件    
\* UIComboBox: 屏蔽FormatString的编辑器    
\* UIFormHelper: 解决ShowNotifier打开多个，全部关闭时出错的问题    
\* UITextBox: 增加SelectionChanged事件    
\* UITextBox: 修复Click点击事件    
\* UILineChartOption: 增加移除线的操作    
\* UIForm: 增加UnRegisterHotKey，卸载全局热键    
\* UIMessageBox: 消息弹窗文本增加滚动条    
\* UIComboTreeView: 修复CanSelectRootNode时可以展开子节点    
\* UIDataGridView: 增加滚动条边框线的设置    
\* UIDataGridView: 修复一处滚动条的显示位置    
\* UILineChart: 修改两个点时可以不显示连接线    
\* FastLZ: 增加了扩展的自定义压缩解压方法    
\* 增加几个通用函数    
\* 增加XML文档文件    
    
#### 2022\-07\-03 V3.2.1    
\+ UIGifAvatar: Gif动态头像    
\+ UICalendar: 增加日历控件    
\* UITextBox: 重写水印文字，解决不同背景色下泛白的问题    
\* 增加XML文档文件    
\* UICheckBoxGroup: 设置条目状态前判断是否创建    
\* UIDoubleUpDown: 修复了不能设置字体颜色的bug    
\* UIIntegerUpDown: 修复了不能设置字体颜色的bug    
\* UISymbolButton: 修复了出现颜色不填充问题    
\* UIMessageBox: 询问窗口的Focus按钮的绘制线换成背景色    
\* UINavMenu: 绘制节点字体图标增加偏移SymbolOffset    
\* UIDataGridView: 删除 ShowRect、ShowGridLine、RowHeight三个属性    
\* UIDropDown: 解决下拉控件关闭报错    
\* UITabControl: 多页面框架关闭页面时执行UIPage的FormClosed事件    
\* UIComboDataGridView: 增加FilterChanged，输出过滤文字和记录条数    
\* UIComboTreeView: 增加下拉框宽度、高度    
\* UIComboDataGridView: 增加下拉框宽度    
    
#### 2022\-06\-16 V3.2.0    
\+ UIVerificationCode: 增加验证码控件    
\* NuGet发布版本去除.Net5 Winform支持，可自行编译    
\* UIBarChart: 重写Y轴坐标显示    
\* UIForm: 弹窗默认关闭半透明遮罩    
\* UICombobox: 增加了几个关闭过滤窗体的地方    
\* UIDataGridView: 恢复几个原生属性：    
\*     隐藏 ShowRect, 设置原生属性：BorderStyle = BorderStyle.FixedSingle;    
\*     隐藏 ShowGridLine, 设置原生属性：CellBorderStyle = DataGridViewCellBorderStyle.Single;    
\*     隐藏 RowHeight, 用 SetRowHeight() 代替，或设置原生属性：AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None，RowTemplate.Height 设置为高度     
\*     不再判断DataSource绑定List为空，出现”索引-1没有值“用户自行判断    
\* UITextBox: 尺寸改变时重绘    
\* UIPanel: 尺寸改变时重绘    
\* UIStatusForm: 标题栏国际化    
\* 修改了控件属性注释    
\* UIComboBox: Selceted=-1，清除文本    
\* UIComboDataGridView: 修复过滤时，列未绑定DataPropertyName出错的问题    
\* UITabControl: 修复了一个首页无法关闭的问题    
\* ControlEx: 更新几个函数    
\* UIMainFrame: 修复了显示页面关闭按钮，移除最后一个页面出错的问题    
    
#### 2022\-05\-15 V3.1.9    
\* UIComboBox: 过滤时修复ValueMember绑定值的显示    
\* UIComboTreeView: 显示CheckBoxes时自己选中节点文字可切换状态    
\* UITreeView: 增加了点击文字改变CheckBox状态的NodeClickChangeCheckBoxes    
\* UITreeView: 修复了一个设计期显示错误    
\* UIListBox: 增加滚动条颜色设置    
\* UILight: 增加文字显示    
\* UILoginForm: 将文本资源放入窗体属性    
\* UITabControl: 修复屏蔽左右键后其他控件无法使用左右键的问题    
\* UIPage: ShowTitle时，可调整Padding    
\* UIForm: 可拖拽时Padding可以调整大小    
\* UIDoubleUpDown: 增加禁止输入属性    
\* UIIntegerUpDown: 增加禁止输入属性    
\* UIDataGridView：ShowGridLine属性更新    
\* UIForm: 屏蔽一些属性    
\* UIPage: 屏蔽一些属性    
\* UIDataGridView: 解决原生控件DataSource绑定List，并且List为空，出现”索引-1没有值“错误    
\* 更新弹窗不置顶的Bug    
    
#### 2022\-04\-20 V3.1.8    
\* UIComboBox: 过滤文字为空时，下拉框显示所有数据列表    
\* UISplitContainer: 修复调用Collapse()后，展开/收回操作失效    
\* UITabControl: 不显示标签页时屏蔽左右键    
\* UILineChart: 关闭Smooth绘制，数值差距大或者持续缩放会出错    
\* UIForm: 可拖拽时Padding.Top增加1，解决一些显示问题    
\* UIForm: 修复一处设置错误    
\* UITabControl: 关闭按钮增加鼠标移入的效果    
\* UIEditForm: 修改一处Show引起的无法获取控件值的问题    
    
#### 2022\-04\-18 V3.1.5    
\* UIComboDataGridView: 增加行多选    
\* UIComboBox: 增加过滤，ShowFilter    
\* UIDataGridView: 增加滚动条的颜色设置    
\* UIComboBox: 过滤下拉控跟随主题配色    
\* UINavBar: 重构扩展函数    
\* UINavMenu: 重构扩展函数    
\* UIComboBox: 根据Text自动选中SelectIndex    
\* UICheckBox: 值相同时不响应值改变事件    
\* UITextBox: 增加对按钮设置ToolTip    
\* UIStyle: 增加属性以期控件跟随窗体缩放    
\* UIAvator: 增加缩放    
\* UISplitContainer: 增加主题样式    
\* UIPage: 调整了标题的最小高度    
\* UIForm: 调整了标题的最小高度    
\* UITitlePanel: 调整了标题的最小高度    
\* UINavBar: 修复一处索引引起的出错    
    
#### 2022\-04\-03 V3.1.3    
\+ UIStyle: 增加全局字体大小设置    
\+ UIUserControl: 增加用户控件基类    
\+ FastLZ: FastLZ压缩解压类    
\* UIForm: 默认设置AutoScaleMode为None    
\* UIPage: 默认设置AutoScaleMode为None    
\* UIUserControl: 默认设置AutoScaleMode为None    
\* UITreeView: 增加水平滚动条    
\* UITreeView: 垂直滚动条未显示时禁止鼠标滚轮滚动刷新    
\* UITreeView: 自定义行颜色，可通过代码给颜色值，SetNodePainter    
\* 编译SunnyUI.Demo为VS2010可编译版本    
\* UIDPIScale: DPI重构下拉框组件配色    
\* UIComboDataGridView: 修复一处点击清除按钮后确定出错    
\* UIComboDataGridView: 更新了FilterColumnName为空时的过滤出错    
\* UIButton: 增加是否显示浅色背景LightStyle    
\* IFrame: 增加两个接口    
\* UIStyle: 修改等待窗体与主题配色一致    
    
#### 2022\-03\-28 V3.1.2
**此版本改动较大，请关注更新日志**  
\- UIComboboxEx: 删除，其源码见：[https://gitee.com/yhuse/SunnyUI/blob/V3.0.8/SunnyUI/Controls/UIComboboxEx.cs](https://gitee.com/yhuse/SunnyUI/blob/V3.0.8/SunnyUI/Controls/UIComboboxEx.cs)    
\- UITitlePage: 删除，其源码见：[https://gitee.com/yhuse/SunnyUI/blob/V3.0.8/SunnyUI/Frames/UITitlePage.cs](https://gitee.com/yhuse/SunnyUI/blob/V3.0.8/SunnyUI/Frames/UITitlePage.cs)    
\- UIStyle: 删除内置主题：White、Office2010Blue、Office2010Silver、Office2010Black、LightBlue、LightGreen、LightOrange、LightRed、LightGray、LightPurple    
\+ UISytle: 增加内置主题：LayuiGreen、LayuiRed、LayuiOrange     
\* UIStyle: 调整内置主题配色：DarkBlue、Black    
\* UIStyle: 重构主题配色代码    
\* UIForm: 增加了查找页面的方法    
\* UINavMenu: 修复TipsText显示位置    
\* UIComboDataGridView: 增加自动过滤、单元格双击选中    
\* UIComboDataGridView: 修改拼写错误，FilterColomnName改为FilterColumnName    
\* UILineChart: 默认图线平滑关闭    
\* UIDataGridView: 更新DPI缩放对控件的适配    
\* UISplitContainer: 更新按钮刷新    
\* UIForm: 修改TitleHeight后窗体仍然保持可拖拽调节大小    
\* UITextBox: 增加滚动条的颜色设置    
\* UIRichTextBox: 增加滚动条的颜色设置    
\* UIForm: 增加DPI强制缩放的函数ReSetDPIScale    
\* UIPanel: 修复Radius设置不能超过35的问题    
\* UIControl: 修复Radius设置不能超过35的问题    
\* UIColorPicker: 修复选中颜色不显示    
\* UIBarChart: 增加X轴文字倾斜    
\* UIListBox: 修复在选中某一项后，清除选中项需要两次操作    
\* UIDataGridView: 更新单选时选中值SelectedIndex值    
\* UIButton: 增加了AutoSize属性    
\* UIComboTreeView: 节点默认不自动展开    
\* UIRichTextBox: 增加了一些原生的属性和事件    
\* UIListBox: 按键上下移动选择项目时，滚动条跟随    
\* UIEditForm: 继承后可修改IsOK    
\* UIDoubleUpDown: 可以设置按钮大小和颜色    
\* UIIntegerUpDown: 可以设置按钮大小和颜色    
\* UITextBox: 增加了只读的颜色设置    
\* UIControl: 基类增加只读颜色设置    

#### 2022\-02\-12 V3.1.1    
\+ UIIPTextBox: 增加IP地址输入框    
\+ UISmoothLabel: 平滑文字带边框的标签    
\* UIAnalogMeter: 支持背景透明色    
\* UIForm: 增加页面间传值方法SetParamToPage    
\* UILineChart: 增加图线隐藏    
\* UIIntegerUpDown: 增加圆角控制    
\* UIDoubleUpDown: 增加圆角控制    
\* UITextBox: 修正编辑框高度    
\* UIDoubleUpDown: 修正默认值不为0时，编辑值为0的问题    
\* UIPanel: 禁止显示滚动条    
\* UITextBox: 关闭设计期水印文字显示    
\* UIBreadcrumb: 增加未选中步骤文字颜色    
\* UIBreadcrumb: 增加两端对齐，AlignBothEnds    
\* UISymbolButton: 设置完图片即可更新    
\* UIDataGridView: 更新单选时选中值SelectedIndex值    
\* UINotifiler: 修改了点击事件    
\* UIComboBox: 增加了下拉框颜色设置    
\* UINavMenu: SelectPage增加Guid接口    
\* UITabControl: 修改删除页面时的页面跳转    
\* UIControl: 设置圆角的范围    
\* UIPanel: 设置圆角的范围    
\* UINavMenu: 设置滚动条背景色    
\* UIPagination: 修复按钮图标配色    
\* UILine: 修复了文本为空不显示的问题    
\* UIPanel: 调整边框和圆角的绘制    
\* UIControl: 调整边框和圆角的绘制    
\* UILineChart: 双坐标轴支持选区域缩放    
\* IFrame：增加了判断页面是否存在的代码    
\* UITextBox: 按钮支持自定义颜色    
\* UILineChart: 支持FillColor透明    
\* 源码兼容VS2019编译    
\* 修复了日期时间选择框下拉框的按钮配色    
\* 修复日期选择控件值切换响应两次    
\* Demo增加全局热键    
\* 修改部分控件小数位数属性名称为DecimalPlaces    
\* 字体图标选择界面，增加搜索框，搜索结果标红显示    
\* 下拉控件增加内存释放    
    
#### 2022\-01\-05 V3.1.0    
\+ ChineseColors: 中国传统颜色    
\+ UITexture: 增加主题背景纹理类    
\+ BmpFile: 24bit真彩色位图数据结构类    
\+ BigEndianBinaryWriter: 大端字节序的二进制写入器    
\+ IFrame: 创建文档    
\* UIStyle: 完善控件支持DPI自适应缩放。    
\* UITreeView: 节点点击时同步父节点和子节点的状态    
\* UILine: 增加线的样式，支持透明背景    
\* UISymbolButton: 字体图标增加颜色设置    
\* UIForm: 标题栏按钮可以设置颜色    
\* UIForm: 右下角弹窗增加点击交互    
\* UINavMenu: 滚动条可设置颜色    
\* UIEditForm: AddComboTreeView 加默认值    
\* UISwitch: 增加是否只读属性    
\* UILineChart: 简化了设置坐标轴范围的函数    
\* UIHeaderButton: 增加角标    
\* UITabControl: 增加角标    
\* UIBarChart: 增加柱子上显示数值    
\* UILineChart: X轴支持字符串显示    
\* UILineChart: 增加自定义坐标轴刻度    
\* UILineChart: 双坐标轴增加纵横参考线    
\* UILineChart: 增加双Y坐标轴    
\* UILineChart: 增加坐标线、图线边框等是否显示的设置    
\* UIPage: 增加NeedReload，页面切换是否需要重载Load    
\* UIListBox: 增加修改文字颜色    
\* UISymbolLabel: 修复Dock和AutoSize同时设置的Bug    
\* UIInputDialog: 输入框增加半透明遮罩    
\* UIForm: 增加全屏遮罩，Form的ShowDialogWithMask()扩展方法    
\* UINavMenu: 选中项可设置背景色渐变    
\* UIButton: 增加了渐变色    
\* UIPanel: 增加了渐变色    
\* UIDoubleUpDown: 增加Readonly属性    
\* UINavMenu: 修改了一处可能的出错    
\* UIComboDataGridView: 有过滤输入框时默认Focus    
\* UIChart: 修改可能显示出错的问题    
\* UIToolTip: 修复默认显示    
\* UIHeaderButton: 更改图片自动刷新    
\* UIPipe: 修正界面设计器删除不了的问题    
\* UIMessageTip: 修复可能出错的问题    
\* UIPage: 增加FeedBack和SetParam函数，用于多页面传值    
\* UIChart: 修复编辑器显示时出错的问题    
\* UIDataGridViewFooter: 修复一处可能不显示的问题    
    
#### 2021\-11\-19 V3.0.9    
\+ SunnyUI: 增加.Net6版本的支持    
\+ UIStyle: 全部SunnyUI控件支持DPI自适应缩放    
\+ UISplitContainer: 增加UISplitContainer控件    
\+ IniFileEx: 增加INI文件读取类（不用WinAPI）    
\+ UIForm: 增加全局热键    
\+ UIForm: 增加IFrame接口    
\* UILabel: 增加文字旋转角度    
\* UIMessageForm: 多个按钮显示时增加FocusLine    
\* UIFlowLayoutPanel: 修改不同DPI缩放滚动条未覆盖的问题    
\* UIComboDataGridView: 增加过滤    
\* UIDataGridView: 增加一个可能出错的判断    
\* UIEditForm: 代码生成增加ComboCheckedListBox类型    
\* UIEditForm: 代码生成增加ComboTreeView类型    
\* UIFlowLayoutPanel: 增加Scroll事件    
\* UIRoundProcess: 增加显示小数位数    
\* UICombobox: 右侧边框不显示时，去除绘制线    
\* UILine: 调整最小长、宽为1    
\* UITextBox: 支持修改背景色    
\* UICheckBoxGroup :增加SetItemCheckState功能    
\* UITextBox: 调整最小高度限制    
\* UIProcessBar: 调整最小高度为3    
\* UILineChart: 修改图线显示超出范围的问题    
\* UITreeView: 判断节点Checked是否改变，只有改变时才赋值    
\* UIListBox: 增加DrawItem和Demo    
\* UILineChart: 修改自定义最大值最小值为无穷时出错的问题    
\* UILineChart: 显示点的颜色支持自定义    
\* UILineChart: 支持数据包括Nan    
    
#### 2021\-10\-01 V3.0.8    
\+ Mapper: 轻量级的对象映射框架，可以映射值类型（包括Struct），和以值类型构成的List和数组。    
\* UITreeView: 修复TreeView默认展开时，绘制半选状态报错的问题    
\* UIDataGridViewFooter: 文字显示方向与Column列显示方向一致    
\* UICombobox: 修复使用BindingList进行绑定，DisplayMember是空字符串显示错误    
\* UIStyle: 修改默认字体的GdiCharSet    
\* UIHeaderButton: 增加Disabled颜色    
\* UISwitch: 增加Disabled颜色    
\* UIForm: 增加Movable属性，控制点击标题行是否能移动窗体    
    
#### 2021\-09\-08 V3.0.7    
\+ MMFile: 增加多进程通信框架    
\+ UIComboDataGridView: 增加表格下拉列表框    
\+ UIMillisecondTimer: 增加毫秒定时器    
\+ 增加ToolTip接口，在用UIToolTip时解决类似UITextBox这类的组合控件无法显示ToolTip的问题    
\* UIForm: 修复多屏时最大化显示的问题    
\* UIPage: 修复OnLoad在加载时重复加载两次的问题    
\* UITextBox: 重写了水印文字的画法，并增加水印文字颜色    
\* UICombobox: 修改Watermark及其颜色    
\* UITextBox: 增加按钮    
\* UIPanel: 支持背景图片显示    
\* UITitlePanel: 增加标题文字颜色    
\* UIDropControl: 优化下拉框控件显示效果    
\* UIEditForm: 代码创建时增加UISwitch开关文字描述    
\* UINavMenu: 增加自定义TipsText显示的颜色    
\* UITreeView:  CheckBoxes增加三态，感谢群友: 笑口常开    
\* UILineChart: 增加可只显示点的模式    
\* UICombobox: 增加ShowDropDown函数    
\* UIGroupBox: 解决Radius为0时的报错    
\* UIAnalogMeter: 增加ValueChanged事件    
\* Demo: 修改Demo的UITitlePage为UIPage，UITitlePage已废弃    
\* UIForm, UIPage: 增加TitleFont属性    
\* UIProcessBar: 修改不显示百分比时，显示数值    
\* UIDatePicker: 增加可选择年、年月、年月日    
\* UIDateTimePicker: 选中的年月日标记显示    
\* UIImageButton: 更改了一个属性为私有，在VB.Net下不区分大小写而出错    
\* UITabControl: 增加DisposeTabPageAfterRemove标志，移除TabPage后，是否自动销毁TabPage    
\* UITabControl: 关闭TabPage并销毁TabPage    
\* 整理了一些GDI绘图的常用方法扩展    
\* 整理了一些扩展函数    
    
#### 2021\-08\-12 V3.0.6  
\+ UIPipe: 增加管道控件  
\+ UIValve: 增加阀门控件  
\+ UIStyle: 增加多彩主题，以颜色深色，文字白色为主  
\+ UIStyle: 增加紫色主题  
\+ UITableLayoutPanel: 增加控件  
\+ Demo: 增加工控分类  
\+ ITranslate: 增加多语翻译接口  
\+ UGif: GIF图片解析类  
\+ SunnyUI: Nuget项目引用增加签名  
\* UIFlowLayoutPanel: 增加了几个原生方法  
\* UITransfer: 增加了显示多个移动的属性  
\* UIProcessBar: 增加垂直方向的进度显示  
\* UILight: 默认不显示灯光亮线  
\* UINavMenu: 显示子节点提示箭头  
\* UINavBar: 增加选中项圆角  
\* UIImageListBox: 从文件载入图片，并且解除占用  
\* UICombobox: 增加几个原生方法  
\* UIListBox: 增加一大波ListBox原生方法  
\* UIListBox: 增加Items变更的事件  
\* UIForm: 修复最大化盖住任务栏的问题  
\* UITextBox: 增加GotFocus和LostFocus事件  
\* UIFlowLayoutPanel: 可像原生控件一样通过Controls.Add增加  
\* UIListBox: 选中项显示方角  
\* UIListBox: 增加多选行  
\* UIComboTreeView : 修复SelectedNode=null的问题  
\* UIRichTextBox: 修改滚动条没有文字时自动隐藏  
\* UIPage: 修复OnMouseMove事件  
\* UIStyle: 更新了放在TableLayoutPanel里控件的自定义颜色问题  
\* UILocalize: 内置支付串已经处理完国际化  
\* UILineChart: 可自定义背景色  
\* UILineChart: 增加实时数据的Demo  
\* UIBarChart, UIPieChart, UIDoughnutChart增加更新数据的方法  
\* UITreeView: 调整了显示CheckBoxes时图片位置  
\* ISymbol: 将字体图标最大尺寸从64调整到128  
\* UITextBox: 修改Focus可用  
\* UIButton: 增加ShowFocusColor，用来显示Focus状态  
\* UIPage: 修复OnLoad在加载时重复加载两次的问题，增加Final函数，每次页面切换，退出页面都会执行  
\* UIStyle: 多彩颜色增加随机颜色Demo  
\* UIScrollingText: 增加属性控制开启滚动  
\* UIPage: 恢复删除的Initialize事件  
\* ISytle: 调整主题切换执行流程  
\* IStyle: 支持自定义主题  
\* ISymbol: 增加SymbolOffset接口  
\* UITabControl: 支持Tab在下方显示  
  
#### 2021\-07\-11 V3.0.5  
\+  字体图标: 增加FontAwesome V5.15版本字体图标  
\+  UISignal: 增加信号强度显示控件  
\+  UIToolStripDropDown: 增加了一个弹窗管理类  
\* UIPage: 增加标题行，后期以替代UITitlePage  
\* Demo的Controls下的页面全部从UITitlePage切换到UIPage，后期会逐步舍弃UITitlePage。  
\* UIDataGridView: 增加了一个RowHeight，默认23  
\* IStyleInterface: 设置为Public，可基于此扩展外部控件  
\* UIRichTextBox: 支持可改背景色  
\* UIPagination: 更新了Demo，分页切换事件加载数据。  
\* UITitlePage: 解决标题栏闪烁  
\* UITextBox: 增加图标和字体图标的显示  
\* UITextBox: MaximumEnabled，MinimumEnabled代替HasMaximum，HasMinim  
\* UIHeaderButton: 增加了TextImageRelation，实现文本和图像的相对位置  
\* UIListBox: 修改对象绑定的显示问题  
\* UICombobox: 更新了数据绑定相关代码  
\* UITabControl: Tab页标题选中高亮颜色增加可调整高度  
\* UINavBar: 标题选中高亮颜色增加可调整高度  
\* UIListBox: 更新一处数据绑定显示错误  
\* UINavMenu: 增加右侧图标  
\* UIBattery: 修改可自定义背景色  
\* UILight: 增加方形显示，优化渐变色  
\* UIHeaderButton: 增加ShowSelected，是否显示选中状态  
\* UIBarChart: 修正一个显示的Bug  
\* UIRoundProcess: 修改显示值  
\* UIRichTextBox: 增加WordWrap属性  
\* UIDataGridView: 自定义单元格颜色  
\* IFame: 增加一个反馈的接口，Feedback，Page可将对象反馈给Frame  
\* UIAvatar: 更改图片显示  
\* UIPagination: 设置总数在页面不超过总页数的情况下不刷新  
\* UITextBox等组合控件将其回调事件的Sender设置为this，而不是其内置控件  
\* UIFlowLayoutPanel: 增加滚动条颜色属性  

#### 2021\-05\-20 V3.0.4  
\+ UIObjectCollection: 带集合个数改变事件的对象集合类  
\+ UIStringCollection: 带集合个数改变事件的字符串集合类  
\+ UIDataGridViewFooter: 增加DataGridView页脚，可做统计显示  
\* UIBreadcrumb, UICheckBoxGroup, UIRadioButtonGroup: 更改列表项为UIObjectCollection  
\* UIScrollingText: 增加属性可设置双击暂停滚动  
\* UIEditForm: 动态生成表单，增加校验方法  
\* UIDoubleUpDown, UIIntegerUpDown: 将双击编辑更改为单机编辑并选中  
\* IFrame: 增加RemovePage接口  
\* UIMessageDialog，UIMessageBox: 增加TopMost参数  
\* UIBarChart: 修改了一个显示负值的Bug  
\* UIForm: 加了个属性AllowAddControlOnTitle，允许在标题栏放置控件  
\* UICombobox: 解决鼠标下拉选择，触发SelectedIndexChanged两次的问题  
\* UISwitch: 更新Active状态改变时触发ValueChanged事件  
\* UIDataGridView: 设置数据行头部颜色  
\* UIEditForm: 代码生成增加Switch类型，增加Combobox类型  
\* UICheckBox，UIRadioButton: 增加默认事件CheckedChanged  
\* UIProcessBar: 可设置显示进度条小数个数  
\* 等待提示框: 更新等待时间短时无法关闭等待窗体的问题  
\* DirEx: 增加一个文件夹选择框  
\* UITextBox: 增加ShowScrollBar属性，单独控制垂直滚动条  
\* UITextBox: 不限制高度为根据字体计算，可进行调整  
\* UITextBox: 解决多行输入时不能输入回车的问题  
\* UITextBox: 修改文字可以居中显示  
\* UIDatePicker,UIDateTimePicker: 增加ShowToday显示今日属性  
\* UILineChart: 有右键菜单时，取消恢复上次缩放，可在右键菜单增加节点，调用ZoomBack()方法  

#### 2021\-04\-11 V3.0.2  
\+ UIMarkLabel: 增加带颜色标签的Label  
\+ UIRoundProcess: 圆形滚动条  
\+ UIBreadcrumb: 增加面包屑导航  
\+ UILedLabel: 增加Led标签  
\* UIHeaderButton: 在工具箱中显示  
\* UILineChart: 支持拖拽选取放大  
\* UIDateTimePicker: 修复下拉选择日期后关闭的Bug  
\* UINavMenu: 增加设置二级菜单底色  
\* UIColorPicker: 增加单击事件以选中颜色  
\* UITitlePage: 增加ShowTitle可控制是否显示标题  
\* UINavBar: 增加可设置背景图片  
\* 框架增加IFrame接口，方便页面跳转  
\* UIDataGridView: 修改垂直滚动条和原版一致，并增加翻页方式滚动  
\* UIPagination: 修正因两次查询数量相等而引起的不刷新  
\* UIHeaderButton: 增加字体图标背景时鼠标移上背景色  
\* UITabControl: 修改第一个TabPage关不掉的Bug  
\* UIDataGridView: 增加EnterAsTab属性，编辑输入时，用Enter键代替Tab键跳到下一个单元格  
\* UILineChart: 增加鼠标框选放大，可多次放大，右键点击恢复一次，双击恢复  
\* UITitlePanel: 修复OnMouseMove事件  
\* UITrackBar: 增加垂直显示方式  
\* UIFlowLayoutPanel: 修改了一处因为其加入控件大小发生变化而引起的滚动条出错  

#### 2021\-02\-26 V3.0.1  
\+ UIForm: 标题栏增加扩展按钮  
\+ UIHeaderButton: 新增大图标的导航按钮  
\+ 新增UIComboboxEx，从Combobox原生控件继承，以方便做查询过滤等操作  
\* UIForm: 修正不显示标题栏时，标题栏位置可放置控件  
\* UIListBox: 增加一些原有属性  
\* FCombobox: 增加数据绑定Demo  
\* UICombobox: 更改索引改变事件的多次触发  
\* UIForm: 修改一处Icon图片显示的问题  
\* UIEditForm: 修改通过代码生成窗体控件的TabIndex  
\* UIDatePicker，UIDateTimePicker: 将日期选择控件的最小值调整为1900年  
\* UIHeaderButton: 将其命名空间从Sunny.UI.Control改为Sunny.UI  

#### 2021\-01\-26 V3.0.0
\+ 同时兼容.Net Framework 4.0\+:、.Net Core3.1、.Net 5 框架  
\* 更新UIMessageTip  
\* UIForm: 增加ShowTitleIcon用来显示标题栏图标，与ShowIcon分开  
\* UINavBar: 增加下拉菜单可设置自动高度或者固定高度，可显示ImageList绑定  
\* UIDataGridView更新行头和列头的选中颜色  

#### 2021\-01\-05 V2.2.10  
\* V2.2 .Net Framewok 4.0最终版本  
\* V3.0 开始将同时兼容.Net Framework 4.0\+:、.Net Core3.1、.Net 5 框架  

#### 2020\-12\-20 V2.2.9  
\+ UIWaitForm: 等待窗体  
\+ UIComboTreeView: 新增下拉框TreeView  
\+ UIMessageForm: 消息提示框增加黑色半透明遮罩层  
\+ Win32API: 新增Win32API函数  
\+ UJsonConfig: 不引用第三方控件，用.Net自带的序列化实现Json，增加Json文件配置类  
\+ UIDataGridViewForm: 增加了一个表格模板基类  
\* UIDataGridView: 修改DataSource赋值后Column改变引起的水平滚动条错误  
\* UIDoubleUpDown，UIIntegerUpDown: 增加双击可编辑数值  
\* UINavMenu: 增加选中后图标的背景色或应用选中图片索引  
\* 页面框架增加页面内跳转方法  
\* 日期、时间选择框增加CanEmpty，输入可为空  

#### 2020\-10\-12 V2.2.8  
\+ UILineChart: 完成曲线图表  
\+ UIScale: 增加坐标轴刻度计算类  
\+ UIFlowLayoutPanel: 增加  
\+ UIBarChartEx: 增加了一个新的柱状图类型，序列个数可以不相等  
\+ UDateTimeInt64: 增加DateTimeInt64类，时间整形互转类  
\* UIForm: 增加窗体阴影  
\* UIMainFrame: 页面框架增加Selecting事件，在页面切换时执行该事件  
\* UITextBox: 解决Anchor包含Top、Bottom时，在窗体最小化后恢复时高度变化  
\* UISwitch: 增加长方形形状开关，取消长宽比锁定  
\* UITreeView: 背景色可改，设置FillColor，以及SystemCustomMode = true  
\* UIDataGridView: 解决水平滚动条在有列冻结时出错的问题  

#### 2020\-09\-17 V2.2.7  
\+ 新增双主键线程安全字典，分组线程安全字典  
\+ UIHorScrollBarEx，UIVerScrollBarEx: 重写了两个滚动条  
\* UIForm: 恢复了WindowState，增加了窗体可拉拽调整大小  
\* 增加控件属性显示值及Sunny UI分类  
\* UIDateTimePicker,UITimePicker: 更改滚轮选择时间的方向  
\* UIButton: Tips颜色可设置  
\* UIChart: 增加图表的边框线颜色设置  
\* UITextBox: 增加FocusedSelectAll属性，激活时全选  
\* UINavBar: 增加节点的Image绘制  
\* UIDataGridView: 调整水平滚动条  
\* UIButton: 添加'是否启用双击事件'属性，解决连续点击效率问题  
\* UIDataGridView: 更新了水平和垂直滚动条的显示，优化滚动效果  
\* UIBbutton: 空格键按下press背景效果  
\* UIListBox优化滚轮快速滚动流畅性  
\* UIBarChart: 可设置柱状图最小宽度  
\* UIIntegerUpDown, UIDoubleUpDown: 增加字体调整  
\* UITabControl: 标题垂直居中  
\* UITreeView: 更新可设置背景色  
\* UIDatePicker，UITimePicker，UIDateTimePicker: 可编辑输入，日期范围控制  
\* UIDatePicker: 更改日期范围最小值和最大值  
\* UITitlePanel: 更新大小调整后的按钮位置  

#### 2020\-07\-30 V2.2.6  
\+ UIPagination: 新增分页控件  
\+ UIToolTip: 新增控件，可修改字体  
\+ UIHorScrollBar: 新增水平滚动条  
\+ UIWaitingBar: 新增等待滚动条控件  
\* UIDataGridView: 重绘水平滚动条，更新默认设置为原生控件设置  
\* UITitlePanel: 增加可收缩选项  
\* UIPieChart,UIBarChart: 增加序列自定义颜色  
\* UISymbolButton: 增加Image属性，增加图片和文字的摆放位置  
\* UIButton: 增加Selected及选中颜色配置  
\* UIForm: 支持点击窗体任务栏图标，可以进行最小化  
\* UIForm: 增加标题栏ICON图标绘制  
\* UIDateTimePicker: 重写下拉窗体，缩短创建时间  
\* UITreeView: 全部重写，增加圆角，CheckBoxes等  
\* UIDatePicker: 重写下拉窗体，缩短创建时间  
\* UICheckBoxGroup,UIRadioButtonGroup: 可以设置初始选中值  
\* UILedBulb: 边缘平滑  
\* UIForm: 仿照QQ，重绘标题栏按钮  

#### 2020\-06\-29 V2.2.5  
\+ UIDoughnutChart: 环状图  
\+ UILoginForm: 登录窗体  
\+ UIScrollingText: 滚动文字  
\+ UIBarChart: 柱状图  
\+ UIPieChart: 饼状图  
\+ UIRichTextBox: 富文本框  
\+ UIBattery: 电池电量显示  
\+ UIDatetimePicker: 日期时间选择框  
\+ UIColorPicker: 颜色选择框  
\+ UITimePicker: 时间选择框  
\+ UIMessageTipHelper: 增加MessageTip扩展方法  
\* UIComboBox: 增加数据绑定  
\* 页面框架支持通过PageIndex和PageGuid关联  
\* UITextBox: 增加Multiline属性，增加滚动条  
\* UITabControl: 新增关闭按钮，重绘左右移动按钮  
\* UIForm: 更新标题移动、双击最大化/正常、到顶最大化、最大化后拖拽正常  
\* UINavMenu: 增加字体图标显示  
\* 字体图标字体调整从资源文件中加载字体，不用另存为文件  
\* UIListBox 增加跟随鼠标滑过高亮  
\* UIDatePicker: 重写日期选择界面  
\* UIButton: 增加ShowFocusLine，可获得焦点并显示  

#### 2020.05.05 V2.2.5  
\+ 增加页面框架    
\+ 增加下拉框窗体，进度提升窗体    
\+ UITreeView    

#### 2020.04.25 V2.2.4  
\* 更新主题风格类，各控件主题颜色调用不交叉，便于新增主题  
\+ 更新Sunny.Demo程序  
\+ 增加UIDataGridView，基于DataGridView增强、美化  
\- UIGrid效率待改，暂时隐藏  

#### 2020.04.19 V2.2.3  
\+ UICheckBoxGroup,UIRadioButtonGroup  

#### 2020.04.11 V2.2.2  
\+ 新增UIGrid  
\- 继承DataGridView更改主题风格的UIGridView  

#### 2020.02.15 V2.2.1  
\* Bug修复  

#### 2020.01.01 V2.2.0  
\* 增加文件说明，为开源做准备  
\+ 增加Office主题风格  

#### 2019.10.01 V2.1.0  
\+ 增加Element主题风格  

#### 2019.03.12 V2.0.0  
\+ 增加自定义控件  

#### 2012.03.31 V1.0.0  
\+ 增加工具类、扩展类  