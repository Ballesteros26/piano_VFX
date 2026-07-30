using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FA RID: 506
	[Serializable]
	public class EventLogPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="machineName">The name of the computer on which to read or write events. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.MachineName" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		// Token: 0x0600103A RID: 4154 RVA: 0x0004967A File Offset: 0x0004787A
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			ResourcePermissionBase.ValidateMachineName(machineName);
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		/// <summary>Gets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the computer on which to read or write events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00049696 File Offset: 0x00047896
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0004969E File Offset: 0x0004789E
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000496A6 File Offset: 0x000478A6
		internal ResourcePermissionBaseEntry CreateResourcePermissionBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.permissionAccess, new string[] { this.machineName });
		}

		// Token: 0x04001153 RID: 4435
		private EventLogPermissionAccess permissionAccess;

		// Token: 0x04001154 RID: 4436
		private string machineName;
	}
}
