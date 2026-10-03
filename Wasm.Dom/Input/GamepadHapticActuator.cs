using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Input
{
    public class GamepadHapticActuator : CachedJSObject<GamepadHapticActuator>
    {
        private readonly int _fid_PlayEffect;
        private readonly int _fid_Pulse;
        private readonly int _fid_Reset;


        public GamepadHapticActuator(Gamepad gamepad, int uid) : base(uid)
        {
            _fid_PlayEffect = RegisterFunction("nkGamepadHapticActuator.PlayEffect");
            _fid_Pulse = RegisterFunction("nkGamepadHapticActuator.Pulse");
            _fid_Reset = RegisterFunction("nkGamepadHapticActuator.Reset");
        }

        public bool PlayEffect(string type, GamepadHapticActuatorParams actuatorParams)
        {
            return InvokeRetBool<float, float, float, float, float, float>(_fid_PlayEffect,
                actuatorParams.StartDelay, actuatorParams.Duration, actuatorParams.StrongMagnitude, actuatorParams.WeakMagnitude, actuatorParams.LeftTrigger, actuatorParams.RightTrigger);
        }

        public bool Pulse(float value, float duration)
        {
            return InvokeRetBool<float, float>(_fid_Pulse, value, duration);
        }

        public bool Reset()
        {
            return InvokeRetBool(_fid_Reset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
