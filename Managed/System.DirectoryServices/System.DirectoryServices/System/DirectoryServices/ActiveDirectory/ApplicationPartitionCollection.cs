using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartitionCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects.</summary>
	// Token: 0x0200004B RID: 75
	public class ApplicationPartitionCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x170000C4 RID: 196
		public ApplicationPartition this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> is in this collection, otherwise, false.</returns>
		/// <param name="applicationPartition">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationPartition" /> is null.</exception>
		// Token: 0x060002E9 RID: 745 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ApplicationPartition applicationPartition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching item. -1 if no member of this collection is identical to <paramref name="applicationPartition" />.</returns>
		/// <param name="applicationPartition">The <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationPartition" /> is null.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ApplicationPartition applicationPartition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="applicationPartitions">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="applicationPartitions" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationPartitions" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060002EB RID: 747 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ApplicationPartition[] applicationPartitions, int index)
		{
			throw new NotImplementedException();
		}
	}
}
