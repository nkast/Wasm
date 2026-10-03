using System;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPU : CachedJSObject<GPU>
    {
        private readonly int _fid_RequestAdapter;
        private readonly int _fid_RequestAdapter1;
        private readonly int _fid_GetPreferredCanvasFormat;

        public static GPU FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt(RegisterFunction("nkGPU.Create"), navigator.Uid);
            if (uid == -1)
                return null;

            GPU gpu = GPU.FromUid(uid);
            if (gpu != null)
                return gpu;

            return new GPU(navigator, uid);
        }

        internal GPU(Navigator navigator, int uid) : base(uid)
        {
            _fid_RequestAdapter = RegisterFunction("nkGPU.RequestAdapter");
            _fid_RequestAdapter1 = RegisterFunction("nkGPU.RequestAdapter1");
            _fid_GetPreferredCanvasFormat = RegisterFunction("nkGPU.GetPreferredCanvasFormat");
        }

        public Task<GPUAdapter> RequestAdapterAsync()
        {
            int uid = InvokeRetInt(_fid_RequestAdapter);

            PromiseJSObject<GPUAdapter> promise = new PromiseJSObject<GPUAdapter>(uid, (int newuid) => new GPUAdapter(newuid));
            return promise.GetTask();
        }

        public Task<GPUAdapter> RequestAdapterAsync(GPUAdapterOptions options)
        {
            int uid = InvokeRetInt<int>(_fid_RequestAdapter1, options.ToBit());

            PromiseJSObject<GPUAdapter> promise = new PromiseJSObject<GPUAdapter>(uid, (int newuid) => new GPUAdapter(newuid));
            return promise.GetTask();
        }

        /// <summary>
        /// Returns the preferred canvas format for the current platform.
        /// The value can be Rgba8Unorm or Bgra8Unorm.
        /// This is the format that should be used when configuring a GPUCanvasContext.
        /// </summary>
        /// <returns>The preferred GPUTextureFormat for the current platform.</returns>
        public GPUTextureFormat GetPreferredCanvasFormat()
        {
            int format = InvokeRetInt(_fid_GetPreferredCanvasFormat);
            GPUTextureFormat textureFormat = (GPUTextureFormat)format;

            return textureFormat;
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
