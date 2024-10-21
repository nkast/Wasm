using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.Input;

namespace nkast.Wasm.XR
{
    public class XRInputSource : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRSpace GripSpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetGripSpace");
                if (uid == -1)
                    return null;

                XRSpace space = XRSpace.FromUid(uid);
                if (space != null)
                    return space;

                return new XRSpace(uid);
            }
        }

        public XRSpace TargetRaySpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetTargetRaySpace");
                if (uid == -1)
                    return null;

                XRSpace space = XRSpace.FromUid(uid);
                if (space != null)
                    return space;

                return new XRSpace(uid);
            }
        }

        public XRHandedness Handedness
        {
            get
            {
                int hand = InvokeRet<int>("nkXRInputSource.GetHandedness");
                return (XRHandedness)hand;
            }
        }

        public Gamepad Gamepad
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetGamepad");
                if (uid == -1)
                    return null;

                Gamepad gamepad = Gamepad.FromUid(uid);
                if (gamepad != null)
                    return gamepad;

                return new Gamepad(uid);
            }
        }

        public XRInputSource(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public static XRInputSource FromUid(int uid)
        {
            if (XRInputSource._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRInputSource)jsObj;
                else
                    XRInputSource._uidMap.Remove(uid);
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
}