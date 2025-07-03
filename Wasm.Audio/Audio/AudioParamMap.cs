using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioParamMap : JSObject
    {
        private AudioNode _audioNode;
        private Dictionary<string, AudioParam> _paramMap = new Dictionary<string, AudioParam>();

        public AudioParamMap(int uid, AudioNode audioNode) : base(uid)
        {
            _audioNode = audioNode;
        }

        public AudioParam this[string key]
        {
            get
            {
                if (_paramMap.TryGetValue(key, out AudioParam param))
                    return param;

                int uid = InvokeRetInt<string>("nkAudioParamMap.Get", key);
                if (uid == -1)
                    return null;

                param = new AudioParam(uid, _audioNode);
                _paramMap.Add(key, param);

                return param;
            }
        }

        public int Count
        {
            get
            {
                int count = InvokeRetInt("nkAudioParamMap.GetSize");
                return count;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _audioNode = null;
            _paramMap = null;

            base.Dispose(disposing);
        }
    }
}