using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible versions of <see cref="T:System.Security.Authentication.SslProtocols" />.</summary>
	// Token: 0x0200037F RID: 895
	[Flags]
	public enum SslProtocols
	{
		/// <summary>No SSL protocol is specified.</summary>
		// Token: 0x040018BC RID: 6332
		None = 0,
		/// <summary>Specifies the SSL 2.0 protocol. SSL 2.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x040018BD RID: 6333
		Ssl2 = 12,
		/// <summary>Specifies the SSL 3.0 protocol. SSL 3.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x040018BE RID: 6334
		Ssl3 = 48,
		/// <summary>Specifies the TLS 1.0 security protocol. The TLS protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x040018BF RID: 6335
		Tls = 192,
		/// <summary>Specifies the TLS 1.1 security protocol. The TLS protocol is defined in IETF RFC 4346.</summary>
		// Token: 0x040018C0 RID: 6336
		[MonoTODO("unsupported")]
		Tls11 = 768,
		/// <summary>Specifies the TLS 1.2 security protocol. The TLS protocol is defined in IETF RFC 5246.</summary>
		// Token: 0x040018C1 RID: 6337
		[MonoTODO("unsupported")]
		Tls12 = 3072,
		/// <summary>Specifies that either Secure Sockets Layer (SSL) 3.0 or Transport Layer Security (TLS) 1.0 are acceptable for secure communications</summary>
		// Token: 0x040018C2 RID: 6338
		Default = 240
	}
}
