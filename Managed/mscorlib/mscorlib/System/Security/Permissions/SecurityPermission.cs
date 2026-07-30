using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Describes a set of security permissions applied to code. This class cannot be inherited.</summary>
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x060040C5 RID: 16581 RVA: 0x000E6B94 File Offset: 0x000E4D94
		public SecurityPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			this.flags = SecurityPermissionFlag.NoFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with the specified initial set state of the flags.</summary>
		/// <param name="flag">The initial state of the permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />. </exception>
		// Token: 0x060040C6 RID: 16582 RVA: 0x000E6BB9 File Offset: 0x000E4DB9
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the security permission flags.</summary>
		/// <returns>The state of the current permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x000E6BC8 File Offset: 0x000E4DC8
		// (set) Token: 0x060040C8 RID: 16584 RVA: 0x000E6BD0 File Offset: 0x000E4DD0
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & SecurityPermissionFlag.AllFlags) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "SecurityPermissionFlag");
				}
				this.flags = value;
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x060040C9 RID: 16585 RVA: 0x000E6C03 File Offset: 0x000E4E03
		public bool IsUnrestricted()
		{
			return this.flags == SecurityPermissionFlag.AllFlags;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x060040CA RID: 16586 RVA: 0x000E6C12 File Offset: 0x000E4E12
		public override IPermission Copy()
		{
			return new SecurityPermission(this.flags);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission object that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040CB RID: 16587 RVA: 0x000E6C20 File Offset: 0x000E4E20
		public override IPermission Intersect(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return null;
			}
			if (this.IsEmpty() || securityPermission.IsEmpty())
			{
				return null;
			}
			if (this.IsUnrestricted() && securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return securityPermission.Copy();
			}
			if (securityPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			SecurityPermissionFlag securityPermissionFlag = this.flags & securityPermission.flags;
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040CC RID: 16588 RVA: 0x000E6C9C File Offset: 0x000E4E9C
		public override IPermission Union(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || securityPermission.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.flags | securityPermission.flags);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x060040CD RID: 16589 RVA: 0x000E6CE4 File Offset: 0x000E4EE4
		public override bool IsSubsetOf(IPermission target)
		{
			SecurityPermission securityPermission = this.Cast(target);
			if (securityPermission == null)
			{
				return this.IsEmpty();
			}
			return securityPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this.flags & ~securityPermission.flags) == SecurityPermissionFlag.NoFlags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not supported. </exception>
		// Token: 0x060040CE RID: 16590 RVA: 0x000E6D28 File Offset: 0x000E4F28
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			string text = esd.Attribute("Flags");
			if (text == null)
			{
				this.flags = SecurityPermissionFlag.NoFlags;
				return;
			}
			this.flags = (SecurityPermissionFlag)Enum.Parse(typeof(SecurityPermissionFlag), text);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x060040CF RID: 16591 RVA: 0x000E6D8C File Offset: 0x000E4F8C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Flags", this.flags.ToString());
			}
			return securityElement;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00029D4E File Offset: 0x00027F4E
		int IBuiltInPermission.GetTokenIndex()
		{
			return 6;
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000E6DD8 File Offset: 0x000E4FD8
		private bool IsEmpty()
		{
			return this.flags == SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000E6DE3 File Offset: 0x000E4FE3
		private SecurityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SecurityPermission));
			}
			return securityPermission;
		}

		// Token: 0x040020D6 RID: 8406
		private const int version = 1;

		// Token: 0x040020D7 RID: 8407
		private SecurityPermissionFlag flags;
	}
}
