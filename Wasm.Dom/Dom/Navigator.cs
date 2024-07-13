using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Input;

namespace nkast.Wasm.Dom
{
    public class Navigator : JSObject
    {
        private readonly Window _window;

        private Dictionary<int,Gamepad> _gamepadMap = new Dictionary<int, Gamepad>();

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

            string[] strs = str.Split(',');
            Gamepad[] gamepads = new Gamepad[strs.Length];

            for (int index = 0; index < strs.Length; index++)
            {
                int gpuid = int.Parse(strs[index]);
                if (gpuid == 0)
                {
                    gamepads[index] = null;
                }
                else
                {
                    if (!_gamepadMap.ContainsKey(gpuid))
                        _gamepadMap[gpuid] = new Gamepad(gpuid);

                    gamepads[index] = _gamepadMap[gpuid];
                }
            }

            List<int> keys = new List<int>(_gamepadMap.Keys);
            for (int index = 0; index < keys.Count; index++)
            {
                bool found = false;
                for (int k = 0; k < strs.Length; k++)
                {
                    int gpuid = int.Parse(strs[k]);
                    if (keys[index] == gpuid)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    _gamepadMap.Remove(keys[index]);
            }

            return gamepads;
        }
    }
}
