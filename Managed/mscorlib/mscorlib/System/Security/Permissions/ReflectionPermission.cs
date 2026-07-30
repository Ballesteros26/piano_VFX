using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls access to non-public types and members through the <see cref="N:System.Reflection" /> APIs. Controls some features of the <see cref="N:System.Reflection.Emit" /> APIs.</summary>
	// Token: 0x020005A9 RID: 1449
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06004080 RID: 16512 RVA: 0x000E5C6E File Offset: 0x000E3E6E
		public ReflectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with the specified access.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />. </exception>
		// Token: 0x06004081 RID: 16513 RVA: 0x000E5C8F File Offset: 0x000E3E8F
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the type of reflection allowed for the current permission.</summary>
		/// <returns>The set flags for the current permission.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000E5C9E File Offset: 0x000E3E9E
		// (set) Token: 0x06004083 RID: 16515 RVA: 0x000E5CA6 File Offset: 0x000E3EA6
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & (ReflectionPermissionFlag.TypeInformation | ReflectionPermissionFlag.MemberAccess | ReflectionPermissionFlag.ReflectionEmit | ReflectionPermissionFlag.RestrictedMemberAccess)) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "ReflectionPermissionFlag");
				}
				this.flags = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06004084 RID: 16516 RVA: 0x000E5CD6 File Offset: 0x000E3ED6
		public override IPermission Copy()
		{
			return new ReflectionPermission(this.flags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06004085 RID: 16517 RVA: 0x000E5CE4 File Offset: 0x000E3EE4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
			string text = esd.Attributes["Flags"] as string;
			if (text.IndexOf("MemberAccess") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.MemberAccess;
			}
			if (text.IndexOf("ReflectionEmit") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.ReflectionEmit;
			}
			if (text.IndexOf("TypeInformation") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.TypeInformation;
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004086 RID: 16518 RVA: 0x000E5D80 File Offset: 0x000E3F80
		public override IPermission Intersect(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				if (reflectionPermission.Flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return reflectionPermission.Copy();
			}
			else if (reflectionPermission.IsUnrestricted())
			{
				if (this.flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
				reflectionPermission2.Flags &= this.flags;
				if (reflectionPermission2.Flags != ReflectionPermissionFlag.NoFlags)
				{
					return reflectionPermission2;
				}
				return null;
			}
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004087 RID: 16519 RVA: 0x000E5DF8 File Offset: 0x000E3FF8
		public override bool IsSubsetOf(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return this.flags == ReflectionPermissionFlag.NoFlags;
			}
			if (this.IsUnrestricted())
			{
				return reflectionPermission.IsUnrestricted();
			}
			return reflectionPermission.IsUnrestricted() || (this.flags & reflectionPermission.Flags) == this.flags;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06004088 RID: 16520 RVA: 0x000E5E48 File Offset: 0x000E4048
		public bool IsUnrestricted()
		{
			return this.flags == ReflectionPermissionFlag.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004089 RID: 16521 RVA: 0x000E5E54 File Offset: 0x000E4054
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.flags == ReflectionPermissionFlag.NoFlags)
			{
				securityElement.AddAttribute("Flags", "NoFlags");
			}
			else if ((this.flags & ReflectionPermissionFlag.AllFlags) == ReflectionPermissionFlag.AllFlags)
			{
				securityElement.AddAttribute("Flags", "AllFlags");
			}
			else
			{
				string text = "";
				if ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
				{
					text = "MemberAccess";
				}
				if ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "ReflectionEmit";
				}
				if ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "TypeInformation";
				}
				securityElement.AddAttribute("Flags", text);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600408A RID: 16522 RVA: 0x000E5F3C File Offset: 0x000E413C
		public override IPermission Union(IPermission other)
		{
			ReflectionPermission reflectionPermission = this.Cast(other);
			if (other == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
			reflectionPermission2.Flags |= this.flags;
			return reflectionPermission2;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x00028BDC File Offset: 0x00026DDC
		int IBuiltInPermission.GetTokenIndex()
		{
			return 4;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000E5F90 File Offset: 0x000E4190
		private ReflectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ReflectionPermission reflectionPermission = target as ReflectionPermission;
			if (reflectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ReflectionPermission));
			}
			return reflectionPermission;
		}

		// Token: 0x040020AD RID: 8365
		private const int version = 1;

		// Token: 0x040020AE RID: 8366
		private ReflectionPermissionFlag flags;
	}
}
