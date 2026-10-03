using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorTargetStateCollection : JSObject, IList<GPUColorTargetState>
    {
        private readonly int _fid_GetLength;
        private readonly int _fid_Set;
        private readonly int _fid_Add;
        private readonly int _fid_Insert;
        private readonly int _fid_RemoveAt;
        private readonly int _fid_Clear;
        private readonly int _fid_Get;


        public int Count
        {
            get { return InvokeRetInt(_fid_GetLength); } 
        }

        public bool IsReadOnly
        {
           get { return false; }
        }

        public GPUColorTargetState this[int index]
        {
            get
            {
                return GetItem(index);
            }
            set
            {
                GPUColorTargetStateData data = GPUColorTargetStateData.FromState(value);
                unsafe { Invoke<int, IntPtr>(_fid_Set, index, new IntPtr(&data)); }
            }
        }

        public GPUColorTargetStateCollection() : base(Register())
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUColorTargetStateCollection.Set");
            _fid_Add = RegisterFunction("nkGPUColorTargetStateCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUColorTargetStateCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkGPUColorTargetStateCollection.RemoveAt");
            _fid_Clear = RegisterFunction("nkGPUColorTargetStateCollection.Clear");
            _fid_Get = RegisterFunction("nkGPUColorTargetStateCollection.Get");
        }

        public GPUColorTargetStateCollection(int count) : base(Register(count))
        {
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
            _fid_Set = RegisterFunction("nkGPUColorTargetStateCollection.Set");
            _fid_Add = RegisterFunction("nkGPUColorTargetStateCollection.Add");
            _fid_Insert = RegisterFunction("nkGPUColorTargetStateCollection.Insert");
            _fid_RemoveAt = RegisterFunction("nkGPUColorTargetStateCollection.RemoveAt");
            _fid_Clear = RegisterFunction("nkGPUColorTargetStateCollection.Clear");
            _fid_Get = RegisterFunction("nkGPUColorTargetStateCollection.Get");
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorTargetStateCollection.Create"));
        }

        private static int Register(int count)
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUColorTargetStateCollection.Create1"), count);
        }

        public void Add(GPUColorTargetState item)
        {
            GPUColorTargetStateData data = GPUColorTargetStateData.FromState(item);
            unsafe { Invoke<IntPtr>(_fid_Add, new IntPtr(&data)); }
        }

        public void Insert(int index, GPUColorTargetState item)
        {
            GPUColorTargetStateData data = GPUColorTargetStateData.FromState(item);
            unsafe { Invoke<int, IntPtr>(_fid_Insert, index, new IntPtr(&data)); }
        }

        public void RemoveAt(int index)
        {
            Invoke(_fid_RemoveAt, index);
        }

        public bool Remove(GPUColorTargetState item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            Invoke(_fid_Clear);
        }

        public int IndexOf(GPUColorTargetState item)
        {
            GPUColorTargetStateData itemData = GPUColorTargetStateData.FromState(item);
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                if (GetItemData(i).Equals(itemData))
                    return i;
            }
            return -1;
        }

        public bool Contains(GPUColorTargetState item)
        {
            return IndexOf(item) != -1;
        }

        private GPUColorTargetState GetItem(int index)
        {
            return GetItemData(index).ToState();
        }

        private unsafe GPUColorTargetStateData GetItemData(int index)
        {
            GPUColorTargetStateData data = new GPUColorTargetStateData();
            Invoke<int, IntPtr>(_fid_Get, index, new IntPtr(&data));
            return data;
        }

        public void CopyTo(GPUColorTargetState[] array, int arrayIndex)
        {
            int count = Count;
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex + count > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < count; i++)
                array[arrayIndex + i] = GetItem(i);
        }

        public IEnumerator<GPUColorTargetState> GetEnumerator()
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
