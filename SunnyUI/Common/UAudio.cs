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

        private static SoundPlayer _soundPlayer;

        #region Methods

        /// <summary>
        /// 停止播放声音。
        /// </summary>
        /// <param name="sound">SoundPlayer 对象。</param>
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

        /// <summary>
        /// 播放 .wav 声音文件。
        /// </summary>
        /// <param name="location">包含声音文件的名称的字符串。</param>
        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }

        /// <summary>
        /// 播放 .wav 声音文件。
        /// </summary>
        /// <param name="location">包含声音文件的名称的字符串。</param>
        /// <param name="playMode">AudioPlayMode 枚举，指示播放模式。</param>
        public static void Play(string location, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, nameof(playMode));
            var validatedLocation = ValidateFilename(location);
            SoundPlayer player = new SoundPlayer(validatedLocation);
            Play(player, playMode);
        }

        /// <summary>
        /// 根据播放模式播放声音。
        /// </summary>
        /// <param name="sound">SoundPlayer 对象。</param>
        /// <param name="mode">AudioPlayMode 枚举，指示播放模式。</param>
        private static void Play(SoundPlayer sound, AudioPlayMode mode)
        {
            if (_soundPlayer != null)
            {
                InternalStop(_soundPlayer);
            }

            _soundPlayer = sound;
            switch (mode)
            {
                case AudioPlayMode.WaitToComplete:
                    _soundPlayer.PlaySync();
                    break;

                case AudioPlayMode.Background:
                    _soundPlayer.Play();
                    break;

                case AudioPlayMode.BackgroundLoop:
                    _soundPlayer.PlayLooping();
                    break;
            }
        }

        /// <summary>
        /// 播放系统声音。
        /// </summary>
        /// <param name="systemSound">SystemSound 对象，代表系统播放声音。</param>
        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException(nameof(systemSound));
            }

            systemSound.Play();
        }

        /// <summary>
        /// 停止在后台播放声音。
        /// </summary>
        public static void Stop()
        {
            SoundPlayer player = new SoundPlayer();
            InternalStop(player);
        }

        /// <summary>
        /// 验证 AudioPlayMode 枚举值。
        /// </summary>
        /// <param name="value">AudioPlayMode 枚举值。</param>
        /// <param name="paramName">参数名称。</param>
        private static void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
        {
            if (!Enum.IsDefined(typeof(AudioPlayMode), value))
            {
                throw new InvalidEnumArgumentException(paramName, (int)value, typeof(AudioPlayMode));
            }
        }

        /// <summary>
        /// 验证文件名。
        /// </summary>
        /// <param name="location">文件名字符串。</param>
        /// <returns>验证后的文件名。</returns>
        private static string ValidateFilename(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
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
        /// 播放 MP3 文件。
        /// </summary>
        /// <param name="mp3FileName">文件名。</param>
        /// <param name="repeat">是否重复播放。</param>
        public static void Play(string mp3FileName, bool repeat)
        {
            Win32.WinMM.mciSendString($"open \"{mp3FileName}\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            Win32.WinMM.mciSendString($"play MediaFile{(repeat ? " repeat" : string.Empty)}", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 暂停播放。
        /// </summary>
        public static void Pause()
        {
            Win32.WinMM.mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 停止播放。
        /// </summary>
        public static void Stop()
        {
            Win32.WinMM.mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }
    }
}