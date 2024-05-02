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
 * 文件名称: UAudio.cs
 * 文件说明: 声音播放辅助类
 * 当前版本: V3.1
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System;
using System.ComponentModel;
using System.Media;
using System.Security;
using System.Security.Permissions;
#pragma warning disable SYSLIB0003 // 类型或成员已过时

namespace Sunny.UI
{
    /// <summary>
    /// Wav声音播放辅助类
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class WavPlayer
    {
        /// <summary>
        /// 指示如何调用音频方法时，播放声音。
        /// </summary>
        public enum AudioPlayMode
        {
            /// <summary>
            /// 播放声音，并等待，直到它完成之前调用代码继续。
            /// </summary>
            WaitToComplete,

            /// <summary>
            /// 在后台播放声音。调用代码继续执行。
            /// </summary>
            Background,

            /// <summary>
            /// 直到stop方法被称为播放背景声音。调用代码继续执行。
            /// </summary>
            BackgroundLoop
        }

        private static SoundPlayer _SoundPlayer;

        #region Methods

        private static void InternalStop(SoundPlayer sound)
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
            try
            {
                sound.Stop();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        /// <summary>播放。wav声音文件。</summary>
        /// <param name="location">String，包含声音文件的名称 </param>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" /><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }

        /// <summary>
        /// 播放。wav声音文件.
        /// </summary>
        /// <param name="location">AudioPlayMode枚举模式播放声音。默认情况下，AudioPlayMode.Background。</param>
        /// <param name="playMode">String，包含声音文件的名称</param>
        public static void Play(string location, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, "playMode");
            string text1 = ValidateFilename(location);
            SoundPlayer player1 = new SoundPlayer(text1);
            Play(player1, playMode);
        }

        private static void Play(SoundPlayer sound, AudioPlayMode mode)
        {
            if (_SoundPlayer != null)
            {
                InternalStop(_SoundPlayer);
            }

            _SoundPlayer = sound;
            switch (mode)
            {
                case AudioPlayMode.WaitToComplete:
                    _SoundPlayer.PlaySync();
                    return;

                case AudioPlayMode.Background:
                    _SoundPlayer.Play();
                    return;

                case AudioPlayMode.BackgroundLoop:
                    _SoundPlayer.PlayLooping();
                    return;
            }
        }

        /// <summary>
        /// 播放系统声音。
        /// </summary>
        /// <param name="systemSound">对象代表系统播放声音。</param>
        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException();
            }

            systemSound.Play();
        }

        /// <summary>
        /// 停止在后台播放声音。
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static void Stop()
        {
            SoundPlayer player1 = new SoundPlayer();
            InternalStop(player1);
        }

        private static void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
        {
            if ((value < AudioPlayMode.WaitToComplete) || (value > AudioPlayMode.BackgroundLoop))
            {
                throw new InvalidEnumArgumentException(paramName, (int)value, typeof(AudioPlayMode));
            }
        }

        private static string ValidateFilename(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException();
            }

            return location;
        }

        #endregion Methods
    }

    /// <summary>
    /// MP3文件播放操作辅助类
    /// </summary>
    public static class Mp3Player
    {
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="MP3_FileName">文件名</param>
        /// <param name="Repeat">重复</param>
        public static void Play(string MP3_FileName, bool Repeat)
        {
            Win32.WinMM.mciSendString("open \"" + MP3_FileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            Win32.WinMM.mciSendString("play MediaFile" + (Repeat ? " repeat" : string.Empty), null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public static void Pause()
        {
            Win32.WinMM.mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            Win32.WinMM.mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }
    }
}