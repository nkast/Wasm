using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Input
{
    public class Gamepad : CachedJSObject<Gamepad>
    {
        private readonly int _fid_GetConnected;
        private readonly int _fid_GetId;
        private readonly int _fid_GetIndex;
        private readonly int _fid_GetMapping;
        private readonly int _fid_GetTimestamp;
        private readonly int _fid_GetButtons;
        private readonly int _fid_GetAxes;
        private readonly int _fid_GetVibrationActuator;


        public bool Connected
        {
            get { return InvokeRetBool(_fid_GetConnected); }
        }

        public string Id
        {
            get { return InvokeRetString(_fid_GetId); }
        }

        public int Index
        {
            get { return InvokeRetInt(_fid_GetIndex); }
        }

        public string Mapping
        {
            get { return InvokeRetString(_fid_GetMapping); }
        }

        public int Timestamp
        {
            get { return InvokeRetInt(_fid_GetTimestamp); }
        }

        public unsafe GamepadButton[] Buttons
        {
            get
            {
                int count = -InvokeRetInt<int, int, IntPtr>(_fid_GetButtons, -1, 0, IntPtr.Zero);
                GamepadButton[] ret = new GamepadButton[count];

                fixed (GamepadButton* pret = ret)
                {
                    count = InvokeRetInt<int, int, IntPtr>(_fid_GetButtons, count, sizeof(GamepadButton), new IntPtr(pret));
                }

                return ret;
            }
        }

        public unsafe float[] Axes
        {
            get
            {
                int count = -InvokeRetInt<int, IntPtr>(_fid_GetAxes, -1, IntPtr.Zero);
                float[] ret = new float[count];

                fixed (float* pret = ret)
                {
                    count = -InvokeRetInt<int, IntPtr>(_fid_GetAxes, count, new IntPtr(pret));
                }

                return ret;
            }
        }

        public GamepadHapticActuator VibrationActuator
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetVibrationActuator);
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
            _fid_GetConnected = RegisterFunction("nkGamepad.GetConnected");
            _fid_GetId = RegisterFunction("nkGamepad.GetId");
            _fid_GetIndex = RegisterFunction("nkGamepad.GetIndex");
            _fid_GetMapping = RegisterFunction("nkGamepad.GetMapping");
            _fid_GetTimestamp = RegisterFunction("nkGamepad.GetTimestamp");
            _fid_GetButtons = RegisterFunction("nkGamepad.GetButtons");
            _fid_GetAxes = RegisterFunction("nkGamepad.GetAxes");
            _fid_GetVibrationActuator = RegisterFunction("nkGamepad.GetVibrationActuator");
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
