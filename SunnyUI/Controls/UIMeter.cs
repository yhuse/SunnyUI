/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2025 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UIMeter.cs
 * 文件说明: 仪表
 * 当前版本: V3.8
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V3.8.3 增加文件说明
******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI;

public class UIMeter : UIUserControl
{
    public UIMeter()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        base.DoubleBuffered = true;
        UpdateStyles();

        Size = new Size(300, 300);
        RectSides = ToolStripStatusLabelBorderSides.None;
    }

    private Point _startPos = new(32, 32);
    [Description("显示起始位置")]
    public Point StartPos { get => _startPos; set { _startPos = value; Invalidate(); } }

    //----------------范围---------------------

    private float _minValue;
    [Description("最小值")]
    public float MinValue { get => _minValue; set { _minValue = value; Invalidate(); } }

    private float _maxValue = 100;
    [Description("最大值")]
    public float MaxValue { get => _maxValue; set { _maxValue = value; Invalidate(); } }

    //---------------阈值及颜色-----------------
    private Color _arcColor = UIColor.Blue;
    [Description("圆弧默认颜色")]
    public Color ArcColor { get => _arcColor; set { _arcColor = value; Invalidate(); } }

    //--------指针------------
    private Color _needleColor = UIColor.Blue;
    [Description("指针颜色")]
    public Color NeedleColor { get => _needleColor; set { _needleColor = value; Invalidate(); } }

    private int _needleInner = 4;
    [Description("指针圆圈内径")]
    public int NeedleInner { get => _needleInner; set { _needleInner = Math.Max(0, value); Invalidate(); } }

    private int _needleOuter = 16;
    [Description("指针圆圈外径")]
    public int NeedleOuter { get => _needleOuter; set { _needleOuter = Math.Max(4, value); Invalidate(); } }

    private int _needleLength = 60;
    [Description("指针圆圈长度")]
    public int NeedleLength { get => _needleLength; set { _needleLength = Math.Max(10, value); Invalidate(); } }

    //----------数值--------------
    private float _thisValue;
    [Description("当前数值")]
    public float Value { get => _thisValue; set { _thisValue = Math.Min(MaxValue, Math.Max(MinValue, value)); Invalidate(); } }

    private int _valueDecimalPlaces = 2;
    [Description("数值小数位数")]
    public int ValueDecimalPlaces { get => _valueDecimalPlaces; set { _valueDecimalPlaces = Math.Max(0, value); Invalidate(); } }

    //-----------刻度--------------
    private Font _scaleFont = new("宋体", 10.5f);
    [Description("刻度字体")]
    public Font ScaleFont { get => _scaleFont; set { _scaleFont = value; Invalidate(); } }

    private int _scaleDecimalPlaces;
    [Description("大刻度显示小数位数")]
    public int ScaleDecimalPlaces { get => _scaleDecimalPlaces; set { _scaleDecimalPlaces = Math.Max(0, value); Invalidate(); } }

    private int _scaleDivisions = 10;
    [Description("大刻度个数")]
    public int ScaleDivisions { get => _scaleDivisions; set { _scaleDivisions = Math.Max(1, value); Invalidate(); } }

    private int _scaleSubDivisions = 5;
    [Description("小刻度个数")]
    public int ScaleSubDivisions { get => _scaleSubDivisions; set { _scaleSubDivisions = Math.Max(1, value); Invalidate(); } }

    private int _scaleLength = 8;
    [Description("大刻度长度")]
    public int ScaleLength { get => _scaleLength; set { _scaleLength = Math.Max(2, value); Invalidate(); } }

    private int _scaleSubLength = 4;
    [Description("小刻度长度")]
    public int ScaleSubLength { get => _scaleSubLength; set { _scaleSubLength = Math.Max(1, value); Invalidate(); } }

    private int _scaleWidth = 2;
    [Description("大刻度线宽")]
    public int ScaleWidth { get => _scaleWidth; set { _scaleWidth = Math.Max(1, value); Invalidate(); } }

    private int _scaleSubWidth = 1;
    [Description("小刻度线宽")]
    public int ScaleSubWidth { get => _scaleSubWidth; set { _scaleSubWidth = Math.Max(1, value); Invalidate(); } }

    private int _scaleInterval = 3;
    [Description("刻度线和弧线直接的间隔")]
    public int ScaleInterval { get => _scaleInterval; set { _scaleInterval = Math.Max(0, value); Invalidate(); } }

    private int _scaleTextInterval = 22;
    [Description("刻度文本和弧线直接的间隔")]
    public int ScaleTextInterval { get => _scaleTextInterval; set { _scaleTextInterval = Math.Max(10, value); Invalidate(); } }

    //-------------单位-----------------
    private string _unit = "";
    [Description("单位")]
    public string Unit { get => _unit; set { _unit = value; Invalidate(); } }

    //------------圆弧------------------
    private int _innerSize = 110;
    [Description("圆弧内圈尺寸")]
    public int InnerSize { get => _innerSize; set { _innerSize = Math.Max(10, value); Invalidate(); } }

    private int _outerSize = 120;
    [Description("圆弧外圈尺寸")]
    public int OuterSize { get => _outerSize; set { _outerSize = Math.Max(InnerSize + 1, value); Invalidate(); } }

    private int _startAngle = 225;
    private int _stopAngle = 135;

    [Description("起始角度")]
    public int StartAngle { get => _startAngle; set { _startAngle = value; Invalidate(); } }

    [Description("终止角度")]
    public int StopAngle { get => _stopAngle; set { _stopAngle = value; Invalidate(); } }

    private Point _valueOffset = new(0, 0);

    [Description("数值偏移")]
    public Point ValueOffset
    {
        get => _valueOffset;
        set { _valueOffset = value; Invalidate(); }
    }

    private bool _showValue = true;
    [Description("是否显示数值")]
    public bool ShowValue
    {
        get => _showValue;
        set { _showValue = value; Invalidate(); }
    }

    public struct Scope
    {
        public float MinValue;
        public float MaxValue;
        public Color Color;

        public Scope(float minvalue, float maxvalue, Color color)
        {
            MinValue = minvalue; MaxValue = maxvalue; Color = color;
        }
    }

    private float _defaultFontSize = -1;

    public override void SetDPIScale()
    {
        if (DesignMode) return;
        if (!UIDPIScale.NeedSetDPIFont()) return;
        base.SetDPIScale();
        if (_defaultFontSize < 0) _defaultFontSize = _scaleFont.Size;
        _scaleFont = _scaleFont.DPIScaleFont(_defaultFontSize);
    }

    public override void SetStyleColor(UIBaseStyle uiColor)
    {
        base.SetStyleColor(uiColor);
        ArcColor = uiColor.ButtonFillColor;
        NeedleColor = uiColor.ButtonFillColor;
    }

    private readonly List<Scope> _scopes = [];

    public void Clear()
    {
        _scopes.Clear();
        Invalidate();
    }

    public void AddScope(float minvalue, float maxvalue, Color color)
    {
        _scopes.Add(new Scope(minvalue, maxvalue, color));
    }

    //-------------绘制------------------
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (MaxValue <= MinValue)
        {
            e.Graphics.DrawString("请设置显示范围", Font, Brushes.White, new RectangleF(0, 0, Width, Height));
            return;
        }

        int totalAngle = StopAngle + 360 - StartAngle;
        float totalValue = MaxValue - MinValue;

        var center = new Point(_startPos.X + _outerSize, _startPos.Y + _outerSize);

        //绘制圆弧
        e.Graphics.FillFan(ArcColor, center, InnerSize, OuterSize, StartAngle - 90, totalAngle);

        foreach (var scope in _scopes)
        {
            float step0 = (scope.MinValue - _minValue) / totalValue * totalAngle;
            float step1 = (scope.MaxValue - scope.MinValue) / totalValue * totalAngle;
            e.Graphics.FillFan(scope.Color, center, InnerSize, OuterSize, StartAngle + step0 - 90, step1);
        }

        PointF pt1 = center.CalcAzRangePoint(InnerSize, StartAngle);
        PointF pt2 = center.CalcAzRangePoint(InnerSize, StopAngle);

        string str = Value.ToString("F" + ValueDecimalPlaces) + Unit;
        SizeF sf = e.Graphics.MeasureString(str, Font);
        using var vbr = new SolidBrush(ForeColor);

        //绘制数值
        if (ShowValue)
        {
            e.Graphics.DrawString(str, Font, vbr, pt1.X + (pt2.X - pt1.X - sf.Width) / 2 + ValueOffset.X, pt1.Y - sf.Height + ValueOffset.Y);
        }

        float scaleAngle = totalAngle * 1.0f / ScaleDivisions;
        float scaleValue = totalValue * 1.0f / _scaleDivisions;
        float scaleSubAngle = scaleAngle * 1.0f / ScaleSubDivisions;
        using var pen1 = new Pen(ForeColor, ScaleWidth);
        using var pen2 = new Pen(ForeColor, ScaleSubWidth);
        using var sbr = new SolidBrush(ForeColor);

        //绘制刻度线
        for (int i = 0; i <= _scaleDivisions; i++)
        {
            float ag = StartAngle + scaleAngle * i;
            float va = MinValue + scaleValue * i;
            pt1 = center.CalcAzRangePoint(InnerSize - ScaleInterval, ag);
            pt2 = center.CalcAzRangePoint(InnerSize - ScaleInterval - ScaleLength, ag);
            e.Graphics.SetHighQuality();
            e.Graphics.DrawLine(pen1, pt1, pt2);

            pt1 = center.CalcAzRangePoint(InnerSize - ScaleTextInterval, ag);
            str = va.ToString("F" + ScaleDecimalPlaces);
            sf = e.Graphics.MeasureString(str, ScaleFont);
            e.Graphics.DrawString(str, ScaleFont, sbr, pt1.X - sf.Width / 2, pt1.Y - sf.Height / 2);

            if (i == _scaleDivisions) break;
            for (int j = 1; j < _scaleSubDivisions; j++)
            {
                float dag = ag + scaleSubAngle * j;
                pt1 = center.CalcAzRangePoint(InnerSize - ScaleInterval, dag);
                pt2 = center.CalcAzRangePoint(InnerSize - ScaleInterval - ScaleSubLength, dag);
                e.Graphics.SetHighQuality();
                e.Graphics.DrawLine(pen2, pt1, pt2);
            }
        }

        //绘制指针
        e.Graphics.FillFan(NeedleColor, center, NeedleInner, _needleOuter, 0, 360);
        float valueAngle = StartAngle + (Value - _minValue) / totalValue * totalAngle;
        pt1 = center.CalcAzRangePoint(_needleOuter, valueAngle - 25);
        pt2 = center.CalcAzRangePoint(_needleOuter, valueAngle + 25);
        PointF pt3 = center.CalcAzRangePoint(_needleLength, valueAngle);
        using var nbr = new SolidBrush(NeedleColor);
        e.Graphics.SetHighQuality();
        e.Graphics.FillClosedCurve(nbr, [pt1, pt2, pt3]);

        using var bbr = new SolidBrush(BackColor);
        e.Graphics.SetHighQuality();
        e.Graphics.FillEllipse(bbr, new Rectangle(center.X - _needleInner, center.Y - _needleInner, _needleInner * 2, _needleInner * 2));

        e.Graphics.SetDefaultQuality();
    }
}