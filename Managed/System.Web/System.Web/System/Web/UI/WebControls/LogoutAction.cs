using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Indicates the page that the user will be directed to when he or she logs out of the Web site. </summary>
	// Token: 0x020002E4 RID: 740
	public enum LogoutAction
	{
		/// <summary>Reloads the current page with the user logged out.</summary>
		// Token: 0x0400171B RID: 5915
		Refresh,
		/// <summary>Redirects the user to a specified URL.</summary>
		// Token: 0x0400171C RID: 5916
		Redirect,
		/// <summary>Redirects the user to the login page defined in the site's configuration files (Machine.config and Web.config).</summary>
		// Token: 0x0400171D RID: 5917
		RedirectToLoginPage
	}
}
