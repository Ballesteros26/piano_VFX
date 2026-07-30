using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects. This class cannot be inherited. </summary>
	// Token: 0x0200059E RID: 1438
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x0600401C RID: 16412 RVA: 0x000E4AC5 File Offset: 0x000E2CC5
		internal KeyContainerPermissionAccessEntryCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000E4AD8 File Offset: 0x000E2CD8
		internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionAccessEntry[] entries)
		{
			if (entries != null)
			{
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in entries)
				{
					this.Add(keyContainerPermissionAccessEntry);
				}
			}
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in the collection.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x000E4B0A File Offset: 0x000E2D0A
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the item at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object at the specified index in the collection.</returns>
		/// <param name="index">The zero-based index of the element to access.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the collection count. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is negative. </exception>
		// Token: 0x17000A8F RID: 2703
		public KeyContainerPermissionAccessEntry this[int index]
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this._list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. </returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessEntry" /> is null.</exception>
		// Token: 0x06004022 RID: 16418 RVA: 0x000E4B2A File Offset: 0x000E2D2A
		public int Add(KeyContainerPermissionAccessEntry accessEntry)
		{
			return this._list.Add(accessEntry);
		}

		/// <summary>Removes all the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects from the collection.</summary>
		// Token: 0x06004023 RID: 16419 RVA: 0x000E4B38 File Offset: 0x000E2D38
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> array that is the destination of the elements copied from the current collection. </param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		// Token: 0x06004024 RID: 16420 RVA: 0x000E4B45 File Offset: 0x000E2D45
		public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		// Token: 0x06004025 RID: 16421 RVA: 0x000E4B45 File Offset: 0x000E2D45
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06004026 RID: 16422 RVA: 0x000E4B54 File Offset: 0x000E2D54
		public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection. </returns>
		// Token: 0x06004027 RID: 16423 RVA: 0x000E4B54 File Offset: 0x000E2D54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection, or –1 if no match is found.</returns>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to locate.</param>
		// Token: 0x06004028 RID: 16424 RVA: 0x000E4B64 File Offset: 0x000E2D64
		public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				if (accessEntry.Equals(this._list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object from thecollection.</summary>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessEntry" /> is null.</exception>
		// Token: 0x06004029 RID: 16425 RVA: 0x000E4BAC File Offset: 0x000E2DAC
		public void Remove(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				if (accessEntry.Equals(this._list[i]))
				{
					this._list.RemoveAt(i);
				}
			}
		}

		// Token: 0x04002084 RID: 8324
		private ArrayList _list;
	}
}
