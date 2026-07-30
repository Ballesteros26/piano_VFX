using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Principal.IdentityReference" /> objects and provides a means of converting sets of <see cref="T:System.Security.Principal.IdentityReference" />-derived objects to <see cref="T:System.Security.Principal.IdentityReference" />-derived types. </summary>
	// Token: 0x02000624 RID: 1572
	[ComVisible(false)]
	public class IdentityReferenceCollection : IEnumerable, ICollection<IdentityReference>, IEnumerable<IdentityReference>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class with zero items in the collection.</summary>
		// Token: 0x0600444C RID: 17484 RVA: 0x000EFEE0 File Offset: 0x000EE0E0
		public IdentityReferenceCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class by using the specified initial size.</summary>
		/// <param name="capacity">The initial number of items in the collection. The value of <paramref name="capacity" /> is a hint only; it is not necessarily the maximum number of items created.</param>
		// Token: 0x0600444D RID: 17485 RVA: 0x000EFEF3 File Offset: 0x000EE0F3
		public IdentityReferenceCollection(int capacity)
		{
			this._list = new ArrayList(capacity);
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Principal.IdentityReference" /> objects in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x000EFF07 File Offset: 0x000EE107
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is read-only.</returns>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Sets or gets the node at the specified index of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> at the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the collection, the return value is null.</returns>
		/// <param name="index">The zero-based index in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</param>
		// Token: 0x17000B70 RID: 2928
		public IdentityReference this[int index]
		{
			get
			{
				if (index >= this._list.Count)
				{
					return null;
				}
				return (IdentityReference)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Principal.IdentityReference" /> object to the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to add to the collection.</param>
		// Token: 0x06004452 RID: 17490 RVA: 0x000EFF46 File Offset: 0x000EE146
		public void Add(IdentityReference identity)
		{
			this._list.Add(identity);
		}

		/// <summary>Clears all <see cref="T:System.Security.Principal.IdentityReference" /> objects from the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		// Token: 0x06004453 RID: 17491 RVA: 0x000EFF55 File Offset: 0x000EE155
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection contains the specified <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>true if the collection contains the specified object.</returns>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to check for.</param>
		// Token: 0x06004454 RID: 17492 RVA: 0x000EFF64 File Offset: 0x000EE164
		public bool Contains(IdentityReference identity)
		{
			using (IEnumerator enumerator = this._list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IdentityReference)enumerator.Current).Equals(identity))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection to an <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array, starting at the specified index.</summary>
		/// <param name="array">An <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array object to which the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		// Token: 0x06004455 RID: 17493 RVA: 0x0002126B File Offset: 0x0001F46B
		public void CopyTo(IdentityReference[] array, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06004456 RID: 17494 RVA: 0x0002126B File Offset: 0x0001F46B
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06004457 RID: 17495 RVA: 0x0002126B File Offset: 0x0001F46B
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Principal.IdentityReference" /> object from the collection.</summary>
		/// <returns>true if the specified object was removed from the collection.</returns>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to remove.</param>
		// Token: 0x06004458 RID: 17496 RVA: 0x000EFFC4 File Offset: 0x000EE1C4
		public bool Remove(IdentityReference identity)
		{
			foreach (object obj in this._list)
			{
				IdentityReference identityReference = (IdentityReference)obj;
				if (identityReference.Equals(identity))
				{
					this._list.Remove(identityReference);
					return true;
				}
			}
			return false;
		}

		/// <summary>Converts the objects in the collection to the specified type. Calling this method is the same as calling <see cref="M:System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)" /> with the second parameter set to false, which means that exceptions will not be thrown for items that fail conversion.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004459 RID: 17497 RVA: 0x0002126B File Offset: 0x0001F46B
		public IdentityReferenceCollection Translate(Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the objects in the collection to the specified type and uses the specified fault tolerance to handle or ignore errors associated with a type not having a conversion mapping.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <param name="forceSuccess">A Boolean value that determines how conversion errors are handled.If <paramref name="forceSuccess" /> is true, conversion errors due to a mapping not being found for the translation result in a failed conversion and exceptions being thrown.If <paramref name="forceSuccess" /> is false, types that failed to convert due to a mapping not being found for the translation are copied without being converted into the collection being returned.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600445A RID: 17498 RVA: 0x0002126B File Offset: 0x0001F46B
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400226A RID: 8810
		private ArrayList _list;
	}
}
