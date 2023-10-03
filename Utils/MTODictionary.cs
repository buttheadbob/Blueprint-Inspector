using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Blueprint_Inspector.Utils;

public class MTODictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>, 
    INotifyCollectionChanged, INotifyPropertyChanged 
    where TKey : notnull
    where TValue : notnull
{
    private Dictionary<TKey, TValue> _internalDictionary = new Dictionary<TKey, TValue>();
    private readonly object _syncRoot = new object();

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public TValue this[TKey key]
    {
        get => _internalDictionary[key];
        set
        {
            if (_internalDictionary.ContainsKey(key))
            {
                _internalDictionary[key] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value));
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public ICollection<TKey> Keys => _internalDictionary.Keys;

    public ICollection<TValue> Values => _internalDictionary.Values;

    public int Count => _internalDictionary.Count;

    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value)
    {
        lock (_syncRoot)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            _internalDictionary.Add(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
            OnPropertyChanged(nameof(Count));
        }
    }

    public bool Remove(TKey key)
    {
        lock (_syncRoot)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            if (_internalDictionary.ContainsKey(key))
            {
                TValue value = _internalDictionary[key];
                var isRemoved = _internalDictionary.Remove(key);
                if (isRemoved)
                {
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value)));
                    OnPropertyChanged(nameof(Count));
                }
                return isRemoved;
            }
            return false;
        }
    }

    public bool ContainsKey(TKey key) => _internalDictionary.ContainsKey(key);

    public bool TryGetValue(TKey key, out TValue value) => _internalDictionary.TryGetValue(key, out value);

    public void Clear()
    {
        lock (_syncRoot)
        {
            _internalDictionary.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(nameof(Count));
        }
    }

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public bool Contains(KeyValuePair<TKey, TValue> item) => _internalDictionary.ContainsKey(item.Key) && EqualityComparer<TValue>.Default.Equals(_internalDictionary[item.Key], item.Value);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((IDictionary<TKey, TValue>)_internalDictionary).CopyTo(array, arrayIndex);

    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _internalDictionary.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _internalDictionary.GetEnumerator();

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(this, e);

    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}