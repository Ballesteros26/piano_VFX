using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.DataProtectionPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class DataProtectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06000028 RID: 40 RVA: 0x00002AEA File Offset: 0x00000CEA
		public DataProtectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the data protection permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.DataProtectionPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002AF3 File Offset: 0x00000CF3
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002AFB File Offset: 0x00000CFB
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & DataProtectionPermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>true if data can be encrypted; otherwise, false.  </returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002B2B File Offset: 0x00000D2B
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002B38 File Offset: 0x00000D38
		public bool ProtectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectData;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.ProtectData;
			}
		}

		/// <summary>Gets or sets a value indicating whether data can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedData" /> class.</summary>
		/// <returns>true if data can be unencrypted; otherwise, false.  </returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002B5B File Offset: 0x00000D5B
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002B68 File Offset: 0x00000D68
		public bool UnprotectData
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectData) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectData;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.UnprotectData;
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be encrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>true if memory can be encrypted; otherwise, false.  </returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002B8B File Offset: 0x00000D8B
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002B98 File Offset: 0x00000D98
		public bool ProtectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.ProtectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.ProtectMemory;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.ProtectMemory;
			}
		}

		/// <summary>Gets or sets a value indicating whether memory can be unencrypted using the <see cref="T:System.Security.Cryptography.ProtectedMemory" /> class.</summary>
		/// <returns>true if memory can be unencrypted; otherwise, false.  </returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BBB File Offset: 0x00000DBB
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public bool UnprotectMemory
		{
			get
			{
				return (this._flags & DataProtectionPermissionFlags.UnprotectMemory) > DataProtectionPermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= DataProtectionPermissionFlags.UnprotectMemory;
					return;
				}
				this._flags &= ~DataProtectionPermissionFlags.UnprotectMemory;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.DataProtectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.DataProtectionPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06000033 RID: 51 RVA: 0x00002BEC File Offset: 0x00000DEC
		public override IPermission CreatePermission()
		{
			DataProtectionPermission dataProtectionPermission;
			if (base.Unrestricted)
			{
				dataProtectionPermission = new DataProtectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				dataProtectionPermission = new DataProtectionPermission(this._flags);
			}
			return dataProtectionPermission;
		}

		// Token: 0x04000090 RID: 144
		private DataProtectionPermissionFlags _flags;
	}
}
