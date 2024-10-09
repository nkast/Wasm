using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;

namespace nkast.Wasm.WebRTC
{
	public class RTCPeerConnection : JSObject
	{
		static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

		//public event EventHandler OnDataChannel;


		internal RTCPeerConnection(int uid) : base(uid)
		{
			_uidMap.Add(Uid, new WeakReference<JSObject>(this));
			Invoke("nkRTCPeerConnection.RegisterEvents");
		}

		[JSInvokable]
		public static void JsRTCPeerConnectionOnDataChannel(int uid)
		{
			Console.WriteLine("data channel connected!");
		}

		public void CreateDataChannel()
		{
			Invoke("nkRTCPeerConnection.CreateDataChannel");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{

			}

			Invoke("nkRTCPeerConnection.UnregisterEvents");
			_uidMap.Remove(Uid);

			base.Dispose(disposing);
		}
	}
}