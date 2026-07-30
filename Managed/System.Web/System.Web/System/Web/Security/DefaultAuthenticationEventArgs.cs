using System;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Provides data for the DefaultAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004BB RID: 1211
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DefaultAuthenticationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationEventArgs" /> class.</summary>
		/// <param name="context">The context for the event. </param>
		// Token: 0x0600367C RID: 13948 RVA: 0x0008E9D8 File Offset: 0x0008CBD8
		public DefaultAuthenticationEventArgs(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._context = context;
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x0008E9F5 File Offset: 0x0008CBF5
		public HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x04001DB8 RID: 7608
		private HttpContext _context;
	}
}
