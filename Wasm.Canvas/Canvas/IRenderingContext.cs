using System;
using System.Collections.Generic;

namespace nkast.Wasm.Canvas
{
    public interface IRenderingContext : IDisposable
    {
        public Canvas Canvas { get; }
    }
}
