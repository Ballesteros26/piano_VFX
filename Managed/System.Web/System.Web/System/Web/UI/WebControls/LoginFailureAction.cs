using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Determines the page that the user will go to when a login attempt is not successful.</summary>
	// Token: 0x020002E2 RID: 738
	public enum LoginFailureAction
	{
		/// <summary>Refreshes the current page so that the <see cref="T:System.Web.UI.WebControls.Login" /> control can display an error message.</summary>
		// Token: 0x04001715 RID: 5909
		Refresh,
		/// <summary>Redirects the user to the login page defined in the site's configuration files (Machine.config and Web.config).</summary>
		// Token: 0x04001716 RID: 5910
		RedirectToLoginPage
	}
}
