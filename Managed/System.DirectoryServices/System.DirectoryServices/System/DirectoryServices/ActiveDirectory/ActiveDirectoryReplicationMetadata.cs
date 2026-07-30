using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> class contains replication information for a set of Active Directory Domain Services attributes.</summary>
	// Token: 0x02000036 RID: 54
	public class ActiveDirectoryReplicationMetadata : DictionaryBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object that represents the specified attribute.</returns>
		/// <param name="name">The LDAP display name of the attribute to get.</param>
		// Token: 0x17000078 RID: 120
		public AttributeMetadata this[string name]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the names that are contained in this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the LDAP display names of the attributes in this collection.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyStringCollection AttributeNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the values that are contained in this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000208C File Offset: 0x0000028C
		public AttributeMetadataCollection Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified attribute is in this collection.</summary>
		/// <returns>true if the attribute is in this collection; otherwise, false.</returns>
		/// <param name="attributeName">The LDAP display name of the attribute to search for.</param>
		// Token: 0x060001BF RID: 447 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(string attributeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough to hold the required number of elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is out of range of the destination array.</exception>
		// Token: 0x060001C0 RID: 448 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(AttributeMetadata[] array, int index)
		{
			throw new NotImplementedException();
		}
	}
}
