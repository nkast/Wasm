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
            get { throw new NotImplementedException(); }
            set
            {
                throw new NotImplementedException();
            }
        }

        public GPUVertexAttributeCollection() : base(Register())
        {
        }

        internal GPUVertexAttributeCollection(int uid) : base(uid)
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexAttributeCollection.Create"));
        }

        public void Add(GPUVertexAttribute item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, GPUVertexAttribute item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


        public IEnumerator<GPUVertexAttribute> GetEnumerator()
        {
            throw new NotImplementedException();
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
