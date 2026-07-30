using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StorePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000378 RID: 888
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06001B36 RID: 6966 RVA: 0x0006CFD5 File Offset: 0x0006B1D5
		public StorePermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._flags = StorePermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the store permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.StorePermissionFlags.NoFlags" />.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0006CFE5 File Offset: 0x0006B1E5
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x0006CFED File Offset: 0x0006B1ED
		public StorePermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & StorePermissionFlags.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("Invalid flags {0}"), value), "StorePermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to add to a store.</summary>
		/// <returns>true if the ability to add to a store is allowed; otherwise, false.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0006D020 File Offset: 0x0006B220
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0006D02E File Offset: 0x0006B22E
		public bool AddToStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.AddToStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.AddToStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.AddToStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to create a store.</summary>
		/// <returns>true if the ability to create a store is allowed; otherwise, false.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0006D052 File Offset: 0x0006B252
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0006D05F File Offset: 0x0006B25F
		public bool CreateStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.CreateStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.CreateStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.CreateStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to delete a store.</summary>
		/// <returns>true if the ability to delete a store is allowed; otherwise, false.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0006D082 File Offset: 0x0006B282
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0006D08F File Offset: 0x0006B28F
		public bool DeleteStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.DeleteStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.DeleteStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.DeleteStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate the certificates in a store.</summary>
		/// <returns>true if the ability to enumerate certificates is allowed; otherwise, false.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0006D0B2 File Offset: 0x0006B2B2
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0006D0C3 File Offset: 0x0006B2C3
		public bool EnumerateCertificates
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateCertificates) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateCertificates;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateCertificates;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate stores.</summary>
		/// <returns>true if the ability to enumerate stores is allowed; otherwise, false.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0006D0ED File Offset: 0x0006B2ED
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0006D0FA File Offset: 0x0006B2FA
		public bool EnumerateStores
		{
			get
			{
				return (this._flags & StorePermissionFlags.EnumerateStores) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.EnumerateStores;
					return;
				}
				this._flags &= ~StorePermissionFlags.EnumerateStores;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to open a store.</summary>
		/// <returns>true if the ability to open a store is allowed; otherwise, false.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0006D11D File Offset: 0x0006B31D
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0006D12B File Offset: 0x0006B32B
		public bool OpenStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.OpenStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.OpenStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.OpenStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to remove a certificate from a store.</summary>
		/// <returns>true if the ability to remove a certificate from a store is allowed; otherwise, false.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0006D14F File Offset: 0x0006B34F
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0006D15D File Offset: 0x0006B35D
		public bool RemoveFromStore
		{
			get
			{
				return (this._flags & StorePermissionFlags.RemoveFromStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				if (value)
				{
					this._flags |= StorePermissionFlags.RemoveFromStore;
					return;
				}
				this._flags &= ~StorePermissionFlags.RemoveFromStore;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StorePermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StorePermission" /> that corresponds to the attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B47 RID: 6983 RVA: 0x0006D184 File Offset: 0x0006B384
		public override IPermission CreatePermission()
		{
			StorePermission storePermission;
			if (base.Unrestricted)
			{
				storePermission = new StorePermission(PermissionState.Unrestricted);
			}
			else
			{
				storePermission = new StorePermission(this._flags);
			}
			return storePermission;
		}

		// Token: 0x04001899 RID: 6297
		private StorePermissionFlags _flags;
	}
}
