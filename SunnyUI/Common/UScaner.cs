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
 * 文件名称: UScaner.cs
 * 文件说明: 扫码枪键盘钩子类
 * 当前版本: V3.3
 * 创建日期: 2023-06-17
 *
 * 2023-06-17: V3.3.9 增加文件说明
******************************************************************************/

/******************************************************************************
using Sunny.UI;
using System.Collections.Concurrent;
using System;

public partial class Form1 : UIForm
{
    private readonly ScanerHook Scaner = new ScanerHook(200, 3);
    private readonly ConcurrentQueue<string> ScanerCodes = new ConcurrentQueue<string>();

    public Form1()
    {
        InitializeComponent();
        Scaner.Start();
        if (Scaner.IsHook) Console.WriteLine("键盘钩子安装成功。");
        Scaner.OnReceiveScanerCodes += Scaner_OnReceiveScanerCodes;
        timer1.Start();
    }

    /// <summary>
    /// 扫码枪扫描结果
    /// </summary>
    /// <param name="codes"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void Scaner_OnReceiveScanerCodes(ScanerCodes codes)
    {
        ScanerCodes.Enqueue(codes.Result);
        Console.WriteLine(codes.Result);
    }

    private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        timer1.Stop();
        Scaner.Stop();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Stop();

        while (ScanerCodes.Count > 0)
        {
            if (ScanerCodes.TryDequeue(out var codesresult))
            {
                //使用扫描结果，可以再次以条码规则校验，例如起始字符、长度等
                Text = codesresult;
            }
        }

        timer1.Start();
    }
}
******************************************************************************/

