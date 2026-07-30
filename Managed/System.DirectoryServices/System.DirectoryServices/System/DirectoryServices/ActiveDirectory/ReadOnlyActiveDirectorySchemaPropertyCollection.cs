using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects.</summary>
	// Token: 0x0200006A RID: 106
	public class ReadOnlyActiveDirectorySchemaPropertyCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
		// Token: 0x1700010A RID: 266
		public ActiveDirectorySchemaProperty this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is in this collection; otherwise, false.</returns>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="properties">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="properties" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="properties" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySchemaProperty[] properties, int index)
		{
			throw new NotImplementedException();
		}
	}
}
