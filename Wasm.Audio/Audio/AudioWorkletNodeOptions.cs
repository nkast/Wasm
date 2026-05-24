using System;

namespace nkast.Wasm.Audio
{
    public struct AudioWorkletNodeOptions
    {
        public int? NumberOfInputs;
        public int? NumberOfOutputs;
        public int[] OutputChannelCount;
    }
}
