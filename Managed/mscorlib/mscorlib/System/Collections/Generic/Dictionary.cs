using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of keys and values.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A2C RID: 2604
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
		// Token: 0x06006000 RID: 24576 RVA: 0x0013C0E3 File Offset: 0x0013A2E3
		public Dictionary()
			: this(0, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06006001 RID: 24577 RVA: 0x0013C0ED File Offset: 0x0013A2ED
		public Dictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		// Token: 0x06006002 RID: 24578 RVA: 0x0013C0F7 File Offset: 0x0013A2F7
		public Dictionary(IEqualityComparer<TKey> comparer)
			: this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06006003 RID: 24579 RVA: 0x0013C101 File Offset: 0x0013A301
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			this.comparer = comparer ?? EqualityComparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default equality comparer for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06006004 RID: 24580 RVA: 0x0013C13E File Offset: 0x0013A33E
		public Dictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06006005 RID: 24581 RVA: 0x0013C148 File Offset: 0x0013A348
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionary.GetType() == typeof(Dictionary<TKey, TValue>))
			{
				Dictionary<TKey, TValue> dictionary2 = (Dictionary<TKey, TValue>)dictionary;
				int num = dictionary2.count;
				Dictionary<TKey, TValue>.Entry[] array = dictionary2.entries;
				for (int i = 0; i < num; i++)
				{
					if (array[i].hashCode >= 0)
					{
						this.Add(array[i].key, array[i].value);
					}
				}
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x0013C224 File Offset: 0x0013A424
		public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, null)
		{
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x0013C230 File Offset: 0x0013A430
		public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
		{
			ICollection<KeyValuePair<TKey, TValue>> collection2 = collection as ICollection<KeyValuePair<TKey, TValue>>;
			this..ctor((collection2 != null) ? collection2.Count : 0, comparer);
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		// Token: 0x06006008 RID: 24584 RVA: 0x0013C2AC File Offset: 0x0013A4AC
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			DictionaryHashHelpers.SerializationInfoTable.Add(this, info);
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> that is used to determine equality of keys for the dictionary. </summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2" /> and to provide hash values for the keys.</returns>
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x0013C2C0 File Offset: 0x0013A4C0
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x0013C2C8 File Offset: 0x0013A4C8
		public int Count
		{
			get
			{
				return this.count - this.freeCount;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x0013C2D7 File Offset: 0x0013A4D7
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x0600600C RID: 24588 RVA: 0x0013C2D7 File Offset: 0x0013A4D7
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x0013C2D7 File Offset: 0x0013A4D7
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x0013C2F3 File Offset: 0x0013A4F3
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x0013C2F3 File Offset: 0x0013A4F3
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x0013C2F3 File Offset: 0x0013A4F3
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x1700112B RID: 4395
		public TValue this[TKey key]
		{
			get
			{
				int num = this.FindEntry(key);
				if (num >= 0)
				{
					return this.entries[num].value;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				this.TryInsert(key, value, InsertionBehavior.OverwriteExisting);
			}
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06006013 RID: 24595 RVA: 0x0013C34C File Offset: 0x0013A54C
		public void Add(TKey key, TValue value)
		{
			this.TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0013C358 File Offset: 0x0013A558
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0013C370 File Offset: 0x0013A570
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value);
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x0013C3B8 File Offset: 0x0013A5B8
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x06006017 RID: 24599 RVA: 0x0013C40C File Offset: 0x0013A60C
		public void Clear()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				Array.Clear(this.entries, 0, this.count);
				this.freeList = -1;
				this.count = 0;
				this.freeCount = 0;
				this.version++;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06006018 RID: 24600 RVA: 0x0013C473 File Offset: 0x0013A673
		public bool ContainsKey(TKey key)
		{
			return this.FindEntry(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x06006019 RID: 24601 RVA: 0x0013C484 File Offset: 0x0013A684
		public bool ContainsValue(TValue value)
		{
			if (value == null)
			{
				for (int i = 0; i < this.count; i++)
				{
					if (this.entries[i].hashCode >= 0 && this.entries[i].value == null)
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int j = 0; j < this.count; j++)
				{
					if (this.entries[j].hashCode >= 0 && @default.Equals(this.entries[j].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x0013C524 File Offset: 0x0013A724
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = this.count;
			Dictionary<TKey, TValue>.Entry[] array2 = this.entries;
			for (int i = 0; i < num; i++)
			{
				if (array2[i].hashCode >= 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(array2[i].key, array2[i].value);
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x0600601B RID: 24603 RVA: 0x0013C5C9 File Offset: 0x0013A7C9
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0013C5D2 File Offset: 0x0013A7D2
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600601D RID: 24605 RVA: 0x0013C5E0 File Offset: 0x0013A7E0
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.version);
			info.AddValue("Comparer", this.comparer, typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this.buckets == null) ? 0 : this.buckets.Length);
			if (this.buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x0013C678 File Offset: 0x0013A878
		private int FindEntry(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				for (int i = this.buckets[num % this.buckets.Length]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0013C714 File Offset: 0x0013A914
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this.buckets = new int[prime];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = -1;
			}
			this.entries = new Dictionary<TKey, TValue>.Entry[prime];
			this.freeList = -1;
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x0013C764 File Offset: 0x0013A964
		private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.comparer.GetHashCode(key) & int.MaxValue;
			int num2 = num % this.buckets.Length;
			int num3 = 0;
			int i = this.buckets[num2];
			while (i >= 0)
			{
				if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
				{
					if (behavior == InsertionBehavior.OverwriteExisting)
					{
						this.entries[i].value = value;
						this.version++;
						return true;
					}
					if (behavior == InsertionBehavior.ThrowOnExisting)
					{
						throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
					}
					return false;
				}
				else
				{
					num3++;
					i = this.entries[i].next;
				}
			}
			int num4;
			if (this.freeCount > 0)
			{
				num4 = this.freeList;
				this.freeList = this.entries[num4].next;
				this.freeCount--;
			}
			else
			{
				if (this.count == this.entries.Length)
				{
					this.Resize();
					num2 = num % this.buckets.Length;
				}
				num4 = this.count;
				this.count++;
			}
			this.entries[num4].hashCode = num;
			this.entries[num4].next = this.buckets[num2];
			this.entries[num4].key = key;
			this.entries[num4].value = value;
			this.buckets[num2] = num4;
			this.version++;
			if (num3 > 100 && this.comparer is NonRandomizedStringEqualityComparer)
			{
				this.comparer = (IEqualityComparer<TKey>)EqualityComparer<string>.Default;
				this.Resize(this.entries.Length, true);
			}
			return true;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2" /> instance is invalid.</exception>
		// Token: 0x06006021 RID: 24609 RVA: 0x0013C960 File Offset: 0x0013AB60
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			DictionaryHashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this.comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.buckets = new int[int2];
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				this.entries = new Dictionary<TKey, TValue>.Entry[int2];
				this.freeList = -1;
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					throw new SerializationException("The keys for this dictionary are missing.");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].Key == null)
					{
						throw new SerializationException("One of the serialized keys is null.");
					}
					this.Add(array[j].Key, array[j].Value);
				}
			}
			else
			{
				this.buckets = null;
			}
			this.version = @int;
			DictionaryHashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x0013CA93 File Offset: 0x0013AC93
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this.count), false);
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x0013CAA8 File Offset: 0x0013ACA8
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			int[] array = new int[newSize];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			Dictionary<TKey, TValue>.Entry[] array2 = new Dictionary<TKey, TValue>.Entry[newSize];
			Array.Copy(this.entries, 0, array2, 0, this.count);
			if (forceNewHashCodes)
			{
				for (int j = 0; j < this.count; j++)
				{
					if (array2[j].hashCode != -1)
					{
						array2[j].hashCode = this.comparer.GetHashCode(array2[j].key) & int.MaxValue;
					}
				}
			}
			for (int k = 0; k < this.count; k++)
			{
				if (array2[k].hashCode >= 0)
				{
					int num = array2[k].hashCode % newSize;
					array2[k].next = array[num];
					array[num] = k;
				}
			}
			this.buckets = array;
			this.entries = array2;
		}

		/// <summary>Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06006024 RID: 24612 RVA: 0x0013CB90 File Offset: 0x0013AD90
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				int num2 = num % this.buckets.Length;
				int num3 = -1;
				for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						if (num3 < 0)
						{
							this.buckets[num2] = this.entries[i].next;
						}
						else
						{
							this.entries[num3].next = this.entries[i].next;
						}
						this.entries[i].hashCode = -1;
						this.entries[i].next = this.freeList;
						this.entries[i].key = default(TKey);
						this.entries[i].value = default(TValue);
						this.freeList = i;
						this.freeCount++;
						this.version++;
						return true;
					}
					num3 = i;
				}
			}
			return false;
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x0013CCFC File Offset: 0x0013AEFC
		public bool Remove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				int num2 = num % this.buckets.Length;
				int num3 = -1;
				for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						if (num3 < 0)
						{
							this.buckets[num2] = this.entries[i].next;
						}
						else
						{
							this.entries[num3].next = this.entries[i].next;
						}
						value = this.entries[i].value;
						this.entries[i].hashCode = -1;
						this.entries[i].next = this.freeList;
						this.entries[i].key = default(TKey);
						this.entries[i].value = default(TValue);
						this.freeList = i;
						this.freeCount++;
						this.version++;
						return true;
					}
					num3 = i;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06006026 RID: 24614 RVA: 0x0013CE88 File Offset: 0x0013B088
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				value = this.entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0013CEC2 File Offset: 0x0013B0C2
		public bool TryAdd(TKey key, TValue value)
		{
			return this.TryInsert(key, value, InsertionBehavior.None);
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06006028 RID: 24616 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0013CECD File Offset: 0x0013B0CD
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600602A RID: 24618 RVA: 0x0013CED8 File Offset: 0x0013B0D8
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
				this.CopyTo(array2, index);
				return;
			}
			if (array is DictionaryEntry[])
			{
				DictionaryEntry[] array3 = array as DictionaryEntry[];
				Dictionary<TKey, TValue>.Entry[] array4 = this.entries;
				for (int i = 0; i < this.count; i++)
				{
					if (array4[i].hashCode >= 0)
					{
						array3[index++] = new DictionaryEntry(array4[i].key, array4[i].value);
					}
				}
				return;
			}
			object[] array5 = array as object[];
			if (array5 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				int num = this.count;
				Dictionary<TKey, TValue>.Entry[] array6 = this.entries;
				for (int j = 0; j < num; j++)
				{
					if (array6[j].hashCode >= 0)
					{
						array5[index++] = new KeyValuePair<TKey, TValue>(array6[j].key, array6[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600602B RID: 24619 RVA: 0x0013C5D2 File Offset: 0x0013A7D2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x0013D084 File Offset: 0x0013B284
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x0600602E RID: 24622 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x0013D0A6 File Offset: 0x0013B2A6
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x0013D0AE File Offset: 0x0013B2AE
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x17001133 RID: 4403
		object IDictionary.this[object key]
		{
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.FindEntry((TKey)((object)key));
					if (num >= 0)
					{
						return this.entries[num].value;
					}
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
						throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
					}
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
				}
			}
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x0013D1A8 File Offset: 0x0013B3A8
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-<paramref name="value" /> is of a type that is not assignable to <paramref name="TValue" />, the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06006035 RID: 24629 RVA: 0x0013D1C4 File Offset: 0x0013B3C4
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
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06006036 RID: 24630 RVA: 0x0013D274 File Offset: 0x0013B474
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06006037 RID: 24631 RVA: 0x0013D28C File Offset: 0x0013B48C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06006038 RID: 24632 RVA: 0x0013D29A File Offset: 0x0013B49A
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x0400305E RID: 12382
		private int[] buckets;

		// Token: 0x0400305F RID: 12383
		private Dictionary<TKey, TValue>.Entry[] entries;

		// Token: 0x04003060 RID: 12384
		private int count;

		// Token: 0x04003061 RID: 12385
		private int version;

		// Token: 0x04003062 RID: 12386
		private int freeList;

		// Token: 0x04003063 RID: 12387
		private int freeCount;

		// Token: 0x04003064 RID: 12388
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04003065 RID: 12389
		private Dictionary<TKey, TValue>.KeyCollection keys;

		// Token: 0x04003066 RID: 12390
		private Dictionary<TKey, TValue>.ValueCollection values;

		// Token: 0x04003067 RID: 12391
		private object _syncRoot;

		// Token: 0x04003068 RID: 12392
		private const string VersionName = "Version";

		// Token: 0x04003069 RID: 12393
		private const string HashSizeName = "HashSize";

		// Token: 0x0400306A RID: 12394
		private const string KeyValuePairsName = "KeyValuePairs";

		// Token: 0x0400306B RID: 12395
		private const string ComparerName = "Comparer";

		// Token: 0x02000A2D RID: 2605
		private struct Entry
		{
			// Token: 0x0400306C RID: 12396
			public int hashCode;

			// Token: 0x0400306D RID: 12397
			public int next;

			// Token: 0x0400306E RID: 12398
			public TKey key;

			// Token: 0x0400306F RID: 12399
			public TValue value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x02000A2E RID: 2606
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06006039 RID: 24633 RVA: 0x0013D2B1 File Offset: 0x0013B4B1
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this.dictionary = dictionary;
				this.version = dictionary.version;
				this.index = 0;
				this.getEnumeratorRetType = getEnumeratorRetType;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600603A RID: 24634 RVA: 0x0013D2E0 File Offset: 0x0013B4E0
			public bool MoveNext()
			{
				if (this.version != this.dictionary.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this.index < this.dictionary.count)
				{
					if (this.dictionary.entries[this.index].hashCode >= 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
						this.index++;
						return true;
					}
					this.index++;
				}
				this.index = this.dictionary.count + 1;
				this.current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17001134 RID: 4404
			// (get) Token: 0x0600603B RID: 24635 RVA: 0x0013D3C1 File Offset: 0x0013B5C1
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this.current;
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" />.</summary>
			// Token: 0x0600603C RID: 24636 RVA: 0x00002194 File Offset: 0x00000394
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17001135 RID: 4405
			// (get) Token: 0x0600603D RID: 24637 RVA: 0x0013D3CC File Offset: 0x0013B5CC
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this.getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this.current.Key, this.current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600603E RID: 24638 RVA: 0x0013D455 File Offset: 0x0013B655
			void IEnumerator.Reset()
			{
				if (this.version != this.dictionary.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.index = 0;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the dictionary at the current position of the enumerator, as a <see cref="T:System.Collections.DictionaryEntry" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17001136 RID: 4406
			// (get) Token: 0x0600603F RID: 24639 RVA: 0x0013D488 File Offset: 0x0013B688
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this.current.Key, this.current.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17001137 RID: 4407
			// (get) Token: 0x06006040 RID: 24640 RVA: 0x0013D4E2 File Offset: 0x0013B6E2
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17001138 RID: 4408
			// (get) Token: 0x06006041 RID: 24641 RVA: 0x0013D51C File Offset: 0x0013B71C
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.Value;
				}
			}

			// Token: 0x04003070 RID: 12400
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x04003071 RID: 12401
			private int version;

			// Token: 0x04003072 RID: 12402
			private int index;

			// Token: 0x04003073 RID: 12403
			private KeyValuePair<TKey, TValue> current;

			// Token: 0x04003074 RID: 12404
			private int getEnumeratorRetType;

			// Token: 0x04003075 RID: 12405
			internal const int DictEntry = 1;

			// Token: 0x04003076 RID: 12406
			internal const int KeyValuePair = 2;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited.</summary>
		// Token: 0x02000A2F RID: 2607
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06006042 RID: 24642 RVA: 0x0013D556 File Offset: 0x0013B756
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</returns>
			// Token: 0x06006043 RID: 24643 RVA: 0x0013D573 File Offset: 0x0013B773
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006044 RID: 24644 RVA: 0x0013D580 File Offset: 0x0013B780
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || index > array.Length)
				{
					throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (array.Length - index < this.dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.Retrieving the value of this property is an O(1) operation.</returns>
			// Token: 0x17001139 RID: 4409
			// (get) Token: 0x06006045 RID: 24645 RVA: 0x0013D623 File Offset: 0x0013B823
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x1700113A RID: 4410
			// (get) Token: 0x06006046 RID: 24646 RVA: 0x00003B29 File Offset: 0x00001D29
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006047 RID: 24647 RVA: 0x0013D630 File Offset: 0x0013B830
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x06006048 RID: 24648 RVA: 0x0013D630 File Offset: 0x0013B830
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x06006049 RID: 24649 RVA: 0x0013D63C File Offset: 0x0013B83C
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x0600604A RID: 24650 RVA: 0x0013D630 File Offset: 0x0013B830
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600604B RID: 24651 RVA: 0x0013D64A File Offset: 0x0013B84A
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x0600604C RID: 24652 RVA: 0x0013D64A File Offset: 0x0013B84A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x0600604D RID: 24653 RVA: 0x0013D65C File Offset: 0x0013B85C
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
				if (array.Length - index < this.dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x1700113B RID: 4411
			// (get) Token: 0x0600604E RID: 24654 RVA: 0x00015ED5 File Offset: 0x000140D5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x1700113C RID: 4412
			// (get) Token: 0x0600604F RID: 24655 RVA: 0x0013D790 File Offset: 0x0013B990
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x04003077 RID: 12407
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			// Token: 0x02000A30 RID: 2608
			[Serializable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06006050 RID: 24656 RVA: 0x0013D79D File Offset: 0x0013B99D
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentKey = default(TKey);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x06006051 RID: 24657 RVA: 0x00002194 File Offset: 0x00000394
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06006052 RID: 24658 RVA: 0x0013D7C8 File Offset: 0x0013B9C8
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentKey = this.dictionary.entries[this.index].key;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentKey = default(TKey);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x1700113D RID: 4413
				// (get) Token: 0x06006053 RID: 24659 RVA: 0x0013D883 File Offset: 0x0013BA83
				public TKey Current
				{
					get
					{
						return this.currentKey;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x1700113E RID: 4414
				// (get) Token: 0x06006054 RID: 24660 RVA: 0x0013D88B File Offset: 0x0013BA8B
				object IEnumerator.Current
				{
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.currentKey;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06006055 RID: 24661 RVA: 0x0013D8C0 File Offset: 0x0013BAC0
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this.index = 0;
					this.currentKey = default(TKey);
				}

				// Token: 0x04003078 RID: 12408
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x04003079 RID: 12409
				private int index;

				// Token: 0x0400307A RID: 12410
				private int version;

				// Token: 0x0400307B RID: 12411
				private TKey currentKey;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x02000A31 RID: 2609
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06006056 RID: 24662 RVA: 0x0013D8F3 File Offset: 0x0013BAF3
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					throw new ArgumentNullException("dictionary");
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x06006057 RID: 24663 RVA: 0x0013D910 File Offset: 0x0013BB10
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006058 RID: 24664 RVA: 0x0013D920 File Offset: 0x0013BB20
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || index > array.Length)
				{
					throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (array.Length - index < this.dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x1700113F RID: 4415
			// (get) Token: 0x06006059 RID: 24665 RVA: 0x0013D9C3 File Offset: 0x0013BBC3
			public int Count
			{
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17001140 RID: 4416
			// (get) Token: 0x0600605A RID: 24666 RVA: 0x00003B29 File Offset: 0x00001D29
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600605B RID: 24667 RVA: 0x0013D9D0 File Offset: 0x0013BBD0
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600605C RID: 24668 RVA: 0x0013D9D0 File Offset: 0x0013BBD0
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600605D RID: 24669 RVA: 0x0013D9D0 File Offset: 0x0013BBD0
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("Mutating a value collection derived from a dictionary is not allowed.");
			}

			// Token: 0x0600605E RID: 24670 RVA: 0x0013D9DC File Offset: 0x0013BBDC
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x0600605F RID: 24671 RVA: 0x0013D9EA File Offset: 0x0013BBEA
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06006060 RID: 24672 RVA: 0x0013D9EA File Offset: 0x0013BBEA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006061 RID: 24673 RVA: 0x0013D9FC File Offset: 0x0013BBFC
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
				if (array.Length - index < this.dictionary.Count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x17001141 RID: 4417
			// (get) Token: 0x06006062 RID: 24674 RVA: 0x00015ED5 File Offset: 0x000140D5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17001142 RID: 4418
			// (get) Token: 0x06006063 RID: 24675 RVA: 0x0013DB30 File Offset: 0x0013BD30
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400307C RID: 12412
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			// Token: 0x02000A32 RID: 2610
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06006064 RID: 24676 RVA: 0x0013DB3D File Offset: 0x0013BD3D
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentValue = default(TValue);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x06006065 RID: 24677 RVA: 0x00002194 File Offset: 0x00000394
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06006066 RID: 24678 RVA: 0x0013DB68 File Offset: 0x0013BD68
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentValue = this.dictionary.entries[this.index].value;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentValue = default(TValue);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17001143 RID: 4419
				// (get) Token: 0x06006067 RID: 24679 RVA: 0x0013DC23 File Offset: 0x0013BE23
				public TValue Current
				{
					get
					{
						return this.currentValue;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x17001144 RID: 4420
				// (get) Token: 0x06006068 RID: 24680 RVA: 0x0013DC2B File Offset: 0x0013BE2B
				object IEnumerator.Current
				{
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						return this.currentValue;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06006069 RID: 24681 RVA: 0x0013DC60 File Offset: 0x0013BE60
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this.index = 0;
					this.currentValue = default(TValue);
				}

				// Token: 0x0400307D RID: 12413
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x0400307E RID: 12414
				private int index;

				// Token: 0x0400307F RID: 12415
				private int version;

				// Token: 0x04003080 RID: 12416
				private TValue currentValue;
			}
		}
	}
}
