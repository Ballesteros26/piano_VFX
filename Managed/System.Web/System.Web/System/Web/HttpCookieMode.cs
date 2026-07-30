using System;

namespace System.Web
{
	/// <summary>Specifies how cookies are used for a Web application.</summary>
	// Token: 0x02000092 RID: 146
	public enum HttpCookieMode
	{
		/// <summary>The calling feature uses the query string to store an identifier regardless of whether the browser or device supports cookies.</summary>
		// Token: 0x04000F5A RID: 3930
		UseUri,
		/// <summary>Cookies are used to persist user data regardless of whether the browser or device supports cookies.</summary>
		// Token: 0x04000F5B RID: 3931
		UseCookies,
		/// <summary>ASP.NET determines whether the requesting browser or device supports cookies. If the requesting browser or device supports cookies then <see cref="F:System.Web.HttpCookieMode.AutoDetect" /> uses cookies to persist user data; otherwise, an identifier is used in the query string. If the browser or device supports the use of cookies but cookies are currently disabled, cookies are still used by the requesting feature.</summary>
		// Token: 0x04000F5C RID: 3932
		AutoDetect,
		/// <summary>ASP.NET determines whether to use cookies based on <see cref="T:System.Web.HttpBrowserCapabilities" /> setting. If the setting indicates that the browser or device supports cookies, cookies are used; otherwise, an identifier is used in the query string.</summary>
		// Token: 0x04000F5D RID: 3933
		UseDeviceProfile
	}
}
