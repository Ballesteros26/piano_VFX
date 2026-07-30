using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation. </summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x02000733 RID: 1843
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x06003A13 RID: 14867 RVA: 0x000D385F File Offset: 0x000D1A5F
		public SortedList()
		{
			this.keys = Array.Empty<TKey>();
			this.values = Array.Empty<TValue>();
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003A14 RID: 14868 RVA: 0x000D3890 File Offset: 0x000D1A90
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x06003A15 RID: 14869 RVA: 0x000D38E0 File Offset: 0x000D1AE0
		public SortedList(IComparer<TKey> comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06003A16 RID: 14870 RVA: 0x000D38F2 File Offset: 0x000D1AF2
		public SortedList(int capacity, IComparer<TKey> comparer)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000D3902 File Offset: 0x000D1B02
		public SortedList(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003A18 RID: 14872 RVA: 0x000D390C File Offset: 0x000D1B0C
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			int count = dictionary.Count;
			if (count != 0)
			{
				TKey[] array = this.keys;
				dictionary.Keys.CopyTo(array, 0);
				dictionary.Values.CopyTo(this.values, 0);
				if (count > 1)
				{
					comparer = this.Comparer;
					Array.Sort<TKey, TValue>(array, this.values, comparer);
					for (int num = 1; num != array.Length; num++)
					{
						if (comparer.Compare(array[num - 1], array[num]) == 0)
						{
							throw new ArgumentException(global::SR.Format("An item with the same key has already been added. Key: {0}", array[num]));
						}
					}
				}
			}
			this._size = count;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x06003A19 RID: 14873 RVA: 0x000D39D0 File Offset: 0x000D1BD0
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(global::SR.Format("An item with the same key has already been added. Key: {0}", key), "key");
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000D3A33 File Offset: 0x000D1C33
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000D3A4C File Offset: 0x000D1C4C
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000D3A90 File Offset: 0x000D1C90
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000D3AD8 File Offset: 0x000D1CD8
		// (set) Token: 0x06003A1E RID: 14878 RVA: 0x000D3AE4 File Offset: 0x000D1CE4
		public int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						throw new ArgumentOutOfRangeException("value", value, "capacity was less than the current size.");
					}
					if (value > 0)
					{
						TKey[] array = new TKey[value];
						TValue[] array2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = Array.Empty<TKey>();
					this.values = Array.Empty<TValue>();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list. </summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000D3B86 File Offset: 0x000D1D86
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x06003A20 RID: 14880 RVA: 0x000D3B90 File Offset: 0x000D1D90
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null && default(TValue) != null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
			if (!(value is TValue) && value != null)
			{
				throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
			}
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000D3C2E File Offset: 0x000D1E2E
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000D3C36 File Offset: 0x000D1E36
		public IList<TKey> Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000D3C36 File Offset: 0x000D1E36
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000D3C36 File Offset: 0x000D1E36
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000D3C36 File Offset: 0x000D1E36
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000D3C3E File Offset: 0x000D1E3E
		public IList<TValue> Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000D3C3E File Offset: 0x000D1E3E
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000D3C3E File Offset: 0x000D1E3E
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000D3C3E File Offset: 0x000D1E3E
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000D3C46 File Offset: 0x000D1E46
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000D3C62 File Offset: 0x000D1E62
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x00004240 File Offset: 0x00002440
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x00004240 File Offset: 0x00002440
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000D3C7E File Offset: 0x000D1E7E
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x06003A31 RID: 14897 RVA: 0x000D3CA0 File Offset: 0x000D1EA0
		public void Clear()
		{
			this.version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				Array.Clear(this.keys, 0, this._size);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				Array.Clear(this.values, 0, this._size);
			}
			this._size = 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A32 RID: 14898 RVA: 0x000D3CF4 File Offset: 0x000D1EF4
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A33 RID: 14899 RVA: 0x000D3D0C File Offset: 0x000D1F0C
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be null for reference types.</param>
		// Token: 0x06003A34 RID: 14900 RVA: 0x000D3D1B File Offset: 0x000D1F1B
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000D3D2C File Offset: 0x000D1F2C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A36 RID: 14902 RVA: 0x000D3DBC File Offset: 0x000D1FBC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + index] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + index] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000D3F00 File Offset: 0x000D2100
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 4 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000D3F3F File Offset: 0x000D213F
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x06003A39 RID: 14905 RVA: 0x000D3F70 File Offset: 0x000D2170
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000D3F70 File Offset: 0x000D2170
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06003A3B RID: 14907 RVA: 0x000D3F7E File Offset: 0x000D217E
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003A3C RID: 14908 RVA: 0x000D3F70 File Offset: 0x000D2170
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000D3F8C File Offset: 0x000D218C
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000E23 RID: 3619
		public TValue this[TKey key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x17000E24 RID: 3620
		object IDictionary.this[object key]
		{
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				TKey tkey = (TKey)((object)key);
				try
				{
					this[tkey] = (TValue)((object)value);
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A42 RID: 14914 RVA: 0x000D4114 File Offset: 0x000D2314
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be null for reference types.</param>
		// Token: 0x06003A43 RID: 14915 RVA: 0x000D4155 File Offset: 0x000D2355
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000D416C File Offset: 0x000D236C
		private void Insert(int index, TKey key, TValue value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A45 RID: 14917 RVA: 0x000D4210 File Offset: 0x000D2410
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x06003A46 RID: 14918 RVA: 0x000D4248 File Offset: 0x000D2448
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				this.keys[this._size] = default(TKey);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				this.values[this._size] = default(TValue);
			}
			this.version++;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A47 RID: 14919 RVA: 0x000D431C File Offset: 0x000D251C
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A48 RID: 14920 RVA: 0x000D4343 File Offset: 0x000D2543
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.SortedList`2" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06003A49 RID: 14921 RVA: 0x000D435C File Offset: 0x000D255C
		public void TrimExcess()
		{
			int num = (int)((double)this.keys.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000D2F70 File Offset: 0x000D1170
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		// Token: 0x04002CE1 RID: 11489
		private TKey[] keys;

		// Token: 0x04002CE2 RID: 11490
		private TValue[] values;

		// Token: 0x04002CE3 RID: 11491
		private int _size;

		// Token: 0x04002CE4 RID: 11492
		private int version;

		// Token: 0x04002CE5 RID: 11493
		private IComparer<TKey> comparer;

		// Token: 0x04002CE6 RID: 11494
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x04002CE7 RID: 11495
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x04002CE8 RID: 11496
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002CE9 RID: 11497
		private const int DefaultCapacity = 4;

		// Token: 0x04002CEA RID: 11498
		private const int MaxArrayLength = 2146435071;

		// Token: 0x02000734 RID: 1844
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06003A4B RID: 14923 RVA: 0x000D4393 File Offset: 0x000D2593
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this._index = 0;
				this._version = this._sortedList.version;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x06003A4C RID: 14924 RVA: 0x000D43D3 File Offset: 0x000D25D3
			public void Dispose()
			{
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x17000E25 RID: 3621
			// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000D43F4 File Offset: 0x000D25F4
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x06003A4E RID: 14926 RVA: 0x000D442C File Offset: 0x000D262C
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._key = default(TKey);
				this._value = default(TValue);
				return false;
			}

			// Token: 0x17000E26 RID: 3622
			// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000D44E0 File Offset: 0x000D26E0
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x17000E27 RID: 3623
			// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000D4530 File Offset: 0x000D2730
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000E28 RID: 3624
			// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000D4544 File Offset: 0x000D2744
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this._key, this._value);
					}
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000E29 RID: 3625
			// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000D45B9 File Offset: 0x000D27B9
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x06003A53 RID: 14931 RVA: 0x000D45EE File Offset: 0x000D27EE
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x04002CEB RID: 11499
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04002CEC RID: 11500
			private TKey _key;

			// Token: 0x04002CED RID: 11501
			private TValue _value;

			// Token: 0x04002CEE RID: 11502
			private int _index;

			// Token: 0x04002CEF RID: 11503
			private int _version;

			// Token: 0x04002CF0 RID: 11504
			private int _getEnumeratorRetType;

			// Token: 0x04002CF1 RID: 11505
			internal const int KeyValuePair = 1;

			// Token: 0x04002CF2 RID: 11506
			internal const int DictEntry = 2;
		}

		// Token: 0x02000735 RID: 1845
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06003A54 RID: 14932 RVA: 0x000D462D File Offset: 0x000D282D
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06003A55 RID: 14933 RVA: 0x000D4648 File Offset: 0x000D2848
			public void Dispose()
			{
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x06003A56 RID: 14934 RVA: 0x000D4660 File Offset: 0x000D2860
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentKey = this._sortedList.keys[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentKey = default(TKey);
				return false;
			}

			// Token: 0x17000E2A RID: 3626
			// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000D46EA File Offset: 0x000D28EA
			public TKey Current
			{
				get
				{
					return this._currentKey;
				}
			}

			// Token: 0x17000E2B RID: 3627
			// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000D46F2 File Offset: 0x000D28F2
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x06003A59 RID: 14937 RVA: 0x000D4727 File Offset: 0x000D2927
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x04002CF3 RID: 11507
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04002CF4 RID: 11508
			private int _index;

			// Token: 0x04002CF5 RID: 11509
			private int _version;

			// Token: 0x04002CF6 RID: 11510
			private TKey _currentKey;
		}

		// Token: 0x02000736 RID: 1846
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x06003A5A RID: 14938 RVA: 0x000D475A File Offset: 0x000D295A
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06003A5B RID: 14939 RVA: 0x000D4775 File Offset: 0x000D2975
			public void Dispose()
			{
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x06003A5C RID: 14940 RVA: 0x000D478C File Offset: 0x000D298C
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentValue = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentValue = default(TValue);
				return false;
			}

			// Token: 0x17000E2C RID: 3628
			// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000D4816 File Offset: 0x000D2A16
			public TValue Current
			{
				get
				{
					return this._currentValue;
				}
			}

			// Token: 0x17000E2D RID: 3629
			// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000D481E File Offset: 0x000D2A1E
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x06003A5F RID: 14943 RVA: 0x000D4853 File Offset: 0x000D2A53
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x04002CF7 RID: 11511
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04002CF8 RID: 11512
			private int _index;

			// Token: 0x04002CF9 RID: 11513
			private int _version;

			// Token: 0x04002CFA RID: 11514
			private TValue _currentValue;
		}

		// Token: 0x02000737 RID: 1847
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
		{
			// Token: 0x06003A60 RID: 14944 RVA: 0x000D4886 File Offset: 0x000D2A86
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000E2E RID: 3630
			// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000D4895 File Offset: 0x000D2A95
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000E2F RID: 3631
			// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000027E2 File Offset: 0x000009E2
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E30 RID: 3632
			// (get) Token: 0x06003A63 RID: 14947 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000E31 RID: 3633
			// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000D48A2 File Offset: 0x000D2AA2
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06003A65 RID: 14949 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Add(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A66 RID: 14950 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A67 RID: 14951 RVA: 0x000D48BB File Offset: 0x000D2ABB
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x06003A68 RID: 14952 RVA: 0x000D48C9 File Offset: 0x000D2AC9
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06003A69 RID: 14953 RVA: 0x000D48EC File Offset: 0x000D2AEC
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x06003A6A RID: 14954 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Insert(int index, TKey value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000E32 RID: 3634
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x06003A6D RID: 14957 RVA: 0x000D4966 File Offset: 0x000D2B66
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06003A6E RID: 14958 RVA: 0x000D4966 File Offset: 0x000D2B66
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06003A6F RID: 14959 RVA: 0x000D4974 File Offset: 0x000D2B74
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06003A70 RID: 14960 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public bool Remove(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A71 RID: 14961 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04002CFB RID: 11515
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x02000738 RID: 1848
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			// Token: 0x06003A72 RID: 14962 RVA: 0x000D49C4 File Offset: 0x000D2BC4
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000E33 RID: 3635
			// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000D49D3 File Offset: 0x000D2BD3
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000E34 RID: 3636
			// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000027E2 File Offset: 0x000009E2
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E35 RID: 3637
			// (get) Token: 0x06003A75 RID: 14965 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000E36 RID: 3638
			// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000D49E0 File Offset: 0x000D2BE0
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06003A77 RID: 14967 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Add(TValue key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A78 RID: 14968 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A79 RID: 14969 RVA: 0x000D49ED File Offset: 0x000D2BED
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x06003A7A RID: 14970 RVA: 0x000D49FB File Offset: 0x000D2BFB
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06003A7B RID: 14971 RVA: 0x000D4A1C File Offset: 0x000D2C1C
			void ICollection.CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, index, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x06003A7C RID: 14972 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void Insert(int index, TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000E37 RID: 3639
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x06003A7F RID: 14975 RVA: 0x000D4A96 File Offset: 0x000D2C96
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06003A80 RID: 14976 RVA: 0x000D4A96 File Offset: 0x000D2C96
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06003A81 RID: 14977 RVA: 0x000D4AA3 File Offset: 0x000D2CA3
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x06003A82 RID: 14978 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public bool Remove(TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06003A83 RID: 14979 RVA: 0x000D48AF File Offset: 0x000D2AAF
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04002CFC RID: 11516
			private SortedList<TKey, TValue> _dict;
		}
	}
}
