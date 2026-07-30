using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication mode to use in a Web application.</summary>
	// Token: 0x0200057E RID: 1406
	public enum XhtmlConformanceMode
	{
		/// <summary>Reverts a number of rendering changes made for conformance to the v1.1 rendering behavior. </summary>
		// Token: 0x0400207B RID: 8315
		Transitional,
		/// <summary>XHTML 1.0 Transitional </summary>
		// Token: 0x0400207C RID: 8316
		Legacy,
		/// <summary>XHTML 1.0 Strict conformance </summary>
		// Token: 0x0400207D RID: 8317
		Strict
	}
}
