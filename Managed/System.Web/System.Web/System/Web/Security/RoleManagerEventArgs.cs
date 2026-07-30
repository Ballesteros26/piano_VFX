using System;

namespace System.Web.Security
{
	/// <summary>Provides event data for the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
	// Token: 0x020004B0 RID: 1200
	public sealed class RoleManagerEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether role information has been applied to the current user.</summary>
		/// <returns>true if role information has been applied to the current user; otherwise, false.</returns>
		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x0008E61A File Offset: 0x0008C81A
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x0008E622 File Offset: 0x0008C822
		public bool RolesPopulated
		{
			get
			{
				return this._RolesPopulated;
			}
			set
			{
				this._RolesPopulated = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x0008E62B File Offset: 0x0008C82B
		public HttpContext Context
		{
			get
			{
				return this._Context;
			}
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.RoleManagerEventArgs" /> class and sets the <see cref="P:System.Web.Security.RoleManagerEventArgs.Context" /> property to the specified <see cref="T:System.Web.HttpContext" />.</summary>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
		// Token: 0x06003638 RID: 13880 RVA: 0x0008E633 File Offset: 0x0008C833
		public RoleManagerEventArgs(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x04001DA4 RID: 7588
		private HttpContext _Context;

		// Token: 0x04001DA5 RID: 7589
		private bool _RolesPopulated;
	}
}
