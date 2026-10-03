using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUAdapterInfo : CachedJSObject<GPUAdapterInfo>
    {
        private readonly int _fid_GetDevice;
        private readonly int _fid_GetDescription;
        private readonly int _fid_GetVendor;
        private readonly int _fid_GetArchitecture;


        public string Device
        {
            get { return InvokeRetString(_fid_GetDevice); }
        }

        public string Description
        {
            get { return InvokeRetString(_fid_GetDescription); }
        }

        public string Vendor
        {
            get { return InvokeRetString(_fid_GetVendor); }
        }

        public string Architecture
        {
            get { return InvokeRetString(_fid_GetArchitecture); }
        }

        internal GPUAdapterInfo(int uid) : base(uid)
        {
            _fid_GetDevice = RegisterFunction("nkGPUAdapterInfo.GetDevice");
            _fid_GetDescription = RegisterFunction("nkGPUAdapterInfo.GetDescription");
            _fid_GetVendor = RegisterFunction("nkGPUAdapterInfo.GetVendor");
            _fid_GetArchitecture = RegisterFunction("nkGPUAdapterInfo.GetArchitecture");
        }

    }
}
