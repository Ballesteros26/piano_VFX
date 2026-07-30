using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of request validation for a control.</summary>
	// Token: 0x02000198 RID: 408
	public enum ValidateRequestMode
	{
		/// <summary>Request validation uses the same behavior as its parent control.</summary>
		// Token: 0x0400133C RID: 4924
		Inherit,
		/// <summary>Request validation is disabled.</summary>
		// Token: 0x0400133D RID: 4925
		Disabled,
		/// <summary>Request validation is enabled.</summary>
		// Token: 0x0400133E RID: 4926
		Enabled
	}
}
