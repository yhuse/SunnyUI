:fa-plus:增加；  :fa-asterisk:修改；  :fa-minus:删除

#### 2021-08-12 V3.0.6  
:fa-plus: UIPipe: 增加管道控件  
:fa-plus: UIValve: 增加阀门控件  
:fa-plus: UIStyle：增加多彩主题，以颜色深色，文字白色为主  
:fa-plus: UIStyle：增加紫色主题  
:fa-plus: UITableLayoutPanel：增加控件  
:fa-plus: Demo：增加工控分类  
:fa-plus: ITranslate：增加多语翻译接口  
:fa-plus: UGif: GIF图片解析类  
:fa-plus: SunnyUI: Nuget项目引用增加签名  
:fa-asterisk: UIFlowLayoutPanel: 增加了几个原生方法  
:fa-asterisk: UITransfer: 增加了显示多个移动的属性  
:fa-asterisk: UIProcessBar: 增加垂直方向的进度显示  
:fa-asterisk: UILight: 默认不显示灯光亮线  
:fa-asterisk: UINavMenu：显示子节点提示箭头  
:fa-asterisk: UINavBar: 增加选中项圆角  
:fa-asterisk: UIImageListBox: 从文件载入图片，并且解除占用  
:fa-asterisk: UICombobox: 增加几个原生方法  
:fa-asterisk: UIListBox: 增加一大波ListBox原生方法  
:fa-asterisk: UIListBox：增加Items变更的事件  
:fa-asterisk: UIForm: 修复最大化盖住任务栏的问题  
:fa-asterisk: UITextBox: 增加GotFocus和LostFocus事件  
:fa-asterisk: UIFlowLayoutPanel: 可像原生控件一样通过Controls.Add增加  
:fa-asterisk: UIListBox: 选中项显示方角  
:fa-asterisk: UIListBox：增加多选行  
:fa-asterisk: UIComboTreeView : 修复SelectedNode=null的问题  
:fa-asterisk: UIRichTextBox: 修改滚动条没有文字时自动隐藏  
:fa-asterisk: UIPage: 修复OnMouseMove事件  
:fa-asterisk: UIStyle: 更新了放在TableLayoutPanel里控件的自定义颜色问题  
:fa-asterisk: UILocalize: 内置支付串已经处理完国际化  
:fa-asterisk: UILineChart：可自定义背景色  
:fa-asterisk: UILineChart：增加实时数据的Demo  
:fa-asterisk: UIBarChart, UIPieChart, UIDoughnutChart增加更新数据的方法  
:fa-asterisk: UITreeView: 调整了显示CheckBoxes时图片位置  
:fa-asterisk: ISymbol: 将字体图标最大尺寸从64调整到128  
:fa-asterisk: UITextBox: 修改Focus可用  
:fa-asterisk: UIButton：增加ShowFocusColor，用来显示Focus状态  
:fa-asterisk: UIPage：修复OnLoad在加载时重复加载两次的问题，增加Final函数，每次页面切换，退出页面都会执行  
:fa-asterisk: UIStyle: 多彩颜色增加随机颜色Demo  
:fa-asterisk: UIScrollingText: 增加属性控制开启滚动  
:fa-asterisk: UIPage：恢复删除的Initialize事件  
:fa-asterisk: ISytle：调整主题切换执行流程  
:fa-asterisk: IStyle：支持自定义主题  
:fa-asterisk: ISymbol：增加SymbolOffset接口  
:fa-asterisk: UITabControl：支持Tab在下方显示  
  
