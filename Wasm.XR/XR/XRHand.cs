using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRHand : CachedJSObject<XRHand>
        , IReadOnlyDictionary<string, XRJointSpace>
    {

        internal XRHand(int uid) : base(uid)
        {
        }

        public XRJointSpace this[string key]
        {
            get
            {
                int uid = InvokeRet<String, int>("nkXRHand.Get", key);
                if (uid == -1)
                    return null;

                XRJointSpace jointSpace = (XRJointSpace)XRJointSpace.FromUid(uid);
                if (jointSpace != null)
                    return jointSpace;

                return new XRJointSpace(uid);
            }
        }

        IEnumerable<string> IReadOnlyDictionary<string, XRJointSpace>.Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerable<XRJointSpace> IReadOnlyDictionary<string, XRJointSpace>.Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool IReadOnlyDictionary<string, XRJointSpace>.ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        bool IReadOnlyDictionary<string, XRJointSpace>.TryGetValue(string key, out XRJointSpace value)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return InvokeRet<int>("nkXRHand.GetSize"); }
        }

        IEnumerator<KeyValuePair<string, XRJointSpace>> IEnumerable<KeyValuePair<string, XRJointSpace>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
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