using System;
using System.Collections.Generic;
using UnityEngine;

namespace Joris
{
    [Serializable]
    public abstract class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        protected abstract List<SerializableKeyValuePair<TKey, TValue>> _keyValuePairs { get; set; }

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            _keyValuePairs.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                _keyValuePairs.Add(new SerializableKeyValuePair<TKey, TValue>(pair.Key, pair.Value));
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize()
        {
            Clear();

            for (int i = 0; i < _keyValuePairs.Count; i++)
            {
                this[_keyValuePairs[i].Key] = _keyValuePairs[i].Value;
            }
        }
    }

    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue> : IEquatable<SerializableKeyValuePair<TKey, TValue>>
    {
        [SerializeField]
        TKey _key;
        public TKey Key { get { return _key; } }

        [SerializeField]
        TValue _value;
        public TValue Value { get { return _value; } }

        public SerializableKeyValuePair()
        {

        }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public bool Equals(SerializableKeyValuePair<TKey, TValue> other)
        {
            var comparer1 = EqualityComparer<TKey>.Default;
            var comparer2 = EqualityComparer<TValue>.Default;

            return comparer1.Equals(_key, other._key) &&
                comparer2.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            var comparer1 = EqualityComparer<TKey>.Default;
            var comparer2 = EqualityComparer<TValue>.Default;

            int h0;
            h0 = comparer1.GetHashCode(_key);
            h0 = (h0 << 5) + h0 ^ comparer2.GetHashCode(_value);
            return h0;
        }

        public override string ToString()
        {
            return string.Format("(Key: {0}, Value: {1})", _key, _value);
        }
    }
}
