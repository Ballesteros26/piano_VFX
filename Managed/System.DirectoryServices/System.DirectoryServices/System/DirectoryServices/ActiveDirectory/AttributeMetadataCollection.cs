using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>A read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects. </summary>
	// Token: 0x0200004D RID: 77
	public class AttributeMetadataCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to get.</param>
		// Token: 0x170000CC RID: 204
		public AttributeMetadata this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> is in this collection, otherwise, false.</returns>
		/// <param name="metadata">The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="metadata" /> is null.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(AttributeMetadata metadata)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching item. -1 if no member of this collection is identical to <paramref name="metadata" />.</returns>
		/// <param name="metadata">The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="metadata" /> is null.</exception>
		// Token: 0x060002F7 RID: 759 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(AttributeMetadata metadata)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="metadata">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="metadata" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="metadata" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060002F8 RID: 760 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(AttributeMetadata[] metadata, int index)
		{
			throw new NotImplementedException();
		}
	}
}
