using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the values for the custom errors modality.</summary>
	// Token: 0x02000561 RID: 1377
	public enum CustomErrorsMode
	{
		/// <summary>Enables custom errors on remote clients only. Custom errors are shown only to remote clients and ASP.NET errors are shown to the local host.</summary>
		// Token: 0x0400200E RID: 8206
		RemoteOnly,
		/// <summary>Enables custom errors. If no <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> is specified, standard errors are issued. </summary>
		// Token: 0x0400200F RID: 8207
		On,
		/// <summary>Disables custom errors, allowing display of standard errors.</summary>
		// Token: 0x04002010 RID: 8208
		Off
	}
}
