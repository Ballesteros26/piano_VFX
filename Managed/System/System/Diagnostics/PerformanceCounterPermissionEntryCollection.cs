using System;
using System.Collections;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000211 RID: 529
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06001147 RID: 4423 RVA: 0x0004B394 File Offset: 0x00049594
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner)
		{
			this.owner = owner;
			ResourcePermissionBaseEntry[] entries = owner.GetEntries();
			if (entries.Length != 0)
			{
				foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
				{
					PerformanceCounterPermissionAccess permissionAccess = (PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess;
					string text = resourcePermissionBaseEntry.PermissionAccessPath[0];
					string text2 = resourcePermissionBaseEntry.PermissionAccessPath[1];
					PerformanceCounterPermissionEntry performanceCounterPermissionEntry = new PerformanceCounterPermissionEntry(permissionAccess, text, text2);
					base.InnerList.Add(performanceCounterPermissionEntry);
				}
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004B400 File Offset: 0x00049600
		internal PerformanceCounterPermissionEntryCollection(ResourcePermissionBaseEntry[] entries)
		{
			foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in entries)
			{
				base.List.Add(new PerformanceCounterPermissionEntry((PerformanceCounterPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0], resourcePermissionBaseEntry.PermissionAccessPath[1]));
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index into the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037A RID: 890
		public PerformanceCounterPermissionEntry this[int index]
		{
			get
			{
				return (PerformanceCounterPermissionEntry)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> to this collection.</summary>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to add. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600114B RID: 4427 RVA: 0x00049742 File Offset: 0x00047942
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600114C RID: 4428 RVA: 0x0004B464 File Offset: 0x00049664
		public void AddRange(PerformanceCounterPermissionEntry[] value)
		{
			foreach (PerformanceCounterPermissionEntry performanceCounterPermissionEntry in value)
			{
				base.List.Add(performanceCounterPermissionEntry);
			}
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600114D RID: 4429 RVA: 0x0004B494 File Offset: 0x00049694
		public void AddRange(PerformanceCounterPermissionEntryCollection value)
		{
			foreach (object obj in value)
			{
				PerformanceCounterPermissionEntry performanceCounterPermissionEntry = (PerformanceCounterPermissionEntry)obj;
				base.List.Add(performanceCounterPermissionEntry);
			}
		}

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object belongs to this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600114E RID: 4430 RVA: 0x000497DC File Offset: 0x000479DC
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> that receives this collection's permission entries. </param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600114F RID: 4431 RVA: 0x000497EA File Offset: 0x000479EA
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		/// <param name="value">The permission entry for which to search. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001150 RID: 4432 RVA: 0x000497F9 File Offset: 0x000479F9
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry. </param>
		/// <param name="value">The permission entry to insert into this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001151 RID: 4433 RVA: 0x00049807 File Offset: 0x00047A07
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06001152 RID: 4434 RVA: 0x0004B4F0 File Offset: 0x000496F0
		protected override void OnClear()
		{
			this.owner.ClearEntries();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />. </param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x06001153 RID: 4435 RVA: 0x0004B4FD File Offset: 0x000496FD
		protected override void OnInsert(int index, object value)
		{
			this.owner.Add(value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found. </param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />. </param>
		// Token: 0x06001154 RID: 4436 RVA: 0x0004B50B File Offset: 0x0004970B
		protected override void OnRemove(int index, object value)
		{
			this.owner.Remove(value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found. </param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />. </param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />. </param>
		// Token: 0x06001155 RID: 4437 RVA: 0x0004B519 File Offset: 0x00049719
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.Remove(oldValue);
			this.owner.Add(newValue);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001156 RID: 4438 RVA: 0x00049859 File Offset: 0x00047A59
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal PerformanceCounterPermissionEntryCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040011AC RID: 4524
		private PerformanceCounterPermission owner;
	}
}
