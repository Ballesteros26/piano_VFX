using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200003E RID: 62
	internal class RSASslSignatureFormatter : AsymmetricSignatureFormatter
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000F144 File Offset: 0x0000D344
		public RSASslSignatureFormatter()
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F14C File Offset: 0x0000D34C
		public RSASslSignatureFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F15C File Offset: 0x0000D35C
		public override byte[] CreateSignature(byte[] rgbHash)
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
			return PKCS1.Sign_v15(this.key, this.hash, rgbHash);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F1AF File Offset: 0x0000D3AF
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == "MD5SHA1")
			{
				this.hash = new MD5SHA1();
				return;
			}
			this.hash = HashAlgorithm.Create(strName);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F1D6 File Offset: 0x0000D3D6
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (!(key is RSA))
			{
				throw new ArgumentException("Specfied key is not an RSA key");
			}
			this.key = key as RSA;
		}

		// Token: 0x04000179 RID: 377
		private RSA key;

		// Token: 0x0400017A RID: 378
		private HashAlgorithm hash;
	}
}
