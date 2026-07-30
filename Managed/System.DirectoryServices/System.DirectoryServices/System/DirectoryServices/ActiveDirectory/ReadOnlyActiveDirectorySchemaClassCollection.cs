using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects.</summary>
	// Token: 0x02000069 RID: 105
	public class ReadOnlyActiveDirectorySchemaClassCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
		// Token: 0x17000109 RID: 265
		public ActiveDirectorySchemaClass this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is in this collection; otherwise, false.</returns>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemaClass" /> parameter is null.</exception>
		// Token: 0x060003DF RID: 991 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</returns>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemaClass" /> parameter is null.</exception>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="classes">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="classes" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="classes" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySchemaClass[] classes, int index)
		{
			throw new NotImplementedException();
		}
	}
}
