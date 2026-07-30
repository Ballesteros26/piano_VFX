using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009E1 RID: 2529
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[ComVisible(true)]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		// Token: 0x06005D63 RID: 23907 RVA: 0x001347EF File Offset: 0x001329EF
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001347FD File Offset: 0x001329FD
		private void Init()
		{
			this.keys = SortedList.emptyArray;
			this.values = SortedList.emptyArray;
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
		// Token: 0x06005D65 RID: 23909 RVA: 0x0013482C File Offset: 0x00132A2C
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("Non-negative number required."));
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		// Token: 0x06005D66 RID: 23910 RVA: 0x00134880 File Offset: 0x00132A80
		public SortedList(IComparer comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="capacity" />.</exception>
		// Token: 0x06005D67 RID: 23911 RVA: 0x00134892 File Offset: 0x00132A92
		public SortedList(IComparer comparer, int capacity)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">One or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface. </exception>
		// Token: 0x06005D68 RID: 23912 RVA: 0x001348A2 File Offset: 0x00132AA2
		public SortedList(IDictionary d)
			: this(d, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface. </exception>
		// Token: 0x06005D69 RID: 23913 RVA: 0x001348AC File Offset: 0x00132AAC
		public SortedList(IDictionary d, IComparer comparer)
			: this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("Dictionary cannot be null."));
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, this.values, comparer);
			this._size = d.Count;
		}

		/// <summary>Adds an element with the specified key and value to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the specified <paramref name="key" /> already exists in the <see cref="T:System.Collections.SortedList" /> object.-or- The <see cref="T:System.Collections.SortedList" /> is set to use the <see cref="T:System.IComparable" /> interface, and <paramref name="key" /> does not implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D6A RID: 23914 RVA: 0x00134928 File Offset: 0x00132B28
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", new object[]
				{
					this.GetKey(num),
					key
				}));
			}
			this.Insert(~num, key, value);
		}

		/// <summary>Gets or sets the capacity of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the current number of elements in the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x00134999 File Offset: 0x00132B99
		// (set) Token: 0x06005D6C RID: 23916 RVA: 0x001349A4 File Offset: 0x00132BA4
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						object[] array2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = SortedList.emptyArray;
					this.values = SortedList.emptyArray;
				}
			}
		}

		/// <summary>Gets the number of elements contained in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x00134A42 File Offset: 0x00132C42
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x00134A4A File Offset: 0x00132C4A
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x00134A52 File Offset: 0x00132C52
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06005D70 RID: 23920 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x00134A5A File Offset: 0x00132C5A
		public virtual object SyncRoot
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

		/// <summary>Removes all elements from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D74 RID: 23924 RVA: 0x00134A7C File Offset: 0x00132C7C
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		/// <summary>Creates a shallow copy of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005D75 RID: 23925 RVA: 0x00134AB8 File Offset: 0x00132CB8
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D76 RID: 23926 RVA: 0x00134B28 File Offset: 0x00132D28
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D77 RID: 23927 RVA: 0x00134B28 File Offset: 0x00132D28
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D78 RID: 23928 RVA: 0x00134B37 File Offset: 0x00132D37
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Copies <see cref="T:System.Collections.SortedList" /> elements to a one-dimensional <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.SortedList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.SortedList" /> object is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.SortedList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D79 RID: 23929 RVA: 0x00134B48 File Offset: 0x00132D48
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("Array cannot be null."));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Destination array is not long enough to copy all the items in the collection. Check array index and length."));
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x00134BF8 File Offset: 0x00132DF8
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x00134C3C File Offset: 0x00132E3C
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 16 : (this.keys.Length * 2));
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

		/// <summary>Gets the value at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the value to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D7C RID: 23932 RVA: 0x00134C7C File Offset: 0x00132E7C
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return this.values[index];
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.SortedList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.SortedList" />.</returns>
		// Token: 0x06005D7D RID: 23933 RVA: 0x00134CA8 File Offset: 0x00132EA8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D7E RID: 23934 RVA: 0x00134CA8 File Offset: 0x00132EA8
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Gets the key at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The key at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the key to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D7F RID: 23935 RVA: 0x00134CB8 File Offset: 0x00132EB8
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return this.keys[index];
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D80 RID: 23936 RVA: 0x00134CE4 File Offset: 0x00132EE4
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D81 RID: 23937 RVA: 0x00134D00 File Offset: 0x00132F00
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		/// <summary>Gets and sets the value associated with a specific key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value associated with the <paramref name="key" /> parameter in the <see cref="T:System.Collections.SortedList" /> object, if <paramref name="key" /> is found; otherwise, null.</returns>
		/// <param name="key">The key associated with the value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001085 RID: 4229
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Returns the zero-based index of the specified key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the <paramref name="key" /> parameter, if <paramref name="key" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D84 RID: 23940 RVA: 0x00134DA8 File Offset: 0x00132FA8
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified value in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter, if <paramref name="value" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D85 RID: 23941 RVA: 0x00134DEE File Offset: 0x00132FEE
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x00134E04 File Offset: 0x00133004
		private void Insert(int index, object key, object value)
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

		/// <summary>Removes the element at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D87 RID: 23943 RVA: 0x00134EA0 File Offset: 0x001330A0
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		/// <summary>Removes the element with the specified key from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06005D88 RID: 23944 RVA: 0x00134F4C File Offset: 0x0013314C
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Replaces the value at a specific index in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index at which to save <paramref name="value" />. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to save into the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D89 RID: 23945 RVA: 0x00134F6C File Offset: 0x0013316C
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			this.values[index] = value;
			this.version++;
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="list">The <see cref="T:System.Collections.SortedList" /> object to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005D8A RID: 23946 RVA: 0x00134FA7 File Offset: 0x001331A7
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		/// <summary>Sets the capacity to the actual number of elements in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005D8B RID: 23947 RVA: 0x00134FBD File Offset: 0x001331BD
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04002F8B RID: 12171
		private object[] keys;

		// Token: 0x04002F8C RID: 12172
		private object[] values;

		// Token: 0x04002F8D RID: 12173
		private int _size;

		// Token: 0x04002F8E RID: 12174
		private int version;

		// Token: 0x04002F8F RID: 12175
		private IComparer comparer;

		// Token: 0x04002F90 RID: 12176
		private SortedList.KeyList keyList;

		// Token: 0x04002F91 RID: 12177
		private SortedList.ValueList valueList;

		// Token: 0x04002F92 RID: 12178
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002F93 RID: 12179
		private const int _defaultCapacity = 16;

		// Token: 0x04002F94 RID: 12180
		private static object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x020009E2 RID: 2530
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06005D8D RID: 23949 RVA: 0x00134FD7 File Offset: 0x001331D7
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17001086 RID: 4230
			// (get) Token: 0x06005D8E RID: 23950 RVA: 0x00134FF4 File Offset: 0x001331F4
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17001087 RID: 4231
			// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0013503C File Offset: 0x0013323C
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001088 RID: 4232
			// (get) Token: 0x06005D90 RID: 23952 RVA: 0x00135044 File Offset: 0x00133244
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001089 RID: 4233
			// (get) Token: 0x06005D91 RID: 23953 RVA: 0x00135051 File Offset: 0x00133251
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700108A RID: 4234
			// (get) Token: 0x06005D92 RID: 23954 RVA: 0x00003B29 File Offset: 0x00001D29
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700108B RID: 4235
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[key];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x06005D95 RID: 23957 RVA: 0x001350F0 File Offset: 0x001332F0
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x1700108C RID: 4236
			// (get) Token: 0x06005D96 RID: 23958 RVA: 0x00135138 File Offset: 0x00133338
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06005D97 RID: 23959 RVA: 0x00135180 File Offset: 0x00133380
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06005D98 RID: 23960 RVA: 0x001351C8 File Offset: 0x001333C8
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._list.Clone();
				}
				return obj;
			}

			// Token: 0x06005D99 RID: 23961 RVA: 0x00135210 File Offset: 0x00133410
			public override bool Contains(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(key);
				}
				return flag2;
			}

			// Token: 0x06005D9A RID: 23962 RVA: 0x00135258 File Offset: 0x00133458
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsKey(key);
				}
				return flag2;
			}

			// Token: 0x06005D9B RID: 23963 RVA: 0x001352A0 File Offset: 0x001334A0
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x06005D9C RID: 23964 RVA: 0x001352E8 File Offset: 0x001334E8
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06005D9D RID: 23965 RVA: 0x00135330 File Offset: 0x00133530
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06005D9E RID: 23966 RVA: 0x00135378 File Offset: 0x00133578
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005D9F RID: 23967 RVA: 0x001353C0 File Offset: 0x001335C0
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x06005DA0 RID: 23968 RVA: 0x00135408 File Offset: 0x00133608
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x06005DA1 RID: 23969 RVA: 0x00135450 File Offset: 0x00133650
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x06005DA2 RID: 23970 RVA: 0x00135498 File Offset: 0x00133698
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfKey(key);
				}
				return num;
			}

			// Token: 0x06005DA3 RID: 23971 RVA: 0x001354F8 File Offset: 0x001336F8
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfValue(value);
				}
				return num;
			}

			// Token: 0x06005DA4 RID: 23972 RVA: 0x00135540 File Offset: 0x00133740
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06005DA5 RID: 23973 RVA: 0x00135588 File Offset: 0x00133788
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06005DA6 RID: 23974 RVA: 0x001355D0 File Offset: 0x001337D0
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06005DA7 RID: 23975 RVA: 0x00135618 File Offset: 0x00133818
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06005DA8 RID: 23976 RVA: 0x00135628 File Offset: 0x00133828
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04002F95 RID: 12181
			private SortedList _list;

			// Token: 0x04002F96 RID: 12182
			private object _root;
		}

		// Token: 0x020009E3 RID: 2531
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06005DA9 RID: 23977 RVA: 0x00135670 File Offset: 0x00133870
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this.sortedList = sortedList;
				this.index = index;
				this.startIndex = index;
				this.endIndex = index + count;
				this.version = sortedList.version;
				this.getObjectRetType = getObjRetType;
				this.current = false;
			}

			// Token: 0x06005DAA RID: 23978 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x1700108D RID: 4237
			// (get) Token: 0x06005DAB RID: 23979 RVA: 0x001356BC File Offset: 0x001338BC
			public virtual object Key
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return this.key;
				}
			}

			// Token: 0x06005DAC RID: 23980 RVA: 0x0013570C File Offset: 0x0013390C
			public virtual bool MoveNext()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.index < this.endIndex)
				{
					this.key = this.sortedList.keys[this.index];
					this.value = this.sortedList.values[this.index];
					this.index++;
					this.current = true;
					return true;
				}
				this.key = null;
				this.value = null;
				this.current = false;
				return false;
			}

			// Token: 0x1700108E RID: 4238
			// (get) Token: 0x06005DAD RID: 23981 RVA: 0x001357A8 File Offset: 0x001339A8
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x1700108F RID: 4239
			// (get) Token: 0x06005DAE RID: 23982 RVA: 0x00135804 File Offset: 0x00133A04
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					if (this.getObjectRetType == 1)
					{
						return this.key;
					}
					if (this.getObjectRetType == 2)
					{
						return this.value;
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x17001090 RID: 4240
			// (get) Token: 0x06005DAF RID: 23983 RVA: 0x00135860 File Offset: 0x00133A60
			public virtual object Value
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
					}
					return this.value;
				}
			}

			// Token: 0x06005DB0 RID: 23984 RVA: 0x001358B0 File Offset: 0x00133AB0
			public virtual void Reset()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.index = this.startIndex;
				this.current = false;
				this.key = null;
				this.value = null;
			}

			// Token: 0x04002F97 RID: 12183
			private SortedList sortedList;

			// Token: 0x04002F98 RID: 12184
			private object key;

			// Token: 0x04002F99 RID: 12185
			private object value;

			// Token: 0x04002F9A RID: 12186
			private int index;

			// Token: 0x04002F9B RID: 12187
			private int startIndex;

			// Token: 0x04002F9C RID: 12188
			private int endIndex;

			// Token: 0x04002F9D RID: 12189
			private int version;

			// Token: 0x04002F9E RID: 12190
			private bool current;

			// Token: 0x04002F9F RID: 12191
			private int getObjectRetType;

			// Token: 0x04002FA0 RID: 12192
			internal const int Keys = 1;

			// Token: 0x04002FA1 RID: 12193
			internal const int Values = 2;

			// Token: 0x04002FA2 RID: 12194
			internal const int DictEntry = 3;
		}

		// Token: 0x020009E4 RID: 2532
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005DB1 RID: 23985 RVA: 0x00135901 File Offset: 0x00133B01
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x17001091 RID: 4241
			// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x00135910 File Offset: 0x00133B10
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x17001092 RID: 4242
			// (get) Token: 0x06005DB3 RID: 23987 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001093 RID: 4243
			// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001094 RID: 4244
			// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x0013591D File Offset: 0x00133B1D
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x17001095 RID: 4245
			// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0013592A File Offset: 0x00133B2A
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06005DB7 RID: 23991 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DB8 RID: 23992 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DB9 RID: 23993 RVA: 0x00135948 File Offset: 0x00133B48
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x06005DBA RID: 23994 RVA: 0x00135956 File Offset: 0x00133B56
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06005DBB RID: 23995 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x17001096 RID: 4246
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("Mutating a key collection derived from a dictionary is not allowed."));
				}
			}

			// Token: 0x06005DBE RID: 23998 RVA: 0x001359B1 File Offset: 0x00133BB1
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x06005DBF RID: 23999 RVA: 0x001359CC File Offset: 0x00133BCC
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06005DC0 RID: 24000 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Remove(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DC1 RID: 24001 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x04002FA3 RID: 12195
			private SortedList sortedList;
		}

		// Token: 0x020009E5 RID: 2533
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x06005DC2 RID: 24002 RVA: 0x00135A21 File Offset: 0x00133C21
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x17001097 RID: 4247
			// (get) Token: 0x06005DC3 RID: 24003 RVA: 0x00135A30 File Offset: 0x00133C30
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x17001098 RID: 4248
			// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001099 RID: 4249
			// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x00003B29 File Offset: 0x00001D29
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700109A RID: 4250
			// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00135A3D File Offset: 0x00133C3D
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x1700109B RID: 4251
			// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x00135A4A File Offset: 0x00133C4A
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06005DC8 RID: 24008 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DC9 RID: 24009 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DCA RID: 24010 RVA: 0x00135A57 File Offset: 0x00133C57
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x06005DCB RID: 24011 RVA: 0x00135A65 File Offset: 0x00133C65
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06005DCC RID: 24012 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x1700109C RID: 4252
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
				}
			}

			// Token: 0x06005DCF RID: 24015 RVA: 0x00135AAF File Offset: 0x00133CAF
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x06005DD0 RID: 24016 RVA: 0x00135AC9 File Offset: 0x00133CC9
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x06005DD1 RID: 24017 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x06005DD2 RID: 24018 RVA: 0x00135937 File Offset: 0x00133B37
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("This operation is not supported on SortedList nested types because they require modifying the original SortedList."));
			}

			// Token: 0x04002FA4 RID: 12196
			private SortedList sortedList;
		}

		// Token: 0x020009E6 RID: 2534
		internal class SortedListDebugView
		{
			// Token: 0x06005DD3 RID: 24019 RVA: 0x00135AE8 File Offset: 0x00133CE8
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this.sortedList = sortedList;
			}

			// Token: 0x1700109D RID: 4253
			// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x00135B05 File Offset: 0x00133D05
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04002FA5 RID: 12197
			private SortedList sortedList;
		}
	}
}
