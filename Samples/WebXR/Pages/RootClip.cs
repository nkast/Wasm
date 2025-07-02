using System.Numerics;
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
        Index _index;
        XRSession.XRAnimationFrameCallback _xrAnimationFrameCallback;

        public static Size vres = new Size(1920, 960);

        TriangleClip _tri;


        public RootClip(Index index) : base()
        {
            this._index = index;
            _xrAnimationFrameCallback = index.OnXRAnimationFrame;

            size = new Size(RootClip.vres.w, RootClip.vres.h);

            _tri = new TriangleClip();
            _tri.Position = new Vector2(0, 0);
            _tri.Scale = 2;
            Add(_tri);

            _xr = XRSystem.FromNavigator(Window.Current.Navigator);
            if (_xr != null)
            {
                _xr.IsSessionSupportedAsync("immersive-vr")
                    .ContinueWith((b) =>
                    {
                        _isVRSessionSupported = b.Result;
                        if (_isVRSessionSupported)
                        {
                            _index.SetbtnEnterVR("click to Enter VR", false);
                        }
                        else
                        {
                            _index.SetbtnEnterVR("VR is not supported", true);
                        }
                    });
            }
            else
            {
                _index.SetbtnEnterVR("VR is not supported", true);
            }
        }

        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            if (_isVRSessionSupported)
            {
                if (uc.CurrMouseState.LeftButton == true
                &&  uc.CurrMouseState.Position.X < 64
                &&  uc.CurrMouseState.Position.Y < 64
                &&  !_initXRStarted)
                {
                    _requestVR = true;
                }
                if (uc.CurrTouchState.IsPressed == true
                &&  uc.CurrTouchState.Position.X < 64
                &&  uc.CurrTouchState.Position.Y < 64
                &&  !_initXRStarted)
                {
                    _requestVR = true;
                }
            }

            if (_requestVR == true)
            {
                _requestVR = false;
                InitXRSessionAsync(uc);
            }

            base.Update(uc);
        }

        public override void Draw(DrawContext dc)
        {
            float dt = (float)dc.dt.TotalSeconds;

            if (dc.xrFrame != null)
            {
                XRSession xrsession = dc.xrFrame.Session;
                XRRenderState renderState = xrsession.RenderState;

                using (XRViewerPose viewerPose = dc.xrFrame.GetViewerPose(_localspace))
                {
                    if (viewerPose != null)
                    {
                        bool emulatedPosition = viewerPose.EmulatedPosition;
                        Vector4? angularVelocity = viewerPose.AngularVelocity;
                        Vector4? linearVelocity = viewerPose.LinearVelocity;
                        XRRigidTransform transform = viewerPose.Transform;
                        Matrix4x4 tranformMtx = transform.Matrix;

                        XRWebGLLayer glLayer = renderState.BaseLayer;
                        float? depthNear = renderState.DepthNear;
                        float? depthFar = renderState.DepthFar;
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

                            float aspect = (float)xrViewport.Width / (float)xrViewport.Height;

                            XRRigidTransform viewTransform = xrView.Transform;
                            XRRigidTransform invViewTransform = viewTransform.Inverse;
                            Matrix4x4 view = invViewTransform.Matrix;
                            Matrix4x4 proj = xrView.ProjectionMatrix;
                            dc.view = view;
                            dc.proj = proj;

                            base.Draw(dc);

                            DrawPointers(dc);

                            xrView.Dispose();
                        }

                        if (glFramebuffer != null)
                            dc.GLContext.BindFramebuffer(WebGLFramebufferType.FRAMEBUFFER, null);
                    }
                }
            }
            else
            {
                base.Draw(dc);
            }
        }


        public bool _isVRSessionSupported;
        internal bool _requestVR;
        public bool _initXRStarted;
        public bool _isXRInitialized;
        public int _xrAnimationHandle;

        public XRSystem _xr;
        public XRSession _xrsession;
        public XRReferenceSpace _localspace;
        public XRWebGLLayer _glLayer;

        internal async void InitXRSessionAsync(UpdateContext uc)
        {
            _initXRStarted = true;
            _index.SetbtnEnterVR("entering VR ...", true);

            var gl = uc.GLContext;

            try
            {
                XRSessionOptions sessionOptions = default;
                sessionOptions.RequiredFeatures = XRSessionFeatures.Local;
                sessionOptions.OptionalFeatures = XRSessionFeatures.LocalFloor
                                                | XRSessionFeatures.HandTracking
                                                //| XRSessionFeatures.Anchors
                                                ;
                _xrsession = await _xr.RequestSessionAsync("immersive-vr", sessionOptions);
                _xrsession.Ended += _xrsession_Ended;
                _xrsession.InputSourcesChanged += _xrsession_InputSourcesChanged;
                await gl.MakeXRCompatibleAsync();
                _localspace = await _xrsession.RequestReferenceSpaceAsync("local");

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
                _index.SetbtnEnterVR("VR enabled", true);
                _xrAnimationHandle = _xrsession.RequestAnimationFrame(_xrAnimationFrameCallback);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InitXRSessionAsync failed. "+ex.Message);
                //Window.Current.Document.Title = "InitXRSessionAsync failed. " + ex.Message;

                _initXRStarted = false;
                _index.SetbtnEnterVR("click to Enter VR (retry)", false);
            }
        }

        private void _xrsession_Ended(object sender, EventArgs e)
        {
            _index.SetbtnEnterVR("click to Enter VR ", false);

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
            foreach(XRInputSource inputSource in _xrsession.InputSources)
            {
                XRHandedness handedness = inputSource.Handedness;

                Gamepad gamepad = inputSource.Gamepad;

                XRSpace gripSpace = inputSource.GripSpace;
                if (gripSpace != null)
                {
                    using (XRPose grip = dc.xrFrame.GetPose(gripSpace, _localspace))
                    {
                        if (grip != null)
                        {
                            Vector4? gripAngularVelocity = grip.AngularVelocity;
                            Vector4? gripLinearVelocity = grip.LinearVelocity;
                            XRRigidTransform gripTransform = grip.Transform;
                            Matrix4x4 gripTranformMtx = gripTransform.Matrix;

                        }
                    }
                }

                XRSpace pointerSpace = inputSource.TargetRaySpace;
                if (pointerSpace != null)
                {
                    using (XRPose pointer = dc.xrFrame.GetPose(pointerSpace, _localspace))
                    {
                        if (pointer != null)
                        {
                            Vector4? pointerAngularVelocity = pointer.AngularVelocity;
                            Vector4? pointerLinearVelocity = pointer.LinearVelocity;
                            XRRigidTransform pointerTransform = pointer.Transform;
                            Matrix4x4 pointerTranformMtx = pointerTransform.Matrix;

                            // draw pointer
                            Matrix4x4 pointerMtx = Matrix4x4.Identity;
                            pointerMtx *= Matrix4x4.CreateRotationX(-(float)Math.Tau / 4f);
                            pointerMtx *= Matrix4x4.CreateScale(0.2f, 1.0f, 1.0f);

                            if (gamepad != null)
                            {
                                GamepadButton[] buttons = gamepad.Buttons;
                                float[] axes = gamepad.Axes;

                                if (buttons[0].Pressed) // 0 = trigger
                                {
                                    pointerMtx *= Matrix4x4.CreateScale(1.0f, 1.0f, 1.1f + (0.4f * buttons[0].Value));
                                    if (gamepad.VibrationActuator != null)
                                        gamepad.VibrationActuator.Pulse( (0.1f + 0.9f * buttons[0].Value) , 100);
                                }
                                else if (buttons[0].Touched) // 0 = trigger
                                {
                                    pointerMtx *= Matrix4x4.CreateScale(1.0f, 1.0f, 1.1f);
                                }
                            }

                            pointerMtx *= Matrix4x4.CreateScale(0.1f);

                            pointerMtx *= pointerTranformMtx;
                            DrawContext dcp = new DrawContext()
                            {
                                GLContext = dc.GLContext,
                                Layer = dc.Layer,
                                t = dc.t,
                                dt = dc.dt,
                                world = pointerMtx,
                                view = dc.view,
                                proj = dc.proj,
                            };
                            _tri.Draw(dcp);
                        }
                    }
                }

                XRHand hand = inputSource.Hand;
                if (hand != null)
                {
                    DrawHand(dc, hand);
                }

            }
        }

        private void DrawHand(DrawContext dc, XRHand hand)
        {
            int handJointCount = hand.Count;

            XRJointSpace wristSpace = hand["wrist"];

            XRJointSpace thumbMetacarpalSpace = hand["thumb-metacarpal"];
            XRJointSpace thumbProximalSpace = hand["thumb-phalanx-proximal"];
            XRJointSpace thumbDistalSpace = hand["thumb-phalanx-distal"];
            XRJointSpace thumbTipSpace = hand["thumb-tip"];

            XRJointSpace indexMetacarpalSpace = hand["index-finger-metacarpal"];
            XRJointSpace indexProximalSpace = hand["index-finger-phalanx-proximal"];
            XRJointSpace indexIntermediateSpace = hand["index-finger-phalanx-intermediate"];
            XRJointSpace indexDistalSpace = hand["index-finger-phalanx-distal"];
            XRJointSpace indexTipSpace = hand["index-finger-tip"];

            XRJointSpace middleMetacarpalSpace = hand["middle-finger-metacarpal"];
            XRJointSpace middleProximalSpace = hand["middle-finger-phalanx-proximal"];
            XRJointSpace middleIntermediateSpace = hand["middle-finger-phalanx-intermediate"];
            XRJointSpace middleDistalSpace = hand["middle-finger-phalanx-distal"];
            XRJointSpace middleTipSpace = hand["middle-finger-tip"];

            XRJointSpace ringMetacarpalSpace = hand["ring-finger-metacarpal"];
            XRJointSpace ringProximalSpace = hand["ring-finger-phalanx-proximal"];
            XRJointSpace ringIntermediateSpace = hand["ring-finger-phalanx-intermediate"];
            XRJointSpace ringDistalSpace = hand["ring-finger-phalanx-distal"];
            XRJointSpace ringTipSpace = hand["ring-finger-tip"];

            XRJointSpace pinkyMetacarpalSpace = hand["pinky-finger-metacarpal"];
            XRJointSpace pinkyProximalSpace = hand["pinky-finger-phalanx-proximal"];
            XRJointSpace pinkyIntermediateSpace = hand["pinky-finger-phalanx-intermediate"];
            XRJointSpace pinkyDistalSpace = hand["pinky-finger-phalanx-distal"];
            XRJointSpace pinkyTipSpace = hand["pinky-finger-tip"];

            DrawBone(dc, wristSpace, thumbMetacarpalSpace);
            DrawBone(dc, thumbMetacarpalSpace, thumbProximalSpace);
            DrawBone(dc, thumbProximalSpace, thumbDistalSpace);
            DrawBone(dc, thumbDistalSpace, thumbTipSpace);

            DrawBone(dc, wristSpace, indexMetacarpalSpace);
            DrawBone(dc, indexMetacarpalSpace, indexProximalSpace);
            DrawBone(dc, indexProximalSpace, indexIntermediateSpace);
            DrawBone(dc, indexIntermediateSpace, indexDistalSpace);
            DrawBone(dc, indexDistalSpace, indexTipSpace);

            DrawBone(dc, wristSpace, middleMetacarpalSpace);
            DrawBone(dc, middleMetacarpalSpace, middleProximalSpace);
            DrawBone(dc, middleProximalSpace, middleIntermediateSpace);
            DrawBone(dc, middleIntermediateSpace, middleDistalSpace);
            DrawBone(dc, middleDistalSpace, middleTipSpace);

            DrawBone(dc, wristSpace, ringMetacarpalSpace);
            DrawBone(dc, ringMetacarpalSpace, ringProximalSpace);
            DrawBone(dc, ringProximalSpace, ringIntermediateSpace);
            DrawBone(dc, ringIntermediateSpace, ringDistalSpace);
            DrawBone(dc, ringDistalSpace, ringTipSpace);

            DrawBone(dc, wristSpace, pinkyMetacarpalSpace);
            DrawBone(dc, pinkyMetacarpalSpace, pinkyProximalSpace);
            DrawBone(dc, pinkyProximalSpace, pinkyIntermediateSpace);
            DrawBone(dc, pinkyIntermediateSpace, pinkyDistalSpace);
            DrawBone(dc, pinkyDistalSpace, pinkyTipSpace);
        }

        private void DrawBone(DrawContext dc, XRJointSpace jointASpace, XRJointSpace jointBSpace)
        {
            using (XRJointPose jointPoseA = dc.xrFrame.GetJointPose(jointASpace, _localspace))
            using (XRJointPose jointPoseB = dc.xrFrame.GetJointPose(jointBSpace, _localspace))
            {
                XRRigidTransform jointPoseTransformA = jointPoseA.Transform;
                XRRigidTransform jointPoseTransformB = jointPoseB.Transform;

                Matrix4x4 jointPoseTransformMtxA = jointPoseTransformA.Matrix;
                Matrix4x4 jointPoseTransformMtxB = jointPoseTransformB.Matrix;

                float jointPoseTransformRadiusA = jointPoseA.Radius;
                float jointPoseTransformRadiusB = jointPoseB.Radius;

                Vector3 diff = jointPoseTransformMtxB.Translation - jointPoseTransformMtxA.Translation;
                float len = diff.Length();

                // draw bone
                Matrix4x4 boneMtx = Matrix4x4.Identity;
                boneMtx *= Matrix4x4.CreateTranslation(0f, 0.5f, 0f);
                boneMtx *= Matrix4x4.CreateRotationX(-(float)Math.Tau / 4f);
                boneMtx *= Matrix4x4.CreateScale(jointPoseTransformRadiusB, jointPoseTransformRadiusB, len);

                boneMtx *= jointPoseTransformMtxB;
                DrawContext dcp = new DrawContext()
                {
                    GLContext = dc.GLContext,
                    Layer = dc.Layer,
                    t = dc.t,
                    dt = dc.dt,
                    world = boneMtx,
                    view = dc.view,
                    proj = dc.proj,
                };
                _tri.Draw(dcp);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tri?.Dispose();
                _tri = null;
            }

            base.Dispose(disposing); 
        }
    }
}