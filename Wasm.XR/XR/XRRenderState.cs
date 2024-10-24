﻿using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRRenderState : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRRenderState(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public static XRRenderState FromUid(int uid)
        {
            if (XRRenderState._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRRenderState)jsObj;
                else
                    XRRenderState._uidMap.Remove(uid);
            }

            return null;
        }

        public XRWebGLLayer BaseLayer
        {
            get
            {
                int uid = InvokeRet<int>("nkXRRenderState.GetBaseLayer");
                XRWebGLLayer glLayer = XRWebGLLayer.FromUid(uid);
                if (glLayer != null)
                    return glLayer;

                throw new NotImplementedException();
                //return new XRWebGLLayer(uid);
            }
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