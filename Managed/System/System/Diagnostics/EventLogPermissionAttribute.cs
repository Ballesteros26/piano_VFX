using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive permission checks for event logging. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F9 RID: 505
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06001034 RID: 4148 RVA: 0x00049614 File Offset: 0x00047814
		public EventLogPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the computer on which events might be read.</summary>
		/// <returns>The name of the computer on which events might be read. The default is ".".</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00049630 File Offset: 0x00047830
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00049638 File Offset: 0x00047838
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				ResourcePermissionBase.ValidateMachineName(value);
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00049647 File Offset: 0x00047847
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x0004964F File Offset: 0x0004784F
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.MachineName" /> property and the requested access levels that are set through the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001039 RID: 4153 RVA: 0x00049658 File Offset: 0x00047858
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.permissionAccess, this.machineName);
		}

		// Token: 0x04001151 RID: 4433
		private string machineName;

		// Token: 0x04001152 RID: 4434
		private EventLogPermissionAccess permissionAccess;
	}
}
