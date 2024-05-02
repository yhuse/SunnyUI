/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2024 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UFastBitmap.cs
 * 文件说明: 快速图片处理类
 * 文件作者: Luiz Fernando
 * 开源协议: MIT
 * 引用地址: https://github.com/LuizZak/FastBitmap
******************************************************************************/

/*
    FastBitmapLib

    The MIT License (MIT)

    Copyright (c) 2014 Luiz Fernando

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.

    

    ----------------------------------------------
    Editing pixels of a bitmap is just as easy as:
    ----------------------------------------------
    Bitmap bitmap = new Bitmap(64, 64);    
    using(var fastBitmap = bitmap.FastLock())
    {
        // Do your changes here...
        fastBitmap.Clear(Color.White);
        fastBitmap.SetPixel(1, 1, Color.Red);
    }

    ----------------------------------------------
    Or alternatively, albeit longer:
    ----------------------------------------------
    Bitmap bitmap = new Bitmap(64, 64);
    FastBitmap fastBitmap = new FastBitmap(bitmap);
    
    // Locking bitmap before doing operations
    fastBitmap.Lock();
    
    // Do your changes here...
    fastBitmap.Clear(Color.White);
    fastBitmap.SetPixel(1, 1, Color.Red);
    
    // Don't forget to unlock!
    fastBitmap.Unlock();
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    /// <summary>
    /// Encapsulates a Bitmap for fast bitmap pixel operations using 32bpp images
    /// </summary>
    public unsafe class FastBitmap : IDisposable
    {
        /// <summary>
        /// Specifies the number of bytes available per pixel of the bitmap object being manipulated
        /// </summary>
        public const int BytesPerPixel = 4;

        /// <summary>
        /// The Bitmap object encapsulated on this FastBitmap
        /// </summary>
        private readonly Bitmap _bitmap;

        /// <summary>
        /// The BitmapData resulted from the lock operation
        /// </summary>
        private BitmapData _bitmapData;

        /// <summary>
        /// The first pixel of the bitmap
        /// </summary>
        private int* _scan0;

        /// <summary>
        /// Gets the width of this FastBitmap object
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of this FastBitmap object
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the pointer to the first pixel of the bitmap
        /// </summary>
        public IntPtr Scan0 => _bitmapData.Scan0;

        /// <summary>
        /// Gets the stride width (in int32-sized values) of the bitmap
        /// </summary>
        public int Stride { get; private set; }

        /// <summary>
        /// Gets the stride width (in bytes) of the bitmap
        /// </summary>
        public int StrideInBytes { get; private set; }

        /// <summary>
        /// Gets a boolean value that states whether this FastBitmap is currently locked in memory
        /// </summary>
        public bool Locked { get; private set; }

        /// <summary>
        /// Gets an array of 32-bit color pixel values that represent this FastBitmap
        /// </summary>
        /// <exception cref="Exception">The locking operation required to extract the values off from the underlying bitmap failed</exception>
        /// <exception cref="InvalidOperationException">The bitmap is already locked outside this fast bitmap</exception>
        public int[] DataArray
        {
            get
            {
                bool unlockAfter = false;
                if (!Locked)
                {
                    Lock();
                    unlockAfter = true;
                }

                // Declare an array to hold the bytes of the bitmap
                int bytes = Math.Abs(_bitmapData.Stride) * _bitmap.Height;
                int[] argbValues = new int[bytes / BytesPerPixel];

                // Copy the RGB values into the array
                Marshal.Copy(_bitmapData.Scan0, argbValues, 0, bytes / BytesPerPixel);

                if (unlockAfter)
                {
                    Unlock();
                }

                return argbValues;
            }
        }

        /// <summary>
        /// Creates a new instance of the FastBitmap class with a specified Bitmap.
        /// The bitmap provided must have a 32bpp depth
        /// </summary>
        /// <param name="bitmap">The Bitmap object to encapsulate on this FastBitmap object</param>
        /// <exception cref="ArgumentException">The bitmap provided does not have a 32bpp pixel format</exception>
        public FastBitmap(Bitmap bitmap)
        {
            if (Image.GetPixelFormatSize(bitmap.PixelFormat) != 32)
            {
                throw new ArgumentException(@"The provided bitmap must have a 32bpp depth", nameof(bitmap));
            }

            _bitmap = bitmap;

            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            if (Locked)
            {
                Unlock();
            }
        }

        /// <summary>
        /// Locks the bitmap to start the bitmap operations. If the bitmap is already locked,
        /// an exception is thrown
        /// </summary>
        /// <returns>A fast bitmap locked struct that will unlock the underlying bitmap after disposal</returns>
        /// <exception cref="InvalidOperationException">The bitmap is already locked</exception>
        /// <exception cref="System.Exception">The locking operation in the underlying bitmap failed</exception>
        /// <exception cref="InvalidOperationException">The bitmap is already locked outside this fast bitmap</exception>
        public FastBitmapLocker Lock()
        {
            return Lock((FastBitmapLockFormat)_bitmap.PixelFormat);
        }

        /// <summary>
        /// Locks the bitmap to start the bitmap operations. If the bitmap is already locked,
        /// an exception is thrown.
        ///
        /// The provided pixel format should be a 32bpp format.
        /// </summary>
        /// <param name="pixelFormat">A pixel format to use when locking the underlying bitmap</param>
        /// <returns>A fast bitmap locked struct that will unlock the underlying bitmap after disposal</returns>
        /// <exception cref="InvalidOperationException">The bitmap is already locked</exception>
        /// <exception cref="Exception">The locking operation in the underlying bitmap failed</exception>
        /// <exception cref="InvalidOperationException">The bitmap is already locked outside this fast bitmap</exception>
        public FastBitmapLocker Lock(FastBitmapLockFormat pixelFormat)
        {
            if (Locked)
            {
                throw new InvalidOperationException("Unlock must be called before a Lock operation");
            }

            return Lock(ImageLockMode.ReadWrite, (PixelFormat)pixelFormat);
        }

        /// <summary>
        /// Locks the bitmap to start the bitmap operations
        /// </summary>
        /// <param name="lockMode">The lock mode to use on the bitmap</param>
        /// <param name="pixelFormat">A pixel format to use when locking the underlying bitmap</param>
        /// <returns>A fast bitmap locked struct that will unlock the underlying bitmap after disposal</returns>
        /// <exception cref="System.Exception">The locking operation in the underlying bitmap failed</exception>
        /// <exception cref="InvalidOperationException">The bitmap is already locked outside this fast bitmap</exception>
        /// <exception cref="ArgumentException"><see cref="!:pixelFormat"/> is not a 32bpp format</exception>
        private FastBitmapLocker Lock(ImageLockMode lockMode, PixelFormat pixelFormat)
        {
            var rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);

            return Lock(lockMode, rect, pixelFormat);
        }

        /// <summary>
        /// Locks the bitmap to start the bitmap operations
        /// </summary>
        /// <param name="lockMode">The lock mode to use on the bitmap</param>
        /// <param name="rect">The rectangle to lock</param>
        /// <param name="pixelFormat">A pixel format to use when locking the underlying bitmap</param>
        /// <returns>A fast bitmap locked struct that will unlock the underlying bitmap after disposal</returns>
        /// <exception cref="System.ArgumentException">The provided region is invalid</exception>
        /// <exception cref="System.Exception">The locking operation in the underlying bitmap failed</exception>
        /// <exception cref="InvalidOperationException">The bitmap region is already locked</exception>
        /// <exception cref="ArgumentException"><see cref="!:pixelFormat"/> is not a 32bpp format</exception>
        private FastBitmapLocker Lock(ImageLockMode lockMode, Rectangle rect, PixelFormat pixelFormat)
        {
            // Lock the bitmap's bits
            _bitmapData = _bitmap.LockBits(rect, lockMode, pixelFormat);

            _scan0 = (int*)_bitmapData.Scan0;
            Stride = _bitmapData.Stride / BytesPerPixel;
            StrideInBytes = _bitmapData.Stride;

            Locked = true;

            return new FastBitmapLocker(this);
        }

        /// <summary>
        /// Unlocks the bitmap and applies the changes made to it. If the bitmap was not locked
        /// beforehand, an exception is thrown
        /// </summary>
        /// <exception cref="InvalidOperationException">The bitmap is already unlocked</exception>
        /// <exception cref="System.Exception">The unlocking operation in the underlying bitmap failed</exception>
        public void Unlock()
        {
            if (!Locked)
            {
                throw new InvalidOperationException("Lock must be called before an Unlock operation");
            }

            _bitmap.UnlockBits(_bitmapData);

            Locked = false;
        }

        /// <summary>
        /// Sets the pixel color at the given coordinates. If the bitmap was not locked beforehands,
        /// an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to set</param>
        /// <param name="y">The Y coordinate of the pixel to set</param>
        /// <param name="color">The new color of the pixel to set</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public void SetPixel(int x, int y, Color color)
        {
            SetPixel(x, y, color.ToArgb());
        }

        /// <summary>
        /// Sets the pixel color at the given coordinates. If the bitmap was not locked beforehands,
        /// an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to set</param>
        /// <param name="y">The Y coordinate of the pixel to set</param>
        /// <param name="color">The new color of the pixel to set</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public void SetPixel(int x, int y, int color)
        {
            SetPixel(x, y, unchecked((uint)color));
        }

        /// <summary>
        /// Sets the pixel color at the given coordinates. If the bitmap was not locked beforehands,
        /// an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to set</param>
        /// <param name="y">The Y coordinate of the pixel to set</param>
        /// <param name="color">The new color of the pixel to set</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public void SetPixel(int x, int y, uint color)
        {
            if (!Locked)
            {
                throw new InvalidOperationException("The FastBitmap must be locked before any pixel operations are made");
            }

            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), @"The X component must be >= 0 and < width");
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), @"The Y component must be >= 0 and < height");
            }

            *(uint*)(_scan0 + x + y * Stride) = color;
        }

        /// <summary>
        /// Gets the pixel color at the given coordinates. If the bitmap was not locked beforehands,
        /// an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to get</param>
        /// <param name="y">The Y coordinate of the pixel to get</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public Color GetPixel(int x, int y)
        {
            return Color.FromArgb(GetPixelInt(x, y));
        }

        /// <summary>
        /// Gets the pixel color at the given coordinates as an integer value. If the bitmap
        /// was not locked beforehands, an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to get</param>
        /// <param name="y">The Y coordinate of the pixel to get</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public int GetPixelInt(int x, int y)
        {
            if (!Locked)
            {
                throw new InvalidOperationException("The FastBitmap must be locked before any pixel operations are made");
            }

            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), @"The X component must be >= 0 and < width");
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), @"The Y component must be >= 0 and < height");
            }

            return *(_scan0 + x + y * Stride);
        }

        /// <summary>
        /// Gets the pixel color at the given coordinates as an unsigned integer value.
        /// If the bitmap was not locked beforehands, an exception is thrown
        /// </summary>
        /// <param name="x">The X coordinate of the pixel to get</param>
        /// <param name="y">The Y coordinate of the pixel to get</param>
        /// <exception cref="InvalidOperationException">The fast bitmap is not locked</exception>
        /// <exception cref="ArgumentOutOfRangeException">The provided coordinates are out of bounds of the bitmap</exception>
        public uint GetPixelUInt(int x, int y)
        {
            if (!Locked)
            {
                throw new InvalidOperationException("The FastBitmap must be locked before any pixel operations are made");
            }

            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), @"The X component must be >= 0 and < width");
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), @"The Y component must be >= 0 and < height");
            }

            return *((uint*)_scan0 + x + y * Stride);
        }

        /// <summary>
        /// Copies the contents of the given array of colors into this FastBitmap.
        /// Throws an ArgumentException if the count of colors on the array mismatches the pixel count from this FastBitmap
        /// </summary>
        /// <param name="colors">The array of colors to copy</param>
        /// <param name="ignoreZeroes">Whether to ignore zeroes when copying the data</param>
        public void CopyFromArray(int[] colors, bool ignoreZeroes = false)
        {
            if (colors.Length != Width * Height)
            {
                throw new ArgumentException(@"The number of colors of the given array mismatch the pixel count of the bitmap", nameof(colors));
            }

            // Simply copy the argb values array
            int* s0t = _scan0;

            fixed (int* source = colors)
            {
                int* s0s = source;

                int count = Width * Height;

                if (!ignoreZeroes)
                {
                    // Unfold the loop
                    const int sizeBlock = 8;
                    int rem = count % sizeBlock;

                    count /= sizeBlock;

                    while (count-- > 0)
                    {
                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);

                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);
                        *(s0t++) = *(s0s++);
                    }

                    while (rem-- > 0)
                    {
                        *(s0t++) = *(s0s++);
                    }
                }
                else
                {
                    while (count-- > 0)
                    {
                        if (*(s0s) == 0) { s0t++; s0s++; continue; }
                        *(s0t++) = *(s0s++);
                    }
                }
            }
        }

        /// <summary>
        /// Clears the bitmap with the given color
        /// </summary>
        /// <param name="color">The color to clear the bitmap with</param>
        public void Clear(Color color)
        {
            Clear(color.ToArgb());
        }

        /// <summary>
        /// Clears the bitmap with the given color
        /// </summary>
        /// <param name="color">The color to clear the bitmap with</param>
        public void Clear(int color)
        {
            bool unlockAfter = false;
            if (!Locked)
            {
                Lock();
                unlockAfter = true;
            }

            // Clear all the pixels
            int count = Width * Height;
            int* curScan = _scan0;

            // Uniform color pixel values can be mem-set straight away
            int component = (color & 0xFF);
            if (component == ((color >> 8) & 0xFF) && component == ((color >> 16) & 0xFF) && component == ((color >> 24) & 0xFF))
            {
                memset(_scan0, component, (ulong)(Height * Stride * BytesPerPixel));
            }
            else
            {
                // Defines the ammount of assignments that the main while() loop is performing per loop.
                // The value specified here must match the number of assignment statements inside that loop
                const int assignsPerLoop = 8;

                int rem = count % assignsPerLoop;
                count /= assignsPerLoop;

                while (count-- > 0)
                {
                    *(curScan++) = color;
                    *(curScan++) = color;
                    *(curScan++) = color;
                    *(curScan++) = color;

                    *(curScan++) = color;
                    *(curScan++) = color;
                    *(curScan++) = color;
                    *(curScan++) = color;
                }
                while (rem-- > 0)
                {
                    *(curScan++) = color;
                }

                if (unlockAfter)
                {
                    Unlock();
                }
            }
        }

        /// <summary>
        /// Clears a square region of this image w/ a given color
        /// </summary>
        /// <param name="region"></param>
        /// <param name="color"></param>
        public void ClearRegion(Rectangle region, Color color)
        {
            ClearRegion(region, color.ToArgb());
        }

        /// <summary>
        /// Clears a square region of this image w/ a given color
        /// </summary>
        /// <param name="region"></param>
        /// <param name="color"></param>
        public void ClearRegion(Rectangle region, int color)
        {
            var thisReg = new Rectangle(0, 0, Width, Height);
            if (!region.IntersectsWith(thisReg))
                return;

            // If the region covers the entire image, use faster Clear().
            if (region == thisReg)
            {
                Clear(color);
                return;
            }

            int minX = region.X;
            int maxX = region.X + region.Width;

            int minY = region.Y;
            int maxY = region.Y + region.Height;

            // Bail out of optimization if there's too few rows to make this worth it
            if (maxY - minY < 16)
            {
                for (int y = minY; y < maxY; y++)
                {
                    for (int x = minX; x < maxX; x++)
                    {
                        *(_scan0 + x + y * Stride) = color;
                    }
                }
                return;
            }

            ulong strideWidth = (ulong)region.Width * BytesPerPixel;

            // Uniform color pixel values can be mem-set straight away
            int component = (color & 0xFF);
            if (component == ((color >> 8) & 0xFF) && component == ((color >> 16) & 0xFF) &&
                component == ((color >> 24) & 0xFF))
            {
                for (int y = minY; y < maxY; y++)
                {
                    memset(_scan0 + minX + y * Stride, component, strideWidth);
                }
            }
            else
            {
                // Prepare a horizontal slice of pixels that will be copied over each horizontal row down.
                var row = new int[region.Width];

                fixed (int* pRow = row)
                {
                    int count = region.Width;
                    int rem = count % 8;
                    count /= 8;
                    int* pSrc = pRow;
                    while (count-- > 0)
                    {
                        *pSrc++ = color;
                        *pSrc++ = color;
                        *pSrc++ = color;
                        *pSrc++ = color;

                        *pSrc++ = color;
                        *pSrc++ = color;
                        *pSrc++ = color;
                        *pSrc++ = color;
                    }
                    while (rem-- > 0)
                    {
                        *pSrc++ = color;
                    }

                    var sx = _scan0 + minX;
                    for (int y = minY; y < maxY; y++)
                    {
                        memcpy(sx + y * Stride, pRow, strideWidth);
                    }
                }
            }
        }

        /// <summary>
        /// Copies a region of the source bitmap into this fast bitmap
        /// </summary>
        /// <param name="source">The source image to copy</param>
        /// <param name="srcRect">The region on the source bitmap that will be copied over</param>
        /// <param name="destRect">The region on this fast bitmap that will be changed</param>
        /// <exception cref="ArgumentException">The provided source bitmap is the same bitmap locked in this FastBitmap</exception>
        public void CopyRegion(Bitmap source, Rectangle srcRect, Rectangle destRect)
        {
            // Throw exception when trying to copy same bitmap over
            if (source == _bitmap)
            {
                throw new ArgumentException(@"Copying regions across the same bitmap is not supported", nameof(source));
            }

            var srcBitmapRect = new Rectangle(0, 0, source.Width, source.Height);
            var destBitmapRect = new Rectangle(0, 0, Width, Height);

            // Check if the rectangle configuration doesn't generate invalid states or does not affect the target image
            if (srcRect.Width <= 0 || srcRect.Height <= 0 || destRect.Width <= 0 || destRect.Height <= 0 ||
                !srcBitmapRect.IntersectsWith(srcRect) || !destRect.IntersectsWith(destBitmapRect))
                return;

            // Find the areas of the first and second bitmaps that are going to be affected
            srcBitmapRect = Rectangle.Intersect(srcRect, srcBitmapRect);

            // Clip the source rectangle on top of the destination rectangle in a way that clips out the regions of the original bitmap
            // that will not be drawn on the destination bitmap for being out of bounds
            srcBitmapRect = Rectangle.Intersect(srcBitmapRect, new Rectangle(srcRect.X, srcRect.Y, destRect.Width, destRect.Height));

            destBitmapRect = Rectangle.Intersect(destRect, destBitmapRect);

            // Clip the source bitmap region yet again here
            srcBitmapRect = Rectangle.Intersect(srcBitmapRect, new Rectangle(-destRect.X + srcRect.X, -destRect.Y + srcRect.Y, Width, Height));

            // Calculate the rectangle containing the maximum possible area that is supposed to be affected by the copy region operation
            int copyWidth = Math.Min(srcBitmapRect.Width, destBitmapRect.Width);
            int copyHeight = Math.Min(srcBitmapRect.Height, destBitmapRect.Height);

            if (copyWidth == 0 || copyHeight == 0)
                return;

            int srcStartX = srcBitmapRect.Left;
            int srcStartY = srcBitmapRect.Top;

            int destStartX = destBitmapRect.Left;
            int destStartY = destBitmapRect.Top;

            using (var fastSource = source.FastLock())
            {
                ulong strideWidth = (ulong)copyWidth * BytesPerPixel;

                // Perform copies of whole pixel rows
                for (int y = 0; y < copyHeight; y++)
                {
                    int destX = destStartX;
                    int destY = destStartY + y;

                    int srcX = srcStartX;
                    int srcY = srcStartY + y;

                    long offsetSrc = (srcX + srcY * fastSource.Stride);
                    long offsetDest = (destX + destY * Stride);

                    memcpy(_scan0 + offsetDest, fastSource._scan0 + offsetSrc, strideWidth);
                }
            }
        }

        /// <summary>
        /// Performs a copy operation of the pixels from the Source bitmap to the Target bitmap.
        /// If the dimensions or pixel depths of both images don't match, the copy is not performed
        /// </summary>
        /// <param name="source">The bitmap to copy the pixels from</param>
        /// <param name="target">The bitmap to copy the pixels to</param>
        /// <returns>Whether the copy proceedure was successful</returns>
        /// <exception cref="ArgumentException">The provided source and target bitmaps are the same</exception>
        public static bool CopyPixels(Bitmap source, Bitmap target)
        {
            if (source == target)
            {
                throw new ArgumentException(@"Copying pixels across the same bitmap is not supported", nameof(source));
            }

            if (source.Width != target.Width || source.Height != target.Height || source.PixelFormat != target.PixelFormat)
                return false;

            using (FastBitmap fastSource = source.FastLock(),
                fastTarget = target.FastLock())
            {
                memcpy(fastTarget.Scan0, fastSource.Scan0, (ulong)(fastSource.Height * fastSource.Stride * BytesPerPixel));
            }

            return true;
        }

        /// <summary>
        /// Clears the given bitmap with the given color
        /// </summary>
        /// <param name="bitmap">The bitmap to clear</param>
        /// <param name="color">The color to clear the bitmap with</param>
        public static void ClearBitmap(Bitmap bitmap, Color color)
        {
            ClearBitmap(bitmap, color.ToArgb());
        }

        /// <summary>
        /// Clears the given bitmap with the given color
        /// </summary>
        /// <param name="bitmap">The bitmap to clear</param>
        /// <param name="color">The color to clear the bitmap with</param>
        public static void ClearBitmap(Bitmap bitmap, int color)
        {
            using (var fb = bitmap.FastLock())
            {
                fb.Clear(color);
            }
        }

        /// <summary>
        /// Copies a region of the source bitmap to a target bitmap
        /// </summary>
        /// <param name="source">The source image to copy</param>
        /// <param name="target">The target image to be altered</param>
        /// <param name="srcRect">The region on the source bitmap that will be copied over</param>
        /// <param name="destRect">The region on the target bitmap that will be changed</param>
        /// <exception cref="ArgumentException">The provided source and target bitmaps are the same bitmap</exception>
        public static void CopyRegion(Bitmap source, Bitmap target, Rectangle srcRect, Rectangle destRect)
        {
            var srcBitmapRect = new Rectangle(0, 0, source.Width, source.Height);
            var destBitmapRect = new Rectangle(0, 0, target.Width, target.Height);

            // If the copy operation results in an entire copy, use CopyPixels instead
            if (srcBitmapRect == srcRect && destBitmapRect == destRect && srcBitmapRect == destBitmapRect)
            {
                CopyPixels(source, target);
                return;
            }

            using (var fastTarget = target.FastLock())
            {
                fastTarget.CopyRegion(source, srcRect, destRect);
            }
        }

        /// <summary>
        /// Returns a bitmap that is a slice of the original provided 32bpp Bitmap.
        /// The region must have a width and a height > 0, and must lie inside the source bitmap's area
        /// </summary>
        /// <param name="source">The source bitmap to slice</param>
        /// <param name="region">The region of the source bitmap to slice</param>
        /// <returns>A Bitmap that represents the rectangle region slice of the source bitmap</returns>
        /// <exception cref="ArgumentException">The provided bimap is not 32bpp</exception>
        /// <exception cref="ArgumentException">The provided region is invalid</exception>
        public static Bitmap SliceBitmap(Bitmap source, Rectangle region)
        {
            if (region.Width <= 0 || region.Height <= 0)
            {
                throw new ArgumentException(@"The provided region must have a width and a height > 0", nameof(region));
            }

            var sliceRectangle = Rectangle.Intersect(new Rectangle(Point.Empty, source.Size), region);

            if (sliceRectangle.IsEmpty)
            {
                throw new ArgumentException(@"The provided region must not lie outside of the bitmap's region completely", nameof(region));
            }

            var slicedBitmap = new Bitmap(sliceRectangle.Width, sliceRectangle.Height);
            CopyRegion(source, slicedBitmap, sliceRectangle, new Rectangle(0, 0, sliceRectangle.Width, sliceRectangle.Height));

            return slicedBitmap;
        }

#if NETSTANDARD
        public static void memcpy(IntPtr dest, IntPtr src, ulong count)
        {
            Buffer.MemoryCopy(src.ToPointer(), dest.ToPointer(), count, count);
        }

        public static void memcpy(void* dest, void* src, ulong count)
        {
            Buffer.MemoryCopy(src, dest, count, count);
        }

        public static void memset(void* dest, int value, ulong count)
        {
            Unsafe.InitBlock(dest, (byte)value, (uint)count);
        }
#else
        /// <summary>
        /// .NET wrapper to native call of 'memcpy'. Requires Microsoft Visual C++ Runtime installed
        /// </summary>
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, ulong count);

        /// <summary>
        /// .NET wrapper to native call of 'memcpy'. Requires Microsoft Visual C++ Runtime installed
        /// </summary>
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(void* dest, void* src, ulong count);

        /// <summary>
        /// .NET wrapper to native call of 'memset'. Requires Microsoft Visual C++ Runtime installed
        /// </summary>
        [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memset(void* dest, int value, ulong count);
#endif

        /// <summary>
        /// Represents a disposable structure that is returned during Lock() calls, and unlocks the bitmap on Dispose calls
        /// </summary>
        public struct FastBitmapLocker : IDisposable
        {
            /// <summary>
            /// Gets the fast bitmap instance attached to this locker
            /// </summary>
            public FastBitmap FastBitmap { get; }

            /// <summary>
            /// Initializes a new instance of the FastBitmapLocker struct with an initial fast bitmap object.
            /// The fast bitmap object passed will be unlocked after calling Dispose() on this struct
            /// </summary>
            /// <param name="fastBitmap">A fast bitmap to attach to this locker which will be released after a call to Dispose</param>
            public FastBitmapLocker(FastBitmap fastBitmap)
            {
                FastBitmap = fastBitmap;
            }

            /// <summary>
            /// 析构函数
            /// </summary>
            public void Dispose()
            {
                if (FastBitmap.Locked)
                    FastBitmap.Unlock();
            }
        }
    }

    /// <summary>
    /// Describes a pixel format to use when locking a bitmap using <see cref="FastBitmap"/>.
    /// </summary>
    public enum FastBitmapLockFormat
    {
        /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components. The remaining 8 bits are not used.</summary>
        Format32bppRgb = 139273,
        /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied, according to the alpha component.</summary>
        Format32bppPArgb = 925707,
        /// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.</summary>
        Format32bppArgb = 2498570,
    }

    /// <summary>
    /// Static class that contains fast bitmap extension methdos for the Bitmap class
    /// </summary>
    public static class FastBitmapExtensions
    {
        /// <summary>
        /// Locks this bitmap into memory and returns a FastBitmap that can be used to manipulate its pixels
        /// </summary>
        /// <param name="bitmap">The bitmap to lock</param>
        /// <returns>A locked FastBitmap</returns>
        public static FastBitmap FastLock(this Bitmap bitmap)
        {
            var fast = new FastBitmap(bitmap);
            fast.Lock();

            return fast;
        }

        /// <summary>
        /// Locks this bitmap into memory and returns a FastBitmap that can be used to manipulate its pixels
        /// </summary>
        /// <param name="bitmap">The bitmap to lock</param>
        /// <param name="lockFormat">The underlying pixel format to use when locking the bitmap</param>
        /// <returns>A locked FastBitmap</returns>
        public static FastBitmap FastLock(this Bitmap bitmap, FastBitmapLockFormat lockFormat)
        {
            var fast = new FastBitmap(bitmap);
            fast.Lock(lockFormat);

            return fast;
        }

        /// <summary>
        /// Returns a deep clone of this Bitmap object, with all the data copied over.
        /// After a deep clone, the new bitmap is completely independent from the original
        /// </summary>
        /// <param name="bitmap">The bitmap to clone</param>
        /// <returns>A deep clone of this Bitmap object, with all the data copied over</returns>
        public static Bitmap DeepClone(this Bitmap bitmap)
        {
            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
        }
    }
}