using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F7 RID: 503
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x0600102A RID: 4138 RVA: 0x00049501 File Offset: 0x00047701
		public EventLogPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission entries.</summary>
		/// <param name="permissionAccessEntries">An array of  objects that represent permission entries. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x0600102B RID: 4139 RVA: 0x0004950F File Offset: 0x0004770F
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x0600102C RID: 4140 RVA: 0x00049543 File Offset: 0x00047743
		public EventLogPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the enumeration values that specifies an access level. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. </param>
		// Token: 0x0600102D RID: 4141 RVA: 0x00049552 File Offset: 0x00047752
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetUp();
			this.innerCollection = new EventLogPermissionEntryCollection(this);
			this.innerCollection.Add(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A collection that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0004957F File Offset: 0x0004777F
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004959B File Offset: 0x0004779B
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine" };
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000495C1 File Offset: 0x000477C1
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000495C9 File Offset: 0x000477C9
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000495D4 File Offset: 0x000477D4
		internal void Add(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.AddPermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000495F4 File Offset: 0x000477F4
		internal void Remove(object obj)
		{
			EventLogPermissionEntry eventLogPermissionEntry = obj as EventLogPermissionEntry;
			base.RemovePermissionAccess(eventLogPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x04001149 RID: 4425
		private EventLogPermissionEntryCollection innerCollection;
	}
}
