using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRInputSourceArray : JSObject
        , IReadOnlyCollection<XRInputSource>
        , IReadOnlyList<XRInputSource>
    {
        public XRInputSourceArray(int uid) : base(uid)
        {
        }

        #region IReadOnlyList

        public XRInputSource this[int index]
        {
            get
            {
                int uid = InvokeRet<int, int>("nkXRInputSourceArray.GetXRInputSource", index);
                if (XRInputSource._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRInputSource)jsObj;
                    else
                        XRInputSource._uidMap.Remove(uid);
                }

                return new XRInputSource(uid);
            }
        }

        #endregion IReadOnlyList

        #region ICollection

        public int Count
        {
            get
            {
                int count = InvokeRet<int>("nkXRInputSourceArray.GetLength");
                return count;
            }
        }

        #endregion ICollection


        #region IEnumerable

        IEnumerator<XRInputSource> IEnumerable<XRInputSource>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        #endregion IEnumerable


    }
}