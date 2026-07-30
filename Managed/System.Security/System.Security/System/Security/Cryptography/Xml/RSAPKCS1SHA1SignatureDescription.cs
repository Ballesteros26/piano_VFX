using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006D RID: 109
	internal class RSAPKCS1SHA1SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000A1FC File Offset: 0x000083FC
		public RSAPKCS1SHA1SignatureDescription()
			: base("SHA1")
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007095 File Offset: 0x00005295
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}
	}
}
