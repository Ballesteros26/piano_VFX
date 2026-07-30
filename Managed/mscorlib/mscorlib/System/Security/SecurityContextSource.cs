using System;

namespace System.Security
{
	/// <summary>Identifies the source for the security context.</summary>
	// Token: 0x02000537 RID: 1335
	public enum SecurityContextSource
	{
		/// <summary>The current application domain is the source for the security context.</summary>
		// Token: 0x04001F34 RID: 7988
		CurrentAppDomain,
		/// <summary>The current assembly is the source for the security context.</summary>
		// Token: 0x04001F35 RID: 7989
		CurrentAssembly
	}
}
