using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the hashing algorithm that ASP.NET uses for forms authentication and for validating view state data, and for out-of-process session state identification.</summary>
	// Token: 0x0200056D RID: 1389
	public enum MachineKeyValidation
	{
		/// <summary>Specifies that ASP.NET uses the Message Digest 5 (MD5) hashing algorithm. </summary>
		// Token: 0x04002029 RID: 8233
		MD5,
		/// <summary>Specifies that ASP.NET uses the HMACSHA1 hash algorithm.</summary>
		// Token: 0x0400202A RID: 8234
		SHA1,
		/// <summary>Specifies that ASP.NET uses the TripleDES (3DES) encryption algorithm. </summary>
		// Token: 0x0400202B RID: 8235
		TripleDES,
		/// <summary>Specifies that ASP.NET uses the AES (Rijndael) encryption algorithm.</summary>
		// Token: 0x0400202C RID: 8236
		AES,
		/// <summary>Specifies that ASP.NET uses the HMACSHA256 hashing algorithm.  This is the default value.</summary>
		// Token: 0x0400202D RID: 8237
		HMACSHA256,
		/// <summary>Specifies that ASP.NET uses the HMACSHA384 hashing algorithm.</summary>
		// Token: 0x0400202E RID: 8238
		HMACSHA384,
		/// <summary>Specifies that ASP.NET uses the HMACSHA512 hashing algorithm.</summary>
		// Token: 0x0400202F RID: 8239
		HMACSHA512,
		/// <summary>Specifies that ASP.NET uses a custom hashing algorithm. </summary>
		// Token: 0x04002030 RID: 8240
		Custom
	}
}
