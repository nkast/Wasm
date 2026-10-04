using System;

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
