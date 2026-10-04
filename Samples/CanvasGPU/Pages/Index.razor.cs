using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGPU;
using CanvasGPU;
using CanvasGPU.Engine;

namespace CanvasGPU.Pages
{
    public partial class Index : IDisposable
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
                JsRuntime.InvokeAsync<object>("initRenderJS", DotNetObjectReference.Create(this));
            }
        }

        Canvas cs;

        MouseState currMouseState;
        MouseState prevMouseState;
        TouchState currTouchState;
        TouchState prevTouchState;

        IGPUCanvasContext context;
        GPUDevice device;
        IGPUTexture currentTexture;
        GPUCommandEncoder commandEncoder;

        async void InitGPUAsync(GPU gpu)
        {
            GPUAdapterOptions adapterOptions = new GPUAdapterOptions();
            adapterOptions.PowerPreference = GPUAdapterOptions.PowerPreferenceType.HighPerformance;
            GPUAdapter adapter = await gpu.RequestAdapterAsync(adapterOptions);
            Console.WriteLine("WebGPU adapter: " + (adapter != null));
            GPUAdapterInfo adapterInfo = adapter.GetInfo();
            Console.WriteLine("WebGPU adapter device: " + adapterInfo.Device);
            Console.WriteLine("WebGPU adapter description: " + adapterInfo.Description);
            Console.WriteLine("WebGPU adapter vendor: " + adapterInfo.Vendor);
            Console.WriteLine("WebGPU adapter architecture: " + adapterInfo.Architecture);
            GPUSupportedLimits adapterLimits = adapter.GetLimits();
            Console.WriteLine("WebGPU adapter maxTextureDimension2D: " + adapterLimits.MaxTextureDimension2D);
            Console.WriteLine("WebGPU adapter maxBufferSize: " + adapterLimits.MaxBufferSize);
            context = cs.GetContext<IGPUCanvasContext>();
            Console.WriteLine("WebGPU canvas context: " + (context != null));

            GPURequiredLimits requiredLimits = new GPURequiredLimits();
            requiredLimits.MaxTextureDimension2D = adapterLimits.MaxTextureDimension2D;
            device = await adapter.RequestDeviceAsync(requiredLimits);
            GPUSupportedLimits deviceLimits = device.GetLimits();
            Console.WriteLine("WebGPU device maxTextureDimension2D: " + deviceLimits.MaxTextureDimension2D);
            Console.WriteLine("WebGPU device maxBufferSize: " + deviceLimits.MaxBufferSize);
            Console.WriteLine("WebGPU device: " + (device != null));

            GPUTextureFormat preferredCanvasFormat = gpu.GetPreferredCanvasFormat();
            Console.WriteLine("WebGPU preferred canvas format: " + preferredCanvasFormat);
            GPUCanvasConfiguration canvasConfiguration = new GPUCanvasConfiguration();
            canvasConfiguration.Device = device;
            canvasConfiguration.Format = preferredCanvasFormat;
            canvasConfiguration.AlphaMode = GPUCanvasConfiguration.CanvasAlphaModeType.Opaque;
            context.Configure(canvasConfiguration);
            Console.WriteLine("WebGPU canvas context configured");
            GPUCanvasConfiguration currentConfiguration = context.GetConfiguration();
            Console.WriteLine("WebGPU canvas context format: " + currentConfiguration.Format);
            Console.WriteLine("WebGPU canvas context alphaMode: " + currentConfiguration.AlphaMode);
            Console.WriteLine("WebGPU canvas context toneMappingMode: " + currentConfiguration.ToneMappingMode);
            Console.WriteLine("WebGPU canvas context colorSpace: " + currentConfiguration.ColorSpace);
            Console.WriteLine("WebGPU canvas context usage: " + currentConfiguration.Usage);
        }

        private void BeginFrame()
        {
            currentTexture = context.GetCurrentTexture();
            //TODO: create view.
            commandEncoder = device.CreateCommandEncoder();

            //TODO: create render pass.

        }

        private void EndFrame()
        {
            // TODO: end render pass.
            // TODO: finish command encoder.
            // TODO: and submit to queue.


            commandEncoder.Dispose();
            commandEncoder = null;
            //currentTexture.Dispose();
            currentTexture = null;
            //GC.Collect();
        }

        [JSInvokable]
        public void TickDotNet()
        {
            if (cs == null)
            {
                cs = Window.Current.Document.GetElementById<Canvas>("theCanvas");

                GPU gpu = GPU.FromNavigator(Window.Current.Navigator);
                Console.WriteLine("WebGPU supported: " + (gpu != null));
                if (gpu != null)
                    InitGPUAsync(gpu);

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

                _root = new RootClip();

                _sw.Start();
                _prevt = _sw.Elapsed;
            }

            if (device == null)
                return;

            // run gameloop tick
            TimeSpan t  = _sw.Elapsed;
            TimeSpan dt = t - _prevt;
            _prevt = t;

            BeginFrame();

            UpdateContext uc = new UpdateContext(
                device,
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

            float aspect = (float)cs.Width / (float)cs.Height;
            Matrix4x4 world = Matrix4x4.CreateTranslation(new Vector3(0, 0, -2f));
            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0));
            Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspect, 0.1f, 100.0f);

            DrawContext dc = new DrawContext()
            {
                GPUDevice = device,
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

            EndFrame();
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


        public void Dispose()
        {
            _root?.Dispose();
            _root = null;

            device?.Dispose();
            device = null;
        }
    }
}