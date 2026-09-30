using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Input
{
    public class Gamepad : CachedJSObject<Gamepad>
    {

        public bool Connected
        {
            get { return InvokeRetBool(RegisterFunction("nkGamepad.GetConnected")); }
        }

        public string Id
        {
            get { return InvokeRetString(RegisterFunction("nkGamepad.GetId")); }
        }

        public int Index
        {
            get { return InvokeRetInt(RegisterFunction("nkGamepad.GetIndex")); }
        }

        public string Mapping
        {
            get { return InvokeRetString(RegisterFunction("nkGamepad.GetMapping")); }
        }

        public int Timestamp
        {
            get { return InvokeRetInt(RegisterFunction("nkGamepad.GetTimestamp")); }
        }

        public unsafe GamepadButton[] Buttons
        {
            get
            {
                int count = -InvokeRetInt<int, int, IntPtr>(RegisterFunction("nkGamepad.GetButtons"), -1, 0, IntPtr.Zero);
                GamepadButton[] ret = new GamepadButton[count];

                fixed (GamepadButton* pret = ret)
                {
                    count = InvokeRetInt<int, int, IntPtr>(RegisterFunction("nkGamepad.GetButtons"), count, sizeof(GamepadButton), new IntPtr(pret));
                }

                return ret;
            }
        }

        public unsafe float[] Axes
        {
            get
            {
                int count = -InvokeRetInt<int, IntPtr>(RegisterFunction("nkGamepad.GetAxes"), -1, IntPtr.Zero);
                float[] ret = new float[count];

                fixed (float* pret = ret)
                {
                    count = -InvokeRetInt<int, IntPtr>(RegisterFunction("nkGamepad.GetAxes"), count, new IntPtr(pret));
                }

                return ret;
            }
        }

        public GamepadHapticActuator VibrationActuator
        {
            get
            {
                int uid = InvokeRetInt(RegisterFunction("nkGamepad.GetVibrationActuator"));
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

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
