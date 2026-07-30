using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited. </summary>
	// Token: 0x020005C1 RID: 1473
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004169 RID: 16745 RVA: 0x000E8B8B File Offset: 0x000E6D8B
		public ZoneIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.zone = SecurityZone.NoZone;
		}

		/// <summary>Gets or sets membership in the content zone specified by the property value.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x000E8B9B File Offset: 0x000E6D9B
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x000E8BA3 File Offset: 0x000E6DA3
		public SecurityZone Zone
		{
			get
			{
				return this.zone;
			}
			set
			{
				this.zone = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600416C RID: 16748 RVA: 0x000E8BAC File Offset: 0x000E6DAC
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x0400210C RID: 8460
		private SecurityZone zone;
	}
}
