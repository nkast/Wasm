using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexBufferLayoutCollection : JSObject, IList<GPUVertexBufferLayout>
    {
        private readonly int _fid_GetLength;
        private readonly int _fid_Set;
        private readonly int _fid_Add;
        private readonly int _fid_Insert;
        private readonly int _fid_RemoveAt;
        private readonly int _fid_Clear;
        private readonly int _fid_GetItem;

        public int Count { get { return InvokeRetInt(_fid_GetLength); } }

        public bool IsReadOnly { get { return false; } }

        public GPUVertexBufferLayoutCollection() : base(Register())
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkJSArray.Set");
            _fid_Add = RegisterFunction("nkJSArray.Add");
            _fid_Insert = RegisterFunction("nkJSArray.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_GetItem = RegisterFunction("nkJSArray.GetItem");
        }

        public GPUVertexBufferLayoutCollection(int count) : base(Register(count))
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkJSArray.Set");
            _fid_Add = RegisterFunction("nkJSArray.Add");
            _fid_Insert = RegisterFunction("nkJSArray.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_GetItem = RegisterFunction("nkJSArray.GetItem");
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexBufferLayoutCollection.Create"));
        }

        private static int Register(int count)
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexBufferLayoutCollection.Create1"), count);
        }

        public GPUVertexBufferLayout this[int index]
        {
            get { return GetItem(index); }
            set { Invoke(_fid_Set, index, value.Uid); }
        }

        public void Add(GPUVertexBufferLayout item)
        {
            Invoke(_fid_Add, item.Uid);
        }

        public void Insert(int index, GPUVertexBufferLayout item)
        {
            Invoke(_fid_Insert, index, item.Uid);
        }

        public void RemoveAt(int index)
        {
            Invoke(_fid_RemoveAt, index);
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
            Invoke(_fid_Clear);
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
            int uid = InvokeRetInt(_fid_GetItem, index);
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
