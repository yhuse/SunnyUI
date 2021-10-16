using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

/*
https://github.com/nathanpjones/randomColorSharped
Port of David Merfield's javascript random color generator. Ported by Nathan Jones.
License: MIT License
*/

namespace Sunny.UI
{
    /// <summary>
    /// Generates random numbers.
    /// </summary>
    public static class RandomColor
    {
        private class DefinedColor
        {
            public Range HueRange
            {
                get; set;
            }
            public Point[] LowerBounds
            {
                get; set;
            }
            public Range SaturationRange
            {
                get; set;
            }
        }

        private static readonly Dictionary<ColorScheme, DefinedColor> ColorDictionary = new Dictionary<ColorScheme, DefinedColor>();
        private static readonly object LockObj = new object();
        private static Random _rng = new Random();

        static RandomColor()
        {
            // Populate the color dictionary
            LoadColorBounds();
        }

        /// <summary>
        /// Gets a new random color.
        /// </summary>
        /// <param name="scheme">Which color schemed to use when generating the color.</param>
        /// <param name="luminosity">The desired luminosity of the color.</param>
        public static Color GetColor(ColorScheme scheme, Luminosity luminosity)
        {
            int H, S, B;

            // First we pick a hue (H)
            H = PickHue(scheme);

            // Then use H to determine saturation (S)
            S = PickSaturation(H, luminosity, scheme);

            // Then use S and H to determine brightness (B).
            B = PickBrightness(H, S, luminosity);

            // Then we return the HSB color in the desired format
            return HsvToColor(H, S, B);
        }

        /// <summary>
        /// Generates multiple random colors.
        /// </summary>
        /// <param name="scheme">Which color schemed to use when generating the color.</param>
        /// <param name="luminosity">The desired luminosity of the color.</param>
        /// <param name="count">How many colors to generate</param>
        public static Color[] GetColors(ColorScheme scheme, Luminosity luminosity, int count)
        {
            var ret = new Color[count];
            for (var i = 0; i < count; i++)
            {
                ret[i] = GetColor(scheme, luminosity);
            }
            return ret;
        }

        /// <summary>
        /// Generate one color for each of the provided generator options.
        /// </summary>
        /// <param name="options">List of options to use when creating colors.</param>
        public static Color[] GetColors(params Options[] options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            return options.Select(o => GetColor(o.ColorScheme, o.Luminosity)).ToArray();
        }

        /// <summary>
        /// Reseeds the random number generated.
        /// </summary>
        /// <param name="seed">The number used to reseed the random number generator.</param>
        public static void Seed(int seed)
        {
            lock (LockObj)
            {
                _rng = new Random(seed);
            }
        }
        /// <summary>
        /// Reseeds the random number generated.
        /// </summary>
        public static void Seed()
        {
            lock (LockObj)
            {
                _rng = new Random();
            }
        }

        private static int PickHue(ColorScheme scheme)
        {
            var hueRange = GetHueRange(scheme);
            var hue = RandomWithin(hueRange);

            // Instead of storing red as two separate ranges,
            // we group them, using negative numbers
            if (hue < 0) hue = 360 + hue;

            return hue;
        }

        private static int PickSaturation(int hue, Luminosity luminosity, ColorScheme scheme)
        {
            if (scheme == ColorScheme.Monochrome)
            {
                return 0;
            }

            if (luminosity == Luminosity.Random)
            {
                return RandomWithin(0, 100);
            }

            var saturationRange = GetColorInfo(hue).SaturationRange;

            var sMin = saturationRange.Lower;
            var sMax = saturationRange.Upper;

            switch (luminosity)
            {
                case Luminosity.Bright:
                    sMin = 55;
                    break;

                case Luminosity.Dark:
                    sMin = sMax - 10;
                    break;

                case Luminosity.Light:
                    sMax = 55;
                    break;
            }

            return RandomWithin(sMin, sMax);
        }

        private static int PickBrightness(int H, int S, Luminosity luminosity)
        {
            var bMin = GetMinimumBrightness(H, S);
            var bMax = 100;

            switch (luminosity)
            {
                case Luminosity.Dark:
                    bMax = bMin + 20;
                    break;

                case Luminosity.Light:
                    bMin = (bMax + bMin) / 2;
                    break;

                case Luminosity.Random:
                    bMin = 0;
                    bMax = 100;
                    break;
            }

            return RandomWithin(bMin, bMax);
        }

