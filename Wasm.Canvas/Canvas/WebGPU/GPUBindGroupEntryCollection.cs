using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBindGroupEntryCollection : JSObject, IList<GPUBindGroupEntry>
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

        public unsafe GPUBindGroupEntry this[int index]
        {
            get { return GetItem(index); }
            set
            {
                GPUBindGroupEntryData data = new GPUBindGroupEntryData();
                data.Binding = value.Binding;
                data.ResourceUid = value.Resource.Uid;

                Invoke(_fid_Set, index, (int)&data);
            }
        }

        public GPUBindGroupEntryCollection() : base(Register())
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUBindGroupEntryCollection.Set");
            _fid_Add = RegisterFunction("nkGPUBindGroupEntryCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUBindGroupEntryCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUBindGroupEntryCollection.Get");
        }

        public GPUBindGroupEntryCollection(int count) : base(Register(count))
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUBindGroupEntryCollection.Set");
            _fid_Add = RegisterFunction("nkGPUBindGroupEntryCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUBindGroupEntryCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkJSArray.RemoveAt");
            _fid_Clear = RegisterFunction("nkJSArray.Clear");
            _fid_Get = RegisterFunction("nkGPUBindGroupEntryCollection.Get");
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUBindGroupEntryCollection.Create"));
        }

        private static int Register(int count)
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUBindGroupEntryCollection.Create1"), count);
        }


        public unsafe void Add(GPUBindGroupEntry item)
        {
            GPUBindGroupEntryData data = new GPUBindGroupEntryData();
            data.Binding = item.Binding;
            data.ResourceUid = item.Resource.Uid;

            Invoke(_fid_Add, (int)&data);
        }

        public unsafe void Insert(int index, GPUBindGroupEntry item)
        {
            GPUBindGroupEntryData data = new GPUBindGroupEntryData();
            data.Binding = item.Binding;
            data.ResourceUid = item.Resource.Uid;

            Invoke(_fid_Insert, index, (int)&data);
        }

        public void RemoveAt(int index)
        {
            Invoke(_fid_RemoveAt, index);
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
            Invoke(_fid_Clear);
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
            Invoke(_fid_Get, index, (int)&data);

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