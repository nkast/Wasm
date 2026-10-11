using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBindGroupEntryCollection : JSObject, IList<GPUBindGroupEntry>
    {
        public int Count { get { return InvokeRetInt(RegisterFunction("nkJSArray.GetLength")); } }

        public bool IsReadOnly { get { return false; } }

        public unsafe GPUBindGroupEntry this[int index]
        {
            get { return GetItem(index); }
            set
            {
                GPUBindGroupEntryData data = new GPUBindGroupEntryData();
                data.Binding = value.Binding;
                data.ResourceUid = value.Resource.Uid;

                Invoke(RegisterFunction("nkGPUBindGroupEntryCollection.Set"), index, (int)&data);
            }
        }

        public GPUBindGroupEntryCollection() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUBindGroupEntryCollection.Create"));
        }


        public unsafe void Add(GPUBindGroupEntry item)
        {
            GPUBindGroupEntryData data = new GPUBindGroupEntryData();
            data.Binding = item.Binding;
            data.ResourceUid = item.Resource.Uid;

            Invoke(RegisterFunction("nkGPUBindGroupEntryCollection.Add"), (int)&data);
        }

        public unsafe void Insert(int index, GPUBindGroupEntry item)
        {
            GPUBindGroupEntryData data = new GPUBindGroupEntryData();
            data.Binding = item.Binding;
            data.ResourceUid = item.Resource.Uid;

            Invoke(RegisterFunction("nkGPUBindGroupEntryCollection.Insert"), index, (int)&data);
        }

        public void RemoveAt(int index)
        {
            Invoke(RegisterFunction("nkJSArray.RemoveAt"), index);
        }

        public int IndexOf(GPUBindGroupEntry item)
        {
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                GPUBindGroupEntry other = GetItem(i);
                if (other.Binding == item.Binding
                &&  other.Resource == item.Resource)
                    return i;
            }
            return -1;
        }

        public void Clear()
        {
            Invoke(RegisterFunction("nkJSArray.Clear"));
        }

        public bool Contains(GPUBindGroupEntry item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GPUBindGroupEntry[] array, int arrayIndex)
        {
            int count = Count;
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex + count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = GetItem(i);
        }

        public bool Remove(GPUBindGroupEntry item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        private unsafe GPUBindGroupEntry GetItem(int index)
        {
            GPUBindGroupEntryData data = new GPUBindGroupEntryData();
            Invoke(RegisterFunction("nkGPUBindGroupEntryCollection.Get"), index, (int)&data);

            GPUBindGroupEntry item = new GPUBindGroupEntry();
            item.Binding = data.Binding;
            item.Resource = (IGPUBindingResource)GPUBuffer.FromUid(data.ResourceUid)
                         ?? GPUTextureView.FromUid(data.ResourceUid);
            return item;
        }

        public IEnumerator<GPUBindGroupEntry> GetEnumerator()
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