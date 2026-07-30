using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FB RID: 507
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x000496C4 File Offset: 0x000478C4
		internal EventLogPermissionEntryCollection(EventLogPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					EventLogPermissionEntry eventLogPermissionEntry = new EventLogPermissionEntry((EventLogPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
					base.InnerList.Add(eventLogPermissionEntry);
				}
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032E RID: 814
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to this collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to add. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001041 RID: 4161 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001042 RID: 4162 RVA: 0x00049750 File Offset: 0x00047950
		public void AddRange(EventLogPermissionEntry[] value)
		{
			foreach (EventLogPermissionEntry eventLogPermissionEntry in value)
			{
				base.List.Add(eventLogPermissionEntry);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001043 RID: 4163 RVA: 0x00049780 File Offset: 0x00047980
		public void AddRange(EventLogPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				EventLogPermissionEntry eventLogPermissionEntry = (EventLogPermissionEntry)obj;
				base.List.Add(eventLogPermissionEntry);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001044 RID: 4164 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that receives this collection's permission entries. </param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001045 RID: 4165 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		/// <param name="value">The permission entry to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001046 RID: 4166 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry. </param>
		/// <param name="value">The permission entry to insert into this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001047 RID: 4167 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06001048 RID: 4168 RVA: 0x00049816 File Offset: 0x00047A16
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x06001049 RID: 4169 RVA: 0x00049823 File Offset: 0x00047A23
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />. </param>
		// Token: 0x0600104A RID: 4170 RVA: 0x00049831 File Offset: 0x00047A31
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x0600104B RID: 4171 RVA: 0x0004983F File Offset: 0x00047A3F
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600104C RID: 4172 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal EventLogPermissionEntryCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001155 RID: 4437
		private EventLogPermission owner;
	}
}
