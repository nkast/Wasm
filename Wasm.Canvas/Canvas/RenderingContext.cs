using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas
{
    public partial class RenderingContext : JSObject, IRenderingContext
    {
        private bool _isDisposed;

        internal bool IsDisposed { get { return _isDisposed; } }

        public Canvas Canvas { get; private set; }

        public RenderingContext(Canvas canvas, int uid) : base(uid)
        {
            this.Canvas = canvas;
        }


        protected override void Dispose(bool disposing)
        {
            _isDisposed = true;

            Canvas = null;
            base.Dispose(disposing);
        }
    }
}