#### 2021-07-11 V3.0.5  
:fa-plus:  字体图标：增加FontAwesome V5.15版本字体图标  
:fa-plus:  UISignal: 增加信号强度显示控件  
:fa-plus:  UIToolStripDropDown: 增加了一个弹窗管理类  
:fa-asterisk: UIPage：增加标题行，后期以替代UITitlePage  
:fa-asterisk: Demo的Controls下的页面全部从UITitlePage切换到UIPage，后期会逐步舍弃UITitlePage。  
:fa-asterisk: UIDataGridView：增加了一个RowHeight，默认23  
:fa-asterisk: IStyleInterface：设置为Public，可基于此扩展外部控件  
:fa-asterisk: UIRichTextBox：支持可改背景色  
:fa-asterisk: UIPagination：更新了Demo，分页切换事件加载数据。  
:fa-asterisk: UITitlePage：解决标题栏闪烁  
:fa-asterisk: UITextBox：增加图标和字体图标的显示  
:fa-asterisk: UITextBox: MaximumEnabled，MinimumEnabled代替HasMaximum，HasMinim  
:fa-asterisk: UIHeaderButton: 增加了TextImageRelation，实现文本和图像的相对位置  
:fa-asterisk: UIListBox：修改对象绑定的显示问题  
:fa-asterisk: UICombobox: 更新了数据绑定相关代码  
:fa-asterisk: UITabControl：Tab页标题选中高亮颜色增加可调整高度  
:fa-asterisk: UINavBar: 标题选中高亮颜色增加可调整高度  
:fa-asterisk: UIListBox：更新一处数据绑定显示错误  
:fa-asterisk: UINavMenu：增加右侧图标  
:fa-asterisk: UIBattery：修改可自定义背景色  
:fa-asterisk: UILight：增加方形显示，优化渐变色  
:fa-asterisk: UIHeaderButton：增加ShowSelected，是否显示选中状态  
:fa-asterisk: UIBarChart：修正一个显示的Bug  
:fa-asterisk: UIRoundProcess：修改显示值  
:fa-asterisk: UIRichTextBox：增加WordWrap属性  
:fa-asterisk: UIDataGridView：自定义单元格颜色  
:fa-asterisk: IFame：增加一个反馈的接口，Feedback，Page可将对象反馈给Frame  
:fa-asterisk: UIAvatar：更改图片显示  
:fa-asterisk: UIPagination：设置总数在页面不超过总页数的情况下不刷新  
:fa-asterisk: UITextBox等组合控件将其回调事件的Sender设置为this，而不是其内置控件  
:fa-asterisk: UIFlowLayoutPanel：增加滚动条颜色属性  

#### 2021-05-20 V3.0.4  
:fa-plus: UIObjectCollection：带集合个数改变事件的对象集合类  
:fa-plus: UIStringCollection：带集合个数改变事件的字符串集合类  
:fa-plus: UIDataGridViewFooter：增加DataGridView页脚，可做统计显示  
:fa-asterisk: UIBreadcrumb, UICheckBoxGroup, UIRadioButtonGroup: 更改列表项为UIObjectCollection  
:fa-asterisk: UIScrollingText：增加属性可设置双击暂停滚动  
:fa-asterisk: UIEditForm：动态生成表单，增加校验方法  
:fa-asterisk: UIDoubleUpDown, UIIntegerUpDown：将双击编辑更改为单机编辑并选中  
:fa-asterisk: IFrame：增加RemovePage接口  
:fa-asterisk: UIMessageDialog，UIMessageBox：增加TopMost参数  
:fa-asterisk: UIBarChart：修改了一个显示负值的Bug  
:fa-asterisk: UIForm：加了个属性AllowAddControlOnTitle，允许在标题栏放置控件  
:fa-asterisk: UICombobox：解决鼠标下拉选择，触发SelectedIndexChanged两次的问题  
:fa-asterisk: UISwitch：更新Active状态改变时触发ValueChanged事件  
:fa-asterisk: UIDataGridView：设置数据行头部颜色  
:fa-asterisk: UIEditForm：代码生成增加Switch类型，增加Combobox类型  
:fa-asterisk: UICheckBox，UIRadioButton：增加默认事件CheckedChanged  
:fa-asterisk: UIProcessBar：可设置显示进度条小数个数  
:fa-asterisk: 等待提示框：更新等待时间短时无法关闭等待窗体的问题  
:fa-asterisk: DirEx：增加一个文件夹选择框  
:fa-asterisk: UITextBox：增加ShowScrollBar属性，单独控制垂直滚动条  
:fa-asterisk: UITextBox：不限制高度为根据字体计算，可进行调整  
:fa-asterisk: UITextBox：解决多行输入时不能输入回车的问题  
:fa-asterisk: UITextBox：修改文字可以居中显示  
:fa-asterisk: UIDatePicker,UIDateTimePicker：增加ShowToday显示今日属性  
:fa-asterisk: UILineChart：有右键菜单时，取消恢复上次缩放，可在右键菜单增加节点，调用ZoomBack()方法  

