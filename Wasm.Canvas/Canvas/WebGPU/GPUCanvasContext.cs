using System;
using System.Diagnostics;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasContext : RenderingContext, IGPUCanvasContext
    {
        private readonly int _fid_Configure;
        private readonly int _fid_GetConfiguration;
        private readonly int _fid_GetCurrentTexture;
        private readonly int _fid_Unconfigure;

        private GPUDevice _device;

        internal GPUCanvasContext(Canvas canvas, int uid) : base(canvas, uid)
        {
            _fid_Configure = RegisterFunction("nkGPUCanvasContext.Configure");
            _fid_GetConfiguration = RegisterFunction("nkGPUCanvasContext.GetConfiguration");
            _fid_GetCurrentTexture = RegisterFunction("nkGPUCanvasContext.GetCurrentTexture");
            _fid_Unconfigure = RegisterFunction("nkGPUCanvasContext.Unconfigure");
        }

        public unsafe void Configure(GPUCanvasConfiguration configuration)
        {
            GPUCanvasConfigurationData data = new GPUCanvasConfigurationData();
            data.DeviceUid       = configuration.Device.Uid;
            data.Format          = (int)configuration.Format;
            data.AlphaMode       = ((int?)configuration.AlphaMode) ?? -1;
            data.ToneMappingMode = ((int?)configuration.ToneMappingMode) ?? -1;
            data.ColorSpace      = ((int?)configuration.ColorSpace) ?? -1;
            data.Usage           = ((int?)configuration.Usage) ?? -1;
            Invoke<IntPtr>(_fid_Configure, new IntPtr(&data));
            _device = configuration.Device;
        }

        public unsafe GPUCanvasConfiguration GetConfiguration()
        {
            GPUCanvasConfigurationData data = new GPUCanvasConfigurationData();
            bool configured = InvokeRetBool<IntPtr>(_fid_GetConfiguration, new IntPtr(&data));
            if (!configured)
                return null;

            GPUCanvasConfiguration configuration = new GPUCanvasConfiguration();
            configuration.Device          = GPUDevice.FromUid(data.DeviceUid);
            Debug.Assert(configuration.Device == _device);
            configuration.Format          = (GPUTextureFormat)data.Format;
            configuration.AlphaMode       = (data.AlphaMode == -1) ? null : (GPUCanvasConfiguration.CanvasAlphaModeType)data.AlphaMode;
            configuration.ToneMappingMode = (data.ToneMappingMode == -1) ? null : (GPUCanvasConfiguration.CanvasToneMappingModeType)data.ToneMappingMode;
            configuration.ColorSpace      = (data.ColorSpace == -1) ? null : (GPUColorSpaceType)data.ColorSpace;
            configuration.Usage           = (data.Usage == -1) ? null : (GPUTextureUsageType)data.Usage;

            
            return configuration;
        }

        public IGPUTexture GetCurrentTexture()
        {
            int uid = InvokeRetInt(_fid_GetCurrentTexture);
            return new GPUCanvasTexture(uid, _device);
        }

        public void Unconfigure()
        {
            Invoke(_fid_Unconfigure);
            _device = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
