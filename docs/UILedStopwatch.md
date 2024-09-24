# UILedStopwatch
---
-  **UILedStopwatch** 
LED计时器。

- 默认属性：Text
- 默认事件：TimerTick
- 属性列表

| 属性        | 说明     | 类型     |  默认值   |
|-----------|--------|--------|-------|
| Text  |获取或设置显示的文本  | string | -   | 
| CharCount| 显示字符个数 | int  | 10 |
| ShowType| 显示方式 | TimeShowType| -|
| Active| 是否开始工作 | bool| false |
| BorderColor | 边框颜色   | Color  | -   |
| BorderInColor | 内线颜色  | Color  | -   |
| LedBackColor | LED背景色   | Color  | -   |
| BorderWidth| 边框宽度 | int  | 1 |
| BorderInWidth| 内线宽度 | int  | 1 |
| IntervalIn | LED亮块间距 | int  | 1 |
| IntervalOn | LED亮块大小 | int  | 2 |
| IntervalH | 左右边距 | int  | 2 |
| IntervalV | 上下边距 | int  | 5 |
| TagString | 获取或设置包含有关控件的数据的对象字符串   | string | -   | 
| Version | 版本  | string  |  -     |
| TimeSpan| 开始计时后用去的时间| TimeSpan|  -     |
| IsWorking| 是否开始工作| bool| false |



- 控件宽度    
  因为本控件模拟的时一个LED点阵显示屏，LED点阵显示屏宽度由可显示字符个数决定。    
  所以需要调整控件的宽度，不是设置Width属性，而是设置CharCount属性。    

  

- 事件   
  TimerTick：当定时器启动后，Text变化时触发一次    

  

- 示例   
  ![输入图片说明](./assets/100744_1b3a73c1_416720.png)