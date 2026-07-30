using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeKeyChoice" /> enumeration defines the type of key used in a key agreement protocol.</summary>
	// Token: 0x02000023 RID: 35
	public enum KeyAgreeKeyChoice
	{
		/// <summary>The key agreement key type is unknown.</summary>
		// Token: 0x040000C8 RID: 200
		Unknown,
		/// <summary>The key agreement key is ephemeral, existing only for the duration of the key agreement protocol.</summary>
		// Token: 0x040000C9 RID: 201
		EphemeralKey,
		/// <summary>The key agreement key is static, existing for an extended period of time.</summary>
		// Token: 0x040000CA RID: 202
		StaticKey
	}
}
