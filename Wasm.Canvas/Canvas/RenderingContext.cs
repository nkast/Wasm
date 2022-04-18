using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas
{
    public partial class RenderingContext : JSObject, IRenderingContext
    {
        internal bool IsDisposed { get { return base.IsDisposed; } }

        public Canvas Canvas { get; private set; }

        public RenderingContext(Canvas canvas, int uid) : base(uid)
        {
            this.Canvas = canvas;
        }


        protected override void Dispose(bool disposing)
        {
            Canvas = null;
            base.Dispose(disposing);
        }
    }
}