#### 2021-04-11 V3.0.2  
:fa-plus: UIMarkLabel：增加带颜色标签的Label  
:fa-plus: UIRoundProcess：圆形滚动条  
:fa-plus: UIBreadcrumb：增加面包屑导航  
:fa-plus: UILedLabel：增加Led标签  
:fa-asterisk: UIHeaderButton：在工具箱中显示  
:fa-asterisk: UILineChart：支持拖拽选取放大  
:fa-asterisk: UIDateTimePicker：修复下拉选择日期后关闭的Bug  
:fa-asterisk: UINavMenu：增加设置二级菜单底色  
:fa-asterisk: UIColorPicker：增加单击事件以选中颜色  
:fa-asterisk: UITitlePage：增加ShowTitle可控制是否显示标题  
:fa-asterisk: UINavBar：增加可设置背景图片  
:fa-asterisk: 框架增加IFrame接口，方便页面跳转  
:fa-asterisk: UIDataGridView：修改垂直滚动条和原版一致，并增加翻页方式滚动  
:fa-asterisk: UIPagination: 修正因两次查询数量相等而引起的不刷新  
:fa-asterisk: UIHeaderButton: 增加字体图标背景时鼠标移上背景色  
:fa-asterisk: UITabControl：修改第一个TabPage关不掉的Bug  
:fa-asterisk: UIDataGridView：增加EnterAsTab属性，编辑输入时，用Enter键代替Tab键跳到下一个单元格  
:fa-asterisk: UILineChart：增加鼠标框选放大，可多次放大，右键点击恢复一次，双击恢复  
:fa-asterisk: UITitlePanel：修复OnMouseMove事件  
:fa-asterisk: UITrackBar：增加垂直显示方式  
:fa-asterisk: UIFlowLayoutPanel：修改了一处因为其加入控件大小发生变化而引起的滚动条出错  

#### 2021-02-26 V3.0.1  
:fa-plus: UIForm：标题栏增加扩展按钮  
:fa-plus: UIHeaderButton：新增大图标的导航按钮  
:fa-plus: 新增UIComboboxEx，从Combobox原生控件继承，以方便做查询过滤等操作  
:fa-asterisk: UIForm：修正不显示标题栏时，标题栏位置可放置控件  
:fa-asterisk: UIListBox：增加一些原有属性  
:fa-asterisk: FCombobox：增加数据绑定Demo  
:fa-asterisk: UICombobox：更改索引改变事件的多次触发  
:fa-asterisk: UIForm：修改一处Icon图片显示的问题  
:fa-asterisk: UIEditForm：修改通过代码生成窗体控件的TabIndex  
:fa-asterisk: UIDatePicker，UIDateTimePicker：将日期选择控件的最小值调整为1900年  
:fa-asterisk: UIHeaderButton：将其命名空间从Sunny.UI.Control改为Sunny.UI  

#### 2021-01-26 V3.0.0
:fa-plus: 同时兼容.Net Framework 4.0:fa-plus:、.Net Core3.1、.Net 5 框架  
:fa-asterisk: 更新UIMessageTip  
:fa-asterisk: UIForm：增加ShowTitleIcon用来显示标题栏图标，与ShowIcon分开  
:fa-asterisk: UINavBar：增加下拉菜单可设置自动高度或者固定高度，可显示ImageList绑定  
:fa-asterisk: UIDataGridView更新行头和列头的选中颜色  

#### 2021-01-05 V2.2.10  
:fa-asterisk: V2.2 .Net Framewok 4.0最终版本  
:fa-asterisk: V3.0 开始将同时兼容.Net Framework 4.0:fa-plus:、.Net Core3.1、.Net 5 框架  

