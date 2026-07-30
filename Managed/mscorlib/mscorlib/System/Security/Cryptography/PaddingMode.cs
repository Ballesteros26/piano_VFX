using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the type of padding to apply when the message data block is shorter than the full number of bytes needed for a cryptographic operation.</summary>
	// Token: 0x0200064C RID: 1612
	[ComVisible(true)]
	[Serializable]
	public enum PaddingMode
	{
		/// <summary>No padding is done.</summary>
		// Token: 0x040023DD RID: 9181
		None = 1,
		/// <summary>The PKCS #7 padding string consists of a sequence of bytes, each of which is equal to the total number of padding bytes added. </summary>
		// Token: 0x040023DE RID: 9182
		PKCS7,
		/// <summary>The padding string consists of bytes set to zero.</summary>
		// Token: 0x040023DF RID: 9183
		Zeros,
		/// <summary>The ANSIX923 padding string consists of a sequence of bytes filled with zeros before the length.</summary>
		// Token: 0x040023E0 RID: 9184
		ANSIX923,
		/// <summary>The ISO10126 padding string consists of random data before the length.</summary>
		// Token: 0x040023E1 RID: 9185
		ISO10126
	}
}
