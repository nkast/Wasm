using System;

namespace nkast.Wasm.XR
{
    public struct XRSessionOptions
    {
        public XRSessionFeatures RequiredFeatures;
        public XRSessionFeatures OptionalFeatures;

        public XRSessionOptions(XRSessionFeatures requiredFeatures, XRSessionFeatures optionalFeatures)
        {
            this.RequiredFeatures = requiredFeatures;
            this.OptionalFeatures = optionalFeatures;

        }
    }
}