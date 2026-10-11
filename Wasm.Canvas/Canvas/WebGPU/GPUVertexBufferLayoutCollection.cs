using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexBufferLayoutCollection : JSObject, IList<GPUVertexBufferLayout>
    {
        public int Count { get { return InvokeRetInt(RegisterFunction("nkJSArray.GetLength")); } }

        public bool IsReadOnly { get { return false; } }

        public GPUVertexBufferLayoutCollection() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexBufferLayoutCollection.Create"));
        }

        public GPUVertexBufferLayout this[int index]
        {
            get { return GetItem(index); }
            set { Invoke(RegisterFunction("nkJSArray.Set"), index, value.Uid); }
        }

        public void Add(GPUVertexBufferLayout item)
        {
            Invoke(RegisterFunction("nkJSArray.Add"), item.Uid);
        }

        public void Insert(int index, GPUVertexBufferLayout item)
        {
            Invoke(RegisterFunction("nkJSArray.Insert"), index, item.Uid);
        }

        public void RemoveAt(int index)
        {
            Invoke(RegisterFunction("nkJSArray.RemoveAt"), index);
        }

        public int IndexOf(GPUVertexBufferLayout item)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                if (GetItem(i).Uid == item.Uid)
                    return i;
            }
            return -1;
        }

        public void Clear()
        {
            Invoke(RegisterFunction("nkJSArray.Clear"));
        }

        public bool Contains(GPUVertexBufferLayout item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GPUVertexBufferLayout[] array, int arrayIndex)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = GetItem(i);
        }

        public bool Remove(GPUVertexBufferLayout item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        private GPUVertexBufferLayout GetItem(int index)
        {
            int uid = InvokeRetInt(RegisterFunction("nkJSArray.GetItem"), index);
            GPUVertexBufferLayout item = GPUVertexBufferLayout.FromUid(uid);
            if (item == null)
                item = new GPUVertexBufferLayout(uid);
            return item;
        }

        public IEnumerator<GPUVertexBufferLayout> GetEnumerator()
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
