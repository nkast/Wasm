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
using nkast.Wasm.XR;

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

        XRSystem xr;
        XRSession xrsession;
        XRReferenceSpace localspace;
        Task<bool> isSessionSupportedPromise;
        Task<XRSession> sessionPromize;
        Task<XRReferenceSpace> localspacePromise;
        bool framerequested;

        bool doupdateRenderState;

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

                xr = XRSystem.FromNavigator(Window.Current.Navigator);

                if (xr != null)
                {
                    //isSessionSupportedPromise = xr.IsSessionSupportedAsync("immersive-vr");
                    isSessionSupportedPromise = xr.IsSessionSupportedAsync("immersive-vr");

                }

                _sw.Start();
                _prevt = _sw.Elapsed;
            }

            if (isSessionSupportedPromise != null && isSessionSupportedPromise.IsCompletedSuccessfully)
            {
                bool isSessionSupported = isSessionSupportedPromise.Result;

                if (isSessionSupported == true)
                {
                    if (sessionPromize == null)
                    {
                        sessionPromize = xr.RequestSessionAsync("immersive-vr");
                        //sessionPromize = xr.RequestSessionAsync("inline");
                    }

                    if (sessionPromize != null && sessionPromize.IsCompletedSuccessfully)
                    {
                        xrsession = sessionPromize.Result;

                        if (!doupdateRenderState)
                        {
                            //xrsession.updateRenderState({ baseLayer: new XRWebGLLayer(session, gl) });
                            //xrsession.UpdateRenderState();
                            doupdateRenderState = true;
                        }

                        if (localspacePromise == null)
                        {
                            localspacePromise = xrsession.RequestReferenceSpace("local");
                        }

                        if (localspacePromise != null && localspacePromise.IsCompletedSuccessfully)
                        {
                            localspace = localspacePromise.Result;

                            if (!framerequested)
                            {
                                int handle = xrsession.RequestAnimationFrame(OnAnimationFrame);
                                framerequested = true;
                            }
                        }
                    }
                }
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

        void OnAnimationFrame(float time, XRFrame xrFrame)
        {
            var viewer = xrFrame.GetViewerPose(this.localspace);

        }

        private void OnResize(object sender)
        {
            var wnd = (Window)sender;
            var w = wnd.InnerWidth;
            var h = wnd.InnerHeight;
        }

        private void OnFocus(object sender)
        {
            var wnd = (Window)sender;
            var hasFocus = true;
        }

        private void OnBlur(object sender)
        {
            var wnd = (Window)sender;
            var hasFocus = false;
        }

        private void OnMouseMove(object sender, int x, int y)
        {
            currMouseState.Position = new Vector2(x, y);
        }

        private void OnMouseDown(object sender, int x, int y, int buttons)
        {
            currMouseState.Position = new Vector2(x, y);
            currMouseState.LeftButton = (buttons & 1) != 0;

            if (x < 20 && y < 20)
            {
                xrsession.RequestAnimationFrame(OnAnimationFrame);
            }
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