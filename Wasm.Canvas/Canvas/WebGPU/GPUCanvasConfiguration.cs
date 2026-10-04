using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUCanvasConfiguration
    {
        public enum CanvasAlphaModeType
        {
            Opaque = 1,
            Premultiplied = 2,
        }

        public enum CanvasToneMappingModeType
        {
            Standard = 1,
            Extended = 2,
        }

        public GPUDevice Device { get; set; }
        /// <summary>
        /// The format that textures returned by getCurrentTexture() will have. 
        /// This can be Rgba8Unorm, Bgra8Unorm, or Rgba16Float.
        /// for best performance it is recommended to use the texture format returned by GPU.GetPreferredCanvasFormat().
        /// </summary>
        public GPUTextureFormat Format { get; set; }
        
        public CanvasAlphaModeType? AlphaMode { get; set; }

        public CanvasToneMappingModeType? ToneMappingMode { get; set; }

        public GPUColorSpaceType? ColorSpace { get; set; }

        public GPUTextureUsageType? Usage { get; set; }

    }
}
