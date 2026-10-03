using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUAdapterInfo : CachedJSObject<GPUAdapterInfo>
    {

        public string Device
        {
            get { return InvokeRetString(RegisterFunction("nkGPUAdapterInfo.GetDevice")); }
        }

        public string Description
        {
            get { return InvokeRetString(RegisterFunction("nkGPUAdapterInfo.GetDescription")); }
        }

        public string Vendor
        {
            get { return InvokeRetString(RegisterFunction("nkGPUAdapterInfo.GetVendor")); }
        }

        public string Architecture
        {
            get { return InvokeRetString(RegisterFunction("nkGPUAdapterInfo.GetArchitecture")); }
        }

        internal GPUAdapterInfo(int uid) : base(uid)
        {
        }

    }
}
