using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUBlendComponent : IEquatable<GPUBlendComponent>
    {
        /// <summary>Blend operation. The default is <see cref="GPUBlendOperationType.Add"/>.</summary>
        public GPUBlendOperationType Operation;

        /// <summary>Source factor. The default is <see cref="GPUBlendFactorType.One"/>.</summary>
        public GPUBlendFactorType SrcFactor;

        /// <summary>Destination factor. The default is <see cref="GPUBlendFactorType.Zero"/>.</summary>
        public GPUBlendFactorType DstFactor;

        public override int GetHashCode()
        {
            return HashCode.Combine(Operation, SrcFactor, DstFactor);
        }

        public bool Equals(GPUBlendComponent other)
        {
            return this.Operation == other.Operation
                && this.SrcFactor == other.SrcFactor
                && this.DstFactor == other.DstFactor;
        }

        public override bool Equals(object obj)
        {
            return (obj is GPUBlendComponent other) && Equals(other);
        }


        public static bool operator ==(GPUBlendComponent a, GPUBlendComponent b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(GPUBlendComponent a, GPUBlendComponent b)
        {
            return !a.Equals(b);
        }
    }
}
