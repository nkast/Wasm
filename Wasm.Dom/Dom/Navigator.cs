using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Input;

namespace nkast.Wasm.Dom
{
    public class Navigator : CachedJSObject<Navigator>
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
            get { return InvokeRetInt("nkNavigator.GetMaxTouchPoints"); }
        }

        internal Navigator(Window window, int uid) : base(uid)
        {
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
            int uid = InvokeRetInt("nkNavigator.GetGamepads");

            GamepadArray gamepadArray = GamepadArray.FromUid(uid);
            if (gamepadArray != null)
                return gamepadArray;

            return new GamepadArray(uid);
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