#### 2020-12-20 V2.2.9  
:fa-plus: UIWaitForm：等待窗体  
:fa-plus: UIComboTreeView：新增下拉框TreeView  
:fa-plus: UIMessageForm：消息提示框增加黑色半透明遮罩层  
:fa-plus: Win32API：新增Win32API函数  
:fa-plus: UJsonConfig：不引用第三方控件，用.Net自带的序列化实现Json，增加Json文件配置类  
:fa-plus: UIDataGridViewForm：增加了一个表格模板基类  
:fa-asterisk: UIDataGridView：修改DataSource赋值后Column改变引起的水平滚动条错误  
:fa-asterisk: UIDoubleUpDown，UIIntegerUpDown：增加双击可编辑数值  
:fa-asterisk: UINavMenu：增加选中后图标的背景色或应用选中图片索引  
:fa-asterisk: 页面框架增加页面内跳转方法  
:fa-asterisk: 日期、时间选择框增加CanEmpty，输入可为空  

#### 2020-10-12 V2.2.8  
:fa-plus: UILineChart：完成曲线图表  
:fa-plus: UIScale：增加坐标轴刻度计算类  
:fa-plus: UIFlowLayoutPanel：增加  
:fa-plus: UIBarChartEx：增加了一个新的柱状图类型，序列个数可以不相等  
:fa-plus: UDateTimeInt64：增加DateTimeInt64类，时间整形互转类  
:fa-asterisk: UIForm：增加窗体阴影  
:fa-asterisk: UIMainFrame：页面框架增加Selecting事件，在页面切换时执行该事件  
:fa-asterisk: UITextBox：解决Anchor包含Top、Bottom时，在窗体最小化后恢复时高度变化  
:fa-asterisk: UISwitch：增加长方形形状开关，取消长宽比锁定  
:fa-asterisk: UITreeView：背景色可改，设置FillColor，以及SystemCustomMode = true  
:fa-asterisk: UIDataGridView：解决水平滚动条在有列冻结时出错的问题  

#### 2020-09-17 V2.2.7  
:fa-plus: 新增双主键线程安全字典，分组线程安全字典  
:fa-plus: UIHorScrollBarEx，UIVerScrollBarEx：重写了两个滚动条  
:fa-asterisk: UIForm：恢复了WindowState，增加了窗体可拉拽调整大小  
:fa-asterisk: 增加控件属性显示值及Sunny UI分类  
:fa-asterisk: UIDateTimePicker,UITimePicker：更改滚轮选择时间的方向  
:fa-asterisk: UIButton：Tips颜色可设置  
:fa-asterisk: UIChart：增加图表的边框线颜色设置  
:fa-asterisk: UITextBox：增加FocusedSelectAll属性，激活时全选  
:fa-asterisk: UINavBar：增加节点的Image绘制  
:fa-asterisk: UIDataGridView：调整水平滚动条  
:fa-asterisk: UIButton：添加'是否启用双击事件'属性，解决连续点击效率问题  
:fa-asterisk: UIDataGridView：更新了水平和垂直滚动条的显示，优化滚动效果  
:fa-asterisk: UIBbutton：空格键按下press背景效果  
:fa-asterisk: UIListBox优化滚轮快速滚动流畅性  
:fa-asterisk: UIBarChart：可设置柱状图最小宽度  
:fa-asterisk: UIIntegerUpDown, UIDoubleUpDown：增加字体调整  
:fa-asterisk: UITabControl：标题垂直居中  
:fa-asterisk: UITreeView：更新可设置背景色  
:fa-asterisk: UIDatePicker，UITimePicker，UIDateTimePicker：可编辑输入，日期范围控制  
:fa-asterisk: UIDatePicker：更改日期范围最小值和最大值  
:fa-asterisk: UITitlePanel：更新大小调整后的按钮位置  

