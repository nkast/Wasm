using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace nkast.Wasm.Dom
{
    public partial class CachedJSObject<TJSObject> : JSObject
        where TJSObject : JSObject
    {
        private static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public CachedJSObject(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
        }

        static public T FromUid<T>(int uid)
            where T : TJSObject
        {
            if (_uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (T)jsObj;

            return null;
        }

        static public TJSObject FromUid(int uid)
        {
            if (_uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (TJSObject)jsObj;

            return null;
        }

        #region implement IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }

        #endregion

    }
}
