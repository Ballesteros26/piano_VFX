using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200068B RID: 1675
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x060047E7 RID: 18407 RVA: 0x00100E41 File Offset: 0x000FF041
		protected RSAPKCS1SignatureDescription(string hashAlgorithm, string digestAlgorithm)
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSA";
			base.DigestAlgorithm = digestAlgorithm;
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x00100E78 File Offset: 0x000FF078
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = base.CreateDeformatter(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x00100E8D File Offset: 0x000FF08D
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = base.CreateFormatter(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x040024C8 RID: 9416
		private string _hashAlgorithm;
	}
}
