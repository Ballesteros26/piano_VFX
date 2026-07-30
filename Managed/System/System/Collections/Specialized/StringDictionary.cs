using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Collections.Specialized
{
	/// <summary>Implements a hash table with the key and the value strongly typed to be strings rather than objects.</summary>
	// Token: 0x0200070C RID: 1804
	[DesignerSerializer("System.Diagnostics.Design.StringDictionaryCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		/// <summary>Gets the number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>The number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000D062E File Offset: 0x000CE82E
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000D063B File Offset: 0x000CE83B
		public virtual bool IsSynchronized
		{
			get
			{
				return this.contents.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, Get returns null, and Set creates a new entry with the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x17000DC2 RID: 3522
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLower(CultureInfo.InvariantCulture)];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLower(CultureInfo.InvariantCulture)] = value;
			}
		}

		/// <summary>Gets a collection of keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000D069A File Offset: 0x000CE89A
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000D06A7 File Offset: 0x000CE8A7
		public virtual object SyncRoot
		{
			get
			{
				return this.contents.SyncRoot;
			}
		}

		/// <summary>Gets a collection of values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000D06B4 File Offset: 0x000CE8B4
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060038E0 RID: 14560 RVA: 0x000D06C1 File Offset: 0x000CE8C1
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060038E1 RID: 14561 RVA: 0x000D06E8 File Offset: 0x000CE8E8
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		// Token: 0x060038E2 RID: 14562 RVA: 0x000D06F5 File Offset: 0x000CE8F5
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLower(CultureInfo.InvariantCulture));
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. The value can be null. </param>
		// Token: 0x060038E3 RID: 14563 RVA: 0x000D071B File Offset: 0x000CE91B
		public virtual bool ContainsValue(string value)
		{
			return this.contents.ContainsValue(value);
		}

		/// <summary>Copies the string dictionary values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the <see cref="T:System.Collections.Specialized.StringDictionary" />. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Collections.Specialized.StringDictionary" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		// Token: 0x060038E4 RID: 14564 RVA: 0x000D0729 File Offset: 0x000CE929
		public virtual void CopyTo(Array array, int index)
		{
			this.contents.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the string dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that iterates through the string dictionary.</returns>
		// Token: 0x060038E5 RID: 14565 RVA: 0x000D0738 File Offset: 0x000CE938
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the string dictionary.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060038E6 RID: 14566 RVA: 0x000D0745 File Offset: 0x000CE945
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000D076B File Offset: 0x000CE96B
		internal void ReplaceHashtable(Hashtable useThisHashtableInstead)
		{
			this.contents = useThisHashtableInstead;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000D0774 File Offset: 0x000CE974
		internal IDictionary<string, string> AsGenericDictionary()
		{
			return new StringDictionary.GenericAdapter(this);
		}

		// Token: 0x04002C79 RID: 11385
		internal Hashtable contents = new Hashtable();

		// Token: 0x0200070D RID: 1805
		private class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x060038E9 RID: 14569 RVA: 0x000D077C File Offset: 0x000CE97C
			internal GenericAdapter(StringDictionary stringDictionary)
			{
				this.m_stringDictionary = stringDictionary;
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x000D078B File Offset: 0x000CE98B
			public void Add(string key, string value)
			{
				this[key] = value;
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x000D0795 File Offset: 0x000CE995
			public bool ContainsKey(string key)
			{
				return this.m_stringDictionary.ContainsKey(key);
			}

			// Token: 0x060038EC RID: 14572 RVA: 0x000D07A3 File Offset: 0x000CE9A3
			public void Clear()
			{
				this.m_stringDictionary.Clear();
			}

			// Token: 0x17000DC6 RID: 3526
			// (get) Token: 0x060038ED RID: 14573 RVA: 0x000D07B0 File Offset: 0x000CE9B0
			public int Count
			{
				get
				{
					return this.m_stringDictionary.Count;
				}
			}

			// Token: 0x17000DC7 RID: 3527
			public string this[string key]
			{
				get
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (!this.m_stringDictionary.ContainsKey(key))
					{
						throw new KeyNotFoundException();
					}
					return this.m_stringDictionary[key];
				}
				set
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					this.m_stringDictionary[key] = value;
				}
			}

			// Token: 0x17000DC8 RID: 3528
			// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000D080A File Offset: 0x000CEA0A
			public ICollection<string> Keys
			{
				get
				{
					if (this._keys == null)
					{
						this._keys = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Key);
					}
					return this._keys;
				}
			}

			// Token: 0x17000DC9 RID: 3529
			// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000D082C File Offset: 0x000CEA2C
			public ICollection<string> Values
			{
				get
				{
					if (this._values == null)
					{
						this._values = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Value);
					}
					return this._values;
				}
			}

			// Token: 0x060038F2 RID: 14578 RVA: 0x000D084E File Offset: 0x000CEA4E
			public bool Remove(string key)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					return false;
				}
				this.m_stringDictionary.Remove(key);
				return true;
			}

			// Token: 0x060038F3 RID: 14579 RVA: 0x000D086D File Offset: 0x000CEA6D
			public bool TryGetValue(string key, out string value)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					value = null;
					return false;
				}
				value = this.m_stringDictionary[key];
				return true;
			}

			// Token: 0x060038F4 RID: 14580 RVA: 0x000D0891 File Offset: 0x000CEA91
			void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
			{
				this.m_stringDictionary.Add(item.Key, item.Value);
			}

			// Token: 0x060038F5 RID: 14581 RVA: 0x000D08AC File Offset: 0x000CEAAC
			bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
			{
				string text;
				return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
			}

			// Token: 0x060038F6 RID: 14582 RVA: 0x000D08DC File Offset: 0x000CEADC
			void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array", global::SR.GetString("Array cannot be null."));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", global::SR.GetString("Non-negative number required."));
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException(global::SR.GetString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
				}
				int num = arrayIndex;
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}

			// Token: 0x17000DCA RID: 3530
			// (get) Token: 0x060038F7 RID: 14583 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection<KeyValuePair<string, string>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060038F8 RID: 14584 RVA: 0x000D09A8 File Offset: 0x000CEBA8
			bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
			{
				if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
				{
					return false;
				}
				this.m_stringDictionary.Remove(item.Key);
				return true;
			}

			// Token: 0x060038F9 RID: 14585 RVA: 0x000D09C8 File Offset: 0x000CEBC8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060038FA RID: 14586 RVA: 0x000D09D0 File Offset: 0x000CEBD0
			public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04002C7A RID: 11386
			private StringDictionary m_stringDictionary;

			// Token: 0x04002C7B RID: 11387
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _values;

			// Token: 0x04002C7C RID: 11388
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

			// Token: 0x0200070E RID: 1806
			internal enum KeyOrValue
			{
				// Token: 0x04002C7E RID: 11390
				Key,
				// Token: 0x04002C7F RID: 11391
				Value
			}

			// Token: 0x0200070F RID: 1807
			private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x060038FB RID: 14587 RVA: 0x000D09DF File Offset: 0x000CEBDF
				public ICollectionToGenericCollectionAdapter(StringDictionary source, StringDictionary.GenericAdapter.KeyOrValue keyOrValue)
				{
					if (source == null)
					{
						throw new ArgumentNullException("source");
					}
					this._internal = source;
					this._keyOrValue = keyOrValue;
				}

				// Token: 0x060038FC RID: 14588 RVA: 0x000D0A03 File Offset: 0x000CEC03
				public void Add(string item)
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x060038FD RID: 14589 RVA: 0x000D0A03 File Offset: 0x000CEC03
				public void Clear()
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x060038FE RID: 14590 RVA: 0x000D0A0B File Offset: 0x000CEC0B
				public void ThrowNotSupportedException()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						throw new NotSupportedException(global::SR.GetString("Mutating a key collection derived from a dictionary is not allowed."));
					}
					throw new NotSupportedException(global::SR.GetString("Mutating a value collection derived from a dictionary is not allowed."));
				}

				// Token: 0x060038FF RID: 14591 RVA: 0x000D0A34 File Offset: 0x000CEC34
				public bool Contains(string item)
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.ContainsKey(item);
					}
					return this._internal.ContainsValue(item);
				}

				// Token: 0x06003900 RID: 14592 RVA: 0x000D0A57 File Offset: 0x000CEC57
				public void CopyTo(string[] array, int arrayIndex)
				{
					this.GetUnderlyingCollection().CopyTo(array, arrayIndex);
				}

				// Token: 0x17000DCB RID: 3531
				// (get) Token: 0x06003901 RID: 14593 RVA: 0x000D0A66 File Offset: 0x000CEC66
				public int Count
				{
					get
					{
						return this._internal.Count;
					}
				}

				// Token: 0x17000DCC RID: 3532
				// (get) Token: 0x06003902 RID: 14594 RVA: 0x000027E2 File Offset: 0x000009E2
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06003903 RID: 14595 RVA: 0x000D0A73 File Offset: 0x000CEC73
				public bool Remove(string item)
				{
					this.ThrowNotSupportedException();
					return false;
				}

				// Token: 0x06003904 RID: 14596 RVA: 0x000D0A7C File Offset: 0x000CEC7C
				private ICollection GetUnderlyingCollection()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.Keys;
					}
					return this._internal.Values;
				}

				// Token: 0x06003905 RID: 14597 RVA: 0x000D0A9D File Offset: 0x000CEC9D
				public IEnumerator<string> GetEnumerator()
				{
					ICollection underlyingCollection = this.GetUnderlyingCollection();
					foreach (object obj in underlyingCollection)
					{
						string text = (string)obj;
						yield return text;
					}
					IEnumerator enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x06003906 RID: 14598 RVA: 0x000D0AAC File Offset: 0x000CECAC
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetUnderlyingCollection().GetEnumerator();
				}

				// Token: 0x04002C80 RID: 11392
				private StringDictionary _internal;

				// Token: 0x04002C81 RID: 11393
				private StringDictionary.GenericAdapter.KeyOrValue _keyOrValue;
			}
		}
	}
}
