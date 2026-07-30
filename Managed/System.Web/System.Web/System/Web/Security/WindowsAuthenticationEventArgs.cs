using System;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security
{
	/// <summary>Provides data for the WindowsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
	// Token: 0x020004B2 RID: 1202
	public sealed class WindowsAuthenticationEventArgs : EventArgs
	{
		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</returns>
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x0008E642 File Offset: 0x0008C842
		// (set) Token: 0x0600363E RID: 13886 RVA: 0x0008E64A File Offset: 0x0008C84A
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
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x0008E653 File Offset: 0x0008C853
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Gets the Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</summary>
		/// <returns>The Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</returns>
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x0008E65B File Offset: 0x0008C85B
		public WindowsIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		/// <summary>Initializes a newly created instance of the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> class.</summary>
		/// <param name="identity">The Windows identity object. </param>
		/// <param name="context">The context for the event. </param>
		// Token: 0x06003641 RID: 13889 RVA: 0x0008E663 File Offset: 0x0008C863
		public WindowsAuthenticationEventArgs(WindowsIdentity identity, HttpContext context)
		{
			this._Identity = identity;
			this._Context = context;
		}

		// Token: 0x04001DA6 RID: 7590
		private IPrincipal _User;

		// Token: 0x04001DA7 RID: 7591
		private HttpContext _Context;

		// Token: 0x04001DA8 RID: 7592
		private WindowsIdentity _Identity;
	}
}
