using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUColorTargetState
    {
        public GPUTextureFormat Format;
        /// <summary>
        /// Channels enabled for writing. When null, defaults to <see cref="GPUColorWriteType.All"/>.
        /// </summary>
        public GPUColorWriteType? WriteMask;

        public GPUBlendState? Blend;
    }
}
