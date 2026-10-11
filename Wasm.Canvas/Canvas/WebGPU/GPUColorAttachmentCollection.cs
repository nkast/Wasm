using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorAttachmentCollection : JSObject, IList<GPUColorAttachment>
    {
        public int Count { get { return InvokeRetInt(RegisterFunction("nkJSArray.GetLength")); } }

        public bool IsReadOnly { get { return false; } }

        public GPUColorAttachmentCollection() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorAttachmentCollection.Create"));
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

                Invoke(RegisterFunction("nkGPUColorAttachmentCollection.Set"), index, (int)&data);
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

            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.Add"), (int)&data);
        }

        public unsafe void Insert(int index, GPUColorAttachment item)
        {
            GPUColorAttachmentData data = new GPUColorAttachmentData();
            data.ViewUid = item.View.Uid;
            data.LoadOp = item.LoadOp;
            data.StoreOp = item.StoreOp;
            data.ClearValue = item.ClearValue;
            data.DepthSlice = item.DepthSlice ?? -1;

            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.Insert"), index, (int)&data);
        }

        public void RemoveAt(int index)
        {
            Invoke(RegisterFunction("nkJSArray.RemoveAt"), index);
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
            Invoke(RegisterFunction("nkJSArray.Clear"));
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
            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.Get"), index, (int)&data);

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
