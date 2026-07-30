using System;

namespace System.Web.Configuration
{
	/// <summary>Defines the encryption format for storing passwords.</summary>
	// Token: 0x02000565 RID: 1381
	public enum FormsAuthPasswordFormat
	{
		/// <summary>Specifies that passwords are not encrypted. This field is constant.</summary>
		// Token: 0x04002019 RID: 8217
		Clear,
		/// <summary>Specifies that passwords are encrypted using the SHA1 hash algorithm. This field is constant.</summary>
		// Token: 0x0400201A RID: 8218
		SHA1,
		/// <summary>Specifies that passwords are encrypted using the MD5 hash algorithm. This field is constant.</summary>
		// Token: 0x0400201B RID: 8219
		MD5,
		// Token: 0x0400201C RID: 8220
		SHA256,
		// Token: 0x0400201D RID: 8221
		SHA384,
		// Token: 0x0400201E RID: 8222
		SHA512
	}
}
