using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of groups within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022C RID: 556
	[ListBindable(false)]
	public class ListViewGroupCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x0008931C File Offset: 0x0008751C
		private ListViewGroupCollection()
		{
			this.list = new List<ListViewGroup>();
			this.default_group = new ListViewGroup("Default Group");
			this.default_group.IsDefault = true;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0008934C File Offset: 0x0008754C
		internal ListViewGroupCollection(ListView listViewOwner)
			: this()
		{
			this.list_view_owner = listViewOwner;
			this.default_group.ListViewOwner = listViewOwner;
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x00089368 File Offset: 0x00087568
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize the collection.</returns>
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x0008936C File Offset: 0x0008756C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <returns>The index at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ListViewGroup" />.-or-<paramref name="value" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		// Token: 0x06002458 RID: 9304 RVA: 0x00089370 File Offset: 0x00087570
		int IList.Add(object value)
		{
			if (!(value is ListViewGroup))
			{
				throw new ArgumentException("value");
			}
			return this.Add((ListViewGroup)value);
		}

		/// <summary>Determines whether the specified value is located in the collection.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ListViewGroup" /> contained in the collection; otherwise, false.</returns>
		/// <param name="value">An object that represents the <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection.</param>
		// Token: 0x06002459 RID: 9305 RVA: 0x000893A0 File Offset: 0x000875A0
		bool IList.Contains(object value)
		{
			return value is ListViewGroup && this.Contains((ListViewGroup)value);
		}

		/// <summary>Returns the index within the collection of the specified value.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to find in the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		// Token: 0x0600245A RID: 9306 RVA: 0x000893BC File Offset: 0x000875BC
		int IList.IndexOf(object value)
		{
			if (value is ListViewGroup)
			{
				return this.IndexOf((ListViewGroup)value);
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.ListViewGroup" /> into the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="index">The position at which the <see cref="T:System.Windows.Forms.ListViewGroup" /> is added to the collection.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection.</param>
		// Token: 0x0600245B RID: 9307 RVA: 0x000893D8 File Offset: 0x000875D8
		void IList.Insert(int index, object value)
		{
			if (value is ListViewGroup)
			{
				this.Insert(index, (ListViewGroup)value);
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000893F4 File Offset: 0x000875F4
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x000893F8 File Offset: 0x000875F8
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</param>
		// Token: 0x0600245E RID: 9310 RVA: 0x000893FC File Offset: 0x000875FC
		void IList.Remove(object value)
		{
			this.Remove((ListViewGroup)value);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroup" /> that represents the item located at the specified index within the collection.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0008940C File Offset: 0x0008760C
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00089418 File Offset: 0x00087618
		object IList.Item
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is ListViewGroup)
				{
					this[index] = (ListViewGroup)value;
				}
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00089434 File Offset: 0x00087634
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x0008943C File Offset: 0x0008763C
		internal ListView ListViewOwner
		{
			get
			{
				return this.list_view_owner;
			}
			set
			{
				this.list_view_owner = value;
			}
		}

		/// <summary>Returns an enumerator used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002463 RID: 9315 RVA: 0x00089448 File Offset: 0x00087648
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Copies the groups in the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to which the groups are copied. </param>
		/// <param name="index">The first index within the array to which the groups are copied. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002464 RID: 9316 RVA: 0x0008945C File Offset: 0x0008765C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Gets the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x0008946C File Offset: 0x0008766C
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection.</summary>
		/// <returns>The index of the group within the collection, or -1 if the group is already present in the collection.</returns>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="group" /> contains at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002466 RID: 9318 RVA: 0x0008947C File Offset: 0x0008767C
		public int Add(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return -1;
			}
			this.AddGroup(group);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
			return this.list.Count - 1;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.ListViewGroup" /> to the collection using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties </summary>
		/// <returns>The new <see cref="T:System.Windows.Forms.ListViewGroup" />.</returns>
		/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property for the new group.</param>
		/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property for the new group.</param>
		// Token: 0x06002467 RID: 9319 RVA: 0x000894C4 File Offset: 0x000876C4
		public ListViewGroup Add(string key, string headerText)
		{
			ListViewGroup listViewGroup = new ListViewGroup(key, headerText);
			this.Add(listViewGroup);
			return listViewGroup;
		}

		/// <summary>Removes all groups from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002468 RID: 9320 RVA: 0x000894E4 File Offset: 0x000876E4
		public void Clear()
		{
			foreach (ListViewGroup listViewGroup in this.list)
			{
				listViewGroup.ListViewOwner = null;
			}
			this.list.Clear();
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Determines whether the specified group is located in the collection.</summary>
		/// <returns>true if the group is in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002469 RID: 9321 RVA: 0x0008956C File Offset: 0x0008776C
		public bool Contains(ListViewGroup value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> within the collection.</summary>
		/// <returns>The zero-based index of the group within the collection, or -1 if the group is not in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to locate in the collection. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600246A RID: 9322 RVA: 0x0008957C File Offset: 0x0008777C
		public int IndexOf(ListViewGroup value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> into the collection at the specified index.</summary>
		/// <param name="index">The index within the collection at which to insert the group. </param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to insert into the collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246B RID: 9323 RVA: 0x0008958C File Offset: 0x0008778C
		public void Insert(int index, ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			this.CheckListViewItemsInGroup(group);
			group.ListViewOwner = this.list_view_owner;
			this.list.Insert(index, group);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ListViewGroup" /> from the collection.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove from the collection. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246C RID: 9324 RVA: 0x000895E0 File Offset: 0x000877E0
		public void Remove(ListViewGroup group)
		{
			int num = this.list.IndexOf(group);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600246D RID: 9325 RVA: 0x00089608 File Offset: 0x00087808
		public void RemoveAt(int index)
		{
			if (this.list.Count <= index || index < 0)
			{
				return;
			}
			ListViewGroup listViewGroup = this.list[index];
			listViewGroup.ListViewOwner = null;
			this.list.RemoveAt(index);
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> at the specified index within the collection.</returns>
		/// <param name="index">The index within the collection of the <see cref="T:System.Windows.Forms.ListViewGroup" /> to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ListViewGroupCollection.Count" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008F2 RID: 2290
		public ListViewGroup this[int index]
		{
			get
			{
				if (this.list.Count <= index || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.list[index];
			}
			set
			{
				if (this.list.Count <= index || index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.Contains(value))
				{
					return;
				}
				if (value != null)
				{
					this.CheckListViewItemsInGroup(value);
				}
				this.list[index] = value;
				if (this.list_view_owner != null)
				{
					this.list_view_owner.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property value. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> with the specified name, or null if no such <see cref="T:System.Windows.Forms.ListViewGroup" /> exists.</returns>
		/// <param name="key">The name of the group to get or set.</param>
		// Token: 0x170008F3 RID: 2291
		public ListViewGroup this[string key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num != -1)
				{
					return this[num];
				}
				return null;
			}
			set
			{
				int num = this.IndexOfKey(key);
				if (num == -1)
				{
					return;
				}
				this[num] = value;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0008975C File Offset: 0x0008795C
		private int IndexOfKey(string key)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].Name == key)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Adds an array of groups to the collection.</summary>
		/// <param name="groups">An array of type <see cref="T:System.Windows.Forms.ListViewGroup" /> that specifies the groups to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002473 RID: 9331 RVA: 0x000897A4 File Offset: 0x000879A4
		public void AddRange(ListViewGroup[] groups)
		{
			foreach (ListViewGroup listViewGroup in groups)
			{
				this.AddGroup(listViewGroup);
			}
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		/// <summary>Adds the groups in an existing <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> to the collection.</summary>
		/// <param name="groups">A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> containing the groups to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="groups" /> contains at least one group with at least one <see cref="T:System.Windows.Forms.ListViewItem" /> that belongs to a <see cref="T:System.Windows.Forms.ListView" /> control other than the one that owns this <see cref="T:System.Windows.Forms.ListViewGroupCollection" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002474 RID: 9332 RVA: 0x000897EC File Offset: 0x000879EC
		public void AddRange(ListViewGroupCollection groups)
		{
			foreach (object obj in groups)
			{
				ListViewGroup listViewGroup = (ListViewGroup)obj;
				this.AddGroup(listViewGroup);
			}
			if (this.list_view_owner != null)
			{
				this.list_view_owner.Redraw(true);
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00089870 File Offset: 0x00087A70
		internal ListViewGroup GetInternalGroup(int index)
		{
			if (index == 0)
			{
				return this.default_group;
			}
			return this.list[index - 1];
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x00089890 File Offset: 0x00087A90
		internal int InternalCount
		{
			get
			{
				return this.list.Count + 1;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000898A0 File Offset: 0x00087AA0
		internal ListViewGroup DefaultGroup
		{
			get
			{
				return this.default_group;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000898A8 File Offset: 0x00087AA8
		private void AddGroup(ListViewGroup group)
		{
			if (this.Contains(group))
			{
				return;
			}
			this.CheckListViewItemsInGroup(group);
			group.ListViewOwner = this.list_view_owner;
			this.list.Add(group);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000898E4 File Offset: 0x00087AE4
		private void CheckListViewItemsInGroup(ListViewGroup value)
		{
			foreach (object obj in value.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView != null && listViewItem.ListView != this.list_view_owner)
				{
					throw new ArgumentException("ListViewItem belongs to a ListView control other than the one that owns this ListViewGroupCollection.", "ListViewGroup");
				}
			}
		}

		// Token: 0x04001298 RID: 4760
		private List<ListViewGroup> list;

		// Token: 0x04001299 RID: 4761
		private ListView list_view_owner;

		// Token: 0x0400129A RID: 4762
		private ListViewGroup default_group;
	}
}
