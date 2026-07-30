using System;

namespace System
{
	/// <summary>Provides the possible values for the configuration setting of the <see cref="T:System.Configuration.IdnElement" /> in the <see cref="N:System.Configuration" /> namespace.</summary>
	// Token: 0x02000101 RID: 257
	public enum UriIdnScope
	{
		/// <summary>This value will not convert any Unicode domain names to use Punycode. This is the default value which is consistent with the .NET Framework 2.0 behavior.</summary>
		// Token: 0x04000CD6 RID: 3286
		None,
		/// <summary>This value will convert all external Unicode domain names to use the Punycode equivalents (IDN names). In this case to handle international names on the local Intranet, the DNS servers that are used for the Intranet should support Unicode names.</summary>
		// Token: 0x04000CD7 RID: 3287
		AllExceptIntranet,
		/// <summary>This value will convert any Unicode domain names to their Punycode equivalents (IDN names).</summary>
		// Token: 0x04000CD8 RID: 3288
		All
	}
}
