using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUAdapter : JSObject
    {
        internal GPUAdapter(int uid) : base(uid)
        {
        }

        public GPUAdapterInfo GetInfo()
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPUAdapter.GetInfo"));

            GPUAdapterInfo info = GPUAdapterInfo.FromUid(uid);
            if (info != null)
                return info;

            return new GPUAdapterInfo(uid);
        }
    }
}
