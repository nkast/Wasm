using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct GPUColorTargetStateData
    {
        public int Format;
        public int WriteMask;
        public int HasBlend;
        public GPUBlendComponent BlendColor;
        public GPUBlendComponent BlendAlpha;

        internal bool Equals(GPUColorTargetStateData other)
        {
            return this.Format == other.Format
                && this.WriteMask == other.WriteMask
                && this.HasBlend == other.HasBlend
                && this.BlendColor == other.BlendColor
                && this.BlendAlpha == other.BlendAlpha;
        }

        internal static GPUColorTargetStateData FromState(GPUColorTargetState state)
        {
            GPUColorTargetStateData data = new GPUColorTargetStateData();
            data.Format    = (int)state.Format;
            data.WriteMask = ((int?)state.WriteMask) ?? -1;
            if (state.Blend.HasValue)
            {
                data.HasBlend   = 1;
                data.BlendColor = state.Blend.Value.Color;
                data.BlendAlpha = state.Blend.Value.Alpha;
            }
            return data;
        }

        internal GPUColorTargetState ToState()
        {
            GPUColorTargetState state = new GPUColorTargetState();
            state.Format    = (GPUTextureFormat)this.Format;
            state.WriteMask = (this.WriteMask == -1) ? null : (GPUColorWriteType?)this.WriteMask;
            if (this.HasBlend != 0)
            {
                GPUBlendState blend = new GPUBlendState();
                blend.Color = this.BlendColor;
                blend.Alpha = this.BlendAlpha;
                state.Blend = blend;
            }
            return state;
        }
    }
}
