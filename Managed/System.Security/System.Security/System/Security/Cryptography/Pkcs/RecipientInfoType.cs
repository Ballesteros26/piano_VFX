using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration defines the types of recipient information.</summary>
	// Token: 0x02000030 RID: 48
	public enum RecipientInfoType
	{
		/// <summary>The recipient information type is unknown.</summary>
		// Token: 0x040000E6 RID: 230
		Unknown,
		/// <summary>Key transport recipient information.</summary>
		// Token: 0x040000E7 RID: 231
		KeyTransport,
		/// <summary>Key agreement recipient information.</summary>
		// Token: 0x040000E8 RID: 232
		KeyAgreement
	}
}
