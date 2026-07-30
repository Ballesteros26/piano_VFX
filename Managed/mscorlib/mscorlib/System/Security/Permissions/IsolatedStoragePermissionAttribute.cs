using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x0200059B RID: 1435
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003FF7 RID: 16375 RVA: 0x000E2D08 File Offset: 0x000E0F08
		protected IsolatedStoragePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the level of isolated storage that should be declared.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000E4604 File Offset: 0x000E2804
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x000E460C File Offset: 0x000E280C
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.usage_allowed;
			}
			set
			{
				this.usage_allowed = value;
			}
		}

		/// <summary>Gets or sets the maximum user storage quota size.</summary>
		/// <returns>The maximum user storage quota size in bytes.</returns>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000E4615 File Offset: 0x000E2815
		// (set) Token: 0x06003FFB RID: 16379 RVA: 0x000E461D File Offset: 0x000E281D
		public long UserQuota
		{
			get
			{
				return this.user_quota;
			}
			set
			{
				this.user_quota = value;
			}
		}

		// Token: 0x04002079 RID: 8313
		private IsolatedStorageContainment usage_allowed;

		// Token: 0x0400207A RID: 8314
		private long user_quota;
	}
}
