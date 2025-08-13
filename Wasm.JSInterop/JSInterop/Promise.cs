using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace nkast.Wasm.JSInterop
{
    public abstract class Promise : JSObject
    {
        private static readonly Dictionary<int, Promise> _uidMap = new Dictionary<int, Promise>();

        internal Promise(int uid) : base(uid)
        {
            _uidMap.Add(uid, this);
            Invoke("nkPromise.RegisterEvents");
        }

        [JSInvokable]
        public static void JsPromiseOnCompleted(int uid)
        {
            Promise promise = _uidMap[uid];

            promise.OnCompleted();
            promise.Dispose();

        }

        [JSInvokable]
        public static void JsPromiseOnError(int uid)
        {
            Promise promise = _uidMap[uid];

            promise.OnError();
            promise.Dispose();
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
        protected TaskCompletionSource<TResult> _tcs;

        internal Promise(int uid) : base(uid)
        {
            _tcs = new TaskCompletionSource<TResult>();
        }

        protected override void OnError()
        {
            string message = InvokeRetString("nkPromise.GetErrorMessage");
            int errorType = InvokeRetInt("nkPromise.GetErrorType");

            Exception ex;
            switch (errorType)
            {
                case 11: // "InvalidStateError"
                    ex = new InvalidOperationException(message);
                    break;
                case 12: // "NotSupportedError"
                    ex = new NotSupportedException(message);
                    break;
                case 13: // "SecurityError"
                    ex = new UnauthorizedAccessException(message);
                    break;
                case 14: // "NotAllowedError"
                    ex = new UnauthorizedAccessException(message);
                    break;
                case 15: // "AbortError"
                    ex = new Exception(message);
                    break;

                default:
                    ex = new Exception(message);
                    break;
            }

            _tcs.SetException(ex);
        }

        public Task<TResult> GetTask()
        {
            return _tcs.Task;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _tcs = null;

            base.Dispose(disposing);
        }
    }

    public class PromiseVoid : Promise
    {
        protected  TaskCompletionSource _tcs;

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
            string message = InvokeRetString("nkPromise.GetErrorMessage");
            int errorType = InvokeRetInt("nkPromise.GetErrorType");

            Exception ex;
            switch (errorType)
            {
                case 11: // "InvalidStateError"
                    ex = new InvalidOperationException(message);
                    break;
                case 12: // "NotSupportedError"
                    ex = new NotSupportedException(message);
                    break;
                case 13: // "SecurityError"
                    ex = new UnauthorizedAccessException(message);
                    break;
                case 14: // "NotAllowedError"
                    ex = new UnauthorizedAccessException(message);
                    break;
                case 15: // "AbortError"
                    ex = new Exception(message);
                    break;

                default:
                    ex = new Exception(message);
                    break;
            }

            _tcs.SetException(ex);
        }

        public Task GetTask()
        {
            return _tcs.Task;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _tcs = null;

            base.Dispose(disposing);
        }
    }

    public class PromiseBoolean : Promise<bool>
    {
        public PromiseBoolean(int uid) : base(uid)
        {
        }

        protected override void OnCompleted()
        {
            bool result = InvokeRetBool("nkPromise.GetValueBoolean");
            _tcs.SetResult(result);
        }
    }

    public class PromiseString : Promise<string>
    {
        public PromiseString(int uid) : base(uid)
        {
        }

        protected override void OnCompleted()
        {
            string result = InvokeRetString("nkPromise.GetValueString");
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
            int uid = InvokeRetInt("nkPromise.GetValueJSObject");

            TResult result = (TResult)_objectFactory(uid);
            _objectFactory = null;
            _tcs.SetResult(result);
        }

    }
}
