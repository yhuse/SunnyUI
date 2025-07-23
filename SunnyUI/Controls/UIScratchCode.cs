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
 * 文件名称: UIScratchCode.cs
 * 文件说明: 刮刮卡类型验证码控件
 * 当前版本: V3.8
 * 创建日期: 2025-07-20
 *
 * 2025-07-20: V3.8.6 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI;

/// <summary>
/// 刮刮卡类型验证码控件
/// </summary>
[DefaultEvent("Click")]
[DefaultProperty("Text")]
[ToolboxItem(true)]
[Description("验证码控件")]
public partial class UIScratchCode : UIControl
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public UIScratchCode()
    {
        SetStyleFlags();
        fillColor = UIStyles.Blue.PlainColor;
        foreColor = UIStyles.Blue.RectColor;
        Width = 100;
        Height = 35;

        MakeCover();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            cover?.Dispose();
            cover = null;

            _Image?.Dispose();
            _Image = null;
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// 设置主题样式
    /// </summary>
    /// <param name="uiColor">主题样式</param>
    public override void SetStyleColor(UIBaseStyle uiColor)
    {
        base.SetStyleColor(uiColor);
        fillColor = uiColor.PlainColor;
        foreColor = uiColor.RectColor;
    }

    /// <summary>
    /// 边框颜色
    /// </summary>
    [Description("边框颜色"), Category("SunnyUI")]
    [DefaultValue(typeof(Color), "80, 160, 255")]
    public Color RectColor
    {
        get => rectColor;
        set => SetRectColor(value);
    }

    /// <summary>
    /// 字体颜色
    /// </summary>
    [Description("字体颜色"), Category("SunnyUI")]
    [DefaultValue(typeof(Color), "80, 160, 255")]
    public override Color ForeColor
    {
        get => foreColor;
        set => SetForeColor(value);
    }

    /// <summary>
    /// 双击更新验证码
    /// </summary>
    /// <param name="e">参数</param>
    protected override void OnDoubleClick(EventArgs e)
    {
        base.OnDoubleClick(e);

        MakeCover();
        _Image?.Dispose();
        _Image = null;

        Invalidate();
    }

    /// <summary>
    /// 绘制填充颜色
    /// </summary>
    /// <param name="g">绘图图面</param>
    /// <param name="path">绘图路径</param>
    protected override void OnPaintFill(Graphics g, GraphicsPath path)
    {
        base.OnPaintFill(g, path);

        _Image ??= CreateImage(RandomChars(CodeLength));
    }

    /// <summary>
    /// 绘制前景颜色
    /// </summary>
    /// <param name="g">绘图图面</param>
    /// <param name="path">绘图路径</param>
    protected override void OnPaintFore(Graphics g, GraphicsPath path)
    {
        if (Text != "") Text = "";
    }

    /// <summary>
    /// Base32字母表：该字母表排除了I、 L、O、U字母，目的是避免混淆和滥用，ULID规范的字符表
    /// https://www.crockford.com/base32.html
    /// </summary>
    private const string Base32 = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";

    /// <summary>
    /// 生成字母和数字的随机字符串
    /// </summary>
    /// <param name="length">长度</param>
    /// <returns>结果</returns>
    private static string RandomChars(int length = 10)
    {
        return RandomBase(Base32.ToCharArray(), length);
    }

    private static string RandomBase(char[] pattern, int length)
    {
        if (pattern.IsNullOrEmpty())
        {
            throw new ArgumentNullException();
        }

        string result = "";
        Random random = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < length; i++)
        {
            result += pattern[random.Next(0, pattern.Length)];
        }

        return result;
    }

    /// <summary>
    /// 用于覆盖“真实”图片的图片
    /// </summary>
    private Bitmap cover;

    /// <summary>
    /// 为 true 时，鼠标移动会移除“覆盖层”图片
    /// </summary>
    private bool scratching = false;

    /// <summary>
    /// 用户上一次刮开的点
    /// </summary>
    private Point last = new Point(0, 0);

    /// <summary>
    /// 顶部被“刮开”时显示的图片
    /// </summary>
    private Image _Image = null;

    /// <summary>
    /// 用于刮开的画笔——越宽一次性显示越多
    /// </summary>
    /// <remarks>
    /// 此画笔的颜色必须与 Paint 事件中用于设置“覆盖层”图片透明色的颜色一致。
    /// </remarks>
    private Pen _ScratchPen = new Pen(Brushes.Red, 5.0F);

    /// <summary>
    /// 用于填充覆盖图片的画刷
    /// </summary>
    private Brush _CoverBrush = Brushes.LightGray;

    [DefaultValue(4)]
    [Description("验证码长度"), Category("SunnyUI")]
    public int CodeLength { get; set; } = 4;

    /// <summary>
    /// 用于刮开覆盖层的画笔：画笔越大，刮开的面积越大！
    /// </summary>
    /// <remarks>
    /// 更改刮刮笔会重置覆盖层。
    /// 不要将刮刮笔颜色设置为覆盖层颜色！
    /// </remarks>
    public Pen ScratchPen
    {
        get { return _ScratchPen; }
        set { _ScratchPen = value; MakeCover(); }
    }

    /// <summary>
    /// 用于填充覆盖图片的画刷
    /// </summary>
    /// <remarks>
    /// 更改覆盖画刷会重置覆盖层。
    /// 不要将覆盖层颜色设置为刮刮笔颜色！
    /// </remarks>
    public Brush CoverBrush
    {
        get { return _CoverBrush; }
        set { _CoverBrush = value; MakeCover(); }
    }

    /// <summary>
    /// 获取当前覆盖层的副本（如果需要可以检查已刮开的区域）
    /// </summary>
    public Image Cover
    {
        get { return new Bitmap(cover); }
    }

    /// <summary>
    /// 绘制图片及其覆盖层。
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_Image != null)
        {
            e.Graphics.DrawImage(_Image, 0, 0, Width, Height);
            cover.MakeTransparent(_ScratchPen.Color);
            e.Graphics.DrawImage(cover, 0, 0, Width, Height);
        }
    }

    /// <summary>
    /// 控件尺寸变化时，重建覆盖层。
    /// </summary>
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        MakeCover();

        _Image?.Dispose();
        _Image = null;
    }

    /// <summary>
    /// 鼠标按下，开始从该点“刮开”。
    /// </summary>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        scratching = true;
        last = e.Location;
    }

    /// <summary>
    /// 鼠标松开，停止“刮开”
    /// </summary>
    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        scratching = false;
    }

    /// <summary>
    /// 如果正在刮开，则在覆盖层上绘制，使透明层显示真实图片。
    /// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (scratching)
        {
            using (Graphics g = Graphics.FromImage(cover))
            {
                g.DrawLine(_ScratchPen, last, e.Location);
            }

            last = e.Location;
            Invalidate();
        }
    }

    /// <summary>
    /// 创建足够大的覆盖层以遮住“真实”图片
    /// </summary>
    private void MakeCover()
    {
        if (cover != null) cover.Dispose();
        cover = new Bitmap(Width, Height);
        using Graphics g = Graphics.FromImage(cover);
        g.FillRegion(_CoverBrush, new Region(new Rectangle(0, 0, Width, Height)));
    }

    [DefaultValue(18)]
    [Description("验证码字体大小"), Category("SunnyUI")]
    public int CodeFontSize { get; set; } = 18;

    [DefaultValue(null)]
    [Description("验证码文字"), Category("SunnyUI")]
    public string Code { get; private set; }

    /// <summary>
    /// 生成图片
    /// </summary>
    /// <param name="code">验证码表达式</param>
    private Bitmap CreateImage(string code)
    {
        byte gdiCharSet = UIStyles.GetGdiCharSet(Font.Name);
        using Font font = new Font(Font.Name, CodeFontSize, FontStyle.Bold, GraphicsUnit.Point, gdiCharSet);
        using Font fontex = font.DPIScaleFont(font.Size);
        Code = code;
        Size sf = TextRenderer.MeasureText(code, fontex);
        Bitmap image = new Bitmap((int)sf.Width + 16, Height - 2);

        //创建画布
        Graphics g = Graphics.FromImage(image);
        Random random = new Random();

        //图片背景色
        g.Clear(fillColor);
        g.DrawRoundRectangle(rectColor, new Rectangle(0, 0, image.Width - 1, image.Height - 1), Radius);

        //画图片背景线
        for (int i = 0; i < 5; i++)
        {
            int x1 = random.Next(image.Width);
            int x2 = random.Next(image.Width);
            int y1 = random.Next(image.Height);
            int y2 = random.Next(image.Height);

            g.DrawLine(Color.Black, x1, y1, x2, y2, true);
        }

        //画图片的前景噪音点
        for (int i = 0; i < 30; i++)
        {
            int x = random.Next(image.Width);
            int y = random.Next(image.Height);
            image.SetPixel(x, y, Color.FromArgb(random.Next()));
        }

        using Brush br = new SolidBrush(foreColor);
        g.DrawString(code, fontex, br, image.Width / 2.0f - sf.Width / 2.0f, image.Height / 2.0f - sf.Height / 2.0f);
        return image;
    }
}