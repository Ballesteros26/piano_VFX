using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006F RID: 111
	internal class RSAPKCS1SHA384SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000A21D File Offset: 0x0000841D
		public RSAPKCS1SHA384SignatureDescription()
			: base("SHA384")
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A22A File Offset: 0x0000842A
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA384.Create();
		}
	}
}
