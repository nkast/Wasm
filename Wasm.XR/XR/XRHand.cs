using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRHand : CachedJSObject<XRHand>
        , IReadOnlyDictionary<string, XRJointSpace>
    {
        private readonly int _fid_Get;
        private readonly int _fid_Get1;
        private readonly int _fid_GetSize;


        internal XRHand(int uid) : base(uid)
        {
            _fid_Get = RegisterFunction("nkXRHand.Get");
            _fid_Get1 = RegisterFunction("nkXRHand.Get1");
            _fid_GetSize = RegisterFunction("nkXRHand.GetSize");
        }

        public XRJointSpace this[string key]
        {
            get
            {
                int uid = InvokeRetInt<String>(_fid_Get, key);
                if (uid == -1)
                    return null;

                XRJointSpace jointSpace = (XRJointSpace)XRJointSpace.FromUid(uid);
                if (jointSpace != null)
                    return jointSpace;

                return new XRJointSpace(uid);
            }
        }

        public XRJointSpace this[XRHandJointType key]
        {
            get
            {
                int uid = InvokeRetInt<XRHandJointType>(_fid_Get1, key);
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
            get { return InvokeRetInt(_fid_GetSize); }
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