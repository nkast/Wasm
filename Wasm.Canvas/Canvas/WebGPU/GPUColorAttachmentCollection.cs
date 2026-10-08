using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorAttachmentCollection : JSObject, IList<GPUColorAttachment>
    {
        public GPUColorAttachmentCollection() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorAttachmentCollection.Create"));
        }

        public int Count { get { return InvokeRetInt(RegisterFunction("nkGPUColorAttachmentCollection.GetCount")); } }

        public bool IsReadOnly { get { return false; } }

        public GPUColorAttachment this[int index]
        {
            get { return GetItem(index); }
            set
            {
                if ((uint)index >= (uint)Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                InvokeWithData("nkGPUColorAttachmentCollection.Set", index, value);
            }
        }

        public void Add(GPUColorAttachment item)
        {
            InvokeWithData("nkGPUColorAttachmentCollection.Add", -1, item);
        }

        public void Insert(int index, GPUColorAttachment item)
        {
            if ((uint)index > (uint)Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            InvokeWithData("nkGPUColorAttachmentCollection.Insert", index, item);
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.RemoveAt"), index);
        }

        public int IndexOf(GPUColorAttachment item)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                GPUColorAttachment other = GetItem(i);
                if (other.View == item.View
                    && other.LoadOp == item.LoadOp
                    && other.StoreOp == item.StoreOp
                    && Nullable.Equals(other.ClearValue, item.ClearValue)
                    && other.DepthSlice == item.DepthSlice)
                    return i;
            }
            return -1;
        }

        private unsafe void InvokeWithData(string function, int index, GPUColorAttachment item)
        {
            GPUColorAttachmentData data = new GPUColorAttachmentData();
            data.ViewUid = item.View.Uid;
            data.LoadOp = item.LoadOp;
            data.StoreOp = item.StoreOp;
            data.ClearValue = item.ClearValue.GetValueOrDefault();
            data.DepthSlice = item.DepthSlice ?? -1;

            if (index < 0)
                Invoke(RegisterFunction(function), (int)&data);
            else
                Invoke(RegisterFunction(function), index, (int)&data);
        }

        public void Clear()
        {
            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.Clear"));
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
            if ((uint)index >= (uint)Count)
                throw new ArgumentOutOfRangeException(nameof(index));

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
            base.Dispose(disposing);
        }
    }
}
