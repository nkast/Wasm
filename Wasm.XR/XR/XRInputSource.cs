using System;
using System.Collections.Generic;
using nkast.Wasm.Input;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRInputSource : CachedJSObject<XRInputSource>
    {
        private readonly int _fid_GetGripSpace;
        private readonly int _fid_GetTargetRaySpace;
        private readonly int _fid_GetHandedness;
        private readonly int _fid_GetGamepad;
        private readonly int _fid_GetHand;


        public XRSpace GripSpace
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetGripSpace);
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
                int uid = InvokeRetInt(_fid_GetTargetRaySpace);
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
                int hand = InvokeRetInt(_fid_GetHandedness);
                return (XRHandedness)hand;
            }
        }

        public Gamepad Gamepad
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetGamepad);
                if (uid == -1)
                    return null;

                Gamepad gamepad = Gamepad.FromUid(uid);
                if (gamepad != null)
                    return gamepad;

                return new Gamepad(uid);
            }
        }

        public XRHand Hand
        {
            get
            {
                //int uid = InvokeRetInt("nkXRInputSource.GetGripSpace");
                int uid = InvokeRetInt(_fid_GetHand);
                if (uid == -1)
                    return null;

                XRHand hand = XRHand.FromUid(uid);
                if (hand != null)
                    return hand;

                return new XRHand(uid);

            }
        }

        internal XRInputSource(int uid) : base(uid)
        {
            _fid_GetGripSpace = RegisterFunction("nkXRInputSource.GetGripSpace");
            _fid_GetTargetRaySpace = RegisterFunction("nkXRInputSource.GetTargetRaySpace");
            _fid_GetHandedness = RegisterFunction("nkXRInputSource.GetHandedness");
            _fid_GetGamepad = RegisterFunction("nkXRInputSource.GetGamepad");
            _fid_GetHand = RegisterFunction("nkXRInputSource.GetHand");
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