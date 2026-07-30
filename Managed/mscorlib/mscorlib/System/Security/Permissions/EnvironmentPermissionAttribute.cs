using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.EnvironmentPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000588 RID: 1416
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityAction" />. </exception>
		// Token: 0x06003F5C RID: 16220 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public EnvironmentPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Sets full access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000E2D11 File Offset: 0x000E0F11
		// (set) Token: 0x06003F5E RID: 16222 RVA: 0x000E2D1D File Offset: 0x000E0F1D
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets read access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for read access.</returns>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x000E2D2D File Offset: 0x000E0F2D
		// (set) Token: 0x06003F60 RID: 16224 RVA: 0x000E2D35 File Offset: 0x000E0F35
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the environment variables specified by the string value.</summary>
		/// <returns>A list of environment variables for write access.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000E2D3E File Offset: 0x000E0F3E
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x000E2D46 File Offset: 0x000E0F46
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.EnvironmentPermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.EnvironmentPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003F63 RID: 16227 RVA: 0x000E2D50 File Offset: 0x000E0F50
		public override IPermission CreatePermission()
		{
			EnvironmentPermission environmentPermission;
			if (base.Unrestricted)
			{
				environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			}
			else
			{
				environmentPermission = new EnvironmentPermission(PermissionState.None);
				if (this.read != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Write, this.write);
				}
			}
			return environmentPermission;
		}

		// Token: 0x04002020 RID: 8224
		private string read;

		// Token: 0x04002021 RID: 8225
		private string write;
	}
}
