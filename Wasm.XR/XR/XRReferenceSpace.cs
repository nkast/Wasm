using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRReferenceSpace : XRSpace
    {
        public XRReferenceSpace(int uid) : base(uid)
        {
        }
    }
}