        private static int GetMinimumBrightness(int H, int S)
        {
            var lowerBounds = GetColorInfo(H).LowerBounds;

            for (var i = 0; i < lowerBounds.Length - 1; i++)
            {
                var s1 = lowerBounds[i].X;
                var v1 = lowerBounds[i].Y;

                var s2 = lowerBounds[i + 1].X;
                var v2 = lowerBounds[i + 1].Y;

                if (S >= s1 && S <= s2)
                {
                    var m = (v2 - v1) / (s2 - s1);
                    var b = v1 - m * s1;

                    return m * S + b;
                }
            }

            return 0;
        }

        private static Range GetHueRange(ColorScheme colorInput)
        {
            DefinedColor color;
            if (ColorDictionary.TryGetValue(colorInput, out color))
            {
                if (color.HueRange != null)
                {
                    return color.HueRange;
                }
            }

            return new Range(0, 360);
        }

        private static DefinedColor GetColorInfo(int hue)
        {
            // Maps red colors to make picking hue easier
            if (hue >= 334 && hue <= 360)
            {
                hue -= 360;
            }

            var ret = ColorDictionary.FirstOrDefault(c => c.Value.HueRange != null &&
                                                          hue >= c.Value.HueRange[0] &&
                                                          hue <= c.Value.HueRange[1]);

            Debug.Assert(ret.Value != null);

            return ret.Value;
        }

        private static int RandomWithin(Range range)
        {
            return RandomWithin(range.Lower, range.Upper);
        }
        private static int RandomWithin(int lower, int upper)
        {
            lock (LockObj)
            {
                return _rng.Next(lower, upper + 1);
            }
        }

        private static void DefineColor(ColorScheme scheme, int[] hueRange, int[,] lowerBounds)
        {
            int[][] jagged = new int[lowerBounds.GetLength(0)][];
            for (int i = 0; i < lowerBounds.GetLength(0); i++)
            {
                jagged[i] = new int[lowerBounds.GetLength(1)];
                for (int j = 0; j < lowerBounds.GetLength(1); j++)
                {
                    jagged[i][j] = lowerBounds[i, j];
                }
            }

            var sMin = jagged[0][0];
            var sMax = jagged[jagged.Length - 1][0];

            ColorDictionary[scheme] = new DefinedColor()
            {
                HueRange = Range.ToRange(hueRange),
                LowerBounds = jagged.Select(j => new Point(j[0], j[1])).ToArray(),
                SaturationRange = new Range(sMin, sMax)
            };
        }

        private static void LoadColorBounds()
        {
            DefineColor(
                ColorScheme.Monochrome,
                null,
                new[,] { { 0, 0 }, { 100, 0 } }
                );

            DefineColor(
                ColorScheme.Red,
                new[] { -26, 18 },
                new[,] { { 20, 100 }, { 30, 92 }, { 40, 89 }, { 50, 85 }, { 60, 78 }, { 70, 70 }, { 80, 60 }, { 90, 55 }, { 100, 50 } }
                );

            DefineColor(
                ColorScheme.Orange,
                new[] { 19, 46 },
                new[,] { { 20, 100 }, { 30, 93 }, { 40, 88 }, { 50, 86 }, { 60, 85 }, { 70, 70 }, { 100, 70 } }
                );

            DefineColor(
                ColorScheme.Yellow,
                new[] { 47, 62 },
                new[,] { { 25, 100 }, { 40, 94 }, { 50, 89 }, { 60, 86 }, { 70, 84 }, { 80, 82 }, { 90, 80 }, { 100, 75 } }
                );

            DefineColor(
                ColorScheme.Green,
                new[] { 63, 178 },
                new[,] { { 30, 100 }, { 40, 90 }, { 50, 85 }, { 60, 81 }, { 70, 74 }, { 80, 64 }, { 90, 50 }, { 100, 40 } }
                );

            DefineColor(
                ColorScheme.Blue,
                new[] { 179, 257 },
                new[,] { { 20, 100 }, { 30, 86 }, { 40, 80 }, { 50, 74 }, { 60, 60 }, { 70, 52 }, { 80, 44 }, { 90, 39 }, { 100, 35 } }
                );

            DefineColor(
                ColorScheme.Purple,
                new[] { 258, 282 },
                new[,] { { 20, 100 }, { 30, 87 }, { 40, 79 }, { 50, 70 }, { 60, 65 }, { 70, 59 }, { 80, 52 }, { 90, 45 }, { 100, 42 } }
                );

            DefineColor(
                ColorScheme.Pink,
                new[] { 283, 334 },
                new[,] { { 20, 100 }, { 30, 90 }, { 40, 86 }, { 60, 84 }, { 80, 80 }, { 90, 75 }, { 100, 73 } }
                );
        }

