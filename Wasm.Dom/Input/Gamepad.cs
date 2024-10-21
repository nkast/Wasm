using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Input
{
    public class Gamepad : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public bool Connected
        {
            get { return InvokeRet<bool>("nkGamepad.GetConnected"); }
        }

        public string Id
        {
            get { return InvokeRet<string>("nkGamepad.GetId"); }
        }

        public int Index
        {
            get { return InvokeRet<int>("nkGamepad.GetIndex"); }
        }

        public string Mapping
        {
            get { return InvokeRet<string>("nkGamepad.GetMapping"); }
        }

        public int Timestamp
        {
            get { return InvokeRet<int>("nkGamepad.GetTimestamp"); }
        }

        public GamepadButton[] Buttons
        {
            get
            {
                string str = InvokeRet<string>("nkGamepad.GetButtons");

                if (str == String.Empty)
                    return new GamepadButton[0];

                string[] strs = str.Split(',');
                GamepadButton[] ret = new GamepadButton[strs.Length/3];
                for (int cnt = 0; cnt < ret.Length; cnt++)
                {
                    ret[cnt].Value = float.Parse(strs[cnt*3+0]);
                    ret[cnt].Pressed = int.Parse(strs[cnt*3+1]) != 0;
                    ret[cnt].Touched = int.Parse(strs[cnt*3+2]) != 0;
                }

                return ret;
            }
        }

        public float[] Axes
        {
            get
            {
                string str = InvokeRet<string>("nkGamepad.GetAxes");

                if (str == String.Empty)
                    return new float[0];

                string[] strs = str.Split(',');
                float[] ret = new float[strs.Length];
                for (int cnt = 0; cnt < ret.Length; cnt++)
                {
                    ret[cnt] = float.Parse(strs[cnt]);
                }

                return ret;
            }
        }

        public GamepadHapticActuator VibrationActuator
        {
            get
            {
                int uid = InvokeRet<int>("nkGamepad.GetVibrationActuator");
                GamepadHapticActuator gamepadHapticActuator = GamepadHapticActuator.FromUid(uid);
                if (gamepadHapticActuator != null)
                    return gamepadHapticActuator;

                if (uid != 0)
                    return new GamepadHapticActuator(this, uid);

                return null;
            }
        }

        public Gamepad(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public static Gamepad FromUid(int uid)
        {
            if (Gamepad._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (Gamepad)jsObj;
                else
                    Gamepad._uidMap.Remove(uid);
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }

    public struct GamepadButton
    {
        public float Value;
        public bool Pressed;
        public bool Touched;

        public override string ToString()
        {
            return String.Format("{{Value:{0}, Pressed:{1}, Touched:{2} }}", Value, Pressed, Touched);
        }
    }
}
