using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Input;

namespace nkast.Wasm.Dom
{
    public class Navigator : JSObject
    {
        private readonly Window _window;
        static Gamepad[] _emptyGamepadArray = new Gamepad[0];
        Dictionary<int,Gamepad> _prevGamepads = new Dictionary<int,Gamepad>();

        public string UserAgent
        {
            get { return InvokeRet<string>("nkNavigator.GetUserAgent"); }
        }

        public int MaxTouchPoints
        {
            get { return InvokeRet<int>("nkNavigator.GetMaxTouchPoints"); }
        }

        internal Navigator(Window window, int uid) : base(uid)
        {
            _window = window;
        }

        public Gamepad[] GetGamepads()
        {
            string str = InvokeRet<string>("nkNavigator.GetGamepads");

            if (str == String.Empty)
                return _emptyGamepadArray;

            string[] strs = str.Split(',');
            Gamepad[] gamepads = new Gamepad[strs.Length];

            for (int index = 0; index < strs.Length; index++)
            {
                int uid = int.Parse(strs[index]);
                Gamepad gamepad = Gamepad.FromUid(uid);
                if (gamepad != null)
                {
                    gamepads[index] = gamepad;
                    continue;
                }

                if (uid != 0)
                    gamepads[index] = new Gamepad(uid);
            }

            foreach(int key in _prevGamepads.Keys)
            {
                if (!gamepads.Contains(_prevGamepads[key]))
                    _prevGamepads[key].Dispose();
            }
            _prevGamepads.Clear();
            for (int index = 0; index < gamepads.Length; index++)
                _prevGamepads.Add(index, gamepads[index]);

            return gamepads;
        }

        public void Vibrate(int duration)
        {
            Invoke<int>("nkNavigator.Vibrate", duration);
        }

        public void Vibrate(TimeSpan duration)
        {
            Invoke<int>("nkNavigator.Vibrate", (int)duration.TotalMilliseconds);
        }
    }
}
