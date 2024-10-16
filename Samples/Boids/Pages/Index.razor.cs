using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;
using nkast.Wasm.Canvas;
using Boids;
using Boids.Engine;

namespace Boids.Pages
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
        ICanvasRenderingContext cx;

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
                attribs.Alpha = true;
                attribs.Desynchronized = null;
                cx = cs.GetContext<ICanvasRenderingContext>(attribs);

                Window.Current.OnResize += this.OnResize;
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

            TimeSpan  t = _sw.Elapsed;
            TimeSpan dt = t - _prevt;
            _prevt = t;

            // reset canvas
            cx.SetTransform(1, 0, 0, 1, 0, 0);
            cx.ClearRect(0, 0, cs.Width, cs.Height);
            
            // scale to virtual resolution
            float bbscalew = cs.Width / RootClip.vres.w;
            float bbscaleh = cs.Height / RootClip.vres.h;
            cx.Scale(bbscalew, bbscalew);

            UpdateContext uc = new UpdateContext(
                t, dt,
                currMouseState, prevMouseState,
                currTouchState, prevTouchState
                );
            prevMouseState = currMouseState;
            prevTouchState = currTouchState;
            uc.tx = uc.tx * Matrix4x4.CreateScale(bbscalew, bbscalew, 1);

            _root.Update(uc);

            DrawContext dc = new DrawContext()
            {
                CanvasContext = cx,
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
            Window wnd = (Window)sender;
            int w = wnd.InnerWidth;
            int h = wnd.InnerHeight;
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