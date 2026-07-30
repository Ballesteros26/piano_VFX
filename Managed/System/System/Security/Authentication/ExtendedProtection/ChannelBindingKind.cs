using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBindingKind" /> enumeration represents the kinds of channel bindings that can be queried from secure channels.</summary>
	// Token: 0x02000384 RID: 900
	public enum ChannelBindingKind
	{
		/// <summary>An unknown channel binding type.</summary>
		// Token: 0x040018C9 RID: 6345
		Unknown,
		/// <summary>A channel binding completely unique to a given channel (a TLS session key, for example).</summary>
		// Token: 0x040018CA RID: 6346
		Unique = 25,
		/// <summary>A channel binding unique to a given endpoint (a TLS server certificate, for example).</summary>
		// Token: 0x040018CB RID: 6347
		Endpoint
	}
}
