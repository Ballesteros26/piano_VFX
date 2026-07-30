using System;

namespace System.Security.Permissions
{
	// Token: 0x02000592 RID: 1426
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06003FD5 RID: 16341 RVA: 0x000E3F18 File Offset: 0x000E2118
		public HostProtectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._resources = HostProtectionResource.All;
				return;
			}
			this._resources = HostProtectionResource.None;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000E3F3D File Offset: 0x000E213D
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = this._resources;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x000E3F51 File Offset: 0x000E2151
		// (set) Token: 0x06003FD8 RID: 16344 RVA: 0x000E3F59 File Offset: 0x000E2159
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				if (!Enum.IsDefined(typeof(HostProtectionResource), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "HostProtectionResource");
				}
				this._resources = value;
			}
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000E3F99 File Offset: 0x000E2199
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000E3FA8 File Offset: 0x000E21A8
		public override IPermission Intersect(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return hostProtectionPermission.Copy();
			}
			if (hostProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new HostProtectionPermission(this._resources & hostProtectionPermission._resources);
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000E400C File Offset: 0x000E220C
		public override IPermission Union(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this._resources | hostProtectionPermission._resources);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000E4054 File Offset: 0x000E2254
		public override bool IsSubsetOf(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this._resources == HostProtectionResource.None;
			}
			return hostProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._resources & ~hostProtectionPermission._resources) == HostProtectionResource.None);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000E409B File Offset: 0x000E229B
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), e.Attribute("Resources"));
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000E40D0 File Offset: 0x000E22D0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			securityElement.AddAttribute("Resources", this._resources.ToString());
			return securityElement;
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000E40F5 File Offset: 0x000E22F5
		public bool IsUnrestricted()
		{
			return this._resources == HostProtectionResource.All;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x0004782B File Offset: 0x00045A2B
		int IBuiltInPermission.GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000E4104 File Offset: 0x000E2304
		private HostProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			HostProtectionPermission hostProtectionPermission = target as HostProtectionPermission;
			if (hostProtectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(HostProtectionPermission));
			}
			return hostProtectionPermission;
		}

		// Token: 0x04002046 RID: 8262
		private const int version = 1;

		// Token: 0x04002047 RID: 8263
		private HostProtectionResource _resources;
	}
}
