using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200003D RID: 61
	internal class RSASslSignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000F091 File Offset: 0x0000D291
		public RSASslSignatureDeformatter()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000F099 File Offset: 0x0000D299
		public RSASslSignatureDeformatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this.key == null)
			{
				throw new CryptographicUnexpectedOperationException("The key is a null reference");
			}
			if (this.hash == null)
			{
				throw new CryptographicUnexpectedOperationException("The hash algorithm is a null reference.");
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("The rgbHash parameter is a null reference.");
			}
			return PKCS1.Verify_v15(this.key, this.hash, rgbHash, rgbSignature);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == "MD5SHA1")
			{
				this.hash = new MD5SHA1();
				return;
			}
			this.hash = HashAlgorithm.Create(strName);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F123 File Offset: 0x0000D323
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (!(key is RSA))
			{
				throw new ArgumentException("Specfied key is not an RSA key");
			}
			this.key = key as RSA;
		}

		// Token: 0x04000177 RID: 375
		private RSA key;

		// Token: 0x04000178 RID: 376
		private HashAlgorithm hash;
	}
}
