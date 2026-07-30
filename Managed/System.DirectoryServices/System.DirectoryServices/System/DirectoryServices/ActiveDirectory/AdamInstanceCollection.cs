using System;
using System.Collections;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects.</summary>
	// Token: 0x02000096 RID: 150
	public class AdamInstanceCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00002644 File Offset: 0x00000844
		internal AdamInstanceCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x17000165 RID: 357
		public AdamInstance this[int index]
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object is in this collection; otherwise, false.</returns>
		/// <param name="adamInstance">The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="adamInstance" /> is null.</exception>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00004D00 File Offset: 0x00002F00
		public bool Contains(AdamInstance adamInstance)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="adamInstances">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="adamInstances" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough to hold the required number of elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="adamInstances" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x060004D6 RID: 1238 RVA: 0x00002644 File Offset: 0x00000844
		public void CopyTo(AdamInstance[] adamInstances, int index)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object.</returns>
		/// <param name="adamInstance">The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="adamInstance" /> is null.</exception>
		// Token: 0x060004D7 RID: 1239 RVA: 0x00004D1C File Offset: 0x00002F1C
		public int IndexOf(AdamInstance adamInstance)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
