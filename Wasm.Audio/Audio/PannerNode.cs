using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class PannerNode : AudioNode
    {
        AudioParam _positionX;
        AudioParam _positionY;
        AudioParam _positionZ;
        AudioParam _orientationX;
        AudioParam _orientationY;
        AudioParam _orientationZ;

        internal PannerNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public PanningModelType PanningModel
        {
            get { return (PanningModelType)InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetPanningModel")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetPanningModel"), (int)value); }
        }

        public AudioParam PositionX
        {
            get
            {
                if (_positionX == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetPositionX"));
                    _positionX = new AudioParam(uid, this);
                }

                return _positionX;
            }
        }

        public AudioParam PositionY
        {
            get
            {
                if (_positionY == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetPositionY"));
                    _positionY = new AudioParam(uid, this);
                }

                return _positionY;
            }
        }

        public AudioParam PositionZ
        {
            get
            {
                if (_positionZ == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetPositionZ"));
                    _positionZ = new AudioParam(uid, this);
                }

                return _positionZ;
            }
        }

        public AudioParam OrientationX
        {
            get
            {
                if (_orientationX == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetOrientationX"));
                    _orientationX = new AudioParam(uid, this);
                }

                return _orientationX;
            }
        }

        public AudioParam OrientationY
        {
            get
            {
                if (_orientationY == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetOrientationY"));
                    _orientationY = new AudioParam(uid, this);
                }

                return _orientationY;
            }
        }

        public AudioParam OrientationZ
        {
            get
            {
                if (_orientationZ == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetOrientationZ"));
                    _orientationZ = new AudioParam(uid, this);
                }

                return _orientationZ;
            }
        }

        public DistanceModelType DistanceModel
        {
            get { return (DistanceModelType)InvokeRetInt(RegisterFunction("nkAudioPannerNode.GetDistanceModel")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetDistanceModel"), (int)value); }
        }

        public double RefDistance
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetRefDistance")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetRefDistance"), value); }
        }

        public double MaxDistance
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetMaxDistance")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetMaxDistance"), value); }
        }

        public double RolloffFactor
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetRolloffFactor")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetRolloffFactor"), value); }
        }

        public double ConeInnerAngle
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetConeInnerAngle")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetConeInnerAngle"), value); }
        }

        public double ConeOuterAngle
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetConeOuterAngle")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetConeOuterAngle"), value); }
        }

        public double ConeOuterGain
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioPannerNode.GetConeOuterGain")); }
            set { Invoke(RegisterFunction("nkAudioPannerNode.SetConeOuterGain"), value); }
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
