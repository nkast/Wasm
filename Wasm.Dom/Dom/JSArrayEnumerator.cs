using System;
using System.Collections;
using System.Collections.Generic;

namespace nkast.Wasm.Dom
{
    public struct JSArrayEnumerator<T> : IEnumerator<T>
    {
        private IReadOnlyList<T> _collection;
        private T _current;
        private int _index;

        public JSArrayEnumerator(IReadOnlyList<T> collection)
        {
            _collection = collection;
            _current = default;
            _index = 0;
        }

        public T Current { get { return _current; } }

        object IEnumerator.Current { get { return _current; } }

        public bool MoveNext()
        {
            if (_index < _collection.Count)
            {
                _current = _collection[_index];
                _index++;
                return true;
            }

            _current = default;
            return false;
        }

        public void Reset()
        {
            _current = default;
            _index = 0;
        }

        public void Dispose()
        {
            _collection = null;
            _current = default;
            _index = 0;
        }
    }
}
