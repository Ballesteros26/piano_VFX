using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursorCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> objects.</summary>
	// Token: 0x02000073 RID: 115
	public class ReplicationCursorCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x17000122 RID: 290
		public ReplicationCursor this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object is in this collection; otherwise, false.</returns>
		/// <param name="cursor">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cursor" /> parameter is null.</exception>
		// Token: 0x0600042B RID: 1067 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ReplicationCursor cursor)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object.</returns>
		/// <param name="cursor">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cursor" /> parameter is null.</exception>
		// Token: 0x0600042C RID: 1068 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ReplicationCursor cursor)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="values">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="values" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="values" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x0600042D RID: 1069 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ReplicationCursor[] values, int index)
		{
			throw new NotImplementedException();
		}
	}
}
