using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Define;

namespace DataStructure
{
    #region 
    public partial class O_Dictionary<TKey,TValue>
        {
            // we keep these synced
            List<KeyValuePair<TKey,TValue>> _innerList;
            Dictionary<TKey, TValue> _innerDictionary;
            IEqualityComparer<TKey> _comparer = null;
            /// <summary>
            /// Creates an ordered dictionary with the specified capacity and comparer
            /// </summary>
            /// <param name="capacity">The initial capacity</param>
            /// <param name="comparer">The comparer</param>
            public O_Dictionary(int capacity,IEqualityComparer<TKey> comparer)
            {
                _innerDictionary = new Dictionary<TKey, TValue>(capacity, comparer);
                _innerList = new List<KeyValuePair<TKey,TValue>>(capacity);
                _comparer = comparer;
            }
            /// <summary>
            /// Creates an ordered dictionary with the specified items and the specified comparer
            /// </summary>
            /// <param name="collection">The collection or dictionary to copy from</param>
            /// <param name="comparer">The comparer to use</param>
            public O_Dictionary(IEnumerable<KeyValuePair<TKey,TValue>> collection, IEqualityComparer<TKey> comparer)
            {
                _innerDictionary = new Dictionary<TKey, TValue>(comparer);
                _innerList = new List<KeyValuePair<TKey, TValue>>();
                _AddValues(collection);
                _comparer = comparer;
            }
            /// <summary>
            /// Creates an ordered dictionary with the specified capacity
            /// </summary>
            /// <param name="capacity">The initial capacity</param>
            public O_Dictionary(int capacity)
            {
                _innerDictionary = new Dictionary<TKey, TValue>(capacity);
                _innerList = new List<KeyValuePair<TKey, TValue>>(capacity);
            }
            /// <summary>
            /// Creates an ordered dictionary filled with the specified collection or dictionary
            /// </summary>
            /// <param name="collection">The collection or dictionary to copy</param>
            public O_Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            {
                _innerDictionary = new Dictionary<TKey, TValue>();
                _innerList = new List<KeyValuePair<TKey, TValue>>();
                _AddValues(collection);
            }
            /// <summary>
            /// Creates an ordered dictionary with the specified comparer
            /// </summary>
            /// <param name="comparer">The equality comparer to use for the keys</param>
            public O_Dictionary(IEqualityComparer<TKey> comparer)
            {
                _innerDictionary = new Dictionary<TKey, TValue>(comparer);
                _innerList = new List<KeyValuePair<TKey, TValue>>();
                _comparer = comparer;
            }
            /// <summary>
            /// Creates a default instance of the ordered dictionary
            /// </summary>
            public O_Dictionary()
            {
                _innerDictionary = new Dictionary<TKey, TValue>();
                _innerList = new List<KeyValuePair<TKey, TValue>>();
            }
            /// <summary>
            /// Gets the value at the specified index
            /// </summary>
            /// <param name="index">The index of the value to retrieve</param>
            /// <returns>The value of the item at the specified index</returns>
            public TKey GetKey(int index)
            {
                return _innerList[index].Key;
            }
            public TValue GetVal(int index)
            {
                return _innerList[index].Value;
            }
            public int IndexOf(TKey key)
            {
                KeyValuePair<TKey,TValue> pair = new KeyValuePair<TKey, TValue>(key,_innerDictionary[key]); 
                return _innerList.IndexOf(pair);
            }

            /// <summary>
            /// Sets the value at the specified index
            /// </summary>
            /// <param name="index">The index of the value to set</param>
            /// <param name="value">The new value to assign</param>
            public void SetAt(int index,TValue value)
            {
                var key = _innerList[index].Key;
                _innerList[index] = new KeyValuePair<TKey, TValue>(key, value);
                _innerDictionary[key] = value;
            }
            /// <summary>
            /// Inserts an item into the ordered dictionary at the specified position
            /// </summary>
            /// <param name="index">The index to insert the item before</param>
            /// <param name="key">The key of the new item</param>
            /// <param name="value">The value of the new item</param>
            public void Insert(int index, TKey key, TValue value)
                => (this as IList<KeyValuePair<TKey, TValue>>).Insert(index, new KeyValuePair<TKey, TValue>(key, value));
            void _AddValues(IEnumerable<KeyValuePair<TKey,TValue>> collection)
            {
                foreach (var kvp in collection)
                {
                    _innerDictionary.Add(kvp.Key, kvp.Value);
                    _innerList.Add(kvp);
                }
            }
	    }
    partial class O_Dictionary<TKey,TValue> : IEnumerable<KeyValuePair<TKey,TValue>>
        {
            /// <summary>
            /// Gets an enumerator for this dictionary
            /// </summary>
            /// <returns>A new enumerator suitable for iterating through the items in the dictionary in stored order</returns>
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
                => _innerList.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    partial class O_Dictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>
		/// Returns the count of items in the dictionary
		/// </summary>
		public int Count => _innerList.Count;

