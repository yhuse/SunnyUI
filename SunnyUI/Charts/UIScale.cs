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
 * 文件名称: UIScale.cs
 * 文件说明: 坐标轴刻度计算类
 * 当前版本: V2.2
 * 创建日期: 2020-10-01
 *
 * 2020-10-01: V2.2.8 完成曲线图表坐标轴刻度计算类
******************************************************************************/

using System;
using System.Globalization;
using static System.Double;

namespace Sunny.UI
{
    public abstract class UIScale
    {
        protected double _rangeMin;
        protected double _rangeMax;

        protected bool _minAuto = true;
        protected bool _maxAuto = true;
        protected double _minGrace = 0.1;
        protected double _maxGrace = 0.1;
        protected double _min, _max;
        protected bool _formatAuto = true;
        protected string _format;

        protected bool _magAuto = true;
        protected int _mag;

        protected static double TargetSteps = 7.0;

        public void SetRange(double rangeMin, double rangeMax)
        {
            _rangeMax = rangeMax;
            _rangeMin = rangeMin;
        }

        public abstract double[] CalcLabels();

        public float CalcXPixel(double value, int origin, int width)
        {
            return origin + (float)((value - _min) * 1.0f * width / (_max - _min));
        }

        public float CalcYPixel(double value, int origin, int height)
        {
            return origin - (float)((value - _min) * 1.0f * height / (_max - _min));
        }

        public float[] CalcXPixels(double[] labels, int origin, int width)
        {
            if (labels == null) return null;
            float[] result = new float[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                result[i] = CalcXPixel(labels[i], origin, width);
            }

            return result;
        }

        public float[] CalcYPixels(double[] labels, int origin, int height)
        {
            if (labels == null) return null;
            float[] result = new float[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                result[i] = CalcYPixel(labels[i], origin, height);
            }

            return result;
        }

        public int Mag
        {
            get { return _mag; }
            set { _mag = value; _magAuto = false; }
        }

        public bool MagAuto
        {
            get { return _magAuto; }
            set { _magAuto = value; }
        }

        public double MinGrace
        {
            get { return _minGrace; }
            set { _minGrace = value; }
        }

        public double MaxGrace
        {
            get { return _maxGrace; }
            set { _maxGrace = value; }
        }

        public bool FormatAuto
        {
            get { return _formatAuto; }
            set { _formatAuto = value; }
        }

        public string Format
        {
            get { return _format; }
            set { _format = value; _formatAuto = false; }
        }

        public virtual double Min
        {
            get => _min;
            set { _min = value; _minAuto = false; }
        }

        public virtual double Max
        {
            get => _max;
            set { _max = value; _maxAuto = false; }
        }

        public bool MinAuto
        {
            get => _minAuto;
            set => _minAuto = value;
        }

        public bool MaxAuto
        {
            get => _maxAuto;
            set => _maxAuto = value;
        }

        public double Step { get; protected set; }

        public virtual void AxisChange()
        {
            double minVal = _rangeMin;
            double maxVal = _rangeMax;

            if (IsInfinity(minVal) || IsNaN(minVal) || minVal.Equals(MaxValue))
                minVal = 0.0;
            if (IsInfinity(maxVal) || IsNaN(maxVal) || maxVal.Equals(MaxValue))
                maxVal = 0.0;

            double range = maxVal - minVal;

            if (_minAuto)
            {
                _min = minVal;
                if (_min < 0 || minVal - _minGrace * range >= 0.0)
                    _min = minVal - _minGrace * range;
            }
            if (_maxAuto)
            {
                _max = maxVal;
                if (_max > 0 || maxVal + _maxGrace * range <= 0.0)
                    _max = maxVal + _maxGrace * range;
            }

            if (_max.Equals(_min) && _maxAuto && _minAuto)
            {
                if (Math.Abs(_max) > 1e-100)
                {
                    _max *= (_min < 0 ? 0.95 : 1.05);
                    _min *= (_min < 0 ? 1.05 : 0.95);
                }
                else
                {
                    _max = 1.0;
                    _min = -1.0;
                }
            }

            if (_max <= _min)
            {
                if (_maxAuto)
                    _max = _min + 1.0;
                else if (_minAuto)
                    _min = _max - 1.0;
            }
        }

        public double CalcStepSize(double range, double targetSteps)
        {
            double tempStep = range / targetSteps;
            double mag = Math.Floor(Math.Log10(tempStep));
            double magPow = Math.Pow(10.0, mag);
            double magMsd = ((int)(tempStep / magPow + .5));
            if (magMsd > 5.0)
                magMsd = 10.0;
            else if (magMsd > 2.0)
                magMsd = 5.0;
            else if (magMsd > 1.0)
                magMsd = 2.0;

            return magMsd * magPow;
        }
    }

