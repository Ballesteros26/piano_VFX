using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x02000418 RID: 1048
	[Flags]
	public enum SecurityProtocolType
	{
		// Token: 0x04001BB8 RID: 7096
		SystemDefault = 0,
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol.</summary>
		// Token: 0x04001BB9 RID: 7097
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol.</summary>
		// Token: 0x04001BBA RID: 7098
		Tls = 192,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.1 security protocol.</summary>
		// Token: 0x04001BBB RID: 7099
		Tls11 = 768,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.2 security protocol.</summary>
		// Token: 0x04001BBC RID: 7100
		Tls12 = 3072
	}
}
