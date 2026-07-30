using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> objects.</summary>
	// Token: 0x02000053 RID: 83
	public class DomainCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x170000E2 RID: 226
		public Domain this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object is in this collection; otherwise, false.</returns>
		/// <param name="domain">The <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> is null.</exception>
		// Token: 0x06000353 RID: 851 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(Domain domain)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
		/// <param name="domain">The <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> is null.</exception>
		// Token: 0x06000354 RID: 852 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(Domain domain)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="domains">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="domains" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domains" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x06000355 RID: 853 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(Domain[] domains, int index)
		{
			throw new NotImplementedException();
		}
	}
}
