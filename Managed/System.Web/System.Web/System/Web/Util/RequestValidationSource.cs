using System;

namespace System.Web.Util
{
	/// <summary>Specifies what kind of HTTP request data to validate.</summary>
	// Token: 0x02000130 RID: 304
	public enum RequestValidationSource
	{
		/// <summary>The query string.</summary>
		// Token: 0x040011CA RID: 4554
		QueryString,
		/// <summary>The form values.</summary>
		// Token: 0x040011CB RID: 4555
		Form,
		/// <summary>The request cookies.</summary>
		// Token: 0x040011CC RID: 4556
		Cookies,
		/// <summary>The uploaded file.</summary>
		// Token: 0x040011CD RID: 4557
		Files,
		/// <summary>The raw URL. (The part of a URL after the domain.)</summary>
		// Token: 0x040011CE RID: 4558
		RawUrl,
		/// <summary>The virtual path.</summary>
		// Token: 0x040011CF RID: 4559
		Path,
		/// <summary>An HTTP <see cref="P:System.Web.HttpRequest.PathInfo" /> string, which is an extension to a URL path. </summary>
		// Token: 0x040011D0 RID: 4560
		PathInfo,
		/// <summary>The request headers.</summary>
		// Token: 0x040011D1 RID: 4561
		Headers
	}
}
