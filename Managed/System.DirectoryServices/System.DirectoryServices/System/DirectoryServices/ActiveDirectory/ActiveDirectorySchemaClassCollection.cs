using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> class is a read/write collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects.</summary>
	// Token: 0x0200003C RID: 60
	public class ActiveDirectorySchemaClassCollection : CollectionBase
	{
		/// <summary>Gets or sets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The member already exists in this collection (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentNullException">The item specified is null (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> specified has not yet been saved to the Active Directory Domain Services store (applies to set only).</exception>
		// Token: 0x1700008C RID: 140
		public ActiveDirectorySchemaClass this[int index]
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

		/// <summary>Appends the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to this collection.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that is appended to this collection.</returns>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClass" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000200 RID: 512 RVA: 0x0000208C File Offset: 0x0000028C
		public int Add(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> array to this collection.</summary>
		/// <param name="schemaClasses">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> array that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClasses" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved to the underlying directory store.</exception>
		// Token: 0x06000201 RID: 513 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySchemaClass[] schemaClasses)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object to this collection.</summary>
		/// <param name="schemaClasses">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClasses" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved to the underlying directory store.</exception>
		// Token: 0x06000202 RID: 514 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySchemaClassCollection schemaClasses)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object to this collection.</summary>
		/// <param name="schemaClasses">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClasses" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000203 RID: 515 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ReadOnlyActiveDirectorySchemaClassCollection schemaClasses)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to remove from this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object does not exist in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClass" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000204 RID: 516 RVA: 0x0000208C File Offset: 0x0000028C
		public void Remove(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object into this collection at the specified index.</summary>
		/// <param name="index">The zero-based index in this collection where the object is inserted.</param>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to insert into this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClass" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000205 RID: 517 RVA: 0x0000208C File Offset: 0x0000028C
		public void Insert(int index, ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is in this collection; otherwise, false.</returns>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClass" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000206 RID: 518 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="schemaClasses">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="schemaClasses" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.ArgumentException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClasses" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x06000207 RID: 519 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySchemaClass[] schemaClasses, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</returns>
		/// <param name="schemaClass">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaClass" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000208 RID: 520 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySchemaClass schemaClass)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
		// Token: 0x06000209 RID: 521 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnClearComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsertComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which the element was inserted.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x0600020A RID: 522 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnInsertComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which the element was removed.</param>
		/// <param name="value">The element that was removed from this collection.</param>
		// Token: 0x0600020B RID: 523 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnRemoveComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which the set operation occurred.</param>
		/// <param name="oldValue">The element that was replaced by <paramref name="newValue" />.</param>
		/// <param name="newValue">The element that replaced <paramref name="oldValue" />.</param>
		// Token: 0x0600020C RID: 524 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnValidate(System.Object)" /> method.</summary>
		/// <param name="value">The element to validate in this collection.</param>
		// Token: 0x0600020D RID: 525 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnValidate(object value)
		{
			throw new NotImplementedException();
		}
	}
}
