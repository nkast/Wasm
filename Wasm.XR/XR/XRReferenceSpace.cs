using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRReferenceSpace : JSObject
    {
        public XRReferenceSpace(int uid) : base(uid)
        {
        }
    }

}
