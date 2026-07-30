using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005BF RID: 1471
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004159 RID: 16729 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public UrlIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the full URL of the calling code.</summary>
		/// <returns>The URL to match with the URL specified by the host.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x000E8948 File Offset: 0x000E6B48
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x000E8950 File Offset: 0x000E6B50
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UrlIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600415C RID: 16732 RVA: 0x000E8959 File Offset: 0x000E6B59
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		// Token: 0x04002109 RID: 8457
		private string url;
	}
}
