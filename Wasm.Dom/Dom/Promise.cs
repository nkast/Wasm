using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    public abstract class Promise : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        internal Promise(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
            Invoke("nkPromise.RegisterEvents");
        }

        public static Promise FromUid(int uid)
        {
            if (Promise._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (Promise)jsObj;
                else
                    Promise._uidMap.Remove(uid);
            }

            return null;
        }

        [JSInvokable]
        public static void JsPromiseOnCompleted(int uid)
        {
            Promise promise = Promise.FromUid(uid);

            promise.OnCompleted();
        }

        [JSInvokable]
        public static void JsPromiseOnError(int uid)
        {
            Promise promise = Promise.FromUid(uid);
            promise.OnError();
        }

        protected abstract void OnCompleted();
        protected abstract void OnError();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }

    public abstract class Promise<TResult> : Promise
    {
        protected readonly TaskCompletionSource<TResult> _tcs;

        internal Promise(int uid) : base(uid)
        {
            _tcs = new TaskCompletionSource<TResult>();
        }

        protected override void OnError()
        {
            string message = "Promise failed.";
            string error = InvokeRet<string>("nkPromise.GetErrorMessage");
            message += " " + error;

            _tcs.SetException(new Exception(message));
        }

        public Task<TResult> GetTask()
        {
            return _tcs.Task;
        }
    }

    public class PromiseVoid : Promise
    {
        protected readonly TaskCompletionSource _tcs;

        public PromiseVoid(int uid) : base(uid)
        {
            _tcs = new TaskCompletionSource();
        }

        protected override void OnCompleted()
        {
            _tcs.SetResult();
        }

        protected override void OnError()
        {
            string message = "Promise failed.";
            string error = InvokeRet<string>("nkPromise.GetErrorMessage");
            message += " " + error;

            _tcs.SetException(new Exception(message));
        }

        public Task GetTask()
        {
            return _tcs.Task;
        }
    }

    public class PromiseBoolean : Promise<bool>
    {
        public PromiseBoolean(int uid) : base(uid)
        {
        }

        protected override void OnCompleted()
        {
            bool result = InvokeRet<bool>("nkPromise.GetValueBoolean");
            _tcs.SetResult(result);
        }
    }

    public class PromiseJSObject<TResult> : Promise<TResult>
        where TResult : JSObject
    {
        Func<int, JSObject> _objectFactory;

        public PromiseJSObject(int uid, Func<int,JSObject> objectFactory) : base(uid)
        {
            _objectFactory = objectFactory;
        }

        protected override void OnCompleted()
        {
            int uid = InvokeRet<int>("nkPromise.GetValueJSObject");

            TResult result = (TResult)_objectFactory(uid);
            _tcs.SetResult(result);
        }

    }
}
