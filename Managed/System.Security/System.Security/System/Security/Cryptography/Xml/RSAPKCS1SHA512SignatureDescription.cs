using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000070 RID: 112
	internal class RSAPKCS1SHA512SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000A231 File Offset: 0x00008431
		public RSAPKCS1SHA512SignatureDescription()
			: base("SHA512")
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A23E File Offset: 0x0000843E
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA512.Create();
		}
	}
}
