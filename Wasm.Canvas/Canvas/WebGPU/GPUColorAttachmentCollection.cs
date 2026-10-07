using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorAttachmentCollection : JSObject, IList<GPUColorAttachment>
    {
        private readonly List<GPUColorAttachment> _items = new List<GPUColorAttachment>();

        public GPUColorAttachmentCollection() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorAttachmentCollection.Create"));
        }

        public int Count { get { return _items.Count; } }

        public bool IsReadOnly { get { return false; } }

        public GPUColorAttachment this[int index]
        {
            get { return _items[index]; }
            set
            {
                if ((uint)index >= (uint)_items.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                InvokeWithData("nkGPUColorAttachmentCollection.Set", index, value);
                _items[index] = value;
            }
        }

        public void Add(GPUColorAttachment item)
        {
            InvokeWithData("nkGPUColorAttachmentCollection.Add", -1, item);
            _items.Add(item);
        }

        public void Insert(int index, GPUColorAttachment item)
        {
            if ((uint)index > (uint)_items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            InvokeWithData("nkGPUColorAttachmentCollection.Insert", index, item);
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Invoke(RegisterFunction("nkGPUColorAttachmentCollection.RemoveAt"), index);
            _items.RemoveAt(index);
        }

        public int IndexOf(GPUColorAttachment item)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                GPUColorAttachment other = _items[i];
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
            _items.Clear();
        }

        public bool Contains(GPUColorAttachment item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GPUColorAttachment[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(GPUColorAttachment item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public IEnumerator<GPUColorAttachment> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _items.Clear();
            }

            base.Dispose(disposing);
        }
    }
}
