using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the type of user interface (UI) the trust manager should use for trust decisions. </summary>
	// Token: 0x0200057F RID: 1407
	[ComVisible(true)]
	public enum TrustManagerUIContext
	{
		/// <summary>An Install UI.</summary>
		// Token: 0x0400200D RID: 8205
		Install,
		/// <summary>An Upgrade UI.</summary>
		// Token: 0x0400200E RID: 8206
		Upgrade,
		/// <summary>A Run UI.</summary>
		// Token: 0x0400200F RID: 8207
		Run
	}
}
