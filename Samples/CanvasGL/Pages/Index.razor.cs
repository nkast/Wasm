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
                _xr.IsSessionSupportedAsync("immersive-vr")
                    .ContinueWith((b) => _isSessionSupported = b.Result);
            }
        }

        [JSInvokable]
        public void TickDotNet()
        {
            if (cs == null)
            {
                InitCanvas();
            }

            // reset canvas
            gl.ClearColor(.39f, .58f, 0.92f, 1f);
            gl.Clear(WebGLBufferBits.COLOR | WebGLBufferBits.DEPTH | WebGLBufferBits.STENCIL);

            float aspect = (float)cs.Width / (float)cs.Height;
            Matrix4x4 world = Matrix4x4.CreateTranslation(new Vector3(0, 0, -5));
            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspect, 0.1f, 100.0f);

            Tick(world, view, proj);
        }

        private void Tick(Matrix4x4 world, Matrix4x4 view, Matrix4x4 proj)
        {
            // run gameloop tick
            TimeSpan t = _sw.Elapsed;
            TimeSpan dt = t - _prevt;
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

        private void gl_ContextLost(object sender, EventArgs e)
        {
        }

        private void gl_ContextRestored(object sender, EventArgs e)
        {
        }

        private async void InitSessionAsync()
        {
            if (!_isSessionSupported)
                return;

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

        private void _xrsession_Ended(object sender, EventArgs e)
        {
        }

        private void _xrsession_InputSourcesChanged(object sender, InputSourcesChangedEventArgs e)
        {
        }

        void OnAnimationFrame(float time, XRFrame xrFrame)
        {
            //XRRenderState renderState = xrFrame.Session.RenderState;
            XRRenderState renderState = this._xrsession.RenderState;

            XRWebGLLayer glLayer = renderState.BaseLayer;


            int w = _glLayer.FramebufferWidth;
            int h = _glLayer.FramebufferHeight;
            bool ign = _glLayer.IgnoreDepthValues;
            bool antialias = _glLayer.Antialias;
            WebGLFramebuffer glFramebuffer = _glLayer.Framebuffer;


            //XRPose pose = xrFrame.GetPose(this._localspace, this._localspace);
            //XRRigidTransform transform = pose.Transform;

            XRViewerPose viewerPose = xrFrame.GetViewerPose(this._localspace);
            bool emulatedPosition = viewerPose.EmulatedPosition;
            XRRigidTransform transform = viewerPose.Transform;

            Matrix4x4 matrix = transform.Matrix;
            //Window.Current.Document.Title = matrix.ToString();

            // reset canvas
            gl.ClearColor(.39f, .58f, 0.92f, 1f);
            gl.Clear(WebGLBufferBits.COLOR | WebGLBufferBits.DEPTH | WebGLBufferBits.STENCIL);


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


                XRInputSourceArray inputSources = _xrsession.InputSources;
                for(int i = 0; i < inputSources.Count; i++)
                {
                    XRInputSource inputSource = inputSources[i];

                    XRHandedness hand = inputSource.Handedness;

                    XRPose grip  = xrFrame.GetPose(inputSource.GripSpace, this._localspace);
                    XRPose pointer = xrFrame.GetPose(inputSource.TargetRaySpace, this._localspace);

                    XRRigidTransform gripTransform = grip.Transform;
                    Matrix4x4 gripTranformMtx = gripTransform.Matrix;

                    XRRigidTransform pointerTransform = pointer.Transform;
                    Matrix4x4 pointerTranformMtx = pointerTransform.Matrix;
                }


                Tick(world, view, proj);
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

        bool initStarted;
        private void OnMouseDown(object sender, int x, int y, int buttons)
        {
            currMouseState.Position = new Vector2(x, y);
            currMouseState.LeftButton = (buttons & 1) != 0;

            if (x < 32 && y < 32)
            {
                if (!initStarted)
                {
                    initStarted = true;
                    InitSessionAsync();
                }
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