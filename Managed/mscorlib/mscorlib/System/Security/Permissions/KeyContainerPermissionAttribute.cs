using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.KeyContainerPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005A0 RID: 1440
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06004030 RID: 16432 RVA: 0x000E4C4A File Offset: 0x000E2E4A
		public KeyContainerPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._spec = -1;
			this._type = -1;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x000E4C61 File Offset: 0x000E2E61
		// (set) Token: 0x06004032 RID: 16434 RVA: 0x000E4C69 File Offset: 0x000E2E69
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x000E4C72 File Offset: 0x000E2E72
		// (set) Token: 0x06004034 RID: 16436 RVA: 0x000E4C7A File Offset: 0x000E2E7A
		public string KeyContainerName
		{
			get
			{
				return this._containerName;
			}
			set
			{
				this._containerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file. </returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x000E4C83 File Offset: 0x000E2E83
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x000E4C8B File Offset: 0x000E2E8B
		public int KeySpec
		{
			get
			{
				return this._spec;
			}
			set
			{
				this._spec = value;
			}
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store. The default is "*".</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000E4C94 File Offset: 0x000E2E94
		// (set) Token: 0x06004038 RID: 16440 RVA: 0x000E4C9C File Offset: 0x000E2E9C
		public string KeyStore
		{
			get
			{
				return this._store;
			}
			set
			{
				this._store = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x000E4CA5 File Offset: 0x000E2EA5
		// (set) Token: 0x0600403A RID: 16442 RVA: 0x000E4CAD File Offset: 0x000E2EAD
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file. </returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x000E4CB6 File Offset: 0x000E2EB6
		// (set) Token: 0x0600403C RID: 16444 RVA: 0x000E4CBE File Offset: 0x000E2EBE
		public int ProviderType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x0600403D RID: 16445 RVA: 0x000E4CC8 File Offset: 0x000E2EC8
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (this.EmptyEntry())
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[]
			{
				new KeyContainerPermissionAccessEntry(this._store, this._providerName, this._type, this._containerName, this._spec, this._flags)
			};
			return new KeyContainerPermission(this._flags, array);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000E4D37 File Offset: 0x000E2F37
		private bool EmptyEntry()
		{
			return this._containerName == null && this._spec == 0 && this._store == null && this._providerName == null && this._type == 0;
		}

		// Token: 0x04002086 RID: 8326
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04002087 RID: 8327
		private string _containerName;

		// Token: 0x04002088 RID: 8328
		private int _spec;

		// Token: 0x04002089 RID: 8329
		private string _store;

		// Token: 0x0400208A RID: 8330
		private string _providerName;

		// Token: 0x0400208B RID: 8331
		private int _type;
	}
}
