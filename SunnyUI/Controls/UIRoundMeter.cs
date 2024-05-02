/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
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
 * 文件名称: UIRoundMeter.cs
 * 文件说明: 圆形图表
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using Sunny.UI.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sunny.UI
{
    /// <summary>
    /// UIRoundMeter
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Angle")]
    public sealed class UIRoundMeter : UIControl
    {
        /// <summary>
        /// Enum TMeterType
        /// </summary>
        public enum TMeterType
        {
            /// <summary>
            /// The custom
            /// </summary>
            Custom,

            /// <summary>
            /// The GPS
            /// </summary>
            Gps,

            /// <summary>
            /// The wind
            /// </summary>
            Wind
        }

        /// <summary>
        /// Enum TRunType
        /// </summary>
        public enum TRunType
        {
            /// <summary>
            /// The clock wise
            /// </summary>
            ClockWise,

            /// <summary>
            /// The anti clock wise
            /// </summary>
            AntiClockWise
        }

        private double _angle;
        private Image _angleImage;
        private Image _backImage;

        /// <summary>
        ///     必需的设计器变量。
        /// </summary>
        private IContainer components;

        private TMeterType _meterType;
        private TRunType _runType;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UIRoundMeter()
        {
            InitializeComponent();
            SetStyleFlags(true, false);
            MeterType = TMeterType.Gps;
            _runType = TRunType.ClockWise;

            Width = 150;
            Height = 150;
        }

        /// <summary>
        /// 旋转方式
        /// </summary>
        [DefaultValue(TRunType.ClockWise)]
        [Description("旋转方式"), Category("SunnyUI")]
        public TRunType RunType
        {
            get => _runType;
            set
            {
                if (_runType == value)
                {
                    return;
                }

                _runType = value;
                Invalidate();
            }
        }

        [DefaultValue(TMeterType.Gps)]
        [Description("显示类型"), Category("SunnyUI")]
        public TMeterType MeterType
        {
            get => _meterType;
            set
            {
                if (_meterType == value)
                {
                    return;
                }

                _meterType = value;

                if (value == TMeterType.Gps)
                {
                    BackgroundImage = Resources.gps1;
                    AngleImage = Resources.gps_postion;
                }

                if (value == TMeterType.Wind)
                {
                    BackgroundImage = Resources.wind;
                    AngleImage = Resources.wind_postion;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 背景图片
        /// </summary>
        [Description("背景图片"), Category("SunnyUI")]
        public new Image BackgroundImage
        {
            get => _backImage;
            set
            {
                if (value == null)
                {
                    _backImage = null;
                    Width = 150;
                    Height = 150;
                }
                else
                {
                    _backImage = new Bitmap(value);
                    Width = _backImage.Width;
                    Height = _backImage.Height;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 箭头图片
        /// </summary>
        [Description("箭头图片"), Category("SunnyUI")]
        public Image AngleImage
        {
            get => _angleImage;
            set
            {
                _angleImage = value == null ? null : new Bitmap(value);
                Invalidate();
            }
        }

        /// <summary>
        /// 角度
        /// </summary>
        [DefaultValue(0D)]
        [Description("角度"), Category("SunnyUI")]
        public double Angle
        {
            get => _angle;
            set
            {
                if (_angle.EqualsDouble(value))
                {
                    return;
                }

                _angle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// BackgroundImageLayout
        /// </summary>
        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout { get; set; }

        /// <summary>
        ///     清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 重载绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_backImage != null)
            {
                e.Graphics.DrawImage(_backImage, Width / 2.0f - _backImage.Width / 2.0f, Height / 2.0f - _backImage.Height / 2.0f);
            }

            if (_angleImage == null)
            {
                return;
            }

            var rawImage = new Bitmap(_angleImage);
            if (_runType == TRunType.ClockWise)
            {
                var bmp = rawImage.Rotate((float)_angle, Color.Transparent);
                e.Graphics.DrawImage(bmp, Width / 2.0f - bmp.Width / 2.0f, Height / 2.0f - bmp.Height / 2.0f);
                bmp.Dispose();
            }

            if (_runType == TRunType.AntiClockWise)
            {
                var bmp = rawImage.Rotate((float)(360 - _angle), Color.Transparent);
                e.Graphics.DrawImage(bmp, Width / 2.0f - bmp.Width / 2.0f, Height / 2.0f - bmp.Height / 2.0f);
                bmp.Dispose();
            }

            rawImage.Dispose();
        }

        #region 组件设计器生成的代码

        /// <summary>
        ///     设计器支持所需的方法 - 不要
        ///     使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
        }

        #endregion 组件设计器生成的代码
    }
}