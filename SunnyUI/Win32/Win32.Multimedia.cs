using System;
using System.Runtime.InteropServices;
using System.Text;
using HANDLE = System.IntPtr;
using HDC = System.IntPtr;
using HWND = System.IntPtr;

namespace Sunny.UI.Win32
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public struct SMPTE
    {
        public byte hour;
        public byte min;
        public byte sec;
        public byte frame;
        public byte fps;
        public byte dummy;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] public byte pad;
    }
    public struct MIDI
    {
        public int songptrpos;
    }
    public struct MMTIME
    {
        public int wType;
        public int u;
    }
    public struct MIDIEVENT
    {
        public int dwDeltaTime;
        public int dwStreamID;
        public int dwEvent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)] public int dwParms;
    }
    public struct MIDISTRMBUFFVER
    {
        public int dwVersion;
        public int dwMid;
        public int dwOEMVersion;
    }
    public struct MIDIPROPTIMEDIV
    {
        public int cbStruct;
        public int dwTimeDiv;
    }
    public struct MIDIPROPTEMPO
    {
        public int cbStruct;
        public int dwTempo;
    }
    public struct MIXERCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public int fdwSupport;
        public int cDestinations;
    }
    public struct TARGET
    {
        public int dwType;
        public int dwDeviceID;
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
    }
    public struct MIXERLINE
    {
        public int cbStruct;
        public int dwDestination;
        public int dwSource;
        public int dwLineID;
        public int fdwLine;
        public int dwUser;
        public int dwComponentType;
        public int cChannels;
        public int cConnections;
        public int cControls;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MIXER_SHORT_NAME_CHARS)]
        public string szShortName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MIXER_LONG_NAME_CHARS)]
        public string szName;
        public TARGET tTarget;
    }
    public struct MIXERCONTROL
    {
        public int cbStruct;
        public int dwControlID;
        public int dwControlType;
        public int fdwControl;
        public int cMultipleItems;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MIXER_SHORT_NAME_CHARS)] public string szShortName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MIXER_LONG_NAME_CHARS)] public string szName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public int[] Bounds;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] public int[] Metrics;
    }
    public struct MIXERLINECONTROLS
    {
        public int cbStruct;
        public int dwLineID;
        public int dwControl;
        public int cControls;
        public int cbmxctrl;
        public MIXERCONTROL pamxctrl;
    }
    public struct MIXERCONTROLDETAILS
    {
        public int cbStruct;
        public int dwControlID;
        public int cChannels;
        public int item;
        public int cbDetails;
        public int paDetails;
    }
    public struct MIXERCONTROLDETAILS_LISTTEXT
    {
        public int dwParam1;
        public int dwParam2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MIXER_LONG_NAME_CHARS)] public string szName;
    }
    public struct MIXERCONTROLDETAILS_BOOLEAN
    {
        public int fValue;
    }
    public struct MIXERCONTROLDETAILS_SIGNED
    {
        public int lValue;
    }
    public struct MIXERCONTROLDETAILS_UNSIGNED
    {
        public int dwValue;
    }
    public struct JOYINFOEX
    {
        public int dwSize;
        public int dwFlags;
        public int dwXpos;
        public int dwYpos;
        public int dwZpos;
        public int dwRpos;
        public int dwUpos;
        public int dwVpos;
        public int dwButtons;
        public int dwButtonNumber;
        public int dwPOV;
        public int dwReserved1;
        public int dwReserved2;
    }
    public struct DRVCONFIGINFO
    {
        public int dwDCISize;
        public string lpszDCISectionName;
        public string lpszDCIAliasName;
        public int dnDevNode;
    }
    public struct WAVEHDR
    {
        public string lpData;
        public int dwBufferLength;
        public int dwBytesRecorded;
        public int dwUser;
        public int dwFlags;
        public int dwLoops;
        public int lpNext;
        public int Reserved;
    }
    public struct WAVEOUTCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public int dwFormats;
        public short wChannels;
        public int dwSupport;
    }
    public struct WAVEINCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public int dwFormats;
        public short wChannels;
    }
    public struct WAVEFORMAT
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
    }
    public struct PCMWAVEFORMAT
    {
        public WAVEFORMAT wf;
        public short wBitsPerSample;
    }
    public struct MIDIOUTCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public short wTechnology;
        public short wVoices;
        public short wNotes;
        public short wChannelMask;
        public int dwSupport;
    }
    public struct MIDIINCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        public string szPname;
    }
    public struct MIDIHDR
    {
        public string lpData;
        public int dwBufferLength;
        public int dwBytesRecorded;
        public int dwUser;
        public int dwFlags;
        public int lpNext;
        public int Reserved;
    }
    public struct AUXCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public short wTechnology;
        public int dwSupport;
    }
    public struct TIMECAPS
    {
        public int wPeriodMin;
        public int wPeriodMax;
    }
    public struct JOYCAPS
    {
        public short wMid;
        public short wPid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)] public string szPname;
        public short wXmin;
        public short wXmax;
        public short wYmin;
        public short wYmax;
        public short wZmin;
        public short wZmax;
        public short wNumButtons;
        public short wPeriodMin;
        public short wPeriodMax;
    }
    public struct JOYINFO
    {
        public int wXpos;
        public int wYpos;
        public int wZpos;
        public int wButtons;
    }
    public struct MMIOINFO
    {
        public int dwFlags;
        public int fccIOProc;
        public int pIOProc;
        public int wErrorRet;
        public HANDLE htask;
        public int cchBuffer;
        public string pchBuffer;
        public string pchNext;
        public string pchEndRead;
        public string pchEndWrite;
        public int lBufOffset;
        public int lDiskOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public int adwInfo;
        public int dwReserved1;
        public int dwReserved2;
        public HANDLE hmmio;
    }
    public struct MMCKINFO
    {
        public int ckid;
        public int ckSize;
        public int fccType;
        public int dwDataOffset;
        public int dwFlags;
    }
    public struct MCI_GENERIC_PARMS
    {
        public int dwCallback;
    }
    public struct MCI_OPEN_PARMS
    {
        public int dwCallback;
        public int wDeviceID;
        public string lpstrDeviceType;
        public string lpstrElementName;
        public string lpstrAlias;
    }
    public struct MCI_PLAY_PARMS
    {
        public int dwCallback;
        public int dwFrom;
        public int dwTo;
    }
    public struct MCI_SEEK_PARMS
    {
        public int dwCallback;
        public int dwTo;
    }
    public struct MCI_STATUS_PARMS
    {
        public int dwCallback;
        public int dwReturn;
        public int dwItem;
        public short dwTrack;
    }
    public struct MCI_INFO_PARMS
    {
        public int dwCallback;
        public string lpstrReturn;
        public int dwRetSize;
    }
    public struct MCI_GETDEVCAPS_PARMS
    {
        public int dwCallback;
        public int dwReturn;
        public int dwIten;
    }
    public struct MCI_SYSINFO_PARMS
    {
        public int dwCallback;
        public string lpstrReturn;
        public int dwRetSize;
        public int dwNumber;
        public int wDeviceType;
    }
    public struct MCI_SET_PARMS
    {
        public int dwCallback;
        public int dwTimeFormat;
        public int dwAudio;
    }
    public struct MCI_BREAK_PARMS
    {
        public int dwCallback;
        public int nVirtKey;
        public HWND hwndBreak;
    }
    public struct MCI_SOUND_PARMS
    {
        public int dwCallback;
        public string lpstrSoundName;
    }
    public struct MCI_SAVE_PARMS
    {
        public int dwCallback;
        public string lpFileName;
    }
    public struct MCI_LOAD_PARMS
    {
        public int dwCallback;
        public string lpFileName;
    }
    public struct MCI_RECORD_PARMS
    {
        public int dwCallback;
        public int dwFrom;
        public int dwTo;
    }
    public struct MCI_VD_PLAY_PARMS
    {
        public int dwCallback;
        public int dwFrom;
        public int dwTo;
        public int dwSpeed;
    }
    public struct MCI_VD_STEP_PARMS
    {
        public int dwCallback;
        public int dwFrames;
    }
    public struct MCI_VD_ESCAPE_PARMS
    {
        public int dwCallback;
        public string lpstrCommand;
    }
    public struct MCI_WAVE_OPEN_PARMS
    {
        public int dwCallback;
        public int wDeviceID;
        public string lpstrDeviceType;
        public string lpstrElementName;
        public string lpstrAlias;
        public int dwBufferSeconds;
    }
    public struct MCI_WAVE_DELETE_PARMS
    {
        public int dwCallback;
        public int dwFrom;
        public int dwTo;
    }
    public struct MCI_WAVE_SET_PARMS
    {
        public int dwCallback;
        public int dwTimeFormat;
        public int dwAudio;
        public int wInput;
        public int wOutput;
        public short wFormatTag;
        public short wReserved2;
        public short nChannels;
        public short wReserved3;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wReserved4;
        public short wBitsPerSample;
        public short wReserved5;
    }
    public struct MCI_SEQ_SET_PARMS
    {
        public int dwCallback;
        public int dwTimeFormat;
        public int dwAudio;
        public int dwTempo;
        public int dwPort;
        public int dwSlave;
        public int dwMaster;
        public int dwOffset;
    }
    public struct MCI_ANIM_OPEN_PARMS
    {
        public int dwCallback;
        public int wDeviceID;
        public string lpstrDeviceType;
        public string lpstrElementName;
        public string lpstrAlias;
        public int dwStyle;
        public HWND hwndParent;
    }
    public struct MCI_ANIM_PLAY_PARMS
    {
        public int dwCallback;
        public int dwFrom;
        public int dwTo;
        public int dwSpeed;
    }
    public struct MCI_ANIM_STEP_PARMS
    {
        public int dwCallback;
        public int dwFrames;
    }
    public struct MCI_ANIM_WINDOW_PARMS
    {
        public int dwCallback;
        public HWND hwnd;
        public int nCmdShow;
        public string lpstrText;
    }
    public struct MCI_ANIM_RECT_PARMS
    {
        public int dwCallback;
        public RECT rc;
    }
    public struct MCI_ANIM_UPDATE_PARMS
    {
        public int dwCallback;
        public RECT rc;
        public HDC hdc;
    }
    public struct MCI_OVLY_OPEN_PARMS
    {
        public int dwCallback;
        public int wDeviceID;
        public string lpstrDeviceType;
        public string lpstrElementName;
        public string lpstrAlias;
        public int dwStyle;
        public HWND hwndParent;
    }
    public struct MCI_OVLY_WINDOW_PARMS
    {
        public int dwCallback;
        public HWND hwnd;
        public int nCmdShow;
        public string lpstrText;
    }
    public struct MCI_OVLY_RECT_PARMS
    {
        public int dwCallback;
        public RECT rc;
    }
    public struct MCI_OVLY_SAVE_PARMS
    {
        public int dwCallback;
        public string lpFileName;
        public RECT rc;
    }
    public struct MCI_OVLY_LOAD_PARMS
    {
        public int dwCallback;
        public string lpFileName;
        public RECT rc;
    }

    public abstract class LZ
    {
        [DllImport("lz32")] public static extern int CopyLZFile(int n1, int n2);
        [DllImport("lz32")] public static extern int LZStart();
        [DllImport("lz32")] public static extern void LZDone();
        [DllImport("lz32")] public static extern int GetExpandedName(string lpszSource, StringBuilder lpszBuffer);
        [DllImport("lz32")] public static extern int LZCopy(HANDLE hfSource, HANDLE hfDest);
        [DllImport("lz32")] public static extern int LZInit(HANDLE hfSrc);
        [DllImport("lz32")] public static extern int LZOpenFile(string lpszFile, ref OFSTRUCT lpOf, int style);
        [DllImport("lz32")] public static extern int LZRead(HANDLE hfFile, string lpvBuf, int cbread);
        [DllImport("lz32")] public static extern int LZSeek(HANDLE hfFile, int lOffset, int nOrigin);
        [DllImport("lz32")] public static extern void LZClose(HANDLE hfFile);
    }
    public partial class WinMM
    {
        [DllImport("winmm")] public static extern int mciGetYieldProc(int mciId, ref int pdwYieldData);
        [DllImport("winmm")] public static extern int mciSetYieldProc(int mciId, int fpYieldProc, int dwYieldData);
        [DllImport("winmm")] public static extern int mmioInstallIOProcA(int fccIOProc, ref int pIOProc, int dwFlags);
        [DllImport("winmm")] public static extern short midiOutGetNumDevs();
        [DllImport("winmm")] public static extern int CloseDriver(HANDLE hDriver, int lParam1, int lParam2);
        [DllImport("winmm")] public static extern int DefDriverProc(int dwDriverIdentifier, HANDLE hdrvr, int uMsg, int lParam1, int lParam2);
        [DllImport("winmm")] public static extern int DrvGetModuleHandle(HANDLE hDriver);
        [DllImport("winmm")] public static extern int GetDriverModuleHandle(HANDLE hDriver);
        [DllImport("winmm")] public static extern int OpenDriver(string szDriverName, string szSectionName, int lParam2);
        [DllImport("winmm")] public static extern int PlaySound(string lpszName, HANDLE hModule, int dwFlags);
        [DllImport("winmm")] public static extern int SendDriverMessage(HANDLE hDriver, int message, int lParam1, int lParam2);
        [DllImport("winmm")] public static extern int auxGetDevCaps(int uDeviceID, ref AUXCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int auxGetNumDevs();
        [DllImport("winmm")] public static extern int auxGetVolume(int uDeviceID, ref int lpdwVolume);
        [DllImport("winmm")] public static extern int auxOutMessage(int uDeviceID, int msg, int dw1, int dw2);
        [DllImport("winmm")] public static extern int auxSetVolume(int uDeviceID, int dwVolume);
        [DllImport("winmm")] public static extern int joyGetDevCaps(int id, ref JOYCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int joyGetNumDevs();
        [DllImport("winmm")] public static extern int joyGetPos(int uJoyID, ref JOYINFO pji);
        [DllImport("winmm")] public static extern int joyGetPosEx(int uJoyID, ref JOYINFOEX pji);
        [DllImport("winmm")] public static extern int joyGetThreshold(int id, ref int lpuThreshold);
        [DllImport("winmm")] public static extern int joyReleaseCapture(int id);
        [DllImport("winmm")] public static extern int joySetCapture(HWND hwnd, int uID, int uPeriod, int bChanged);
        [DllImport("winmm")] public static extern int joySetThreshold(int id, int uThreshold);
        [DllImport("winmm")] public static extern int mciExecute(string lpstrCommand);
        [DllImport("winmm")] public static extern int mciGetCreatorTask(int wDeviceID);
        [DllImport("winmm")] public static extern int mciGetDeviceID(string lpstrName);
        [DllImport("winmm")] public static extern int mciGetDeviceIDFromElementID(int dwElementID, string lpstrType);
        [DllImport("winmm")] public static extern int mciGetErrorString(int dwError, string lpstrBuffer, int uLength);
        [DllImport("winmm")] public static extern int mciSendCommand(int wDeviceID, int uMessage, int dwParam1, IntPtr dwParam2);
        [DllImport("winmm")] public static extern int mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, HWND hwndCallback);
        [DllImport("winmm")] public static extern int midiConnect(HANDLE hmi, HANDLE hmo, IntPtr pReserved);
        [DllImport("winmm")] public static extern int midiDisconnect(HANDLE hmi, HANDLE hmo, IntPtr pReserved);
        [DllImport("winmm")] public static extern int midiInAddBuffer(HANDLE hMidiIn, ref MIDIHDR lpMidiInHdr, int uSize);
        [DllImport("winmm")] public static extern int midiInClose(HANDLE hMidiIn);
        [DllImport("winmm")] public static extern int midiInGetDevCaps(int uDeviceID, ref MIDIINCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int midiInGetErrorText(int err, string lpText, int uSize);
        [DllImport("winmm")] public static extern int midiInGetID(HANDLE hMidiIn, ref int lpuDeviceID);
        [DllImport("winmm")] public static extern int midiInGetNumDevs();
        [DllImport("winmm")] public static extern int midiInMessage(HANDLE hMidiIn, int msg, int dw1, int dw2);
        [DllImport("winmm")] public static extern int midiInOpen(int lphMidiIn, int uDeviceID, int dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm")] public static extern int midiInPrepareHeader(HANDLE hMidiIn, ref MIDIHDR lpMidiInHdr, int uSize);
        [DllImport("winmm")] public static extern int midiInReset(HANDLE hMidiIn);
        [DllImport("winmm")] public static extern int midiInStart(HANDLE hMidiIn);
        [DllImport("winmm")] public static extern int midiInStop(HANDLE hMidiIn);
        [DllImport("winmm")] public static extern int midiInUnprepareHeader(HANDLE hMidiIn, ref MIDIHDR lpMidiInHdr, int uSize);
        [DllImport("winmm")] public static extern int midiOutCacheDrumPatches(HANDLE hMidiOut, int uPatch, ref int lpKeyArray, int uFlags);
        [DllImport("winmm")] public static extern int midiOutCachePatches(HANDLE hMidiOut, int uBank, ref int lpPatchArray, int uFlags);
        [DllImport("winmm")] public static extern int midiOutClose(HANDLE hMidiOut);
        [DllImport("winmm")] public static extern int midiOutGetDevCaps(int uDeviceID, ref MIDIOUTCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int midiOutGetErrorText(int err, StringBuilder lpText, int uSize);
        [DllImport("winmm")] public static extern int midiOutGetID(HANDLE hMidiOut, ref int lpuDeviceID);
        [DllImport("winmm")] public static extern int midiOutGetVolume(int uDeviceID, ref int lpdwVolume);
        [DllImport("winmm")] public static extern int midiOutLongMsg(HANDLE hMidiOut, ref MIDIHDR lpMidiOutHdr, int uSize);
        [DllImport("winmm")] public static extern int midiOutMessage(HANDLE hMidiOut, int msg, int dw1, int dw2);
        [DllImport("winmm")] public static extern int midiOutOpen(int lphMidiOut, int uDeviceID, int dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm")] public static extern int midiOutPrepareHeader(HANDLE hMidiOut, ref MIDIHDR lpMidiOutHdr, int uSize);
        [DllImport("winmm")] public static extern int midiOutReset(HANDLE hMidiOut);
        [DllImport("winmm")] public static extern int midiOutSetVolume(int uDeviceID, int dwVolume);
        [DllImport("winmm")] public static extern int midiOutShortMsg(HANDLE hMidiOut, int dwMsg);
        [DllImport("winmm")] public static extern int midiOutUnprepareHeader(HANDLE hMidiOut, ref MIDIHDR lpMidiOutHdr, int uSize);
        [DllImport("winmm")] public static extern int midiStreamClose(HANDLE hms);
        [DllImport("winmm")] public static extern int midiStreamOpen(int phms, int puDeviceID, int cMidi, int dwCallback, int dwInstance, int fdwOpen);
        [DllImport("winmm")] public static extern int midiStreamOut(HANDLE hms, ref MIDIHDR pmh, int cbmh);
        [DllImport("winmm")] public static extern int midiStreamPause(HANDLE hms);
        [DllImport("winmm")] public static extern int midiStreamPosition(HANDLE hms, ref MMTIME lpmmt, int cbmmt);
        [DllImport("winmm")] public static extern int midiStreamProperty(HANDLE hms, Byte lppropdata, int dwProperty);
        [DllImport("winmm")] public static extern int midiStreamRestart(HANDLE hms);
        [DllImport("winmm")] public static extern int midiStreamStop(HANDLE hms);
        [DllImport("winmm")] public static extern int mixerClose(HANDLE hmx);
        [DllImport("winmm")] public static extern int mixerGetControlDetails(HANDLE hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);
        [DllImport("winmm")] public static extern int mixerGetDevCaps(int uMxId, ref MIXERCAPS pmxcaps, int cbmxcaps);
        [DllImport("winmm")] public static extern int mixerGetID(HANDLE hmxobj, ref int pumxID, int fdwId);
        [DllImport("winmm")] public static extern int mixerGetLineControls(HANDLE hmxobj, ref MIXERLINECONTROLS pmxlc, int fdwControls);
        [DllImport("winmm")] public static extern int mixerGetLineInfo(HANDLE hmxobj, ref MIXERLINE pmxl, int fdwInfo);
        [DllImport("winmm")] public static extern int mixerGetNumDevs();
        [DllImport("winmm")] public static extern int mixerMessage(HANDLE hmx, int uMsg, int dwParam1, int dwParam2);
        [DllImport("winmm")] public static extern int mixerOpen(int phmx, int uMxId, int dwCallback, int dwInstance, int fdwOpen);
        [DllImport("winmm")] public static extern int mixerSetControlDetails(HANDLE hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);
        [DllImport("winmm")] public static extern int mmioAdvance(HANDLE hmmio, ref MMIOINFO lpmmioinfo, int uFlags);
        [DllImport("winmm")] public static extern int mmioAscend(HANDLE hmmio, ref MMCKINFO lpck, int uFlags);
        [DllImport("winmm")] public static extern int mmioClose(HANDLE hmmio, int uFlags);
        [DllImport("winmm")] public static extern int mmioCreateChunk(HANDLE hmmio, ref MMCKINFO lpck, int uFlags);
        [DllImport("winmm")] public static extern int mmioDescend(HANDLE hmmio, ref MMCKINFO lpck, ref MMCKINFO lpckParent, int uFlags);
        [DllImport("winmm")] public static extern int mmioFlush(HANDLE hmmio, int uFlags);
        [DllImport("winmm")] public static extern int mmioGetInfo(HANDLE hmmio, ref MMIOINFO lpmmioinfo, int uFlags);
        [DllImport("winmm")] public static extern int mmioOpen(string szFileName, ref MMIOINFO lpmmioinfo, int dwOpenFlags);
        [DllImport("winmm")] public static extern int mmioRead(HANDLE hmmio, string pch, int cch);
        [DllImport("winmm")] public static extern int mmioRename(string szFileName, string SzNewFileName, ref MMIOINFO lpmmioinfo, int dwRenameFlags);
        [DllImport("winmm")] public static extern int mmioSeek(HANDLE hmmio, int lOffset, int iOrigin);
        [DllImport("winmm")] public static extern int mmioSendMessage(HANDLE hmmio, int uMsg, int lParam1, int lParam2);
        [DllImport("winmm")] public static extern int mmioSetBuffer(HANDLE hmmio, string pchBuffer, int cchBuffer, int uFlags);
        [DllImport("winmm")] public static extern int mmioSetInfo(HANDLE hmmio, ref MMIOINFO lpmmioinfo, int uFlags);
        [DllImport("winmm")] public static extern int mmioStringToFOURCC(string sz, int uFlags);
        [DllImport("winmm")] public static extern int mmioWrite(HANDLE hmmio, string pch, int cch);
        [DllImport("winmm")] public static extern int mmsystemGetVersion();
        [DllImport("winmm")] public static extern int sndPlaySound(string lpszSoundName, int uFlags);
        [DllImport("winmm")] public static extern int timeBeginPeriod(int uPeriod);
        [DllImport("winmm")] public static extern int timeEndPeriod(int uPeriod);
        [DllImport("winmm")] public static extern int timeGetDevCaps(ref TIMECAPS lpTimeCaps, int uSize);
        [DllImport("winmm")] public static extern int timeGetSystemTime(ref MMTIME lpTime, int uSize);
        [DllImport("winmm")] public static extern int timeGetTime();
        [DllImport("winmm")] public static extern int timeKillEvent(int uID);
        [DllImport("winmm")] public static extern int timeSetEvent(int uDelay, int uResolution, ref int lpFunction, int dwUser, int uFlags);
        [DllImport("winmm")] public static extern int waveInAddBuffer(HANDLE hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm")] public static extern int waveInClose(HANDLE hWaveIn);
        [DllImport("winmm")] public static extern int waveInGetDevCaps(int uDeviceID, ref WAVEINCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int waveInGetErrorText(int err, string lpText, int uSize);
        [DllImport("winmm")] public static extern int waveInGetID(HANDLE hWaveIn, ref int lpuDeviceID);
        [DllImport("winmm")] public static extern int waveInGetNumDevs();
        [DllImport("winmm")] public static extern int waveInGetPosition(HANDLE hWaveIn, ref MMTIME lpInfo, int uSize);
        [DllImport("winmm")] public static extern int waveInMessage(HANDLE hWaveIn, int msg, int dw1, int dw2);
        [DllImport("winmm")] public static extern int waveInOpen(int lphWaveIn, int uDeviceID, ref WAVEFORMAT lpFormat, int dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm")] public static extern int waveInPrepareHeader(HANDLE hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm")] public static extern int waveInReset(HANDLE hWaveIn);
        [DllImport("winmm")] public static extern int waveInStart(HANDLE hWaveIn);
        [DllImport("winmm")] public static extern int waveInStop(HANDLE hWaveIn);
        [DllImport("winmm")] public static extern int waveInUnprepareHeader(HANDLE hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm")] public static extern int waveOutBreakLoop(HANDLE hWaveOut);
        [DllImport("winmm")] public static extern int waveOutClose(HANDLE hWaveOut);
        [DllImport("winmm")] public static extern int waveOutGetDevCaps(int uDeviceID, ref WAVEOUTCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int waveOutGetErrorText(int err, string lpText, int uSize);
        [DllImport("winmm")] public static extern int waveOutGetID(HANDLE hWaveOut, ref int lpuDeviceID);
        [DllImport("winmm")] public static extern int waveOutGetNumDevs();
        [DllImport("winmm")] public static extern int waveOutGetPitch(HANDLE hWaveOut, ref int lpdwPitch);
        [DllImport("winmm")] public static extern int waveOutGetPlaybackRate(HANDLE hWaveOut, ref int lpdwRate);
        [DllImport("winmm")] public static extern int waveOutGetPosition(HANDLE hWaveOut, ref MMTIME lpInfo, int uSize);
        [DllImport("winmm")] public static extern int waveOutGetVolume(int uDeviceID, ref int lpdwVolume);
        [DllImport("winmm")] public static extern int waveOutMessage(HANDLE hWaveOut, int msg, int dw1, int dw2);
        [DllImport("winmm")] public static extern int waveOutOpen(int lphWaveOut, int uDeviceID, ref WAVEFORMAT lpFormat, int dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm")] public static extern int waveOutPause(HANDLE hWaveOut);
        [DllImport("winmm")] public static extern int waveOutPrepareHeader(HANDLE hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm")] public static extern int waveOutReset(HANDLE hWaveOut);
        [DllImport("winmm")] public static extern int waveOutRestart(HANDLE hWaveOut);
        [DllImport("winmm")] public static extern int waveOutSetPitch(HANDLE hWaveOut, int dwPitch);
        [DllImport("winmm")] public static extern int waveOutSetPlaybackRate(HANDLE hWaveOut, int dwRate);
        [DllImport("winmm")] public static extern int waveOutSetVolume(int uDeviceID, int dwVolume);
        [DllImport("winmm")] public static extern int waveOutUnprepareHeader(HANDLE hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm")] public static extern int waveOutWrite(HANDLE hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);

        public const int AUXCAPS_AUXIN = 2;
        public const int AUXCAPS_CDAUDIO = 1;
        public const int AUXCAPS_LRVOLUME = 0x2;
        public const int AUXCAPS_VOLUME = 0x1;
        public const int AUX_MAPPER = -1;
        public const int C1_TRANSPARENT = 0x1;
        public const int CALLBACK_FUNCTION = 0x30000;
        public const int CALLBACK_NULL = 0x0;
        public const int CALLBACK_TASK = 0x20000;
        public const int CALLBACK_TYPEMASK = 0x70000;
        public const int CALLBACK_WINDOW = 0x10000;
        public const int CAPS1 = 94;
        public const int DRVCNF_CANCEL = 0x0;
        public const int DRVCNF_OK = 0x1;
        public const int DRVCNF_RESTART = 0x2;
        public const int DRV_CANCEL = DRVCNF_CANCEL;
        public const int DRV_CLOSE = 0x4;
        public const int DRV_CONFIGURE = 0x7;
        public const int DRV_DISABLE = 0x5;
        public const int DRV_ENABLE = 0x2;
        public const int DRV_EXITSESSION = 0xB;
        public const int DRV_FREE = 0x6;
        public const int DRV_INSTALL = 0x9;
        public const int DRV_LOAD = 0x1;
        public const int DRV_MCI_FIRST = DRV_RESERVED;
        public const int DRV_MCI_LAST = DRV_RESERVED + 0xFFF;
        public const int DRV_OK = DRVCNF_OK;
        public const int DRV_OPEN = 0x3;
        public const int DRV_POWER = 0xF;
        public const int DRV_QUERYCONFIGURE = 0x8;
        public const int DRV_REMOVE = 0xA;
        public const int DRV_RESERVED = 0x800;
        public const int DRV_RESTART = DRVCNF_RESTART;
        public const int DRV_USER = 0x4000;
        public const int JOYERR_BASE = 160;
        public const int JOYERR_NOCANDO = (JOYERR_BASE + 6);
        public const int JOYERR_NOERROR = (0);
        public const int JOYERR_PARMS = (JOYERR_BASE + 5);
        public const int JOYERR_UNPLUGGED = (JOYERR_BASE + 7);
        public const int JOYSTICKID1 = 0;
        public const int JOYSTICKID2 = 1;
        public const int JOY_BUTTON1 = 0x1;
        public const int JOY_BUTTON10 = 0x200;
        public const int JOY_BUTTON11 = 0x400;
        public const int JOY_BUTTON12 = 0x800;
        public const int JOY_BUTTON13 = 0x1000;
        public const int JOY_BUTTON14 = 0x2000;
        public const int JOY_BUTTON15 = 0x4000;
        public const int JOY_BUTTON16 = 0x8000;
        public const int JOY_BUTTON17 = 0x10000;
        public const int JOY_BUTTON18 = 0x20000;
        public const int JOY_BUTTON19 = 0x40000;
        public const int JOY_BUTTON1CHG = 0x100;
        public const int JOY_BUTTON2 = 0x2;
        public const int JOY_BUTTON20 = 0x80000;
        public const int JOY_BUTTON21 = 0x100000;
        public const int JOY_BUTTON22 = 0x200000;
        public const int JOY_BUTTON23 = 0x400000;
        public const int JOY_BUTTON24 = 0x800000;
        public const int JOY_BUTTON25 = 0x1000000;
        public const int JOY_BUTTON26 = 0x2000000;
        public const int JOY_BUTTON27 = 0x4000000;
        public const int JOY_BUTTON28 = 0x8000000;
        public const int JOY_BUTTON29 = 0x10000000;
        public const int JOY_BUTTON2CHG = 0x200;
        public const int JOY_BUTTON3 = 0x4;
        public const int JOY_BUTTON30 = 0x20000000;
        public const int JOY_BUTTON31 = 0x40000000;
        public const int JOY_BUTTON32 = unchecked((int)0x80000000);
        public const int JOY_BUTTON3CHG = 0x400;
        public const int JOY_BUTTON4 = 0x8;
        public const int JOY_BUTTON4CHG = 0x800;
        public const int JOY_BUTTON5 = 0x10;
        public const int JOY_BUTTON6 = 0x20;
        public const int JOY_BUTTON7 = 0x40;
        public const int JOY_BUTTON8 = 0x80;
        public const int JOY_BUTTON9 = 0x100;
        public const int JOY_CAL_READ3 = 0x40000;
        public const int JOY_CAL_READ4 = 0x80000;
        public const int JOY_CAL_READ5 = 0x400000;
        public const int JOY_CAL_READ6 = 0x800000;
        public const int JOY_CAL_READALWAYS = 0x10000;
        public const int JOY_CAL_READRONLY = 0x2000000;
        public const int JOY_CAL_READUONLY = 0x4000000;
        public const int JOY_CAL_READVONLY = 0x8000000;
        public const int JOY_CAL_READXONLY = 0x100000;
        public const int JOY_CAL_READXYONLY = 0x20000;
        public const int JOY_CAL_READYONLY = 0x200000;
        public const int JOY_CAL_READZONLY = 0x1000000;
        public const int JOY_POVBACKWARD = 18000;
        public const int JOY_POVCENTERED = -1;
        public const int JOY_POVFORWARD = 0;
        public const int JOY_POVLEFT = 27000;
        public const int JOY_POVRIGHT = 9000;
        public const int JOY_RETURNALL = (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ | JOY_RETURNR | JOY_RETURNU | JOY_RETURNV | JOY_RETURNPOV | JOY_RETURNBUTTONS);
        public const int JOY_RETURNBUTTONS = 0x80;
        public const int JOY_RETURNCENTERED = 0x400;
        public const int JOY_RETURNPOV = 0x40;
        public const int JOY_RETURNPOVCTS = 0x200;
        public const int JOY_RETURNR = 0x8;
        public const int JOY_RETURNRAWDATA = 0x100;
        public const int JOY_RETURNU = 0x10;
        public const int JOY_RETURNV = 0x20;
        public const int JOY_RETURNX = 0x1;
        public const int JOY_RETURNY = 0x2;
        public const int JOY_RETURNZ = 0x4;
        public const int JOY_USEDEADZONE = 0x800;
        public const int LZERROR_BADINHANDLE = (-1);
        public const int LZERROR_BADOUTHANDLE = (-2);
        public const int LZERROR_BADVALUE = (-7);
        public const int LZERROR_GLOBLOCK = (-6);
        public const int LZERROR_PUBLICLOC = (-5);
        public const int LZERROR_READ = (-3);
        public const int LZERROR_UNKNOWNALG = (-8);
        public const int LZERROR_WRITE = (-4);
        public const int MAXERRORLENGTH = 128;
        public const int MAXPNAMELEN = 32;
        public const int MCIERR_BAD_CONSTANT = (MCIERR_BASE + 34);
        public const int MCIERR_BAD_INTEGER = (MCIERR_BASE + 14);
        public const int MCIERR_BAD_TIME_FORMAT = (MCIERR_BASE + 37);
        public const int MCIERR_BASE = 256;
        public const int MCIERR_CANNOT_LOAD_DRIVER = (MCIERR_BASE + 10);
        public const int MCIERR_CANNOT_USE_ALL = (MCIERR_BASE + 23);
        public const int MCIERR_CREATEWINDOW = (MCIERR_BASE + 91);
        public const int MCIERR_CUSTOM_DRIVER_BASE = (MCIERR_BASE + 256);
        public const int MCIERR_DEVICE_LENGTH = (MCIERR_BASE + 54);
        public const int MCIERR_DEVICE_LOCKED = (MCIERR_BASE + 32);
        public const int MCIERR_DEVICE_NOT_INSTALLED = (MCIERR_BASE + 50);
        public const int MCIERR_DEVICE_NOT_READY = (MCIERR_BASE + 20);
        public const int MCIERR_DEVICE_OPEN = (MCIERR_BASE + 9);
        public const int MCIERR_DEVICE_ORD_LENGTH = (MCIERR_BASE + 55);
        public const int MCIERR_DEVICE_TYPE_REQUIRED = (MCIERR_BASE + 31);
        public const int MCIERR_DRIVER = (MCIERR_BASE + 22);
        public const int MCIERR_DRIVER_INTERNAL = (MCIERR_BASE + 16);
        public const int MCIERR_DUPLICATE_ALIAS = (MCIERR_BASE + 33);
        public const int MCIERR_DUPLICATE_FLAGS = (MCIERR_BASE + 39);
        public const int MCIERR_EXTENSION_NOT_FOUND = (MCIERR_BASE + 25);
        public const int MCIERR_EXTRA_CHARACTERS = (MCIERR_BASE + 49);
        public const int MCIERR_FILENAME_REQUIRED = (MCIERR_BASE + 48);
        public const int MCIERR_FILE_NOT_FOUND = (MCIERR_BASE + 19);
        public const int MCIERR_FILE_NOT_SAVED = (MCIERR_BASE + 30);
        public const int MCIERR_FILE_READ = (MCIERR_BASE + 92);
        public const int MCIERR_FILE_WRITE = (MCIERR_BASE + 93);
        public const int MCIERR_FLAGS_NOT_COMPATIBLE = (MCIERR_BASE + 28);
        public const int MCIERR_GET_CD = (MCIERR_BASE + 51);
        public const int MCIERR_HARDWARE = (MCIERR_BASE + 6);
        public const int MCIERR_ILLEGAL_FOR_AUTO_OPEN = (MCIERR_BASE + 47);
        public const int MCIERR_INTERNAL = (MCIERR_BASE + 21);
        public const int MCIERR_INVALID_DEVICE_ID = (MCIERR_BASE + 1);
        public const int MCIERR_INVALID_DEVICE_NAME = (MCIERR_BASE + 7);
        public const int MCIERR_INVALID_FILE = (MCIERR_BASE + 40);
        public const int MCIERR_MISSING_COMMAND_STRING = (MCIERR_BASE + 11);
        public const int MCIERR_MISSING_DEVICE_NAME = (MCIERR_BASE + 36);
        public const int MCIERR_MISSING_PARAMETER = (MCIERR_BASE + 17);
        public const int MCIERR_MISSING_STRING_ARGUMENT = (MCIERR_BASE + 13);
        public const int MCIERR_MULTIPLE = (MCIERR_BASE + 24);
        public const int MCIERR_MUST_USE_SHAREABLE = (MCIERR_BASE + 35);
        public const int MCIERR_NEW_REQUIRES_ALIAS = (MCIERR_BASE + 43);
        public const int MCIERR_NONAPPLICABLE_FUNCTION = (MCIERR_BASE + 46);
        public const int MCIERR_NOTIFY_ON_AUTO_OPEN = (MCIERR_BASE + 44);
        public const int MCIERR_NO_CLOSING_QUOTE = (MCIERR_BASE + 38);
        public const int MCIERR_NO_ELEMENT_ALLOWED = (MCIERR_BASE + 45);
        public const int MCIERR_NO_INTEGER = (MCIERR_BASE + 56);
        public const int MCIERR_NO_WINDOW = (MCIERR_BASE + 90);
        public const int MCIERR_NULL_PARAMETER_BLOCK = (MCIERR_BASE + 41);
        public const int MCIERR_OUTOFRANGE = (MCIERR_BASE + 26);
        public const int MCIERR_OUT_OF_MEMORY = (MCIERR_BASE + 8);
        public const int MCIERR_PARAM_OVERFLOW = (MCIERR_BASE + 12);
        public const int MCIERR_PARSER_INTERNAL = (MCIERR_BASE + 15);
        public const int MCIERR_SEQ_DIV_INCOMPATIBLE = (MCIERR_BASE + 80);
        public const int MCIERR_SEQ_NOMIDIPRESENT = (MCIERR_BASE + 87);
        public const int MCIERR_SEQ_PORTUNSPECIFIED = (MCIERR_BASE + 86);
        public const int MCIERR_SEQ_PORT_INUSE = (MCIERR_BASE + 81);
        public const int MCIERR_SEQ_PORT_MAPNODEVICE = (MCIERR_BASE + 83);
        public const int MCIERR_SEQ_PORT_MISCERROR = (MCIERR_BASE + 84);
        public const int MCIERR_SEQ_PORT_NONEXISTENT = (MCIERR_BASE + 82);
        public const int MCIERR_SEQ_TIMER = (MCIERR_BASE + 85);
        public const int MCIERR_SET_CD = (MCIERR_BASE + 52);
        public const int MCIERR_SET_DRIVE = (MCIERR_BASE + 53);
        public const int MCIERR_UNNAMED_RESOURCE = (MCIERR_BASE + 42);
        public const int MCIERR_UNRECOGNIZED_COMMAND = (MCIERR_BASE + 5);
        public const int MCIERR_UNRECOGNIZED_KEYWORD = (MCIERR_BASE + 3);
        public const int MCIERR_UNSUPPORTED_FUNCTION = (MCIERR_BASE + 18);
        public const int MCIERR_WAVE_INPUTSINUSE = (MCIERR_BASE + 66);
        public const int MCIERR_WAVE_INPUTSUNSUITABLE = (MCIERR_BASE + 72);
        public const int MCIERR_WAVE_INPUTUNSPECIFIED = (MCIERR_BASE + 69);
        public const int MCIERR_WAVE_OUTPUTSINUSE = (MCIERR_BASE + 64);
        public const int MCIERR_WAVE_OUTPUTSUNSUITABLE = (MCIERR_BASE + 70);
        public const int MCIERR_WAVE_OUTPUTUNSPECIFIED = (MCIERR_BASE + 68);
        public const int MCIERR_WAVE_SETINPUTINUSE = (MCIERR_BASE + 67);
        public const int MCIERR_WAVE_SETINPUTUNSUITABLE = (MCIERR_BASE + 73);
        public const int MCIERR_WAVE_SETOUTPUTINUSE = (MCIERR_BASE + 65);
        public const int MCIERR_WAVE_SETOUTPUTUNSUITABLE = (MCIERR_BASE + 71);
        public const int MCI_ALL_DEVICE_ID = -1;
        public const int MCI_ANIM_GETDEVCAPS_CAN_REVERSE = 0x4001;
        public const int MCI_ANIM_GETDEVCAPS_CAN_STRETCH = 0x4007;
        public const int MCI_ANIM_GETDEVCAPS_FAST_RATE = 0x4002;
        public const int MCI_ANIM_GETDEVCAPS_MAX_WINDOWS = 0x4008;
        public const int MCI_ANIM_GETDEVCAPS_NORMAL_RATE = 0x4004;
        public const int MCI_ANIM_GETDEVCAPS_PALETTES = 0x4006;
        public const int MCI_ANIM_GETDEVCAPS_SLOW_RATE = 0x4003;
        public const int MCI_ANIM_INFO_TEXT = 0x10000;
        public const int MCI_ANIM_OPEN_NOSTATIC = 0x40000;
        public const int MCI_ANIM_OPEN_PARENT = 0x20000;
        public const int MCI_ANIM_OPEN_WS = 0x10000;
        public const int MCI_ANIM_PLAY_FAST = 0x40000;
        public const int MCI_ANIM_PLAY_REVERSE = 0x20000;
        public const int MCI_ANIM_PLAY_SCAN = 0x100000;
        public const int MCI_ANIM_PLAY_SLOW = 0x80000;
        public const int MCI_ANIM_PLAY_SPEED = 0x10000;
        public const int MCI_ANIM_PUT_DESTINATION = 0x40000;
        public const int MCI_ANIM_PUT_SOURCE = 0x20000;
        public const int MCI_ANIM_REALIZE_BKGD = 0x20000;
        public const int MCI_ANIM_REALIZE_NORM = 0x10000;
        public const int MCI_ANIM_RECT = 0x10000;
        public const int MCI_ANIM_STATUS_FORWARD = 0x4002;
        public const int MCI_ANIM_STATUS_HPAL = 0x4004;
        public const int MCI_ANIM_STATUS_HWND = 0x4003;
        public const int MCI_ANIM_STATUS_SPEED = 0x4001;
        public const int MCI_ANIM_STATUS_STRETCH = 0x4005;
        public const int MCI_ANIM_STEP_FRAMES = 0x20000;
        public const int MCI_ANIM_STEP_REVERSE = 0x10000;
        public const int MCI_ANIM_UPDATE_HDC = 0x20000;
        public const int MCI_ANIM_WHERE_DESTINATION = 0x40000;
        public const int MCI_ANIM_WHERE_SOURCE = 0x20000;
        public const int MCI_ANIM_WINDOW_DEFAULT = 0x0;
        public const int MCI_ANIM_WINDOW_DISABLE_STRETCH = 0x200000;
        public const int MCI_ANIM_WINDOW_ENABLE_STRETCH = 0x100000;
        public const int MCI_ANIM_WINDOW_HWND = 0x10000;
        public const int MCI_ANIM_WINDOW_STATE = 0x40000;
        public const int MCI_ANIM_WINDOW_TEXT = 0x80000;
        public const int MCI_BREAK = 0x811;
        public const int MCI_BREAK_HWND = 0x200;
        public const int MCI_BREAK_KEY = 0x100;
        public const int MCI_BREAK_OFF = 0x400;
        public const int MCI_CD_OFFSET = 1088;
        public const int MCI_CLOSE = 0x804;
        public const int MCI_COPY = 0x852;
        public const int MCI_CUE = 0x830;
        public const int MCI_CUT = 0x851;
        public const int MCI_DELETE = 0x856;
        public const int MCI_DEVTYPE_ANIMATION = 519;
        public const int MCI_DEVTYPE_CD_AUDIO = 516;
        public const int MCI_DEVTYPE_DAT = 517;
        public const int MCI_DEVTYPE_DIGITAL_VIDEO = 520;
        public const int MCI_DEVTYPE_FIRST = MCI_DEVTYPE_VCR;
        public const int MCI_DEVTYPE_FIRST_USER = 0x1000;
        public const int MCI_DEVTYPE_LAST = MCI_DEVTYPE_SEQUENCER;
        public const int MCI_DEVTYPE_OTHER = 521;
        public const int MCI_DEVTYPE_OVERLAY = 515;
        public const int MCI_DEVTYPE_SCANNER = 518;
        public const int MCI_DEVTYPE_SEQUENCER = 523;
        public const int MCI_DEVTYPE_VCR = 513;
        public const int MCI_DEVTYPE_VIDEODISC = 514;
        public const int MCI_DEVTYPE_WAVEFORM_AUDIO = 522;
        public const int MCI_ESCAPE = 0x805;
        public const int MCI_FIRST = 0x800;
        public const int MCI_FORMAT_BYTES = 8;
        public const int MCI_FORMAT_FRAMES = 3;
        public const int MCI_FORMAT_HMS = 1;
        public const int MCI_FORMAT_MILLISECONDS = 0;
        public const int MCI_FORMAT_MSF = 2;
        public const int MCI_FORMAT_SAMPLES = 9;
        public const int MCI_FORMAT_SMPTE_24 = 4;
        public const int MCI_FORMAT_SMPTE_25 = 5;
        public const int MCI_FORMAT_SMPTE_30 = 6;
        public const int MCI_FORMAT_SMPTE_30DROP = 7;
        public const int MCI_FORMAT_TMSF = 10;
        public const int MCI_FREEZE = 0x844;
        public const int MCI_FROM = 0x4;
        public const int MCI_GETDEVCAPS = 0x80B;
        public const int MCI_GETDEVCAPS_CAN_EJECT = 0x7;
        public const int MCI_GETDEVCAPS_CAN_PLAY = 0x8;
        public const int MCI_GETDEVCAPS_CAN_RECORD = 0x1;
        public const int MCI_GETDEVCAPS_CAN_SAVE = 0x9;
        public const int MCI_GETDEVCAPS_COMPOUND_DEVICE = 0x6;
        public const int MCI_GETDEVCAPS_DEVICE_TYPE = 0x4;
        public const int MCI_GETDEVCAPS_HAS_AUDIO = 0x2;
        public const int MCI_GETDEVCAPS_HAS_VIDEO = 0x3;
        public const int MCI_GETDEVCAPS_ITEM = 0x100;
        public const int MCI_GETDEVCAPS_USES_FILES = 0x5;
        public const int MCI_INFO = 0x80A;
        public const int MCI_INFO_FILE = 0x200;
        public const int MCI_INFO_PRODUCT = 0x100;
        public const int MCI_LAST = 0xFFF;
        public const int MCI_LOAD = 0x850;
        public const int MCI_LOAD_FILE = 0x100;
        public const int MCI_MODE_NOT_READY = (MCI_STRING_OFFSET + 12);
        public const int MCI_MODE_OPEN = (MCI_STRING_OFFSET + 18);
        public const int MCI_MODE_PAUSE = (MCI_STRING_OFFSET + 17);
        public const int MCI_MODE_PLAY = (MCI_STRING_OFFSET + 14);
        public const int MCI_MODE_RECORD = (MCI_STRING_OFFSET + 15);
        public const int MCI_MODE_SEEK = (MCI_STRING_OFFSET + 16);
        public const int MCI_MODE_STOP = (MCI_STRING_OFFSET + 13);
        public const int MCI_NOTIFY = 0x1;
        public const int MCI_NOTIFY_ABORTED = 0x4;
        public const int MCI_NOTIFY_FAILURE = 0x8;
        public const int MCI_NOTIFY_SUCCESSFUL = 0x1;
        public const int MCI_NOTIFY_SUPERSEDED = 0x2;
        public const int MCI_OPEN = 0x803;
        public const int MCI_OPEN_ALIAS = 0x400;
        public const int MCI_OPEN_ELEMENT = 0x200;
        public const int MCI_OPEN_ELEMENT_ID = 0x800;
        public const int MCI_OPEN_SHAREABLE = 0x100;
        public const int MCI_OPEN_TYPE = 0x2000;
        public const int MCI_OPEN_TYPE_ID = 0x1000;
        public const int MCI_OVLY_GETDEVCAPS_CAN_FREEZE = 0x4002;
        public const int MCI_OVLY_GETDEVCAPS_CAN_STRETCH = 0x4001;
        public const int MCI_OVLY_GETDEVCAPS_MAX_WINDOWS = 0x4003;
        public const int MCI_OVLY_INFO_TEXT = 0x10000;
        public const int MCI_OVLY_OPEN_PARENT = 0x20000;
        public const int MCI_OVLY_OPEN_WS = 0x10000;
        public const int MCI_OVLY_PUT_DESTINATION = 0x40000;
        public const int MCI_OVLY_PUT_FRAME = 0x80000;
        public const int MCI_OVLY_PUT_SOURCE = 0x20000;
        public const int MCI_OVLY_PUT_VIDEO = 0x100000;
        public const int MCI_OVLY_RECT = 0x10000;
        public const int MCI_OVLY_STATUS_HWND = 0x4001;
        public const int MCI_OVLY_STATUS_STRETCH = 0x4002;
        public const int MCI_OVLY_WHERE_DESTINATION = 0x40000;
        public const int MCI_OVLY_WHERE_FRAME = 0x80000;
        public const int MCI_OVLY_WHERE_SOURCE = 0x20000;
        public const int MCI_OVLY_WHERE_VIDEO = 0x100000;
        public const int MCI_OVLY_WINDOW_DEFAULT = 0x0;
        public const int MCI_OVLY_WINDOW_DISABLE_STRETCH = 0x200000;
        public const int MCI_OVLY_WINDOW_ENABLE_STRETCH = 0x100000;
        public const int MCI_OVLY_WINDOW_HWND = 0x10000;
        public const int MCI_OVLY_WINDOW_STATE = 0x40000;
        public const int MCI_OVLY_WINDOW_TEXT = 0x80000;
        public const int MCI_PASTE = 0x853;
        public const int MCI_PAUSE = 0x809;
        public const int MCI_PLAY = 0x806;
        public const int MCI_PUT = 0x842;
        public const int MCI_REALIZE = 0x840;
        public const int MCI_RECORD = 0x80F;
        public const int MCI_RECORD_INSERT = 0x100;
        public const int MCI_RECORD_OVERWRITE = 0x200;
        public const int MCI_RESUME = 0x855;
        public const int MCI_SAVE = 0x813;
        public const int MCI_SAVE_FILE = 0x100;
        public const int MCI_SEEK = 0x807;
        public const int MCI_SEEK_TO_END = 0x200;
        public const int MCI_SEEK_TO_START = 0x100;
        public const int MCI_SEQ_DIV_PPQN = (0 + MCI_SEQ_OFFSET);
        public const int MCI_SEQ_DIV_SMPTE_24 = (1 + MCI_SEQ_OFFSET);
        public const int MCI_SEQ_DIV_SMPTE_25 = (2 + MCI_SEQ_OFFSET);
        public const int MCI_SEQ_DIV_SMPTE_30 = (4 + MCI_SEQ_OFFSET);
        public const int MCI_SEQ_DIV_SMPTE_30DROP = (3 + MCI_SEQ_OFFSET);
        public const int MCI_SEQ_FILE = 0x4002;
        public const int MCI_SEQ_FORMAT_SONGPTR = 0x4001;
        public const int MCI_SEQ_MAPPER = 65535;
        public const int MCI_SEQ_MIDI = 0x4003;
        public const int MCI_SEQ_NONE = 65533;
        public const int MCI_SEQ_OFFSET = 1216;
        public const int MCI_SEQ_SET_MASTER = 0x80000;
        public const int MCI_SEQ_SET_OFFSET = 0x1000000;
        public const int MCI_SEQ_SET_PORT = 0x20000;
        public const int MCI_SEQ_SET_SLAVE = 0x40000;
        public const int MCI_SEQ_SET_TEMPO = 0x10000;
        public const int MCI_SEQ_SMPTE = 0x4004;
        public const int MCI_SEQ_STATUS_DIVTYPE = 0x400A;
        public const int MCI_SEQ_STATUS_MASTER = 0x4008;
        public const int MCI_SEQ_STATUS_OFFSET = 0x4009;
        public const int MCI_SEQ_STATUS_PORT = 0x4003;
        public const int MCI_SEQ_STATUS_SLAVE = 0x4007;
        public const int MCI_SEQ_STATUS_TEMPO = 0x4002;
        public const int MCI_SET = 0x80D;
        public const int MCI_SET_AUDIO = 0x800;
        public const int MCI_SET_AUDIO_ALL = 0x4001;
        public const int MCI_SET_AUDIO_LEFT = 0x4002;
        public const int MCI_SET_AUDIO_RIGHT = 0x4003;
        public const int MCI_SET_DOOR_CLOSED = 0x200;
        public const int MCI_SET_DOOR_OPEN = 0x100;
        public const int MCI_SET_OFF = 0x4000;
        public const int MCI_SET_ON = 0x2000;
        public const int MCI_SET_TIME_FORMAT = 0x400;
        public const int MCI_SET_VIDEO = 0x1000;
        public const int MCI_SOUND = 0x812;
        public const int MCI_SOUND_NAME = 0x100;
        public const int MCI_SPIN = 0x80C;
        public const int MCI_STATUS = 0x814;
        public const int MCI_STATUS_CURRENT_TRACK = 0x8;
        public const int MCI_STATUS_ITEM = 0x100;
        public const int MCI_STATUS_LENGTH = 0x1;
        public const int MCI_STATUS_MEDIA_PRESENT = 0x5;
        public const int MCI_STATUS_MODE = 0x4;
        public const int MCI_STATUS_NUMBER_OF_TRACKS = 0x3;
        public const int MCI_STATUS_POSITION = 0x2;
        public const int MCI_STATUS_READY = 0x7;
        public const int MCI_STATUS_START = 0x200;
        public const int MCI_STATUS_TIME_FORMAT = 0x6;
        public const int MCI_STEP = 0x80E;
        public const int MCI_STOP = 0x808;
        public const int MCI_STRING_OFFSET = 512;
        public const int MCI_SYSINFO = 0x810;
        public const int MCI_SYSINFO_INSTALLNAME = 0x800;
        public const int MCI_SYSINFO_NAME = 0x400;
        public const int MCI_SYSINFO_OPEN = 0x200;
        public const int MCI_SYSINFO_QUANTITY = 0x100;
        public const int MCI_TO = 0x8;
        public const int MCI_TRACK = 0x10;
        public const int MCI_UNFREEZE = 0x845;
        public const int MCI_UPDATE = 0x854;
        public const int MCI_USER_MESSAGES = (0x400 + MCI_FIRST);
        public const int MCI_VD_ESCAPE_STRING = 0x100;
        public const int MCI_VD_FORMAT_TRACK = 0x4001;
        public const int MCI_VD_GETDEVCAPS_CAN_REVERSE = 0x4002;
        public const int MCI_VD_GETDEVCAPS_CAV = 0x20000;
        public const int MCI_VD_GETDEVCAPS_CLV = 0x10000;
        public const int MCI_VD_GETDEVCAPS_FAST_RATE = 0x4003;
        public const int MCI_VD_GETDEVCAPS_NORMAL_RATE = 0x4005;
        public const int MCI_VD_GETDEVCAPS_SLOW_RATE = 0x4004;
        public const int MCI_VD_MEDIA_CAV = (MCI_VD_OFFSET + 3);
        public const int MCI_VD_MEDIA_CLV = (MCI_VD_OFFSET + 2);
        public const int MCI_VD_MEDIA_OTHER = (MCI_VD_OFFSET + 4);
        public const int MCI_VD_MODE_PARK = (MCI_VD_OFFSET + 1);
        public const int MCI_VD_OFFSET = 1024;
        public const int MCI_VD_PLAY_FAST = 0x20000;
        public const int MCI_VD_PLAY_REVERSE = 0x10000;
        public const int MCI_VD_PLAY_SCAN = 0x80000;
        public const int MCI_VD_PLAY_SLOW = 0x100000;
        public const int MCI_VD_PLAY_SPEED = 0x40000;
        public const int MCI_VD_SEEK_REVERSE = 0x10000;
        public const int MCI_VD_SPIN_DOWN = 0x20000;
        public const int MCI_VD_SPIN_UP = 0x10000;
        public const int MCI_VD_STATUS_DISC_SIZE = 0x4006;
        public const int MCI_VD_STATUS_FORWARD = 0x4003;
        public const int MCI_VD_STATUS_MEDIA_TYPE = 0x4004;
        public const int MCI_VD_STATUS_SIDE = 0x4005;
        public const int MCI_VD_STATUS_SPEED = 0x4002;
        public const int MCI_VD_STEP_FRAMES = 0x10000;
        public const int MCI_VD_STEP_REVERSE = 0x20000;
        public const int MCI_WAIT = 0x2;
        public const int MCI_WAVE_GETDEVCAPS_INPUTS = 0x4001;
        public const int MCI_WAVE_GETDEVCAPS_OUTPUTS = 0x4002;
        public const int MCI_WAVE_INPUT = 0x400000;
        public const int MCI_WAVE_MAPPER = (MCI_WAVE_OFFSET + 1);
        public const int MCI_WAVE_OFFSET = 1152;
        public const int MCI_WAVE_OPEN_BUFFER = 0x10000;
        public const int MCI_WAVE_OUTPUT = 0x800000;
        public const int MCI_WAVE_PCM = (MCI_WAVE_OFFSET + 0);
        public const int MCI_WAVE_SET_ANYINPUT = 0x4000000;
        public const int MCI_WAVE_SET_ANYOUTPUT = 0x8000000;
        public const int MCI_WAVE_SET_AVGBYTESPERSEC = 0x80000;
        public const int MCI_WAVE_SET_BITSPERSAMPLE = 0x200000;
        public const int MCI_WAVE_SET_BLOCKALIGN = 0x100000;
        public const int MCI_WAVE_SET_CHANNELS = 0x20000;
        public const int MCI_WAVE_SET_FORMATTAG = 0x10000;
        public const int MCI_WAVE_SET_SAMPLESPERSEC = 0x40000;
        public const int MCI_WAVE_STATUS_AVGBYTESPERSEC = 0x4004;
        public const int MCI_WAVE_STATUS_BITSPERSAMPLE = 0x4006;
        public const int MCI_WAVE_STATUS_BLOCKALIGN = 0x4005;
        public const int MCI_WAVE_STATUS_CHANNELS = 0x4002;
        public const int MCI_WAVE_STATUS_FORMATTAG = 0x4001;
        public const int MCI_WAVE_STATUS_LEVEL = 0x4007;
        public const int MCI_WAVE_STATUS_SAMPLESPERSEC = 0x4003;
        public const int MCI_WHERE = 0x843;
        public const int MCI_WINDOW = 0x841;
        public const int MEVT_F_CALLBACK = 0x40000000;
        public const int MEVT_F_LONG = unchecked((int)0x80000000);
        public const int MEVT_F_SHORT = 0x0;
        public const int MHDR_DONE = 0x1;
        public const int MHDR_INQUEUE = 0x4;
        public const int MHDR_PREPARED = 0x2;
        public const int MHDR_VALID = 0x7;
        public const int MIDICAPS_CACHE = 0x4;
        public const int MIDICAPS_LRVOLUME = 0x2;
        public const int MIDICAPS_STREAM = 0x8;
        public const int MIDICAPS_VOLUME = 0x1;
        public const int MIDIERR_BASE = 64;
        public const int MIDIERR_INVALIDSETUP = (MIDIERR_BASE + 5);
        public const int MIDIERR_LASTERROR = (MIDIERR_BASE + 5);
        public const int MIDIERR_NODEVICE = (MIDIERR_BASE + 4);
        public const int MIDIERR_NOMAP = (MIDIERR_BASE + 2);
        public const int MIDIERR_NOTREADY = (MIDIERR_BASE + 3);
        public const int MIDIERR_STILLPLAYING = (MIDIERR_BASE + 1);
        public const int MIDIERR_UNPREPARED = (MIDIERR_BASE + 0);
        public const int MIDIMAPPER = (-1);
        public const int MIDIPROP_GET = 0x40000000;
        public const int MIDIPROP_SET = unchecked((int)0x80000000);
        public const int MIDIPROP_TEMPO = 0x2;
        public const int MIDIPROP_TIMEDIV = 0x1;
        public const int MIDISTRM_ERROR = -2;
        public const int MIDI_CACHE_ALL = 1;
        public const int MIDI_CACHE_BESTFIT = 2;
        public const int MIDI_CACHE_QUERY = 3;
        public const int MIDI_CACHE_VALID = (MIDI_CACHE_ALL | MIDI_CACHE_BESTFIT | MIDI_CACHE_QUERY | MIDI_UNCACHE);
        public const int MIDI_IO_STATUS = 0x20;
        public const int MIDI_MAPPER = -1;
        public const int MIDI_UNCACHE = 4;
        public const int MIM_CLOSE = MM_MIM_CLOSE;
        public const int MIM_DATA = MM_MIM_DATA;
        public const int MIM_ERROR = MM_MIM_ERROR;
        public const int MIM_LONGDATA = MM_MIM_LONGDATA;
        public const int MIM_LONGERROR = MM_MIM_LONGERROR;
        public const int MIM_MOREDATA = MM_MIM_MOREDATA;
        public const int MIM_OPEN = MM_MIM_OPEN;
        public const int MIXERCONTROL_CONTROLF_DISABLED = unchecked((int)0x80000000);
        public const int MIXERCONTROL_CONTROLF_MULTIPLE = 0x2;
        public const int MIXERCONTROL_CONTROLF_UNIFORM = 0x1;
        public const int MIXERCONTROL_CONTROLTYPE_BASS = (MIXERCONTROL_CONTROLTYPE_FADER + 2);
        public const int MIXERCONTROL_CONTROLTYPE_BOOLEAN = (MIXERCONTROL_CT_CLASS_SWITCH | MIXERCONTROL_CT_SC_SWITCH_BOOLEAN | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_BOOLEANMETER = (MIXERCONTROL_CT_CLASS_METER | MIXERCONTROL_CT_SC_METER_POLLED | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_BUTTON = (MIXERCONTROL_CT_CLASS_SWITCH | MIXERCONTROL_CT_SC_SWITCH_BUTTON | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_CUSTOM = (MIXERCONTROL_CT_CLASS_CUSTOM | MIXERCONTROL_CT_UNITS_CUSTOM);
        public const int MIXERCONTROL_CONTROLTYPE_DECIBELS = (MIXERCONTROL_CT_CLASS_NUMBER | MIXERCONTROL_CT_UNITS_DECIBELS);
        public const int MIXERCONTROL_CONTROLTYPE_EQUALIZER = (MIXERCONTROL_CONTROLTYPE_FADER + 4);
        public const int MIXERCONTROL_CONTROLTYPE_FADER = (MIXERCONTROL_CT_CLASS_FADER | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_LOUDNESS = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 4);
        public const int MIXERCONTROL_CONTROLTYPE_MICROTIME = (MIXERCONTROL_CT_CLASS_TIME | MIXERCONTROL_CT_SC_TIME_MICROSECS | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_MILLITIME = (MIXERCONTROL_CT_CLASS_TIME | MIXERCONTROL_CT_SC_TIME_MILLISECS | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_MIXER = (MIXERCONTROL_CONTROLTYPE_MULTIPLESELECT + 1);
        public const int MIXERCONTROL_CONTROLTYPE_MONO = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 3);
        public const int MIXERCONTROL_CONTROLTYPE_MULTIPLESELECT = (MIXERCONTROL_CT_CLASS_LIST | MIXERCONTROL_CT_SC_LIST_MULTIPLE | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_MUTE = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 2);
        public const int MIXERCONTROL_CONTROLTYPE_MUX = (MIXERCONTROL_CONTROLTYPE_SINGLESELECT + 1);
        public const int MIXERCONTROL_CONTROLTYPE_ONOFF = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 1);
        public const int MIXERCONTROL_CONTROLTYPE_PAN = (MIXERCONTROL_CONTROLTYPE_SLIDER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_PEAKMETER = (MIXERCONTROL_CONTROLTYPE_SIGNEDMETER + 1);
        public const int MIXERCONTROL_CONTROLTYPE_PERCENT = (MIXERCONTROL_CT_CLASS_NUMBER | MIXERCONTROL_CT_UNITS_PERCENT);
        public const int MIXERCONTROL_CONTROLTYPE_QSOUNDPAN = (MIXERCONTROL_CONTROLTYPE_SLIDER + 2);
        public const int MIXERCONTROL_CONTROLTYPE_SIGNED = (MIXERCONTROL_CT_CLASS_NUMBER | MIXERCONTROL_CT_UNITS_SIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_SIGNEDMETER = (MIXERCONTROL_CT_CLASS_METER | MIXERCONTROL_CT_SC_METER_POLLED | MIXERCONTROL_CT_UNITS_SIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_SINGLESELECT = (MIXERCONTROL_CT_CLASS_LIST | MIXERCONTROL_CT_SC_LIST_SINGLE | MIXERCONTROL_CT_UNITS_BOOLEAN);
        public const int MIXERCONTROL_CONTROLTYPE_SLIDER = (MIXERCONTROL_CT_CLASS_SLIDER | MIXERCONTROL_CT_UNITS_SIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_STEREOENH = (MIXERCONTROL_CONTROLTYPE_BOOLEAN + 5);
        public const int MIXERCONTROL_CONTROLTYPE_TREBLE = (MIXERCONTROL_CONTROLTYPE_FADER + 3);
        public const int MIXERCONTROL_CONTROLTYPE_UNSIGNED = (MIXERCONTROL_CT_CLASS_NUMBER | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_UNSIGNEDMETER = (MIXERCONTROL_CT_CLASS_METER | MIXERCONTROL_CT_SC_METER_POLLED | MIXERCONTROL_CT_UNITS_UNSIGNED);
        public const int MIXERCONTROL_CONTROLTYPE_VOLUME = (MIXERCONTROL_CONTROLTYPE_FADER + 1);
        public const int MIXERCONTROL_CT_CLASS_CUSTOM = 0x0;
        public const int MIXERCONTROL_CT_CLASS_FADER = 0x50000000;
        public const int MIXERCONTROL_CT_CLASS_LIST = 0x70000000;
        public const int MIXERCONTROL_CT_CLASS_MASK = unchecked((int)0xF0000000);
        public const int MIXERCONTROL_CT_CLASS_METER = 0x10000000;
        public const int MIXERCONTROL_CT_CLASS_NUMBER = 0x30000000;
        public const int MIXERCONTROL_CT_CLASS_SLIDER = 0x40000000;
        public const int MIXERCONTROL_CT_CLASS_SWITCH = 0x20000000;
        public const int MIXERCONTROL_CT_CLASS_TIME = 0x60000000;
        public const int MIXERCONTROL_CT_SC_LIST_MULTIPLE = 0x1000000;
        public const int MIXERCONTROL_CT_SC_LIST_SINGLE = 0x0;
        public const int MIXERCONTROL_CT_SC_METER_POLLED = 0x0;
        public const int MIXERCONTROL_CT_SC_SWITCH_BOOLEAN = 0x0;
        public const int MIXERCONTROL_CT_SC_SWITCH_BUTTON = 0x1000000;
        public const int MIXERCONTROL_CT_SC_TIME_MICROSECS = 0x0;
        public const int MIXERCONTROL_CT_SC_TIME_MILLISECS = 0x1000000;
        public const int MIXERCONTROL_CT_SUBCLASS_MASK = 0xF000000;
        public const int MIXERCONTROL_CT_UNITS_BOOLEAN = 0x10000;
        public const int MIXERCONTROL_CT_UNITS_CUSTOM = 0x0;
        public const int MIXERCONTROL_CT_UNITS_DECIBELS = 0x40000;
        public const int MIXERCONTROL_CT_UNITS_MASK = 0xFF0000;
        public const int MIXERCONTROL_CT_UNITS_PERCENT = 0x50000;
        public const int MIXERCONTROL_CT_UNITS_SIGNED = 0x20000;
        public const int MIXERCONTROL_CT_UNITS_UNSIGNED = 0x30000;
        public const int MIXERLINE_COMPONENTTYPE_DST_DIGITAL = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_DST_FIRST = 0x0;
        public const int MIXERLINE_COMPONENTTYPE_DST_HEADPHONES = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_DST_LAST = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_DST_LINE = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_DST_MONITOR = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_DST_TELEPHONE = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_DST_UNDEFINED = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_DST_VOICEIN = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 8);
        public const int MIXERLINE_COMPONENTTYPE_DST_WAVEIN = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_SRC_ANALOG = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);
        public const int MIXERLINE_COMPONENTTYPE_SRC_AUXILIARY = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 9);
        public const int MIXERLINE_COMPONENTTYPE_SRC_COMPACTDISC = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 5);
        public const int MIXERLINE_COMPONENTTYPE_SRC_DIGITAL = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 1);
        public const int MIXERLINE_COMPONENTTYPE_SRC_FIRST = 0x1000;
        public const int MIXERLINE_COMPONENTTYPE_SRC_LAST = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 10);
        public const int MIXERLINE_COMPONENTTYPE_SRC_LINE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 2);
        public const int MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 3);
        public const int MIXERLINE_COMPONENTTYPE_SRC_PCSPEAKER = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 7);
        public const int MIXERLINE_COMPONENTTYPE_SRC_SYNTHESIZER = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 4);
        public const int MIXERLINE_COMPONENTTYPE_SRC_TELEPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 6);
        public const int MIXERLINE_COMPONENTTYPE_SRC_UNDEFINED = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 0);
        public const int MIXERLINE_COMPONENTTYPE_SRC_WAVEOUT = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 8);
        public const int MIXERLINE_LINEF_ACTIVE = 0x1;
        public const int MIXERLINE_LINEF_DISCONNECTED = 0x8000;
        public const int MIXERLINE_LINEF_SOURCE = unchecked((int)0x80000000);
        public const int MIXERLINE_TARGETTYPE_AUX = 5;
        public const int MIXERLINE_TARGETTYPE_MIDIIN = 4;
        public const int MIXERLINE_TARGETTYPE_MIDIOUT = 3;
        public const int MIXERLINE_TARGETTYPE_UNDEFINED = 0;
        public const int MIXERLINE_TARGETTYPE_WAVEIN = 2;
        public const int MIXERLINE_TARGETTYPE_WAVEOUT = 1;
        public const int MIXERR_BASE = 1024;
        public const int MIXERR_INVALCONTROL = (MIXERR_BASE + 1);
        public const int MIXERR_INVALLINE = (MIXERR_BASE + 0);
        public const int MIXERR_INVALVALUE = (MIXERR_BASE + 2);
        public const int MIXERR_LASTERROR = (MIXERR_BASE + 2);
        public const int MIXER_GETCONTROLDETAILSF_LISTTEXT = 0x1;
        public const int MIXER_GETCONTROLDETAILSF_QUERYMASK = 0xF;
        public const int MIXER_GETCONTROLDETAILSF_VALUE = 0x0;
        public const int MIXER_GETLINECONTROLSF_ALL = 0x0;
        public const int MIXER_GETLINECONTROLSF_ONEBYID = 0x1;
        public const int MIXER_GETLINECONTROLSF_ONEBYTYPE = 0x2;
        public const int MIXER_GETLINECONTROLSF_QUERYMASK = 0xF;
        public const int MIXER_GETLINEINFOF_COMPONENTTYPE = 0x3;
        public const int MIXER_GETLINEINFOF_DESTINATION = 0x0;
        public const int MIXER_GETLINEINFOF_LINEID = 0x2;
        public const int MIXER_GETLINEINFOF_QUERYMASK = 0xF;
        public const int MIXER_GETLINEINFOF_SOURCE = 0x1;
        public const int MIXER_GETLINEINFOF_TARGETTYPE = 0x4;
        public const int MIXER_LONG_NAME_CHARS = 64;
        public const int MIXER_OBJECTF_AUX = 0x50000000;
        public const int MIXER_OBJECTF_HANDLE = unchecked((int)0x80000000);
        public const int MIXER_OBJECTF_HMIDIIN = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIDIIN);
        public const int MIXER_OBJECTF_HMIDIOUT = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIDIOUT);
        public const int MIXER_OBJECTF_HMIXER = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_MIXER);
        public const int MIXER_OBJECTF_HWAVEIN = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_WAVEIN);
        public const int MIXER_OBJECTF_HWAVEOUT = (MIXER_OBJECTF_HANDLE | MIXER_OBJECTF_WAVEOUT);
        public const int MIXER_OBJECTF_MIDIIN = 0x40000000;
        public const int MIXER_OBJECTF_MIDIOUT = 0x30000000;
        public const int MIXER_OBJECTF_MIXER = 0x0;
        public const int MIXER_OBJECTF_WAVEIN = 0x20000000;
        public const int MIXER_OBJECTF_WAVEOUT = 0x10000000;
        public const int MIXER_SETCONTROLDETAILSF_CUSTOM = 0x1;
        public const int MIXER_SETCONTROLDETAILSF_QUERYMASK = 0xF;
        public const int MIXER_SETCONTROLDETAILSF_VALUE = 0x0;
        public const int MIXER_SHORT_NAME_CHARS = 16;
        public const int MMIOERR_BASE = 256;
        public const int MMIOERR_CANNOTCLOSE = (MMIOERR_BASE + 4);
        public const int MMIOERR_CANNOTEXPAND = (MMIOERR_BASE + 8);
        public const int MMIOERR_CANNOTOPEN = (MMIOERR_BASE + 3);
        public const int MMIOERR_CANNOTREAD = (MMIOERR_BASE + 5);
        public const int MMIOERR_CANNOTSEEK = (MMIOERR_BASE + 7);
        public const int MMIOERR_CANNOTWRITE = (MMIOERR_BASE + 6);
        public const int MMIOERR_CHUNKNOTFOUND = (MMIOERR_BASE + 9);
        public const int MMIOERR_FILENOTFOUND = (MMIOERR_BASE + 1);
        public const int MMIOERR_OUTOFMEMORY = (MMIOERR_BASE + 2);
        public const int MMIOERR_UNBUFFERED = (MMIOERR_BASE + 10);
        public const int MMIOM_CLOSE = 4;
        public const int MMIOM_OPEN = 3;
        public const int MMIOM_READ = MMIO_READ;
        public const int MMIOM_RENAME = 6;
        public const int MMIOM_SEEK = 2;
        public const int MMIOM_USER = 0x8000;
        public const int MMIOM_WRITE = MMIO_WRITE;
        public const int MMIOM_WRITEFLUSH = 5;
        public const int MMIO_ALLOCBUF = 0x10000;
        public const int MMIO_COMPAT = 0x0;
        public const int MMIO_CREATE = 0x1000;
        public const int MMIO_CREATELIST = 0x40;
        public const int MMIO_CREATERIFF = 0x20;
        public const int MMIO_DEFAULTBUFFER = 8192;
        public const int MMIO_DELETE = 0x200;
        public const int MMIO_DENYNONE = 0x40;
        public const int MMIO_DENYREAD = 0x30;
        public const int MMIO_DENYWRITE = 0x20;
        public const int MMIO_DIRTY = 0x10000000;
        public const int MMIO_EMPTYBUF = 0x10;
        public const int MMIO_EXCLUSIVE = 0x10;
        public const int MMIO_EXIST = 0x4000;
        public const int MMIO_FHOPEN = 0x10;
        public const int MMIO_FINDCHUNK = 0x10;
        public const int MMIO_FINDLIST = 0x40;
        public const int MMIO_FINDPROC = 0x40000;
        public const int MMIO_FINDRIFF = 0x20;
        public const int MMIO_GETTEMP = 0x20000;
        public const int MMIO_INSTALLPROC = 0x10000;
        public const int MMIO_OPEN_VALID = 0x3FFFF;
        public const int MMIO_PARSE = 0x100;
        public const int MMIO_PUBLICPROC = 0x10000000;
        public const int MMIO_READ = 0x0;
        public const int MMIO_READWRITE = 0x2;
        public const int MMIO_REMOVEPROC = 0x20000;
        public const int MMIO_RWMODE = 0x3;
        public const int MMIO_SHAREMODE = 0x70;
        public const int MMIO_TOUPPER = 0x10;
        public const int MMIO_UNICODEPROC = 0x1000000;
        public const int MMIO_VALIDPROC = 0x11070000;
        public const int MMIO_WRITE = 0x1;
        public const int MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4);
        public const int MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2);
        public const int MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9);
        public const int MMSYSERR_BASE = 0;
        public const int MMSYSERR_ERROR = (MMSYSERR_BASE + 1);
        public const int MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12);
        public const int MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10);
        public const int MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5);
        public const int MMSYSERR_INVALIDALIAS = (MMSYSERR_BASE + 13);
        public const int MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11);
        public const int MMSYSERR_LASTERROR = (MMSYSERR_BASE + 13);
        public const int MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6);
        public const int MMSYSERR_NOERROR = 0;
        public const int MMSYSERR_NOMEM = (MMSYSERR_BASE + 7);
        public const int MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3);
        public const int MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8);
        public const int MM_ADLIB = 9;
        public const int MM_JOY1BUTTONDOWN = 0x3B5;
        public const int MM_JOY1BUTTONUP = 0x3B7;
        public const int MM_JOY1MOVE = 0x3A0;
        public const int MM_JOY1ZMOVE = 0x3A2;
        public const int MM_JOY2BUTTONDOWN = 0x3B6;
        public const int MM_JOY2BUTTONUP = 0x3B8;
        public const int MM_JOY2MOVE = 0x3A1;
        public const int MM_JOY2ZMOVE = 0x3A3;
        public const int MM_MCINOTIFY = 0x3B9;
        public const int MM_MCISIGNAL = 0x3CB;
        public const int MM_MCISYSTEM_STRING = 0x3CA;
        public const int MM_MICROSOFT = 1;
        public const int MM_MIDI_MAPPER = 1;
        public const int MM_MIM_CLOSE = 0x3C2;
        public const int MM_MIM_DATA = 0x3C3;
        public const int MM_MIM_ERROR = 0x3C5;
        public const int MM_MIM_LONGDATA = 0x3C4;
        public const int MM_MIM_LONGERROR = 0x3C6;
        public const int MM_MIM_MOREDATA = 0x3CC;
        public const int MM_MIM_OPEN = 0x3C1;
        public const int MM_MOM_CLOSE = 0x3C8;
        public const int MM_MOM_DONE = 0x3C9;
        public const int MM_MOM_OPEN = 0x3C7;
        public const int MM_MOM_POSITIONCB = 0x3CA;
        public const int MM_MPU401_MIDIIN = 11;
        public const int MM_MPU401_MIDIOUT = 10;
        public const int MM_PC_JOYSTICK = 12;
        public const int MM_SNDBLST_MIDIIN = 4;
        public const int MM_SNDBLST_MIDIOUT = 3;
        public const int MM_SNDBLST_SYNTH = 5;
        public const int MM_SNDBLST_WAVEIN = 7;
        public const int MM_SNDBLST_WAVEOUT = 6;
        public const int MM_WAVE_MAPPER = 2;
        public const int MM_WIM_CLOSE = 0x3BF;
        public const int MM_WIM_DATA = 0x3C0;
        public const int MM_WIM_OPEN = 0x3BE;
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;
        public const int MM_WOM_OPEN = 0x3BB;
        public const int MOD_FMSYNTH = 4;
        public const int MOD_MAPPER = 5;
        public const int MOD_MIDIPORT = 1;
        public const int MOD_SQSYNTH = 3;
        public const int MOD_SYNTH = 2;
        public const int MOM_CLOSE = MM_MOM_CLOSE;
        public const int MOM_DONE = MM_MOM_DONE;
        public const int MOM_OPEN = MM_MOM_OPEN;
        public const int MOM_POSITIONCB = MM_MOM_POSITIONCB;
        public const int NEWTRANSPARENT = 3;
        public const int QUERYROPSUPPORT = 40;
        public const int SEEK_CUR = 1;
        public const int SEEK_END = 2;
        public const int SEEK_SET = 0;
        public const int SELECTDIB = 41;
        public const int SND_ALIAS = 0x10000;
        public const int SND_ALIAS_ID = 0x110000;
        public const int SND_ALIAS_START = 0;
        public const int SND_APPLICATION = 0x80;
        public const int SND_ASYNC = 0x1;
        public const int SND_FILENAME = 0x20000;
        public const int SND_LOOP = 0x8;
        public const int SND_MEMORY = 0x4;
        public const int SND_NODEFAULT = 0x2;
        public const int SND_NOSTOP = 0x10;
        public const int SND_NOWAIT = 0x2000;
        public const int SND_PURGE = 0x40;
        public const int SND_RESERVED = unchecked((int)0xFF000000);
        public const int SND_RESOURCE = 0x40004;
        public const int SND_SYNC = 0x0;
        public const int SND_TYPE_MASK = 0x170007;
        public const int SND_VALID = 0x1F;
        public const int SND_VALIDFLAGS = 0x17201F;
        public const int TIMERR_BASE = 96;
        public const int TIMERR_NOCANDO = (TIMERR_BASE + 1);
        public const int TIMERR_NOERROR = (0);
        public const int TIMERR_STRUCT = (TIMERR_BASE + 33);
        public const int TIME_BYTES = 0x4;
        public const int TIME_MIDI = 0x10;
        public const int TIME_MS = 0x1;
        public const int TIME_ONESHOT = 0;
        public const int TIME_PERIODIC = 1;
        public const int TIME_SAMPLES = 0x2;
        public const int TIME_SMPTE = 0x8;
        public const int WAVECAPS_LRVOLUME = 0x8;
        public const int WAVECAPS_PITCH = 0x1;
        public const int WAVECAPS_PLAYBACKRATE = 0x2;
        public const int WAVECAPS_SYNC = 0x10;
        public const int WAVECAPS_VOLUME = 0x4;
        public const int WAVERR_BADFORMAT = (WAVERR_BASE + 0);
        public const int WAVERR_BASE = 32;
        public const int WAVERR_LASTERROR = (WAVERR_BASE + 3);
        public const int WAVERR_STILLPLAYING = (WAVERR_BASE + 1);
        public const int WAVERR_SYNC = (WAVERR_BASE + 3);
        public const int WAVERR_UNPREPARED = (WAVERR_BASE + 2);
        public const int WAVE_ALLOWSYNC = 0x2;
        public const int WAVE_FORMAT_1M08 = 0x1;
        public const int WAVE_FORMAT_1M16 = 0x4;
        public const int WAVE_FORMAT_1S08 = 0x2;
        public const int WAVE_FORMAT_1S16 = 0x8;
        public const int WAVE_FORMAT_2M08 = 0x10;
        public const int WAVE_FORMAT_2M16 = 0x40;
        public const int WAVE_FORMAT_2S08 = 0x20;
        public const int WAVE_FORMAT_2S16 = 0x80;
        public const int WAVE_FORMAT_4M08 = 0x100;
        public const int WAVE_FORMAT_4M16 = 0x400;
        public const int WAVE_FORMAT_4S08 = 0x200;
        public const int WAVE_FORMAT_4S16 = 0x800;
        public const int WAVE_FORMAT_DIRECT = 0x8;
        public const int WAVE_FORMAT_DIRECT_QUERY = (WAVE_FORMAT_QUERY | WAVE_FORMAT_DIRECT);
        public const int WAVE_FORMAT_PCM = 1;
        public const int WAVE_FORMAT_QUERY = 0x1;
        public const int WAVE_INVALIDFORMAT = 0x0;
        public const int WAVE_MAPPED = 0x4;
        public const int WAVE_MAPPER = -1;
        public const int WAVE_VALID = 0x3;
        public const int WHDR_BEGINLOOP = 0x4;
        public const int WHDR_DONE = 0x1;
        public const int WHDR_ENDLOOP = 0x8;
        public const int WHDR_INQUEUE = 0x10;
        public const int WHDR_PREPARED = 0x2;
        public const int WHDR_VALID = 0x1F;
        public const int WIM_CLOSE = MM_WIM_CLOSE;
        public const int WIM_DATA = MM_WIM_DATA;
        public const int WIM_OPEN = MM_WIM_OPEN;
        public const int WOM_CLOSE = MM_WOM_CLOSE;
        public const int WOM_DONE = MM_WOM_DONE;
        public const int WOM_OPEN = MM_WOM_OPEN;
        public const string CFSEPCHAR = "+";
    }

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}