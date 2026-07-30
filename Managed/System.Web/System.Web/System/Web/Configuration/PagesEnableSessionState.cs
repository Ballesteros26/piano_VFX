using System;

namespace System.Web.Configuration
{
	/// <summary>Used to determine session-state activation for a single Web page or an entire Web application.</summary>
	// Token: 0x0200056E RID: 1390
	public enum PagesEnableSessionState
	{
		/// <summary>Session state is disabled.</summary>
		// Token: 0x04002032 RID: 8242
		False,
		/// <summary>Session state is enabled, but not writable.</summary>
		// Token: 0x04002033 RID: 8243
		ReadOnly,
		/// <summary>Session state is enabled.</summary>
		// Token: 0x04002034 RID: 8244
		True
	}
}
