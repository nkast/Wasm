using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class BaseAudioContext : JSObject
    {
        AudioDestinationNode _destination;
        AudioListener _listener;

        public BaseAudioContext(int uid) : base(uid)
        {
        }

        public int SampleRate
        {
            get
            {
                int sampleRate = InvokeRet<int>("nkAudioBaseContext.GetSampleRate");
                return SampleRate;
            }
        }

        public AudioDestinationNode Destination
        {
            get
            {
                if (_destination == null)
                {
                    int uid = InvokeRet<int>("nkAudioBaseContext.GetDestination");
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
                    int uid = InvokeRet<int>("nkAudioBaseContext.GetListener");
                    _listener = new AudioListener(uid, this);
                }

                return _listener;
            }
        }

        public ContextState State
        {
            get
            {
                string str = InvokeRet<string>("nkAudioBaseContext.GetState");
                return Enum.Parse<ContextState>(str, true);
            }
            set { Invoke("nkAudioBaseContext.SetState", value.ToString().ToLower()); }
        }

        public AudioBuffer CreateBuffer(int numOfChannels, int  length, int sampleRate)
        {
            int uid = InvokeRet<int, int, int, int>("nkAudioBaseContext.CreateBuffer", numOfChannels, length, sampleRate);
            return new AudioBuffer(uid, this);
        }

        public AudioBufferSourceNode CreateBufferSource()
        {
            int uid = InvokeRet<int>("nkAudioBaseContext.CreateBufferSource");
            return new AudioBufferSourceNode(uid, this);
        }

        public OscillatorNode CreateOscillator()
        {
            int uid = InvokeRet<int>("nkAudioBaseContext.CreateOscillator");
            return new OscillatorNode(uid, this);
        }        

        public GainNode CreateGain()
        {
            int uid = InvokeRet<int>("nkAudioBaseContext.CreateGain");
            return new GainNode(uid, this);
        }

        public StereoPannerNode CreateStereoPanner()
        {
            int uid = InvokeRet<int>("nkAudioBaseContext.CreateStereoPanner");
            return new StereoPannerNode(uid, this);
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
