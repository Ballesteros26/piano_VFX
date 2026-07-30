using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006E RID: 110
	internal class RSAPKCS1SHA256SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000A209 File Offset: 0x00008409
		public RSAPKCS1SHA256SignatureDescription()
			: base("SHA256")
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A216 File Offset: 0x00008416
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA256.Create();
		}
	}
}
