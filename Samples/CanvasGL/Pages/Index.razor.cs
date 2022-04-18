using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Numerics;
using Microsoft.AspNetCore.Components.Web;
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
                gl = cs.GetContext<IWebGLRenderingContext>();

                Window.Current.OnResize += this.OnResize;
                Window.Current.OnMouseMove += this.OnMouseMove;
                Window.Current.OnMouseDown += this.OnMouseDown;
                Window.Current.OnMouseUp += this.OnMouseUp;
                Window.Current.OnMouseWheel += this.OnMouseWheel;

                _sw.Start();
                _prevt = _sw.Elapsed;
            }

            // run gameloop tick

            var t = _sw.Elapsed;
            var dt = t - _prevt;
            _prevt = t;

            // reset canvas
            gl.ClearColor(.39f, .58f, 0.92f, 1f);
            gl.Clear(WebGLBufferBits.COLOR| WebGLBufferBits.DEPTH| WebGLBufferBits.STENCIL);
            
            // scale to virtual resolution
            var bbscalew = cs.Width / RootClip.vres.w;
            var bbscaleh = cs.Height / RootClip.vres.h;

            var uc = new UpdateContext(
                t, dt,
                currMouseState, prevMouseState,
                currTouchState, prevTouchState
                );
            prevMouseState = currMouseState;
            prevTouchState = currTouchState;
            uc.tx = uc.tx * Matrix4x4.CreateScale(bbscalew, bbscalew, 1);

            _root.Update(uc);

            var dc = new DrawContext()
            {
                GLContext = gl,
                Layer = 0,
                t  = t,
                dt = dt
            };

            for (int l = 0; l < 3; l++)
            {
                dc.Layer = l;
                _root.Draw(dc);
            }

        }

        private void OnResize(object sender)
        {
            var wnd = (Window)sender;
            var w = wnd.InnerWidth;
            var h = wnd.InnerHeight;
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

        public void OnTouchStart(TouchEventArgs e)
        {
            currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
            currTouchState.IsPressed = true;
            prevTouchState = currTouchState;
        }

        public void OnTouchMove(TouchEventArgs e)
        {
            currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
        }

        public void OnTouchEnd(TouchEventArgs e)
        {
            currTouchState.Position.X = (float)e.ChangedTouches[0].ClientX;
            currTouchState.Position.Y = (float)e.ChangedTouches[0].ClientY;
            currTouchState.IsPressed = false;
        }

        
    }
}