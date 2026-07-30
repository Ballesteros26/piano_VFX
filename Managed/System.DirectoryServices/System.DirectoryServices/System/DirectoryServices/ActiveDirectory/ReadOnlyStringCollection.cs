using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyStringCollection" /> class is a read-only collection that contains <see cref="T:System.String" /> objects.</summary>
	// Token: 0x0200006F RID: 111
	public class ReadOnlyStringCollection : ReadOnlyCollectionBase
	{
		/// <summary>Gets a <see cref="T:System.String" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.String" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.String" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x1700010F RID: 271
		public string this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.String" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.String" /> object is in this collection, otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x060003FD RID: 1021 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.String" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.String" /> object.</returns>
		/// <param name="value">The <see cref="T:System.String" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		// Token: 0x060003FE RID: 1022 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(string value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.String" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="values">The array of <see cref="T:System.String" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="values" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060003FF RID: 1023 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(string[] values, int index)
		{
			throw new NotImplementedException();
		}
	}
}
