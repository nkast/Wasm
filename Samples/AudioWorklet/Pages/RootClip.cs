using System.Numerics;
using nkast.Wasm.Dom;
using nkast.Wasm.Input;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.Audio;
using nkast.Wasm.Media;
using AudioWorklet.Engine;

namespace AudioWorklet.Pages
{
    internal class RootClip : Clip
    {
        Index _index;

        public static Size vres = new Size(1920, 960);

        AudioContext _ac;
        AudioWorkletNode _streamSource;
        MediaStreamSourceNode _micNode;
        MediaStream _micStream;
        AudioWorkletNode _micWorkletNode;


        public RootClip(Index index) : base()
        {
            this._index = index;

            size = new Size(RootClip.vres.w, RootClip.vres.h);

        }

        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            base.Update(uc);
        }

        public override void Draw(DrawContext dc)
        {
            float dt = (float)dc.dt.TotalSeconds;

            base.Draw(dc);
        }

        internal async void InitAudioWorkletAsync()
        {
            _index.SetbtnStartAudioWorklet("starting AudioWorklet ...", true);

            try
            {
                _ac = new AudioContext();
                int sampleRate = _ac.SampleRate;

                await _ac.AudioWorklet.AddModule("js/streamProcessor.js");
                _streamSource = _ac.CreateWorklet("stream-processor");
                _streamSource.Connect(_ac.Destination);

                _streamSource.Port.Message += (sender, e) =>
                {
                    Console.WriteLine("audioWorklet received message: " + e.DataFloat64);
                };

                await _ac.AudioWorklet.AddModule("js/micProcessor.js");

                MediaDevices md = MediaDevices.FromNavigator(Window.Current.Navigator);
                _micStream = await md.GetUserMedia(new UserMediaConstraints() { Audio = true });
                _micNode = _ac.CreateMediaStreamSource(_micStream);
                {
                    _micWorkletNode = _ac.CreateWorklet("mic-processor");

                    _micWorkletNode.Port.Message += (sender, e) =>
                    {
                        if (e.DataByteArray != null)
                        {
                            byte[] audioBuffer = e.DataByteArray.ToArray();
                            _streamSource.Port.PostMessage(audioBuffer);
                            e.DataByteArray.Dispose();
                        }
                    };

                    _micNode.Connect(_micWorkletNode);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("audioWorklet failed. " + ex.Message);
                //Window.Current.Document.Title = "audioWorklet failed. " + ex.Message;

                _index.SetbtnStartAudioWorklet("failed to Start audioWorklet", false);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing); 
        }
    }
}