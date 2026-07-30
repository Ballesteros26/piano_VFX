using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005B8 RID: 1464
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004125 RID: 16677 RVA: 0x000E2D08 File Offset: 0x000E0F08
		public StrongNameIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the name of the strong name identity.</summary>
		/// <returns>A name to compare against the name specified by the security provider.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x000E8020 File Offset: 0x000E6220
		// (set) Token: 0x06004127 RID: 16679 RVA: 0x000E8028 File Offset: 0x000E6228
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

		/// <summary>Gets or sets the public key value of the strong name identity expressed as a hexadecimal string.</summary>
		/// <returns>The public key value of the strong name identity expressed as a hexadecimal string.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x000E8031 File Offset: 0x000E6231
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x000E8039 File Offset: 0x000E6239
		public string PublicKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		/// <summary>Gets or sets the version of the strong name identity.</summary>
		/// <returns>The version number of the strong name identity.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x000E8042 File Offset: 0x000E6242
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x000E804A File Offset: 0x000E624A
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The method failed because the key is null.</exception>
		// Token: 0x0600412C RID: 16684 RVA: 0x000E8054 File Offset: 0x000E6254
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.name == null && this.key == null && this.version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.key == null)
			{
				throw new ArgumentException(Locale.GetText("PublicKey is required"));
			}
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = StrongNamePublicKeyBlob.FromString(this.key);
			Version version = null;
			if (this.version != null)
			{
				version = new Version(this.version);
			}
			return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.name, version);
		}

		// Token: 0x040020F5 RID: 8437
		private string name;

		// Token: 0x040020F6 RID: 8438
		private string key;

		// Token: 0x040020F7 RID: 8439
		private string version;
	}
}
