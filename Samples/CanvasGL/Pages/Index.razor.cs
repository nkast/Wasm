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
using nkast.Wasm.XR;
using nkast.Wasm.Input;

namespace CanvasGL.Pages
{
    public partial class Index
    {
        Stopwatch _sw = new Stopwatch();
        TimeSpan _prevt;

        RootClip _root;

        Canvas cs;
        IWebGLRenderingContext gl;

        MouseState currMouseState;
        MouseState prevMouseState;
        TouchState currTouchState;
        TouchState prevTouchState;

        XRSystem _xr;
        XRSession _xrsession;
        XRReferenceSpace _localspace;
        XRWebGLLayer _glLayer;
        bool _isSessionSupported;

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

        private void InitCanvas()
        {
            cs = Window.Current.Document.GetElementById<Canvas>("theCanvas");
            ContextAttributes attribs = new ContextAttributes();
            attribs.Depth = true;
            //attribs.XrCompatible = true;
            gl = cs.GetContext<IWebGLRenderingContext>(attribs);
            gl.ContextLost += gl_ContextLost;
            gl.ContextRestored += gl_ContextRestored;

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

            _xr = XRSystem.FromNavigator(Window.Current.Navigator);
            if (_xr != null)
            {
                //_xr.IsSessionSupportedAsync("immersive-ar")
                _xr.IsSessionSupportedAsync("immersive-vr")
                    .ContinueWith((b) => _isSessionSupported = b.Result);
            }
        }

        [JSInvokable]
        public void TickDotNet()
        {
            if (_initXRStarted)
                return;

            if (cs == null)
            {
                InitCanvas();
            }

            // reset canvas
            //gl.ClearColor(.39f, _g, 0.92f, 1f);
            //gl.Clear(WebGLBufferBits.COLOR | WebGLBufferBits.DEPTH | WebGLBufferBits.STENCIL);

            TimeSpan t, dt;
            DoUpdate(out t, out dt);

            float aspect = (float)cs.Width / (float)cs.Height;
            Matrix4x4 world = Matrix4x4.CreateTranslation(new Vector3(0, 0, -2f));
            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspect, 0.1f, 100.0f);

            DoDraw(t, dt, world, view, proj);
        }

        private void DoUpdate(out TimeSpan t, out TimeSpan dt)
        {
            // run gameloop tick
            t = _sw.Elapsed;
            dt = t - _prevt;
            _prevt = t;

            UpdateContext uc = new UpdateContext(
                t, dt,
                currMouseState, prevMouseState,
                currTouchState, prevTouchState
                );
            prevMouseState = currMouseState;
            prevTouchState = currTouchState;

            // scale to virtual resolution
            float bbscalew = cs.Width / RootClip.vres.w;
            float bbscaleh = cs.Height / RootClip.vres.h;
            uc.tx = uc.tx * Matrix4x4.CreateScale(bbscalew, bbscalew, 1);

            _root.Update(uc);
        }