#### 2020-07-30 V2.2.6  
:fa-plus: UIPagination：新增分页控件  
:fa-plus: UIToolTip：新增控件，可修改字体  
:fa-plus: UIHorScrollBar：新增水平滚动条  
:fa-plus: UIWaitingBar：新增等待滚动条控件  
:fa-asterisk: UIDataGridView：重绘水平滚动条，更新默认设置为原生控件设置  
:fa-asterisk: UITitlePanel：增加可收缩选项  
:fa-asterisk: UIPieChart,UIBarChart：增加序列自定义颜色  
:fa-asterisk: UISymbolButton：增加Image属性，增加图片和文字的摆放位置  
:fa-asterisk: UIButton：增加Selected及选中颜色配置  
:fa-asterisk: UIForm：支持点击窗体任务栏图标，可以进行最小化  
:fa-asterisk: UIForm：增加标题栏ICON图标绘制  
:fa-asterisk: UIDateTimePicker：重写下拉窗体，缩短创建时间  
:fa-asterisk: UITreeView：全部重写，增加圆角，CheckBoxes等  
:fa-asterisk: UIDatePicker：重写下拉窗体，缩短创建时间  
:fa-asterisk: UICheckBoxGroup,UIRadioButtonGroup：可以设置初始选中值  
:fa-asterisk: UILedBulb：边缘平滑  
:fa-asterisk: UIForm：仿照QQ，重绘标题栏按钮  

#### 2020-06-29 V2.2.5  
:fa-plus: UIDoughnutChart：环状图  
:fa-plus: UILoginForm：登录窗体  
:fa-plus: UIScrollingText：滚动文字  
:fa-plus: UIBarChart：柱状图  
:fa-plus: UIPieChart：饼状图  
:fa-plus: UIRichTextBox：富文本框  
:fa-plus: UIBattery：电池电量显示  
:fa-plus: UIDatetimePicker：日期时间选择框  
:fa-plus: UIColorPicker：颜色选择框  
:fa-plus: UITimePicker：时间选择框  
:fa-plus: UIMessageTipHelper：增加MessageTip扩展方法  
:fa-asterisk: UIComboBox：增加数据绑定  
:fa-asterisk: 页面框架支持通过PageIndex和PageGuid关联  
:fa-asterisk: UITextBox：增加Multiline属性，增加滚动条  
:fa-asterisk: UITabControl：新增关闭按钮，重绘左右移动按钮  
:fa-asterisk: UIForm：更新标题移动、双击最大化/正常、到顶最大化、最大化后拖拽正常  
:fa-asterisk: UINavMenu：增加字体图标显示  
:fa-asterisk: 字体图标字体调整从资源文件中加载字体，不用另存为文件  
:fa-asterisk: UIListBox 增加跟随鼠标滑过高亮  
:fa-asterisk: UIDatePicker：重写日期选择界面  
:fa-asterisk: UIButton：增加ShowFocusLine，可获得焦点并显示  

#### 2020.05.05 V2.2.5  
:fa-plus: 增加页面框架
:fa-plus: 增加下拉框窗体，进度提升窗体  
:fa-plus: UITreeView  

#### 2020.04.25 V2.2.4  
:fa-asterisk: 更新主题风格类，各控件主题颜色调用不交叉，便于新增主题  
:fa-plus: 更新Sunny.Demo程序  
:fa-plus: 增加UIDataGridView，基于DataGridView增强、美化  
:fa-minus: UIGrid效率待改，暂时隐藏  

#### 2020.04.19 V2.2.3  
:fa-plus: UICheckBoxGroup,UIRadioButtonGroup  

#### 2020.04.11 V2.2.2  
:fa-plus: 新增UIGrid  
:fa-minus: 继承DataGridView更改主题风格的UIGridView  

#### 2020.02.15 V2.2.1  
:fa-asterisk: Bug修复  

#### 2020.01.01 V2.2.0  
:fa-asterisk: 增加文件说明，为开源做准备  
:fa-plus: 增加Office主题风格  

#### 2019.10.01 V2.1.0  
:fa-plus: 增加Element主题风格  

#### 2019.03.12 V2.0.0  
:fa-plus: 增加自定义控件  

#### 2012.03.31 V1.0.0  
:fa-plus: 增加工具类、扩展类  