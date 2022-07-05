using System;
using System.Runtime.InteropServices;
using HANDLE = System.IntPtr;
using HDC = System.IntPtr;
using HWND = System.IntPtr;

namespace Sunny.UI.Win32
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public struct XFORM
    {
        public double eM11;
        public double eM12;
        public double eM21;
        public double eM22;
        public double eDx;
        public double eDy;
    }

    public struct BITMAP
    {
        public int bmType;
        public int bmWidth;
        public int bmHeight;
        public int bmWidthBytes;
        public short bmPlanes;
        public short bmBitsPixel;
        public int bmBits;
    }

    public struct RGBTRIPLE
    {
        public byte rgbtBlue;
        public byte rgbtGreen;
        public byte rgbtRed;
    }

    public struct RGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    public struct BITMAPCOREHEADER
    {
        public int bcSize;
        public short bcWidth;
        public short bcHeight;
        public short bcPlanes;
        public short bcBitCount;
    }

    public struct BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public RGBQUAD bmiColors;
    }

    public struct BITMAPCOREINFO
    {
        public BITMAPCOREHEADER bmciHeader;
        public RGBTRIPLE bmciColors;
    }

    public struct BITMAPFILEHEADER
    {
        public short bfType;
        public int bfSize;
        public short bfReserved1;
        public short bfReserved2;
        public int bfOffBits;
    }

    public struct HANDLETABLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public int objectHandle;
    }

    public struct METARECORD
    {
        public int rdSize;
        public short rdFunction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public short rdParm;
    }

    public struct METAFILEPICT
    {
        public int mm;
        public int xExt;
        public int yExt;
        public HANDLE hMF;
    }

    public struct METAHEADER
    {
        public short mtType;
        public short mtHeaderSize;
        public short mtVersion;
        public int mtSize;
        public short mtNoObjects;
        public int mtMaxRecord;
        public short mtNoParameters;
    }

    public struct ENHMETARECORD
    {
        public int iType;
        public int nSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public int dParm;
    }

    public struct ENHMETAHEADER
    {
        public int iType;
        public int nSize;
        public RECT rclBounds;
        public RECT rclFrame;
        public int dSignature;
        public int nVersion;
        public int nBytes;
        public int nRecords;
        public short nHandles;
        public short sReserved;
        public int nDescription;
        public int offDescription;
        public int nPalEntries;
        public SIZE szlDevice;
        public SIZE szlMillimeters;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct TEXTMETRIC
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public byte tmFirstChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmLastChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmDefaultChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmBreakChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TEXTMETRICW
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public ushort tmFirstChar;
        public ushort tmLastChar;
        public ushort tmDefaultChar;
        public ushort tmBreakChar;
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
    }

    [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct TEXTMETRICA
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public byte tmFirstChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmLastChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmDefaultChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmBreakChar;    // for compatibility issues it must be byte instead of char (see the comment for further details above)
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
    }

    public struct NEWTEXTMETRIC
    {
        public int tmHeight;
        public int tmAscent;
        public int tmDescent;
        public int tmInternalLeading;
        public int tmExternalLeading;
        public int tmAveCharWidth;
        public int tmMaxCharWidth;
        public int tmWeight;
        public int tmOverhang;
        public int tmDigitizedAspectX;
        public int tmDigitizedAspectY;
        public byte tmFirstChar;
        public byte tmLastChar;
        public byte tmDefaultChar;
        public byte tmBreakChar;
        public byte tmItalic;
        public byte tmUnderlined;
        public byte tmStruckOut;
        public byte tmPitchAndFamily;
        public byte tmCharSet;
        public int ntmFlags;
        public int ntmSizeEM;
        public int ntmCellHeight;
        public int ntmAveWidth;
    }

    public struct PELARRAY
    {
        public int paXCount;
        public int paYCount;
        public int paXExt;
        public int paYExt;
        public short paRGBs;
    }

    public struct LOGBRUSH
    {
        public int lbStyle;
        public uint lbColor;
        public int lbHatch;
    }

    public struct LOGPEN
    {
        public int lopnStyle;
        public POINT lopnWidth;
        public uint lopnColor;
    }

    public struct EXTLOGPEN
    {
        public int elpPenStyle;
        public int elpWidth;
        public int elpBrushStyle;
        public int elpColor;
        public int elpHatch;
        public int elpNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public int elpStyleEntry;
    }

    public struct PALETTEENTRY
    {
        public byte peRed;
        public byte peGreen;
        public byte peBlue;
        public byte peFlags;
    }

    public struct LOGPALETTE
    {
        public short palVersion;
        public short palNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public PALETTEENTRY palPalEntry;
    }

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    //public class LOGFONT
    //{
    //    public long lfHeight;
    //    public long lfWidth;
    //    public long lfEscapement;
    //    public long lfOrientation;
    //    public long lfWeight;
    //    public byte lfItalic;
    //    public byte lfUnderline;
    //    public byte lfStrikeOut;
    //    public byte lfCharSet;
    //    public byte lfOutPrecision;
    //    public byte lfClipPrecision;
    //    public byte lfQuality;
    //    public byte lfPitchAndFamily;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string lfFaceName;
    //}

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    //public class LOGFONT2
    //{
    //    public int lfHeight;
    //    public int lfWidth;
    //    public int lfEscapement;
    //    public int lfOrientation;
    //    public int lfWeight;
    //    public byte lfItalic;
    //    public byte lfUnderline;
    //    public byte lfStrikeOut;
    //    public byte lfCharSet;
    //    public byte lfOutPrecision;
    //    public byte lfClipPrecision;
    //    public byte lfQuality;
    //    public byte lfPitchAndFamily;
    //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string lfFaceName;
    //}

    public struct LOGFONT
    {
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName;
    }

    public struct TT_HHEA
    {
        public uint Version;
        public Int16 Ascender;
        public Int16 Descender;
        public Int16 LineGap;
        public UInt16 advanceWidthMax;
        public Int16 minLeftSideBearing;
        public Int16 minRightSideBearing;
        public Int16 xMaxExtent;
        public Int16 caretSlopeRise;
        public Int16 caretSlopeRun;
        public Int16 caretOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public Int16[] reserved;
        public Int16 metricDataFormat;
        public UInt16 numberOfHMetrics;
    }

    public struct NONCLIENTMETRICS
    {
        public int cbSize;
        public int iBorderWidth;
        public int iScrollWidth;
        public int iScrollHeight;
        public int iCaptionWidth;
        public int iCaptionHeight;
        public LOGFONT lfCaptionFont;
        public int iSMCaptionWidth;
        public int iSMCaptionHeight;
        public LOGFONT lfSMCaptionFont;
        public int iMenuWidth;
        public int iMenuHeight;
        public LOGFONT lfMenuFont;
        public LOGFONT lfStatusFont;
        public LOGFONT lfMessageFont;
    }

    public struct ENUMLOGFONT
    {
        public LOGFONT elfLogFont;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FULLFACESIZE)] public byte[] elfFullName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FACESIZE)] public byte[] elfStyle;
    }

    public struct PANOSE
    {
        //jtpublic int ulculture;
        public byte bFamilyType;
        public byte bSerifStyle;
        public byte bWeight;
        public byte bProportion;
        public byte bContrast;
        public byte bStrokeVariation;
        public byte bArmStyle;
        public byte bLetterform;
        public byte bMidline;
        public byte bXHeight;
    }

    public struct EXTLOGFONT
    {
        public LOGFONT elfLogFont;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FULLFACESIZE)] public byte[] elfFullName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FACESIZE)] public byte[] elfStyle;
        public int elfVersion;
        public int elfStyleSize;
        public int elfMatch;
        public int elfReserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.ELF_VENDOR_SIZE)] public byte[] elfVendorId;
        public int elfCulture;
        public PANOSE elfPanose;
    }

    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;
        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = GDI.CCHFORMNAME)] public string dmFormName;
        public short dmUnusedPadding;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
    }

    public struct RGNDATAHEADER
    {
        public int dwSize;
        public int iType;
        public int nCount;
        public int nRgnSize;
        public RECT rcBound;
    }

    public struct RGNDATA
    {
        public RGNDATAHEADER rdh;
        public byte Buffer;
    }
    public struct ABC
    {
        public int abcA;
        public int abcB;
        public int abcC;
    }

    public struct ABCFLOAT
    {
        public double abcfA;
        public double abcfB;
        public double abcfC;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct OUTLINETEXTMETRICW
    {
        public uint otmSize;
        public TEXTMETRICW otmTextMetrics;
        public byte otmFiller;
        public PANOSE otmPanoseNumber;
        public uint otmfsSelection;
        public uint otmfsType;
        public int otmsCharSlopeRise;
        public int otmsCharSlopeRun;
        public int otmItalicAngle;
        public uint otmEMSquare;
        public int otmAscent;
        public int otmDescent;
        public uint otmLineGap;
        public uint otmsCapEmHeight;
        public uint otmsXHeight;
        public RECT otmrcFontBox;
        public int otmMacAscent;
        public int otmMacDescent;
        public uint otmMacLineGap;
        public uint otmusMinimumPPEM;
        public POINT otmptSubscriptSize;
        public POINT otmptSubscriptOffset;
        public POINT otmptSuperscriptSize;
        public POINT otmptSuperscriptOffset;
        public uint otmsStrikeoutSize;
        public int otmsStrikeoutPosition;
        public int otmsUnderscoreSize;
        public int otmsUnderscorePosition;
        public IntPtr otmpFamilyName;
        public IntPtr otmpFaceName;
        public IntPtr otmpStyleName;
        public IntPtr otmpFullName;
    }

    public struct POLYTEXT
    {
        public int x;
        public int y;
        public int n;
        public string lpStr;
        public int uiFlags;
        public RECT rcl;
        public int pdx;
    }

    public struct FIXED
    {
        public short fract;
        public short Value;
    }

    public struct MAT2
    {
        public FIXED eM11;
        public FIXED eM12;
        public FIXED eM21;
        public FIXED eM22;
    }

    public struct GLYPHMETRICS
    {
        public int gmBlackBoxX;
        public int gmBlackBoxY;
        public POINT gmptGlyphOrigin;
        public short gmCellIncX;
        public short gmCellIncY;
    }

    public struct POINTFX
    {
        public FIXED x;
        public FIXED y;
    }

    public struct TTPOLYCURVE
    {
        public short wType;
        public short cpfx;
        public POINTFX apfx;
    }

    public struct TTPOLYGONHEADER
    {
        public int cb;
        public int dwType;
        public POINTFX pfxStart;
    }

    public struct RASTERIZER_STATUS
    {
        public short nSize;
        public short wFlags;
        public short nLanguageID;
    }

    public struct COLORADJUSTMENT
    {
        public short caSize;
        public short caFlags;
        public short caIlluminantIndex;
        public short caRedGamma;
        public short caGreenGamma;
        public short caBlueGamma;
        public short caReferenceBlack;
        public short caReferenceWhite;
        public short caContrast;
        public short caBrightness;
        public short caColorfulness;
        public short caRedGreenTint;
    }

    public struct DOCINFO
    {
        public int cbSize;
        public string lpszDocName;
        public string lpszOutput;
    }

    public struct KERNINGPAIR
    {
        public short wFirst;
        public short wSecond;
        public int iKernAmount;
    }

    public struct BITMAPV4HEADER
    {
        public int bV4Size;
        public int bV4Width;
        public int bV4Height;
        public short bV4Planes;
        public short bV4BitCount;
        public int bV4V4Compression;
        public int bV4SizeImage;
        public int bV4XPelsPerMeter;
        public int bV4YPelsPerMeter;
        public int bV4ClrUsed;
        public int bV4ClrImportant;
        public int bV4RedMask;
        public int bV4GreenMask;
        public int bV4BlueMask;
        public int bV4AlphaMask;
        public int bV4CSType;
        public int bV4Endpoints;
        public int bV4GammaRed;
        public int bV4GammaGreen;
        public int bV4GammaBlue;
    }

    public struct FONTSIGNATURE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public uint[] fsUsb;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public uint[] fsCsb;
    }

    public struct CHARSETINFO
    {
        public int ciCharset;
        public int ciACP;
        public FONTSIGNATURE fs;
    }

    public struct LOCALESIGNATURE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public int lsUsb;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public int lsCsbDefault;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public int lsCsbSupported;
    }

    public struct NEWTEXTMETRICEX
    {
        public NEWTEXTMETRIC ntmTm;
        public FONTSIGNATURE ntmFontSig;
    }

    public struct ENUMLOGFONTEX
    {
        public LOGFONT elfLogFont;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FULLFACESIZE)] public byte[] elfFullName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FACESIZE)] public byte[] elfStyle;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GDI.LF_FACESIZE)] public byte[] elfScript;
    }

    public struct GCP_RESULTS
    {
        public int lStructSize;
        public string lpOutString;
        public int lpOrder;
        public int lpDX;
        public int lpCaretPos;
        public string lpClass;
        public string lpGlyphs;
        public int nGlyphs;
        public int nMaxFit;
    }

    public struct CIEXYZ
    {
        public int ciexyzX;
        public int ciexyzY;
        public int ciexyzZ;
    }

    public struct CIEXYZTRIPLE
    {
        public CIEXYZ ciexyzRed;
        public CIEXYZ ciexyzGreen;
        public CIEXYZ ciexyBlue;
    }

    public struct LOGCOLORSPACE
    {
        public int lcsSignature;
        public int lcsVersion;
        public int lcsSize;
        public int lcsCSType;
        public int lcsIntent;
        public CIEXYZTRIPLE lcsEndPoints;
        public int lcsGammaRed;
        public int lcsGammaGreen;
        public int lcsGammaBlue;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Kernel.MAX_PATH)] public string lcsFileName;
    }

    public struct PIXELFORMATDESCRIPTOR
    {
        public short nSize;
        public short nVersion;
        public int dwFlags;
        public byte iPixelType;
        public byte cColorBits;
        public byte cRedBits;
        public byte cRedShift;
        public byte cGreenBits;
        public byte cGreenShift;
        public byte cBlueBits;
        public byte cBlueShift;
        public byte cAlphaBits;
        public byte cAlphaShift;
        public byte cAccumBits;
        public byte cAccumRedBits;
        public byte cAccumGreenBits;
        public byte cAccumBlueBits;
        public byte cAccumAlphaBits;
        public byte cDepthBits;
        public byte cStencilBits;
        public byte cAuxBuffers;
        public byte iLayerType;
        public byte bReserved;
        public int dwLayerMask;
        public int dwVisibleMask;
        public int dwDamageMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    public partial class GDI
    {
        [DllImport("gdi32")] public static extern int AbortDoc(HDC hdc);
        [DllImport("gdi32")] public static extern int AbortPath(HDC hdc);
        [DllImport("gdi32")] public static extern int AddFontResource(string lpFileName);
        [DllImport("gdi32")] public static extern int AngleArc(HDC hdc, int x, int y, int dwRadius, double eStartAngle, double eSweepAngle);
        [DllImport("gdi32")] public static extern int AnimatePalette(HANDLE hPalette, int wStartIndex, int wNumEntries, PALETTEENTRY[] lpPaletteColors);
        [DllImport("gdi32")] public static extern int Arc(HDC hdc, int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4);
        [DllImport("gdi32")] public static extern int ArcTo(HDC hdc, int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4);
        [DllImport("gdi32")] public static extern int BeginPath(HDC hdc);
        [DllImport("gdi32")] public static extern int BitBlt(HDC hDestDC, int x, int y, int nWidth, int nHeight, HDC hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("gdi32")] public static extern int CancelDC(HDC hdc);
        [DllImport("gdi32")] public static extern int CheckColorsInGamut(HDC hdc, IntPtr lpv, IntPtr lpv2, int dw);
        [DllImport("gdi32")] public static extern int ChoosePixelFormat(HWND hdc, ref PIXELFORMATDESCRIPTOR pPixelFormatDescriptor);
        [DllImport("gdi32")] public static extern int Chord(HDC hdc, int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4);
        [DllImport("gdi32")] public static extern int CloseEnhMetaFile(HDC hdc);
        [DllImport("gdi32")] public static extern int CloseFigure(HDC hdc);
        [DllImport("gdi32")] public static extern int CloseMetaFile(HANDLE hMF);
        [DllImport("gdi32")] public static extern int ColorMatchToTarget(HDC hdc, HWND hdc2, int dw);
        [DllImport("gdi32")] public static extern int CombineRgn(HANDLE hDestRgn, HANDLE hSrcRgn1, HANDLE hSrcRgn2, int nCombineMode);
        [DllImport("gdi32")] public static extern int CombineTransform(ref XFORM lpxformResult, ref XFORM lpxform1, ref XFORM lpxform2);
        [DllImport("gdi32")] public static extern int CopyEnhMetaFile(HANDLE hemfSrc, string lpszFile);
        [DllImport("gdi32")] public static extern int CopyMetaFile(HANDLE hMF, string lpFileName);
        [DllImport("gdi32")] public static extern int CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitCount, IntPtr lpBits);
        [DllImport("gdi32")] public static extern int CreateBitmapIndirect(ref BITMAP lpBitmap);
        [DllImport("gdi32")] public static extern int CreateBrushIndirect(ref LOGBRUSH lpLogBrush);
        [DllImport("gdi32")] public static extern int CreateColorSpace(ref LOGCOLORSPACE lplogcolorspace);
        [DllImport("gdi32")] public static extern int CreateCompatibleBitmap(HDC hdc, int nWidth, int nHeight);
        [DllImport("gdi32")] public static extern int CreateCompatibleDC(HDC hdc);
        [DllImport("gdi32")] public static extern int CreateDC(string lpDriverName, string lpDeviceName, string lpOutput, ref DEVMODE lpInitData);
        [DllImport("gdi32")] public static extern int CreateDIBPatternBrush(HANDLE hPackedDIB, int wUsage);
        [DllImport("gdi32")] public static extern int CreateDIBPatternBrushPt(IntPtr lpPackedDIB, int iUsage);
        [DllImport("gdi32")] public static extern int CreateDIBSection(HWND hdc, ref BITMAPINFO pBitmapInfo, int un, ref IntPtr lplpVoid, HANDLE handle, int dw);
        [DllImport("gdi32")] public static extern int CreateDIBitmap(HDC hdc, ref BITMAPINFOHEADER lpInfoHeader, int dwUsage, IntPtr lpInitBits, ref BITMAPINFO lpInitInfo, int wUsage);
        [DllImport("gdi32")] public static extern int CreateDiscardableBitmap(HDC hdc, int nWidth, int nHeight);
        [DllImport("gdi32")] public static extern int CreateEllipticRgn(int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern IntPtr CreateEllipticRgnIndirect(ref RECT lpRect);
        [DllImport("gdi32")] public static extern int CreateEnhMetaFile(HWND hdcRef, string lpFileName, ref RECT lpRect, string lpDescription);
        [DllImport("gdi32")] public static extern int CreateFont(HANDLE h, int W, int E, int O, int W2, int I, int u, int S, int C, int OP, int CP, int Q, int PAF, string F);
        [DllImport("gdi32")] public static extern int CreateFontIndirect(ref LOGFONT lpLogFont);
        [DllImport("gdi32")] public static extern int CreateHalftonePalette(HDC hdc);
        [DllImport("gdi32")] public static extern int CreateHatchBrush(int nIndex, int crColor);
        [DllImport("gdi32")] public static extern int CreateIC(string lpDriverName, string lpDeviceName, string lpOutput, ref DEVMODE lpInitData);
        [DllImport("gdi32")] public static extern int CreateMetaFile(string lpString);
        [DllImport("gdi32")] public static extern int CreatePalette(ref LOGPALETTE lpLogPalette);
        [DllImport("gdi32")] public static extern int CreatePatternBrush(HANDLE hBitmap);
        [DllImport("gdi32")] public static extern int CreatePen(int nPenStyle, int nWidth, int crColor);
        [DllImport("gdi32")] public static extern int CreatePenIndirect(ref LOGPEN lpLogPen);
        [DllImport("gdi32")] public static extern int CreatePolyPolygonRgn(ref POINT lpPoint, ref int lpPolyCounts, int nCount, int nPolyFillMode);
        [DllImport("gdi32")] public static extern IntPtr CreatePolygonRgn(POINT[] lpPoint, int nCount, int nPolyFillMode);
        [DllImport("gdi32")] public static extern IntPtr CreateRectRgn(int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int CreateRectRgnIndirect(ref RECT lpRect);
        [DllImport("gdi32")] public static extern int CreateRoundRectRgn(int X1, int Y1, int X2, int Y2, int X3, int Y3);
        [DllImport("gdi32")] public static extern int CreateScalableFontResource(int fHidden, string lpszResourceFile, string lpszFontFile, string lpszCurrentPath);
        [DllImport("gdi32")] public static extern int CreateSolidBrush(int crColor);
        [DllImport("gdi32")] public static extern int DPtoLP(HDC hdc, ref POINT lpPoint, int nCount);
        [DllImport("gdi32")] public static extern int DeleteColorSpace(HANDLE hcolorspace);
        [DllImport("gdi32")] public static extern int DeleteDC(HDC hdc);
        [DllImport("gdi32")] public static extern int DeleteEnhMetaFile(HANDLE hemf);
        [DllImport("gdi32")] public static extern int DeleteMetaFile(HANDLE hMF);
        [DllImport("gdi32")] public static extern int DeleteObject(HANDLE hObject);
        [DllImport("gdi32")] public static extern int DescribePixelFormat(HWND hdc, int n, int un, ref PIXELFORMATDESCRIPTOR lpPixelFormatDescriptor);
        [DllImport("gdi32")] public static extern int DrawEscape(HDC hdc, int nEscape, int cbInput, string lpszInData);
        [DllImport("gdi32")] public static extern int Ellipse(HDC hdc, int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int EndDoc(HDC hdc);
        [DllImport("gdi32")] public static extern int EndPage(HDC hdc);
        [DllImport("gdi32")] public static extern int EndPath(HDC hdc);
        [DllImport("gdi32")] public static extern int EnumEnhMetaFile(HDC hdc, HANDLE hemf, ref int lpEnhMetaFunc, IntPtr lpData, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int EnumFontFamilies(HDC hdc, string lpszFamily, ref int lpEnumFontFamProc, int lParam);
        [DllImport("gdi32")] public static extern int EnumFontFamiliesEx(HDC hdc, ref LOGFONT lpLogFont, ref int lpEnumFontProc, int lParam, int dw);
        [DllImport("gdi32")] public static extern int EnumFonts(HWND hdc, string lpsz, ref int lpFontEnumProc, int lParam);
        [DllImport("gdi32")] public static extern int EnumICMProfiles(HDC hdc, int icmEnumProc, int lParam);
        [DllImport("gdi32")] public static extern int EnumMetaFile(HWND hdc, HANDLE hMetafile, ref int lpMFEnumProc, int lParam);
        [DllImport("gdi32")] public static extern int EnumObjects(HWND hdc, int n, ref int lpGOBJEnumProc, IntPtr lpVoid);
        [DllImport("gdi32")] public static extern int EqualRgn(HANDLE hSrcRgn1, HANDLE hSrcRgn2);
        [DllImport("gdi32")] public static extern int Escape(HDC hdc, int nEscape, int nCount, string lpInData, IntPtr lpOutData);
        [DllImport("gdi32")] public static extern int ExcludeClipRect(HDC hdc, int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int ExtCreatePen(int dwPenStyle, int dwWidth, ref LOGBRUSH lplb, int dwStyleCount, ref int lpStyle);
        [DllImport("gdi32")] public static extern int ExtCreatePen(int dwPenStyle, int dwWidth, ref LOGBRUSH lplb, int dwStyleCount, IntPtr lpStyle);
        [DllImport("gdi32")] public static extern int ExtCreateRegion(ref XFORM lpXform, int nCount, ref RGNDATA lpRgnData);
        [DllImport("gdi32")] public static extern int ExtEscape(HDC hdc, int nEscape, int cbInput, string lpszInData, int cbOutput, string lpszOutData);
        [DllImport("gdi32")] public static extern int ExtFloodFill(HDC hdc, int x, int y, int crColor, int wFillType);
        [DllImport("gdi32")] public static extern int ExtSelectClipRgn(HDC hdc, HANDLE hRgn, int fnMode);
        [DllImport("gdi32", EntryPoint = "ExtTextOutW", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int ExtTextOut(HDC hdc, int x, int y, int wOptions, ref RECT lpRect, string lpString, int nCount, IntPtr lpDx);
        [DllImport("gdi32", EntryPoint = "ExtTextOutW", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int ExtTextOut(HDC hdc, int x, int y, int wOptions, IntPtr lpRect, string lpString, int nCount, IntPtr lpDx);
        [DllImport("gdi32")] public static extern int FillPath(HDC hdc);
        [DllImport("gdi32")] public static extern int FillRgn(HDC hdc, HANDLE hRgn, HANDLE hBrush);
        [DllImport("gdi32")] public static extern int FixBrushOrgEx(HWND hdc, int n1, int n2, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int FlattenPath(HDC hdc);
        [DllImport("gdi32")] public static extern int FloodFill(HDC hdc, int x, int y, int crColor);
        [DllImport("gdi32")] public static extern int FrameRgn(HDC hdc, HANDLE hRgn, HANDLE hBrush, int nWidth, int nHeight);
        [DllImport("gdi32")] public static extern int GdiComment(HDC hdc, int cbSize, Byte lpData);
        [DllImport("gdi32")] public static extern int GdiFlush();
        [DllImport("gdi32")] public static extern int GdiGetBatchLimit();
        [DllImport("gdi32")] public static extern int GdiSetBatchLimit(int dwLimit);
        [DllImport("gdi32")] public static extern int GetArcDirection(HDC hdc);
        [DllImport("gdi32")] public static extern int GetAspectRatioFilterEx(HDC hdc, ref SIZE lpAspectRatio);
        [DllImport("gdi32")] public static extern int GetBitmapBits(HANDLE hBitmap, int dwCount, IntPtr lpBits);
        [DllImport("gdi32")] public static extern int GetBitmapDimensionEx(HANDLE hBitmap, ref SIZE lpDimension);
        [DllImport("gdi32")] public static extern int GetBkColor(HDC hdc);
        [DllImport("gdi32")] public static extern int GetBkMode(HDC hdc);
        [DllImport("gdi32")] public static extern int GetBoundsRect(HDC hdc, ref RECT lprcBounds, int flags);
        [DllImport("gdi32")] public static extern int GetBrushOrgEx(HWND hdc, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int GetCharABCWidths(HDC hdc, int uFirstChar, int uLastChar, ref ABC lpabc);
        [DllImport("gdi32")] public static extern int GetCharABCWidthsFloat(HDC hdc, int iFirstChar, int iLastChar, ref ABCFLOAT lpABCF);
        [DllImport("gdi32")] public static extern int GetCharWidth(HDC hdc, int wFirstChar, int wLastChar, int[] lpBuffer);
        [DllImport("gdi32")] public static extern int GetCharWidth32(HDC hdc, int iFirstChar, int iLastChar, int[] lpBuffer);
        [DllImport("gdi32")] public static extern int GetCharWidthFloat(HDC hdc, int iFirstChar, int iLastChar, double[] pxBuffer);
        [DllImport("gdi32")] public static extern int GetCharacterPlacement(HDC hdc, string lpsz, int n1, int n2, ref GCP_RESULTS lpGcpResults, int dw);
        [DllImport("gdi32")] public static extern int GetClipBox(HDC hdc, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int GetClipRgn(HDC hdc, HANDLE hRgn);
        [DllImport("gdi32")] public static extern int GetColorAdjustment(HDC hdc, ref COLORADJUSTMENT lpca);
        [DllImport("gdi32")] public static extern int GetColorSpace(HDC hdc);
        [DllImport("gdi32")] public static extern int GetCurrentObject(HDC hdc, int uObjectType);
        [DllImport("gdi32")] public static extern int GetCurrentPositionEx(HDC hdc, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int GetDCOrgEx(HDC hdc, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int GetDIBColorTable(HWND hdc, int un1, int un2, ref RGBQUAD pRGBQuad);
        [DllImport("gdi32")] public static extern int GetDIBits(int aHDC, HANDLE hBitmap, int nStartScan, int nNumScans, IntPtr lpBits, ref BITMAPINFO lpBI, int wUsage);
        [DllImport("gdi32")] public static extern int GetDeviceCaps(HDC hdc, int nIndex);
        [DllImport("gdi32")] public static extern int GetDeviceGammaRamp(HDC hdc, IntPtr lpv);
        [DllImport("gdi32")] public static extern int GetEnhMetaFile(string lpszMetaFile);
        [DllImport("gdi32")] public static extern int GetEnhMetaFileBits(HANDLE hemf, int cbBuffer, Byte lpbBuffer);
        [DllImport("gdi32")] public static extern int GetEnhMetaFileDescription(HANDLE hemf, int cchBuffer, string lpszDescription);
        [DllImport("gdi32")] public static extern int GetEnhMetaFileHeader(HANDLE hemf, int cbBuffer, ref ENHMETAHEADER lpemh);
        [DllImport("gdi32")] public static extern int GetEnhMetaFilePaletteEntries(HANDLE hemf, int cEntries, ref PALETTEENTRY lppe);
        //jt[DllImport("gdi32")] public static extern int GetFontData(HDC hdc, int dwTable, int dwOffset, IntPtr lpvBuffer, int cbData);
        [DllImport("gdi32")] public static extern int GetFontData(HDC hdc, int dwTable, int dwOffset, ref TT_HHEA lpvBuffer, int cbData);
        [DllImport("gdi32")] public static extern int GetFontLanguageInfo(HDC hdc);
        [DllImport("gdi32")] public static extern int GetGlyphOutline(HDC hdc, int uChar, int fuFormat, ref GLYPHMETRICS lpgm, int cbBuffer, IntPtr lpBuffer, ref MAT2 lpmat2);
        [DllImport("gdi32")] public static extern int GetGraphicsMode(HDC hdc);
        [DllImport("gdi32")] public static extern int GetICMProfile(HDC hdc, int dw, string lpStr);
        [DllImport("gdi32")] public static extern int GetKerningPairs(HDC hdc, int cPairs, ref KERNINGPAIR lpkrnpair);
        [DllImport("gdi32")] public static extern int GetLogColorSpace(HANDLE hcolorspace, ref LOGCOLORSPACE lplogcolorspace, int dw);
        [DllImport("gdi32")] public static extern int GetMapMode(HDC hdc);
        [DllImport("gdi32")] public static extern int GetMetaFile(string lpFileName);
        [DllImport("gdi32")] public static extern int GetMetaFileBitsEx(HANDLE hMF, int nSize, IntPtr lpvData);
        [DllImport("gdi32")] public static extern int GetMetaRgn(HDC hdc, HANDLE hRgn);
        [DllImport("gdi32")] public static extern int GetMiterLimit(HDC hdc, double peLimit);
        [DllImport("gdi32")] public static extern int GetNearestColor(HDC hdc, int crColor);
        [DllImport("gdi32")] public static extern int GetNearestPaletteIndex(HANDLE hPalette, int crColor);
        [DllImport("gdi32")] public static extern int GetObject(HANDLE hObject, int nCount, IntPtr lpObject);
        [DllImport("gdi32")] public static extern int GetObject(HANDLE hObject, int nCount, ref LOGFONT logfont);
        [DllImport("gdi32")] public static extern int GetObjectType(HANDLE hgdiobj);
        [DllImport("gdi32", EntryPoint = "GetOutlineTextMetricsW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern uint GetOutlineTextMetrics(HDC hdc, uint cbData, IntPtr lpotm);
        //[DllImport("gdi32", EntryPoint = "GetOutlineTextMetricsW", CharSet = CharSet.Unicode, ExactSpelling = true)] 
        //public static extern int GetOutlineTextMetrics(HDC hdc, int cbData, ref OUTLINETEXTMETRICW lpotm);
        [DllImport("gdi32")] public static extern int GetPaletteEntries(HANDLE hPalette, int wStartIndex, int wNumEntries, ref PALETTEENTRY lpPaletteEntries);
        [DllImport("gdi32")] public static extern int GetPath(HDC hdc, ref POINT lpPoint, Byte lpTypes, int nSize);
        [DllImport("gdi32")] public static extern int GetPixel(HDC hdc, int x, int y);
        [DllImport("gdi32")] public static extern int GetPixelFormat(HWND hdc);
        [DllImport("gdi32")] public static extern int GetPolyFillMode(HDC hdc);
        [DllImport("gdi32")] public static extern int GetROP2(HDC hdc);
        [DllImport("gdi32")] public static extern int GetRasterizerCaps(ref RASTERIZER_STATUS lpraststat, int cb);
        [DllImport("gdi32")] public static extern int GetRegionData(HANDLE hRgn, int dwCount, ref RGNDATA lpRgnData);
        [DllImport("gdi32")] public static extern int GetRgnBox(HANDLE hRgn, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int GetStockObject(int nIndex);
        [DllImport("gdi32")] public static extern int GetStretchBltMode(HDC hdc);
        [DllImport("gdi32")] public static extern int GetSystemPaletteEntries(HDC hdc, int wStartIndex, int wNumEntries, ref PALETTEENTRY lpPaletteEntries);
        [DllImport("gdi32")] public static extern int GetSystemPaletteUse(HDC hdc);
        [DllImport("gdi32")] public static extern int GetTextAlign(HDC hdc);
        [DllImport("gdi32")] public static extern int GetTextCharacterExtra(HDC hdc);
        [DllImport("gdi32")] public static extern int GetTextCharset(HDC hdc);
        [DllImport("gdi32")] public static extern int GetTextCharsetInfo(HDC hdc, ref FONTSIGNATURE lpSig, int dwFlags);
        [DllImport("gdi32")] public static extern int GetTextColor(HDC hdc);

        [DllImport("gdi32", EntryPoint = "GetTextExtentExPointW", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GetTextExtentExPoint(HDC hdc, string lpszStr, int cchString, int nMaxExtent, int lpnFit, int[] alpDx, ref SIZE lpSize);

        [DllImport("gdi32", EntryPoint = "GetTextExtentExPointW", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GetTextExtentExPoint(HDC hdc, string lpszStr, int cchString, int nMaxExtent, IntPtr lpnFit, int[] alpDx, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int GetTextExtentPoint(HDC hdc, string lpszString, int cbString, ref SIZE lpSize);

        [DllImport("gdi32", EntryPoint = "GetTextExtentPoint32W", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GetTextExtentPoint32(HDC hdc, string lpsz, int cbString, ref SIZE lpSize);

        [DllImport("gdi32")] public static extern int GetTextFace(HDC hdc, int nCount, string lpFacename);
        [DllImport("gdi32")] public static extern int GetTextMetrics(HDC hdc, ref TEXTMETRICW lpMetrics);
        [DllImport("gdi32")] public static extern int GetViewportExtEx(HDC hdc, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int GetViewportOrgEx(HDC hdc, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int GetWinMetaFileBits(HANDLE hemf, int cbBuffer, Byte lpbBuffer, int fnMapMode, HWND hdcRef);
        [DllImport("gdi32")] public static extern int GetWindowExtEx(HDC hdc, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int GetWindowOrgEx(HDC hdc, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int GetWorldTransform(HDC hdc, ref XFORM lpXform);
        [DllImport("gdi32")] public static extern int IntersectClipRect(HDC hdc, int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int InvertRgn(HDC hdc, HANDLE hRgn);
        [DllImport("gdi32")] public static extern int LPtoDP(HDC hdc, ref POINT lpPoint, int nCount);
        [DllImport("gdi32")] public static extern int LineDDA(int n1, int n2, int n3, int n4, ref int lpLineDDAProc, int lParam);
        [DllImport("gdi32")] public static extern int LineTo(HDC hdc, int x, int y);
        [DllImport("gdi32")] public static extern int MaskBlt(HWND hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, HWND hdcSrc, int nXSrc, int nYSrc, HANDLE hbmMask, int xMask, int yMask, int dwRop);
        [DllImport("gdi32")] public static extern int ModifyWorldTransform(HDC hdc, ref XFORM lpXform, int iMode);
        [DllImport("gdi32")] public static extern int MoveToEx(HDC hdc, int x, int y, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int MoveToEx(HDC hdc, int x, int y, IntPtr lpPoint);
        [DllImport("gdi32")] public static extern int OffsetClipRgn(HDC hdc, int x, int y);
        [DllImport("gdi32")] public static extern int OffsetRgn(HANDLE hRgn, int x, int y);
        [DllImport("gdi32")] public static extern int OffsetViewportOrgEx(HDC hdc, int nX, int nY, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int OffsetWindowOrgEx(HDC hdc, int nX, int nY, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int PaintRgn(HDC hdc, HANDLE hRgn);
        [DllImport("gdi32")] public static extern int PatBlt(HDC hdc, int x, int y, int nWidth, int nHeight, int dwRop);
        [DllImport("gdi32")] public static extern int PathToRegion(HDC hdc);
        [DllImport("gdi32")] public static extern int Pie(HDC hdc, int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4);
        [DllImport("gdi32")] public static extern int PlayEnhMetaFile(HDC hdc, HANDLE hemf, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int PlayEnhMetaFileRecord(HDC hdc, ref HANDLETABLE lpHandletable, ref ENHMETARECORD lpEnhMetaRecord, int nHandles);
        [DllImport("gdi32")] public static extern int PlayMetaFile(HDC hdc, HANDLE hMF);
        [DllImport("gdi32")] public static extern int PlayMetaFileRecord(HDC hdc, ref HANDLETABLE lpHandletable, ref METARECORD lpMetaRecord, int nHandles);
        [DllImport("gdi32")] public static extern int PlgBlt(HWND hdcDest, ref POINT lpPoint, HWND hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight, HANDLE hbmMask, int xMask, int yMask);
        [DllImport("gdi32")] public static extern int PolyBezier(HDC hdc, ref POINT lppt, int cPoints);
        [DllImport("gdi32")] public static extern int PolyBezierTo(HDC hdc, ref POINT lppt, int cCount);
        [DllImport("gdi32")] public static extern int PolyDraw(HDC hdc, ref POINT lppt, Byte lpbTypes, int cCount);
        [DllImport("gdi32")] public static extern int PolyPolygon(HDC hdc, ref POINT lpPoint, ref int lpPolyCounts, int nCount);
        [DllImport("gdi32")] public static extern int PolyPolyline(HDC hdc, ref POINT lppt, ref int lpdwPolyPoints, int cCount);
        [DllImport("gdi32")] public static extern int PolyTextOut(HDC hdc, ref POLYTEXT pptxt, int cStrings);
        [DllImport("gdi32")] public static extern int Polygon(HDC hdc, ref POINT lpPoint, int nCount);
        [DllImport("gdi32")] public static extern int Polyline(HDC hdc, ref POINT lpPoint, int nCount);
        [DllImport("gdi32")] public static extern int PolylineTo(HDC hdc, ref POINT lppt, int cCount);
        [DllImport("gdi32")] public static extern bool PtInRegion(HANDLE hRgn, int x, int y);
        [DllImport("gdi32")] public static extern int PtVisible(HDC hdc, int x, int y);
        [DllImport("gdi32")] public static extern int RealizePalette(HDC hdc);
        [DllImport("gdi32")] public static extern int RectInRegion(HANDLE hRgn, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int RectVisible(HDC hdc, ref RECT lpRect);
        [DllImport("gdi32")] public static extern int Rectangle(HDC hdc, int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int RemoveFontResource(string lpFileName);
        [DllImport("gdi32")] public static extern int ResetDC(HDC hdc, ref DEVMODE lpInitData);
        [DllImport("gdi32")] public static extern int ResizePalette(HANDLE hPalette, int nNumEntries);
        [DllImport("gdi32")] public static extern int RestoreDC(HDC hdc, int nSavedDC);
        [DllImport("gdi32")] public static extern int RoundRect(HDC hdc, int X1, int Y1, int X2, int Y2, int X3, int Y3);
        [DllImport("gdi32")] public static extern int SaveDC(HDC hdc);
        [DllImport("gdi32")] public static extern int ScaleViewportExtEx(HDC hdc, int nXnum, int nXdenom, int nYnum, int nYdenom, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int ScaleWindowExtEx(HDC hdc, int nXnum, int nXdenom, int nYnum, int nYdenom, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int SelectClipPath(HDC hdc, int iMode);
        [DllImport("gdi32")] public static extern int SelectClipRgn(HDC hdc, HANDLE hRgn);
        [DllImport("gdi32")] public static extern int SelectObject(HDC hdc, HANDLE hObject);
        [DllImport("gdi32")] public static extern int SelectPalette(HDC hdc, HANDLE hPalette, int bForceBackground);
        [DllImport("gdi32")] public static extern int SetAbortProc(HWND hdc, ref int lpAbortProc);
        [DllImport("gdi32")] public static extern int SetArcDirection(HDC hdc, int ArcDirection);
        [DllImport("gdi32")] public static extern int SetBitmapBits(HANDLE hBitmap, int dwCount, IntPtr lpBits);
        [DllImport("gdi32")] public static extern int SetBitmapDimensionEx(HANDLE hbm, int nX, int nY, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int SetBkColor(HDC hdc, uint crColor);
        [DllImport("gdi32")] public static extern int SetBkMode(HDC hdc, int nBkMode);
        [DllImport("gdi32")] public static extern int SetBoundsRect(HDC hdc, ref RECT lprcBounds, int flags);
        [DllImport("gdi32")] public static extern int SetBrushOrgEx(HDC hdc, int nXOrg, int nYOrg, ref POINT lppt);
        [DllImport("gdi32")] public static extern int SetColorAdjustment(HDC hdc, ref COLORADJUSTMENT lpca);
        [DllImport("gdi32")] public static extern int SetColorSpace(HDC hdc, HANDLE hcolorspace);
        [DllImport("gdi32")] public static extern int SetDIBColorTable(HWND hdc, int un1, int un2, ref RGBQUAD pcRGBQuad);
        [DllImport("gdi32")] public static extern int SetDIBits(HDC hdc, HANDLE hBitmap, int nStartScan, int nNumScans, IntPtr lpBits, ref BITMAPINFO lpBI, int wUsage);
        [DllImport("gdi32")] public static extern int SetDIBitsToDevice(HDC hdc, int x, int y, int dx, int dy, int SrcX, int SrcY, int Scan, int NumScans, IntPtr Bits, BITMAPINFO BitsInfo, int wUsage);
        [DllImport("gdi32")] public static extern int SetDeviceGammaRamp(HDC hdc, IntPtr lpv);
        [DllImport("gdi32")] public static extern int SetEnhMetaFileBits(int cbBuffer, Byte lpData);
        [DllImport("gdi32")] public static extern int SetGraphicsMode(HDC hdc, int iMode);
        [DllImport("gdi32")] public static extern int SetICMMode(HDC hdc, int n);
        [DllImport("gdi32")] public static extern int SetICMProfile(HDC hdc, string lpStr);
        [DllImport("gdi32")] public static extern int SetMapMode(HDC hdc, int nMapMode);
        [DllImport("gdi32")] public static extern int SetMapperFlags(HDC hdc, int dwFlag);
        [DllImport("gdi32")] public static extern int SetMetaFileBitsEx(int nSize, Byte lpData);
        [DllImport("gdi32")] public static extern int SetMetaRgn(HDC hdc);
        [DllImport("gdi32")] public static extern int SetMiterLimit(HDC hdc, double eNewLimit, double peOldLimit);
        [DllImport("gdi32")] public static extern int SetPaletteEntries(HANDLE hPalette, int wStartIndex, int wNumEntries, ref PALETTEENTRY lpPaletteEntries);
        [DllImport("gdi32")] public static extern int SetPixel(HDC hdc, int x, int y, int crColor);
        [DllImport("gdi32")] public static extern int SetPixelFormat(HWND hdc, int n, ref PIXELFORMATDESCRIPTOR pcPixelFormatDescriptor);
        [DllImport("gdi32")] public static extern int SetPixelV(HDC hdc, int x, int y, int crColor);
        [DllImport("gdi32")] public static extern int SetPolyFillMode(HDC hdc, int nPolyFillMode);
        [DllImport("gdi32")] public static extern int SetROP2(HDC hdc, int nDrawMode);
        [DllImport("gdi32")] public static extern int SetRectRgn(HANDLE hRgn, int X1, int Y1, int X2, int Y2);
        [DllImport("gdi32")] public static extern int SetStretchBltMode(HDC hdc, int nStretchMode);
        [DllImport("gdi32")] public static extern int SetSystemPaletteUse(HDC hdc, int wUsage);
        [DllImport("gdi32")] public static extern int SetTextAlign(HDC hdc, int wFlags);
        [DllImport("gdi32")] public static extern int SetTextCharacterExtra(HDC hdc, int nCharExtra);
        [DllImport("gdi32")] public static extern int SetTextColor(HDC hdc, int crColor);
        [DllImport("gdi32")] public static extern int SetTextJustification(HDC hdc, int nBreakExtra, int nBreakCount);
        [DllImport("gdi32")] public static extern int SetViewportExtEx(HDC hdc, int nX, int nY, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int SetViewportOrgEx(HDC hdc, int nX, int nY, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int SetWinMetaFileBits(int cbBuffer, Byte lpbBuffer, HWND hdcRef, ref METAFILEPICT lpmfp);
        [DllImport("gdi32")] public static extern int SetWindowExtEx(HDC hdc, int nX, int nY, ref SIZE lpSize);
        [DllImport("gdi32")] public static extern int SetWindowOrgEx(HDC hdc, int nX, int nY, ref POINT lpPoint);
        [DllImport("gdi32")] public static extern int SetWorldTransform(HDC hdc, ref XFORM lpXform);
        [DllImport("gdi32")] public static extern int StartDoc(HDC hdc, ref DOCINFO lpdi);
        [DllImport("gdi32")] public static extern int StartPage(HDC hdc);
        [DllImport("gdi32")] public static extern int StretchBlt(HDC hdc, int x, int y, int nWidth, int nHeight, HANDLE hSrcDC, int xSrc, int ySrc, int nSrcWidth, int nSrcHeight, int dwRop);
        [DllImport("gdi32")] public static extern int StretchDIBits(HDC hdc, int x, int y, int dx, int dy, int SrcX, int SrcY, int wSrcWidth, int wSrcHeight, IntPtr lpBits, ref BITMAPINFO lpBitsInfo, int wUsage, int dwRop);
        [DllImport("gdi32")] public static extern int StrokeAndFillPath(HDC hdc);
        [DllImport("gdi32")] public static extern int StrokePath(HDC hdc);
        [DllImport("gdi32")] public static extern int SwapBuffers(HWND hdc);
        [DllImport("gdi32")]//, EntryPoint = "TextOutW", CharSet = CharSet.Unicode, ExactSpelling = true)] 
        public static extern int TextOut(HDC hdc, int x, int y, string lpString, int nCount);
        [DllImport("gdi32")] public static extern int TranslateCharsetInfo(int lpSrc, ref CHARSETINFO lpcs, int dwFlags);
        [DllImport("gdi32")] public static extern int UnrealizeObject(HANDLE hObject);
        [DllImport("gdi32")] public static extern int UpdateColors(HDC hdc);
        [DllImport("gdi32")] public static extern int WidenPath(HDC hdc);

        [DllImport("msimg32")] // coredll
        extern public static Int32 AlphaBlend(IntPtr hdcDest, Int32 xDest, Int32 yDest, Int32 cxDest, Int32 cyDest,
            IntPtr hdcSrc, Int32 xSrc, Int32 ySrc, Int32 cxSrc, Int32 cySrc, BLENDFUNCTION blendFunction);

        public const byte AC_SRC_ALPHA = 1;
        public const byte AC_SRC_OVER = 0;
        public const int ULW_ALPHA = 2;

        public const int ABSOLUTE = 1;
        public const int AD_CLOCKWISE = 2;
        public const int AD_COUNTERCLOCKWISE = 1;
        public const int ALTERNATE = 1;
        public const int ANSI_CHARSET = 0;
        public const int ANSI_FIXED_FONT = 11;
        public const int ANSI_VAR_FONT = 12;
        public const int ARABIC_CHARSET = 178;
        public const int ASPECTX = 40;
        public const int ASPECTXY = 44;
        public const int ASPECTY = 42;
        public const int ASPECT_FILTERING = 0x1;
        public const int AbortDocC = 2;
        public const int BALTIC_CHARSET = 186;
        public const int BANDINFO = 24;
        public const int BEGIN_PATH = 4096;
        public const int BITSPIXEL = 12;
        public const int BI_RGB = 0;
        public const int BI_RLE4 = 2;
        public const int BI_RLE8 = 1;
        public const int BI_bitfields = 3;
        public const int BKMODE_LAST = 2;
        public const int BLACKNESS = 0x42;
        public const int BLACKONWHITE = 1;
        public const int BLACK_BRUSH = 4;
        public const int BLACK_PEN = 7;
        public const int BS_DIBPATTERN = 5;
        public const int BS_DIBPATTERN8X8 = 8;
        public const int BS_DIBPATTERNPT = 6;
        public const int BS_HATCHED = 2;
        public const int BS_HOLLOW = BS_NULL;
        public const int BS_INDEXED = 4;
        public const int BS_NULL = 1;
        public const int BS_PATTERN = 3;
        public const int BS_PATTERN8X8 = 7;
        public const int BS_SOLID = 0;
        public const int CA_LOG_FILTER = 0x2;
        public const int CA_NEGATIVE = 0x1;
        public const int CBM_CREATEDIB = 0x2;
        public const int CBM_INIT = 0x4;
        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;
        public const int CC_CHORD = 4;
        public const int CC_CIRCLES = 1;
        public const int CC_ELLIPSES = 8;
        public const int CC_INTERIORS = 128;
        public const int CC_NONE = 0;
        public const int CC_PIE = 2;
        public const int CC_ROUNDRECT = 256;
        public const int CC_STYLED = 32;
        public const int CC_WIDE = 16;
        public const int CC_WIDESTYLED = 64;
        public const int CHINESEBIG5_CHARSET = 136;
        public const int CLIPCAPS = 36;
        public const int CLIP_CHARACTER_PRECIS = 1;
        public const int CLIP_DEFAULT_PRECIS = 0;
        public const int CLIP_EMBEDDED = 128;
        public const int CLIP_LH_ANGLES = 16;
        public const int CLIP_MASK = 0xF;
        public const int CLIP_STROKE_PRECIS = 2;
        public const int CLIP_TO_PATH = 4097;
        public const int CLIP_TT_ALWAYS = 32;
        public const int CLOSECHANNEL = 4112;
        public const int CLR_INVALID = 0xFFFF;
        public const int COLORONCOLOR = 3;
        public const int COLORRES = 108;
        public const int COLOR_ADJ_MAX = 100;
        public const int COLOR_ADJ_MIN = -100;
        public const int COMPLEXREGION = 3;
        public const int CP_NONE = 0;
        public const int CP_RECTANGLE = 1;
        public const int CP_REGION = 2;
        public const int CURVECAPS = 28;
        public const int DCBA_FACEDOWNCENTER = 0x101;
        public const int DCBA_FACEDOWNLEFT = 0x102;
        public const int DCBA_FACEDOWNNONE = 0x100;
        public const int DCBA_FACEDOWNRIGHT = 0x103;
        public const int DCBA_FACEUPCENTER = 0x1;
        public const int DCBA_FACEUPLEFT = 0x2;
        public const int DCBA_FACEUPNONE = 0x0;
        public const int DCBA_FACEUPRIGHT = 0x3;
        public const int DCB_ACCUMULATE = 0x2;
        public const int DCB_DIRTY = DCB_ACCUMULATE;
        public const int DCB_DISABLE = 0x8;
        public const int DCB_ENABLE = 0x4;
        public const int DCB_RESET = 0x1;
        public const int DCB_SET = (DCB_RESET | DCB_ACCUMULATE);
        public const int DCTT_BITMAP = 0x1;
        public const int DCTT_DOWNLOAD = 0x2;
        public const int DCTT_DOWNLOAD_OUTLINE = 0x8;
        public const int DCTT_SUBDEV = 0x4;
        public const int DC_BINADJUST = 19;
        public const int DC_BINNAMES = 12;
        public const int DC_BINS = 6;
        public const int DC_COLLATE = 22;
        public const int DC_COPIES = 18;
        public const int DC_DATATYPE_PRODUCED = 21;
        public const int DC_DRIVER = 11;
        public const int DC_DUPLEX = 7;
        public const int DC_EMF_COMPLIANT = 20;
        public const int DC_ENUMRESOLUTIONS = 13;
        public const int DC_EXTRA = 9;
        public const int DC_FIELDS = 1;
        public const int DC_FILEDEPENDENCIES = 14;
        public const int DC_MAXEXTENT = 5;
        public const int DC_MINEXTENT = 4;
        public const int DC_ORIENTATION = 17;
        public const int DC_PAPERNAMES = 16;
        public const int DC_PAPERS = 2;
        public const int DC_PAPERSIZE = 3;
        public const int DC_SIZE = 8;
        public const int DC_TRUETYPE = 15;
        public const int DC_VERSION = 10;
        public const int DEFAULT_CHARSET = 1;
        public const int DEFAULT_GUI_FONT = 17;
        public const int DEFAULT_PALETTE = 15;
        public const int DEFAULT_PITCH = 0;
        public const int DEFAULT_QUALITY = 0;
        public const int DEVICEDATA = 19;
        public const int DEVICE_DEFAULT_FONT = 14;
        public const int DEVICE_FONTTYPE = 0x2;
        public const int DIB_PAL_COLORS = 1;
        public const int DIB_PAL_INDICES = 2;
        public const int DIB_PAL_LOGINDICES = 4;
        public const int DIB_PAL_PHYSINDICES = 2;
        public const int DIB_RGB_COLORS = 0;
        public const int DKGRAY_BRUSH = 3;
        public const int DMBIN_AUTO = 7;
        public const int DMBIN_CASSETTE = 14;
        public const int DMBIN_ENVELOPE = 5;
        public const int DMBIN_ENVMANUAL = 6;
        public const int DMBIN_FIRST = DMBIN_UPPER;
        public const int DMBIN_LARGECAPACITY = 11;
        public const int DMBIN_LARGEFMT = 10;
        public const int DMBIN_LAST = DMBIN_CASSETTE;
        public const int DMBIN_LOWER = 2;
        public const int DMBIN_MANUAL = 4;
        public const int DMBIN_MIDDLE = 3;
        public const int DMBIN_ONLYONE = 1;
        public const int DMBIN_SMALLFMT = 9;
        public const int DMBIN_TRACTOR = 8;
        public const int DMBIN_UPPER = 1;
        public const int DMBIN_USER = 256;
        public const int DMCOLLATE_FALSE = 0;
        public const int DMCOLLATE_TRUE = 1;
        public const int DMCOLOR_COLOR = 2;
        public const int DMCOLOR_MONOCHROME = 1;
        public const int DMDITHER_COARSE = 2;
        public const int DMDITHER_FINE = 3;
        public const int DMDITHER_GRAYSCALE = 5;
        public const int DMDITHER_LINEART = 4;
        public const int DMDITHER_NONE = 1;
        public const int DMDITHER_USER = 256;
        public const int DMDUP_HORIZONTAL = 3;
        public const int DMDUP_SIMPLEX = 1;
        public const int DMDUP_VERTICAL = 2;
        public const int DMICMMETHOD_DEVICE = 4;
        public const int DMICMMETHOD_DRIVER = 3;
        public const int DMICMMETHOD_NONE = 1;
        public const int DMICMMETHOD_SYSTEM = 2;
        public const int DMICMMETHOD_USER = 256;
        public const int DMICM_COLORMETRIC = 3;
        public const int DMICM_CONTRAST = 2;
        public const int DMICM_SATURATE = 1;
        public const int DMICM_USER = 256;
        public const int DMMEDIA_GLOSSY = 2;
        public const int DMMEDIA_STANDARD = 1;
        public const int DMMEDIA_TRANSPARENCY = 3;
        public const int DMMEDIA_USER = 256;
        public const int DMORIENT_LANDSCAPE = 2;
        public const int DMORIENT_PORTRAIT = 1;
        public const int DMPAPER_10X11 = 45;
        public const int DMPAPER_10X14 = 16;
        public const int DMPAPER_11X17 = 17;
        public const int DMPAPER_15X11 = 46;
        public const int DMPAPER_9X11 = 44;
        public const int DMPAPER_A2 = 66;
        public const int DMPAPER_A3 = 8;
        public const int DMPAPER_A3_EXTRA = 63;
        public const int DMPAPER_A3_EXTRA_TRANSVERSE = 68;
        public const int DMPAPER_A3_TRANSVERSE = 67;
        public const int DMPAPER_A4 = 9;
        public const int DMPAPER_A4SMALL = 10;
        public const int DMPAPER_A4_EXTRA = 53;
        public const int DMPAPER_A4_PLUS = 60;
        public const int DMPAPER_A4_TRANSVERSE = 55;
        public const int DMPAPER_A5 = 11;
        public const int DMPAPER_A5_EXTRA = 64;
        public const int DMPAPER_A5_TRANSVERSE = 61;
        public const int DMPAPER_A_PLUS = 57;
        public const int DMPAPER_B4 = 12;
        public const int DMPAPER_B5 = 13;
        public const int DMPAPER_B5_EXTRA = 65;
        public const int DMPAPER_B5_TRANSVERSE = 62;
        public const int DMPAPER_B_PLUS = 58;
        public const int DMPAPER_CSHEET = 24;
        public const int DMPAPER_DSHEET = 25;
        public const int DMPAPER_ENV_10 = 20;
        public const int DMPAPER_ENV_11 = 21;
        public const int DMPAPER_ENV_12 = 22;
        public const int DMPAPER_ENV_14 = 23;
        public const int DMPAPER_ENV_9 = 19;
        public const int DMPAPER_ENV_B4 = 33;
        public const int DMPAPER_ENV_B5 = 34;
        public const int DMPAPER_ENV_B6 = 35;
        public const int DMPAPER_ENV_C3 = 29;
        public const int DMPAPER_ENV_C4 = 30;
        public const int DMPAPER_ENV_C5 = 28;
        public const int DMPAPER_ENV_C6 = 31;
        public const int DMPAPER_ENV_C65 = 32;
        public const int DMPAPER_ENV_DL = 27;
        public const int DMPAPER_ENV_INVITE = 47;
        public const int DMPAPER_ENV_ITALY = 36;
        public const int DMPAPER_ENV_MONARCH = 37;
        public const int DMPAPER_ENV_PERSONAL = 38;
        public const int DMPAPER_ESHEET = 26;
        public const int DMPAPER_EXECUTIVE = 7;
        public const int DMPAPER_FANFOLD_LGL_GERMAN = 41;
        public const int DMPAPER_FANFOLD_STD_GERMAN = 40;
        public const int DMPAPER_FANFOLD_US = 39;
        public const int DMPAPER_FIRST = DMPAPER_LETTER;
        public const int DMPAPER_FOLIO = 14;
        public const int DMPAPER_ISO_B4 = 42;
        public const int DMPAPER_JAPANESE_POSTCARD = 43;
        public const int DMPAPER_LAST = DMPAPER_FANFOLD_LGL_GERMAN;
        public const int DMPAPER_LEDGER = 4;
        public const int DMPAPER_LEGAL = 5;
        public const int DMPAPER_LEGAL_EXTRA = 51;
        public const int DMPAPER_LETTER = 1;
        public const int DMPAPER_LETTERSMALL = 2;
        public const int DMPAPER_LETTER_EXTRA = 50;
        public const int DMPAPER_LETTER_EXTRA_TRANSVERSE = 56;
        public const int DMPAPER_LETTER_PLUS = 59;
        public const int DMPAPER_LETTER_TRANSVERSE = 54;
        public const int DMPAPER_NOTE = 18;
        public const int DMPAPER_QUARTO = 15;
        public const int DMPAPER_RESERVED_48 = 48;
        public const int DMPAPER_RESERVED_49 = 49;
        public const int DMPAPER_STATEMENT = 6;
        public const int DMPAPER_TABLOID = 3;
        public const int DMPAPER_TABLOID_EXTRA = 52;
        public const int DMPAPER_USER = 256;
        public const int DMRES_DRAFT = (-1);
        public const int DMRES_HIGH = (-4);
        public const int DMRES_LOW = (-2);
        public const int DMRES_MEDIUM = (-3);
        public const int DMTT_BITMAP = 1;
        public const int DMTT_DOWNLOAD = 2;
        public const int DMTT_DOWNLOAD_OUTLINE = 4;
        public const int DMTT_SUBDEV = 3;
        public const int DM_COLLATE = 0x8000;
        public const int DM_COLOR = 0x800;
        public const int DM_COPIES = 0x100;
        public const int DM_COPY = 2;
        public const int DM_DEFAULTSOURCE = 0x200;
        public const int DM_DITHERTYPE = 0x10000000;
        public const int DM_DUPLEX = 0x1000;
        public const int DM_FORMNAME = 0x10000;
        public const int DM_GRAYSCALE = 0x1;
        public const int DM_ICMINTENT = 0x4000000;
        public const int DM_ICMMETHOD = 0x2000000;
        public const int DM_INTERLACED = 0x2;
        public const int DM_IN_BUFFER = DM_MODIFY;
        public const int DM_IN_PROMPT = DM_PROMPT;
        public const int DM_MEDIATYPE = 0x8000000;
        public const int DM_MODIFY = 8;
        public const int DM_ORIENTATION = 0x1;
        public const int DM_OUT_BUFFER = DM_COPY;
        public const int DM_OUT_DEFAULT = DM_UPDATE;
        public const int DM_PAPERLENGTH = 0x4;
        public const int DM_PAPERSIZE = 0x2;
        public const int DM_PAPERWIDTH = 0x8;
        public const int DM_PRINTQUALITY = 0x400;
        public const int DM_PROMPT = 4;
        public const int DM_RESERVED1 = 0x800000;
        public const int DM_RESERVED2 = 0x1000000;
        public const int DM_SCALE = 0x10;
        public const int DM_SPECVERSION = 0x320;
        public const int DM_TTOPTION = 0x4000;
        public const int DM_UPDATE = 1;
        public const int DM_YRESOLUTION = 0x2000;
        public const int DOWNLOADFACE = 514;
        public const int DOWNLOADHEADER = 4111;
        public const int DRAFTMODE = 7;
        public const int DRAFT_QUALITY = 1;
        public const int DRAWPATTERNRECT = 25;
        public const int DRIVERVERSION = 0;
        public const int DSTINVERT = 0x550009;
        public const int DT_CHARSTREAM = 4;
        public const int DT_DISPFILE = 6;
        public const int DT_METAFILE = 5;
        public const int DT_PLOTTER = 0;
        public const int DT_RASCAMERA = 3;
        public const int DT_RASDISPLAY = 1;
        public const int DT_RASPRINTER = 2;
        public const int EASTEUROPE_CHARSET = 238;
        public const int ELF_CULTURE_LATIN = 0;
        public const int ELF_VENDOR_SIZE = 4;
        public const int ELF_VERSION = 0;
        public const int ENABLEDUPLEX = 28;
        public const int ENABLEPAIRKERNING = 769;
        public const int ENABLERELATIVEWIDTHS = 768;
        public const int ENCAPSULATED_POSTSCRIPT = 4116;
        public const int END_PATH = 4098;
        public const int ENHMETA_SIGNATURE = 0x464D4520;
        public const int ENHMETA_STOCK_OBJECT = unchecked((int)0x80000000);
        public const int ENUMPAPERBINS = 31;
        public const int ENUMPAPERMETRICS = 34;
        public const int EPSPRINTING = 33;
        public const int ERROR = 0;
        public const int ETO_CLIPPED = 4;
        public const int ETO_GRAYED = 1;
        public const int ETO_OPAQUE = 2;
        public const int EXT_DEVICE_CAPS = 4099;
        public const int EndDocC = 11;
        public const int ExtTextOutC = 512;
        public const int FF_DECORATIVE = 80;
        public const int FF_DONTCARE = 0;
        public const int FF_MODERN = 48;
        public const int FF_ROMAN = 16;
        public const int FF_SCRIPT = 64;
        public const int FF_SWISS = 32;
        public const int FIXED_PITCH = 1;
        public const int FLI_GLYPHS = 0x40000;
        public const int FLI_MASK = 0x103B;
        public const int FLOODFILLBORDER = 0;
        public const int FLOODFILLSURFACE = 1;
        public const int FLUSHOUTPUT = 6;
        public const int FONTMAPPER_MAX = 10;
        public const int FS_ARABIC = 0x40;
        public const int FS_BALTIC = 0x80;
        public const int FS_CHINESESIMP = 0x40000;
        public const int FS_CHINESETRAD = 0x100000;
        public const int FS_CYRILLIC = 0x4;
        public const int FS_GREEK = 0x8;
        public const int FS_HEBREW = 0x20;
        public const int FS_JISJAPAN = 0x20000;
        public const int FS_JOHAB = 0x200000;
        public const int FS_LATIN1 = 0x1;
        public const int FS_LATIN2 = 0x2;
        public const int FS_SYMBOL = unchecked((int)0x80000000);
        public const int FS_THAI = 0x10000;
        public const int FS_TURKISH = 0x10;
        public const int FS_WANSUNG = 0x80000;
        public const int FW_BLACK = FW_HEAVY;
        public const int FW_BOLD = 700;
        public const int FW_DEMIBOLD = FW_SEMIBOLD;
        public const int FW_DONTCARE = 0;
        public const int FW_EXTRABOLD = 800;
        public const int FW_EXTRALIGHT = 200;
        public const int FW_HEAVY = 900;
        public const int FW_LIGHT = 300;
        public const int FW_MEDIUM = 500;
        public const int FW_NORMAL = 400;
        public const int FW_REGULAR = FW_NORMAL;
        public const int FW_SEMIBOLD = 600;
        public const int FW_THIN = 100;
        public const int FW_ULTRABOLD = FW_EXTRABOLD;
        public const int FW_ULTRALIGHT = FW_EXTRALIGHT;
        public const int GCPCLASS_ARABIC = 2;
        public const int GCPCLASS_HEBREW = 2;
        public const int GCPCLASS_LATIN = 1;
        public const int GCPCLASS_LATINNUMBER = 5;
        public const int GCPCLASS_LATINNUMERICSEPARATOR = 7;
        public const int GCPCLASS_LATINNUMERICTERMINATOR = 6;
        public const int GCPCLASS_LOCALNUMBER = 4;
        public const int GCPCLASS_NEUTRAL = 3;
        public const int GCPCLASS_NUMERICSEPARATOR = 8;
        public const int GCPCLASS_PREBOUNDLTR = 0x40;
        public const int GCPCLASS_PREBOUNDRTL = 0x80;
        public const int GCP_CLASSIN = 0x80000;
        public const int GCP_DBCS = 0x1;
        public const int GCP_DIACRITIC = 0x100;
        public const int GCP_DISPLAYZWG = 0x400000;
        public const int GCP_ERROR = 0x8000;
        public const int GCP_GLYPHSHAPE = 0x10;
        public const int GCP_JUSTIFY = 0x10000;
        public const int GCP_JUSTIFYIN = 0x200000;
        public const int GCP_KASHIDA = 0x400;
        public const int GCP_LIGATE = 0x20;
        public const int GCP_MAXEXTENT = 0x100000;
        public const int GCP_NEUTRALOVERRIDE = 0x2000000;
        public const int GCP_NODIACRITICS = 0x20000;
        public const int GCP_NUMERICOVERRIDE = 0x1000000;
        public const int GCP_NUMERICSLATIN = 0x4000000;
        public const int GCP_NUMERICSLOCAL = 0x8000000;
        public const int GCP_REORDER = 0x2;
        public const int GCP_SYMSWAPOFF = 0x800000;
        public const int GCP_USEKERNING = 0x8;
        //jtpublic const int GDI_ERROR = 0xFFFF;
        public const uint GDI_ERROR = 0xFFFFFFFF;
        public const int GETCOLORTABLE = 5;
        public const int GETDEVICEUNITS = 42;
        public const int GETEXTENDEDTEXTMETRICS = 256;
        public const int GETEXTENTTABLE = 257;
        public const int GETFACENAME = 513;
        public const int GETPAIRKERNTABLE = 258;
        public const int GETPENWIDTH = 16;
        public const int GETPHYSPAGESIZE = 12;
        public const int GETPRINTINGOFFSET = 13;
        public const int GETSCALINGFACTOR = 14;
        public const int GETSETPAPERBINS = 29;
        public const int GETSETPAPERMETRICS = 35;
        public const int GETSETPRINTORIENT = 30;
        public const int GETSETSCREENPARAMS = 3072;
        public const int GETTECHNOLGY = 20;
        public const int GETTECHNOLOGY = 20;
        public const int GETTRACKKERNTABLE = 259;
        public const int GETVECTORBRUSHSIZE = 27;
        public const int GETVECTORPENSIZE = 26;
        public const int GGO_BITMAP = 1;
        public const int GGO_GLYPH_INDEX = 0x80;
        public const int GGO_GRAY2_BITMAP = 4;
        public const int GGO_GRAY4_BITMAP = 5;
        public const int GGO_GRAY8_BITMAP = 6;
        public const int GGO_METRICS = 0;
        public const int GGO_NATIVE = 2;
        public const int GM_ADVANCED = 2;
        public const int GM_COMPATIBLE = 1;
        public const int GM_LAST = 2;
        public const int GRAY_BRUSH = 2;
        public const int GREEK_CHARSET = 161;
        public const int HALFTONE = 4;
        public const int HANGEUL_CHARSET = 129;
        public const int HEBREW_CHARSET = 177;
        public const int HGDI_ERROR = 0xFFFF;
        public const int HOLLOW_BRUSH = NULL_BRUSH;
        public const int HORZRES = 8;
        public const int HORZSIZE = 4;
        public const int HS_API_MAX = 25;
        public const int HS_BDIAGONAL = 3;
        public const int HS_BDIAGONAL1 = 7;
        public const int HS_CROSS = 4;
        public const int HS_DENSE1 = 9;
        public const int HS_DENSE2 = 10;
        public const int HS_DENSE3 = 11;
        public const int HS_DENSE4 = 12;
        public const int HS_DENSE5 = 13;
        public const int HS_DENSE6 = 14;
        public const int HS_DENSE7 = 15;
        public const int HS_DENSE8 = 16;
        public const int HS_DIAGCROSS = 5;
        public const int HS_DITHEREDBKCLR = 24;
        public const int HS_DITHEREDCLR = 20;
        public const int HS_DITHEREDTEXTCLR = 22;
        public const int HS_FDIAGONAL = 2;
        public const int HS_FDIAGONAL1 = 6;
        public const int HS_HALFTONE = 18;
        public const int HS_HORIZONTAL = 0;
        public const int HS_NOSHADE = 17;
        public const int HS_SOLID = 8;
        public const int HS_SOLIDBKCLR = 23;
        public const int HS_SOLIDCLR = 19;
        public const int HS_SOLIDTEXTCLR = 21;
        public const int HS_VERTICAL = 1;
        public const int ICM_OFF = 1;
        public const int ICM_ON = 2;
        public const int ICM_QUERY = 3;
        public const int ILLUMINANT_A = 1;
        public const int ILLUMINANT_B = 2;
        public const int ILLUMINANT_C = 3;
        public const int ILLUMINANT_D50 = 4;
        public const int ILLUMINANT_D55 = 5;
        public const int ILLUMINANT_D65 = 6;
        public const int ILLUMINANT_D75 = 7;
        public const int ILLUMINANT_DAYLIGHT = ILLUMINANT_C;
        public const int ILLUMINANT_DEVICE_DEFAULT = 0;
        public const int ILLUMINANT_F2 = 8;
        public const int ILLUMINANT_FLUORESCENT = ILLUMINANT_F2;
        public const int ILLUMINANT_MAX_INDEX = ILLUMINANT_F2;
        public const int ILLUMINANT_NTSC = ILLUMINANT_C;
        public const int ILLUMINANT_TUNGSTEN = ILLUMINANT_A;
        public const int JOHAB_CHARSET = 130;
        public const int LC_INTERIORS = 128;
        public const int LC_MARKER = 4;
        public const int LC_NONE = 0;
        public const int LC_POLYLINE = 2;
        public const int LC_POLYMARKER = 8;
        public const int LC_STYLED = 32;
        public const int LC_WIDE = 16;
        public const int LC_WIDESTYLED = 64;
        public const int LF_FACESIZE = 32;
        public const int LF_FULLFACESIZE = 64;
        public const int LINECAPS = 30;
        public const int LOGPIXELSX = 88;
        public const int LOGPIXELSY = 90;
        public const int LTGRAY_BRUSH = 1;
        public const int MAC_CHARSET = 77;
        public const int MAXSTRETCHBLTMODE = 4;
        public const int MERGECOPY = 0xC000CA;
        public const int MERGEPAINT = 0xBB0226;
        public const int META_ANIMATEPALETTE = 0x436;
        public const int META_ARC = 0x817;
        public const int META_BITBLT = 0x922;
        public const int META_CHORD = 0x830;
        public const int META_CREATEBRUSHINDIRECT = 0x2FC;
        public const int META_CREATEFONTINDIRECT = 0x2FB;
        public const int META_CREATEPALETTE = 0xF7;
        public const int META_CREATEPATTERNBRUSH = 0x1F9;
        public const int META_CREATEPENINDIRECT = 0x2FA;
        public const int META_CREATEREGION = 0x6FF;
        public const int META_DELETEOBJECT = 0x1F0;
        public const int META_DIBBITBLT = 0x940;
        public const int META_DIBCREATEPATTERNBRUSH = 0x142;
        public const int META_DIBSTRETCHBLT = 0xB41;
        public const int META_ELLIPSE = 0x418;
        public const int META_ESCAPE = 0x626;
        public const int META_EXCLUDECLIPRECT = 0x415;
        public const int META_EXTFLOODFILL = 0x548;
        public const int META_EXTTEXTOUT = 0xA32;
        public const int META_FILLREGION = 0x228;
        public const int META_FLOODFILL = 0x419;
        public const int META_FRAMEREGION = 0x429;
        public const int META_INTERSECTCLIPRECT = 0x416;
        public const int META_INVERTREGION = 0x12A;
        public const int META_LINETO = 0x213;
        public const int META_MOVETO = 0x214;
        public const int META_OFFSETCLIPRGN = 0x220;
        public const int META_OFFSETVIEWPORTORG = 0x211;
        public const int META_OFFSETWINDOWORG = 0x20F;
        public const int META_PAINTREGION = 0x12B;
        public const int META_PATBLT = 0x61D;
        public const int META_PIE = 0x81A;
        public const int META_POLYGON = 0x324;
        public const int META_POLYLINE = 0x325;
        public const int META_POLYPOLYGON = 0x538;
        public const int META_REALIZEPALETTE = 0x35;
        public const int META_RECTANGLE = 0x41B;
        public const int META_RESIZEPALETTE = 0x139;
        public const int META_RESTOREDC = 0x127;
        public const int META_ROUNDRECT = 0x61C;
        public const int META_SAVEDC = 0x1E;
        public const int META_SCALEVIEWPORTEXT = 0x412;
        public const int META_SCALEWINDOWEXT = 0x410;
        public const int META_SELECTCLIPREGION = 0x12C;
        public const int META_SELECTOBJECT = 0x12D;
        public const int META_SELECTPALETTE = 0x234;
        public const int META_SETBKCOLOR = 0x201;
        public const int META_SETBKMODE = 0x102;
        public const int META_SETDIBTODEV = 0xD33;
        public const int META_SETMAPMODE = 0x103;
        public const int META_SETMAPPERFLAGS = 0x231;
        public const int META_SETPALENTRIES = 0x37;
        public const int META_SETPIXEL = 0x41F;
        public const int META_SETPOLYFILLMODE = 0x106;
        public const int META_SETRELABS = 0x105;
        public const int META_SETROP2 = 0x104;
        public const int META_SETSTRETCHBLTMODE = 0x107;
        public const int META_SETTEXTALIGN = 0x12E;
        public const int META_SETTEXTCHAREXTRA = 0x108;
        public const int META_SETTEXTCOLOR = 0x209;
        public const int META_SETTEXTJUSTIFICATION = 0x20A;
        public const int META_SETVIEWPORTEXT = 0x20E;
        public const int META_SETVIEWPORTORG = 0x20D;
        public const int META_SETWINDOWEXT = 0x20C;
        public const int META_SETWINDOWORG = 0x20B;
        public const int META_STRETCHBLT = 0xB23;
        public const int META_STRETCHDIB = 0xF43;
        public const int META_TEXTOUT = 0x521;
        public const int MFCOMMENT = 15;
        public const int MM_ANISOTROPIC = 8;
        public const int MM_HIENGLISH = 5;
        public const int MM_HIMETRIC = 3;
        public const int MM_ISOTROPIC = 7;
        public const int MM_LOENGLISH = 4;
        public const int MM_LOMETRIC = 2;
        public const int MM_MAX = MM_ANISOTROPIC;
        public const int MM_MAX_FIXEDSCALE = MM_TWIPS;
        public const int MM_MIN = MM_TEXT;
        public const int MM_TEXT = 1;
        public const int MM_TWIPS = 6;
        public const int MONO_FONT = 8;
        public const int MOUSETRAILS = 39;
        public const int MWT_IDENTITY = 1;
        public const int MWT_LEFTMULTIPLY = 2;
        public const int MWT_MAX = MWT_RIGHTMULTIPLY;
        public const int MWT_MIN = MWT_IDENTITY;
        public const int MWT_RIGHTMULTIPLY = 3;
        public const int NEWFRAME = 1;
        public const int NEXTBAND = 3;
        public const int NOTSRCCOPY = 0x330008;
        public const int NOTSRCERASE = 0x1100A6;
        public const int NTM_BOLD = 0x20;
        public const int NTM_ITALIC = 0x1;
        public const int NTM_REGULAR = 0x40;
        public const int NULLREGION = 1;
        public const int NULL_BRUSH = 5;
        public const int NULL_PEN = 8;
        public const int NUMBRUSHES = 16;
        public const int NUMCOLORS = 24;
        public const int NUMFONTS = 22;
        public const int NUMMARKERS = 20;
        public const int NUMPENS = 18;
        public const int NUMRESERVED = 106;
        public const int OBJ_BITMAP = 7;
        public const int OBJ_BRUSH = 2;
        public const int OBJ_DC = 3;
        public const int OBJ_ENHMETADC = 12;
        public const int OBJ_ENHMETAFILE = 13;
        public const int OBJ_EXTPEN = 11;
        public const int OBJ_FONT = 6;
        public const int OBJ_MEMDC = 10;
        public const int OBJ_METADC = 4;
        public const int OBJ_METAFILE = 9;
        public const int OBJ_PAL = 5;
        public const int OBJ_PEN = 1;
        public const int OBJ_REGION = 8;
        public const int OEM_CHARSET = 255;
        public const int OEM_FIXED_FONT = 10;
        public const int OPAQUE = 2;
        public const int OPENCHANNEL = 4110;
        public const int OUT_CHARACTER_PRECIS = 2;
        public const int OUT_DEFAULT_PRECIS = 0;
        public const int OUT_DEVICE_PRECIS = 5;
        public const int OUT_OUTLINE_PRECIS = 8;
        public const int OUT_RASTER_PRECIS = 6;
        public const int OUT_STRING_PRECIS = 1;
        public const int OUT_STROKE_PRECIS = 3;
        public const int OUT_TT_ONLY_PRECIS = 7;
        public const int OUT_TT_PRECIS = 4;
        public const int PANOSE_COUNT = 10;
        public const int PAN_ANY = 0;
        public const int PAN_ARMSTYLE_INDEX = 6;
        public const int PAN_BENT_ARMS_DOUBLE_SERIF = 11;
        public const int PAN_BENT_ARMS_HORZ = 7;
        public const int PAN_BENT_ARMS_SINGLE_SERIF = 10;
        public const int PAN_BENT_ARMS_VERT = 9;
        public const int PAN_BENT_ARMS_WEDGE = 8;
        public const int PAN_CONTRAST_HIGH = 8;
        public const int PAN_CONTRAST_INDEX = 4;
        public const int PAN_CONTRAST_LOW = 4;
        public const int PAN_CONTRAST_MEDIUM = 6;
        public const int PAN_CONTRAST_MEDIUM_HIGH = 7;
        public const int PAN_CONTRAST_MEDIUM_LOW = 5;
        public const int PAN_CONTRAST_NONE = 2;
        public const int PAN_CONTRAST_VERY_HIGH = 9;
        public const int PAN_CONTRAST_VERY_LOW = 3;
        public const int PAN_CULTURE_LATIN = 0;
        public const int PAN_FAMILYTYPE_INDEX = 0;
        public const int PAN_FAMILY_DECORATIVE = 4;
        public const int PAN_FAMILY_PICTORIAL = 5;
        public const int PAN_FAMILY_SCRIPT = 3;
        public const int PAN_FAMILY_TEXT_DISPLAY = 2;
        public const int PAN_LETTERFORM_INDEX = 7;
        public const int PAN_LETT_NORMAL_BOXED = 4;
        public const int PAN_LETT_NORMAL_CONTACT = 2;
        public const int PAN_LETT_NORMAL_FLATTENED = 5;
        public const int PAN_LETT_NORMAL_OFF_CENTER = 7;
        public const int PAN_LETT_NORMAL_ROUNDED = 6;
        public const int PAN_LETT_NORMAL_SQUARE = 8;
        public const int PAN_LETT_NORMAL_WEIGHTED = 3;
        public const int PAN_LETT_OBLIQUE_BOXED = 11;
        public const int PAN_LETT_OBLIQUE_CONTACT = 9;
        public const int PAN_LETT_OBLIQUE_FLATTENED = 12;
        public const int PAN_LETT_OBLIQUE_OFF_CENTER = 14;
        public const int PAN_LETT_OBLIQUE_ROUNDED = 13;
        public const int PAN_LETT_OBLIQUE_SQUARE = 15;
        public const int PAN_LETT_OBLIQUE_WEIGHTED = 10;
        public const int PAN_MIDLINE_CONSTANT_POINTED = 9;
        public const int PAN_MIDLINE_CONSTANT_SERIFED = 10;
        public const int PAN_MIDLINE_CONSTANT_TRIMMED = 8;
        public const int PAN_MIDLINE_HIGH_POINTED = 6;
        public const int PAN_MIDLINE_HIGH_SERIFED = 7;
        public const int PAN_MIDLINE_HIGH_TRIMMED = 5;
        public const int PAN_MIDLINE_INDEX = 8;
        public const int PAN_MIDLINE_LOW_POINTED = 12;
        public const int PAN_MIDLINE_LOW_SERIFED = 13;
        public const int PAN_MIDLINE_LOW_TRIMMED = 11;
        public const int PAN_MIDLINE_STANDARD_POINTED = 3;
        public const int PAN_MIDLINE_STANDARD_SERIFED = 4;
        public const int PAN_MIDLINE_STANDARD_TRIMMED = 2;
        public const int PAN_NO_FIT = 1;
        public const int PAN_PROPORTION_INDEX = 3;
        public const int PAN_PROP_CONDENSED = 6;
        public const int PAN_PROP_EVEN_WIDTH = 4;
        public const int PAN_PROP_EXPANDED = 5;
        public const int PAN_PROP_MODERN = 3;
        public const int PAN_PROP_MONOSPACED = 9;
        public const int PAN_PROP_OLD_STYLE = 2;
        public const int PAN_PROP_VERY_CONDENSED = 8;
        public const int PAN_PROP_VERY_EXPANDED = 7;
        public const int PAN_SERIFSTYLE_INDEX = 1;
        public const int PAN_SERIF_BONE = 8;
        public const int PAN_SERIF_COVE = 2;
        public const int PAN_SERIF_EXAGGERATED = 9;
        public const int PAN_SERIF_FLARED = 14;
        public const int PAN_SERIF_NORMAL_SANS = 11;
        public const int PAN_SERIF_OBTUSE_COVE = 3;
        public const int PAN_SERIF_OBTUSE_SANS = 12;
        public const int PAN_SERIF_OBTUSE_SQUARE_COVE = 5;
        public const int PAN_SERIF_PERP_SANS = 13;
        public const int PAN_SERIF_ROUNDED = 15;
        public const int PAN_SERIF_SQUARE = 6;
        public const int PAN_SERIF_SQUARE_COVE = 4;
        public const int PAN_SERIF_THIN = 7;
        public const int PAN_SERIF_TRIANGLE = 10;
        public const int PAN_STRAIGHT_ARMS_DOUBLE_SERIF = 6;
        public const int PAN_STRAIGHT_ARMS_HORZ = 2;
        public const int PAN_STRAIGHT_ARMS_SINGLE_SERIF = 5;
        public const int PAN_STRAIGHT_ARMS_VERT = 4;
        public const int PAN_STRAIGHT_ARMS_WEDGE = 3;
        public const int PAN_STROKEVARIATION_INDEX = 5;
        public const int PAN_STROKE_GRADUAL_DIAG = 2;
        public const int PAN_STROKE_GRADUAL_HORZ = 5;
        public const int PAN_STROKE_GRADUAL_TRAN = 3;
        public const int PAN_STROKE_GRADUAL_VERT = 4;
        public const int PAN_STROKE_INSTANT_VERT = 8;
        public const int PAN_STROKE_RAPID_HORZ = 7;
        public const int PAN_STROKE_RAPID_VERT = 6;
        public const int PAN_WEIGHT_BLACK = 10;
        public const int PAN_WEIGHT_BOLD = 8;
        public const int PAN_WEIGHT_BOOK = 5;
        public const int PAN_WEIGHT_DEMI = 7;
        public const int PAN_WEIGHT_HEAVY = 9;
        public const int PAN_WEIGHT_INDEX = 2;
        public const int PAN_WEIGHT_LIGHT = 3;
        public const int PAN_WEIGHT_MEDIUM = 6;
        public const int PAN_WEIGHT_NORD = 11;
        public const int PAN_WEIGHT_THIN = 4;
        public const int PAN_WEIGHT_VERY_LIGHT = 2;
        public const int PAN_XHEIGHT_CONSTANT_LARGE = 4;
        public const int PAN_XHEIGHT_CONSTANT_SMALL = 2;
        public const int PAN_XHEIGHT_CONSTANT_STD = 3;
        public const int PAN_XHEIGHT_DUCKING_LARGE = 7;
        public const int PAN_XHEIGHT_DUCKING_SMALL = 5;
        public const int PAN_XHEIGHT_DUCKING_STD = 6;
        public const int PAN_XHEIGHT_INDEX = 9;
        public const int PASSTHROUGH = 19;
        public const int PATCOPY = 0xF00021;
        public const int PATINVERT = 0x5A0049;
        public const int PATPAINT = 0xFB0A09;
        public const int PC_EXPLICIT = 0x2;
        public const int PC_INTERIORS = 128;
        public const int PC_NOCOLLAPSE = 0x4;
        public const int PC_NONE = 0;
        public const int PC_POLYGON = 1;
        public const int PC_RECTANGLE = 2;
        public const int PC_RESERVED = 0x1;
        public const int PC_SCANLINE = 8;
        public const int PC_STYLED = 32;
        public const int PC_TRAPEZOID = 4;
        public const int PC_WIDE = 16;
        public const int PC_WIDESTYLED = 64;
        public const int PC_WINDPOLYGON = 4;
        public const int PDEVICESIZE = 26;
        public const int PHYSICALHEIGHT = 111;
        public const int PHYSICALOFFSETX = 112;
        public const int PHYSICALOFFSETY = 113;
        public const int PHYSICALWIDTH = 110;
        public const int PLANES = 14;
        public const int POLYFILL_LAST = 2;
        public const int POLYGONALCAPS = 32;
        public const int POSTSCRIPT_DATA = 37;
        public const int POSTSCRIPT_IGNORE = 38;
        public const int POSTSCRIPT_PASSTHROUGH = 4115;
        public const int PROOF_QUALITY = 2;
        public const int PR_JOBSTATUS = 0x0;
        public const int PS_ALTERNATE = 8;
        public const int PS_COSMETIC = 0x0;
        public const int PS_DASH = 1;
        public const int PS_DASHDOT = 3;
        public const int PS_DASHDOTDOT = 4;
        public const int PS_DOT = 2;
        public const int PS_ENDCAP_FLAT = 0x200;
        public const int PS_ENDCAP_MASK = 0xF00;
        public const int PS_ENDCAP_ROUND = 0x0;
        public const int PS_ENDCAP_SQUARE = 0x100;
        public const int PS_GEOMETRIC = 0x10000;
        public const int PS_INSIDEFRAME = 6;
        public const int PS_JOIN_BEVEL = 0x1000;
        public const int PS_JOIN_MASK = 0xF000;
        public const int PS_JOIN_MITER = 0x2000;
        public const int PS_JOIN_ROUND = 0x0;
        public const int PS_NULL = 5;
        public const int PS_SOLID = 0;
        public const int PS_STYLE_MASK = 0xF;
        public const int PS_TYPE_MASK = 0xF0000;
        public const int PS_USERSTYLE = 7;
        public const int PT_BEZIERTO = 0x4;
        public const int PT_CLOSEFIGURE = 0x1;
        public const int PT_LINETO = 0x2;
        public const int PT_MOVETO = 0x6;
        public const int QUERYESCSUPPORT = 8;
        public const int R2_BLACK = 1;
        public const int R2_COPYPEN = 13;
        public const int R2_LAST = 16;
        public const int R2_MASKNOTPEN = 3;
        public const int R2_MASKPEN = 9;
        public const int R2_MASKPENNOT = 5;
        public const int R2_MERGENOTPEN = 12;
        public const int R2_MERGEPEN = 15;
        public const int R2_MERGEPENNOT = 14;
        public const int R2_NOP = 11;
        public const int R2_NOT = 6;
        public const int R2_NOTCOPYPEN = 4;
        public const int R2_NOTMASKPEN = 8;
        public const int R2_NOTMERGEPEN = 2;
        public const int R2_NOTXORPEN = 10;
        public const int R2_WHITE = 16;
        public const int R2_XORPEN = 7;
        public const int RASTERCAPS = 38;
        public const int RASTER_FONTTYPE = 0x1;
        public const int RC_BANDING = 2;
        public const int RC_BIGFONT = 0x400;
        public const int RC_BITBLT = 1;
        public const int RC_BITMAP64 = 8;
        public const int RC_DEVBITS = 0x8000;
        public const int RC_DIBTODEV = 0x200;
        public const int RC_DI_BITMAP = 0x80;
        public const int RC_FLOODFILL = 0x1000;
        public const int RC_GDI20_OUTPUT = 0x10;
        public const int RC_GDI20_STATE = 0x20;
        public const int RC_NONE = 0;
        public const int RC_OP_DX_OUTPUT = 0x4000;
        public const int RC_PALETTE = 0x100;
        public const int RC_SAVEBITMAP = 0x40;
        public const int RC_SCALING = 4;
        public const int RC_STRETCHBLT = 0x800;
        public const int RC_STRETCHDIB = 0x2000;
        public const int RDH_RECTANGLES = 1;
        public const int REFERENCE_BLACK_MAX = 4000;
        public const int REFERENCE_BLACK_MIN = 0;
        public const int REFERENCE_WHITE_MAX = 10000;
        public const int REFERENCE_WHITE_MIN = 6000;
        public const int RELATIVE = 2;
        public const int RESTORE_CTM = 4100;
        public const int RGB_GAMMA_MAX = 65000;
        public const int RGB_GAMMA_MIN = 2500;
        public const int RGN_AND = 1;
        public const int RGN_COPY = 5;
        public const int RGN_DIFF = 4;
        public const int RGN_MAX = RGN_COPY;
        public const int RGN_MIN = RGN_AND;
        public const int RGN_OR = 2;
        public const int RGN_XOR = 3;
        public const int RUSSIAN_CHARSET = 204;
        public const int SAVE_CTM = 4101;
        public const int SCALINGFACTORX = 114;
        public const int SCALINGFACTORY = 115;
        public const int SELECTPAPERSOURCE = 18;
        public const int SETABORTPROC = 9;
        public const int SETALLJUSTVALUES = 771;
        public const int SETCHARSET = 772;
        public const int SETCOLORTABLE = 4;
        public const int SETCOPYCOUNT = 17;
        public const int SETDIBSCALING = 32;
        public const int SETKERNTRACK = 770;
        public const int SETLINECAP = 21;
        public const int SETLINEJOIN = 22;
        public const int SET_ARC_DIRECTION = 4102;
        public const int SET_BACKGROUND_COLOR = 4103;
        public const int SET_BOUNDS = 4109;
        public const int SET_CLIP_BOX = 4108;
        public const int SET_MIRROR_MODE = 4110;
        public const int SET_POLY_MODE = 4104;
        public const int SET_SCREEN_ANGLE = 4105;
        public const int SET_SPREAD = 4106;
        public const int SHIFTJIS_CHARSET = 128;
        public const int SIMPLEREGION = 2;
        public const int SIZEPALETTE = 104;
        public const int SP_APPABORT = (-2);
        public const int SP_ERROR = (-1);
        public const int SP_NOTREPORTED = 0x4000;
        public const int SP_OUTOFDISK = (-4);
        public const int SP_OUTOFMEMORY = (-5);
        public const int SP_USERABORT = (-3);
        public const int SRCAND = 0x8800C6;
        public const int SRCCOPY = 0xCC0020;
        public const int SRCERASE = 0x440328;
        public const int SRCINVERT = 0x660046;
        public const int SRCPAINT = 0xEE0086;
        public const int STOCK_LAST = 16;
        public const int STRETCH_ANDSCANS = 1;
        public const int STRETCH_DELETESCANS = 3;
        public const int STRETCH_HALFTONE = 4;
        public const int STRETCH_ORSCANS = 2;
        public const int SYMBOL_CHARSET = 2;
        public const int SYSPAL_ERROR = 0;
        public const int SYSPAL_NOSTATIC = 2;
        public const int SYSPAL_STATIC = 1;
        public const int SYSTEM_FIXED_FONT = 16;
        public const int SYSTEM_FONT = 13;
        public const int SetMiterLimitC = 23;
        public const int StartDocC = 10;
        public const int StretchBltC = 2048;
        public const int TA_BASELINE = 24;
        public const int TA_BOTTOM = 8;
        public const int TA_CENTER = 6;
        public const int TA_LEFT = 0;
        public const int TA_MASK = (TA_BASELINE + TA_CENTER + TA_UPDATECP);
        public const int TA_NOUPDATECP = 0;
        public const int TA_RIGHT = 2;
        public const int TA_TOP = 0;
        public const int TA_UPDATECP = 1;
        public const int TCI_SRCCHARSET = 1;
        public const int TCI_SRCCODEPAGE = 2;
        public const int TCI_SRCFONTSIG = 3;
        public const int TC_CP_STROKE = 0x4;
        public const int TC_CR_90 = 0x8;
        public const int TC_CR_ANY = 0x10;
        public const int TC_EA_DOUBLE = 0x200;
        public const int TC_IA_ABLE = 0x400;
        public const int TC_OP_CHARACTER = 0x1;
        public const int TC_OP_STROKE = 0x2;
        public const int TC_RA_ABLE = 0x2000;
        public const int TC_RESERVED = 0x8000;
        public const int TC_SA_CONTIN = 0x100;
        public const int TC_SA_DOUBLE = 0x40;
        public const int TC_SA_INTEGER = 0x80;
        public const int TC_SCROLLBLT = 0x10000;
        public const int TC_SF_X_YINDEP = 0x20;
        public const int TC_SO_ABLE = 0x1000;
        public const int TC_UA_ABLE = 0x800;
        public const int TC_VA_ABLE = 0x4000;
        public const int TECHNOLOGY = 2;
        public const int TEXTCAPS = 34;
        public const int THAI_CHARSET = 222;
        public const int TMPF_DEVICE = 0x8;
        public const int TMPF_FIXED_PITCH = 0x1;
        public const int TMPF_TRUETYPE = 0x4;
        public const int TMPF_VECTOR = 0x2;
        public const int TRANSFORM_CTM = 4107;
        public const int TRANSPARENT = 1;
        public const int TRUETYPE_FONTTYPE = 0x4;
        public const int TT_AVAILABLE = 0x1;
        public const int TT_ENABLED = 0x2;
        public const int TT_POLYGON_TYPE = 24;
        public const int TT_PRIM_LINE = 1;
        public const int TT_PRIM_QSPLINE = 2;
        public const int TURKISH_CHARSET = 162;
        public const int VARIABLE_PITCH = 2;
        public const int VERTRES = 10;
        public const int VERTSIZE = 6;
        public const int VTA_BASELINE = TA_BASELINE;
        public const int VTA_BOTTOM = TA_RIGHT;
        public const int VTA_CENTER = TA_CENTER;
        public const int VTA_LEFT = TA_BOTTOM;
        public const int VTA_RIGHT = TA_TOP;
        public const int VTA_TOP = TA_LEFT;
        public const int WHITENESS = 0xFF0062;
        public const int WHITEONBLACK = 2;
        public const int WHITE_BRUSH = 0;
        public const int WHITE_PEN = 6;
        public const int WINDING = 2;
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}