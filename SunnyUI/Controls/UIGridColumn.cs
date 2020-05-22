/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2020 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@qq.com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIGridColumn.cs
 * 文件说明: 表格列定义类
 * 当前版本: V2.2
 * 创建日期: 2020-04-15
 *
 * 2020-04-11: V2.2.2 增加UIGrid
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

#pragma warning disable 1591

namespace Sunny.UI
{
    public enum UIGridColumnType
    {
        Label,
        Button,
        CheckBox,
        Combobox,
        Image,
        LinkLabel,
        TextBox,
        IntTextBox,
        DoubleTextBox
    }

    public enum UIGridColumnSizeMode
    {
        Fill,
        Fixed
    }

    public delegate string UIGridColumnFormat(object obj);

    [Serializable]
    public sealed class UIGridColumn : IStyleInterface
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string FooterText { get; set; }

        /// <summary>
        /// 标题对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        public StringAlignment HeaderTextAlignment { get; set; } = StringAlignment.Center;

        /// <summary>
        /// 页脚对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        public StringAlignment FooterTextAlignment { get; set; } = StringAlignment.Center;

        /// <summary>
        /// Fill模式，自动填充时所占比率
        /// </summary>
        [DefaultValue(100)]
        public int FillWeight { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        [DefaultValue(UIGridColumnType.Label)]
        public UIGridColumnType ColumnType { get; set; }

        /// <summary>
        /// 列宽度类型
        /// </summary>
        [DefaultValue(UIGridColumnSizeMode.Fill)]
        public UIGridColumnSizeMode SizeMode { get; set; } = UIGridColumnSizeMode.Fill;

        /// <summary>
        /// 绑定数据名称
        /// </summary>
        public string DataPropertyName { get; set; }

        /// <summary>
        /// Fixed模式，固定宽度
        /// </summary>
        [DefaultValue(100)]
        public int Width { get; set; } = 100;

        /// <summary>
        /// 文字对齐方式
        /// </summary>
        [DefaultValue(StringAlignment.Center)]
        public StringAlignment TextAlignment { get; set; } = StringAlignment.Center;

        public event UIGridColumnFormat OnDataFormat;

        /// <summary>
        /// 类型为按钮时按钮标题
        /// </summary>
        public string ButtonText { get; set; } = "Edit";

        [DefaultValue(true)]
        public bool ReadOnly { get; set; } = true;

        private List<object> ComboboxItems = new List<object>();

        public List<object> GetComboboxItems()
        {
            return ComboboxItems;
        }

        public UIGridColumn()
        {
        }

        public UIGridColumn(string name, string headerText, string dataPropertyName,
            UIGridColumnType columnType = UIGridColumnType.Label,
            UIGridColumnSizeMode sizeMode = UIGridColumnSizeMode.Fill, int size = 100)
        {
            Name = name;
            HeaderText = headerText;
            DataPropertyName = dataPropertyName;
            ColumnType = columnType;
            SizeMode = sizeMode;
            if (sizeMode == UIGridColumnSizeMode.Fill)
                FillWeight = size;
            else
                Width = size;
        }

        public UIGridColumn SetComboboxItems(List<object> objects)
        {
            ComboboxItems.Clear();
            foreach (object obj in objects)
            {
                ComboboxItems.Add(obj);
            }

            return this;
        }

        public string DoFormat(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (OnDataFormat == null)
            {
                return obj.ToString();
            }

            try
            {
                return OnDataFormat?.Invoke(obj);
            }
            catch
            {
                return obj.ToString();
            }
        }

        [DefaultValue(UIStyle.Blue)]
        public UIStyle Style { get; set; } = UIStyle.Blue;

        [DefaultValue(false)]
        public bool StyleCustomMode { get; set; }

        public string Version { get; } = UIGlobal.Version;

        public string TagString { get; set; }

        public Rectangle Bounds { get; set; }

        [DefaultValue(2)]
        public int TextBoxDecimals { get; set; } = 2;

        public string Guid { get; private set; } = RandomEx.RandomLong().ToString();
    }
}