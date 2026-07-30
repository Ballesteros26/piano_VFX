using System;

namespace System.Security.Authentication
{
	/// <summary>Specifies the algorithm used for generating message authentication codes (MACs).</summary>
	// Token: 0x0200037D RID: 893
	public enum HashAlgorithmType
	{
		/// <summary>No hashing algorithm is used.</summary>
		// Token: 0x040018B5 RID: 6325
		None,
		/// <summary>The Message Digest 5 (MD5) hashing algorithm.</summary>
		// Token: 0x040018B6 RID: 6326
		Md5 = 32771,
		/// <summary>The Secure Hashing Algorithm (SHA1).</summary>
		// Token: 0x040018B7 RID: 6327
		Sha1,
		// Token: 0x040018B8 RID: 6328
		Sha256 = 32780,
		// Token: 0x040018B9 RID: 6329
		Sha384,
		// Token: 0x040018BA RID: 6330
		Sha512
	}
}
