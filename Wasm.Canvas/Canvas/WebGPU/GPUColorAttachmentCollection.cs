using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorAttachmentCollection : JSObject, IList<GPUColorAttachment>
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

        public GPUColorAttachmentCollection() : base(Register())
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUColorAttachmentCollection.Set");
            _fid_Add = RegisterFunction("nkGPUColorAttachmentCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUColorAttachmentCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUColorAttachmentCollection.Get");
        }

        public GPUColorAttachmentCollection(int count) : base(Register(count))
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUColorAttachmentCollection.Set");
            _fid_Add = RegisterFunction("nkGPUColorAttachmentCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUColorAttachmentCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUColorAttachmentCollection.Get");
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorAttachmentCollection.Create"));
        }

        private static int Register(int count)
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorAttachmentCollection.Create1"), count);
        }

        public unsafe GPUColorAttachment this[int index]
        {
            get { return GetItem(index); }
            set
            {
                GPUColorAttachmentData data = new GPUColorAttachmentData();
                data.ViewUid = value.View.Uid;
                data.LoadOp = value.LoadOp;
                data.StoreOp = value.StoreOp;
                data.ClearValue = value.ClearValue;
                data.DepthSlice = value.DepthSlice ?? -1;

                Invoke(_fid_Set, index, (int)&data);
            }
        }

        public unsafe void Add(GPUColorAttachment item)
        {
            GPUColorAttachmentData data = new GPUColorAttachmentData();
            data.ViewUid = item.View.Uid;
            data.LoadOp = item.LoadOp;
            data.StoreOp = item.StoreOp;
            data.ClearValue = item.ClearValue;
            data.DepthSlice = item.DepthSlice ?? -1;

            Invoke(_fid_Add, (int)&data);
        }

        public unsafe void Insert(int index, GPUColorAttachment item)
        {
            GPUColorAttachmentData data = new GPUColorAttachmentData();
            data.ViewUid = item.View.Uid;
            data.LoadOp = item.LoadOp;
            data.StoreOp = item.StoreOp;
            data.ClearValue = item.ClearValue;
            data.DepthSlice = item.DepthSlice ?? -1;

            Invoke(_fid_Insert, index, (int)&data);
        }

        public void RemoveAt(int index)
        {
            Invoke(_fid_RemoveAt, index);
        }

        public int IndexOf(GPUColorAttachment item)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                GPUColorAttachment other = GetItem(i);
                if (other.View == item.View
                &&  other.LoadOp == item.LoadOp
                &&  other.StoreOp == item.StoreOp
                &&  other.ClearValue.Equals(item.ClearValue)
                &&  other.DepthSlice == item.DepthSlice)
                    return i;
            }
            return -1;
        }

        public void Clear()
        {
            Invoke(_fid_Clear);
        }

        public bool Contains(GPUColorAttachment item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GPUColorAttachment[] array, int arrayIndex)
        {
            int count = Count;
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex + count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = GetItem(i);
        }

        public bool Remove(GPUColorAttachment item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        private unsafe GPUColorAttachment GetItem(int index)
        {
            GPUColorAttachmentData data = new GPUColorAttachmentData();
            Invoke(_fid_Get, index, (int)&data);

            GPUColorAttachment item = new GPUColorAttachment();
            item.View = GPUTextureView.FromUid(data.ViewUid);
            item.LoadOp = data.LoadOp;
            item.StoreOp = data.StoreOp;
            item.ClearValue = data.ClearValue;
            item.DepthSlice = (data.DepthSlice != -1) ? data.DepthSlice : (int?)null;
            return item;
        }

        public IEnumerator<GPUColorAttachment> GetEnumerator()
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
