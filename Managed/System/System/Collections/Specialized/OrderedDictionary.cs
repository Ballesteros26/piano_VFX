using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of key/value pairs that are accessible by the key or index.</summary>
	// Token: 0x02000707 RID: 1799
	[Serializable]
	public class OrderedDictionary : IOrderedDictionary, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class.</summary>
		// Token: 0x06003889 RID: 14473 RVA: 0x000CFC5E File Offset: 0x000CDE5E
		public OrderedDictionary()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		// Token: 0x0600388A RID: 14474 RVA: 0x000CFC67 File Offset: 0x000CDE67
		public OrderedDictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x0600388B RID: 14475 RVA: 0x000CFC71 File Offset: 0x000CDE71
		public OrderedDictionary(IEqualityComparer comparer)
			: this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity and comparer.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x0600388C RID: 14476 RVA: 0x000CFC7B File Offset: 0x000CDE7B
		public OrderedDictionary(int capacity, IEqualityComparer comparer)
		{
			this._initialCapacity = capacity;
			this._comparer = comparer;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000CFC94 File Offset: 0x000CDE94
		private OrderedDictionary(OrderedDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._readOnly = true;
			this._objectsArray = dictionary._objectsArray;
			this._objectsTable = dictionary._objectsTable;
			this._comparer = dictionary._comparer;
			this._initialCapacity = dictionary._initialCapacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		// Token: 0x0600388E RID: 14478 RVA: 0x000CFCEC File Offset: 0x000CDEEC
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of key/values pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000CFCFB File Offset: 0x000CDEFB
		public int Count
		{
			get
			{
				return this.objectsArray.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000CFD08 File Offset: 0x000CDF08
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000CFD08 File Offset: 0x000CDF08
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object is synchronized (thread-safe).</summary>
		/// <returns>This method always returns false.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x000CFD10 File Offset: 0x000CDF10
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, true);
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000CFD1E File Offset: 0x000CDF1E
		private ArrayList objectsArray
		{
			get
			{
				if (this._objectsArray == null)
				{
					this._objectsArray = new ArrayList(this._initialCapacity);
				}
				return this._objectsArray;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x000CFD3F File Offset: 0x000CDF3F
		private Hashtable objectsTable
		{
			get
			{
				if (this._objectsTable == null)
				{
					this._objectsTable = new Hashtable(this._initialCapacity, this._comparer);
				}
				return this._objectsTable;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x000CFD66 File Offset: 0x000CDF66
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

		/// <summary>Gets or sets the value at the specified index.</summary>
		/// <returns>The value of the item at the specified index. </returns>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x17000DAD RID: 3501
		public object this[int index]
		{
			get
			{
				return ((DictionaryEntry)this.objectsArray[index]).Value;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
				}
				if (index < 0 || index >= this.objectsArray.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object key = ((DictionaryEntry)this.objectsArray[index]).Key;
				this.objectsArray[index] = new DictionaryEntry(key, value);
				this.objectsTable[key] = value;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x17000DAE RID: 3502
		public object this[object key]
		{
			get
			{
				return this.objectsTable[key];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
				}
				if (this.objectsTable.Contains(key))
				{
					this.objectsTable[key] = value;
					this.objectsArray[this.IndexOfKey(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000CFEA7 File Offset: 0x000CE0A7
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the lowest available index.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. This value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x0600389C RID: 14492 RVA: 0x000CFEB5 File Offset: 0x000CE0B5
		public void Add(object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Add(new DictionaryEntry(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x0600389D RID: 14493 RVA: 0x000CFEF4 File Offset: 0x000CE0F4
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
			}
			this.objectsTable.Clear();
			this.objectsArray.Clear();
		}

		/// <summary>Returns a read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>A read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x0600389E RID: 14494 RVA: 0x000CFF24 File Offset: 0x000CE124
		public OrderedDictionary AsReadOnly()
		{
			return new OrderedDictionary(this);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		// Token: 0x0600389F RID: 14495 RVA: 0x000CFF2C File Offset: 0x000CE12C
		public bool Contains(object key)
		{
			return this.objectsTable.Contains(key);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> elements to a one-dimensional <see cref="T:System.Array" /> object at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060038A0 RID: 14496 RVA: 0x000CFF3A File Offset: 0x000CE13A
		public void CopyTo(Array array, int index)
		{
			this.objectsTable.CopyTo(array, index);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000CFF4C File Offset: 0x000CE14C
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.objectsArray.Count; i++)
			{
				object key2 = ((DictionaryEntry)this.objectsArray[i]).Key;
				if (this._comparer != null)
				{
					if (this._comparer.Equals(key2, key))
					{
						return i;
					}
				}
				else if (key2.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a new entry into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the specified key and value at the specified index.</summary>
		/// <param name="index">The zero-based index at which the element should be inserted.</param>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is out of range.</exception>
		/// <exception cref="T:System.NotSupportedException">This collection is read-only.</exception>
		// Token: 0x060038A2 RID: 14498 RVA: 0x000CFFB0 File Offset: 0x000CE1B0
		public void Insert(int index, object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
			}
			if (index > this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Insert(index, new DictionaryEntry(key, value));
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is invalid.</exception>
		// Token: 0x060038A3 RID: 14499 RVA: 0x000D0014 File Offset: 0x000CE214
		protected virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				throw new SerializationException(global::SR.GetString("OnDeserialization method was called while the object was not being deserialized."));
			}
			this._comparer = (IEqualityComparer)this._siInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this._readOnly = this._siInfo.GetBoolean("ReadOnly");
			this._initialCapacity = this._siInfo.GetInt32("InitialCapacity");
			object[] array = (object[])this._siInfo.GetValue("ArrayList", typeof(object[]));
			if (array != null)
			{
				foreach (object obj in array)
				{
					DictionaryEntry dictionaryEntry;
					try
					{
						dictionaryEntry = (DictionaryEntry)obj;
					}
					catch
					{
						throw new SerializationException(global::SR.GetString("There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries."));
					}
					this.objectsArray.Add(dictionaryEntry);
					this.objectsTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Removes the entry at the specified index from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x060038A4 RID: 14500 RVA: 0x000D0118 File Offset: 0x000CE318
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
			}
			if (index >= this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object key = ((DictionaryEntry)this.objectsArray[index]).Key;
			this.objectsArray.RemoveAt(index);
			this.objectsTable.Remove(key);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060038A5 RID: 14501 RVA: 0x000D0188 File Offset: 0x000CE388
		public void Remove(object key)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(global::SR.GetString("The OrderedDictionary is readonly and cannot be modified."));
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num < 0)
			{
				return;
			}
			this.objectsTable.Remove(key);
			this.objectsArray.RemoveAt(num);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060038A6 RID: 14502 RVA: 0x000D01E0 File Offset: 0x000CE3E0
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060038A7 RID: 14503 RVA: 0x000D01E0 File Offset: 0x000CE3E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060038A8 RID: 14504 RVA: 0x000D01F0 File Offset: 0x000CE3F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this._comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this._readOnly);
			info.AddValue("InitialCapacity", this._initialCapacity);
			object[] array = new object[this.Count];
			this._objectsArray.CopyTo(array);
			info.AddValue("ArrayList", array);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060038A9 RID: 14505 RVA: 0x000D026C File Offset: 0x000CE46C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		// Token: 0x04002C64 RID: 11364
		private ArrayList _objectsArray;

		// Token: 0x04002C65 RID: 11365
		private Hashtable _objectsTable;

		// Token: 0x04002C66 RID: 11366
		private int _initialCapacity;

		// Token: 0x04002C67 RID: 11367
		private IEqualityComparer _comparer;

		// Token: 0x04002C68 RID: 11368
		private bool _readOnly;

		// Token: 0x04002C69 RID: 11369
		private object _syncRoot;

		// Token: 0x04002C6A RID: 11370
		private SerializationInfo _siInfo;

		// Token: 0x04002C6B RID: 11371
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04002C6C RID: 11372
		private const string ArrayListName = "ArrayList";

		// Token: 0x04002C6D RID: 11373
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04002C6E RID: 11374
		private const string InitCapacityName = "InitialCapacity";

		// Token: 0x02000708 RID: 1800
		private class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060038AA RID: 14506 RVA: 0x000D0275 File Offset: 0x000CE475
			internal OrderedDictionaryEnumerator(ArrayList array, int objectReturnType)
			{
				this.arrayEnumerator = array.GetEnumerator();
				this._objectReturnType = objectReturnType;
			}

			// Token: 0x17000DB0 RID: 3504
			// (get) Token: 0x060038AB RID: 14507 RVA: 0x000D0290 File Offset: 0x000CE490
			public object Current
			{
				get
				{
					if (this._objectReturnType == 1)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Key;
					}
					if (this._objectReturnType == 2)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Value;
					}
					return this.Entry;
				}
			}

			// Token: 0x17000DB1 RID: 3505
			// (get) Token: 0x060038AC RID: 14508 RVA: 0x000D02EC File Offset: 0x000CE4EC
			public DictionaryEntry Entry
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					object key = dictionaryEntry.Key;
					dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return new DictionaryEntry(key, dictionaryEntry.Value);
				}
			}

			// Token: 0x17000DB2 RID: 3506
			// (get) Token: 0x060038AD RID: 14509 RVA: 0x000D0330 File Offset: 0x000CE530
			public object Key
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Key;
				}
			}

			// Token: 0x17000DB3 RID: 3507
			// (get) Token: 0x060038AE RID: 14510 RVA: 0x000D0358 File Offset: 0x000CE558
			public object Value
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Value;
				}
			}

			// Token: 0x060038AF RID: 14511 RVA: 0x000D037D File Offset: 0x000CE57D
			public bool MoveNext()
			{
				return this.arrayEnumerator.MoveNext();
			}

			// Token: 0x060038B0 RID: 14512 RVA: 0x000D038A File Offset: 0x000CE58A
			public void Reset()
			{
				this.arrayEnumerator.Reset();
			}

			// Token: 0x04002C6F RID: 11375
			private int _objectReturnType;

			// Token: 0x04002C70 RID: 11376
			internal const int Keys = 1;

			// Token: 0x04002C71 RID: 11377
			internal const int Values = 2;

			// Token: 0x04002C72 RID: 11378
			internal const int DictionaryEntry = 3;

			// Token: 0x04002C73 RID: 11379
			private IEnumerator arrayEnumerator;
		}

		// Token: 0x02000709 RID: 1801
		private class OrderedDictionaryKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060038B1 RID: 14513 RVA: 0x000D0397 File Offset: 0x000CE597
			public OrderedDictionaryKeyValueCollection(ArrayList array, bool isKeys)
			{
				this._objects = array;
				this.isKeys = isKeys;
			}

			// Token: 0x060038B2 RID: 14514 RVA: 0x000D03B0 File Offset: 0x000CE5B0
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				foreach (object obj in this._objects)
				{
					array.SetValue(this.isKeys ? ((DictionaryEntry)obj).Key : ((DictionaryEntry)obj).Value, index);
					index++;
				}
			}

			// Token: 0x17000DB4 RID: 3508
			// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000D044C File Offset: 0x000CE64C
			int ICollection.Count
			{
				get
				{
					return this._objects.Count;
				}
			}

			// Token: 0x17000DB5 RID: 3509
			// (get) Token: 0x060038B4 RID: 14516 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000DB6 RID: 3510
			// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000D0459 File Offset: 0x000CE659
			object ICollection.SyncRoot
			{
				get
				{
					return this._objects.SyncRoot;
				}
			}

			// Token: 0x060038B6 RID: 14518 RVA: 0x000D0466 File Offset: 0x000CE666
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new OrderedDictionary.OrderedDictionaryEnumerator(this._objects, this.isKeys ? 1 : 2);
			}

			// Token: 0x04002C74 RID: 11380
			private ArrayList _objects;

			// Token: 0x04002C75 RID: 11381
			private bool isKeys;
		}
	}
}
