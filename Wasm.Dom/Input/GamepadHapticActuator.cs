using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Input
{
    public class GamepadHapticActuator : CachedJSObject<GamepadHapticActuator>
    {

        public GamepadHapticActuator(Gamepad gamepad, int uid) : base(uid)
        {
        }

        public bool PlayEffect(string type, GamepadHapticActuatorParams actuatorParams)
        {
            return InvokeRetBool<float, float, float, float, float, float>("nkGamepadHapticActuator.PlayEffect",
                actuatorParams.StartDelay, actuatorParams.Duration, actuatorParams.StrongMagnitude, actuatorParams.WeakMagnitude, actuatorParams.LeftTrigger, actuatorParams.RightTrigger);
        }

        public bool Pulse(float value, float duration)
        {
            return InvokeRetBool<float, float>("nkGamepadHapticActuator.Pulse", value, duration);
        }

        public bool Reset()
        {
            return InvokeRetBool("nkGamepadHapticActuator.Reset");
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
