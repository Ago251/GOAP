using System;
using System.Collections.Generic;

namespace Ecosystem.AI.GOAP
{
    public class WorldState
    {
        private Dictionary<string, object> _data = new();

        public T GetValue<T>(string key)
        {
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, default(T));
            }

            return  (T)_data[key];
        }

        public void SetValue<T>(string key, T value)
        {
            _data[key] = value;
        }

        public WorldState Clone()
        {
            var result = new WorldState();
            foreach (var entry in _data)
            {
                result._data[entry.Key] = entry.Value is ICloneable cloneable ? cloneable.Clone() : entry.Value;
            }

            return result;
        }
    }
}