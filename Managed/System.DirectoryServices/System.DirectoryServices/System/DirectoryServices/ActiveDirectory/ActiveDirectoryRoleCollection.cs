using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRoleCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> objects.</summary>
	// Token: 0x02000038 RID: 56
	public class ActiveDirectoryRoleCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x1700007B RID: 123
		public ActiveDirectoryRole this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object is in this collection; otherwise, false.</returns>
		/// <param name="role">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="role" /> is null.</exception>
		// Token: 0x060001C3 RID: 451 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectoryRole role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object.</returns>
		/// <param name="role">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="role" /> is null.</exception>
		// Token: 0x060001C4 RID: 452 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectoryRole role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> objects in this collection to the specified array, starting at the specified index in the array.</summary>
		/// <param name="roles">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="roles" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough to hold the required number of elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roles" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060001C5 RID: 453 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectoryRole[] roles, int index)
		{
			throw new NotImplementedException();
		}
	}
}
