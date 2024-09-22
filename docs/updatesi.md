# 升级指南
---
## 3.5.2-->3.6.0
- 本次升级对主题进行了重构，隐藏了修改自定义颜色时需要设置的StyleCustomMode属性。    
 **不需要设置全局主题时**
   所有控件的颜色修改所见即所得，窗体关闭后再打开，所设置的颜色会保存。默认的主题为UIStyle.Inherited(继承的全局主题)   
    **需要设置全局主题时**
   不切换主题，和上面的设置一致，如需切换主题仍然保留控件用户自定义修改颜色，在修改完颜色后，将控件的主题设置为UIStyle.Custom(自定义)    
   重构主要是方便了新手用户的使用体验。    
   
- UICheckBoxGroup    
  升级后原有的值变化事件    
  private void uiCheckBoxGroup1_ValueChanged(object sender, int index, string text, bool isChecked)    
  修改为：    
  private void uiCheckBoxGroup1_ValueChanged(object sender, CheckBoxGroupEventArgs e)    
  按Demo示例操作修改即可。    
  
- UIForm: 修改默认ShowShadow边框阴影打开，ShowRadius显示圆角关闭
ShowShadow打开时，在Win11显示圆角窗体，不需要可以关闭

