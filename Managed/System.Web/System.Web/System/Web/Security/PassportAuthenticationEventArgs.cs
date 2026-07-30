using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>The event argument passed to the <see cref="E:System.Web.Security.PassportAuthenticationModule.Authenticate" /> event by a <see cref="T:System.Web.Security.PassportAuthenticationModule" />. Since there is already an identity at this point, this is useful mainly for attaching a custom <see cref="T:System.Security.Principal.IPrincipal" /> object to the context using the supplied identity. This class is deprecated.</summary>
	// Token: 0x020004AE RID: 1198
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request. This class is deprecated.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request.</returns>
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x0008E5E3 File Offset: 0x0008C7E3
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x0008E5EB File Offset: 0x0008C7EB
		public IPrincipal User
		{
			get
			{
				return this._User;
			}
			[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
			set
			{
				this._User = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request. This class is deprecated.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x0008E5F4 File Offset: 0x0008C7F4
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets an authenticated Passport identity. This class is deprecated.</summary>
		/// <returns>An authenticated Passport identity.</returns>
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x0008E5FC File Offset: 0x0008C7FC
		public PassportIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationEventArgs" /> class. This class is deprecated.</summary>
		/// <param name="identity">The identity object </param>
		/// <param name="context">The context for the event. </param>
		// Token: 0x06003630 RID: 13872 RVA: 0x0008E604 File Offset: 0x0008C804
		public PassportAuthenticationEventArgs(PassportIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04001DA1 RID: 7585
		private IPrincipal _User;

		// Token: 0x04001DA2 RID: 7586
		private HttpContext _Context;

		// Token: 0x04001DA3 RID: 7587
		private PassportIdentity _Identity;
	}
}
