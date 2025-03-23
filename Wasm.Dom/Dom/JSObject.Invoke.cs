﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    public partial class JSObject
    {
        private readonly WebAssemblyJSRuntime Runtime = new WasmJSRuntime();

        private static Dictionary<string, int> _fidMap = new Dictionary<string, int>();

        [JSImport("globalThis.window.nkJSObject.JSRegisterFunction")]
        private static partial int JSRegisterFunction(int pidentifier, int identifierLength);

        [JSImport("globalThis.window.nkJSObject.JSInvoke1Void")]
        private static partial void JSInvoke1Void(int fid, int uid);

        [JSImport("globalThis.window.nkJSObject.JSInvoke1Bool")]
        private static partial bool JSInvoke1Bool(int fid, int uid);

        [JSImport("globalThis.window.nkJSObject.JSInvoke2Void")]
        private static partial void JSInvoke2Void(int fid, int uid, int d);

        [JSImport("globalThis.window.nkJSObject.JSInvoke2Bool")]
        private static partial bool JSInvoke2Bool(int fid, int uid, int d);


        private static unsafe int RegisterFunction(string identifier)
        {
            if (_fidMap.TryGetValue(identifier, out int fid))
                return fid;

            fixed (char* pidentifier = identifier)
            {
                fid = JSRegisterFunction((int)pidentifier, identifier.Length);
            }

            _fidMap.Add(identifier, fid);
            return fid;
        }


        protected void Invoke(string identifier)
        {
            int fid = RegisterFunction(identifier);
            JSInvoke1Void(fid, Uid);
        }

        protected unsafe bool InvokeRetBool(string identifier)
        {
            int fid = RegisterFunction(identifier);
            return JSInvoke1Bool(fid, Uid);
        }

        protected int InvokeRetInt(string identifier)
        {
            return Runtime.InvokeUnmarshalled<int, int>(identifier, Uid);
        }

        protected unsafe float InvokeRetFloat(string identifier)
        {
            return float.Parse(Runtime.InvokeUnmarshalled<int, string>(identifier, Uid));
        }

        protected unsafe string InvokeRetString(string identifier)
        {
            return Runtime.InvokeUnmarshalled<int, string>(identifier, Uid);
        }

        protected unsafe void Invoke<T1>(string identifier, T1 arg1)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, Net7Padding);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        const int Net7Padding = 0;
        protected unsafe bool InvokeRetBool<T1>(string identifier, T1 arg1)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, Net7Padding);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1>(string identifier, T1 arg1)
        {
            var args = ValueTuple.Create(arg1, Net7Padding);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1>(string identifier, T1 arg1)
        {
            var args = ValueTuple.Create(arg1, Net7Padding);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1>(string identifier, T1 arg1)
        {
            var args = ValueTuple.Create(arg1, Net7Padding);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            var args = ValueTuple.Create(arg1, arg2);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            var args = ValueTuple.Create(arg1, arg2);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            var args = ValueTuple.Create(arg1, arg2);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            int fid = RegisterFunction(identifier);
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1,arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8,arg9);
            return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, string>(identifier, Uid, args);
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
            JSInvoke2Void(fid, Uid, (int)&args);
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            int fid = RegisterFunction(identifier);
            var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
            return JSInvoke2Bool(fid, Uid, (int)&args);
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
            return Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, int>(identifier, Uid, args);
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
            return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, string>(identifier, Uid, args));
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
            return Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, string>(identifier, Uid, args);
        }
    }

    internal struct FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public readonly T1 Value1;
        public readonly T2 Value2;
        public readonly T3 Value3;
        public readonly T4 Value4;
        public readonly T5 Value5;
        public readonly T6 Value6;
        public readonly T7 Value7;
        public readonly T8 Value8;

        public FixedStruct8(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8) : this()
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
            Value6 = value6;
            Value7 = value7;
            Value8 = value8;
        }
    }

    internal struct FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public readonly T1 Value1;
        public readonly T2 Value2;
        public readonly T3 Value3;
        public readonly T4 Value4;
        public readonly T5 Value5;
        public readonly T6 Value6;
        public readonly T7 Value7;
        public readonly T8 Value8;
        public readonly T9 Value9;

        public FixedStruct9(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9) : this()
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
            Value6 = value6;
            Value7 = value7;
            Value8 = value8;
            Value9 = value9;
        }
    }

    internal struct FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>
    {
        public readonly T1 Value1;
        public readonly T2 Value2;
        public readonly T3 Value3;
        public readonly T4 Value4;
        public readonly T5 Value5;
        public readonly T6 Value6;
        public readonly T7 Value7;
        public readonly T8 Value8;
        public readonly T9 Value9;
        public readonly TA ValueA;

        public FixedStructA(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, TA valueA) : this()
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
            Value6 = value6;
            Value7 = value7;
            Value8 = value8;
            Value9 = value9;
            ValueA = valueA;
        }
    }
}
