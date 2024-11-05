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

        public unsafe GamepadButton[] Buttons
        {
            get
            {
                int count = -InvokeRet<int, int, IntPtr, int>("nkGamepad.GetButtons", -1, 0, IntPtr.Zero);
                GamepadButton[] ret = new GamepadButton[count];

                fixed (GamepadButton* pret = ret)
                {
                    count = InvokeRet<int, int, IntPtr, int>("nkGamepad.GetButtons", count, sizeof(GamepadButton), new IntPtr(pret));
                }

                return ret;
            }
        }

        public unsafe float[] Axes
        {
            get
            {
                int count = -InvokeRet<int, IntPtr, int>("nkGamepad.GetAxes", -1, IntPtr.Zero);
                float[] ret = new float[count];

                fixed (float* pret = ret)
                {
                    count = -InvokeRet<int, IntPtr, int>("nkGamepad.GetAxes", count, new IntPtr(pret));
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

                if (uid > 0)
                    return new GamepadHapticActuator(this, uid);

                return null;
            }
        }

        public Gamepad(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
        }

        public static Gamepad FromUid(int uid)
        {
            if (Gamepad._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (Gamepad)jsObj;

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
