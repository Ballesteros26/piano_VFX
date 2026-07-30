using System;

namespace System.Web
{
	/// <summary>Represents the method that handles post-cache substitution.</summary>
	/// <returns>The content inserted into the cached response before being sent to the client. </returns>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains the HTTP request information for the page with the control that requires post-cache substitution.</param>
	// Token: 0x020000B0 RID: 176
	// (Invoke) Token: 0x06000938 RID: 2360
	public delegate string HttpResponseSubstitutionCallback(HttpContext context);
}
