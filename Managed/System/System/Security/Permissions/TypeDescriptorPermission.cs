using System;
using System.Globalization;

namespace System.Security.Permissions
{
	/// <summary>Defines partial-trust access to the <see cref="T:System.ComponentModel.TypeDescriptor" /> class.</summary>
	// Token: 0x02000373 RID: 883
	[Serializable]
	public sealed class TypeDescriptorPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class. </summary>
		/// <param name="state">The <see cref="T:System.Security.Permissions.PermissionState" /> to request. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />  and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid permission state. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />  and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid. </exception>
		// Token: 0x06001AF8 RID: 6904 RVA: 0x0006BFBC File Offset: 0x0006A1BC
		public TypeDescriptorPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(global::SR.GetString("Invalid permission state."));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class with the specified permission flags. </summary>
		/// <param name="flag">The permission flags to request.</param>
		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006BFEA File Offset: 0x0006A1EA
		public TypeDescriptorPermission(TypeDescriptorPermissionFlags flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006C007 File Offset: 0x0006A207
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.Reset();
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0006C01A File Offset: 0x0006A21A
		private void Reset()
		{
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor. </summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor.</returns>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0006C033 File Offset: 0x0006A233
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0006C023 File Offset: 0x0006A223
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		/// <summary>Gets a value that indicates whether the type descriptor may be called from partially trusted code. </summary>
		/// <returns>true if the <see cref="P:System.Security.Permissions.TypeDescriptorPermission.Flags" /> property is set to <see cref="F:System.Security.Permissions.TypeDescriptorPermissionFlags.RestrictedRegistrationAccess" />; otherwise, false. </returns>
		// Token: 0x06001AFE RID: 6910 RVA: 0x0006C03B File Offset: 0x0006A23B
		public bool IsUnrestricted()
		{
			return this.m_flags == TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
		}

		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		// Token: 0x06001AFF RID: 6911 RVA: 0x0006C048 File Offset: 0x0006A248
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission permission;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = this.m_flags | typeDescriptorPermission.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return permission;
		}

		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		// Token: 0x06001B00 RID: 6912 RVA: 0x0006C0BC File Offset: 0x0006A2BC
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == TypeDescriptorPermissionFlags.NoFlags;
			}
			bool flag;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags flags = this.m_flags;
				TypeDescriptorPermissionFlags flags2 = typeDescriptorPermission.m_flags;
				flag = (flags & flags2) == flags;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return flag;
		}

		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		// Token: 0x06001B01 RID: 6913 RVA: 0x0006C12C File Offset: 0x0006A32C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission permission;
			try
			{
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = ((TypeDescriptorPermission)target).m_flags & this.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return permission;
		}

		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06001B02 RID: 6914 RVA: 0x0006C19C File Offset: 0x0006A39C
		public override IPermission Copy()
		{
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006C1A9 File Offset: 0x0006A3A9
		private void VerifyAccess(TypeDescriptorPermissionFlags type)
		{
			if ((type & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("Illegal enum value: {0}."), (int)type));
			}
		}

		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001B04 RID: 6916 RVA: 0x0006C1D4 File Offset: 0x0006A3D4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06001B05 RID: 6917 RVA: 0x0006C274 File Offset: 0x0006A474
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(global::SR.GetString("The value of \"class\" attribute is invalid."), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = (TypeDescriptorPermissionFlags)Enum.Parse(typeof(TypeDescriptorPermissionFlags), text3);
				TypeDescriptorPermission.VerifyFlags(typeDescriptorPermissionFlags);
				this.m_flags = typeDescriptorPermissionFlags;
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006C326 File Offset: 0x0006A526
		internal static void VerifyFlags(TypeDescriptorPermissionFlags flags)
		{
			if ((flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("Illegal enum value: {0}."), (int)flags));
			}
		}

		// Token: 0x0400188C RID: 6284
		private TypeDescriptorPermissionFlags m_flags;
	}
}
