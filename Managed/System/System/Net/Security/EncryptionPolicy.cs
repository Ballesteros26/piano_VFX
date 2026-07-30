using System;

namespace System.Net.Security
{
	/// <summary>The EncryptionPolicy to use. </summary>
	// Token: 0x020005EB RID: 1515
	public enum EncryptionPolicy
	{
		/// <summary>Require encryption and never allow a NULL cipher.</summary>
		// Token: 0x0400278E RID: 10126
		RequireEncryption,
		/// <summary>Prefer that full encryption be used, but allow a NULL cipher (no encryption) if the server agrees. </summary>
		// Token: 0x0400278F RID: 10127
		AllowNoEncryption,
		/// <summary>Allow no encryption and request that a NULL cipher be used if the other endpoint can handle a NULL cipher.</summary>
		// Token: 0x04002790 RID: 10128
		NoEncryption
	}
}
