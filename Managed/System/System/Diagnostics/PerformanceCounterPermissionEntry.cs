using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for a <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000210 RID: 528
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The <see cref="P:System.Diagnostics.PerformanceCounterPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The server on which the category of the performance counter resides. </param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is null.-or-<paramref name="machineName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionAccess" /> is not a valid <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> value.-or-<paramref name="machineName" /> is not a valid computer name.</exception>
		// Token: 0x06001142 RID: 4418 RVA: 0x0004B2FC File Offset: 0x000494FC
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if ((permissionAccess | PerformanceCounterPermissionAccess.Administer) != PerformanceCounterPermissionAccess.Administer)
			{
				throw new ArgumentException("permissionAccess");
			}
			ResourcePermissionBase.ValidateMachineName(machineName);
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
			this.categoryName = categoryName;
		}

		/// <summary>Gets the name of the performance counter category (performance object).</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0004B357 File Offset: 0x00049557
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		/// <summary>Gets the name of the server on which the category of the performance counter resides.</summary>
		/// <returns>The name of the server on which the category resides.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0004B35F File Offset: 0x0004955F
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access level of the entry.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0004B367 File Offset: 0x00049567
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004B36F File Offset: 0x0004956F
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName, this.categoryName });
		}

		// Token: 0x040011A8 RID: 4520
		private const PerformanceCounterPermissionAccess All = PerformanceCounterPermissionAccess.Administer;

		// Token: 0x040011A9 RID: 4521
		private PerformanceCounterPermissionAccess permissionAccess;

		// Token: 0x040011AA RID: 4522
		private string machineName;

		// Token: 0x040011AB RID: 4523
		private string categoryName;
	}
}
