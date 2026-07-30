using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PrincipalPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005A6 RID: 1446
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004064 RID: 16484 RVA: 0x000E5925 File Offset: 0x000E3B25
		public PrincipalPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.authenticated = true;
		}

		/// <summary>Gets or sets a value indicating whether the current principal has been authenticated by the underlying role-based security provider.</summary>
		/// <returns>true if the current principal has been authenticated; otherwise, false.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x000E5935 File Offset: 0x000E3B35
		// (set) Token: 0x06004066 RID: 16486 RVA: 0x000E593D File Offset: 0x000E3B3D
		public bool Authenticated
		{
			get
			{
				return this.authenticated;
			}
			set
			{
				this.authenticated = value;
			}
		}

		/// <summary>Gets or sets the name of the identity associated with the current principal.</summary>
		/// <returns>A name to match against that provided by the underlying role-based security provider.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000E5946 File Offset: 0x000E3B46
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x000E594E File Offset: 0x000E3B4E
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets membership in a specified security role.</summary>
		/// <returns>The name of a role from the underlying role-based security provider.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000E5957 File Offset: 0x000E3B57
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x000E595F File Offset: 0x000E3B5F
		public string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PrincipalPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600406B RID: 16491 RVA: 0x000E5968 File Offset: 0x000E3B68
		public override IPermission CreatePermission()
		{
			PrincipalPermission principalPermission;
			if (base.Unrestricted)
			{
				principalPermission = new PrincipalPermission(PermissionState.Unrestricted);
			}
			else
			{
				principalPermission = new PrincipalPermission(this.name, this.role, this.authenticated);
			}
			return principalPermission;
		}

		// Token: 0x040020A5 RID: 8357
		private bool authenticated;

		// Token: 0x040020A6 RID: 8358
		private string name;

		// Token: 0x040020A7 RID: 8359
		private string role;
	}
}
