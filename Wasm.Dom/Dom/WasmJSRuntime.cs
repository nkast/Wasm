using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
    {
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
