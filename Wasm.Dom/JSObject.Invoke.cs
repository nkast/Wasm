using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    public partial class JSObject
    {
        private readonly WebAssemblyJSRuntime Runtime = new WasmJSRuntime();



        //protected void Invoke(string identifier)
        //{
        //    InvokeRet<object>(identifier);
        //}

        //protected TRes InvokeRet<TRes>(string identifier)
        //{
        //    return Runtime.InvokeUnmarshalled<int, TRes>(identifier, Uid);
        //}

        //protected void Invoke<T1>(string identifier, T1 arg1)
        //{
        //    InvokeRet<T1, object>(identifier, arg1);
        //}

        //protected TRes InvokeRet<T1, TRes>(string identifier, T1 arg1)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1));
        //}

        //protected void Invoke<T1, T2>(string identifier, T1 arg1, T2 arg2)
        //{
        //    InvokeRet<T1, T2, object>(identifier, arg1, arg2);
        //}

        //protected TRes InvokeRet<T1, T2, TRes>(string identifier, T1 arg1, T2 arg2)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2));
        //}

        //protected void Invoke<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        //{
        //    InvokeRet<T1, T2, T3, object>(identifier, arg1, arg2, arg3);
        //}

        //protected TRes InvokeRet<T1, T2, T3, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2, arg3));
        //}

        //protected void Invoke<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        //{
        //    InvokeRet<T1, T2, T3, T4, object>(identifier, arg1, arg2, arg3, arg4);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2, arg3, arg4));
        //}

        //protected void Invoke<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        //{
        //    InvokeRet<T1, T2, T3, T4, T5, object>(identifier, arg1, arg2, arg3, arg4, arg5);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, T5, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2, arg3, arg4, arg5));
        //}

        //protected void Invoke<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        //{
        //    InvokeRet<T1, T2, T3, T4, T5, T6, object>(identifier, arg1, arg2, arg3, arg4, arg5, arg6);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, T5, T6, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6));
        //}

        //protected void Invoke<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        //{
        //    InvokeRet<T1, T2, T3, T4, T5, T6, T7, object>(identifier, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, T5, T6, T7, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        //{
        //    return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, TRes>(identifier, Uid,
        //        ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        //}

        //protected void Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        //{
        //    InvokeRet<T1, T2, T3, T4, T5, T6, T7, T8, object>(identifier, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, T5, T6, T7, T8, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        //{
        //    var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(
        //        value1: arg1,
        //        value2: arg2,
        //        value3: arg3,
        //        value4: arg4,
        //        value5: arg5,
        //        value6: arg6,
        //        value7: arg7,
        //        value8: arg8
        //    );
        //    return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, TRes>(identifier, Uid, args);
        //}

        //protected void Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        //{
        //    InvokeRet<T1, T2, T3, T4, T5, T6, T7, T8, T9, object>(identifier, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        //}

        //protected TRes InvokeRet<T1, T2, T3, T4, T5, T6, T7, T8, T9, TRes>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        //{
        //    var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8,arg9);
        //    return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, TRes>(identifier, Uid, args);
        //}
    }
}