using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Sunny.UI
{
    /// <summary>
    /// 扫码枪键盘钩子类
    /// 扫码枪扫描内容：ASCII字母组合，以回车结束
    /// 指定输入间隔以内时为有效输入，默认200毫秒
    /// </summary>
    public class ScanerHook
    {
        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        public event OnScanerCodes OnReceiveScanerCodes;
        private int idHook = 0;
        private readonly ScanerCodes codes;
        private HookProc hookproc;
        private GCHandle gc;

        //是否安装了钩子
        public bool IsHook { get; private set; } = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32", EntryPoint = "ToAscii")]
        private static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeySate, ref uint lpChar, int uFlags);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string name);

        //把击键信息传递到下一个监听键盘事件的应用程序
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        private int MinLength { get; set; } = 0;
        private int MaxLength { get; set; } = ushort.MaxValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timeout">指定输入间隔以内时为有效输入(单位：毫秒)</param>
        /// <param name="minLength">条码最小长度</param>
        /// <param name="maxLength">条码最大长度</param>
        public ScanerHook(int timeout = 200, int minLength = 0, int maxLength = ushort.MaxValue)
        {
            codes = new ScanerCodes(200);
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public bool Start()
        {
            if (idHook == 0)
            {
                hookproc = new HookProc(KeyboardHookProc);
                //GetModuleHandle 函数 替代 Marshal.GetHINSTANCE
                //防止在 framework4.0中 注册钩子不成功
                IntPtr modulePtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
                //全局钩子 WH_KEYBOARD_LL = 13
                idHook = SetWindowsHookEx(13, hookproc, modulePtr, 0);

                if (idHook > 0)
                {
                    IsHook = true;
                    //保持活动 避免 回调过程 被垃圾回收
                    gc = GCHandle.Alloc(hookproc);
                }
                else
                {
                    IsHook = false;
                    UnhookWindowsHookEx(idHook);
                }
            }

            return idHook != 0;
        }

        public bool Stop()
        {
            if (idHook != 0)
            {
                return UnhookWindowsHookEx(idHook);
            }

            return true;
        }

        private int KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            EVENTMSG msg = (EVENTMSG)Marshal.PtrToStructure(lParam, typeof(EVENTMSG));
            codes.Add(msg);
            if (OnReceiveScanerCodes != null && msg.message == 13 && msg.paramH > 0 && !string.IsNullOrEmpty(codes.Result))
            {
                if (codes.Result.Length >= MinLength && codes.Result.Length <= MaxLength)
                {
                    OnReceiveScanerCodes(codes);
                }
            }

            return CallNextHookEx(idHook, nCode, wParam, lParam);
        }
    }

    public delegate void OnScanerCodes(ScanerCodes codes);

    public class ScanerCodes
    {
        private int ts = 200; //指定输入间隔为200毫秒以内时为有效输入
        private List<List<EVENTMSG>> _keys = new List<List<EVENTMSG>>();
        private List<int> _keydown = new List<int>();   // 保存组合键状态
        private List<string> _result = new List<string>();  // 返回结果集
        private DateTime _last = DateTime.Now;
        private byte[] _state = new byte[256];
        private string _key = string.Empty;
        private string _cur = string.Empty;

        public ScanerCodes(int timeout)
        {
            ts = timeout;
        }

        public EVENTMSG Event
        {
            get
            {
                if (_keys.Count == 0)
                {
                    return new EVENTMSG();
                }
                else
                {
                    return _keys[_keys.Count - 1][_keys[_keys.Count - 1].Count - 1];
                }
            }
        }

        public List<int> KeyDowns
        {
            get
            {
                return _keydown;
            }
        }

        public DateTime LastInput
        {
            get
            {
                return _last;
            }
        }

        public byte[] KeyboardState
        {
            get
            {
                return _state;
            }
        }

        public int KeyDownCount
        {
            get
            {
                return _keydown.Count;
            }
        }

        public string Result
        {
            get
            {
                if (_result.Count > 0)
                {
                    return _result[_result.Count - 1].Trim();
                }
                else
                {
                    return null;
                }
            }
        }

        public string CurrentKey
        {
            get
            {
                return _key;
            }
        }

        public string CurrentChar
        {
            get
            {
                return _cur;
            }
        }

        public bool isShift
        {
            get
            {
                return _keydown.Contains(160);
            }
        }

        public void Add(EVENTMSG msg)
        {
            #region 记录按键信息
            if (_keys.Count == 0)
            {
                // 首次按下按键
                _keys.Add(new List<EVENTMSG>());
                _keys[0].Add(msg);
                _result.Add(string.Empty);
            }
            else if (_keydown.Count > 0)
            {
                // 未释放其他按键时按下按键
                _keys[_keys.Count - 1].Add(msg);
            }
            else if ((DateTime.Now - _last).TotalMilliseconds < ts)
            {
                // 单位时间内按下按键
                _keys[_keys.Count - 1].Add(msg);
            }
            else
            {
                // 从新记录输入内容
                _keys.Add(new List<EVENTMSG>());
                _keys[_keys.Count - 1].Add(msg);
                _result.Add(string.Empty);
            }

            #endregion

            _last = DateTime.Now;

            #region 获取键盘状态
            // 记录正在按下的按键
            if (msg.paramH == 0 && !_keydown.Contains(msg.message))
            {
                _keydown.Add(msg.message);
            }

            // 清除已松开的按键
            if (msg.paramH > 0 && _keydown.Contains(msg.message))
            {
                _keydown.Remove(msg.message);
            }

            #endregion

            #region 计算按键信息
            int v = msg.message & 0xff;
            int c = msg.paramL & 0xff;
            StringBuilder strKeyName = new StringBuilder(500);
            if (GetKeyNameText(c * 65536, strKeyName, 255) > 0)
            {
                _key = strKeyName.ToString().Trim(new char[] { ' ', '\0' });
                GetKeyboardState(_state);
                if (_key.Length == 1 && msg.paramH == 0)
                {
                    // 根据键盘状态和shift缓存判断输出字符
                    _cur = ShiftChar(_key, isShift, _state).ToString();
                    _result[_result.Count - 1] += _cur;
                }
                else
                {
                    _cur = string.Empty;
                }
            }

            #endregion
        }

        [DllImport("user32", EntryPoint = "GetKeyNameText")]
        private static extern int GetKeyNameText(int IParam, StringBuilder lpBuffer, int nSize);

        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        private char ShiftChar(string k, bool isShiftDown, byte[] state)
        {
            bool capslock = state[0x14] == 1;
            bool numlock = state[0x90] == 1;
            bool scrolllock = state[0x91] == 1;
            bool shiftdown = state[0xa0] == 1;
            char chr = (capslock ? k.ToUpper() : k.ToLower()).ToCharArray()[0];
            if (isShiftDown)
            {
                if (chr >= 'a' && chr <= 'z')
                {
                    chr = (char)(chr - 32);
                }
                else if (chr >= 'A' && chr <= 'Z')
                {
                    chr = (char)(chr + 32);
                }
                else
                {
                    string s = "`1234567890-=[];',./";
                    string u = "~!@#$%^&*()_+{}:\"<>?";
                    if (s.IndexOf(chr) >= 0)
                    {
                        return (u.ToCharArray())[s.IndexOf(chr)];
                    }
                }
            }

            return chr;
        }
    }
}