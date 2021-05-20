\+ 增加； \- 删除； \* 修改

#### 2021-04-11 V3.0.2
\+ UIMarkLabel：增加带颜色标签的Label</br>
\+ UIRoundProcess：圆形滚动条</br>
\+ UIBreadcrumb：增加面包屑导航</br>
\+ UILedLabel：增加Led标签</br>
\* UIHeaderButton：在工具箱中显示</br>
\* UILineChart：支持拖拽选取放大</br>
\* UIDateTimePicker：修复下拉选择日期后关闭的Bug</br>
\* UINavMenu：增加设置二级菜单底色</br>
\* UIColorPicker：增加单击事件以选中颜色</br>
\* UITitlePage：增加ShowTitle可控制是否显示标题</br>
\* UINavBar：增加可设置背景图片</br>
\* 框架增加IFrame接口，方便页面跳转</br>
\* UIDataGridView：修改垂直滚动条和原版一致，并增加翻页方式滚动</br>
\* UIPagination: 修正因两次查询数量相等而引起的不刷新</br>
\* UIHeaderButton: 增加字体图标背景时鼠标移上背景色</br>
\* UITabControl：修改第一个TabPage关不掉的Bug</br>
\* UIDataGridView：增加EnterAsTab属性，编辑输入时，用Enter键代替Tab键跳到下一个单元格</br>
\* UILineChart：增加鼠标框选放大，可多次放大，右键点击恢复一次，双击恢复</br>
\* UITitlePanel：修复OnMouseMove事件</br>
\* UITrackBar：增加垂直显示方式</br>
\* UIFlowLayoutPanel：修改了一处因为其加入控件大小发生变化而引起的滚动条出错。</br>

#### 2021-02-26 V3.0.1
\+ UIForm：标题栏增加扩展按钮</br>
\+ UIHeaderButton：新增大图标的导航按钮</br>
\+ 新增UIComboboxEx，从Combobox原生控件继承，以方便做查询过滤等操作</br>
\* UIForm：修正不显示标题栏时，标题栏位置可放置控件</br>
\* UIListBox：增加一些原有属性</br>
\* FCombobox：增加数据绑定Demo</br>
\* UICombobox：更改索引改变事件的多次触发</br>
\* UIForm：修改一处Icon图片显示的问题</br>
\* UIEditForm：修改通过代码生成窗体控件的TabIndex</br>
\* UIDatePicker，UIDateTimePicker：将日期选择控件的最小值调整为1900年</br>
\* UIHeaderButton：将其命名空间从Sunny.UI.Control改为Sunny.UI</br>

#### 2021-01-26 V3.0.0
\+ 同时兼容.Net Framework 4.0+、.Net Core3.1、.Net 5 框架</br>
\* 更新UIMessageTip</br>
\* UIForm：增加ShowTitleIcon用来显示标题栏图标，与ShowIcon分开</br>
\* UINavBar：增加下拉菜单可设置自动高度或者固定高度，可显示ImageList绑定</br>
\* UIDataGridView更新行头和列头的选中颜色</br>

#### 2021-01-05 V2.2.10
\* V2.2 .Net Framewok 4.0最终版本</br>
\* V3.0 开始将同时兼容.Net Framework 4.0+、.Net Core3.1、.Net 5 框架</br>

#### 2020-12-20 V2.2.9
\+ UIWaitForm：等待窗体</br>
\+ UIComboTreeView：新增下拉框TreeView</br>
\+ UIMessageForm：消息提示框增加黑色半透明遮罩层</br>
\+ Win32API：新增Win32API函数</br>
\+ UJsonConfig：不引用第三方控件，用.Net自带的序列化实现Json，增加Json文件配置类</br>
\+ UIDataGridViewForm：增加了一个表格模板基类</br>
\* UIDataGridView：修改DataSource赋值后Column改变引起的水平滚动条错误</br>
\* UIDoubleUpDown，UIIntegerUpDown：增加双击可编辑数值</br>
\* UINavMenu：增加选中后图标的背景色或应用选中图片索引</br>
\* 页面框架增加页面内跳转方法</br>
\* 日期、时间选择框增加CanEmpty，输入可为空</br>

#### 2020-10-12 V2.2.8
\+ UILineChart：完成曲线图表</br>
\+ UIScale：增加坐标轴刻度计算类</br>
\+ UIFlowLayoutPanel：增加</br>
\+ UIBarChartEx：增加了一个新的柱状图类型，序列个数可以不相等</br>
\+ UDateTimeInt64：增加DateTimeInt64类，时间整形互转类</br>
\* UIForm：增加窗体阴影</br>
\* UIMainFrame：页面框架增加Selecting事件，在页面切换时执行该事件</br>
\* UITextBox：解决Anchor包含Top、Bottom时，在窗体最小化后恢复时高度变化</br>
\* UISwitch：增加长方形形状开关，取消长宽比锁定</br>
\* UITreeView：背景色可改，设置FillColor，以及SystemCustomMode = true</br>
\* UIDataGridView：解决水平滚动条在有列冻结时出错的问题</br>

