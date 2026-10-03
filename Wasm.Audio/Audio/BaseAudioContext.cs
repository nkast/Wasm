using System;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class BaseAudioContext : JSObject
    {
        private readonly int _fid_GetSampleRate;
        private readonly int _fid_GetCurrentTime;
        private readonly int _fid_GetDestination;
        private readonly int _fid_GetListener;
        private readonly int _fid_GetAudioWorklet;
        private readonly int _fid_GetState;
        private readonly int _fid_CreateBuffer;
        private readonly int _fid_CreateBufferSource;
        private readonly int _fid_CreateOscillator;
        private readonly int _fid_CreateMediaElementSource;
        private readonly int _fid_CreateGain;
        private readonly int _fid_CreatePanner;
        private readonly int _fid_CreateStereoPanner;
        private readonly int _fid_CreateWorklet;
        private readonly int _fid_CreateWorklet1;

        AudioDestinationNode _destination;
        AudioListener _listener;

        public BaseAudioContext(int uid) : base(uid)
        {
            _fid_GetSampleRate = RegisterFunction("nkAudioBaseContext.GetSampleRate");
            _fid_GetCurrentTime = RegisterFunction("nkAudioBaseContext.GetCurrentTime");
            _fid_GetDestination = RegisterFunction("nkAudioBaseContext.GetDestination");
            _fid_GetListener = RegisterFunction("nkAudioBaseContext.GetListener");
            _fid_GetAudioWorklet = RegisterFunction("nkAudioBaseContext.GetAudioWorklet");
            _fid_GetState = RegisterFunction("nkAudioBaseContext.GetState");
            _fid_CreateBuffer = RegisterFunction("nkAudioBaseContext.CreateBuffer");
            _fid_CreateBufferSource = RegisterFunction("nkAudioBaseContext.CreateBufferSource");
            _fid_CreateOscillator = RegisterFunction("nkAudioBaseContext.CreateOscillator");
            _fid_CreateMediaElementSource = RegisterFunction("nkAudioBaseContext.CreateMediaElementSource");
            _fid_CreateGain = RegisterFunction("nkAudioBaseContext.CreateGain");
            _fid_CreatePanner = RegisterFunction("nkAudioBaseContext.CreatePanner");
            _fid_CreateStereoPanner = RegisterFunction("nkAudioBaseContext.CreateStereoPanner");
            _fid_CreateWorklet = RegisterFunction("nkAudioBaseContext.CreateWorklet");
            _fid_CreateWorklet1 = RegisterFunction("nkAudioBaseContext.CreateWorklet1");
        }

        public int SampleRate
        {
            get
            {
                int sampleRate = InvokeRetInt(_fid_GetSampleRate);
                return sampleRate;
            }
        }

        public double CurrentTime
        {
            get
            {
                double currentTime = InvokeRetDouble(_fid_GetCurrentTime);
                return currentTime;
            }
        }

        public AudioDestinationNode Destination
        {
            get
            {
                if (_destination == null)
                {
                    int uid = InvokeRetInt(_fid_GetDestination);
                    _destination = new AudioDestinationNode(uid, this);
                }

                return _destination;
            }
        }

        public AudioListener Listener
        {
            get
            {
                if (_listener == null)
                {
                    int uid = InvokeRetInt(_fid_GetListener);
                    _listener = new AudioListener(uid, this);
                }

                return _listener;
            }
        }

        public AudioWorklet AudioWorklet
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetAudioWorklet);
                AudioWorklet audioWorklet = AudioWorklet.FromUid(uid);
                if (audioWorklet != null)
                    return audioWorklet;

                return new AudioWorklet(uid);
            }
        }

        public ContextState State
        {
            get { return (ContextState)InvokeRetInt(_fid_GetState); }
        }

        public AudioBuffer CreateBuffer(int numOfChannels, int  length, int sampleRate)
        {
            int uid = InvokeRetInt<int, int, int>(_fid_CreateBuffer, numOfChannels, length, sampleRate);
            return new AudioBuffer(uid, this);
        }

        public AudioBufferSourceNode CreateBufferSource()
        {
            int uid = InvokeRetInt(_fid_CreateBufferSource);
            return new AudioBufferSourceNode(uid, this);
        }

        public OscillatorNode CreateOscillator()
        {
            int uid = InvokeRetInt(_fid_CreateOscillator);
            return new OscillatorNode(uid, this);
        }        

        public MediaElementAudioSourceNode CreateMediaElementSource(IHTMLMediaElement media)
        {
            int uid = InvokeRetInt<int>(_fid_CreateMediaElementSource, ((JSObject)media).Uid);
            return new MediaElementAudioSourceNode(uid, this, media);
        }

        public GainNode CreateGain()
        {
            int uid = InvokeRetInt(_fid_CreateGain);
            return new GainNode(uid, this);
        }

        public PannerNode CreatePanner()
        {
            int uid = InvokeRetInt(_fid_CreatePanner);
            return new PannerNode(uid, this);
        }

        public StereoPannerNode CreateStereoPanner()
        {
            int uid = InvokeRetInt(_fid_CreateStereoPanner);
            return new StereoPannerNode(uid, this);
        }

        public AudioWorkletNode CreateWorklet(string name)
        {
            int uid = InvokeRetInt(_fid_CreateWorklet, name);
            return new AudioWorkletNode(uid, this);
        }

        private static int[] _emptyOutputChannelCount = new int[0];
        public unsafe AudioWorkletNode CreateWorklet(string name, AudioWorkletNodeOptions options)
        {
            int numberOfInputs = options.NumberOfInputs ?? -1;
            int numberOfOutputs = options.NumberOfOutputs ?? -1;
            int[] outputChannelCount = options.OutputChannelCount ?? _emptyOutputChannelCount;

            int uid;
            fixed (int* pOutputChannelCount = outputChannelCount)
            {
                uid = InvokeRetInt(_fid_CreateWorklet1, name, numberOfInputs, numberOfOutputs, (int)pOutputChannelCount, outputChannelCount.Length);
            }
            return new AudioWorkletNode(uid, this);
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
