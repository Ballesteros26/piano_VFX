using System;
using System.Collections;

namespace System.DirectoryServices
{
	/// <summary>Contains a list of the schema names that the <see cref="P:System.DirectoryServices.DirectoryEntries.SchemaFilter" /> property of a <see cref="T:System.DirectoryServices.DirectoryEntries" /> object can use.          </summary>
	// Token: 0x0200002C RID: 44
	public class SchemaNameCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00002050 File Offset: 0x00000250
		internal SchemaNameCollection()
		{
		}

		/// <summary>Adds an item to the list.</summary>
		/// <returns>The position into which the new item was inserted.</returns>
		/// <param name="value">The item to add to the list.</param>
		// Token: 0x0600016B RID: 363 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		int IList.Add(object avalue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends a specified schema name to this collection.          </summary>
		/// <returns>The zero-based index of the specified property value. If the object is not found, the return value is -1.</returns>
		/// <param name="value">The schema name to add to this collection.</param>
		// Token: 0x0600016C RID: 364 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public int Add(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all objects from this collection.          </summary>
		// Token: 0x0600016D RID: 365 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the list contains a specified value.</summary>
		/// <returns>true if the value is found in the list, otherwise false.</returns>
		/// <param name="value">The value to locate in the list.</param>
		// Token: 0x0600016E RID: 366 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		bool IList.Contains(object cvalue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines the index of a specified item in the list.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The item to locate in the list.</param>
		// Token: 0x0600016F RID: 367 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		int IList.IndexOf(object ivalue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts an item to the list at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">The item to insert into the list.</param>
		// Token: 0x06000170 RID: 368 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		void IList.Insert(int index, object ivalue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the first occurrence of a specific <paramref name="value" /> from the list.</summary>
		/// <param name="value">The <paramref name="value" /> to remove from the list.</param>
		// Token: 0x06000171 RID: 369 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		void IList.Remove(object rvalue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the schema name that is at a specified index from this collection.          </summary>
		/// <param name="index">The zero-based index of the schema name to remove.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The zero-based index is either less than zero or equal to the size of the collection.</exception>
		// Token: 0x06000172 RID: 370 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000047D6 File Offset: 0x000029D6
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000047D6 File Offset: 0x000029D6
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IList" /> is read-only. </exception>
		// Token: 0x17000063 RID: 99
		object IList.this[int recordIndex]
		{
			[MonoTODO]
			get
			{
				throw new InvalidOperationException();
			}
			[MonoTODO]
			set
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the object that exists at a specified index.          </summary>
		/// <returns>The object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection.</param>
		// Token: 0x17000064 RID: 100
		public string this[int index]
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
			[MonoTODO]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>The <see cref="P:System.DirectoryServices.SchemaNameCollection.Count" /> property gets the number of objects in this collection.          </summary>
		/// <returns>The number of objects in this collection.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000047E0 File Offset: 0x000029E0
		public int Count
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000047D6 File Offset: 0x000029D6
		bool ICollection.IsSynchronized
		{
			[MonoTODO]
			get
			{
				return true;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00002058 File Offset: 0x00000258
		object ICollection.SyncRoot
		{
			[MonoTODO]
			get
			{
				return this;
			}
		}

		/// <summary>Returns an enumerator that you can use to iterate through this collection.          </summary>
		/// <returns>An enumerator that you can used to iterate through this collection.</returns>
		// Token: 0x0600017C RID: 380 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends a set of specified schema names to this collection.          </summary>
		/// <param name="value">A <see cref="T:System.DirectoryServices.SchemaNameCollection" /> that contains the schema names to add.</param>
		// Token: 0x0600017D RID: 381 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void AddRange(SchemaNameCollection value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends a set of specified schema names to this collection.          </summary>
		/// <param name="value">An array of type <see cref="T:System.String" /> that contains the schema names to add.</param>
		// Token: 0x0600017E RID: 382 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void AddRange(string[] value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether this collection contains a specified schema name.          </summary>
		/// <returns>The return value is true if the specified property belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The schema name to search for.</param>
		// Token: 0x0600017F RID: 383 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public bool Contains(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06000180 RID: 384 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		void ICollection.CopyTo(Array arr, int pos)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the schema names from this collection to an array, starting at a particular index of the array.          </summary>
		/// <param name="stringArray">An array of type <see cref="T:System.String" /> that receives this collection's schema names.</param>
		/// <param name="index">The zero-based array index at which to begin copying the schema names.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void CopyTo(string[] stringArray, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines the index of a specified schema name in this collection.          </summary>
		/// <returns>The zero-based index of the specified schema name, or -1 if the schema name was not found in the collection.</returns>
		/// <param name="value">The schema name to search for.</param>
		// Token: 0x06000182 RID: 386 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public int IndexOf(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts a schema name into this collection at a specified index.          </summary>
		/// <param name="index">The zero-based index into the collection at which to insert the schema name.</param>
		/// <param name="value">The schema name to insert into this collection.</param>
		// Token: 0x06000183 RID: 387 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void Insert(int index, string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a specified schema name from this collection.          </summary>
		/// <param name="value">The schema name to remove.</param>
		// Token: 0x06000184 RID: 388 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		public void Remove(string value)
		{
			throw new NotImplementedException();
		}
	}
}