    public class UILinearScale : UIScale
    {
        private static readonly double ZeroLever = 0.25;

        public override void AxisChange()
        {
            base.AxisChange();

            if (_max - _min < 1.0e-30)
            {
                if (_maxAuto) _max += 0.2 * (_max == 0 ? 1.0 : Math.Abs(_max));
                if (_minAuto) _min -= 0.2 * (_min == 0 ? 1.0 : Math.Abs(_min));
            }

            if (_minAuto && _min > 0 && _min / (_max - _min) < ZeroLever) _min = 0;
            if (_maxAuto && _max < 0 && Math.Abs(_max / (_max - _min)) < ZeroLever) _max = 0;

            Step = CalcStepSize(_max - _min, TargetSteps);

            if (_minAuto) _min -= MyMod(_min, Step);
            if (_maxAuto) _max = MyMod(_max, Step) == 0.0 ? _max : _max + Step - MyMod(_max, Step);
            SetScaleMag();
        }

        private void SetScaleMag()
        {
            if (_magAuto)
            {
                double minMag = Math.Floor(Math.Log10(Math.Abs(_min)));
                double maxMag = Math.Floor(Math.Log10(Math.Abs(_max)));
                double mag = Math.Max(maxMag, minMag);
                if (Math.Abs(mag) <= 3) mag = 0;
                _mag = (int)(Math.Floor(mag / 3.0) * 3.0);
            }

            if (_formatAuto)
            {
                int numDec = 0 - (int)(Math.Floor(Math.Log10(Step)) - _mag);
                if (numDec < 0) numDec = 0;
                _format = "f" + numDec.ToString(CultureInfo.InvariantCulture);
            }
        }

        private double MyMod(double x, double y)
        {
            if (y == 0) return 0;
            double temp = x / y;
            return y * (temp - Math.Floor(temp));
        }

        public override double[] CalcLabels()
        {
            int nTics = CalcNumTics();
            double startVal = CalcBaseTic();
            double[] result = new double[nTics];
            for (int i = 0; i < nTics; i++)
            {
                result[i] = CalcMajorTicValue(startVal, i);
            }

            return result;
        }

        private double CalcMajorTicValue(double baseVal, double tic)
        {
            return baseVal + Step * tic;
        }

        private double CalcBaseTic()
        {
            return Math.Ceiling(_min / Step - 0.00000001) * Step;
        }

        private int CalcNumTics()
        {
            int nTics = (int)((_max - _min) / Step + 0.01) + 1;

            if (nTics < 1) nTics = 1;
            else if (nTics > 1000) nTics = 1000;

            return nTics;
        }
    }

    public class UIDateScale : UIScale
    {
        public override void AxisChange()
        {
            base.AxisChange();

            if (_max - _min < 1.0e-20)
            {
                if (_maxAuto) _max += 0.2 * (_max == 0 ? 1.0 : Math.Abs(_max));
                if (_minAuto) _min -= 0.2 * (_min == 0 ? 1.0 : Math.Abs(_min));
            }

            DateTimeInt64 max = new DateTimeInt64(_max);
            DateTimeInt64 min = new DateTimeInt64(_min);
            Step = CalcDateStepSize(max, min, TargetSteps);

            if (_minAuto) _min = CalcEvenStepDate(_min, -1);
            if (_maxAuto) _max = CalcEvenStepDate(_max, 1);
            _mag = 0;
        }

        public override double[] CalcLabels()
        {
            int nTics = CalcNumTics();
            double startVal = CalcBaseTic();
            double[] result = new double[nTics];
            for (int i = 0; i < nTics; i++)
            {
                result[i] = CalcMajorTicValue(startVal, i);
            }

            return result;
        }

