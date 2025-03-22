using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using Microsoft.JSInterop.Infrastructure;

namespace nkast.Wasm.Dom
{
    public partial class JSObject
    {
        private readonly WebAssemblyJSRuntime Runtime = new WasmJSRuntime();



        protected unsafe void Invoke(string identifier)
        {
            fixed (char* pidentifier = identifier)
            {
                Runtime.InvokeUnmarshalled<int, IJSVoidResult>(identifier, Uid);
            }
        }

        protected unsafe bool InvokeRetBool(string identifier)
        {
            fixed (char* pidentifier = identifier)
            {
                return Runtime.InvokeUnmarshalled<int, bool>(identifier, Uid);
            }
        }

        protected unsafe int InvokeRetInt(string identifier)
        {
            fixed (char* pidentifier = identifier)
            {
                return Runtime.InvokeUnmarshalled<int, int>(identifier, Uid);
            }
        }

        protected unsafe float InvokeRetFloat(string identifier)
        {
            fixed (char* pidentifier = identifier)
            {
                return float.Parse(Runtime.InvokeUnmarshalled<int, string>(identifier, Uid));
            }
        }

        protected unsafe string InvokeRetString(string identifier)
        {
            fixed (char* pidentifier = identifier)
            {
                return Runtime.InvokeUnmarshalled<int, string>(identifier, Uid);
            }
        }

        protected unsafe void Invoke<T1>(string identifier, T1 arg1)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, Net7Padding);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, IJSVoidResult>(identifier, Uid, args);
            }
        }

            const int Net7Padding = 0;
        protected unsafe bool InvokeRetBool<T1>(string identifier, T1 arg1)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, Net7Padding);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1>(string identifier, T1 arg1)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, Net7Padding);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1>(string identifier, T1 arg1)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, Net7Padding);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1>(string identifier, T1 arg1)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, Net7Padding);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, int>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2>(string identifier, T1 arg1, T2 arg2)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3>(string identifier, T1 arg1, T2 arg2, T3 arg3)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                return float.Parse(Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = ValueTuple.Create(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                return Runtime.InvokeUnmarshalled<int, ValueTuple<T1, T2, T3, T4, T5, T6, T7>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1,arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                return Runtime.InvokeUnmarshalled<int, FixedStruct8<T1, T2, T3, T4, T5, T6, T7, T8>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                return Runtime.InvokeUnmarshalled<int, FixedStruct9<T1, T2, T3, T4, T5, T6, T7, T8, T9>, string>(identifier, Uid, args);
            }
        }

        protected unsafe void Invoke<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
                Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, IJSVoidResult>(identifier, Uid, args);
            }
        }

        protected unsafe bool InvokeRetBool<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
                return Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, bool>(identifier, Uid, args);
            }
        }

        protected unsafe int InvokeRetInt<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
                return Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, int>(identifier, Uid, args);
            }
        }

        protected unsafe float InvokeRetFloat<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
                return float.Parse(Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, string>(identifier, Uid, args));
            }
        }

        protected unsafe string InvokeRetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(string identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, TA argA)
        {
            fixed (char* pidentifier = identifier)
            {
                var args = new FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, argA);
                return Runtime.InvokeUnmarshalled<int, FixedStructA<T1, T2, T3, T4, T5, T6, T7, T8, T9, TA>, string>(identifier, Uid, args);
            }
        }
    }
}
