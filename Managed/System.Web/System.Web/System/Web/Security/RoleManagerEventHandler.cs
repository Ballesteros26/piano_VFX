using System;

namespace System.Web.Security
{
	/// <summary>Defines the delegate for the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
	/// <param name="sender">The <see cref="T:System.Web.Security.RoleManagerModule" /> that raised the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event.</param>
	/// <param name="e">A <see cref="T:System.Web.Security.RoleManagerEventArgs" /> object that contains the event data.</param>
	// Token: 0x020004B1 RID: 1201
	// (Invoke) Token: 0x0600363A RID: 13882
	public delegate void RoleManagerEventHandler(object sender, RoleManagerEventArgs e);
}
