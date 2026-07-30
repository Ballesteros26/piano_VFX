using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted on the key. </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000723 RID: 1827
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		// Token: 0x060039A2 RID: 14754 RVA: 0x000D2974 File Offset: 0x000D0B74
		public SortedDictionary()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060039A3 RID: 14755 RVA: 0x000D297D File Offset: 0x000D0B7D
		public SortedDictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060039A4 RID: 14756 RVA: 0x000D2988 File Offset: 0x000D0B88
		public SortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this._set.Add(keyValuePair);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060039A5 RID: 14757 RVA: 0x000D29FC File Offset: 0x000D0BFC
		public SortedDictionary(IComparer<TKey> comparer)
		{
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000D2A15 File Offset: 0x000D0C15
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this._set.Add(keyValuePair);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000D2A24 File Offset: 0x000D0C24
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (keyValuePair.Value == null)
			{
				return node.Item.Value == null;
			}
			return EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000D2A88 File Offset: 0x000D0C88
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value))
			{
				this._set.Remove(keyValuePair);
				return true;
			}
			return false;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000DF1 RID: 3569
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					throw new KeyNotFoundException();
				}
				return node.Item.Value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
					return;
				}
				node.Item = new KeyValuePair<TKey, TValue>(node.Item.Key, value);
				this._set.UpdateVersion();
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000D2B9F File Offset: 0x000D0D9F
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /></returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000D2BAC File Offset: 0x000D0DAC
		public IComparer<TKey> Comparer
		{
			get
			{
				return ((SortedDictionary<TKey, TValue>.KeyValuePairComparer)this._set.Comparer).keyComparer;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000D2BC3 File Offset: 0x000D0DC3
		public SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new SortedDictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000D2BDF File Offset: 0x000D0DDF
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x000D2BDF File Offset: 0x000D0DDF
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000D2BE7 File Offset: 0x000D0DE7
		public SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new SortedDictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x000D2C03 File Offset: 0x000D0E03
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000D2C03 File Offset: 0x000D0E03
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000D2C0B File Offset: 0x000D0E0B
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000D2C33 File Offset: 0x000D0E33
		public void Clear()
		{
			this._set.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000D2C40 File Offset: 0x000D0E40
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._set.Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x060039B7 RID: 14775 RVA: 0x000D2C7C File Offset: 0x000D0E7C
		public bool ContainsValue(TValue value)
		{
			bool found = false;
			if (value == null)
			{
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (node.Item.Value == null)
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			else
			{
				EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (valueComparer.Equals(node.Item.Value, value))
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			return found;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> to the specified array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures that is the destination of the elements copied from the current <see cref="T:System.Collections.Generic.SortedDictionary`2" /> The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000D2CFA File Offset: 0x000D0EFA
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this._set.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" /> for the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000D2D09 File Offset: 0x000D0F09
		public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000D2D12 File Offset: 0x000D0F12
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060039BB RID: 14779 RVA: 0x000D2D20 File Offset: 0x000D0F20
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this._set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060039BC RID: 14780 RVA: 0x000D2D5C File Offset: 0x000D0F5C
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
			if (node == null)
			{
				value = default(TValue);
				return false;
			}
			value = node.Item.Value;
			return true;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060039BD RID: 14781 RVA: 0x000D2DB8 File Offset: 0x000D0FB8
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._set).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x00004240 File Offset: 0x00002440
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x00004240 File Offset: 0x00002440
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000D2BDF File Offset: 0x000D0DDF
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000D2C03 File Offset: 0x000D0E03
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x17000DFE RID: 3582
		object IDictionary.this[object key]
		{
			get
			{
				TValue tvalue;
				if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				try
				{
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
				catch (InvalidCastException)
				{
					throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
				}
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x060039C4 RID: 14788 RVA: 0x000D2EA8 File Offset: 0x000D10A8
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
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(global::SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060039C5 RID: 14789 RVA: 0x000D2F58 File Offset: 0x000D1158
		bool IDictionary.Contains(object key)
		{
			return SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000D2F70 File Offset: 0x000D1170
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060039C7 RID: 14791 RVA: 0x000D2F89 File Offset: 0x000D1189
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060039C8 RID: 14792 RVA: 0x000D2F97 File Offset: 0x000D1197
		void IDictionary.Remove(object key)
		{
			if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000D2FAE File Offset: 0x000D11AE
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._set).SyncRoot;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060039CB RID: 14795 RVA: 0x000D2D12 File Offset: 0x000D0F12
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x04002CC7 RID: 11463
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04002CC8 RID: 11464
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x04002CC9 RID: 11465
		private TreeSet<KeyValuePair<TKey, TValue>> _set;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x02000724 RID: 1828
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x060039CC RID: 14796 RVA: 0x000D2FBB File Offset: 0x000D11BB
			internal Enumerator(SortedDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._treeEnum = dictionary._set.GetEnumerator();
				this._getEnumeratorRetType = getEnumeratorRetType;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060039CD RID: 14797 RVA: 0x000D2FD5 File Offset: 0x000D11D5
			public bool MoveNext()
			{
				return this._treeEnum.MoveNext();
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" />.</summary>
			// Token: 0x060039CE RID: 14798 RVA: 0x000D2FE2 File Offset: 0x000D11E2
			public void Dispose()
			{
				this._treeEnum.Dispose();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17000E01 RID: 3585
			// (get) Token: 0x060039CF RID: 14799 RVA: 0x000D2FEF File Offset: 0x000D11EF
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._treeEnum.Current;
				}
			}

			// Token: 0x17000E02 RID: 3586
			// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000D2FFC File Offset: 0x000D11FC
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._treeEnum.NotStartedOrEnded;
				}
			}

			// Token: 0x060039D1 RID: 14801 RVA: 0x000D3009 File Offset: 0x000D1209
			internal void Reset()
			{
				this._treeEnum.Reset();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x060039D2 RID: 14802 RVA: 0x000D3009 File Offset: 0x000D1209
			void IEnumerator.Reset()
			{
				this._treeEnum.Reset();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E03 RID: 3587
			// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000D3018 File Offset: 0x000D1218
			object IEnumerator.Current
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair;
					if (this._getEnumeratorRetType == 2)
					{
						keyValuePair = this.Current;
						object obj = keyValuePair.Key;
						keyValuePair = this.Current;
						return new DictionaryEntry(obj, keyValuePair.Value);
					}
					keyValuePair = this.Current;
					TKey key = keyValuePair.Key;
					keyValuePair = this.Current;
					return new KeyValuePair<TKey, TValue>(key, keyValuePair.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E04 RID: 3588
			// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000D3098 File Offset: 0x000D1298
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E05 RID: 3589
			// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000D30CC File Offset: 0x000D12CC
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Value;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</summary>
			/// <returns>The element in the collection at the current position of the dictionary, as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000E06 RID: 3590
			// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000D3100 File Offset: 0x000D1300
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this.NotStartedOrEnded)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x04002CCA RID: 11466
			private SortedSet<KeyValuePair<TKey, TValue>>.Enumerator _treeEnum;

			// Token: 0x04002CCB RID: 11467
			private int _getEnumeratorRetType;

			// Token: 0x04002CCC RID: 11468
			internal const int KeyValuePair = 1;

			// Token: 0x04002CCD RID: 11469
			internal const int DictEntry = 2;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x02000725 RID: 1829
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x060039D7 RID: 14807 RVA: 0x000D314B File Offset: 0x000D134B
			public KeyCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x060039D8 RID: 14808 RVA: 0x000D3168 File Offset: 0x000D1368
			public SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x060039D9 RID: 14809 RVA: 0x000D3175 File Offset: 0x000D1375
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x060039DA RID: 14810 RVA: 0x000D3175 File Offset: 0x000D1375
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x060039DB RID: 14811 RVA: 0x000D3188 File Offset: 0x000D1388
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TKey[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Key;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x060039DC RID: 14812 RVA: 0x000D3220 File Offset: 0x000D1420
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
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this._dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				try
				{
					object[] objects = (object[])array;
					this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Key;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x17000E07 RID: 3591
			// (get) Token: 0x060039DD RID: 14813 RVA: 0x000D3338 File Offset: 0x000D1538
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17000E08 RID: 3592
			// (get) Token: 0x060039DE RID: 14814 RVA: 0x000027E2 File Offset: 0x000009E2
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060039DF RID: 14815 RVA: 0x000D3345 File Offset: 0x000D1545
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x060039E0 RID: 14816 RVA: 0x000D3345 File Offset: 0x000D1545
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x060039E1 RID: 14817 RVA: 0x000D3351 File Offset: 0x000D1551
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x060039E2 RID: 14818 RVA: 0x000D3345 File Offset: 0x000D1545
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x17000E09 RID: 3593
			// (get) Token: 0x060039E3 RID: 14819 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000E0A RID: 3594
			// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000D335F File Offset: 0x000D155F
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04002CCE RID: 11470
			private SortedDictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			// Token: 0x02000726 RID: 1830
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x060039E5 RID: 14821 RVA: 0x000D336C File Offset: 0x000D156C
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this._dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x060039E6 RID: 14822 RVA: 0x000D337A File Offset: 0x000D157A
				public void Dispose()
				{
					this._dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060039E7 RID: 14823 RVA: 0x000D3387 File Offset: 0x000D1587
				public bool MoveNext()
				{
					return this._dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000E0B RID: 3595
				// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000D3394 File Offset: 0x000D1594
				public TKey Current
				{
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._dictEnum.Current;
						return keyValuePair.Key;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000E0C RID: 3596
				// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000D33B4 File Offset: 0x000D15B4
				object IEnumerator.Current
				{
					get
					{
						if (this._dictEnum.NotStartedOrEnded)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x060039EA RID: 14826 RVA: 0x000D33D9 File Offset: 0x000D15D9
				void IEnumerator.Reset()
				{
					this._dictEnum.Reset();
				}

				// Token: 0x04002CCF RID: 11471
				private SortedDictionary<TKey, TValue>.Enumerator _dictEnum;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited</summary>
		// Token: 0x0200072A RID: 1834
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x060039F0 RID: 14832 RVA: 0x000D3463 File Offset: 0x000D1663
			public ValueCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x060039F1 RID: 14833 RVA: 0x000D3480 File Offset: 0x000D1680
			public SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x060039F2 RID: 14834 RVA: 0x000D348D File Offset: 0x000D168D
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x060039F3 RID: 14835 RVA: 0x000D348D File Offset: 0x000D168D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x060039F4 RID: 14836 RVA: 0x000D34A0 File Offset: 0x000D16A0
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TValue[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Value;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x060039F5 RID: 14837 RVA: 0x000D3538 File Offset: 0x000D1738
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
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				if (array.Length - index < this._dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				try
				{
					object[] objects = (object[])array;
					this._dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Value;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x17000E0D RID: 3597
			// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000D3650 File Offset: 0x000D1850
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17000E0E RID: 3598
			// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000027E2 File Offset: 0x000009E2
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060039F8 RID: 14840 RVA: 0x000D365D File Offset: 0x000D185D
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x060039F9 RID: 14841 RVA: 0x000D365D File Offset: 0x000D185D
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x060039FA RID: 14842 RVA: 0x000D3669 File Offset: 0x000D1869
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x060039FB RID: 14843 RVA: 0x000D365D File Offset: 0x000D185D
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x17000E0F RID: 3599
			// (get) Token: 0x060039FC RID: 14844 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000E10 RID: 3600
			// (get) Token: 0x060039FD RID: 14845 RVA: 0x000D3677 File Offset: 0x000D1877
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04002CD5 RID: 11477
			private SortedDictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			// Token: 0x0200072B RID: 1835
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x060039FE RID: 14846 RVA: 0x000D3684 File Offset: 0x000D1884
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this._dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x060039FF RID: 14847 RVA: 0x000D3692 File Offset: 0x000D1892
				public void Dispose()
				{
					this._dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003A00 RID: 14848 RVA: 0x000D369F File Offset: 0x000D189F
				public bool MoveNext()
				{
					return this._dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000E11 RID: 3601
				// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000D36AC File Offset: 0x000D18AC
				public TValue Current
				{
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this._dictEnum.Current;
						return keyValuePair.Value;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17000E12 RID: 3602
				// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000D36CC File Offset: 0x000D18CC
				object IEnumerator.Current
				{
					get
					{
						if (this._dictEnum.NotStartedOrEnded)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003A03 RID: 14851 RVA: 0x000D36F1 File Offset: 0x000D18F1
				void IEnumerator.Reset()
				{
					this._dictEnum.Reset();
				}

				// Token: 0x04002CD6 RID: 11478
				private SortedDictionary<TKey, TValue>.Enumerator _dictEnum;
			}
		}

		// Token: 0x0200072F RID: 1839
		[Serializable]
		internal sealed class KeyValuePairComparer : Comparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x06003A09 RID: 14857 RVA: 0x000D377B File Offset: 0x000D197B
			public KeyValuePairComparer(IComparer<TKey> keyComparer)
			{
				if (keyComparer == null)
				{
					this.keyComparer = Comparer<TKey>.Default;
					return;
				}
				this.keyComparer = keyComparer;
			}

			// Token: 0x06003A0A RID: 14858 RVA: 0x000D3799 File Offset: 0x000D1999
			public override int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x04002CDC RID: 11484
			internal IComparer<TKey> keyComparer;
		}
	}
}
