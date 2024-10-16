using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGL;
using CanvasGL;
using CanvasGL.Engine;

namespace CanvasGL.Pages
{
    public partial class Index
    {
        Stopwatch _sw = new Stopwatch();
        TimeSpan _prevt;

        RootClip _root;

        // Summary:
        //     Method invoked when the component is ready to start, having received its initial
        //     parameters from its parent in the render tree.
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                Init();

                JsRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            }
        }

        private void Init()
        {
            _root = new RootClip();
        }

        Canvas cs;
        IWebGLRenderingContext gl;

        MouseState currMouseState;
        MouseState prevMouseState;
        TouchState currTouchState;
        TouchState prevTouchState;

        [JSInvokable]
        public void TickDotNet()
        {
            if (cs == null)
            {
                cs = Window.Current.Document.GetElementById<Canvas>("theCanvas");
                ContextAttributes attribs = new ContextAttributes();
                attribs.Depth = true;
                gl = cs.GetContext<IWebGLRenderingContext>(attribs);

                Window.Current.OnResize += this.OnResize;
                Window.Current.OnFocus += this.OnFocus;
                Window.Current.OnBlur += this.OnBlur;
                Window.Current.OnMouseMove += this.OnMouseMove;
                Window.Current.OnMouseDown += this.OnMouseDown;
                Window.Current.OnMouseUp += this.OnMouseUp;
                Window.Current.OnMouseWheel += this.OnMouseWheel;

                Window.Current.OnTouchStart += this.OnTouchStart;
                Window.Current.OnTouchMove += this.OnTouchMove;
                Window.Current.OnTouchEnd += this.OnTouchEnd;

                _sw.Start();
                _prevt = _sw.Elapsed;
            }

            // run gameloop tick

            TimeSpan t  = _sw.Elapsed;
            TimeSpan dt = t - _prevt;
            _prevt = t;

            // reset canvas
            gl.ClearColor(.39f, .58f, 0.92f, 1f);
            gl.Clear(WebGLBufferBits.COLOR | WebGLBufferBits.DEPTH | WebGLBufferBits.STENCIL);

            // scale to virtual resolution
            float bbscalew = cs.Width / RootClip.vres.w;
            float bbscaleh = cs.Height / RootClip.vres.h;

            UpdateContext uc = new UpdateContext(
                t, dt,
                currMouseState, prevMouseState,
                currTouchState, prevTouchState
                );
            prevMouseState = currMouseState;
            prevTouchState = currTouchState;
            uc.tx = uc.tx * Matrix4x4.CreateScale(bbscalew, bbscalew, 1);

            _root.Update(uc);

            float aspect = (float)cs.Width / (float)cs.Height;
            Matrix4x4 world = Matrix4x4.CreateTranslation(new Vector3(0, 0, -5));
            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspect, 0.1f, 100.0f);

            DrawContext dc = new DrawContext()
            {
                GLContext = gl,
                Layer = 0,
                t  = t,
                dt = dt,
                world = world,
                view = view,
                proj = proj,
            };

            for (int l = 0; l < 3; l++)
            {
                dc.Layer = l;
                _root.Draw(dc);
            }

        }

        private void OnResize(object sender)
        {
            Window wnd = (Window)sender;
            int w = wnd.InnerWidth;
            int h = wnd.InnerHeight;
        }

        private void OnFocus(object sender)
        {
            Window wnd = (Window)sender;
            bool hasFocus = true;
        }

        private void OnBlur(object sender)
        {
            Window wnd = (Window)sender;
            bool hasFocus = false;
        }

        private void OnMouseMove(object sender, int x, int y)
        {
            currMouseState.Position = new Vector2(x, y);
        }

        private void OnMouseDown(object sender, int x, int y, int buttons)
        {
            currMouseState.Position = new Vector2(x, y);
            currMouseState.LeftButton = (buttons & 1) != 0;
        }

        private void OnMouseUp(object sender, int x, int y, int buttons)
        {
            currMouseState.Position = new Vector2(x, y);
            currMouseState.LeftButton = (buttons & 1) != 0;
        }

        public void OnMouseWheel(object sender, int deltaX, int deltaY, int deltaZ, int deltaMode)
        {
            currMouseState.Wheel += (float)deltaY;
        }

        private void OnTouchStart(object sender, float x, float y, int identifier)
        {
            currTouchState.Position.X = x;
            currTouchState.Position.Y = y;
            currTouchState.IsPressed = true;
            prevTouchState = currTouchState;
        }

        private void OnTouchMove(object sender, float x, float y, int identifier)
        {
            currTouchState.Position.X = x;
            currTouchState.Position.Y = y;
        }

        private void OnTouchEnd(object sender, float x, float y, int identifier)
        {
            currTouchState.Position.X = x;
            currTouchState.Position.Y = y;
            currTouchState.IsPressed = false;
        }

        
    }
}