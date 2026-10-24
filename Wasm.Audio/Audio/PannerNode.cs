using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class PannerNode : AudioNode
    {
        private readonly int _fid_GetPanningModel;
        private readonly int _fid_SetPanningModel;
        private readonly int _fid_GetPositionX;
        private readonly int _fid_GetPositionY;
        private readonly int _fid_GetPositionZ;
        private readonly int _fid_GetOrientationX;
        private readonly int _fid_GetOrientationY;
        private readonly int _fid_GetOrientationZ;
        private readonly int _fid_GetDistanceModel;
        private readonly int _fid_SetDistanceModel;
        private readonly int _fid_GetRefDistance;
        private readonly int _fid_SetRefDistance;
        private readonly int _fid_GetMaxDistance;
        private readonly int _fid_SetMaxDistance;
        private readonly int _fid_GetRolloffFactor;
        private readonly int _fid_SetRolloffFactor;
        private readonly int _fid_GetConeInnerAngle;
        private readonly int _fid_SetConeInnerAngle;
        private readonly int _fid_GetConeOuterAngle;
        private readonly int _fid_SetConeOuterAngle;
        private readonly int _fid_GetConeOuterGain;
        private readonly int _fid_SetConeOuterGain;

        AudioParam _positionX;
        AudioParam _positionY;
        AudioParam _positionZ;
        AudioParam _orientationX;
        AudioParam _orientationY;
        AudioParam _orientationZ;

        internal PannerNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_GetPanningModel = RegisterFunction("nkAudioPannerNode.GetPanningModel");
            _fid_SetPanningModel = RegisterFunction("nkAudioPannerNode.SetPanningModel");
            _fid_GetPositionX = RegisterFunction("nkAudioPannerNode.GetPositionX");
            _fid_GetPositionY = RegisterFunction("nkAudioPannerNode.GetPositionY");
            _fid_GetPositionZ = RegisterFunction("nkAudioPannerNode.GetPositionZ");
            _fid_GetOrientationX = RegisterFunction("nkAudioPannerNode.GetOrientationX");
            _fid_GetOrientationY = RegisterFunction("nkAudioPannerNode.GetOrientationY");
            _fid_GetOrientationZ = RegisterFunction("nkAudioPannerNode.GetOrientationZ");
            _fid_GetDistanceModel = RegisterFunction("nkAudioPannerNode.GetDistanceModel");
            _fid_SetDistanceModel = RegisterFunction("nkAudioPannerNode.SetDistanceModel");
            _fid_GetRefDistance = RegisterFunction("nkAudioPannerNode.GetRefDistance");
            _fid_SetRefDistance = RegisterFunction("nkAudioPannerNode.SetRefDistance");
            _fid_GetMaxDistance = RegisterFunction("nkAudioPannerNode.GetMaxDistance");
            _fid_SetMaxDistance = RegisterFunction("nkAudioPannerNode.SetMaxDistance");
            _fid_GetRolloffFactor = RegisterFunction("nkAudioPannerNode.GetRolloffFactor");
            _fid_SetRolloffFactor = RegisterFunction("nkAudioPannerNode.SetRolloffFactor");
            _fid_GetConeInnerAngle = RegisterFunction("nkAudioPannerNode.GetConeInnerAngle");
            _fid_SetConeInnerAngle = RegisterFunction("nkAudioPannerNode.SetConeInnerAngle");
            _fid_GetConeOuterAngle = RegisterFunction("nkAudioPannerNode.GetConeOuterAngle");
            _fid_SetConeOuterAngle = RegisterFunction("nkAudioPannerNode.SetConeOuterAngle");
            _fid_GetConeOuterGain = RegisterFunction("nkAudioPannerNode.GetConeOuterGain");
            _fid_SetConeOuterGain = RegisterFunction("nkAudioPannerNode.SetConeOuterGain");
        }

        public PanningModelType PanningModel
        {
            get { return (PanningModelType)InvokeRetInt(_fid_GetPanningModel); }
            set { Invoke(_fid_SetPanningModel, (int)value); }
        }

        public AudioParam PositionX
        {
            get
            {
                if (_positionX == null)
                {
                    int uid = InvokeRetInt(_fid_GetPositionX);
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
                    int uid = InvokeRetInt(_fid_GetPositionY);
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
                    int uid = InvokeRetInt(_fid_GetPositionZ);
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
                    int uid = InvokeRetInt(_fid_GetOrientationX);
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
                    int uid = InvokeRetInt(_fid_GetOrientationY);
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
                    int uid = InvokeRetInt(_fid_GetOrientationZ);
                    _orientationZ = new AudioParam(uid, this);
                }

                return _orientationZ;
            }
        }

        public DistanceModelType DistanceModel
        {
            get { return (DistanceModelType)InvokeRetInt(_fid_GetDistanceModel); }
            set { Invoke(_fid_SetDistanceModel, (int)value); }
        }

        public double RefDistance
        {
            get { return InvokeRetDouble(_fid_GetRefDistance); }
            set { Invoke(_fid_SetRefDistance, value); }
        }

        public double MaxDistance
        {
            get { return InvokeRetDouble(_fid_GetMaxDistance); }
            set { Invoke(_fid_SetMaxDistance, value); }
        }

        public double RolloffFactor
        {
            get { return InvokeRetDouble(_fid_GetRolloffFactor); }
            set { Invoke(_fid_SetRolloffFactor, value); }
        }

        public double ConeInnerAngle
        {
            get { return InvokeRetDouble(_fid_GetConeInnerAngle); }
            set { Invoke(_fid_SetConeInnerAngle, value); }
        }

        public double ConeOuterAngle
        {
            get { return InvokeRetDouble(_fid_GetConeOuterAngle); }
            set { Invoke(_fid_SetConeOuterAngle, value); }
        }

        public double ConeOuterGain
        {
            get { return InvokeRetDouble(_fid_GetConeOuterGain); }
            set { Invoke(_fid_SetConeOuterGain, value); }
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
