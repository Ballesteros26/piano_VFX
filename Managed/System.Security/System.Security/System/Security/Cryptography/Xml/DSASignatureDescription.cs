using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000052 RID: 82
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00006FF0 File Offset: 0x000051F0
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = typeof(DSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(DSASignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(DSASignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = "SHA1";
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000704D File Offset: 0x0000524D
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoHelpers.CreateFromName(base.DeformatterAlgorithm);
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm("SHA1");
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007071 File Offset: 0x00005271
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoHelpers.CreateFromName(base.FormatterAlgorithm);
			asymmetricSignatureFormatter.SetKey(key);
			asymmetricSignatureFormatter.SetHashAlgorithm("SHA1");
			return asymmetricSignatureFormatter;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007095 File Offset: 0x00005295
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}

		// Token: 0x0400012B RID: 299
		private const string HashAlgorithm = "SHA1";
	}
}
