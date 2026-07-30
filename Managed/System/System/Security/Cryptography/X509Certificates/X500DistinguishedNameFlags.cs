using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies characteristics of the X.500 distinguished name.</summary>
	// Token: 0x02000399 RID: 921
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		/// <summary>The distinguished name has no special characteristics.</summary>
		// Token: 0x0400190E RID: 6414
		None = 0,
		/// <summary>The distinguished name is reversed.</summary>
		// Token: 0x0400190F RID: 6415
		Reversed = 1,
		/// <summary>The distinguished name uses semicolons.</summary>
		// Token: 0x04001910 RID: 6416
		UseSemicolons = 16,
		/// <summary>The distinguished name does not use the plus sign.</summary>
		// Token: 0x04001911 RID: 6417
		DoNotUsePlusSign = 32,
		/// <summary>The distinguished name does not use quotation marks.</summary>
		// Token: 0x04001912 RID: 6418
		DoNotUseQuotes = 64,
		/// <summary>The distinguished name uses commas.</summary>
		// Token: 0x04001913 RID: 6419
		UseCommas = 128,
		/// <summary>The distinguished name uses the new line character.</summary>
		// Token: 0x04001914 RID: 6420
		UseNewLines = 256,
		/// <summary>The distinguished name uses UTF8 encoding instead of Unicode character encoding.</summary>
		// Token: 0x04001915 RID: 6421
		UseUTF8Encoding = 4096,
		/// <summary>The distinguished name uses T61 encoding.</summary>
		// Token: 0x04001916 RID: 6422
		UseT61Encoding = 8192,
		/// <summary>Forces the distinguished name to encode specific X.500 keys as UTF-8 strings rather than printable Unicode strings. For more information and the list of X.500 keys affected, see the X500NameFlags enumeration.</summary>
		// Token: 0x04001917 RID: 6423
		ForceUTF8Encoding = 16384
	}
}
