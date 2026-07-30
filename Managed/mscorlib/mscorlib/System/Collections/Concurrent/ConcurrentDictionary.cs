using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently. </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	// Token: 0x020009F7 RID: 2551
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06005EA6 RID: 24230 RVA: 0x001377A8 File Offset: 0x001359A8
		private static bool IsValueWriteAtomic()
		{
			Type typeFromHandle = typeof(TValue);
			if (!typeFromHandle.IsValueType)
			{
				return true;
			}
			switch (Type.GetTypeCode(typeFromHandle))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Single:
				return true;
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Double:
				return IntPtr.Size == 8;
			default:
				return false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level, has the default initial capacity, and uses the default comparer for the key type.</summary>
		// Token: 0x06005EA7 RID: 24231 RVA: 0x00137817 File Offset: 0x00135A17
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the specified concurrency level and capacity, and uses the default comparer for the key type.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> is less than 1.-or-<paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06005EA8 RID: 24232 RVA: 0x00137828 File Offset: 0x00135A28
		public ConcurrentDictionary(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IEnumerable`1" />, has the default concurrency level, has the default initial capacity, and uses the default comparer for the key type.</summary>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or any of its keys is  null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collection" /> contains one or more duplicate keys.</exception>
		// Token: 0x06005EA9 RID: 24233 RVA: 0x00137834 File Offset: 0x00135A34
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level and capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The equality comparison implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06005EAA RID: 24234 RVA: 0x0013783E File Offset: 0x00135A3E
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" /> has the default concurrency level, has the default initial capacity, and uses the specified  <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="comparer" /> is null.</exception>
		// Token: 0x06005EAB RID: 24235 RVA: 0x0013784F File Offset: 0x00135A4F
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" />, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="collection">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="comparer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> is less than 1.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collection" /> contains one or more duplicate keys.</exception>
		// Token: 0x06005EAC RID: 24236 RVA: 0x0013786D File Offset: 0x00135A6D
		public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, 31, false, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x00137890 File Offset: 0x00135A90
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, this._comparer.GetHashCode(keyValuePair.Key), keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException("The source argument contains duplicate keys.");
				}
			}
			if (this._budget == 0)
			{
				this._budget = this._tables._buckets.Length / this._tables._locks.Length;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the specified concurrency level, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="concurrencyLevel">The estimated number of threads that will update the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> concurrently.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="concurrencyLevel" /> or <paramref name="capacity" /> is less than 1.</exception>
		// Token: 0x06005EAE RID: 24238 RVA: 0x00137948 File Offset: 0x00135B48
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, capacity, false, comparer)
		{
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x00137954 File Offset: 0x00135B54
		internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", "The concurrencyLevel argument must be positive.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "The capacity argument must be greater than or equal to zero.");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] array2 = new int[array.Length];
			ConcurrentDictionary<TKey, TValue>.Node[] array3 = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array3, array, array2);
			this._comparer = comparer ?? EqualityComparer<TKey>.Default;
			this._growLockArray = growLockArray;
			this._budget = array3.Length / array.Length;
		}

		/// <summary>Attempts to add the specified key and value to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the key/value pair was added to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> successfully; false if the key already exists.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be  null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005EB0 RID: 24240 RVA: 0x001379F8 File Offset: 0x00135BF8
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryAddInternal(key, this._comparer.GetHashCode(key), value, false, true, out tvalue);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005EB1 RID: 24241 RVA: 0x00137A2C File Offset: 0x00135C2C
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		/// <summary>Attempts to remove and return the value that has the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the object was removed successfully; otherwise, false.</returns>
		/// <param name="key">The key of the element to remove and return.</param>
		/// <param name="value">When this method returns, contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, or the default value of  the TValue type if <paramref name="key" /> does not exist. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		// Token: 0x06005EB2 RID: 24242 RVA: 0x00137A50 File Offset: 0x00135C50
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x00137A7C File Offset: 0x00135C7C
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			int hashCode = this._comparer.GetHashCode(key);
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashCode, out num, out num2, tables._buckets.Length, tables._locks.Length);
				object obj = tables._locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num];
					while (node2 != null)
					{
						if (hashCode == node2._hashcode && this._comparer.Equals(node2._key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2._value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node2._next);
							}
							else
							{
								node._next = node2._next;
							}
							value = node2._value;
							tables._countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2._next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Attempts to get the value associated with the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the key was found in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the object from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> that has the specified key, or the default value of , if the operation failed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		// Token: 0x06005EB4 RID: 24244 RVA: 0x00137BD0 File Offset: 0x00135DD0
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryGetValueInternal(key, this._comparer.GetHashCode(key), out value);
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x00137BF4 File Offset: 0x00135DF4
		private bool TryGetValueInternal(TKey key, int hashcode, out TValue value)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
			int bucket = ConcurrentDictionary<TKey, TValue>.GetBucket(hashcode, tables._buckets.Length);
			for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[bucket]); node != null; node = node._next)
			{
				if (hashcode == node._hashcode && this._comparer.Equals(node._key, key))
				{
					value = node._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Compares the existing value for the specified key with a specified value, and if they are equal, updates the key with a third value.</summary>
		/// <returns>true if the value with <paramref name="key" /> was equal to <paramref name="comparisonValue" /> and was replaced with <paramref name="newValue" />; otherwise, false.</returns>
		/// <param name="key">The key whose value is compared with <paramref name="comparisonValue" /> and possibly replaced.</param>
		/// <param name="newValue">The value that replaces the value of the element that has the specified <paramref name="key" /> if the comparison results in equality.</param>
		/// <param name="comparisonValue">The value that is compared to the value of the element that has the specified <paramref name="key" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005EB6 RID: 24246 RVA: 0x00137C6C File Offset: 0x00135E6C
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryUpdateInternal(key, this._comparer.GetHashCode(key), newValue, comparisonValue);
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x00137C90 File Offset: 0x00135E90
		private bool TryUpdateInternal(TKey key, int hashcode, TValue newValue, TValue comparisonValue)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool flag2;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashcode, out num, out num2, tables._buckets.Length, tables._locks.Length);
				object obj = tables._locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num];
					while (node2 != null)
					{
						if (hashcode == node2._hashcode && this._comparer.Equals(node2._key, key))
						{
							if (@default.Equals(node2._value, comparisonValue))
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2._value = newValue;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2._key, newValue, hashcode, node2._next);
									if (node == null)
									{
										Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node3);
									}
									else
									{
										node._next = node3;
									}
								}
								return true;
							}
							return false;
						}
						else
						{
							node = node2;
							node2 = node2._next;
						}
					}
					flag2 = false;
				}
				break;
			}
			return flag2;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		// Token: 0x06005EB8 RID: 24248 RVA: 0x00137DBC File Offset: 0x00135FBC
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this._tables._locks, new int[this._tables._countPerLock.Length]);
				this._tables = tables;
				this._budget = Math.Max(1, tables._buckets.Length / tables._locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x00137E44 File Offset: 0x00136044
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				int num3 = 0;
				while (num3 < this._tables._locks.Length && num2 >= 0)
				{
					num2 += this._tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Copies the key and value pairs stored in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of key and value pairs copied from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06005EBA RID: 24250 RVA: 0x00137EEC File Offset: 0x001360EC
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int num = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] array;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this._tables._locks.Length; i++)
					{
						num2 += this._tables._countPerLock[i];
					}
					if (num2 == 0)
					{
						array = Array.Empty<KeyValuePair<TKey, TValue>>();
					}
					else
					{
						KeyValuePair<TKey, TValue>[] array2 = new KeyValuePair<TKey, TValue>[num2];
						this.CopyToPairs(array2, 0);
						array = array2;
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return array;
			}
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x00137F70 File Offset: 0x00136170
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x00137FC8 File Offset: 0x001361C8
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x0013802C File Offset: 0x0013622C
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06005EBE RID: 24254 RVA: 0x00138085 File Offset: 0x00136285
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this._tables._buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current;
				for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]); current != null; current = current._next)
				{
					yield return new KeyValuePair<TKey, TValue>(current._key, current._value);
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x00138094 File Offset: 0x00136294
		private bool TryAddInternal(TKey key, int hashcode, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			checked
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables;
				bool flag;
				for (;;)
				{
					tables = this._tables;
					int num;
					int num2;
					ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashcode, out num, out num2, tables._buckets.Length, tables._locks.Length);
					flag = false;
					bool flag2 = false;
					try
					{
						if (acquireLock)
						{
							Monitor.Enter(tables._locks[num2], ref flag2);
						}
						if (tables != this._tables)
						{
							continue;
						}
						ConcurrentDictionary<TKey, TValue>.Node node = null;
						for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num]; node2 != null; node2 = node2._next)
						{
							if (hashcode == node2._hashcode && this._comparer.Equals(node2._key, key))
							{
								if (updateIfExists)
								{
									if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
									{
										node2._value = value;
									}
									else
									{
										ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2._key, value, hashcode, node2._next);
										if (node == null)
										{
											Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node3);
										}
										else
										{
											node._next = node3;
										}
									}
									resultingValue = value;
								}
								else
								{
									resultingValue = node2._value;
								}
								return false;
							}
							node = node2;
						}
						Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashcode, tables._buckets[num]));
						tables._countPerLock[num2]++;
						if (tables._countPerLock[num2] > this._budget)
						{
							flag = true;
						}
					}
					finally
					{
						if (flag2)
						{
							Monitor.Exit(tables._locks[num2]);
						}
					}
					break;
				}
				if (flag)
				{
					this.GrowTable(tables);
				}
				resultingValue = value;
				return true;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value of the key/value pair at the specified index.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x170010DC RID: 4316
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNotFoundException();
				}
				return tvalue;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				this.TryAddInternal(key, this._comparer.GetHashCode(key), value, true, true, out tvalue);
			}
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x00025116 File Offset: 0x00023316
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNotFoundException()
		{
			throw new KeyNotFoundException();
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x00138287 File Offset: 0x00136487
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNullException()
		{
			throw new ArgumentNullException("key");
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005EC4 RID: 24260 RVA: 0x00138294 File Offset: 0x00136494
		public int Count
		{
			get
			{
				int num = 0;
				int countInternal;
				try
				{
					this.AcquireAllLocks(ref num);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return countInternal;
			}
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x001382D0 File Offset: 0x001364D0
		private int GetCountInternal()
		{
			int num = 0;
			for (int i = 0; i < this._tables._countPerLock.Length; i++)
			{
				num += this._tables._countPerLock[i];
			}
			return num;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function, if the key does not already exist.</summary>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="valueFactory">The function used to generate a value for the key</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="valueFactory" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005EC6 RID: 24262 RVA: 0x00138310 File Offset: 0x00136510
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, valueFactory(key), false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x00138368 File Offset: 0x00136568
		public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, valueFactory(key, factoryArgument), false, true, out tvalue);
			}
			return tvalue;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist.</summary>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">the value to be added, if the key does not already exist</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005EC8 RID: 24264 RVA: 0x001383C0 File Offset: 0x001365C0
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, value, false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x00138404 File Offset: 0x00136604
		public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue, factoryArgument);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValueFactory(key, factoryArgument), false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		/// <summary>Uses the specified functions to add a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist, or to update a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key already exists.</summary>
		/// <returns>The new value for the key. This will be either be the result of addValueFactory (if the key was absent) or the result of updateValueFactory (if the key was present).</returns>
		/// <param name="key">The key to be added or whose value should be updated</param>
		/// <param name="addValueFactory">The function used to generate a value for an absent key</param>
		/// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" />, <paramref name="addValueFactory" />, or <paramref name="updateValueFactory" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005ECA RID: 24266 RVA: 0x00138484 File Offset: 0x00136684
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValueFactory(key), false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist, or updates a key/value pair in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function if the key already exists.</summary>
		/// <returns>The new value for the key. This will be either be addValue (if the key was absent) or the result of updateValueFactory (if the key was present).</returns>
		/// <param name="key">The key to be added or whose value should be updated</param>
		/// <param name="addValue">The value to be added for an absent key</param>
		/// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="updateValueFactory" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005ECB RID: 24267 RVA: 0x00138500 File Offset: 0x00136700
		public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValue, false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> is empty; otherwise, false.</returns>
		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x00138568 File Offset: 0x00136768
		public bool IsEmpty
		{
			get
			{
				int num = 0;
				try
				{
					this.AcquireAllLocks(ref num);
					for (int i = 0; i < this._tables._countPerLock.Length; i++)
					{
						if (this._tables._countPerLock[i] != 0)
						{
							return false;
						}
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return true;
			}
		}

		// Token: 0x06005ECD RID: 24269 RVA: 0x001385D0 File Offset: 0x001367D0
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException("The key already existed in the dictionary.");
			}
		}

		// Token: 0x06005ECE RID: 24270 RVA: 0x001385E8 File Offset: 0x001367E8
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection of keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x001385FE File Offset: 0x001367FE
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x001385FE File Offset: 0x001367FE
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Gets a collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />. </returns>
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x00138606 File Offset: 0x00136806
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005ED2 RID: 24274 RVA: 0x00138606 File Offset: 0x00136806
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x0013860E File Offset: 0x0013680E
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x00138624 File Offset: 0x00136824
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x00138658 File Offset: 0x00136858
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException("keyValuePair", "TKey is a reference type and item.Key is null.");
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06005ED7 RID: 24279 RVA: 0x0013869A File Offset: 0x0013689A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type  of the <see cref="T:System.Collections.Generic.Dictionary`2" />. -or- <paramref name="value" /> is of a type that is not assignable to the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />. -or-A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06005ED8 RID: 24280 RVA: 0x001386A4 File Offset: 0x001368A4
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (!(key is TKey))
			{
				throw new ArgumentException("The key was of an incorrect type for this dictionary.");
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("The value was of an incorrect type for this dictionary.");
			}
			((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), tvalue);
		}

		/// <summary>Gets whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005ED9 RID: 24281 RVA: 0x00138700 File Offset: 0x00136900
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Provides a <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x06005EDA RID: 24282 RVA: 0x00138720 File Offset: 0x00136920
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns false.</returns>
		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns false.</returns>
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys of the  <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>An interface that contains the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x001385FE File Offset: 0x001367FE
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06005EDE RID: 24286 RVA: 0x00138728 File Offset: 0x00136928
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An interface that contains the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005EDF RID: 24287 RVA: 0x00138606 File Offset: 0x00136806
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key, or  null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type or the value type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</exception>
		// Token: 0x170010E8 RID: 4328
		object IDictionary.this[object key]
		{
			get
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				if (!(key is TKey))
				{
					throw new ArgumentException("The key was of an incorrect type for this dictionary.");
				}
				if (!(value is TValue))
				{
					throw new ArgumentException("The value was of an incorrect type for this dictionary.");
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" />. -or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06005EE2 RID: 24290 RVA: 0x001387DC File Offset: 0x001369DC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num2 = 0;
				int num3 = 0;
				while (num3 < tables._locks.Length && num2 >= 0)
				{
					num2 += tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException("The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.", "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> this property always returns false.</returns>
		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06005EE3 RID: 24291 RVA: 0x00015ED5 File Offset: 0x000140D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Always returns null.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06005EE4 RID: 24292 RVA: 0x001388D0 File Offset: 0x00136AD0
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x001388DC File Offset: 0x00136ADC
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables)
		{
			int num = 0;
			try
			{
				this.AcquireLocks(0, 1, ref num);
				if (tables == this._tables)
				{
					long num2 = 0L;
					for (int i = 0; i < tables._countPerLock.Length; i++)
					{
						num2 += (long)tables._countPerLock[i];
					}
					if (num2 < (long)(tables._buckets.Length / 4))
					{
						this._budget = 2 * this._budget;
						if (this._budget < 0)
						{
							this._budget = int.MaxValue;
						}
					}
					else
					{
						int num3 = 0;
						bool flag = false;
						object[] array;
						checked
						{
							try
							{
								num3 = tables._buckets.Length * 2 + 1;
								while (num3 % 3 == 0 || num3 % 5 == 0 || num3 % 7 == 0)
								{
									num3 += 2;
								}
								if (num3 > 2146435071)
								{
									flag = true;
								}
							}
							catch (OverflowException)
							{
								flag = true;
							}
							if (flag)
							{
								num3 = 2146435071;
								this._budget = int.MaxValue;
							}
							this.AcquireLocks(1, tables._locks.Length, ref num);
							array = tables._locks;
						}
						if (this._growLockArray && tables._locks.Length < 1024)
						{
							array = new object[tables._locks.Length * 2];
							Array.Copy(tables._locks, 0, array, 0, tables._locks.Length);
							for (int j = tables._locks.Length; j < array.Length; j++)
							{
								array[j] = new object();
							}
						}
						ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[num3];
						int[] array3 = new int[array.Length];
						for (int k = 0; k < tables._buckets.Length; k++)
						{
							checked
							{
								ConcurrentDictionary<TKey, TValue>.Node next;
								for (ConcurrentDictionary<TKey, TValue>.Node node = tables._buckets[k]; node != null; node = next)
								{
									next = node._next;
									int num4;
									int num5;
									ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(node._hashcode, out num4, out num5, array2.Length, array.Length);
									array2[num4] = new ConcurrentDictionary<TKey, TValue>.Node(node._key, node._value, node._hashcode, array2[num4]);
									array3[num5]++;
								}
							}
						}
						this._budget = Math.Max(1, array2.Length / array.Length);
						this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, array3);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x00138B24 File Offset: 0x00136D24
		private static int GetBucket(int hashcode, int bucketCount)
		{
			return (hashcode & int.MaxValue) % bucketCount;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x00138B2F File Offset: 0x00136D2F
		private static void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x00138B43 File Offset: 0x00136D43
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return PlatformHelper.ProcessorCount;
			}
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x00138B4C File Offset: 0x00136D4C
		private void AcquireAllLocks(ref int locksAcquired)
		{
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this._tables._buckets.Length);
			}
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this._tables._locks.Length, ref locksAcquired);
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x00138BA0 File Offset: 0x00136DA0
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this._tables._locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x00138BF0 File Offset: 0x00136DF0
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this._tables._locks[i]);
			}
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x00138C20 File Offset: 0x00136E20
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int num = 0;
			ReadOnlyCollection<TKey> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TKey> list = new List<TKey>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._key);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x00138CB8 File Offset: 0x00136EB8
		private ReadOnlyCollection<TValue> GetValues()
		{
			int num = 0;
			ReadOnlyCollection<TValue> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TValue> list = new List<TValue>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._value);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x04002FCE RID: 12238
		private volatile ConcurrentDictionary<TKey, TValue>.Tables _tables;

		// Token: 0x04002FCF RID: 12239
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04002FD0 RID: 12240
		private readonly bool _growLockArray;

		// Token: 0x04002FD1 RID: 12241
		private int _budget;

		// Token: 0x04002FD2 RID: 12242
		private const int DefaultCapacity = 31;

		// Token: 0x04002FD3 RID: 12243
		private const int MaxLockNumber = 1024;

		// Token: 0x04002FD4 RID: 12244
		private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();

		// Token: 0x020009F8 RID: 2552
		private sealed class Tables
		{
			// Token: 0x06005EEF RID: 24303 RVA: 0x00138D5C File Offset: 0x00136F5C
			internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock)
			{
				this._buckets = buckets;
				this._locks = locks;
				this._countPerLock = countPerLock;
			}

			// Token: 0x04002FD5 RID: 12245
			internal readonly ConcurrentDictionary<TKey, TValue>.Node[] _buckets;

			// Token: 0x04002FD6 RID: 12246
			internal readonly object[] _locks;

			// Token: 0x04002FD7 RID: 12247
			internal volatile int[] _countPerLock;
		}

		// Token: 0x020009F9 RID: 2553
		[Serializable]
		private sealed class Node
		{
			// Token: 0x06005EF0 RID: 24304 RVA: 0x00138D7B File Offset: 0x00136F7B
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this._key = key;
				this._value = value;
				this._next = next;
				this._hashcode = hashcode;
			}

			// Token: 0x04002FD8 RID: 12248
			internal readonly TKey _key;

			// Token: 0x04002FD9 RID: 12249
			internal TValue _value;

			// Token: 0x04002FDA RID: 12250
			internal volatile ConcurrentDictionary<TKey, TValue>.Node _next;

			// Token: 0x04002FDB RID: 12251
			internal readonly int _hashcode;
		}

		// Token: 0x020009FA RID: 2554
		[Serializable]
		private sealed class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005EF1 RID: 24305 RVA: 0x00138DA2 File Offset: 0x00136FA2
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this._enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x170010EC RID: 4332
			// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x00138DB8 File Offset: 0x00136FB8
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170010ED RID: 4333
			// (get) Token: 0x06005EF3 RID: 24307 RVA: 0x00138DFC File Offset: 0x00136FFC
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170010EE RID: 4334
			// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00138E24 File Offset: 0x00137024
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170010EF RID: 4335
			// (get) Token: 0x06005EF5 RID: 24309 RVA: 0x00138E49 File Offset: 0x00137049
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06005EF6 RID: 24310 RVA: 0x00138E56 File Offset: 0x00137056
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06005EF7 RID: 24311 RVA: 0x00138E63 File Offset: 0x00137063
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x04002FDC RID: 12252
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}
	}
}
