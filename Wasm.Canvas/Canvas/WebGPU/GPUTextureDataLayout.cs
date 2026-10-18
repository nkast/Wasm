using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUTextureDataLayout
    {
        public long Offset;
        public int? BytesPerRow;
        public int? RowsPerImage;
    }
}