        /// <summary>
        /// Converts hue, saturation, and lightness to a color.
        /// </summary>
        public static Color HsvToColor(int hue, int saturation, double value)
        {
            // this doesn't work for the values of 0 and 360
            // here's the hacky fix
            var h = Convert.ToDouble(hue);
            if (h == 0)
            {
                h = 1;
            }
            if (h.EqualsDouble(360))
            {
                h = 359;
            }

            // Rebase the h,s,v values
            h = h / 360.0;
            var s = saturation / 100.0;
            var v = value / 100.0;

            var hInt = (int)Math.Floor(h * 6.0);
            var f = h * 6 - hInt;
            var p = v * (1 - s);
            var q = v * (1 - f * s);
            var t = v * (1 - (1 - f) * s);
            var r = 256.0;
            var g = 256.0;
            var b = 256.0;

            switch (hInt)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }
            var c = Color.FromArgb(255,
                                   (byte)Math.Floor(r * 255.0),
                                   (byte)Math.Floor(g * 255.0),
                                   (byte)Math.Floor(b * 255.0));

            return c;
        }
    }

    /// <summary>
    /// Predefined color schemes.
    /// </summary>
    public enum ColorScheme
    {
        /// <summary>
        /// Select randomly from among the other color schemes.
        /// </summary>
        Random,
        /// <summary>
        /// Generates only grayscale colors.
        /// </summary>
        Monochrome,
        /// <summary>
        /// Generates only red colors.
        /// </summary>
        Red,
        /// <summary>
        /// Generates only orange colors.
        /// </summary>
        Orange,
        /// <summary>
        /// Generates only yellow colors.
        /// </summary>
        Yellow,
        /// <summary>
        /// Generates only green colors.
        /// </summary>
        Green,
        /// <summary>
        /// Generates only blue colors.
        /// </summary>
        Blue,
        /// <summary>
        /// Generates only purple colors.
        /// </summary>
        Purple,
        /// <summary>
        /// Generates only pink colors.
        /// </summary>
        Pink
    }

    /// <summary>
    /// Predefined luminosity ranges.
    /// </summary>
    public enum Luminosity
    {
        /// <summary>
        /// Select randomly from among the other luminosities.
        /// </summary>
        Random,
        /// <summary>
        /// Generate dark colors.
        /// </summary>
        Dark,
        /// <summary>
        /// Generate light, pastel colors.
        /// </summary>
        Light,
        /// <summary>
        /// Generate vibrant colors.
        /// </summary>
        Bright,
    }

    /// <summary>
    /// Represents a range using an upper and lower value.
    /// </summary>
    [DebuggerDisplay(@"\{{Lower},{Upper}\}")]
    internal class Range
    {
        public int Lower
        {
            get; set;
        }
        public int Upper
        {
            get; set;
        }

        public Range()
        {
        }
        public Range(int lower, int upper)
        {
            Lower = lower;
            Upper = upper;
        }

        /// <summary>
        /// Gets the lower range for an index of 0 and the upper for an index of 1.
        /// </summary>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Lower;
                    case 1: return Upper;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Lower = value; break;
                    case 1: Upper = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        internal static Range ToRange(int[] range)
        {
            if (range == null) return null;
            Debug.Assert(range.Length == 2);
            return new Range(range[0], range[1]);
        }
    }

    /// <summary>
    /// Options for generating a random color.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the color scheme to use when generating the color.
        /// </summary>
        public ColorScheme ColorScheme
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the luminosity range to use when generating the color.
        /// </summary>
        public Luminosity Luminosity
        {
            get; set;
        }

        /// <summary>
        /// Creates a new instance using default values.
        /// </summary>
        public Options()
        {
        }

        /// <summary>
        /// Creates a new instance with the given color scheme and luminosity range.
        /// </summary>
        /// <param name="scheme">The color scheme to use when generating the color.</param>
        /// <param name="luminosity">The luminosity range to use when generating the color.</param>
        public Options(ColorScheme scheme, Luminosity luminosity)
        {
            ColorScheme = scheme;
            Luminosity = luminosity;
        }
    }

}