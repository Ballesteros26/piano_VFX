using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalogCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> objects.</summary>
	// Token: 0x02000063 RID: 99
	public class GlobalCatalogCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x17000108 RID: 264
		public GlobalCatalog this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> is in this collection; otherwise, false.</returns>
		/// <param name="globalCatalog">The <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="globalCatalog" /> is null.</exception>
		// Token: 0x060003DA RID: 986 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(GlobalCatalog globalCatalog)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object.</returns>
		/// <param name="globalCatalog">The <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="globalCatalog" /> is null.</exception>
		// Token: 0x060003DB RID: 987 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(GlobalCatalog globalCatalog)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="globalCatalogs">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="globalCatalogs" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="globalCatalogs" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003DC RID: 988 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(GlobalCatalog[] globalCatalogs, int index)
		{
			throw new NotImplementedException();
		}
	}
}
