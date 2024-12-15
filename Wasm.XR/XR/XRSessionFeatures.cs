using System;
using System.Text;

namespace nkast.Wasm.XR
{
    [Flags]
    public enum XRSessionFeatures : Int32
    {
        // reference spaces
        Local        = 0x0000_0001,
        LocalFloor   = 0x0000_0002,
        Unbounded    = 0x0000_0004,
        BoundedFloor = 0x0000_0008,
        Viewer       = 0x0000_0010,

        Anchors         = 0x0000_0020,
        DepthSensing    = 0x0000_0040,
        DomOverlay      = 0x0000_0080,
        HandTracking    = 0x0000_0100,
        HitTest         = 0x0000_0200,
        Layers          = 0x0000_0400,
        LightEstimation = 0x0000_0800,
        SecondaryViews  = 0x0000_1000,
    }
}