#### 2020-09-17 V2.2.7
\+ 新增双主键线程安全字典，分组线程安全字典</br>
\+ UIHorScrollBarEx，UIVerScrollBarEx：重写了两个滚动条</br>
\* UIForm：恢复了WindowState，增加了窗体可拉拽调整大小</br>
\* 增加控件属性显示值及Sunny UI分类</br>
\* UIDateTimePicker,UITimePicker：更改滚轮选择时间的方向</br>
\* UIButton：Tips颜色可设置</br>
\* UIChart：增加图表的边框线颜色设置</br>
\* UITextBox：增加FocusedSelectAll属性，激活时全选。</br>
\* UINavBar：增加节点的Image绘制</br>
\* UIDataGridView：调整水平滚动条</br>
\* UIButton：添加'是否启用双击事件'属性，解决连续点击效率问题</br>
\* UIDataGridView：更新了水平和垂直滚动条的显示，优化滚动效果</br>
\* UIBbutton：空格键按下press背景效果</br>
\* UIListBox优化滚轮快速滚动流畅性</br>
\* UIBarChart：可设置柱状图最小宽度</br>
\* UIIntegerUpDown, UIDoubleUpDown：增加字体调整</br>
\* UITabControl：标题垂直居中</br>
\* UITreeView：更新可设置背景色</br>
\* UIDatePicker，UITimePicker，UIDateTimePicker：可编辑输入，日期范围控制</br>
\* UIDatePicker：更改日期范围最小值和最大值</br>
\* UITitlePanel：更新大小调整后的按钮位置</br>

#### 2020-07-30 V2.2.6
\+ UIPagination：新增分页控件</br>
\+ UIToolTip：新增控件，可修改字体</br>
\+ UIHorScrollBar：新增水平滚动条</br>
\+ UIWaitingBar：新增等待滚动条控件</br>
\* UIDataGridView：重绘水平滚动条，更新默认设置为原生控件设置</br>
\* UITitlePanel：增加可收缩选项</br>
\* UIPieChart,UIBarChart：增加序列自定义颜色</br>
\* UISymbolButton：增加Image属性，增加图片和文字的摆放位置</br>
\* UIButton：增加Selected及选中颜色配置</br>
\* UIForm：支持点击窗体任务栏图标，可以进行最小化</br>
\* UIForm：增加标题栏ICON图标绘制</br>
\* UIDateTimePicker：重写下拉窗体，缩短创建时间</br>
\* UITreeView：全部重写，增加圆角，CheckBoxes等</br>
\* UIDatePicker：重写下拉窗体，缩短创建时间</br>
\* UICheckBoxGroup,UIRadioButtonGroup：可以设置初始选中值</br>
\* UILedBulb：边缘平滑</br>
\* UIForm：仿照QQ，重绘标题栏按钮。</br>

#### 2020-06-29 V2.2.5
\+ UIDoughnutChart：环状图</br>
\+ UILoginForm：登录窗体</br>
\+ UIScrollingText：滚动文字</br>
\+ UIBarChart：柱状图</br>
\+ UIPieChart：饼状图</br>
\+ UIRichTextBox：富文本框</br>
\+ UIBattery：电池电量显示</br>
\+ UIDatetimePicker：日期时间选择框</br>
\+ UIColorPicker：颜色选择框</br>
\+ UITimePicker：时间选择框</br>
\+ UIMessageTipHelper：增加MessageTip扩展方法</br>
\* UIComboBox：增加数据绑定</br>
\* 页面框架支持通过PageIndex和PageGuid关联</br>
\* UITextBox：增加Multiline属性，增加滚动条</br>
\* UITabControl：新增关闭按钮，重绘左右移动按钮</br>
\* UIForm：更新标题移动、双击最大化/正常、到顶最大化、最大化后拖拽正常</br>
\* UINavMenu：增加字体图标显示</br>
\* 字体图标字体调整从资源文件中加载字体，不用另存为文件</br>
\* UIListBox 增加跟随鼠标滑过高亮</br>
\* UIDatePicker：重写日期选择界面</br>
\* UIButton：增加ShowFocusLine，可获得焦点并显示</br>

#### 2020.05.05 V2.2.5
\+ 增加页面框架</br>
\+ 增加下拉框窗体，进度提升窗体</br>
\+ UITreeView</br>

#### 2020.04.25 V2.2.4
\* 更新主题风格类，各控件主题颜色调用不交叉，便于新增主题</br>
\+ 更新Sunny.Demo程序</br>
\+ 增加UIDataGridView，基于DataGridView增强、美化</br>
\- UIGrid效率待改，暂时隐藏</br>

#### 2020.04.19 V2.2.3
\+ UICheckBoxGroup,UIRadioButtonGroup</br>

#### 2020.04.11 V2.2.2
\+ 新增UIGrid</br>
\- 继承DataGridView更改主题风格的UIGridView</br>

#### 2020.02.15 V2.2.1
\* Bug修复</br>

#### 2020.01.01 V2.2.0
\* 增加文件说明，为开源做准备</br>
\+ 增加Office主题风格</br>

#### 2019.10.01 V2.1.0
\+ 增加Element主题风格</br>

#### 2019.03.12 V2.0.0
\+ 增加自定义控件</br>

#### 2012.03.31 V1.0.0
\+ 增加工具类、扩展类</br>