using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexAttributeCollection : CachedJSObject<GPUVertexAttributeCollection>, IList<GPUVertexAttribute>
    {
        private readonly int _fid_GetLength;
        private readonly int _fid_Set;
        private readonly int _fid_Add;
        private readonly int _fid_Insert;
        private readonly int _fid_RemoveAt;
        private readonly int _fid_Clear;
        private readonly int _fid_Get;

        public int Count { get { return InvokeRetInt(_fid_GetLength); } }

        public bool IsReadOnly { get { return false; } }

        public GPUVertexAttribute this[int index]
        {
            get { return GetItem(index); }
            set
            {
                unsafe { Invoke<int, IntPtr>(_fid_Set, index, new IntPtr(&value)); }
            }
        }

        public GPUVertexAttributeCollection() : base(Register())
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUVertexAttributeCollection.Set");
            _fid_Add = RegisterFunction("nkGPUVertexAttributeCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUVertexAttributeCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUVertexAttributeCollection.Get");
        }

        public GPUVertexAttributeCollection(int count) : base(Register(count))
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUVertexAttributeCollection.Set");
            _fid_Add = RegisterFunction("nkGPUVertexAttributeCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUVertexAttributeCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUVertexAttributeCollection.Get");
        }

        internal GPUVertexAttributeCollection(int uid, bool existing) : base(uid)
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUVertexAttributeCollection.Set");
            _fid_Add = RegisterFunction("nkGPUVertexAttributeCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUVertexAttributeCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUVertexAttributeCollection.Get");
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
            unsafe { Invoke<IntPtr>(_fid_Add, new IntPtr(&item)); }
        }

        public void Insert(int index, GPUVertexAttribute item)
        {
            unsafe { Invoke<int, IntPtr>(_fid_Insert, index, new IntPtr(&item)); }
        }

        public void RemoveAt(int index)
        {
            Invoke(_fid_RemoveAt, index);
        }

        public void Clear()
        {
            Invoke(_fid_Clear);
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
            Invoke<int, IntPtr>(_fid_Get, index, new IntPtr(&item));
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
