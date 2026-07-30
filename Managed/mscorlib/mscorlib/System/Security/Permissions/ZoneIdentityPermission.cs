using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the zone from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x020005C0 RID: 1472
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x0600415D RID: 16733 RVA: 0x000E8984 File Offset: 0x000E6B84
		public ZoneIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
			this.zone = SecurityZone.NoZone;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> class to represent the specified zone identity.</summary>
		/// <param name="zone">The zone identifier. </param>
		// Token: 0x0600415E RID: 16734 RVA: 0x000E899B File Offset: 0x000E6B9B
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x0600415F RID: 16735 RVA: 0x000E89AA File Offset: 0x000E6BAA
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null, this permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone, and the specified permission is not equal to the current permission. </exception>
		// Token: 0x06004160 RID: 16736 RVA: 0x000E89B8 File Offset: 0x000E6BB8
		public override bool IsSubsetOf(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				return this.zone == SecurityZone.NoZone;
			}
			return this.zone == SecurityZone.NoZone || this.zone == zoneIdentityPermission.zone;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal and the current permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone.</exception>
		// Token: 0x06004161 RID: 16737 RVA: 0x000E89F4 File Offset: 0x000E6BF4
		public override IPermission Union(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				if (this.zone != SecurityZone.NoZone)
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.zone == zoneIdentityPermission.zone || zoneIdentityPermission.zone == SecurityZone.NoZone)
				{
					return this.Copy();
				}
				if (this.zone == SecurityZone.NoZone)
				{
					return zoneIdentityPermission.Copy();
				}
				throw new ArgumentException(Locale.GetText("Union impossible"));
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06004162 RID: 16738 RVA: 0x000E8A5C File Offset: 0x000E6C5C
		public override IPermission Intersect(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null || this.zone == SecurityZone.NoZone)
			{
				return null;
			}
			if (this.zone == zoneIdentityPermission.zone)
			{
				return this.Copy();
			}
			return null;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06004163 RID: 16739 RVA: 0x000E8A98 File Offset: 0x000E6C98
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Zone");
			if (text == null)
			{
				this.zone = SecurityZone.NoZone;
				return;
			}
			this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06004164 RID: 16740 RVA: 0x000E8AE8 File Offset: 0x000E6CE8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", this.zone.ToString());
			}
			return securityElement;
		}

		/// <summary>Gets or sets the zone represented by the current <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The parameter value is not a valid value of <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000E8B23 File Offset: 0x000E6D23
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x000E8B2B File Offset: 0x000E6D2B
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "SecurityZone");
				}
				this.zone = value;
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x0003CD83 File Offset: 0x0003AF83
		int IBuiltInPermission.GetTokenIndex()
		{
			return 14;
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000E8B6B File Offset: 0x000E6D6B
		private ZoneIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ZoneIdentityPermission));
			}
			return zoneIdentityPermission;
		}

		// Token: 0x0400210A RID: 8458
		private const int version = 1;

		// Token: 0x0400210B RID: 8459
		private SecurityZone zone;
	}
}
