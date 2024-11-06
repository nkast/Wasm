using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRInputSourceArray : CachedJSObject<XRInputSourceArray>
        , IReadOnlyCollection<XRInputSource>
        , IReadOnlyList<XRInputSource>
    {

        internal XRInputSourceArray(int uid) : base(uid)
        {
        }

        #region IReadOnlyList

        public XRInputSource this[int index]
        {
            get
            {
                int uid = InvokeRet<int, int>("nkJSArray.GetItem", index);
                XRInputSource inputSource = XRInputSource.FromUid(uid);
                if (inputSource != null)
                    return inputSource;

                if (uid == -1)
                    return null;

                return new XRInputSource(uid);
            }
        }

        #endregion IReadOnlyList

        #region ICollection

        public int Count
        {
            get { return InvokeRet<int>("nkJSArray.GetLength"); }
        }

        #endregion ICollection


        #region IEnumerable

        IEnumerator<XRInputSource> IEnumerable<XRInputSource>.GetEnumerator()
        {
            return new JSArrayEnumerator<XRInputSource>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<XRInputSource>)this).GetEnumerator();
        }

        #endregion IEnumerable


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
