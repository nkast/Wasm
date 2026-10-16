using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace nkast.Wasm.JSInterop
{
    public abstract class Promise : JSObject
    {
        private static readonly Dictionary<int, Promise> _uidMap = new Dictionary<int, Promise>();

        private readonly int _fid_RegisterEvents;

        internal Promise(int uid) : base(uid)
        {
            _fid_RegisterEvents = RegisterFunction("nkPromise.RegisterEvents");

            _uidMap.Add(uid, this);
            Invoke(_fid_RegisterEvents);
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

        private readonly int _fid_GetErrorMessage;
        private readonly int _fid_GetErrorType;

        internal Promise(int uid) : base(uid)
        {
            _fid_GetErrorMessage = RegisterFunction("nkPromise.GetErrorMessage");
            _fid_GetErrorType = RegisterFunction("nkPromise.GetErrorType");

            _tcs = new TaskCompletionSource<TResult>();
        }

        protected override void OnError()
        {
            string message = InvokeRetString(_fid_GetErrorMessage);
            int errorType = InvokeRetInt(_fid_GetErrorType);

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

        private readonly int _fid_GetErrorMessage;
        private readonly int _fid_GetErrorType;

        public PromiseVoid(int uid) : base(uid)
        {
            _fid_GetErrorMessage = RegisterFunction("nkPromise.GetErrorMessage");
            _fid_GetErrorType = RegisterFunction("nkPromise.GetErrorType");

            _tcs = new TaskCompletionSource();
        }

        protected override void OnCompleted()
        {
            _tcs.SetResult();
        }

        protected override void OnError()
        {
            string message = InvokeRetString(_fid_GetErrorMessage);
            int errorType = InvokeRetInt(_fid_GetErrorType);

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
        private readonly int _fid_GetValueBoolean;

        public PromiseBoolean(int uid) : base(uid)
        {
            _fid_GetValueBoolean = RegisterFunction("nkPromise.GetValueBoolean");
        }

        protected override void OnCompleted()
        {
            bool result = InvokeRetBool(_fid_GetValueBoolean);
            _tcs.SetResult(result);
        }
    }

    public class PromiseString : Promise<string>
    {
        private readonly int _fid_GetValueString;

        public PromiseString(int uid) : base(uid)
        {
            _fid_GetValueString = RegisterFunction("nkPromise.GetValueString");
        }

        protected override void OnCompleted()
        {
            string result = InvokeRetString(_fid_GetValueString);
            _tcs.SetResult(result);
        }
    }

    public class PromiseJSObject<TResult> : Promise<TResult>
        where TResult : JSObject
    {
        private readonly int _fid_GetValueJSObject;
        Func<int, JSObject> _objectFactory;

        public PromiseJSObject(int uid, Func<int,JSObject> objectFactory) : base(uid)
        {
            _fid_GetValueJSObject = RegisterFunction("nkPromise.GetValueJSObject");
            _objectFactory = objectFactory;
        }

        protected override void OnCompleted()
        {
            int uid = InvokeRetInt(_fid_GetValueJSObject);

            TResult result = (TResult)_objectFactory(uid);
            _objectFactory = null;
            _tcs.SetResult(result);
        }

    }
}
