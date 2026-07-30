using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies values for how the URL of the original request is handled when a custom error page is displayed.</summary>
	// Token: 0x02000562 RID: 1378
	public enum CustomErrorsRedirectMode
	{
		/// <summary>Display the error page and change the URL of the original request.</summary>
		// Token: 0x04002012 RID: 8210
		ResponseRedirect,
		/// <summary>Display the error page without changing the original URL.</summary>
		// Token: 0x04002013 RID: 8211
		ResponseRewrite
	}
}
