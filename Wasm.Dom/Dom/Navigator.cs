using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using nkast.Wasm.Input;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Navigator : CachedJSObject<Navigator>
    {
        private readonly int _fid_GetUserAgent;
        private readonly int _fid_GetMaxTouchPoints;
        private readonly int _fid_GetGamepads;
        private readonly int _fid_Vibrate;

        private readonly Window _window;
        static Gamepad[] _emptyGamepadArray = new Gamepad[0];
        Dictionary<int,Gamepad> _prevGamepads = new Dictionary<int,Gamepad>();

        public string UserAgent
        {
            get { return InvokeRetString(_fid_GetUserAgent); }
        }

        public int MaxTouchPoints
        {
            get { return InvokeRetInt(_fid_GetMaxTouchPoints); }
        }

        internal Navigator(Window window, int uid) : base(uid)
        {
            _fid_GetUserAgent = RegisterFunction("nkNavigator.GetUserAgent");
            _fid_GetMaxTouchPoints = RegisterFunction("nkNavigator.GetMaxTouchPoints");
            _fid_GetGamepads = RegisterFunction("nkNavigator.GetGamepads");
            _fid_Vibrate = RegisterFunction("nkNavigator.Vibrate");
            _window = window;
        }

        public Gamepad[] GetGamepads()
        {
            using(GamepadArray gamepadsArray = GetGamepadArray())
            {
                Gamepad[] gamepads = new Gamepad[gamepadsArray.Count];
                for (int index = 0; index < gamepadsArray.Count; index++)
                {
                    Gamepad gamepad = gamepadsArray[index];
                    if (gamepad != null)
                    {
                        gamepads[index] = gamepad;
                        continue;
                    }
                }

                foreach (int key in _prevGamepads.Keys)
                {
                    if (!gamepads.Contains(_prevGamepads[key]))
                        _prevGamepads[key].Dispose();
                }

                _prevGamepads.Clear();
                for (int index = 0; index < gamepads.Length; index++)
                    _prevGamepads.Add(index, gamepads[index]);

                return gamepads;
            }
        }

        private GamepadArray GetGamepadArray()
        {
            int uid = InvokeRetInt(_fid_GetGamepads);

            GamepadArray gamepadArray = GamepadArray.FromUid(uid);
            if (gamepadArray != null)
                return gamepadArray;

            return new GamepadArray(uid);
        }

        public void Vibrate(int duration)
        {
            Invoke<int>(_fid_Vibrate, duration);
        }

        public void Vibrate(TimeSpan duration)
        {
            Invoke<int>(_fid_Vibrate, (int)duration.TotalMilliseconds);
        }
    }
}
