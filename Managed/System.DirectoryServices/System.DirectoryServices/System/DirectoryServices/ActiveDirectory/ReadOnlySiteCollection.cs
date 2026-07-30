using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> class is a read-only collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects.</summary>
	// Token: 0x0200006C RID: 108
	public class ReadOnlySiteCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.             </summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x1700010C RID: 268
		public ActiveDirectorySite this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection.             </summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection; otherwise, false.</returns>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		// Token: 0x060003EE RID: 1006 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.             </summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object.</returns>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> parameter is null.</exception>
		// Token: 0x060003EF RID: 1007 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects in this collection to the specified array, starting at the specified index of the target array.             </summary>
		/// <param name="sites">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="sites" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sites" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySite[] sites, int index)
		{
			throw new NotImplementedException();
		}
	}
}
