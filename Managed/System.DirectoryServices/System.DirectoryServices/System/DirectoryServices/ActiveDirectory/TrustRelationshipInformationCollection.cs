using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformationCollection" /> class contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects.</summary>
	// Token: 0x0200008C RID: 140
	public class TrustRelationshipInformationCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x1700014D RID: 333
		public TrustRelationshipInformation this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object is contained in this collection.</summary>
		/// <returns>true if the specified object is in this collection; otherwise, false.</returns>
		/// <param name="information">The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="information" /> is null.</exception>
		// Token: 0x0600047D RID: 1149 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(TrustRelationshipInformation information)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object.</returns>
		/// <param name="information">The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="information" /> is null.</exception>
		// Token: 0x0600047E RID: 1150 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(TrustRelationshipInformation information)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x0600047F RID: 1151 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(TrustRelationshipInformation[] array, int index)
		{
			throw new NotImplementedException();
		}
	}
}
