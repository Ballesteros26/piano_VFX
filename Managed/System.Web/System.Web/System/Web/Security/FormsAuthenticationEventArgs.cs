using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>Provides data for the FormsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004AB RID: 1195
	public sealed class FormsAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</returns>
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x0008E5BB File Offset: 0x0008C7BB
		// (set) Token: 0x06003621 RID: 13857 RVA: 0x0008E5C3 File Offset: 0x0008C7C3
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

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x0008E5CC File Offset: 0x0008C7CC
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event. </param>
		// Token: 0x06003623 RID: 13859 RVA: 0x0008E5D4 File Offset: 0x0008C7D4
		public FormsAuthenticationEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001D9F RID: 7583
		private IPrincipal _User;

		// Token: 0x04001DA0 RID: 7584
		private HttpContext _Context;
	}
}
