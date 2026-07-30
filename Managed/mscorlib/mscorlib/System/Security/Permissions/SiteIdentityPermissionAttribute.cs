using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005B5 RID: 1461
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004103 RID: 16643 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public SiteIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the site name of the calling code.</summary>
		/// <returns>The site name to compare against the site name specified by the security provider.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000E74F0 File Offset: 0x000E56F0
		// (set) Token: 0x06004105 RID: 16645 RVA: 0x000E74F8 File Offset: 0x000E56F8
		public string Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.SiteIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06004106 RID: 16646 RVA: 0x000E7504 File Offset: 0x000E5704
		public override IPermission CreatePermission()
		{
			SiteIdentityPermission siteIdentityPermission;
			if (base.Unrestricted)
			{
				siteIdentityPermission = new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			else if (this.site == null)
			{
				siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			}
			else
			{
				siteIdentityPermission = new SiteIdentityPermission(this.site);
			}
			return siteIdentityPermission;
		}

		// Token: 0x040020ED RID: 8429
		private string site;
	}
}
