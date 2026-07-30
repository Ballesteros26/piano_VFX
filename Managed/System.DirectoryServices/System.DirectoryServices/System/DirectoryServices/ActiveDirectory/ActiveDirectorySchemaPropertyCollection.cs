using System;
using System.Collections;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> class is a read/write collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects.</summary>
	// Token: 0x0200003E RID: 62
	public class ActiveDirectorySchemaPropertyCollection : CollectionBase
	{
		/// <summary>Gets or sets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object in this collection.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The member already exists in this collection (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentNullException">The item specified is null (applies to set only).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" />                                      object has not yet been saved to the Active Directory Domain Services store (applies to set only).</exception>
		// Token: 0x1700009F RID: 159
		public ActiveDirectorySchemaProperty this[int index]
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

		/// <summary>Appends the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to this collection.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that is appended to this collection.</returns>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x0600023C RID: 572 RVA: 0x0000208C File Offset: 0x0000028C
		public int Add(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> array to this collection.</summary>
		/// <param name="properties">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> array that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="properties" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x0600023D RID: 573 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySchemaProperty[] properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object to this collection.</summary>
		/// <param name="properties">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="properties" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x0600023E RID: 574 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ActiveDirectorySchemaPropertyCollection properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object to this collection.</summary>
		/// <param name="properties">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains the objects to append to this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="properties" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x0600023F RID: 575 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddRange(ReadOnlyActiveDirectorySchemaPropertyCollection properties)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to remove from this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object does not exist in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000240 RID: 576 RVA: 0x0000208C File Offset: 0x0000028C
		public void Remove(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object into this collection at the specified index.</summary>
		/// <param name="index">The zero-based index in this collection where the object is inserted.</param>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to insert into this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="site" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemaProperty" /> specified is out of range of the collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000241 RID: 577 RVA: 0x0000208C File Offset: 0x0000028C
		public void Insert(int index, ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is in this collection.</summary>
		/// <returns>true if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is in this collection; otherwise, false.</returns>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000242 RID: 578 RVA: 0x0000208C File Offset: 0x0000028C
		public bool Contains(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies all of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
		/// <param name="properties">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in <paramref name="properties" /> where this method starts copying this collection.</param>
		/// <exception cref="T:System.ArgumentException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="properties" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
		// Token: 0x06000243 RID: 579 RVA: 0x0000208C File Offset: 0x0000028C
		public void CopyTo(ActiveDirectorySchemaProperty[] properties, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object in this collection.</summary>
		/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <param name="schemaProperty">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to search for in this collection.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="schemaProperty" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		// Token: 0x06000244 RID: 580 RVA: 0x0000208C File Offset: 0x0000028C
		public int IndexOf(ActiveDirectorySchemaProperty schemaProperty)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
		// Token: 0x06000245 RID: 581 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnClearComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
		/// <param name="index">The zero-based index at which the element was inserted.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06000246 RID: 582 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnInsertComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which the element was removed.</param>
		/// <param name="value">The element that was removed from this collection.</param>
		// Token: 0x06000247 RID: 583 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnRemoveComplete(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which the set operation occurred.</param>
		/// <param name="oldValue">The element that was replaced by <paramref name="newValue" />.</param>
		/// <param name="newValue">The element that replaced <paramref name="oldValue" />.</param>
		// Token: 0x06000248 RID: 584 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnValidate(System.Object)" /> method.</summary>
		/// <param name="value">The element in this collection to validate.</param>
		// Token: 0x06000249 RID: 585 RVA: 0x0000208C File Offset: 0x0000028C
		protected override void OnValidate(object value)
		{
			throw new NotImplementedException();
		}
	}
}
