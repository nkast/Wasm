using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Input
{
    public class GamepadHapticActuator : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public GamepadHapticActuator(Gamepad gamepad, int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public static GamepadHapticActuator FromUid(int uid)
        {
            if (GamepadHapticActuator._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (GamepadHapticActuator)jsObj;
                else
                    GamepadHapticActuator._uidMap.Remove(uid);
            }

            return null;
        }

        public bool PlayEffect(string type, GamepadHapticActuatorParams actuatorParams)
        {
            return InvokeRet<float, float, float, float, float, float, bool>("nkGamepadHapticActuator.PlayEffect",
                actuatorParams.StartDelay, actuatorParams.Duration, actuatorParams.StrongMagnitude, actuatorParams.WeakMagnitude, actuatorParams.LeftTrigger, actuatorParams.RightTrigger);
        }

        public bool Pulse(float value, float duration)
        {
            return InvokeRet<float, float, bool>("nkGamepadHapticActuator.Pulse", value, duration);
        }

        public bool Reset()
        {
            return InvokeRet<bool>("nkGamepadHapticActuator.Reset");
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
}
