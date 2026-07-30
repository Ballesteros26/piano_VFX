using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteCollection" /> class is a read/write collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects.</summary>
	// Token: 0x02000041 RID: 65
	public class ActiveDirectorySiteCollection : CollectionBase
	{
		/// <summary>Gets or sets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The member already exists in this collection (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentNullException">The item specified is null (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store (applies to set only).</exception>
		// Token: 0x170000B0 RID: 176
		public ActiveDirectorySite this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Appends the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to this collection.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that is appended to this collection.</returns>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000271 RID: 625 RVA: 0x0000208C File Offset: 0x0000028C
		public int Add(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> array to this collection.</summary>
		/// <param name="sites">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> array that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sites" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000272 RID: 626 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySite[] sites)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteCollection" /> object to this collection. </summary>
		/// <param name="sites">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sites" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000273 RID: 627 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySiteCollection sites)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection; otherwise, false.</returns>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000274 RID: 628 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index of <paramref name="array" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.ArgumentException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x06000275 RID: 629 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySite[] array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object.</returns>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000276 RID: 630 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object into this collection at the specified index.</summary>
		/// <param name="index">The zero-based index in the collection where the object is inserted.</param>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to insert into this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000277 RID: 631 RVA: 0x0000208C File Offset: 0x0000028C
		public void Insert(int index, ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object.</summary>
		/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to remove from this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object does not exist in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000278 RID: 632 RVA: 0x0000208C File Offset: 0x0000028C
		public void Remove(ActiveDirectorySite site)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
		// Token: 0x06000279 RID: 633 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnClearComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsertComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index where the element was inserted.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x0600027A RID: 634 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnInsertComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index where the element was removed.</param>
		/// <param name="value">The element that was removed from the collection.</param>
		// Token: 0x0600027B RID: 635 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnRemoveComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index where the object was set.</param>
		/// <param name="oldValue">The element that was replaced by <paramref name="newValue" />.</param>
		/// <param name="newValue">The element that replaced <paramref name="oldValue" />.</param>
		// Token: 0x0600027C RID: 636 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnValidate(System.Object)" /> method.</summary>
		/// <param name="value">The element in this collection to validate.</param>
		// Token: 0x0600027D RID: 637 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnValidate(object value)
		{
			throw new NotImplementedException();
		}
	}
}
