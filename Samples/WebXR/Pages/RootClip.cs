﻿using System.Numerics;
using WebXR.Engine;
using nkast.Wasm.Dom;
using nkast.Wasm.Input;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.XR;

namespace WebXR.Pages
{
    internal class RootClip : Clip
    {
        XRSession.XRAnimationFrameCallback _xrAnimationFrameCallback;

        public static Size vres = new Size(1920, 960);

        TriangleClip _tri;


        public RootClip(XRSession.XRAnimationFrameCallback xrAnimationFrameCallback) : base()
        {
            _xrAnimationFrameCallback = xrAnimationFrameCallback;

            size = new Size(RootClip.vres.w, RootClip.vres.h);

            _tri = new TriangleClip();
            _tri.Position = new Vector2(0, 0);
            _tri.Scale = 2;
            Add(_tri);

            _xr = XRSystem.FromNavigator(Window.Current.Navigator);
            if (_xr != null)
            {
                _xr.IsSessionSupportedAsync("immersive-vr")
                    .ContinueWith((b) => _isXRSessionSupported = b.Result);
            }
        }

        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            if (_isXRSessionSupported)
            {
                if (uc.CurrMouseState.LeftButton == true
                &&  uc.CurrMouseState.Position.X < 64
                &&  uc.CurrMouseState.Position.Y < 64
                &&  !_initXRStarted)
                {
                    _initXRStarted = true;
                    InitXRSessionAsync(uc);
                }
                if (uc.CurrTouchState.IsPressed == true
                &&  uc.CurrTouchState.Position.X < 64
                &&  uc.CurrTouchState.Position.Y < 64
                &&  !_initXRStarted)
                {
                    _initXRStarted = true;
                    InitXRSessionAsync(uc);
                }
            }

            base.Update(uc);
        }

        public override void Draw(DrawContext dc)
        {
            float dt = (float)dc.dt.TotalSeconds;

            if (dc.xrFrame != null)
            {
                //XRRenderState renderState = dc.xrFrame.Session.RenderState;
                XRRenderState renderState = _xrsession.RenderState;
                XRViewerPose viewerPose = dc.xrFrame.GetViewerPose(_localspace);
                if (viewerPose != null)
                {
                    bool emulatedPosition = viewerPose.EmulatedPosition;
                    XRRigidTransform transform = viewerPose.Transform;

                    XRWebGLLayer glLayer = renderState.BaseLayer;
                    int w = glLayer.FramebufferWidth;
                    int h = glLayer.FramebufferHeight;
                    bool ign = glLayer.IgnoreDepthValues;
                    bool antialias = glLayer.Antialias;
                    WebGLFramebuffer glFramebuffer = glLayer.Framebuffer;

                    if (glFramebuffer != null)
                        dc.GLContext.BindFramebuffer(WebGLFramebufferType.FRAMEBUFFER, glFramebuffer);

                    foreach (XRView xrView in viewerPose.Views)
                    {
                        XREye eye = xrView.Eye;

                        XRViewport xrViewport = glLayer.GetViewport(xrView);
                        dc.GLContext.Viewport(xrViewport.X, xrViewport.Y, xrViewport.Width, xrViewport.Height);

                        XRRigidTransform vTransform = xrView.Transform;
                        float aspect = (float)xrViewport.Width / (float)xrViewport.Height;
                        Matrix4x4 view = vTransform.Inverse.Matrix;
                        Matrix4x4 proj = xrView.ProjectionMatrix;
                        dc.view = view;
                        dc.proj = proj;

                        base.Draw(dc);

                        DrawPointers(dc);
                    }

                    if (glFramebuffer != null)
                        dc.GLContext.BindFramebuffer(WebGLFramebufferType.FRAMEBUFFER, null);
                }
            }
            else
            {
                base.Draw(dc);
            }
        }


        public bool _isXRSessionSupported;
        public bool _initXRStarted;
        public bool _isXRInitialized;
        public int _xrAnimationHandle;

        public XRSystem _xr;
        public XRSession _xrsession;
        public XRReferenceSpace _localspace;
        public XRWebGLLayer _glLayer;

        private async void InitXRSessionAsync(UpdateContext uc)
        {
            var gl = uc.GLContext;

            try
            {
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

                _isXRInitialized = true;
                _xrAnimationHandle = _xrsession.RequestAnimationFrame(_xrAnimationFrameCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InitXRSessionAsync failed. "+ex.Message);
                Window.Current.Document.Title = "InitXRSessionAsync failed. " + ex.Message;
                throw;
            }
        }

        private void _xrsession_Ended(object sender, EventArgs e)
        {
            Console.WriteLine("_xrsession_Ended");
            _xrsession.CancelAnimationFrame(_xrAnimationHandle);
            _isXRInitialized = false;
            _initXRStarted = false;

            _xrsession = null;
            _localspace = null;
            _glLayer = null;
        }

        private void _xrsession_InputSourcesChanged(object sender, InputSourcesChangedEventArgs e)
        {
            Console.WriteLine("_xrsession_InputSourcesChanged");
        }

        internal void DrawPointers(DrawContext dc)
        {
            XRInputSourceArray inputSources = _xrsession.InputSources;
            for (int i = 0; i < inputSources.Count; i++)
            {
                XRInputSource inputSource = inputSources[i];

                XRHandedness hand = inputSource.Handedness;

                Gamepad gamepad = inputSource.Gamepad;

                XRSpace gripSpace = inputSource.GripSpace;
                if (gripSpace != null)
                {
                    XRPose grip = dc.xrFrame.GetPose(gripSpace, _localspace);
                    if (grip != null)
                    {
                        XRRigidTransform gripTransform = grip.Transform;
                        Matrix4x4 gripTranformMtx = gripTransform.Matrix;
                    }
                }

                XRSpace pointerSpace = inputSource.TargetRaySpace;
                if (pointerSpace != null)
                {
                    XRPose pointer = dc.xrFrame.GetPose(pointerSpace, _localspace);
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
                            GLContext = dc.GLContext,
                            Layer = dc.Layer,
                            t  = dc.t,
                            dt = dc.dt,
                            world = pointerMtx,
                            view = dc.view,
                            proj = dc.proj,
                        };
                        _tri.Draw(dcp);
                    }
                }
            }
        }

    }
}