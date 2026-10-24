using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasTexture : CachedJSObject<GPUCanvasTexture>, IGPUTexture
    {
        private readonly int _fid_GetWidth;
        private readonly int _fid_GetHeight;
        private readonly int _fid_GetDepthOrArrayLayers;
        private readonly int _fid_GetMipLevelCount;
        private readonly int _fid_GetSampleCount;
        private readonly int _fid_GetDimension;
        private readonly int _fid_GetFormat;
        private readonly int _fid_GetUsage;
        private readonly int _fid_CreateView;
        private readonly int _fid_CreateView1;

        public GPUDevice _device;

        internal GPUCanvasTexture(int uid, GPUDevice device) : base(uid)
        {
            _fid_GetWidth = RegisterFunction("nkGPUTexture.GetWidth");
            _fid_GetHeight = RegisterFunction("nkGPUTexture.GetHeight");
            _fid_GetDepthOrArrayLayers = RegisterFunction("nkGPUTexture.GetDepthOrArrayLayers");
            _fid_GetMipLevelCount = RegisterFunction("nkGPUTexture.GetMipLevelCount");
            _fid_GetSampleCount = RegisterFunction("nkGPUTexture.GetSampleCount");
            _fid_GetDimension = RegisterFunction("nkGPUTexture.GetDimension");
            _fid_GetFormat = RegisterFunction("nkGPUTexture.GetFormat");
            _fid_GetUsage = RegisterFunction("nkGPUTexture.GetUsage");
            _fid_CreateView = RegisterFunction("nkGPUTexture.CreateView");
            _fid_CreateView1 = RegisterFunction("nkGPUTexture.CreateView1");
            _device = device;
        }

        public int Width
        {
            get { return InvokeRetInt(_fid_GetWidth); }
        }

        public int Height
        {
            get { return InvokeRetInt(_fid_GetHeight); }
        }

        public int DepthOrArrayLayers
        {
            get { return InvokeRetInt(_fid_GetDepthOrArrayLayers); }
        }

        public int MipLevelCount
        {
            get { return InvokeRetInt(_fid_GetMipLevelCount); }
        }

        public int SampleCount
        {
            get { return InvokeRetInt(_fid_GetSampleCount); }
        }

        public GPUTextureDimensionType Dimension
        {
            get { return (GPUTextureDimensionType)InvokeRetInt(_fid_GetDimension); }
        }

        public GPUTextureFormat Format
        {
            get { return (GPUTextureFormat)InvokeRetInt(_fid_GetFormat); }
        }

        public GPUTextureUsageType Usage
        {
            get { return (GPUTextureUsageType)InvokeRetInt(_fid_GetUsage); }
        }
        public GPUTextureView CreateView()
        {
            int uid = InvokeRetInt(_fid_CreateView);
            return new GPUTextureView(uid, _device);
        }

        public GPUTextureView CreateView(GPUTextureViewDescriptor descriptor)
        {
            int uid = InvokeRetInt<int, int, int, int, int, int, int, int, int>(
                _fid_CreateView1,
                descriptor.BaseArrayLayer,
                descriptor.BaseMipLevel,
                (int)descriptor.Aspect,
                descriptor.Swizzle?.Pack() ?? -1,
                descriptor.ArrayLayerCount ?? -1,
                (int?)descriptor.Dimension ?? -1,
                (int?)descriptor.Format ?? -1,
                descriptor.MipLevelCount ?? -1,
                (int?)descriptor.Usage ?? -1);
            return new GPUTextureView(uid, _device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Invoke(RegisterFunction("nkGPUTexture.Destroy"));

            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
