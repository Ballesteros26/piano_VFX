using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Allows security actions for <see cref="T:System.Web.AspNetHostingPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000122 RID: 290
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class AspNetHostingPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> enumeration values. </param>
		// Token: 0x060007C9 RID: 1993 RVA: 0x00026E30 File Offset: 0x00025030
		public AspNetHostingPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this._level = AspNetHostingPermissionLevel.None;
		}

		/// <summary>Creates a new <see cref="T:System.Web.AspNetHostingPermission" /> with the permission level previously set by the <see cref="P:System.Web.AspNetHostingPermissionAttribute.Level" /> property.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that is the new <see cref="T:System.Web.AspNetHostingPermission" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007CA RID: 1994 RVA: 0x00026E41 File Offset: 0x00025041
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new AspNetHostingPermission(PermissionState.Unrestricted);
			}
			return new AspNetHostingPermission(this._level);
		}

		/// <summary>Gets or sets the current hosting permission level.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00026E5D File Offset: 0x0002505D
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00026E65 File Offset: 0x00025065
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value < AspNetHostingPermissionLevel.None || value > AspNetHostingPermissionLevel.Unrestricted)
				{
					throw new ArgumentException(string.Format(global::Locale.GetText("Invalid enum {0}."), value), "Level");
				}
				this._level = value;
			}
		}

		// Token: 0x04000D6F RID: 3439
		private AspNetHostingPermissionLevel _level;
	}
}
