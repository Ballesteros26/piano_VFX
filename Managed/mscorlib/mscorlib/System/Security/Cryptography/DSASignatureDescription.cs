using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000690 RID: 1680
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060047EE RID: 18414 RVA: 0x00100EEA File Offset: 0x000FF0EA
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