        private double CalcEvenStepDate(double date, int direction)
        {
            DateTimeInt64 dtLong = new DateTimeInt64(date);
            int year = dtLong.Year;
            int month = dtLong.Month;
            int day = dtLong.Day;
            int hour = dtLong.Hour;
            int minute = dtLong.Minute;
            int second = dtLong.Second;
            int millisecond = dtLong.Millisecond;

            if (direction < 0) direction = 0;
            switch (_scaleLevel)
            {
                default:
                    if (direction == 1 && month == 1 && day == 1 && hour == 0 && minute == 0 && second == 0)
                        return date;
                    else
                        return new DateTimeInt64(new DateTime(year + direction, 1, 1, 0, 0, 0)).DoubleValue;

                case UIDateScaleLevel.Month:
                    if (direction == 1 && day == 1 && hour == 0 && minute == 0 && second == 0)
                        return date;
                    else
                        return new DateTimeInt64(new DateTime(year, month + direction, 1, 0, 0, 0)).DoubleValue;

                case UIDateScaleLevel.Day:
                    if (direction == 1 && hour == 0 && minute == 0 && second == 0)
                        return date;
                    else
                        return new DateTimeInt64(new DateTime(year, month, day + direction, 0, 0, 0)).DoubleValue;

                case UIDateScaleLevel.Hour:
                    if (direction == 1 && minute == 0 && second == 0)
                        return date;
                    else
                        return new DateTimeInt64(new DateTime(year, month, day, hour + direction, 0, 0)).DoubleValue;

                case UIDateScaleLevel.Minute:
                    if (direction == 1 && second == 0)
                        return date;
                    else
                        return new DateTimeInt64(new DateTime(year, month, day, hour, minute + direction, 0)).DoubleValue;

                case UIDateScaleLevel.Second:
                    return new DateTimeInt64(new DateTime(year, month, day, hour, minute, second + direction)).DoubleValue;

                case UIDateScaleLevel.Millisecond:
                    return new DateTimeInt64(new DateTime(year, month, day, hour, minute, second, millisecond + direction)).DoubleValue;
            }
        }

        private UIDateScaleLevel _scaleLevel;

        private double CalcDateStepSize(DateTimeInt64 max, DateTimeInt64 min, double targetSteps)
        {
            double range = _max - _min;
            double tempStep = range / targetSteps;
            TimeSpan span = max.DateTime - min.DateTime;

            if (span.TotalDays > 1825) // 5 years
            {
                _scaleLevel = UIDateScaleLevel.Year;
                if (_formatAuto) _format = "yyyy";
                tempStep = Math.Ceiling(tempStep / 365.0);
            }
            else if (span.TotalDays > 730) // 2 years
            {
                _scaleLevel = UIDateScaleLevel.Year;
                if (_formatAuto) _format = "yyyy-MM";
                tempStep = Math.Ceiling(tempStep / 365.0);
            }
            else if (span.TotalDays > 300) // 10 months
            {
                _scaleLevel = UIDateScaleLevel.Month;
                if (_formatAuto) _format = "yyyy-MM";
                tempStep = Math.Ceiling(tempStep / 30.0);
            }
            else if (span.TotalDays > 10)  // 10 days
            {
                _scaleLevel = UIDateScaleLevel.Day;
                if (_formatAuto) _format = "yyyy-MM-dd";
                tempStep = Math.Ceiling(tempStep);
            }
            else if (span.TotalDays > 3)  // 3 days
            {
                _scaleLevel = UIDateScaleLevel.Day;
                if (_formatAuto) _format = "yyyy-MM-dd HH:mm";
                tempStep = Math.Ceiling(tempStep);
            }
            else if (span.TotalHours > 10) // 10 hours
            {
                _scaleLevel = UIDateScaleLevel.Hour;
                if (_formatAuto) _format = "HH:mm";
                tempStep = Math.Ceiling(tempStep * 24.0);

                if (tempStep > 12.0) tempStep = 24.0;
                else if (tempStep > 6.0) tempStep = 12.0;
                else if (tempStep > 2.0) tempStep = 6.0;
                else if (tempStep > 1.0) tempStep = 2.0;
                else tempStep = 1.0;
            }
            else if (span.TotalHours > 3) // 3 hours
            {
                _scaleLevel = UIDateScaleLevel.Hour;
                if (_formatAuto) _format = "HH:mm";
                tempStep = Math.Ceiling(tempStep * 24.0);
            }
            else if (span.TotalMinutes > 10) // 10 Minutes
            {
                _scaleLevel = UIDateScaleLevel.Minute;
                if (_formatAuto) _format = "HH:mm";
                tempStep = Math.Ceiling(tempStep * 1440.0);
                // make sure the minute step size is 1, 5, 15, or 30 minutes
                if (tempStep > 15.0) tempStep = 30.0;
                else if (tempStep > 5.0) tempStep = 15.0;
                else if (tempStep > 1.0) tempStep = 5.0;
                else tempStep = 1.0;
            }
            else if (span.TotalMinutes > 3)  // 3 Minutes
            {
                _scaleLevel = UIDateScaleLevel.Minute;
                if (_formatAuto) _format = "mm:ss";
                tempStep = Math.Ceiling(tempStep * 1440.0);
            }
            else if (span.TotalSeconds > 3)  // 3 Seconds
            {
                _scaleLevel = UIDateScaleLevel.Second;
                if (_formatAuto) _format = "mm:ss";

                tempStep = Math.Ceiling(tempStep * 86400.0);
                // make sure the second step size is 1, 5, 15, or 30 seconds
                if (tempStep > 15.0) tempStep = 30.0;
                else if (tempStep > 5.0) tempStep = 15.0;
                else if (tempStep > 1.0) tempStep = 5.0;
                else tempStep = 1.0;
            }
            else
            {
                _scaleLevel = UIDateScaleLevel.Millisecond;
                if (_formatAuto) _format = "ss.fff";
                tempStep = CalcStepSize(span.TotalMilliseconds, targetSteps);
            }

            return tempStep;
        }

