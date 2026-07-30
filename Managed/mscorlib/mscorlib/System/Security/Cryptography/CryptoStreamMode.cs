using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the mode of a cryptographic stream.</summary>
	// Token: 0x02000652 RID: 1618
	[ComVisible(true)]
	[Serializable]
	public enum CryptoStreamMode
	{
		/// <summary>Read access to a cryptographic stream.</summary>
		// Token: 0x040023FB RID: 9211
		Read,
		/// <summary>Write access to a cryptographic stream.</summary>
		// Token: 0x040023FC RID: 9212
		Write
	}
}
