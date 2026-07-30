using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000071 RID: 113
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000A248 File Offset: 0x00008448
		public RSAPKCS1SignatureDescription(string hashAlgorithmName)
		{
			base.KeyAlgorithm = typeof(RSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = hashAlgorithmName;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A2A1 File Offset: 0x000084A1
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoHelpers.CreateFromName(base.DeformatterAlgorithm);
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A2C6 File Offset: 0x000084C6
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoHelpers.CreateFromName(base.FormatterAlgorithm);
			asymmetricSignatureFormatter.SetKey(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x060002C8 RID: 712
		public abstract override HashAlgorithm CreateDigest();
	}
}
