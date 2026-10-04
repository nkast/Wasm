using System;
using System.Diagnostics;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasContext : RenderingContext, IGPUCanvasContext
    {
        private GPUDevice _device;

        internal GPUCanvasContext(Canvas canvas, int uid) : base(canvas, uid)
        {
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
            Invoke<IntPtr>(RegisterFunction("nkGPUCanvasContext.Configure"), new IntPtr(&data));
            _device = configuration.Device;
        }

        public unsafe GPUCanvasConfiguration GetConfiguration()
        {
            GPUCanvasConfigurationData data = new GPUCanvasConfigurationData();
            bool configured = InvokeRetBool<IntPtr>(RegisterFunction("nkGPUCanvasContext.GetConfiguration"), new IntPtr(&data));
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

        public void Unconfigure()
        {
            Invoke(RegisterFunction("nkGPUCanvasContext.Unconfigure"));
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
