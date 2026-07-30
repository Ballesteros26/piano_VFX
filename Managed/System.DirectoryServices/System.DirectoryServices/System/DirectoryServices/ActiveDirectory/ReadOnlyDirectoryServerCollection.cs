using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects.</summary>
	// Token: 0x0200006B RID: 107
	public class ReadOnlyDirectoryServerCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x1700010B RID: 267
		public DirectoryServer this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object is in this collection; otherwise, false.</returns>
		/// <param name="directoryServer">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="directoryServer" /> is null.</exception>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(DirectoryServer directoryServer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object.</returns>
		/// <param name="directoryServer">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="directoryServer" /> is null.</exception>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(DirectoryServer directoryServer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="directoryServers">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="directoryServers" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="directoryServers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003EB RID: 1003 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(DirectoryServer[] directoryServers, int index)
		{
			throw new NotImplementedException();
		}
	}
}
