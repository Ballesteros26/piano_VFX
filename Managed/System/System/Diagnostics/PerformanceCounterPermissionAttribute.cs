using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive performance counter permission checks. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200020F RID: 527
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x0600113A RID: 4410 RVA: 0x0004B264 File Offset: 0x00049464
		public PerformanceCounterPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the performance counter category.</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0004B28A File Offset: 0x0004948A
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0004B292 File Offset: 0x00049492
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CategoryName");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the computer name for the performance counter.</summary>
		/// <returns>The server on which the category of the performance counter resides.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.MachineName" /> format is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0004B2A9 File Offset: 0x000494A9
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0004B2B1 File Offset: 0x000494B1
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
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004B2C0 File Offset: 0x000494C0
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0004B2C8 File Offset: 0x000494C8
		public PerformanceCounterPermissionAccess PermissionAccess
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

		/// <summary>Creates the permission based on the requested access levels that are set through the <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001141 RID: 4417 RVA: 0x0004B2D1 File Offset: 0x000494D1
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.permissionAccess, this.machineName, this.categoryName);
		}

		// Token: 0x040011A5 RID: 4517
		private string categoryName;

		// Token: 0x040011A6 RID: 4518
		private string machineName;

		// Token: 0x040011A7 RID: 4519
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
