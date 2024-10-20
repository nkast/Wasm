using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;

namespace nkast.Wasm.WebRTC
{

	public class RTCDataChannel : JSObject
	{
		static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

		//public event EventHandler OnDataChannel;


		internal RTCDataChannel(int uid) : base(uid)
		{
			_uidMap.Add(Uid, new WeakReference<JSObject>(this));
			Invoke("nkRTCDataChannel.RegisterEvents");
		}

		[JSInvokable]
		public static void JsRTCDataChannelOnOpen(int uid)
		{
			Console.WriteLine("data channel open!");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{

			}

			Invoke("nkRTCDataChannel.UnregisterEvents");
			_uidMap.Remove(Uid);

			base.Dispose(disposing);
		}
	}
}
