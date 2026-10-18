using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUColorTargetStateCollection : JSObject, IList<GPUColorTargetState>
    {

        public int Count
        {
            get { return InvokeRetInt(RegisterFunction("nkJSArray.GetLength")); } 
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
                unsafe { Invoke<int, IntPtr>(RegisterFunction("nkGPUColorTargetStateCollection.Set"), index, new IntPtr(&data)); }
            }
        }

        public GPUColorTargetStateCollection() : base(Register())
        {
        }

        public GPUColorTargetStateCollection(int count) : base(Register(count))
        {
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
            unsafe { Invoke<IntPtr>(RegisterFunction("nkGPUColorTargetStateCollection.Add"), new IntPtr(&data)); }
        }

        public void Insert(int index, GPUColorTargetState item)
        {
            GPUColorTargetStateData data = GPUColorTargetStateData.FromState(item);
            unsafe { Invoke<int, IntPtr>(RegisterFunction("nkGPUColorTargetStateCollection.Insert"), index, new IntPtr(&data)); }
        }

        public void RemoveAt(int index)
        {
            Invoke(RegisterFunction("nkGPUColorTargetStateCollection.RemoveAt"), index);
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
            Invoke(RegisterFunction("nkGPUColorTargetStateCollection.Clear"));
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
            Invoke<int, IntPtr>(RegisterFunction("nkGPUColorTargetStateCollection.Get"), index, new IntPtr(&data));
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
