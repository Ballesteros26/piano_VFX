using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>Contains a strongly-typed collection of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects.          </summary>
	// Token: 0x0200001B RID: 27
	[MonoTODO("Fix serialization compatibility with MS.NET")]
	[Serializable]
	public class DirectoryServicesPermissionEntryCollection : CollectionBase
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00003E78 File Offset: 0x00002078
		internal DirectoryServicesPermissionEntryCollection(DirectoryServicesPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					DirectoryServicesPermissionEntry directoryServicesPermissionEntry = new DirectoryServicesPermissionEntry((DirectoryServicesPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
					base.InnerList.Add(directoryServicesPermissionEntry);
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object in this collection.          </summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to get or set.</param>
		// Token: 0x17000046 RID: 70
		public DirectoryServicesPermissionEntry this[int index]
		{
			get
			{
				return base.List[index] as DirectoryServicesPermissionEntry;
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to this collection.          </summary>
		/// <returns>The zero-based index of the added <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object that is appended to this collection.</returns>
		/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to add to this collection.</param>
		// Token: 0x060000ED RID: 237 RVA: 0x00003EF6 File Offset: 0x000020F6
		public int Add(DirectoryServicesPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> array to this collection.          </summary>
		/// <param name="value">
		///   <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> array that contains the objects to append to this collection.</param>
		// Token: 0x060000EE RID: 238 RVA: 0x00003F04 File Offset: 0x00002104
		public void AddRange(DirectoryServicesPermissionEntry[] value)
		{
			foreach (DirectoryServicesPermissionEntry directoryServicesPermissionEntry in value)
			{
				this.Add(directoryServicesPermissionEntry);
			}
		}

		/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object to this collection.          </summary>
		/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object that contains the objects to append to this collection.</param>
		// Token: 0x060000EF RID: 239 RVA: 0x00003F30 File Offset: 0x00002130
		public void AddRange(DirectoryServicesPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				DirectoryServicesPermissionEntry directoryServicesPermissionEntry = (DirectoryServicesPermissionEntry)obj;
				this.Add(directoryServicesPermissionEntry);
			}
		}

		/// <summary>Copies all <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects in this collection to the specified array, starting at the specified index in the target array.          </summary>
		/// <param name="array">The array of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects that receives the elements of this collection.</param>
		/// <param name="index">The zero-based index in the array where this method starts copying this collection.</param>
		// Token: 0x060000F0 RID: 240 RVA: 0x00003F88 File Offset: 0x00002188
		public void CopyTo(DirectoryServicesPermissionEntry[] array, int index)
		{
			foreach (object obj in base.List)
			{
				DirectoryServicesPermissionEntry directoryServicesPermissionEntry = (DirectoryServicesPermissionEntry)obj;
				array[index++] = directoryServicesPermissionEntry;
			}
		}

		/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object is in this collection.          </summary>
		/// <returns>true if the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object is in this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to search for in this collection.</param>
		// Token: 0x060000F1 RID: 241 RVA: 0x00003FE4 File Offset: 0x000021E4
		public bool Contains(DirectoryServicesPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object in this collection.          </summary>
		/// <returns>The zero-based index of the first matching object.  Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object.</returns>
		/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to search for in this collection.</param>
		// Token: 0x060000F2 RID: 242 RVA: 0x00003FF2 File Offset: 0x000021F2
		public int IndexOf(DirectoryServicesPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> into this collection at the specified index.          </summary>
		/// <param name="index">The zero-based index in this collection where the object is inserted.</param>
		/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to insert into this collection.</param>
		// Token: 0x060000F3 RID: 243 RVA: 0x00004000 File Offset: 0x00002200
		public void Insert(int index, DirectoryServicesPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object.          </summary>
		/// <param name="value">The specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to remove from this collection.</param>
		// Token: 0x060000F4 RID: 244 RVA: 0x0000400F File Offset: 0x0000220F
		public void Remove(DirectoryServicesPermissionEntry value)
		{
			base.List.Remove(value);
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClear" /> method.         </summary>
		// Token: 0x060000F5 RID: 245 RVA: 0x0000401D File Offset: 0x0000221D
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsert(System.Int32,System.Object)" /> method.          </summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060000F6 RID: 246 RVA: 0x0000402A File Offset: 0x0000222A
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemove(System.Int32,System.Object)" /> method.          </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x00004038 File Offset: 0x00002238
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSet(System.Int32,System.Object,System.Object)" /> method.          </summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x060000F8 RID: 248 RVA: 0x00004046 File Offset: 0x00002246
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002644 File Offset: 0x00000844
		internal DirectoryServicesPermissionEntryCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000087 RID: 135
		private DirectoryServicesPermission owner;
	}
}
