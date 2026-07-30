using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed collection of key/value pairs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009C0 RID: 2496
	[ComVisible(true)]
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x001312DC File Offset: 0x0012F4DC
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this.hashtable == null)
				{
					this.hashtable = new Hashtable();
				}
				return this.hashtable;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x00002119 File Offset: 0x00000319
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x001312F7 File Offset: 0x0012F4F7
		public int Count
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x0013130E File Offset: 0x0012F50E
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x0013131B File Offset: 0x0012F51B
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x00131328 File Offset: 0x0012F528
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06005C49 RID: 23625 RVA: 0x00131335 File Offset: 0x0012F535
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x00131342 File Offset: 0x0012F542
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005C4B RID: 23627 RVA: 0x0013134F File Offset: 0x0012F54F
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.DictionaryBase" /> elements to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from the <see cref="T:System.Collections.DictionaryBase" /> instance. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.DictionaryBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.DictionaryBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C4C RID: 23628 RVA: 0x0013135C File Offset: 0x0012F55C
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x17001024 RID: 4132
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.DictionaryBase" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.DictionaryBase" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06005C4F RID: 23631 RVA: 0x00131418 File Offset: 0x0012F618
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.DictionaryBase" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x06005C50 RID: 23632 RVA: 0x00131428 File Offset: 0x0012F628
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C51 RID: 23633 RVA: 0x0013147C File Offset: 0x0012F67C
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x06005C52 RID: 23634 RVA: 0x00131498 File Offset: 0x0012F698
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object obj = this.InnerHashtable[key];
				this.OnValidate(key, obj);
				this.OnRemove(key, obj);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, obj);
				}
				catch
				{
					this.InnerHashtable.Add(key, obj);
					throw;
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C53 RID: 23635 RVA: 0x00131508 File Offset: 0x0012F708
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" />.</returns>
		// Token: 0x06005C54 RID: 23636 RVA: 0x00131508 File Offset: 0x0012F708
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Gets the element with the specified key and value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the element with the specified key and value.</returns>
		/// <param name="key">The key of the element to get. </param>
		/// <param name="currentValue">The current value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06005C55 RID: 23637 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06005C56 RID: 23638 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x06005C57 RID: 23639 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnInsert(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x06005C58 RID: 23640 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x06005C59 RID: 23641 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnRemove(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating the element with the specified key and value.</summary>
		/// <param name="key">The key of the element to validate. </param>
		/// <param name="value">The value of the element to validate. </param>
		// Token: 0x06005C5A RID: 23642 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnValidate(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06005C5B RID: 23643 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x06005C5C RID: 23644 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x06005C5D RID: 23645 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x06005C5E RID: 23646 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x04002F3B RID: 12091
		private Hashtable hashtable;
	}
}
