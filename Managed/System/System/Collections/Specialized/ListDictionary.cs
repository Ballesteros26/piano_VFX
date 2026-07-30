using System;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary using a singly linked list. Recommended for collections that typically include fewer than 10 items.</summary>
	// Token: 0x020006F9 RID: 1785
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the default comparer.</summary>
		// Token: 0x060037F4 RID: 14324 RVA: 0x000020EB File Offset: 0x000002EB
		public ListDictionary()
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x060037F5 RID: 14325 RVA: 0x000CDD4B File Offset: 0x000CBF4B
		public ListDictionary(IComparer comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x17000D7E RID: 3454
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", global::SR.GetString("Key cannot be null."));
				}
				ListDictionary.DictionaryNode dictionaryNode = this.head;
				if (this.comparer == null)
				{
					while (dictionaryNode != null)
					{
						object key2 = dictionaryNode.key;
						if (key2 != null && key2.Equals(key))
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				else
				{
					while (dictionaryNode != null)
					{
						object key3 = dictionaryNode.key;
						if (key3 != null && this.comparer.Compare(key3, key) == 0)
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", global::SR.GetString("Key cannot be null."));
				}
				this.version++;
				ListDictionary.DictionaryNode dictionaryNode = null;
				ListDictionary.DictionaryNode next;
				for (next = this.head; next != null; next = next.next)
				{
					object key2 = next.key;
					if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
					{
						break;
					}
					dictionaryNode = next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000CDE98 File Offset: 0x000CC098
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000CDEA0 File Offset: 0x000CC0A0
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, true);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x00004240 File Offset: 0x00002440
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000CDEA9 File Offset: 0x000CC0A9
		public object SyncRoot
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

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000CDECB File Offset: 0x000CC0CB
		public ICollection Values
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </exception>
		// Token: 0x060037FF RID: 14335 RVA: 0x000CDED4 File Offset: 0x000CC0D4
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", global::SR.GetString("Key cannot be null."));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					throw new ArgumentException(global::SR.GetString("An item with the same key has already been added. Key: {0}"));
				}
				dictionaryNode = next;
			}
			ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		// Token: 0x06003800 RID: 14336 RVA: 0x000CDF8D File Offset: 0x000CC18D
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.ListDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06003801 RID: 14337 RVA: 0x000CDFAC File Offset: 0x000CC1AC
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", global::SR.GetString("Key cannot be null."));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.ListDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.ListDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.ListDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.ListDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003802 RID: 14338 RVA: 0x000CE014 File Offset: 0x000CC214
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", global::SR.GetString("Non-negative number required."));
			}
			if (array.Length - index < this.count)
			{
				throw new ArgumentException(global::SR.GetString("Insufficient space in the target location to copy the information."));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06003803 RID: 14339 RVA: 0x000CE09D File Offset: 0x000CC29D
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x06003804 RID: 14340 RVA: 0x000CE09D File Offset: 0x000CC29D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06003805 RID: 14341 RVA: 0x000CE0A8 File Offset: 0x000CC2A8
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", global::SR.GetString("Key cannot be null."));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			ListDictionary.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					break;
				}
				dictionaryNode = next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04002C28 RID: 11304
		private ListDictionary.DictionaryNode head;

		// Token: 0x04002C29 RID: 11305
		private int version;

		// Token: 0x04002C2A RID: 11306
		private int count;

		// Token: 0x04002C2B RID: 11307
		private IComparer comparer;

		// Token: 0x04002C2C RID: 11308
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020006FA RID: 1786
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003806 RID: 14342 RVA: 0x000CE151 File Offset: 0x000CC351
			public NodeEnumerator(ListDictionary list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17000D86 RID: 3462
			// (get) Token: 0x06003807 RID: 14343 RVA: 0x000CE17A File Offset: 0x000CC37A
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000D87 RID: 3463
			// (get) Token: 0x06003808 RID: 14344 RVA: 0x000CE187 File Offset: 0x000CC387
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x17000D88 RID: 3464
			// (get) Token: 0x06003809 RID: 14345 RVA: 0x000CE1BC File Offset: 0x000CC3BC
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.current.key;
				}
			}

			// Token: 0x17000D89 RID: 3465
			// (get) Token: 0x0600380A RID: 14346 RVA: 0x000CE1E1 File Offset: 0x000CC3E1
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.current.value;
				}
			}

			// Token: 0x0600380B RID: 14347 RVA: 0x000CE208 File Offset: 0x000CC408
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x0600380C RID: 14348 RVA: 0x000CE27C File Offset: 0x000CC47C
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04002C2D RID: 11309
			private ListDictionary list;

			// Token: 0x04002C2E RID: 11310
			private ListDictionary.DictionaryNode current;

			// Token: 0x04002C2F RID: 11311
			private int version;

			// Token: 0x04002C30 RID: 11312
			private bool start;
		}

		// Token: 0x020006FB RID: 1787
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x0600380D RID: 14349 RVA: 0x000CE2AF File Offset: 0x000CC4AF
			public NodeKeyValueCollection(ListDictionary list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x0600380E RID: 14350 RVA: 0x000CE2C8 File Offset: 0x000CC4C8
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", global::SR.GetString("Non-negative number required."));
				}
				for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x17000D8A RID: 3466
			// (get) Token: 0x0600380F RID: 14351 RVA: 0x000CE338 File Offset: 0x000CC538
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000D8B RID: 3467
			// (get) Token: 0x06003810 RID: 14352 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D8C RID: 3468
			// (get) Token: 0x06003811 RID: 14353 RVA: 0x000CE364 File Offset: 0x000CC564
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06003812 RID: 14354 RVA: 0x000CE371 File Offset: 0x000CC571
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionary.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04002C31 RID: 11313
			private ListDictionary list;

			// Token: 0x04002C32 RID: 11314
			private bool isKeys;

			// Token: 0x020006FC RID: 1788
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06003813 RID: 14355 RVA: 0x000CE384 File Offset: 0x000CC584
				public NodeKeyValueEnumerator(ListDictionary list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17000D8D RID: 3469
				// (get) Token: 0x06003814 RID: 14356 RVA: 0x000CE3B4 File Offset: 0x000CC5B4
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06003815 RID: 14357 RVA: 0x000CE3F0 File Offset: 0x000CC5F0
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06003816 RID: 14358 RVA: 0x000CE464 File Offset: 0x000CC664
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(global::SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04002C33 RID: 11315
				private ListDictionary list;

				// Token: 0x04002C34 RID: 11316
				private ListDictionary.DictionaryNode current;

				// Token: 0x04002C35 RID: 11317
				private int version;

				// Token: 0x04002C36 RID: 11318
				private bool isKeys;

				// Token: 0x04002C37 RID: 11319
				private bool start;
			}
		}

		// Token: 0x020006FD RID: 1789
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04002C38 RID: 11320
			public object key;

			// Token: 0x04002C39 RID: 11321
			public object value;

			// Token: 0x04002C3A RID: 11322
			public ListDictionary.DictionaryNode next;
		}
	}
}
