using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GPUPrimitiveState
    {
        private int _topology;
        private int _frontFace;
        private int _cullMode;
        private int _unclippedDepth;
        private int _stripIndexFormat; // 0 = undefined

        public GPUPrimitiveTopologyType Topology
        {
            get { return (GPUPrimitiveTopologyType)_topology; }
            set { _topology = (int)value; }
        }

        public GPUFrontFaceType FrontFace
        {
            get { return (GPUFrontFaceType)_frontFace; }
            set { _frontFace = (int)value; }
        }

        public GPUCullModeType CullMode
        {
            get { return (GPUCullModeType)_cullMode; }
            set { _cullMode = (int)value; }
        }

        public bool UnclippedDepth
        {
            get { return _unclippedDepth != 0; }
            set { _unclippedDepth = value ? 1 : 0; }
        }

        public GPUIndexFormatType? StripIndexFormat
        {
            get { return (_stripIndexFormat != 0) ? (GPUIndexFormatType?)_stripIndexFormat : null; }
            set { _stripIndexFormat = value.HasValue ? (int)value.Value : 0; }
        }
    }
}