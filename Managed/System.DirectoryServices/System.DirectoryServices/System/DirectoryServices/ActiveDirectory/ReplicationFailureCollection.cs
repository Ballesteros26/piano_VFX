using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailureCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> objects.</summary>
	// Token: 0x02000075 RID: 117
	public class ReplicationFailureCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x17000128 RID: 296
		public ReplicationFailure this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object is in this collection; otherwise, false.</returns>
		/// <param name="failure">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="failure" /> is null.</exception>
		// Token: 0x06000436 RID: 1078 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ReplicationFailure failure)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object.</returns>
		/// <param name="failure">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="failure" /> is null.</exception>
		// Token: 0x06000437 RID: 1079 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ReplicationFailure failure)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="failures">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailure" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="failures" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="failures" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ReplicationFailure[] failures, int index)
		{
			throw new NotImplementedException();
		}
	}
}