		bool ICollection<KeyValuePair<TKey,TValue>>.IsReadOnly => false;

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			_innerDictionary.Add(item.Key, item.Value);
			_innerList.Add(item);
		}
		/// <summary>
		/// Clears all the items from the dictionary
		/// </summary>
		public void Clear()
		{
			_innerDictionary.Clear();
			_innerList.Clear();
		}

		bool ICollection<KeyValuePair<TKey,TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			if (null == _comparer)
				return _innerList.Contains(item);
			for(int ic=_innerList.Count,i=0;i<ic;++i)
			{
				var kvp = _innerList[i];
				if(_comparer.Equals(item.Key,kvp.Key))
					if (Equals(item.Value, kvp.Value))
						return true;
			}
			return false;
		}
		/// <summary>
		/// Copies the items in the dictionary to the specified array, starting at the specified destination index
		/// </summary>
		/// <param name="array">The array to copy to</param>
		/// <param name="arrayIndex">The index into <paramref name="array"/> at which copying begins</param>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			_innerList.CopyTo(array, arrayIndex);
		}
		bool ICollection<KeyValuePair<TKey,TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			if ((_innerDictionary as ICollection<KeyValuePair<TKey, TValue>>).Remove(item))
				return _innerList.Remove(item); // should always return true
			return false;
		}

	}
    partial class O_Dictionary<TKey,TValue> : IDictionary<TKey,TValue>
	{
		/// <summary>
		/// Adds an item to the end of the dictionary
		/// </summary>
		/// <param name="key">The key to add</param>
		/// <param name="value">The value to add</param>
		public void Add(TKey key, TValue value)
			=> (this as ICollection<KeyValuePair<TKey, TValue>>).Add(new KeyValuePair<TKey, TValue>(key,value));
		/// <summary>
		/// Indicates whether the specified key is contained in the dictionary
		/// </summary>
		/// <param name="key">The key to look for</param>
		/// <returns>True if the key is present in the dictionary, otherwise false</returns>
		public bool ContainsKey(TKey key)
			=> _innerDictionary.ContainsKey(key);
		/// <summary>
		/// Removes an item from the dictionary
		/// </summary>
		/// <param name="key">The key of the item to remove</param>
		/// <returns>True if the item was removed, or false if not found</returns>
		public bool Remove(TKey key)
		{
			TValue value;
			if(_innerDictionary.TryGetValue(key, out value))
			{
				_innerDictionary.Remove(key);
				_innerList.Remove(new KeyValuePair<TKey, TValue>(key,value));
				return true;
			}
			return false;
		}
		/// <summary>
		/// Attempts to retrieve the value for the specified key
		/// </summary>
		/// <param name="key">The key to look up</param>
		/// <param name="value">The value to return</param>
		/// <returns>True if the key is present, otherwise false</returns>
		public bool TryGetValue(TKey key, out TValue value)
			=> _innerDictionary.TryGetValue(key, out value);
		/// <summary>
		/// Gets or sets the value at the specified key
		/// </summary>
		/// <param name="key">The key to look up</param>
		/// <returns>The value</returns>
		public TValue this[TKey key] {
			get =>_innerDictionary[key];
			set {
				TValue v;
				if (_innerDictionary.TryGetValue(key, out v))
				{
					// change an existing key
					_innerDictionary[key] = value;
					_innerList[_innerList.IndexOf(new KeyValuePair<TKey, TValue>(key,v))] = new KeyValuePair<TKey, TValue>(key, value);
				} else
				{
					_innerDictionary.Add(key, value);
					_innerList.Add(new KeyValuePair<TKey,TValue>(key,value));
				}
			}
		}
		/// <summary>
		/// Indicates the keys in this dictionary
		/// </summary>
		public ICollection<TKey> Keys
			=> new KeysCollection(this);
		/// <summary>
		/// Indicates the value in this dictionary
		/// </summary>
		public ICollection<TValue> Values
			=> new ValuesCollection(this);
	}
    partial class O_Dictionary<TKey,TValue>
	{
		private sealed class KeysCollection : ICollection<TKey>
		{
			O_Dictionary<TKey, TValue> _outer;
			public KeysCollection(O_Dictionary<TKey, TValue> outer)
			{
				_outer = outer;
			}
			public int Count => _outer.Count;
			public bool Contains(TKey key)
				=> _outer.ContainsKey(key);
			public void CopyTo(TKey[] array, int index)
			{
				var count = _outer.Count;
				// check our parameters for validity
				if (null == array)
					throw new ArgumentNullException(nameof(array));
				if (1 != array.Rank || 0 != array.GetLowerBound(0))
					throw new ArgumentException("The array is not an SZArray", nameof(array));
				if (0 > index)
					throw new ArgumentOutOfRangeException(nameof(index),
						  "The index cannot be less than zero.");
				if (array.Length <= index)
					throw new ArgumentOutOfRangeException(nameof(index),
						  "The index cannot be greater than the length of the array." + "index :"+ index + ", length : "+array.Length);
				if (count > array.Length + index)
					throw new ArgumentException
					("The array is not big enough to hold the collection entries.", nameof(array));
				for (var i = 0; i < count; ++i)
					array[i + index] = _outer._innerList[i].Key;
			}
			public IEnumerator<TKey> GetEnumerator()
				=> new Enumerator(_outer);
			IEnumerator IEnumerable.GetEnumerator()
				=> GetEnumerator();
			void ICollection<TKey>.Add(TKey key)
				=> throw new NotSupportedException("The collection is read only.");
			bool ICollection<TKey>.Remove(TKey key)
				=> throw new NotSupportedException("The collection is read only.");
			void ICollection<TKey>.Clear()
				=> throw new NotSupportedException("The collection is read only.");
			bool ICollection<TKey>.IsReadOnly => true;
			// this is the meat of our enumeration capabilities
			struct Enumerator : IEnumerator<TKey>
			{
				IEnumerator<KeyValuePair<TKey, TValue>> _inner;
				public Enumerator(O_Dictionary<TKey,TValue> outer)
				{
					_inner = outer.GetEnumerator();
				}
				public void Reset()
					=> _inner.Reset();
				void IDisposable.Dispose()
					=> _inner.Dispose();
				public bool MoveNext()
					=> _inner.MoveNext();
				public TKey Current
					=> _inner.Current.Key;
				object IEnumerator.Current 
					=> Current;
			}
		}
	}
    partial class O_Dictionary<TKey, TValue> : IList<KeyValuePair<TKey, TValue>>
	{
		/// <summary>
		/// Removes the item at the specified index
		/// </summary>
		/// <param name="index">The index of the item to remove</param>
		public void RemoveAt(int index)
		{
			var key = _innerList[index].Key;
			_innerDictionary.Remove(key);
			_innerList.RemoveAt(index);
		}
		int IList<KeyValuePair<TKey, TValue>>.IndexOf(KeyValuePair<TKey, TValue> item)
			=> _innerList.IndexOf(item);
		void IList<KeyValuePair<TKey, TValue>>.Insert(int index,KeyValuePair<TKey, TValue> item)
		{
			_innerDictionary.Add(item.Key, item.Value);
			_innerList.Insert(index, item);
		}
		KeyValuePair<TKey, TValue> IList<KeyValuePair<TKey, TValue>>.this[int index] {
			get {
				return _innerList[index];
			}
			set {
				_innerList[index]=value;
			}
		}
	}
    partial class O_Dictionary<TKey, TValue>
	{
		private sealed class ValuesCollection : ICollection<TValue>
		{
			O_Dictionary<TKey, TValue> _outer;
			public ValuesCollection(O_Dictionary<TKey, TValue> outer)
			{
				_outer = outer;
			}
			public int Count => _outer.Count;
			public bool Contains(TValue value)
			{
				for(int ic = Count,i=0;i<ic;++i)
					if (Equals(_outer._innerList[i].Value, value))
						return true;
				return false;
			}
			public void CopyTo(TValue[] array, int index)
			{
				var count = _outer.Count;
				// check our parameters for validity
				if (null == array)
					throw new ArgumentNullException(nameof(array));
				if (1 != array.Rank || 0 != array.GetLowerBound(0))
					throw new ArgumentException("The array is not an SZArray", nameof(array));
				if (0 > index)
					throw new ArgumentOutOfRangeException(nameof(index),
						  "The index cannot be less than zero.");
				if (array.Length <= index)
					throw new ArgumentOutOfRangeException(nameof(index),
						  "The index cannot be greater than the length of the array.");
				if (count > array.Length + index)
					throw new ArgumentException
					("The array is not big enough to hold the collection entries.", nameof(array));
				for (var i = 0; i < count; ++i)
					array[i + index] = _outer._innerList[i].Value;
			}
			public IEnumerator<TValue> GetEnumerator()
				=> new Enumerator(_outer);
			IEnumerator IEnumerable.GetEnumerator()
				=> GetEnumerator();
			void ICollection<TValue>.Add(TValue value)
				=> throw new NotSupportedException("The collection is read only.");
			bool ICollection<TValue>.Remove(TValue value)
				=> throw new NotSupportedException("The collection is read only.");
			void ICollection<TValue>.Clear()
				=> throw new NotSupportedException("The collection is read only.");
			bool ICollection<TValue>.IsReadOnly => true;
			// this is the meat of our enumeration capabilities
			struct Enumerator : IEnumerator<TValue>
			{
				IEnumerator<KeyValuePair<TKey, TValue>> _inner;
				public Enumerator(O_Dictionary<TKey, TValue> outer)
				{
					_inner = outer.GetEnumerator();
				}
				public void Reset()
					=> _inner.Reset();
				void IDisposable.Dispose()
					=> _inner.Dispose();
				public bool MoveNext()
					=> _inner.MoveNext();
				public TValue Current
					=> _inner.Current.Value;
				object IEnumerator.Current
					=> Current;
			}
		}
	}
    
    #endregion
    [Serializable]
    public class SDictionary<Tkey,Tvalue> : ISerializationCallbackReceiver
    {
        [SerializeField]
        public Tkey[] keys;
        [SerializeField]
        public Tvalue[] values;
        public O_Dictionary<Tkey,Tvalue> dict = new O_Dictionary<Tkey, Tvalue>();
        public O_Dictionary<Tkey,Tvalue> ToDictionary() {return dict;}
        
        public SDictionary(O_Dictionary<Tkey,Tvalue> dictionary)
        {
            this.dict = dictionary;
        }

        public Tvalue this[Tkey key]
        {
            get{return dict[key];}
            set{dict[key] = value;}
        }

        public int Count() {return dict.Count;}
        public bool ContainsKey(Tkey key) {return dict.ContainsKey(key);}
        public void Add(Tkey key, Tvalue value) {dict.Add(key,value);}

        public bool Remove(Tkey key) {return dict.Remove(key);}
        public ICollection<Tkey> Keys() {return dict.Keys;}

        public void OnBeforeSerialize()
        {
            int count = 0;
            if(dict != null)
                count = dict.Count;
            keys = new Tkey[count];
            values = new Tvalue[count];
            if(count != 0)
            {
                dict.Keys.CopyTo(keys,0);
                dict.Values.CopyTo(values,0);
            }
        }
        
        public void OnAfterDeserialize()
        {
           int count;
            // if(keys == null || values ==null)
            //     count = 0;
            // else
            count = Math.Min(keys.Length, values.Length);
            dict = new O_Dictionary<Tkey, Tvalue>(count);
            for (var i = 0; i < count; ++i)
            {
                dict.Add(keys[i],values[i]);
            }
        }
    }

    public class CSVDict
    {
        public Dictionary<int,int[]> int_dict;
        public Dictionary<int,float[]> float_dict;
        public Dictionary<int,string[]> string_dict;

        public Dictionary<DBHeader,int> key_dict;

        public CSVDict()
        {
            int_dict = new Dictionary<int, int[]>();
            float_dict = new Dictionary<int, float[]>();
            string_dict = new Dictionary<int, string[]>();
            key_dict = new Dictionary<DBHeader, int>();
        }

        public Array this[DBHeader key]
        {
            get
            {
                int key_val = key_dict[key];
                switch(key_val%3)
                {
                    case 0 :
                        return int_dict[key_val];
                    case 1 : 
                        return float_dict[key_val];
                    case 2 :
                        return string_dict[key_val];
                    default :
                        return null;
                }
            }
        }

        public ref int I(DBHeader key, int i)
        {
            return ref int_dict[key_dict[key]][i];
        }
        public int[] I(DBHeader key)
        {
            return int_dict[key_dict[key]];
        }
        public ref float F(DBHeader key,int i)
        {
            return ref float_dict[key_dict[key]][i];
        }
        public float[] F(DBHeader key)
        {
            return float_dict[key_dict[key]];
        }
        public ref string S(DBHeader key,int i)
        {
            return ref string_dict[key_dict[key]][i];
        }
        public string[] S(DBHeader key)
        {
            return string_dict[key_dict[key]];
        }

    }
    [Serializable]
    public class BookInfoDict : SDictionary<int,BookInfo>
    {
        public BookInfoDict(O_Dictionary<int,BookInfo> dictionary) : base(dictionary) {}
    }
    [Serializable]
    public class MapInfoDict : SDictionary<int,MapInfo>
    {
        public MapInfoDict(O_Dictionary<int,MapInfo> dictionary) : base(dictionary) {}
    }
    [Serializable]
    public class PlanetInfoDict : SDictionary<int,MapInfoDict>
    {
        public PlanetInfoDict(O_Dictionary<int,MapInfoDict> dictionary) : base(dictionary) {}
    }

    [Serializable]
    public class ItemInfoDict: SDictionary<int,ItemInfo>
    {
        public ItemInfoDict(O_Dictionary<int,ItemInfo> dictionary) : base(dictionary){}  
    }
    [Serializable]
    public class QuestInfoDict: SDictionary<int,QuestInfo>
    {
        public QuestInfoDict(O_Dictionary<int,QuestInfo> dictionary) : base(dictionary){}
    }

    [Serializable]
    public struct ActivityCount
    {
        public int act_1;
        public int act_2;

        public ActivityCount(int act1, int act2)
        {
            this.act_1 = act1;
            this.act_2 = act2;
        }
    }
    [Serializable]
    public struct BookInfo
    {
        public int read_count;
        public ActivityCount act_count;

        public BookInfo(int index, ActivityCount act_count, int read_count = 1)
        {
            this.act_count = act_count;
            this.read_count = read_count;
        }

    }
    [Serializable]
    public struct ItemTypeInfo
    {
        public int type;
        public int index;
        public int count;

        public ItemTypeInfo(int type, int index, int count)
        {
            this.type = type;
            this.index = index;
            this.count = count;
        }
    }
    
    [Serializable]       
    public class ItemInfo
    {
        public int count;
        public int type;

        public ItemInfo(int count,int type)
        {
            this.count = count;
            this.type = type;
        }

        public int Add(int count)
        {
            this.count += count;
            return this.count;
        }
    }

    public struct BuildInfo
    {
        public int index; // build item index
        public int time; // built time;
        public int level;

        public BuildInfo(int index = 0, int time = 0)
        {
            this.index = index;
            this.time = time;
            this.level = 0;
        }
    }
    public struct Row<T>
    {
        [SerializeField]
        public T[] x;

        public Row(int x)
        {
            this.x = new T[x];
        }
    }
    [Serializable]
    public struct MapData<T>
    {  
        public int x_length;
        public int z_length;
        [SerializeField]
        public Row<T>[] z;

        public MapData(int x, int z)
        {
            this.z = new Row<T>[z];
            for(int i = 0; i<z; i++)
            {
                this.z[i] = new Row<T>(x);
            }
            x_length = x;
            z_length = z;
        }
    }
    [Serializable]
    public struct MapInfo
    {
        public int x_length;
        public int z_length;
        public MapData<BuildInfo> data;

        public MapInfo(MapData<BuildInfo> data)
        {
            this.data = data;
            this.x_length = data.x_length;
            this.z_length = data.z_length;

        }
    }

    [Serializable]
    public struct Achievement
    {
        [SerializeField]
        public bool[] arr;

        public Achievement(int length)
        {
            this.arr = new bool[length];
        }
    }
    [Serializable]
    public class QuestInfo
    {
        public string name;
        public int character;
        public ItemTypeInfo info;
        public int type;
        public int state;
        public bool hidden;

        public bool duty;

        public QuestInfo(string name,int type, int character, ItemTypeInfo info, bool duty, int state = 0, bool hidden = false)
        {
            this.name = name;
            this.type = type;
            this.character = character;
            this.info = info;
            this.duty = duty;
            this.state = state;
            this.hidden = hidden;
        }
    }
    [Serializable]
    public class UserData
    {
        public int index;
        public string user_name;
        public string password;
        public int book_count;
        public int time;
        public int money;
        public int intimacy;
        public int stage;
        public int stage_step;
        public int[] character_stage;
        //quest
        //npcinfo
        public float BGM_volume;
        public float effect_volume;
        public Achievement achievement;
        // [SerializeField]
        public BookInfoDict book;
        public PlanetInfoDict planet;
        public ItemInfoDict item_normal;
        public ItemInfoDict item_build;
        public QuestInfoDict quest;
    }

}