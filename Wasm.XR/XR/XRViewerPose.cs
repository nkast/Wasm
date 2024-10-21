using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRViewerPose : XRPose
    {
        List<XRView> _views = new List<XRView>();
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

                int count = InvokeRet<IntPtr, int>("nkXRViewerPose.GetViews", new IntPtr(puids));
                _views.Clear();

                for (int i = 0; i < count; i++)
                {
                    int uid = puids[i];
                    _views.Add(new XRView(uid));
                }

                return _readOnlyViews;
            }
        }
    }
}