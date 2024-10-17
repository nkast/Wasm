using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRViewerPose : XRPose
    {
        XRView[] _views = new XRView[2];
        IReadOnlyList<XRView> _readOnlyViews;

        public XRViewerPose(int uid) : base(uid)
        {
            _readOnlyViews = new ReadOnlyCollection<XRView>(_views);
        }

        public unsafe IReadOnlyList<XRView> Views
        {
            get
            {
                int* puids = stackalloc int[2];

                Invoke("nkXRViewerPose.GetViews", new IntPtr(puids));
                for (int i = 0; i < 2; i++)
                {
                    int uid = puids[i];
                    _views[i] = new XRView(uid);
                }

                return _readOnlyViews;
            }
        }
    }
}