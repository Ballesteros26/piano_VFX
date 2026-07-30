using System;
using System.Collections;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRoleCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> items.</summary>
	// Token: 0x02000097 RID: 151
	public class AdamRoleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x00002644 File Offset: 0x00000844
		internal AdamRoleCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> object to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		// Token: 0x17000166 RID: 358
		public AdamRole this[int index]
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return AdamRole.SchemaRole;
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item is in this collection; otherwise, false.</returns>
		/// <param name="role">The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item to search for in this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="role" /> parameter is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> value.</exception>
		// Token: 0x060004DA RID: 1242 RVA: 0x00004D54 File Offset: 0x00002F54
		public bool Contains(AdamRole role)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> items in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="roles">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> items that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="roles" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The destination array is not large enough to hold the required number of elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="roles" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter that is specified is out of range of the destination array.</exception>
		// Token: 0x060004DB RID: 1243 RVA: 0x00002644 File Offset: 0x00000844
		public void CopyTo(AdamRole[] roles, int index)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item in this collection.</summary>
		/// <returns>The zero-based index of the first matching item. -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item.</returns>
		/// <param name="role">The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> item to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="role" /> parameter is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> value.</exception>
		// Token: 0x060004DC RID: 1244 RVA: 0x00004D70 File Offset: 0x00002F70
		public int IndexOf(AdamRole role)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
