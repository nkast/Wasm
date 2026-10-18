using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexAttributeCollection : CachedJSObject<GPUVertexAttributeCollection>, IList<GPUVertexAttribute>
    {
        public int Count { get { return InvokeRetInt(RegisterFunction("nkJSArray.GetLength")); } }

        public bool IsReadOnly { get { return false; } }

        public GPUVertexAttribute this[int index]
        {
            get { return GetItem(index); }
            set
            {
                unsafe { Invoke<int, IntPtr>(RegisterFunction("nkGPUVertexAttributeCollection.Set"), index, new IntPtr(&value)); }
            }
        }

        public GPUVertexAttributeCollection() : base(Register())
        {
        }

        public GPUVertexAttributeCollection(int count) : base(Register(count))
        {
        }

        internal GPUVertexAttributeCollection(int uid, bool existing) : base(uid)
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexAttributeCollection.Create"));
        }

        private static int Register(int count)
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexAttributeCollection.Create1"), count);
        }

        public void Add(GPUVertexAttribute item)
        {
            unsafe { Invoke<IntPtr>(RegisterFunction("nkGPUVertexAttributeCollection.Add"), new IntPtr(&item)); }
        }

        public void Insert(int index, GPUVertexAttribute item)
        {
            unsafe { Invoke<int, IntPtr>(RegisterFunction("nkGPUVertexAttributeCollection.Insert"), index, new IntPtr(&item)); }
        }

        public void RemoveAt(int index)
        {
            Invoke(RegisterFunction("nkJSArray.RemoveAt"), index);
        }

        public void Clear()
        {
            Invoke(RegisterFunction("nkJSArray.Clear"));
        }

        public int IndexOf(GPUVertexAttribute item)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                GPUVertexAttribute other = GetItem(i);
                if (other.Format == item.Format
                    && other.Offset == item.Offset
                    && other.ShaderLocation == item.ShaderLocation)
                    return i;
            }
            return -1;
        }

        public bool Contains(GPUVertexAttribute item)
        {
            return IndexOf(item) != -1;
        }

        public bool Remove(GPUVertexAttribute item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public void CopyTo(GPUVertexAttribute[] array, int arrayIndex)
        {
            int count = Count;

            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = GetItem(i);
        }

        private unsafe GPUVertexAttribute GetItem(int index)
        {
            GPUVertexAttribute item = new GPUVertexAttribute();
            Invoke<int, IntPtr>(RegisterFunction("nkGPUVertexAttributeCollection.Get"), index, new IntPtr(&item));
            return item;
        }

        public IEnumerator<GPUVertexAttribute> GetEnumerator()
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                yield return GetItem(i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
