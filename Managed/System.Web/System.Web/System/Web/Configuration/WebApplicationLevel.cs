using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the path level of a Web-application configuration file.</summary>
	// Token: 0x0200057C RID: 1404
	public enum WebApplicationLevel
	{
		/// <summary>Specifies that the configuration file is in a global directory in relation to the current ASP.NET Web application.</summary>
		// Token: 0x04002074 RID: 8308
		AboveApplication = 10,
		/// <summary>Specifies that the configuration file is in the root directory of the current ASP.NET Web application.</summary>
		// Token: 0x04002075 RID: 8309
		AtApplication = 20,
		/// <summary>Specifies that the configuration file is in a sub-directory of the current ASP.NET Web application.</summary>
		// Token: 0x04002076 RID: 8310
		BelowApplication = 30
	}
}
