using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020D RID: 525
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class.</summary>
		// Token: 0x06001130 RID: 4400 RVA: 0x0004B158 File Offset: 0x00049358
		public PerformanceCounterPermission()
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.PerformanceCounterPermission.PermissionEntries" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is null.</exception>
		// Token: 0x06001131 RID: 4401 RVA: 0x0004B166 File Offset: 0x00049366
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.AddRange(permissionAccessEntries);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001132 RID: 4402 RVA: 0x0004B19A File Offset: 0x0004939A
		public PerformanceCounterPermission(PermissionState state)
			: base(state)
		{
			this.SetUp();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified access levels, the name of the computer to use, and the category associated with the performance counter.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. </param>
		/// <param name="machineName">The server on which the performance counter and its associate category reside. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which the performance counter is associated. </param>
		// Token: 0x06001133 RID: 4403 RVA: 0x0004B1A9 File Offset: 0x000493A9
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetUp();
			this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
			this.innerCollection.Add(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0004B1D7 File Offset: 0x000493D7
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this);
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004B1F3 File Offset: 0x000493F3
		private void SetUp()
		{
			base.TagNames = new string[] { "Machine", "Category" };
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000495C1 File Offset: 0x000477C1
		internal ResourcePermissionBaseEntry[] GetEntries()
		{
			return base.GetPermissionEntries();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000495C9 File Offset: 0x000477C9
		internal void ClearEntries()
		{
			base.Clear();
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004B224 File Offset: 0x00049424
		internal void Add(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.AddPermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004B244 File Offset: 0x00049444
		internal void Remove(object obj)
		{
			PerformanceCounterPermissionEntry performanceCounterPermissionEntry = obj as PerformanceCounterPermissionEntry;
			base.RemovePermissionAccess(performanceCounterPermissionEntry.CreateResourcePermissionBaseEntry());
		}

		// Token: 0x0400119D RID: 4509
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