        private double CalcMajorTicValue(double baseVal, double tic)
        {
            DateTimeInt64 dtLong = new DateTimeInt64(baseVal);
            switch (_scaleLevel)
            {
                default: dtLong.AddYears((int)(tic * Step)); break;
                case UIDateScaleLevel.Month: dtLong.AddMonths((int)(tic * Step)); break;
                case UIDateScaleLevel.Day: dtLong.AddDays(tic * Step); break;
                case UIDateScaleLevel.Hour: dtLong.AddHours(tic * Step); break;
                case UIDateScaleLevel.Minute: dtLong.AddMinutes(tic * Step); break;
                case UIDateScaleLevel.Second: dtLong.AddSeconds(tic * Step); break;
                case UIDateScaleLevel.Millisecond: dtLong.AddMilliseconds(tic * Step); break;
            }

            return dtLong.DoubleValue;
        }

        private int CalcNumTics()
        {
            int nTics;
            DateTimeInt64 max = new DateTimeInt64(_max);
            DateTimeInt64 min = new DateTimeInt64(_min);

            switch (_scaleLevel)
            {
                default: nTics = (int)((max.Year - min.Year) / Step + 1.001); break;
                case UIDateScaleLevel.Month: nTics = (int)((max.Month - min.Month + 12.0 * (max.Year - min.Year)) / Step + 1.001); break;
                case UIDateScaleLevel.Day: nTics = (int)((_max - _min) / Step + 1.001); break;
                case UIDateScaleLevel.Hour: nTics = (int)((_max - _min) / (Step / HoursPerDay) + 1.001); break;
                case UIDateScaleLevel.Minute: nTics = (int)((_max - _min) / (Step / MinutesPerDay) + 1.001); break;
                case UIDateScaleLevel.Second: nTics = (int)((_max - _min) / (Step / SecondsPerDay) + 1.001); break;
                case UIDateScaleLevel.Millisecond: nTics = (int)((_max - _min) / (Step / MillisecondsPerDay) + 1.001); break;
            }

            if (nTics < 1)
                nTics = 1;
            else if (nTics > 1000)
                nTics = 1000;

            return nTics;
        }

        private double CalcBaseTic()
        {
            DateTimeInt64 dtLong = new DateTimeInt64(_min);
            int year = dtLong.Year;
            int month = dtLong.Month;
            int day = dtLong.Day;
            int hour = dtLong.Hour;
            int minute = dtLong.Minute;
            int second = dtLong.Second;
            int millisecond = dtLong.Millisecond;

            switch (_scaleLevel)
            {
                default: month = 1; day = 1; hour = 0; minute = 0; second = 0; millisecond = 0; break;
                case UIDateScaleLevel.Month: day = 1; hour = 0; minute = 0; second = 0; millisecond = 0; break;
                case UIDateScaleLevel.Day: hour = 0; minute = 0; second = 0; millisecond = 0; break;
                case UIDateScaleLevel.Hour: minute = 0; second = 0; millisecond = 0; break;
                case UIDateScaleLevel.Minute: second = 0; millisecond = 0; break;
                case UIDateScaleLevel.Second: millisecond = 0; break;
                case UIDateScaleLevel.Millisecond: break;
            }

            DateTimeInt64 xlDateNew = new DateTimeInt64(year, month, day, hour, minute, second, millisecond);
            double xlDate = xlDateNew.DoubleValue;
            if (xlDate < _min)
            {
                switch (_scaleLevel)
                {
                    default: year++; break;
                    case UIDateScaleLevel.Month: month++; break;
                    case UIDateScaleLevel.Day: day++; break;
                    case UIDateScaleLevel.Hour: hour++; break;
                    case UIDateScaleLevel.Minute: minute++; break;
                    case UIDateScaleLevel.Second: second++; break;
                    case UIDateScaleLevel.Millisecond: millisecond++; break;
                }

                xlDateNew = new DateTimeInt64(year, month, day, hour, minute, second, millisecond);
            }

            return xlDateNew.DoubleValue;
        }

        private const double HoursPerDay = 24.0;
        private const double MinutesPerDay = 1440.0;
        private const double SecondsPerDay = 86400.0;
        private const double MillisecondsPerDay = 86400000.0;
    }

    public enum UIDateScaleLevel
    {
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        Millisecond
    }
}