        private void DoDraw(TimeSpan t, TimeSpan dt, Matrix4x4 world, Matrix4x4 view, Matrix4x4 proj)
        {
            DrawContext dc = new DrawContext()
            {
                GLContext = gl,
                Layer = 0,
                t = t,
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


        private async void InitSessionAsync()
        {
            if (!_isSessionSupported)
                return;

            //_xrsession = await _xr.RequestSessionAsync("immersive-ar");
            _xrsession = await _xr.RequestSessionAsync("immersive-vr");
            _xrsession.Ended += _xrsession_Ended;
            _xrsession.InputSourcesChanged += _xrsession_InputSourcesChanged;
            await gl.MakeXRCompatibleAsync();
            _localspace = await _xrsession.RequestReferenceSpace("local");

            _glLayer = new XRWebGLLayer(_xrsession, gl);
            RenderStateAttributes attribs = new RenderStateAttributes();
            attribs.BaseLayer = _glLayer;
            _xrsession.UpdateRenderState(attribs);

            // test XRWebGLLayer
            {
                int w = _glLayer.FramebufferWidth;
                int h = _glLayer.FramebufferHeight;
                bool ign = _glLayer.IgnoreDepthValues;
                bool antialias = _glLayer.Antialias;
                WebGLFramebuffer glFramebufferNil = _glLayer.Framebuffer;
            }

            int handle = _xrsession.RequestAnimationFrame(OnAnimationFrame);
        }

        private void gl_ContextLost(object sender, EventArgs e)
        {
            Console.WriteLine("gl_ContextLost");
        }

        private void gl_ContextRestored(object sender, EventArgs e)
        {
            Console.WriteLine("gl_ContextRestored");
        }

        private void _xrsession_Ended(object sender, EventArgs e)
        {
            Console.WriteLine("_xrsession_Ended");
        }

        private void _xrsession_InputSourcesChanged(object sender, InputSourcesChangedEventArgs e)
        {
            Console.WriteLine("_xrsession_InputSourcesChanged");
        }

        void OnAnimationFrame(float time, XRFrame xrFrame)
        {
            //XRRenderState renderState = xrFrame.Session.RenderState;
            XRRenderState renderState = this._xrsession.RenderState;

            XRViewerPose viewerPose = xrFrame.GetViewerPose(this._localspace);
            if (viewerPose == null)
            {
                // Request next frame
                int handle2 = _xrsession.RequestAnimationFrame(OnAnimationFrame);
                return;
            }

            bool emulatedPosition = viewerPose.EmulatedPosition;
            XRRigidTransform transform = viewerPose.Transform;

            XRWebGLLayer glLayer = renderState.BaseLayer;
            int w = glLayer.FramebufferWidth;
            int h = glLayer.FramebufferHeight;
            bool ign = glLayer.IgnoreDepthValues;
            bool antialias = glLayer.Antialias;
            WebGLFramebuffer glFramebuffer = glLayer.Framebuffer;

            if (glFramebuffer != null)
            {
                _g = 0.2f;
                gl.BindFramebuffer(WebGLFramebufferType.FRAMEBUFFER, glFramebuffer);
            }
            else
            {
                _g = 0.8f;
            }


            TimeSpan t, dt;
            DoUpdate(out t, out dt);

            // reset canvas
            //gl.ClearColor(.39f, .58f, 0.92f, 1f);
            //gl.ClearColor(.2f, _g, 0.2f, 1f);
            //gl.Clear(WebGLBufferBits.COLOR | WebGLBufferBits.DEPTH | WebGLBufferBits.STENCIL);


            foreach (XRView xrView in viewerPose.Views)
            {
                XREye eye = xrView.Eye;

                XRViewport xrViewport = glLayer.GetViewport(xrView);
                gl.Viewport(xrViewport.X, xrViewport.Y, xrViewport.Width, xrViewport.Height);

                XRRigidTransform vTransform = xrView.Transform;
                float aspect = (float)xrViewport.Width / (float)xrViewport.Height;
                Matrix4x4 world = Matrix4x4.CreateTranslation(new Vector3(0, 0, -5));
                Matrix4x4 view = vTransform.Inverse.Matrix;
                Matrix4x4 proj = xrView.ProjectionMatrix;

                DoDraw(t, dt, world, view, proj);


                XRInputSourceArray inputSources = _xrsession.InputSources;
                for (int i = 0; i < inputSources.Count; i++)
                {
                    XRInputSource inputSource = inputSources[i];

                    XRHandedness hand = inputSource.Handedness;

                    Gamepad gamepad = inputSource.Gamepad;

                    XRSpace gripSpace = inputSource.GripSpace;
                    if (gripSpace != null)
                    {
                        XRPose grip = xrFrame.GetPose(gripSpace, this._localspace);
                        if (grip != null)
                        {
                            XRRigidTransform gripTransform = grip.Transform;
                            Matrix4x4 gripTranformMtx = gripTransform.Matrix;
                        }
                    }

                    XRSpace pointerSpace = inputSource.TargetRaySpace;
                    if (pointerSpace != null)
                    {
                        XRPose pointer = xrFrame.GetPose(pointerSpace, this._localspace);
                        if (pointer != null)
                        {
                            XRRigidTransform pointerTransform = pointer.Transform;
                            Matrix4x4 pointerTranformMtx = pointerTransform.Matrix;

                            // draw pointer
                            Matrix4x4 pointerMtx = Matrix4x4.Identity;
                            pointerMtx *= Matrix4x4.CreateRotationX(-(float)Math.Tau / 4f);
                            pointerMtx *= Matrix4x4.CreateScale(0.2f, 1.0f, 1.0f);

                            if (gamepad != null)
                            {
                                GamepadButton[] buttons = gamepad.Buttons;

                                if (buttons[0].Pressed) // 0 = trigger
                                {
                                    pointerMtx *= Matrix4x4.CreateScale(1.0f, 1.0f, 1.5f);
                                    gamepad.VibrationActuator.Pulse(1.0f, 100);
                                }
                                else if (buttons[0].Touched) // 0 = trigger
                                {
                                    pointerMtx *= Matrix4x4.CreateScale(1.0f, 1.0f, 1.5f);
                                }
                            }

                            pointerMtx *= Matrix4x4.CreateScale(0.1f);

                            pointerMtx *= pointerTranformMtx;
                            DrawContext dcp = new DrawContext()
                            {
                                GLContext = gl,
                                Layer = 0,
                                t = t,
                                dt = dt,
                                world = pointerMtx,
                                view = view,
                                proj = proj,
                            };
                            _root._tri.Draw(dcp);
                        }
                    }
                }




            }

            // Request next frame
            int handle = _xrsession.RequestAnimationFrame(OnAnimationFrame);
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

        float _g = .58f;
        bool _initXRStarted;
        private void OnClick(int x, int y)
        {
            if (x < 64 && y < 64)
            {
                Window.Current.Navigator.Vibrate(TimeSpan.FromMilliseconds(100));

                if (!_initXRStarted)
                {
                    _initXRStarted = true;

                    _g = _g + 0.3f;
                    while (_g > 1)
                        _g -= 1f;

                    InitSessionAsync();
                }
            }
        }

        private void OnMouseDown(object sender, int x, int y, int buttons)
        {
            currMouseState.Position = new Vector2(x, y);
            currMouseState.LeftButton = (buttons & 1) != 0;

            OnClick(x, y);
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

            OnClick((int)x, (int)y);
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