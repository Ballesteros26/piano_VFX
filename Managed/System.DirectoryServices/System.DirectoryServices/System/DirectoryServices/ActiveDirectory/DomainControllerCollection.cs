using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> objects.</summary>
	// Token: 0x02000056 RID: 86
	public class DomainControllerCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x170000EE RID: 238
		public DomainController this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object is in this collection; otherwise, false.</returns>
		/// <param name="domainController">The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domainController" /> is null.</exception>
		// Token: 0x0600037C RID: 892 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(DomainController domainController)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object.</returns>
		/// <param name="domainController">The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domainController" /> is null.</exception>
		// Token: 0x0600037D RID: 893 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(DomainController domainController)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="domainControllers">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="domainControllers" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domainControllers" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x0600037E RID: 894 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(DomainController[] domainControllers, int index)
		{
			throw new NotImplementedException();
		}
	}
}
