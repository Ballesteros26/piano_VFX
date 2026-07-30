using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies how to match versions when locating application trusts in a collection.</summary>
	// Token: 0x0200055C RID: 1372
	[ComVisible(true)]
	public enum ApplicationVersionMatch
	{
		/// <summary>Match on the exact version.</summary>
		// Token: 0x04001FA4 RID: 8100
		MatchExactVersion,
		/// <summary>Match on all versions.</summary>
		// Token: 0x04001FA5 RID: 8101
		MatchAllVersions
	}
}
