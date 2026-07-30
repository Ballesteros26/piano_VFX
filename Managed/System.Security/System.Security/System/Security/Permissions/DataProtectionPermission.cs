using System;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access encrypted data and memory. This class cannot be inherited. </summary>
	// Token: 0x0200000F RID: 15
	[Serializable]
	public sealed class DataProtectionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission state. </summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value. </exception>
		// Token: 0x0600001C RID: 28 RVA: 0x000028F9 File Offset: 0x00000AF9
		public DataProtectionPermission(PermissionState state)
		{
			if (PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = DataProtectionPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.DataProtectionPermission" /> class with the specified permission flags. </summary>
		/// <param name="flag">A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. </exception>
		// Token: 0x0600001D RID: 29 RVA: 0x00002913 File Offset: 0x00000B13
		public DataProtectionPermission(DataProtectionPermissionFlags flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the data and memory protection flags.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value is not a valid combination of the <see cref="T:System.Security.Permissions.DataProtectionPermissionFlags" /> values. </exception>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002922 File Offset: 0x00000B22
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000292A File Offset: 0x00000B2A
		public DataProtectionPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & ~(DataProtectionPermissionFlags.ProtectData | DataProtectionPermissionFlags.UnprotectData | DataProtectionPermissionFlags.ProtectMemory | DataProtectionPermissionFlags.UnprotectMemory)) != DataProtectionPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "DataProtectionPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06000020 RID: 32 RVA: 0x00002959 File Offset: 0x00000B59
		public bool IsUnrestricted()
		{
			return this._flags == DataProtectionPermissionFlags.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06000021 RID: 33 RVA: 0x00002965 File Offset: 0x00000B65
		public override IPermission Copy()
		{
			return new DataProtectionPermission(this._flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06000022 RID: 34 RVA: 0x00002974 File Offset: 0x00000B74
		public override IPermission Intersect(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && dataProtectionPermission.IsUnrestricted())
			{
				return new DataProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return dataProtectionPermission.Copy();
			}
			if (dataProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new DataProtectionPermission(this._flags & dataProtectionPermission._flags);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06000023 RID: 35 RVA: 0x000029D8 File Offset: 0x00000BD8
		public override IPermission Union(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || dataProtectionPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new DataProtectionPermission(this._flags | dataProtectionPermission._flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null and does not specify a permission of the same type as the current permission. </exception>
		// Token: 0x06000024 RID: 36 RVA: 0x00002A20 File Offset: 0x00000C20
		public override bool IsSubsetOf(IPermission target)
		{
			DataProtectionPermission dataProtectionPermission = this.Cast(target);
			if (dataProtectionPermission == null)
			{
				return this._flags == DataProtectionPermissionFlags.NoFlags;
			}
			return dataProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._flags & ~dataProtectionPermission._flags) == DataProtectionPermissionFlags.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specific state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.-or- The version number of <paramref name="securityElement" /> is not supported. </exception>
		// Token: 0x06000025 RID: 37 RVA: 0x00002A67 File Offset: 0x00000C67
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._flags = (DataProtectionPermissionFlags)Enum.Parse(typeof(DataProtectionPermissionFlags), securityElement.Attribute("Flags"));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		// Token: 0x06000026 RID: 38 RVA: 0x00002A9C File Offset: 0x00000C9C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(DataProtectionPermission), 1);
			securityElement.AddAttribute("Flags", this._flags.ToString());
			return securityElement;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002ACA File Offset: 0x00000CCA
		private DataProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DataProtectionPermission dataProtectionPermission = target as DataProtectionPermission;
			if (dataProtectionPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(DataProtectionPermission));
			}
			return dataProtectionPermission;
		}

		// Token: 0x0400008E RID: 142
		private const int version = 1;

		// Token: 0x0400008F RID: 143
		private DataProtectionPermissionFlags _flags;
	}
}
