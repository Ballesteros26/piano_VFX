using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x020005EA RID: 1514
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x0400278A RID: 10122
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x0400278B RID: 10123
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x0400278C RID: 10124
		EncryptAndSign
	}
}
